using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System.Data;
using System.Globalization;

namespace Checka
{
    public partial class BSoft : Form
    {
        List<string> CTEs = new List<string>();
        decimal soma1 = 0, soma2 = 0;
        List<int> IDs = new List<int>();
        DataGridView CRBsoftDatagridSave = new DataGridView();
        List<DataGridViewRow> ListaRows = new List<DataGridViewRow>();
        List<object[]> linhasPlanilhaCTE = new List<object[]>();
        public BSoft()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
            using (ExcelPackage pacote = new ExcelPackage(new FileInfo("C:\\Users\\moniq\\desktop\\Info.xlsx")))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "CTE");
                int totalColunas = planilha.Dimension.End.Column;

                for (int i = 2; i <= planilha.Dimension.End.Row; i++)
                {
                    object[] linha = new object[totalColunas + 1]; // +1 pois as colunas começam em 1
                    for (int j = 1; j <= totalColunas; j++)
                    {
                        linha[j] = planilha.Cells[i, j].Value;
                    }
                    linhasPlanilhaCTE.Add(linha);
                }
            }
        }

        private void PortalDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BuscarDescontos()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pacote = new ExcelPackage(new FileInfo("C:\\Users\\moniq\\desktop\\Info.xlsx")))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "CTE");
                foreach (DataGridViewRow row in BSoftDataGrid.Rows)
                {
                    if (row.IsNewRow) continue;
                    for (int i = 1; i < planilha.Dimension.Rows; i++)
                    {
                        if (planilha.Cells[i, 6].Value?.ToString() == row.Cells[1].Value?.ToString())
                        {
                            if (planilha.Cells[i, 15].Value?.ToString() != "0")
                            {
                                CTEs.Add(planilha.Cells[i, 6].Value?.ToString());
                                Descontos.Text += $"{row.Cells[1].Value?.ToString()} - RS${planilha.Cells[i, 15].Value?.ToString()}" + Environment.NewLine;
                            }
                        }
                    }
                }
            }
        }

        public void PintarPorCTE()
        {
            foreach (DataGridViewRow rows in BSoftDataGrid.Rows)
            {
                foreach (string cte in CTEs)
                {
                    if (rows.Cells[1].Value?.ToString().GetHashCode() == cte.GetHashCode())
                    {
                        if ((bool)rows.Cells[1].Value?.ToString().Equals(cte))
                        {
                            rows.Cells[1].Style.BackColor = Color.LightYellow;
                        }
                    }
                }
            }
        }

        private void RefreshBsoft_Click(object sender, EventArgs e)
        {
            DateTime contagemtempo1 = DateTime.Now;
            //Console.Clear();
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                var command = new MySqlCommand();
                if (sender.ToString() == "A")
                {
                    command = new MySqlCommand("SELECT * FROM checka.crbsoft LIMIT 30", conexao);
                }
                else
                {
                    command = new MySqlCommand("SELECT * FROM checka.crbsoft", conexao);
                }

                var dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                conexao.Close();

                // 1. Cria o BindingSource
                BindingSource bs = new BindingSource();
                bs.DataSource = dataTable;

                // 2. Configura o BSoftDataGrid
                BSoftDataGrid.AutoGenerateColumns = false;
                BSoftDataGrid.Columns.Clear();

                // 3. Cria as colunas com filtro
                foreach (DataColumn col in dataTable.Columns)
                {
                    var colFiltro = new DataGridViewAutoFilterTextBoxColumn
                    {
                        DataPropertyName = col.ColumnName,
                        HeaderText = col.ColumnName,
                        Name = col.ColumnName,
                        SortMode = DataGridViewColumnSortMode.Programmatic
                    };

                    BSoftDataGrid.Columns.Add(colFiltro);
                }

                if (!dataTable.Columns.Contains("ValorCTE"))
                {
                    var colFiltro = new DataGridViewAutoFilterTextBoxColumn
                    {
                        DataPropertyName = "ValorCTE",
                        HeaderText = "ValorCTE",
                        Name = "ValorCTE",
                        SortMode = DataGridViewColumnSortMode.Programmatic
                    };
                    BSoftDataGrid.Columns.Add(colFiltro);
                }

                // 4. Atribui a fonte de dados
                BSoftDataGrid.DataSource = bs;

                Dictionary<string, double> keyValues = new Dictionary<string, double>();

                if (BSoftDataGrid.Columns.Contains("ValorCTE"))
                {
                    foreach (DataGridViewRow row in BSoftDataGrid.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string cte = row.Cells["IdBus"].Value?.ToString();
                        if (!keyValues.ContainsKey(cte))
                        {
                            keyValues.Add(cte, 0);
                            //Console.Write($"{cte}; ");
                        }
                    }

                    //Console.WriteLine();

                    int contagem = 1;
                    foreach (object[] linha in linhasPlanilhaCTE)
                    {
                        //Console.WriteLine("Linha " + contagem + ": CTE DA LINHA: " + linha[25].ToString());
                        string ctePlanilha = linha[25]?.ToString();
                        if (!string.IsNullOrEmpty(ctePlanilha) && keyValues.ContainsKey(ctePlanilha))
                        {
                            string valorString = linha[16]?.ToString();
                            double valor = 0;
                            double.TryParse(
                                valorString?.Replace(".", "").Replace(",", "."),
                                NumberStyles.Any,
                                CultureInfo.InvariantCulture,
                                out valor
                            );

                            keyValues[ctePlanilha] += valor;
                        }
                        contagem++;
                    }

                    Console.WriteLine();

                    contagem = 1;
                    int contagemLinhas = BSoftDataGrid.RowCount;

                    // Salva os estados atuais
                    var antigoAutoSize = BSoftDataGrid.AutoSizeColumnsMode;
                    bool antigoEnabled = BSoftDataGrid.Enabled;

                    // Desativa atualizações visuais e interações
                    BSoftDataGrid.Enabled = false;
                    BSoftDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    BSoftDataGrid.SuspendLayout();

                    foreach (DataGridViewRow row in BSoftDataGrid.Rows)
                    {
                        try
                        {
                            if (row.IsNewRow) continue;
                            string cte = row.Cells["IdBus"].Value?.ToString();
                            //Console.WriteLine(contagem + " - " + cte + "; LEFT: " + (contagemLinhas - contagem));
                            row.Cells["ValorCTE"].Value = keyValues[cte];
                            contagem++;
                        }
                        catch (KeyNotFoundException)
                        {
                            continue;
                        }
                    }

                    // Retoma os estados antigos
                    BSoftDataGrid.ResumeLayout();
                    BSoftDataGrid.AutoSizeColumnsMode = antigoAutoSize;
                    BSoftDataGrid.Enabled = antigoEnabled;

                }
            }
            BSoftDataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            Descontos.Text = string.Empty;
            CopiarDataGrid(BSoftDataGrid, CRBsoftDatagridSave);
            BuscarDescontos();
            DateTime contagemtempo2 = DateTime.Now;
            TimeSpan timeSpan = contagemtempo2 - contagemtempo1;
            string timeSpanToString = timeSpan.Hours + ":" + timeSpan.Minutes + ":" + timeSpan.Seconds + ":" + timeSpan.Milliseconds + ":" + timeSpan.Nanoseconds;
            Console.WriteLine("TEMPO PASSADO: " + timeSpanToString);
        }

        private void RefreshAcerto_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                var command = new MySqlCommand();
                if (sender.ToString() == "A")
                {
                    command = new MySqlCommand("SELECT Posicao, NumeroDocumento AS Doc, Total, Pagador, Empresa, ID FROM checka.acertobsoft LIMIT 30", conexao);
                }
                else
                {
                    command = new MySqlCommand("SELECT Posicao, NumeroDocumento AS Doc, Total, Pagador, Empresa, ID FROM checka.acertobsoft", conexao);
                }
                var dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                AcertoDataGrid.DataSource = dataTable;

                conexao.Close();
            }
        }

        private void RefreshPortal_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                var command = new MySqlCommand();
                if (sender.ToString() == "A")
                {
                    command = new MySqlCommand(@"
                        SELECT 
                            MAX(FRETE) AS FRETE,
                            Idbus,
                            MAX(ID) AS ID,
                            MAX(`DATAEMBARQUE`) AS `DATAEMBARQUE`,
                            MAX(TRANSPORTADORA) AS TRANSPORTADORA,
                            MAX(VENCIMENTO) AS VENCIMENTO,
                            MAX(FATURAMENTO) AS FATURAMENTO,
                            MAX(`CODREVENDA`) AS `CODREVENDA`,
                            MAX(CENTRAL) AS CENTRAL,
                            SUM(CAST(REPLACE(`VALORFRETE`, ',', '.') AS DECIMAL(10,2))) AS `TOTAL_VALOR_FRETE`,
                            MAX(`OBSERVACOES`) AS `OBSERVACOES`,
                            MAX(`ANOTACOES`) AS `ANOTACOES`
                        FROM (
                            SELECT * FROM checka.principal
                            WHERE TRANSPORTADORA IN (
                                'VIA APPIA',
                                'VIA APPIA 3',
                                'VARSOVIA MG',
                                'VARSOVIA RJ'
                            )
    
                            UNION ALL

                            SELECT * FROM checka.arquivo
                            WHERE TRANSPORTADORA IN (
                                'VIA APPIA',
                                'VIA APPIA 3',
                                'VARSOVIA MG',
                                'VARSOVIA RJ'
                            )
                        ) AS combinados
                        GROUP BY Idbus
                        LIMIT 30", conexao);

                }
                else
                {
                    command = new MySqlCommand(@"
                        SELECT 
                            MAX(FRETE) AS FRETE,
                            Idbus,
                            MAX(ID) AS ID,
                            MAX(`DATAEMBARQUE`) AS `DATAEMBARQUE`,
                            MAX(TRANSPORTADORA) AS TRANSPORTADORA,
                            MAX(VENCIMENTO) AS VENCIMENTO,
                            MAX(FATURAMENTO) AS FATURAMENTO,
                            MAX(`CODREVENDA`) AS `CODREVENDA`,
                            MAX(CENTRAL) AS CENTRAL,
                            SUM(CAST(REPLACE(`VALORFRETE`, ',', '.') AS DECIMAL(10,2))) AS `TOTAL_VALOR_FRETE`,
                            MAX(`OBSERVACOES`) AS `OBSERVACOES`,
                            MAX(`ANOTACOES`) AS `ANOTACOES`
                        FROM (
                            SELECT * FROM checka.principal
                            WHERE TRANSPORTADORA IN (
                                'VIA APPIA',
                                'VIA APPIA 3',
                                'VARSOVIA MG',
                                'VARSOVIA RJ'
                            )
    
                            UNION ALL

                            SELECT * FROM checka.arquivo
                            WHERE TRANSPORTADORA IN (
                                'VIA APPIA',
                                'VIA APPIA 3',
                                'VARSOVIA MG',
                                'VARSOVIA RJ'
                            )
                        ) AS combinados
                        GROUP BY Idbus", conexao);
                }

                var dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                PortalDataGrid.DataSource = dataTable;

                conexao.Close();
            }
        }

        private void BSoft_Load(object sender, EventArgs e)
        {
            CTEs.Clear();
            RefreshBsoft_Click("A", e);
            RefreshAcerto_Click("A", e);
            RefreshPortal_Click("A", e);
            BSoftDataGrid.ColumnHeaderMouseClick += BSoftDataGrid_ColumnHeaderMouseClick;

            BSoftDataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            AcertoDataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            AcertoDataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            PortalDataGrid.Columns[1].Visible = checkBoxCarga.Checked;
            PortalDataGrid.Columns[2].Visible = checkBoxID.Checked;
            PortalDataGrid.Columns[3].Visible = checkBoxDtEmbarque.Checked;
            PortalDataGrid.Columns[0].Visible = checkBoxFrete.Checked;
            PortalDataGrid.Columns[4].Visible = checkBoxTransportadora.Checked;
            PortalDataGrid.Columns[5].Visible = checkBoxVencimento.Checked;
            PortalDataGrid.Columns[6].Visible = checkBoxFaturamento.Checked;
            PortalDataGrid.Columns[7].Visible = checkBoxCodRevenda.Checked;
            PortalDataGrid.Columns[8].Visible = checkBoxCentral.Checked;
            PortalDataGrid.Columns[9].Visible = checkBoxTotalFrete.Checked;
            PortalDataGrid.Columns[10].Visible = checkBoxObs.Checked;
            PortalDataGrid.Columns[11].Visible = checkBoxAno.Checked;


        }

        private void Pesquisar_Click(object sender, EventArgs e)
        {
            if (CheckCRBsoft.Checked)
            {
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();
                    string columnQuery = @"
                SELECT COLUMN_NAME 
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_SCHEMA = 'checka' 
                AND TABLE_NAME = 'crbsoft' 
                ORDER BY ORDINAL_POSITION";

                    MySqlCommand cmd = new MySqlCommand(columnQuery, conexao);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    string columns = "";
                    while (reader.Read())
                    {
                        if (columns.Length > 0)
                        {
                            columns += "OR ";
                        }
                        columns += reader["COLUMN_NAME"].ToString() + $" LIKE '%{textBox1.Text}%' ";
                    }
                    reader.Close();

                    var dataAdapter = new MySqlDataAdapter($"SELECT * FROM checka.crbsoft WHERE {columns}", conexao);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    BSoftDataGrid.DataSource = dataTable;
                    conexao.Close();
                }
                Descontos.Text = string.Empty;
                BuscarDescontos();
            }
            if (CheckAcerto.Checked)
            {
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();
                    string columnQuery = @"
                SELECT COLUMN_NAME 
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_SCHEMA = 'checka' 
                AND TABLE_NAME = 'acertobsoft' 
                ORDER BY ORDINAL_POSITION";

                    MySqlCommand cmd = new MySqlCommand(columnQuery, conexao);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    string columns = "";
                    while (reader.Read())
                    {
                        if (columns.Length > 0)
                        {
                            columns += "OR ";
                        }
                        columns += reader["COLUMN_NAME"].ToString() + $" LIKE '%{textBox1.Text}%' ";
                    }
                    reader.Close();

                    var dataAdapter = new MySqlDataAdapter($"SELECT * FROM checka.acertobsoft WHERE {columns}", conexao);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    AcertoDataGrid.DataSource = dataTable;
                    conexao.Close();
                }
            }
            if (CheckPortal.Checked)
            {
                if (PortalDataGrid.DataSource == null)
                {
                    MessageBox.Show("Nenhum dado carregado.");
                    return;
                }

                DataTable originalData = (DataTable)PortalDataGrid.DataSource;

                // Filtra os dados manualmente com LINQ, convertendo tudo pra string
                var filteredRows = originalData.AsEnumerable()
                    .Where(row => row.ItemArray.Any(field =>
                        field != null && field.ToString().IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0))
                    .ToList();

                if (filteredRows.Count == 0)
                {
                    MessageBox.Show("Nenhum resultado encontrado.");
                    return;
                }

                // Agrupa por CARGA
                var grouped = filteredRows
                    .GroupBy(row => row["IdBus"])
                    .Select(g =>
                    {
                        var first = g.First();
                        var newRow = originalData.NewRow();

                        foreach (DataColumn col in originalData.Columns)
                        {
                            if (col.ColumnName == "TOTAL_VALOR_FRETE")
                            {
                                // Soma os fretes
                                newRow[col.ColumnName] = g.Sum(r => Convert.ToDecimal(r["TOTAL_VALOR_FRETE"]));
                            }
                            else
                            {
                                newRow[col.ColumnName] = first[col.ColumnName];
                            }
                        }

                        return newRow;
                    }).ToList();

                // Monta novo DataTable com os dados agrupados
                DataTable result = originalData.Clone();
                foreach (var row in grouped)
                {
                    DataRow newRow = result.NewRow();
                    foreach (DataColumn col in result.Columns)
                    {
                        newRow[col.ColumnName] = row[col.ColumnName];
                    }
                    result.Rows.Add(newRow);
                }

                PortalDataGrid.DataSource = result;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                PortalDataGrid.Columns[1].Visible = checkBoxCarga.Checked;
                PortalDataGrid.Columns[2].Visible = checkBoxID.Checked;
                PortalDataGrid.Columns[3].Visible = checkBoxDtEmbarque.Checked;
                PortalDataGrid.Columns[0].Visible = checkBoxFrete.Checked;
                PortalDataGrid.Columns[4].Visible = checkBoxTransportadora.Checked;
                PortalDataGrid.Columns[5].Visible = checkBoxVencimento.Checked;
                PortalDataGrid.Columns[6].Visible = checkBoxFaturamento.Checked;
                PortalDataGrid.Columns[7].Visible = checkBoxCodRevenda.Checked;
                PortalDataGrid.Columns[8].Visible = checkBoxCentral.Checked;
                PortalDataGrid.Columns[9].Visible = checkBoxTotalFrete.Checked;
                PortalDataGrid.Columns[10].Visible = checkBoxObs.Checked;
                PortalDataGrid.Columns[11].Visible = checkBoxAno.Checked;
            }
            panel1.Visible = !panel1.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage pacote = new ExcelPackage(new FileInfo("C:\\Users\\moniq\\desktop\\Info.xlsx")))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "BSOFT");
                ExcelWorksheet planilhaCte = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "CTE");
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();

                    for (int i = 2; i < planilha.Dimension.Rows + 1; i++)
                    {
                        UInt64 numeroDocumento = Convert.ToUInt64(planilha.Cells[i, 50].Value?.ToString());
                        string cliente = planilha.Cells[i, 54].Value?.ToString();
                        string centroDeReceita = planilha.Cells[i, 61].Value?.ToString();
                        DateTime dataEmissao = DateTime.Parse(planilha.Cells[i, 105].Value?.ToString());
                        DateTime dataBaixa = DateTime.Parse(planilha.Cells[i, 113].Value?.ToString());
                        string valor = planilha.Cells[i, 28].Value?.ToString();
                        string frete = planilha.Cells[i, 117].Value?.ToString();
                        int empresa = int.Parse(planilha.Cells[i, 5].Value?.ToString());
                        string CodigoCliente = planilha.Cells[i, 53].Value?.ToString();
                        string IdBus = planilha.Cells[i, 120].Value?.ToString();
                        string conc = planilha.Cells[i, 118].Value?.ToString(); // <- CONC (coluna 118)
                        int rowCount = planilhaCte.Dimension.End.Row;

                        int LinhaCte = Enumerable.Range(2, rowCount - 1) // começa da linha 2 (ignorando header)
                            .Select(linha => new
                            {
                                Linha = linha,
                                ValorCelula26 = planilhaCte.Cells[linha, 26].Text
                            })
                            .Where(x => !string.IsNullOrEmpty(x.ValorCelula26) && x.ValorCelula26.ToString() == IdBus.ToString())
                            .Select(x => x.Linha)
                            .FirstOrDefault();
                        string cte = (!(LinhaCte < 2)) ? planilhaCte.Cells[LinhaCte, 26].Value?.ToString() : "0";
                        if (cte != "0")
                        {
                            Console.WriteLine($"DOCUMENTO: {numeroDocumento} || CTE: {cte}");
                        }
                        // Verifica se o CONC já existe
                        string checkQuery = "SELECT Validado FROM checka.crbsoft WHERE CONC = @conc";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conexao))
                        {
                            checkCmd.Parameters.AddWithValue("@conc", conc);
                            object resultado = checkCmd.ExecuteScalar();

                            if (resultado == null)
                            {
                                // INSERT: não existe no banco
                                string insertQuery = @"
                            INSERT INTO checka.crbsoft 
                            (CONC, NumeroDocumento, Cliente, CentroDeReceita, Valor, DataEmissao, DataBaixa, Frete, Empresa, CodigoCliente, IdBus, CTE)
                            VALUES 
                            (@conc, @numero, @cliente, @centro, @valor, @emissao, @baixa, @frete, @empresa, @codigoCliente, @IdBus, @cte)";
                                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conexao))
                                {
                                    insertCmd.Parameters.AddWithValue("@conc", conc);
                                    insertCmd.Parameters.AddWithValue("@numero", numeroDocumento);
                                    insertCmd.Parameters.AddWithValue("@cliente", cliente);
                                    insertCmd.Parameters.AddWithValue("@centro", centroDeReceita);
                                    insertCmd.Parameters.AddWithValue("@valor", valor);
                                    insertCmd.Parameters.AddWithValue("@emissao", dataEmissao);
                                    insertCmd.Parameters.AddWithValue("@baixa", dataBaixa);
                                    insertCmd.Parameters.AddWithValue("@frete", frete);
                                    insertCmd.Parameters.AddWithValue("@empresa", empresa);
                                    insertCmd.Parameters.AddWithValue("@codigoCliente", CodigoCliente);
                                    insertCmd.Parameters.AddWithValue("@IdBus", IdBus);
                                    insertCmd.Parameters.AddWithValue("@cte", cte);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string validado = resultado.ToString();
                                if (validado != "OK")
                                {
                                    // UPDATE: existe, mas não está validado
                                    string updateQuery = @"
                                UPDATE checka.crbsoft SET
                                    NumeroDocumento = @numero,
                                    Cliente = @cliente,
                                    CentroDeReceita = @centro,
                                    Valor = @valor,
                                    DataEmissao = @emissao,
                                    DataBaixa = @baixa,
                                    Frete = @frete,
                                    Empresa = @empresa,
                                    CodigoCliente = @codigoCliente,
                                    IdBus = @IdBus,
                                    CTE = @cte
                                WHERE CONC = @conc";
                                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conexao))
                                    {
                                        updateCmd.Parameters.AddWithValue("@conc", conc);
                                        updateCmd.Parameters.AddWithValue("@numero", numeroDocumento);
                                        updateCmd.Parameters.AddWithValue("@cliente", cliente);
                                        updateCmd.Parameters.AddWithValue("@centro", centroDeReceita);
                                        updateCmd.Parameters.AddWithValue("@valor", valor);
                                        updateCmd.Parameters.AddWithValue("@emissao", dataEmissao);
                                        updateCmd.Parameters.AddWithValue("@baixa", dataBaixa);
                                        updateCmd.Parameters.AddWithValue("@frete", frete);
                                        updateCmd.Parameters.AddWithValue("@empresa", empresa);
                                        updateCmd.Parameters.AddWithValue("@codigoCliente", CodigoCliente);
                                        updateCmd.Parameters.AddWithValue("@IdBus", IdBus);
                                        updateCmd.Parameters.AddWithValue("@cte", cte);
                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                                // else: está validado como "OK", ignora
                            }
                        }
                    }

                    conexao.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pacote = new ExcelPackage(new FileInfo("C:\\Users\\moniq\\desktop\\Info.xlsx")))
            {
                ExcelWorksheet planilha = pacote.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "ACERTO BSOFT");
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();

                    for (int i = 2; i < planilha.Dimension.Rows + 1; i++)
                    {
                        UInt64 numerotitulo = Convert.ToUInt64(planilha.Cells[i, 5].Value?.ToString());
                        string posicao = planilha.Cells[i, 4].Value?.ToString();
                        string obs = planilha.Cells[i, 42].Value?.ToString();
                        string total = planilha.Cells[i, 37].Value?.ToString();
                        string pagador = planilha.Cells[i, 19].Value?.ToString();
                        int empresa = int.Parse(planilha.Cells[i, 2].Value?.ToString());

                        MySqlCommand comando = new MySqlCommand($"INSERT INTO checka.acertobsoft (Posicao, NumeroDocumento, Observacao, Total, Pagador, Empresa) VALUES (@Posicao, @NumeroDocumento, @Observacao, @Total, @Pagador, @Empresa);", conexao);
                        comando.Parameters.AddWithValue("@Posicao", posicao);
                        comando.Parameters.AddWithValue("@NumeroDocumento", numerotitulo);
                        comando.Parameters.AddWithValue("@Observacao", obs);
                        comando.Parameters.AddWithValue("@Total", total);
                        comando.Parameters.AddWithValue("@Pagador", pagador);
                        comando.Parameters.AddWithValue("@Empresa", empresa);

                        comando.ExecuteNonQuery();
                    }

                    conexao.Close();
                }
            }
        }

        private void BSoftDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var grid = sender as DataGridView;
            int colIndex = e.ColumnIndex;

            if (grid.DataSource is BindingSource bs && bs.DataSource is DataTable dt)
            {
                string coluna = grid.Columns[colIndex].DataPropertyName;
                if(coluna == "ValorCTE")
                {
                    return;
                }
                var valores = dt.AsEnumerable()
                    .Select(r => r[coluna]?.ToString())
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Distinct()
                    .OrderBy(v => v)
                    .ToList();

                CheckedListBox checklist = new CheckedListBox
                {
                    CheckOnClick = true,
                    Width = 200,
                    Height = 200
                };

                foreach (var val in valores)
                    checklist.Items.Add(val);




                // Botão limpar
                Button limpar = new Button
                {
                    Text = "Aplicar",
                    Height = 30,
                    Dock = DockStyle.Bottom
                };

                limpar.Click += (s, a) =>
                {
                    var selecionados = checklist.CheckedItems
                        .Cast<string>()
                        .Select(v => $"[{coluna}] = '{v.Replace("'", "''")}'")
                        .ToList();

                    if (selecionados.Count > 0)
                    {
                        // Se já houver um filtro anterior, combina com AND
                        if (!string.IsNullOrWhiteSpace(bs.Filter))
                        {
                            // Se já tiver filtro da mesma coluna, remove primeiro
                            var regex = new System.Text.RegularExpressions.Regex($@"(\[?{coluna}\]?\s*=\s*'[^']*')( OR )?");
                            bs.Filter = regex.Replace(bs.Filter, "").Trim();

                            if (bs.Filter.EndsWith("OR"))
                                bs.Filter = bs.Filter[..^2].Trim();
                        }

                        string novoFiltro = string.Join(" OR ", selecionados);

                        if (string.IsNullOrWhiteSpace(bs.Filter))
                            bs.Filter = novoFiltro;
                        else
                            bs.Filter += $" AND ({novoFiltro})";
                    }
                    else
                    {
                        bs.RemoveFilter();
                    }

                    // Fecha o dropdown
                    var parent = ((Control)s).Parent?.Parent as ToolStripDropDown;
                    parent?.Close();
                };

                // Botão aplicar
                Button aplicar = new Button
                {
                    Text = "Aplicar",
                    Height = 30,
                    Dock = DockStyle.Bottom
                };

                aplicar.Click += (s, a) =>
                {
                    bs.RemoveFilter();
                    var parent = ((Control)s).Parent?.Parent as ToolStripDropDown;
                    parent?.Close();
                };

                Panel painel = new Panel { Width = 200, Height = 260 };
                painel.Controls.Add(checklist);
                painel.Controls.Add(aplicar);
                painel.Controls.Add(limpar);

                ToolStripControlHost host = new ToolStripControlHost(painel);
                ToolStripDropDown drop = new ToolStripDropDown();
                drop.Items.Add(host);

                var cellRect = grid.GetCellDisplayRectangle(colIndex, -1, true);
                var pt = new Point(cellRect.Left, cellRect.Bottom);

                drop.Show(grid, pt);
            }
        }

        private void BSoftDataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var culturaBR = new System.Globalization.CultureInfo("pt-BR");
            decimal soma = 0.0m;

            foreach (DataGridViewRow row in BSoftDataGrid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells[4].Value != null &&
                    decimal.TryParse(row.Cells[4].Value.ToString(), NumberStyles.Any, culturaBR, out decimal valor))
                {
                    soma += valor;
                }
            }

            BSoftDataGrid.Columns["Conc"].Visible = false;

            //Dictionary<string, double> keyValues = new Dictionary<string, double>();

            //if (BSoftDataGrid.Columns.Contains("ValorCTE"))
            //{
            //    foreach (DataGridViewRow row in BSoftDataGrid.Rows)
            //    {
            //        if (row.IsNewRow) continue;
            //        string cte = row.Cells["CTE"].Value?.ToString();
            //        if (!keyValues.ContainsKey(cte))
            //        {
            //            keyValues.Add(cte, 0);
            //        }
            //    }

            //    foreach (object[] linha in linhasPlanilhaCTE)
            //    {
            //        string ctePlanilha = linha[25]?.ToString();
            //        if (!string.IsNullOrEmpty(ctePlanilha) && keyValues.ContainsKey(ctePlanilha))
            //        {
            //            string valorString = linha[16]?.ToString();
            //            double valor = 0;
            //            double.TryParse(
            //                valorString?.Replace(".", "").Replace(",", "."),
            //                NumberStyles.Any,
            //                CultureInfo.InvariantCulture,
            //                out valor
            //            );

            //            keyValues[ctePlanilha] += valor;
            //        }
            //    }

            //    foreach (DataGridViewRow row in BSoftDataGrid.Rows)
            //    {
            //        if (row.IsNewRow) continue;
            //        string cte = row.Cells["CTE"].Value?.ToString();
            //        if (!string.IsNullOrEmpty(cte) && keyValues.ContainsKey(cte))
            //        {
            //            row.Cells["ValorCTE"].Value = keyValues[cte].ToString("N2");
            //        }
            //    }
            // }

            ValorSomadoCRBsoft.Text = "TOTAL: " + soma.ToString("N2", culturaBR);
            soma2 = soma;
            ValorDiff.Text = "DIFERENÇA: " + (soma1 - soma2).ToString("N2", culturaBR);
        }

        private void PortalDataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var culturaBR = new System.Globalization.CultureInfo("pt-BR");
            decimal soma = 0.0m;

            foreach (DataGridViewRow row in PortalDataGrid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells[4].Value != null &&
                    decimal.TryParse(row.Cells["TOTAL_VALOR_FRETE"].Value.ToString(), NumberStyles.Any, culturaBR, out decimal valor))
                {
                    soma += valor;
                }
            }

            ValorSomadoPortal.Text = "TOTAL: " + soma.ToString("N2", culturaBR);
            soma1 = soma;
            ValorDiff.Text = "DIFERENÇA: " + (soma1 - soma2).ToString("N2", culturaBR);
        }

        private void AcertoDataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CultureInfo culturaBR = new CultureInfo("pt-BR");
            decimal soma = 0.0m;

            foreach (DataGridViewRow row in AcertoDataGrid.Rows)
            {
                if (row.Visible && row.Cells["Total"].Value != null) // <- ajuste o índice da coluna "Total" se necessário
                {
                    string valorStr = row.Cells["Total"].Value.ToString(); // índice da coluna "Total"
                    if (decimal.TryParse(valorStr, NumberStyles.Any, culturaBR, out decimal valor))
                    {
                        soma += valor;
                    }
                }
            }

            ValorSomadoAcerto.Text = "TOTAL: " + soma.ToString("N2", culturaBR);
        }

        private void FreteBotao_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Deseja enviar as informações?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();
                    new MySqlCommand(@"
            INSERT INTO arquivo_bsoft (
                ID,
                NumeroDocumento,
                Cliente,
                CentroDeReceita,
                Valor,
                DataEmissao,
                DataBaixa,
                Frete,
                Empresa,
                Validado
            )
            SELECT
                ID,
                NumeroDocumento,
                Cliente,
                CentroDeReceita,
                Valor,
                DataEmissao,
                DataBaixa,
                Frete,
                Empresa,
                Validado
            FROM checka.crbsoft AS src
            WHERE Validado = 'OK'
            AND NOT EXISTS (
                SELECT 1 FROM arquivo_bsoft AS dest
                WHERE
                    dest.ID = src.ID AND
                    dest.NumeroDocumento = src.NumeroDocumento AND
                    dest.Cliente = src.Cliente AND
                    dest.CentroDeReceita = src.CentroDeReceita AND
                    dest.Valor = src.Valor AND
                    dest.DataEmissao = src.DataEmissao AND
                    (dest.DataBaixa <=> src.DataBaixa) AND -- usando <=> para lidar com NULLs
                    dest.Frete = src.Frete AND
                    dest.Empresa = src.Empresa AND
                    dest.Validado = src.Validado
            );", conexao).ExecuteNonQuery();

                    new MySqlCommand($"UPDATE checka.principal SET Validado = 'OK' WHERE Frete IN (SELECT Frete FROM checka.crbsoft WHERE Validado = 'OK');", conexao).ExecuteNonQuery();
                    new MySqlCommand($"UPDATE checka.arquivo SET Validado = 'OK' WHERE Frete IN (SELECT Frete FROM checka.crbsoft WHERE Validado = 'OK');", conexao).ExecuteNonQuery();
                    conexao.Close();
                }
            }
            else
            {
                return;
            }
        }

        private void BSoftDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = BSoftDataGrid.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            // Toggle "Validado"
            if (BSoftDataGrid.Columns[e.ColumnIndex].Name == "Validado")
            {
                var valorAtual = row.Cells["Validado"].Value?.ToString()?.Trim();
                if (string.IsNullOrEmpty(valorAtual))
                {
                    row.Cells["Validado"].Value = "OK";
                }
                else
                {
                    row.Cells["Validado"].Value = "OK";
                }

                // Garante que o valor é salvo no DataGridView
                BSoftDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                BSoftDataGrid.EndEdit();
            }

            // Adiciona o ID sempre que clicar em qualquer célula (sem duplicatas)
            int id = Convert.ToInt32(row.Cells[0].Value);
            if (!IDs.Contains(id))
                IDs.Add(id);
        }

        private void BSoftDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var row = BSoftDataGrid.Rows[e.RowIndex];
            if (row.IsNewRow) return;
            if (BSoftDataGrid.Columns[e.ColumnIndex].Name == "Validado")
            {
                row.Cells["Validado"].Value = "";
            }
        }

        private void SalvarBsoft_Click(object sender, EventArgs e)
        {
            // Finaliza qualquer edição pendente
            BSoftDataGrid.EndEdit();
            BSoftDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

            // Atualiza a cópia da grid ANTES de continuar
            ListaRows.Clear();
            foreach (DataGridViewRow row in BSoftDataGrid.Rows)
            {
                if (!row.IsNewRow && row.Cells[0].Value != null)
                {
                    ListaRows.Add(row);
                }
            }

            // Aqui você salva os dados, mas usa ListaRows — que está atualizada
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();
                foreach (DataGridViewRow row in ListaRows)
                {
                    int idAtual = Convert.ToInt32(row.Cells[0].Value);

                    if (IDs.Contains(idAtual))
                    {
                        string query = @"
                    UPDATE checka.crbsoft SET
                        NumeroDocumento = @NumeroDocumento,
                        Cliente = @Cliente,
                        CentroDeReceita = @CentroDeReceita,
                        Valor = @Valor,
                        DataEmissao = @DataEmissao,
                        DataBaixa = @DataBaixa,
                        Frete = @Frete,
                        Empresa = @Empresa,
                        Validado = @Validado
                    WHERE ID = @ID";

                        using (var command = new MySqlCommand(query, conexao))
                        {
                            command.Parameters.AddWithValue("@ID", row.Cells["ID"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@NumeroDocumento", row.Cells["NumeroDocumento"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Cliente", row.Cells["Cliente"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@CentroDeReceita", row.Cells["CentroDeReceita"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Valor", row.Cells["Valor"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@DataEmissao", row.Cells["DataEmissao"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@DataBaixa", row.Cells["DataBaixa"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Frete", row.Cells["Frete"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Empresa", row.Cells["Empresa"].Value ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Validado", row.Cells["Validado"].Value ?? DBNull.Value);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                conexao.Close();
            }

            MessageBox.Show("Alterações salvas!");
        }

        private void FreteBotao2_Click(object sender, EventArgs e)
        {
            new ArquivoBsoft().Show();
        }

        private void CrJoao_Click(object sender, EventArgs e)
        {
            new CheckJoao().Show();
        }

        private void CopiarDataGrid(DataGridView origem, DataGridView destino)
        {
            destino.Columns.Clear();
            destino.Rows.Clear();
            destino.AutoGenerateColumns = false;

            // Copiar colunas
            foreach (DataGridViewColumn coluna in origem.Columns)
            {
                destino.Columns.Add((DataGridViewColumn)coluna.Clone());
            }

            // Copiar valores das linhas
            foreach (DataGridViewRow row in origem.Rows)
            {
                if (!row.IsNewRow)
                {
                    int index = destino.Rows.Add();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        destino.Rows[index].Cells[i].Value = row.Cells[i].Value;
                    }
                }
            }
        }

        private void LimparCRBsoft_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Deseja APAGAR as informações do acerto e do Contas a Receber?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();
                    new MySqlCommand("DELETE FROM checka.crbsoft WHERE ID > 0;", conexao).ExecuteNonQuery();
                    new MySqlCommand("DELETE FROM checka.acertobsoft WHERE ID > 0;", conexao).ExecuteNonQuery();
                    conexao.Close();
                }
            }
            else
            {
                MessageBox.Show("Indecisa hein", "indecisão");
            }
        }
    }
}
