namespace SaginEmployees.Models;

public class MongoDbConnection
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string EmployeesCollectionName { get; set; }
    public string EmployersCollectionName { get; set; }
    public string CurrenciesCollectionName { get; set; }
}

