using System;
using System.Collections.Generic;

namespace Cine_API.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public virtual ICollection<Entrada> Entrada { get; set; } = new List<Entrada>();
}
