using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCanvas.Models.UserModels;

namespace WebCanvas.ViewModels.UserModels {
    public class RegisterUserModel : SimpleUser {

        [Required(ErrorMessage = "Не указано имя")]
        public override string FirstName { get => base.FirstName; set => base.FirstName = value; }

        [StringLength(40, ErrorMessage = "Короткий логин", MinimumLength = 5)]
        [Required(ErrorMessage = "Не указан логин")]
        public override string Login { get => base.Login; set => base.Login = value; }

        [StringLength(50, ErrorMessage = "Короткий пароль", MinimumLength = 6)]
        [Required(ErrorMessage = "Не указан пароль")]
        public override string Password { get => base.Password; set => base.Password = value; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Почта")]
        [Required(ErrorMessage = "Не указана почта")]
        public String Email { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Код подтверждения")]
        [Required(ErrorMessage = "Не верный код подтверждения")]
        public int Code { get; set; }

    }
}
