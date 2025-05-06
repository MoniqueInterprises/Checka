using MySql.Data.MySqlClient;
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
    public partial class Arquivo : Form
    {
        public Arquivo()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                var command = new MySqlCommand($"SELECT * FROM checka.arquivo", conexao);

                var dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                conexao.Close();
            }
            label1.Text = "Linhas: " + (dataGridView1.RowCount - 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja REALMENTE deletar TODO o ARQUIVO?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                new MySqlCommand($"DELETE FROM checka.arquivo WHERE ID > 0", conexao).ExecuteNonQuery();

                conexao.Close();
            }
            MessageBox.Show("DELETADAS TODAS AS INSTÂNCIAS NO ARQUIVO!");
            Refresh_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
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

                var dataAdapter = new MySqlDataAdapter($"SELECT * FROM checka.arquivo WHERE {columns}", conexao);
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

        private void Arquivo_Resize(object sender, EventArgs e)
        {
            int margin = 12;

            dataGridView1.Width = this.ClientSize.Width - dataGridView1.Left - margin;
            dataGridView1.Height = this.ClientSize.Height - dataGridView1.Top - margin * 4;
        }

        private void DeletarLinha_Click(object sender, EventArgs e)
        {
            new DeletarLinha(1).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Restaurar().Show();
        }

        private void LimparMes_Click(object sender, EventArgs e)
        {
            new LimparMes().Show();
        }
    }
}
