using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelper
    {
        public void Delete(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        //todo : check file extensions (allow extensions)
        //todo : change return type by allowed file extensions 
        public string Upload(IFormFile file, string root)
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

                string[] allowExtensions = {  };
                if (allowExtensions.FirstOrDefault(x => x.ToUpper() == extension.ToUpper()) != null)
                {
                    using (FileStream fileStream = File.Create(root + filePath))
                    {

                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        return filePath;
                    }
                }
                else return "file type is not allowed";


            }
            return "file is empty";
        }

    }
}
