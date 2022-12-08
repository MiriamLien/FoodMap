using DeliciousMap.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliciousMap
{
    public partial class MapList : System.Web.UI.Page
    {
        private MapContentManager _mgr = new MapContentManager();
        private const int _pageSize = 2;

        public string aaa = "aaa";
        protected void Page_Load(object sender, EventArgs e)
        {
            string pageIndexText = this.Request.QueryString["Index"];
            int pageIndex =
                (string.IsNullOrWhiteSpace(pageIndexText))
                    ? 1
                    : Convert.ToInt32(pageIndexText);

            if (!this.IsPostBack)
            {
                string keyword = this.Request.QueryString["keyword"];

                if (!string.IsNullOrWhiteSpace(keyword))
                    this.txtkeyword.Text = keyword;

                int totalrows = 0;
                var list = this._mgr.GetMapList(keyword, _pageSize, pageIndex, out totalrows);
                //this.ProcessPager(keyword, pageIndex, totalrows);
                this.ucPage.TotalRows = totalrows;
                this.ucPage.PageIndex = pageIndex;
                this.ucPage.Bind("keyword", keyword);

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.Request.Url.LocalPath + "?keyword=" + this.txtkeyword.Text);
        }
    }
}