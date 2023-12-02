using System;
using System.Collections.Generic;

namespace ElGiganteWebApp.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdProveedor { get; set; }
        public string? NombreProveedor { get; set; }
        public int? IdPais { get; set; }

        public virtual Pai? IdPaisNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
