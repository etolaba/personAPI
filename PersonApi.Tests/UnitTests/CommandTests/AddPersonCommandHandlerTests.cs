using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PersonApi.Application.Commands.AddPerson;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Tests.UnitTests.CommandTests
{
    public class AddPersonCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldAddPersonSuccessfully()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var logger = new Mock<ILogger<AddPersonCommandHandler>>();

            mockRepo.Setup(r => r.Add(It.IsAny<Person>()))
                    .Callback<Person>(p => p.Id = 123);

            var handler = new AddPersonCommandHandler(mockRepo.Object, logger.Object);

            var command = new AddPersonCommand
            {
                Name = "Chester",
                Surname = "Shinoda",
                Gender = "M"
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeGreaterThan(0);
            mockRepo.Verify(r => r.Add(It.IsAny<Person>()), Times.Once);
        }
    }
}