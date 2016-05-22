namespace ZipTool
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
            this.button_unzip = new System.Windows.Forms.Button();
            this.progressBar_unzip = new System.Windows.Forms.ProgressBar();
            this.label_progressbar = new System.Windows.Forms.Label();
            this.button_src = new System.Windows.Forms.Button();
            this.textBox_src = new System.Windows.Forms.TextBox();
            this.textBox_tar = new System.Windows.Forms.TextBox();
            this.button_tar = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_zip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_unzip
            // 
            this.button_unzip.Location = new System.Drawing.Point(302, 155);
            this.button_unzip.Name = "button_unzip";
            this.button_unzip.Size = new System.Drawing.Size(75, 23);
            this.button_unzip.TabIndex = 0;
            this.button_unzip.Text = "UnZip";
            this.button_unzip.UseVisualStyleBackColor = true;
            this.button_unzip.Click += new System.EventHandler(this.button_unzip_Click);
            // 
            // progressBar_unzip
            // 
            this.progressBar_unzip.Location = new System.Drawing.Point(23, 208);
            this.progressBar_unzip.Name = "progressBar_unzip";
            this.progressBar_unzip.Size = new System.Drawing.Size(354, 23);
            this.progressBar_unzip.TabIndex = 1;
            // 
            // label_progressbar
            // 
            this.label_progressbar.AutoSize = true;
            this.label_progressbar.Location = new System.Drawing.Point(383, 208);
            this.label_progressbar.Name = "label_progressbar";
            this.label_progressbar.Size = new System.Drawing.Size(0, 16);
            this.label_progressbar.TabIndex = 2;
            // 
            // button_src
            // 
            this.button_src.Location = new System.Drawing.Point(302, 36);
            this.button_src.Name = "button_src";
            this.button_src.Size = new System.Drawing.Size(75, 23);
            this.button_src.TabIndex = 3;
            this.button_src.Text = "Browse";
            this.button_src.UseVisualStyleBackColor = true;
            this.button_src.Click += new System.EventHandler(this.button_src_Click);
            // 
            // textBox_src
            // 
            this.textBox_src.Location = new System.Drawing.Point(23, 36);
            this.textBox_src.Name = "textBox_src";
            this.textBox_src.Size = new System.Drawing.Size(256, 22);
            this.textBox_src.TabIndex = 4;
            // 
            // textBox_tar
            // 
            this.textBox_tar.Location = new System.Drawing.Point(23, 95);
            this.textBox_tar.Name = "textBox_tar";
            this.textBox_tar.Size = new System.Drawing.Size(256, 22);
            this.textBox_tar.TabIndex = 5;
            // 
            // button_tar
            // 
            this.button_tar.Location = new System.Drawing.Point(302, 95);
            this.button_tar.Name = "button_tar";
            this.button_tar.Size = new System.Drawing.Size(75, 23);
            this.button_tar.TabIndex = 6;
            this.button_tar.Text = "Browse";
            this.button_tar.UseVisualStyleBackColor = true;
            this.button_tar.Click += new System.EventHandler(this.button_tar_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(102, 155);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 7;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_zip
            // 
            this.button_zip.Location = new System.Drawing.Point(204, 155);
            this.button_zip.Name = "button_zip";
            this.button_zip.Size = new System.Drawing.Size(75, 23);
            this.button_zip.TabIndex = 8;
            this.button_zip.Text = "Zip";
            this.button_zip.UseVisualStyleBackColor = true;
            this.button_zip.Click += new System.EventHandler(this.button_zip_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 257);
            this.Controls.Add(this.button_zip);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_tar);
            this.Controls.Add(this.textBox_tar);
            this.Controls.Add(this.textBox_src);
            this.Controls.Add(this.button_src);
            this.Controls.Add(this.label_progressbar);
            this.Controls.Add(this.progressBar_unzip);
            this.Controls.Add(this.button_unzip);
            this.Name = "MainForm";
            this.Text = "ZipTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_unzip;
        private System.Windows.Forms.ProgressBar progressBar_unzip;
        private System.Windows.Forms.Label label_progressbar;
        private System.Windows.Forms.Button button_src;
        private System.Windows.Forms.TextBox textBox_src;
        private System.Windows.Forms.TextBox textBox_tar;
        private System.Windows.Forms.Button button_tar;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_zip;
    }
}

