namespace ClinicaApp.Models;

public class Medico
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
    public string CRM { get; set; } = string.Empty;
    public string Especialidade { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

}
