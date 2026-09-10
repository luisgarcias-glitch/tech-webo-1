using BibliotecaApp.Interfaces;

namespace BibliotecaApp.Repositories;


public class Repositorio<T> : IRepositorio<T>
{
    private readonly List<T> _items = new();

    public void Agregar(T item)
    {
        _items.Add(item);
    }

    public bool Eliminar(Predicate<T> criterio)
    {
        int eliminados = _items.RemoveAll(criterio);
        return eliminados > 0;
    }

    public List<T> ObtenerTodos()
    {
        return new List<T>(_items);
    }

    public T? Buscar(Predicate<T> criterio)
    {
        return _items.FirstOrDefault(x => criterio(x));
    }

    public bool Existe(Predicate<T> criterio)
    {
        return _items.Exists(criterio);
    }
}
