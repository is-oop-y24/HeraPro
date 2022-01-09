using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.TimeTable
{
    public class Timetable
    {
        public Timetable(string id)
        {
            Id = id ?? throw new IsuExtraException(IsuExtraException.TimetableBuildException);
            Schedule = new List<Lesson>();
        }

        public Timetable(string id, Lesson lesson)
        {
            if (id == null || lesson == null)
                throw new IsuExtraException(IsuExtraException.TimetableBuildException);

            Id = id;
            Schedule = new List<Lesson>() { lesson };
        }

        public Timetable(string id, IEnumerable<Lesson> schedule)
        {
            if (id == null || schedule == null)
                throw new IsuExtraException(IsuExtraException.TimetableBuildException);

            Id = id;
            Schedule = schedule.ToList();
        }

        public string Id { get; }
        public List<Lesson> Schedule { get; }
    }
}