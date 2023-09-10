using System;
using System.Collections.Generic;

namespace grandesAlmacenes.Models;

public partial class Cajero
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
