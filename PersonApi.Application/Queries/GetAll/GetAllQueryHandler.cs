using MediatR;
using Microsoft.Extensions.Logging;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PersonApi.Application.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Person>>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<GetAllQueryHandler> _logger;

        public GetAllQueryHandler(IPersonRepository repository, ILogger<GetAllQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<IEnumerable<Person>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllPeopleQuery");
            var people = _repository.GetAll();
            return Task.FromResult(people);
        }
    }
}
