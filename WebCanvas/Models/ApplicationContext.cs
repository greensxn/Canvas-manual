using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCanvas.Models.UserModels;
using WebCanvas.ViewModels;
using WebCanvas.ViewModels.UserModels;

namespace WebCanvas.Models {
    public class ApplicationContext : DbContext {

        public DbSet<User> Users { get; set; }

        public DbSet<UserFile> Files { get; set; }

        public DbSet<UserRole> Roles { get; set; }

        public DbSet<WebPage> Pages { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Auth> Authentication { get; set; }

        public readonly IHostingEnvironment HostingEnv;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHostingEnvironment hostingEnv) : base(options) {
            Database.EnsureCreated();
            HostingEnv = hostingEnv;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // добавляем роли
            RoleType adminRoleName = RoleType.ADMIN;
            RoleType editorRoleName = RoleType.EDITOR;
            RoleType userRoleName = RoleType.USER;

            UserRole userRole = new UserRole(1, userRoleName);
            UserRole editorRole = new UserRole(2, editorRoleName);
            UserRole adminRole = new UserRole(3, adminRoleName);

            // добавляем клиента
            string userLogin = "user";
            string userPassword = "userp";
            string userFirstName = "Дима";
            string userLastName = "Богданов";
            string userEmail = "dmytro.bohdanov@nure.ua";
            User user = new User(userFirstName, userLastName, userLogin, userPassword, userEmail, userRole, userRole.ID_role);
            user.isConfirmed = true;
            user.ID_user = 2;
            user.ID_role = userRole.ID_role;

            // добавляем менеджера
            string editorLogin = "editor";
            string editorPassword = "editorp";
            string editorFirstName = "Bogdan";
            string editorLastName = "Dimoff";
            string editorEmail = "dmytro.bohdanov@nure.ua";
            User editor = new User(editorFirstName, editorLastName, editorLogin, editorPassword, editorEmail, editorRole, editorRole.ID_role);
            editor.isConfirmed = true;
            editor.ID_user = 3;
            editor.ID_role = editorRole.ID_role;

            // добавляем админа
            string adminLogin = "admin";
            string adminPassword = "adminp";
            string adminFirstName = "Богдан";
            string adminLastName = "Димов";
            string adminEmail = "dmytro.bohdanov@nure.ua";
            User admin = new User(adminFirstName, adminLastName, adminLogin, adminPassword, adminEmail, adminRole, adminRole.ID_role);
            admin.isConfirmed = true;
            admin.ID_user = 1;
            admin.ID_role = adminRole.ID_role;

            // добавляем названия для страниц
            WebPage page1 = new WebPage(1, "Базовое использование");
            WebPage page2 = new WebPage(2, "Рисование фигур с помощью canvas");
            WebPage page3 = new WebPage(3, "Стили и цвета");
            WebPage page4 = new WebPage(4, "Атрибуты");
            WebPage page5 = new WebPage(5, "События");

            WebPage page6 = new WebPage(6, "Комментарии");
            WebPage page7 = new WebPage(7, "Главная");
            WebPage page8 = new WebPage(8, "Вход");
            WebPage page9 = new WebPage(9, "Регистрация");
            WebPage page10 = new WebPage(10, "Профиль");
            WebPage page11 = new WebPage(11, "Редактирование");
            WebPage page12 = new WebPage(12, "Создать");
            WebPage page13 = new WebPage(13, "Пользователи");

            modelBuilder.Entity<UserRole>().HasData(new UserRole[] { adminRole, editorRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { admin, editor, user });
            modelBuilder.Entity<WebPage>().HasData(new WebPage[] { page1, page2, page3, page4, page5, page6, page7, page8, page9, page10, page11, page12, page13 });
            base.OnModelCreating(modelBuilder);
        }

        public UserModel GetUser(String Login) {
            User userDB = Users.Where(userDb => userDb.Login == Login).FirstOrDefault();
            return ConvertUserToModel(userDB);
        }


        public List<UserModel> GetUsers(SearchForm filter) {
            List<User> users = Users.ToList();
            if (!String.IsNullOrEmpty(filter.FNAME)) 
                users = users.Where(u => u.FirstName == filter.FNAME).ToList();
            if (!String.IsNullOrEmpty(filter.LNAME)) 
                users = users.Where(u => u.LastName == filter.LNAME).ToList();
            List<UserModel> UsersModel = new List<UserModel>();
            foreach (User userDB in users)
                UsersModel.Add(ConvertUserToModel(userDB));
            return UsersModel;
        }


        public UserModel GetUserByEmail(String Email) {
            User userDB = Users.Where(userDb => userDb.Email == Email).FirstOrDefault();
            return ConvertUserToModel(userDB);
        }

        public UserModel GetUser(String Login, String Password) {
            User userDB = Users.Where(userDb => userDb.Login == Login && userDb.Password == Password).FirstOrDefault();
            if (userDB != null && Password == userDB.Password)
                return ConvertUserToModel(userDB);
            else return null;
        }

        public UserModel GetUser(int ID) {
            User userDB = Users.Where(userDb => userDb.ID_user == ID).FirstOrDefault();
            return ConvertUserToModel(userDB);
        }

        private UserModel ConvertUserToModel(User userDB) {
            if (userDB != null) {
                UserRole Role = Roles.Where(r => r.ID_role == userDB.ID_role).FirstOrDefault();
                UserRoleModel roleModel = new UserRoleModel(Role);
                UserFile Ava = Files.Where(ava => ava.ID_user == userDB.ID_user).FirstOrDefault();
                FilesModel files = null;

                if (Ava != null)
                    files = new FilesModel(Ava);

                String AvaLoc = Ava == null ? "\\lib\\userDoc\\user.png" : "\\lib\\userDoc\\" + Ava.Avatar;

                UserModel userModel = new UserModel(
                    userDB.ID_user,
                    userDB.FirstName,
                    userDB.LastName,
                    userDB.Login,
                    userDB.Password,
                    userDB.Email,
                    userDB.isConfirmed,
                    roleModel,
                    files,
                    AvaLoc
                    );

                return userModel;
            }
            return null;
        }

        public async Task CreateUser(CreateUserModel user) {
            UserRole userRole = Roles.Where(r => r.Name == user.Role.Name).FirstOrDefault();
            User userCreate = new User(user.FirstName, user.LastName, user.Login, user.Password, user.Email, userRole, userRole.ID_role);
            userCreate.isConfirmed = true;
            await Users.AddAsync(userCreate);

            await SaveChangesAsync();

            if (user.Avatar != null)
                await UploadUserPhoto(user.Avatar, GetUser(user.Login).ID_user);

            await SaveChangesAsync();
        }

        public async Task CreateUser(RegisterUserModel user, RoleType Role) {
            UserRole userRole = Roles.Where(r => r.Name == Role.ToString()).FirstOrDefault();
            User userCreate = new User(user.FirstName, user.LastName, user.Login, user.Password, user.Email, userRole, userRole.ID_role);
            await Users.AddAsync(userCreate);
            await SaveChangesAsync();

            UserModel registeredUser = GetUser(userCreate.Login);
            if (Authentication.Where(a => a.ID_user == registeredUser.ID_user).FirstOrDefault() == null)
                await Authentication.AddAsync(new Auth(registeredUser.ID_user));

            await SaveChangesAsync();
        }

        public async Task EditUser(EditUserModel editedUser) {
            User userDb = await Users.FindAsync(editedUser.ID_user);
            userDb.FirstName = editedUser.FirstName == null ? userDb.FirstName : editedUser.FirstName;
            userDb.LastName = editedUser.LastName == null ? "" : editedUser.LastName;
            userDb.Login = editedUser.Login == null ? userDb.Login : editedUser.Login;
            userDb.Password = editedUser.Password == null ? userDb.Password : editedUser.Password;
            if (editedUser.Role != null)
                userDb.ID_role = Roles.Where(r => r.Name == editedUser.Role.Name).FirstOrDefault().ID_role;
            if (editedUser.Avatar != null)
                await UploadUserPhoto(editedUser.Avatar, editedUser.ID_user);
            else if (editedUser.isDeletePhoto)
                await DeleteUserPhoto(editedUser.ID_user);

            await SaveChangesAsync();
        }

        public async Task DeleteUser(int ID_user) {
            User userDelete = Users.Where(userDb => userDb.ID_user == ID_user).FirstOrDefault();
            await DeleteUserPhoto(userDelete.ID_user);
            Users.Remove(userDelete);
            await SaveChangesAsync();
        }

        public async Task UploadUserPhoto(IFormFile file, int userID) {
            String uniqueFileName = String.Empty;
            String filePath = String.Empty;
            String uploadsFolder = String.Empty;
            if (file != null) {
                uploadsFolder = Path.Combine(HostingEnv.WebRootPath, "lib\\userDoc");
                uniqueFileName = Guid.NewGuid().ToString() + $"_{file.FileName}";
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    file.CopyTo(fs);
            }

            await DeleteUserPhoto(userID);

            UserFile newFile = new UserFile();
            newFile.ID_user = userID;
            newFile.Avatar = uniqueFileName;
            Files.Add(newFile);

            await SaveChangesAsync();
        }

        public async Task DeleteUserPhoto(int userID) {
            UserFile oldFile = Files.Where(f => f.ID_user == userID && f.Avatar != null).FirstOrDefault();
            if (oldFile != null) {
                String uploadsFolder = Path.Combine(HostingEnv.WebRootPath, "lib\\userDoc");
                String fileDelete = uploadsFolder + "\\" + oldFile.Avatar;
                Files.Remove(oldFile);
                File.Delete(fileDelete);
                await SaveChangesAsync();
            }
        }

        public List<CommentModel> GetComments(int ID_page) {
            List<Comment> commentsTemp = Comments.Where(c => c.ID_page == ID_page).ToList();
            List<CommentModel> commentsModel = new List<CommentModel>();
            foreach (Comment comment in commentsTemp)
                commentsModel.Add(ConvertCommentToModel(comment));
            return commentsModel;
        }

        public List<CommentModel> GetReplyComments(int ID_comment) {
            List<Comment> replyCommentsTemp = Comments.Where(c => c.ID_reply_comment == ID_comment).ToList();
            List<CommentModel> replyCommentsModel = new List<CommentModel>();
            foreach (Comment replyComment in replyCommentsTemp)
                replyCommentsModel.Add(ConvertCommentToModel(replyComment));
            return replyCommentsModel;
        }

        public List<CommentModel> GetComments(String pageName) {
            WebPage page = Pages.Where(p => p.pageName == pageName).FirstOrDefault();
            if (page != null) {
                List<Comment> commentsTemp = Comments.Where(c => c.ID_page == page.ID_page).ToList();
                List<CommentModel> commentsModel = new List<CommentModel>();
                foreach (Comment comment in commentsTemp)
                    commentsModel.Add(ConvertCommentToModel(comment));
                return commentsModel;
            }
            return null;
        }

        public WebPage GetPage(String pageName) {
            WebPage page = Pages.Where(p => p.pageName == pageName).FirstOrDefault();
            if (page != null)
                return page;
            return null;
        }

        public async Task NewComment(CommentModel comment) {
            if (!String.IsNullOrWhiteSpace(comment.Commentary)) {
                Comments.Add(new Comment {
                    ID_user = comment.ID_user,
                    ID_page = comment.ID_page,
                    Commentary = comment.Commentary,
                    Date = comment.Date,
                    isReply = comment.isReply,
                    ID_reply_comment = comment.ID_reply_comment
                });
                await SaveChangesAsync();
            }
        }

        public async Task DeleteComment(int ID_comment) {
            Comment comment = Comments.Where(c => c.ID_comment == ID_comment).FirstOrDefault();
            if (comment != null) {
                await DeleteChainComment(ID_comment);
                await SaveChangesAsync();
            }
        }

        private async Task DeleteChainComment(int ID_comment) {
            List<Comment> comments = Comments.Where(c => c.ID_reply_comment == ID_comment).ToList();
            foreach (Comment comment in comments)
                await DeleteChainComment(comment.ID_comment);
            Comments.Remove(Comments.Where(c => c.ID_comment == ID_comment).FirstOrDefault());
        }

        public CommentModel ConvertCommentToModel(Comment comment) {
            return new CommentModel(comment, Users.Where(u => u.ID_user == comment.ID_user).FirstOrDefault(), Pages.Where(p => p.ID_page == comment.ID_page).FirstOrDefault());
        }

        public async Task SetAuthCode(int ID_user, int code) {
            Auth user = Authentication.Where(auth => auth.ID_user == ID_user).FirstOrDefault();
            if (user != null) {
                user.Code = code;
                await SaveChangesAsync();
            }
        }

        public async Task SetConfirmUser(int ID_user) {
            User user = Users.Where(u => u.ID_user == ID_user).FirstOrDefault();
            if (user != null) {
                user.isConfirmed = true;
                await SaveChangesAsync();
            }
        }

        public async Task DeleteRepeatUsersByEmail(String Email) {
            List<User> users = Users.Where(u => u.Login == Email && !u.isConfirmed).ToList();
            if (users.Count > 0) {
                foreach (User user in users)
                    Users.Remove(user);
                await SaveChangesAsync();
            }
        }
    }
}
