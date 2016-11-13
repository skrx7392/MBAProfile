using MBAProfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MBAProfile.Controllers
{

    public class HomeController : ApiController
    {
        /// <summary>
        /// Authenticates user from Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public IHttpActionResult ValidateUser([FromBody]UserLogin user)
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
        /// <summary>
        /// Get All Student Details
        /// </summary>
        /// <returns></returns>
        [Route("getStudents")]
        public IHttpActionResult getUCMStudentDetails()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.StudentsInfo.getAll());
            }
        }

        /// <summary>
        /// Retrieves user with his Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getUser/{id}")]
        public IHttpActionResult getUCMUser(int id)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.UserInfo.GetUser(id));
        }

        /// Pulls all the advisors list
        [Route("getAdvisors")]
        public IHttpActionResult getUCMAdvisors()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.AdvisorInfo.getAll());
            }
        }

        /// Pulls Director Details
        [Route("getDirector")]
        public IHttpActionResult getDirector()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.UserInfo.GetDirector());
        }
        /// <summary>
        /// Students who Completed their Trainings
        /// </summary>
        /// <returns></returns>

        [Route("getStudentsFinishedTrainings")]

        public IHttpActionResult getStudentsFinishedTrainings()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.StudentsInfo.StudentsFinishedTrainings());
            }
        }
        /// <summary>
        /// Students who had not completed trainngs
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("getStudentsWithTrainings")]
        public IHttpActionResult getStudentsWithTrainings()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.StudentsInfo.StudentsWithACMTrainingDue());
            }
        }
        /// <summary>
        /// Pulls all the active advisors
        /// </summary>
        /// <returns></returns>
        [Route("getActiveAdvisors")]
        public IHttpActionResult getAllActiveAdvisors()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.AdvisorInfo.getAllActiveAdvisors());
            }
        }
        /// <summary>
        /// Pulls all Inactive Advisors
        /// </summary>
        /// <returns></returns>
        [Route("getInActiveAdvisors")]
        public IHttpActionResult getAllInActiveAdvisors()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            {
                return Ok(unitOfWork.AdvisorInfo.getAllInActiveAdvisors());
            }
        }

        /// <summary>
        /// Fetches all the concentrations available
        /// </summary>
        /// <returns></returns>
        [Route("GetPrograms")]
        public IHttpActionResult getAllPrograms()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.ProgramInfo.getAll());
        }

        /// <summary>
        /// All the Major details
        /// </summary>
        /// <returns></returns>
        [Route("GetMajors")]
        public IHttpActionResult getMajors()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.MajorsInfo.getAll());
        }

        /// <summary>
        /// Fetches all the Roles(Advisor,Director and Student)
        /// </summary>
        /// <returns></returns>
        [Route("GetRoles")]
        public IHttpActionResult getRoles()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.RolesInfo.getAll());
        }

        /// <summary>
        /// Get Students who had not completed the Academic Status
        /// </summary>
        /// <returns></returns>
        [Route("GetStudentAcademicStatus")]
        public IHttpActionResult getStudentAcademicStatus()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.StudentAcademicStatusInfo.getAll());
        }

        /// <summary>
        /// Get Students with respective to training details
        /// </summary>
        /// <returns></returns>
        [Route("GetStudentTrainingStatus")]
        public IHttpActionResult getStudentTrainingStatus()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.StudentTrainingStatusInfo.getAll());
        }

        /// <summary>
        /// Get table of Training Statuses
        /// </summary>
        /// <returns></returns>
        [Route("GetTrainingRepo")]
        public IHttpActionResult getTrainingRepo()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.TrainingInfo.getAll());
        }
        /// <summary>
        /// Pulls out all Courses
        /// </summary>
        /// <returns></returns>
        [Route("GetCourses")]
        public IHttpActionResult getCourses()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            return Ok(unitOfWork.CoursesInfo.getAll());
        }

        #endregion
        /// Creates Student Profile with Details   
        [Route("AddStudent")]
        [HttpPost]
        public IHttpActionResult addStudent([FromBody]UCMStudent student)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.StudentsInfo.Add(student);
            if (unitOfWork.Save() >= 1)
            {
                return Ok("Success");
            }
            return BadRequest("There is an error");
        }
        /// <summary>
        /// Updates the student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [Route("UpdateStudent")]
        [HttpPost]
        public IHttpActionResult updateStudent([FromBody] UCMStudent student)
        {

            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.StudentsInfo.Update(student);
            if (unitOfWork.Save() >= 1)
            {
                return Ok("Success");
            }
            return BadRequest("There is an error");
        }

        /// <summary>
        /// Adds a program to the database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [Route("addProgram")]
        [HttpPost]
        public IHttpActionResult addProgram([FromBody]Program program)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.ProgramInfo.Add(program);
            if (unitOfWork.Save() >= 1)
            {
                return Ok("Success");
            }
            return BadRequest("There is an error");
        }

        /// <summary>
        /// Updates the program in database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [Route("UpdateProgram")]
        [HttpPost]
        public IHttpActionResult updateProgram([FromBody]Program program)
        {

            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.ProgramInfo.Update(program);
            if (unitOfWork.Save() >= 1)
            {
                return Ok("Success");
            }
            return BadRequest("There is an error");
        }

        /// <summary>
        /// Creates the advisor details.
        /// </summary>
        /// <param name="advisor"></param>
        /// <returns></returns>
        /// 
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

        /// <summary>
        /// Updates the advisor details
        /// </summary>
        /// <param name="advisor"></param>
        /// <returns></returns>
        [Route("UpdateAdvisor")]
        [HttpPost]
        public IHttpActionResult updateAdvisor([FromBody] UCMModerator advisor)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.AdvisorInfo.Update(advisor);
            if (unitOfWork.Save() >= 1)
            {
                return Ok("Success");
            }
            return BadRequest("There is an error");
        }
        /// <summary>
        /// Can add the Couse details
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [Route("AddCourse")]
        [HttpPost]
        public IHttpActionResult addCourse([FromBody] CourseInfo course)
        {
            Entities unitOfWork = new Entities();
            {
                int result = unitOfWork.AddCourse(course.Name, course.CourseNumber, course.CCode, course.PreqId, course.PrereqIsActive);
                if (result >= 1)
                {
                    return Ok("Success");
                }
                return BadRequest("There is an error");
            }
        }

        /// <summary>
        /// Updates the Course Details
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [Route("UpdateCourse")]
        [HttpPost]
        public IHttpActionResult updateCourse([FromBody] CourseInfo course)
        {
            Entities unitOfWork = new Entities();
            {
                int result = unitOfWork.UpdateCourse(course.Id, course.Name, course.CourseNumber, course.CCode, course.PreqId, course.PrereqIsActive);
                if (result >= 1)
                {
                    return Ok("Success");
                }
                return BadRequest("There is an error");
            }
        }

        /// <summary>
        /// Updates the User details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("updateUser")]
        [HttpPost]
        public IHttpActionResult updateUser([FromBody] UCMUser user)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.UserInfo.Update(user);
            if (unitOfWork.Save() >= 1)
            {
                return Ok("Success");
            }
            return BadRequest("There is an error");
        }
    }
}
