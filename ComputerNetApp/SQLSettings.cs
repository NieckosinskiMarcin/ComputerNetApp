using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComputerNetApp;

namespace ComputerNetApp
{
    public partial class SQLSettings : Form
    {
        public SQLSettings()
        {
            InitializeComponent();
    }

        private void button1_Click(object sender, EventArgs e)
        {
            mainWindow.myConn = SQLConn.newConn(textBox1.Text, textBox2.Text, textBox3.Text);

            String verifierCmd = "SELECT database_id FROM sys.databases WHERE Name = 'CN_Test'";
            String dbstr = "CREATE DATABASE CN_Test;";

            String tablestr = "CREATE TABLE CN_Test.dbo.CN_pracownicy" +
                "(Prac_Dzial VARCHAR(50), Prac_Kod VARCHAR(50), Prac_Nazwisko VARCHAR(50)," +
                " Prac_Imie VARCHAR(50), Prac_Stanowisko VARCHAR(50));";

            SqlCommand verifyDBExist = new SqlCommand(verifierCmd, mainWindow.myConn);
            SqlCommand createDBCommand = new SqlCommand(dbstr, mainWindow.myConn);
            SqlCommand createTBCommabd = new SqlCommand(tablestr, mainWindow.myConn);

            mainWindow frm = Application.OpenForms.OfType<mainWindow>().FirstOrDefault();

            try {
                mainWindow.myConn.Open();
                object existFlag = verifyDBExist.ExecuteScalar();
                if (existFlag == null)
                {
                    createDBCommand.ExecuteNonQuery();
                    createTBCommabd.ExecuteNonQuery();
                    MessageBox.Show("Utworzono CN_Test i tabelę CN_pracownicy!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else { MessageBox.Show("Zalogowano!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                frm.enableButtons(true);
                this.Close();

            } 
            catch(System.Exception ex) { MessageBox.Show(ex.ToString(), "Error info", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
