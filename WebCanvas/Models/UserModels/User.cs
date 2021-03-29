using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebCanvas.Models.UserModels;

namespace WebCanvas.Models {

    public class User : SimpleUser {

        [Key]
        public int ID_user { get; set; }

        public int ID_role { get; set; }

        [Display(Name = "Аутентификация")]
        public bool isConfirmed { get; set; }

        public UserRole Role { get; set; }

        public UserFile File { get; set; }

        public String Email { get; set; }

        public List<Comment> comments { get; set; }

        public User(String FirstName, String LastName, String Login, String Password, String Email, UserRole Role, int ID_role) {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
            this.ID_role = ID_role;
        }

        public User() { }

    }

    public class UserRole {

        [Key]
        public int ID_role { get; set; }

        [Display(Name = "Статус")]
        public String Name { get; set; }

        [Display(Name = "Статус")]
        public RoleType Role { get; set; }

        List<User> Users { get; set; }

        public UserRole() { }

        public UserRole(int ID_role, RoleType Role) {
            this.ID_role = ID_role;
            this.Role = Role;
            Name = Role.ToString("F");
        }
    }

    public class UserFile {

        [Key]
        public int ID_file { get; set; }

        public int ID_user { get; set; }
        User user { get; set; }

        [Display(Name = "Фото")]
        public String Avatar { get; set; }

        public UserFile(int ID_user, String AvatarLoc) {
            this.ID_user = ID_user;
            Avatar = AvatarLoc;
        }

        public UserFile() { }
    }

    public enum RoleType {
        USER, EDITOR, ADMIN, UNKNOWN
    }
}
