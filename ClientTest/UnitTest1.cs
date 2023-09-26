using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RRSEasyPark.Models;
using RRSEASYPARK.ApiControllers;
using RRSEASYPARK.Models;
using RRSEASYPARK.Models.Dto;
using RRSEASYPARK.Service;
using RRSEASYPARK.Utilities;

namespace ClientTest
{
    
    public class UnitTest1
    {
        [Fact]
        public async Task Get()
        {
            // Arrange
            var mockRepositorio = new Mock<IClientParkingLotService>();
            mockRepositorio.Setup(repo => repo.GetClientParkingLots())
                          .Returns(Task.FromResult<IEnumerable<ClientParkingLot>>(new List<ClientParkingLot>
                          {
                              new ClientParkingLot
                          {
                              Id = Guid.NewGuid(),
                              Name = "Cliente1",
                              Identification = 123456789,
                              Email = "cliente1@empresa.com",
                              Telephone = 987654321,
                              UserId = Guid.NewGuid()
                          },
                          new ClientParkingLot
                          {
                              Id = Guid.NewGuid(),
                              Name = "Cliente2",
                              Identification = 987654321,
                              Email = "cliente2@empresa.com",
                              Telephone = 123456789,
                              UserId = Guid.NewGuid()
                          },
                          }));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            var getClient = new ApiClientParkingLotController(mockRepositorio.Object, mapper);

            // Act
            var result = await getClient.GetClients();

            // Assert
            var model = Assert.IsAssignableFrom<List<ClientParkingLotDto>>(result);
            Assert.Equal(2, model.Count); // Verificar que se retornen dos clientes

        }

        [Fact]
        public async Task post()
        {
            // Arrange
            var mockClienteService = new Mock<IClientParkingLotService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.AddClientParkingLot(It.IsAny<string>(),
              It.IsAny<long>(),
              It.IsAny<string>(),
              It.IsAny<long>(),
             It.IsAny<Guid>())).ReturnsAsync((string name, long identification, string email, long telephone, Guid idUser) =>
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Cliente agregado exitosamente"
             });
            var clienteController = new ApiClientParkingLotController(mockClienteService.Object, mapper);

            // Act
            var nuevoClienteDto = new ClientParkingLotDto
            {
                Id = Guid.NewGuid(),
                Name = "Nuevo Cliente",
                Identification = 555555555,
                Email = "nuevo@cliente.com",
                Telephone = 555555555,
                UserId = Guid.NewGuid()
            };

            var result = await clienteController.AddClient(nuevoClienteDto);

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
            var mockClienteService = new Mock<IClientParkingLotService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.UpdateClientParkingLot(It.IsAny<Guid>(), It.IsAny<string>(),
              It.IsAny<long>(),
              It.IsAny<string>(),
              It.IsAny<long>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Cliente actualizado exitosamente"
             });
            var clienteController = new ApiClientParkingLotController(mockClienteService.Object, mapper);

            // Act
            var ClienteDto = new ClientParkingLotDto
            {
                Id = Guid.NewGuid(),
                Name = "Nuevo Cliente",
                Identification = 555555555,
                Email = "nuevo@cliente.com",
                Telephone = 555555555
            };

            var result = await clienteController.UpdateClient(ClienteDto);

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
            var mockClienteService = new Mock<IClientParkingLotService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.DeleteClientParkingLot(It.IsAny<Guid>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Cliente eliminado exitosamente"
             });
            var clienteController = new ApiClientParkingLotController(mockClienteService.Object, mapper);

            // Act
            var ClienteDto = new ClientParkingLotDto
            {
                Id = Guid.NewGuid()
            };

            var result = await clienteController.DeleteClient(ClienteDto);

            // Assert
            // Verificar si el resultado es de tipo OkResult
            var okResult = Assert.IsType<OkResult>(result);

            // Opcionalmente, puedes verificar el código de estado si es necesario
            Assert.Equal(200, okResult.StatusCode);

        }

    }
    
}