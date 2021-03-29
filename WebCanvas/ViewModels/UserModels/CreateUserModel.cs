using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using WebCanvas.Models.UserModels;

namespace WebCanvas.ViewModels.UserModels {
    public class CreateUserModel : SimpleUser {

        [Required]
        public override string Login { get => base.Login; set => base.Login = value; }

        [Required]
        public override string Password { get => base.Password; set => base.Password = value; }

        public String Email { get; set; }

        [Required]
        public override string FirstName { get => base.FirstName; set => base.FirstName = value; }

        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public IFormFile Avatar { get; set; }

        public UserRoleModel Role { get; set; }


    }
}
