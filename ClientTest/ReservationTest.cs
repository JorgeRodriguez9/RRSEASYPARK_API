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
    public class ReservationTest
    {

        [Fact]
        public async Task Get()
        {
            // Arrange
            var mockRepositorio = new Mock<IReservationService>();
            mockRepositorio.Setup(repo => repo.GetReservations())
                          .Returns(Task.FromResult<IEnumerable<Reservation>>(new List<Reservation>
                          {
                              new Reservation
                          {
                              Id = Guid.NewGuid(),
                              Date = DateTime.Now,
                              TotalPrice = 20000,
                              Disabled = "SI",
                              ClientParkingLotId = Guid.NewGuid(),
                              TypeVehicleId = Guid.NewGuid(),
                              ParkingLotId = Guid.NewGuid()
                          },
                          new Reservation
                          {
                              Id = Guid.NewGuid(),
                              Date = DateTime.Now,
                              TotalPrice = 20000,
                              Disabled = "SI",
                              ClientParkingLotId = Guid.NewGuid(),
                              TypeVehicleId = Guid.NewGuid(),
                              ParkingLotId = Guid.NewGuid()
                          },
                          }));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            var getReservation  = new ApiReservationController(mockRepositorio.Object, mapper);

            // Act
            var result = await getReservation.GetReservation();

            // Assert
            var model = Assert.IsAssignableFrom<List<ReservationDto>>(result);
            Assert.Equal(2, model.Count); // Verificar que se retornen dos clientes

        }

        [Fact]
        public async Task post()
        {
            // Arrange
            var mockClienteService = new Mock<IReservationService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.AddReservation(It.IsAny<DateTime>(),
              It.IsAny<long>(),
              It.IsAny<string>(),
             It.IsAny<Guid>(),
             It.IsAny<Guid>(),
             It.IsAny<Guid>())).ReturnsAsync((DateTime date, long totalPrice, string disabled, Guid clientId, Guid typeVehicleId, Guid parkingLotId) =>
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Reservacion agregado exitosamente"
             });
            var reservationController = new ApiReservationController(mockClienteService.Object, mapper);

            // Act
            var newReservation = new ReservationDto
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                TotalPrice = 20000,
                Disabled = "SI",
                ClientId = Guid.NewGuid(),
                TypeVehicleId = Guid.NewGuid(),
                ParkingLotId = Guid.NewGuid()
            };

            var result = await reservationController.AddReservation(newReservation);

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
            var mockClienteService = new Mock<IReservationService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.UpdateReservation(It.IsAny<Guid>(), It.IsAny<DateTime>(),
              It.IsAny<long>(),
              It.IsAny<string>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Reserva actualizada exitosamente"
             });
            var reservationController = new ApiReservationController(mockClienteService.Object, mapper);

            // Act
            var newReservation = new ReservationDto
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                TotalPrice = 20000,
                Disabled = "SI",
                ClientId = Guid.NewGuid(),
                TypeVehicleId = Guid.NewGuid(),
                ParkingLotId = Guid.NewGuid()
            };

            var result = await reservationController.UpdateReservation(newReservation);

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
            var mockClienteService = new Mock<IReservationService>();


            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMaperProfile>());
            IMapper mapper = config.CreateMapper();

            // Configurar el mock para el servicio
            mockClienteService.Setup(service => service.DeleteReservation(It.IsAny<Guid>())).ReturnsAsync(
             new ServiceResponse
             {
                 Result = ServiceResponseType.Succeded,
                 InformationMessage = "Reservacion eliminada exitosamente"
             });
            var reservationController = new ApiReservationController(mockClienteService.Object, mapper);

            // Act
            var newReservation = new ReservationDto
            {
                Id = Guid.NewGuid()
            };

            var result = await reservationController.DeleteReservation(newReservation);

            // Assert
            // Verificar si el resultado es de tipo OkResult
            var okResult = Assert.IsType<OkResult>(result);

            // Opcionalmente, puedes verificar el código de estado si es necesario
            Assert.Equal(200, okResult.StatusCode);

        }

    }
}
