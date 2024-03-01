using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiAlmacen.Models
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public int Identificacion { get; set; }
        public string Nombre { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
