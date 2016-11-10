using MBAProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MBAProfile.Controllers
{
    public class HomeController : ApiController
    {
        [Route("Login")]
        [HttpPost]
        public IHttpActionResult ValidateUser(UserLogin user)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                if (unitOfWork.LoginInfo.ValidateLogin(user))
                {
                    return Ok(true);
                }
                return Ok(false);
            }

        }

        #region HttpGet Methods
        [Route("getStudents")]
        public IHttpActionResult getUCMStudentDetails()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.StudentsInfo.getAll());
            }
        }

        [Route("getUser/{id}")]
        public IHttpActionResult getUCMUser(int id)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.UserInfo.GetUser(id));
        }

        [Route("getAdvisors")]
        public IHttpActionResult getUCMAdvisors()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.AdvisorInfo.getAll());
            }
        }

        //StudentsFinishedTrainings
        [Route("getStudentsFinishedTrainings")]
        public IHttpActionResult getStudentsFinishedTrainings()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.StudentsInfo.StudentsFinishedTrainings());
            }
        }
        
        [Route("getStudentsWithTrainings")]
        public IHttpActionResult getStudentsWithTrainings()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.StudentsInfo.StudentsWithACMTrainingDue());
            }
        }

        [Route("getActiveAdvisors")]
        public IHttpActionResult getAllActiveAdvisors()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.AdvisorInfo.getAllActiveAdvisors());
            }
        }

        [Route("getInActiveAdvisors")]
        public IHttpActionResult getAllInActiveAdvisors()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.AdvisorInfo.getAllInActiveAdvisors());
            }
        }
        
        [Route("GetPrograms")]
        public IHttpActionResult getAllPrograms()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.ProgramInfo.getAll());
        }

        [Route("GetMajors")]
        public IHttpActionResult getMajors()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.MajorsInfo.getAll());
        }

        [Route("GetRoles")]
        public IHttpActionResult getRoles()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.RolesInfo.getAll());
        }

        [Route("GetStudentAcademicStatus")]
        public IHttpActionResult getStudentAcademicStatus()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.StudentAcademicStatusInfo.getAll());
        }

        [Route("GetStudentTrainingStatus")]
        public IHttpActionResult getStudentTrainingStatus()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.StudentTrainingStatusInfo.getAll());
        }

        [Route("GetTrainingRepo")]
        public IHttpActionResult getTrainingRepo()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.TrainingInfo.getAll());
        }

        [Route("GetCourses")]
        public IHttpActionResult getCourses()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.CoursesInfo.getAll());
        }

        #endregion

        [Route("AddStudent")]
        [HttpPost]
        public IHttpActionResult addStudent(StudentInfo student)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                UCMStudent _student = new UCMStudent();
                _student.Address = student.Address;
                _student.Advisor = student.Advisor;
                _student.ApprovedGrad = student.ApprovedGrad;
                _student.Comments = student.Comments;
                _student.AlternateEmail = student.AlternateEmail;
                _student.CreatedDate = student.CreatedDate;
                _student.Email = student.Email;
                _student.FirstName = student.FirstName;
                _student.GMATScore = student.GMATScore;
                _student.GPA = student.GPA;
                _student.GREScore = student.GREScore;
                _student.LastName = student.LastName;
                _student.ModifiedDate = student.ModifiedDate;
                _student.Password = student.Password;
                _student.PhoneNumber = student.PhoneNumber;
                _student.PrereqsMet = student.PrereqsMet;
                _student.ProgramId = student.ProgramId;
                _student.RoleId = student.RoleId;
                _student.StartDate = student.StartDate;
                _student.StudentTrainingStatusId = student.StudentTrainingStatusId;
                _student.Student_AcademicStatusId = student.Student_AcademicStatusId;
                _student.TrainingId = student.TrainingId;
                unitOfWork.StudentsInfo.Add(_student);
                if (unitOfWork.Save() >= 1)
                {
                    return Ok("Success");
                }
                return BadRequest("There is an error");
            }
        }
        
        [Route("AddAdvisor")]
        [HttpPost]
        public IHttpActionResult addAdvisor([FromBody] UCMModerator advisor)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                unitOfWork.AdvisorInfo.Add(advisor);
                if (unitOfWork.Save() >= 1)
                {
                    return Ok("Success");
                }
                return BadRequest("There is an error");
            }
        }

        [Route("AddCourse")]
        [HttpPost]
        public IHttpActionResult addCourse([FromBody] CourseInfo course)
        {
            Entities unitOfWork = new Entities();
            {
               int result= unitOfWork.AddCourse(course.Name, course.CourseNumber, course.CCode, course.PreqId);
                if (result>=1)
                {
                    return Ok("Success");
                }
                return BadRequest("There is an error");
            }
        }

        [Route("UpdateCourse")]
        [HttpPost]
        public IHttpActionResult updateCourse([FromBody] CourseInfo course)
        {
            Entities unitOfWork = new Entities();
            {
                int result = unitOfWork.UpdateCourse(course.Id, course.Name, course.CourseNumber, course.CCode, course.PreqId);
                if (result>=1)
                {
                    return Ok("Success");
                }
                return BadRequest("There is an error");
            }
        }
    }
}
