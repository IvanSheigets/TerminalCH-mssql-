namespace TerminalCH
{
    partial class Main_form
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_exit = new System.Windows.Forms.Button();
            this.button_priem_material = new System.Windows.Forms.Button();
            this.button_vidachavproizvodstvo = new System.Windows.Forms.Button();
            this.button_priem_gp = new System.Windows.Forms.Button();
            this.button_otgryzka_gp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_exit
            // 
            this.button_exit.Location = new System.Drawing.Point(34, 249);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(173, 51);
            this.button_exit.TabIndex = 0;
            this.button_exit.Text = "F5 - Выход";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // button_priem_material
            // 
            this.button_priem_material.Enabled = false;
            this.button_priem_material.Location = new System.Drawing.Point(34, 21);
            this.button_priem_material.Name = "button_priem_material";
            this.button_priem_material.Size = new System.Drawing.Size(173, 51);
            this.button_priem_material.TabIndex = 1;
            this.button_priem_material.Text = "F1 - Прием материала";
            this.button_priem_material.UseVisualStyleBackColor = true;
            this.button_priem_material.Click += new System.EventHandler(this.button_priem_material_Click);
            // 
            // button_vidachavproizvodstvo
            // 
            this.button_vidachavproizvodstvo.Enabled = false;
            this.button_vidachavproizvodstvo.Location = new System.Drawing.Point(34, 78);
            this.button_vidachavproizvodstvo.Name = "button_vidachavproizvodstvo";
            this.button_vidachavproizvodstvo.Size = new System.Drawing.Size(173, 51);
            this.button_vidachavproizvodstvo.TabIndex = 2;
            this.button_vidachavproizvodstvo.Text = "F2 - Выдача в производство";
            this.button_vidachavproizvodstvo.UseVisualStyleBackColor = true;
            this.button_vidachavproizvodstvo.Click += new System.EventHandler(this.button_vidachavproizvodstvo_Click);
            // 
            // button_priem_gp
            // 
            this.button_priem_gp.Enabled = false;
            this.button_priem_gp.Location = new System.Drawing.Point(34, 135);
            this.button_priem_gp.Name = "button_priem_gp";
            this.button_priem_gp.Size = new System.Drawing.Size(173, 51);
            this.button_priem_gp.TabIndex = 3;
            this.button_priem_gp.Text = "F3 - Прием ГП";
            this.button_priem_gp.UseVisualStyleBackColor = true;
            this.button_priem_gp.Click += new System.EventHandler(this.button_priem_gp_Click);
            // 
            // button_otgryzka_gp
            // 
            this.button_otgryzka_gp.Location = new System.Drawing.Point(34, 192);
            this.button_otgryzka_gp.Name = "button_otgryzka_gp";
            this.button_otgryzka_gp.Size = new System.Drawing.Size(173, 51);
            this.button_otgryzka_gp.TabIndex = 4;
            this.button_otgryzka_gp.Text = "F4 - Отгрузка ГП";
            this.button_otgryzka_gp.UseVisualStyleBackColor = true;
            this.button_otgryzka_gp.Click += new System.EventHandler(this.button_otgryzka_gp_Click);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.button_otgryzka_gp);
            this.Controls.Add(this.button_priem_gp);
            this.Controls.Add(this.button_vidachavproizvodstvo);
            this.Controls.Add(this.button_priem_material);
            this.Controls.Add(this.button_exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main_form";
            this.ShowIcon = false;
            this.Text = "Itak_terminal";
            this.Load += new System.EventHandler(this.Main_form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_form_FormClosing);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Main_form_PreviewKeyDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_form_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_exit;
        private System.Windows.Forms.Button button_priem_material;
        private System.Windows.Forms.Button button_vidachavproizvodstvo;
        private System.Windows.Forms.Button button_priem_gp;
        private System.Windows.Forms.Button button_otgryzka_gp;

    }
}

