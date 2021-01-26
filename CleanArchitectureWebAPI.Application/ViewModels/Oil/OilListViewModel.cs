using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.ViewModels.Oil
{
    public class OilListViewModel
    {
        public IEnumerable<OilViewModel> Oils { get; set; }

    }
}
