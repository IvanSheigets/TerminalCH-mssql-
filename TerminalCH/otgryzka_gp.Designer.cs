namespace TerminalCH
{
    partial class otgryzka_gp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button_print = new System.Windows.Forms.Button();
            this.button_resetPalet = new System.Windows.Forms.Button();
            this.button_enterRylon = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox_barcode = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_zakazchik = new System.Windows.Forms.Label();
            this.label_netto = new System.Windows.Forms.Label();
            this.label_brytto = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_dlinaRylonov = new System.Windows.Forms.Label();
            this.label_countEtiket = new System.Windows.Forms.Label();
            this.button_menu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_print
            // 
            this.button_print.Enabled = false;
            this.button_print.Location = new System.Drawing.Point(131, 8);
            this.button_print.Name = "button_print";
            this.button_print.Size = new System.Drawing.Size(102, 23);
            this.button_print.TabIndex = 0;
            this.button_print.Text = "F1 - Печать";
            this.button_print.UseVisualStyleBackColor = true;
            this.button_print.Visible = false;
            this.button_print.Click += new System.EventHandler(this.button_print_Click);
            // 
            // button_resetPalet
            // 
            this.button_resetPalet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_resetPalet.Location = new System.Drawing.Point(119, 270);
            this.button_resetPalet.Name = "button_resetPalet";
            this.button_resetPalet.Size = new System.Drawing.Size(102, 22);
            this.button_resetPalet.TabIndex = 1;
            this.button_resetPalet.Text = "F2 - Сброс";
            this.button_resetPalet.UseVisualStyleBackColor = true;
            this.button_resetPalet.Click += new System.EventHandler(this.button_resetPalet_Click);
            // 
            // button_enterRylon
            // 
            this.button_enterRylon.Enabled = false;
            this.button_enterRylon.Location = new System.Drawing.Point(131, 37);
            this.button_enterRylon.Name = "button_enterRylon";
            this.button_enterRylon.Size = new System.Drawing.Size(102, 23);
            this.button_enterRylon.TabIndex = 2;
            this.button_enterRylon.Text = "F2 - Ввод рулона";
            this.button_enterRylon.UseVisualStyleBackColor = true;
            this.button_enterRylon.Visible = false;
            this.button_enterRylon.Click += new System.EventHandler(this.button_newPalet_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(119, 293);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(102, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "F9 - Назад";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox_barcode
            // 
            this.textBox_barcode.Location = new System.Drawing.Point(12, 20);
            this.textBox_barcode.Name = "textBox_barcode";
            this.textBox_barcode.Size = new System.Drawing.Size(216, 20);
            this.textBox_barcode.TabIndex = 0;
            this.textBox_barcode.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox_barcode_PreviewKeyDown);
            this.textBox_barcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_barcode_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(2, 47);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(236, 164);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Замовник:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Нетто, кг:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(9, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Брутто, кг:";
            // 
            // label_zakazchik
            // 
            this.label_zakazchik.AutoSize = true;
            this.label_zakazchik.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_zakazchik.ForeColor = System.Drawing.Color.Blue;
            this.label_zakazchik.Location = new System.Drawing.Point(57, 214);
            this.label_zakazchik.Name = "label_zakazchik";
            this.label_zakazchik.Size = new System.Drawing.Size(11, 12);
            this.label_zakazchik.TabIndex = 10;
            this.label_zakazchik.Text = "1";
            // 
            // label_netto
            // 
            this.label_netto.AutoSize = true;
            this.label_netto.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_netto.ForeColor = System.Drawing.Color.Blue;
            this.label_netto.Location = new System.Drawing.Point(57, 229);
            this.label_netto.Name = "label_netto";
            this.label_netto.Size = new System.Drawing.Size(11, 12);
            this.label_netto.TabIndex = 11;
            this.label_netto.Text = "1";
            // 
            // label_brytto
            // 
            this.label_brytto.AutoSize = true;
            this.label_brytto.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_brytto.ForeColor = System.Drawing.Color.Blue;
            this.label_brytto.Location = new System.Drawing.Point(57, 245);
            this.label_brytto.Name = "label_brytto";
            this.label_brytto.Size = new System.Drawing.Size(11, 12);
            this.label_brytto.TabIndex = 12;
            this.label_brytto.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(113, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "К-ть м.п.:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(113, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "К-ть етикеток:";
            // 
            // label_dlinaRylonov
            // 
            this.label_dlinaRylonov.AutoSize = true;
            this.label_dlinaRylonov.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_dlinaRylonov.ForeColor = System.Drawing.Color.Blue;
            this.label_dlinaRylonov.Location = new System.Drawing.Point(159, 229);
            this.label_dlinaRylonov.Name = "label_dlinaRylonov";
            this.label_dlinaRylonov.Size = new System.Drawing.Size(11, 12);
            this.label_dlinaRylonov.TabIndex = 15;
            this.label_dlinaRylonov.Text = "1";
            // 
            // label_countEtiket
            // 
            this.label_countEtiket.AutoSize = true;
            this.label_countEtiket.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_countEtiket.ForeColor = System.Drawing.Color.Blue;
            this.label_countEtiket.Location = new System.Drawing.Point(177, 245);
            this.label_countEtiket.Name = "label_countEtiket";
            this.label_countEtiket.Size = new System.Drawing.Size(11, 12);
            this.label_countEtiket.TabIndex = 16;
            this.label_countEtiket.Text = "1";
            // 
            // button_menu
            // 
            this.button_menu.Enabled = false;
            this.button_menu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_menu.Location = new System.Drawing.Point(11, 270);
            this.button_menu.Name = "button_menu";
            this.button_menu.Size = new System.Drawing.Size(102, 46);
            this.button_menu.TabIndex = 18;
            this.button_menu.Text = "F1 - Меню";
            this.button_menu.UseVisualStyleBackColor = true;
            this.button_menu.Click += new System.EventHandler(this.button_menu_Click);
            // 
            // otgryzka_gp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.button_menu);
            this.Controls.Add(this.label_countEtiket);
            this.Controls.Add(this.label_dlinaRylonov);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_brytto);
            this.Controls.Add(this.label_netto);
            this.Controls.Add(this.label_zakazchik);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox_barcode);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button_enterRylon);
            this.Controls.Add(this.button_resetPalet);
            this.Controls.Add(this.button_print);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "otgryzka_gp";
            this.ShowIcon = false;
            this.Text = "otgryzka_gp";
            this.Load += new System.EventHandler(this.otgryzka_gp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_print;
        private System.Windows.Forms.Button button_resetPalet;
        private System.Windows.Forms.Button button_enterRylon;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox_barcode;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_zakazchik;
        private System.Windows.Forms.Label label_netto;
        private System.Windows.Forms.Label label_brytto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_dlinaRylonov;
        private System.Windows.Forms.Label label_countEtiket;
        private System.Windows.Forms.Button button_menu;
    }
}