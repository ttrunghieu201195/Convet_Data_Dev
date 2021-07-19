
namespace Convert_Data
{
    partial class TestingForm
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
            this.cb_Services.Location = new System.Drawing.Point(213, 124);
            this.cb_Services.Name = "cb_Services";
            this.cb_Services.Size = new System.Drawing.Size(229, 33);
            this.cb_Services.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(91, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Service:";
            // 
            // btn_StartService
            // 
            this.btn_StartService.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StartService.Location = new System.Drawing.Point(256, 216);
            this.btn_StartService.Name = "btn_StartService";
            this.btn_StartService.Size = new System.Drawing.Size(134, 53);
            this.btn_StartService.TabIndex = 2;
            this.btn_StartService.Text = "Start";
            this.btn_StartService.UseVisualStyleBackColor = true;
            this.btn_StartService.Click += new System.EventHandler(this.btn_StartService_Click);
            // 
            // TestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 347);
            this.Controls.Add(this.btn_StartService);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_Services);
            this.Name = "TestingForm";
            this.Text = "TestingForm";
            this.Load += new System.EventHandler(this.TestingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_Services;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_StartService;
    }
}