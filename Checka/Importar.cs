using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checka
{
    public partial class Importar : Form
    {
        public static string[,] Matriz;
        int CountLinhas;
        int CountColunas;
        public Importar()
        {
            InitializeComponent();
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage pacote = new ExcelPackage(new FileInfo(textBox1.Text)))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "PORTAL");
                if (planilha != null)
                {
                    int linhas = planilha.Dimension.Rows;
                    CountLinhas = linhas;
                    int colunas = planilha.Dimension.Columns;
                    CountColunas = colunas;

                    Matriz = new string[linhas + 1, colunas + 1];

                    for (int i = 1; i <= linhas; i++)
                    {
                        for (int j = 1; j <= colunas; j++)
                        {
                            if (planilha.Cells[i, j].Value == null) continue;

                            Matriz[i, j] = planilha.Cells[i, j].Value.ToString();
                        }
                    }
                }
            }
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                if (checkBox1.Checked)
                {
                    var reader = new MySqlCommand($"SELECT Carga FROM checka.principal WHERE Checkado = 'OK';", conexao).ExecuteReader();
                    reader.Read();
                    bool ThereIsRows = reader.HasRows;
                    reader.Close();
                    if (ThereIsRows)
                    {
                        MessageBox.Show("Tem OK!!!");
                        conexao.Close();
                        return;
                    }
                    
                    new MySqlCommand($"DELETE FROM checka.principal WHERE ID > 1;", conexao).ExecuteNonQuery();
                }

                for (int i = 1; i <= CountLinhas; i++)
                {
                    if (Matriz[i, 1] == "0")
                    {
                        break;
                    }
                    string[] data1AUX = Matriz[i, 2].Split('/');
                    string DIA1 = data1AUX[0];
                    string MES1 = data1AUX[1];
                    string ANO1 = data1AUX[2].Split(' ')[0];
                    if (DIA1.Length == 1)
                    {
                        string aux = DIA1;
                        DIA1 = "0" + DIA1;
                    }
                    if (MES1.Length == 1)
                    {
                        string aux = MES1;
                        MES1 = "0" + MES1;
                    }
                    if (ANO1.Length == 1)
                    {
                        string aux = ANO1;
                        ANO1 = "0" + ANO1;
                    }

                    string data1AUX2 = $"{DIA1}/{MES1}/{ANO1}";
                    string data1;
                    try
                    {
                        data1 = DateTime.ParseExact(data1AUX2, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    }
                    catch(System.FormatException ex)
                    {
                        continue;
                    }

                    string[] data2AUX = Matriz[i, 5].Split('/');
                    string DIA2 = data2AUX[0];
                    string MES2 = data2AUX[1];
                    string ANO2 = data2AUX[2].Split(' ')[0];
                    if (DIA2.Length == 1)
                    {
                        string aux = DIA2;
                        DIA2 = "0" + DIA2;
                    }
                    if (MES2.Length == 1)
                    {
                        string aux = MES2;
                        MES2 = "0" + MES2;
                    }
                    if (ANO2.Length == 1)
                    {
                        string aux = ANO2;
                        ANO2 = "0" + ANO2;
                    }
                    string data2AUX2 = $"{DIA2}/{MES2}/{ANO2}";
                    string data2 = DateTime.ParseExact(data2AUX2, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                    var command = new MySqlCommand($"INSERT INTO checka.principal " +
                        $"(idBus, DataEmbarque, Frete, Transportadora, Vencimento, Carga, Faturamento, CodRevenda, Central, Codigo, Descricao, PLTS, Quantidade, ValorFrete, Prazo, Observacoes, Anotacoes) " +
                        $"VALUES " +
                        $"('{Matriz[i, 1]}', '{data1}', '{Matriz[i, 3]}', '{Matriz[i, 4]}', '{data2}', '{Matriz[i, 6]}', '{Matriz[i, 7]}', '{Matriz[i, 8]}', '{Matriz[i, 9]}', '{Matriz[i, 10]}', '{Matriz[i, 11]}', '{Matriz[i, 12]}', '{Matriz[i, 13]}', '{Matriz[i, 14]}', '{Matriz[i, 15]}', '{Matriz[i, 16]}', '{Matriz[i, 17]}');", conexao);

                    command.ExecuteNonQuery();
                }

                conexao.Close();
            }
            MessageBox.Show("CONCLUIDO");
        }
    }
}
