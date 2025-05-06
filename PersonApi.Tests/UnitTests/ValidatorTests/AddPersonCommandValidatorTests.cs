using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PersonApi.Application.Commands.AddPerson;
using PersonApi.Application.Commands.RecordBirth;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Enums;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Tests.UnitTests.ValidatorTests
{
    public class AddPersonCommandValidatorTests
    {
        [Fact]
        public void Validator_ShouldFailWhenGenderIsInvalid()
        {
            // Arrange
            var validator = new AddPersonCommandValidator();
            var command = new AddPersonCommand
            {
                Name = "Name",
                Gender = "BananaGender"
            };

            // Act
            var result = validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Gender");
        }

        [Fact]
        public void Validator_ShouldSucceedWithValidData()
        {
            var validator = new AddPersonCommandValidator();
            var command = new AddPersonCommand
            {
                Name = "Lamin",
                Surname = "Lewan",
                Gender = "M"
            };

            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }
    }
}