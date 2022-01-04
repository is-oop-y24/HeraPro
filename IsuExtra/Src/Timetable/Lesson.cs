using System;
using IsuExtra.Tools;
using IsuExtra.ValueObj;

namespace IsuExtra.TimeTable
{
    public class Lesson
    {
        public Lesson(DateTime start, DateTime end, PersonExtra teacher, int auditory)
        {
            if (teacher == null || auditory <= 0)
                throw new IsuExtraException(IsuExtraException.LessonBuildException);

            Start = start;
            End = end;
            Teacher = teacher;
            Auditory = auditory;
        }

        public DateTime Start { get; }
        public DateTime End { get; }
        public PersonExtra Teacher { get; }
        public int Auditory { get; }
    }
}