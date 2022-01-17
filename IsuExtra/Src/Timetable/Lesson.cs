using System;
using Isu.Entities;
using IsuExtra.Tools;
using IsuExtra.ValueObj;

namespace IsuExtra.TimeTable
{
    public class Lesson
    {
        public Lesson(DateTime start, DateTime end, PersonExtra teacher, int auditory, GroupName group)
        {
            if (teacher == null || auditory <= 0)
                throw new IsuExtraException(IsuExtraException.LessonBuildException);

            Start = start;
            End = end;
            Teacher = teacher;
            Auditory = auditory;
            Group = group;
        }

        public DateTime Start { get; }
        public DateTime End { get; }
        public PersonExtra Teacher { get; }
        public int Auditory { get; }
        public GroupName Group { get; }
    }
}