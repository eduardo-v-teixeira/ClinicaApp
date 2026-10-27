# ClinicaApp

[Português](README.md) | **English**

The start of a **WPF (.NET 8)** desktop application to manage a clinic: patients, doctors and appointments, with data stored in JSON files.

> **Status: work in progress.** The models and the patient and doctor services are written, but the user interface has not been built yet: the main window is empty and no screen calls the services.

## Features

### Implemented (service layer)

- **Patients** (`PacienteService`): list, add, update and delete.
  - When adding, the name is required and the CPF (Brazilian taxpayer ID) is validated: check digits and rejection of CPFs with all digits equal.
- **Doctors** (`MedicoService`): list, search by specialty (case-insensitive), add and update.
  - When adding, the name and the CRM (Brazilian medical license number) are required, and two doctors with the same CRM are rejected.
- **JSON persistence** with Newtonsoft.Json: each service reads its file when it is created and rewrites the file on every change.

### Not implemented yet

- Registration and search screens.
- Appointment scheduling: only the `Consulta` (appointment) model exists, with patient, doctor, date and time, price, payment method, status (scheduled/completed/cancelled) and notes. There is no service for it.

## Tech stack

- **C#** / **.NET 8** (`net8.0-windows`)
- **WPF** (XAML)
- **Newtonsoft.Json** 13.0.1
- **LINQ**

## Structure

```
ClinicaApp/
├── ClinicaApp.sln
└── ClinicaApp/
    ├── Models/       # Paciente (patient), Medico (doctor), Consulta (appointment)
    ├── Services/     # PacienteService, MedicoService (rules and JSON read/write)
    ├── Data/         # Folder for the JSON files
    ├── App.xaml
    └── MainWindow.xaml   # Main window (still empty)
```

The services write `Data/Pacientes.Json` and `Data/Medicos.Json` inside the project folder, which they find by going three levels up from the executable's folder (default `bin/Debug/net8.0-windows` output).

## How to run

Requirements: **Windows** and the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0). Visual Studio 2022 is optional.

From the repository root:

```bash
dotnet restore ClinicaApp.sln
dotnet build ClinicaApp.sln
dotnet run --project ClinicaApp/ClinicaApp.csproj
```

At this stage the program opens an empty window.

## Next steps

- Patient and doctor screens using the existing services.
- Appointment scheduling service and screen.
