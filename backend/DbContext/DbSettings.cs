namespace backend.DbContext  
{  
    public class DbSettings : IDbSettings  
    {   public string ConnectionString { get; set; }  
        public string DatabaseName { get; set; }  
        public string UserCollectionName { get; set; }  
        public string ServiceCollectionName { get; set; } 
    }  
  
    public interface IDbSettings  
    {  
        public string ConnectionString { get; set; }  
        public string DatabaseName { get; set; }  
        public string UserCollectionName { get; set; }  
        public string ServiceCollectionName { get; set; }  


    }  
}  