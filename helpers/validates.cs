using UserModel;
using BooksStoreDBConfig;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UsersServices;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using MongoDB.Bson;

namespace Validate
{
    public class validateUserData
    {
        private readonly IMongoCollection<userSignUp> _user;
        public validateUserData(IMongoCollection<userSignUp> userCollection)
        {
            this._user = userCollection;
        }

        // public bool validateSignUp(User user)
        // {

        //     // var validateEmail = Builders<BsonDocument>.Filter.Eq("email", BsonNull.Value);

        //     // if (!string.IsNullOrWhiteSpace(user.Email))
        //     // {
        //     //     var emailFilter = builder.Eq("email", user.Email);
        //     //     var result = _user.Find(emailFilter).FirstOrDefault();
        //     //     if (result != null)
        //     //     {
        //     //         return false;
        //     //     }
        //     // }
        //     // if (!string.IsNullOrWhiteSpace(user.userName))
        //     // {
        //     //     var userNameFilter = builder.Eq("userName", user.userName == BsonNull.Value);
        //     //     var result = _user.Find(userNameFilter).FirstOrDefault();
        //     //     if (result != null)
        //     //     {
        //     //         return false;
        //     //     }
        //     // }
        //     // if (!string.IsNullOrWhiteSpace(user.passWord))
        //     // {
        //     //     var passwordFilter = builder.Eq("passWord", user.passWord == BsonNull.Value);
        //     //     var result = _user.Find(passwordFilter).FirstOrDefault();
        //     //     if (result != null)
        //     //     {
        //     //         return false;
        //     //     }
        //     // }
        //     // if (!string.IsNullOrWhiteSpace(user.confirmPassword))
        //     // {
        //     //     var confirmPasswordFilter = builder.Eq("confirmPassword", user.confirmPassword == BsonNull.Value);
        //     //     var result = _user.Find(confirmPasswordFilter).FirstOrDefault();
        //     //     if (result != null)
        //     //     {
        //     //         return false;
        //     //     }
        //     // }
        //     // if (!string.IsNullOrWhiteSpace(user.phoneNumber))
        //     // {

        //     // }
        //     // var phoneNumberFilter = builder.Eq("phoneNumber", user.phoneNumber == BsonNull.Value);
        //     var phoneNumberFilter= Builders<User>.Filter.Eq("phoneNumber", BsonNull.Value);
        //     var result = _user.Find(phoneNumberFilter).Filter;
        //     if (phoneNumberFilter != null)
        //     {
        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // }

        //validate Email

        // public bool isValidEmail(string email){
        //     try
        //     {
        //         var addr = new System.Net.Mail.MailAddress(email);
        //         return addr.Address == email;
        //     }
        //     catch (System.Exception ex)
        //     {
        //          return false;
        //     }
        // }

        //validate PassWord
        public bool comparePassWord(userSignUp user)
        {
                if (user.passWord == user.confirmPassword)
                {
                    return true;
                }
                return false;
        
        }
    }
}