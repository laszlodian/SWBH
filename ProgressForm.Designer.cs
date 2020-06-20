namespace SWB_OptionPackageInstaller
{
    partial class ProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbArtifactsProgress = new System.Windows.Forms.Label();
            this.lbProductsProgress = new System.Windows.Forms.Label();
            this.pbProducts = new System.Windows.Forms.ProgressBar();
            this.pbArtifatcts = new System.Windows.Forms.ProgressBar();
            this.lbInfo = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.88935F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.11065F));
            this.tableLayoutPanel1.Controls.Add(this.lbArtifactsProgress, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbProductsProgress, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pbProducts, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pbArtifatcts, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.progressBar2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1317, 153);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lbArtifactsProgress
            // 
            this.lbArtifactsProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbArtifactsProgress.AutoSize = true;
            this.lbArtifactsProgress.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbArtifactsProgress.Location = new System.Drawing.Point(4, 58);
            this.lbArtifactsProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbArtifactsProgress.Name = "lbArtifactsProgress";
            this.lbArtifactsProgress.Size = new System.Drawing.Size(306, 27);
            this.lbArtifactsProgress.TabIndex = 6;
            this.lbArtifactsProgress.Text = "Progress Artifacts Copy:";
            this.lbArtifactsProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbProductsProgress
            // 
            this.lbProductsProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProductsProgress.AutoSize = true;
            this.lbProductsProgress.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProductsProgress.Location = new System.Drawing.Point(4, 108);
            this.lbProductsProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbProductsProgress.Name = "lbProductsProgress";
            this.lbProductsProgress.Size = new System.Drawing.Size(306, 27);
            this.lbProductsProgress.TabIndex = 5;
            this.lbProductsProgress.Text = "Progress Products Copy:";
            this.lbProductsProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbProducts
            // 
            this.pbProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProducts.Location = new System.Drawing.Point(318, 107);
            this.pbProducts.Margin = new System.Windows.Forms.Padding(4);
            this.pbProducts.Name = "pbProducts";
            this.pbProducts.Size = new System.Drawing.Size(995, 28);
            this.pbProducts.TabIndex = 4;
            // 
            // pbArtifatcts
            // 
            this.pbArtifatcts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pbArtifatcts.Location = new System.Drawing.Point(318, 58);
            this.pbArtifatcts.Margin = new System.Windows.Forms.Padding(4);
            this.pbArtifatcts.Name = "pbArtifatcts";
            this.pbArtifatcts.Size = new System.Drawing.Size(995, 28);
            this.pbArtifatcts.TabIndex = 3;
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Arial Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfo.Location = new System.Drawing.Point(4, 0);
            this.lbInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(306, 54);
            this.lbInfo.TabIndex = 1;
            this.lbInfo.Text = "Progress SWB Copy And Unzip:";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar2
            // 
            this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar2.Location = new System.Drawing.Point(318, 13);
            this.progressBar2.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(995, 28);
            this.progressBar2.TabIndex = 2;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(1317, 153);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProgressForm";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbArtifactsProgress;
        private System.Windows.Forms.Label lbProductsProgress;
        private System.Windows.Forms.ProgressBar pbProducts;
        private System.Windows.Forms.ProgressBar pbArtifatcts;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.ProgressBar progressBar2;
    }
}