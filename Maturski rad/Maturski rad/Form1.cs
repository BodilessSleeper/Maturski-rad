using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Maturski_rad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileInfo x = new FileInfo("Baza.accdb");
            if (x.LastWriteTime.Date != DateTime.Now)
            {
                
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Baza.accdb");
                con.Open();
                OleDbCommand com = new OleDbCommand();
                DateTime lwt = x.LastWriteTime;
                if (x.LastWriteTime.Month != DateTime.Now.Month)
                {
                    com = new OleDbCommand("CREATE TABLE [" + lwt.ToString("yyyy MM") + "]([Predmet] Text, [Kolicina] Float, [Opstina] Text)", con);
                    com.ExecuteNonQuery();
                    com = new OleDbCommand("INSERT INTO [" + lwt.ToString("yyyy MM") + "] SELECT * FROM [Mesec]", con);
                    com.ExecuteNonQuery();
                    com = new OleDbCommand("DROP TABLE [Mesec]", con);
                    com.ExecuteNonQuery();
                    com = new OleDbCommand("CREATE TABLE [Mesec]([Predmet] Text, [Kolicina] Float, [Opstina] Text)", con);
                    com.ExecuteNonQuery();
                }
                com = new OleDbCommand("DROP TABLE [Dan]", con);
                com.ExecuteNonQuery();
                com = new OleDbCommand("CREATE TABLE [Dan]([Predmet] Text, [Kolicina] Float, [Opstina] Text)", con);
                com.ExecuteNonQuery();
                com = new OleDbCommand("INSERT INTO [Dan] SELECT * FROM [Dan1]", con);
                com.ExecuteNonQuery();
                com = new OleDbCommand("DROP TABLE [Kamioni]", con);
                com.ExecuteNonQuery();
                com = new OleDbCommand("CREATE TABLE [Kamioni]([Text] Text)", con);
                com.ExecuteNonQuery();
            }
        }
    }
}
