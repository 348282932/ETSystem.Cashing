namespace ETSystem.Cashing
{
    partial class CashingFileConvertForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashingFileConvertForm));
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.btn_ConvertFile = new System.Windows.Forms.Button();
            this.lbl_FliePath = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_GrammageStatus = new System.Windows.Forms.Label();
            this.btn_SetGrammage = new System.Windows.Forms.Button();
            this.cbx_Style = new System.Windows.Forms.ComboBox();
            this.cbx_ReservedBit = new System.Windows.Forms.ComboBox();
            this.lbl_ReservedBits = new System.Windows.Forms.Label();
            this.lbl_Format = new System.Windows.Forms.Label();
            this.tbx_OutputPath = new System.Windows.Forms.TextBox();
            this.tbx_FileName = new System.Windows.Forms.TextBox();
            this.btn_OpenPath = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.Location = new System.Drawing.Point(17, 30);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(66, 23);
            this.btn_OpenFile.TabIndex = 0;
            this.btn_OpenFile.Text = "选取文件";
            this.btn_OpenFile.UseVisualStyleBackColor = true;
            this.btn_OpenFile.Click += new System.EventHandler(this.btn_OpenFile_Click);
            // 
            // btn_ConvertFile
            // 
            this.btn_ConvertFile.Location = new System.Drawing.Point(98, 225);
            this.btn_ConvertFile.Name = "btn_ConvertFile";
            this.btn_ConvertFile.Size = new System.Drawing.Size(134, 46);
            this.btn_ConvertFile.TabIndex = 0;
            this.btn_ConvertFile.Text = "一键转换";
            this.btn_ConvertFile.UseVisualStyleBackColor = true;
            this.btn_ConvertFile.Click += new System.EventHandler(this.btn_ConvertFile_Click);
            // 
            // lbl_FliePath
            // 
            this.lbl_FliePath.AutoSize = true;
            this.lbl_FliePath.Location = new System.Drawing.Point(30, 189);
            this.lbl_FliePath.Name = "lbl_FliePath";
            this.lbl_FliePath.Size = new System.Drawing.Size(0, 12);
            this.lbl_FliePath.TabIndex = 2;
            this.lbl_FliePath.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lbl_GrammageStatus);
            this.groupBox1.Controls.Add(this.btn_SetGrammage);
            this.groupBox1.Controls.Add(this.cbx_Style);
            this.groupBox1.Controls.Add(this.cbx_ReservedBit);
            this.groupBox1.Controls.Add(this.lbl_ReservedBits);
            this.groupBox1.Controls.Add(this.lbl_Format);
            this.groupBox1.Controls.Add(this.tbx_OutputPath);
            this.groupBox1.Controls.Add(this.tbx_FileName);
            this.groupBox1.Controls.Add(this.btn_OpenPath);
            this.groupBox1.Controls.Add(this.btn_OpenFile);
            this.groupBox1.Controls.Add(this.btn_ConvertFile);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 277);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // lbl_GrammageStatus
            // 
            this.lbl_GrammageStatus.AutoSize = true;
            this.lbl_GrammageStatus.Location = new System.Drawing.Point(98, 74);
            this.lbl_GrammageStatus.Name = "lbl_GrammageStatus";
            this.lbl_GrammageStatus.Size = new System.Drawing.Size(41, 12);
            this.lbl_GrammageStatus.TabIndex = 12;
            this.lbl_GrammageStatus.Text = "未设置";
            // 
            // btn_SetGrammage
            // 
            this.btn_SetGrammage.Location = new System.Drawing.Point(17, 69);
            this.btn_SetGrammage.Name = "btn_SetGrammage";
            this.btn_SetGrammage.Size = new System.Drawing.Size(66, 23);
            this.btn_SetGrammage.TabIndex = 11;
            this.btn_SetGrammage.Text = "设置克重";
            this.btn_SetGrammage.UseVisualStyleBackColor = true;
            this.btn_SetGrammage.Click += new System.EventHandler(this.btn_SetGrammage_Click);
            // 
            // cbx_Style
            // 
            this.cbx_Style.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Style.FormattingEnabled = true;
            this.cbx_Style.Items.AddRange(new object[] {
            "样式一"});
            this.cbx_Style.Location = new System.Drawing.Point(99, 103);
            this.cbx_Style.Name = "cbx_Style";
            this.cbx_Style.Size = new System.Drawing.Size(181, 20);
            this.cbx_Style.TabIndex = 10;
            // 
            // cbx_ReservedBit
            // 
            this.cbx_ReservedBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_ReservedBit.FormattingEnabled = true;
            this.cbx_ReservedBit.Items.AddRange(new object[] {
            "保留1位小数",
            "保留2位小数",
            "保留3位小数",
            "保留4位小数"});
            this.cbx_ReservedBit.Location = new System.Drawing.Point(100, 138);
            this.cbx_ReservedBit.Name = "cbx_ReservedBit";
            this.cbx_ReservedBit.Size = new System.Drawing.Size(181, 20);
            this.cbx_ReservedBit.TabIndex = 10;
            // 
            // lbl_ReservedBits
            // 
            this.lbl_ReservedBits.AutoSize = true;
            this.lbl_ReservedBits.Location = new System.Drawing.Point(28, 141);
            this.lbl_ReservedBits.Name = "lbl_ReservedBits";
            this.lbl_ReservedBits.Size = new System.Drawing.Size(65, 12);
            this.lbl_ReservedBits.TabIndex = 9;
            this.lbl_ReservedBits.Text = "设置精度：";
            // 
            // lbl_Format
            // 
            this.lbl_Format.AutoSize = true;
            this.lbl_Format.Location = new System.Drawing.Point(28, 106);
            this.lbl_Format.Name = "lbl_Format";
            this.lbl_Format.Size = new System.Drawing.Size(65, 12);
            this.lbl_Format.TabIndex = 9;
            this.lbl_Format.Text = "格式样式：";
            // 
            // tbx_OutputPath
            // 
            this.tbx_OutputPath.Location = new System.Drawing.Point(99, 172);
            this.tbx_OutputPath.Name = "tbx_OutputPath";
            this.tbx_OutputPath.ReadOnly = true;
            this.tbx_OutputPath.Size = new System.Drawing.Size(182, 21);
            this.tbx_OutputPath.TabIndex = 2;
            // 
            // tbx_FileName
            // 
            this.tbx_FileName.Location = new System.Drawing.Point(98, 30);
            this.tbx_FileName.Name = "tbx_FileName";
            this.tbx_FileName.ReadOnly = true;
            this.tbx_FileName.Size = new System.Drawing.Size(182, 21);
            this.tbx_FileName.TabIndex = 2;
            // 
            // btn_OpenPath
            // 
            this.btn_OpenPath.Location = new System.Drawing.Point(18, 172);
            this.btn_OpenPath.Name = "btn_OpenPath";
            this.btn_OpenPath.Size = new System.Drawing.Size(66, 23);
            this.btn_OpenPath.TabIndex = 0;
            this.btn_OpenPath.Text = "设置目录";
            this.btn_OpenPath.UseVisualStyleBackColor = true;
            this.btn_OpenPath.Click += new System.EventHandler(this.btn_OpenPath_Click);
            // 
            // CashingFileConvertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(360, 301);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_FliePath);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CashingFileConvertForm";
            this.Text = "算绒助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EasyFileConvertForm_FormClosing);
            this.Load += new System.EventHandler(this.EasyFileConvertForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OpenFile;
        private System.Windows.Forms.Button btn_ConvertFile;
        private System.Windows.Forms.Label lbl_FliePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbx_FileName;
        private System.Windows.Forms.Label lbl_Format;
        private System.Windows.Forms.TextBox tbx_OutputPath;
        private System.Windows.Forms.Button btn_OpenPath;
        private System.Windows.Forms.Label lbl_ReservedBits;
        private System.Windows.Forms.ComboBox cbx_ReservedBit;
        private System.Windows.Forms.ComboBox cbx_Style;
        private System.Windows.Forms.Button btn_SetGrammage;
        private System.Windows.Forms.Label lbl_GrammageStatus;
    }
}

