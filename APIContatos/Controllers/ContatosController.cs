using Microsoft.AspNetCore.Mvc;
using APIContatos.Data;
using APIContatos.Models;

namespace APIContatos.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatosController : ControllerBase
{
    private readonly ILogger<ContatosController> _logger;
    private readonly ContatosRepository _repository;

    public ContatosController(ILogger<ContatosController> logger,
        ContatosRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public IEnumerable<Contato> Get()
    {
        var data = _repository.GetAll();
        _logger.LogInformation($"[{nameof(Get)}] No. de registros encontrados: {data.Count()}");
        return data;
    }
}