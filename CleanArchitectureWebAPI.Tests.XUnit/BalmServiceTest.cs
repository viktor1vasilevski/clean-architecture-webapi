using AutoMapper;
using CleanArchitectureWebAPI.Application.Services;
using CleanArchitectureWebAPI.Application.ViewModels.Balm;
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
    public class BalmServiceTest
    {
        [Fact]
        public void GetBalms_NotExistingBalms_ResultShouldBeEmpty()
        {
            // arrange
            var mockBalmRepository = new Mock<IBalmRepository>();
            var mockMapper = new Mock<IMapper>();

            // act
            var balmService = new BalmService(mockBalmRepository.Object, mockMapper.Object);
            var result = balmService.GetBalms();

            // assert
            result.Balms.Should().BeEmpty();
        }
        
        [Fact]
        public void GetBalms_NotExistingBalms_ResultShouldBeOfTypeBalmListViewModel()
        {
            // arrance
            var mockBalmRepository = new Mock<IBalmRepository>();
            var mockMapper = new Mock<IMapper>();

            // act
            var balmService = new BalmService(mockBalmRepository.Object, mockMapper.Object);
            var result = balmService.GetBalms();

            // assert
            result.Should().BeOfType(typeof(BalmListViewModel));
        }

        [Fact]
        public void GetBalms_ExistingBalms_ResultShouldBeTwoBalms()
        {
            // arrange
            var mockBalmRepository = new Mock<IBalmRepository>();

            var mockBalms = new List<Balm>
            {
                new Balm
                {
                    Id = Guid.NewGuid(),
                    Volume = 30,
                    Brand = "Balm Brand 1",
                    Description = "Balm Description 1",
                    UnitQuantity = 100,
                    UnitPrice = 30,
                    URL = "img/balm1.jpg"
                },
                new Balm
                {
                    Id = Guid.NewGuid(),
                    Volume = 33,
                    Brand = "Balm Brand 2",
                    Description = "Balm Description 2",
                    UnitQuantity = 100,
                    UnitPrice = 32,
                    URL = "img/balm2.jpg"
                }
            };

            mockBalmRepository.Setup(x => x.GetAll()).Returns(mockBalms);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Balm, BalmViewModel>();
            });
            var mapper = config.CreateMapper();

            // act
            var balmService = new BalmService(mockBalmRepository.Object, mapper);
            var result = balmService.GetBalms();

            // assert
            result.Balms.Should().HaveCount(2);
        }

        [Fact]
        public void GetBalmById_NotExpectingBalm_ResultShouldBeNull()
        {
            // arrance
            var mockBalmRepository = new Mock<IBalmRepository>();
            var mockMapper = new Mock<IMapper>();

            // act
            var balmService = new BalmService(mockBalmRepository.Object, mockMapper.Object);
            var result = balmService.GetBalmById(Guid.NewGuid());

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetBalmById_ExpectingBalm_ResultShouldReturnBalmViewModel()
        {
            // arrance
            var mockBalmRepository = new Mock<IBalmRepository>();

            var balm = new Balm()
            {
                Id = Guid.NewGuid(),
                Volume = 30,
                Brand = "Balm Brand 1",
                Description = "Balm Description 1",
                UnitQuantity = 100,
                UnitPrice = 30,
                URL = "img/balm1.jpg"
            };

            mockBalmRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(balm);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Balm, BalmViewModel>();
            });
            var mapper = config.CreateMapper();

            // act
            var balmService = new BalmService(mockBalmRepository.Object, mapper);
            var result = balmService.GetBalmById(Guid.NewGuid());

            // assert
            result.Should().BeOfType(typeof(BalmViewModel));
        }
    }
}
