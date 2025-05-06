using PersonApi.Domain.Enums;
using System;
using System.Reflection;

namespace PersonApi.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthLocation { get; set; }
        public DateTime? DeathDate { get; set; }
        public string? DeathLocation { get; set; }
    }
}