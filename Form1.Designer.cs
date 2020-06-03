namespace SWB_OptionPackageInstaller
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btCleanUp = new System.Windows.Forms.Button();
            this.btOptions = new System.Windows.Forms.Button();
            this.btBrowseForPackages = new System.Windows.Forms.Button();
            this.tbPathOfPackages = new System.Windows.Forms.TextBox();
            this.lbPathOfSWB = new System.Windows.Forms.Label();
            this.lbPathOfPackages = new System.Windows.Forms.Label();
            this.tbPathOfSWB = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.lbInfoText = new System.Windows.Forms.Label();
            this.tbInfo1 = new System.Windows.Forms.TextBox();
            this.btStartSWB = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btGenPath = new System.Windows.Forms.Button();
            this.lbCheckFolderContents = new System.Windows.Forms.Label();
            this.btCheckOPs = new System.Windows.Forms.Button();
            this.dgvForPackagesInFolder = new System.Windows.Forms.DataGridView();
            this.tbPageOptionPackages = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbInfoText2 = new System.Windows.Forms.Label();
            this.lbOPServer = new System.Windows.Forms.Label();
            this.tbOptionPackagesServer = new System.Windows.Forms.TextBox();
            this.btCollectFeaturesFromRemote = new System.Windows.Forms.Button();
            this.tbInfo2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbPageCollectedArtifacts = new System.Windows.Forms.TabPage();
            this.mainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForPackagesInFolder)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabPage1);
            this.mainTabControl.Controls.Add(this.tbPageOptionPackages);
            this.mainTabControl.Controls.Add(this.tabPage2);
            this.mainTabControl.Controls.Add(this.tbPageCollectedArtifacts);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1067, 554);
            this.mainTabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1059, 525);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Path settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Bisque;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.44056F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.55944F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel1.Controls.Add(this.btCleanUp, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btOptions, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btBrowseForPackages, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPathOfPackages, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbPathOfSWB, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbPathOfPackages, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPathOfSWB, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btOK, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbInfoText, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbInfo1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btStartSWB, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btGenPath, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbCheckFolderContents, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btCheckOPs, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.dgvForPackagesInFolder, 1, 6);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1051, 517);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btCleanUp
            // 
            this.btCleanUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btCleanUp.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCleanUp.Location = new System.Drawing.Point(877, 231);
            this.btCleanUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btCleanUp.Name = "btCleanUp";
            this.btCleanUp.Size = new System.Drawing.Size(170, 46);
            this.btCleanUp.TabIndex = 15;
            this.btCleanUp.Text = "Clean Up";
            this.btCleanUp.UseVisualStyleBackColor = true;
            this.btCleanUp.Click += new System.EventHandler(this.btCleanUp_Click);
            // 
            // btOptions
            // 
            this.btOptions.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOptions.Location = new System.Drawing.Point(877, 332);
            this.btOptions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btOptions.Name = "btOptions";
            this.btOptions.Size = new System.Drawing.Size(164, 46);
            this.btOptions.TabIndex = 14;
            this.btOptions.Text = "Options";
            this.btOptions.UseVisualStyleBackColor = true;
            this.btOptions.Click += new System.EventHandler(this.btOptions_Click);
            // 
            // btBrowseForPackages
            // 
            this.btBrowseForPackages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowseForPackages.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowseForPackages.Location = new System.Drawing.Point(877, 59);
            this.btBrowseForPackages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btBrowseForPackages.Name = "btBrowseForPackages";
            this.btBrowseForPackages.Size = new System.Drawing.Size(170, 36);
            this.btBrowseForPackages.TabIndex = 4;
            this.btBrowseForPackages.Text = "...";
            this.btBrowseForPackages.UseVisualStyleBackColor = true;
            this.btBrowseForPackages.Click += new System.EventHandler(this.btBrowseForPackages_Click);
            // 
            // tbPathOfPackages
            // 
            this.tbPathOfPackages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPathOfPackages.Location = new System.Drawing.Point(349, 63);
            this.tbPathOfPackages.Margin = new System.Windows.Forms.Padding(4);
            this.tbPathOfPackages.Name = "tbPathOfPackages";
            this.tbPathOfPackages.Size = new System.Drawing.Size(520, 28);
            this.tbPathOfPackages.TabIndex = 3;
            this.tbPathOfPackages.Tag = "1";
            this.tbPathOfPackages.Text = "C:\\_SWB\\OpModeProblem\\";
            this.tbPathOfPackages.TextChanged += new System.EventHandler(this.tbPathOfPackages_TextChanged);
            // 
            // lbPathOfSWB
            // 
            this.lbPathOfSWB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPathOfSWB.AutoSize = true;
            this.lbPathOfSWB.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPathOfSWB.Location = new System.Drawing.Point(105, 14);
            this.lbPathOfSWB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPathOfSWB.Name = "lbPathOfSWB";
            this.lbPathOfSWB.Size = new System.Drawing.Size(135, 24);
            this.lbPathOfSWB.TabIndex = 0;
            this.lbPathOfSWB.Text = "Path Of SWB:";
            // 
            // lbPathOfPackages
            // 
            this.lbPathOfPackages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPathOfPackages.AutoSize = true;
            this.lbPathOfPackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPathOfPackages.Location = new System.Drawing.Point(82, 65);
            this.lbPathOfPackages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPathOfPackages.Name = "lbPathOfPackages";
            this.lbPathOfPackages.Size = new System.Drawing.Size(180, 24);
            this.lbPathOfPackages.TabIndex = 1;
            this.lbPathOfPackages.Text = "Path Of Packages:";
            // 
            // tbPathOfSWB
            // 
            this.tbPathOfSWB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPathOfSWB.Location = new System.Drawing.Point(349, 12);
            this.tbPathOfSWB.Margin = new System.Windows.Forms.Padding(4);
            this.tbPathOfSWB.Name = "tbPathOfSWB";
            this.tbPathOfSWB.Size = new System.Drawing.Size(520, 28);
            this.tbPathOfSWB.TabIndex = 1;
            this.tbPathOfSWB.Tag = "0";
            this.tbPathOfSWB.Text = "C:\\_SWB\\OpModeProblem\\SWB";
            this.tbPathOfSWB.TextChanged += new System.EventHandler(this.tbPathOfSWB_TextChanged);
            // 
            // btOK
            // 
            this.btOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOK.Location = new System.Drawing.Point(349, 107);
            this.btOK.Margin = new System.Windows.Forms.Padding(4);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(520, 67);
            this.btOK.TabIndex = 5;
            this.btOK.Text = "Start";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // lbInfoText
            // 
            this.lbInfoText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfoText.AutoSize = true;
            this.lbInfoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfoText.Location = new System.Drawing.Point(4, 128);
            this.lbInfoText.Name = "lbInfoText";
            this.lbInfoText.Size = new System.Drawing.Size(337, 24);
            this.lbInfoText.TabIndex = 7;
            this.lbInfoText.Text = "Current Status:";
            this.lbInfoText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbInfo1
            // 
            this.tbInfo1.BackColor = System.Drawing.Color.Bisque;
            this.tbInfo1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInfo1.Location = new System.Drawing.Point(4, 181);
            this.tbInfo1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbInfo1.Multiline = true;
            this.tbInfo1.Name = "tbInfo1";
            this.tbInfo1.Size = new System.Drawing.Size(337, 146);
            this.tbInfo1.TabIndex = 8;
            // 
            // btStartSWB
            // 
            this.btStartSWB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btStartSWB.Location = new System.Drawing.Point(348, 231);
            this.btStartSWB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btStartSWB.Name = "btStartSWB";
            this.btStartSWB.Size = new System.Drawing.Size(522, 46);
            this.btStartSWB.TabIndex = 9;
            this.btStartSWB.Text = "Start installed SWB";
            this.btStartSWB.UseVisualStyleBackColor = true;
            this.btStartSWB.Visible = false;
            this.btStartSWB.Click += new System.EventHandler(this.btStartSWB_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(877, 8);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(170, 36);
            this.button2.TabIndex = 10;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btBrowseSWBPath_Click);
            // 
            // btGenPath
            // 
            this.btGenPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btGenPath.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGenPath.Location = new System.Drawing.Point(877, 105);
            this.btGenPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btGenPath.Name = "btGenPath";
            this.btGenPath.Size = new System.Drawing.Size(170, 71);
            this.btGenPath.TabIndex = 2;
            this.btGenPath.Text = "Generate Path";
            this.btGenPath.UseVisualStyleBackColor = true;
            this.btGenPath.Click += new System.EventHandler(this.btGenPath_Click);
            // 
            // lbCheckFolderContents
            // 
            this.lbCheckFolderContents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCheckFolderContents.AutoSize = true;
            this.lbCheckFolderContents.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCheckFolderContents.Location = new System.Drawing.Point(4, 343);
            this.lbCheckFolderContents.Name = "lbCheckFolderContents";
            this.lbCheckFolderContents.Size = new System.Drawing.Size(337, 24);
            this.lbCheckFolderContents.TabIndex = 11;
            this.lbCheckFolderContents.Text = "Check Option Packages Folder:";
            this.lbCheckFolderContents.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btCheckOPs
            // 
            this.btCheckOPs.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btCheckOPs.Location = new System.Drawing.Point(348, 332);
            this.btCheckOPs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btCheckOPs.Name = "btCheckOPs";
            this.btCheckOPs.Size = new System.Drawing.Size(522, 46);
            this.btCheckOPs.TabIndex = 12;
            this.btCheckOPs.Text = "Check Option Packages In Folder";
            this.btCheckOPs.UseVisualStyleBackColor = true;
            this.btCheckOPs.Click += new System.EventHandler(this.btCheckOPs_Click);
            // 
            // dgvForPackagesInFolder
            // 
            this.dgvForPackagesInFolder.ColumnHeadersHeight = 29;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvForPackagesInFolder, 2);
            this.dgvForPackagesInFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvForPackagesInFolder.Location = new System.Drawing.Point(348, 435);
            this.dgvForPackagesInFolder.Name = "dgvForPackagesInFolder";
            this.dgvForPackagesInFolder.RowHeadersVisible = false;
            this.dgvForPackagesInFolder.RowHeadersWidth = 51;
            this.tableLayoutPanel1.SetRowSpan(this.dgvForPackagesInFolder, 2);
            this.dgvForPackagesInFolder.RowTemplate.Height = 24;
            this.dgvForPackagesInFolder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvForPackagesInFolder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvForPackagesInFolder.Size = new System.Drawing.Size(699, 78);
            this.dgvForPackagesInFolder.TabIndex = 13;
            // 
            // tbPageOptionPackages
            // 
            this.tbPageOptionPackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPageOptionPackages.Location = new System.Drawing.Point(4, 25);
            this.tbPageOptionPackages.Margin = new System.Windows.Forms.Padding(4);
            this.tbPageOptionPackages.Name = "tbPageOptionPackages";
            this.tbPageOptionPackages.Padding = new System.Windows.Forms.Padding(4);
            this.tbPageOptionPackages.Size = new System.Drawing.Size(1059, 525);
            this.tbPageOptionPackages.TabIndex = 1;
            this.tbPageOptionPackages.Text = "Installed option packages";
            this.tbPageOptionPackages.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1059, 525);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Copy resources from server";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.93252F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.06747F));
            this.tableLayoutPanel2.Controls.Add(this.lbInfoText2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbOPServer, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbOptionPackagesServer, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btCollectFeaturesFromRemote, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbInfo2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.button1, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.67308F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.32692F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1051, 517);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lbInfoText2
            // 
            this.lbInfoText2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbInfoText2.AutoSize = true;
            this.lbInfoText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfoText2.Location = new System.Drawing.Point(89, 225);
            this.lbInfoText2.Name = "lbInfoText2";
            this.lbInfoText2.Size = new System.Drawing.Size(147, 24);
            this.lbInfoText2.TabIndex = 8;
            this.lbInfoText2.Text = "Current Status:";
            // 
            // lbOPServer
            // 
            this.lbOPServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOPServer.AutoSize = true;
            this.lbOPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOPServer.Location = new System.Drawing.Point(4, 15);
            this.lbOPServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbOPServer.Name = "lbOPServer";
            this.lbOPServer.Size = new System.Drawing.Size(317, 50);
            this.lbOPServer.TabIndex = 0;
            this.lbOPServer.Text = "Path of remote dropdown folder:";
            this.lbOPServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbOptionPackagesServer
            // 
            this.tbOptionPackagesServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOptionPackagesServer.Location = new System.Drawing.Point(329, 25);
            this.tbOptionPackagesServer.Margin = new System.Windows.Forms.Padding(4);
            this.tbOptionPackagesServer.Name = "tbOptionPackagesServer";
            this.tbOptionPackagesServer.Size = new System.Drawing.Size(718, 30);
            this.tbOptionPackagesServer.TabIndex = 1;
            this.tbOptionPackagesServer.Text = "\\\\KUKA.int.kuka.com\\s\\KROS_Pool\\Daily\\NavigationSolution\\master\\";
            // 
            // btCollectFeaturesFromRemote
            // 
            this.btCollectFeaturesFromRemote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btCollectFeaturesFromRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCollectFeaturesFromRemote.Location = new System.Drawing.Point(329, 221);
            this.btCollectFeaturesFromRemote.Margin = new System.Windows.Forms.Padding(4);
            this.btCollectFeaturesFromRemote.Name = "btCollectFeaturesFromRemote";
            this.btCollectFeaturesFromRemote.Size = new System.Drawing.Size(718, 32);
            this.btCollectFeaturesFromRemote.TabIndex = 2;
            this.btCollectFeaturesFromRemote.Text = "Collect";
            this.btCollectFeaturesFromRemote.UseVisualStyleBackColor = true;
            this.btCollectFeaturesFromRemote.Click += new System.EventHandler(this.btCollect_Click);
            // 
            // tbInfo2
            // 
            this.tbInfo2.BackColor = System.Drawing.Color.Bisque;
            this.tbInfo2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInfo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInfo2.Location = new System.Drawing.Point(3, 396);
            this.tbInfo2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbInfo2.Multiline = true;
            this.tbInfo2.Name = "tbInfo2";
            this.tbInfo2.Size = new System.Drawing.Size(319, 119);
            this.tbInfo2.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(328, 435);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(720, 41);
            this.button1.TabIndex = 10;
            this.button1.Text = "Clean Up Copied Resources";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CleanUpEverything_Click);
            // 
            // tbPageCollectedArtifacts
            // 
            this.tbPageCollectedArtifacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPageCollectedArtifacts.Location = new System.Drawing.Point(4, 25);
            this.tbPageCollectedArtifacts.Margin = new System.Windows.Forms.Padding(4);
            this.tbPageCollectedArtifacts.Name = "tbPageCollectedArtifacts";
            this.tbPageCollectedArtifacts.Padding = new System.Windows.Forms.Padding(4);
            this.tbPageCollectedArtifacts.Size = new System.Drawing.Size(1059, 525);
            this.tbPageCollectedArtifacts.TabIndex = 3;
            this.tbPageCollectedArtifacts.Text = "Collected Option Packages";
            this.tbPageCollectedArtifacts.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.mainTabControl);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.mainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForPackagesInFolder)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btBrowseForPackages;
        private System.Windows.Forms.TextBox tbPathOfPackages;
        private System.Windows.Forms.Label lbPathOfSWB;
        private System.Windows.Forms.Label lbPathOfPackages;
        private System.Windows.Forms.TextBox tbPathOfSWB;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Label lbInfoText;
        private System.Windows.Forms.TextBox tbInfo1;
        private System.Windows.Forms.Button btStartSWB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btGenPath;
        private System.Windows.Forms.TabPage tbPageOptionPackages;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbInfoText2;
        private System.Windows.Forms.Label lbOPServer;
        private System.Windows.Forms.TextBox tbOptionPackagesServer;
        private System.Windows.Forms.Button btCollectFeaturesFromRemote;
        private System.Windows.Forms.TextBox tbInfo2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tbPageCollectedArtifacts;
        private System.Windows.Forms.Button btCheckOPs;
        private System.Windows.Forms.DataGridView dgvForPackagesInFolder;
        private System.Windows.Forms.Label lbCheckFolderContents;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btOptions;
        private System.Windows.Forms.Button btCleanUp;
    }
}