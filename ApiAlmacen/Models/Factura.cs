using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiAlmacen.Models
{
    public partial class Factura
    {
        public Factura()
        {
            FacturaDetails = new HashSet<FacturaDetail>();
        }

        public int IdFactura { get; set; }
        public int CcCliente { get; set; }
        public int CcVendedor { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }

        
        public virtual Cliente CcClienteNavigation { get; set; } = null!;
       
        public virtual Vendedor CcVendedorNavigation { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<FacturaDetail> FacturaDetails { get; set; }
    }
}
