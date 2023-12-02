using System;
using System.Collections.Generic;

namespace ElGiganteWebApp.Models
{
    public partial class Pais
    {
        public Pais()
        {
            Proveedors = new HashSet<Proveedor>();
        }

        public int IdPais { get; set; }
        public string? NombrePais { get; set; }

        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
