namespace BibliotecaApp.Interfaces;


public interface IRepositorio<T>
{
    void Agregar(T item);
    bool Eliminar(Predicate<T> criterio);
    List<T> ObtenerTodos();
    T? Buscar(Predicate<T> criterio);
    bool Existe(Predicate<T> criterio);
}
