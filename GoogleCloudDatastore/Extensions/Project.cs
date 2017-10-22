using System.Linq;
using Google.Cloud.Datastore.V1;
using GoogleCloudDatastore.Domain;
using System.Collections.Generic;
using System;

namespace GoogleCloudDatastore.Extensions
{
    public static class ProjectExtensions
    {
        public static Entity ToEntity(this Project project, DatastoreDb db) => new Entity()
        {
            Key = db.CreateKeyFactory("project").CreateKey(project.Id.ToString()),
            ["name"] = project.Name,
            ["owner"] = project.Owner,
            ["client"] = project.Client.ToEntity(),
            ["tasks"] = project.Tasks.ToArrayValue()
        };

        public static Entity ToEntity(this Client client) => new Entity()
        {
            ["name"] = client.Name,
            ["addressone"] = client.AddressOne,
            ["addresstwo"] = client.AddressTwo
        };

        public static ArrayValue ToArrayValue(this IList<Task> tasks)
        {
            ArrayValue valueTasks = new ArrayValue();

            foreach(Task task in tasks)
            {
                Entity value = new Entity()
                {
                    ["name"] = task.Name,
                    ["description"] = task.Description
                };

                valueTasks.Values.Add(value);
            }

            return valueTasks;
        }

        public static Project ToProject(this Entity entity) => new Project()
        {
            Id = new Guid(entity.Key.Path.First().Name),
            Name = (string)entity["name"],
            Owner = (string)entity["owner"],
            Client = entity["client"].EntityValue.ToClient(),
            Tasks = entity["tasks"].ArrayValue.ToList()
        };

        public static Client ToClient(this Entity entity) => new Client()
        {
            Name = (string)entity["name"],
            AddressOne = (string)entity["addressone"],
            AddressTwo = (string)entity["addresstwo"]
        };

        public static List<Task> ToList(this ArrayValue array)
        {
            List<Task> tasks = new List<Task>();

            foreach(Value value in array.Values)
            {
                Entity entity = value.EntityValue;

                Task task = new Task()
                {
                    Name = (string)entity["name"],
                    Description = (string)entity["description"]
                };

                tasks.Add(task);
            }

            return tasks;
        }
    }
}
