using Checka.Entities;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Checka
{
    public partial class Frete : Form
    {
        List<string> MesesKardex = new List<string>();
        List<string> MesesPagar = new List<string>();
        public Frete()
        {
            InitializeComponent();
        }

        private void PopularTabela()
        {
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                var command = new MySqlCommand($"SELECT idBus, CodRevenda, Central, Codigo, Descricao, ValorFrete, ID, Transportadora, DataEmbarque, Vencimento from checka.principal WHERE Frete = '{FreteText.Text}'", conexao);
                var dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                Tabela.DataSource = dataTable;

                conexao.Close();
            }
            foreach (DataGridViewRow row in Tabela.Rows)
            {
                switch (row.Cells["Codigo"].Value?.ToString())
                {
                    case "5009":
                        row.Cells["Codigo"].Value = "5108";
                        break;
                    case "5106":
                        row.Cells["Codigo"].Value = "5104";
                        break;
                    case "5038":
                        row.Cells["Codigo"].Value = "5105";
                        break;
                }
            }
        }
        private string RemoverNaoNumero(string input)
        {
            if (input[4] == 'R')
            {
                string output = "";
                for (int i = 0; i < input.Length - 3; i++)
                {
                    if (input[i] == '0' && i == 5) continue;
                    if (i == 4) continue;
                    output += input[i];
                }
                return output;
            }
            return new string(input.Where(char.IsDigit).ToArray());
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            //inicializando
            Buscacha.Text = string.Empty;
            RazaoSocialText.Text = string.Empty;
            ValoresText.Text = string.Empty;
            ValoresDoc.Text = string.Empty;
            CodigoProdutoKardex.Text = string.Empty;
            CTEText.Text = string.Empty;
            CtePortal.Text = string.Empty;
            ValorDocPortal.Text = string.Empty;
            PagadorPortal.Text = string.Empty;
            VencimentoPortal.Text = string.Empty;
            RazaoSocialPagarText.Text = string.Empty;
            VencimentoPagar.Text = string.Empty;
            RevendasText.Text = string.Empty;
            MesesKardex.Clear();
            MesesPagar.Clear();
            label17.Text = string.Empty;
            label18.Text = string.Empty;
            ConfTextBox.Text = string.Empty;
            label22.Text = string.Empty;
            PopularTabela();
            var sdiosdafjuojusoaf = Tabela; //teste

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pacote = new ExcelPackage(new FileInfo("C:\\Users\\moniq\\desktop\\Info.xlsx")))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "CUBO"); // Cria planilha
                if (planilha != null)
                {
                    //Começa a buscar valores base
                    List<string> Concss = new List<string>();
                    List<LinhaDeBusca> list = new();
                    string revenda;
                    string codigoProduto;
                    string frete;
                    string CONC = "";
                    try
                    {
                        for (int i = 0; i < Tabela.RowCount - 1; i++)
                        {
                            revenda = Tabela.Rows[i].Cells[1].Value.ToString();
                            codigoProduto = Tabela.Rows[i].Cells[3].Value.ToString();
                            frete = FreteText.Text.Replace("-", "");
                            if (frete[0] == 'R')
                            {
                                frete = frete.Substring(1);
                                if (frete[0] == '0')
                                {
                                    frete = frete.Substring(1);
                                }
                                if (frete.EndsWith("25"))
                                {
                                    string AKHFHIAIHFIASFO = "";
                                    for (int o = 0; o < frete.Length - 2; o++)
                                    {
                                        AKHFHIAIHFIASFO += frete[o];
                                    }
                                    frete = AKHFHIAIHFIASFO;
                                }
                            }
                            CONC =
                                revenda +
                                codigoProduto +
                                frete;
                            Concss.Add(CONC);

                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show("Não foi achada nenhuma informação no portal com este frete.");
                        return;
                    }
                    List<string> Concs = Concss.Distinct().ToList();

                    List<int> LinhasParaPular = new List<int>();
                    for (int i = 0; i < Tabela.RowCount; i++)
                    {
                        if (Tabela.Rows[i].IsNewRow) continue;
                        if (Tabela.Rows[i].Cells[5].Value.ToString() == "0")
                        {
                            LinhasParaPular.Add(int.Parse(Tabela.Rows[i].Cells[6].Value.ToString()));
                        }
                    }

                    List<string> CodigosDeProduto = new List<string>();
                    List<string> Valores = new List<string>();
                    foreach (string conc in Concs)
                    {
                        List<LinhaDeBusca> AuxList = new List<LinhaDeBusca>();

                        for (int i = 1; i < planilha.Dimension.Rows; i++)
                        {
                            if (planilha.Cells[i, 21].Value?.ToString() == conc)
                            {
                                list.Add
                                    (
                                        new LinhaDeBusca
                                        (
                                            planilha.Cells[i, 21].Value?.ToString(),
                                            new List<string>() { planilha.Cells[i, 8].Value?.ToString() },
                                            planilha.Cells[i, 14].Value?.ToString()
                                        )
                                    );
                                CodigosDeProduto.Add(planilha.Cells[i, 4].Value?.ToString());
                                Valores.Add(planilha.Cells[i, 7].Value?.ToString());
                                MesesKardex.Add(planilha.Cells[i, 11].Value?.ToString());
                            }
                        }
                    }



                    var listaSomada = list
                    .GroupBy(x => x.Conc)
                    .Select(g => new LinhaDeBusca
                    {
                        Conc = g.Key,
                        RazaoSocial = g.SelectMany(x => x.RazaoSocial).ToList(),
                        Valor = g.First().Valor,
                        ValorDouble = g.Sum(x => double.TryParse(x.Valor, NumberStyles.Any, CultureInfo.InvariantCulture, out double v) ? v : 0)
                    })
                    .ToList();

                    int maxI = 0;
                    try
                    {
                        ExcelWorksheet planilhaCTE = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "CTE");
                        List<string> ConcCTEEs = new List<string>();
                        for (int i = 1; i < planilhaCTE.Dimension.Rows; i++)
                        {
                            if (i == 376)
                            {

                            }
                            if (planilhaCTE.Cells[i, 1].Value?.ToString() == FreteText.Text)
                            {
                                ConcCTEEs.Add
                                    (
                                        RemoverNaoNumero(planilhaCTE.Cells[i, 18].Value?.ToString())
                                    );
                                ConcCTEEs.Add
                                    (
                                        RemoverNaoNumero(planilhaCTE.Cells[i, 20].Value?.ToString())
                                    );
                            }
                            maxI = i;
                        }
                        List<string> ConcCTEs = ConcCTEEs.Distinct().ToList();
                        int asdojfsa = maxI;

                        ExcelWorksheet planilhaPagar = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "PAGAR");
                        List<string> ValoresDosDocumentos = new List<string>();
                        List<string> CTEs = new List<string>();
                        List<string> RazoesSociaisPagar = new List<string>();
                        List<string> VencimentoPagarList = new List<string>();
                        foreach (var a in ConcCTEs)
                        {
                            for (int i = 1; i < planilhaPagar.Dimension.Rows; i++)
                            {
                                if (planilhaPagar.Cells[i, 22].Value?.ToString() == a)
                                {
                                    if (planilhaPagar.Cells[i, 7].Value?.ToString() != "7" && planilhaPagar.Cells[i, 7].Value?.ToString() != "8")
                                    {
                                        ValoresDosDocumentos.Add(planilhaPagar.Cells[i, 8].Value?.ToString());
                                        CTEs.Add(planilhaPagar.Cells[i, 15].Value?.ToString());
                                        RazoesSociaisPagar.Add(planilhaPagar.Cells[i, 17].Value?.ToString());
                                        VencimentoPagarList.Add(planilhaPagar.Cells[i, 20].Value?.ToString());
                                        MesesPagar.Add(planilhaPagar.Cells[i, 26].Value?.ToString());
                                    }
                                }
                            }
                        }
                        foreach (var a in ValoresDosDocumentos)
                        {
                            ValoresDoc.AppendText(double.Parse(a).ToString("F2") + Environment.NewLine);
                        }
                        foreach (var a in CTEs)
                        {
                            CTEText.AppendText(a + Environment.NewLine);
                        }
                        foreach (var a in RazoesSociaisPagar)
                        {
                            RazaoSocialPagarText.AppendText(a.Split(' ')[0] + ' ' + a.Split(' ')[1] + Environment.NewLine);
                        }
                        foreach (var a in VencimentoPagarList)
                        {
                            VencimentoPagar.AppendText(a.Split(' ')[0] + Environment.NewLine);
                        }

                        List<string> CTE_Portal = new List<string>();
                        List<string> Valor_Doc_Portal = new List<string>();
                        List<string> Pagador_Portal = new List<string>();
                        List<string> Vencimento_Portal = new List<string>();
                        for (int i = 1; i <= planilhaCTE.Dimension.Rows; i++)
                        {
                            if (planilhaCTE.Cells[i, 1].Value?.ToString() == FreteText.Text)
                            {
                                CTE_Portal.Add(planilhaCTE.Cells[i, 6].Value?.ToString());
                                Valor_Doc_Portal.Add(planilhaCTE.Cells[i, 16].Value?.ToString());
                                Pagador_Portal.Add(planilhaCTE.Cells[i, 11].Value?.ToString());
                                Vencimento_Portal.Add(planilhaCTE.Cells[i, 13].Value?.ToString());
                            }
                        }
                        foreach (var a in CTE_Portal)
                        {
                            CtePortal.AppendText(a + Environment.NewLine);
                        }
                        foreach (var a in Valor_Doc_Portal)
                        {
                            ValorDocPortal.AppendText(double.Parse(a).ToString("F2") + Environment.NewLine);
                        }
                        foreach (var a in Pagador_Portal)
                        {
                            if (a.Split(' ').Length < 2)
                            {
                                PagadorPortal.AppendText(a + Environment.NewLine);
                            }
                            else
                            {
                                PagadorPortal.AppendText(a.Split(' ')[0] + " " + a.Split(' ')[1] + Environment.NewLine);
                            }
                        }
                        foreach (var a in Vencimento_Portal)
                        {
                            VencimentoPortal.AppendText(a.Split(' ')[0] + Environment.NewLine);
                        }

                        foreach (var linha in listaSomada)
                        {
                            foreach (var razaosocial in linha.RazaoSocial)
                            {
                                RazaoSocialText.AppendText(razaosocial.Split(' ')[0] + " " + razaosocial.Split(' ')[1] + Environment.NewLine);
                            }
                        }

                        foreach (var a in Valores)
                        {
                            ValoresText.AppendText(double.Parse(a).ToString("F2") + Environment.NewLine);
                        }

                        double SomaDoTextBox = 0.0;
                        foreach (string a in ValoresText.Lines)
                        {
                            if (a == "") continue;
                            SomaDoTextBox += Convert.ToDouble(a);
                        }
                        double SomaDoPortal = 0.0;
                        foreach (string a in ValorDocPortal.Lines)
                        {
                            if (a == "") continue;
                            SomaDoPortal += Convert.ToDouble(a);
                        }
                        double SomaDoPagar = 0.0;
                        foreach (string a in ValoresDoc.Lines)
                        {
                            if (a == "") continue;
                            SomaDoPagar += Convert.ToDouble(a);
                        }

                        ValorText.Text = SomaDoTextBox.ToString("F2");
                        ValorTotalPortal.Text = SomaDoPortal.ToString("F2");
                        SomaValorDoc.Text = SomaDoPagar.ToString("F2");
                        double soma = 0.0;
                        foreach (DataGridViewRow item in Tabela.Rows)
                        {
                            soma += Convert.ToDouble(item.Cells[5].Value);
                        }

                        ValorSomado.Text = soma.ToString("F2");
                        DiferencaKardex.Text = "Diferença: " + (soma - SomaDoTextBox).ToString("F2");
                        DiferencaPortal.Text = "Diferença: " + (soma - SomaDoPortal).ToString("F2");
                        DiferencaPagar.Text = "Diferença: " + (soma - SomaDoPagar).ToString("F2");

                        var allLines = VencimentoPagar.Lines.Concat(VencimentoPortal.Lines).Where(x => !string.IsNullOrEmpty(x));
                        if (allLines.GroupBy(x => x).Count() != 1)
                        {
                            MessageBox.Show("Verificar vencimentos!!!!!!!!!!!!!");
                        }

                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        MessageBox.Show("Erro: Não foi encontrado no cubo.");
                        return;
                    }

                    foreach (var a in CodigosDeProduto)
                    {
                        CodigoProdutoKardex.AppendText(a + Environment.NewLine);
                    }
                }

                List<string> Cargas = new List<string>();
                List<string> Revendas = new List<string>();
                using (MySqlConnection conexao = new MySqlConnection(Auxl.BuscachaStr))
                {
                    conexao.Open();

                    var command = new MySqlCommand($"SELECT Carga, Revenda FROM buscacha.cubo WHERE PortalFrete = '{FreteText.Text}';", conexao);
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                    }
                    else
                    {
                        for (int i = 0; reader.Read(); i++)
                        {
                            Cargas.Add(reader.GetString("Carga"));
                            Revendas.Add(reader.GetInt32("Revenda").ToString());
                        }
                        reader.Close();
                    }
                    reader.Close();
                    var command2 = new MySqlCommand($"SELECT Carga, Revenda FROM buscacha.LimboCubo WHERE PortalFrete = '{FreteText.Text}';", conexao);
                    var reader2 = command2.ExecuteReader();
                    if (!reader2.HasRows)
                    {
                        reader2.Close();
                    }
                    else
                    {
                        for (int i = 0; reader2.Read(); i++)
                        {
                            Cargas.Add(reader2.GetString("Carga"));
                            Revendas.Add(reader2.GetInt32("Revenda").ToString());
                        }
                        reader2.Close();
                    }
                    reader2.Close();

                    var command3 = new MySqlCommand($"SELECT QuemEnviouFrete FROM buscacha.cubo WHERE PortalFrete = '{FreteText.Text}'", conexao);
                    string? QuemEnviouCubo = Convert.ToString(command3.ExecuteScalar());
                    var command4 = new MySqlCommand($"SELECT QuemEnviouFrete FROM buscacha.LimboCubo WHERE PortalFrete = '{FreteText.Text}'", conexao);
                    string? QuemEnviouLimbo = Convert.ToString(command4.ExecuteScalar());

                    if (QuemEnviouCubo != "")
                    {
                        QuemFez.Text = QuemEnviouCubo;
                    }
                    else
                    {
                        QuemFez.Text = QuemEnviouLimbo;
                    }

                    conexao.Close();

                    foreach (string Carga in Cargas)
                    {
                        Buscacha.AppendText(Carga + Environment.NewLine);
                    }
                    foreach (string Revenda in Revendas)
                    {
                        RevendasText.AppendText(Revenda + Environment.NewLine);
                    }
                    for (int i = 0; i < CodigoProdutoKardex.Lines.Length; i++)
                    {
                        label18.Text = "Produtos: " + i;
                    }
                    if (Buscacha.Text == string.Empty)
                    {
                        MessageBox.Show($"Não foi encontrada nenhuma carga com o frete {FreteText.Text}!");
                    }
                }

                if (Buscacha.Text == null)
                {
                    return;
                }

                List<string> IdBusPlural = new List<string>();
                List<string> RevendaPlural = new List<string>();
                List<string> CodigoDeProdutoPlural = new List<string>();
                foreach (DataGridViewRow item in Tabela.Rows)
                {
                    if (item.IsNewRow) break;
                    IdBusPlural.Add(item.Cells[0].Value?.ToString());
                    RevendaPlural.Add(item.Cells[1].Value?.ToString());
                    CodigoDeProdutoPlural.Add(item.Cells[3].Value?.ToString());
                }

                List<BuscachaAUX> ListaBuscacha = new List<BuscachaAUX>();
                List<string> CargasBuscacha = new List<string>();
                foreach (string linha in Buscacha.Lines)
                {
                    ListaBuscacha.Add(new BuscachaAUX(linha));
                    CargasBuscacha.Add(linha);
                }
                List<string> RevendasBuscacha = new List<string>();
                foreach (string linha in RevendasText.Lines)
                {
                    RevendasBuscacha.Add(linha);
                }
                List<string> CodigosProdutoKardexList = new List<string>();
                foreach (string linha in CodigoProdutoKardex.Lines)
                {
                    CodigosProdutoKardexList.Add(linha);
                }

                var intersect1 = IdBusPlural.Intersect(CargasBuscacha).ToHashSet();
                var intersect2 = RevendaPlural.Intersect(RevendasBuscacha).ToHashSet();
                var intersect3 = CodigoDeProdutoPlural.Intersect(CodigosProdutoKardexList).ToHashSet();
                //Seta a lista de valores com base no TextBox "ValoresText" em um array de string.
                string[] ValoresList = new string[ValoresText.Lines.Length];
                for (int i = 0; i < ValoresText.Lines.Length; i++)
                {
                    ValoresList[i] = ValoresText.Lines[i].Split(',')[0];
                }
                int Contador = 0;
                bool Continuar = true;

                foreach (DataGridViewRow row in Tabela.Rows)
                {
                    if (row.IsNewRow) continue;

                    // Verificar cada coluna e pintar apenas a célula correspondente
                    if (intersect1.Contains(row.Cells[0].Value?.ToString()))
                    {
                        row.Cells[0].Style.BackColor = Color.LightGreen;
                    }
                    if (intersect2.Contains(row.Cells[1].Value?.ToString()))
                    {
                        row.Cells[1].Style.BackColor = Color.LightGreen;
                    }
                    if (intersect3.Contains(row.Cells[3].Value?.ToString()))
                    {
                        row.Cells[3].Style.BackColor = Color.LightGreen;
                    }
                    if (row.Cells[5].Value?.ToString() == "0")
                    {
                        row.Cells[3].Style.BackColor = Color.LightBlue;
                        row.Cells[5].Style.BackColor = Color.LightBlue;
                    }
                    try
                    {
                        var ftyytf = ValoresList[Contador];
                        if (ValoresList[Contador] == row.Cells[5].Value?.ToString().Split(',')[0] && Continuar)
                        {
                            row.Cells[5].Style.BackColor = Color.LightGreen;
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Continuar = false;
                    }
                    if (row.Cells[5].Value?.ToString() != "0")
                    {
                        Contador++;
                    }
                }
                foreach (var linha in Buscacha.Lines)
                {
                    foreach(var item in ListaBuscacha)
                    {
                        if(linha == item.Carga)
                        {
                            var aux = Tabela.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[0].Value?.ToString() == item.Carga).Distinct().Select(x => x.Cells[0].Value?.ToString());
                            var CargasEmComum = ListaBuscacha.Select(obj => obj.Carga).Where(carga => aux.Contains(carga)).ToList();
                            foreach(var a in CargasEmComum)
                            {
                                if (Buscacha.Lines.Contains(a))
                                {
                                    ConfTextBox.Text += "OK" + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                label22.Text = "Diferenças: " + (Buscacha.Lines.Length - ConfTextBox.Lines.Length);

                // Após percorrer a lista, marcar as células que ficaram sem correspondência
                foreach (DataGridViewRow row in Tabela.Rows)
                {
                    if (row.IsNewRow) continue;

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Style.BackColor != Color.LightGreen)
                        {
                            if (i == 0 || i == 1)
                            {
                                row.Cells[i].Style.BackColor = Color.Red;
                                row.Cells[i].Style.ForeColor = Color.LightGoldenrodYellow;
                            }
                            if (!(row.Cells[3].Style.BackColor == Color.LightBlue) && i == 3)
                            {
                                row.Cells[i].Style.BackColor = Color.Red;
                                row.Cells[i].Style.ForeColor = Color.LightGoldenrodYellow;
                            }
                            if (!(row.Cells[5].Style.BackColor == Color.LightBlue) && i == 5)
                            {
                                row.Cells[i].Style.BackColor = Color.Red;
                                row.Cells[i].Style.ForeColor = Color.LightGoldenrodYellow;
                            }
                        }

                    }
                }
                List<string> CodigosDeProdutoSomar = new List<string>();
                for (int i = 0; i < Tabela.Rows.Count; i++)
                {
                    if (Tabela.Rows[i].Cells[3].Style.BackColor == Color.LightGreen)
                    {
                        CodigosDeProdutoSomar.Add(Tabela.Rows[i].Cells[3].Value.ToString());
                    }
                }

                int ContagemProdutos = CodigosDeProdutoSomar.Distinct().ToList().Count;
                label17.Text = "Produtos: " + ContagemProdutos;
                var ConcatMeses = MesesPagar.Concat(MesesKardex);
                bool TemApenasUm = ConcatMeses.Where(x => !string.IsNullOrEmpty(x)).GroupBy(x => x).Count() == 1;

                if (!TemApenasUm)
                {
                    MessageBox.Show("Verificar mês de emissão");
                }

                textBox1.Text = Tabela.Rows[0].Cells["Transportadora"].Value?.ToString();
                textBox2.Text = Tabela.Rows[0].Cells["DataEmbarque"].Value?.ToString().Split(' ')[0];
                textBox3.Text = Tabela.Rows[0].Cells["Vencimento"].Value?.ToString().Split(' ')[0];
                Tabela.ClearSelection();

                if(SomaValorDoc.Text != ValorText.Text)
                {
                    MessageBox.Show("Diferença no Control!!!!");
                }
            }
        }
        private void Frete_Load(object sender, EventArgs e)
        {

        }

        private void CTEText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                foreach (DataGridViewRow linha in Tabela.Rows)
                {
                    if (linha.IsNewRow) continue;
                    if (linha.Cells[0].Style.BackColor == Color.LightGreen && linha.Cells[1].Style.BackColor == Color.LightGreen && (linha.Cells[3].Style.BackColor == Color.LightGreen || linha.Cells[3].Style.BackColor == Color.LightBlue))
                    {
                        new MySqlCommand($"UPDATE checka.principal SET Checkado = 'OK' WHERE ID = {linha.Cells[6].Value}", conexao).ExecuteNonQuery();
                    }
                }

                conexao.Close();
                MessageBox.Show("Enviado!");
            }
            Buscacha.Text = string.Empty;
            RazaoSocialText.Text = string.Empty;
            ValoresText.Text = string.Empty;
            ValoresDoc.Text = string.Empty;
            CodigoProdutoKardex.Text = string.Empty;
            CTEText.Text = string.Empty;
            CtePortal.Text = string.Empty;
            ValorDocPortal.Text = string.Empty;
            PagadorPortal.Text = string.Empty;
            VencimentoPortal.Text = string.Empty;
            RazaoSocialPagarText.Text = string.Empty;
            VencimentoPagar.Text = string.Empty;
            RevendasText.Text = string.Empty;
            MesesKardex.Clear();
            MesesPagar.Clear();
            Tabela.DataSource = null;
            DiferencaKardex.Text = "Diferença: ";
            DiferencaPortal.Text = "Diferença: ";
            DiferencaPagar.Text = "Diferença: ";
            ValorSomado.Text = string.Empty;
            ValorTotalPortal.Text = string.Empty;
            SomaValorDoc.Text = string.Empty;
            ValorText.Text = string.Empty;
            QuemFez.Text = string.Empty;
            FreteText.Text = string.Empty;
            label17.Text = string.Empty;
            label18.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            ConfTextBox.Text = string.Empty;
            label22.Text = string.Empty;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RazaoSocialPagarText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
