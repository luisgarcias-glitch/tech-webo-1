# Sistema de Gestión de Biblioteca

Aplicación de consola en **C# / .NET 10** para administrar el catálogo de
libros, los usuarios y los préstamos de una biblioteca.

## Funcionalidades

- Registrar libros (título, autor, categoría, código, disponibilidad).
- Registrar usuarios (identificador, nombre, correo).
- Listar, buscar por código y eliminar libros.
- Registrar préstamos y devoluciones, con validación de disponibilidad.
- Consultar libros disponibles y préstamos activos.
- Manejo de errores controlado: el programa nunca se cierra ante un dato
  inválido o un elemento inexistente.

## Estructura del proyecto

```
BibliotecaApp/
├── Program.cs                  # Menú de consola (punto de entrada)
├── Models/
│   ├── Libro.cs                # Clase Libro
│   ├── Usuario.cs              # Clase Usuario
│   └── Prestamo.cs             # Record Prestamo (inmutable)
├── Interfaces/
│   └── IRepositorio.cs         # Interfaz genérica IRepositorio<T>
├── Repositories/
│   └── Repositorio.cs          # Implementación genérica sobre List<T>
├── Services/
│   └── BibliotecaService.cs    # Lógica de negocio y consultas LINQ
└── Exceptions/
    └── BibliotecaExceptions.cs # Excepciones personalizadas del dominio
```

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Cómo ejecutar

```bash
# Clonar el repositorio
git clone <url-del-repositorio>
cd BibliotecaApp

# Restaurar y ejecutar
dotnet restore
dotnet run
```

Al iniciar, el sistema carga dos libros y un usuario de ejemplo para poder
probar el menú de inmediato.

## Reglas de negocio implementadas

- No se puede prestar un libro inexistente o no disponible.
- No se puede devolver un préstamo que no existe o ya fue devuelto.
- Los códigos de libro e identificadores de usuario son únicos.
- Las búsquedas sin resultados muestran un mensaje claro en vez de fallar.
- Cualquier error de validación se captura y el programa continúa activo.

## Consultas LINQ incluidas

1. Libros disponibles (`Where`).
2. Libros por autor o categoría (`Where`).
3. Libros ordenados por título (`OrderBy`).
4. Búsqueda de libro por código (`FirstOrDefault`).
5. Resumen de préstamos activos (`Where` + `Select`).
