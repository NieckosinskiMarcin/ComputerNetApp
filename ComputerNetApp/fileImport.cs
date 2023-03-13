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
using IronXL;

namespace ComputerNetApp
{
    public partial class fileImport : Form
    {
        String filePath = string.Empty;

        public fileImport()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "c:\\";
                ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    textBox1.Text = filePath;
                }    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool areDifferent = validateComboBox();
            if (textBox1.Text == "Ścieżka pliku")
            {
                MessageBox.Show("Nie wybrano pliku!", "WARNING",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!areDifferent)
                {
                    MessageBox.Show("Pola w selekcji nie mogą się powtarzać oraz nie mogą być puste!", "WARNING",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    WorkBook wb = WorkBook.Load(textBox1.Text);
                    WorkSheet ws = wb.DefaultWorkSheet;
                    DataTable dt = ws.ToDataTable(true);
                    dt.Columns.Remove("Wiek");
                    dt.Columns.Remove("Zatrudnienie");
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        String dataInsertion = String.Format("INSERT INTO CN_Test.dbo.CN_pracownicy ({0}, {1}, {2}, {3}, {4}) VALUES ",
                                comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, comboBox5.Text);
                        dataInsertion += String.Format("(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\');",
                            dt.Rows[i][4], dt.Rows[i][1], dt.Rows[i][2], dt.Rows[i][0], dt.Rows[i][3]);
                        SqlCommand insertion = new SqlCommand(dataInsertion, mainWindow.myConn);
                        insertion.ExecuteNonQuery();
                    }
                    MessageBox.Show("Ukończono import!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private bool validateComboBox()
        {
            List<String> selection = new List<String>();
            bool isAnyEmpty = false;
            selection.Add(comboBox1.Text);
            selection.Add(comboBox2.Text);
            selection.Add(comboBox3.Text);
            selection.Add(comboBox4.Text);
            selection.Add(comboBox5.Text);

            foreach (String select in selection)
            {
                if (select == "")
                {
                    isAnyEmpty = true;
                }
            }

            if (selection.Count != selection.Distinct().Count() || isAnyEmpty)
            {
                return false;
            }
            else return true;
        }
    }
}
