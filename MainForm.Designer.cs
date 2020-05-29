namespace SWB_OptionPackageInstaller
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbPathOfSWB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPathOfOptionpackages = new System.Windows.Forms.TextBox();
            this.btStart = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbPageQuickInstall = new System.Windows.Forms.TabPage();
            this.tbPageOptionPackages = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.multipleSWBCreationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyArtifactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sWBExtractionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installOptionPackagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbPageQuickInstall.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tbPathOfSWB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPathOfOptionpackages, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btStart, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(918, 459);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbPathOfSWB
            // 
            this.tbPathOfSWB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPathOfSWB.Location = new System.Drawing.Point(491, 54);
            this.tbPathOfSWB.Name = "tbPathOfSWB";
            this.tbPathOfSWB.Size = new System.Drawing.Size(394, 24);
            this.tbPathOfSWB.TabIndex = 3;
            this.tbPathOfSWB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPathOfSWB_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(139, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path of optionpackages:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(177, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Path of SWB:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPathOfOptionpackages
            // 
            this.tbPathOfOptionpackages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPathOfOptionpackages.Location = new System.Drawing.Point(491, 10);
            this.tbPathOfOptionpackages.Name = "tbPathOfOptionpackages";
            this.tbPathOfOptionpackages.Size = new System.Drawing.Size(394, 24);
            this.tbPathOfOptionpackages.TabIndex = 2;
            this.tbPathOfOptionpackages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPathOfOptionpackages_KeyDown);
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btStart.Location = new System.Drawing.Point(764, 92);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(151, 35);
            this.btStart.TabIndex = 4;
            this.btStart.Text = "Start Installing";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbPageQuickInstall);
            this.tabControl1.Controls.Add(this.tbPageOptionPackages);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(932, 491);
            this.tabControl1.TabIndex = 1;
            // 
            // tbPageQuickInstall
            // 
            this.tbPageQuickInstall.Controls.Add(this.tableLayoutPanel1);
            this.tbPageQuickInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPageQuickInstall.Location = new System.Drawing.Point(4, 22);
            this.tbPageQuickInstall.Name = "tbPageQuickInstall";
            this.tbPageQuickInstall.Padding = new System.Windows.Forms.Padding(3);
            this.tbPageQuickInstall.Size = new System.Drawing.Size(924, 465);
            this.tbPageQuickInstall.TabIndex = 0;
            this.tbPageQuickInstall.Text = "Quick Install";
            this.tbPageQuickInstall.UseVisualStyleBackColor = true;
            // 
            // tbPageOptionPackages
            // 
            this.tbPageOptionPackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbPageOptionPackages.Location = new System.Drawing.Point(4, 22);
            this.tbPageOptionPackages.Name = "tbPageOptionPackages";
            this.tbPageOptionPackages.Padding = new System.Windows.Forms.Padding(3);
            this.tbPageOptionPackages.Size = new System.Drawing.Size(924, 465);
            this.tbPageOptionPackages.TabIndex = 1;
            this.tbPageOptionPackages.Text = "Option Packages List";
            this.tbPageOptionPackages.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripDropDownButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(932, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(687, 20);
            this.toolStripStatusLabel1.Text = "Settings Info: ShowConsole: Off; SWBExtract: Off; CopyArtifacts: Off; MultipleSWB" +
    "Creation: Off  ";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.multipleSWBCreationToolStripMenuItem,
            this.copyArtifactsToolStripMenuItem,
            this.sWBExtractionToolStripMenuItem,
            this.showConsoleToolStripMenuItem,
            this.installOptionPackagesToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 23);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // multipleSWBCreationToolStripMenuItem
            // 
            this.multipleSWBCreationToolStripMenuItem.Name = "multipleSWBCreationToolStripMenuItem";
            this.multipleSWBCreationToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.multipleSWBCreationToolStripMenuItem.Text = "MultipleSWBCreation";
            // 
            // copyArtifactsToolStripMenuItem
            // 
            this.copyArtifactsToolStripMenuItem.Name = "copyArtifactsToolStripMenuItem";
            this.copyArtifactsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.copyArtifactsToolStripMenuItem.Text = "CopyArtifacts";
            // 
            // sWBExtractionToolStripMenuItem
            // 
            this.sWBExtractionToolStripMenuItem.Name = "sWBExtractionToolStripMenuItem";
            this.sWBExtractionToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.sWBExtractionToolStripMenuItem.Text = "SWB Extraction";
            this.sWBExtractionToolStripMenuItem.Click += new System.EventHandler(this.sWBExtractionToolStripMenuItem_Click);
            // 
            // showConsoleToolStripMenuItem
            // 
            this.showConsoleToolStripMenuItem.Name = "showConsoleToolStripMenuItem";
            this.showConsoleToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.showConsoleToolStripMenuItem.Text = "ShowConsole";
            this.showConsoleToolStripMenuItem.Click += new System.EventHandler(this.showConsoleToolStripMenuItem_Click);
            // 
            // installOptionPackagesToolStripMenuItem
            // 
            this.installOptionPackagesToolStripMenuItem.Name = "installOptionPackagesToolStripMenuItem";
            this.installOptionPackagesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.installOptionPackagesToolStripMenuItem.Text = "Install Option Packages";
            this.installOptionPackagesToolStripMenuItem.Click += new System.EventHandler(this.installOptionPackagesToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 516);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbPageQuickInstall.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbPathOfSWB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPathOfOptionpackages;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbPageQuickInstall;
        private System.Windows.Forms.TabPage tbPageOptionPackages;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem multipleSWBCreationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyArtifactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sWBExtractionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installOptionPackagesToolStripMenuItem;
    }
}

