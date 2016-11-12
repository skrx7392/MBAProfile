using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MBAProfile.Models;
namespace MBAProfile.Models
{
    public enum TrainingStatus
    {
        Due =2,
        Completed=3
    }
    
    interface IRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> getAll();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entites);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entites);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> entities);
    }

    public class LoginRepository
    {
        Entities UCMDbContext;
        public LoginRepository(Entities context)
        {
            UCMDbContext = context;
        }

        public bool ValidateLogin(UserLogin user)
        {
            var getUserById = UCMDbContext.UCMUsers.AsNoTracking().FirstOrDefault(usr => usr.Id == user.ID);
            if (getUserById!=null && getUserById.Password.Equals(user.Password))
            {
                return true;
            }
            return false;
        }
    }

    public class UCMUserRepository : UCMRepository<UCMUser>
    {
        Entities UCMDbContext;
        public UCMUserRepository(Entities context) : base(context)
        {
            UCMDbContext = context;
        }

        public string GetUser(int id)
        {
            int roleId = UCMDbContext.UCMUsers.FirstOrDefault(p => p.Id == id).RoleId;
            string role = UCMDbContext.Roles.FirstOrDefault(p => p.Id == roleId).Name;
            return role;
        }

        public UCMUser GetDirector()
        {
            Role role = UCMDbContext.Roles.FirstOrDefault(p => p.Name.Equals("Director"));
            return UCMDbContext.UCMUsers.FirstOrDefault(p => p.RoleId == role.Id);
        }
    }

    public class UCMStudentRepository : UCMRepository<UCMStudent> 
    {
        Entities UCMDbContext;
        public UCMStudentRepository(Entities context) : base(context)
        {
            UCMDbContext = context;
        }

        public IEnumerable<UCMStudent> StudentsWithACMTrainingDue()
        {
            return UCMDbContext.UCMStudents.Where(student => student.TrainingId == 1 && student.Student_TrainingStatus.Id==(int)TrainingStatus.Due).AsNoTracking().AsEnumerable();
        }

        public IEnumerable<UCMStudent> StudentsFinishedTrainings()
        {
            return UCMDbContext.UCMStudents.Where(student => student.StudentTrainingStatusId == (int)TrainingStatus.Completed).AsNoTracking().AsEnumerable();
        }
    }

    public class UCMCourseRepository : UCMRepository<Course>
    {
        Entities UCMDbContext;
        public UCMCourseRepository(Entities context) : base(context)
        {
            UCMDbContext = context;
        }
    }

    public class UCMProgramRepository : UCMRepository<Program>
    {
        Entities UCMDbContext;
        public UCMProgramRepository(Entities context) : base(context)
        {
            UCMDbContext = context;
        }
    }

    public class UCMRolesRepository : UCMRepository<Role>
    {
        Entities UCMDbContext;
        public UCMRolesRepository(Entities context) : base(context)
        {
            UCMDbContext = context;
        }
    }

    public class UCMStudent_AcademicStatusRepository : UCMRepository<Student_AcademicStatus>
    {
        Entities UCMDbContext;
        public UCMStudent_AcademicStatusRepository(Entities context):base(context)
        {
            UCMDbContext = context;
        }
    }

    public class UCMStudent_TrainingStatusRepository : UCMRepository<Student_TrainingStatus>
    {
        Entities UCMDbContext;
        public UCMStudent_TrainingStatusRepository(Entities context):base(context)
        {
            UCMDbContext = context;
        }
    }

    public class UCMTrainingRepository : UCMRepository<Training>
    {
        Entities UCMDbContext;
        public UCMTrainingRepository(Entities context):base(context)
        {
            UCMDbContext = context;
        }
    }

    public class UCMMajorRepository : UCMRepository<Training>
    {
        Entities UCMDbContext;
        public UCMMajorRepository(Entities context) : base(context)
        {
            UCMDbContext = context;
        }
    }

    public class UCMAdvisorRepository : UCMRepository<UCMModerator>
    {
        Entities UCMDbContext;
        public UCMAdvisorRepository(Entities context) : base(context)
        {
            UCMDbContext = context;
        }

        public IEnumerable<UCMModerator> getAllActiveAdvisors()
        {
            return UCMDbContext.UCMModerators.Where(advisor => advisor.IsActive).AsNoTracking().AsEnumerable();
        }

        public IEnumerable<UCMModerator> getAllInActiveAdvisors()
        {
            return UCMDbContext.UCMModerators.Where(advisor => !advisor.IsActive).AsNoTracking().AsEnumerable();
        }
    }

    public interface IUnitOfWork:IDisposable
    {
        UCMStudentRepository StudentsInfo  { get; }
        UCMAdvisorRepository AdvisorInfo { get;}
        LoginRepository LoginInfo { get; }
        UCMCourseRepository CoursesInfo { get; }
        UCMProgramRepository ProgramInfo { get; }
        UCMMajorRepository MajorsInfo { get; }
        UCMRolesRepository RolesInfo { get; }
        UCMStudent_TrainingStatusRepository StudentTrainingStatusInfo { get; }
        UCMStudent_AcademicStatusRepository StudentAcademicStatusInfo { get; }
        UCMTrainingRepository TrainingInfo { get; }
        UCMUserRepository UserInfo { get; }
        int Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        Entities UCMDbContext;
        public UnitOfWork(Entities context)
        {
            UCMDbContext = context;
            AdvisorInfo = new UCMAdvisorRepository(UCMDbContext);
            StudentsInfo = new UCMStudentRepository(UCMDbContext);
            CoursesInfo = new UCMCourseRepository(UCMDbContext);
            LoginInfo = new LoginRepository(UCMDbContext);
            ProgramInfo = new UCMProgramRepository(UCMDbContext);
            MajorsInfo = new UCMMajorRepository(UCMDbContext);
            RolesInfo = new UCMRolesRepository(UCMDbContext);
            StudentTrainingStatusInfo = new UCMStudent_TrainingStatusRepository(UCMDbContext);
            StudentAcademicStatusInfo = new UCMStudent_AcademicStatusRepository(UCMDbContext);
            TrainingInfo = new UCMTrainingRepository(UCMDbContext);
            UserInfo = new UCMUserRepository(UCMDbContext);
        }
        public LoginRepository LoginInfo
        {
            get; private set;
        }

        public UCMUserRepository UserInfo { get; private set; }

        public UCMProgramRepository ProgramInfo { get; private set; }

        public UCMMajorRepository MajorsInfo { get; private set; }

        public UCMRolesRepository RolesInfo { get; private set; }

        public UCMStudent_TrainingStatusRepository StudentTrainingStatusInfo { get; private set; }

        public UCMStudent_AcademicStatusRepository StudentAcademicStatusInfo { get; private set; }

        public UCMTrainingRepository TrainingInfo { get; private set; }

        public UCMCourseRepository CoursesInfo { get; private set; }

        public UCMAdvisorRepository AdvisorInfo { get; private set; }

        public UCMStudentRepository StudentsInfo { get; private set; }

        public void Dispose()
        {
            UCMDbContext.Dispose();
        }

        public int Save()
        {
            return UCMDbContext.SaveChanges();
        }
    }

    public class UCMRepository<TEntity> : IRepository<TEntity> where TEntity:class
    {
        DbContext UCMDbContext;
        public UCMRepository(DbContext context)
        {
            UCMDbContext = context;
        }

        public TEntity Get(int id)
        {
            return UCMDbContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> getAll()
        {
            return UCMDbContext.Set<TEntity>().AsNoTracking().AsEnumerable();
        }
        public void Add(TEntity entity)
        {
            UCMDbContext.Set<TEntity>().Add(entity);
        }
        
        public void AddRange(IEnumerable<TEntity> entites)
        {
            UCMDbContext.Set<TEntity>().AddRange(entites);

        }

        public void Update(TEntity entity)
        {
            UCMDbContext.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> entities)
        {
            return UCMDbContext.Set<TEntity>().Where(entities).AsNoTracking().AsEnumerable();
        }

        public void Remove(TEntity entity)
        {
            UCMDbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entites)
        {
            UCMDbContext.Set<TEntity>().RemoveRange(entites);
        }
    }
}