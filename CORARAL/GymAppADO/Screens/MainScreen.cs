using Spectre.Console;
using GymAppADO.Services;
using GymAppADO.Models;

namespace GymAppADO.Screens
{
    public class MainScreen
    {
        private readonly IMiembroService _miembroService;

        public MainScreen(IMiembroService miembroService)
        {
            _miembroService = miembroService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new FigletText("Gym ADO.NET").Color(Color.Blue));

                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("¿Qué acción deseas realizar?")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "1. Registrar un nuevo miembro",
                            "2. Listar todos los miembros",
                            "3. Buscar un miembro por cédula",
                            "4. Actualizar el teléfono de un miembro",
                            "5. Eliminar un miembro",
                            "6. Salir"
                        }));

                switch (opcion.Substring(0, 1))
                {
                    case "1": RegistrarMiembro(); break;
                    case "2": ListarMiembros(); break;
                    case "3": BuscarMiembro(); break;
                    case "4": ActualizarTelefono(); break;
                    case "5": EliminarMiembro(); break;
                    case "6": return;
                }

                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[grey]Presiona cualquier tecla para continuar...[/]");
                System.Console.ReadKey(true);
            }
        }

        private void RegistrarMiembro()
        {
            AnsiConsole.MarkupLine("[bold green]--- Registrar Miembro ---[/]");
            var nombre = AnsiConsole.Ask<string>("Nombre Completo:");
            var cedula = AnsiConsole.Ask<string>("Cédula:");
            var telefono = AnsiConsole.Ask<string>("Teléfono:");

            _miembroService.RegistrarMiembro(nombre, cedula, telefono);
            AnsiConsole.MarkupLine("[bold lime]Miembro registrado exitosamente.[/]");
        }

        private void ListarMiembros()
        {
            AnsiConsole.MarkupLine("[bold blue]--- Listar Miembros ---[/]");
            var miembros = _miembroService.ListarMiembros();

            if (miembros.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No hay miembros registrados.[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("Nombre Completo");
            table.AddColumn("Cédula");
            table.AddColumn("Teléfono");

            foreach (var m in miembros)
            {
                table.AddRow(m.NombreCompleto, m.Cedula, m.Telefono);
            }

            AnsiConsole.Write(table);
        }

        private void BuscarMiembro()
        {
            AnsiConsole.MarkupLine("[bold cyan]--- Buscar Miembro ---[/]");
            var cedula = AnsiConsole.Ask<string>("Ingresa la cédula del miembro a buscar:");

            var miembro = _miembroService.BuscarPorCedula(cedula);
            if (miembro != null)
            {
                var panel = new Panel(
                    $"[bold]Nombre:[/] {miembro.NombreCompleto}\n" +
                    $"[bold]Cédula:[/] {miembro.Cedula}\n" +
                    $"[bold]Teléfono:[/] {miembro.Telefono}");
                panel.Header = new PanelHeader("Detalles del Miembro");
                AnsiConsole.Write(panel);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
            }
        }

        private void ActualizarTelefono()
        {
            AnsiConsole.MarkupLine("[bold yellow]--- Actualizar Teléfono ---[/]");
            var cedula = AnsiConsole.Ask<string>("Ingresa la cédula del miembro a actualizar:");
            var nuevoTelefono = AnsiConsole.Ask<string>("Ingresa el nuevo teléfono:");

            if (_miembroService.ActualizarTelefono(cedula, nuevoTelefono))
            {
                AnsiConsole.MarkupLine("[bold green]Teléfono actualizado exitosamente.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
            }
        }

        private void EliminarMiembro()
        {
            AnsiConsole.MarkupLine("[bold red]--- Eliminar Miembro ---[/]");
            var cedula = AnsiConsole.Ask<string>("Ingresa la cédula del miembro a eliminar:");

            if (_miembroService.EliminarMiembro(cedula))
            {
                AnsiConsole.MarkupLine("[bold green]Miembro eliminado exitosamente.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
            }
        }
    }
}
