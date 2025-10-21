using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ClinicaApp.Models;
namespace ClinicaApp.Services;
public class PacienteService
{
    private readonly string FilePath;
    private List<Paciente> pacientes;

    public PacienteService()
    {
        // caminho do arquivo JSON
        var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? "";
        var projectDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", ".."));
        var dataDirectory = Path.Combine(projectDirectory, "Data");
        FilePath = Path.Combine(dataDirectory, "Pacientes.Json");

        // cria a pasta Data se não existir

        if (!Directory.Exists(dataDirectory))
        {
            Directory.CreateDirectory(dataDirectory);
        }
        // Se o arquivo já existir, ler os dados
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            pacientes = JsonConvert.DeserializeObject<List<Paciente>>(json) ?? new List<Paciente>();
        }
        // Se o arquivo não existir, inicializar a lista vazia
        else
        {
            pacientes = new List<Paciente>();
            SaveChanges();
        }
    }

    public List<Paciente> GetAll()
    {
        return pacientes;
    }

    public void Add(Paciente paciente)
    {
        // Validação simples antes de adicionar
        if (string.IsNullOrWhiteSpace(paciente.Nome))
            throw new ArgumentException("O nome do paciente é obrigatório.");

        if (!ValidarCPF(paciente.CPF))
            throw new ArgumentException("CPF Inválido.");

        paciente.Id = Guid.NewGuid();
        pacientes.Add(paciente);
        SaveChanges();
    }

    public void Update(Paciente paciente)
    {
        var existente = pacientes.FirstOrDefault(p => p.Id == paciente.Id);
        if (existente == null)
        {

        }
    }
}
