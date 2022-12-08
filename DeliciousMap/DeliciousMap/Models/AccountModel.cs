using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliciousMap.Models
{
    public class AccountModel
    {
        public Guid ID { get; set; }
        //帳號
        public string Account {get; set;}
        //密碼
        public string Password { get; set;}
        //使用者權限
        public UserLevelEnum UserLevel { get; set; }
    }
}