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
using RRSEASYPARK.Models.Request;

namespace RRSEASYPARKTESTING
{
    public class UserTest
    {
        
        [Fact]
        public async Task Get()
        {
            // Arrange
            var mockRepositorio = new Mock<IUserService>();
            mockRepositorio.Setup(repo => repo.GetUser())
                          .Returns(Task.FromResult<IEnumerable<User>>(new List<User>
                          {
                              new User
                          {
                              Id = Guid.NewGuid(),
                              Name = "User1",
                              Password = "password",
                              RolId = Guid.NewGuid()
                           
                          },
                          new User
                          {
                              Id = Guid.NewGuid(),
                              Name = "User2",
                              Password = "password1",
                              RolId = Guid.NewGuid()

                          },
                          }));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            var getUser = new ApiUserController(mockRepositorio.Object, mapper);

            // Act
            var result = await getUser.GetUser();

            // Assert
            var model = Assert.IsAssignableFrom<List<UserDto>>(result);
            Assert.Equal(2, model.Count); // Verificar que se retornen dos clientes

        }

        [Fact]
        public async Task post()
        {
            // Arrange
            var mockClienteService = new Mock<IUserService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();
            var userController = new ApiUserController(mockClienteService.Object, mapper);

            // Act
            var newUserDto = new AuthRequest
            {
                nameUser = "User2",
                Password = "password1",
            };

            var result = await userController.Autentification(newUserDto);

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
            var mockClienteService = new Mock<IUserService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.UpdateUser(It.IsAny<Guid>(), It.IsAny<string>(),
              It.IsAny<string>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Usuario actualizado exitosamente"
             });
            var userController = new ApiUserController(mockClienteService.Object, mapper);

            // Act
            var newUserDto = new UserDto
            {
                Id = Guid.NewGuid(),
                Name = "User2",
                Password = "password1",
                RolId = Guid.NewGuid()
            };

            var result = await userController.UpdateUser(newUserDto);

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
            var mockClienteService = new Mock<IUserService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.DeleteUser(It.IsAny<Guid>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Usuario eliminado exitosamente"
             });
            var userController = new ApiUserController(mockClienteService.Object, mapper);

            // Act
            var newUserDto = new UserDto
            {
                Id = Guid.NewGuid()
            };

            var result = await userController.DeleteUser(newUserDto);

            // Assert
            // Verificar si el resultado es de tipo OkResult
            var okResult = Assert.IsType<OkResult>(result);

            // Opcionalmente, puedes verificar el código de estado si es necesario
            Assert.Equal(200, okResult.StatusCode);

        }
        
    }
}
