using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DeliciousMap.Helpers
{
    public class FileHelper
    {
        private static int _UploadMB = 15;
        private static int _UploadBytes = _UploadMB * 1024 * 1024;
        private static string[] _imageFileExtArr =
        {
            ".jpg",".bmp",".png"
        };

        public static string[] ImageFileExtArr
        {
            get{ return _imageFileExtArr; }
        }

        public static int UploadMB
        {
            get { return _UploadMB; }
        }

        public static bool ValidImageExtension(string fileName)
        {
            return ValidFileExtension(fileName,_imageFileExtArr);
        }

        //file signture 檔案簽章
        public static bool ValidFileExtension(string fileName, params string[] avaiExis)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            string ext = Path.GetExtension(fileName);

            if(_imageFileExtArr.Contains(ext.ToLower()))
                return true;
            else
                return false;
        }

        public static bool ValidFileLength(byte[] bytes)
        {
            if(bytes == null)
                return false;

            int fileLength = bytes.Length;

            if(fileLength > _UploadBytes)
                return false;
            else
                return true;
        }
    }
}