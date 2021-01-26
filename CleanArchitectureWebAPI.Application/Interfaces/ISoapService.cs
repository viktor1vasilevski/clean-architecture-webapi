using CleanArchitectureWebAPI.Application.ViewModels.Soap;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.Interfaces
{
    public interface ISoapService
    {
        SoapListViewModel GetSoaps();
        SoapViewModel GetSoapById(Guid id);
        SoapViewModel AddSoap(SoapViewModel soapRequest);
        void EditSoap(SoapViewModel soapRequest);
        bool DeleteSoap(Guid id);
    }
}
