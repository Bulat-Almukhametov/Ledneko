using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ledneko.WebUI.Areas.Admin.Models;
using Ledneko.WebUI.Infrastructure.Abstract;

namespace Ledneko.WebUI.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Solutions", "List"));
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Имя пользователя или пароль неверны");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

    }
}
