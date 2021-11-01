using System;
using System.Collections.Generic;
using BooksModel;
using BooksStoreDBConfig;
using MongoDB.Driver;

namespace BooksServices
{
    public class BookServices
    {
        private readonly IMongoCollection<Book> _book;

        public BookServices(IBookstoreDBConfig settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _book = database.GetCollection<Book>("Books");
        }
        public List<Book> GetAll() => _book.Find(book => true).ToList();

        public Book Get(string id)
        {
            var foundedBook = _book.Find<Book>(book => book.Id == id).FirstOrDefault();
            if (foundedBook == null)
            {
                return null;
            }
            foundedBook.viewCounts += 1;
            _book.ReplaceOneAsync(book => book.Id == id, foundedBook);
            return foundedBook;
        }
        public List<Book> GetLatestBooks()
        {
            var foundedList = _book.Find<Book>(book => true)
            .Sort("{_id: -1}")
            .Limit(6);
            return foundedList.ToList();
        }
        public List<Book> GetHighLightBook(){
            var foundedList = _book.Find<Book>(book => true)
            .Sort("{viewCounts: -1}")
            .Limit(6);
            return foundedList.ToList();
        }
        public Book Add(Book book)
        {
            _book.InsertOne(book);
            return book;
        }

        public Object pagination(int? querryPage, string category)
        {
            var filter = Builders<Book>.Filter.Empty;
            if(!string.IsNullOrEmpty(category)){
                filter = Builders<Book>.Filter.Regex(field:"category",regex: new MongoDB.Bson.BsonRegularExpression(pattern:category,options:"i"));
            }
            // var filterByCategory = Builders<Book>.Filter.Eq(x => x.category, category); 
            var find = _book.Find(filter);

            int page = querryPage.GetValueOrDefault(0);
            int perPage = 8;
            var total = find.CountDocuments();
            return new
            {
                data = find.Skip(page * perPage).Limit(perPage).ToList(),
                total,
                page = page + 1,
                last_page = total / perPage
        };
    }

    public void Update(string id, Book bookUpdate)
    {
        _book.ReplaceOne(book => book.Id == id, bookUpdate);
    }
    public void Remove(string id) => _book.DeleteOne(book => book.Id == id);
}
}