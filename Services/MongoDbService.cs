using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SaginEmployees.Dto;
using SaginEmployees.Models;

namespace SaginEmployees.Services;

public class MongoDbService
{
    private readonly IMongoCollection<EmployeeDto> _employeesCollection;
    private readonly IMongoCollection<EmployerDto> _employersCollection;
    private readonly IMongoCollection<CurrencyDto> _currenciesCollection;

    public MongoDbService(IOptions<MongoDbConnection> mongoDbConnection, IMongoClient mongoClient)
    {
        var mongoDatabase = mongoClient.GetDatabase(mongoDbConnection.Value.DatabaseName);
        _employeesCollection = mongoDatabase.GetCollection<EmployeeDto>(mongoDbConnection.Value.EmployeesCollectionName);
        _employersCollection = mongoDatabase.GetCollection<EmployerDto>(mongoDbConnection.Value.EmployersCollectionName);
        _currenciesCollection = mongoDatabase.GetCollection<CurrencyDto>(mongoDbConnection.Value.CurrenciesCollectionName);
    }

    public async Task<List<EmployeeDto>> GetEmployees() =>
        await _employeesCollection.Find(employee => true).ToListAsync();

    public async Task<EmployeeDto> GetEmployee(string id) => 
        await _employeesCollection.Find(employee => employee.Id == id).FirstOrDefaultAsync();
    
    public async Task<List<EmployeeDto>> GetEmployeesByDepartmentName(string department) =>
        await _employeesCollection.Find(employee => employee.Department == department).ToListAsync();

    public async Task AddEmployee(EmployeeDto employee) =>
        await _employeesCollection.InsertOneAsync(employee);
    
    public async Task UpdateEmployee(string id, EmployeeDto employee) =>
        await _employeesCollection.ReplaceOneAsync(employee => employee.Id == id, employee);
    
    public async Task DeleteEmployee(string id) =>
        await _employeesCollection.DeleteOneAsync(employee => employee.Id == id);


    // EMPLOYERS
    // EMPLOYERS
    // EMPLOYERS
    
    
    public async Task CreateEmployer(EmployerDto user) =>
        await _employersCollection.InsertOneAsync(user);
    
    public async Task<EmployerDto> GetEmployer(string id) =>
        await _employersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
    
    public async Task<EmployerDto> GetEmployerByEmail(string email) =>
        await _employersCollection.Find(user => user.Email == email).FirstOrDefaultAsync();
    
    public async Task UpdateEmployer(string id, EmployerDto user) =>
        await _employersCollection.ReplaceOneAsync(user => user.Id == id, user);
    
    
    // CURRENCIES
    // CURRENCIES
    // CURRENCIES
    
    public async Task<List<CurrencyDto>> GetCurrencies() =>
        await _currenciesCollection.Find(currency => true).ToListAsync();
    
    public async Task<CurrencyDto> GetCurrency(string id) =>
        await _currenciesCollection.Find(currency => currency.Id == id).FirstOrDefaultAsync();
    
    public async Task AddCurrency(CurrencyDto currency) =>
        await _currenciesCollection.InsertOneAsync(currency);
    
    public async Task UpdateCurrency(string id, CurrencyDto currency) =>
        await _currenciesCollection.ReplaceOneAsync(currency => currency.Id == id, currency);
    
    public async Task DeleteCurrency(string id) =>
        await _currenciesCollection.DeleteOneAsync(currency => currency.Id == id);
    

}