using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaginEmployees.Dto;
using SaginEmployees.Services;

namespace SaginEmployees.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CurrencyController : Controller
{
    private readonly MongoDbService _mongoDb;

    public CurrencyController(MongoDbService mongoDb) => _mongoDb = mongoDb;

    [HttpGet("GetCurrency")]
    public async Task<List<CurrencyDto>> GetCurrency() => await _mongoDb.GetCurrencies();
}