using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBAProfile.Models
{
    public class CourseInfo
    {
        //course.Id, course.Name, course.CourseNumber, course.ConcentrationCode, course.PreqId, course.IsActive
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseNumber { get; set; }
        public string CCode { get; set; }
        public string PreqId { get; set; }
        public bool PrereqIsActive { get; set; }
    }
}