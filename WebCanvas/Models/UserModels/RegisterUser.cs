using System.ComponentModel.DataAnnotations;
using WebCanvas.Models.UserModels;

namespace WebCanvas.Models {
    public class RegisterUser : SimpleUser {

        [Required(ErrorMessage = "Не указано имя")]
        public override string FirstName { get => base.FirstName; set => base.FirstName = value; }

        [Required(ErrorMessage = "Не указан логин")]
        public override string Login { get => base.Login; set => base.Login = value; }

        [Required(ErrorMessage = "Не указан пароль")]
        public override string Password { get => base.Password; set => base.Password = value; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

    }
}
