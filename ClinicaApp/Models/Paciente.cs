namespace ClinicaApp.Models;

public class Paciente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; } = DateTime.MinValue;
}
