namespace FunPop
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fixNow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fixNow
            // 
            this.fixNow.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.fixNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fixNow.Location = new System.Drawing.Point(550, 281);
            this.fixNow.Name = "fixNow";
            this.fixNow.Size = new System.Drawing.Size(121, 38);
            this.fixNow.TabIndex = 0;
            this.fixNow.Text = "Fix Now";
            this.fixNow.UseVisualStyleBackColor = false;
            this.fixNow.Click += new System.EventHandler(this.fixNow_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1217, 576);
            this.Controls.Add(this.fixNow);
            this.Name = "MainForm";
            this.Text = "FunPop";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button fixNow;
    }
}
