using MediatR;
using PersonApi.Domain.Entities;
using System.Collections.Generic;

namespace PersonApi.Application.Queries.GetAll
{
    public class GetAllQuery : IRequest<IEnumerable<Person>>
    {
    }
}
