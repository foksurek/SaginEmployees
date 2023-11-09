namespace SaginEmployees.Models;

public class MongoDbConnection
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string EmployeesCollectionName { get; set; }
}

