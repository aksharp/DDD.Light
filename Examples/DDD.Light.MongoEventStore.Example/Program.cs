using System;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;

namespace DDD.Light.MongoEventStore.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var personReadModel = new MongoRepository<PersonDTO>("mongodb://localhost", "DDD_Light_MongoEventStore_Example", "Person_ReadModel");
            MongoEventStore.Instance.Configure("mongodb://localhost", "DDD_Light_MongoEventStore_Example", "EventStore");
            EventBus.Instance.Configure(MongoEventStore.Instance);
            EventBus.Instance.Subscribe((PersonCreated personCreated) =>
                {                    
                    var personDTO = new PersonDTO {Id = personCreated.Id};
                    personReadModel.Save(personDTO);
                });
            
            EventBus.Instance.Subscribe((PersonNamed personNamed) =>
                {                    
                    var personDTO = personReadModel.GetById(personNamed.PersonId);
                    personDTO.Name = personNamed.Name;
                    personDTO.WasRenamed = false;
                    personReadModel.Save(personDTO);
                });

            EventBus.Instance.Subscribe((PersonRenamed personRenamed) =>
                {                    
                    var personDTO = personReadModel.GetById(personRenamed.PersonId);
                    personDTO.Name = personRenamed.Name;                    
                    personDTO.WasRenamed = true;                    
                    personReadModel.Save(personDTO);
                });

            
            NamePerson(personReadModel);

            NameAndRenamePerson(personReadModel);
        }

        private static void NamePerson(IRepository<PersonDTO> personReadModel)
        {
            Console.Write("Enter person's Name: ");
            var name = Console.ReadLine();

            var id = Guid.NewGuid();
            var person = new Person(id);
            person.NameMe(name);

            var personDTO = personReadModel.GetById(id);
            Console.WriteLine("Person ID: " + personDTO.Id);
            Console.WriteLine("Person Name: " + personDTO.Name);
            Console.WriteLine("Person Was Renamed: " + personDTO.WasRenamed);
            Console.ReadLine();
        }

        private static void NameAndRenamePerson(IRepository<PersonDTO> personReadModel)
        {
            Console.Write("Enter person's Name: ");
            var name = Console.ReadLine();

            var id = Guid.NewGuid();
            var person = new Person(id);
            person.NameMe(name);

            Console.Write("Enter person's Name: ");
            var renamedName = Console.ReadLine();
            person = MongoEventStore.Instance.GetById<Person>(id);
            person.NameMe(renamedName);

            var personDTO = personReadModel.GetById(id);
            Console.WriteLine("Person ID: " + personDTO.Id);
            Console.WriteLine("Person Name: " + personDTO.Name);
            Console.WriteLine("Person Was Renamed: " + personDTO.WasRenamed);
            Console.ReadLine();
        }
    }

    internal class PersonDTO : Entity
    {
        public string Name { get; set; }
        public bool WasRenamed { get; set; }
    }
}
