using Core.Utilities.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelper : IFileHelper
    {
        //C:\Users\90544\source\repos\HRMS\WebAPI\wwwroot\Uploads\cv\004e9022-701c-4304-95a7-3be5c2d56c39.pdf
        public void Delete(string filePath)
        {
            string fullPath = "wwwroot\\Uploads\\cv\\" + filePath;
            try
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        //todo : check file extensions (allow extensions)
        //todo : change return type by allowed file extensions 
        public ResultItem Upload(IFormFile file, string root)
        {

            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(file.FileName);
                string guid = Guid.NewGuid().ToString();
                string filePath = guid + extension;

                string[] allowExtensions = { ".pdf", ".word" };
                if (allowExtensions.FirstOrDefault(x => x.ToUpper() == extension.ToUpper()) != null)
                {
                    using (FileStream fileStream = File.Create(root + filePath))
                    {

                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        return new ResultItem(true, filePath, "file added succesfully");
                    }
                }
                else return new ResultItem(false, null, "file type is not allowed");

            }
            return new ResultItem(false, null, "file cannot be null");
        }

    }
}
