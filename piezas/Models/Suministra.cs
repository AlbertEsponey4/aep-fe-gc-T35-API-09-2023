using System;
using System.Collections.Generic;

namespace piezas.Models;

public partial class Suministra
{
    public int CodigoPieza { get; set; }

    public string IdProveedor { get; set; } = null!;

    public int? Precio { get; set; }

    public virtual Pieza CodigoPiezaNavigation { get; set; } = null!;

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}
