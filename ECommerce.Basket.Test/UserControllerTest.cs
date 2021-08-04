using ECommerce.Basket.Api.Features.User;
using ECommerce.Basket.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Basket.Test
{
    public class UserControllerTest
    {
        private Mock<IMediator> _mediator;
        public UserControllerTest()
        {
            _mediator = new Mock<IMediator>();
        }
        [Fact]
        public async Task Login_Success_Result()
        {
            var command = new PostLoginCommand(new Data.Entities.User { Username = "ecommerce", Password = "asd123" });
            _mediator.Setup(x => x.Send(It.IsAny<PostLoginCommand>(), new CancellationToken())).
                ReturnsAsync(new TokenModel());
            var userController = new UserController(_mediator.Object);

            //Action
            var result = await userController.Login(command.User);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsNotType<UnauthorizedObjectResult>(result);
        }
    }
}
