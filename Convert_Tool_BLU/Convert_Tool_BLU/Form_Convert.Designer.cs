
namespace Convert_Data
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
            this.txt_StartYear = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Progress = new System.Windows.Forms.TextBox();
            this.chkbox_DeleteDoc = new System.Windows.Forms.CheckBox();
            this.chkbox_deleteCategory = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_EndYear = new System.Windows.Forms.TextBox();
            this.cbBox_Donvi = new System.Windows.Forms.ComboBox();
            this.btn_OpenVerify = new System.Windows.Forms.Button();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.grpBox_DocInfo.SuspendLayout();
            this.grpBox_Category.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // run_action
            // 
            this.run_action.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.run_action.Location = new System.Drawing.Point(740, 747);
            this.run_action.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkbox_OutGoing_Info.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkbox_OutGoing_Flow.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkbox_OutGoing_Log.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkbox_InComing_Info.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkbox_InComing_Flow.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkbox_InComing_Log.Margin = new System.Windows.Forms.Padding(4);
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
            this.grpBox_DocInfo.Location = new System.Drawing.Point(135, 519);
            this.grpBox_DocInfo.Margin = new System.Windows.Forms.Padding(4);
            this.grpBox_DocInfo.Name = "grpBox_DocInfo";
            this.grpBox_DocInfo.Padding = new System.Windows.Forms.Padding(4);
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
            this.grpBox_Category.Location = new System.Drawing.Point(135, 360);
            this.grpBox_Category.Margin = new System.Windows.Forms.Padding(4);
            this.grpBox_Category.Name = "grpBox_Category";
            this.grpBox_Category.Padding = new System.Windows.Forms.Padding(4);
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
            this.chkBox_Linhvuc.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkBox_HinhThuc.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkBox_Confidential.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkBox_Priority.Margin = new System.Windows.Forms.Padding(4);
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
            this.chkBox_Book.Margin = new System.Windows.Forms.Padding(4);
            this.chkBox_Book.Name = "chkBox_Book";
            this.chkBox_Book.Size = new System.Drawing.Size(119, 26);
            this.chkBox_Book.TabIndex = 0;
            this.chkBox_Book.Text = "Sổ văn bản";
            this.chkBox_Book.UseVisualStyleBackColor = true;
            // 
            // txt_Schema
            // 
            this.txt_Schema.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Schema.Location = new System.Drawing.Point(256, 188);
            this.txt_Schema.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Schema.Multiline = true;
            this.txt_Schema.Name = "txt_Schema";
            this.txt_Schema.Size = new System.Drawing.Size(338, 33);
            this.txt_Schema.TabIndex = 9;
            this.txt_Schema.Text = "QLVB_BLU_TINHBACLIEU";
            this.txt_Schema.TextChanged += new System.EventHandler(this.txt_Schema_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(131, 192);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "Schema";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(426, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 22);
            this.label2.TabIndex = 11;
            this.label2.Text = "Năm";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txt_StartYear
            // 
            this.txt_StartYear.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_StartYear.Location = new System.Drawing.Point(493, 51);
            this.txt_StartYear.Margin = new System.Windows.Forms.Padding(4);
            this.txt_StartYear.Multiline = true;
            this.txt_StartYear.Name = "txt_StartYear";
            this.txt_StartYear.Size = new System.Drawing.Size(59, 29);
            this.txt_StartYear.TabIndex = 12;
            this.txt_StartYear.Text = "2011";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(132, 700);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 22);
            this.label3.TabIndex = 15;
            this.label3.Text = "Progress";
            // 
            // txt_Progress
            // 
            this.txt_Progress.Location = new System.Drawing.Point(231, 700);
            this.txt_Progress.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Progress.Multiline = true;
            this.txt_Progress.Name = "txt_Progress";
            this.txt_Progress.Size = new System.Drawing.Size(606, 33);
            this.txt_Progress.TabIndex = 14;
            // 
            // chkbox_DeleteDoc
            // 
            this.chkbox_DeleteDoc.AutoSize = true;
            this.chkbox_DeleteDoc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_DeleteDoc.Location = new System.Drawing.Point(377, 61);
            this.chkbox_DeleteDoc.Name = "chkbox_DeleteDoc";
            this.chkbox_DeleteDoc.Size = new System.Drawing.Size(210, 26);
            this.chkbox_DeleteDoc.TabIndex = 15;
            this.chkbox_DeleteDoc.Text = "Xóa Thông tin văn bản";
            this.chkbox_DeleteDoc.UseVisualStyleBackColor = true;
            // 
            // chkbox_deleteCategory
            // 
            this.chkbox_deleteCategory.AutoSize = true;
            this.chkbox_deleteCategory.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_deleteCategory.Location = new System.Drawing.Point(34, 61);
            this.chkbox_deleteCategory.Name = "chkbox_deleteCategory";
            this.chkbox_deleteCategory.Size = new System.Drawing.Size(151, 26);
            this.chkbox_deleteCategory.TabIndex = 16;
            this.chkbox_deleteCategory.Text = "Xóa Danh Mục";
            this.chkbox_deleteCategory.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkbox_deleteCategory);
            this.groupBox1.Controls.Add(this.chkbox_DeleteDoc);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(135, 244);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(857, 100);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Xóa dữ liệu cũ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 22);
            this.label4.TabIndex = 18;
            this.label4.Text = "Đơn vị";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txt_EndYear);
            this.groupBox2.Controls.Add(this.cbBox_Donvi);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txt_StartYear);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(135, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(857, 114);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dữ liệu đầu vào";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(560, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 22);
            this.label5.TabIndex = 23;
            this.label5.Text = "-";
            // 
            // txt_EndYear
            // 
            this.txt_EndYear.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EndYear.Location = new System.Drawing.Point(585, 51);
            this.txt_EndYear.Margin = new System.Windows.Forms.Padding(4);
            this.txt_EndYear.Multiline = true;
            this.txt_EndYear.Name = "txt_EndYear";
            this.txt_EndYear.Size = new System.Drawing.Size(66, 29);
            this.txt_EndYear.TabIndex = 22;
            this.txt_EndYear.Text = "2019";
            // 
            // cbBox_Donvi
            // 
            this.cbBox_Donvi.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBox_Donvi.FormattingEnabled = true;
            this.cbBox_Donvi.ItemHeight = 22;
            this.cbBox_Donvi.Location = new System.Drawing.Point(121, 52);
            this.cbBox_Donvi.Name = "cbBox_Donvi";
            this.cbBox_Donvi.Size = new System.Drawing.Size(237, 30);
            this.cbBox_Donvi.TabIndex = 20;
            this.cbBox_Donvi.SelectedIndexChanged += new System.EventHandler(this.cbBox_Donvi_SelectedIndexChanged);
            // 
            // btn_OpenVerify
            // 
            this.btn_OpenVerify.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenVerify.Location = new System.Drawing.Point(383, 747);
            this.btn_OpenVerify.Name = "btn_OpenVerify";
            this.btn_OpenVerify.Size = new System.Drawing.Size(110, 39);
            this.btn_OpenVerify.TabIndex = 20;
            this.btn_OpenVerify.Text = "Checking";
            this.btn_OpenVerify.UseVisualStyleBackColor = true;
            this.btn_OpenVerify.Click += new System.EventHandler(this.btn_OpenVerifyForm);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(100, 27);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 23);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(565, 747);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(97, 39);
            this.btn_Delete.TabIndex = 21;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            // 
            // form_Convert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 797);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_OpenVerify);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_Progress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Schema);
            this.Controls.Add(this.grpBox_Category);
            this.Controls.Add(this.grpBox_DocInfo);
            this.Controls.Add(this.run_action);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "form_Convert";
            this.Text = "Convert Data BLU";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpBox_DocInfo.ResumeLayout(false);
            this.grpBox_DocInfo.PerformLayout();
            this.grpBox_Category.ResumeLayout(false);
            this.grpBox_Category.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.TextBox txt_StartYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Progress;
        private System.Windows.Forms.CheckBox chkbox_DeleteDoc;
        private System.Windows.Forms.CheckBox chkbox_deleteCategory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbBox_Donvi;
        private System.Windows.Forms.Button btn_OpenVerify;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_EndYear;
        private System.Windows.Forms.Button btn_Delete;
    }
}

