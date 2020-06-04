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
using System.Security.Cryptography;
using System.Data.OleDb;

namespace Maturski_rad
{
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double d = Convert.ToDouble(textBox2.Text);
                if (d > 0) dataGridView1.Rows.Add(textBox1.Text, d, textBox3.Text);
                else MessageBox.Show("Unesite pozitivnu tezinu");
            }
            catch
            {
                MessageBox.Show("Mozete uneti samo brojeve i tacku za tezinu");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(row.Index);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wr("Dan");
            wr("Dan1");
            wr("Mesec");
            this.Close();
        }

        public void wr(string s)
        {
            if (dataGridView1.Rows.Count == 0) return;
            else
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Baza.accdb");
                con.Open();
                OleDbDataAdapter ad = new OleDbDataAdapter();
                foreach (DataGridViewRow x in dataGridView1.Rows)
                {
                    ad.SelectCommand = new OleDbCommand("SELECT Predmet, Opstina FROM " + s + " WHERE (Predmet='" + x.Cells[0].Value.ToString() + "') AND (Opstina='" + x.Cells[2].Value.ToString() + "')", con);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        ad.InsertCommand = new OleDbCommand("INSERT INTO " + s + " VALUES ('" + x.Cells[0].Value.ToString() + "', " + x.Cells[1].Value.ToString() + ", '" + x.Cells[2].Value.ToString() + "')", con);
                        ad.InsertCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        ad.UpdateCommand = new OleDbCommand("UPDATE " + s + " SET Kolicina = Kolicina + " + x.Cells[1].Value.ToString() + " WHERE (Predmet='" + x.Cells[0].Value.ToString() + "') AND (Opstina='" + x.Cells[2].Value.ToString() + "')", con);
                        ad.UpdateCommand.ExecuteScalar();
                    }
                }
                con.Close();
            }
        }

        
    }
}
