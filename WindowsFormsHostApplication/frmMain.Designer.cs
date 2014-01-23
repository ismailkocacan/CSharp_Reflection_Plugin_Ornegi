namespace WindowsFormsHostApplication
{
    partial class frmMain
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
            this.layoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnIkinciPluginiCalistir = new System.Windows.Forms.Button();
            this.btnUnloadPlugin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutPanel.Location = new System.Drawing.Point(0, 221);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.Size = new System.Drawing.Size(339, 162);
            this.layoutPanel.TabIndex = 0;
            // 
            // btnIkinciPluginiCalistir
            // 
            this.btnIkinciPluginiCalistir.Location = new System.Drawing.Point(23, 30);
            this.btnIkinciPluginiCalistir.Name = "btnIkinciPluginiCalistir";
            this.btnIkinciPluginiCalistir.Size = new System.Drawing.Size(177, 23);
            this.btnIkinciPluginiCalistir.TabIndex = 1;
            this.btnIkinciPluginiCalistir.Text = "2. Plugini Çalıştır";
            this.btnIkinciPluginiCalistir.UseVisualStyleBackColor = true;
            this.btnIkinciPluginiCalistir.Click += new System.EventHandler(this.btnIkinciPluginiCalistir_Click);
            // 
            // btnUnloadPlugin
            // 
            this.btnUnloadPlugin.Location = new System.Drawing.Point(23, 88);
            this.btnUnloadPlugin.Name = "btnUnloadPlugin";
            this.btnUnloadPlugin.Size = new System.Drawing.Size(177, 23);
            this.btnUnloadPlugin.TabIndex = 2;
            this.btnUnloadPlugin.Text = "UnLoad Plugins";
            this.btnUnloadPlugin.UseVisualStyleBackColor = true;
            this.btnUnloadPlugin.Click += new System.EventHandler(this.btnUnloadPlugin_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 383);
            this.Controls.Add(this.btnUnloadPlugin);
            this.Controls.Add(this.btnIkinciPluginiCalistir);
            this.Controls.Add(this.layoutPanel);
            this.Name = "frmMain";
            this.Text = "Host Uygulaması";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel layoutPanel;
        private System.Windows.Forms.Button btnIkinciPluginiCalistir;
        private System.Windows.Forms.Button btnUnloadPlugin;
    }
}

