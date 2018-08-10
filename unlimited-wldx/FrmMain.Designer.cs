namespace unlimited_wldx
{
    partial class FromMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FromMain));
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelNav = new System.Windows.Forms.Panel();
            this.TextNav = new System.Windows.Forms.TextBox();
            this.BtnGo = new System.Windows.Forms.Button();
            this.WebBrowserMain = new System.Windows.Forms.WebBrowser();
            this.ExampleSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.DatabaseOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuMain.SuspendLayout();
            this.panelNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusMain
            // 
            this.statusMain.Location = new System.Drawing.Point(0, 537);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(800, 22);
            this.statusMain.TabIndex = 0;
            this.statusMain.Text = "statusStrip1";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportToolStripMenuItem,
            this.ShowToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // ImportToolStripMenuItem
            // 
            this.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem";
            this.ImportToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.ImportToolStripMenuItem.Text = "&Import questions xml";
            this.ImportToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // ShowToolStripMenuItem
            // 
            this.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem";
            this.ShowToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.ShowToolStripMenuItem.Text = "&Show hints";
            this.ShowToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.ExitToolStripMenuItem.Text = "&Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportStripMenuItem,
            this.toolStripSeparator2,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "&Help";
            // 
            // ExportStripMenuItem
            // 
            this.ExportStripMenuItem.Name = "ExportStripMenuItem";
            this.ExportStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.ExportStripMenuItem.Text = "&Export example template";
            this.ExportStripMenuItem.Click += new System.EventHandler(this.ExportStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.AboutToolStripMenuItem.Text = "&About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // panelNav
            // 
            this.panelNav.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelNav.Controls.Add(this.TextNav);
            this.panelNav.Controls.Add(this.BtnGo);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNav.Location = new System.Drawing.Point(0, 24);
            this.panelNav.Name = "panelNav";
            this.panelNav.Padding = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.panelNav.Size = new System.Drawing.Size(800, 28);
            this.panelNav.TabIndex = 2;
            // 
            // TextNav
            // 
            this.TextNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextNav.Location = new System.Drawing.Point(10, 1);
            this.TextNav.Multiline = true;
            this.TextNav.Name = "TextNav";
            this.TextNav.Size = new System.Drawing.Size(705, 26);
            this.TextNav.TabIndex = 1;
            this.TextNav.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextNav_KeyPress);
            // 
            // BtnGo
            // 
            this.BtnGo.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnGo.Location = new System.Drawing.Point(715, 1);
            this.BtnGo.Name = "BtnGo";
            this.BtnGo.Size = new System.Drawing.Size(75, 26);
            this.BtnGo.TabIndex = 0;
            this.BtnGo.Text = "GO";
            this.BtnGo.UseVisualStyleBackColor = true;
            this.BtnGo.Click += new System.EventHandler(this.BtnGo_Click);
            // 
            // WebBrowserMain
            // 
            this.WebBrowserMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserMain.Location = new System.Drawing.Point(0, 52);
            this.WebBrowserMain.MinimumSize = new System.Drawing.Size(20, 22);
            this.WebBrowserMain.Name = "WebBrowserMain";
            this.WebBrowserMain.ScriptErrorsSuppressed = true;
            this.WebBrowserMain.Size = new System.Drawing.Size(800, 485);
            this.WebBrowserMain.TabIndex = 3;
            this.WebBrowserMain.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowserMain_Navigating);
            // 
            // ExampleSaveFileDialog
            // 
            this.ExampleSaveFileDialog.FileName = "example.xml";
            this.ExampleSaveFileDialog.Filter = "XML file (*.xml)|*.xml|all file (*.*)|*.*";
            this.ExampleSaveFileDialog.RestoreDirectory = true;
            // 
            // DatabaseOpenFileDialog
            // 
            this.DatabaseOpenFileDialog.Filter = "XML file (*.xml)|*.xml|all file (*.*)|*.*";
            this.DatabaseOpenFileDialog.RestoreDirectory = true;
            this.DatabaseOpenFileDialog.ShowReadOnly = true;
            // 
            // FromMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 559);
            this.Controls.Add(this.WebBrowserMain);
            this.Controls.Add(this.panelNav);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "FromMain";
            this.Text = "unlimited-wldx";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FromMain_FormClosing);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.panelNav.ResumeLayout(false);
            this.panelNav.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportToolStripMenuItem;
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Button BtnGo;
        private System.Windows.Forms.TextBox TextNav;
        private System.Windows.Forms.WebBrowser WebBrowserMain;
        private System.Windows.Forms.ToolStripMenuItem ExportStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SaveFileDialog ExampleSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog DatabaseOpenFileDialog;
    }
}

