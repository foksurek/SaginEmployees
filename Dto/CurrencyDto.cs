using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaginEmployees.Dto;

public class CurrencyDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Symbol { get; set; } = null!;
    public double DolarRate { get; set; }
}