using CleanArchitectureWebAPI.Application.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitectureWebAPI.Application.ViewModels.Soap
{
    public class SoapViewModel : BaseViewModel
    {
        [MaxLength(50), Required]
        public string Edition { get; set; }
    }
}
