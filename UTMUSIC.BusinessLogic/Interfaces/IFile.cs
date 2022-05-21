using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UTMUSIC.BusinessLogic.Interfaces
{
    public interface IFile
    {
        string UploadImage(HttpPostedFileBase image, string folderPath);
        string UploadAudio(HttpPostedFileBase audio, string folderPath);
        bool ValidateImage(HttpPostedFileBase image);
        bool ValidateAudio(HttpPostedFileBase audio);
        string SaveImageOnDisk(HttpPostedFileBase image, string missingPath);
        string SaveAudioOnDisk(HttpPostedFileBase image, string folderPath);
        void DeleteFileFromDisk(string folderPath, string fileName);
        void DeleteDirFromDisk(string folderPath);

    }
}
