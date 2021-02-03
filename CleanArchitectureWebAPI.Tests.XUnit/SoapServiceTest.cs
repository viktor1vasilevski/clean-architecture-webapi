using AutoMapper;
using CleanArchitectureWebAPI.Application.Services;
using CleanArchitectureWebAPI.Application.ViewModels.Soap;
using CleanArchitectureWebAPI.Domian.Interfaces;
using CleanArchitectureWebAPI.Domian.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanArchitectureWebAPI.Tests.XUnit
{
    public class SoapServiceTest
    {
        [Fact]
        public void GetSoaps_NotExistingSoaps_ResultShouldBeEmpty()
        {
            // arrange
            var mockSoapRepository = new Mock<ISoapRepository>();
            var mockMapper = new Mock<IMapper>();

            // act
            var soapService = new SoapService(mockSoapRepository.Object, mockMapper.Object);
            var result = soapService.GetSoaps();

            // assert
            result.Soaps.Should().BeEmpty();
        }

        [Fact]
        public void GetSoaps_NotExistingSoaps_ResultShouldBeOfTypeSoapListViewModel()
        {
            // arrange
            var mockSoapRepository = new Mock<ISoapRepository>();
            var mockMapper = new Mock<IMapper>();

            // act
            var soapService = new SoapService(mockSoapRepository.Object, mockMapper.Object);
            var result = soapService.GetSoaps();

            // arrange
            result.Should().BeOfType(typeof(SoapListViewModel));
        }

        [Fact]
        public void GetSoaps_ExistingSoaps_ResultShouldBeTwoSoaps()
        {
            // arrance
            var mockSoapRepository = new Mock<ISoapRepository>();
            var mockedSoaps = new List<Soap>
            {
                new Soap
                {
                    Id = Guid.NewGuid(),
                    Brand = "Soap Brand 1",
                    Edition = "Soap Edition 1",
                    Description = "Soap Description 1",
                    UnitPrice = 20,
                    UnitQuantity = 100,
                    URL = "img/soap1.jpeg"
                },
                new Soap
                {
                    Id = Guid.NewGuid(),
                    Brand = "Soap Brand 2",
                    Edition = "Soap Edition 2",
                    Description = "Soap Description 2",
                    UnitPrice = 21,
                    UnitQuantity = 100,
                    URL = "img/soap2.jpeg"
                }
            };

            mockSoapRepository.Setup(x => x.GetAll()).Returns(mockedSoaps);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Soap, SoapViewModel>();
            });
            var mapper = config.CreateMapper();

            // act
            var soapService = new SoapService(mockSoapRepository.Object, mapper);
            var result = soapService.GetSoaps();

            // assert
            result.Soaps.Should().HaveCount(2);
        }


        [Fact]
        public void GetSoapById_NotExpectingSoap_ResultShouldBeNull()
        {
            // arrange
            var mockSoapRepository = new Mock<ISoapRepository>();
            var mockMapper = new Mock<IMapper>();

            // act
            var soapService = new SoapService(mockSoapRepository.Object, mockMapper.Object);
            var result = soapService.GetSoapById(Guid.NewGuid());

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetSoapById_ExpectingSoap_ResultShouldReturnSoapViewModel()
        {
            // arrange
            var mockSoapRepository = new Mock<ISoapRepository>();

            var mockSoap = new Soap()
            {
                Id = Guid.NewGuid(),
                Brand = "Soap Brand 1",
                Edition = "Soap Edition 1",
                Description = "Soap Description 1",
                UnitPrice = 20,
                UnitQuantity = 100,
                URL = "img/soap1.jpeg"
            };

            mockSoapRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(mockSoap);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Soap, SoapViewModel>();
            });

            var mapper = config.CreateMapper();

            // act
            var soapService = new SoapService(mockSoapRepository.Object, mapper);
            var result = soapService.GetSoapById(Guid.NewGuid());

            // assert
            result.Should().BeOfType<SoapViewModel>();
        }
        
    }
}
