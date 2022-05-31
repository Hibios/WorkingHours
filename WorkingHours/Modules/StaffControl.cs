using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WorkingHours.EF.Connection;
using WorkingHours.Model;

namespace WorkingHours.Modules
{
    public class StaffControl
    {
        public static void employeeDismiss(User user)
        {
            user.IdRole = null;
            user.IdOrganization = null;
            db.SaveChanges();
        }

        public static void UseBreak(Break breakT)
        {
            DateTime nowTime = DateTime.Now;
            breakT.BreakStart = nowTime.TimeOfDay;
            breakT.BreakEnd = nowTime.Add(TimeSpan.FromMinutes(breakT.Duration)).TimeOfDay;
            breakT.BreakCheck = true;
            db.SaveChanges();
        }

        public static void AddBreak(string description, string duration)
        {
            Break CustomBreak = new Break { IdBreak = db.Breaks.Count() + 1, 
                BreakType = db.BreakTypes.FirstOrDefault(bt => bt.IdBreakType == 3), 
                IdTimeMonitoring = UserForRole.monitoring.IdTimeMonitoring, 
                BreakDescription = description, 
                BreakCheck = false, 
                Duration = Convert.ToInt16(duration), };

            UserForRole.breakList.Add(CustomBreak);
            db.Breaks.Add(CustomBreak);
            db.SaveChanges();
        }
    }
}
