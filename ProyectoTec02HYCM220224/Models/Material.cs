using System;
using System.Collections.Generic;

namespace ProyectoTec02HYCM220224.Models
{
    public partial class Material
    {
        public Material()
        {
            Sillas = new HashSet<Silla>();
        }

        public int IdMaterial { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Silla> Sillas { get; set; }
    }
}
