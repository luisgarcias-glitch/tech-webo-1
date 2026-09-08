using BibliotecaApp.Exceptions;
using BibliotecaApp.Interfaces;
using BibliotecaApp.Models;

namespace BibliotecaApp.Services;

// Servicio central: coordina los repositorios de Libro y Usuario, y la
// lista de Prestamos. Aquí viven las reglas de negocio y las consultas LINQ.
public class BibliotecaService
{
    private readonly IRepositorio<Libro> _libros;
    private readonly IRepositorio<Usuario> _usuarios;
    private readonly List<Prestamo> _prestamos = new();

    // Array de tamaño fijo: categorías válidas del catálogo.
    // (Requisito: "Array para datos fijos").
    private static readonly string[] CategoriasValidas =
    {
        "Ficción", "Ciencia", "Historia", "Tecnología", "Infantil"
    };

    public BibliotecaService(IRepositorio<Libro> libros, IRepositorio<Usuario> usuarios)
    {
        _libros = libros;
        _usuarios = usuarios;
    }

    public string[] ObtenerCategoriasValidas() => CategoriasValidas;

    // ---------------- LIBROS ----------------

    public void RegistrarLibro(string codigo, string titulo, string autor, string categoria)
    {
        if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El código y el título son obligatorios.");

        if (_libros.Existe(l => l.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)))
            throw new CodigoDuplicadoException(codigo);

        if (!CategoriasValidas.Contains(categoria, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException(
                $"Categoría inválida. Use una de: {string.Join(", ", CategoriasValidas)}");

        _libros.Agregar(new Libro(codigo, titulo, autor, categoria));
    }

    public bool EliminarLibro(string codigo)
    {
        var libro = _libros.Buscar(l => l.Codigo == codigo)
            ?? throw new LibroNoEncontradoException(codigo);

        if (!libro.Disponible)
            throw new InvalidOperationException(
                "No se puede eliminar un libro que está actualmente prestado.");

        return _libros.Eliminar(l => l.Codigo == codigo);
    }

    // LINQ #1: libros disponibles (Where)
    public List<Libro> ObtenerLibrosDisponibles()
    {
        return _libros.ObtenerTodos()
            .Where(l => l.Disponible)
            .ToList();
    }

    // LINQ #2: libros por autor o categoría (Where con condición flexible)
    public List<Libro> BuscarPorAutorOCategoria(string? autor, string? categoria)
    {
        return _libros.ObtenerTodos()
            .Where(l =>
                (!string.IsNullOrWhiteSpace(autor) &&
                    l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrWhiteSpace(categoria) &&
                    l.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    // LINQ #3: libros ordenados por título (OrderBy)
    public List<Libro> ObtenerLibrosOrdenadosPorTitulo()
    {
        return _libros.ObtenerTodos()
            .OrderBy(l => l.Titulo)
            .ToList();
    }

    // LINQ #4: búsqueda de libro por código (FirstOrDefault)
    public Libro BuscarLibroPorCodigo(string codigo)
    {
        return _libros.ObtenerTodos()
            .FirstOrDefault(l => l.Codigo == codigo)
            ?? throw new LibroNoEncontradoException(codigo);
    }

    // ---------------- USUARIOS ----------------

    public void RegistrarUsuario(string id, string nombre, string correo)
    {
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El identificador y el nombre son obligatorios.");

        if (_usuarios.Existe(u => u.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
            throw new CodigoDuplicadoException(id);

        if (!correo.Contains('@'))
            throw new ArgumentException("El correo no tiene un formato válido.");

        _usuarios.Agregar(new Usuario(id, nombre, correo));
    }

    public List<Usuario> ObtenerUsuarios() => _usuarios.ObtenerTodos();

    // ---------------- PRÉSTAMOS ----------------

    public void RegistrarPrestamo(string codigoLibro, string idUsuario)
    {
        var libro = _libros.Buscar(l => l.Codigo == codigoLibro)
            ?? throw new LibroNoEncontradoException(codigoLibro);

        var usuario = _usuarios.Buscar(u => u.Id == idUsuario)
            ?? throw new UsuarioNoEncontradoException(idUsuario);

        if (!libro.Disponible)
            throw new LibroNoDisponibleException(codigoLibro);

        libro.Disponible = false;
        _prestamos.Add(new Prestamo(codigoLibro, idUsuario, DateTime.Now, null));
    }

    public void RegistrarDevolucion(string codigoLibro)
    {
        int indice = _prestamos.FindIndex(p => p.LibroCodigo == codigoLibro && p.Activo);

        if (indice == -1)
            throw new PrestamoNoEncontradoException(codigoLibro);

        // Los records son inmutables: creamos una copia actualizada con "with".
        _prestamos[indice] = _prestamos[indice] with { FechaDevolucion = DateTime.Now };

        var libro = _libros.Buscar(l => l.Codigo == codigoLibro)
            ?? throw new LibroNoEncontradoException(codigoLibro);
        libro.Disponible = true;
    }

    // LINQ #5 (extra): préstamos activos, proyectados con Select a un resumen.
    public List<string> ObtenerResumenPrestamosActivos()
    {
        return _prestamos
            .Where(p => p.Activo)
            .Select(p =>
            {
                string titulo = _libros.Buscar(l => l.Codigo == p.LibroCodigo)?.Titulo ?? "(desconocido)";
                string nombre = _usuarios.Buscar(u => u.Id == p.UsuarioId)?.Nombre ?? "(desconocido)";
                return $"{titulo} -> prestado a {nombre} el {p.FechaPrestamo:dd/MM/yyyy}";
            })
            .ToList();
    }
}
