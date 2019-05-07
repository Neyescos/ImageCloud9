using AutoMapper;
using ImageCloud.Models;
using ImageCloudBLL.DTO;
using ImageCloudBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ImageCloud.Controllers
{
    public class ImageController : Controller
    {
        IService<ImageDTO> service;
        public ImageController (IService<ImageDTO> serv)
        {
            service = serv;
        }
        public ActionResult Index()
        {
            IEnumerable<ImageDTO> imageDtos = service.GetElements();
            List<ImageViewModel> l = new List<ImageViewModel>();
            foreach(var element in imageDtos)
            {
                l.Add(new ImageViewModel { ImageName = element.ImageName, ImageDate = element.ImageDate, Picture = element.Picture });
            }
            return View(l);
        }
        [HttpGet]
        public ActionResult UploadImage()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(ImageViewModel imageViewModel,HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath("~/Data/Pictures/" + fileName));
            ImageDTO uploaded = new ImageDTO { ImageName = imageViewModel.ImageName, ImageDate = DateTime.Now, Picture = Server.MapPath("~/Data/Pictures/" + fileName) };
            service.Make(uploaded);

            return RedirectToAction("Index");
        }
        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}