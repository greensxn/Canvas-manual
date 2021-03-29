using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebCanvas.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using WebCanvas.ViewModels;
using Microsoft.AspNetCore.Http;
using System;

namespace RolesApp.Controllers {

    [Authorize(Roles = "ADMIN")]
    public class AccountController : Controller {

        private ApplicationContext db;


        public AccountController(ApplicationContext context) {
            db = context;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() {
            return View(new PageModel() {
                Title = "Вход",
                isComments = false,
                db = db
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(PageModel model) {
            if (ModelState.IsValid) {
                UserModel userModel = db.GetUser(model.userLogInModel.Login, model.userLogInModel.Password);
                if (userModel != null && userModel.isConfirmed) {
                    HttpContext.Session.SetObject("userModel", userModel);
                    await Authenticate(userModel); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(new PageModel() {
                Title = "Вход",
                isComments = false,
                db = db
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() {
            return View(new PageModel() {
                Title = "Регистрация",
                isComments = false,
                db = db,
                Navigation = new PageNav(navigationOnPage: new NavOnPage[] {
                    new NavOnPage("Вход","Account","Login")
                })
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(PageModel model) {
            if (ModelState.IsValid) {
                User user = db.Users.Where(userDb => userDb.Login == model.userRegisterModel.Login || userDb.Email == model.userRegisterModel.Email).FirstOrDefault();
                if (user == null || (!user.isConfirmed && model.userRegisterModel.Code == 0)) {

                    await db.CreateUser(model.userRegisterModel, RoleType.USER);
                    UserModel newUser = db.GetUser(model.userRegisterModel.Login);
                  

                    if (!newUser.isConfirmed)
                        return Redirect(Url.Action("Authentication", new { Email = newUser.Email }));
                }
                else
                    ModelState.AddModelError("", "Пользователь с таким логином и(или) почтой уже существует");
            }
            return View(new PageModel() {
                Title = "Регистрация",
                isComments = false,
                db = db
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Authentication(String Email) {
            UserModel user = db.GetUserByEmail(Email);
            Authentication auth = new Authentication(db);
            auth.SendCode(user);
            return View(new PageModel() {
                Title = "Аутентификация",
                isComments = false,
                db = db,
                user = user,
                Navigation = new PageNav(navigationOnPage: new NavOnPage[] {
                    new NavOnPage("Вход","Account","Login"),
                    new NavOnPage("Регистрация","Account","Register")
                })
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication(PageModel model) {
            User user = db.Users.Where(userDb => userDb.Login == model.userRegisterModel.Login || userDb.Email == model.userRegisterModel.Email).FirstOrDefault();
            if (!user.isConfirmed && model.userRegisterModel.Code != 0) {
                Auth auth = db.Authentication.Where(a => a.ID_user == user.ID_user).FirstOrDefault();
                if (auth != null && auth.Code == model.userRegisterModel.Code) {
                    UserModel newUser = db.GetUser(model.userRegisterModel.Login);
                    await Authenticate(newUser); // аутентификация
                    await db.SetConfirmUser(newUser.ID_user);
                    await db.DeleteRepeatUsersByEmail(newUser.Email);
                    HttpContext.Session.SetObject("userModel", newUser);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Неверный код подтверждения");
            }


            return View(new PageModel() {
                Title = "Аутентификация",
                isComments = false,
                db = db
            });
        }

        [HttpGet]
        public IActionResult Create() {
            return View(new PageModel() {
                Title = "Создать",
                db = db,
                isComments = false,
                Navigation = new PageNav(navigationOnPage: new NavOnPage[] {
                    new NavOnPage("Пользователи","Account","Users")
                })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PageModel pageModel) {
            if (ModelState.IsValid) {
                User user = db.Users.Where(userDb => userDb.Login == pageModel.userCreateModel.Login).FirstOrDefault();
                if (user == null) {

                    await db.CreateUser(pageModel.userCreateModel);
                    return RedirectToAction("Users", "Account");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(new PageModel() {
                Title = "Создать",
                db = db,
                isComments = false
            });
        }

        [HttpGet]
        public IActionResult Edit(int ID_user) {
            UserModel user = db.GetUser(ID_user);
            if (user != null)
                return View(new PageModel() {
                    Title = "Редактирование",
                    user = user,
                    db = db,
                    isComments = false,
                    Navigation = new PageNav(navigationOnPage: new NavOnPage[] {
                    new NavOnPage("Пользователи","Account","Users")
                })
                });
            else
                return RedirectToAction("Users", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PageModel pageModel) {
            await db.EditUser(pageModel.userEditModel);
            UserModel userFromSession = HttpContext.Session.GetObject<UserModel>("userModel");
            if (userFromSession.ID_user == pageModel.userEditModel.ID_user) {
                UserModel newUser = db.GetUser(userFromSession.ID_user);
                HttpContext.Session.SetObject("userModel", newUser);
                await Authenticate(newUser);
            }
            return RedirectToAction("Users", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int ID_user) {
            if (HttpContext.Session.GetObject<UserModel>("userModel").ID_user == ID_user)
                await HttpContext.SignOutAsync();
            await db.DeleteUser(ID_user);
            return Redirect(Url.Action("Users"));
        }

        private async Task Authenticate(UserModel user) {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Users() {
            return View(new PageModel() {
                db = db,
                Users = new SearchForm(db).GetUsers(),
                Title = "Пользователи",
                isComments = false
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Users(SearchForm filter) {
            return View(new PageModel() {
                db = db,
                Users = new SearchForm(filter, db).GetUsers(),
                Title = "Пользователи",
                isComments = false
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetObject("userModel", null);
            return RedirectToAction("Index", "Home");
        }
    }
}