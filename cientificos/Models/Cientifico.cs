using System;
using System.Collections.Generic;

namespace cientificos.Models;

public partial class Cientifico
{
    public string Dni { get; set; } = null!;

    public string? NomApels { get; set; }

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
