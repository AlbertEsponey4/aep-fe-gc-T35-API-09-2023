using System;
using System.Collections.Generic;

namespace grandesAlmacenes.Models;

public partial class MaquinasRegistradora
{
    public int Codigo { get; set; }

    public int? Piso { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
