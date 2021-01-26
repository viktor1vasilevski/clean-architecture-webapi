using AutoMapper;
using CleanArchitectureWebAPI.Application.ViewModels.Balm;
using CleanArchitectureWebAPI.Application.ViewModels.Oil;
using CleanArchitectureWebAPI.Application.ViewModels.Soap;
using CleanArchitectureWebAPI.Domian.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureWebAPI.Application.Mappers
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Soap, SoapViewModel>().ReverseMap();
            CreateMap<Oil, OilViewModel>().ReverseMap();
            CreateMap<Balm, BalmViewModel>().ReverseMap();
        }
    }
}
