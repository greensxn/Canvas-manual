using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCanvas.Models {
    public class WebPage {

        [Key]
        public int ID_page { get; set; }

        public List<Comment> comments { get; set; }

        public String pageName { get; set; }

        public WebPage(int ID_page, String pageName) {
            this.ID_page = ID_page;
            this.pageName = pageName;
        }

        public WebPage() {

        }

    }

    public class Comment {

        [Key]
        public int ID_comment { get; set; }

        public WebPage Page { get; set; }
        public int ID_page { get; set; }

        public User User { get; set; }
        public int ID_user { get; set; }

        public DateTime Date { get; set; }

        public bool isReply { get; set; }

        public int ID_reply_comment { get; set; }

        public String Commentary { get; set; }
    }

}
