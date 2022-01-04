using System;

namespace IsuExtra.Tools
{
    public class IsuExtraException : Exception
    {
        public const string ElectiveBuildException = "Elective build is not successed";
        public const string LessonBuildException = "Lesson build is not successed";
        public const string TimetableBuildException = "Timetable build is not successed";
        public const string PersonExtraBuildException = "PersonExtra build is not successed";

        public IsuExtraException()
        {
        }

        public IsuExtraException(string message)
            : base(message)
        {
        }

        public IsuExtraException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}