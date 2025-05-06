using MediatR;
using System;

namespace PersonApi.Application.Commands.AddPerson
{
    public class AddPersonCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }
}
