# ClinicaApp

**Português** | [English](README.en.md)

Início de uma aplicação desktop em **WPF (.NET 8)** para gestão de uma clínica: pacientes, médicos e consultas, com dados em arquivos JSON.

> **Status: em desenvolvimento.** Os modelos e os serviços de pacientes e médicos estão escritos, mas a interface ainda não foi construída: a janela principal está vazia e nenhuma tela chama os serviços.

## Funcionalidades

### Implementado (camada de serviços)

- **Pacientes** (`PacienteService`): listar, incluir, atualizar e excluir.
  - Na inclusão, o nome é obrigatório e o CPF é validado (dígitos verificadores e rejeição de CPFs com todos os dígitos iguais).
- **Médicos** (`MedicoService`): listar, buscar por especialidade (sem diferenciar maiúsculas e minúsculas), incluir e atualizar.
  - Na inclusão, nome e CRM são obrigatórios e não é permitido cadastrar dois médicos com o mesmo CRM.
- **Persistência em JSON** com Newtonsoft.Json: cada serviço lê o seu arquivo ao ser criado e regrava o arquivo a cada alteração.

### Ainda não implementado

- Telas de cadastro e consulta.
- Agendamento de consultas: existe apenas o modelo `Consulta` (paciente, médico, data e hora, valor, forma de pagamento, status Agendada/Concluída/Cancelada e observações), sem serviço.

## Tecnologias

- **C#** / **.NET 8** (`net8.0-windows`)
- **WPF** (XAML)
- **Newtonsoft.Json** 13.0.1
- **LINQ**

## Estrutura

```
ClinicaApp/
├── ClinicaApp.sln
└── ClinicaApp/
    ├── Models/       # Paciente, Medico, Consulta
    ├── Services/     # PacienteService, MedicoService (regras e leitura/gravação dos JSON)
    ├── Data/         # Pasta dos arquivos JSON
    ├── App.xaml
    └── MainWindow.xaml   # Janela principal (ainda vazia)
```

Os serviços gravam `Data/Pacientes.Json` e `Data/Medicos.Json` dentro da pasta do projeto, que localizam subindo três níveis a partir da pasta do executável (saída padrão `bin/Debug/net8.0-windows`).

## Como executar

Requisitos: **Windows** e [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0). O Visual Studio 2022 é opcional.

Na raiz do repositório:

```bash
dotnet restore ClinicaApp.sln
dotnet build ClinicaApp.sln
dotnet run --project ClinicaApp/ClinicaApp.csproj
```

Nesta fase, o programa abre uma janela vazia.

## Próximos passos

- Telas de pacientes e médicos usando os serviços existentes.
- Serviço e tela de agendamento de consultas.
