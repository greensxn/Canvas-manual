using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using WebCanvas.Models.UserModels;
using WebCanvas.ViewModels;
using WebCanvas.ViewModels.UserModels;

namespace WebCanvas.Models {
    public class PageModel {

        public String Title { get; set; }

        public bool isComments { get; set; } = true;

        public bool isMainBoxNavigation { get; set; } = true;

        public bool isPageCounter { get; set; }

        public UserModel user { get; set; }

        public RegisterUserModel userRegisterModel { get; set; }

        public LogInUser userLogInModel { get; set; }

        public CreateUserModel userCreateModel { get; set; }

        public EditUserModel userEditModel { get; set; }

        public PageNav Navigation { get; set; }

        public ApplicationContext db { get; set; }

        public WebPage Page { get; set; }

        public CommentModel newComment { get; set; }

        public List<CommentModel> comments { get; set; }
        public List<UserModel> Users { get; set; }

        public Color pageColor { get; set; } = Color.White;

        public PageModel() {

        }

    }
}
