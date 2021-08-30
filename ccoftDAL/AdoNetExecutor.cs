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
using System.Data.SqlClient;


namespace ccoftDAL
{
    public class AdoNetExecutor : BaseExecutor
    {
        public AdoNetExecutor() : base()
        {
            this.m_sConnectionSTring = "Server=178.157.15.114;Database=TEST;User Id=test;Password=CihanCopur2021*;";
        }
        public override ProcessResult Insert(string p_sTableName)
        {
            try
            {
                using (SqlConnection l_cSqlConnection = new SqlConnection(this.m_sConnectionSTring))
                {
                    if (l_cSqlConnection.State == ConnectionState.Closed)
                    {
                        l_cSqlConnection.Open();
                    }
                    string l_SqlQuery = "INSERT INTO " + p_sTableName;
                    if (this.m_lWithParameterList.Count > 0)
                    {
                        l_SqlQuery += "(";
                        foreach (KeyValuePair<string, object> l_cItem in this.m_lWithParameterList)
                        {
                            l_SqlQuery += "" + l_cItem.Key + ",";
                        }
                        l_SqlQuery = l_SqlQuery.Substring(0, l_SqlQuery.Length - 1);
                        l_SqlQuery += ")  VALUES (";
                    }
                    foreach (KeyValuePair<string, object> l_cItem in m_lWithParameterList)
                    {
                        l_SqlQuery += "@" + l_cItem.Key + ",";
                    }
                    l_SqlQuery = l_SqlQuery.Substring(0, l_SqlQuery.Length - 1);
                    l_SqlQuery += "); SELECT CAST(scope_identity() AS int)";
                    using (SqlCommand l_cSqlCommand = new SqlCommand(l_SqlQuery, l_cSqlConnection))
                    {
                        foreach (KeyValuePair<string, object> item in m_lWithParameterList)
                        {
                            l_cSqlCommand.Parameters.Add(new SqlParameter(item.Key, item.Value));
                        }
                        this.m_iLastId = (int)l_cSqlCommand.ExecuteScalar();
                        if (this.m_iLastId > 0)
                        {
                            return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableName, m_bIsDataTable), ProcessState.Successful, null, SystemMessage.Empty);
                        }
                        else
                        {
                            return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableName, false), ProcessState.Failed, null, SystemMessage.SystemException);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableName, false), ProcessState.SystemException, ex, SystemMessage.SystemException);
            }
            finally
            {
                m_lWithParameterList.Clear();
                m_sReturnSqlQuery = "";
            }
        }
        public override ProcessResult Update(string p_sTableNameOrSqlQuery)
        {
            try
            {
                using (SqlConnection l_cSqlConnection = new SqlConnection(this.m_sConnectionSTring))
                {
                    if (l_cSqlConnection.State == ConnectionState.Closed)
                    {
                        l_cSqlConnection.Open();

                    }
                    string l_sSqlQuery = "";
                    if (p_sTableNameOrSqlQuery.ToLower().Contains(" set "))
                    {
                        l_sSqlQuery = p_sTableNameOrSqlQuery;
                    }
                    else
                    {
                        l_sSqlQuery = "UPDATE " + p_sTableNameOrSqlQuery;
                        l_sSqlQuery += " SET ";
                        foreach (KeyValuePair<string, object> l_cItem in m_lWithParameterList)
                        {
                            l_sSqlQuery += "" + l_cItem.Key + "=@" + l_cItem.Key;
                            l_sSqlQuery += ",";
                        }
                        l_sSqlQuery = l_sSqlQuery.Substring(0, l_sSqlQuery.Length - 1);
                        l_sSqlQuery += " WHERE ";
                        foreach (KeyValuePair<string, object> l_cItem in m_lWhereParameterList)
                            l_sSqlQuery += l_cItem.Key + "=@" + l_cItem.Key + "1 AND ";
                        l_sSqlQuery = l_sSqlQuery.Substring(0, l_sSqlQuery.Length - 1);
                        l_sSqlQuery += " 1=1";
                    }
                    using (SqlCommand l_cSqlCommand = new SqlCommand(l_sSqlQuery, l_cSqlConnection))
                    {
                        foreach (KeyValuePair<string, object> l_cItem in m_lWithParameterList)
                        {
                            l_cSqlCommand.Parameters.Add(new SqlParameter(l_cItem.Key, l_cItem.Value));
                        }
                        foreach (KeyValuePair<string, object> l_cItem in m_lWhereParameterList)
                        {
                            l_cSqlCommand.Parameters.Add(new SqlParameter(l_cItem.Key + "1", l_cItem.Value));
                        }
                        this.m_iLastId = l_cSqlCommand.ExecuteNonQuery();
                    }
                    if (this.m_iLastId > 0)
                    {
                        return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableNameOrSqlQuery, m_bIsDataTable), ProcessState.Successful, null, SystemMessage.Empty);
                    }
                    else
                    {
                        return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableNameOrSqlQuery, false), ProcessState.Failed, null, SystemMessage.NoRecordsFound);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableNameOrSqlQuery, false), ProcessState.SystemException, ex, SystemMessage.SystemException);
            }
            finally
            {
                m_lWithParameterList.Clear();
                m_lWhereParameterList.Clear();
                m_sReturnSqlQuery = "";
            }
        }
        public override ProcessResult Delete(string p_sTableName)
        {
            try
            {
                using (SqlConnection l_cSqlConnection = new SqlConnection(this.m_sConnectionSTring))
                {
                    if (l_cSqlConnection.State == ConnectionState.Closed)
                    {
                        l_cSqlConnection.Open();
                    }
                    string l_sSqlQuery = "DELETE FROM " + p_sTableName;
                    l_sSqlQuery += " WHERE ";
                    foreach (KeyValuePair<string, object> l_cItem in m_lWhereParameterList)
                        l_sSqlQuery += l_cItem.Key + "=@" + l_cItem.Key + "1 AND ";
                    l_sSqlQuery = l_sSqlQuery.Substring(0, l_sSqlQuery.Length - 1);
                    l_sSqlQuery += " 1=1";
                    using (SqlCommand l_cSqlCommand = new SqlCommand(l_sSqlQuery, l_cSqlConnection))
                    {
                        foreach (KeyValuePair<string, object> l_cItem in m_lWhereParameterList)
                        {
                            l_cSqlCommand.Parameters.Add(new SqlParameter(l_cItem.Key + "1", l_cItem.Value));
                        }
                        if (this.m_lWhereParameterList.Count > 0)
                        {
                            this.m_iLastId = l_cSqlCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            this.m_iLastId = -1;
                            return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableName, false), ProcessState.Failed, null, SystemMessage.NoRecordsFound);
                        }
                    }
                    if (this.m_iLastId > 0)
                    {
                        return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableName, m_bIsDataTable), ProcessState.Successful, null, SystemMessage.Empty);
                    }
                    else
                    {
                        return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableName, false), ProcessState.Failed, null, SystemMessage.NoRecordsFound);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableName, false), ProcessState.SystemException, ex, SystemMessage.SystemException);
            }
            finally
            {
                m_lWhereParameterList.Clear();
                m_sReturnSqlQuery = "";
            }
        }
        public override ProcessResult Select(string p_sTableNameOrSqlQuery)
        {
            DataTable l_cDataTable = new DataTable();
            try
            {
                using (SqlConnection l_cSqlConnection = new SqlConnection(this.m_sConnectionSTring))
                {
                    if (l_cSqlConnection.State == ConnectionState.Closed)
                    {
                        l_cSqlConnection.Open();
                    }
                    string l_sSqlQuery = "";
                    if (p_sTableNameOrSqlQuery.ToLower().Contains("select"))
                    {
                        l_sSqlQuery = p_sTableNameOrSqlQuery;
                    }
                    else
                    {
                        if (this.m_lIndexColumnList.Count > 0)
                        {
                            l_sSqlQuery = "SELECT ";
                            for (int i = 0; i < this.m_lIndexColumnList.Count; i++)
                                l_sSqlQuery += "[" + this.m_lIndexColumnList[i] + "],";
                            l_sSqlQuery = l_sSqlQuery.Substring(0, l_sSqlQuery.Length - 1);
                            l_sSqlQuery += " FROM " + p_sTableNameOrSqlQuery;
                        }
                        else
                            l_sSqlQuery = "SELECT * FROM " + p_sTableNameOrSqlQuery;
                        l_sSqlQuery += " WITH(NOLOCK) WHERE ";
                        foreach (KeyValuePair<string, object> l_cItem in m_lWhereParameterList)
                        {
                            l_sSqlQuery += l_cItem.Key + "=@" + l_cItem.Key + "1 AND ";
                        }
                        l_sSqlQuery = l_sSqlQuery.Substring(0, l_sSqlQuery.Length - 1);
                        l_sSqlQuery += " 1=1";
                    }
                    using (SqlCommand l_cSqlCommand = new SqlCommand(l_sSqlQuery, l_cSqlConnection))
                    {
                        foreach (KeyValuePair<string, object> l_cItem in m_lWhereParameterList)
                        {
                            l_cSqlCommand.Parameters.Add(new SqlParameter(l_cItem.Key + "1", l_cItem.Value));
                        }
                        using (SqlDataAdapter l_cSqlDataAdapter = new SqlDataAdapter(l_cSqlCommand))
                        {
                            l_cSqlDataAdapter.Fill(l_cDataTable);
                        }
                    }
                    return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, l_cDataTable, ProcessState.Successful, null, SystemMessage.Empty);
                }
            }
            catch (Exception ex)
            {
                return new ProcessResult(this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, this.m_iLastId, this.f_GetDataTable(p_sTableNameOrSqlQuery, false), ProcessState.SystemException, ex, SystemMessage.SystemException);
            }
            finally
            {
                m_lWhereParameterList.Clear();
                m_lIndexColumnList.Clear();
            }
        }
        public override ProcessResult Like(string p_sTableNameOrSqlQuery)
        {
            DataTable l_cDataTable = new DataTable();
            try
            {
                using (SqlConnection l_cSqlConnection = new SqlConnection(this.m_sConnectionSTring))
                {
                    if (l_cSqlConnection.State == ConnectionState.Closed)
                    {
                        l_cSqlConnection.Open();
                    }
                    string l_sSqlQuery = "";
                    if (p_sTableNameOrSqlQuery.ToLower().Contains("select"))
                    {
                        l_sSqlQuery = p_sTableNameOrSqlQuery;
                    }
                    else
                    {
                        if (this.m_lIndexColumnList.Count > 0)
                        {
                            l_sSqlQuery = "SELECT ";
                            for (int i = 0; i < this.m_lIndexColumnList.Count; i++)
                                l_sSqlQuery += "[" + this.m_lIndexColumnList[i] + "],";
                            l_sSqlQuery = l_sSqlQuery.Substring(0, l_sSqlQuery.Length - 1);
                            l_sSqlQuery += " FROM " + p_sTableNameOrSqlQuery;
                        }
                        else
                            l_sSqlQuery = "SELECT * FROM " + p_sTableNameOrSqlQuery;
                        l_sSqlQuery += "  WITH(NOLOCK)  WHERE ";
                        foreach (KeyValuePair<string, object> l_cItem in m_lWhereParameterList)
                        {
                            l_sSqlQuery += " (" + l_cItem.Key + " LIKE '' +@" + l_cItem.Key + "+'%') AND ";
                        }

                        l_sSqlQuery = l_sSqlQuery.Substring(0, l_sSqlQuery.Length - 4);
                    }
                    using (SqlCommand l_cSqlCommand = new SqlCommand(l_sSqlQuery, l_cSqlConnection))
                    {
                        foreach (KeyValuePair<string, object> l_cItem in m_lWhereParameterList)
                        {
                            l_cSqlCommand.Parameters.Add(new SqlParameter(l_cItem.Key, l_cItem.Value));
                        }
                        using (SqlDataAdapter l_cSqlDataAdapter = new SqlDataAdapter(l_cSqlCommand))
                        {
                            l_cSqlDataAdapter.Fill(l_cDataTable);

                            foreach (DataRow item in l_cDataTable.Rows)
                            {
                                string asd = item[0].ToString();
                            }
                        }
                    }
                    return new ProcessResult("AdoNetExecutor", "ProcessResult Select(string p_sTableNameOrSqlQuery)", this.m_iLastId, l_cDataTable, ProcessState.Successful, null, SystemMessage.Empty);
                }
            }
            catch (Exception ex)
            {
                return new ProcessResult("AdoNetExecutor", "ProcessResult Select(string p_sTableNameOrSqlQuery)", this.m_iLastId, this.f_GetDataTable(p_sTableNameOrSqlQuery, false), ProcessState.SystemException, ex, SystemMessage.SystemException);
            }
            finally
            {
                m_lWhereParameterList.Clear();
                m_lIndexColumnList.Clear();
            }
        }
        public override DataTable f_GetDataTable(string p_sTableNameOrSqlQuery, bool p_bIsUsing)
        {
            DataTable l_cDataTable = new DataTable();
            if (!string.IsNullOrEmpty(m_sReturnSqlQuery))
            {
                p_sTableNameOrSqlQuery = m_sReturnSqlQuery;
            }
            if (p_bIsUsing == true)
            {
                using (SqlConnection l_cSqlConnection = new SqlConnection(this.m_sConnectionSTring))
                {
                    if (l_cSqlConnection.State == ConnectionState.Closed)
                    {
                        l_cSqlConnection.Open();
                    }
                    if (p_sTableNameOrSqlQuery.ToLower().Contains("select"))
                    {
                        using (SqlCommand l_cSqlCommand = new SqlCommand(p_sTableNameOrSqlQuery, l_cSqlConnection))
                        {
                            using (SqlDataAdapter l_cSqlDataAdapter = new SqlDataAdapter(l_cSqlCommand))
                            {
                                l_cSqlDataAdapter.Fill(l_cDataTable);
                            }
                        }
                    }
                    else
                    {
                        using (SqlCommand l_cSqlCommand = new SqlCommand("SELECT * FROM " + p_sTableNameOrSqlQuery + " WITH(NOLOCK)", l_cSqlConnection))
                        {
                            using (SqlDataAdapter l_cSqlDataAdapter = new SqlDataAdapter(l_cSqlCommand))
                            {
                                l_cSqlDataAdapter.Fill(l_cDataTable);
                            }
                        }
                    }
                }
            }
            return l_cDataTable;
        }
    }
}
