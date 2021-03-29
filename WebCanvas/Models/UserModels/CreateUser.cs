using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebCanvas.Models.UserModels {
    public class CreateUser : SimpleUser {

        [Required]
        public override string Login { get => base.Login; set => base.Login = value; }

        [Required]
        public override string Password { get => base.Password; set => base.Password = value; }

        [Required]
        public override string FirstName { get => base.FirstName; set => base.FirstName = value; }

        public IFormFile Avatar{ get; set; }

        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public int RoleID { get; set; }
    }
}
