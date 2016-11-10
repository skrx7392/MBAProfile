using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBAProfile.Models
{
    public class StudentInfo
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int ProgramId { get; set; }
        public decimal GPA { get; set; }
        public Nullable<int> GREScore { get; set; }
        public Nullable<int> GMATScore { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<int> TrainingId { get; set; }
        public Nullable<int> StudentTrainingStatusId { get; set; }
        public int Student_AcademicStatusId { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> ApprovedGrad { get; set; }
        public Nullable<bool> PrereqsMet { get; set; }
        public Nullable<int> Advisor { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public int RoleId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}