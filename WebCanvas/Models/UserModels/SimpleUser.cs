using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCanvas.Models.UserModels {
    public class SimpleUser {

        [Display(Name = "Логин")]
        public virtual string Login { get; set; }

        [Display(Name = "Пароль")]
        public virtual string Password { get; set; }

        [Display(Name = "Имя")]
        public virtual string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public virtual string LastName { get; set; }

    }
}
