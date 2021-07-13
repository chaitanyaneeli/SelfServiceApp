using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;  
  
namespace backend.Models  
{ 
    public class User  
    {  
        public User(string _id, string EmpID, string Name, string Email){
            this._id = _id;
            this.EmpId = EmpID;
            this.Name = Name;
            this.Email = Email;
            this.Password = "";
        }

        [BsonId]  
        [BsonRepresentation(BsonType.ObjectId)]  
        public string _id { get; set; }  

        [BsonElement("empid")]
        public string EmpId { get; set; }


        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("password")]
        public string Password { get; set; } 

        [BsonElement("email")]
        public string Email { get; set; }   

    }
}  