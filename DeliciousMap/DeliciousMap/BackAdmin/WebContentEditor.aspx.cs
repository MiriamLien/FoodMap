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
    public partial class WebContentEditor : System.Web.UI.Page
    {
        private MapContentManager _mgr = new MapContentManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string id = this.hfID.Value.Trim();
            string name = this.txttitle.Text.Trim();
            string body = this.txtbody.Text.Trim();
            string LatitudeText = this.txtLatitude.Text.Trim();
            string LongitudeText = this.txtLongitude.Text.Trim();

            float Latitude;
            float Longitude;

            MapContentModels model = new MapContentModels();
            model.Title = name;
            model.Body = body;
            
            if(float.TryParse(LatitudeText, out Latitude))
                model.Latitude = Latitude;
            else
                model.Latitude = 0;

            if (float.TryParse(LongitudeText, out Longitude))
                model.Longitude = Longitude;
            else
                model.Longitude = 0;

            if(!this.Checkinput())
                return;

            _mgr.CreateMapContent(model);
            this.ltlMessage.Text = "儲存成功";
            this.txttitle.Text = string.Empty;
            this.txtbody.Text = string.Empty;
            this.txtLatitude.Text = string.Empty;
            this.txtLongitude.Text = string.Empty;
        }

        public bool Checkinput()
        {
            List<string> msglist = new List<string>();

            //if(this.txttitle.Text.Length == 0)
            if(string.IsNullOrWhiteSpace(this.txttitle.Text))
            {
                msglist.Add("標題為必填");
            }

            if (string.IsNullOrWhiteSpace(this.txtbody.Text))
            {
                msglist.Add("內文為必填");
            }

            if (!string.IsNullOrWhiteSpace(this.txtLatitude.Text))
            {
                float Latitude;
                if(!float.TryParse(this.txtLatitude.Text, out Latitude))
                  msglist.Add("緯度為數字，並介於 -90~90 之間");
                else if(Latitude < -90 || Latitude >90)
                  msglist.Add("緯度為數字，並介於 -90~90 之間");
            }

            if (!string.IsNullOrWhiteSpace(this.txtLongitude.Text))
            {
                float Longitude;
                if (!float.TryParse(this.txtLatitude.Text, out Longitude))
                    msglist.Add("緯度為數字，並介於 -180~80 之間");
                else if (Longitude < -180 || Longitude > 180)
                    msglist.Add("緯度為數字，並介於 -180~80 之間");
            }

            if(msglist.Count > 0)
            {
                string allError = string.Join("<br/>", msglist);
                this.ltlMessage.Text = allError;
                return false;
            }
            return true;
        }
    }
}