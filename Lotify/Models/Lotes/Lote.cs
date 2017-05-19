using Lotify.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class Lote
    {
        public Lote()
        {

        }

        public int Id { get; set; }
        public int NumeroLote { get; set; }
        public decimal Precio { get; set; }
        
        //Foreign Key Para Medida
        public int MedidaId { get; set; }
        public virtual Medida Medida { get; set; }

        //Foreign Key para EstadoLote
        public int EstadoLoteId { get; set; }
        public virtual EstadoLote EstadoLote { get; set; }

        //Foreign Key a Lotificadora
        public int LotificadoraId { get; set; }
        public virtual Lotificadora Lotificadora { get; set; }

        public int ManzanaId { get; set; }
        public virtual Manzana Manzana { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        
        //public int UbicacionId { get; set; }
        //public virtual Ubicacion Ubicacion { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVentas { get; set; }

    }

    public class LoteConfiguration: EntityTypeConfiguration<Lote>
    {
        public LoteConfiguration()
        {
            ToTable("Lotes");
        }
    }
}