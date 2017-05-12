using Lotify.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Pagos
{
    public class Pago
    {
        public Pago()
        {

        }

        public int Id { get; set; }
        public int NumeroComprobante { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal CantidadCancelada { get; set; }
        
        //Foreign Key a Venta
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }

        //Foreign Key a TipoPago
        public int TipoPagoId { get; set; }
        public virtual TipoPago TipoPago { get; set; }

        //Foreign Key a MesPago
        public int MesPagoId { get; set; }
        public virtual MesPago MesPago { get; set; }
    }

    public class PagoConfiguration: EntityTypeConfiguration<Pago>
    {
        public PagoConfiguration()
        {
            ToTable("Pagos");

            Property(p => p.FechaPago)
                .IsRequired();

            Property(p => p.CantidadCancelada)
                .IsRequired();
        }
    }
}