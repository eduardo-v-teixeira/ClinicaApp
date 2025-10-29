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

}
