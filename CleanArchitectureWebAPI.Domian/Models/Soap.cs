using CleanArchitectureWebAPI.Domian.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Domian.Models
{
    public class Soap : ProductEntity
    {
        /*
            This is the place where we set the 
            specific property for that class(product).
         */
        public string Edition { get; set; }
    }
}
