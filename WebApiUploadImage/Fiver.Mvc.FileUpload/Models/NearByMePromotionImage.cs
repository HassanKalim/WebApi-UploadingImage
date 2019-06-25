﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiver.Mvc.FileUpload.Models
{
    public class NearByMePromotionImage
    {
        public Int64 imageId { get; set; }
        public string imageCaption { get; set; }
        public string imagePath { get; set; }
        public int promotionId { get; set; }
        public Boolean featuredImage { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
