using System.Collections.Generic;
using BooksStoreDBConfig;
using UserModel;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using MongoDB.Bson;
using Validate;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using Authorization;

namespace UsersServices
{
    public class UserServices
    {
        private readonly UserAuthorization _author;
        private readonly IMongoCollection<userSignUp> _user;
        private readonly validateUserData _userValidate;
        public UserServices(IBookstoreDBConfig settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<userSignUp>("Users");
            _userValidate = new validateUserData(_user);
            _author = new UserAuthorization();

        }
        public Task<List<userSignUp>> GetAll() => _user.Find(user => true).ToListAsync();

        public Task<userSignUp> GetUser(string id) => _user.Find(user => user.Id == id).FirstOrDefaultAsync();

        public async Task<HttpResponseMessage> signUp(userSignUp user)
        {
            var filterByEmail = new BsonDocument { { "email", user.Email } };
            bool validPassWord = _userValidate.comparePassWord(user);
            var foundedUser = await _user.Find(filterByEmail).FirstOrDefaultAsync();
            if (foundedUser != null || !validPassWord)
            {
                // System.Console.WriteLine("Email đã được sử dụng!!!");

                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else
            {
                user.passWord = BC.HashPassword(user.passWord, workFactor: 10);
                _user.InsertOne(user);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
        }
        public async Task<ActionResult<object>> Login(userLogIn user)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            var filterByEmail = new BsonDocument { { "email", user.Email } };
            var foundedUser = await _user.Find(filterByEmail).FirstOrDefaultAsync();

            if (foundedUser != null)
            {
                string passWord = foundedUser.passWord;
                bool comparePassWord = BC.Verify(user.passWord, passWord);
                if (!comparePassWord)
                {
                    return null;
                }
                else
                {
                    var userName = foundedUser.userName;
                    var token = _author.GenerateJwtToken(foundedUser);
                    object result = new {userName,token};
                    return result;
                }
            }
            else
            {
                return null;
            }

        }

        public void Update(string id, userSignUp userUpdate)
        {
            _user.ReplaceOne(user => user.Id == id, userUpdate);
        }

        public void Remove(string id) => _user.DeleteOne(user => user.Id == id);
    }

}