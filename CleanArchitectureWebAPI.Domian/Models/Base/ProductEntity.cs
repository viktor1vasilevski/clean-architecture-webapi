using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Domian.Models.Base
{
    public class ProductEntity : AuditableBaseEntity
    {
        public string Brand { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int UnitQuantity { get; set; }
        public string URL { get; set; }
    }
}
