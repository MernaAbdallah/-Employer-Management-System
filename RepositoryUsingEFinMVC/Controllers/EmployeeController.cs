using RepositoryUsingEFinMVC.DAL;
using RepositoryUsingEFinMVC.GenericRepository;
using RepositoryUsingEFinMVC.Repository;
using RepositoryUsingEFinMVC.UnitOfWork;
using System.Web.Mvc;
using System;

public class EmployeeController : Controller
{
    //While Creating an Instance of UnitOfWork, we need to specify the Actual Context Object
    private UnitOfWork<EmployeeDataBaseEntities1> unitOfWork = new UnitOfWork<EmployeeDataBaseEntities1>();
    private GenericRepository<Employee> genericRepository;
    private IEmployeeRepository employeeRepository;
    public EmployeeController()
    {
        //If you want to use Generic Repository with Unit of work
        genericRepository = new GenericRepository<Employee>(unitOfWork);
        //If you want to use a Specific Repository with Unit of work
        employeeRepository = new EmployeeRepository(unitOfWork);
    }
    [HttpGet]
    public ActionResult Index()
    {
        //Using Generic Repository
        var model = genericRepository.GetAll();
        //Using Specific Repository
        //var model = employeeRepository.GetEmployeesByDepartment(1);
        return View(model);
    }
    [HttpGet]
    public ActionResult AddEmployee()
    {
        return View();
    }
    [HttpPost]
    public ActionResult AddEmployee(Employee model)
    {
        try
        {
            //First, Begin the Transaction
            unitOfWork.CreateTransaction();
            if (ModelState.IsValid)
            {
                //Do the Database Operation
                genericRepository.Insert(model);
                //Call the Save Method to call the Context Class Save Changes Method
                unitOfWork.Save();
                //Do Some Other Tasks with the Database
                //If everything is working then commit the transaction else rollback the transaction
                unitOfWork.Commit();
                return RedirectToAction("Index", "Employee");
            }
        }
        catch (Exception ex)
        {
            //Log the exception and rollback the transaction
            unitOfWork.Rollback();
        }
        return View();
    }
    [HttpGet]
    public ActionResult EditEmployee(int EmployeeId)
    {
        Employee model = genericRepository.GetById(EmployeeId);
        return View(model);
    }
    [HttpPost]
    public ActionResult EditEmployee(Employee model)
    {
        if (ModelState.IsValid)
        {
            genericRepository.Update(model);
            unitOfWork.Save();
            return RedirectToAction("Index", "Employee");
        }
        else
        {
            return View(model);
        }
    }
    [HttpGet]
    public ActionResult DeleteEmployee(int EmployeeId)
    {
        Employee model = genericRepository.GetById(EmployeeId);
        return View(model);
    }
    [HttpPost]
    public ActionResult Delete(int EmployeeID)
    {
        Employee model = genericRepository.GetById(EmployeeID);
        genericRepository.Delete(model);
        unitOfWork.Save();
        return RedirectToAction("Index", "Employee");
    }
}