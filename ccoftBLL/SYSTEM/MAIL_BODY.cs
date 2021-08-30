/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using ccoftDAL;
using ccoftOBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ccoftBLL.SYSTEM
{
    public class MAIL_BODY
    {
        public int MAIL_BODY_ID { get; set; }
        public string MAIL_BODY_HTML { get; set; }
        public string SUBJECT { get; set; }
        public string TAGS { get; set; }
        public MAIL_BODY()
        {
            this.MAIL_BODY_ID = 0;
            this.MAIL_BODY_HTML = "";
            this.SUBJECT = "";
            this.TAGS = "";
        }
        public MAIL_BODY(DataRow p_row)
        {
            this.MAIL_BODY_ID = p_row.getInt("MAIL_BODY_ID");
            this.MAIL_BODY_HTML = p_row.getString("MAIL_BODY_HTML");
            this.SUBJECT = p_row.getString("SUBJECT");
            this.TAGS = p_row.getString("TAGS");
        }
        public Result<List<MAIL_BODY>> GetById(int p_iId)
        {
            Result<List<MAIL_BODY>> l_cProcessResult = new Result<List<MAIL_BODY>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<MAIL_BODY>();
            try
            {
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                l_cExecutor.AddWhere("MAIL_BODY_ID", p_iId);
                l_cProcessResult.m_cDetail = l_cExecutor.Select("MAIL_BODY");
                l_cExecutor.f_GetClassList<MAIL_BODY>(ref l_cProcessResult);
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public string ChangeTags(List<BODY_DATA> p_sTagDataList)
        {
            string l_sResult = "";
            try
            {
                for (int i = 0; i < p_sTagDataList.Count; i++)
                {
                    this.MAIL_BODY_HTML = this.MAIL_BODY_HTML.Replace(p_sTagDataList[i].TAG, p_sTagDataList[i].VALUE);
                }
                l_sResult = this.MAIL_BODY_HTML;
            }
            catch
            {
                return l_sResult;
            }
            return l_sResult;
        }
    }
    public class BODY_DATA
    {
        public string VALUE { get; set; }
        public string TAG { get; set; }
        public BODY_DATA()
        {
            VALUE = "";
            TAG = "";
        }
        public BODY_DATA(string p_sTag, object p_sValue)
        {
            VALUE = Convert.ToString(p_sValue);
            TAG = p_sTag;
        }
    }
}
