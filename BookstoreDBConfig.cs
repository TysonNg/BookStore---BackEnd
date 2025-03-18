namespace BooksStoreDBConfig
{
    
    public interface IBookstoreDBConfig
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class BookstoreDBConfig : IBookstoreDBConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }

}