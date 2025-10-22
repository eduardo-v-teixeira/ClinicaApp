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

    }
}
