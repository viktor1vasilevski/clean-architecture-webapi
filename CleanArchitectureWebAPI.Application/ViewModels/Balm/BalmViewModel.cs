using CleanArchitectureWebAPI.Application.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitectureWebAPI.Application.ViewModels.Balm
{
    public class BalmViewModel : BaseViewModel
    {
        [Required]
        public int Volume { get; set; }
    }
}
