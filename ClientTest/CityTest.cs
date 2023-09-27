using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RRSEASYPARK.ApiControllers;
using RRSEASYPARK.Models.Dto;
using RRSEasyPark.Models;
using RRSEASYPARK.Models;
using RRSEASYPARK.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRSEASYPARK.Utilities;

namespace RRSEASYPARKTESTING
{
    public class CityTest
    {

        [Fact]
        public async Task Get()
        {
            // Arrange
            var mockRepositorio = new Mock<ICityService>();
            mockRepositorio.Setup(repo => repo.GetCities())
                          .Returns(Task.FromResult<IEnumerable<City>>(new List<City>
                          {
                              new City
                          {
                              Id = Guid.NewGuid(),
                              Name = "Bogota"
                          },
                          new City
                          {
                              Id = Guid.NewGuid(),
                              Name = "Medellin"
                          },
                          }));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            var getCity = new ApiCityController(mockRepositorio.Object, mapper);

            // Act
            var result = await getCity.GetCities();

            // Assert
            var model = Assert.IsAssignableFrom<List<CityDto>>(result);
            Assert.Equal(2, model.Count); // Verificar que se retornen dos clientes

        }

    }

    
}
