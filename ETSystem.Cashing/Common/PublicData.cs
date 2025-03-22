using System.Net;

namespace ETSystem.Cashing.Common
{
    /// <summary>
    /// 公共缓存数据
    /// </summary>
    public static class PublicData
    {
        /// <summary>
        /// 消费端用户
        /// </summary>
        public static UserInfo UserA { get; set; }

        /// <summary>
        /// 服务端用户
        /// </summary>
        public static UserInfo UserB { get; set; }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo 
    {
        /// <summary>
        /// Cookie
        /// </summary>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 所属公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 登录用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录用户ID
        /// </summary>
        public int UserId { get; set; }
    }

    /// <summary>
    /// OSS 配置
    /// </summary>
    public class OssConfig
    {
        public string AccessId { get; set; }

        public string Dir { get; set; } 

        public string FileName { get; set; }
        
        public string OssHost { get; set; }

        public string Policy { get; set; } 

        public string Signature { get; set; }

        public string Mode { get; set; }

    }
}
