using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PersonApi.Infrastructure.Repositories
{
    public class JsonPersonRepository : IPersonRepository
    {
        private readonly string _filePath = Path.Combine("data", "people.json");
        private readonly object _lock = new();
        private readonly string _versionFilePath = Path.Combine("data", "person_versions.json");

        public JsonPersonRepository()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public void Add(Person person)
        {
            lock (_lock)
            {
                var people = LoadAll().ToList();
                person.Id = people.Count > 0 ? people.Max(p => p.Id) + 1 : 1;
                people.Add(person);
                SaveAll(people);

                SaveVersion(person);
            }
        }

        public Person? GetById(int id)
        {
            lock (_lock)
            {
                return LoadAll().FirstOrDefault(p => p.Id == id);
            }
        }

        public IEnumerable<Person> GetAll()
        {
            lock (_lock)
            {
                return LoadAll();
            }
        }

        private IEnumerable<Person> LoadAll()
        {
            var json = File.ReadAllText(_filePath);
            var people = JsonSerializer.Deserialize<List<Person>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            }) ?? new List<Person>();
            return people;
        }

        private void SaveAll(IEnumerable<Person> people)
        {
            var json = JsonSerializer.Serialize(people, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            });

            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<PersonVersion> GetHistory(int personId)
        {
            if (!File.Exists(_versionFilePath))
                return Enumerable.Empty<PersonVersion>();

            var json = File.ReadAllText(_versionFilePath);
            var allVersions = JsonSerializer.Deserialize<List<PersonVersion>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            }) ?? new List<PersonVersion>();

            return allVersions
                .Where(v => v.PersonId == personId)
                .OrderByDescending(v => v.VersionDate);
        }
        public void Update(Person person)
        {
            lock (_lock)
            {
                var people = LoadAll().ToList();
                var index = people.FindIndex(p => p.Id == person.Id);
                if (index != -1)
                {
                    people[index] = person;
                    SaveAll(people);
                    SaveVersion(people[index]);
                }
            }
        }


        private void SaveVersion(Person person)
        {
            var versions = LoadVersions().ToList();

            versions.Add(new PersonVersion
            {
                PersonId = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Gender = person.Gender,
                BirthDate = person.BirthDate,
                BirthLocation = person.BirthLocation,
                DeathDate = person.DeathDate,
                DeathLocation = person.DeathLocation,
                VersionDate = DateTime.UtcNow
            });

            var json = JsonSerializer.Serialize(versions, new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            });

            File.WriteAllText(_versionFilePath, json);
        }

        private IEnumerable<PersonVersion> LoadVersions()
        {
            if (!File.Exists(_versionFilePath))
                return new List<PersonVersion>();

            var json = File.ReadAllText(_versionFilePath);
            return JsonSerializer.Deserialize<List<PersonVersion>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            }) ?? new List<PersonVersion>();
        }

    }
}
