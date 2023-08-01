using System;
using System.Collections.Generic;

namespace Cine_API.Models;

public partial class Pelicula
{
    public int IdPelicula { get; set; }

    public string Titulo { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public TimeSpan Duracion { get; set; }

    public string Sinopsis { get; set; } = null!;

    public string? Poster { get; set; }

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();
}
