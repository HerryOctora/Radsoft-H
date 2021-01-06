using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;


namespace RFSRepository
{
    public class CurrencyReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[Currency] " +
                            "([CurrencyPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private Currency setCurrency(SqlDataReader dr)
        {
            Currency M_Currency = new Currency();
            M_Currency.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_Currency.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Currency.Status = Convert.ToInt32(dr["Status"]);
            M_Currency.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Currency.Notes = Convert.ToString(dr["Notes"]);
            M_Currency.ID = dr["ID"].ToString();
            M_Currency.Name = dr["Name"].ToString();
            M_Currency.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Currency.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Currency.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Currency.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Currency.EntryTime = dr["EntryTime"].ToString();
            M_Currency.UpdateTime = dr["UpdateTime"].ToString();
            M_Currency.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Currency.VoidTime = dr["VoidTime"].ToString();
            M_Currency.DBUserID = dr["DBUserID"].ToString();
            M_Currency.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Currency.LastUpdate = dr["LastUpdate"].ToString();
            M_Currency.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Currency;
        }

        public List<Currency> Currency_Select(int _status)
        {
            
           try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Currency> L_Currency = new List<Currency>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Currency where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Currency order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Currency.Add(setCurrency(dr));
                                }
                            }
                            return L_Currency;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
   
        }

        public int Currency_Add(Currency _currency, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(CurrencyPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From Currency";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _currency.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(CurrencyPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From Currency";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _currency.ID);
                        cmd.Parameters.AddWithValue("@Name", _currency.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _currency.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Currency");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int Currency_Update(Currency _currency, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_currency.CurrencyPK, _currency.HistoryPK, "Currency");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Currency set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where CurrencyPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _currency.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                            cmd.Parameters.AddWithValue("@ID", _currency.ID);
                            cmd.Parameters.AddWithValue("@Notes", _currency.Notes);
                            cmd.Parameters.AddWithValue("@Name", _currency.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _currency.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _currency.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Currency set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CurrencyPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _currency.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        return 0;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Currency set  Notes=@Notes,ID=@ID,Name=@Name," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where CurrencyPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _currency.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                                cmd.Parameters.AddWithValue("@ID", _currency.ID);
                                cmd.Parameters.AddWithValue("@Notes", _currency.Notes);
                                cmd.Parameters.AddWithValue("@Name", _currency.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _currency.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_currency.CurrencyPK, "Currency");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Currency where CurrencyPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _currency.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _currency.ID);
                                cmd.Parameters.AddWithValue("@Name", _currency.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _currency.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Currency set status= 4,Notes=@Notes,"+
                                    "lastupdate=@lastupdate where CurrencyPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _currency.Notes);
                                cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _currency.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return _newHisPK;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Currency_Approved(Currency _currency)
        {
           try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Currency set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where CurrencyPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                        cmd.Parameters.AddWithValue("@historyPK", _currency.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _currency.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Currency set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CurrencyPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _currency.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Currency_Reject(Currency _currency)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Currency set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CurrencyPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                        cmd.Parameters.AddWithValue("@historyPK", _currency.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _currency.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Currency set status= 2,lastupdate=@lastupdate where CurrencyPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Currency_Void(Currency _currency)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Currency set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CurrencyPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _currency.CurrencyPK);
                        cmd.Parameters.AddWithValue("@historyPK", _currency.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _currency.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<CurrencyCombo> Currency_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CurrencyCombo> L_Currency = new List<CurrencyCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CurrencyPK,ID + ' - ' + Name as ID, Name FROM [Currency]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CurrencyCombo M_Currency = new CurrencyCombo();
                                    M_Currency.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
                                    M_Currency.ID = Convert.ToString(dr["ID"]);
                                    M_Currency.Name = Convert.ToString(dr["Name"]);
                                    L_Currency.Add(M_Currency);
                                }
                            }
                            return L_Currency;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<CurrencyCombo> Currency_ComboRpt()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CurrencyCombo> L_Currency = new List<CurrencyCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CurrencyPK,ID + ' - ' + Name as ID, Name FROM [Currency]  where status = 2 Union All Select 0 CurrencyPK,'ALL' ID,'' Name order by CurrencyPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CurrencyCombo M_Currency = new CurrencyCombo();
                                    M_Currency.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
                                    M_Currency.ID = Convert.ToString(dr["ID"]);
                                    M_Currency.Name = Convert.ToString(dr["Name"]);
                                    L_Currency.Add(M_Currency);
                                }
                            }
                            return L_Currency;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    }
}