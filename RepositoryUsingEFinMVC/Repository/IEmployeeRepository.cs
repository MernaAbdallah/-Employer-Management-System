using RepositoryUsingEFinMVC.DAL;
using RepositoryUsingEFinMVC.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryUsingEFinMVC.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesByGender(string Gender);
        IEnumerable<Employee> GetEmployeesByDepartment(string Dept);
    }
}