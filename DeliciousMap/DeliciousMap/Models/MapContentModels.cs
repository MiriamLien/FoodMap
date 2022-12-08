using System;

namespace DeliciousMap.Models
{
    public class MapContentModels
    {
        //主要欄位
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CoverImage { get; set; }
        public bool IsEnable { get; set; }  
        public double Longitude { get; set; } //經度
        public double Latitude { get; set; }  //緯度

        //管理用欄位
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateUser { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Guid? DeleteUser { get; set; }

        public string Content
        {
            get { return this.Body; }
            set { this.Body = value; }
        }
    }
}