using CleanArchitectureWebAPI.Application.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CleanArchitectureWebAPI.Application.ViewModels.Balm
{
    public class BalmViewModel : BaseViewModel
    {
        /*
        This is the specific view model that you will be using throughout the app,
        so here you can use Data Annotation like [Required] or [MaxLength(20)]
        */
        [Required]
        public int Volume { get; set; }
    }
}
