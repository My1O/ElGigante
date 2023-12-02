using System;
using System.Collections.Generic;

namespace ElGiganteWebApp.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdBodega { get; set; }
        public string? NombreProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public string? UnidadMedida { get; set; }
        public int? CantidadExistencia { get; set; }
        public int? IdProveedor { get; set; }

        public virtual Bodega? IdBodegaNavigation { get; set; }
        public virtual Categorium? IdCategoriaNavigation { get; set; }
        public virtual Proveedor? IdProveedorNavigation { get; set; }
    }
}
