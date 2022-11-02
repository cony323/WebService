using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebTest
{
    /// <summary>
    /// <modules runAllManagedModulesForAllRequests="true">
    ///  <add name = "AsmxRequestModule" type="WebTest.AsmxRequestModule"/>
    ///  </modules>
    ///  
    /// CatchTextStream操作类 修改返回数据模式
    /// </summary>
    public class AsmxRequestModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(Application_BeginRequest);
        }

        public void Dispose()
        {
        }

        public void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            string extension = Path.GetExtension(application.Request.Path);
            if (application.Request.Path.IndexOf(".asmx/") > -1)
            {
                application.Response.Filter = new CatchTextStream(application.Response.Filter);
            }
        }
    }
}