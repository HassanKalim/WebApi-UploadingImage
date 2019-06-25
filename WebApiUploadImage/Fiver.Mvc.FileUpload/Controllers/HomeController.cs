using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Fiver.Mvc.FileUpload.Models.Home;
using Microsoft.Extensions.FileProviders;
using Fiver.Mvc.FileUpload.Models;

namespace Fiver.Mvc.FileUpload.Controllers
{
    /// <summary>
    /// /sf
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IFileProvider fileProvider;

        public HomeController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot", 
                        file.GetFilename());

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }











            return RedirectToAction("Files");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Content("files not selected");

            foreach (var file in files)
            {
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot", 
                        file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Files");
        }

        [HttpPost]
        //public async Task<IActionResult> UploadFileViaModel(FileInputModel model)
        //{
        //    if (model == null || 
        //        model.FileToUpload == null || model.FileToUpload.Length == 0)
        //        return Content("file not selected");

        //    var path = Path.Combine(
        //                Directory.GetCurrentDirectory(), "wwwroot",
        //                model.FileToUpload.GetFilename());

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await model.FileToUpload.CopyToAsync(stream);
        //    }

        //    return RedirectToAction("Files");
        //}

        //  , string username = "hasan", string password = "12345"
        // username == "hasan" && password == "12345" && 

        public async Task<IActionResult> UploadFileViaModel(FileInputModel model , User user)
        {

          //  User c = new User();
         

           if (/*  model.Username =="Hasan"  && model.Password == 12345 &&  */model.FileToUpload != null && model.FileToUpload.Count > 0)
            {


    //            nearByMePromotionImages
    //                {
    //                imageId:'',
                //    imageCaption: '',
                //    imagePath
                //    promotionId
                //    featuredImage
                //    createdDate
                //    updatedDate
                //}
                            List<FileDetails> imagesName = new List<FileDetails>();
                List<NearByMePromotionImage> nearByMePromotionImages = new List<NearByMePromotionImage>();
                for (int i = 0; i < model.FileToUpload.Count; i++)
                {
                    string fileName = model.FileToUpload[i].GetFilename();
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot",
                            model.FileToUpload[i].GetFilename());
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                      await  model.FileToUpload[i].CopyToAsync(stream);
                    }
                    FileDetails currentImage = new FileDetails();
                    currentImage.Name = fileName;
                    currentImage.Path = path;
                    imagesName.Add(currentImage);

                    NearByMePromotionImage tempObj = new NearByMePromotionImage();
                    tempObj.imageId = i;
                    tempObj.imagePath = currentImage.Name;
                    nearByMePromotionImages.Add(tempObj);

                }



                for (int i = 0; i < nearByMePromotionImages.Count; i++)
                {
                    for (int j = 0; j < imagesName.Count; j++)
                    {
                        if (nearByMePromotionImages[i].imagePath == imagesName[j].Name)
                        {
                            nearByMePromotionImages[i].imagePath = imagesName[j].Path;
                            
                        }
                    }
                }



                var result = nearByMePromotionImages;


            }
            else
            {
                return Content("file not selected");
            }

            return RedirectToAction("Files");
        }


        

















        public IActionResult Files()
        {
            var model = new FilesViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            return View(model);
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                { ".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}

// POST /api/v1/near-by-me-promotion/addNewPromotion
// end 