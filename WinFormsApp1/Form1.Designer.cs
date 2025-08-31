namespace WinFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtMpdUrl = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblMpdUrl = new System.Windows.Forms.Label();
            this.btnSelectOutput = new System.Windows.Forms.Button();
            this.lblOutputDir = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMpdUrl
            // 
            this.txtMpdUrl.Location = new System.Drawing.Point(12, 38);
            this.txtMpdUrl.Name = "txtMpdUrl";
            this.txtMpdUrl.Size = new System.Drawing.Size(460, 20);
            this.txtMpdUrl.TabIndex = 0;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(478, 36);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(100, 23);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 90);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(566, 274);
            this.txtOutput.TabIndex = 2;
            // 
            // lblMpdUrl
            // 
            this.lblMpdUrl.AutoSize = true;
            this.lblMpdUrl.Location = new System.Drawing.Point(12, 19);
            this.lblMpdUrl.Name = "lblMpdUrl";
            this.lblMpdUrl.Size = new System.Drawing.Size(55, 13);
            this.lblMpdUrl.TabIndex = 3;
            this.lblMpdUrl.Text = "MPD URL:";
            // 
            // btnSelectOutput
            // 
            this.btnSelectOutput.Location = new System.Drawing.Point(478, 61);
            this.btnSelectOutput.Name = "btnSelectOutput";
            this.btnSelectOutput.Size = new System.Drawing.Size(100, 23);
            this.btnSelectOutput.TabIndex = 4;
            this.btnSelectOutput.Text = "Select Folder";
            this.btnSelectOutput.UseVisualStyleBackColor = true;
            this.btnSelectOutput.Click += new System.EventHandler(this.btnSelectOutput_Click);
            // 
            // lblOutputDir
            // 
            this.lblOutputDir.AutoSize = true;
            this.lblOutputDir.Location = new System.Drawing.Point(12, 66);
            this.lblOutputDir.Name = "lblOutputDir";
            this.lblOutputDir.Size = new System.Drawing.Size(80, 13);
            this.lblOutputDir.TabIndex = 5;
            this.lblOutputDir.Text = "Output: [path]";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 370);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(80, 13);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.Text = "Progress: 0%";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 392);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblOutputDir);
            this.Controls.Add(this.btnSelectOutput);
            this.Controls.Add(this.lblMpdUrl);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.txtMpdUrl);
            this.Name = "Form1";
            this.Text = "Video Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtMpdUrl;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblMpdUrl;
        private System.Windows.Forms.Button btnSelectOutput;
        private System.Windows.Forms.Label lblOutputDir;
        private System.Windows.Forms.Label lblProgress;
    }
}