using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
///using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;

namespace TerminalCH
{
    public partial class Main_form : Form
    {
        public SqlConnection myConnection;
       /* MyStruct structSettings;
        FileInfo fileNameSettings;
        FileInfo fileNameLog;

        public string m_strPrinter;*/
        public string Connect = "Database=itak_etiketka;Data Source=192.168.0.9;User Id=ivan;Password=coolworld";
        //public string Connect = "Database=itak_etiketka;Data Source=localhost;User Id=ivan;Password=coolworld";

        private otgryzka_gp m_formOtgryzkaGP;
        public Main_form()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }


        private void Main_form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void Main_form_KeyDown(object sender, KeyEventArgs e)
        {


           /* if (e.KeyCode == Keys.F1)
                PriemTovara();
            else if (e.KeyCode == Keys.F2)
                VidachaVProizvodstvo();
            else if (e.KeyCode == Keys.F3)
                PriemGP();
            else*/ if (e.KeyCode == Keys.F4)
                OtgryzkaGP();
            else if (e.KeyCode == Keys.F5)
                MyExit();

        }

        private void button_priem_material_Click(object sender, EventArgs e)
        {
            PriemTovara();
        }
        private void button_vidachavproizvodstvo_Click(object sender, EventArgs e)
        {
            VidachaVProizvodstvo();
        }
        private void button_priem_gp_Click(object sender, EventArgs e)
        {
            PriemGP();
        }
        private void button_exit_Click(object sender, EventArgs e)
        {
            MyExit();
        }
        private void button_otgryzka_gp_Click(object sender, EventArgs e)
        {
            OtgryzkaGP();
        }

        private void PriemTovara()
        {
            MessageBox.Show("PriemTovara");
        }
        private void VidachaVProizvodstvo()
        {
            MessageBox.Show("Vidacha v proizvodstvo");
        }
        private void PriemGP()
        {
            MessageBox.Show("Прием готовой продукции");
        }
        private void OtgryzkaGP()
        {
            m_formOtgryzkaGP = new otgryzka_gp();
            m_formOtgryzkaGP.myConnection = myConnection;
            if (m_formOtgryzkaGP.ShowDialog() == DialogResult.Cancel)
                MyExit();
            //MessageBox.Show("Отгрузка");

        }
        private void MyExit()
        {
            this.Close();
        }

        private void Main_form_Load(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////
            //загрузка настроек
           /* fileNameSettings = new FileInfo("settings.bin");
//            FileStream fileStreamLog = fileNameLog.Open(FileMode.Append, FileAccess.Write);
            
            try
            {
                FileStream fileStreamSettings = fileNameSettings.Open(FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                
                structSettings = new MyStruct();
                structSettings = (MyStruct)bf.Deserialize(fileStreamSettings);
                fileStreamSettings.Close();

                m_strPrinter = structSettings.strPrinter;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Невозможно загрузить настройки!!!");
            }
            

            

            //////////////////////////////////////////////////////////////////////////*/

            myConnection = new SqlConnection(Connect);
            try
            {
                myConnection.Open();
            }
            catch (System.Exception ex)
            {
                ////MessageBox.Show(ex.Message);
            }

            OtgryzkaGP();
        }
        

        private void Main_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }
        
       /* [SerializableAttribute]
        public struct MyStruct
        {
            public string strPrinter;
            
        };*/

       /* private void button_settings_Click(object sender, EventArgs e)
        {

            

            FileInfo fileSettings = new FileInfo("settings.bin");
            FileStream fileStreamSeattings = fileSettings.Open(FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();

            MyStruct structParameters = new MyStruct();
            structParameters.strPrinter = "Brother HL-5140 series";
            
            
            bf.Serialize(fileStreamSeattings, structParameters);

            fileStreamSeattings.Close();
        }*/

    }
}
