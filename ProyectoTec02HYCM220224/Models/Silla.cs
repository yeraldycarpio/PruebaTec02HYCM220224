using System;
using System.Collections.Generic;

namespace ProyectoTec02HYCM220224.Models
{
    public partial class Silla
    {
        public int IdSilla { get; set; }
        public string? Nombre { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public byte[]? Imagen { get; set; }
        public int? IdMaterial { get; set; }

        public virtual Material? IdMaterialNavigation { get; set; }
    }
}
