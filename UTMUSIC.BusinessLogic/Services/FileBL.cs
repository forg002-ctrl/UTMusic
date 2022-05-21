using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using UTMUSIC.BusinessLogic.Interfaces;
using UTMUSIC.Domain.Constants;

namespace UTMUSIC.BusinessLogic.Services
{
    public class FileBL : IFile
    {
        public string UploadImage(HttpPostedFileBase image, string folderPath)
        {
            if(image == null)
            {
                throw new ArgumentNullException("You haven't sent 'image'");
            }

            if (ValidateImage(image))
            {
                try
                {
                    return SaveImageOnDisk(image, folderPath);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public string UploadAudio(HttpPostedFileBase audio, string folderPath)
        {
            if(audio == null)
            {
                throw new ArgumentNullException("You haven't sent 'audio'");
            }

            if (ValidateAudio(audio))
            {
                try
                {
                    return SaveAudioOnDisk(audio, folderPath);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return null;
        }

        public bool ValidateImage(HttpPostedFileBase image)
        {
            string fileExtension = System.IO.Path.GetExtension(image.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((image.ContentLength > 0 && image.ContentLength < 2097152) &&
                allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }

        public bool ValidateAudio(HttpPostedFileBase audio)
        {
            string fileExtension = System.IO.Path.GetExtension(audio.FileName).ToLower();
            string[] allowedFileTypes = { ".mp3", ".mp4" };
            if ((audio.ContentLength > 0 && audio.ContentLength < 20971520) &&
                allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
 
        public string SaveImageOnDisk(HttpPostedFileBase image, string missingPath)
        {
            string folderPath = Constants.FullPath + missingPath;
            WebImage img = new WebImage(image.InputStream);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                if (missingPath.Count(c => (c == '/')) <= 2)  // If there are two or more symbols '\\' - it means, that we save an image for album
                {
                    Directory.CreateDirectory(folderPath + "/Songs");
                    Directory.CreateDirectory(folderPath + "/Albums");
                }
            }

            if (missingPath.Count(c => (c == '/')) <= 2)
            {
                img.Save(Constants.VirtualPath + missingPath + "photo");
                return Constants.VirtualPath + missingPath + "photo.jpeg";
            }

            img.Save(Constants.VirtualPath + missingPath + "front");
            return Constants.VirtualPath + missingPath + "front.jpeg";
        }

        public string SaveAudioOnDisk(HttpPostedFileBase audio, string missingPath)
        {
            audio.SaveAs(HttpContext.Current.Server.MapPath(Constants.VirtualPath + missingPath + ".mp3"));
            return Constants.VirtualPath + missingPath + ".mp3";
        }

        public void DeleteFileFromDisk(string folderPath, string fileName)
        {

            if (File.Exists(folderPath + fileName))
            {
                File.Delete(folderPath + fileName);
            }
            else
            {
                throw new Exception("There isn't any file with such path");
            }
        }

        public void DeleteDirFromDisk(string folderPath)
        {
            folderPath = folderPath.Replace("~", "");
            folderPath = folderPath.Replace('/', '\\');
            folderPath = folderPath.Replace("\\photo.jpeg", "");

            var fullPath = "C:\\Users\\vitea\\OneDrive\\Desktop\\semestr-4\\_WEB\\UTMUSIC\\UTMUSIC" + folderPath;

            if (Directory.Exists(fullPath))
            {
                DirectoryInfo di = new DirectoryInfo(fullPath);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }
                
                di.Delete();
            }
            else
            {
                throw new Exception("There isn't any file with such path");
            }
        }
    }
}
