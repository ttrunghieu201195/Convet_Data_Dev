
namespace Convert_Data
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
            this.cb_Services = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_StartService = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_Services
            // 
            this.cb_Services.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Services.FormattingEnabled = true;
            this.cb_Services.Location = new System.Drawing.Point(160, 101);
            this.cb_Services.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_Services.Name = "cb_Services";
            this.cb_Services.Size = new System.Drawing.Size(216, 28);
            this.cb_Services.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Service:";
            // 
            // btn_StartService
            // 
            this.btn_StartService.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StartService.Location = new System.Drawing.Point(192, 176);
            this.btn_StartService.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_StartService.Name = "btn_StartService";
            this.btn_StartService.Size = new System.Drawing.Size(100, 43);
            this.btn_StartService.TabIndex = 2;
            this.btn_StartService.Text = "Start";
            this.btn_StartService.UseVisualStyleBackColor = true;
            this.btn_StartService.Click += new System.EventHandler(this.btn_StartService_Click);
            // 
            // TestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 282);
            this.Controls.Add(this.btn_StartService);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_Services);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TestingForm";
            this.Text = "TestingForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_Services;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_StartService;
    }
}