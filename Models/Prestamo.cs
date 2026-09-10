namespace BibliotecaApp.Models;


public record Prestamo(
    string LibroCodigo,
    string UsuarioId,
    DateTime FechaPrestamo,
    DateTime? FechaDevolucion
)
{
    public bool Activo => FechaDevolucion is null;
}
