namespace Corporate.Plataforms.Settings.DevTools
{
    partial class frmDevTools
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hubTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAtfactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hubTestToolStripMenuItem,
            this.createAtfactsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hubTestToolStripMenuItem
            // 
            this.hubTestToolStripMenuItem.Name = "hubTestToolStripMenuItem";
            this.hubTestToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.hubTestToolStripMenuItem.Text = "Hub Test";
            this.hubTestToolStripMenuItem.Click += new System.EventHandler(this.hubTestToolStripMenuItem_Click);
            // 
            // createAtfactsToolStripMenuItem
            // 
            this.createAtfactsToolStripMenuItem.Name = "createAtfactsToolStripMenuItem";
            this.createAtfactsToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.createAtfactsToolStripMenuItem.Text = "Create artfacts";
            this.createAtfactsToolStripMenuItem.Click += new System.EventHandler(this.createAtfactsToolStripMenuItem_Click);
            // 
            // frmDevTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmDevTools";
            this.Text = "Settings Manager DevTools";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hubTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createAtfactsToolStripMenuItem;
    }
}