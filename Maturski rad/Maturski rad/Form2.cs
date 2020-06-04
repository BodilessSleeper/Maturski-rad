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
using System.Data.OleDb;

namespace Maturski_rad
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void brk()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Baza.accdb");
            con.Open();
            OleDbCommand com = new OleDbCommand("SELECT DISTINCT [Opstina] FROM [Dan1]", con);
            OleDbDataAdapter ad = new OleDbDataAdapter();
            ad.SelectCommand = com;
            DataTable dt = new DataTable();
            ad.Fill(dt);
            MessageBox.Show(dt.Rows[0][0].ToString());
            foreach(DataRow x in dt.Rows)
            {
                com = new OleDbCommand("SELECT [Kolicina] FROM [Dan1] WHERE [Opstina]='" + x[0].ToString() + "'", con);
                ad.SelectCommand = com;
                DataTable dt1 = new DataTable();
                ad.Fill(dt1);
                double z = 0;
                foreach (DataRow y in dt1.Rows)
                {
                    z += Convert.ToDouble(y[0]);
                }
                if (z > 10000.0) listBox1.Items.Add("Potrebno je " + Convert.ToString(Convert.ToInt64(z / 10000.0)) + " kamiona na opstini " + x[0].ToString());
            }
        }

        public void show(string s)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Baza.accdb");
            con.Open();
            OleDbDataAdapter ad = new OleDbDataAdapter();
            ad.SelectCommand = new OleDbCommand("SELECT * FROM " + s + " ORDER BY Opstina ASC", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Baza.accdb");
            con.Open();
            OleDbDataAdapter ad = new OleDbDataAdapter();
            ad.SelectCommand = new OleDbCommand("SELECT * FROM [Kamioni]", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            foreach(DataRow x in dt.Rows)
            {
                listBox2.Items.Add(x[0]);
            }
            con.Close();
            show("Dan");
            if (dataGridView1.Rows.Count != 0) brk();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string opst = listBox1.SelectedItem.ToString().Split(' ')[6];
            if (listBox1.SelectedItem != null)
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Baza.accdb");
                con.Open();
                OleDbCommand com = new OleDbCommand("SELECT * FROM [Dan1] WHERE [Opstina]='" + opst + "'", con);
                OleDbDataAdapter ad = new OleDbDataAdapter();
                ad.SelectCommand = com;
                DataTable dt = new DataTable();
                ad.Fill(dt);
                double tezina = 10000.0;
                foreach(DataRow x in dt.Rows)
                {
                    if((tezina - Convert.ToDouble(x[1])) < 0)
                    {
                        com = new OleDbCommand("UPDATE [Dan1] SET Kolicina = " + (Convert.ToDouble(x[1])-tezina).ToString() + " WHERE (Predmet='" + x[0].ToString() + "') AND (Opstina='" + x[2].ToString() + "')", con);
                        com.ExecuteNonQuery();
                        tezina = 0;
                    }
                    else
                    {
                        com = new OleDbCommand("DELETE FROM [Dan1] WHERE (Predmet='" + x[0].ToString() + "') AND (Opstina='" + x[2].ToString() + "')", con);
                        com.ExecuteNonQuery();
                        tezina -= Convert.ToDouble(x[1]);
                    }
                }
                listBox2.Items.Add("Poslato je " + listBox1.SelectedItem.ToString().Split(' ')[2] + " kamiona na opstinu " + opst);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                com = new OleDbCommand("INSERT INTO [Kamioni] VALUES('" + listBox2.Items[listBox2.Items.Count-1].ToString() + "')", con);
                com.ExecuteNonQuery();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) show("Dan");
            else show("Dan1");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) show("Dan");
            else show("Dan1");
        }
    }
}
