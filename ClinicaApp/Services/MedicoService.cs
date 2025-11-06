using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ClinicaApp.Models;

namespace ClinicaApp.Services;

public class MedicoService
{
    private readonly string FilePath;
    private List<Medico> medicos;

    public MedicoService()
    {
        // Local do assembly (DLL/ Exe) que está sendo executado
        var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;

        // Diretório do assembly
        var assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? "";

        // sobe 3 niveis a partir do bin/Debug/net8.0 para chegar na raiz do projeto
        var projectDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", ".."));

        //pasta onde os jsons serão salvos
        var dataDirectory = Path.Combine(projectDirectory, "Data");

        //Arquivo especifico para médicos
        FilePath = Path.Combine(dataDirectory, "Medicos.Json");

        //Gartnate que a pasta existe e evita DirectoryNotFoundException
        if (!Directory.Exists(dataDirectory))
        {
            Directory.CreateDirectory(dataDirectory);
        }

        // Se o arquivo já existir, ler os dados
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            medicos = JsonConvert.DeserializeObject<List<Medico>>(json) ?? new List<Medico>();
        }
        else
        {
            medicos = new List<Medico>();
            SaveChanges();
        }
    }

    //Retorna todos os medicos

    public List<Medico> GetAll()
    {
        return medicos;
    }

    // busca medicos por especialidade

    public List<Medico> GetByEspecialidade(string especialidade)
    {
        if (string.IsNullOrWhiteSpace(especialidade))

            return medicos;

        return medicos
            .Where(m => m.Especialidade != null && m.Especialidade
            .Contains(especialidade, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    //Adiciona um novo medico (com validação simples)
    public void Add(Medico medico)
    {
        if (string.IsNullOrWhiteSpace(medico.Nome))
            throw new ArgumentException("O nome do medico é obrigatorio.");

        if (string.IsNullOrWhiteSpace(medico.CRM))
            throw new ArgumentException("o CRM do medico é obrigatiorio.");
    
        //evita cadastro duplicado por CRM
        if (medicos.Any(m => m.CRM.Equals(medico.CRM, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Já existe um médico cadastrado com esse CRM.");

        medico.Id = Guid.NewGuid();
        medicos.Add(medico);
        SaveChanges();
    }

    //Atualiza um medico existente (procura por ID)
    public void Update(Medico medico)
    {
        var existente = medicos.FirstOrDefault(m => m.Id == medico.Id);
        if (existente != null)
        {
            existente.Nome = medico.Nome;

            existente.CRM = medico.CRM;
            existente.Especialidade = medico.Especialidade;
            existente.Telefone = medico.Telefone;
            SaveChanges();
        }
    }

    //Salva as mudanças no arquivo JSON
    private void SaveChanges()
    {
        var json = JsonConvert.SerializeObject(medicos, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

}
