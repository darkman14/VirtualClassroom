using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClassroom.Model;

namespace VirtualClassroom.Interfaces
{
    public interface ICalendarInfoInterface
    {
        IQueryable<CalendarInfo> GetCalendarForUser(int userId);
        void Add(CalendarInfo calInfo);
        void Remove(int calInfoId);
    }
}
