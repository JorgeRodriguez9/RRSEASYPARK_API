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
    public class ParkingLotTest
    {

        [Fact]
        public async Task Get()
        {
            // Arrange
            var mockRepositorio = new Mock<IParkingLotService>();
            mockRepositorio.Setup(repo => repo.GetParkingLots())
                          .Returns(Task.FromResult<IEnumerable<ParkingLot>>(new List<ParkingLot>
                          {
                              new ParkingLot
                          {
                              Id = Guid.NewGuid(),
                              Name = "Parqueadero",
                              Adress = "calle 18 # 13-06",
                              Nit = "1231354644-03",
                              Telephone = 123456789,
                              NormalPrice = 5000,
                              DisabilityPrice = 2500,
                              Info = "Parqueadero nuevo",
                              CantSpacesCar = 12,
                              CantSpacesDisability = 12,
                              CantSpacesMotorcycle = 12,
                              disabilityservices = "SI",
                              CityId = Guid.NewGuid(),
                              PropietaryParkId = Guid.NewGuid()
                          },
                          new ParkingLot
                          {
                             Id = Guid.NewGuid(),
                              Name = "Parqueadero2",
                              Adress = "calle 28 # 13-06",
                              Nit = "1781354644-03",
                              Telephone = 125456789,
                              NormalPrice = 5500,
                              DisabilityPrice = 3500,
                              Info = "Parqueadero nuevo 2",
                              CantSpacesCar = 22,
                              CantSpacesDisability = 22,
                              CantSpacesMotorcycle = 22,
                              disabilityservices = "SI",
                              CityId = Guid.NewGuid(),
                              PropietaryParkId = Guid.NewGuid()
                          },
                          }));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            var getParkingLot = new ApiParkingLotController(mockRepositorio.Object, mapper);

            // Act
            var result = await getParkingLot.GetParkingLots();

            // Assert
            var model = Assert.IsAssignableFrom<List<ParkingLotDto>>(result);
            Assert.Equal(2, model.Count); // Verificar que se retornen dos clientes

        }

        [Fact]
        public async Task post()
        {
            // Arrange
            var mockClienteService = new Mock<IParkingLotService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.AddParkingLot(It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<long>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
              It.IsAny<string>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
             It.IsAny<Guid>(),
             It.IsAny<Guid>())).ReturnsAsync((string name, string adress, string nit, long telefhone, int price, int disabilityPrice, string info, int cantSpacesMoto, int cantSpacesCar, int cantSpacesDisability, Guid cityId, Guid propietaryParkId) =>
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Cliente agregado exitosamente"
             });
            var parkingLotController = new ApiParkingLotController(mockClienteService.Object, mapper);

            // Act
            var newParkingLot = new ParkingLotDto
            {
                Id = Guid.NewGuid(),
                Name = "Parqueadero2",
                Adress = "calle 28 # 13-06",
                Nit = "1781354644-03",
                Telephone = 125456789,
                NormalPrice = 5500,
                DisabilityPrice = 3500,
                Info = "Parqueadero nuevo 2",
                CantSpacesCar = 22,
                CantSpacesDisability = 22,
                CantSpacesMotorcycle = 22,
                disabilityservices = "SI",
                CityId = Guid.NewGuid(),
                PropietaryParkId = Guid.NewGuid()
            };

            var result = await parkingLotController.AddParkingLot(newParkingLot);

            // Assert
            // Verificar si el resultado es de tipo OkResult
            var okResult = Assert.IsType<OkResult>(result);

            // Opcionalmente, puedes verificar el código de estado si es necesario
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task put()
        {
            // Arrange
            var mockClienteService = new Mock<IParkingLotService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.UpdateParkingLot(It.IsAny<Guid>(), 
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<long>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
              It.IsAny<string>(),
              It.IsAny<int>(),
              It.IsAny<int>(),
              It.IsAny<int>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Cliente actualizado exitosamente"
             });
            var clienteController = new ApiParkingLotController(mockClienteService.Object, mapper);

            // Act
            var newParkingLot = new ParkingLotDto
            {
                Id = Guid.NewGuid(),
                Name = "Parqueadero2",
                Adress = "calle 28 # 13-06",
                Nit = "1781354644-03",
                Telephone = 125456789,
                NormalPrice = 5500,
                DisabilityPrice = 3500,
                Info = "Parqueadero nuevo 2",
                CantSpacesCar = 22,
                CantSpacesDisability = 22,
                CantSpacesMotorcycle = 22,
                disabilityservices = "SI",
                CityId = Guid.NewGuid(),
                PropietaryParkId = Guid.NewGuid()
            };

            var result = await clienteController.UpdateParkingLot(newParkingLot);

            // Assert
            // Verificar si el resultado es de tipo OkResult
            var okResult = Assert.IsType<OkResult>(result);

            // Opcionalmente, puedes verificar el código de estado si es necesario
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task delete()
        {
            // Arrange
            var mockClienteService = new Mock<IParkingLotService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.DeleteParkingLot(It.IsAny<Guid>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Cliente eliminado exitosamente"
             });
            var clienteController = new ApiParkingLotController(mockClienteService.Object, mapper);

            // Act
            var newParkingLotDto = new ParkingLotDto
            {
                Id = Guid.NewGuid()
            };

            var result = await clienteController.DeleteParkingLot(newParkingLotDto);

            // Assert
            // Verificar si el resultado es de tipo OkResult
            var okResult = Assert.IsType<OkResult>(result);

            // Opcionalmente, puedes verificar el código de estado si es necesario
            Assert.Equal(200, okResult.StatusCode);

        }

    }
}
