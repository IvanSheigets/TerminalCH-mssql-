using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace TerminalCH
{
    public partial class otgryzka_gp_enterRylon : Form
    {
        SqlCommand myCommand;
        public SqlConnection myConnection;
        SqlDataReader reader;
        private string strQuery;

        public bool boolEnterRylon = false;

        //double dNetto = 0;
        double dBrytto = 0;
        //int iMP = 0;
       // int iKolEtiket = 0;
       // int iZakazId = 0;



        public otgryzka_gp_enterRylon()
        {
            InitializeComponent();
            textBox_Partiya.Focus();

        }

      
        private void textBox_numRylon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
                e.Handled = true;
        }

        private void textBox_Partiya_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            }
        }

        private void textBox_Partiya_Leave(object sender, EventArgs e)
        {
            SelectZakaz(textBox_Partiya.Text, "");
        }

        private void otgryzka_gp_enterRylon_Load(object sender, EventArgs e)
        {
            myCommand = myConnection.CreateCommand();
         
        }

        private void SelectRylon(int zakazid)
        {
            strQuery = "select brytto-vagatary, brytto, dlinarylona, koletiketki from itak_vihidrylon where zakaz_id=" + zakazid.ToString();
            myCommand.CommandText = strQuery;
            reader = myCommand.ExecuteReader();
            while (reader.Read())
                dBrytto += Convert.ToDouble(reader["brytto"]);
            reader.Close();

            label_brytto.Text = dBrytto.ToString() + " кг";

        }

        private void SelectZakaz(string strPartiya, string strNumRylon)
        {
            //strQuery = "select partiya,datezakaz, id from itak_zakaz where partiya like '%" + strPartiya + "%'";
            if (strPartiya.Length != 0)
            {
                checkedListBox1.Items.Clear();
                int iCountRows = 0;

                if (strNumRylon.Length!=0)
                    strQuery = "SELECT DISTINCT z.partiya, p.product_name, z.datezakaz, z.timestartzakaz, z.id " +
                            " FROM itak_vihidrylon vh, itak_zakaz z, itak_product p" +
                            " WHERE vh.zakaz_id = z.id AND vh.product_id = p.id AND z.partiya LIKE  '%" + strPartiya + "%' and vh.num_rylon ="+strNumRylon;
                else
                    strQuery = "SELECT DISTINCT z.partiya, p.product_name, z.datezakaz, z.timestartzakaz, z.id " +
                            " FROM itak_vihidrylon vh, itak_zakaz z, itak_product p" +
                            " WHERE vh.zakaz_id = z.id AND vh.product_id = p.id AND z.partiya LIKE  '%" + strPartiya + "%'";

                myCommand.CommandText = strQuery;
                reader = myCommand.ExecuteReader();
                while (reader.Read())
                    iCountRows++;
                reader.Close();

               // if (iCountRows > 1)
                {
                    reader = myCommand.ExecuteReader();
                    try
                    {
                        string strDate;
                        while (reader.Read())
                        {
                            strDate = reader["datezakaz"].ToString();
                            strDate = strDate.Substring(0, 10);
                            checkedListBox1.Items.Add(
                                reader["partiya"].ToString() + "-" +
                                reader["product_name"].ToString() + "-" +
                                strDate
                                );
                        }
                    }
                    catch (System.Exception ex)
                    {
                        string str = ex.Message.ToString();
                    }
                    reader.Close();
                }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
               //MessageBox.Show(e.Index.ToString());
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, false);
            checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);

        }

        private void textBox_Partiya_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Partiya.Text.Length>=2)
                SelectZakaz(textBox_Partiya.Text, "");
        }

        private void textBox_numRylon_TextChanged(object sender, EventArgs e)
        {
            SelectZakaz(textBox_Partiya.Text, textBox_numRylon.Text);
        }

        private void textBox_numRylon_Leave(object sender, EventArgs e)
        {
            SelectZakaz(textBox_Partiya.Text, textBox_numRylon.Text);
        }

        private void button_enter_Click(object sender, EventArgs e)
        {
            boolEnterRylon = true;
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            boolEnterRylon = false;
            this.Close();
        }
    }
}
