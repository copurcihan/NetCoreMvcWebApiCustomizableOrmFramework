/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using ccoftBLL.USER;
using ccoftDAL;
using ccoftOBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ccoftBLL.SYSTEM
{
    public class PAGE
    {
        public int PAGE_ID { get; set; }
        public int SUPER_ID { get; set; }
        public string PAGE_TR { get; set; }
        public string PAGE_EN { get; set; }
        public string ICON { get; set; }
        public string LINK { get; set; }
        public List<PAGE> SUB_PAGE { get; set; }
        public PAGE()
        {
            this.PAGE_ID = 0;
            this.SUPER_ID = 0;
            this.PAGE_TR = "";
            this.PAGE_EN = "";
            this.ICON = "";
            this.SUB_PAGE = new List<PAGE>();
            this.LINK = "";
        }
        public PAGE(DataRow p_row)
        {
            this.PAGE_ID = p_row.getInt("PAGE_ID");
            this.SUPER_ID = p_row.getInt("SUPER_ID");
            this.PAGE_TR = p_row.getString("PAGE_TR");
            this.PAGE_EN = p_row.getString("PAGE_EN");
            this.ICON = p_row.getString("ICON");
            this.LINK = p_row.getString("LINK");
            this.SUB_PAGE = new List<PAGE>();
        }
        public Result<List<PAGE>> GetMyPages(string p_sEmail)
        {
            Result<List<PAGE>> l_cProcessResult = new Result<List<PAGE>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<PAGE>();
            try
            {
                p_sEmail = p_sEmail.ToLower();
                Result<List<SYSTEM_USER>> l_cTempUser = new Result<List<SYSTEM_USER>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                l_cTempUser = new SYSTEM_USER().GetByEmail(p_sEmail);
                l_cProcessResult.m_cDetail = l_cTempUser.m_cDetail;
                if (l_cTempUser.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cTempUser.m_cData.Count > 0)
                    {
                        Result<List<PAGE>> l_cTempPages = new Result<List<PAGE>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        l_cTempPages.m_cData = new List<PAGE>();
                        string l_sQuery = "SELECT * FROM PAGE WHERE PAGE_ID IN (" + l_cTempUser.m_cData[0].SYSTEM_USER_TYPE.PAGE + ") ORDER BY SORT,PAGE_ID";
                        BaseExecutor l_cExecutor = new AdoNetExecutor();
                        l_cTempPages.m_cDetail = l_cExecutor.Select(l_sQuery);
                        l_cProcessResult.m_cDetail = l_cTempPages.m_cDetail;
                        l_cExecutor.f_GetClassList<PAGE>(ref l_cTempPages);
                        for (int i = 0; i < l_cTempPages.m_cData.Count; i++)
                        {
                            if (l_cTempPages.m_cData[i].SUPER_ID == 0)
                            {
                                l_cProcessResult.m_cData.Add(l_cTempPages.m_cData[i]);
                            }
                        }
                        for (int i = 0; i < l_cTempPages.m_cData.Count; i++)
                        {
                            if (l_cTempPages.m_cData[i].SUPER_ID != 0)
                            {
                                for (int k = 0; k < l_cProcessResult.m_cData.Count; k++)
                                {
                                    if (l_cTempPages.m_cData[i].SUPER_ID == l_cProcessResult.m_cData[k].PAGE_ID)
                                    {
                                        l_cProcessResult.m_cData[k].SUB_PAGE.Add(l_cTempPages.m_cData[i]);
                                        break;
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < l_cTempPages.m_cData.Count; i++)
                        {
                            if (l_cTempPages.m_cData[i].SUPER_ID != 0)
                            {
                                for (int k = 0; k < l_cProcessResult.m_cData.Count; k++)
                                {
                                    for (int j = 0; j < l_cProcessResult.m_cData[k].SUB_PAGE.Count; j++)
                                    {
                                        if (l_cTempPages.m_cData[i].SUPER_ID == l_cProcessResult.m_cData[k].SUB_PAGE[j].PAGE_ID)
                                        {
                                            l_cProcessResult.m_cData[k].SUB_PAGE[j].SUB_PAGE.Add(l_cTempPages.m_cData[i]);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<PAGE>> GetById(int p_iId)
        {
            Result<List<PAGE>> l_cProcessResult = new Result<List<PAGE>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<PAGE>();
            try
            {
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                l_cExecutor.AddWhere("PAGE_ID", p_iId);
                l_cProcessResult.m_cDetail = l_cExecutor.Select("PAGE");
                l_cExecutor.f_GetClassList<PAGE>(ref l_cProcessResult);
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<PAGE>> GetByLink(string p_sLink)
        {
            Result<List<PAGE>> l_cProcessResult = new Result<List<PAGE>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<PAGE>();
            try
            {
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                l_cExecutor.AddWhere("LINK", p_sLink);
                l_cProcessResult.m_cDetail = l_cExecutor.Select("PAGE");
                l_cExecutor.f_GetClassList<PAGE>(ref l_cProcessResult);
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }

    }
}
