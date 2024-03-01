using System;
using System.Collections.Generic;

namespace ApiAlmacen.Models
{
    public partial class FacturaDetail
    {
        public int IdDetalle { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }

        public virtual Factura IdFacturaNavigation { get; set; } = null!;
        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
