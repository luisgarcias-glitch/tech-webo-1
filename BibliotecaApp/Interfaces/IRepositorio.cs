namespace BibliotecaApp.Interfaces;

// Interfaz genérica: define el contrato mínimo que cualquier repositorio
// de entidades debe cumplir, sin importar si guarda Libros, Usuarios, etc.
public interface IRepositorio<T>
{
    void Agregar(T item);
    bool Eliminar(Predicate<T> criterio);
    List<T> ObtenerTodos();
    T? Buscar(Predicate<T> criterio);
    bool Existe(Predicate<T> criterio);
}
