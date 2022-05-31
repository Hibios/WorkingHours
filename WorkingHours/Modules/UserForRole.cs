using SharpAvi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model;
using static WorkingHours.EF.Connection;

namespace WorkingHours.Modules
{
    public class UserForRole
    {
        public static User userForRole;
        public static TimeMonitoring monitoring;
        public static Recorder currentRecorder;
        public static List<Break> breakList = new List<Break>();
        public static string BaseFolder;

        public static void Login(User user)
        {
            userForRole = user;

            // Если у пользователя ещё нет видео менеджера, создаём его
            CreateVideoManager();

            // Если пользователь не заходил сегодня в программу
            if (UserForRole.getTodayMonitoring() == null)
            {
                monitoring = new TimeMonitoring
                {
                    IdTimeMonitoring = db.TimeMonitorings.Count() + 1,
                    IdUser = userForRole.IdUser,
                    LoginTime = DateTime.Now.TimeOfDay,
                    WorkingDay = DateTime.Now.Date
                };

                // Всем по обеду и полднику, как полагается
                Break Lunch = new Break { IdBreak = db.Breaks.Count() + 1, BreakType = db.BreakTypes.FirstOrDefault(bt => bt.IdBreakType == 1), IdTimeMonitoring = monitoring.IdTimeMonitoring, BreakCheck = false, Duration = 60 };
                Break Break = new Break { IdBreak = db.Breaks.Count() + 2, BreakType = db.BreakTypes.FirstOrDefault(bt => bt.IdBreakType == 2), IdTimeMonitoring = monitoring.IdTimeMonitoring, BreakCheck = false, Duration = 30 };
                db.Breaks.Add(Lunch);
                db.Breaks.Add(Break);
                breakList.Add(Lunch);
                breakList.Add(Break);
                db.TimeMonitorings.Add(monitoring);
                db.SaveChanges();
            }
            else
            {
                monitoring = UserForRole.getTodayMonitoring();
                userForRole.VideoManager = UserForRole.userForRole.VideoManager;
                UserForRole.BaseFolder = GetBaseDir();
                foreach (Break b in db.Breaks.Where(tb => tb.IdTimeMonitoring == monitoring.IdTimeMonitoring).ToList())
                    breakList.Add(b);
            }
            currentRecorder = VM.StartRecording($"{DateTime.Now.Date:MM-dd-yy.avi}");
        }

        public static void Logout()
        {
            TimeMonitoring usrMonitoring = UserForRole.getTodayMonitoring();
            usrMonitoring.LogoutTime = DateTime.Now.TimeOfDay;
            db.SaveChanges();
            userForRole = null;
            currentRecorder.Dispose();
        }

        private static void CreateVideoManager()
        {
            // Если пользователь первый раз заходит после регистрации, настраиваем его видео запись 
            if (db.Users.FirstOrDefault(usr => usr.IdUser == userForRole.IdUser && usr.VideoManager != null) == null)
            {
                UserForRole.BaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string VideoDir = $@"{UserForRole.BaseFolder}\WorkingHours\Videos\Videos_{userForRole.IdUser}";

                if (!Directory.Exists(VideoDir))
                {
                    DirectoryInfo di = Directory.CreateDirectory(VideoDir);
                }

                userForRole.VideoManager = new VideoManager { IdVideoManager = db.VideoManagers.Count() + 1, VideoFolder = VideoDir };
                db.VideoManagers.Add(userForRole.VideoManager);
                db.SaveChanges();
            }
        }

        public static void ChangeVideoManager()
        {
            string VideoDir = $@"{UserForRole.BaseFolder}\WorkingHours\Videos\Videos_{userForRole.IdUser}";

            if (!Directory.Exists(VideoDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(VideoDir);
            }

            userForRole.VideoManager.VideoFolder = VideoDir;
            db.SaveChanges();
        }

        public static string GetBaseDir()
        {
            return userForRole.VideoManager.VideoFolder.Substring(0, userForRole.VideoManager.VideoFolder.Length - $"|WorkingHours|Videos|Videos_{userForRole.IdUser}".Length);
        }

        public static TimeMonitoring getTodayMonitoring()
        {
            Console.WriteLine(DateTime.Today);
            Console.WriteLine(DateTime.Now.Date);

            DateTime dt = DateTime.Now.Date;

            return db.TimeMonitorings.FirstOrDefault(tm => tm.IdUser == userForRole.IdUser && tm.WorkingDay == dt);
        }
    }
}
