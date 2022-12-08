using DeliciousMap.Managers;
using DeliciousMap.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliciousMap.BackAdmin
{
    public partial class MapDetail : AdminPageBace
    {
        private bool _isEditMode = false;
        private MapContentManager _mgr = new MapContentManager();
        private static UserLevelEnum[] _pageLevel = { UserLevelEnum.Admin, UserLevelEnum.Super };
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
            //做編輯模式或是新增模式的判斷
            if(!string.IsNullOrWhiteSpace(this.Request.QueryString["ID"]))
                _isEditMode = false;
            else
                _isEditMode = true;

            if(this._isEditMode)
                this.InitEditMode();
            else
                this.InitCreateMode();
        //新增模式初始化

        //編輯模式初始化
    }

        //新增模式初始化
        private void InitCreateMode()
        {
            this.flCoverimage.Visible = false;
            this.lblMsg.Text = "尚待新增";
            //this.txtAccount.Visible = true;
        }
        //編輯模式初始化
        private void InitEditMode()
        {
            //this.ltlAccount.Visible = true;
            //this.txtAccount.Visible = false;

            //MemberAccount member = this._mgr.GetAccount(id);
            //if (member == null)
            //{
            //    this.lblMsg.Text = "查無此 id";
            //    return;
            //}
            //this.ltlAccount.Text = member.Account;

        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            errorMsgList = new List<string>();

            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
                errorMsgList.Add("標題為必填");
            if (string.IsNullOrWhiteSpace(this.txtBody.Text))
                errorMsgList.Add("內文為必填");
            if (string.IsNullOrWhiteSpace(this.txtLongitude.Text))
                errorMsgList.Add("經度為必填");
            if (string.IsNullOrWhiteSpace(this.txtLatitude.Text))
                errorMsgList.Add("緯度為必填");
            if (!this._isEditMode)  // 只有新增模式，才做封面圖的必填
            {
                if (!this.flCoverimage.HasFile)
                    errorMsgList.Add("封面圖為必填。");
            }

            double temp;
            if (!double.TryParse(this.txtLongitude.Text.Trim(), out temp))
                errorMsgList.Add("經度須介於 -180~180 度，精度允許六位小數。");
            else if (temp < -180 || temp > 180)
                errorMsgList.Add("經度須介於 -180~180 度，精度允許六位小數。");

            if (!double.TryParse(this.txtLatitude.Text.Trim(), out temp))
                errorMsgList.Add("緯度須介於 -90~90 度，精度允許六位小數。");
            else if (temp < -90 || temp > 90)
                errorMsgList.Add("緯度須介於 -90~90 度，精度允許六位小數。");

            if (errorMsgList.Count > 0)
                return false;
            else
                return true;
        }


        protected void btnsave_Click(object sender, EventArgs e)
        {
            //欄未必填檢查
            List<string> errorMsgList = new List<string>();
            if(!this.CheckInput(out errorMsgList))
            {
                this.lblMsg.Text = string.Join("<br/>", errorMsgList);
                return;
            }            
            
            MapContentModels model = new MapContentModels()
            {
                Title = this.txtTitle.Text.Trim(),
                Body = this.txtBody.Text.Trim(),
                Longitude = Convert.ToDouble(this.txtLongitude.Text.Trim()),
                Latitude = Convert.ToDouble(this.txtLatitude.Text.Trim()),
                IsEnable = this.chkIsEnable.Checked,
            };

            if(this.flCoverimage.HasFile)
            {
                System.Threading.Thread.Sleep(3);
                Random random = new Random((int)DateTime.Now.Ticks);
                string folderPath = "~/FileDownload/MapContent/";
                string filename = DateTime.Now.ToString("yyyyMMdd-HHmmss-FFFFF") + " - " + random.Next(100000).ToString("00000") + Path.GetExtension(this.flCoverimage.FileName);

                folderPath = HostingEnvironment.MapPath(folderPath);

                // 假如資料夾不存在，先建立
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string newFilePath = Path.Combine(folderPath, filename);
                this.flCoverimage.SaveAs(newFilePath);  
                //不要誤刪，然後不打回來

                model.CoverImage = "/FileDownload/MapContent/" + filename;
            }

            // 取得登入者
            AccountModel account = new AccountManager().GetCurrentUser();

            //儲存
            this._mgr.CreateMapContent(model, account.ID);

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //跳回前頁
            Response.Redirect("MapList.aspx");
        }
    }
}