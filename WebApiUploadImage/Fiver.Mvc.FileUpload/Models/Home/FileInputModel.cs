using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Fiver.Mvc.FileUpload.Models.Home
{
    public class FileInputModel
    {
        public string Username { get; set; }
        public int Password { get; set; }
        public List<IFormFile> FileToUpload { get; set; }


    }
}
