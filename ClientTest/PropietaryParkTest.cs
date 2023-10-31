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
    public class PropietaryParkTest
    {
        
        [Fact]
        public async Task Get()
        {
            // Arrange
            var mockUser = new Mock<IUserService>();
            mockUser.Setup(repo => repo.GetUser())
                          .Returns(Task.FromResult<IEnumerable<User>>(new List<User>
                          {
                              new User
                          {
                              Id = Guid.NewGuid(),
                              Name = "User0",
                              Password = "JEJEJE",
                              RolId = Guid.NewGuid(),

                          },
                          new User
                          {
                             Id = Guid.NewGuid(),
                              Name = "User1",
                              Password = "JEJEJE",
                              RolId = Guid.NewGuid()
                          },
                          }));
            var mockRepositorio = new Mock<IPropietaryParkService>();
            mockRepositorio.Setup(repo => repo.GetPropietaryParks())
                          .Returns(Task.FromResult<IEnumerable<PropietaryPark>>(new List<PropietaryPark>
                          {
                              new PropietaryPark
                          {
                              Id = Guid.NewGuid(),
                              Name = "Propetario",
                              Identification = 123456789,
                              Email = "Propetario2@empresa.com",
                              UserId = Guid.NewGuid()
                          },
                          new PropietaryPark
                          {
                              Id = Guid.NewGuid(),
                              Name = "Propetario",
                              Identification = 987654321,
                              Email = "Propetario@empresa.com",
                              UserId = Guid.NewGuid()
                          },
                          }));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            var getPropietaryPark = new ApiPropietaryParkController(mockRepositorio.Object, mapper, mockUser.Object);

            // Act
            var result = await getPropietaryPark.GetPropietaryParks();

            // Assert
            var model = Assert.IsAssignableFrom<List<PropietaryParkDto>>(result);
            Assert.Equal(2, model.Count); // Verificar que se retornen dos Propetario

        }

        [Fact]
        public async Task post()
        {
            // Arrange
            var mockClienteService = new Mock<IPropietaryParkService>();
            var mockUser = new Mock<IUserService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.AddPropietaryPark(It.IsAny<string>(),
             It.IsAny<long>(),
             It.IsAny<string>(),
             It.IsAny<long>(),
             It.IsAny<Guid>())).ReturnsAsync((string name, long identification, string email, Guid idUser) =>
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Propierio agregado exitosamente"
             });
            mockUser.Setup(service => service.AddUser(It.IsAny<string>(),
           It.IsAny<string>(),
           It.IsAny<string>(),
          It.IsAny<Guid>())).ReturnsAsync((string name, string password, string rol, Guid id) =>
          new ServiceResponse
          {
              Result = ServiceResponseType.Succeded,
              InformationMessage = "Cliente agregado exitosamente"
          });
            var propietaryController = new ApiPropietaryParkController(mockClienteService.Object, mapper, mockUser.Object);

            // Act
            var newPropietaryPark = new PropietaryParkPostDto
            {
              
                Name = "Nuevo Propietario",
                Identification = 555555555,
                Email = "propietario@cliente.com",
             
            };

            var result = await propietaryController.AddPropietaryPark(newPropietaryPark);

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
            var mockClienteService = new Mock<IPropietaryParkService>();
            var mockUser = new Mock<IUserService>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.UpdatePropietaryPark(It.IsAny<Guid>(), It.IsAny<string>(),
              It.IsAny<long>(),
              It.IsAny<string>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Propierio actualizado exitosamente"
             });
            mockUser.Setup(service => service.UpdateUser(It.IsAny<Guid>(),
           It.IsAny<string>(),
           It.IsAny<string>())).ReturnsAsync((Guid id, string name, string password) =>
          new ServiceResponse
          {
              Result = ServiceResponseType.Succeded,
              InformationMessage = "Cliente agregado exitosamente"
          });
            var propietaryController = new ApiPropietaryParkController(mockClienteService.Object, mapper, mockUser.Object);

            // Act
            var newPropietaryPark = new PropietaryParkDto
            {
                Id = Guid.NewGuid(),
                Name = "Nuevo Propietario",
                Identification = 555555555,
                Email = "propietario@cliente.com",
                UserId = Guid.NewGuid()
            };

            var result = await propietaryController.UpdatePropietaryPark(newPropietaryPark);

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
            var mockClienteService = new Mock<IPropietaryParkService>();
            var mockUser = new Mock<IUserService>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.DeletePropietaryPark(It.IsAny<Guid>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Propierio eliminado exitosamente"
             });
            mockUser.Setup(service => service.DeleteUser(It.IsAny<Guid>())).ReturnsAsync(
            new ServiceResponse
            {
                Result = ServiceResponseType.Succeded,
                InformationMessage = "Cliente eliminado exitosamente"
            });
            var propietaryController = new ApiPropietaryParkController(mockClienteService.Object, mapper, mockUser.Object);

            // Act
            var newPropietaryPark = new PropietaryParkDto
            {
                Id = Guid.NewGuid()
            };

            var result = await propietaryController.DeletePropietaryPark(newPropietaryPark);

            // Assert
            // Verificar si el resultado es de tipo OkResult
            var okResult = Assert.IsType<OkResult>(result);

            // Opcionalmente, puedes verificar el código de estado si es necesario
            Assert.Equal(200, okResult.StatusCode);

        }
        

    }
}
