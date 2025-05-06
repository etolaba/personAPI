using PersonApi.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PersonApi.Domain.Interfaces
{
    public interface IPersonRepository
    {
        void Add(Person person);
        Person? GetById(int id);
        IEnumerable<Person> GetAll();
        void Update(Person person);
        IEnumerable<PersonVersion> GetHistory(int personId);

    }
}
