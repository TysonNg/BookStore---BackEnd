namespace BooksStoreDBConfig
{
    public class BookstoreDBConfig : IBookstoreDBConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBookstoreDBConfig
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}