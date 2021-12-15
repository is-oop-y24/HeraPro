using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.TimeTable
{
    public class TimeTable
    {
        public TimeTable(string id, Lesson lesson)
        {
            Id = id;
            Schedule = new List<Lesson>() { lesson };
        }

        public TimeTable(string id, IEnumerable<Lesson> schedule)
        {
            Id = id;
            Schedule = schedule.ToList();
        }

        public string Id { get; }
        public List<Lesson> Schedule { get; }
    }
}