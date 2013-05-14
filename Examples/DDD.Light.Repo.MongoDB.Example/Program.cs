using System;
using System.Linq;

namespace DDD.Light.Repo.MongoDB.Example
{
    class Program
    {
        private static void Main(string[] args)
        {
            // before running example, make sure MongoDB is running by executing %MONGO_INSTALL_DIR%\bin\mongod.exe

            // connection string to your MongoDB
            const string connectionString = "mongodb://localhost";
            // name of the database
            const string databaseName = "MyCompanyDB";
            // name of the collection (loosely equivalet to a TABLE in releational database)
            const string collectionName = "people";
            // instantiate a repository
            // in real application, this should be setup as a singleton (can be done through StructureMap, Unity or another DI container)
            var personRepository = new Repository<Person>(connectionString, databaseName, collectionName);

            Console.Write("Please enter person's first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Please enter person's last name: ");
            var lastName = Console.ReadLine();

            var id = Guid.NewGuid();

            var newPerson = new Person
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };

            // save to repository
            personRepository.Save(newPerson);

            Console.WriteLine("person saved");

            // get by id
            var personFoundById = personRepository.GetById(id);
            Console.WriteLine("get by id " + id + " result: " + personFoundById.FirstName + " " + personFoundById.LastName);

            // query repository
            var peopleFoundByQuery = personRepository.Get().Where(p => p.FirstName == newPerson.FirstName);
            Console.WriteLine("get by matching FirstName == " + newPerson.FirstName);
            peopleFoundByQuery.ToList().ForEach(p => Console.WriteLine(" match: " + p.FirstName + " " + p.LastName));
            

            Console.ReadLine();

        }
    }
}
