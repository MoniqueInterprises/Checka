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
    public partial class LimparMes : Form
    {
        public LimparMes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime data1 = monthCalendar1.SelectionStart;
            DateTime data2 = monthCalendar2.SelectionEnd;
            DialogResult confirmacao = MessageBox.Show($"Deseja REALMENTE apagar TUDO destas DATAS ('{data1:dd-MM-yyyy}' e '{data2:dd-MM-yyyy}')?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacao == DialogResult.No)
            {
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection(Auxl.Str))
            {
                conexao.Open();

                MySqlCommand command = new MySqlCommand("DELETE FROM checka.arquivo WHERE DataEmbarque BETWEEN @data1 AND @data2", conexao);
                command.Parameters.AddWithValue("@data1", data1);
                command.Parameters.AddWithValue("@data2", data2);
                command.ExecuteNonQuery();

                conexao.Close();
            }
        }
    }
}
