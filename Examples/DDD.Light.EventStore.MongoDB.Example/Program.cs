using System;
using DDD.Light.CQRS.InProcess;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;

namespace DDD.Light.EventStore.MongoDB.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var personReadModel = new MongoRepository<PersonDTO>("mongodb://localhost", "DDD_Light_MongoEventStore_Example", "Person_ReadModel");
            var mongoConfigStorageStrategy = new MongoStorageConfigStrategy("mongodb://localhost", "DDD_Light_MongoEventStore_Example", "EventStore");
            EventStore.MongoDB.MongoEventStore.Instance.Configure(mongoConfigStorageStrategy);
            EventBus.Instance.Configure(EventStore.MongoDB.MongoEventStore.Instance);

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

            // Drop readmodel on mongo and then run this to restore
            //EventBus.Instance.RestoreReadModel(EventBus.Instance);


            Console.ReadLine();
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
            person = EventStore.MongoDB.MongoEventStore.Instance.GetById<Person>(id); 
            //can also do this: 
            // person = MongoEventStore.Instance.GetById(id) as Person;
            person.NameMe(renamedName);

            var personDTO = personReadModel.GetById(id);
            Console.WriteLine("Person ID: " + personDTO.Id);
            Console.WriteLine("Person Name: " + personDTO.Name);
            Console.WriteLine("Person Was Renamed: " + personDTO.WasRenamed);
        }
    }
}
