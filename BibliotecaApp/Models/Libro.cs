namespace BibliotecaApp.Models;


public class Libro
{
    public string Codigo { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Categoria { get; set; }
    public bool Disponible { get; set; }

    public Libro(string codigo, string titulo, string autor, string categoria, bool disponible = true)
    {
        Codigo = codigo;
        Titulo = titulo;
        Autor = autor;
        Categoria = categoria;
        Disponible = disponible;
    }

    public override string ToString()
    {
        string estado = Disponible ? "Disponible" : "Prestado";
        return $"[{Codigo}] \"{Titulo}\" - {Autor} ({Categoria}) - {estado}";
    }
}
