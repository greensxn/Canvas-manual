using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCanvas.Models.UserModels {
    public class EditUser : SimpleUser {

        public int ID { get; set; }

        public override string FirstName { get => base.FirstName; set => base.FirstName = value; }

        public override string LastName { get => base.LastName; set => base.LastName = value; }

        public override string Login { get => base.Login; set => base.Login = value; }

        public override string Password { get => base.Password; set => base.Password = value; }

        //[DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public int RoleID { get; set; }

        [Display(Name = "Статус")]
        public UserRole Role { get; set; }

    }
}
