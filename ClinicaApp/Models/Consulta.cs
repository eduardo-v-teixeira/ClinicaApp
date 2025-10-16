namespace ClinicaApp.Models;

public class Consulta
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PacienteId { get; set; } //referencia o paciente
    public Guid MedicoId { get; set; } // referencia o medico
    public DateTime DataHora { get; set; } // data e hora da consulta
    public decimal Valor { get; set; } = 0m;
    public string FormaPagamento { get; set; } = string.Empty; // dinheiro, cartão, convênio
    public string Status { get; set; } = "Agendada"; // Agendada, Concluída, Cancelada
    public bool Finalizada { get; set; } = false;
    public string Observacoes { get; set; } = string.Empty;
}
