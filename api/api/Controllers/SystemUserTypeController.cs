/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using ccoftBLL.SYSTEM;
using ccoftBLL.USER;
using ccoftOBJ;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Controllers
{
    /// <summary>
    /// It contains functions that allow you to perform basic database operations related to the SYSTEM_USER_TYPE table.
    /// </summary>
    [Route("[controller]/[action]")]
    public class SystemUserTypeController : ControllerBase
    {

        /// <summary>
        /// List all SYSTEM_USER_TYPE records.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<List<SYSTEM_USER_TYPE>>> Get()
        {
            return await Task.FromResult(new SYSTEM_USER_TYPE().Get());
        }
        /// <summary>
        /// Return a specific SYSTEM_USER_TYPE record
        /// </summary>
        /// <param name="p_iId">SYSTEM_USER_TYPE_ID of SYSTEM_USER_TYPE Table</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<List<SYSTEM_USER_TYPE>>> GetById(int p_iId)
        {
            return await Task.FromResult(new SYSTEM_USER_TYPE().GetById(p_iId));
        }
        /// <summary>
        /// List all SYSTEM_USER_TYPE records by language parameter
        /// </summary>
        /// <param name="p_iLanguage">1: Turkish 2: English</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<List<NAMEID>>> GetAsNameId(int p_iLanguage)
        {
            return await Task.FromResult(new SYSTEM_USER_TYPE().GetAsNameId(p_iLanguage));
        }
        //public async Task<DataTable> Get()
        //{
        //    return await Task.FromResult(new DataTable());
        //}
        //public ProcessResult1 Get()
        //{
        //    return new ProcessResult1();
        //}
        //[HttpGet]
        //public async Task<List<List<string>>> Get1212()
        //{
        //    return await Task.FromResult(SYSTEM_MESSAGE.MESSAGE_LIST);
        //}

    }
}
