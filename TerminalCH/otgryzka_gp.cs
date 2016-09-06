using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
//using BarCode39Reader;
using System.IO;
using System.Drawing.Imaging;

namespace TerminalCH
{
    public partial class otgryzka_gp : Form
    {
        SqlCommand myCommand;
        public SqlConnection myConnection;
        SqlDataReader reader;
        private string strQuery;

        int m_iCountRylon = 0;

        message_dialog MyDialog;

        List<string> m_lstBarCode;
        List<MyRylon> m_lstMyRylon;

        int m_iZakazId = 0;
        int m_iProductId = 0;
        int m_iProductIdNew = 0;
        double m_dNetto = 0;
        double m_dBrytto = 0;
        int m_iDlinaRylona = 0;
        int m_iCountEtiketki = 0;
        int m_iNumRylona = 0;
        int m_iRylonWidth = 0;

        int m_iDlinaEtiketki = 0;
        double m_dSquare = 0;

        bool m_bSelectRylon = true;
        bool m_bAddRylon = true;

        string m_strZakazchik = "";
        string m_strPartiya = "";
        string m_strDateZakaz = "";
        string m_strSmena = "";
        string m_strProductName = "";
        string m_strMaterial = "";
        string m_strTols = "";
        string m_strManagerName = "";

        
        int m_iZakazchikId = 0;
        int m_iZakazchikIdNew = 0;

        int m_iPaletteID = 0;

        double m_dAllNetto = 0;
        double m_dAllBrytto = 0;
        int m_iAllCountEtiketki = 0;
        int m_iAllDlinaRylonov = 0;


        int m_iCounter = 0;
        int m_iCountRows=0;


        bool testPrint = false;
        int testPrintCountRows = 0;

        ///////////////////////////////////
        int m_iColumnCount = 0;

        System.Drawing.Font fnt10Bold = new Font("Times New Roman", 10, FontStyle.Bold);
        System.Drawing.Font fnt10 = new Font("Times New Roman", 10, FontStyle.Regular);

        System.Drawing.Font fnt12Bold = new Font("Times New Roman", 12, FontStyle.Bold);
        System.Drawing.Font fnt12 = new Font("Times New Roman", 12, FontStyle.Regular);
        System.Drawing.Font fnt12BoldUnder = new Font("Times New Roman", 12, FontStyle.Bold | FontStyle.Underline);
        System.Drawing.Font fnt14Bold = new Font("Times New Roman", 14, FontStyle.Bold);
        System.Drawing.Font fnt14 = new Font("Times New Roman", 14, FontStyle.Regular);
        System.Drawing.Font fnt14BoldUnder = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Underline);
        System.Drawing.Font fnt16Bold = new Font("Times New Roman", 16, FontStyle.Bold);
        System.Drawing.Font fnt16BoldUnder = new Font("Times New Roman", 16, FontStyle.Bold | FontStyle.Underline);
        //////////////////////////////////////////
        

       
        bool m_boolPrintSpec = false;
        int m_iFlagPrintPages = 3; // печать страниц

        struct MyRylon
        {
            public int iBarCode;
            public int iNumRylona;
            public double dNetto;
            public double dBrytto;
            public int iDlinaRylona;
            public int iCountEtiketok;
            public int iZakazID;
            public int iProductID;
        }

