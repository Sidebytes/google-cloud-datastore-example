using System;
using System.Linq;
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
            if(args.Length==0 || args.Length > 2)
            {
                Console.WriteLine();
                Console.WriteLine("Application requires at least one and at most two arguments to run");
                Console.WriteLine("Argument 1 - GCP project ID being targeted - Required");
                Console.WriteLine("Argument 2 - Namespace within cloud datastore to save to - Optional");

                Environment.Exit(0);
            }

            string projectId = args[0].ToString();
            string namespaceId = string.Empty;

            if(args.Length > 1)
            {
                namespaceId = args[1].ToString();
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Write a project to cloud datastore");
                Console.WriteLine("2. Get all projects from cloud datastore");
                Console.WriteLine("3. Exit");

                Console.WriteLine();
                Console.Write("Select an option: ");

                int result = 0;
                int.TryParse(Console.ReadLine(),out result);

                switch (result)
                {
                    case 1:
                        WriteProjectToGoogleCloudDatastore(projectId, namespaceId);
                        break;
                    case 2:
                        GetAllProjectsFromGoogleCloudDatastore(projectId, namespaceId);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid menu opion");
                        break;
                }
            }
        }

        private static void WriteProjectToGoogleCloudDatastore(string projectId, string namespaceId)
        {
            var project = new Project() { Id = Guid.NewGuid(), Name = "Project One", Owner = "Mr Smith", };
            project.Client = new Client { Name = "The Client Name", AddressOne = "12345 Some Street", AddressTwo = "Someville" };

            project.Tasks = new List<Task>()
            {
                new Task(){ Name="Task number one", Description="This is task number one"},
                new Task(){ Name="Task number two", Description="This is task number two"}
            };

            DatastoreDb db = DatastoreDb.Create(projectId, namespaceId);

            var task = project.ToEntity(db);

            using (DatastoreTransaction transaction = db.BeginTransaction())
            {
                transaction.Upsert(task);
                transaction.Commit();
            }

            Console.WriteLine();
            Console.WriteLine($"Success! Saved project with ID {task.Key.Path[0].Name}");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------");
        }

        private static void GetAllProjectsFromGoogleCloudDatastore(string projectId, string namespaceId)
        {
            DatastoreDb db = DatastoreDb.Create(projectId, namespaceId);

            var query = new Query("project");
            var results = db.RunQuery(query);

            IList<Project> projects = results.Entities.Select(entity => entity.ToProject()).ToList();

            Console.WriteLine();
            Console.WriteLine($"Found {projects.Count} projects");
            foreach(Project project in projects)
            {
                Console.WriteLine($"Found project ID {project.Id}");
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------");
        }
    }
}
