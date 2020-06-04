using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace Maturski_rad
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Baza.accdb");
            con.Open();
            var tabele = con.GetSchema("Tables");
            foreach (DataRow tabela in tabele.Rows)
            {
                if (char.IsDigit(tabela[2].ToString().First()))comboBox1.Items.Add(tabela[2]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Baza.accdb");
            con.Open();
            OleDbCommand com = new OleDbCommand("SELECT * FROM [" + comboBox1.Text + "]", con);
            OleDbDataAdapter ad = new OleDbDataAdapter();
            ad.SelectCommand = com;
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
