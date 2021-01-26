using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.ViewModels.Soap
{
    public class SoapListViewModel
    {
        public IEnumerable<SoapViewModel> Soaps { get; set; }
    }
}
