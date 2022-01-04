using System;
using System.Collections.Generic;
using IsuExtra.Entity;
using IsuExtra.ValueObj;

namespace IsuExtra.TimeTable
{
    public class TimeTableService
    {
        private List<Timetable> _repositoryOfTimeTables;

        public TimeTableService()
        {
            _repositoryOfTimeTables = new List<Timetable>();
        }

        public Timetable AddTimeTable(Elective elective)
        {
            var timeTable = new Timetable(elective.FacultyId);
            _repositoryOfTimeTables.Add(timeTable);
            return timeTable;
        }

        public Timetable AddTimeTable(Elective elective, IEnumerable<Lesson> lessons)
        {
            var timeTable = new Timetable(elective.FacultyId, lessons);
            _repositoryOfTimeTables.Add(timeTable);
            return timeTable;
        }

        public Timetable AddLessonToTimeTable(Timetable timetable, Lesson lesson)
        {
            List<Lesson> list = timetable.Schedule;
            list.Add(lesson);
            var newTimeTable = new Timetable(timetable.Id, list);
            _repositoryOfTimeTables.Remove(timetable);
            _repositoryOfTimeTables.Add(newTimeTable);

            return newTimeTable;
        }

        public Timetable AddLessonToTimeTable(Timetable timetable, DateTime start, DateTime end, PersonExtra teacher, int auditory)
        {
            Lesson lesson = AddLesson(start, end, teacher, auditory);
            List<Lesson> list = timetable.Schedule;
            list.Add(lesson);
            var newTimeTable = new Timetable(timetable.Id, list);
            _repositoryOfTimeTables.Remove(timetable);
            _repositoryOfTimeTables.Add(newTimeTable);

            return newTimeTable;
        }

        public Timetable RemoveLessonFromTimeTable(Timetable timetable, Lesson lesson)
        {
            List<Lesson> list = timetable.Schedule;
            list.Remove(lesson);
            var newTimeTable = new Timetable(timetable.Id, list);
            _repositoryOfTimeTables.Remove(timetable);
            _repositoryOfTimeTables.Add(newTimeTable);

            return newTimeTable;
        }

        public Timetable RemoveLessonFromTimeTable(Timetable timetable, DateTime start, DateTime end, PersonExtra teacher, int auditory)
        {
            Lesson lesson = AddLesson(start, end, teacher, auditory);
            List<Lesson> list = timetable.Schedule;
            list.Remove(lesson);
            var newTimeTable = new Timetable(timetable.Id, list);
            _repositoryOfTimeTables.Remove(timetable);
            _repositoryOfTimeTables.Add(newTimeTable);

            return newTimeTable;
        }

        public IEnumerable<Timetable> GetTimeTables()
        {
            return _repositoryOfTimeTables.AsReadOnly();
        }

        public Timetable GetTimeTable(string id)
        {
            return _repositoryOfTimeTables.Find(x => x.Id.Equals(id));
        }

        private Lesson AddLesson(DateTime start, DateTime end, PersonExtra teacher, int auditory)
        {
            return new Lesson(start, end, teacher, auditory);
        }
    }
}