        public otgryzka_gp()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MyClose();
        }

        private bool CheckConnect()
        {
            if (myConnection.State.ToString() == "Closed")
            {
                try
                {
                    myConnection.Open();
                    return true;
                }
                catch (System.Exception ex)
                {
                    WriteLog("Ошибка подключения к бд", ex);
                    return false;
                }
            }
            else
                return true;
        }

        private void WriteLog(string err, System.Exception ex)
        {
            ///////log file
            string strError = "";
            String current_time_str;
            StreamWriter logFile = null;
            ///////////////////////////

            try
            {
                FileInfo fi = new FileInfo("log.txt");
                logFile = fi.AppendText();
                current_time_str = DateTime.Now.ToString("[dd.MM:yyyy - HH:mm:ss]");
                strError = current_time_str + "- " + err + "- " + ex.Message;
                logFile.WriteLine(strError);
                logFile.Close();
            }
            catch (System.Exception ex1)
            {
                string s = ex1.Message;
                logFile.Close();
            }
        }

        private void otgryzka_gp_Load(object sender, EventArgs e)
        {
            textBox_barcode.Focus();
            dataGridView1.Columns.Add("pn", "№");
            dataGridView1.Columns.Add("barcode", "Код");
            dataGridView1.Columns.Add("num_rylon", "№ рулона");
            dataGridView1.Columns.Add("netto", "Нетто");
            dataGridView1.Columns.Add("brytto", "Брутто");
            dataGridView1.Columns.Add("count_etik", "К-во этик");

            dataGridView1.Columns["pn"].Width = 21;
            dataGridView1.Columns["barcode"].Width = 50;
            dataGridView1.Columns["num_rylon"].Width = 36;
            dataGridView1.Columns["netto"].Width = 34;
            dataGridView1.Columns["brytto"].Width = 34;
            dataGridView1.Columns["count_etik"].Width = 40;

            myCommand = myConnection.CreateCommand();

            label_netto.Text = m_dNetto.ToString() + " кг";
            label_brytto.Text = m_dBrytto.ToString() + " кг";
            label_dlinaRylonov.Text = m_iAllDlinaRylonov.ToString() + " м.";
            label_countEtiket.Text = m_iAllCountEtiketki.ToString() + " шт.";

            label_zakazchik.Text = m_strZakazchik;
                        
            m_lstBarCode = new List<string>();
            m_lstMyRylon = new List<MyRylon>();

            MyDialog = new message_dialog();
            

        }

        private void MyClose()
        {
            this.Close();
        }

        private void textBox_barcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OnEnterBarCode("");
            else if (e.KeyCode == Keys.F1 && button_menu.Enabled == true)
                ButtonMenu();
            else if (e.KeyCode == Keys.F2)
                ClearPalett();
            else if (e.KeyCode == Keys.F3)
                MyPrint();
            else if (e.KeyCode == Keys.F4)
                PriemGP();
            else if (e.KeyCode == Keys.F5)
                OtgryzkaGP();
            else if (e.KeyCode == Keys.F9)
                MyClose();
            //else if (e.KeyCode == Keys.F10)
               // TestPrint();


        }
        private void textBox_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            }
        }

        private void ClearVariable()
        {
            m_iZakazId = 0;
            m_iProductId = 0;
            m_dNetto = 0;
            m_dBrytto = 0;
            m_iDlinaRylona = 0;
            m_iCountEtiketki = 0;
            m_iNumRylona = 0;
           // m_lstMyRylon.Clear();
           // m_lstBarCode.Clear();
        }

        private void OnEnterBarCode(string strBar)
        {
            string strBarcode ="";
            if (strBar.Length != 0)
                strBarcode = strBar;
            else
                strBarcode = textBox_barcode.Text;

            if (strBarcode.Length != 0)//проверка на пустоту кода
            {
                if (strBarcode.Length>1)//проверка на длину больше 1
                {
                    int ibar = Convert.ToInt32(strBarcode.Substring(0, 1));
                    if (ibar == 4)
                    {
                        List<int> ls_iBarCodes = new List<int>();
                        strBarcode = strBarcode.Substring(1);

                        strQuery = "select zk.zakazchik_name , pr.product_name, sp.count_rylon, sp.netto, sp.brytto "+
                                    "from itak_etiketka.dbo.itak_zakazchik zk, itak_etiketka.dbo.itak_zakaz z, itak_etiketka.dbo.itak_product pr, itak_etiketka.dbo.itak_sklad_palette sp "+
                                    "where z.zakazchik_id=zk.id and z.id=sp.zakaz_id and sp.product_id=pr.id and sp.id="+strBarcode;
                        myCommand.CommandText = strQuery;
                        reader = myCommand.ExecuteReader();
                        try
                        {
                            reader.Read();

                            if (reader.HasRows)
                            {
                                if (MyDialog.Show("Добавление палеты\n\n" + reader[0].ToString().Trim() + "\n" + reader[1].ToString().Trim() + "\nК-во рулонов:" + reader[2].ToString().Trim() + "\nНетто: " + Convert.ToDouble(reader[3]).ToString("0.00").Trim() + "\nБрутто: " + Convert.ToDouble(reader[4]).ToString("0.00").Trim(), true) == 1)
                                {
                                    reader.Close();

                                    strQuery = "select id from itak_etiketka.dbo.itak_vihidrylon where palette_id=" + strBarcode;
                                    myCommand.CommandText = strQuery;
                                    reader = myCommand.ExecuteReader();
                                    try
                                    {
                                        while (reader.Read())
                                        {
                                            //OnEnterBarCode("3"+reader[0].ToString());
                                            ls_iBarCodes.Add(Convert.ToInt32("3" + reader[0].ToString()));
                                        }
                                        reader.Close();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        WriteLog("OnEnterBarCode() - otgryzka gp - 1",ex);
                                        reader.Close();
                                    }
                                }

                            }
                            else
                            {
                                MyDialog.Show("Палета не найдена");
                                reader.Close();
                            }

                        }
                        catch (System.Exception ex)
                        {
                            WriteLog("OnEnterBarCode() - otgryzka gp - 2", ex);
                            reader.Close();
                        }

                        for (int i=0;i<ls_iBarCodes.Count;i++)
                        {
                            OnEnterBarCode(ls_iBarCodes[i].ToString());
                        }

                        textBox_barcode.Clear();
                        textBox_barcode.Focus();
                    }
                    else if (ibar == 3)
                    {

                        strBarcode = strBarcode.Substring(1);
                        int iCounter = 0;
                        if (m_lstBarCode.Count != 0)
                        {
                            for (int i = 0; i < m_lstBarCode.Count; i++)
                            {
                                if (m_lstBarCode[i] == strBarcode)
                                {
                                    MyDialog.Show("Данный рулон уже есть в списке");
                                    m_bSelectRylon = false;
                                    m_bAddRylon = false;
                                    break;
                                    /*if (MyDialog.Show("Данный рулон уже есть в списке, добавить еще раз?", true) == 1)
                                    {
                                        m_lstBarCode.Add(strBarcode);
                                        m_bSelectRylon = true;
                                        m_bAddRylon = true;
                                        break;
                                    }
                                    else 
                                    {
                                        m_bSelectRylon = false;
                                        m_bAddRylon = false;
                                        textBox_barcode.Clear();
                                        textBox_barcode.Focus();
                                        break;
                                    }*/
                                }
                                else iCounter++;
                            }
                        }

                        if (iCounter == m_lstBarCode.Count)
                        {
                            m_bSelectRylon = true;
                            m_bAddRylon = true;
                        }

                        if (m_bSelectRylon == true)
                        {
                            ClearVariable();

                            strQuery = "select zakaz_id, product_id, vagatary, brytto, dlinarylona, koletiketki, num_rylon, width from itak_etiketka.dbo.itak_vihidrylon where id=" + strBarcode;
                            myCommand.CommandText = strQuery;
                            reader = myCommand.ExecuteReader();
                            try
                            {
                                reader.Read();

                                if (reader.HasRows)
                                {
                                    m_iZakazId = Convert.ToInt32(reader["zakaz_id"]);

                                    if (m_iProductId == 0)
                                        m_iProductId = m_iProductIdNew = Convert.ToInt32(reader["product_id"]);
                                    else
                                        m_iProductIdNew = Convert.ToInt32(reader["product_id"]);

                                    m_dNetto = Convert.ToDouble(reader["brytto"]) - Convert.ToDouble(reader["vagatary"]);
                                    m_dBrytto = Convert.ToDouble(reader["brytto"]);
                                    m_iDlinaRylona = Convert.ToInt32(reader["dlinarylona"]);
                                    m_iCountEtiketki = Convert.ToInt32(reader["koletiketki"]);
                                    m_iNumRylona = Convert.ToInt32(reader["num_rylon"]);
                                    m_iRylonWidth = Convert.ToInt32(reader["width"]);
                                    m_iZakazId = Convert.ToInt32(reader["zakaz_id"]);
                                    m_iProductId = Convert.ToInt32(reader["product_id"]);

                                    m_bAddRylon = true;
                                }
                                else
                                {
                                    MyDialog.Show("Рулон не найден. Проверьте корректность ввода");
                                    m_bAddRylon = false;
                                }
                                reader.Close();
                            }
                            catch (System.Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                                WriteLog("OnEnterBarCode() - otgryzka gp - 3", ex);
                                reader.Close();
                                ClearVariable();
                            }



                            if (m_bAddRylon == true)
                            {

                                ////получение первоначальных значений для заполнения полей связаных с заказом
                                if (m_strZakazchik.Length == 0 && m_iZakazId != 0)
                                {
                                    strQuery = "select z.zakazchik_id, zk.zakazchik_name, z.partiya, z.datezakaz, z.machine, z.smena, pm.product_material, pt.product_tols, z.dlinaetiketki, m.manager_name " +
                                    " from itak_etiketka.dbo.itak_zakaz z, itak_etiketka.dbo.itak_zakazchik zk, itak_etiketka.dbo.itak_productmaterial pm, itak_etiketka.dbo.itak_producttols pt, itak_manager m "+
                                    " where z.producttols_id=pt.id and pm.id=z.productmaterial_id and z.zakazchik_id=zk.id and z.manager_id=m.id and z.id=" + m_iZakazId;
                                    myCommand.CommandText = strQuery;
                                    reader = myCommand.ExecuteReader();
                                    try
                                    {
                                        reader.Read();
                                        if (reader.HasRows)
                                        {
                                            m_strPartiya = reader["partiya"].ToString().Trim();
                                            m_strZakazchik = reader["zakazchik_name"].ToString().Trim();

                                            m_strDateZakaz = reader["datezakaz"].ToString().Trim();
                                            if (m_strDateZakaz.Length > 11)
                                                m_strDateZakaz = m_strDateZakaz.Remove(11);

                                            m_strSmena = reader["machine"].ToString().Trim() + "/" + reader["smena"].ToString().Trim();
                                            m_strTols = reader["product_tols"].ToString().Trim();
                                            m_strMaterial = reader["product_material"].ToString().Trim() + " " + m_strTols + "x" + m_iRylonWidth.ToString().Trim();

                                            m_iZakazchikId = m_iZakazchikIdNew = Convert.ToInt32(reader["zakazchik_id"]);

                                            m_iDlinaEtiketki = Convert.ToInt32(reader["dlinaetiketki"]);
                                            m_strManagerName = reader["manager_name"].ToString().Trim();

                                            label_zakazchik.Text = m_strZakazchik;

                                        }
                                        reader.Close();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        WriteLog("OnEnterBarCode() - otgryzka gp - 4", ex);
                                        string str = ex.Message.ToString();
                                        reader.Close();
                                    }
                                  
                                }



                                ////////получение последующих значении заказчика и проверка на добаление этого рулона
                                /*if (m_strZakazchik.Length != 0 && m_iZakazId != 0 && m_bAddRylon == true)
                                {
                                    strQuery = "select zakazchik_id from itak_zakaz where id=" + m_iZakazId;
                                    myCommand.CommandText = strQuery;
                                    reader = myCommand.ExecuteReader();
                                    try
                                    {
                                        reader.Read();
                                        if (reader.HasRows)
                                        {
                                            m_iZakazchikIdNew = Convert.ToInt32(reader["zakazchik_id"]);
                                            if (m_iZakazchikIdNew != m_iZakazchikId)
                                            {
                                                if (MyDialog.Show("Данный рулон другого заказчика. Добавить его?", true) == 1)
                                                    m_bAddRylon = true;
                                                else
                                                    m_bAddRylon = false;
                                            }
                                            else m_bAddRylon = true;
                                        }
                                        reader.Close();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        string str = ex.Message.ToString();
                                        reader.Close();
                                    }
                                }*/

                                if (m_iProductId != m_iProductIdNew)
                                    if (MyDialog.Show("Данный рулон другого вида! Добавить его?", true) == 1)
                                    {
                                        m_bAddRylon = true;
                                    }
                                    else m_bAddRylon = false;
                                /////////////////////////////////////

                                //
                                if (m_strProductName.Length == 0 && m_iZakazId != 0)
                                {
                                    reader.Close();
                                    strQuery = "select product_name from itak_etiketka.dbo.itak_product where id=" + m_iProductId;
                                    myCommand.CommandText = strQuery;
                                    reader = myCommand.ExecuteReader();
                                    try
                                    {
                                        reader.Read();
                                        if (reader.HasRows)
                                            m_strProductName = reader["product_name"].ToString().Trim();

                                        reader.Close();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        //MessageBox.Show(ex.Message);
                                        WriteLog("OnEnterBarCode() - otgryzka gp - 5", ex);
                                        reader.Close();
                                    }
                                }
                            }
                        }

                        if (m_bAddRylon == true)
                        {
                            //MessageBox.Show("add");
                            int iCounter1 = dataGridView1.Rows.Count;
                            //dataGridView1.Rows.Insert(0, 0, strBarcode);
                            if (m_lstBarCode.Count == 0 || iCounter == m_lstBarCode.Count)
                            {
                                MyRylon mr;
                                mr.iBarCode = Convert.ToInt32(strBarcode);
                                mr.iNumRylona = m_iNumRylona;
                                mr.iCountEtiketok = m_iCountEtiketki;
                                mr.iDlinaRylona = m_iDlinaRylona;
                                mr.dNetto = m_dNetto;
                                mr.dBrytto = m_dBrytto;
                                mr.iProductID = m_iProductId;
                                mr.iZakazID = m_iZakazId;

                                m_iAllDlinaRylonov += m_iDlinaRylona;
                                m_iAllCountEtiketki += m_iCountEtiketki;
                                m_dAllNetto += m_dNetto;
                                m_dAllBrytto += m_dBrytto;

                                m_lstMyRylon.Add(mr);

                                m_lstBarCode.Add(strBarcode);
                                m_iCountRylon++;

                                dataGridView1.Rows.Insert(0,
                                                         iCounter1.ToString(),
                                                         "3" + strBarcode,
                                                         m_iNumRylona.ToString(),
                                                         m_dNetto.ToString(),
                                                         m_dBrytto.ToString(),
                                                         m_iCountEtiketki.ToString());

                                if (iCounter1 % 2 == 0)
                                    for (int i = 0; i < dataGridView1.Rows[0].Cells.Count; i++)
                                        dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.Cyan;
                                else
                                    for (int i = 0; i < dataGridView1.Rows[0].Cells.Count; i++)
                                        dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.Yellow;

                                label_brytto.Text = m_dAllBrytto.ToString("0.00").Trim() + " кг";
                                label_netto.Text = m_dAllNetto.ToString("0.00").Trim() + " кг";
                                label_countEtiket.Text = m_iAllCountEtiketki + " шт.";
                                label_dlinaRylonov.Text = m_iAllDlinaRylonov + " м.";

                            }

                        }

                        textBox_barcode.Clear();
                        textBox_barcode.Focus();
                    }
                    
                }
                else//проверка на длину больше 1
                {
                    MyDialog.Show("Некоректный код");
                    textBox_barcode.Clear();
                    textBox_barcode.Focus();
                }
            }
            else//проверка на пустоту кода
            {
                MyDialog.Show("Введите код товара");
                textBox_barcode.Clear();
                textBox_barcode.Focus();
            }
        }

        private List<MyRylon> SetListTempValue(int countItems)
        {
            List<MyRylon> list = new List<MyRylon>();
            Random rnd = new Random();

            for (int i = 0; i < countItems; i++)
            {
                MyRylon item = new MyRylon();

                item.iNumRylona = i + 1;
                item.iBarCode = rnd.Next(100000, 500000);
                list.Add(item);
            }

            return list;
        }

        private void TestPrint()
        {
            testPrint = true;
            testPrintCountRows = 780;

            m_lstMyRylon = SetListTempValue(testPrintCountRows);

            /*if (MyDialog.Show("Друкувати специфікацію?", true) == 1)
                m_boolPrintSpec = true;
            else
                m_boolPrintSpec = false;*/
            // for (int i = 0; i < 2; i++)
            {
                m_iFlagPrintPages = 3;
                PrintDocument docPrint = new PrintDocument();
                if (docPrint.PrinterSettings.IsValid)
                {
                    docPrint.DefaultPageSettings.Margins.Top = 10;
                    docPrint.DefaultPageSettings.Margins.Bottom = 10;
                    docPrint.DefaultPageSettings.Margins.Right = 10;
                    docPrint.DefaultPageSettings.Margins.Left = 10;
                    docPrint.PrintPage += new PrintPageEventHandler
                            (this.docPrint_PrintPage);
                    docPrint.PrinterSettings.PrintFileName = "ИтакОтгрузка";

                    //docPrintt.PrinterSettings.Copies = (short)pr.m_iNumCopies;
                    docPrint.PrinterSettings.Copies = 2;

                    PrintPreviewDialog dlgPrint = new PrintPreviewDialog();
                    dlgPrint.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height);
                    dlgPrint.Document = docPrint;

                    //dlgPrint.ShowDialog();
                    docPrint.Print();
                }
            }
        }

        private void MyPrint()
        {
            if (dataGridView1.Rows.Count > 1)
            {
                DateTime dt = System.DateTime.Now;
                string strCreatePaletteTime = "";
                strCreatePaletteTime = dt.Year + "." + dt.Month + "." + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;

                if (CheckConnect())
                {
                    if (m_lstMyRylon.Count > 0)
                    {
                        strQuery = "select palette_id from itak_etiketka.dbo.itak_vihidrylon where id=" + m_lstMyRylon[0].iBarCode;
                        myCommand.CommandText = strQuery;
                        reader = myCommand.ExecuteReader();
                        try
                        {
                            reader.Read();
                            if (reader[0] != DBNull.Value && Convert.ToInt16(reader[0])!=0)
                            {
                                m_iPaletteID = Convert.ToInt32(reader[0]);
                                reader.Close();

                                strQuery = "select count_rylon from itak_etiketka.dbo.itak_sklad_palette where id=" + m_iPaletteID;
                                myCommand.CommandText = strQuery;
                                reader = myCommand.ExecuteReader();
                                try
                                {
                                    reader.Read();
                                    if (reader.HasRows)
                                    {
                                        int iCountRylon = Convert.ToInt32(reader[0]);
                                        reader.Close();
                                        if (m_lstMyRylon.Count!=iCountRylon)
                                        {
                                            strQuery = "insert into itak_etiketka.dbo.itak_sklad_palette (date_create) values (convert(smalldatetime,'" + strCreatePaletteTime + "',101))";
                                            try
                                            {
                                                myCommand.CommandText = strQuery;
                                                myCommand.ExecuteNonQuery();
                                            }
                                            catch (System.Exception ex)
                                            {
                                                WriteLog("otgryzka_gp - MyPrint() - вставка в itak_sklad_palette", ex);
                                            }

                                            strQuery = "select @@IDENTITY AS 'Identity'";
                                            myCommand.CommandText = strQuery;
                                            reader = myCommand.ExecuteReader();
                                            try
                                            {
                                                reader.Read();
                                                if (reader.HasRows)
                                                {
                                                    m_iPaletteID = Convert.ToInt32(reader[0]);
                                                   
                                                }
                                                reader.Close();
                                            }
                                            catch (System.Exception ex)
                                            {
                                                WriteLog("otgryzka_gp - MyPrint() - получение ID вставленной записи", ex);
                                                reader.Close();
                                            }
                                        }                                        
                                    }

                                }
                                catch (System.Exception ex)
                                {
                                    WriteLog("otgryzka_gp - MyPrint() - получение количества рулонв в палете", ex);
                                    reader.Close();
                                }
                            }
                            else
                            {
                                reader.Close();
                                strQuery = "insert into itak_etiketka.dbo.itak_sklad_palette (date_create) values (convert(smalldatetime,'" + strCreatePaletteTime + "',101))";
                                try
                                {
                                    myCommand.CommandText = strQuery;
                                    myCommand.ExecuteNonQuery();
                                }
                                catch (System.Exception ex)
                                {
                                    WriteLog("otgryzka_gp - MyPrint() - вставка в itak_sklad_palette", ex);
                                }

                                strQuery = "select @@IDENTITY AS 'Identity'";
                                myCommand.CommandText = strQuery;
                                reader = myCommand.ExecuteReader();
                                try
                                {
                                    reader.Read();
                                    if (reader.HasRows)
                                    {
                                        m_iPaletteID = Convert.ToInt32(reader[0]);
                                        
                                    }
                                    reader.Close();
                                }
                                catch (System.Exception ex)
                                {
                                    WriteLog("otgryzka_gp - MyPrint() - получение ID вставленной записи", ex);
                                    reader.Close();
                                }

                            }

                        }
                        catch (System.Exception ex)
                        {
                            WriteLog("otgryzka_gp - MyPrint() - получение ID вставленной записи", ex);
                            reader.Close();
                        }

                    }

                    if (m_lstMyRylon.Count > 0)
                    {

                        int iRylonCount = 0;
                        double dNetto = 0;
                        double dBrytto = 0;
                        reader.Close();
                        for (int i = 0; i < m_lstMyRylon.Count; i++)
                        {
                            strQuery = "update itak_etiketka.dbo.itak_vihidrylon set palette_id=" + m_iPaletteID + " where id=" + m_lstMyRylon[i].iBarCode;
                            try
                            {
                                myCommand.CommandText = strQuery;
                                myCommand.ExecuteNonQuery();
                                iRylonCount++;
                                dNetto += m_lstMyRylon[i].dNetto;
                                dBrytto += m_lstMyRylon[i].dBrytto;

                            }
                            catch (System.Exception ex)
                            {
                                WriteLog("otgryzka_gp - MyPrint() - обновление itak_vihidrtlon - вставка номера палеты на каждый рулон", ex);

                            }
                        }
                        string strNetto = dNetto.ToString();
                        strNetto = strNetto.Replace(',', '.');
                        string strBrytto = dBrytto.ToString();
                        strBrytto = strBrytto.Replace(',', '.');

                        strQuery = "update itak_etiketka.dbo.itak_sklad_palette set " +
                                                                    "zakaz_id=" + m_lstMyRylon[0].iZakazID +
                                                                    ", product_id=" + m_lstMyRylon[0].iProductID +
                                                                    ", count_rylon=" + iRylonCount +
                                                                    ", netto=" + strNetto +
                                                                    ", brytto=" + strBrytto +
                                                                    " where id=" + m_iPaletteID;
                        try
                        {
                            myCommand.CommandText = strQuery;
                            myCommand.ExecuteNonQuery();
                        }
                        catch (System.Exception ex)
                        {
                            WriteLog("otgryzka_gp - MyPrint() - обновление itak_sklad_palette - вставка информации про сосканированые руллоны", ex);
                        }
                    }


                    

                    /*if (MyDialog.Show("Друкувати специфікацію?", true) == 1)
                        m_boolPrintSpec = true;
                    else
                        m_boolPrintSpec = false;*/
                   // for (int i = 0; i < 2; i++)
                    {
                        m_iFlagPrintPages = 3;
                        PrintDocument docPrint = new PrintDocument();
                        if (docPrint.PrinterSettings.IsValid)
                        {
                            docPrint.DefaultPageSettings.Margins.Top = 10;
                            docPrint.DefaultPageSettings.Margins.Bottom = 10;
                            docPrint.DefaultPageSettings.Margins.Right = 10;
                            docPrint.DefaultPageSettings.Margins.Left = 10;
                            docPrint.PrintPage += new PrintPageEventHandler
                                   (this.docPrint_PrintPage);
                            docPrint.PrinterSettings.PrintFileName = "ИтакОтгрузка";

                            //docPrintt.PrinterSettings.Copies = (short)pr.m_iNumCopies;
                            docPrint.PrinterSettings.Copies = 2;

                            PrintPreviewDialog dlgPrint = new PrintPreviewDialog();
                            dlgPrint.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Height);
                            dlgPrint.Document = docPrint;

                            //dlgPrint.ShowDialog();
                            docPrint.Print();

                        }
                        else
                            MessageBox.Show("Принтер не доступен!");
                    }
                }
                else
                    MessageBox.Show("Список отгрузки не должен быть пустым!");
            }
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            MyPrint();
        }

        private void DrawColumnHeaders(PrintPageEventArgs e, int[] iCoord1, int columnCount, int iLeft1, ref int t1, int iTableWidth1)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(iLeft1, t1), new Point(iTableWidth1, t1));
            e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(iLeft1, t1 + 35), new Point(iTableWidth1, t1 + 35));
            for (int j = 0; j <= columnCount - 1; j++)
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(iCoord1[j], t1 - 1), new Point(iCoord1[j], t1 + 36));

            e.Graphics.DrawString("№", fnt10Bold, Brushes.Black, iLeft1 + 15, t1 + 2);
            e.Graphics.DrawString("рулона", fnt10Bold, Brushes.Black, iLeft1, t1 + 15);

            e.Graphics.DrawString("Масса", fnt10Bold, Brushes.Black, iLeft1 + 57, t1 + 2);
            e.Graphics.DrawString("нетто, кг", fnt10Bold, Brushes.Black, iLeft1 + 48, t1 + 15);

            e.Graphics.DrawString("Масса", fnt10Bold, Brushes.Black, iLeft1 + 121, t1 + 2);
            e.Graphics.DrawString("брутто, кг", fnt10Bold, Brushes.Black, iLeft1 + 108, t1 + 15);

            e.Graphics.DrawString("Довжина", fnt10Bold, Brushes.Black, iLeft1 + 176, t1 + 2);
            e.Graphics.DrawString("м.п.", fnt10Bold, Brushes.Black, iLeft1 + 193, t1 + 15);

            e.Graphics.DrawString("Кількість", fnt10Bold, Brushes.Black, iLeft1 + 238, t1 + 2);
            e.Graphics.DrawString("тис. шт.", fnt10Bold, Brushes.Black, iLeft1 + 242, t1 + 15);

            e.Graphics.DrawString("Площа", fnt10Bold, Brushes.Black, iLeft1 + 304, t1 + 2);
            e.Graphics.DrawString("м. кв.", fnt10Bold, Brushes.Black, iLeft1 + 313, t1 + 15);

            t1 += 36;
        }

        private void DrawColumnHeaderSeparator(PrintPageEventArgs e, int[] iCoord1, int iColumnCount, int t1)
        {
            for (int j = 0; j <= iColumnCount - 1; j++)
                if (j == 0 || j == iColumnCount - 1)
                    e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(iCoord1[j], t1 - 1), new Point(iCoord1[j], t1 + 19));
                else
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(iCoord1[j], t1 - 1), new Point(iCoord1[j], t1 + 19));
        }

        private void DrawRylonData(PrintPageEventArgs e, int iCountRows, int iCurrentRylon, int [] iCoord1, int iLeft1, int t1, int iTableWidth1)
        {
            string strTemp1 = m_lstMyRylon[iCurrentRylon].iNumRylona.ToString();//надо
            if (Convert.ToInt32(strTemp1) > 9)
                e.Graphics.DrawString(m_lstMyRylon[iCurrentRylon].iNumRylona.ToString(), fnt10, Brushes.Black, iCoord1[0] + 16, t1);
            else
                e.Graphics.DrawString(m_lstMyRylon[iCurrentRylon].iNumRylona.ToString(), fnt10, Brushes.Black, iCoord1[0] + 23, t1);

            e.Graphics.DrawString(m_lstMyRylon[iCurrentRylon].dNetto.ToString("0.00"), fnt10, Brushes.Black, iCoord1[1] + 3, t1);
            e.Graphics.DrawString(m_lstMyRylon[iCurrentRylon].dBrytto.ToString("0.00"), fnt10, Brushes.Black, iCoord1[2] + 3, t1);
            e.Graphics.DrawString(m_lstMyRylon[iCurrentRylon].iDlinaRylona.ToString(), fnt10, Brushes.Black, iCoord1[3] + 3, t1);
            e.Graphics.DrawString(m_lstMyRylon[iCurrentRylon].iCountEtiketok.ToString(), fnt10, Brushes.Black, iCoord1[4] + 3, t1);
            double square = (m_lstMyRylon[iCurrentRylon].iDlinaRylona * m_iRylonWidth) / 1000.0;
            m_dSquare += square;
            e.Graphics.DrawString(square.ToString("0.0"), fnt10, Brushes.Black, iCoord1[5] + 3, t1);

            if (iCurrentRylon == iCountRows - 1 || iCurrentRylon == (iCountRows - 1) / 2)
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(iLeft1, t1 + 18), new Point(iTableWidth1, t1 + 18));
            else
                e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(iLeft1, t1 + 18), new Point(iTableWidth1, t1 + 18));
        }

        private void DrawSummaryInformation(PrintPageEventArgs e, ref int t)
        {
            e.Graphics.DrawString("Маса нетто:", fnt12Bold, Brushes.Black, 40, t);
            e.Graphics.DrawString(m_dAllNetto.ToString("0.00") + " кг", fnt12BoldUnder, Brushes.Black, 140, t);

            // if (m_strDateVigotv.Length > 11)
            //   m_strDateVigotv = m_strDateVigotv.Remove(11);
            e.Graphics.DrawString("Дата виготовлення:", fnt12Bold, Brushes.Black, 450, t);
            e.Graphics.DrawString(m_strDateZakaz, fnt12BoldUnder, Brushes.Black, 610, t);

            t += 25;
            e.Graphics.DrawString("Маса брутто:", fnt12Bold, Brushes.Black, 40, t);
            e.Graphics.DrawString(m_dAllBrytto.ToString("0.00") + " кг", fnt12BoldUnder, Brushes.Black, 150, t);

            t += 25;
            e.Graphics.DrawString("Кількість м.п.:", fnt12Bold, Brushes.Black, 40, t);
            e.Graphics.DrawString(m_iAllDlinaRylonov.ToString() + " м.", fnt12BoldUnder, Brushes.Black, 160, t);

            e.Graphics.DrawString("Зміна:", fnt12Bold, Brushes.Black, 450, t);
            e.Graphics.DrawString(m_strSmena, fnt12BoldUnder, Brushes.Black, 505, t);

            t += 25;
            e.Graphics.DrawString("Кількість рулонів:", fnt12Bold, Brushes.Black, 40, t);
            e.Graphics.DrawString(m_iCountRylon.ToString() + " шт.", fnt12BoldUnder, Brushes.Black, 190, t);

            t += 25;
            e.Graphics.DrawString("Кількість етикетки тис.шт.:", fnt12Bold, Brushes.Black, 40, t);
            e.Graphics.DrawString(m_iAllCountEtiketki + " шт.", fnt12BoldUnder, Brushes.Black, 265, t);

            t += 25;
            e.Graphics.DrawString("Майстер:", fnt12Bold, Brushes.Black, 550, t);
            e.Graphics.DrawString("______________", fnt12Bold, Brushes.Black, 630, t);

            e.Graphics.DrawString("Загальна площа:", fnt12Bold, Brushes.Black, 40, t);
            e.Graphics.DrawString(m_dSquare.ToString("0.00") + " м.кв.", fnt12BoldUnder, Brushes.Black, 180, t);

            t += 40;
            e.Graphics.DrawString("Термін зберігання - 12 місяців від дати виготовлення.", fnt14Bold, Brushes.Black, 40, t);
            t += 25;
            e.Graphics.DrawString("Умови зберігання:", fnt12Bold, Brushes.Black, 40, t);
            t += 20;
            e.Graphics.DrawString("Плівка повинна зберігатися в закритому приміщенні при температурі +5С до +25С ", fnt12Bold, Brushes.Black, 40, t);
            t += 20;
            e.Graphics.DrawString("і відносній вологості повітря не більше 80%, на відстані не менш 1 метра від нагріваючих", fnt12Bold, Brushes.Black, 40, t);
            t += 20;
            e.Graphics.DrawString("пристроїв, в захищеному місці від дії прямих сонячних променів, при відсутності у", fnt12Bold, Brushes.Black, 40, t);
            t += 20;
            e.Graphics.DrawString("приміщенні кислотно лужних та інших агресивних середовищ.", fnt12Bold, Brushes.Black, 40, t);
        }
        
        //////////////////////////////////////////////////////////////////////////
        private void docPrint_PrintPage(object sender, PrintPageEventArgs e)
        {
           /* m_strZakazchik = "ПАТ ОБОЛОНЬ";
            m_strNaimenovanie = "Зыберт";*/
           
            
            if (m_iFlagPrintPages==1)
            {
                /*int top = 70;
                e.Graphics.DrawString("Палетна накладна", fnt16Bold, Brushes.Black, 320, top);
               // e.Graphics.DrawImage((Image.FromFile("itak.jpg")), 40, e.PageBounds.Top + 25, 700, 140);
               // e.Graphics.DrawImage((Image.FromFile("zont.jpg")), 490, e.PageBounds.Top + 130, 150, 150);
                // e.Graphics.DrawLine(new Pen(Color.Black, 2), 20, 90, e.PageSettings.Bounds.Width - 50, 90);

                top += 50;
                e.Graphics.DrawString("Виробник:", fnt12Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString("ТОВ «ІТАК»", fnt14Bold, Brushes.Black, 150, top);
                top += 20;
                e.Graphics.DrawString("02660, Україна, м. Київ, вул. Червоноткацька, 44", fnt12Bold, Brushes.Black, 150, top);

                top += 30;
                e.Graphics.DrawString("Замовник:", fnt12Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_strZakazchik, fnt16Bold, Brushes.Black, 150, top-4);

                top += 30;
                e.Graphics.DrawString("Плівка з малюнком", fnt12Bold, Brushes.Black, 50, top);
                top += 20;
                e.Graphics.DrawString("ТУ У 22.1-16476839-001-2004", fnt12Bold, Brushes.Black, 50, top);

                top += 35;
                e.Graphics.DrawString(m_strProductName, fnt14Bold, Brushes.Black, 50, top);

                if (m_iZakazchikId == 71)
                {
                    e.Graphics.DrawString("Арт.", fnt16Bold, Brushes.Black, 400, top - 4);
                    if (m_iProductId == 590)//пташине молоко
                        e.Graphics.DrawString("3921 40 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 944)//сатурн
                        e.Graphics.DrawString("3921 43 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 943)//аркадия
                        e.Graphics.DrawString("3921 33 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1298 || m_iProductId == 679)
                        e.Graphics.DrawString("3921 34 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1536)
                        e.Graphics.DrawString("3921 37 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1421)
                        e.Graphics.DrawString("3921 38 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 942)
                        e.Graphics.DrawString("3921 35 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId ==1420)
                        e.Graphics.DrawString("3921 36 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1068)//кекс вишневый
                        e.Graphics.DrawString("3921 46 082 7 97", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if(m_iProductId == 2094)//кекс с логотипом
                        e.Graphics.DrawString("3921 20 082 7 97", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                }

                top += 35;

                e.Graphics.DrawString("Партія №:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_strPartiya, fnt14BoldUnder, Brushes.Black, 150, top);

                //////////////////////////////////////////////////////////////////////////
                top += 30;
                e.Graphics.DrawString("Маса нетто:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_dAllNetto.ToString("0.00") + " кг", fnt14BoldUnder, Brushes.Black, 170, top);

                if (m_iZakazchikId == 71)
                    e.Graphics.DrawString("Пакувальник _______________", fnt14Bold, Brushes.Black, 400, top - 4);

                top += 30;
                e.Graphics.DrawString("Маса брутто:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_dAllBrytto.ToString("0.00") + " кг", fnt14BoldUnder, Brushes.Black, 180, top);

                top += 30;
                e.Graphics.DrawString("Кількість рулонів:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_iCountRylon + " шт.", fnt14BoldUnder, Brushes.Black, 230, top);

                if (m_iAllCountEtiketki != 0)
                {
                    top += 30;
                    e.Graphics.DrawString("Кількість етикеток:", fnt14Bold, Brushes.Black, 50, top);
                    e.Graphics.DrawString(m_iAllCountEtiketki + " шт.", fnt14BoldUnder, Brushes.Black, 245, top);
                }

                top += 30;
                e.Graphics.DrawString("Кількість м.п.:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_iAllDlinaRylonov.ToString()+" м", fnt14BoldUnder, Brushes.Black, 200, top);

                top += 30;
                //if (m_strDateVigotv.Length!=0)
                //  m_strDateVigotv = m_strDateVigotv.Remove(m_strDateVigotv.IndexOf("0:"));

               // if (m_strDateVigotv.Length > 11)
                //    m_strDateVigotv = m_strDateVigotv.Remove(11);
                e.Graphics.DrawString("Дата виготовлення:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_strDateZakaz, fnt14BoldUnder, Brushes.Black, 245, top);

                top += 35;
                e.Graphics.DrawString("Менеджер:", fnt12Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_strManagerName, fnt12BoldUnder, Brushes.Black, 140, top);

                top += 40;
                e.Graphics.DrawString("Термін зберігання – 12 місяців від дати виготовлення.", fnt14Bold, Brushes.Black, 50, top);

                top += 70;
                //e.Graphics.DrawLine(new Pen(Color.Gray, 1), 20, top, e.PageSettings.Bounds.Width - 50, top);

                

                e.Graphics.DrawLine(new Pen(Color.Gray,1), 20, top, e.PageSettings.Bounds.Width - 50, top);
                top += 70;

                //////////////////////////////////////////////////////////////////////////
                e.Graphics.DrawString("Палетна накладна", fnt16Bold, Brushes.Black, 320, top);
                // e.Graphics.DrawImage((Image.FromFile("itak.jpg")), 40, e.PageBounds.Top + 25, 700, 140);
                // e.Graphics.DrawImage((Image.FromFile("zont.jpg")), 490, e.PageBounds.Top + 130, 150, 150);
                // e.Graphics.DrawLine(new Pen(Color.Black, 2), 20, 90, e.PageSettings.Bounds.Width - 50, 90);

                top += 50;
                e.Graphics.DrawString("Виробник:", fnt12Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString("ТОВ «ІТАК»", fnt14Bold, Brushes.Black, 150, top);
                top += 20;
                e.Graphics.DrawString("02660, Україна, м. Київ, вул. Червоноткацька, 44", fnt12Bold, Brushes.Black, 150, top);

                top += 30;
                e.Graphics.DrawString("Замовник:", fnt12Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_strZakazchik, fnt16Bold, Brushes.Black, 150, top - 4);


                top += 30;
                e.Graphics.DrawString("Плівка з малюнком", fnt12Bold, Brushes.Black, 50, top);
                top += 20;
                e.Graphics.DrawString("ТУ У 22.1-16476839-001-2004", fnt12Bold, Brushes.Black, 50, top);

                top += 35;
                e.Graphics.DrawString(m_strProductName, fnt14Bold, Brushes.Black, 50, top);

                if (m_iZakazchikId == 71)
                {
                    e.Graphics.DrawString("Арт.", fnt16Bold, Brushes.Black, 400, top - 4);
                    if (m_iProductId == 590)//пташине молоко
                        e.Graphics.DrawString("3921 40 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 944)//сатурн
                        e.Graphics.DrawString("3921 43 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 943)//аркадия
                        e.Graphics.DrawString("3921 33 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1298 || m_iProductId == 679)
                        e.Graphics.DrawString("3921 34 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1536)
                        e.Graphics.DrawString("3921 37 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1421)
                        e.Graphics.DrawString("3921 38 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 942)
                        e.Graphics.DrawString("3921 35 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1420)
                        e.Graphics.DrawString("3921 36 082 7 90", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 1068)//кекс вишневый
                        e.Graphics.DrawString("3921 46 082 7 97", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                    else if (m_iProductId == 2094)//кекс с логотипом
                        e.Graphics.DrawString("3921 20 082 7 97", fnt16BoldUnder, Brushes.Black, 460, top - 4);
                }

                top += 35;
                e.Graphics.DrawString("Партія №:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_strPartiya, fnt14BoldUnder, Brushes.Black, 150, top);


                //////////////////////////////////////////////////////////////////////////
                top += 30;
                e.Graphics.DrawString("Маса нетто:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_dAllNetto.ToString("0.00") + " кг", fnt14BoldUnder, Brushes.Black, 170, top);

                if (m_iZakazchikId == 71)
                    e.Graphics.DrawString("Пакувальник _______________", fnt14Bold, Brushes.Black, 400, top - 4);

                top += 30;
                e.Graphics.DrawString("Маса брутто:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_dAllBrytto.ToString("0.00") + " кг", fnt14BoldUnder, Brushes.Black, 180, top);

                top += 30;
                e.Graphics.DrawString("Кількість рулонів:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_iCountRylon + " шт.", fnt14BoldUnder, Brushes.Black, 230, top);

                if (m_iAllCountEtiketki != 0)
                {
                    top += 30;
                    e.Graphics.DrawString("Кількість етикеток:", fnt14Bold, Brushes.Black, 50, top);
                    e.Graphics.DrawString(m_iAllCountEtiketki + " шт.", fnt14BoldUnder, Brushes.Black, 245, top);
                }

                top += 30;
                e.Graphics.DrawString("Кількість м.п.:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_iAllDlinaRylonov.ToString() + " м", fnt14BoldUnder, Brushes.Black, 200, top);

                top += 30;
                //if (m_strDateVigotv.Length!=0)
                //  m_strDateVigotv = m_strDateVigotv.Remove(m_strDateVigotv.IndexOf("0:"));

                e.Graphics.DrawString("Дата виготовлення:", fnt14Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_strDateZakaz, fnt14BoldUnder, Brushes.Black, 245, top);

                top += 35;
                e.Graphics.DrawString("Менеджер:", fnt12Bold, Brushes.Black, 50, top);
                e.Graphics.DrawString(m_strManagerName, fnt12BoldUnder, Brushes.Black, 140, top);

                top += 40;
                e.Graphics.DrawString("Термін зберігання – 12 місяців від дати виготовлення.", fnt14Bold, Brushes.Black, 50, top);
                //////////////////////////////////////////////////////////////////////////
                */
                e.HasMorePages = true;
                m_iFlagPrintPages = 2;
            }
            else if (m_iFlagPrintPages==2)
            {
                /*int t = 30;
                e.Graphics.DrawString("ТОВ «ІТАК»", fnt14Bold, Brushes.Black, 350, t);
                e.Graphics.DrawString("02660, Україна, м. Київ, вул. Червоноткацька, 44", fnt12Bold, Brushes.Black, 220, (t+=20));

                t += 40;
                e.Graphics.DrawString("Накладна № _________", fnt12Bold, Brushes.Black, 100, t );
                e.Graphics.DrawString("\"___\"___________20___р.", fnt12Bold, Brushes.Black, 500, t);

                t += 40;
                e.Graphics.DrawString("Замовник:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strZakazchik, fnt12BoldUnder, Brushes.Black, 200, t);

                t += 25;
                e.Graphics.DrawString("Найменування:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strProductName, fnt12BoldUnder, Brushes.Black, 230, t);

                if (m_iZakazchikId == 71)
                {
                    e.Graphics.DrawString("Арт.", fnt16Bold, Brushes.Black, 500, t - 4);
                    if (m_iProductId == 590)//пташине молоко
                        e.Graphics.DrawString("3921 40 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 944)//сатурн
                        e.Graphics.DrawString("3921 43 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 943)//аркадия
                        e.Graphics.DrawString("3921 33 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1298 || m_iProductId == 679)
                        e.Graphics.DrawString("3921 34 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1536)
                        e.Graphics.DrawString("3921 37 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1421)
                        e.Graphics.DrawString("3921 38 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 942)
                        e.Graphics.DrawString("3921 35 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1420)
                        e.Graphics.DrawString("3921 36 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1068)//кекс вишневый
                        e.Graphics.DrawString("3921 46 082 7 97", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 2094)//кекс с логотипом
                        e.Graphics.DrawString("3921 20 082 7 97", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                }

                t += 25;
                e.Graphics.DrawString("Матеріал:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strMaterial, fnt12BoldUnder, Brushes.Black, 230, t);

                t += 25;
                e.Graphics.DrawString("Партія:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strPartiya, fnt12BoldUnder, Brushes.Black, 170, t);

                t += 25;
                e.Graphics.DrawString("К-ть рулонів:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iCountRylon.ToString()+ " шт.", fnt12BoldUnder, Brushes.Black, 210, t);

                e.Graphics.DrawString("Вага нетто:", fnt12Bold, Brushes.Black, 400, t);
                e.Graphics.DrawString(m_dAllNetto.ToString("0.00") + " кг", fnt12BoldUnder, Brushes.Black, 500, t);

                t += 25;
                e.Graphics.DrawString("К-ть етикеток:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iAllCountEtiketki.ToString() + " шт.", fnt12BoldUnder, Brushes.Black, 220, t);

                e.Graphics.DrawString("Вага брутто:", fnt12Bold, Brushes.Black, 400, t);
                e.Graphics.DrawString(m_dAllBrytto.ToString("0.00") + " кг", fnt12BoldUnder, Brushes.Black, 500, t);

                t += 25;
                e.Graphics.DrawString("К-ть м.п.:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iAllDlinaRylonov.ToString() + " м", fnt12BoldUnder, Brushes.Black, 185, t);

                t += 25;
                e.Graphics.DrawString("Менеджер:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strManagerName, fnt12BoldUnder, Brushes.Black, 190, t);

                t += 40;
                e.Graphics.DrawString("Відпустив __________________", fnt12Bold, Brushes.Black, 100, t);

                e.Graphics.DrawString("Одержав __________________", fnt12Bold, Brushes.Black, 500, t);

                t += 40;
                e.Graphics.DrawLine(new Pen(Color.Gray, 1), 20, t, e.PageSettings.Bounds.Width - 50, t);


                //////////////////////////////////////////////////////////////////////////
                t += 25;
                e.Graphics.DrawString("ТОВ «ІТАК»", fnt14Bold, Brushes.Black, 350, t);
                e.Graphics.DrawString("02660, Україна, м. Київ, вул. Червоноткацька, 44", fnt12Bold, Brushes.Black, 220, (t += 20));

                t += 40;
                e.Graphics.DrawString("Накладна № _________", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString("\"___\"___________20___р.", fnt12Bold, Brushes.Black, 500, t);

                t += 40;
                e.Graphics.DrawString("Замовник:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strZakazchik, fnt12BoldUnder, Brushes.Black, 200, t);

                t += 25;
                e.Graphics.DrawString("Найменування:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strProductName, fnt12BoldUnder, Brushes.Black, 230, t);

                if (m_iZakazchikId == 71)
                {
                    e.Graphics.DrawString("Арт.", fnt16Bold, Brushes.Black, 500, t - 4);
                    if (m_iProductId == 590)//пташине молоко
                        e.Graphics.DrawString("3921 40 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 944)//сатурн
                        e.Graphics.DrawString("3921 43 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 943)//аркадия
                        e.Graphics.DrawString("3921 33 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1298 || m_iProductId == 679)
                        e.Graphics.DrawString("3921 34 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1536)
                        e.Graphics.DrawString("3921 37 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1421)
                        e.Graphics.DrawString("3921 38 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 942)
                        e.Graphics.DrawString("3921 35 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1420)
                        e.Graphics.DrawString("3921 36 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1068)//кекс вишневый
                        e.Graphics.DrawString("3921 46 082 7 97", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 2094)//кекс с логотипом
                        e.Graphics.DrawString("3921 20 082 7 97", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                }

                t += 25;
                e.Graphics.DrawString("Матеріал:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strMaterial, fnt12BoldUnder, Brushes.Black, 230, t);

                t += 25;
                e.Graphics.DrawString("Партія:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strPartiya, fnt12BoldUnder, Brushes.Black, 170, t);

                t += 25;
                e.Graphics.DrawString("К-ть рулонів:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iCountRylon.ToString() + " шт.", fnt12BoldUnder, Brushes.Black, 210, t);

                e.Graphics.DrawString("Вага нетто:", fnt12Bold, Brushes.Black, 400, t);
                e.Graphics.DrawString(m_dAllNetto.ToString("0.00") + " кг", fnt12BoldUnder, Brushes.Black, 500, t);

                t += 25;
                e.Graphics.DrawString("К-ть етикеток:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iAllCountEtiketki.ToString() + " шт.", fnt12BoldUnder, Brushes.Black, 220, t);

                e.Graphics.DrawString("Вага брутто:", fnt12Bold, Brushes.Black, 400, t);
                e.Graphics.DrawString(m_dAllBrytto.ToString("0.00") + " кг", fnt12BoldUnder, Brushes.Black, 500, t);

                t += 25;
                e.Graphics.DrawString("К-ть м.п.:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iAllDlinaRylonov.ToString() + " м", fnt12BoldUnder, Brushes.Black, 185, t);

                t += 25;
                e.Graphics.DrawString("Менеджер:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strManagerName, fnt12BoldUnder, Brushes.Black, 190, t);

                t += 40;
                e.Graphics.DrawString("Відпустив __________________", fnt12Bold, Brushes.Black, 100, t);

                e.Graphics.DrawString("Одержав __________________", fnt12Bold, Brushes.Black, 500, t);

                t += 40;
                e.Graphics.DrawLine(new Pen(Color.Gray, 1), 20, t, e.PageSettings.Bounds.Width - 50, t);
                //////////////////////////////////////////////////////////////////////////

                //////////////////////////////////////////////////////////////////////////
                t += 25;
                e.Graphics.DrawString("ТОВ «ІТАК»", fnt14Bold, Brushes.Black, 350, t);
                e.Graphics.DrawString("02660, Україна, м. Київ, вул. Червоноткацька, 44", fnt12Bold, Brushes.Black, 220, (t += 20));

                t += 40;
                e.Graphics.DrawString("Накладна № _________", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString("\"___\"___________20___р.", fnt12Bold, Brushes.Black, 500, t);

                t += 40;
                e.Graphics.DrawString("Замовник:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strZakazchik, fnt12BoldUnder, Brushes.Black, 200, t);

                t += 25;
                e.Graphics.DrawString("Найменування:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strProductName, fnt12BoldUnder, Brushes.Black, 230, t);

                if (m_iZakazchikId == 71)
                {
                    e.Graphics.DrawString("Арт.", fnt16Bold, Brushes.Black, 500, t - 4);
                    if (m_iProductId == 590)//пташине молоко
                        e.Graphics.DrawString("3921 40 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 944)//сатурн
                        e.Graphics.DrawString("3921 43 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 943)//аркадия
                        e.Graphics.DrawString("3921 33 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1298 || m_iProductId == 679)
                        e.Graphics.DrawString("3921 34 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1536)
                        e.Graphics.DrawString("3921 37 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1421)
                        e.Graphics.DrawString("3921 38 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 942)
                        e.Graphics.DrawString("3921 35 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1420)
                        e.Graphics.DrawString("3921 36 082 7 90", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 1068)//кекс вишневый
                        e.Graphics.DrawString("3921 46 082 7 97", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                    else if (m_iProductId == 2094)//кекс с логотипом
                        e.Graphics.DrawString("3921 20 082 7 97", fnt16BoldUnder, Brushes.Black, 560, t - 4);
                }

                t += 25;
                e.Graphics.DrawString("Матеріал:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strMaterial, fnt12BoldUnder, Brushes.Black, 230, t);

                t += 25;
                e.Graphics.DrawString("Партія:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strPartiya, fnt12BoldUnder, Brushes.Black, 170, t);

                t += 25;
                e.Graphics.DrawString("К-ть рулонів:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iCountRylon.ToString() + " шт.", fnt12BoldUnder, Brushes.Black, 210, t);

                e.Graphics.DrawString("Вага нетто:", fnt12Bold, Brushes.Black, 400, t);
                e.Graphics.DrawString(m_dAllNetto.ToString("0.00") + " кг", fnt12BoldUnder, Brushes.Black, 500, t);

                t += 25;
                e.Graphics.DrawString("К-ть етикеток:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iAllCountEtiketki.ToString() + " шт.", fnt12BoldUnder, Brushes.Black, 220, t);

                e.Graphics.DrawString("Вага брутто:", fnt12Bold, Brushes.Black, 400, t);
                e.Graphics.DrawString(m_dAllBrytto.ToString("0.00") + " кг", fnt12BoldUnder, Brushes.Black, 500, t);

                t += 25;
                e.Graphics.DrawString("К-ть м.п.:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_iAllDlinaRylonov.ToString() + " м", fnt12BoldUnder, Brushes.Black, 185, t);

                t += 25;
                e.Graphics.DrawString("Менеджер:", fnt12Bold, Brushes.Black, 100, t);
                e.Graphics.DrawString(m_strManagerName, fnt12BoldUnder, Brushes.Black, 190, t);

                t += 40;
                e.Graphics.DrawString("Відпустив __________________", fnt12Bold, Brushes.Black, 100, t);

                e.Graphics.DrawString("Одержав __________________", fnt12Bold, Brushes.Black, 500, t);

                
                //////////////////////////////////////////////////////////////////////////
                */
                if (m_boolPrintSpec == true)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
                

                m_iFlagPrintPages = 3;
            }
            else if (m_iFlagPrintPages == 3)
            {
                
                int t = 30;
                e.Graphics.DrawString("Приходна специфікація № ____________", fnt12Bold, Brushes.Black, 70, t);
                e.Graphics.DrawString("Партія: ", fnt12Bold, Brushes.Black, 590, t);
                e.Graphics.DrawString(m_strPartiya, fnt12BoldUnder, Brushes.Black, 655, t);
                
                t += 30;
                e.Graphics.DrawString("Від _________________", fnt12Bold, Brushes.Black, 40, t);

                t += 45;
                e.Graphics.DrawString("Замовник:", fnt12, Brushes.Black, 40, t);
                e.Graphics.DrawString(m_strZakazchik, fnt14BoldUnder, Brushes.Black, 130, t-5);

                e.Graphics.DrawString("Виробник:", fnt12, Brushes.Black, 430, t);
                e.Graphics.DrawString("ТОВ «ІТАК»", fnt14BoldUnder, Brushes.Black, 520, t-5);
                e.Graphics.DrawString(", Україна, м. Київ,", fnt12, Brushes.Black, 640, t);
                t += 20;
                e.Graphics.DrawString("вул. Червоноткацька, 44, тел. 44-574-04-07", fnt12, Brushes.Black, 465, t);

                t += 10;
                e.Graphics.DrawString("Менеджер:", fnt12, Brushes.Black, 40, t);
                e.Graphics.DrawString(m_strManagerName, fnt12BoldUnder, Brushes.Black, 130, t );



                t += 30;
                e.Graphics.DrawString("Матеріал:", fnt12, Brushes.Black, 40, t);
                string s1 = m_strMaterial.Replace(" ", string.Empty);
                e.Graphics.DrawString(s1, fnt12Bold, Brushes.Black, 120, t);
                
                t += 25;
                e.Graphics.DrawString("Ширина етикетки:", fnt12, Brushes.Black, 40, t);
                e.Graphics.DrawString(m_iRylonWidth + " мм", fnt12Bold, Brushes.Black, 185, t);


                Dictionary<char, string> a = new Dictionary<char, string>();

                a.Add('*', "wnnwnwwnwwnwnn");
                a.Add('0', "wnwnnwwnwwnwnn");
                a.Add('1', "wwnwnnwnwnwwnn");
                a.Add('2', "wnwwnnwnwnwwnn");
                a.Add('3', "wwnwwnnwnwnwnn");
                a.Add('4', "wnwnnwwnwnwwnn");
                a.Add('5', "wwnwnnwwnwnwnn");
                a.Add('6', "wnwwnnwwnwnwnn");
                a.Add('7', "wnwnnwnwwnwwnn");
                a.Add('8', "wwnwnnwnwwnwnn");
                a.Add('9', "wnwwnnwnwwnwnn");


                //t += 25;
                int iHeight = t + 45;
                int x = 480;

                string strPrintBar = "*4" + m_iPaletteID.ToString() + "*";
                int iLenght = strPrintBar.Length;

                for (int i = 0; i < iLenght; i++)
                {
                    string strSymbol = strPrintBar.Substring(0, 1);
                    strPrintBar = strPrintBar.Substring(1);
                    string strCodeSymbol = a[strSymbol[0]];
                    int ssL = strCodeSymbol.Length;

                    for (int j = 0; j < ssL; j++)
                    {
                        /*if (strCodeSymbol[j] == 'b')
                            e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(x, t), new Point(x, iHeight));
                        else if (strCodeSymbol[j] == 'w')
                            e.Graphics.DrawLine(new Pen(Color.White, 1), new Point(x, t), new Point(x, iHeight));*/

                        if (strCodeSymbol[j] == 'w')
                            e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(x, t), new Point(x, iHeight));
                        else if (strCodeSymbol[j] == 'n')
                            e.Graphics.DrawLine(new Pen(Color.White, 1), new Point(x, t), new Point(x, iHeight));

                        x += 1;
                    }
                }


              //  t += 45;
                e.Graphics.DrawString("4" + m_iPaletteID.ToString(), fnt10, Brushes.Black, 470 + 50, t + 45);
                
                ////////////////////////////////////////////////////////////////////////////

                t += 20;
                e.Graphics.DrawString("Товщина етикетки:", fnt12, Brushes.Black, 40, t);
                e.Graphics.DrawString(m_strTols + " мкм", fnt12Bold, Brushes.Black, 190, t);

                t += 20;
                e.Graphics.DrawString("Розмір етикетки:", fnt12, Brushes.Black, 40, t);
                e.Graphics.DrawString(m_iDlinaEtiketki + " мм", fnt12Bold, Brushes.Black, 170, t);

                t += 30;
                e.Graphics.DrawString("Малюнок:", fnt12, Brushes.Black, 40, t);
                e.Graphics.DrawString(m_strProductName, fnt14Bold, Brushes.Black, 120, t - 2);

                if (m_iZakazchikId == 71)
                {
                    e.Graphics.DrawString("Арт.", fnt16Bold, Brushes.Black, 400, t - 4);
                    if (m_iProductId == 590)//пташине молоко
                        e.Graphics.DrawString("3921 40 082 7 90", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 944)//сатурн
                        e.Graphics.DrawString("3921 43 082 7 90", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 943)//аркадия
                        e.Graphics.DrawString("3921 33 082 7 90", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 1298 || m_iProductId == 679)
                        e.Graphics.DrawString("3921 34 082 7 90", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 1536)
                        e.Graphics.DrawString("3921 37 082 7 90", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 1421)
                        e.Graphics.DrawString("3921 38 082 7 90", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 942)
                        e.Graphics.DrawString("3921 35 082 7 90", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 1420)
                        e.Graphics.DrawString("3921 36 082 7 90", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 1068)//кекс вишневый
                        e.Graphics.DrawString("3921 46 082 7 97", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                    else if (m_iProductId == 2094)//кекс с логотипом
                        e.Graphics.DrawString("3921 20 082 7 97", fnt16BoldUnder, Brushes.Black, 460, t - 4);
                }


                t += 40;
                int iLeft = 40;
                
                int iTableWidth = iLeft+355;
                int[] iCoord = { iLeft, iLeft + 60,iLeft+130,iLeft+210,iLeft+280,iLeft+355 };
                
                int t1 = t;
                int iLeft1 = 0;
                int t2 = t;
                int iTableWidth1 = 0;


                if (testPrint)
                    m_iCountRows = testPrintCountRows;  //не надо
                else
                    m_iCountRows = dataGridView1.Rows.Count-1;//надо
                
                 
                int iCountRows = 0;
                if (m_iCountRows > 76)
                    iCountRows = 76;
                else
                    iCountRows = m_iCountRows;


                int[] iBarCode = new int[m_iCountRows];
                if (testPrint)
                {
                    for (int i = 0; i < m_iCountRows; i++)
                        iBarCode[i] = 71078;
                }
                else
                {
                    string strTemp;
                    for (int i = 0; i < iCountRows; i++)//надо
                    {
                        strTemp = dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(1);
                        iBarCode[i] = Convert.ToInt32(strTemp);
                    }
                }


                for (int i = 0; i < m_lstMyRylon.Count - 1; i++)
                {
                    MyRylon iTemp;
                    for (int j = i; j < m_lstMyRylon.Count; j++)
                        if (m_lstMyRylon[i].iNumRylona > m_lstMyRylon[j].iNumRylona)
                        {
                            iTemp = m_lstMyRylon[i];
                            m_lstMyRylon[i] = m_lstMyRylon[j];
                            m_lstMyRylon[j] = iTemp;
                        }
                }

                m_iColumnCount = 7;
                int[] iCoord1 = new int[m_iColumnCount];// = { iLeft, iLeft + 60, iLeft + 130, iLeft + 210, iLeft + 280, iLeft + 355 };
                //int iCountPrintRows = 0;

                iLeft1 = iLeft;
                for (m_iCounter = 0; m_iCounter < iCountRows; m_iCounter++)
                {
                    if (m_iCounter == 0)
                        iLeft1 = iLeft;
                    else if (m_iCounter == ((iCountRows + 1) / 2))
                        iLeft1 = iLeft + 356;

                    iTableWidth1 = iLeft1 + 355;

                    iCoord1[0] = iLeft1;
                    iCoord1[1] = iLeft1 + 49;
                    iCoord1[2] = iLeft1 + 109;
                    iCoord1[3] = iLeft1 + 176;
                    iCoord1[4] = iLeft1 + 238;
                    iCoord1[5] = iLeft1 + 304;
                    iCoord1[6] = iLeft1 + 355;

                   if (m_iCounter == 0 || m_iCounter == ((iCountRows + 1) / 2))
                   {
                        t2 = t1;
                        t1 = t;
                        DrawColumnHeaders(e, iCoord1, m_iColumnCount, iLeft1, ref t1, iTableWidth1);
                    }

                   DrawColumnHeaderSeparator(e, iCoord1, m_iColumnCount, t1);

                    /*string strTemp1 = (m_iCounter + 1).ToString();//не надо
                    if (Convert.ToInt32(strTemp1) > 9)
                        e.Graphics.DrawString(strTemp1, fnt10, Brushes.Black, iCoord1[0] + 18, t1);
                    else
                        e.Graphics.DrawString(strTemp1, fnt10, Brushes.Black, iCoord1[0] + 25, t1);*/

                   DrawRylonData(e, iCountRows, m_iCounter, iCoord1, iLeft1, t1, iTableWidth1);

                    t1 += 20;
                }

               

                if (iCountRows <= 50)
                {
                    t = t1;
                    t += 20;
                    DrawSummaryInformation(e, ref t);

                    m_iCounter = 0;
                    m_iCountRows = 0;
                    e.HasMorePages = false;
                    m_iFlagPrintPages = 1;
                }
                else
                {
                    e.Graphics.DrawString("-  1  -", fnt14Bold, Brushes.Black, 375, 1110);

                    e.HasMorePages = true;
                    m_iFlagPrintPages = 4;
                }
            }
            else if (m_iFlagPrintPages == 4)
           {
                int t = 40;
                int t1 = t;
                int t2 = t;
                int i = 0;

                if (m_iCountRows>76)
                {
                    int iLeft = 50;
                    int iLeft1 = 50;
                    int[] iCoord1 = new int[6];
                    int iTableWidth1 = 0;
                    int iCountPageRows = 0;

                    if (m_iCountRows > 178)
                        iCountPageRows = 178;
                    else iCountPageRows = m_iCountRows;

                    int iCountRows = iCountPageRows - m_iCounter;

                    
                    for (i = m_iCounter; i < iCountPageRows; i++)
                    {
                        if (i == 76)
                            iLeft1 = iLeft;
                        else if ((i-m_iCounter) == ((iCountRows + 1) / 2))
                            iLeft1 = iLeft + 356;

                        iTableWidth1 = iLeft1 + 355;

                        if (i == 76 || ((i-m_iCounter) == (iCountRows+1) / 2))
                        {
                            t2 = t1;
                            t1 = t;
                            DrawColumnHeaders(e, iCoord1, m_iColumnCount, iLeft1, ref t1, iTableWidth1);
                        }
                        DrawColumnHeaderSeparator(e, iCoord1, m_iColumnCount, t1);
                        
                        /*string strTemp1 = (i + 1).ToString();//не надо
                        if (Convert.ToInt32(strTemp1) > 9)
                            e.Graphics.DrawString(strTemp1, fnt10, Brushes.Black, iCoord1[0] + 18, t1);
                        else
                            e.Graphics.DrawString(strTemp1, fnt10, Brushes.Black, iCoord1[0] + 25, t1);*/


                        DrawRylonData(e, iCountRows, m_iCounter, iCoord1, iLeft1, t1, iTableWidth1);
                        t1 += 20;
                }

                
                    }


                if (m_iCountRows <= 150)
                {

                    t = t1 + 20;
                    DrawSummaryInformation(e, ref t);

                    e.Graphics.DrawString("-  2  -", fnt14Bold, Brushes.Black, 375, 1110);
                    m_iCounter = 0;
                    m_iCountRows = 0;
                    e.HasMorePages = false;
                    m_iFlagPrintPages = 1;
                }
                else
                {
                    e.Graphics.DrawString("-  2  -", fnt14Bold, Brushes.Black, 375, 1110);
                    e.HasMorePages = true;
                    m_iCounter = i;
                    m_iFlagPrintPages = 5;
                }
            }
            else if (m_iFlagPrintPages==5)
            {
                int t = 40;
                int t1 = t;
                int t2 = t;
                int i =0;

                if (m_iCountRows > 178)
                {
                    int iLeft = 50;
                    int iLeft1 = 50;
                    int[] iCoord1 = new int[6];
                    int iTableWidth1 = 0;
                    int iCountPageRows = 0;

                    if (m_iCountRows > 280)
                        iCountPageRows = 280;
                    else iCountPageRows = m_iCountRows;

                    int iCountRows = iCountPageRows - m_iCounter;

                    for (i = m_iCounter; i < iCountPageRows; i++)
                    {
                        if (i == 178)
                            iLeft1 = iLeft;
                        else if ((i - m_iCounter) == ((iCountRows + 1) / 2))
                            iLeft1 = iLeft + 356;

                        iTableWidth1 = iLeft1 + 355;


                        if (i == 178 || ((i - m_iCounter) == (iCountRows + 1) / 2))
                        {
                            t2 = t1;
                            t1 = t;
                            DrawColumnHeaders(e, iCoord1, m_iColumnCount, iLeft1, ref t1, iTableWidth1);
                        }
                        
                        DrawColumnHeaderSeparator(e, iCoord1, m_iColumnCount, t1);


                        /*string strTemp1 = (i + 1).ToString();//не надо
                        if (Convert.ToInt32(strTemp1) > 9)
                            e.Graphics.DrawString(strTemp1, fnt10, Brushes.Black, iCoord1[0] + 18, t1);
                        else
                            e.Graphics.DrawString(strTemp1, fnt10, Brushes.Black, iCoord1[0] + 25, t1);*/


                        DrawRylonData(e, iCountRows, m_iCounter, iCoord1, iLeft1, t1, iTableWidth1);

                        t1 += 20;
                    }


                }

                if (m_iCountRows < 256)
                {
                    t = t1 + 20;

                    DrawSummaryInformation(e, ref t);

                    e.Graphics.DrawString("-  3  -", fnt14Bold, Brushes.Black, 375, 1110);
                    m_iCounter = 0;
                    m_iCountRows = 0;
                    e.HasMorePages = false;
                    m_iFlagPrintPages = 1;
                }
                else
                {
                    e.Graphics.DrawString("-  3  -", fnt14Bold, Brushes.Black, 375, 1110);
                    e.HasMorePages = true;
                    m_iCounter = i;
                    m_iFlagPrintPages = 6;

                }
            }

            else if(m_iFlagPrintPages == 6)
            {
                int t = 40;
                int t1 = t;
                int t2 = t;
                int i = 0;

                if (m_iCountRows > 280)
                {
                    int iLeft = 50;
                    int iLeft1 = 50;
                    int[] iCoord1 = new int[6];
                    int iTableWidth1 = 0;
                    int iCountPageRows = 0;

                    if (m_iCountRows > 382)
                        iCountPageRows = 382;
                    else iCountPageRows = m_iCountRows;

                    int iCountRows = iCountPageRows - m_iCounter;

                    for (i = m_iCounter; i < iCountPageRows; i++)
                    {
                        if (i == 280)
                            iLeft1 = iLeft;
                        else if ((i - m_iCounter) == ((iCountRows + 1) / 2))
                            iLeft1 = iLeft + 356;

                        iTableWidth1 = iLeft1 + 355;

                        if (i == 280 || ((i - m_iCounter) == (iCountRows + 1) / 2))
                        {
                            t2 = t1;
                            t1 = t;
                            DrawColumnHeaders(e, iCoord1, m_iColumnCount, iLeft1, ref t1, iTableWidth1);
                        }
                        DrawColumnHeaderSeparator(e, iCoord1, m_iColumnCount, t1);

                        DrawRylonData(e, iCountRows, m_iCounter, iCoord1, iLeft1, t1, iTableWidth1);

                        t1 += 20;
                    }
                }

                if (m_iCountRows < 358)
                {
                    t = t1 + 20;

                    DrawSummaryInformation(e, ref t);

                    e.Graphics.DrawString("-  4  -", fnt14Bold, Brushes.Black, 375, 1110);
                    m_iCounter = 0;
                    m_iCountRows = 0;
                    e.HasMorePages = false;
                    m_iFlagPrintPages = 1;
                }
                else
                {
                    e.Graphics.DrawString("-  4  -", fnt14Bold, Brushes.Black, 375, 1110);
                    e.HasMorePages = true;
                    m_iCounter = i;
                    m_iFlagPrintPages = 7;

                }
                
            }


            else if (m_iFlagPrintPages == 7)
            {
                int t = 40;
                int t1 = t;
                int t2 = t;
                int i = 0;

                if (m_iCountRows > 382) //+ 102
                {
                    int iLeft = 50;
                    int iLeft1 = 50;
                    int[] iCoord1 = new int[6];
                    int iTableWidth1 = 0;
                    int iCountPageRows = 0;

                    if (m_iCountRows > 484) //+102
                        iCountPageRows = 484;
                    else iCountPageRows = m_iCountRows;

                    int iCountRows = iCountPageRows - m_iCounter;

                    for (i = m_iCounter; i < iCountPageRows; i++)
                    {
                        if (i == 382)
                            iLeft1 = iLeft;
                        else if ((i - m_iCounter) == ((iCountRows + 1) / 2))
                            iLeft1 = iLeft + 356;

                        iTableWidth1 = iLeft1 + 355;

                        if (i == 382 || ((i - m_iCounter) == (iCountRows + 1) / 2))
                        {
                            t2 = t1;
                            t1 = t;
                            DrawColumnHeaders(e, iCoord1, m_iColumnCount, iLeft1, ref t1, iTableWidth1);
                        }
                        DrawColumnHeaderSeparator(e, iCoord1, m_iColumnCount, t1);

                        DrawRylonData(e, iCountRows, m_iCounter, iCoord1, iLeft1, t1, iTableWidth1);

                        t1 += 20;
                    }
                }

                if (m_iCountRows < 460) 
                {
                    t = t1 + 20;

                    DrawSummaryInformation(e, ref t);

                    e.Graphics.DrawString("-  5  -", fnt14Bold, Brushes.Black, 375, 1110);
                    m_iCounter = 0;
                    m_iCountRows = 0;
                    e.HasMorePages = false;
                    m_iFlagPrintPages = 1;
                }
                else
                {
                    e.Graphics.DrawString("-  5  -", fnt14Bold, Brushes.Black, 375, 1110);
                    e.HasMorePages = true;
                    m_iCounter = i;
                    m_iFlagPrintPages = 8;

                }

            }


            else if (m_iFlagPrintPages == 8)
            {
                int t = 40;
                int t1 = t;
                int t2 = t;
                int i = 0;

                if (m_iCountRows > 484) //+ 102
                {
                    int iLeft = 50;
                    int iLeft1 = 50;
                    int[] iCoord1 = new int[6];
                    int iTableWidth1 = 0;
                    int iCountPageRows = 0;

                    if (m_iCountRows > 586) //+102
                        iCountPageRows = 586;
                    else iCountPageRows = m_iCountRows;

                    int iCountRows = iCountPageRows - m_iCounter;

                    for (i = m_iCounter; i < iCountPageRows; i++)
                    {
                        if (i == 484)
                            iLeft1 = iLeft;
                        else if ((i - m_iCounter) == ((iCountRows + 1) / 2))
                            iLeft1 = iLeft + 356;

                        iTableWidth1 = iLeft1 + 355;

                        if (i == 484 || ((i - m_iCounter) == (iCountRows + 1) / 2))
                        {
                            t2 = t1;
                            t1 = t;
                            DrawColumnHeaders(e, iCoord1, m_iColumnCount, iLeft1, ref t1, iTableWidth1);
                        }
                        DrawColumnHeaderSeparator(e, iCoord1, m_iColumnCount, t1);

                        DrawRylonData(e, iCountRows, m_iCounter, iCoord1, iLeft1, t1, iTableWidth1);

                        t1 += 20;
                    }
                }

                if (m_iCountRows < 562) // top + 78
                {
                    t = t1 + 20;

                    DrawSummaryInformation(e, ref t);

                    e.Graphics.DrawString("-  6  -", fnt14Bold, Brushes.Black, 375, 1110);
                    m_iCounter = 0;
                    m_iCountRows = 0;
                    e.HasMorePages = false;
                    m_iFlagPrintPages = 1;
                }
                else
                {
                    e.Graphics.DrawString("-  6  -", fnt14Bold, Brushes.Black, 375, 1110);
                    e.HasMorePages = true;
                    m_iCounter = i;
                    m_iFlagPrintPages = 9;
                }
            }

            else if (m_iFlagPrintPages == 9)
            {
                int t = 40;
                int t1 = t;
                int t2 = t;
                int i = 0;

                if (m_iCountRows > 586) //+ 102
                {
                    int iLeft = 50;
                    int iLeft1 = 50;
                    int[] iCoord1 = new int[6];
                    int iTableWidth1 = 0;
                    int iCountPageRows = 0;

                    if (m_iCountRows > 688) //+102
                        iCountPageRows = 688;
                    else iCountPageRows = m_iCountRows;

                    int iCountRows = iCountPageRows - m_iCounter;

                    for (i = m_iCounter; i < iCountPageRows; i++)
                    {
                        if (i == 586)   //top
                            iLeft1 = iLeft;
                        else if ((i - m_iCounter) == ((iCountRows + 1) / 2))
                            iLeft1 = iLeft + 356;

                        iTableWidth1 = iLeft1 + 355;

                        if (i == 586 || ((i - m_iCounter) == (iCountRows + 1) / 2)) //top
                        {
                            t2 = t1;
                            t1 = t;
                            DrawColumnHeaders(e, iCoord1, m_iColumnCount, iLeft1, ref t1, iTableWidth1);
                        }
                        DrawColumnHeaderSeparator(e, iCoord1, m_iColumnCount, t1);

                        DrawRylonData(e, iCountRows, m_iCounter, iCoord1, iLeft1, t1, iTableWidth1);

                        t1 += 20;
                    }
                }

                if (m_iCountRows < 664) // top + 78
                {
                    t = t1 + 20;

                    DrawSummaryInformation(e, ref t);

                    e.Graphics.DrawString("-  7  -", fnt14Bold, Brushes.Black, 375, 1110);
                    m_iCounter = 0;
                    m_iCountRows = 0;
                    e.HasMorePages = false;
                    m_iFlagPrintPages = 1;
                }
                else
                {
                    e.Graphics.DrawString("-  7  -", fnt14Bold, Brushes.Black, 375, 1110);
                    e.HasMorePages = true;
                    m_iCounter = i;
                    m_iFlagPrintPages = 10;
                }
            }

            else if (m_iFlagPrintPages == 10)
            {
                int t = 40;
                int t1 = t;
                int t2 = t;
                int i = 0;

                if (m_iCountRows > 688) //+ 102
                {
                    int iLeft = 50;
                    int iLeft1 = 50;
                    int[] iCoord1 = new int[6];
                    int iTableWidth1 = 0;
                    int iCountPageRows = 0;

                    if (m_iCountRows > 790) //+102
                        iCountPageRows = 790;
                    else iCountPageRows = m_iCountRows;

                    int iCountRows = iCountPageRows - m_iCounter;

                    for (i = m_iCounter; i < iCountPageRows; i++)
                    {
                        if (i == 688)   //top
                            iLeft1 = iLeft;
                        else if ((i - m_iCounter) == ((iCountRows + 1) / 2))
                            iLeft1 = iLeft + 356;

                        iTableWidth1 = iLeft1 + 355;

                        if (i == 688 || ((i - m_iCounter) == (iCountRows + 1) / 2)) //top
                        {
                            t2 = t1;
                            t1 = t;
                            DrawColumnHeaders(e, iCoord1, m_iColumnCount, iLeft1, ref t1, iTableWidth1);
                        }
                        DrawColumnHeaderSeparator(e, iCoord1, m_iColumnCount, t1);

                        DrawRylonData(e, iCountRows, m_iCounter, iCoord1, iLeft1, t1, iTableWidth1);

                        t1 += 20;
                    }
                }

                if (m_iCountRows < 766) // top + 78
                {
                    t = t1 + 20;

                    DrawSummaryInformation(e, ref t);

                    e.Graphics.DrawString("-  8  -", fnt14Bold, Brushes.Black, 375, 1110);
                    m_iCounter = 0;
                    m_iCountRows = 0;
                    e.HasMorePages = false;
                    m_iFlagPrintPages = 1;
                }
                else
                {
                    e.Graphics.DrawString("-  8  -", fnt14Bold, Brushes.Black, 375, 1110);
                    e.HasMorePages = true;
                    m_iCounter = i;
                    m_iFlagPrintPages = 11;
                }
            }


         
            else if (m_iFlagPrintPages == 11 )
            {
                int t = 40;
                int t1 = t;
                int t2 = t;
                
                t = t1 + 20;

                DrawSummaryInformation(e, ref t);

                e.Graphics.DrawString("-  9  -", fnt14Bold, Brushes.Black, 375, 1110);
                m_iCounter = 0;
                m_iCountRows = 0;
                e.HasMorePages = false;
                m_iFlagPrintPages = 1;
            }


                        
        }

        private void ClearPalett()
        {
            if (MyDialog.Show("Очистить список?", true) == 1)
            {
                //////////////////////////////////////////////////////////////////////////
                m_iCountRylon = 0;
                m_lstBarCode.Clear();
                m_lstMyRylon.Clear();

                m_iZakazId = 0;
                m_iProductId = 0;
                m_iProductIdNew = 0;
                m_dNetto = 0;
                m_dBrytto = 0;
                m_iDlinaRylona = 0;
                m_iCountEtiketki = 0;
                m_iNumRylona = 0;
                m_iRylonWidth = 0;

                m_iDlinaEtiketki = 0;

                m_bSelectRylon = true;
                m_bAddRylon = true;

                m_strZakazchik = "";
                m_strPartiya = "";
                m_strDateZakaz = "";
                m_strSmena = "";
                m_strProductName = "";
                m_strMaterial = "";
                m_strTols = "";
                
                m_iZakazchikId = 0;
                m_iZakazchikIdNew = 0;

                m_dAllNetto = 0;
                m_dAllBrytto = 0;
                m_iAllCountEtiketki = 0;
                m_iAllDlinaRylonov = 0;

                            
                m_boolPrintSpec = false;
                m_iFlagPrintPages = 1; // печать страниц
                 

                label_zakazchik.Text = m_strZakazchik;
                label_netto.Text = m_dNetto.ToString() + " кг";
                label_brytto.Text = m_dBrytto.ToString() + " кг";
                label_dlinaRylonov.Text = m_iCountRylon.ToString() + " шт";
                label_countEtiket.Text = m_iAllCountEtiketki.ToString() + " шт";

                dataGridView1.Rows.Clear();
                textBox_barcode.Clear();
            }
            button_menu.Enabled = false;
            textBox_barcode.Focus();
        }

        private void button_resetPalet_Click(object sender, EventArgs e)
        {
            ClearPalett();            
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.Rows.Count > 1 && button_print.Enabled == false)
            {
                //button_print.Enabled = true;
                button_menu.Enabled = true;
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dataGridView1.Rows.Count < 2 && button_print.Enabled == true)
            {
                //button_print.Enabled = false;
                button_menu.Enabled = false;
            }
        }

        private void VvodRylona()
        {
            otgryzka_gp_enterRylon enterRylon = new otgryzka_gp_enterRylon();
            enterRylon.myConnection = myConnection;
            enterRylon.ShowDialog();
            if (enterRylon.boolEnterRylon == true)
            {
                MessageBox.Show("1");
            }
        }

        private void button_newPalet_Click(object sender, EventArgs e)
        {
            //NewPalet();
            VvodRylona();
        }


        private void button_menu_Click(object sender, EventArgs e)
        {
            ButtonMenu();
        }

        private void ButtonMenu()
        {
            MenuForm menuForm = new MenuForm();
            menuForm.ShowDialog();

            switch (menuForm.GetDialiogResult())
            {
                case 2: ClearPalett();  break;
                case 3: MyPrint();      break;
                case 4: PriemGP();      break;
                case 5: OtgryzkaGP();   break;
                case 9: MyClose();      break;
            }

            menuForm = null;
        }


        private void PriemGP()
        {
            DateTime dt = System.DateTime.Now;
            bool flag = false;
            string strPriemTime = "";

            if (CheckConnect())
            {
                for (int i = 0; i < m_lstMyRylon.Count; i++)
                {
                    strPriemTime = dt.Year + "." + dt.Month + "." + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;

    //                strQuery = "select id from itak_etiketka.dbo.itak_sklad where rylon_id=" + m_lstMyRylon[i].iBarCode;
                    strQuery = "select id from itak_etiketka.dbo.itak_sklad where rylon_id=" + m_lstMyRylon[i].iBarCode;// +" and zakaz_id=" + m_lstMyRylon[i].iZakazID;

                    myCommand.CommandText = strQuery;
                    reader = myCommand.ExecuteReader();
                    try
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            int iId = Convert.ToInt32(reader["id"]);
                            reader.Close();

                            strQuery = "update itak_etiketka.dbo.itak_sklad set rylon_state=1 where id=" + iId;
                                
                            myCommand.CommandText = strQuery;
                            myCommand.ExecuteNonQuery();
                            
                        }
                        else
                        {
                            reader.Close();
                            strQuery = "insert into itak_etiketka.dbo.itak_sklad " +
                                "(rylon_id,zakaz_id,palette_id,date_postyp,rylon_state) " +
                                "values (" + m_lstMyRylon[i].iBarCode + "," + m_lstMyRylon[i].iZakazID + ",(select palette_id from itak_etiketka.dbo.itak_vihidrylon where id=" + m_lstMyRylon[i].iBarCode + "),convert(smalldatetime,'" + strPriemTime + "',101),1)";

                            try
                            {
                                myCommand.CommandText = strQuery;
                                myCommand.ExecuteNonQuery();
                                flag = true;

                            }
                            catch (System.Exception ex)
                            {
                                WriteLog("PriemGP() - вставка значения в itak_sklad", ex);//MessageBox.Show(ex.Message);
                                flag = false;
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        WriteLog("PriemGP() - обновления поля в itak_sklad", ex);
                        reader.Close();
                    }
                }
                if (flag == true)
                    MyDialog.Show("Продукция перемещена на склад");
            }
        }

        private void OtgryzkaGP()
        {
            DateTime dt;
            string strOtgryzTime = "";
            bool flag=false;

            if (CheckConnect())
            {
                if (m_lstMyRylon.Count >= 0)
                {
                    for (int i = 0; i < m_lstMyRylon.Count; i++)
                    {
                        dt = System.DateTime.Now;
                        strOtgryzTime = dt.Year + "." + dt.Month + "." + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;

                        strQuery = "select id from itak_etiketka.dbo.itak_sklad where rylon_id=" + m_lstMyRylon[i].iBarCode; // +" and zakaz_id=" + m_lstMyRylon[i].iZakazID;
                        myCommand.CommandText = strQuery;
                        reader = myCommand.ExecuteReader();
                        try
                        {
                            reader.Read();
                            if (reader.HasRows)
                            {
                                int iId = Convert.ToInt32(reader["id"]);
                                reader.Close();

                                strQuery = "update itak_etiketka.dbo.itak_sklad set rylon_state=2 , date_otgryz=convert(smalldatetime,'" + strOtgryzTime + "',101) where id=" + iId;

                            }
                            else
                            {
                                reader.Close();

                                strQuery = "insert into itak_etiketka.dbo.itak_sklad " +
                                    "(rylon_id,zakaz_id,palette_id, date_postyp,date_otgryz,rylon_state) " +
                                    "values (" + m_lstMyRylon[i].iBarCode + "," + m_lstMyRylon[i].iZakazID +",(select palette_id from itak_etiketka.dbo.itak_vihidrylon where id=" + m_lstMyRylon[i].iBarCode + "),convert(smalldatetime,'" + strOtgryzTime + "',101),convert(smalldatetime,'" + strOtgryzTime + "',101),2)";

                            }

                            myCommand.CommandText = strQuery;
                            myCommand.ExecuteNonQuery();
                            flag = true;
                        }
                        catch (System.Exception ex)
                        {
                            WriteLog("OtgryzkaGP() - вставка рулонов  в итак_склад при отгрузке ",ex);
                            flag = false;
                        }
                        //reader.Close();

                    }

                    //reader.Close();
                    strQuery = "select palette_id from itak_etiketka.dbo.itak_vihidrylon where id=" + m_lstMyRylon[0].iBarCode;
                    myCommand.CommandText = strQuery;
                    reader = myCommand.ExecuteReader();
                    try
                    {
                        reader.Read();
                        if (reader.HasRows && reader[0]!=DBNull.Value)
                        {
                            int iId = Convert.ToInt32(reader[0]);
                            reader.Close();
                            dt = System.DateTime.Now;
                            strOtgryzTime = dt.Year + "." + dt.Month + "." + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
                            strQuery = "update itak_etiketka.dbo.itak_sklad_palette set date_otgryz=convert(smalldatetime,'" + strOtgryzTime + "',101) where id=" + iId;
                            try
                            {
                                myCommand.CommandText = strQuery;
                                myCommand.ExecuteNonQuery();
                            }
                            catch (System.Exception ex)
                            {
                                WriteLog("OtgryzkaGP() - обновление паллеты при отгрузке", ex);
                            }

                        }
                        reader.Close();

                    }
                    catch (System.Exception ex)
                    {
                        WriteLog("OtgryzkaGP() - получение ид палетты при отгрузке", ex);
                        reader.Close();
                    }



                    //strQuery = "update itak_etiketka.dbo.itak_sklad set rylon_state=2 , date_otgryz='" + strOtgryzTime + "' where id=" + iId;



                    if (flag == true)
                        MyDialog.Show("Продукция успешно отгружена");
                }
            }

        }


        //////////////////////////////////////////////////////////////////////////
    }
           
}