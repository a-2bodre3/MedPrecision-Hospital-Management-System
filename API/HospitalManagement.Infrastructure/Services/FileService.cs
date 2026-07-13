using HospitalManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string folderName)
        {
            string webRootPath = _env.WebRootPath;

            string folderPath = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            string fullFilePath = Path.Combine(folderPath, uniqueFileName);
            using (var fileStreamOutput = new FileStream(fullFilePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamOutput);
            }
            return uniqueFileName;
        }

        public void DeleteFile(string fileName, string folderName)
        {
            string fullPath = Path.Combine(_env.WebRootPath, folderName, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
