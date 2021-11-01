using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BooksModel
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string bookName { get; set; }

        public string[] thumbnail { get; set; }

        public double price { get; set; }
        public string description { get; set; }

        public string category { get; set; }

        public string author { get; set; }
        public int viewCounts { get; set; }
        public Book()
        {
            viewCounts = 0;
        }
    }


}