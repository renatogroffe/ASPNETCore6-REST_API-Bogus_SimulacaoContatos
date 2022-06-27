using APIContatos.Models;
using Bogus;
using Bogus.Extensions.Brazil;

namespace APIContatos.Data;

public class ContatosRepository
{
    private readonly List<Contato> _contatos;

    public ContatosRepository(
        ILogger<ContatosRepository> logger,
        IConfiguration configuration)
    {
        var numeroContatos = Convert.ToInt32(configuration["NumeroContatos"]);
        logger.LogInformation($"Gerando {numeroContatos} registro(s) de contato(s)...");
        _contatos = new Faker<Contato>("pt_BR").StrictMode(false)
            .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
            .RuleFor(c => c.Nome, f => f.Name.FullName())
            .RuleFor(c => c.CPF, f => f.Person.Cpf(true))
            .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome).ToLower())
            .RuleFor(c => c.Empresa, f => f.Company.CompanyName())
            .RuleFor(c => c.CNPJ, f => f.Company.Cnpj(true))
            .Generate(numeroContatos);
    }

    public List<Contato> GetAll()
    {
        return _contatos;
    }
}