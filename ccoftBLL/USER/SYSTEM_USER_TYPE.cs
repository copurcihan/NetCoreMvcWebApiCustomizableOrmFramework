/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using ccoftBLL.SYSTEM;
using ccoftDAL;
using ccoftOBJ;
using System;
using System.Collections.Generic;
using System.Data;

namespace ccoftBLL.USER
{
    public class SYSTEM_USER_TYPE
    {
        public int SYSTEM_USER_TYPE_ID { get; set; }
        public string SYSTEM_USER_TYPE_TR { get; set; }
        public string SYSTEM_USER_TYPE_EN { get; set; }
        public string PAGE { get; set; }
        public SYSTEM_USER_TYPE()
        {
            this.SYSTEM_USER_TYPE_ID = 0;
            this.SYSTEM_USER_TYPE_TR = "";
            this.SYSTEM_USER_TYPE_EN = "";
            this.PAGE = "";
        }
        public SYSTEM_USER_TYPE(DataRow p_row)
        {
            this.SYSTEM_USER_TYPE_ID = p_row.getInt("SYSTEM_USER_TYPE_ID");
            this.SYSTEM_USER_TYPE_TR = p_row.getString("SYSTEM_USER_TYPE_TR");
            this.SYSTEM_USER_TYPE_EN = p_row.getString("SYSTEM_USER_TYPE_EN");
            this.PAGE = p_row.getString("PAGE");
        }
        public Result<List<SYSTEM_USER_TYPE>> Get()
        {
            Result<List<SYSTEM_USER_TYPE>> l_cProcessResult = new Result<List<SYSTEM_USER_TYPE>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<SYSTEM_USER_TYPE>();
            try
            {
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                l_cProcessResult.m_cDetail = l_cExecutor.Select("SYSTEM_USER_TYPE");
                l_cExecutor.f_GetClassList<SYSTEM_USER_TYPE>(ref l_cProcessResult);
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<SYSTEM_USER_TYPE>> GetById(int p_iId)
        {
            Result<List<SYSTEM_USER_TYPE>> l_cProcessResult = new Result<List<SYSTEM_USER_TYPE>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<SYSTEM_USER_TYPE>();
            try
            {
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                l_cExecutor.AddWhere("SYSTEM_USER_TYPE_ID", p_iId);
                l_cProcessResult.m_cDetail = l_cExecutor.Select("SYSTEM_USER_TYPE");
                l_cExecutor.f_GetClassList<SYSTEM_USER_TYPE>(ref l_cProcessResult);
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<NAMEID>> GetAsNameId(int p_iLanguage)
        {
            Result<List<NAMEID>> l_cProcessResult = new Result<List<NAMEID>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<NAMEID>();
            Result<List<SYSTEM_USER_TYPE>> l_cTempResult = new Result<List<SYSTEM_USER_TYPE>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cTempResult.m_cData = new List<SYSTEM_USER_TYPE>();
            try
            {
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                l_cTempResult.m_cDetail = l_cExecutor.Select("SELECT * FROM SYSTEM_USER_TYPE WHERE SYSTEM_USER_TYPE_ID<=3");
                l_cExecutor.f_GetClassList<SYSTEM_USER_TYPE>(ref l_cTempResult);
                l_cProcessResult.m_cDetail = l_cTempResult.m_cDetail;
                for (int i = 0; i < l_cTempResult.m_cData.Count; i++)
                {
                    if (p_iLanguage == 1)
                    {
                        l_cProcessResult.m_cData.Add(new NAMEID(l_cTempResult.m_cData[i].SYSTEM_USER_TYPE_ID, l_cTempResult.m_cData[i].SYSTEM_USER_TYPE_TR));
                    }
                    else
                    {
                        l_cProcessResult.m_cData.Add(new SYSTEM.NAMEID(l_cTempResult.m_cData[i].SYSTEM_USER_TYPE_ID, l_cTempResult.m_cData[i].SYSTEM_USER_TYPE_EN));
                    }

                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
    }
}
