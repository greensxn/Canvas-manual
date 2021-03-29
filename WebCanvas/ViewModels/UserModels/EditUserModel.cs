using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCanvas.Models;
using WebCanvas.Models.UserModels;

namespace WebCanvas.ViewModels.UserModels {
    public class EditUserModel : SimpleUser {

        public int ID_user { get; set; }

        public override string FirstName { get => base.FirstName; set => base.FirstName = value; }

        public override string LastName { get => base.LastName; set => base.LastName = value; }

        public override string Login { get => base.Login; set => base.Login = value; }

        public override string Password { get => base.Password; set => base.Password = value; }

        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public IFormFile Avatar { get; set; }

        public bool isDeletePhoto { get; set; }

        [Display(Name = "Статус")]
        public UserRoleModel Role { get; set; }

        public EditUserModel() {

        }

    }
}
