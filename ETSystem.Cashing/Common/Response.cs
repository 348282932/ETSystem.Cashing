using System;

namespace ETSystem.Cashing
{
    /// <summary>
    /// 响应
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 响应状态
        /// </summary>
        public Boolean Success { get; set; } = true;

        /// <summary>
        /// 响应消息
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public Response(Boolean success, String message)
        {
            this.Success = success;

            this.Message = message;
        }
    }

    /// <summary>
    /// 响应
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response
    {
        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public Response(Boolean success, String message) : base(success, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="data"></param>
        public Response(Boolean success, T data)
        {
            this.Success = success;

            this.Data = data;
        }
    }
}
