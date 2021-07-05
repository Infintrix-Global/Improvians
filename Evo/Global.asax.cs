using Evo.BAL_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Evo
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            foreach (SiteMapProvider mapProvider in SiteMap.Providers)
            {
                mapProvider.SiteMapResolve += ChangeMapPath;
            }
        }
        public SiteMapNode ChangeMapPath(Object sender, SiteMapResolveEventArgs e)
        {
            if (SiteMap.CurrentNode == null)
                return null;
            SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
            SiteMapNode tempNode = currentNode;
            string PageType = e.Context.Request.QueryString["PageType"];

            if (!string.IsNullOrEmpty(PageType))
            {
                var node = e.Provider.FindSiteMapNodeFromKey(tempNode.Url + "?PageType=" + PageType);
                if (node != null)
                {
                    tempNode.ParentNode = node.ParentNode;
                    tempNode.Title = node.Title;
                }
            }
            return currentNode;
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            General.ErrorMessage(Server.GetLastError().ToString());
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}