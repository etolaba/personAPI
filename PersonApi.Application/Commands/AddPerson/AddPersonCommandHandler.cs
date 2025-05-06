using MediatR;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Enums;
using PersonApi.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace PersonApi.Application.Commands.AddPerson
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, int>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<AddPersonCommandHandler> _logger;

        public AddPersonCommandHandler(IPersonRepository repository, ILogger<AddPersonCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<int> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AddPersonCommand for {Name} {Surname}", request.Name, request.Surname);

            var person = new Person
            {
                Name = request.Name,
                Surname = request.Surname,
                Gender = Enum.TryParse<Gender>(request.Gender, true, out var genderParsed) ? genderParsed : Gender.Unknown
            };

            _repository.Add(person);

            return Task.FromResult(person.Id);
        }
    }

}
