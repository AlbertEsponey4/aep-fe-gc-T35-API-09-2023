using System;
using System.Collections.Generic;

namespace investigadores.Models;

public partial class Reserva
{
    public string Dni { get; set; } = null!;

    public string NumSerie { get; set; } = null!;

    public DateOnly? Comienzo { get; set; }

    public DateOnly? Fin { get; set; }

    public virtual Investigadore DniNavigation { get; set; } = null!;

    public virtual Equipo NumSerieNavigation { get; set; } = null!;
}
