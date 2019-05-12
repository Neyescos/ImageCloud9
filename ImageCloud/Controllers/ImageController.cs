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
                l.Add(new ImageViewModel { Id = element.Id, ImageName = element.ImageName, ImageDate = element.ImageDate, Picture = element.Picture });
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
            ImageDTO uploaded = new ImageDTO { ImageName = imageViewModel.ImageName, ImageDate = DateTime.Now, Picture = "~/Data/Pictures/" + fileName };
            service.Make(uploaded);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int?id)
        {
            var im = service.GetElement(id);
            ImageViewModel image = new ImageViewModel { Id = im.Id, ImageDate= im.ImageDate, ImageName = im.ImageName, Picture = im.Picture, UserId = im.UserId };
            return View(image);
        }
        [HttpPost]
        public ActionResult Delete(ImageViewModel imageViewModel)
        {

            service.Delete(new ImageDTO { Id = imageViewModel.Id, ImageDate = imageViewModel.ImageDate, ImageName = imageViewModel.ImageName, Picture = imageViewModel.Picture, UserId = imageViewModel.UserId });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int?id)
        {
            var im = service.GetElement(id);
            return View(new ImageViewModel { Id = im.Id, ImageDate = im.ImageDate, ImageName = im.ImageName, Picture = im.Picture, UserId = im.UserId });
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