using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaginEmployees.Models;

public class EmployeeModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public Salary Salary { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Adress Address { get; set; }
}
public class Salary
{
    public double Amount { get; set; }
    public string Currency { get; set; }
}
public class Adress
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}