using AutoMapper;
using ImageCloud.Models;
using ImageCloud.Variables;
using ImageCloudBLL.DTO;
using ImageCloudBLL.Interfaces;
using ImageCloudDAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ImageCloud.Controllers
{
    public class UserController : Controller
    {
        IService<UserDTO> service;

        public UserController(IService<UserDTO> serv)
        {
            service = serv;
        }
        public ActionResult Index()
        {
            IEnumerable<UserDTO> Users = service.GetElements();

            List<UserViewModel> users = new List<UserViewModel>();
            foreach (var el in Users)
            {
                users.Add(new UserViewModel { Email = el.Email, Id = el.Id, IsBanned = el.IsBanned, IsEmailVerified = el.IsEmailVerified, Password = el.Password, UserName = el.UserName, UserRole = el.UserRole });
            }
            return View(users);
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateUser(UserViewModel userViewModel)
        {
            bool f(User m) { return m.Email == userViewModel.Email; };
            UserDTO UserFormData = new UserDTO();
            //
            //validation regex block
            //
            Regex regexPassword = new Regex(@"(\w{7,})");
            Regex regexUser = new Regex(@"\w{1,20}");
            Regex regexEmail = new Regex(@"(\w{6,}@)");

            UserFormData = service.Find(f);
            if (UserFormData!=null)
            {
                if (userViewModel.Email == UserFormData.Email)
                {
                    ModelState.AddModelError("Email", "На данную почту уже зарегистрирован аккаунт ImageCloud");
                }

                bool f1(User m) { return m.UserName == userViewModel.UserName; };
                var el1 = service.Find(f1);
                if (el1 != null && userViewModel.UserName == el1.UserName)
                {
                    ModelState.AddModelError("UserName", "Пользователь с таким ником уже зарегистрирован");
                }
            }

                
                if (userViewModel.UserName == null || !regexUser.IsMatch(userViewModel.UserName))
                {
                    ModelState.AddModelError("UserName", "Введите ник или вЫберите имя покороче");
                }

                if (userViewModel.Password == null || !regexPassword.IsMatch(userViewModel.Password))
                {
                    ModelState.AddModelError("Password", "Длина пароля должна быть больше 8 символов ");
                }
              

                if (userViewModel.Email == null || !regexEmail.IsMatch(userViewModel.Email))
                {
                    ModelState.AddModelError("Email", "Введите адрес электронной почты ");
                }
                
            
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO { UserRole = "Simple User", Email = userViewModel.Email, Id = userViewModel.Id, IsBanned = userViewModel.IsBanned, IsEmailVerified = userViewModel.IsEmailVerified, Password = userViewModel.Password, UserName = userViewModel.UserName };
                service.Make(user);
                return RedirectToAction("Index");
            }
            ViewBag.Messege = "Запрос не прошел валидацию";
            return View(userViewModel);
        }
        [HttpGet]
        public ActionResult Delete(int?id)
        {
           var el = service.GetElement(id);
            return View(new UserViewModel { Email = el.Email, Id = el.Id, IsBanned =el.IsBanned, IsEmailVerified = el.IsEmailVerified, Password = el.Password, UserName = el.UserName, UserRole = el.UserRole });
        }
        [HttpPost]
        public ActionResult Delete(UserViewModel user)
        {
            service.Delete(new UserDTO { Email = user.Email, Id = user.Id, IsBanned = user.IsBanned, IsEmailVerified = user.IsEmailVerified, Password = user.Password, UserName = user.UserName, UserRole = user.UserRole });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int?id)
        {
            var el = service.GetElement(id);
            return View(new UserViewModel { Email = el.Email, Id = el.Id, IsBanned = el.IsBanned, UserRole = el.UserRole, UserName = el.UserName });
        }
        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            var el = service.GetElement(user.Id);
            service.Change(new UserDTO { UserName = user.UserName, UserRole = user.UserRole, IsEmailVerified = el.IsEmailVerified, Email = el.Email, Id = user.Id, IsBanned = user.IsBanned, Password = el.Password });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(UserViewModel user)
        {
            
            if (user.Email == null)
            {
                ModelState.AddModelError("Email", "Введите эллектронную почту");
            }

            bool f(User m) { return m.Email == user.Email && m.UserPassword == user.Password; };
            var el = service.Find(f);
            if(el ==null)
            {
                ModelState.AddModelError("Email", "Неправильно введена эллектронная почта или пароль");
            }

            if (el != null && el.IsEmailVerified == false)
            {
                ModelState.AddModelError("IsEmailVerified", "Ваша почта не подтверждена");
            }
            if (user.IsBanned == true)
            {
                ModelState.AddModelError("IsBanned", "Вход на этот сайт данному пользователю был запрещен");
            }
            if (ModelState.IsValid)
            {
                Variables.Variables.CurrentUser = new UserV { Email = el.Email, Id = el.Id, Password = el.Password, IsBanned = el.IsBanned, IsEmailVerified = el.IsEmailVerified, UserName = el.UserName, UserRole = el.UserRole };
                return RedirectToAction("Index","Image");
            }
            else
            {
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult ConfirmEmail(int?id)
        {
            var el = service.GetElement(id);
            el.IsEmailVerified = true;
            service.Change(el);
            Variables.Variables.CurrentUser = new UserV { IsEmailVerified = el.IsEmailVerified, Email = el.Email, Id = el.Id, IsBanned = el.IsBanned, Password = el.Password, UserName = el.UserName, UserRole = el.UserRole };
            return RedirectToAction("Index","Image");
        }
        [HttpGet]
        public ActionResult SignOut()
        {
            Variables.Variables.CurrentUser = null;
            return RedirectToAction("SignIn");
        }
    }
}