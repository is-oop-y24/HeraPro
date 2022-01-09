using System;
using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entity;
using IsuExtra.ValueObj;

namespace IsuExtra.TimeTable
{
    public class TimeTableService
    {
        private readonly List<Timetable> _repositoryOfTimeTables;

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

        public Timetable AddLessonToTimeTable(Timetable timetable, DateTime start, DateTime end, PersonExtra teacher, int auditory, GroupName group)
        {
            Lesson lesson = AddLesson(start, end, teacher, auditory, group);
            timetable.Schedule.Add(lesson);
            List<Lesson> list = timetable.Schedule;
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

        public Timetable RemoveLessonFromTimeTable(Timetable timetable, DateTime start, DateTime end, PersonExtra teacher, int auditory, GroupName group)
        {
            Lesson lesson = AddLesson(start, end, teacher, auditory, group);
            List<Lesson> list = timetable.Schedule;
            list.Remove(lesson);
            var newTimeTable = new Timetable(timetable.Id, list);
            _repositoryOfTimeTables.Remove(timetable);
            _repositoryOfTimeTables.Add(newTimeTable);

            return newTimeTable;
        }

        public bool Intersects(Lesson lesson1, Lesson lesson2)
        {
            if (lesson1.Start == lesson1.End || lesson2.Start == lesson2.End)
                return false;

            if (lesson1.Start == lesson2.Start || lesson1.End == lesson2.End)
                return true;

            if (lesson1.Start < lesson2.Start)
            {
                if (lesson1.End > lesson2.Start && lesson1.End < lesson2.End)
                    return true;

                if (lesson1.End > lesson2.End)
                    return true;
            }
            else
            {
                if (lesson2.End > lesson1.Start && lesson2.End < lesson1.End)
                    return true; // Condition 2

                if (lesson2.End > lesson1.End)
                    return true; // Condition 4
            }

            return false;
        }

        public IEnumerable<Timetable> GetTimeTables()
        {
            return _repositoryOfTimeTables.AsReadOnly();
        }

        public Timetable GetTimeTable(string id)
        {
            return _repositoryOfTimeTables.Find(x => x.Id.Equals(id));
        }

        private Lesson AddLesson(DateTime start, DateTime end, PersonExtra teacher, int auditory, GroupName group)
        {
            return new Lesson(start, end, teacher, auditory, group);
        }
    }
}