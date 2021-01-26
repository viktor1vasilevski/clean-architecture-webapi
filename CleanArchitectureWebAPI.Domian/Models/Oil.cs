using CleanArchitectureWebAPI.Domian.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Domian.Models
{
    public class Oil : ProductEntity
    {
        public string Scent { get; set; }
        public int LiquidVolume { get; set; }

    }
}
