using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace mine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Visible = false;

            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= ../../mdb/CMMVS.MDB";
            OleDbConnection con = new OleDbConnection(connect);
            con.Open();

            // Заполнение списка горизонтов
            OleDbCommand horizont = new OleDbCommand("select NGOR from CMMVS group by NGOR", con);
            OleDbDataReader horizontList = horizont.ExecuteReader();

            while (horizontList.Read())
            {
                listHorizont.Items.Add(horizontList["NGOR"]);
            }
            listHorizont.SetSelected(0, true);
            listNbl.SetSelected(0, true);
            // --------------

            con.Close();
        }
        
        private void listHorizont_SelectedIndexChanged(object sender, EventArgs e)
        {
            listNbl.Items.Clear();
            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= ../../mdb/CMMVS.MDB";
            OleDbConnection con = new OleDbConnection(connect);
            con.Open();

            // Заполнение списка номеров блоков
            OleDbCommand nbl = new OleDbCommand("SELECT CMMVS.NBL FROM CMMVS WHERE CMMVS.NGOR=" +listHorizont.SelectedItem+ " GROUP BY CMMVS.NBL", con);
            OleDbDataReader nblList = nbl.ExecuteReader();

            while (nblList.Read())
            {
                listNbl.Items.Add(nblList["NBL"]);
            }
            // --------------

            con.Close();
            listNbl.SetSelected(0, true);
        }

        private void listNbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();

            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= ../../mdb/CMMVS.MDB";
            OleDbConnection con = new OleDbConnection(connect);
            con.Open();

            OleDbCommand data = new OleDbCommand("SELECT CMMVS.NSK, CMMVS.X, CMMVS.Y, CMMVS.Z, CMMVS.CUOB, CMMVS.CUOK, CMMVS.MOOB, CMMVS.MOSF FROM CMMVS WHERE CMMVS.NGOR=" +listHorizont.SelectedItem+ " AND CMMVS.NBL=" +listNbl.SelectedItem, con);
            OleDbDataReader dataGrid = data.ExecuteReader();

            int i = 0;
            int colls = dataGrid.FieldCount;
            while (dataGrid.Read())
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < colls; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = dataGrid[j];
                }
                i++;
            }
            con.Close();

        }

        private void TwoDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source= ../../mdb/CMMVS.MDB";
            OleDbConnection con = new OleDbConnection(connect);
            con.Open();

            // Подсчет записей:
            OleDbCommand dataCount = new OleDbCommand("SELECT CMMVS.NSK FROM CMMVS WHERE CMMVS.NGOR=" + listHorizont.SelectedItem + " AND CMMVS.NBL=" + listNbl.SelectedItem, con);
            OleDbDataReader dataCountReader = dataCount.ExecuteReader();
            int count = 0;
            while (dataCountReader.Read())
            {
                count++;
            }
            // ----------------

            // Формирование массива данных для импорта на форму графика:
            OleDbCommand dataQuery = new OleDbCommand("SELECT CMMVS.NSK, CMMVS.X, CMMVS.Y, CMMVS.CUOB, CMMVS.CUOK, CMMVS.MOOB, CMMVS.MOSF FROM CMMVS WHERE CMMVS.NGOR=" + listHorizont.SelectedItem + " AND CMMVS.NBL=" + listNbl.SelectedItem, con);
            OleDbDataReader dataRead = dataQuery.ExecuteReader();

            double[,] data2D = new double[count, dataRead.FieldCount];

            int i = 0;
            while (dataRead.Read())
            {
                for (int j = 0; j < dataRead.FieldCount; j++)
                {
                    if (dataRead[j].ToString() == "" || dataRead[j] == null)
                    {
                        data2D[i, j] = 0;
                    }
                    else
                    {
                        data2D[i, j] = Convert.ToDouble(dataRead[j]);
                    }
                }
                i++;
            }
            // ---------------------------------------------------------
            con.Close();

            Form graf2D = new _2D(data2D, count);
            graf2D.Show();
            
        }

        private void dToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void simplexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form simplexFrm = new Simplex();
            simplexFrm.Show();
        }
    }
}
