using ETSystem.Cashing.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ETSystem.Cashing
{
    public partial class SetGrammageForm : Form
    {
        public static ETFile uploadFile = new ETFile();

        public event Action<List<Weight>> OKClick;

        public SetGrammageForm(ETFile file)
        {
            StartPosition = FormStartPosition.CenterScreen;

            this.MinimizeBox = false; // 隐藏最小化按钮

            this.MaximizeBox = false; // 隐藏最大化按钮

            this.FormBorderStyle = FormBorderStyle.FixedSingle; // 禁止调整窗体大小

            uploadFile = file;

            InitializeComponent();

            dgv_Grammage.AutoGenerateColumns = false;
        }

        private void SetGrammageForm_Load(object sender, EventArgs e)
        {
            dgv_Grammage.Rows.Clear();

            dgv_Grammage.DataSource = uploadFile.WeightList;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            var dataSource = dgv_Grammage.DataSource as List<Weight>;

            if (dataSource != null)
            {
                uploadFile.WeightList = dataSource;
            }

            OKClick?.Invoke(dataSource);

            this.Close();
        }
    }
}
