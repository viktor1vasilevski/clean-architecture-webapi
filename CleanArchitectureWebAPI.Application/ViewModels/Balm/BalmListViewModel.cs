using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.ViewModels.Balm
{
    public class BalmListViewModel
    {
        public IEnumerable<BalmViewModel> Balms { get; set; }
    }
}
