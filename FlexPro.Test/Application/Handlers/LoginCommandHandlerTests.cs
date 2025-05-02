using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexPro.Api.Application.Commands.Auth;
using FlexPro.Api.Infrastructure.Persistance;
using FlexPro.Api.Infrastructure.Services;
using FlexPro.Api.Application.DTOs;
using FlexPro.Api.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FlexPro.Test.Application.Handlers
{
    public class LoginCommandHandlerTests
    {
        private Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var userManagerMock = CreateUserManagerMock();
            var jwtMock = new Mock<IJwtTokenGenerator>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var user = new ApplicationUser { UserName = "testuser" };
            userManagerMock.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.CheckPasswordAsync(user, "123456")).ReturnsAsync(true);
            jwtMock.Setup(x => x.GenerateToken(user)).ReturnsAsync("fake-jwt-token");

            var handler = new LoginCommandHandler(userManagerMock.Object, jwtMock.Object, httpContextAccessorMock.Object);
            var command = new LoginCommand("testuser", "123456");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("fake-jwt-token", result);
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenUserNotFound()
        {
            var userManagerMock = CreateUserManagerMock();
            var jwtMock = new Mock<IJwtTokenGenerator>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            userManagerMock.Setup(x => x.FindByNameAsync("unknown")).ReturnsAsync((ApplicationUser)null);

            var handler = new LoginCommandHandler(userManagerMock.Object, jwtMock.Object, httpContextAccessorMock.Object);
            var command = new LoginCommand("unknown", "any");

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenPasswordIsIncorrect()
        {
            var userManagerMock = CreateUserManagerMock();
            var jwtMock = new Mock<IJwtTokenGenerator>();
            var user = new ApplicationUser { UserName = "testuser" };
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            userManagerMock.Setup(x => x.FindByNameAsync("testuser")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.CheckPasswordAsync(user, "wrongpass")).ReturnsAsync(false);

            var handler = new LoginCommandHandler(userManagerMock.Object, jwtMock.Object, httpContextAccessorMock.Object);
            var command = new LoginCommand("testuser", "wrongpass");

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
