/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Data;

namespace ccoftOBJ
{
    public class Result<T>
    {
        public T m_cData { get; set; }
        public ProcessResult m_cDetail { get; set; }
        public Result(string p_sClass, string p_sMethod)
        {
            this.m_cDetail = new ProcessResult(p_sClass, p_sMethod);
        }

    }
    public class ProcessResult
    {
        public string m_sClass { get; set; }
        public string m_sMethod { get; set; }
        public int m_iLastId { get; set; }
        public DataTable m_cDataTable { get; set; }
        public ProcessState m_eProcessState;
        public List<String> m_lUserMessageList { get; set; }
        public string m_sErrorMessage { get; set; }
        public Exception m_cException { get; set; }
        public ProcessResult(string p_sClass, string p_sMethod)
        {
            m_sClass = p_sClass;
            m_sMethod = p_sMethod;
            m_eProcessState = ProcessState.Successful;
            m_cDataTable = new DataTable();
            m_lUserMessageList = new List<string>();
            if (m_eProcessState == ProcessState.Failed)
            {
                m_lUserMessageList.Add("İşlem Başarısız!");
                m_lUserMessageList.Add("Process Unsuccessful!");
            }
            else if (m_eProcessState == ProcessState.SystemException)
            {
                m_lUserMessageList.Add("İşlem esnasında beklenmedik bir hatayla karşılaşıldı! Lütfen tekrar deneyiniz ya da destek için; info@cihancopur.com! Hata: " + m_sErrorMessage);
                m_lUserMessageList.Add("An unexpected error was encountered during the process! Please try again or for support; info@cihancopur.com! Error: " + m_sErrorMessage);
            }
            else if (m_eProcessState == ProcessState.Successful)
            {
                m_lUserMessageList.Add("İşlem Başarılı");
                m_lUserMessageList.Add("Process Successful");
            }
        }
        public ProcessResult(string p_sClass, string p_sMethod, int p_iLastId, DataTable p_cDataTable,
            ProcessState p_eProcessState, Exception p_cException, SystemMessage p_eMessage)
        {
            m_sClass = p_sClass;
            m_sMethod = p_sMethod;
            m_iLastId = p_iLastId;
            if (p_cDataTable != null)
            {
                m_cDataTable = p_cDataTable;
            }
            else
            {
                m_cDataTable = new DataTable();
            }
            m_eProcessState = p_eProcessState;
            m_lUserMessageList = new List<string>();
            m_cException = p_cException;
            if (m_cException != null)
            {
                m_sErrorMessage = m_cException.Message;
            }
            if (p_eProcessState == ProcessState.Failed)
            {
                m_lUserMessageList.Add("İşlem Başarısız!");
                m_lUserMessageList.Add("Process Unsuccessful!");
            }
            else if (p_eProcessState == ProcessState.SystemException)
            {
                m_lUserMessageList.Add("İşlem esnasında beklenmedik bir hatayla karşılaşıldı! Lütfen tekrar deneyiniz ya da destek için; info@cihancopur.com! Hata: " + m_sErrorMessage);
                m_lUserMessageList.Add("An unexpected error was encountered during the process! Please try again or for support; info@cihancopur.com! Error: " + m_sErrorMessage);
            }
            else if (p_eProcessState == ProcessState.Successful)
            {
                m_lUserMessageList.Add("İşlem Başarılı");
                m_lUserMessageList.Add("Process Successful");
            }
            if (Convert.ToInt32(p_eMessage) > -1)
            {
                m_lUserMessageList = SYSTEM_MESSAGE.MESSAGE_LIST[Convert.ToInt32(p_eMessage)];
            }
        }
        public ProcessResult(int p_iLastId, DataTable p_cDataTable,
    ProcessState p_eProcessState, Exception p_cException, SystemMessage p_eMessage)
        {
            m_iLastId = p_iLastId;
            if (p_cDataTable != null)
            {
                m_cDataTable = p_cDataTable;
            }
            else
            {
                m_cDataTable = new DataTable();
            }
            m_eProcessState = p_eProcessState;
            m_lUserMessageList = new List<string>();
            m_cException = p_cException;
            if (m_cException != null)
            {
                m_sErrorMessage = m_cException.Message;
            }
            if (p_eProcessState == ProcessState.Failed)
            {
                m_lUserMessageList.Add("İşlem Başarısız!");
                m_lUserMessageList.Add("Process Unsuccessful!");
            }
            else if (p_eProcessState == ProcessState.SystemException)
            {
                m_lUserMessageList.Add("İşlem esnasında beklenmedik bir hatayla karşılaşıldı! Lütfen tekrar deneyiniz ya da destek için; info@cihancopur.com! Hata: " + m_sErrorMessage);
                m_lUserMessageList.Add("An unexpected error was encountered during the process! Please try again or for support; info@cihancopur.com! Error: " + m_sErrorMessage);
            }
            else if (p_eProcessState == ProcessState.Successful)
            {
                m_lUserMessageList.Add("İşlem Başarılı");
                m_lUserMessageList.Add("Process Successful");
            }
            if (Convert.ToInt32(p_eMessage) > -1)
            {
                m_lUserMessageList = SYSTEM_MESSAGE.MESSAGE_LIST[Convert.ToInt32(p_eMessage)];
            }
        }
        public void ChangeMessage(SystemMessage p_eMessage)
        {
            int P_iMsgNo = Convert.ToInt32(p_eMessage);

            if (this.m_lUserMessageList != null)
            {
                this.m_lUserMessageList = new List<string>();
            }
            if (P_iMsgNo > -1)
            {
                this.m_lUserMessageList = SYSTEM_MESSAGE.MESSAGE_LIST[P_iMsgNo];
            }
        }
    }
    public enum ProcessState
    {
        Failed = -1,
        SystemException = 0,
        Successful = 1
    }
    public enum SystemMessage
    {
        Empty = -1,
        NoRecordsFound = 0,
        SystemException = 1,
        RequiredFields = 2,
        RegisteredUser = 3,
        ActivateAccount = 4,
        LoginSuccessful = 5,
        LoginFailed = 6,
        EmailError = 7,
        NotApprovedAccount = 8,
        PasswordSent = 9,
        NotAuthorized = 10,
        CheckPassword = 11,
        NoUserFound = 12,
        ApprovedAccount = 13,
        CountCheck = 14,
        IsAvailableHorse = 15,
        AvailableRecord = 16,
        ShouldChangeRequestStatus = 17,
        Approved = 18,
        ApprovalRemoved = 19,
        IneligibleRequestStatus = 20,
        AtLeast2Generation = 21,
        EmailSent = 22,
        NotApprovedHorse = 23,
        SuccessfullHorseAddRequest = 24,
        HorseShouldBeYoungerFromParent = 25,
        SuccessfullHorseUpdateRequest = 26,
        NothingChanged = 27,
        UpdatedProfileSuccessfull = 28,
        CannotCreateReport = 29,
        ReportCreatedSuccessfully = 30,
        SuccessfulOrder = 31,
        ShouldBeLogin = 32,
        SuccessfullyAddedFavoriteAds = 33,
        SuccessfullyRemovedFavoriteAds = 34,
        AdsStatus1 = 35,
        AdsStatus3 = 36,
        AdsStatus4 = 37,
        AdsStatus5 = 38,
        AdsStatus6 = 39,
        ClosedBid = 40,
        ShouldBeGreaterMinOffer = 41,
        ShouldBeGreaterPreviousOffer = 42,
        SuccessfullyAddedBid = 43,
        SuccessfullyRemovedBid = 44,
        NotAvailableAnyMore = 45,
        SuccessfullyAddedAd = 46

    }
}
