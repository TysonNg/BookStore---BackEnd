using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace UserModel
{
    public class userSignUp
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("email")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required!!!")] public string Email { get; set; }

        [BsonElement("UserName")]
        [Required(ErrorMessage = "userName is required!!!")]
        public string userName { get; set; }

        [BsonElement("PassWord")]
        [Required(ErrorMessage = "passWord is required!!!")] public string passWord { get; set; }

        [BsonElement("Confirm Password")]
        [Required(ErrorMessage = "confirmPassWord is required!!!")] public string confirmPassword { get; set; }

        [BsonElement("Phone Number")]
        [Required(ErrorMessage = "PhoneNumber is required!!!")] public string phoneNumber { get; set; }

        [BsonElement("Role")]
        [BsonDefaultValue("User")] public string Role { get; set; } = "user";
        public userSignUp()
        {
            Role = "user";
        }

    }

    public class userLogIn
    {
        [BsonElement("email")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required!!!")] public string Email { get; set; }

        [BsonElement("PassWord")]
        [Required(ErrorMessage = "passWord is required!!!")] public string passWord { get; set; }

    }
}