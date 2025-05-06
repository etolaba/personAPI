using MediatR;
using Microsoft.Extensions.Logging;
using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace PersonApi.Application.Queries.GetPersonHistory
{
    public class GetPersonHistoryQueryHandler : IRequestHandler<GetPersonHistoryQuery, IEnumerable<PersonVersion>>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<GetPersonHistoryQueryHandler> _logger;

        public GetPersonHistoryQueryHandler(IPersonRepository repository, ILogger<GetPersonHistoryQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<IEnumerable<PersonVersion>> Handle(GetPersonHistoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetPersonHistoryQuery for PersonId={PersonId}", request.PersonId);

            var history = _repository.GetHistory(request.PersonId);

            if (!history.Any())
            {
                _logger.LogWarning("No history found for PersonId={PersonId}", request.PersonId);
            }
            else
            {
                _logger.LogInformation("Found {Count} version(s) for PersonId={PersonId}", history.Count(), request.PersonId);
            }

            return Task.FromResult(history);
        }
    }
}
