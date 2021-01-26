using CleanArchitectureWebAPI.Application.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitectureWebAPI.Application.ViewModels.Oil
{
    public class OilViewModel : BaseViewModel
    {
        [MaxLength(30), Required]
        public string Scent { get; set; }

        [Required]
        public int LiquidVolume { get; set; }
    }
}
