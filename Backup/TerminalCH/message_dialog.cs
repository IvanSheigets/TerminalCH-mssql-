using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerminalCH
{
    public partial class message_dialog : Form
    {
        int iRes = -1;
        public message_dialog()
        {
            InitializeComponent();
            button_yes.Visible = false;
            button_no.Visible = false;
            button_ok.Visible = false;
        }

        private void PrintMessage(string strMessage)
        {
            if (strMessage.Length>40)
            {
                string s1 = strMessage.Substring(0, 21);
                strMessage = strMessage.Remove(0, 21);
                string s2 = strMessage.Substring(0,20);
                strMessage = strMessage.Remove(0, 20);
                string s3 = strMessage;
                label_main1.Text = s1;
                label_main2.Text = s2;
                label_main3.Text = s3;
            }
            else if (strMessage.Length > 24 )
            {
                string s1 = strMessage.Substring(0, 21);
                strMessage = strMessage.Remove(0, 21);
                string s2 = strMessage;
                label_main1.Text = s1;
                label_main2.Text = s2;
                label_main3.Text = "";
            }
            else
            {
                label_main1.Text = strMessage;
                label_main2.Text = "";
                label_main3.Text = "";
            }
        }
        
        public int Show(string strMessage)
        {
            //label_main.Text = strMessage;
            PrintMessage(strMessage);
            button_ok.Visible = true;
            button_yes.Visible = false;
            button_no.Visible = false;
            //button_ok.Focus();
            ShowDialog();           
            return iRes;
        }

        public int Show(string strMessage, bool bButtonsYesNo)
        {
            PrintMessage(strMessage);
            if (bButtonsYesNo==true)
            {
                button_yes.Visible = true;
                button_no.Visible = true;
                button_ok.Visible = false;
                //button_yes.Focus();
            }
            else
            {
                button_yes.Visible = false;
                button_no.Visible = false;
                button_ok.Visible = true;

            }
            ShowDialog();    
            return iRes;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            iRes = 2;
            this.Close();
        }

        private void message_dialog_Load(object sender, EventArgs e)
        {
            /*button_yes.Enabled = false;
            button_no.Enabled = false;
            button_ok.Enabled = false;*/

            


        }

        private void button_yes_Click(object sender, EventArgs e)
        {
            iRes = 1;
            
            this.Close();
        }

        private void button_no_Click(object sender, EventArgs e)
        {
            iRes = 0;
           
            this.Close();
        }

        private void message_dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void message_dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            label_main1.Text = "";
            label_main2.Text = "";
            label_main3.Text = "";
        }
    }
}
