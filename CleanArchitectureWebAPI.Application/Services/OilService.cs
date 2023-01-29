using AutoMapper;
using CleanArchitectureWebAPI.Application.Interfaces;
using CleanArchitectureWebAPI.Application.ViewModels.Oil;
using CleanArchitectureWebAPI.Domian.Interfaces;
using CleanArchitectureWebAPI.Domian.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.Services
{
    public class OilService : IOilService
    {
        private readonly IOilRepository _oilRepository;
        private readonly IMapper _mapper;
        public OilService(IOilRepository oilRepository, IMapper mapper)
        {
            _mapper = mapper;
            _oilRepository = oilRepository;
        }
        public OilViewModel AddOil(OilViewModel oilRequest)
        {
            var oil = _mapper.Map<Oil>(oilRequest);
            oil.Id = Guid.NewGuid();

            var addedOil = _oilRepository.Add(oil);

            _oilRepository.SaveChanges();

            return _mapper.Map<OilViewModel>(addedOil);
        }

        public bool DeleteOil(Guid id)
        {
            var oil = _oilRepository.GetById(id);

            if (oil != null)
            {
                _oilRepository.Delete(oil);
                _oilRepository.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void EditOil(OilViewModel oilRequest)
        {
            var oil = _mapper.Map<Oil>(oilRequest);
            _oilRepository.Update(oil);

            _oilRepository.SaveChanges();
        }

        public OilViewModel GetOilById(Guid id)
        {
            var oil = _oilRepository.GetById(id);
            return _mapper.Map<OilViewModel>(oil);
        }

        public OilListViewModel GetOils()
        {
            var oils = _oilRepository.GetAll();
            var oilListViewModel = _mapper.Map<IEnumerable<OilViewModel>>(oils);

            return new OilListViewModel()
            {
                Oils = oilListViewModel
            };
        }
    }
}
