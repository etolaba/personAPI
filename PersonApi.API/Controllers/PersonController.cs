using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Application.Commands.AddPerson;
using PersonApi.Application.Commands.RecordBirth;
using PersonApi.Application.Queries.GetAll;
using PersonApi.Application.Queries.GetPersonById;
using PersonApi.Application.Queries.GetPersonHistory;
using System;
using System.Threading.Tasks;

namespace PersonApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adds a Person
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] AddPersonCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(AddPerson), new { id = result }, null);
        }

        /// <summary>
        /// Gets a person by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id));
            if (person == null)
                return NotFound($"Person with ID {id} not found.");
            return Ok(person);
        }

        /// <summary>
        /// Returns all people
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllQuery());
            return Ok(result);
        }

        /// <summary>
        /// Records birth info for an existing person.
        /// </summary>
        [HttpPut("record-birth")]
        public async Task<IActionResult> RecordBirth([FromBody] RecordBirthCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound($"Person with ID {command.PersonId} not found.");
            return NoContent();
        }

        /// <summary>
        /// Gets version history for a person.
        /// </summary>
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var history = await _mediator.Send(new GetPersonHistoryQuery(id));
            if (!history.Any())
                return NotFound($"No history found for person with ID {id}.");
            return Ok(history);
        }

    }
}
