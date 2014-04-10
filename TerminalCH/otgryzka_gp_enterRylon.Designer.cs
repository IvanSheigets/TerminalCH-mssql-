namespace TerminalCH
{
    partial class otgryzka_gp_enterRylon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Partiya = new System.Windows.Forms.TextBox();
            this.textBox_numRylon = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button_enter = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_netto = new System.Windows.Forms.Label();
            this.label_brytto = new System.Windows.Forms.Label();
            this.label_mp = new System.Windows.Forms.Label();
            this.label_etiket = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "№ Партии";
            // 
            // textBox_Partiya
            // 
            this.textBox_Partiya.Location = new System.Drawing.Point(100, 9);
            this.textBox_Partiya.Name = "textBox_Partiya";
            this.textBox_Partiya.Size = new System.Drawing.Size(128, 20);
            this.textBox_Partiya.TabIndex = 1;
            this.textBox_Partiya.TextChanged += new System.EventHandler(this.textBox_Partiya_TextChanged);
            this.textBox_Partiya.Leave += new System.EventHandler(this.textBox_Partiya_Leave);
            this.textBox_Partiya.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Partiya_KeyPress);
            // 
            // textBox_numRylon
            // 
            this.textBox_numRylon.Location = new System.Drawing.Point(100, 35);
            this.textBox_numRylon.Name = "textBox_numRylon";
            this.textBox_numRylon.Size = new System.Drawing.Size(128, 20);
            this.textBox_numRylon.TabIndex = 3;
            this.textBox_numRylon.TextChanged += new System.EventHandler(this.textBox_numRylon_TextChanged);
            this.textBox_numRylon.Leave += new System.EventHandler(this.textBox_numRylon_Leave);
            this.textBox_numRylon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_numRylon_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "№ Рулона";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.HorizontalScrollbar = true;
            this.checkedListBox1.Location = new System.Drawing.Point(2, 72);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(236, 89);
            this.checkedListBox1.TabIndex = 4;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // button_enter
            // 
            this.button_enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_enter.Location = new System.Drawing.Point(5, 267);
            this.button_enter.Name = "button_enter";
            this.button_enter.Size = new System.Drawing.Size(112, 45);
            this.button_enter.TabIndex = 5;
            this.button_enter.Text = "Ввод";
            this.button_enter.UseVisualStyleBackColor = true;
            this.button_enter.Click += new System.EventHandler(this.button_enter_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(123, 267);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(112, 45);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(9, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Нетто:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(9, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Брутто:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(9, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "К-во м.п.:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(9, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "К-во этикеток:";
            // 
            // label_netto
            // 
            this.label_netto.AutoSize = true;
            this.label_netto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_netto.ForeColor = System.Drawing.Color.Blue;
            this.label_netto.Location = new System.Drawing.Point(66, 165);
            this.label_netto.Name = "label_netto";
            this.label_netto.Size = new System.Drawing.Size(41, 20);
            this.label_netto.TabIndex = 11;
            this.label_netto.Text = "0 кг";
            // 
            // label_brytto
            // 
            this.label_brytto.AutoSize = true;
            this.label_brytto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_brytto.ForeColor = System.Drawing.Color.Blue;
            this.label_brytto.Location = new System.Drawing.Point(70, 187);
            this.label_brytto.Name = "label_brytto";
            this.label_brytto.Size = new System.Drawing.Size(41, 20);
            this.label_brytto.TabIndex = 15;
            this.label_brytto.Text = "0 кг";
            // 
            // label_mp
            // 
            this.label_mp.AutoSize = true;
            this.label_mp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_mp.ForeColor = System.Drawing.Color.Blue;
            this.label_mp.Location = new System.Drawing.Point(81, 210);
            this.label_mp.Name = "label_mp";
            this.label_mp.Size = new System.Drawing.Size(36, 20);
            this.label_mp.TabIndex = 16;
            this.label_mp.Text = "0 м";
            // 
            // label_etiket
            // 
            this.label_etiket.AutoSize = true;
            this.label_etiket.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_etiket.ForeColor = System.Drawing.Color.Blue;
            this.label_etiket.Location = new System.Drawing.Point(117, 234);
            this.label_etiket.Name = "label_etiket";
            this.label_etiket.Size = new System.Drawing.Size(48, 20);
            this.label_etiket.TabIndex = 17;
            this.label_etiket.Text = "0 шт";
            // 
            // otgryzka_gp_enterRylon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.label_etiket);
            this.Controls.Add(this.label_mp);
            this.Controls.Add(this.label_brytto);
            this.Controls.Add(this.label_netto);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_enter);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.textBox_numRylon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Partiya);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "otgryzka_gp_enterRylon";
            this.ShowIcon = false;
            this.Text = "otgryzka_gp_enterRylon";
            this.Load += new System.EventHandler(this.otgryzka_gp_enterRylon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Partiya;
        private System.Windows.Forms.TextBox textBox_numRylon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button_enter;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_netto;
        private System.Windows.Forms.Label label_brytto;
        private System.Windows.Forms.Label label_mp;
        private System.Windows.Forms.Label label_etiket;
    }
}