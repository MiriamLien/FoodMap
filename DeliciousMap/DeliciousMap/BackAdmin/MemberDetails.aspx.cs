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

    public partial class MemberDetails : AdminPageBace
    {
        private bool _isEditMode = true;
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
            if (!string.IsNullOrEmpty(this.Request.QueryString["ID"]))
                _isEditMode = true;
            else
                _isEditMode = false;

            if (_isEditMode)
            {
                string idText = this.Request.QueryString["ID"];
                Guid id;
                if (!Guid.TryParse(idText, out id))
                {
                    this.lblMsg.Text = "id 錯誤";
                }
                else
                    this.InitEditMode(id);
            }
            else
                this.InitCreateMode();
        }

        //初始化新增模式的內頁
        private void InitCreateMode()
        {
            this.ltlAccount.Visible = false;
            this.txtAccount.Visible = true;
            this.ltlID.Text = "尚待新增";
        }
        private void InitEditMode(Guid id)
        {
            this.ltlAccount.Visible = true;
            this.txtAccount.Visible = false;

            AccountModel member = this._mgr.GetAccount(id);
            if (member == null)
            {
                this.lblMsg.Text = "查無此 id";
                return;
            }
            this.ltlAccount.Text = member.Account;

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            string account = this.txtAccount.Text.Trim();
            string pwd = this.txtPassword.Text.Trim();
            if (!_isEditMode)
            {
                AccountModel member = new AccountModel();
                member.Account = account;
                member.Password = pwd;

                this._mgr.CreateAccount(member);
                Response.Redirect("MemberDetails.aspx?ID=" + member.ID);
            }
            else
            {
                string idText = this.Request.QueryString["ID"];
                Guid id;
                if (!Guid.TryParse(idText, out id))
                {
                    this.lblMsg.Text = "id 錯誤";
                    return;
                }

                //從資料庫查出來更新
                AccountModel member = this._mgr.GetAccount(id);
                member.Password = pwd;
                this._mgr.UpdateAccount(member);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberList.aspx");
        }
    }
}