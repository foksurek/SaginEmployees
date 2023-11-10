using DnsClient.Protocol;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaginEmployees.Dto;

public class EmployeeDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Status { get; set; }
    public Salary Salary { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Adress Address { get; set; }
}
public class Salary
{
    public double Amount { get; set; }
    public string CurrencyId { get; set; }
}
public class Adress
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}