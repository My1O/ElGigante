using System;
using System.Collections.Generic;

namespace ElGiganteWebApp.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string? NombreProducto { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
