using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        internal CourseNumber(int id)
        {
            if (id <= 0)
                throw new Exception(IsuException.IncorrectCourseNumber);

            Id = id;
        }

        public int Id { get; }
    }
}