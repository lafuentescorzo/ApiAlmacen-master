using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiAlmacen.Models
{
    public partial class Producto
    {
        public Producto()
        {
            FacturaDetails = new HashSet<FacturaDetail>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public int Unidades { get; set; }
        public decimal Precio { get; set; }

        [JsonIgnore]
        public virtual ICollection<FacturaDetail> FacturaDetails { get; set; }
    }
}
