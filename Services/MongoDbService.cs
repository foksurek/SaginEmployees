using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SaginEmployees.Dto;
using SaginEmployees.Models;

namespace SaginEmployees.Services;

public class MongoDbService
{
    private readonly IMongoCollection<EmployeeDto> _employeesCollection;

    public MongoDbService(IOptions<MongoDbConnection> mongoDbConnection, IMongoClient mongoClient)
    {
        var mongoDatabase = mongoClient.GetDatabase(mongoDbConnection.Value.DatabaseName);
        _employeesCollection = mongoDatabase.GetCollection<EmployeeDto>(mongoDbConnection.Value.EmployeesCollectionName);
    }

    public async Task<List<EmployeeDto>> GetEmployees() =>
        await _employeesCollection.Find(employee => true).ToListAsync();

    public async Task<EmployeeDto> GetEmployee(string id) => 
        await _employeesCollection.Find(employee => employee.Id == id).FirstOrDefaultAsync();

    public async Task CreateEmployee(EmployeeDto employee) =>
        await _employeesCollection.InsertOneAsync(employee);
    
    public async Task UpdateEmployee(string id, EmployeeDto employee) =>
        await _employeesCollection.ReplaceOneAsync(employee => employee.Id == id, employee);
    
    public async Task DeleteEmployee(string id) =>
        await _employeesCollection.DeleteOneAsync(employee => employee.Id == id);
}