using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerNetApp
{
    public partial class mainWindow : Form
    {
        public static SqlConnection myConn;

        public mainWindow()
        {
            InitializeComponent();
            if (myConn == null)
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLSettings sqlsettings = new SQLSettings();
            sqlsettings.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileImport fileimport = new fileImport();
            fileimport.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fileExport fileexport = new fileExport();
            fileexport.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if(myConn != null) { myConn.Close(); }
            base.OnFormClosing(e);
        }

        public void enableButtons(bool Enable)
        {
            this.button2.Enabled = Enable;
            this.button3.Enabled = Enable;
        }
    }
}
