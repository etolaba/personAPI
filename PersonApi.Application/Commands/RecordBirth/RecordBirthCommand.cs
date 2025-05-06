using MediatR;
using System;

namespace PersonApi.Application.Commands.RecordBirth
{
    public class RecordBirthCommand : IRequest<bool>
    {
        public int PersonId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthLocation { get; set; }
    }
}
