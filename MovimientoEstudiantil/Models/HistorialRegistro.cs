using System;

public class HistorialRegistro
{
    public int IdHistorial { get; set; }

    public int IdUsuario { get; set; }

    public string Accion { get; set; }

    public string Descripcion { get; set; } 

    public DateTime FechaRegistro { get; set; }

    public TimeSpan Hora { get; set; }

    public string Rol { get; set; } 
}
