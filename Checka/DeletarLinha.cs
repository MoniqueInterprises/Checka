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
    public partial class DeletarLinha : Form
    {
        int A = 0;
        public DeletarLinha()
        {
            A = 0;
            InitializeComponent();
        }
        public DeletarLinha(int a)
        {
            A = a;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (A == 2)
            {
                DialogResult result = MessageBox.Show($"Deseja deletar o ID {textBox1.Text}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                    {
                        conexao.Open();
                        new MySqlCommand($"DELETE FROM checka.arquivo_bsoft WHERE ID = {textBox1.Text}", conexao).ExecuteNonQuery();
                        new MySqlCommand($"UPDATE checka.crbsoft SET Validado = NULL WHERE ID = {textBox1.Text}", conexao).ExecuteNonQuery();
                        conexao.Close();
                    }
                    MessageBox.Show($"DELETADO O ID '{textBox1.Text}'!");
                }
                else
                {
                    return;
                }
                return;
            }
            if (A == 1)
            {
                DialogResult result = MessageBox.Show($"Deseja deletar o ID {textBox1.Text}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                    {
                        conexao.Open();
                        new MySqlCommand($"DELETE FROM checka.arquivo WHERE ID = {textBox1.Text}", conexao).ExecuteNonQuery();
                        conexao.Close();
                    }
                    MessageBox.Show($"DELETADO O ID '{textBox1.Text}'!");
                }
                else
                {
                    return;
                }
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show($"Deseja deletar o ID {textBox1.Text}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                    {
                        conexao.Open();
                        new MySqlCommand($"DELETE FROM checka.principal WHERE ID = {textBox1.Text}", conexao).ExecuteNonQuery();
                        conexao.Close();
                    }
                    MessageBox.Show($"DELETADO O ID '{textBox1.Text}'!");
                }
                else
                {
                    return;
                }
            }
        }
    }
}
