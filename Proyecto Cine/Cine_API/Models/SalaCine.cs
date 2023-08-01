using System;
using System.Collections.Generic;

namespace Cine_API.Models;

public partial class SalaCine
{
    public int IdSala { get; set; }

    public int CapacidadSala { get; set; }

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();
}
