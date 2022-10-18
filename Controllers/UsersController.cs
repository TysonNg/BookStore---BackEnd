using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using UsersServices;
using UserModel;

using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Users.Controller
{
    [ApiController]

    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {

        private readonly UserServices _userServices;
        public UsersController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<userSignUp>>> getAllUsers()
        {
            try
            {
                return await _userServices.GetAll();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<userSignUp>> getUser(string id)
        {

            try
            {
                var foundedUser = await _userServices.GetUser(id);
                if (foundedUser == null)
                {
                    return StatusCode(400, "Khong tim thay user!!!");
                }
                return foundedUser;
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponseMessage>> signUp(userSignUp user)
        {

            try
            {
                await _userServices.signUp(user);
                return CreatedAtAction("GetUser", new { id = user.Id.ToString() }, user);
            }
            catch (Exception )
            {
                return StatusCode(500, "Đăng ký thất bại!!!");
            }

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<HttpResponseMessage>> Login(userLogIn user)
        {

            try
            {
                var resultToken = await _userServices.Login(user);
                if (resultToken == null)
                {
                    return StatusCode(500, "Sai ten dang nhap hoac mat khau!!!");
                }
                object result = new { user = resultToken };
                return Ok(result);
            }
            catch (Exception )
            {
                return StatusCode(500, "Đăng nhập thất bại!!");
            }

        }
    }
}