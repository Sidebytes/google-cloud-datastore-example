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
            var project = new Project() { Id = Guid.NewGuid(), Name = "Project One", Owner="Mr Smith",};
            project.Client = new Client { Name="The Client Name", AddressOne="12345 Some Street",AddressTwo="Someville"};

            project.Tasks = new List<Task>()
            {
                new Task(){ Name="Task number one", Description="This is task number one"},
                new Task(){ Name="Task number two", Description="This is task number two"}
            };

            var projectId = "ENTER PROJECT NAME HERE";
            var namespaceId = "ENTER NAMESAPCE HERE";

            DatastoreDb db = DatastoreDb.Create(projectId,namespaceId);
            
            var task = project.ToEntity(db);

            using (DatastoreTransaction transaction = db.BeginTransaction())
            {
                transaction.Upsert(task);
                transaction.Commit();

                Console.WriteLine($"Saved {task.Key.Path[0].Name}");
            }

            Console.ReadLine();
        }
    }
}
