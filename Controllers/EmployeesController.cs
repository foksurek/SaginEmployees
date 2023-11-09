using Microsoft.AspNetCore.Mvc;
using SaginEmployees.Models;

namespace SaginEmployees.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : Controller
{
    private readonly MongoDbService _mongoDb;

    public EmployeesController(MongoDbService mongoDb) => _mongoDb = mongoDb;

    [HttpGet("getEmployees")]
    public async Task<List<EmployeeModel>> GetEmployees() =>
        await _mongoDb.GetEmployees();
    
    [HttpGet("getEmployee/{id}")]
    public async Task<EmployeeModel> GetEmployee(string id) =>
        await _mongoDb.GetEmployee(id);
    
    [HttpPost("AddEmployee")]
    public async Task AddEmployee(EmployeeModel employee) =>
        await _mongoDb.CreateEmployee(employee);
    
    [HttpPut("UpdateEmployee/{id}")]
    public async Task UpdateEmployee(string id, EmployeeModel employee) =>
        await _mongoDb.UpdateEmployee(id, employee);

    [HttpDelete("DeleteEmployee/{id}")]
    public async Task<OkObjectResult> DeleteEmployee(string id)
    {
        await _mongoDb.DeleteEmployee(id);
        return Ok($"removed employee({id})");
    }
}