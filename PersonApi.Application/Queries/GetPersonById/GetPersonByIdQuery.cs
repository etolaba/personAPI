using MediatR;
using PersonApi.Domain.Entities;

namespace PersonApi.Application.Queries.GetPersonById
{
    public class GetPersonByIdQuery : IRequest<Person?>
    {
        public int Id { get; }

        public GetPersonByIdQuery(int id)
        {
            Id = id;
        }
    }
}
