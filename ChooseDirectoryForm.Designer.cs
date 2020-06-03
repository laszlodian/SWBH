namespace SWB_OptionPackageInstaller
{
    partial class ChooseDirectoryForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAllBuildDirectories = new System.Windows.Forms.ComboBox();
            this.btChooseDirectory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btChooseDirectory);
            this.splitContainer1.Panel2.Controls.Add(this.cbAllBuildDirectories);
            this.splitContainer1.Size = new System.Drawing.Size(731, 76);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose another build directory:";
            // 
            // cbAllBuildDirectories
            // 
            this.cbAllBuildDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAllBuildDirectories.FormattingEnabled = true;
            this.cbAllBuildDirectories.Location = new System.Drawing.Point(30, 17);
            this.cbAllBuildDirectories.Name = "cbAllBuildDirectories";
            this.cbAllBuildDirectories.Size = new System.Drawing.Size(351, 21);
            this.cbAllBuildDirectories.TabIndex = 0;
            // 
            // btChooseDirectory
            // 
            this.btChooseDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btChooseDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btChooseDirectory.Location = new System.Drawing.Point(83, 44);
            this.btChooseDirectory.Name = "btChooseDirectory";
            this.btChooseDirectory.Size = new System.Drawing.Size(229, 26);
            this.btChooseDirectory.TabIndex = 1;
            this.btChooseDirectory.Text = "Use this build directory";
            this.btChooseDirectory.UseVisualStyleBackColor = true;
            this.btChooseDirectory.Click += new System.EventHandler(this.btChooseDirectory_Click);
            // 
            // ChooseDirectoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 76);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "ChooseDirectoryForm";
            this.Text = "Choose Build Directory";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAllBuildDirectories;
        private System.Windows.Forms.Button btChooseDirectory;
    }
}