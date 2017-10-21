using Google.Cloud.Datastore.V1;
using GoogleCloudDatastore.Domain;
using System.Collections.Generic;

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
            ["addressone"] = client.Name,
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
    }
}
