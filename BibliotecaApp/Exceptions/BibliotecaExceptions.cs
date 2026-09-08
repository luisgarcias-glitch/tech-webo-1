namespace BibliotecaApp.Exceptions;

// Excepciones propias: heredan de Exception y describen errores de negocio
// específicos de la biblioteca, en vez de usar excepciones genéricas.

public class LibroNoEncontradoException : Exception
{
    public LibroNoEncontradoException(string codigo)
        : base($"No existe un libro con el código '{codigo}'.") { }
}

public class LibroNoDisponibleException : Exception
{
    public LibroNoDisponibleException(string codigo)
        : base($"El libro con código '{codigo}' no está disponible para préstamo.") { }
}

public class UsuarioNoEncontradoException : Exception
{
    public UsuarioNoEncontradoException(string id)
        : base($"No existe un usuario con el identificador '{id}'.") { }
}

public class PrestamoNoEncontradoException : Exception
{
    public PrestamoNoEncontradoException(string codigoLibro)
        : base($"No existe un préstamo activo para el libro '{codigoLibro}'.") { }
}

public class CodigoDuplicadoException : Exception
{
    public CodigoDuplicadoException(string codigo)
        : base($"El código o identificador '{codigo}' ya está registrado.") { }
}
