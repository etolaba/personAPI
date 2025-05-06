using MediatR;
using Microsoft.Extensions.Logging;
using PersonApi.Application.Queries.GetAll;
using PersonApi.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PersonApi.Application.Commands.RecordBirth
{
    public class RecordBirthCommandHandler : IRequestHandler<RecordBirthCommand, bool>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<RecordBirthCommandHandler> _logger;

        public RecordBirthCommandHandler(IPersonRepository repository, ILogger<RecordBirthCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<bool> Handle(RecordBirthCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RecordBirthCommand");

            var person = _repository.GetById(request.PersonId);
            if (person == null)
                return Task.FromResult(false);

            if (request.BirthDate.HasValue)
                person.BirthDate = request.BirthDate;

            if (!string.IsNullOrWhiteSpace(request.BirthLocation))
                person.BirthLocation = request.BirthLocation;

            _repository.Update(person);
            return Task.FromResult(true);
        }
    }
}
