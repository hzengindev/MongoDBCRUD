using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbSample.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace MongoDbSample.Controllers
{
    public class HomeController : Controller
    {
        private MongoClient mongoClient;
        private string connectionString = string.Empty;
        private IMongoDatabase db;
        private IMongoCollection<BsonDocument> userCollection;

        public HomeController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            mongoClient = new MongoClient(connectionString);
            db = mongoClient.GetDatabase("zngndb");
            userCollection = db.GetCollection<BsonDocument>("User");
        }

        public ActionResult Index()
        {
            List<User> userList = new List<User>();

            var userDocument = userCollection.Find(new BsonDocument()).ToList();

            foreach (var item in userDocument)
            {
                userList.Add(new User
                {
                    ID = item["_id"].AsObjectId,
                    Name = item["Name"].AsString,
                    Lastname = item["Lastname"].AsString,
                    Username = item["Username"].AsString,
                    Password = item["Password"].AsString
                });
            }

            return View(userList);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                BsonDocument bDoc = new BsonDocument();
                bDoc.Add("Name", user.Name);
                bDoc.Add("Lastname", user.Lastname);
                bDoc.Add("Username", user.Username);
                bDoc.Add("Password", user.Password);

                userCollection.InsertOne(bDoc);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Edit(string id)
        {
            UserViewModel user = new UserViewModel();

            BsonDocument bDoc = new BsonDocument();
            bDoc.Add("_id", ObjectId.Parse(id));

            var userValue = userCollection.Find(bDoc).Single();

            user.ID = userValue["_id"].ToString();
            user.Name = userValue["Name"].AsString;
            user.Lastname = userValue["Lastname"].AsString;
            user.Username = userValue["Username"].AsString;
            user.Password = userValue["Password"].AsString;


            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                BsonDocument filter = new BsonDocument();
                filter.Add("_id", ObjectId.Parse(user.ID));


                BsonDocument bDoc = new BsonDocument();
                bDoc.Add("_id", ObjectId.Parse(user.ID));
                bDoc.Add("Name", user.Name);
                bDoc.Add("Lastname", user.Lastname);
                bDoc.Add("Username", user.Username);
                bDoc.Add("Password", user.Password);

                ReplaceOneResult ror = userCollection.ReplaceOne(filter, bDoc);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public ActionResult Delete(string id)
        {
            
            return View();
        }
    }
}