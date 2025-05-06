using PersonApi.Domain.Enums;
using System;

namespace PersonApi.Domain.Entities
{
    public class PersonVersion
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthLocation { get; set; }
        public DateTime? DeathDate { get; set; }
        public string? DeathLocation { get; set; }
        public DateTime VersionDate { get; set; }
    }
}
