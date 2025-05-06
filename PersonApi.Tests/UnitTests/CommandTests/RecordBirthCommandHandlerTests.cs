using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PersonApi.Application.Commands.AddPerson;
using PersonApi.Application.Commands.RecordBirth;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Enums;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Tests.UnitTests.CommandTests
{
    public class RecordBirthCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdatePersonBirthInfo()
        {
            // Arrange
            var person = new Person
            {
                Id = 1,
                Name = "Jet",
                Surname = "Scott",
                Gender = Gender.F
            };

            var mockRepo = new Mock<IPersonRepository>();
            mockRepo.Setup(r => r.GetById(1)).Returns(person);
            var logger = new Mock<ILogger<RecordBirthCommandHandler>>();

            var handler = new RecordBirthCommandHandler(mockRepo.Object, logger.Object);

            var command = new RecordBirthCommand
            {
                PersonId = 1,
                BirthDate = new DateTime(2000, 1, 1),
                BirthLocation = "Shibuya"
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeTrue();
            mockRepo.Verify(r => r.Update(It.Is<Person>(p =>
                p.BirthDate == command.BirthDate &&
                p.BirthLocation == command.BirthLocation
            )), Times.Once);
        }
    }

}