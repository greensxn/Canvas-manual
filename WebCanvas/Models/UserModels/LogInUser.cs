using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCanvas.Models.UserModels {
    public class LogInUser : SimpleUser {

        [Required(ErrorMessage = "Не указан логин")]
        public override string Login { get => base.Login; set => base.Login = value; }

        [Required(ErrorMessage = "Не указан пароль")]
        public override string Password { get => base.Password; set => base.Password = value; }

       

    }
}
