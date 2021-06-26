
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
            this.chkbox_DeleteDoc = new System.Windows.Forms.CheckBox();
            this.chkbox_deleteCategory = new System.Windows.Forms.CheckBox();
            this.grpBox_DocInfo.SuspendLayout();
            this.grpBox_Category.SuspendLayout();
            this.SuspendLayout();
            // 
            // run_action
            // 
            this.run_action.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.run_action.Location = new System.Drawing.Point(720, 670);
            this.run_action.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.run_action.Name = "run_action";
            this.run_action.Size = new System.Drawing.Size(97, 39);
            this.run_action.TabIndex = 0;
            this.run_action.Text = "Run";
            this.run_action.UseVisualStyleBackColor = true;
            this.run_action.Click += new System.EventHandler(this.run_Action);
            // 
            // chkbox_OutGoing_Info
            // 
            this.chkbox_OutGoing_Info.AutoSize = true;
            this.chkbox_OutGoing_Info.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_OutGoing_Info.Location = new System.Drawing.Point(27, 47);
            this.chkbox_OutGoing_Info.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkbox_OutGoing_Info.Name = "chkbox_OutGoing_Info";
            this.chkbox_OutGoing_Info.Size = new System.Drawing.Size(193, 26);
            this.chkbox_OutGoing_Info.TabIndex = 1;
            this.chkbox_OutGoing_Info.Text = "Thông tin văn bản đi";
            this.chkbox_OutGoing_Info.UseVisualStyleBackColor = true;
            // 
            // chkbox_OutGoing_Flow
            // 
            this.chkbox_OutGoing_Flow.AutoSize = true;
            this.chkbox_OutGoing_Flow.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_OutGoing_Flow.Location = new System.Drawing.Point(293, 47);
            this.chkbox_OutGoing_Flow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkbox_OutGoing_Flow.Name = "chkbox_OutGoing_Flow";
            this.chkbox_OutGoing_Flow.Size = new System.Drawing.Size(213, 26);
            this.chkbox_OutGoing_Flow.TabIndex = 2;
            this.chkbox_OutGoing_Flow.Text = "Luồng xử lý văn bản đi";
            this.chkbox_OutGoing_Flow.UseVisualStyleBackColor = true;
            // 
            // chkbox_OutGoing_Log
            // 
            this.chkbox_OutGoing_Log.AutoSize = true;
            this.chkbox_OutGoing_Log.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_OutGoing_Log.Location = new System.Drawing.Point(605, 47);
            this.chkbox_OutGoing_Log.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkbox_OutGoing_Log.Name = "chkbox_OutGoing_Log";
            this.chkbox_OutGoing_Log.Size = new System.Drawing.Size(195, 26);
            this.chkbox_OutGoing_Log.TabIndex = 3;
            this.chkbox_OutGoing_Log.Text = "Log xử lý văn bản đi";
            this.chkbox_OutGoing_Log.UseVisualStyleBackColor = true;
            // 
            // chkbox_InComing_Info
            // 
            this.chkbox_InComing_Info.AutoSize = true;
            this.chkbox_InComing_Info.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_InComing_Info.Location = new System.Drawing.Point(27, 116);
            this.chkbox_InComing_Info.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkbox_InComing_Info.Name = "chkbox_InComing_Info";
            this.chkbox_InComing_Info.Size = new System.Drawing.Size(205, 26);
            this.chkbox_InComing_Info.TabIndex = 4;
            this.chkbox_InComing_Info.Text = "Thông tin văn bản đến";
            this.chkbox_InComing_Info.UseVisualStyleBackColor = true;
            // 
            // chkbox_InComing_Flow
            // 
            this.chkbox_InComing_Flow.AutoSize = true;
            this.chkbox_InComing_Flow.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_InComing_Flow.Location = new System.Drawing.Point(293, 116);
            this.chkbox_InComing_Flow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkbox_InComing_Flow.Name = "chkbox_InComing_Flow";
            this.chkbox_InComing_Flow.Size = new System.Drawing.Size(225, 26);
            this.chkbox_InComing_Flow.TabIndex = 5;
            this.chkbox_InComing_Flow.Text = "Luồng xử lý văn bản đến";
            this.chkbox_InComing_Flow.UseVisualStyleBackColor = true;
            // 
            // chkbox_InComing_Log
            // 
            this.chkbox_InComing_Log.AutoSize = true;
            this.chkbox_InComing_Log.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_InComing_Log.Location = new System.Drawing.Point(605, 116);
            this.chkbox_InComing_Log.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkbox_InComing_Log.Name = "chkbox_InComing_Log";
            this.chkbox_InComing_Log.Size = new System.Drawing.Size(207, 26);
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
            this.grpBox_DocInfo.Location = new System.Drawing.Point(115, 419);
            this.grpBox_DocInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpBox_DocInfo.Name = "grpBox_DocInfo";
            this.grpBox_DocInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpBox_DocInfo.Size = new System.Drawing.Size(857, 162);
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
            this.grpBox_Category.Location = new System.Drawing.Point(115, 259);
            this.grpBox_Category.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpBox_Category.Name = "grpBox_Category";
            this.grpBox_Category.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpBox_Category.Size = new System.Drawing.Size(857, 140);
            this.grpBox_Category.TabIndex = 8;
            this.grpBox_Category.TabStop = false;
            this.grpBox_Category.Text = "Danh mục";
            // 
            // chkBox_Linhvuc
            // 
            this.chkBox_Linhvuc.AutoSize = true;
            this.chkBox_Linhvuc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_Linhvuc.Location = new System.Drawing.Point(293, 95);
            this.chkBox_Linhvuc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBox_Linhvuc.Name = "chkBox_Linhvuc";
            this.chkBox_Linhvuc.Size = new System.Drawing.Size(102, 26);
            this.chkBox_Linhvuc.TabIndex = 4;
            this.chkBox_Linhvuc.Text = "Lĩnh vực";
            this.chkBox_Linhvuc.UseVisualStyleBackColor = true;
            // 
            // chkBox_HinhThuc
            // 
            this.chkBox_HinhThuc.AutoSize = true;
            this.chkBox_HinhThuc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_HinhThuc.Location = new System.Drawing.Point(27, 98);
            this.chkBox_HinhThuc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBox_HinhThuc.Name = "chkBox_HinhThuc";
            this.chkBox_HinhThuc.Size = new System.Drawing.Size(109, 26);
            this.chkBox_HinhThuc.TabIndex = 3;
            this.chkBox_HinhThuc.Text = "Hình thức";
            this.chkBox_HinhThuc.UseVisualStyleBackColor = true;
            // 
            // chkBox_Confidential
            // 
            this.chkBox_Confidential.AutoSize = true;
            this.chkBox_Confidential.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_Confidential.Location = new System.Drawing.Point(605, 37);
            this.chkBox_Confidential.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBox_Confidential.Name = "chkBox_Confidential";
            this.chkBox_Confidential.Size = new System.Drawing.Size(89, 26);
            this.chkBox_Confidential.TabIndex = 2;
            this.chkBox_Confidential.Text = "Độ mật";
            this.chkBox_Confidential.UseVisualStyleBackColor = true;
            // 
            // chkBox_Priority
            // 
            this.chkBox_Priority.AutoSize = true;
            this.chkBox_Priority.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_Priority.Location = new System.Drawing.Point(293, 37);
            this.chkBox_Priority.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBox_Priority.Name = "chkBox_Priority";
            this.chkBox_Priority.Size = new System.Drawing.Size(97, 26);
            this.chkBox_Priority.TabIndex = 1;
            this.chkBox_Priority.Text = "Độ khẩn";
            this.chkBox_Priority.UseVisualStyleBackColor = true;
            // 
            // chkBox_Book
            // 
            this.chkBox_Book.AutoSize = true;
            this.chkBox_Book.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBox_Book.Location = new System.Drawing.Point(27, 37);
            this.chkBox_Book.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBox_Book.Name = "chkBox_Book";
            this.chkBox_Book.Size = new System.Drawing.Size(119, 26);
            this.chkBox_Book.TabIndex = 0;
            this.chkBox_Book.Text = "Sổ văn bản";
            this.chkBox_Book.UseVisualStyleBackColor = true;
            // 
            // txt_Schema
            // 
            this.txt_Schema.Location = new System.Drawing.Point(224, 212);
            this.txt_Schema.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Schema.Name = "txt_Schema";
            this.txt_Schema.Size = new System.Drawing.Size(200, 22);
            this.txt_Schema.TabIndex = 9;
            this.txt_Schema.Text = "QLVB_BLU_SKHDT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(124, 211);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 22);
            this.label1.TabIndex = 10;
            this.label1.Text = "Schema";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(559, 213);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 22);
            this.label2.TabIndex = 11;
            this.label2.Text = "Năm";
            // 
            // txt_Year
            // 
            this.txt_Year.Location = new System.Drawing.Point(639, 214);
            this.txt_Year.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Year.Name = "txt_Year";
            this.txt_Year.Size = new System.Drawing.Size(132, 22);
            this.txt_Year.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(124, 628);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 22);
            this.label3.TabIndex = 13;
            this.label3.Text = "Progress";
            // 
            // txt_Progress
            // 
            this.txt_Progress.Location = new System.Drawing.Point(224, 629);
            this.txt_Progress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Progress.Name = "txt_Progress";
            this.txt_Progress.Size = new System.Drawing.Size(492, 22);
            this.txt_Progress.TabIndex = 14;
            // 
            // chkbox_DeleteDoc
            // 
            this.chkbox_DeleteDoc.AutoSize = true;
            this.chkbox_DeleteDoc.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_DeleteDoc.Location = new System.Drawing.Point(552, 71);
            this.chkbox_DeleteDoc.Name = "chkbox_DeleteDoc";
            this.chkbox_DeleteDoc.Size = new System.Drawing.Size(265, 33);
            this.chkbox_DeleteDoc.TabIndex = 15;
            this.chkbox_DeleteDoc.Text = "Xóa Thông tin văn bản";
            this.chkbox_DeleteDoc.UseVisualStyleBackColor = true;
            // 
            // chkbox_deleteCategory
            // 
            this.chkbox_deleteCategory.AutoSize = true;
            this.chkbox_deleteCategory.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_deleteCategory.Location = new System.Drawing.Point(115, 71);
            this.chkbox_deleteCategory.Name = "chkbox_deleteCategory";
            this.chkbox_deleteCategory.Size = new System.Drawing.Size(190, 33);
            this.chkbox_deleteCategory.TabIndex = 16;
            this.chkbox_deleteCategory.Text = "Xóa Danh Mục";
            this.chkbox_deleteCategory.UseVisualStyleBackColor = true;
            // 
            // form_Convert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 752);
            this.Controls.Add(this.chkbox_deleteCategory);
            this.Controls.Add(this.chkbox_DeleteDoc);
            this.Controls.Add(this.txt_Progress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Year);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Schema);
            this.Controls.Add(this.grpBox_Category);
            this.Controls.Add(this.grpBox_DocInfo);
            this.Controls.Add(this.run_action);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.CheckBox chkbox_DeleteDoc;
        private System.Windows.Forms.CheckBox chkbox_deleteCategory;
    }
}

