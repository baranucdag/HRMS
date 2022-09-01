using Core.Utilities.Result;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelper
{
    public interface IFileHelper
    {
        ResultItem Upload(IFormFile file, string root);
        void Delete(string filePath);
    }
}
