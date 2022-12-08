using DeliciousMap.Managers;
using DeliciousMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliciousMap.BackAdmin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        private string _loginPage = "~/login.aspx";
        private string _indexPage = "~/BackAdmin/index.aspx";
        private AccountManager _mgr = new AccountManager();
        protected void Page_Init(object sender, EventArgs e)
        {
            //如果尚未登入，跳至登入頁
            //且同時，要強制停止目前的 HttpContext 繼續執行
            if(!new AccountManager().IsLogined())
                Response.Redirect(_loginPage, true);

            if(this.Page is AdminPageBace)
            {
                AdminPageBace adminPage = (AdminPageBace)this.Page;
                UserLevelEnum[] pageUserLevel = adminPage.GetUserLevel();

                AccountModel model = this._mgr.GetCurrentUser();
                if(!pageUserLevel.Contains(model.UserLevel))
                    Response.Redirect(_loginPage, true);
            }
        }
    }
}