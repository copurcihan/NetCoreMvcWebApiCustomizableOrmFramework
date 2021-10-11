/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 29.09.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using ccoftBLL.SYSTEM;
using ccoftBLL.USER;
using ccoftOBJ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api.Controllers
{
    /// <summary>
    /// It contains functions that allow you to perform basic database operations related to the SYSTEM_USER_TYPE table.
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public SystemUserController(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Adds a record to the SYSTEM_USERS table by signing up
        /// </summary>
        /// <param name="p_cObject">Required Fields: NAME,SURNAME,EMAIL,PASSWORD,PASSWORD_AGAIN</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProcessResult> SignUp([FromBody]SYSTEM_USER p_cObject)
        {
            return await Task.FromResult(p_cObject.SignUp());
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="p_cObject">Required Fields: EMAIL,PASSWORD</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<List<SYSTEM_USER>>> Login(SYSTEM_USER p_cObject)
        {
            return await Task.FromResult(p_cObject.Login());
        }

        /// <summary>
        /// Updates a registered user's information in the SYSTEM_USERS table
        /// </summary>
        /// <param name="p_cObject">Required Fields: NAME,SURNAME,EMAIL,PASSWORD</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProcessResult> UpdateMyProfile(SYSTEM_USER p_cObject)
        {
            return await Task.FromResult(p_cObject.UpdateMyProfile(User.Identity.Name));

        }
        /// <summary>
        /// The authorized user updates their information in the SYSTEM_USERS table of a registered user.
        /// </summary>
        /// <param name="p_cObject">Required Fields: NAME,SURNAME</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProcessResult> UpdateProfile(SYSTEM_USER p_cObject)
        {
            return await Task.FromResult(p_cObject.UpdateProfile(User.Identity.Name));
        }
        /// <summary>
        /// The authorized user adds records to the SYSTEM_USERS table
        /// </summary>
        /// <param name="p_cObject">Required Fields: NAME,SURNAME,EMAIL,PASSWORD</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProcessResult> Add(SYSTEM_USER p_cObject)
        {
            return await Task.FromResult(p_cObject.Add(User.Identity.Name));
        }
        /// <summary>
        /// Return a specific SYSTEM_USER record.
        /// </summary>
        /// <param name="p_iId">SYSTEM_USER_ID of SYSTEM_USER Table.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<List<SYSTEM_USER>>> GetById(int p_iId)
        {
            return await Task.FromResult(new SYSTEM_USER().GetById(p_iId));
        }
        /// <summary>
        /// Return a specific SYSTEM_USER record.
        /// </summary>
        /// <param name="p_sEmail">EMAIL of SYSTEM_USER Table.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<List<SYSTEM_USER>>> GetByEmail(string p_sEmail)
        {
            return await Task.FromResult(new SYSTEM_USER().GetByEmail(p_sEmail));
        }
        /// <summary>
        /// Sends a registered user's password to their email address
        /// </summary>
        /// <param name="p_sEmail">EMAIL of SYSTEM_USER Table.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ProcessResult> RemindPassword(string p_sEmail)
        {
            return await Task.FromResult(new SYSTEM_USER().RemindPassword(p_sEmail));
        }
        /// <summary>
        /// Verifies registered user account
        /// </summary>
        /// <param name="p_sBaseCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ProcessResult> ApproveMyAccount(string p_sBaseCode)
        {
            return await Task.FromResult(new SYSTEM_USER().ApproveMyAccount(p_sBaseCode));
        }
        /// <summary>
        /// Authorized user verifies a registered user's account
        /// </summary>
        /// <param name="p_cObject">Required Fields: SYSTEM_USER_ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProcessResult> ApproveUser(SYSTEM_USER p_cObject)
        {
            return await Task.FromResult(p_cObject.ApproveUser(User.Identity.Name));
        }
        /// <summary>
        /// Authorized user activate a registered user's account
        /// </summary>
        /// <param name="p_cObject">Required Fields: SYSTEM_USER_ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProcessResult> ActivateUser(SYSTEM_USER p_cObject)
        {
            return await Task.FromResult(p_cObject.ActivateUser(User.Identity.Name));
        }
        /// <summary>
        /// List all SYSTEM_USERS records as NAME AND ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<List<NAMEID>>> GetAsNameAndId()
        {
            return await Task.FromResult(new SYSTEM_USER().GetAsNameAndId(User.Identity.Name));
        }
        /// <summary>
        /// Create JWT Token
        /// </summary>
        /// <param name="p_CredentialValues">Pass UserName,PassWord as Base64</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate(string p_CredentialValues)
        {
            try
            {
                var l_cCredential = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(p_CredentialValues));
                var values = l_cCredential.Split(':');
                Result<List<SYSTEM_USER>> l_cSystemUser = new SYSTEM_USER().Authenticate(p_sEmail: values[0], p_sPassword: values[1]);
                if (l_cSystemUser.m_cDetail.m_eProcessState == ProcessState.Successful && l_cSystemUser.m_cData.Count > 0)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("AppSettings:Secret"));
                    Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name,l_cSystemUser.m_cData[0].SYSTEM_USER_ID.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    // return basic user info (without password) and token to store client side
                    return await Task.FromResult(Ok(new
                    {
                        Id = l_cSystemUser.m_cData[0].SYSTEM_USER_ID,
                        Username = l_cSystemUser.m_cData[0].EMAIL,
                        FirstName = l_cSystemUser.m_cData[0].NAME,
                        LastName = l_cSystemUser.m_cData[0].SURNAME,
                        Token = tokenString
                    }));
                }
                else
                    return await Task.FromResult(BadRequest(new { message = "Username or password is incorrect" }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(BadRequest(e.Message));
            }


        }
    }
}
