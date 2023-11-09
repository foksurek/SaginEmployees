using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SaginEmployees.Models;

namespace SaginEmployees;

public class MongoDbService
{
    private readonly IMongoCollection<EmployeeModel> _employeesCollection;

    public MongoDbService(IOptions<MongoDbConnection> mongoDbConnection)
    { 
        var mongoClient = new MongoClient(mongoDbConnection.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbConnection.Value.DatabaseName);
        _employeesCollection = mongoDatabase.GetCollection<EmployeeModel>(mongoDbConnection.Value.EmployeesCollectionName);
    }
    
    
    public async Task<List<EmployeeModel>> GetEmployees() =>
        await _employeesCollection.Find(employee => true).ToListAsync();

    public async Task<EmployeeModel> GetEmployee(string id) => 
        await _employeesCollection.Find(employee => employee.Id == id).FirstOrDefaultAsync();

    public async Task CreateEmployee(EmployeeModel employee) =>
        await _employeesCollection.InsertOneAsync(employee);
    
    public async Task UpdateEmployee(string id, EmployeeModel employee) =>
        await _employeesCollection.ReplaceOneAsync(employee => employee.Id == id, employee);
    
    public async Task DeleteEmployee(string id) =>
        await _employeesCollection.DeleteOneAsync(employee => employee.Id == id);
}