using AutoMapper;
using CleanArchitectureWebAPI.Application.Interfaces;
using CleanArchitectureWebAPI.Application.ViewModels.Soap;
using CleanArchitectureWebAPI.Domian.Interfaces;
using CleanArchitectureWebAPI.Domian.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.Services
{
    public class SoapService : ISoapService
    {
        private readonly ISoapRepository _soapRepository;
        private readonly IMapper _mapper;
        public SoapService(ISoapRepository soapRepository, IMapper mapper)
        {
            _soapRepository = soapRepository;
            _mapper = mapper;
        }
        public SoapViewModel AddSoap(SoapViewModel soapRequest)
        {
            var soap = _mapper.Map<Soap>(soapRequest);
            var addedSoap = _soapRepository.Add(soap);
            return _mapper.Map<SoapViewModel>(addedSoap);
        }

        public bool DeleteSoap(Guid id)
        {
            var soap = _soapRepository.GetById(id);

            if (soap != null)
            {
                _soapRepository.Delete(soap);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EditSoap(SoapViewModel soapRequest)
        {
            var soap = _mapper.Map<Soap>(soapRequest);
            _soapRepository.Update(soap);
        }

        public SoapViewModel GetSoapById(Guid id)
        {
            var soap = _soapRepository.GetById(id);
            return _mapper.Map<SoapViewModel>(soap);
        }

        public SoapListViewModel GetSoaps()
        {
            var soaps = _soapRepository.GetAll();
            var soapListViewModel = _mapper.Map<IEnumerable<SoapViewModel>>(soaps);

            return new SoapListViewModel()
            {
                Soaps = soapListViewModel
            };
        }
    }
}
