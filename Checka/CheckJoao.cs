using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Checka
{
    public partial class CheckJoao : Form
    {
        private void AtualizarDiferencasEDados()
        {
            // Garante que as colunas "Diferenca" existem nos DataTables
            DataTable dt1 = (DataTable)dataGridView1.DataSource;
            DataTable dt2 = (DataTable)dataGridView2.DataSource;

            if (!dt1.Columns.Contains("Diferenca"))
                dt1.Columns.Add("Diferenca", typeof(string));

            if (!dt2.Columns.Contains("Diferenca"))
                dt2.Columns.Add("Diferenca", typeof(string));

            // Preenche "Diferenca" no dataGridView2
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                double valorA = TryParseDouble(row.Cells["TotalValor"].Value);
                double valorB = 0.0;

                foreach (DataGridViewRow row2 in dataGridView1.Rows)
                {
                    if (row2.IsNewRow) continue;

                    if (row2.Cells["Frete"].Value?.ToString() == row.Cells["Frete"].Value?.ToString())
                    {
                        valorB = TryParseDouble(row2.Cells["TOTAL_VALOR_FRETE"].Value);
                        break;
                    }
                }

                row.Cells["Diferenca"].Value = (valorA - valorB).ToString("F2");
            }

            // Preenche "Diferenca" no dataGridView1
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                double valorA = TryParseDouble(row.Cells["TOTAL_VALOR_FRETE"].Value);
                double valorB = 0.0;

                foreach (DataGridViewRow row2 in dataGridView2.Rows)
                {
                    if (row2.IsNewRow) continue;

                    if (row2.Cells["Frete"].Value?.ToString() == row.Cells["Frete"].Value?.ToString())
                    {
                        valorB = TryParseDouble(row2.Cells["TotalValor"].Value);
                        break;
                    }
                }

                row.Cells["Diferenca"].Value = (valorA - valorB).ToString("F2");
            }

            // Atualiza contadores
            label3.Text = "Linhas: " + (dataGridView1.RowCount - 1);
            label4.Text = "Linhas: " + (dataGridView2.RowCount - 1);

            // Totais
            double soma1 = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Sum(r => TryParseDouble(r.Cells["TOTAL_VALOR_FRETE"].Value));

            TotalPortal.Text = "Total: " + soma1.ToString("F2");

            double soma2 = dataGridView2.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Sum(r => TryParseDouble(r.Cells["TotalValor"].Value));

            TotalJoao.Text = "Total: " + soma2.ToString("F2");

            Diff.Text = "Diferença: " + (soma1 - soma2).ToString("F2");
        }

        // Função auxiliar para parse seguro de double
        private double TryParseDouble(object value)
        {
            return double.TryParse(value?.ToString()?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double result)
                ? result : 0.0;
        }
        public CheckJoao()
        {
            InitializeComponent();
        }

        private void Data_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = !monthCalendar1.Visible;
            monthCalendar2.Visible = !monthCalendar2.Visible;
            BuscarData.Visible = !BuscarData.Visible;
        }

        private void BuscarData_Click(object sender, EventArgs e)
        {
            SearchBox.Text = string.Empty;
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                var command = new MySqlCommand(@$"
                    SELECT
                        Frete,
                        SUM(CAST(REPLACE(ValorFrete, ',', '.') AS DECIMAL(10,2))) AS TOTAL_VALOR_FRETE,
                        MAX(Validado) AS Validado
                    FROM (
                        SELECT Frete, ValorFrete, Validado FROM checka.principal
                        WHERE DataEmbarque BETWEEN '{monthCalendar1.SelectionStart:yyyy-MM-dd}' AND '{monthCalendar2.SelectionStart:yyyy-MM-dd}' AND TRANSPORTADORA IN (
                            'VIA APPIA',
                            'VIA APPIA 3',
                            'VARSOVIA MG',
                            'VARSOVIA RJ'
                        )

                        UNION ALL

                        SELECT Frete, ValorFrete, Validado FROM checka.arquivo
                        WHERE DataEmbarque BETWEEN '{monthCalendar1.SelectionStart:yyyy-MM-dd}' AND '{monthCalendar2.SelectionStart:yyyy-MM-dd}' AND TRANSPORTADORA IN (
                            'VIA APPIA',
                            'VIA APPIA 3',
                            'VARSOVIA MG',
                            'VARSOVIA RJ'
                        )
                    ) AS combinados
                    GROUP BY Frete;", conexao);

                var dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                SearchBox.Text = string.Empty;

                conexao.Close();
            }
            AtualizarDiferencasEDados();
            Data_Click(sender, e);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            SearchBox.Text = string.Empty;
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                var command = new MySqlCommand(@$"
                    SELECT
                        Frete,
                        SUM(CAST(REPLACE(ValorFrete, ',', '.') AS DECIMAL(10,2))) AS TOTAL_VALOR_FRETE,
                        MAX(Validado) AS Validado
                    FROM (
                        SELECT Frete, ValorFrete, Validado FROM checka.principal
                        WHERE TRANSPORTADORA IN (
                            'VIA APPIA',
                            'VIA APPIA 3',
                            'VARSOVIA MG',
                            'VARSOVIA RJ'
                        )

                        UNION ALL

                        SELECT Frete, ValorFrete, Validado FROM checka.arquivo
                        WHERE TRANSPORTADORA IN (
                            'VIA APPIA',
                            'VIA APPIA 3',
                            'VARSOVIA MG',
                            'VARSOVIA RJ'
                        )
                    ) AS combinados
                    GROUP BY Frete;", conexao);

                var dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                var command2 = new MySqlCommand(
                    @$"
                          SELECT
                          Frete,
                          SUM(Total) AS TotalValor,
                          GROUP_CONCAT(ID_OK ORDER BY ID_OK SEPARATOR ', ') AS IDs
                        FROM
                          checka.check_joao
                        GROUP BY
                          Frete", conexao);
                var dataAdapter2 = new MySqlDataAdapter(command2);
                DataTable dataTable2 = new DataTable();
                dataAdapter2.Fill(dataTable2);
                dataGridView2.DataSource = dataTable2;

                conexao.Close();
            }

            AtualizarDiferencasEDados();
        }

        private void CheckJoao_Load(object sender, EventArgs e)
        {
            Refresh_Click(sender, e);
            AtualizarDiferencasEDados();
        }

        private void LoadJoao_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage pacote = new ExcelPackage(new FileInfo("C:\\Users\\moniq\\desktop\\Info.xlsx")))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "CHECK J");

                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();

                    new MySqlCommand($"DELETE FROM checka.check_joao WHERE ID > 0", conexao).ExecuteNonQuery();

                    for (int i = 2; i < planilha.Dimension.Rows + 1; i++)
                    {
                        if (planilha.Cells[i, 21].Value?.ToString().ToLower() == "x")
                        {
                            break;
                        }
                        string supply1 = planilha.Cells[i, 21].Value?.ToString();
                        string supply2 = supply1?.Replace(',', '.');
                        double Total = double.TryParse(supply2, NumberStyles.Any, CultureInfo.InvariantCulture, out double valorFinal)
                        ? valorFinal
                        : 0.0;
                        Total = Math.Round(Total, 2);
                        string ID_OK = planilha.Cells[i, 28].Value?.ToString();
                        string frete = planilha.Cells[i, 29].Value?.ToString();
                        string obs = planilha.Cells[i, 30].Value?.ToString();

                        MySqlCommand comando = new MySqlCommand($"INSERT INTO checka.check_joao (Total, ID_OK, Frete, OBS) VALUES (@Total, @id_ok, @Frete, @OBS);", conexao);
                        comando.Parameters.AddWithValue("@Total", Total);
                        comando.Parameters.AddWithValue("@id_ok", ID_OK);
                        comando.Parameters.AddWithValue("@Frete", frete);
                        comando.Parameters.AddWithValue("@OBS", obs);

                        comando.ExecuteNonQuery();
                    }

                    conexao.Close();
                }
            }
            MessageBox.Show("INFORMAÇÕES CARREGADAS!");
            Refresh_Click(sender, e);
        }

        private void Pesquisar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();
                if (1 + 1 == 2)
                {
                    var dataAdapter = new MySqlDataAdapter($@"
                        SELECT * FROM (
                            SELECT 
                                Frete,
                                SUM(Total) AS TotalValor,
                                GROUP_CONCAT(ID_OK ORDER BY ID_OK SEPARATOR ', ') AS IDs
                            FROM checka.check_joao
                            GROUP BY Frete
                        ) AS sub
                        WHERE Frete LIKE '%{SearchBox.Text}%'
                           OR TotalValor LIKE '%{SearchBox.Text}%'
                           OR IDs LIKE '%{SearchBox.Text}%';
                    ", conexao);

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView2.DataSource = dataTable;
                }
                if (2 - 1 == 1)
                {
                    var dataAdapter = new MySqlDataAdapter($@"
                        SELECT * FROM (
                            SELECT
                                Frete,
                                SUM(CAST(REPLACE(ValorFrete, ',', '.') AS DECIMAL(10,2))) AS TOTAL_VALOR_FRETE,
                                MAX(Validado) AS Validado
                            FROM (
                                SELECT Frete, ValorFrete, Validado FROM checka.principal
                                WHERE TRANSPORTADORA IN (
                                    'VIA APPIA',
                                    'VIA APPIA 3',
                                    'VARSOVIA MG',
                                    'VARSOVIA RJ'
                                )
                                UNION ALL
                                SELECT Frete, ValorFrete, Validado FROM checka.arquivo
                                WHERE TRANSPORTADORA IN (
                                    'VIA APPIA',
                                    'VIA APPIA 3',
                                    'VARSOVIA MG',
                                    'VARSOVIA RJ'
                                )
                            ) AS combinados
                            GROUP BY Frete
                        ) AS sub
                        WHERE Frete LIKE '%{SearchBox.Text}%' 
                           OR TOTAL_VALOR_FRETE LIKE '%{SearchBox.Text}%';", conexao);

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                conexao.Close();
            }
            AtualizarDiferencasEDados();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            AtualizarDiferencasEDados();
        }

        private void dataGridView2_Sorted(object sender, EventArgs e)
        {
            AtualizarDiferencasEDados();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Validado"].Value?.ToString() == "OK")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(79, 255, 79);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 79, 79);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
