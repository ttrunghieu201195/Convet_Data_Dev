
namespace Convert_Data
{
    partial class CleanProgress
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
            this.txt_clean_Progress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_clean_Progress
            // 
            this.txt_clean_Progress.Location = new System.Drawing.Point(57, 30);
            this.txt_clean_Progress.Margin = new System.Windows.Forms.Padding(4);
            this.txt_clean_Progress.Multiline = true;
            this.txt_clean_Progress.Name = "txt_clean_Progress";
            this.txt_clean_Progress.Size = new System.Drawing.Size(431, 42);
            this.txt_clean_Progress.TabIndex = 14;
            this.txt_clean_Progress.TextChanged += new System.EventHandler(this.txt_Progress_TextChanged);
            // 
            // CleanProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 106);
            this.Controls.Add(this.txt_clean_Progress);
            this.Name = "CleanProgress";
            this.Text = "CleanProgress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_clean_Progress;
    }
}