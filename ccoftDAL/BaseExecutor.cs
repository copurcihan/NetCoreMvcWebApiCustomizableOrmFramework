/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using ccoftOBJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ccoftDAL
{
    public abstract class BaseExecutor
    {
        protected string m_sConnectionSTring { get; set; }
        public string m_sReturnSqlQuery { get; set; }
        protected int m_iLastId { get; set; }
        public bool m_bIsDataTable { get; set; }
        protected Dictionary<string, object> m_lWithParameterList = new Dictionary<string, object>();
        protected Dictionary<string, object> m_lWhereParameterList = new Dictionary<string, object>();
        protected List<string> m_lIndexColumnList = new List<string>();
        public BaseExecutor()
        {
            m_sReturnSqlQuery = "";
            m_iLastId = 0;
            m_bIsDataTable = false;
            m_lWithParameterList = new Dictionary<string, object>();
            m_lWhereParameterList = new Dictionary<string, object>();
            m_lIndexColumnList = new List<string>();
        }
        #region PARAMETERS
        public void AddWith(string p_sParameter, object p_oValue)
        {
            if (!(p_oValue == null || p_oValue == DBNull.Value))
            {
                this.m_lWithParameterList.Add(p_sParameter, p_oValue);
            }
        }
        public void AddWhere(string p_sParameter, object p_oValue)
        {
            if (!(p_oValue == null || p_oValue == DBNull.Value))
            {
                this.m_lWhereParameterList.Add(p_sParameter, p_oValue);
            }

        }
        public void AddColumn(string p_sParameter)
        {
            this.m_lIndexColumnList.Add(p_sParameter);
        }
        #endregion

        public abstract ProcessResult Insert(string p_sTableName);
        public abstract ProcessResult Update(string p_sTableName);
        public abstract ProcessResult Delete(string p_sTableName);
        public abstract ProcessResult Select(string p_sTableNameOrSqlQuery);
        public abstract ProcessResult Like(string p_sTableNameOrSqlQuery);
        public abstract System.Data.DataTable f_GetDataTable(string p_sTableNameOrSqlQuery, bool p_bIsUsing);
        public DataRow f_GetDataRow(string TableName)
        {
            DataTable l_cDataTable = this.f_GetDataTable(TableName, true);
            return (DataRow)l_cDataTable.Rows[0][0];
        }
        private List<T> f_pGetTableGeneric<T>(DataTable p_cDataTable) where T : class
        {
            List<T> l_cResultList = new List<T>();
            for (int i = 0; i < p_cDataTable.Rows.Count; i++)
            {
                var l_cConstructorInfo = typeof(T).GetConstructor(new[] { typeof(DataRow) });
                if (l_cConstructorInfo != null)
                {
                    T l_cObject = l_cConstructorInfo.Invoke(new object[] { p_cDataTable.Rows[i] }) as T;
                    MethodInfo l_cType = typeof(T).GetMethod("Run");
                    if (l_cType != null)
                    {
                        object[] parametersArray = new object[] { };
                        l_cType.Invoke(l_cObject, parametersArray);
                    }
                    l_cResultList.Add(l_cObject);
                }
            }
            return l_cResultList;
        }
        public void f_GetClassList<T>(ref Result<List<T>> p_cResult) where T : class
        {
            if (p_cResult.m_cDetail.m_cDataTable != null)
            {
                p_cResult.m_cData = f_pGetTableGeneric<T>(p_cResult.m_cDetail.m_cDataTable);
                p_cResult.m_cDetail.m_cDataTable = new DataTable();
                if (p_cResult.m_cData.Count == 0)
                {
                    p_cResult.m_cDetail = new ProcessResult(p_cResult.m_cDetail.m_sClass, p_cResult.m_cDetail.m_sMethod, -1, null, ProcessState.Failed, null, SystemMessage.NoRecordsFound);
                }
                else
                {
                    p_cResult.m_cDetail = new ProcessResult(p_cResult.m_cDetail.m_sClass, p_cResult.m_cDetail.m_sMethod, -1, null, ProcessState.Successful, null, SystemMessage.Empty);
                }
            }
        }
    }
}
