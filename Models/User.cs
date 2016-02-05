using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDbSample.Models
{
    public class User
    {
        
        public ObjectId ID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
