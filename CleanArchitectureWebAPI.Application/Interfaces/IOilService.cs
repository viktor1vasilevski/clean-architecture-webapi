using CleanArchitectureWebAPI.Application.ViewModels.Oil;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.Interfaces
{
    public interface IOilService
    {
        OilListViewModel GetOils();
        OilViewModel GetOilById(Guid id);
        OilViewModel AddOil(OilViewModel oilRequest);
        void EditOil(OilViewModel oilRequest);
        bool DeleteOil(Guid id);
    }
}
