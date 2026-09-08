namespace BibliotecaApp.Models;

// Record: tipo inmutable ideal para representar un "hecho" como un préstamo.
// Cada vez que se devuelve un libro, no modificamos el objeto: creamos una
// copia nueva con "with" y la fecha de devolución actualizada.
public record Prestamo(
    string LibroCodigo,
    string UsuarioId,
    DateTime FechaPrestamo,
    DateTime? FechaDevolucion
)
{
    // Propiedad calculada: true si el préstamo sigue activo (no devuelto).
    public bool Activo => FechaDevolucion is null;
}
