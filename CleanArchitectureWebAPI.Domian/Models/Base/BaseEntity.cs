using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Domian.Models.Base
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}
