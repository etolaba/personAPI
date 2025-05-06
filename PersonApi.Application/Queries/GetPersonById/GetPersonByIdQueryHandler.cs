using MediatR;
using Microsoft.Extensions.Logging;
using PersonApi.Application.Commands.RecordBirth;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PersonApi.Application.Queries.GetPersonById
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Person?>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<GetPersonByIdQueryHandler> _logger;

        public GetPersonByIdQueryHandler(IPersonRepository repository, ILogger<GetPersonByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<Person?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetPersonByIdQuery with Id: {Id}", request.Id);
            var person = _repository.GetById(request.Id);
            return Task.FromResult(person);
        }
    }
}
