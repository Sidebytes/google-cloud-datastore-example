using System;
using System.Collections.Generic;

namespace GoogleCloudDatastore.Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public Client Client { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
