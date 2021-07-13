using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;  
  
namespace backend.Models  
{  
    public class Service  
    {  
        [BsonId]  
        [BsonRepresentation(BsonType.ObjectId)]  
        public string _id { get; set; }  

        [BsonElement("empid")]
        public string EmpID { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("desc")]
        public string Desc { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }
    
    }  
}  