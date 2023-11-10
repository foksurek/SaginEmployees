using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaginEmployees.Dto;

public class EmployerDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}