using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

using backend.Models;

namespace backend.DbContext
{
    public class ServiceOperation  
    {  
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Service> serviceCollection; 

        public ServiceOperation(IDbSettings settings)  
        {  
            client = new MongoClient(settings.ConnectionString);  
            database = client.GetDatabase(settings.DatabaseName); 
  
            serviceCollection = database.GetCollection<Service>(settings.ServiceCollectionName);  
  
        }  
  
        public List<Service> Get(string empID)  
        {  
            List<Service> services; 

            var filter = Builders<Service>.Filter.Eq("empid", empID);
            var sort = Builders<Service>.Sort.Descending("_id");

            services  = serviceCollection.Find(filter).Sort(sort).ToList();
            
            return services;  
        }  

        public string NewService(Service service)
        {
                if(service != null){

                    serviceCollection.InsertOne(service);
                    return "SUCCESS";
                }
                else {   
                    return "FAIL";
                }
        }
  
    }  
}  