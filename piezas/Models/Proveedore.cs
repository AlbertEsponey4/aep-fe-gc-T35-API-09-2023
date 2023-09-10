using System;
using System.Collections.Generic;

namespace piezas.Models;

public partial class Proveedore
{
    public string Codigo { get; set; } = null!;

    public string? Nombre { get; set; }

    public virtual ICollection<Suministra> Suministras { get; set; } = new List<Suministra>();
}
