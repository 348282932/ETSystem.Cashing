using System.Security.Cryptography;
using System.Text;

namespace ETSystem.Cashing.Common
{
    /// <summary>
    /// 加密类
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5(string str)
        {
            var md5 = MD5.Create();

            byte[] strbuffer = Encoding.Default.GetBytes(str);

            strbuffer = md5.ComputeHash(strbuffer);

            string strNew = "";

            foreach (byte item in strbuffer)
            {
                strNew += item.ToString("x2");
            }

            return strNew;
        }
    }
}
