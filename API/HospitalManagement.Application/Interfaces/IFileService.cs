using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Domain.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string folderName);
        void DeleteFile(string fileName, string folderName);
    }
}
