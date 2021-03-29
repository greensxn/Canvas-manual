using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCanvas.Models;
using WebCanvas.ViewModels;

namespace WebCanvas.Controllers {

    [AllowAnonymous]
    public class PagesController : Controller {

        public ApplicationContext db { get; }
        private Dictionary<String, String> DirPage { get; }
        public PageNav leftNavigation { get; set; }

        public PagesController(ApplicationContext db) {
            this.db = db;
            List<WebPage> pages = db.Pages.ToList();
            DirPage = new Dictionary<string, string> {
                { pages[0].pageName, "usingCanvas" },
                { pages[1].pageName, "figureDraw" },
                { pages[2].pageName, "colorAndStyle" },
                { pages[3].pageName, "attributes" },
                { pages[4].pageName, "events" }
            };
        }

        [HttpGet]
        public IActionResult usingCanvas() {
            Nav[] pages = new Nav[] {
                new Nav("Элемент <canvas>", "element",
                new NavExtra("Запасное содержимое", "content"),
                new NavExtra("Требуется тег </canvas>", "tagneedless")),

                new Nav("Рендеринг содержимого (контекста)", "rendering"),

                new Nav("Простой пример", "example"),

                new Nav("Пример часов", "example2")
            };

            leftNavigation = new PageNav(pages);
            return View(new PageModel {
                Navigation = leftNavigation,
                isPageCounter = true,
                db = db,
                Title = "Базовое использование",
                pageColor = Color.FromArgb(63, 190, 0)
            });
        }

        [HttpGet]
        public IActionResult figureDraw() {
            Nav[] pages = new Nav[] {
                new Nav("Сетка", "cell"),

                new Nav("Контуры (path)", "path",
                new NavExtra("Рисование линий", "lineDraw"),
                new NavExtra("Рисование треугольника", "triangleDraw"),
                new NavExtra("Рисование дуг и окружностей", "circleDraw"),
                new NavExtra("Рисование треугольника", "triangleDraw"),
                new NavExtra("Рисование прямоугольника", "rectangleDraw"),
                new NavExtra("Передвижение пера", "penMove")),

                new Nav("Кривые Безье", "bezie",
                new NavExtra("Квадратичные кривые Безье", "bezieSquare"),
                new NavExtra("Кубические кривые Безье", "bezieCube")),

                new Nav("Path2D объекты", "path2D",
                new NavExtra("Path2D пример", "path2DEx"))
            };
            leftNavigation = new PageNav(pages);
            return View(new PageModel {
                Navigation = leftNavigation,
                isPageCounter = true,
                db = db,
                Title = "Рисование фигур с помощью canvas",
                pageColor = Color.FromArgb(153, 0, 85)
            });
        }


        [HttpGet]
        public IActionResult colorAndStyle() {
            Nav[] pages = new Nav[] {
                new Nav("Цвета", "colors",
                new NavExtra("Пример fillStyle", "fillStyle"),
                new NavExtra("Пример strokeStyle", "strokeStyle")),

                new Nav("Прозрачность", "opacity",
                new NavExtra("Пример globalAlpha","globalAlpha"),
                new NavExtra("Пример использования rgba()","rgba")),

                new Nav("Стили линий", "lineStyle",
                new NavExtra("Пример lineWidth", "exampleLineWidth"),
                new NavExtra("Пример lineCap", "exampleLineCap"),
                new NavExtra("Пример lineJoin", "exampleLineJoin"))
            };
            leftNavigation = new PageNav(pages);
            return View(new PageModel {
                Navigation = leftNavigation,
                isPageCounter = true,
                db = db,
                Title = "Стили и цвета",
                pageColor = Color.FromArgb(182, 16, 57)
            });
        }

        [HttpGet]
        public IActionResult attributes() {
            Nav[] pages = new Nav[] {
                new Nav("Универсальные атрибуты", "attributes")
            };
            leftNavigation = new PageNav(pages);
            return View(new PageModel {
                Navigation = leftNavigation,
                isPageCounter = true,
                db = db, Title = "Атрибуты",
                pageColor = Color.FromArgb(0, 136, 190)
            });
        }

        [HttpGet]
        public IActionResult events() {
            Nav[] pages = new Nav[] {
                new Nav("События", "events")
            };
            leftNavigation = new PageNav(pages);
            return View(new PageModel {
                Navigation = leftNavigation,
                isPageCounter = true,
                db = db, Title = "События",
                pageColor = Color.FromArgb(255, 140, 58)
            });
        }

        [HttpGet]
        public IActionResult comments(String page) {
            KeyValuePair<string, string> value = DirPage.Where(a => a.Key == page).FirstOrDefault();

            if (value.Key != null) {
                return View(new PageModel {
                    Navigation = new PageNav(navigationOnPage: new NavOnPage[] {
                    new NavOnPage(page, "Pages", DirPage[page])
                }),
                    db = db,
                    Title = "Комментарии",
                    isComments = false
                });
            }
            else
                return Redirect(Url.Action("Index", "Home"));
        }


        [HttpPost]
        public async Task<IActionResult> OnComment(PageModel model) {
            String pageForReditect = db.Pages.Where(p => p.ID_page == model.newComment.ID_page).FirstOrDefault().pageName;
            if (String.IsNullOrWhiteSpace(model.newComment.Commentary))
                goto CheckPoint;

            await db.NewComment(model.newComment);

        CheckPoint:
            if (model.Navigation != null && model.Navigation.navigationOnPage?[0].Name != null) {
                NavOnPage previousPage = model.Navigation.navigationOnPage[0];
                return RedirectToAction("comments", new { page = previousPage.Name });
            }
            return Redirect(Url.Action(DirPage[pageForReditect], "Pages") + "#footer");
        }

        [HttpPost]
        public async Task<IActionResult> OnDeleteComment(PageModel model) {
            String pageForReditect = db.Pages.Where(p => p.ID_page == model.newComment.ID_page).FirstOrDefault().pageName;

            await db.DeleteComment(model.newComment.ID_comment);

            if (model.Navigation != null && model.Navigation.navigationOnPage?[0].Name != null) {
                NavOnPage previousPage = model.Navigation.navigationOnPage[0];
                return RedirectToAction("comments", new { page = previousPage.Name });
            }
            return Redirect(Url.Action(DirPage[pageForReditect], "Pages") + "#footer");
        }
    }
}