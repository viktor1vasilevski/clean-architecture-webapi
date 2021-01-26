using CleanArchitectureWebAPI.Application.ViewModels.Balm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.Interfaces
{
    public interface IBalmService
    {
        BalmListViewModel GetBalms();
        BalmViewModel GetBalmById(Guid id);
        BalmViewModel AddBalm(BalmViewModel balmRequest);
        void EditBalm(BalmViewModel balmRequest);
        bool DeleteBalm(Guid id);
    }
}
