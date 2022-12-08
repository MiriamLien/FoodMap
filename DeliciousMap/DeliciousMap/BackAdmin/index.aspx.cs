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
    public partial class index : System.Web.UI.Page
    {
        private AccountManager _mgr = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountModel account = this._mgr.GetCurrentUser();
            this.ltlAccount.Text = account.Account;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this._mgr.Logout();
            Response.Redirect("~/Login.aspx");
        }
    }
}