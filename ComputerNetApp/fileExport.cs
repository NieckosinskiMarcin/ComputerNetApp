using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerNetApp
{
    public partial class fileExport : Form
    {
        public fileExport()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            textBox1.Text = dialog.SelectedPath;
            textBox2.Text = DateTime.Now.ToString().Replace(".", "_").Replace(":", "_").Replace(" ", "_");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "Kliknij aby wybrać lokalizację...")
            {
                MessageBox.Show("Wybierz folder!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string getAllFromTableCmd = "SELECT * FROM CN_Test.dbo.CN_pracownicy";
                SqlCommand getData = new SqlCommand(getAllFromTableCmd, mainWindow.myConn);
                SqlDataAdapter da = new SqlDataAdapter(getData);
                DataTable dt = new DataTable();
                dt.TableName = "databaseXML";
                da.Fill(dt);
                da.Dispose();
                String filepath = textBox1.Text + "\\" + textBox2.Text + ".xml";
                using (StreamWriter sw = new StreamWriter(filepath))
                {
                    MemoryStream str = new MemoryStream();
                    dt.WriteXml(str, true);
                    str.Seek(0, SeekOrigin.Begin);
                    StreamReader sr = new StreamReader(str);
                    string xmlstr = sr.ReadToEnd();
                    sw.Write(xmlstr);
                    MessageBox.Show("Plik znajduje się w " + filepath, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
