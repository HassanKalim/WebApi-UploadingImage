using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Fiver.Mvc.FileUpload.Models.Home
{
    public class FileInputModel
    {
        public List<IFormFile> FileToUpload { get; set; }
    }
}
