using System;
using Google.Cloud.Datastore.V1;
using System.Collections.Generic;
using GoogleCloudDatastore.Domain;
using GoogleCloudDatastore.Extensions;

namespace GoogleCloudDatastore
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Write to cloud datastore");
                Console.WriteLine("2. Query cloud datastore");
                Console.WriteLine("3. Exit");

                Console.WriteLine();
                Console.Write("Select an option: ");

                var result = Console.ReadLine();

                switch (Convert.ToInt32(result))
                {
                    case 1:
                        WriteToGoogleCloudDatastore();
                        break;
                    case 2:
                        QueryGoogleCloudDatastore();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        
        private static void WriteToGoogleCloudDatastore()
        {
            var project = new Project() { Id = Guid.NewGuid(), Name = "Project One", Owner = "Mr Smith", };
            project.Client = new Client { Name = "The Client Name", AddressOne = "12345 Some Street", AddressTwo = "Someville" };

            project.Tasks = new List<Task>()
            {
                new Task(){ Name="Task number one", Description="This is task number one"},
                new Task(){ Name="Task number two", Description="This is task number two"}
            };

            //var projectId = "ENTER PROJECT NAME HERE";
            //var namespaceId = "ENTER NAMESAPCE HERE";

            var projectId = "timeme-dev";
            var namespaceId = "project";


            DatastoreDb db = DatastoreDb.Create(projectId, namespaceId);

            var task = project.ToEntity(db);

            using (DatastoreTransaction transaction = db.BeginTransaction())
            {
                transaction.Upsert(task);
                transaction.Commit();
            }

            Console.WriteLine($"Success! Saved project with ID {task.Key.Path[0].Name}");
            Console.WriteLine();
        }

        private static void QueryGoogleCloudDatastore()
        {

        }
    }
}
