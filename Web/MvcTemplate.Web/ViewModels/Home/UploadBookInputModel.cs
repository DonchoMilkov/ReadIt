﻿namespace MvcTemplate.Web.ViewModels.Home
{
    using System.Web;

    public class UploadBookInputModel
    {
        public string Category { get; set; }

        public string Language { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}
