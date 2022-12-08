using DeliciousMap.Helpers;
using DeliciousMap.Managers;
using DeliciousMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliciousMap
{
    public partial class MapDetail : System.Web.UI.Page
    {
        private MapContentManager _mgr = new MapContentManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];

            // 如果沒有帶 id ，跳回列表頁
            if (string.IsNullOrWhiteSpace(idText))
                this.BackToListPage();

            Guid id;
            if (!Guid.TryParse(idText, out id))
                this.BackToListPage();

            // 查資料
            MapContentModels model = this._mgr.GetMap(id);
            if (model == null)
                this.BackToListPage();

            // 不開放前台顯示
            if (!model.IsEnable)
                this.BackToListPage();

            // 顯示資料
            this.ShowDetail(model);
        }

        private void ShowDetail(MapContentModels model)
        {
            this.ltlcreateDate.Text = model.CreateDate.ToString("yyyy-MM-dd");
            this.ltlTitle.Text = model.Title;
            this.ltlBody.Text = model.Body;
            this.imgCoverImage.Src = model.CoverImage;
            this.aLinkPic.HRef = model.CoverImage;

            this.ltlLatitude.Text = TextFormatHelper.ConvertDigitalToDegrees(model.Latitude);
            this.ltlLongitude.Text = TextFormatHelper.ConvertDigitalToDegrees(model.Longitude);
        }

        private void BackToListPage()
        {
            this.Response.Redirect("MapList.aspx", true);
        }
    }
}