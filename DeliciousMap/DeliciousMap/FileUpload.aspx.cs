using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DeliciousMap.Helpers;

namespace DeliciousMap
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //目前編到的序號
        private static int _fileNumber = 1;

        private string GetSavePath()
        {
            string folder = "~/FileDownload/";
            //string folderPath = Server.MapPath(folder);  不安全的方法
            string folderPath = HostingEnvironment.MapPath(folder);  //System.Web.Hosting
            return folderPath;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.FileUpload[] fuArr =
            {
                this.FileUpload1, this.FileUpload2,this.FileUpload3,this.FileUpload4
            };
            Label[] lblArr = { this.lblMasg, this.lblMsg2, this.lblMsg3, this.lblMsg4 };

            bool isChecked = true;
            for (int i = 0; i < fuArr.Length; i++)
            {
                System.Web.UI.WebControls.FileUpload fu = fuArr[i];
                Label lbl = lblArr[i];

                List<string> msgList;
                if (!this.VaildFileUpload(this.FileUpload1, out msgList))
                {
                    lbl.Text = string.Join("<br/>", msgList);
                    isChecked = false;
                }
            }
            if (!isChecked)
                return;
            for (var i = 0; i < fuArr.Length; i++)
            {
                System.Web.UI.WebControls.FileUpload fu = fuArr[i];

                string fileName = fu.FileName;
                string saveFolderPath = this.GetSavePath();
                string newFileName = GetNewFileName(fileName);
                string newFilePath = Path.Combine(saveFolderPath, newFileName);
                fu.SaveAs(newFilePath);
            }
        }

        private string GetNewFileName(string fileName)
        {
            //使用流水號
            //string newFileName = _fileNumber.ToString("0012300") + System.IO.Path.GetExtension(fileName);
            //_fileNumber += 1;

            //使用guid
            //string newFileName = (Guid.NewGuid()).ToString() + System.IO.Path.GetExtension(fileName);

            System.Threading.Thread.Sleep(3);
            Random random = new Random((int)DateTime.Now.Ticks);

            //使用當下時間
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssFFFFF") + "_" + random.Next(10000).ToString("00000") + System.IO.Path.GetExtension(fileName);

            return newFileName;
        }

        private bool VaildFileUpload(System.Web.UI.WebControls.FileUpload fileUpload, out List<string> errorMsg)
        {
            List<string> msgList = new List<string>();

            if (!fileUpload.HasFile)
                msgList.Add("需上傳檔案");
            else
            {
                string fileName = fileUpload.FileName;
                if (!FileHelper.ValidImageExtension(fileName))
                {
                    string fileExts = string.Join(",", FileHelper.ImageFileExtArr);
                    msgList.Add("必須是" + fileExts + "檔案格式");
                }

                //檢查檔案容量是否府合規定
                byte[] fileContent = this.FileUpload1.FileBytes;
                if (!FileHelper.ValidFileLength(fileContent))
                {
                    //this.lblMasg.Text = "檔案容量必須是" + FileHelper.UploadMB + "10MB 以內";
                    //return;
                    msgList.Add("檔案容量必須是" + FileHelper.UploadMB + "10MB 以內");
                }
            }

            //檢查檔案副檔名是否府合規定


            errorMsg = msgList;
            if (errorMsg.Count > 0)
                return false;
            else
                return true;
        }
    }
}