using System;
using System.Collections.Generic;

namespace ElGiganteWebApp.Models
{
    public partial class Bodega
    {
        public Bodega()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdBodega { get; set; }
        public string? NombreAlmacen { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
