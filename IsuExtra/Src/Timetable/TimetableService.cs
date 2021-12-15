using System;
using System.Collections.Generic;
using IsuExtra.Entity;
using IsuExtra.ValueObj;

namespace IsuExtra.TimeTable
{
    public class TimeTableService
    {
        private List<TimeTable> _repositoryOfTimeTables;

        public TimeTableService()
        {
            _repositoryOfTimeTables = new List<TimeTable>();
        }

        public Lesson AddLesson(DateTime start, DateTime end, PersonExtra teacher, int auditory) =>
            new Lesson(start, end, teacher, auditory);

        public TimeTable AddTimeTable(Elective elective, Lesson lesson)
        {
            var timeTable = new TimeTable(elective.FacultyId, lesson);
            _repositoryOfTimeTables.Add(timeTable);
            return timeTable;
        }

        public TimeTable AddTimeTable(Elective elective, IEnumerable<Lesson> lessons)
        {
            var timeTable = new TimeTable(elective.FacultyId, lessons);
            _repositoryOfTimeTables.Add(timeTable);
            return timeTable;
        }

        public TimeTable AddLessonToTimeTable(TimeTable timeTable, Lesson lesson)
        {
            List<Lesson> list = timeTable.Schedule;
            list.Add(lesson);
            var newTimeTable = new TimeTable(timeTable.Id, list);
            _repositoryOfTimeTables.Remove(timeTable);
            _repositoryOfTimeTables.Add(newTimeTable);

            return newTimeTable;
        }

        public TimeTable RemoveLessonFromTimeTable(TimeTable timeTable, Lesson lesson)
        {
            List<Lesson> list = timeTable.Schedule;
            list.Remove(lesson);
            var newTimeTable = new TimeTable(timeTable.Id, list);
            _repositoryOfTimeTables.Remove(timeTable);
            _repositoryOfTimeTables.Add(newTimeTable);

            return newTimeTable;
        }

        public IEnumerable<TimeTable> GetTimeTables()
        {
            return _repositoryOfTimeTables.AsReadOnly();
        }

        public TimeTable GetTimeTable(string id)
        {
            return _repositoryOfTimeTables.Find(x => x.Id.Equals(id));
        }
    }
}