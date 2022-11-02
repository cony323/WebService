using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace WebTest
{
    public class CatchTextStream : Stream
    {
        #region 数据流
        /// <summary>
        /// 数据流
        /// </summary>
        private readonly Stream output;
        #endregion
        #region 构造函数
        public CatchTextStream(Stream s)
        {
            output = s;
        }
        #endregion
        #region 重载属性及方法
        public override bool CanRead => output.CanRead;

        public override bool CanSeek => output.CanSeek;

        public override bool CanWrite => output.CanWrite;

        public override void Flush()
        {
            output.Flush();
        }
        public override long Length => output.Length;

        public override long Position
        {
            get => output.Position;
            set => output.Position = value;
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            return output.Read(buffer, offset, count);
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            return output.Seek(offset, origin);
        }
        public override void SetLength(long value)
        {
            output.SetLength(value);
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (HttpContext.Current != null)
            {
                HttpContext context = HttpContext.Current;
                Encoding encoding = context.Response.ContentEncoding;
                string responseInfo = encoding.GetString(buffer, offset, count);
                responseInfo = responseInfo.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<string xmlns=\"http://tempuri.org/\">", "");
                responseInfo = responseInfo.Substring(0, responseInfo.Length - 9);

                responseInfo = System.Web.HttpUtility.HtmlDecode(responseInfo);

                buffer = encoding.GetBytes(responseInfo);
                output.Write(buffer, 0, buffer.Length);
            }
        }
        #endregion
    }
}