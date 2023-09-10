using System;
using System.Collections.Generic;

namespace investigadores.Models;

public partial class Facultad
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<Investigadore> Investigadores { get; set; } = new List<Investigadore>();
}
