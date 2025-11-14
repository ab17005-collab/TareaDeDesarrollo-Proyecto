using System;

public class Mascota
{
    public int IdMascota { get; set; }
    public string Nombre { get; set; }
    public string Especie { get; set; }
    public string Raza { get; set; }
    public string Sexo { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int IdCliente { get; set; }

    public Mascota() { }

    public Mascota(string nombre, string especie, string raza, string sexo, DateTime fechaNacimiento, int idCliente)
    {
        Nombre = nombre;
        Especie = especie;
        Raza = raza;
        Sexo = sexo;
        FechaNacimiento = fechaNacimiento;
        IdCliente = idCliente;
    }
}
