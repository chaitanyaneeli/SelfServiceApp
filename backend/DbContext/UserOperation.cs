using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using backend.Models;

namespace backend.DbContext
{
    public class UserOperation  
    {  
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<User> userCollection; 

        public UserOperation(IDbSettings settings)  
        {  
            client = new MongoClient(settings.ConnectionString);  
            database = client.GetDatabase(settings.DatabaseName); 
  
            userCollection = database.GetCollection<User>(settings.UserCollectionName);  
  
        }  
  
        public List<User> Get()  
        {  
            List<User> users;  
            users = userCollection.Find(u => true).ToList();  
            return users;  
        }  
  
        public User Get(string empid) =>  
            userCollection.Find<User>(u => u.EmpId == empid.ToString()).FirstOrDefault();  

        public string NewUser(User user)
        {
            User u = this.Get(user.EmpId);

            if(u == null) {
                userCollection.InsertOne(user);
                return "SUCCESS";
            }
            else
            {
                return "DUPLICATE";
            }
        }
  
    }  
}  