using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebCanvas.ViewModels;

namespace WebCanvas.Models {
    public class Authentication {

        private ApplicationContext db;
        private Dictionary<String, MailType> dicMail { get; set; }
        private List<String> mails { get; set; }

        private MailAddress from { get; set; }

        private static Random random = new Random();

        public Authentication(ApplicationContext db) {

            dicMail = new Dictionary<string, MailType>() {
                { "mail", new MailType("smtp.mail.ru", 25, true) },
                { "gmail", new MailType("smtp.gmail.com", 587, true) }
            };
            mails = new List<string>();
            from = new MailAddress("xxvnvss@gmail.com", "WebCanvas");
            this.db = db;
        }

        private int GetCode() {
            int Code = random.Next(11111, 99999);
            return Code;
        }

        public async void SendCode(UserModel user) {
            try {
                int Code = GetCode();
                await db.SetAuthCode(user.ID_user, Code);

                String key = from.Address.Split('@')[from.Address.Split('@').Length - 1].Split('.')[0];
                using (MailMessage msg = new MailMessage(from, new MailAddress(user.Email)))
                using (SmtpClient client = new SmtpClient()) {
                    msg.Subject = "Код подтверждения";
                    msg.Body = $"Здравствуйте {user.FirstName} {user.LastName}! \nВаш код подтверждения - {Code}"; //CODE

                    client.Host = dicMail[key].Host;
                    client.Port = dicMail[key].Port;
                    client.EnableSsl = dicMail[key].EnableSsl;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(from.Address, "20202020xxXX");
                    client.Timeout = 12000;

                    await Task.Run(() => {
                        try {
                            client.Send(msg);
                        }
                        catch { }
                    });
                }
            }
            catch { }
        }

    }

    class MailType {
        public String Host;
        public int Port;
        public bool EnableSsl;

        public MailType(String Host, int Port, bool EnableSsl) {
            this.Host = Host;
            this.Port = Port;
            this.EnableSsl = EnableSsl;
        }
    }
}
