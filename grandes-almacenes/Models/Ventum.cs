using System;
using System.Collections.Generic;

namespace grandesAlmacenes.Models;

public partial class Ventum
{
    public int Cajero { get; set; }

    public int Maquina { get; set; }

    public int Producto { get; set; }

    public virtual Cajero CajeroNavigation { get; set; } = null!;

    public virtual Producto MaquinaNavigation { get; set; } = null!;

    public virtual MaquinasRegistradora ProductoNavigation { get; set; } = null!;
}
