using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class Ubicacion
    {
        public Ubicacion()
        {

        }

        public int Id { get; set; }
        public int ManzanaId { get; set; }
        public virtual Manzana Manzana { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }

        public virtual ICollection<Lote> Lotes { get; set; }
    }

    public class UbicacionConfiguration: EntityTypeConfiguration<Ubicacion>
    {
        public UbicacionConfiguration()
        {
            ToTable("Ubicacion");
        }
    }
}