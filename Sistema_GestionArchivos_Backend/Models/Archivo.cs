using System;
using System.Collections.Generic;

namespace Sistema_GestionArchivos_Backend.Models;

public partial class Archivo
{
    public int IdArchivo { get; set; }

    public string? Nombre { get; set; }

    public string? Ruta { get; set; }
}
