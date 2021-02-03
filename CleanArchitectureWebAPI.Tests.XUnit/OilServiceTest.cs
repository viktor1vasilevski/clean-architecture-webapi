using AutoMapper;
using CleanArchitectureWebAPI.Application.Services;
using CleanArchitectureWebAPI.Application.ViewModels.Oil;
using CleanArchitectureWebAPI.Domian.Interfaces;
using CleanArchitectureWebAPI.Domian.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanArchitectureWebAPI.Tests.XUnit
{
    public class OilServiceTest
    {
        [Fact]
        public void GetOils_NotExistingOils_ResultShouldBeEmpty()
        {
            // arrange
            var mockOilRepository = new Mock<IOilRepository>();
            var mockMapper = new Mock<IMapper>();

            // act 
            var oilService = new OilService(mockOilRepository.Object, mockMapper.Object);
            var result = oilService.GetOils();

            // assert
            result.Oils.Should().BeEmpty();
        }

        [Fact]
        public void GetOils_NoExistingOils_ResultShouldBeOfTypeOilListViewModel()
        {
            // arrange
            var mockOilRepository = new Mock<IOilRepository>();
            var mockMapper = new Mock<IMapper>();

            // act
            var oilService = new OilService(mockOilRepository.Object, mockMapper.Object);
            var result = oilService.GetOils();

            // assert
            //Assert.IsType<OilListViewModel>(result);
            result.Should().BeOfType(typeof(OilListViewModel));
        }

        [Fact]
        public void GetOilById_NoExistingOil_ResultShouldBeNull()
        {
            // arrange
            var mockOilRepository = new Mock<IOilRepository>();
            var mockMapper = new Mock<IMapper>();

            // act
            var oilService = new OilService(mockOilRepository.Object, mockMapper.Object);
            var result = oilService.GetOilById(Guid.NewGuid());

            //assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetOilById_ExistingOil_ShouldReturnOilViewModel()
        {
            // arrange
            var mockOilRepository = new Mock<IOilRepository>();
            var oil = new Oil()
            {
                Id = Guid.NewGuid(),
                Brand = "New Oil",
                Description = "New Description",
                Scent = "New Scent",
                LiquidVolume = 22,
                UnitPrice = 20,
                UnitQuantity = 100,
                URL = "img/oil.jpg"
            };
            mockOilRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(oil);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Oil, OilViewModel>();
            });
            var mapper = config.CreateMapper();

            var oilService = new OilService(mockOilRepository.Object, mapper);

            // act
            var result = oilService.GetOilById(Guid.NewGuid());

            // assert
            result.Should().BeOfType<OilViewModel>();
        }


        [Fact]
        public void GetOils_ExistingOils_ResultShouldBeTwoOils()
        {
            // arrange
            var mockOilRepository = new Mock<IOilRepository>();

            var mockedOils = new List<Oil>
            {
                new Oil
                {
                    Id = Guid.NewGuid(),
                    Brand = "New Oil 1",
                    Description = "New Description 1",
                    Scent = "New Scent 1",
                    LiquidVolume = 22,
                    UnitPrice = 20,
                    UnitQuantity = 100,
                    URL = "img/oil1.jpg"
                },
                new Oil
                {
                    Id = Guid.NewGuid(),
                    Brand = "New Oil 2",
                    Description = "New Description 2",
                    Scent = "New Scent 2",
                    LiquidVolume = 21,
                    UnitPrice = 21,
                    UnitQuantity = 100,
                    URL = "img/oil2.jpg"
                },
            };

            mockOilRepository.Setup(x => x.GetAll()).Returns(mockedOils);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Oil, OilViewModel>();
            });
            var mapper = config.CreateMapper();

            // act
            var oilService = new OilService(mockOilRepository.Object, mapper);
            var result = oilService.GetOils();

            // assert
            result.Oils.Should().HaveCount(2);
        }
    }
}
