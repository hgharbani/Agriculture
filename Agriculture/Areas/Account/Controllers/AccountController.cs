using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Agriculture.Data;
using Agriculture.DomainClass.Models;
using Agriculture.Model.ViewModel;
using Agriculture.Uitlity;
using MyEshop;

namespace Agriculture.Areas.Account.Controllers
{
    public partial class AccountController : Controller
    {
        private AgricultureContext db = new AgricultureContext();
        // GET: Account/Account

        [Route("Register")]
        public virtual ActionResult Register()
        {
            var model=new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(u => u.Email == register.Email.Trim().ToLower()) )
                {
                    if(db.Humans.Any(u => u.MobileNumber == register.MobileNumber.Trim().ToLower()))
                    {
                        ModelState.AddModelError("MobileNumber", "شماره موبایل وارد شده تکراری است");
                        return View("register", register);
                    }
                    if (db.Humans.Any(u => u.NationalCode == register.NationalNumber.Trim().ToLower()))
                    {
                        ModelState.AddModelError("NationalNumber", "کد ملی وارد شده تکراری است");
                        return View("register", register);
                    }
                    
                    var human=new Human()
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        MobileNumber = register.MobileNumber,
                        NationalCode = register.NationalNumber,
                        Users = new List<User>()
                    };
                    User user = new User()
                    {
                        Email = register.Email.Trim().ToLower(),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        ActiveCode = Guid.NewGuid().ToString(),
                        Active = false,
                        RegisterDate = DateTime.Now,
                        RoleId = 1,
                        UserName = register.UserName
                    };
                    human.Users.Add(user);

                    db.Humans.Add(human);

                    db.SaveChanges();

                    //Send Active Email
                    string body = PartialToStringClass.RenderPartialView("Account", "ActiviationEmail", user);
                    SendEmail.Send(user.Email, "ایمیل فعالسازی", body);
                    //End Send Active Email

                    return View("SuccessRegister", user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
                }
            }

            return View("register",register);
        }


        [Route("Login")]
        public virtual ActionResult Login()
        {
            LoginViewModel login=new LoginViewModel();
            return View(login);
        }

        [HttpPost]
        [Route("Login")]
        public virtual ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                var user = db.Users.SingleOrDefault(u => u.Email == login.Email && u.Password == hashPassword);
                if (user != null)
                {
                    if (user.Active)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememmberMe);
                        if (user.Role.RoleId == 1)
                        { return RedirectToAction(@MVC.Admin.Dashboard.Actions.Index());
                        }
                        return Redirect("/");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با اطلاعات وارد شده یافت نشد");
                }
            }

            return View(login);
        }

        public virtual ActionResult ActiveUser(string id)
        {
            var user = db.Users.SingleOrDefault(u => u.ActiveCode == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.Active = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            db.SaveChanges();
            ViewBag.UserName = user.UserName;
            return View();
        }
        public virtual ActionResult ActiviationEmail()
        {
            return PartialView();
        }

        public virtual ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}