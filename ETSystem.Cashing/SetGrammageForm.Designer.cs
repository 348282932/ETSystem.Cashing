namespace ETSystem.Cashing
{
    partial class SetGrammageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetGrammageForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_Grammage = new System.Windows.Forms.DataGridView();
            this.btn_OK = new System.Windows.Forms.Button();
            this.ETSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalGrammage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Grammage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_Grammage);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 178);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置克重";
            // 
            // dgv_Grammage
            // 
            this.dgv_Grammage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Grammage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ETSize,
            this.TotalGrammage});
            this.dgv_Grammage.Location = new System.Drawing.Point(16, 20);
            this.dgv_Grammage.Name = "dgv_Grammage";
            this.dgv_Grammage.RowTemplate.Height = 23;
            this.dgv_Grammage.Size = new System.Drawing.Size(243, 144);
            this.dgv_Grammage.TabIndex = 0;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(108, 196);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ETSize
            // 
            this.ETSize.DataPropertyName = "ETSize";
            this.ETSize.Frozen = true;
            this.ETSize.HeaderText = "型号";
            this.ETSize.Name = "ETSize";
            this.ETSize.ReadOnly = true;
            // 
            // TotalGrammage
            // 
            this.TotalGrammage.DataPropertyName = "TotalGrammage";
            this.TotalGrammage.HeaderText = "总克重";
            this.TotalGrammage.Name = "TotalGrammage";
            // 
            // SetGrammageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 224);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetGrammageForm";
            this.Text = "算绒助手";
            this.Load += new System.EventHandler(this.SetGrammageForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Grammage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_Grammage;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.DataGridViewTextBoxColumn ETSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalGrammage;
    }
}