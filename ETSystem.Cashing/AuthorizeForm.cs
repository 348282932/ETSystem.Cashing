using ETSystem.Cashing.Common;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace ETSystem.Cashing
{
    public partial class AuthorizeForm : Form
    {
        /// <summary>
        /// 授权缓存文件地址
        /// </summary>
        public static readonly string cachePath = $"{Application.StartupPath}\\AuthorizeCache.data";

        public AuthorizeForm()
        {
            StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
        }

        private void AuthorizeForm_Load(object sender, EventArgs e)
        {
            var localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            var machineGuid = localMachine64.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography").GetValue("MachineGuid")?.ToString();

            if (string.IsNullOrEmpty(machineGuid)) 
            {
                MessageBox.Show("此程序只支持64位的操作系统！");

                return;
            }

            tbx_MachineCode.Text = machineGuid;
        }

        private void tbx_Activation_Click(object sender, EventArgs e)
        {
            if (Encryption.Md5($"SRZS{tbx_MachineCode.Text}SRZS").ToLower() != tbx_ActivationCode.Text.Trim().ToLower()) 
            {
                MessageBox.Show("激活失败，激活码不正确！");

                return;
            }

            MessageBox.Show("激活成功！");

            var authorizeCache = JsonConvert.SerializeObject(new { MachineCode = tbx_MachineCode.Text, ActivationCode = tbx_ActivationCode.Text.Trim()});

            File.WriteAllText(cachePath, authorizeCache);

            var mainForm = new CashingFileConvertForm();

            mainForm.Show();

            this.Hide();
        }
    }
}
