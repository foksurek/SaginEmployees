using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaginEmployees.Dto;
using SaginEmployees.Services;

namespace SaginEmployees.Controllers;


[Authorize]
[ApiController]
[Route("[controller]")]
public class EmployerController : Controller
{
    private readonly MongoDbService _mongoDb;

    public EmployerController(MongoDbService mongoDb) => _mongoDb = mongoDb;
    
    
    [HttpPost("AddEmployee")]
    public async Task<OkObjectResult> AddEmployee(EmployeeDto employee)
    {
        await _mongoDb.AddEmployee(employee);
        return Ok($"Successfully added employee {employee.Name}");
    }
    
    [HttpPut("UpdateEmployee/{id}")]
    public async Task<OkObjectResult> UpdateEmployee(string id, EmployeeDto employee)
    {
        employee.Id = id;
        await _mongoDb.UpdateEmployee(id, employee);
        return Ok("Successfully updated employee");
    }

    [HttpDelete("DeleteEmployee/{id}")]
    public async Task<OkObjectResult> DeleteEmployee(string id)
    {
        await _mongoDb.DeleteEmployee(id);
        return Ok($"Successfully removed employee({id})");
    }
    
    [HttpGet("CalculateMonthlySalaryForEmployee/{id}")]
    public async Task<JsonResult> CalculateMonthlySalaryForEmployee(string id)
    {
        var employee = await _mongoDb.GetEmployee(id);
        var currency = await _mongoDb.GetCurrency(employee.Salary.CurrencyId);
        var salary = employee.Salary.Amount;
        var monthlySalaryInDollars = salary / currency.DolarRate;
        var yearlySalaryInDollars = monthlySalaryInDollars * 12;

        return Json(new
        {
            employee.Name,
            employee.Salary.Amount,
            employee.Salary.CurrencyId,
            monthlySalaryInDollars,
            yearlySalaryInDollars
        });
    }

    [HttpGet("CalculateSalaryForDepartment/{name}")]
    public async Task<JsonResult> CalculateSalaryForDepartment(string name)
    {
        var employees = await _mongoDb.GetEmployeesByDepartmentName(name);
        double salaryInDollars = 0;
        var employeesCount = employees.Count;
        foreach (var employee in employees)
        {
            var currency = await _mongoDb.GetCurrency(employee.Salary.CurrencyId);
            var salary = employee.Salary.Amount;
            var monthlySalaryInDollars = salary / currency.DolarRate;
            
           salaryInDollars += monthlySalaryInDollars;
        }
        var yearlySalaryInDollars = salaryInDollars * 12;

        return Json(new
        {
            name,
            employeesCount,
            salaryInDollars,
            yearlySalaryInDollars
            
        });
    }
}