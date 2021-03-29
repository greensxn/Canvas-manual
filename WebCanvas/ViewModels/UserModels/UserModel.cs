using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCanvas.Models;
using WebCanvas.Models.UserModels;

namespace WebCanvas.ViewModels {
    public class UserModel : SimpleUser {

        public int ID_user { get; set; }

        [Display(Name = "Подтверждение")]
        public bool isConfirmed { get; set; }

        [Display(Name = "Почта")]
        public String Email { get; set; }

        public UserRoleModel Role { get; set; }

        public string AvaLoc { get; set; }

        public FilesModel Files { get; set; }

        [Display(Name = "Фото")]
        public IFormFile Avatar { get; set; }

        public UserModel(int ID_user, String FirstName, String LastName, String Login, String Password, String Email, bool isConfirmed, UserRoleModel Role, FilesModel Files, String AvaLoc) {
            this.ID_user = ID_user;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
            this.isConfirmed = isConfirmed;
            this.Role = Role;
            this.AvaLoc = AvaLoc;
        }

        public UserModel() {

        }

    }

    public class UserRoleModel {

        [Display(Name = "Статус")]
        public String Name { get; set; }

        [Display(Name = "Статус")]
        public RoleType RoleType { get; set; }

        public UserRoleModel() { }

        public UserRoleModel(UserRole Role) {
            switch (Role.Name) {
                case "ADMIN":
                    RoleType = RoleType.ADMIN;
                    Name = "ADMIN";
                    break;
                case "USER":
                    RoleType = RoleType.USER;
                    Name = "USER";
                    break;
                case "EDITOR":
                    RoleType = RoleType.EDITOR;
                    Name = "EDITOR";
                    break;
                default:
                    RoleType = RoleType.UNKNOWN;
                    break;
            }
        }
    }

    public class FilesModel {

        public int ID_user { get; set; }

        public IFormFile Avatar { get; set; }

        public FilesModel(UserFile files) {
            ID_user = files.ID_user;
        }

        public FilesModel() {

        }

    }
}
