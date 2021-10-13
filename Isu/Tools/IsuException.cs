using System;

namespace Isu.Tools
{
    public class IsuException : Exception
    {
        public const string IncorrectGroupName = "incorrect group name, pattern: M3XYY";
        public const string AddSameGroupNameTwiceError = "group with this name already exists";
        public const string NoSuchGroup = "can't add student: no such group exists";
        public const string NoStudentWithSuchId = "no student with such id";
        public const string MaxStudentsPerGroupReached = "max students per group reached";
        public const string IncorrectCourseNumber = "incorrect course number";
        public const string StudentIsNotFound = "no student found in this group";
        public const string IncorrectIdSet = "id should be >= 0";
        public const string GroupIsNotFound = "no groups been found";
        public const string NoGroups = "no groups to be added";
        public IsuException()
        {
        }

        public IsuException(string message)
            : base(message)
        {
        }

        public IsuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}