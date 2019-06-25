﻿using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Fiver.Mvc.FileUpload.Models.Home
{
    public static class IFormFileExtensions
    {
        public static string GetFilename(this IFormFile file)
        {
            string fileName = ContentDispositionHeaderValue.Parse(
                            file.ContentDisposition).FileName.ToString().Trim('"');


            fileName = fileName.Remove(fileName.Length - 4, 4) + "_u_" + Guid.NewGuid() + ".jpg";
            return fileName;
        }


        public static async Task<MemoryStream> GetFileStream(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream;
        }

        public static async Task<byte[]> GetFileArray(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream.ToArray();
        }
    }
}
