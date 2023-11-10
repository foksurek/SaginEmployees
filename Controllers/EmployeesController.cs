using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaginEmployees.Dto;
using SaginEmployees.Models;
using SaginEmployees.Services;

namespace SaginEmployees.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EmployeesController : Controller
{
    private readonly MongoDbService _mongoDb;
    public EmployeesController(MongoDbService mongoDb) => _mongoDb = mongoDb;

    [HttpGet("getEmployees")]
    public async Task<List<EmployeeDto>> GetEmployees()
    {
        var employeesFromCompany = await _mongoDb.GetEmployees();
        return employeesFromCompany;
    }

    [HttpGet("getEmployee/{id}")]
    public async Task<EmployeeDto> GetEmployee(string id) =>
        await _mongoDb.GetEmployee(id);
    
    
    [HttpPost("DismissEmployee")]
    public async Task DismissEmployee(string id)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Status = "Dismissed";
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("HireEmployee")]
    public async Task HireEmployee(string id)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Status = "Hired";
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangeSalary")]
    public async Task ChangeSalary(string id, double amount)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Salary.Amount = amount;
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangeCurrency")]
    public async Task ChangeDepartment(string id, string department)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Department = department;
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangeAddress")]
    public async Task ChangeAddress(string id, Adress address)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Address = address;
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangePhone")]
    public async Task ChangePhone(string id, string phone)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Phone = phone;
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangeEmail")]
    public async Task ChangeEmail(string id, string email)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Email = email;
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangeName")]
    public async Task ChangeName(string id, string name)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Name = name;
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangeState")]
    public async Task ChangeState(string id, string state)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Address.State = state;
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangeCity")]
    public async Task ChangeCity(string id, string city)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Address.City = city;
        await _mongoDb.UpdateEmployee(id, employee);
    }
    
    [HttpPost("ChangeStreet")]
    public async Task ChangeStreet(string id, string street)
    {
        var employee = await _mongoDb.GetEmployee(id);
        employee.Address.Street = street;
        await _mongoDb.UpdateEmployee(id, employee);
    }
}