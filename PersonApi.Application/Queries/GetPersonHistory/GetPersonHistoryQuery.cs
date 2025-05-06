using MediatR;
using PersonApi.Domain.Entities;
using System.Collections.Generic;

namespace PersonApi.Application.Queries.GetPersonHistory
{
    public class GetPersonHistoryQuery : IRequest<IEnumerable<PersonVersion>>
    {
        public int PersonId { get; }

        public GetPersonHistoryQuery(int personId)
        {
            PersonId = personId;
        }
    }
}