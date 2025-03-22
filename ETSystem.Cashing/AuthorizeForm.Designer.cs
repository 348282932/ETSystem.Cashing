namespace ETSystem.Cashing
{
    partial class AuthorizeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizeForm));
            this.lbl_MachineCode = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbx_Activation = new System.Windows.Forms.Button();
            this.tbx_ActivationCode = new System.Windows.Forms.TextBox();
            this.lbl_ActivationCode = new System.Windows.Forms.Label();
            this.tbx_MachineCode = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_MachineCode
            // 
            this.lbl_MachineCode.AutoSize = true;
            this.lbl_MachineCode.Location = new System.Drawing.Point(47, 41);
            this.lbl_MachineCode.Name = "lbl_MachineCode";
            this.lbl_MachineCode.Size = new System.Drawing.Size(65, 12);
            this.lbl_MachineCode.TabIndex = 0;
            this.lbl_MachineCode.Text = "产品编码：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbx_Activation);
            this.groupBox1.Controls.Add(this.tbx_ActivationCode);
            this.groupBox1.Controls.Add(this.lbl_ActivationCode);
            this.groupBox1.Controls.Add(this.tbx_MachineCode);
            this.groupBox1.Controls.Add(this.lbl_MachineCode);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 167);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tbx_Activation
            // 
            this.tbx_Activation.Location = new System.Drawing.Point(169, 118);
            this.tbx_Activation.Name = "tbx_Activation";
            this.tbx_Activation.Size = new System.Drawing.Size(132, 33);
            this.tbx_Activation.TabIndex = 2;
            this.tbx_Activation.Text = "激  活";
            this.tbx_Activation.UseVisualStyleBackColor = true;
            this.tbx_Activation.Click += new System.EventHandler(this.tbx_Activation_Click);
            // 
            // tbx_ActivationCode
            // 
            this.tbx_ActivationCode.Location = new System.Drawing.Point(118, 78);
            this.tbx_ActivationCode.Name = "tbx_ActivationCode";
            this.tbx_ActivationCode.Size = new System.Drawing.Size(248, 21);
            this.tbx_ActivationCode.TabIndex = 1;
            // 
            // lbl_ActivationCode
            // 
            this.lbl_ActivationCode.AutoSize = true;
            this.lbl_ActivationCode.Location = new System.Drawing.Point(59, 81);
            this.lbl_ActivationCode.Name = "lbl_ActivationCode";
            this.lbl_ActivationCode.Size = new System.Drawing.Size(53, 12);
            this.lbl_ActivationCode.TabIndex = 0;
            this.lbl_ActivationCode.Text = "激活码：";
            // 
            // tbx_MachineCode
            // 
            this.tbx_MachineCode.Location = new System.Drawing.Point(118, 38);
            this.tbx_MachineCode.Name = "tbx_MachineCode";
            this.tbx_MachineCode.ReadOnly = true;
            this.tbx_MachineCode.Size = new System.Drawing.Size(248, 21);
            this.tbx_MachineCode.TabIndex = 1;
            // 
            // AuthorizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 191);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthorizeForm";
            this.Text = "工具激活";
            this.Load += new System.EventHandler(this.AuthorizeForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_MachineCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button tbx_Activation;
        private System.Windows.Forms.TextBox tbx_ActivationCode;
        private System.Windows.Forms.Label lbl_ActivationCode;
        private System.Windows.Forms.TextBox tbx_MachineCode;
    }
}