namespace TerminalCH
{
    partial class message_dialog
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
            this.label_main1 = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_yes = new System.Windows.Forms.Button();
            this.button_no = new System.Windows.Forms.Button();
            this.label_main2 = new System.Windows.Forms.Label();
            this.label_main3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_main1
            // 
            this.label_main1.AutoSize = true;
            this.label_main1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_main1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_main1.Location = new System.Drawing.Point(12, 9);
            this.label_main1.Name = "label_main1";
            this.label_main1.Size = new System.Drawing.Size(180, 16);
            this.label_main1.TabIndex = 0;
            this.label_main1.Text = "label1label1label1label1";
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(69, 153);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(63, 41);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "Ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_yes
            // 
            this.button_yes.Location = new System.Drawing.Point(15, 153);
            this.button_yes.Name = "button_yes";
            this.button_yes.Size = new System.Drawing.Size(63, 41);
            this.button_yes.TabIndex = 2;
            this.button_yes.Text = "Да";
            this.button_yes.UseVisualStyleBackColor = true;
            this.button_yes.Click += new System.EventHandler(this.button_yes_Click);
            // 
            // button_no
            // 
            this.button_no.Location = new System.Drawing.Point(115, 153);
            this.button_no.Name = "button_no";
            this.button_no.Size = new System.Drawing.Size(63, 41);
            this.button_no.TabIndex = 3;
            this.button_no.Text = "Нет";
            this.button_no.UseVisualStyleBackColor = true;
            this.button_no.Click += new System.EventHandler(this.button_no_Click);
            // 
            // label_main2
            // 
            this.label_main2.AutoSize = true;
            this.label_main2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_main2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_main2.Location = new System.Drawing.Point(12, 39);
            this.label_main2.Name = "label_main2";
            this.label_main2.Size = new System.Drawing.Size(51, 16);
            this.label_main2.TabIndex = 4;
            this.label_main2.Text = "label1";
            // 
            // label_main3
            // 
            this.label_main3.AutoSize = true;
            this.label_main3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_main3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_main3.Location = new System.Drawing.Point(12, 69);
            this.label_main3.Name = "label_main3";
            this.label_main3.Size = new System.Drawing.Size(51, 16);
            this.label_main3.TabIndex = 5;
            this.label_main3.Text = "label1";
            // 
            // message_dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 206);
            this.Controls.Add(this.label_main3);
            this.Controls.Add(this.label_main2);
            this.Controls.Add(this.button_no);
            this.Controls.Add(this.button_yes);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label_main1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "message_dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "message_dialog";
            this.Load += new System.EventHandler(this.message_dialog_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.message_dialog_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.message_dialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_main1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_yes;
        private System.Windows.Forms.Button button_no;
        private System.Windows.Forms.Label label_main2;
        private System.Windows.Forms.Label label_main3;
    }
}