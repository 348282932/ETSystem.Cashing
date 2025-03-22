using System.Net;

namespace ETSystem.Cashing
{
    /// <summary>
    /// 请求
    /// </summary>
    public class Request
    {
        public bool IsPost { get; set; } = true;

        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string Data { get; set; }
    }
}
