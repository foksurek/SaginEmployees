using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaginEmployees.Dto;
using SaginEmployees.Models;
using SaginEmployees.Services;

namespace SaginEmployees.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : Controller
{
    private readonly MongoDbService _mongoDb;
    public EmployeesController(MongoDbService mongoDb) => _mongoDb = mongoDb;

    [HttpGet("getEmployees")]
    public async Task<List<EmployeeDto>> GetEmployees() =>
        await _mongoDb.GetEmployees();
    
    [HttpGet("getEmployee/{id}")]
    public async Task<EmployeeDto> GetEmployee(string id) =>
        await _mongoDb.GetEmployee(id);
    
    [HttpPost("AddEmployee")]
    public async Task<OkObjectResult> AddEmployee(EmployeeDto employee)
    {
        await _mongoDb.CreateEmployee(employee);
        return Ok("Successfully added employee");
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
}