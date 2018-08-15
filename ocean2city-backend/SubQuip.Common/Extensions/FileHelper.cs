using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Ocean2City.Common.CommonData;

namespace Ocean2City.Common.Extensions
{
    public static class FileHelper
    {
        public static FileDetails GetFileDetails(IFormFile file)
        {
            var fileModel = new FileDetails
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                Content = GetFileContent(file)
            };
            return fileModel;
        }

        public static string GetFileContent(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }
            string content = null;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                content = Convert.ToBase64String(fileBytes);
            }
            return content;
        }

        public static void SaveFile(IFormFile file, IHostingEnvironment hostingEnvironment, string folder)
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!string.IsNullOrEmpty(file.FileName))
            {
                var filePath = Path.Combine(path, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
        }

        public static void DeleteFile(string fileName, IHostingEnvironment hostingEnvironment, string folder)
        {
            string path = Path.Combine(hostingEnvironment.ContentRootPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                var filePath = Path.Combine(path, fileName);
                if (!string.IsNullOrEmpty(filePath))
                {
                    File.Delete(filePath);
                }               
            }
        }
    }
}

