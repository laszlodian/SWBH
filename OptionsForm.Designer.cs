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
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btOptionsCancel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btOptionsOK, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbDefaultPathForPackages, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbDefaultPathForSWB, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbDefaultPathForSWB, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbInstallAllFoundOPs, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1311, 536);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btOptionsCancel
            // 
            this.btOptionsCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btOptionsCancel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOptionsCancel.Location = new System.Drawing.Point(658, 349);
            this.btOptionsCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btOptionsCancel.Name = "btOptionsCancel";
            this.btOptionsCancel.Size = new System.Drawing.Size(650, 46);
            this.btOptionsCancel.TabIndex = 16;
            this.btOptionsCancel.Text = "Cancel";
            this.btOptionsCancel.UseVisualStyleBackColor = true;
            this.btOptionsCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOptionsOK
            // 
            this.btOptionsOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btOptionsOK.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOptionsOK.Location = new System.Drawing.Point(3, 349);
            this.btOptionsOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btOptionsOK.Name = "btOptionsOK";
            this.btOptionsOK.Size = new System.Drawing.Size(649, 46);
            this.btOptionsOK.TabIndex = 15;
            this.btOptionsOK.Text = "Save";
            this.btOptionsOK.UseVisualStyleBackColor = true;
            this.btOptionsOK.Click += new System.EventHandler(this.btOptionsOK_Click);
            // 
            // tbDefaultPathForPackages
            // 
            this.tbDefaultPathForPackages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDefaultPathForPackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDefaultPathForPackages.Location = new System.Drawing.Point(658, 84);
            this.tbDefaultPathForPackages.Name = "tbDefaultPathForPackages";
            this.tbDefaultPathForPackages.Size = new System.Drawing.Size(650, 30);
            this.tbDefaultPathForPackages.TabIndex = 5;
            this.tbDefaultPathForPackages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbDefaultPathForSWB
            // 
            this.lbDefaultPathForSWB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDefaultPathForSWB.AutoSize = true;
            this.lbDefaultPathForSWB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDefaultPathForSWB.Location = new System.Drawing.Point(3, 20);
            this.lbDefaultPathForSWB.Name = "lbDefaultPathForSWB";
            this.lbDefaultPathForSWB.Size = new System.Drawing.Size(649, 25);
            this.lbDefaultPathForSWB.TabIndex = 0;
            this.lbDefaultPathForSWB.Text = "Default Path for SWB folders:";
            this.lbDefaultPathForSWB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(649, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Default Path for Option Packages folder:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(649, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Install all found option package:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbDefaultPathForSWB
            // 
            this.tbDefaultPathForSWB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDefaultPathForSWB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDefaultPathForSWB.Location = new System.Drawing.Point(658, 17);
            this.tbDefaultPathForSWB.Name = "tbDefaultPathForSWB";
            this.tbDefaultPathForSWB.Size = new System.Drawing.Size(650, 30);
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
            this.cbInstallAllFoundOPs.Location = new System.Drawing.Point(658, 164);
            this.cbInstallAllFoundOPs.Name = "cbInstallAllFoundOPs";
            this.cbInstallAllFoundOPs.Size = new System.Drawing.Size(650, 13);
            this.cbInstallAllFoundOPs.TabIndex = 17;
            this.cbInstallAllFoundOPs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbInstallAllFoundOPs.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 536);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "OptionsForm";
            this.Text = "Options For Default Values";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
    }
}