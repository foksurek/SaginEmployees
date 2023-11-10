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
}