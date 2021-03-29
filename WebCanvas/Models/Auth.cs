using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCanvas.Models {
    public class Auth {

        [Key]
        public int ID_auth { get; set; }

        public int ID_user { get; set; }
        public User user { get; set; }

        public int Code { get; set; }


        public Auth(int ID_user) {
            this.ID_user = ID_user;
        }

        public Auth() {

        }

    }
}
