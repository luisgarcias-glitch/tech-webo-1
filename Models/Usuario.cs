namespace BibliotecaApp.Models;

public class Usuario
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }

    public Usuario(string id, string nombre, string correo)
    {
        Id = id;
        Nombre = nombre;
        Correo = correo;
    }

    public override string ToString()
    {
        return $"[{Id}] {Nombre} - {Correo}";
    }
}
