using BibliotecaApp.Interfaces;
using BibliotecaApp.Models;
using BibliotecaApp.Repositories;
using BibliotecaApp.Services;


IRepositorio<Libro> repositorioLibros = new Repositorio<Libro>();
IRepositorio<Usuario> repositorioUsuarios = new Repositorio<Usuario>();
BibliotecaService biblioteca = new(repositorioLibros, repositorioUsuarios);

SembrarDatosDePrueba(biblioteca);

bool continuar = true;

while (continuar)
{
    MostrarMenu();
    string opcion = Console.ReadLine() ?? string.Empty;

    try
    {
        switch (opcion)
        {
            case "1":
                RegistrarLibro(biblioteca);
                break;
            case "2":
                RegistrarUsuario(biblioteca);
                break;
            case "3":
                ListarLibros(biblioteca);
                break;
            case "4":
                BuscarLibro(biblioteca);
                break;
            case "5":
                EliminarLibro(biblioteca);
                break;
            case "6":
                RegistrarPrestamo(biblioteca);
                break;
            case "7":
                RegistrarDevolucion(biblioteca);
                break;
            case "8":
                ConsultarDisponibles(biblioteca);
                break;
            case "9":
                ConsultarPrestamosActivos(biblioteca);
                break;
            case "0":
                continuar = false;
                Console.WriteLine("¡Hasta luego!");
                break;
            default:
                Console.WriteLine("Opción inválida. Intente de nuevo.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n⚠ Error: {ex.Message}\n");
    }

    if (continuar)
    {
        Console.WriteLine("\nPresione ENTER para continuar...");
        Console.ReadLine();
    }
}


static void MostrarMenu()
{
    Console.Clear();
    Console.WriteLine("===== SISTEMA DE GESTIÓN DE BIBLIOTECA =====");
    Console.WriteLine("1. Registrar libro");
    Console.WriteLine("2. Registrar usuario");
    Console.WriteLine("3. Listar libros (ordenados por título)");
    Console.WriteLine("4. Buscar libro por código");
    Console.WriteLine("5. Eliminar libro");
    Console.WriteLine("6. Registrar préstamo");
    Console.WriteLine("7. Registrar devolución");
    Console.WriteLine("8. Consultar libros disponibles");
    Console.WriteLine("9. Consultar préstamos activos");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opción: ");
}

static void RegistrarLibro(BibliotecaService biblioteca)
{
    Console.Write("Código: ");
    string codigo = Console.ReadLine() ?? string.Empty;
    Console.Write("Título: ");
    string titulo = Console.ReadLine() ?? string.Empty;
    Console.Write("Autor: ");
    string autor = Console.ReadLine() ?? string.Empty;

    Console.WriteLine($"Categorías válidas: {string.Join(", ", biblioteca.ObtenerCategoriasValidas())}");
    Console.Write("Categoría: ");
    string categoria = Console.ReadLine() ?? string.Empty;

    biblioteca.RegistrarLibro(codigo, titulo, autor, categoria);
    Console.WriteLine("Libro registrado correctamente.");
}

static void RegistrarUsuario(BibliotecaService biblioteca)
{
    Console.Write("Identificador: ");
    string id = Console.ReadLine() ?? string.Empty;
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine() ?? string.Empty;
    Console.Write("Correo: ");
    string correo = Console.ReadLine() ?? string.Empty;

    biblioteca.RegistrarUsuario(id, nombre, correo);
    Console.WriteLine("Usuario registrado correctamente.");
}

static void ListarLibros(BibliotecaService biblioteca)
{
    var libros = biblioteca.ObtenerLibrosOrdenadosPorTitulo();

    if (libros.Count == 0)
    {
        Console.WriteLine("No hay libros registrados.");
        return;
    }

    foreach (var libro in libros)
        Console.WriteLine(libro);
}

static void BuscarLibro(BibliotecaService biblioteca)
{
    Console.Write("Código del libro: ");
    string codigo = Console.ReadLine() ?? string.Empty;

    var libro = biblioteca.BuscarLibroPorCodigo(codigo);
    Console.WriteLine(libro);
}

static void EliminarLibro(BibliotecaService biblioteca)
{
    Console.Write("Código del libro a eliminar: ");
    string codigo = Console.ReadLine() ?? string.Empty;

    biblioteca.EliminarLibro(codigo);
    Console.WriteLine("Libro eliminado correctamente.");
}

static void RegistrarPrestamo(BibliotecaService biblioteca)
{
    Console.Write("Código del libro: ");
    string codigo = Console.ReadLine() ?? string.Empty;
    Console.Write("Identificador del usuario: ");
    string idUsuario = Console.ReadLine() ?? string.Empty;

    biblioteca.RegistrarPrestamo(codigo, idUsuario);
    Console.WriteLine("Préstamo registrado correctamente.");
}

static void RegistrarDevolucion(BibliotecaService biblioteca)
{
    Console.Write("Código del libro a devolver: ");
    string codigo = Console.ReadLine() ?? string.Empty;

    biblioteca.RegistrarDevolucion(codigo);
    Console.WriteLine("Devolución registrada correctamente.");
}

static void ConsultarDisponibles(BibliotecaService biblioteca)
{
    var disponibles = biblioteca.ObtenerLibrosDisponibles();

    if (disponibles.Count == 0)
    {
        Console.WriteLine("No hay libros disponibles en este momento.");
        return;
    }

    foreach (var libro in disponibles)
        Console.WriteLine(libro);
}

static void ConsultarPrestamosActivos(BibliotecaService biblioteca)
{
    var resumen = biblioteca.ObtenerResumenPrestamosActivos();

    if (resumen.Count == 0)
    {
        Console.WriteLine("No hay préstamos activos.");
        return;
    }

    foreach (var linea in resumen)
        Console.WriteLine(linea);
}

static void SembrarDatosDePrueba(BibliotecaService biblioteca)
{
    biblioteca.RegistrarLibro("L001", "Cien años de soledad", "Gabriel García Márquez", "Ficción");
    biblioteca.RegistrarLibro("L002", "Breve historia del tiempo", "Stephen Hawking", "Ciencia");
    biblioteca.RegistrarUsuario("U001", "Ana Torres", "ana.torres@correo.com");
}
