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
    public partial class Restaurar : Form
    {
        int Parametro = 0;
        public Restaurar()
        {
            InitializeComponent();
        }
        public Restaurar(int parametro)
        {
            InitializeComponent();
            Parametro = parametro;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Parametro == 2)
            {
                using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
                {
                    conexao.Open();
                    var query = $@"
                        INSERT IGNORE INTO crbsoft (
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
                        FROM checka.arquivo_bsoft
                        WHERE ID = {textBox1.Text};";

                    using (var command = new MySqlCommand(query, conexao))
                    {
                        command.ExecuteNonQuery();
                    }
                    new MySqlCommand($"UPDATE checka.principal SET Validado = '' WHERE Frete IN (SELECT Frete FROM checka.arquivo_bsoft WHERE ID = {textBox1.Text})", conexao).ExecuteNonQuery();
                    new MySqlCommand($"UPDATE checka.arquivo SET Validado = '' WHERE Frete IN (SELECT Frete FROM checka.arquivo_bsoft WHERE ID = {textBox1.Text})", conexao).ExecuteNonQuery();
                    new MySqlCommand($"UPDATE checka.crbsoft SET Validado = '' WHERE ID = {textBox1.Text}", conexao).ExecuteNonQuery();
                    new MySqlCommand($"DELETE FROM checka.arquivo_bsoft WHERE ID = {textBox1.Text}", conexao).ExecuteNonQuery();
                    conexao.Close();
                }
                MessageBox.Show($"RESTAURADO O ID {textBox1.Text}!");
                return;
            }
            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();
                new MySqlCommand($"INSERT INTO checka.principal SELECT * FROM checka.arquivo WHERE ID = {textBox1.Text}", conexao).ExecuteNonQuery();
                new MySqlCommand($"DELETE FROM checka.arquivo WHERE ID = {textBox1.Text}", conexao).ExecuteNonQuery();
                conexao.Close();
            }
            MessageBox.Show($"RESTAURADO O ID {textBox1.Text}!");
        }
    }
}
