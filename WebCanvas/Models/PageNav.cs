using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCanvas.Models {
    public class PageNav {

        public Nav[] pages { get; set; }
        public NavOnPage[] navigationOnPage { get; set; }

        public PageNav(Nav[] pages = null, NavOnPage[] navigationOnPage = null) {
            this.pages = pages;
            this.navigationOnPage = navigationOnPage;
        }

        public PageNav() { }

    }

    public class Nav {

        public String name { get; set; }
        public String link { get; set; }
        public bool isComment { get; set; }
        public NavExtra[] extra { get; set; }

        public Nav(String name, String link, params NavExtra[] extra) {
            this.name = name;
            this.link = link;
            this.extra = extra;
        }

        public Nav(String name, String link, bool isComment = false) {
            this.name = name;
            this.link = link;
            this.isComment = isComment;
        }

        public Nav() { }

    }

    public class NavExtra : Nav {

        public NavExtra(String name, String link) : base(name, link) { }
        public NavExtra() { }

    }

    public class NavOnPage {

        public String Name { get; set; }
        public String Controller { get; set; }
        public String Page { get; set; }

        public NavOnPage(String Name, String Controller, String Page) {
            this.Name = Name;
            this.Controller = Controller;
            this.Page = Page;
        }
        public NavOnPage() { }

    }
}
