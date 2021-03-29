using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebCanvas.Models;
using WebCanvas.ViewModels;
using WebCanvas.ViewModels.UserModels;

namespace WebCanvas.Controllers {
    public class HomeController : Controller {

        public PageNav leftNavigation;

        public readonly ApplicationContext db;

        public HomeController(ApplicationContext db) {
            this.db = db;
        }


        [HttpGet]
        public ActionResult Index() {
            Nav[] pages = new Nav[] {
                new Nav("Тэг <canvas>", "tag",
                new NavExtra("Описание", "description"),
                new NavExtra("Синтаксис", "sintaxis"),
                new NavExtra("Атрибуты", "atributes"),
                new NavExtra("Закрывающий тег", "closedTag"))
            };
            leftNavigation = new PageNav(pages);
            return View(new PageModel { Title = "Веб-справочник", Navigation = leftNavigation, db = db, isComments = false, isMainBoxNavigation = false });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile() {
            return View(new PageModel { Title = "Профиль", db = db, isComments = false });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProfilePhoto(PageModel model) {
            if (model.userEditModel.Avatar != null) {
                await db.UploadUserPhoto(model.userEditModel.Avatar, model.userEditModel.ID_user);
                HttpContext.Session.SetObject("userModel", db.GetUser(HttpContext.Session.GetObject<UserModel>("userModel").ID_user));
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProfileInfo(PageModel model) {
            await db.EditUser(model.userEditModel);
            HttpContext.Session.SetObject("userModel", db.GetUser(HttpContext.Session.GetObject<UserModel>("userModel").ID_user));
            return RedirectToAction("Profile");
        }
    }
}