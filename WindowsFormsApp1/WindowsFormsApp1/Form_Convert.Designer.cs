
namespace WindowsFormsApp1
{
    partial class form_Convert
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
            this.run_action = new System.Windows.Forms.Button();
            this.chkbox_OutGoing_Info = new System.Windows.Forms.CheckBox();
            this.chkbox_OutGoing_Flow = new System.Windows.Forms.CheckBox();
            this.chkbox_OutGoing_Log = new System.Windows.Forms.CheckBox();
            this.chkbox_InComing_Info = new System.Windows.Forms.CheckBox();
            this.chkbox_InComing_Flow = new System.Windows.Forms.CheckBox();
            this.chkbox_InComing_Log = new System.Windows.Forms.CheckBox();
            this.grpBox_DocInfo = new System.Windows.Forms.GroupBox();
            this.grpBox_Category = new System.Windows.Forms.GroupBox();
            this.chkBox_Linhvuc = new System.Windows.Forms.CheckBox();
            this.chkBox_HinhThuc = new System.Windows.Forms.CheckBox();
            this.chkBox_Confidential = new System.Windows.Forms.CheckBox();
            this.chkBox_Priority = new System.Windows.Forms.CheckBox();
            this.chkBox_Book = new System.Windows.Forms.CheckBox();
            this.txt_Schema = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Year = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Progress = new System.Windows.Forms.TextBox();
            this.grpBox_DocInfo.SuspendLayout();
            this.grpBox_Category.SuspendLayout();
            this.SuspendLayout();
            // 
            // run_action
            // 
            this.run_action.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.run_action.Location = new System.Drawing.Point(507, 406);
            this.run_action.Name = "run_action";
            this.run_action.Size = new System.Drawing.Size(73, 32);
            this.run_action.TabIndex = 0;
            this.run_action.Text = "Run";
            this.run_action.UseVisualStyleBackColor = true;
            this.run_action.Click += new System.EventHandler(this.run_Action);
            // 
            // chkbox_OutGoing_Info
            // 
            this.chkbox_OutGoing_Info.AutoSize = true;
            this.chkbox_OutGoing_Info.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_OutGoing_Info.Location = new System.Drawing.Point(20, 38);
            this.chkbox_OutGoing_Info.Name = "chkbox_OutGoing_Info";
            this.chkbox_OutGoing_Info.Size = new System.Drawing.Size(150, 23);
            this.chkbox_OutGoing_Info.TabIndex = 1;
            this.chkbox_OutGoing_Info.Text = "Thông tin văn bản đi";
            this.chkbox_OutGoing_Info.UseVisualStyleBackColor = true;
            // 
            // chkbox_OutGoing_Flow
            // 
            this.chkbox_OutGoing_Flow.AutoSize = true;
            this.chkbox_OutGoing_Flow.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_OutGoing_Flow.Location = new System.Drawing.Point(220, 38);
            this.chkbox_OutGoing_Flow.Name = "chkbox_OutGoing_Flow";
            this.chkbox_OutGoing_Flow.Size = new System.Drawing.Size(166, 23);
            this.chkbox_OutGoing_Flow.TabIndex = 2;
            this.chkbox_OutGoing_Flow.Text = "Luồng xử lý văn bản đi";
            this.chkbox_OutGoing_Flow.UseVisualStyleBackColor = true;
            // 
            // chkbox_OutGoing_Log
            // 
            this.chkbox_OutGoing_Log.AutoSize = true;
            this.chkbox_OutGoing_Log.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_OutGoing_Log.Location = new System.Drawing.Point(454, 38);
            this.chkbox_OutGoing_Log.Name = "chkbox_OutGoing_Log";
            this.chkbox_OutGoing_Log.Size = new System.Drawing.Size(152, 23);
            this.chkbox_OutGoing_Log.TabIndex = 3;
            this.chkbox_OutGoing_Log.Text = "Log xử lý văn bản đi";
            this.chkbox_OutGoing_Log.UseVisualStyleBackColor = true;
            // 
            // chkbox_InComing_Info
            // 
            this.chkbox_InComing_Info.AutoSize = true;
            this.chkbox_InComing_Info.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_InComing_Info.Location = new System.Drawing.Point(20, 94);
            this.chkbox_InComing_Info.Name = "chkbox_InComing_Info";
            this.chkbox_InComing_Info.Size = new System.Drawing.Size(161, 23);
            this.chkbox_InComing_Info.TabIndex = 4;
            this.chkbox_InComing_Info.Text = "Thông tin văn bản đến";
            this.chkbox_InComing_Info.UseVisualStyleBackColor = true;
            // 
            // chkbox_InComing_Flow
            // 
            this.chkbox_InComing_Flow.AutoSize = true;
            this.chkbox_InComing_Flow.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_InComing_Flow.Location = new System.Drawing.Point(220, 94);
            this.chkbox_InComing_Flow.Name = "chkbox_InComing_Flow";
            this.chkbox_InComing_Flow.Size = new System.Drawing.Size(177, 23);
            this.chkbox_InComing_Flow.TabIndex = 5;
            this.chkbox_InComing_Flow.Text = "Luồng xử lý văn bản đến";
            this.chkbox_InComing_Flow.UseVisualStyleBackColor = true;
            // 
            // chkbox_InComing_Log
            // 
            this.chkbox_InComing_Log.AutoSize = true;
            this.chkbox_InComing_Log.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_InComing_Log.Location = new System.Drawing.Point(454, 94);
            this.chkbox_InComing_Log.Name = "chkbox_InComing_Log";
            this.chkbox_InComing_Log.Size = new System.Drawing.Size(163, 23);
            this.chkbox_InComing_Log.TabIndex = 6;
            this.chkbox_InComing_Log.Text = "Log xử lý văn bản đến";
            this.chkbox_InComing_Log.UseVisualStyleBackColor = true;
            // 
            // grpBox_DocInfo
            // 
            this.grpBox_DocInfo.Controls.Add(this.chkbox_OutGoing_Info);
            this.grpBox_DocInfo.Controls.Add(this.chkbox_InComing_Log);
            this.grpBox_DocInfo.Controls.Add(this.chkbox_OutGoing_Flow);
            this.grpBox_DocInfo.Controls.Add(this.chkbox_InComing_Flow);
            this.grpBox_DocInfo.Controls.Add(this.chkbox_OutGoing_Log);
            this.grpBox_DocInfo.Controls.Add(this.chkbox_InComing_Info);
            this.grpBox_DocInfo.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBox_DocInfo.Location = new System.Drawing.Point(53, 202);
            this.grpBox_DocInfo.Name = "grpBox_DocInfo";
            this.grpBox_DocInfo.Size = new System.Drawing.Size(643, 132);
            this.grpBox_DocInfo.TabIndex = 7;
            this.grpBox_DocInfo.TabStop = false;
            this.grpBox_DocInfo.Text = "Thông tin văn bản";
            // 
            // grpBox_Category
            // 
            this.grpBox_Category.Controls.Add(this.chkBox_Linhvuc);
            this.grpBox_Category.Controls.Add(this.chkBox_HinhThuc);
            this.grpBox_Category.Controls.Add(this.chkBox_Confidential);
            this.grpBox_Category.Controls.Add(this.chkBox_Priority);
            this.grpBox_Category.Controls.Add(this.chkBox_Book);
            this.grpBox_Category.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBox_Category.Location = new System.Drawing.Point(53, 72);
            this.grpBox_Category.Name = "grpBox_Category";
            this.grpBox_Category.Size = new System.Drawing.Size(643, 114);
            this.grpBox_Category.TabIndex = 8;
            this.grpBox_Category.TabStop = false;
            this.grpBox_Category.Text = "Danh mục";
            // 
            // chkBox_Linhvuc
            // 
            this.chkBox_Linhvuc.AutoSize = true;
            this.chkBox_Linhvuc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_Linhvuc.Location = new System.Drawing.Point(220, 77);
            this.chkBox_Linhvuc.Name = "chkBox_Linhvuc";
            this.chkBox_Linhvuc.Size = new System.Drawing.Size(81, 23);
            this.chkBox_Linhvuc.TabIndex = 4;
            this.chkBox_Linhvuc.Text = "Lĩnh vực";
            this.chkBox_Linhvuc.UseVisualStyleBackColor = true;
            // 
            // chkBox_HinhThuc
            // 
            this.chkBox_HinhThuc.AutoSize = true;
            this.chkBox_HinhThuc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_HinhThuc.Location = new System.Drawing.Point(20, 80);
            this.chkBox_HinhThuc.Name = "chkBox_HinhThuc";
            this.chkBox_HinhThuc.Size = new System.Drawing.Size(87, 23);
            this.chkBox_HinhThuc.TabIndex = 3;
            this.chkBox_HinhThuc.Text = "Hình thức";
            this.chkBox_HinhThuc.UseVisualStyleBackColor = true;
            // 
            // chkBox_Confidential
            // 
            this.chkBox_Confidential.AutoSize = true;
            this.chkBox_Confidential.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_Confidential.Location = new System.Drawing.Point(454, 30);
            this.chkBox_Confidential.Name = "chkBox_Confidential";
            this.chkBox_Confidential.Size = new System.Drawing.Size(73, 23);
            this.chkBox_Confidential.TabIndex = 2;
            this.chkBox_Confidential.Text = "Độ mật";
            this.chkBox_Confidential.UseVisualStyleBackColor = true;
            // 
            // chkBox_Priority
            // 
            this.chkBox_Priority.AutoSize = true;
            this.chkBox_Priority.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_Priority.Location = new System.Drawing.Point(220, 30);
            this.chkBox_Priority.Name = "chkBox_Priority";
            this.chkBox_Priority.Size = new System.Drawing.Size(80, 23);
            this.chkBox_Priority.TabIndex = 1;
            this.chkBox_Priority.Text = "Độ khẩn";
            this.chkBox_Priority.UseVisualStyleBackColor = true;
            // 
            // chkBox_Book
            // 
            this.chkBox_Book.AutoSize = true;
            this.chkBox_Book.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_Book.Location = new System.Drawing.Point(20, 30);
            this.chkBox_Book.Name = "chkBox_Book";
            this.chkBox_Book.Size = new System.Drawing.Size(96, 23);
            this.chkBox_Book.TabIndex = 0;
            this.chkBox_Book.Text = "Sổ văn bản";
            this.chkBox_Book.UseVisualStyleBackColor = true;
            // 
            // txt_Schema
            // 
            this.txt_Schema.Location = new System.Drawing.Point(135, 34);
            this.txt_Schema.Name = "txt_Schema";
            this.txt_Schema.Size = new System.Drawing.Size(151, 20);
            this.txt_Schema.TabIndex = 9;
            this.txt_Schema.Text = "QLVB_BLU_SKHDT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "Schema";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(386, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Năm";
            // 
            // txt_Year
            // 
            this.txt_Year.Location = new System.Drawing.Point(446, 36);
            this.txt_Year.Name = "txt_Year";
            this.txt_Year.Size = new System.Drawing.Size(100, 20);
            this.txt_Year.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 372);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 19);
            this.label3.TabIndex = 13;
            this.label3.Text = "Progress";
            // 
            // txt_Progress
            // 
            this.txt_Progress.Location = new System.Drawing.Point(135, 373);
            this.txt_Progress.Name = "txt_Progress";
            this.txt_Progress.Size = new System.Drawing.Size(370, 20);
            this.txt_Progress.TabIndex = 14;
            // 
            // form_Convert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_Progress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Year);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Schema);
            this.Controls.Add(this.grpBox_Category);
            this.Controls.Add(this.grpBox_DocInfo);
            this.Controls.Add(this.run_action);
            this.Name = "form_Convert";
            this.Text = "Convert Data BLU";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpBox_DocInfo.ResumeLayout(false);
            this.grpBox_DocInfo.PerformLayout();
            this.grpBox_Category.ResumeLayout(false);
            this.grpBox_Category.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button run_action;
        private System.Windows.Forms.CheckBox chkbox_OutGoing_Info;
        private System.Windows.Forms.CheckBox chkbox_OutGoing_Flow;
        private System.Windows.Forms.CheckBox chkbox_OutGoing_Log;
        private System.Windows.Forms.CheckBox chkbox_InComing_Info;
        private System.Windows.Forms.CheckBox chkbox_InComing_Flow;
        private System.Windows.Forms.CheckBox chkbox_InComing_Log;
        private System.Windows.Forms.GroupBox grpBox_DocInfo;
        private System.Windows.Forms.GroupBox grpBox_Category;
        private System.Windows.Forms.CheckBox chkBox_Book;
        private System.Windows.Forms.CheckBox chkBox_Priority;
        private System.Windows.Forms.CheckBox chkBox_Confidential;
        private System.Windows.Forms.CheckBox chkBox_Linhvuc;
        private System.Windows.Forms.CheckBox chkBox_HinhThuc;
        private System.Windows.Forms.TextBox txt_Schema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Year;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Progress;
    }
}

