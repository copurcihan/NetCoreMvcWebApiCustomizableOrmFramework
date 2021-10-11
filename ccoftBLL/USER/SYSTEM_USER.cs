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
    public class SYSTEM_USER
    {
        public int SYSTEM_USER_ID { get; set; }
        public SYSTEM_USER_TYPE SYSTEM_USER_TYPE { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string PASSWORD_AGAIN { get; set; }
        public int APPROVED { get; set; }
        public int ACTIVE { get; set; }
        public DateTime DATE { get; set; }
        public DateTime LAST_LOGIN_DATE { get; set; }
        public List<PAGE> PAGE_LIST { get; set; }
        public SYSTEM_USER(int p_iSystemUserId, string p_sName, string p_sSurname)
        {
            this.SYSTEM_USER_ID = p_iSystemUserId;
            this.SYSTEM_USER_TYPE = null;
            this.NAME = p_sName;
            this.SURNAME = p_sSurname;
            this.EMAIL = "";
            this.PASSWORD = "";
            this.PASSWORD_AGAIN = "";
            this.APPROVED = 0;
            this.ACTIVE = 0;
            this.PAGE_LIST = null;
            this.DATE = DateTime.Now;
            this.LAST_LOGIN_DATE = DateTime.Now;
        }
        public SYSTEM_USER()
        {
            this.SYSTEM_USER_ID = 0;
            this.SYSTEM_USER_TYPE = new SYSTEM_USER_TYPE();
            this.NAME = "";
            this.SURNAME = "";
            this.EMAIL = "";
            this.PASSWORD = "";
            this.PASSWORD_AGAIN = "";
            this.APPROVED = 0;
            this.ACTIVE = 0;
            this.PAGE_LIST = new List<PAGE>();
            this.DATE = DateTime.Now;
            this.LAST_LOGIN_DATE = DateTime.Now;
        }
        public SYSTEM_USER(DataRow p_row)
        {
            this.SYSTEM_USER_ID = p_row.getInt("SYSTEM_USER_ID");
            this.SYSTEM_USER_TYPE = new SYSTEM_USER_TYPE(p_row);
            this.NAME = p_row.getString("NAME");
            this.SURNAME = p_row.getString("SURNAME");
            this.EMAIL = p_row.getString("EMAIL");
            this.PASSWORD = p_row.getString("PASSWORD");
            this.PASSWORD_AGAIN = "";
            this.APPROVED = p_row.getInt("APPROVED");
            this.ACTIVE = p_row.getInt("ACTIVE");
            this.PAGE_LIST = new List<PAGE>();
            this.DATE = p_row.getDate("DATE");
            this.LAST_LOGIN_DATE = p_row.getDate("LAST_LOGIN_DATE");
        }
        public ProcessResult SignUp()
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (!this.NAME.IsOk() ||
                    !this.SURNAME.IsOk() ||
                    !this.EMAIL.IsOk() ||
                    !this.PASSWORD.IsOk() ||
                    !this.PASSWORD_AGAIN.IsOk())
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                if (this.PASSWORD != this.PASSWORD_AGAIN)
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.CheckPassword);
                    return l_cProcessResult;
                }
                this.EMAIL = this.EMAIL.ToLower();
                Result<List<SYSTEM_USER>> l_cSystemUserResult = GetByEmail(this.EMAIL);
                if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful && l_cSystemUserResult.m_cData.Count > 0)
                    return l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RegisteredUser);

                this.NAME = this.NAME.TrimStart().TrimEnd();
                this.SURNAME = this.SURNAME.TrimStart().TrimEnd();
                this.EMAIL = this.EMAIL.TrimStart().TrimEnd();
                this.PASSWORD = this.PASSWORD.TrimStart().TrimEnd();
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                l_cExecutor.AddWith("SYSTEM_USER_TYPE_ID", 2);
                l_cExecutor.AddWith("NAME", this.NAME);
                l_cExecutor.AddWith("SURNAME", this.SURNAME);
                l_cExecutor.AddWith("EMAIL", this.EMAIL);
                l_cExecutor.AddWith("PASSWORD", this.PASSWORD);
                l_cExecutor.AddWith("APPROVED", 1);
                l_cExecutor.AddWith("ACTIVE", 1);
                l_cExecutor.AddWith("DATE", DateTime.Now);
                l_cExecutor.AddWith("LAST_LOGIN_DATE", DateTime.Now);
                l_cProcessResult = l_cExecutor.Insert("SYSTEM_USERS");
                int l_iId = l_cProcessResult.m_iLastId;
                if (l_cProcessResult.m_eProcessState == ProcessState.Successful)
                {
                    l_cSystemUserResult = GetById(l_iId);
                    if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                    {
                        if (l_cSystemUserResult.m_cData.Count > 0)
                            l_cProcessResult = SendConfirmationMail(l_cSystemUserResult.m_cData[0]);
                    }
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<SYSTEM_USER>> Login()
        {
            Result<List<SYSTEM_USER>> l_cProcessResult = new Result<List<SYSTEM_USER>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (!this.EMAIL.IsOk() || !this.PASSWORD.IsOk())
                {
                    l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                this.EMAIL = this.EMAIL.ToLower();
                l_cProcessResult = GetByEmail(this.EMAIL);
                if (l_cProcessResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cProcessResult.m_cData.Count == 1)
                    {
                        SYSTEM_USER l_cObject = l_cProcessResult.m_cData[0];
                        if (l_cObject.ACTIVE == 1)
                        {
                            if (l_cObject.APPROVED == 1)
                            {
                                if (l_cObject.EMAIL == this.EMAIL && l_cObject.PASSWORD == this.PASSWORD)
                                {
                                    l_cProcessResult.m_cDetail.ChangeMessage(SystemMessage.LoginSuccessful);
                                    l_cProcessResult.m_cData[0].PASSWORD = "Atatürk";
                                    l_cProcessResult.m_cData[0].PAGE_LIST = new PAGE().GetMyPages(l_cProcessResult.m_cData[0].EMAIL).m_cData;
                                    UpdateLastLoginDate(l_cProcessResult.m_cData[0].SYSTEM_USER_ID);
                                }
                                else
                                {
                                    l_cProcessResult.m_cData = new List<SYSTEM_USER>();
                                    l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.LoginFailed);
                                }
                            }
                            else
                            {
                                l_cProcessResult.m_cData = new List<SYSTEM_USER>();
                                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.NotApprovedAccount);
                                SendConfirmationMail(l_cObject);
                            }

                        }
                        else
                        {
                            l_cProcessResult.m_cData = new List<SYSTEM_USER>();
                            l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.LoginFailed);
                        }

                    }
                    else
                    {
                        l_cProcessResult.m_cData = new List<SYSTEM_USER>();
                        l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.LoginFailed);
                    }
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        private ProcessResult UpdateLastLoginDate(int p_iSystemUserId)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (!p_iSystemUserId.IsId())
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                l_cExecutor.AddWith("LAST_LOGIN_DATE", DateTime.Now);
                l_cExecutor.AddWhere("SYSTEM_USER_ID", p_iSystemUserId);
                l_cProcessResult = l_cExecutor.Update("SYSTEM_USERS");
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public ProcessResult UpdateMyProfile(string p_sEmail)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (!this.NAME.IsOk() ||
                    !this.SURNAME.IsOk() ||
                    !this.EMAIL.IsOk() ||
                    !this.PASSWORD.IsOk())
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                p_sEmail = p_sEmail.ToLower();
                this.EMAIL = this.EMAIL.ToLower();
                Result<List<SYSTEM_USER>> l_cSystemUserResult;
                if (p_sEmail != this.EMAIL)
                {
                    l_cSystemUserResult = GetByEmail(this.EMAIL);
                    if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                    {
                        if (l_cSystemUserResult.m_cData.Count > 0)
                        {
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RegisteredUser);
                            return l_cProcessResult;
                        }
                    }
                }
                l_cSystemUserResult = GetByEmail(p_sEmail);
                if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cSystemUserResult.m_cData.Count > 0)
                    {
                        this.NAME = this.NAME.TrimStart().TrimEnd();
                        this.SURNAME = this.SURNAME.TrimStart().TrimEnd();
                        this.EMAIL = this.EMAIL.TrimStart().TrimEnd();
                        this.PASSWORD = this.PASSWORD.TrimStart().TrimEnd();
                        BaseExecutor l_cExecutor = new AdoNetExecutor();
                        l_cExecutor.AddWith("NAME", this.NAME);
                        l_cExecutor.AddWith("SURNAME", this.SURNAME);
                        l_cExecutor.AddWith("EMAIL", this.EMAIL);
                        l_cExecutor.AddWith("PASSWORD", this.PASSWORD);
                        l_cExecutor.AddWith("APPROVED", p_sEmail == this.EMAIL);
                        l_cExecutor.AddWhere("SYSTEM_USER_ID", l_cSystemUserResult.m_cData[0].SYSTEM_USER_ID);
                        l_cProcessResult = l_cExecutor.Update("SYSTEM_USERS");
                        if (l_cProcessResult.m_eProcessState == ProcessState.Successful)
                        {
                            if (p_sEmail != this.EMAIL)
                            {
                                l_cSystemUserResult = GetById(l_cSystemUserResult.m_cData[0].SYSTEM_USER_ID);
                                if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                                {
                                    if (l_cSystemUserResult.m_cData.Count > 0)
                                        l_cProcessResult = SendConfirmationMail(l_cSystemUserResult.m_cData[0]);
                                }
                            }
                        }
                        l_cProcessResult.ChangeMessage(SystemMessage.UpdatedProfileSuccessfull);
                    }
                }

            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public ProcessResult UpdateProfile(string p_sEmail)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (!this.SYSTEM_USER_ID.IsId() ||
                    !this.SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID.IsId() ||
                    !this.NAME.IsOk() ||
                    !this.SURNAME.IsOk())
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                if (!this.EMAIL.IsOk())
                    this.EMAIL = "";
                if (!this.PASSWORD.IsOk())
                    this.PASSWORD = "";
                p_sEmail = p_sEmail.ToLower();
                this.EMAIL = this.EMAIL.ToLower();
                Result<List<SYSTEM_USER>> l_cAdminSystemUserResult = GetByEmail(p_sEmail);
                if (l_cAdminSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cAdminSystemUserResult.m_cData.Count > 0)
                    {
                        if (l_cAdminSystemUserResult.m_cData[0].SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID == 1)
                        {
                            Result<List<SYSTEM_USER>> l_cSystemUserResult = GetById(this.SYSTEM_USER_ID);
                            if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                            {
                                if (l_cSystemUserResult.m_cData.Count > 0)
                                {
                                    if (l_cSystemUserResult.m_cData[0].EMAIL != this.EMAIL && this.EMAIL != "")
                                    {
                                        Result<List<SYSTEM_USER>> l_cTempSystemUserResult = GetByEmail(this.EMAIL);
                                        if (l_cTempSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                                        {
                                            if (l_cTempSystemUserResult.m_cData.Count > 0)
                                            {
                                                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RegisteredUser);
                                                return l_cProcessResult;
                                            }
                                        }
                                    }
                                }
                            }
                            this.NAME = this.NAME.TrimStart().TrimEnd();
                            this.SURNAME = this.SURNAME.TrimStart().TrimEnd();
                            this.EMAIL = this.EMAIL.TrimStart().TrimEnd();
                            this.PASSWORD = this.PASSWORD.TrimStart().TrimEnd();
                            BaseExecutor l_cExecutor = new AdoNetExecutor();
                            l_cExecutor.AddWith("SYSTEM_USER_TYPE_ID", this.SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID);
                            l_cExecutor.AddWith("NAME", this.NAME);
                            l_cExecutor.AddWith("SURNAME", this.SURNAME);
                            l_cExecutor.AddWith("EMAIL", this.EMAIL);
                            l_cExecutor.AddWith("PASSWORD", this.PASSWORD);
                            l_cExecutor.AddWith("APPROVED", l_cSystemUserResult.m_cData[0].EMAIL == this.EMAIL);
                            l_cExecutor.AddWhere("SYSTEM_USER_ID", this.SYSTEM_USER_ID);
                            l_cProcessResult = l_cExecutor.Update("SYSTEM_USERS");
                            if (l_cProcessResult.m_eProcessState == ProcessState.Successful)
                            {
                                if (l_cSystemUserResult.m_cData[0].EMAIL != this.EMAIL && this.EMAIL != "")
                                {
                                    l_cSystemUserResult = GetById(l_cSystemUserResult.m_cData[0].SYSTEM_USER_ID);
                                    if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                                    {
                                        if (l_cSystemUserResult.m_cData.Count > 0)
                                            l_cProcessResult = SendConfirmationMail(l_cSystemUserResult.m_cData[0]);
                                    }
                                }
                            }
                            l_cProcessResult.ChangeMessage(SystemMessage.UpdatedProfileSuccessfull);
                        }
                        else
                        {
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.NotAuthorized);
                        }


                    }
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public ProcessResult Add(string p_sEmail)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (!this.SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID.IsId() ||
                    !this.NAME.IsOk() ||
                    !this.SURNAME.IsOk())
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                if (this.SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID == 1 || this.SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID == 2)
                {
                    if (!this.EMAIL.IsOk() ||
                    !this.PASSWORD.IsOk())
                    {
                        l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                        return l_cProcessResult;
                    }
                }
                if (!this.EMAIL.IsOk())
                    this.EMAIL = "";
                if (!this.PASSWORD.IsOk())
                    this.PASSWORD = "";
                p_sEmail = p_sEmail.ToLower();
                this.EMAIL = this.EMAIL.ToLower();
                Result<List<SYSTEM_USER>> l_cAdminSystemUserResult = GetByEmail(p_sEmail);
                if (l_cAdminSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cAdminSystemUserResult.m_cData.Count > 0)
                    {
                        if (l_cAdminSystemUserResult.m_cData[0].SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID == 1)
                        {
                            Result<List<SYSTEM_USER>> l_cSystemUserResult = GetByEmail(this.EMAIL);
                            if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                            {
                                if (l_cSystemUserResult.m_cData.Count > 0 && this.EMAIL != "")
                                {
                                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RegisteredUser);
                                    return l_cProcessResult;
                                }
                            }
                            BaseExecutor l_cExecutor = new AdoNetExecutor();
                            this.NAME = this.NAME.TrimStart().TrimEnd();
                            this.SURNAME = this.SURNAME.TrimStart().TrimEnd();
                            this.EMAIL = this.EMAIL.TrimStart().TrimEnd();
                            this.PASSWORD = this.PASSWORD.TrimStart().TrimEnd();
                            l_cExecutor.AddWith("SYSTEM_USER_TYPE_ID", this.SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID);
                            l_cExecutor.AddWith("NAME", this.NAME);
                            l_cExecutor.AddWith("SURNAME", this.SURNAME);
                            l_cExecutor.AddWith("EMAIL", this.EMAIL);
                            l_cExecutor.AddWith("PASSWORD", this.PASSWORD);
                            l_cExecutor.AddWith("APPROVED", 1);
                            l_cExecutor.AddWith("ACTIVE", 1);
                            l_cExecutor.AddWith("DATE", DateTime.Now);
                            l_cExecutor.AddWith("LAST_LOGIN_DATE", DateTime.Now);
                            l_cProcessResult = l_cExecutor.Insert("SYSTEM_USERS");
                            int l_iId = l_cProcessResult.m_iLastId;
                            if (l_cProcessResult.m_eProcessState == ProcessState.Successful && this.EMAIL != "")
                            {
                                l_cSystemUserResult = GetById(l_iId);
                                if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                                {
                                    if (l_cSystemUserResult.m_cData.Count > 0)
                                        l_cProcessResult = SendConfirmationMail(l_cSystemUserResult.m_cData[0]);
                                }
                            }
                        }
                        else
                        {
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.NotAuthorized);
                        }


                    }
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<SYSTEM_USER>> GetById(int p_iId)
        {
            Result<List<SYSTEM_USER>> l_cProcessResult = new Result<List<SYSTEM_USER>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<SYSTEM_USER>();
            try
            {
                if (!p_iId.IsId())
                {
                    l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                string l_sQuery = @"SELECT * FROM SYSTEM_USERS SU
                            LEFT OUTER JOIN SYSTEM_USER_TYPE SUT ON SUT.SYSTEM_USER_TYPE_ID = SU.SYSTEM_USER_TYPE_ID WHERE SU.SYSTEM_USER_ID=" + p_iId;
                l_cProcessResult.m_cDetail = l_cExecutor.Select(l_sQuery);
                l_cExecutor.f_GetClassList<SYSTEM_USER>(ref l_cProcessResult);
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<SYSTEM_USER>> GetByEmail(string p_sEmail)
        {
            Result<List<SYSTEM_USER>> l_cProcessResult = new Result<List<SYSTEM_USER>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<SYSTEM_USER>();
            try
            {
                if (!p_sEmail.IsOk())
                {
                    l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                p_sEmail = p_sEmail.ToLower();
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                string l_sQuery = @"SELECT * FROM SYSTEM_USERS SU
                            LEFT OUTER JOIN SYSTEM_USER_TYPE SUT ON SUT.SYSTEM_USER_TYPE_ID = SU.SYSTEM_USER_TYPE_ID WHERE SU.EMAIL='" + p_sEmail + "'";
                l_cProcessResult.m_cDetail = l_cExecutor.Select(l_sQuery);
                l_cExecutor.f_GetClassList<SYSTEM_USER>(ref l_cProcessResult);
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public ProcessResult RemindPassword(string p_sEmail)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                Result<List<SYSTEM_USER>> l_cUserResult = GetByEmail(p_sEmail);
                l_cProcessResult = l_cUserResult.m_cDetail;
                if (l_cUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cUserResult.m_cData.Count > 0)
                    {
                        l_cProcessResult = SendRemindPasswordMail(l_cUserResult.m_cData[0]);
                    }
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        private ProcessResult SendConfirmationMail(SYSTEM_USER p_cObject)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                Result<List<MAIL_BODY>> l_cMailBodyResult = new MAIL_BODY().GetById(1);
                l_cProcessResult = l_cMailBodyResult.m_cDetail;
                if (l_cMailBodyResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cMailBodyResult.m_cData.Count > 0)
                    {
                        List<string> l_sEmailList = new List<string>();
                        List<string> l_sBccEmailList = new List<string>();
                        l_sEmailList.Add(p_cObject.EMAIL);
                        l_sBccEmailList.Add("copurchn@gmail.com");
                        List<BODY_DATA> l_cBodyDate = new List<BODY_DATA>();
                        l_cBodyDate.Add(new BODY_DATA("#NAME#", p_cObject.NAME));
                        l_cBodyDate.Add(new BODY_DATA("#EPOSTA#", p_cObject.EMAIL));
                        l_cBodyDate.Add(new BODY_DATA("#SIFRE#", p_cObject.PASSWORD));
                        l_cBodyDate.Add(new BODY_DATA("#WEBSITE#", APPLICATION.APPLICATION_NAME));
                        l_cBodyDate.Add(new BODY_DATA("#LINK#", APPLICATION.WEB_ADDRESS));
                        l_cBodyDate.Add(new BODY_DATA("#ADRES#", APPLICATION.ADDRESS));
                        l_cBodyDate.Add(new BODY_DATA("#YIL#", DateTime.Now.Year));
                        string p_sUrl = APPLICATION.EMAIL_VERIFICATION + Base64Encode(p_cObject.SYSTEM_USER_ID);
                        l_cBodyDate.Add(new BODY_DATA("#URL#", p_sUrl));
                        if (new EMAIL_MANAGER().f_gSendEmail(l_cMailBodyResult.m_cData[0].ChangeTags(l_cBodyDate), l_sEmailList, l_sBccEmailList, l_cMailBodyResult.m_cData[0].SUBJECT))
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Successful, null, SystemMessage.ActivateAccount);
                        else
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.EmailError);
                    }
                    else
                    {
                        l_cProcessResult.ChangeMessage(SystemMessage.EmailError);
                        return l_cProcessResult;
                    }
                }
                else
                {
                    l_cProcessResult.ChangeMessage(SystemMessage.EmailError);
                    return l_cProcessResult;
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        private ProcessResult SendRemindPasswordMail(SYSTEM_USER p_cObject)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                Result<List<MAIL_BODY>> l_cMailBodyResult = new MAIL_BODY().GetById(2);
                l_cProcessResult = l_cMailBodyResult.m_cDetail;
                if (l_cMailBodyResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cMailBodyResult.m_cData.Count > 0)
                    {
                        List<string> l_sEmailList = new List<string>();
                        List<string> l_sBccEmailList = new List<string>();
                        l_sEmailList.Add(p_cObject.EMAIL);
                        l_sBccEmailList.Add("copurchn@gmail.com");
                        List<BODY_DATA> l_cBodyDate = new List<BODY_DATA>();
                        l_cBodyDate.Add(new BODY_DATA("#NAME#", p_cObject.NAME));
                        l_cBodyDate.Add(new BODY_DATA("#EPOSTA#", p_cObject.EMAIL));
                        l_cBodyDate.Add(new BODY_DATA("#SIFRE#", p_cObject.PASSWORD));
                        l_cBodyDate.Add(new BODY_DATA("#WEBSITE#", APPLICATION.APPLICATION_NAME));
                        l_cBodyDate.Add(new BODY_DATA("#LINK#", APPLICATION.WEB_ADDRESS));
                        l_cBodyDate.Add(new BODY_DATA("#ADRES#", APPLICATION.ADDRESS));
                        l_cBodyDate.Add(new BODY_DATA("#YIL#", DateTime.Now.Year));
                        l_cBodyDate.Add(new BODY_DATA("#AKTIF#", APPLICATION.WEB_ADDRESS));
                        if (new EMAIL_MANAGER().f_gSendEmail(l_cMailBodyResult.m_cData[0].ChangeTags(l_cBodyDate), l_sEmailList, l_sBccEmailList, l_cMailBodyResult.m_cData[0].SUBJECT))
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Successful, null, SystemMessage.PasswordSent);
                        else
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.EmailError);
                    }
                    else
                    {
                        l_cProcessResult.ChangeMessage(SystemMessage.EmailError);
                        return l_cProcessResult;
                    }
                }
                else
                {
                    l_cProcessResult.ChangeMessage(SystemMessage.EmailError);
                    return l_cProcessResult;
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        private static string Base64Encode(int p_iUser_id)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(p_iUser_id.ToString());
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private static int Base64Decode(string base64EncodedData)
        {
            int l_iResult = 0;
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                l_iResult = Convert.ToInt32(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
            }
            catch (Exception)
            {

                return l_iResult;
            }
            return l_iResult;
        }
        public ProcessResult ApproveMyAccount(string p_sBaseCode)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                int l_iSystemUserId = Base64Decode(p_sBaseCode);
                if (l_iSystemUserId > 0)
                {
                    BaseExecutor l_cExecutor = new AdoNetExecutor();
                    Result<List<SYSTEM_USER>> l_cTempProcessResult = GetById(l_iSystemUserId);
                    if (l_cTempProcessResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                    {
                        if (l_cTempProcessResult.m_cData.Count == 1)
                        {
                            l_cExecutor.AddWith("APPROVED", 1);
                            l_cExecutor.AddWhere("SYSTEM_USER_ID", l_iSystemUserId);
                            l_cProcessResult = l_cExecutor.Update("SYSTEM_USERS");
                            if (l_cProcessResult.m_eProcessState == ProcessState.Successful)
                            {
                                l_cProcessResult.ChangeMessage(SystemMessage.ApprovedAccount);

                            }
                        }
                        else
                        {
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.NoUserFound);
                        }
                    }
                    else
                    {
                        l_cProcessResult = l_cTempProcessResult.m_cDetail;
                    }
                }
                else
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.NoUserFound);
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }

            return l_cProcessResult;
        }
        public ProcessResult ApproveUser(string p_sEmail)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (!this.SYSTEM_USER_ID.IsId())
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                p_sEmail = p_sEmail.ToLower();
                Result<List<SYSTEM_USER>> l_cAdminSystemUserResult = GetByEmail(p_sEmail);
                if (l_cAdminSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cAdminSystemUserResult.m_cData.Count > 0)
                    {
                        if (l_cAdminSystemUserResult.m_cData[0].SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID == 1)
                        {
                            Result<List<SYSTEM_USER>> l_cSystemUserResult = GetById(this.SYSTEM_USER_ID);
                            if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                            {
                                if (l_cSystemUserResult.m_cData.Count > 0)
                                {
                                    if (l_cSystemUserResult.m_cData[0].APPROVED == 1)
                                        l_cSystemUserResult.m_cData[0].APPROVED = 0;
                                    else
                                        l_cSystemUserResult.m_cData[0].APPROVED = 1;
                                }
                            }
                            BaseExecutor l_cExecutor = new AdoNetExecutor();
                            l_cExecutor.AddWith("APPROVED", l_cSystemUserResult.m_cData[0].APPROVED);
                            l_cExecutor.AddWhere("SYSTEM_USER_ID", this.SYSTEM_USER_ID);
                            l_cProcessResult = l_cExecutor.Update("SYSTEM_USERS");
                            if (l_cProcessResult.m_eProcessState == ProcessState.Successful)
                            {
                                if (l_cSystemUserResult.m_cData[0].APPROVED == 0)
                                    l_cProcessResult = SendConfirmationMail(l_cSystemUserResult.m_cData[0]);
                                if (l_cProcessResult.m_eProcessState == ProcessState.Successful)
                                {
                                    if (l_cSystemUserResult.m_cData[0].APPROVED == 0)
                                        l_cProcessResult.ChangeMessage(SystemMessage.ApprovalRemoved);
                                    else
                                        l_cProcessResult.ChangeMessage(SystemMessage.Approved);
                                }
                            }
                        }
                        else
                        {
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.NotAuthorized);
                        }
                    }
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public ProcessResult ActivateUser(string p_sEmail)
        {
            ProcessResult l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                if (!this.SYSTEM_USER_ID.IsId())
                {
                    l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                p_sEmail = p_sEmail.ToLower();
                Result<List<SYSTEM_USER>> l_cAdminSystemUserResult = GetByEmail(p_sEmail);
                if (l_cAdminSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cAdminSystemUserResult.m_cData.Count > 0)
                    {
                        if (l_cAdminSystemUserResult.m_cData[0].SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID == 1)
                        {
                            Result<List<SYSTEM_USER>> l_cSystemUserResult = GetById(this.SYSTEM_USER_ID);
                            if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                            {
                                if (l_cSystemUserResult.m_cData.Count > 0)
                                {
                                    if (l_cSystemUserResult.m_cData[0].ACTIVE == 1)
                                        l_cSystemUserResult.m_cData[0].ACTIVE = 0;
                                    else
                                        l_cSystemUserResult.m_cData[0].ACTIVE = 1;
                                }
                            }
                            BaseExecutor l_cExecutor = new AdoNetExecutor();
                            l_cExecutor.AddWith("ACTIVE", l_cSystemUserResult.m_cData[0].ACTIVE);
                            l_cExecutor.AddWhere("SYSTEM_USER_ID", this.SYSTEM_USER_ID);
                            l_cProcessResult = l_cExecutor.Update("SYSTEM_USERS");
                        }
                        else
                        {
                            l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.NotAuthorized);
                        }


                    }
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<NAMEID>> GetAsNameAndId(string p_sEmail)
        {
            string l_sClass = "SYSTEM_USER";
            string l_sFunction = "Result<List<NAMEID>> GetAsNameAndId(string p_sEmail)";
            Result<List<NAMEID>> l_cProcessResult = new Result<List<NAMEID>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<NAMEID>();
            try
            {
                p_sEmail = p_sEmail.ToLower();
                Result<List<SYSTEM_USER>> l_cSystemUserResult = new SYSTEM_USER().GetByEmail(p_sEmail);
                l_cProcessResult.m_cDetail = l_cSystemUserResult.m_cDetail;
                if (l_cSystemUserResult.m_cDetail.m_eProcessState == ProcessState.Successful)
                {
                    if (l_cSystemUserResult.m_cData.Count > 0)
                    {
                        if (l_cSystemUserResult.m_cData[0].SYSTEM_USER_TYPE.SYSTEM_USER_TYPE_ID == 1)
                        {
                            BaseExecutor l_cExecutor = new AdoNetExecutor();
                            string l_sQuery = "SELECT SYSTEM_USERS.SYSTEM_USER_ID AS ID, SYSTEM_USERS.NAME+' '+SYSTEM_USERS.SURNAME AS NAME FROM SYSTEM_USERS ORDER BY NAME ";
                            l_cProcessResult.m_cDetail = l_cExecutor.Select(l_sQuery);
                            l_cExecutor.f_GetClassList<NAMEID>(ref l_cProcessResult);
                            if (l_cProcessResult.m_cData.Count == 0)
                            {
                                l_cProcessResult.m_cDetail = new ProcessResult(l_sClass, l_sFunction, -1, null, ProcessState.Failed, null, SystemMessage.NoRecordsFound);
                                return l_cProcessResult;
                            }
                        }
                        else
                            l_cProcessResult.m_cDetail.ChangeMessage(SystemMessage.NotAuthorized);
                    }
                }
            }
            catch (Exception l_cE)
            {
                l_cProcessResult.m_cDetail = new ProcessResult(l_sClass, l_sFunction, -1, null, ProcessState.SystemException, l_cE, SystemMessage.SystemException);
            }
            return l_cProcessResult;
        }
        public Result<List<SYSTEM_USER>> Authenticate(string p_sEmail, string p_sPassword)
        {
            Result<List<SYSTEM_USER>> l_cProcessResult = new Result<List<SYSTEM_USER>>(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            l_cProcessResult.m_cData = new List<SYSTEM_USER>();
            try
            {
                if (!p_sEmail.IsOk())
                {
                    l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.RequiredFields);
                    return l_cProcessResult;
                }
                p_sEmail = p_sEmail.ToLower();
                BaseExecutor l_cExecutor = new AdoNetExecutor();
                string l_sQuery = "SELECT SYSTEM_USERS.* FROM SYSTEM_USERS";
                l_sQuery = l_sQuery + " WHERE SYSTEM_USERS.EMAIL='" + p_sEmail + "' AND SYSTEM_USERS.PASSWORD='" + p_sPassword+ "' AND SYSTEM_USERS.ACTIVE=1";
                l_cProcessResult.m_cDetail = l_cExecutor.Select(l_sQuery);
                l_cExecutor.f_GetClassList<SYSTEM_USER>(ref l_cProcessResult);
                if (l_cProcessResult.m_cData.Count != 1)
                {
                    l_cProcessResult.m_cDetail = new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, -1, null, ProcessState.Failed, null, SystemMessage.LoginFailed);
                    return l_cProcessResult;
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
