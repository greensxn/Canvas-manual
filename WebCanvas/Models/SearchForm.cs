using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCanvas.ViewModels;

namespace WebCanvas.Models {
    public class SearchForm {
        public String FNAME { get; set; }
        public String LNAME { get; set; }
        private ApplicationContext Db { get; }

        public SearchForm() {
            FNAME = "";
            LNAME = "";
        }

        public SearchForm(SearchForm filter, ApplicationContext Db) {
            FNAME = filter.FNAME;
            LNAME = filter.LNAME;
            this.Db = Db;
        }

        public SearchForm(String FNAME, String LNAME) {
            this.FNAME = FNAME;
            this.LNAME = LNAME;
        }

        public SearchForm(ApplicationContext db) => Db = db;

        public List<UserModel> GetUsers() => Db.GetUsers(this);
    }
}
