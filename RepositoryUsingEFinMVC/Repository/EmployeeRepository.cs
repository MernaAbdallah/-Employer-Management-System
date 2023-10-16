using RepositoryUsingEFinMVC.DAL;
using RepositoryUsingEFinMVC.GenericRepository;
using RepositoryUsingEFinMVC.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RepositoryUsingEFinMVC.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //While Creating an Instance of EmployeeRepository, we need to pass the UnitOfWork instance
        //That UnitOfWork instance contains the Context Object that our EmployeeRepository is going to use
        public EmployeeRepository(IUnitOfWork<EmployeeDataBaseEntities1> unitOfWork)
            : base(unitOfWork)
        {
        }
        //If you don't want to use the Unit Of Work, then use the following Constructor
        public EmployeeRepository(EmployeeDataBaseEntities1 context)
            : base(context)
        {
        }
        //The following Method is going to return the Employees by Gender
        public IEnumerable<Employee> GetEmployeesByGender(string Gender)
        {
            return Context.Employees.Where(emp => emp.Gender == Gender).ToList();
        }
        //The following Method is going to return the Employees by Department
        public IEnumerable<Employee> GetEmployeesByDepartment(string Dept)
        {
            return Context.Employees.Where(emp => emp.Dept == Dept).ToList();
        }
    }
}
