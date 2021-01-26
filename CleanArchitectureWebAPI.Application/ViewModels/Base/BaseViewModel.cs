using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.ViewModels.Base
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int UnitQuantity { get; set; }
        public string URL { get; set; }
    }
}
