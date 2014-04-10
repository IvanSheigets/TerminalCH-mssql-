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
    public partial class MenuForm : Form
    {
        private int m_iDialogResult = -1;

        public MenuForm()
        {
            InitializeComponent();
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            m_iDialogResult = 3;
            this.Close();
        }

        private void button_priem_Click(object sender, EventArgs e)
        {
            m_iDialogResult = 4;
            this.Close();
        }

        private void button_otgryzka_Click(object sender, EventArgs e)
        {
            m_iDialogResult = 5;
            this.Close();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            m_iDialogResult = 2;
            this.Close();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            //m_iDialogResult = 9;
            this.Close();
        }

        public int GetDialiogResult()
        {
            return m_iDialogResult;
        }

        
        private void button_print_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                m_iDialogResult = 2;
            else if (e.KeyCode == Keys.F3)
                m_iDialogResult = 3;
            else if (e.KeyCode == Keys.F4)
                m_iDialogResult = 4;
            else if (e.KeyCode == Keys.F5)
                m_iDialogResult = 5;
            else if (e.KeyCode == Keys.F9)
                m_iDialogResult = 9;

            this.Close();
        }
    }
}
