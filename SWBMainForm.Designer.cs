namespace SWB_OptionPackageInstaller
{
    partial class SWBMainForm
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
            this.tbPathOfPackages = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbPathOfSWB = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.tbPageOptionPackages = new System.Windows.Forms.TabPage();
            this.tabPageOptionPackages = new System.Windows.Forms.TabPage();
            this.tabControlAllDAta = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPageOptionPackages.SuspendLayout();
            this.tabControlAllDAta.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPathOfPackages
            // 
            this.tbPathOfPackages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPathOfPackages.Location = new System.Drawing.Point(442, 5);
            this.tbPathOfPackages.Name = "tbPathOfPackages";
            this.tbPathOfPackages.Size = new System.Drawing.Size(195, 20);
            this.tbPathOfPackages.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.42938F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.57062F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPathOfPackages, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbPathOfSWB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btOK, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.47368F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.52632F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 354F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 418);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tbPathOfSWB
            // 
            this.tbPathOfSWB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPathOfSWB.Location = new System.Drawing.Point(442, 37);
            this.tbPathOfSWB.Name = "tbPathOfSWB";
            this.tbPathOfSWB.Size = new System.Drawing.Size(195, 20);
            this.tbPathOfSWB.TabIndex = 1;
            // 
            // btOK
            // 
            this.btOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOK.Location = new System.Drawing.Point(488, 220);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(104, 40);
            this.btOK.TabIndex = 3;
            this.btOK.Text = "Start";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // tbPageOptionPackages
            // 
            this.tbPageOptionPackages.Location = new System.Drawing.Point(4, 22);
            this.tbPageOptionPackages.Name = "tbPageOptionPackages";
            this.tbPageOptionPackages.Padding = new System.Windows.Forms.Padding(3);
            this.tbPageOptionPackages.Size = new System.Drawing.Size(792, 424);
            this.tbPageOptionPackages.TabIndex = 1;
            this.tbPageOptionPackages.Text = "tabPage2";
            this.tbPageOptionPackages.UseVisualStyleBackColor = true;
            // 
            // tabPageOptionPackages
            // 
            this.tabPageOptionPackages.Controls.Add(this.tableLayoutPanel1);
            this.tabPageOptionPackages.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptionPackages.Name = "tabPageOptionPackages";
            this.tabPageOptionPackages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptionPackages.Size = new System.Drawing.Size(792, 424);
            this.tabPageOptionPackages.TabIndex = 0;
            this.tabPageOptionPackages.Text = "tabPage1";
            this.tabPageOptionPackages.UseVisualStyleBackColor = true;
            // 
            // tabControlAllDAta
            // 
            this.tabControlAllDAta.Controls.Add(this.tabPageOptionPackages);
            this.tabControlAllDAta.Controls.Add(this.tbPageOptionPackages);
            this.tabControlAllDAta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAllDAta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlAllDAta.Location = new System.Drawing.Point(0, 0);
            this.tabControlAllDAta.Name = "tabControlAllDAta";
            this.tabControlAllDAta.SelectedIndex = 0;
            this.tabControlAllDAta.Size = new System.Drawing.Size(800, 450);
            this.tabControlAllDAta.TabIndex = 2;
            this.tabControlAllDAta.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Path of option packages:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(68, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Path of SWB folder:";
            // 
            // SWBMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlAllDAta);
            this.Name = "SWBMainForm";
            this.Text = "SWBSWBMainForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPageOptionPackages.ResumeLayout(false);
            this.tabControlAllDAta.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbPathOfPackages;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbPathOfSWB;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbPageOptionPackages;
        private System.Windows.Forms.TabPage tabPageOptionPackages;
        private System.Windows.Forms.TabControl tabControlAllDAta;
    }
}