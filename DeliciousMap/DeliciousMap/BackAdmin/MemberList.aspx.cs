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
    public partial class MemberList : AdminPageBace
    {
        private AccountManager _mgr = new AccountManager();
        private static UserLevelEnum[] _pageLevel = { UserLevelEnum.Super };
        private AccountManager _accMgr = new AccountManager();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!_pageLevel.Contains(this._accMgr.GetCurrentUser().UserLevel))
            {
                this.Response.Redirect("index.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = this.Request.QueryString["keyword"];
                this.txtkeyword.Text = keyword;

                List<AccountModel> list = _mgr.GetAccountList(keyword);
                if (list.Count > 0)
                {
                    this.gvList.DataSource = list;
                    this.gvList.DataBind();

                    this.plcEmpty.Visible = false;
                    this.gvList.Visible = true;
                }
                else
                {
                    this.plcEmpty.Visible = true;
                    this.gvList.Visible = false;
                }
            }

        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberDetails.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtkeyword.Text.Trim();

            if(string.IsNullOrWhiteSpace(keyword))
              Response.Redirect("MemberList.aspx");
            else
               Response.Redirect("MemberList.aspx?keyword=" + keyword);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {  
            List<Guid> idList = new List<Guid>();  
            foreach(GridViewRow gRow in this.gvList.Rows)
            {
                CheckBox chkdel = gRow.FindControl("chkdel") as CheckBox;
                HiddenField hfID = gRow.FindControl("hfID") as HiddenField;

                if(chkdel != null && hfID != null)
                {
                    if(chkdel.Checked)
                    {
                        Guid id;
                        if (Guid.TryParse(hfID.Value, out id))
                            idList.Add(id);
                    }
                }
            }
            if (idList.Count > 0)
            {
                this._mgr.DeleteAccounts(idList);
                this.Response.Redirect(this.Request.RawUrl);
            }
        }
    }
}