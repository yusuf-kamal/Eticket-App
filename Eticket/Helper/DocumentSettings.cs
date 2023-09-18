using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Eticket.Helper
{
    public class DocumentSettings
    {
       
        public static string UploadFile(IFormFile file, string folderName)
            {
            //C:\Users\dell\Desktop\Demo.Pl\Demo.Pl\wwwroot\Images\

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file?.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file?.CopyTo(fileStream);
            return fileName;


        }
        public static void DeletFile(string folderName, string fileName)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",folderName,fileName);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
     }
}
