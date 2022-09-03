

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ITM.DAL.EF.Models
{
    public partial class Entity
    {
        public Entity()
        {
            Form4Reports = new HashSet<Form4Report>();
        }


        [Key]
        public System.Int64? ID { get; set; }

        [ForeignKey("FK_Entity_EntityType")]
        public System.Int64 EntityTypeID { get; set; }
        public System.Int32 CIK { get; set; }
        public System.String Name { get; set; }
        public System.String TradingSymbol { get; set; }
        public System.Boolean IsMonitored { get; set; }

        public virtual EntityType EntityType { get; set; }

        public virtual ICollection<Form4Report> Form4Reports { get; set; }
    }
}