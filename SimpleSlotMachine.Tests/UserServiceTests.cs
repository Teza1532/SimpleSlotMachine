using Microsoft.EntityFrameworkCore;
using Moq;
using SimpleSlotMachine.Database;
using SimpleSlotMachine.Models;
using SimpleSlotMachine.Repositories;
using SimpleSlotMachine.Repositories.Interfaces;
using SimpleSlotMachine.Services.Classes;
using System;
using Xunit;

namespace SimpleSlotMachine.Tests
{
    public class UserServiceTests
    {

        private readonly DbContextOptions<SimpleSlotMachineContext> _contextOptions;

        public UserServiceTests()
        {
            _contextOptions = new DbContextOptionsBuilder<SimpleSlotMachineContext>()
                .UseInMemoryDatabase("SimleSlotMachineTestDatabase")
                .Options;

            using var context = new SimpleSlotMachineContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Fact]
        public void Should_Add_Funds()
        {
            //Arrange
            decimal funds = 100;
            decimal expected = 100;

            var unitOfWork = new UnitOfWork(new SimpleSlotMachineContext(_contextOptions));

            UserService userService = new UserService(unitOfWork);
            
            //Act
            Guid userGuid = userService.AddUser(funds);
            decimal actualFunds = userService.GetFunds(userGuid);

            //Assert
            Assert.Equal(expected, actualFunds);
        }

        [Fact]
        public void Should_Not_Add_Funds_As_Amount_Below_Zero()
        {
            //Arrange
            decimal funds = -1;

            var unitOfWork = new UnitOfWork(new SimpleSlotMachineContext(_contextOptions));

            UserService userService = new UserService(unitOfWork);

            //Act
            Action actual = () => userService.AddUser(funds);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }

        [Fact]
        public void Should_Not_Get_Funds_As_No_User_Matches_Id()
        {
            //Arrange
            var unitOfWork = new UnitOfWork(new SimpleSlotMachineContext(_contextOptions));

            UserService userService = new UserService(unitOfWork);

            Guid randomUserGUID = new();

            //Act

            Action actual = () => userService.GetFunds(randomUserGUID);

            //Assert

            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void Should_Add_Stake()
        {
            //Arrange
            Guid userGuid = Guid.NewGuid();
            UserModel user = GetUserByGuid(userGuid);
            decimal expected = 100;
            decimal funds = 100;

            var mockUnitOfWork = new Mock<IUnitOfWork>();

           // var userService = new UserService(mockUnitOfWork.Object);

            //Act

            //Assert
        }

        private static UserModel GetUserByGuid(Guid userGuid)
        {
            return new UserModel
            {
                Id = userGuid,
                Funds = 0,
            };

            throw new NotImplementedException();
        }

    }
}
