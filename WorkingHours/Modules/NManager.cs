using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WorkingHours.EF.Connection;
using WorkingHours.Model;
using System.Windows.Controls;

namespace WorkingHours.Modules
{
    public class NManager
    {
        public static void SendRequestNotification(User recipient)
        {
            Notification notification = new Notification
            {
                IdNotification = db.Notifications.Count() + 1,
                IdTypeNotification = 1,
                TitleNotification = "Подключение к организации",
                DescriptionNotification = $"{UserForRole.userForRole.Email} - хочет подключится к вашей организации.",
                CheckIt = false,
                IdSender = UserForRole.userForRole.IdUser,
                IdRecipient = recipient.IdUser
            };
            UserForRole.userForRole.isVaitingForOrg = true;
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        internal static void SendNotification(string title, string descr, string rec)
        {
            User recipient = db.Users.FirstOrDefault(usr => usr.Email == rec);

            Notification notification = new Notification
            {
                IdNotification = db.Notifications.Count() + 1,
                IdTypeNotification = 2,
                TitleNotification = title,
                DescriptionNotification = descr,
                CheckIt = false,
                IdSender = UserForRole.userForRole.IdUser,
                IdRecipient = recipient.IdUser
            };
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public static void AcceptNotificationRequest(User sender, Notification notification)
        {
            sender.isVaitingForOrg = false;
            sender.IdOrganization = UserForRole.userForRole.IdOrganization;
            sender.IdRole = 2; // id стандартной роли сотрудника
            notification.CheckIt = true;


            db.SaveChanges();
        }

        public static void AcceptNotification(Notification notification)
        {
            notification.CheckIt = true;
            db.SaveChanges();
        }

        public static void RejectNotificationRequest(User sender, Notification notification)
        {
            sender.isVaitingForOrg = false;
            db.Notifications.Remove(notification);
            db.SaveChanges();
        }

        public static void CheckNotifications(Button btnNotification, Page page)
        {
            int notificationCount = db.Notifications.Where(n => n.IdRecipient == UserForRole.userForRole.IdUser && n.CheckIt == false).Count();
            if (notificationCount == 0)
            {
                btnNotification.Content = page.FindResource("NotificationIcon");
            }
            else
            {
                Grid grd = page.FindResource("ActiveNotificationIcon") as Grid;
                TextBlock counter = grd.Children.Cast<TextBlock>().FirstOrDefault(i => Grid.GetZIndex(i) == 1);
                counter.Text = notificationCount.ToString();
                btnNotification.Content = grd;
            }
        }
    }
}
