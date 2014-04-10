namespace TerminalCH
{
    partial class MenuForm
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
            this.button_print = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.button_priem = new System.Windows.Forms.Button();
            this.button_otgryzka = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_print
            // 
            this.button_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_print.Location = new System.Drawing.Point(20, 30);
            this.button_print.Name = "button_print";
            this.button_print.Size = new System.Drawing.Size(160, 30);
            this.button_print.TabIndex = 1;
            this.button_print.Text = "F3 - Печать";
            this.button_print.UseVisualStyleBackColor = true;
            this.button_print.Click += new System.EventHandler(this.button_print_Click);
            this.button_print.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button_print_KeyDown);
            // 
            // button_exit
            // 
            this.button_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_exit.Location = new System.Drawing.Point(20, 174);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(160, 30);
            this.button_exit.TabIndex = 5;
            this.button_exit.Text = "Назад";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // button_priem
            // 
            this.button_priem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_priem.Location = new System.Drawing.Point(20, 66);
            this.button_priem.Name = "button_priem";
            this.button_priem.Size = new System.Drawing.Size(160, 30);
            this.button_priem.TabIndex = 2;
            this.button_priem.Text = "F4 - Прием ГП";
            this.button_priem.UseVisualStyleBackColor = true;
            this.button_priem.Click += new System.EventHandler(this.button_priem_Click);
            // 
            // button_otgryzka
            // 
            this.button_otgryzka.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_otgryzka.Location = new System.Drawing.Point(20, 102);
            this.button_otgryzka.Name = "button_otgryzka";
            this.button_otgryzka.Size = new System.Drawing.Size(160, 30);
            this.button_otgryzka.TabIndex = 3;
            this.button_otgryzka.Text = "F5 - Отгрузка ГП";
            this.button_otgryzka.UseVisualStyleBackColor = true;
            this.button_otgryzka.Click += new System.EventHandler(this.button_otgryzka_Click);
            // 
            // button_clear
            // 
            this.button_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_clear.Location = new System.Drawing.Point(20, 138);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(160, 30);
            this.button_clear.TabIndex = 4;
            this.button_clear.Text = "F2 - Очистить";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 235);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.button_otgryzka);
            this.Controls.Add(this.button_priem);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_print);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MenuForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_print;
        private System.Windows.Forms.Button button_exit;
        private System.Windows.Forms.Button button_priem;
        private System.Windows.Forms.Button button_otgryzka;
        private System.Windows.Forms.Button button_clear;
    }
}