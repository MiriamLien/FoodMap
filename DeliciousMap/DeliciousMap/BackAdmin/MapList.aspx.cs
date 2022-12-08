using DeliciousMap.Managers;
using DeliciousMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliciousMap.BackAdmin
{
    public partial class MapList : AdminPageBace
    {
        //public override UserLevelEnum[] PageUserLevel { get; set; } = { UserLevelEnum.Admin, UserLevelEnum.Super };
        private MapContentManager _mgr = new MapContentManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string keyword = this.Request.QueryString["keyword"];
                if (!string.IsNullOrWhiteSpace(keyword))
                    this.txtkeyword.Text = keyword;

                var list = this._mgr.GetAdminMapList(keyword);
                if (list.Count == 0)
                {
                    this.plcEmpty.Visible = true;
                    this.rptList.Visible = false;
                }
                else
                {
                    this.plcEmpty.Visible = false;
                    this.rptList.Visible = true;

                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
            }
        }

        public void btnCreate_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("MapDetail.aspx");
        }

        public void btnDelete_Click(object sender, EventArgs e)
        {
            List<Guid> ids = new List<Guid>();
            foreach(RepeaterItem item in this.rptList.Items)
            {
                CheckBox chk = this.FindControl("ckbDel") as CheckBox;
                HiddenField hfId = this.FindControl("hfid") as HiddenField;

                if(chk != null && hfId != null && chk.Checked)
                {
                    Guid id;
                    if(Guid.TryParse(hfId.Value, out id))
                    {
                        ids.Add(id);
                    }
                }

                List<MapContentModels> pickedList = this._mgr.GetMapList(ids);
                foreach (MapContentModels model in pickedList)
                {
                    this.DeleteImage(model.CoverImage);
                }
                if (ids.Count > 0)
                    this._mgr.DeleteMapContent(ids);
            }
        }

        private void DeleteImage(string imagePath)
        {
            string filePath = HostingEnvironment.MapPath("~/" + imagePath);

            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtkeyword.Text))
            {
                this.Response.Redirect("MapList.aspx");
            }
            else
            {
                string keyword = this.txtkeyword.Text.Trim();
                Response.Redirect("MapList.aspx?keyword=" + keyword);
            }
        }
    }
}