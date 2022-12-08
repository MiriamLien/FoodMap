using DeliciousMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliciousMap.BackAdmin
{
    public class AdminPageBace : System.Web.UI.Page
    {

        public virtual UserLevelEnum[] PageUserLevel { get; set; } = {UserLevelEnum.Admin, UserLevelEnum.Super};
        public UserLevelEnum[] GetUserLevel()
        {
            return this.PageUserLevel;
        }
    }
}