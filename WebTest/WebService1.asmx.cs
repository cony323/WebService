using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Web.Services;

namespace WebTest
{
    using WebTest.Models;
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        private const string _xmlheader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        private const string _replacestr1 = " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"";
        private const string _replacestr2 = " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"";
        [WebMethod]
        public string HelloWorld(string inXml)
        {
            PersonModel model = new PersonModel();
            model.UserId = inXml;
            model.UserName = "番茄土豆";
            model.Tel = null;
            model.SignTime = DateTime.Now;

            return XmlSerialize(model);
        }

        /// <summary>
        /// xml序列化成字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>xml字符串</returns>
        private static string XmlSerialize(object obj)
        {
            string returnStr = "";
            XmlSerializer serializer = GetSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = null;
            StreamReader sr = null;
            try
            {
                xtw = new System.Xml.XmlTextWriter(ms, Encoding.UTF8);
                xtw.Formatting = System.Xml.Formatting.Indented;
                serializer.Serialize(xtw, obj);
                ms.Seek(0, SeekOrigin.Begin);
                sr = new StreamReader(ms);
                returnStr = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
                if (sr != null)
                    sr.Close();
                ms.Close();
            }
            return returnStr.Replace(_replacestr1, "").Replace(_replacestr2, "").Replace(_xmlheader, "").Replace(" ", "");
        }

        private static XmlSerializer GetSerializer(Type t)
        {
            int type_hash = t.GetHashCode();
            var serializer_dict = new Dictionary<int, XmlSerializer>();
            if (!serializer_dict.ContainsKey(type_hash))
                serializer_dict.Add(type_hash, new XmlSerializer(t));

            return serializer_dict[type_hash];
        }
    }
}
