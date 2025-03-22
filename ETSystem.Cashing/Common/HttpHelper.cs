using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ETSystem.Cashing.Common
{
    /// <summary>
    /// Http 助手类
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// POST 请求
        /// </summary>
        /// <param name="Url"> 请求地址 </param>
        /// <param name="postDataStr"> 请求参数 </param>
        /// <param name="cookie"> 地址相关的 Cookie </param>
        /// <returns> 返回页面数据 </returns>
        public static Response HttpPost(string Url, string postDataStr, CookieContainer cookie = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                request.Method = "POST";

                request.ContentType = "application/json;charset=UTF-8";

                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);

                // 设置 ET 站点 TOKEN

                if (cookie != null)
                {
                    request.CookieContainer = cookie;

                    var token = cookie.GetCookies(new Uri(Url))["ET-Token"]?.Value;

                    if (!string.IsNullOrEmpty(token)) request.Headers["Authorization"] = $"Bearer {token}";
                }

                Stream myRequestStream = request.GetRequestStream();

                using (var binaryWriter = new BinaryWriter(myRequestStream))
                {
                    binaryWriter.Write(Encoding.UTF8.GetBytes(postDataStr));
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);

                String retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();

                myResponseStream.Close();

                if (!string.IsNullOrEmpty(retString))
                {
                    var jTokenObj = (JToken)JsonConvert.DeserializeObject(retString);

                    if ((string)jTokenObj["Success"] != "True")
                    {
                        return new Response(false, (string)jTokenObj["ErrMes"]);
                    }
                }
                else
                {
                    return new Response(false, "服务器异常，请稍后再试！");
                }

                return new Response(true, retString);
            }
            catch (WebException)
            {
                return new Response(false, "网络异常，请稍后再试！");
            }
            catch (JsonException)
            {
                return new Response(false, "返回数据无法序列化，请联系管理员升级程序！");
            }
            catch (Exception ex)
            {
                return new Response(false, $"程序异常，{ex.Message}，堆栈：{ex.StackTrace}");
            }
        }

        /// <summary>
        /// GET 请求
        /// </summary>
        /// <param name="Url"> 请求地址 </param>
        /// <param name="postDataStr"> 请求参数 </param>
        /// <param name="cookie"> 地址相关的 Cookie </param>
        /// <returns> 返回页面数据 </returns>
        public static Response HttpGet(string Url, string postDataStr, CookieContainer cookie = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);

                request.Method = "GET";

                request.ContentType = "text/html;charset=UTF-8";

                // 设置 ET 站点 TOKEN

                if (cookie != null)
                {
                    request.CookieContainer = cookie;

                    var token = cookie.GetCookies(new Uri(Url))["ET-Token"]?.Value;

                    if(!string.IsNullOrEmpty(token)) request.Headers["Authorization"] = $"Bearer {token}";
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                String retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();

                myResponseStream.Close();

                if (!string.IsNullOrEmpty(retString))
                {
                    var jTokenObj = (JToken)JsonConvert.DeserializeObject(retString);

                    if ((string)jTokenObj["Success"] != "True")
                    {
                        return new Response(false, (string)jTokenObj["ErrMes"]);
                    }
                }
                else
                {
                    return new Response(false, "服务器异常，请稍后再试！");
                }

                return new Response(true, retString);
            }
            catch (WebException)
            {
                return new Response(false, "网络异常，请稍后再试！");
            }
            catch (JsonException)
            {
                return new Response(false, "返回数据无法序列化，请联系管理员升级程序！");
            }
            catch (Exception ex)
            {
                return new Response(false, $"程序异常，{ex.Message}，堆栈：{ex.StackTrace}");
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Response HttpUpload(string url, string filePath, NameValueCollection data = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";

                Stream postStream = new MemoryStream();

                #region 处理Form表单文件上传

                // 通过表单上传文件

                string boundary = "----" + DateTime.Now.Ticks.ToString("x");

                var stringBuilder = new StringBuilder();

                stringBuilder.AppendFormat("--{0}\r\n", boundary);

                if (data != null) 
                {
                    foreach (string key in data.Keys)
                    {
                        stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n--{2}\r\n", key, data[key], boundary);
                    }
                }

                stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\nContent-Type: application/octet-stream\r\n\r\n", Path.GetFileName(filePath));

                // 准备文件流

                using (var fileStream = FileToStream(filePath))
                {
                    var formdata = stringBuilder.ToString();

                    var formdataBytes = Encoding.UTF8.GetBytes(formdata);

                    postStream.Write(formdataBytes, 0, formdataBytes.Length);

                    // 写入文件

                    byte[] buffer = new byte[1024];

                    int bytesRead;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        postStream.Write(buffer, 0, bytesRead);
                    }
                }

                // 结尾

                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

                postStream.Write(footer, 0, footer.Length);

                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);

                #endregion

                request.ContentLength = postStream != null ? postStream.Length : 0;

                request.Accept = "application/json, text/plain, */*";

                request.KeepAlive = true;

                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

                #region 输入二进制流

                if (postStream != null)
                {
                    postStream.Position = 0;

                    // 直接写入流

                    using (var requestStream = request.GetRequestStream())
                    {
                        byte[] buffer = new byte[1024];

                        int bytesRead;

                        while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }

                        postStream.Close(); //关闭文件访问
                    } 
                }
                #endregion

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                    {
                        string retString = myStreamReader.ReadToEnd();

                        return new Response(true, retString);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                return new Response(false, "找不到待上传的文件，请重新选择待上传的文件后再试！");
            }
            catch (DirectoryNotFoundException)
            {
                return new Response(false, "找不到待上传的文件路径，请重新选择待上传的文件后再试！");
            }
            catch (IOException)
            {
                return new Response(false, "文件被占用，请关闭打开tes文件的应用后再试！");
            }
            catch (WebException)
            {
                return new Response(false, "网络异常，请稍后再试！");
            }
            catch (Exception ex)
            {
                return new Response(false, $"程序异常，{ex.Message}，堆栈：{ex.StackTrace}");
            }
        }

        /// <summary>
        /// 文件转换为流
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件

            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // 读取文件的 byte[]

            byte[] bytes = new byte[fileStream.Length];

            fileStream.Read(bytes, 0, bytes.Length);

            fileStream.Close();

            // 把 byte[] 转换成 Stream

            Stream stream = new MemoryStream(bytes);

            return stream;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Response HttpDownload(string url, string fileName, string filePath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    string filePathDir = $"{Application.StartupPath}\\Download";

                    if (!Directory.Exists(filePathDir))
                    {
                        Directory.CreateDirectory(filePathDir);
                    }

                    filePath = $"{filePathDir}\\{fileName}";
                }
                else 
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    filePath = $"{filePath}\\{fileName}";
                }

                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                Stream responseStream = response.GetResponseStream();

                //创建本地文件写入流

                byte[] bArr = new byte[1024];

                int iTotalSize = 0;

                int size = responseStream.Read(bArr, 0, bArr.Length);

                while (size > 0)
                {
                    iTotalSize += size;

                    fs.Write(bArr, 0, size);

                    size = responseStream.Read(bArr, 0, bArr.Length);
                }

                fs.Close();

                responseStream.Close();

                return new Response();
            }
            catch (IOException)
            {
                return new Response(false, "文件被占用，请关闭打开tes文件的应用后再试！");
            }
            catch (WebException)
            {
                return new Response(false, "网络异常，请稍后再试！");
            }
            catch (Exception ex)
            {
                return new Response(false, $"程序异常，{ex.Message}，堆栈：{ex.StackTrace}");
            }
        }
    }
}
