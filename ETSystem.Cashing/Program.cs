using ETSystem.Cashing.Common;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using System.IO;

namespace ETSystem.Cashing
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (CheckAuthorize())
            {
                Application.Run(new CashingFileConvertForm());
            }
            else
            {
                Application.Run(new AuthorizeForm());
            }
        }

        /// <summary>
        /// 验证授权
        /// </summary>
        /// <returns></returns>
        private static bool CheckAuthorize()
        {
            return true;

            if (!File.Exists(AuthorizeForm.cachePath)) return false;

            var jTokenObj = (JToken)JsonConvert.DeserializeObject(File.ReadAllText(AuthorizeForm.cachePath));

            var machineCode = jTokenObj["MachineCode"].ToString();

            var activationCode = jTokenObj["ActivationCode"].ToString();

            var localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            var machineGuid = localMachine64.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography").GetValue("MachineGuid")?.ToString();

            if (machineCode == machineGuid && activationCode.ToLower() == Encryption.Md5($"ET{machineCode}ET").ToLower()) return true;

            return false;
        }
    }
}
