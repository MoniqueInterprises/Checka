using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Checka
{
    public partial class Home : Form
    {
        private List<DataGridViewRow> LinhasAlteradas = new List<DataGridViewRow>();
        public Home()
        {
            InitializeComponent();
        }

        private string TransformarPara2Caracteres(string numero)
        {
            if (numero.Length == 1)
            {
                return ("0" + numero);
            }
            else
            {
                return numero;
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            SearchBox.Text = string.Empty;
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                var command = new MySqlCommand($"SELECT * FROM checka.principal", conexao);

                var dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                conexao.Close();
            }
            label1.Text = "Linhas: " + (dataGridView1.RowCount - 1);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Refresh_Click(sender, e);
            dataGridView1.Columns["Auditado"].ReadOnly = true;
            pictureBox1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Importar().Show();
        }

        private void Home_Resize(object sender, EventArgs e)
        {
            int margin = 12;

            dataGridView1.Width = this.ClientSize.Width - dataGridView1.Left - margin;
            dataGridView1.Height = this.ClientSize.Height - dataGridView1.Top - margin * 4;
            pictureBox1.Location = new Point(this.ClientSize.Width - 490 - margin);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Frete().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();
                try
                {
                    string[] colunas = new string[dataGridView1.ColumnCount];
                    colunas[0] = "ID";
                    for (int i = 1; i < colunas.Length; i++)
                    {
                        colunas[i] = dataGridView1.Columns[i].Name;
                    }
                    foreach (DataGridViewRow row in LinhasAlteradas)
                    {
                        if (row.Cells[0].Value is null)
                        {
                            continue;
                        }
                        if (row.IsNewRow) continue;

                        if (row.Cells["Checkado"].Value?.ToString() == "OK")
                        {
                            var command2 = new MySqlCommand($"INSERT INTO checka.arquivo SELECT * FROM checka.principal WHERE ID = {row.Cells[0].Value}", conexao).ExecuteNonQuery();

                            var command3 = new MySqlCommand($"DELETE FROM checka.principal WHERE ID = {row.Cells[0].Value?.ToString()}", conexao);
                            command3.ExecuteNonQuery();
                        }
                        else
                        {
                            var a = new MySqlCommand($"SELECT * FROM checka.principal WHERE ID = {row.Cells[0].Value}", conexao);
                            var b = a.ExecuteReader();
                            b.Read();
                            object[] c = new object[b.FieldCount];
                            for (int i = 0; i < b.FieldCount; i++)
                            {
                                c[i] = b.GetValue(i);
                            }
                            b.Close();

                            string diaData1 = TransformarPara2Caracteres(c[2].ToString().Split('/')[0]);
                            string mesData1 = TransformarPara2Caracteres(c[2].ToString().Split('/')[1]);
                            string diaData2 = TransformarPara2Caracteres(c[5].ToString().Split('/')[0]);
                            string mesData2 = TransformarPara2Caracteres(c[5].ToString().Split('/')[1]);

                            string Data1 = c[2].ToString().Split('/')[2].Split(' ')[0] + "-" + mesData1 + "-" + diaData1;
                            string Data2 = c[5].ToString().Split('/')[2].Split(' ')[0] + "-" + mesData2 + "-" + diaData2;
                            var command = new MySqlCommand($"UPDATE checka.principal SET " +
                                 $"{colunas[1]} = '{row.Cells[1].Value}', " +
                                 $"{colunas[2]} = '{Data1}', " +
                                 $"{colunas[3]} = '{row.Cells[3].Value}', " +
                                 $"{colunas[4]} = '{row.Cells[4].Value}', " +
                                 $"{colunas[5]} = '{Data2}', " +
                                 $"{colunas[6]} = '{row.Cells[6].Value}', " +
                                 $"{colunas[7]} = '{row.Cells[7].Value}', " +
                                 $"{colunas[8]} = '{row.Cells[8].Value}', " +
                                 $"{colunas[9]} = '{row.Cells[9].Value}', " +
                                 $"{colunas[10]} = '{row.Cells[10].Value}', " +
                                 $"{colunas[11]} = '{row.Cells[11].Value}', " +
                                 $"{colunas[12]} = '{row.Cells[12].Value}', " +
                                 $"{colunas[13]} = '{row.Cells[13].Value}', " +
                                 $"{colunas[14]} = '{row.Cells[14].Value}', " +
                                 $"{colunas[15]} = '{row.Cells[15].Value}', " +
                                 $"{colunas[16]} = '{row.Cells[16].Value}', " +
                                 $"{colunas[17]} = '{row.Cells[17].Value}', " +
                                 $"{colunas[18]} = '{row.Cells[18].Value}', " +
                                 $"{colunas[19]} = {row.Cells[19].Value}  WHERE ID = {row.Cells[0].Value}", conexao);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Alterações salvas!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
            Refresh_Click(sender, e);
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (LinhasAlteradas.Contains(dataGridView1.Rows[e.RowIndex]))
            {
                return;
            }
            LinhasAlteradas.Add(dataGridView1.Rows[e.RowIndex]);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderCell.Value.ToString() == "Auditado")
            {
                DataGridViewCell cellC = dataGridView1.Rows[e.RowIndex].Cells["Auditado"];
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (cellC.Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "1";
                        foreach (DataGridViewCell cell2 in row.Cells)
                        {
                            cell2.Style.BackColor = Color.FromArgb(166, 86, 152);
                            cell2.Style.ForeColor = Color.Silver;
                        }
                    }
                    else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "1")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                        if (dataGridView1.Rows[e.RowIndex].Cells["Auditado"].Value.ToString() == "0")
                        {
                            foreach (DataGridViewCell cell2 in row.Cells)
                            {
                                cell2.Style.BackColor = Color.White;
                                cell2.Style.ForeColor = Color.Black;
                            }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new DeletarLinha().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Arquivo().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult confirmacao = MessageBox.Show("Deseja REALMENTE apagar TUDO?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacao == DialogResult.No)
            {
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                new MySqlCommand($"DELETE FROM checka.principal WHERE ID > 1", conexao).ExecuteNonQuery();

                conexao.Close();
            }
            Refresh_Click(sender, e);
        }

        private void Pesquisar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();
                string columnQuery = @"
                SELECT COLUMN_NAME 
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_SCHEMA = 'checka' 
                AND TABLE_NAME = 'principal' 
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
                    columns += reader["COLUMN_NAME"].ToString() + $" LIKE '%{SearchBox.Text}%' ";
                }
                reader.Close();

                var dataAdapter = new MySqlDataAdapter($"SELECT * FROM checka.principal WHERE {columns}", conexao);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
                conexao.Close();
                label1.Text = "Linhas: " + (dataGridView1.RowCount - 1);
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                string searchValue = ((TextBox)sender).Text;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && cell.Value.ToString().Contains(searchValue))
                        {
                            dataGridView1.CurrentCell = cell;
                            return;
                        }
                    }
                }
                MessageBox.Show("Texto não encontrado.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Checkado"].Value?.ToString() == "OK")
                {
                    if (11 - 10 == 1)
                    {
                        if (LinhasAlteradas.Contains(dataGridView1.Rows[row.Index]))
                        {
                            return;
                        }
                        LinhasAlteradas.Add(dataGridView1.Rows[row.Index]);
                    }
                    if (true)
                    {
                        DataGridViewCell cellC = dataGridView1.Rows[row.Index].Cells["Auditado"];
                        DataGridViewRow row2 = dataGridView1.Rows[row.Index];
                        if (cellC.Value != null)
                        {
                            if (dataGridView1.Rows[row.Index].Cells["Auditado"].Value.ToString() == "0")
                            {
                                dataGridView1.Rows[row.Index].Cells["Auditado"].Value = "1";
                                foreach (DataGridViewCell cell2 in row.Cells)
                                {
                                    cell2.Style.BackColor = Color.FromArgb(166, 86, 152);
                                    cell2.Style.ForeColor = Color.Silver;
                                }
                            }
                            else if (dataGridView1.Rows[row.Index].Cells["Auditado"].Value.ToString() == "1")
                            {
                                dataGridView1.Rows[row.Index].Cells["Auditado"].Value = "0";
                                if (dataGridView1.Rows[row.Index].Cells["Auditado"].Value.ToString() == "0")
                                {
                                    foreach (DataGridViewCell cell2 in row.Cells)
                                    {
                                        cell2.Style.BackColor = Color.White;
                                        cell2.Style.ForeColor = Color.Black;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Data_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = !monthCalendar1.Visible;
            monthCalendar2.Visible = !monthCalendar2.Visible;
            EmissaoVencimentoCheck.Visible = !EmissaoVencimentoCheck.Visible;
            BuscarData.Visible = !BuscarData.Visible;
        }

        private void BuscarData_Click(object sender, EventArgs e)
        {
            if (EmissaoVencimentoCheck.Checked)
            {
                //buscar por vencimento
                SearchBox.Text = string.Empty;
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();

                    var command = new MySqlCommand($"SELECT * FROM checka.principal WHERE Vencimento BETWEEN " +
                        $"'{monthCalendar1.SelectionStart:yyyy-MM-dd}' AND '{monthCalendar2.SelectionStart:yyyy-MM-dd}'", conexao);

                    var dataAdapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                    conexao.Close();
                }
                label1.Text = "Linhas: " + (dataGridView1.RowCount - 1);
            }
            else
            {
                //buscar por emissão
                SearchBox.Text = string.Empty;
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();

                    var command = new MySqlCommand($"SELECT * FROM checka.principal WHERE DataEmbarque BETWEEN " +
                        $"'{monthCalendar1.SelectionStart:yyyy-MM-dd}' AND '{monthCalendar2.SelectionStart:yyyy-MM-dd}'", conexao);

                    var dataAdapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                    conexao.Close();
                }
                label1.Text = "Linhas: " + (dataGridView1.RowCount - 1);
            }
            Data_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new BSoft().Show();
        }
    }
}
