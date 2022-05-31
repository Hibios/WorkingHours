using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model;

namespace WorkingHours.EF
{
    public class Connection
    {
        public static WorkingHoursEntities db = new WorkingHoursEntities();
    }
}
