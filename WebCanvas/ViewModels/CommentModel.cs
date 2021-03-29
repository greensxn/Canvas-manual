using System;
using WebCanvas.Models;

namespace WebCanvas.ViewModels {
    public class CommentModel {

        public int ID_comment { get; set; }

        public WebPage Page { get; set; }
        public int ID_page { get; set; }

        public User User { get; set; }
        public int ID_user { get; set; }

        public DateTime Date { get; set; }

        public bool isReply { get; set; }

        public int ID_reply_comment { get; set; }

        public String Commentary { get; set; }

        public CommentModel(Comment comment, User user, WebPage page) {
            ID_comment = comment.ID_comment;
            ID_page = comment.ID_page;
            ID_user = comment.ID_user;
            Date = comment.Date;
            Commentary = comment.Commentary;
            User = user;
            Page = Page;
            isReply = comment.isReply;
            ID_reply_comment = comment.ID_reply_comment;
        }

        public CommentModel() { }
    }
}
