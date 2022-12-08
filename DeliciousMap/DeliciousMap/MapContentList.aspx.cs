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
    public partial class MapContentList : System.Web.UI.Page
    {
        private MapContentManager _mgr = new MapContentManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<MapContentModels> list = this._mgr.GetMapList();
            if (list.Count == 0)
            {
                this.GridView1.Visible = false;
                this.plcEmpty.Visible = true;
            }
            else
            {
                   this.GridView1.DataSource = list;
                   this.GridView1.DataBind();
            }
            
        }
    }
}