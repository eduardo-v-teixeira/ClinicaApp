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
        if (existente != null)
        {
            existente.Nome = paciente.Nome;
            existente.CPF = paciente.CPF;
            existente.Telefone = paciente.Telefone;
            existente.Endereco = paciente.Endereco;
            existente.DataNascimento = paciente.DataNascimento;
            SaveChanges();
        }
    }

    public void Delete(Guid id)
    {
        var paciente = pacientes.FirstOrDefault(p => p.Id == id);
        if (paciente != null)
        {
            pacientes.Remove(paciente);
            SaveChanges();
        }
    }

    private void SaveChanges()
    {
        var json = JsonConvert.SerializeObject(pacientes, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    public static bool ValidarCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        if (new string(cpf[0], cpf.Length) == cpf)
            return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        string digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }

}
