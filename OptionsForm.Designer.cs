namespace SWB_OptionPackageInstaller
{
    partial class OptionsForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btOptionsCancel = new System.Windows.Forms.Button();
            this.btOptionsOK = new System.Windows.Forms.Button();
            this.tbDefaultPathForPackages = new System.Windows.Forms.TextBox();
            this.lbDefaultPathForSWB = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDefaultPathForSWB = new System.Windows.Forms.TextBox();
            this.cbInstallAllFoundOPs = new System.Windows.Forms.CheckBox();
            this.lbSetArgsForConsole = new System.Windows.Forms.Label();
            this.argumentsForConsoleTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lbArgInstallFromDefaultFolder = new System.Windows.Forms.Label();
            this.cbIntallFromDefaultFolder = new System.Windows.Forms.CheckBox();
            this.lbCollectLatestPackagesAndInstall = new System.Windows.Forms.Label();
            this.cbCollectAndInstall = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbStorePackages = new System.Windows.Forms.Label();
            this.cbStoreCollectedPAckages = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.argumentsForConsoleTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cbStoreCollectedPAckages, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbStorePackages, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbSetArgsForConsole, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btOptionsCancel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btOptionsOK, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbDefaultPathForPackages, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbDefaultPathForSWB, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbDefaultPathForSWB, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbInstallAllFoundOPs, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.argumentsForConsoleTableLayout, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(983, 436);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // btOptionsCancel
            // 
            this.btOptionsCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btOptionsCancel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOptionsCancel.Location = new System.Drawing.Point(493, 375);
            this.btOptionsCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btOptionsCancel.Name = "btOptionsCancel";
            this.btOptionsCancel.Size = new System.Drawing.Size(488, 37);
            this.btOptionsCancel.TabIndex = 16;
            this.btOptionsCancel.Text = "Cancel";
            this.btOptionsCancel.UseVisualStyleBackColor = true;
            this.btOptionsCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOptionsOK
            // 
            this.btOptionsOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btOptionsOK.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOptionsOK.Location = new System.Drawing.Point(2, 375);
            this.btOptionsOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btOptionsOK.Name = "btOptionsOK";
            this.btOptionsOK.Size = new System.Drawing.Size(487, 37);
            this.btOptionsOK.TabIndex = 15;
            this.btOptionsOK.Text = "Save";
            this.btOptionsOK.UseVisualStyleBackColor = true;
            this.btOptionsOK.Click += new System.EventHandler(this.btOptionsOK_Click);
            // 
            // tbDefaultPathForPackages
            // 
            this.tbDefaultPathForPackages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDefaultPathForPackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDefaultPathForPackages.Location = new System.Drawing.Point(493, 31);
            this.tbDefaultPathForPackages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDefaultPathForPackages.Name = "tbDefaultPathForPackages";
            this.tbDefaultPathForPackages.Size = new System.Drawing.Size(488, 26);
            this.tbDefaultPathForPackages.TabIndex = 5;
            this.tbDefaultPathForPackages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbDefaultPathForSWB
            // 
            this.lbDefaultPathForSWB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDefaultPathForSWB.AutoSize = true;
            this.lbDefaultPathForSWB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDefaultPathForSWB.Location = new System.Drawing.Point(2, 0);
            this.lbDefaultPathForSWB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDefaultPathForSWB.Name = "lbDefaultPathForSWB";
            this.lbDefaultPathForSWB.Size = new System.Drawing.Size(487, 16);
            this.lbDefaultPathForSWB.TabIndex = 0;
            this.lbDefaultPathForSWB.Text = "Default Path for SWB folders:";
            this.lbDefaultPathForSWB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(487, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Default Path for Option Packages folder:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(487, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Install all found option package:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbDefaultPathForSWB
            // 
            this.tbDefaultPathForSWB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDefaultPathForSWB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDefaultPathForSWB.Location = new System.Drawing.Point(493, 2);
            this.tbDefaultPathForSWB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDefaultPathForSWB.Name = "tbDefaultPathForSWB";
            this.tbDefaultPathForSWB.Size = new System.Drawing.Size(488, 26);
            this.tbDefaultPathForSWB.TabIndex = 4;
            this.tbDefaultPathForSWB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbInstallAllFoundOPs
            // 
            this.cbInstallAllFoundOPs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInstallAllFoundOPs.AutoSize = true;
            this.cbInstallAllFoundOPs.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbInstallAllFoundOPs.Checked = true;
            this.cbInstallAllFoundOPs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInstallAllFoundOPs.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cbInstallAllFoundOPs.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cbInstallAllFoundOPs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbInstallAllFoundOPs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbInstallAllFoundOPs.Location = new System.Drawing.Point(493, 96);
            this.cbInstallAllFoundOPs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbInstallAllFoundOPs.Name = "cbInstallAllFoundOPs";
            this.cbInstallAllFoundOPs.Size = new System.Drawing.Size(488, 11);
            this.cbInstallAllFoundOPs.TabIndex = 17;
            this.cbInstallAllFoundOPs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbInstallAllFoundOPs.UseVisualStyleBackColor = true;
            // 
            // lbSetArgsForConsole
            // 
            this.lbSetArgsForConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSetArgsForConsole.AutoSize = true;
            this.lbSetArgsForConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSetArgsForConsole.Location = new System.Drawing.Point(2, 204);
            this.lbSetArgsForConsole.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSetArgsForConsole.Name = "lbSetArgsForConsole";
            this.lbSetArgsForConsole.Size = new System.Drawing.Size(487, 20);
            this.lbSetArgsForConsole.TabIndex = 18;
            this.lbSetArgsForConsole.Text = "Set arguments for console mode:";
            this.lbSetArgsForConsole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // argumentsForConsoleTableLayout
            // 
            this.argumentsForConsoleTableLayout.ColumnCount = 2;
            this.argumentsForConsoleTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.argumentsForConsoleTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.argumentsForConsoleTableLayout.Controls.Add(this.checkBox1, 1, 2);
            this.argumentsForConsoleTableLayout.Controls.Add(this.label3, 0, 2);
            this.argumentsForConsoleTableLayout.Controls.Add(this.cbCollectAndInstall, 1, 1);
            this.argumentsForConsoleTableLayout.Controls.Add(this.lbCollectLatestPackagesAndInstall, 0, 1);
            this.argumentsForConsoleTableLayout.Controls.Add(this.lbArgInstallFromDefaultFolder, 0, 0);
            this.argumentsForConsoleTableLayout.Controls.Add(this.cbIntallFromDefaultFolder, 1, 0);
            this.argumentsForConsoleTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentsForConsoleTableLayout.Location = new System.Drawing.Point(494, 135);
            this.argumentsForConsoleTableLayout.Name = "argumentsForConsoleTableLayout";
            this.argumentsForConsoleTableLayout.RowCount = 3;
            this.argumentsForConsoleTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.argumentsForConsoleTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.argumentsForConsoleTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.argumentsForConsoleTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.argumentsForConsoleTableLayout.Size = new System.Drawing.Size(486, 159);
            this.argumentsForConsoleTableLayout.TabIndex = 19;
            // 
            // lbArgInstallFromDefaultFolder
            // 
            this.lbArgInstallFromDefaultFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbArgInstallFromDefaultFolder.AutoSize = true;
            this.lbArgInstallFromDefaultFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbArgInstallFromDefaultFolder.Location = new System.Drawing.Point(3, 17);
            this.lbArgInstallFromDefaultFolder.Name = "lbArgInstallFromDefaultFolder";
            this.lbArgInstallFromDefaultFolder.Size = new System.Drawing.Size(237, 17);
            this.lbArgInstallFromDefaultFolder.TabIndex = 0;
            this.lbArgInstallFromDefaultFolder.Text = "Install from Default folder:";
            this.lbArgInstallFromDefaultFolder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbIntallFromDefaultFolder
            // 
            this.cbIntallFromDefaultFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIntallFromDefaultFolder.AutoSize = true;
            this.cbIntallFromDefaultFolder.Location = new System.Drawing.Point(246, 17);
            this.cbIntallFromDefaultFolder.Name = "cbIntallFromDefaultFolder";
            this.cbIntallFromDefaultFolder.Size = new System.Drawing.Size(237, 17);
            this.cbIntallFromDefaultFolder.TabIndex = 1;
            this.cbIntallFromDefaultFolder.Text = "Just Install Packages";
            this.cbIntallFromDefaultFolder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbIntallFromDefaultFolder.UseVisualStyleBackColor = true;
            // 
            // lbCollectLatestPackagesAndInstall
            // 
            this.lbCollectLatestPackagesAndInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCollectLatestPackagesAndInstall.AutoSize = true;
            this.lbCollectLatestPackagesAndInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCollectLatestPackagesAndInstall.Location = new System.Drawing.Point(3, 61);
            this.lbCollectLatestPackagesAndInstall.Name = "lbCollectLatestPackagesAndInstall";
            this.lbCollectLatestPackagesAndInstall.Size = new System.Drawing.Size(237, 34);
            this.lbCollectLatestPackagesAndInstall.TabIndex = 2;
            this.lbCollectLatestPackagesAndInstall.Text = "Collect latest packages and Install from Default folder:";
            this.lbCollectLatestPackagesAndInstall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbCollectAndInstall
            // 
            this.cbCollectAndInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCollectAndInstall.AutoSize = true;
            this.cbCollectAndInstall.Location = new System.Drawing.Point(246, 69);
            this.cbCollectAndInstall.Name = "cbCollectAndInstall";
            this.cbCollectAndInstall.Size = new System.Drawing.Size(237, 17);
            this.cbCollectAndInstall.TabIndex = 3;
            this.cbCollectAndInstall.Text = "Collect and Install Packages";
            this.cbCollectAndInstall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbCollectAndInstall.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Set default libraries:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(246, 123);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(237, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Set default libraries ";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lbStorePackages
            // 
            this.lbStorePackages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStorePackages.AutoSize = true;
            this.lbStorePackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStorePackages.Location = new System.Drawing.Point(2, 314);
            this.lbStorePackages.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbStorePackages.Name = "lbStorePackages";
            this.lbStorePackages.Size = new System.Drawing.Size(487, 20);
            this.lbStorePackages.TabIndex = 20;
            this.lbStorePackages.Text = "Store collected packages:";
            this.lbStorePackages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbStoreCollectedPAckages
            // 
            this.cbStoreCollectedPAckages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStoreCollectedPAckages.AutoSize = true;
            this.cbStoreCollectedPAckages.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbStoreCollectedPAckages.Checked = true;
            this.cbStoreCollectedPAckages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStoreCollectedPAckages.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cbStoreCollectedPAckages.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.cbStoreCollectedPAckages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStoreCollectedPAckages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStoreCollectedPAckages.Location = new System.Drawing.Point(493, 319);
            this.cbStoreCollectedPAckages.Margin = new System.Windows.Forms.Padding(2);
            this.cbStoreCollectedPAckages.Name = "cbStoreCollectedPAckages";
            this.cbStoreCollectedPAckages.Size = new System.Drawing.Size(488, 11);
            this.cbStoreCollectedPAckages.TabIndex = 21;
            this.cbStoreCollectedPAckages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbStoreCollectedPAckages.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 436);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "OptionsForm";
            this.Text = "Options For Default Values";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.argumentsForConsoleTableLayout.ResumeLayout(false);
            this.argumentsForConsoleTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbDefaultPathForSWB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbDefaultPathForSWB;
        public System.Windows.Forms.TextBox tbDefaultPathForPackages;
        private System.Windows.Forms.Button btOptionsCancel;
        private System.Windows.Forms.Button btOptionsOK;
        private System.Windows.Forms.CheckBox cbInstallAllFoundOPs;
        private System.Windows.Forms.Label lbSetArgsForConsole;
        public System.Windows.Forms.TableLayoutPanel argumentsForConsoleTableLayout;
        public System.Windows.Forms.Label lbArgInstallFromDefaultFolder;
        private System.Windows.Forms.CheckBox cbCollectAndInstall;
        public System.Windows.Forms.Label lbCollectLatestPackagesAndInstall;
        private System.Windows.Forms.CheckBox cbIntallFromDefaultFolder;
        private System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbStoreCollectedPAckages;
        private System.Windows.Forms.Label lbStorePackages;
    }
}