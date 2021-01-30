using AutoMapper;
using CleanArchitectureWebAPI.Application.Interfaces;
using CleanArchitectureWebAPI.Application.ViewModels.Balm;
using CleanArchitectureWebAPI.Domian.Interfaces;
using CleanArchitectureWebAPI.Domian.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.Services
{
    public class BalmService : IBalmService
    {
        /*
            This is where all the logic is, in the Services. 
        */
        private readonly IBalmRepository _balmRepository;
        private readonly IMapper _mapper;
        public BalmService(IBalmRepository balmRepository, IMapper mapper)
        {
            _mapper = mapper;
            _balmRepository = balmRepository;
        }
        public BalmViewModel AddBalm(BalmViewModel balmRequest)
        {
            var balm = _mapper.Map<Balm>(balmRequest);
            var addedBalm = _balmRepository.Add(balm);

            _balmRepository.SaveChanges();

            return _mapper.Map<BalmViewModel>(addedBalm);
        }

        public bool DeleteBalm(Guid id)
        {
            var balm = _balmRepository.GetById(id);

            if (balm != null)
            {
                _balmRepository.Delete(balm);
                _balmRepository.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void EditBalm(BalmViewModel balmRequest)
        {
            var balm = _mapper.Map<Balm>(balmRequest);
            _balmRepository.Update(balm);

            _balmRepository.SaveChanges();
        }

        public BalmViewModel GetBalmById(Guid id)
        {
            var balm = _balmRepository.GetById(id);
            return _mapper.Map<BalmViewModel>(balm);
        }

        public BalmListViewModel GetBalms()
        {
            var balms = _balmRepository.GetAll();
            var balmListViewModel = _mapper.Map<IEnumerable<BalmViewModel>>(balms);

            return new BalmListViewModel()
            {
                Balms = balmListViewModel
            };
        }
    }
}
