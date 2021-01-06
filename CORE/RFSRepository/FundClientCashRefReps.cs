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
using System.Text;
using System.Threading;
using System.Data;
using System.Data.OleDb;

namespace RFSRepository
{
    public class FundClientCashRefReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClientCashRef] " +
                            "([FundClientCashRefPK],[HistoryPK],[Status],[FundClientPK],[FundPK],[FundCashRefPK],";
        string _paramaterCommand = "@FundClientPK,@FundPK,@FundCashRefPK,";

        //2
        private FundClientCashRef setFundClientCashRef(SqlDataReader dr)
        {
            FundClientCashRef M_FundClientCashRef = new FundClientCashRef();
            M_FundClientCashRef.FundClientCashRefPK = Convert.ToInt32(dr["FundClientCashRefPK"]);
            M_FundClientCashRef.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientCashRef.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientCashRef.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientCashRef.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientCashRef.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientCashRef.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_FundClientCashRef.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_FundClientCashRef.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundClientCashRef.FundID = Convert.ToString(dr["FundID"]);
            M_FundClientCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
            M_FundClientCashRef.FundCashRefID = Convert.ToString(dr["FundCashRefID"]);
            M_FundClientCashRef.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientCashRef.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientCashRef.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientCashRef.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientCashRef.EntryTime = dr["EntryTime"].ToString();
            M_FundClientCashRef.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientCashRef.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientCashRef.VoidTime = dr["VoidTime"].ToString();
            M_FundClientCashRef.DBUserID = dr["DBUserID"].ToString();
            M_FundClientCashRef.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientCashRef.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientCashRef.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundClientCashRef;
        }

        public List<FundClientCashRef> FundClientCashRef_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCashRef> L_FundClientCashRef = new List<FundClientCashRef>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when FCR.status=1 then 'PENDING' else Case When FCR.status = 2 then 'APPROVED' else Case when FCR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID FundCashRefID, FC.ID FundClientID,FC.Name FundClientName,F.ID FundID,FCR.* from FundClientCashRef FCR left join
                           FundCashRef CR on FCR.FundCashRefPK = CR.FundCashRefPK and CR.status = 2 left join 
                           FundClient FC on FCR.FundClientPK = FC.FundClientPK  and FC.status = 2 left join
                           Fund F on FCR.FundPK = F.FundPK and F.status = 2
                           where FCR.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when FCR.status=1 then 'PENDING' else Case When FCR.status = 2 then 'APPROVED' else Case when FCR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID FundCashRefID, FC.ID FundClientID,FC.Name FundClientName,F.ID FundID,FCR.* from FundClientCashRef FCR left join
                           FundCashRef CR on FCR.FundCashRefPK = CR.FundCashRefPK and CR.status = 2 left join
                           FundClient FC on FCR.FundClientPK = FC.FundClientPK  and FC.status = 2 left join
                           Fund F on FCR.FundPK = F.FundPK and F.status = 2
                           order by FundCashRefPK,FundClientPK,FundPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientCashRef.Add(setFundClientCashRef(dr));
                                }
                            }
                            return L_FundClientCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public int FundClientCashRef_Add(FundClientCashRef _FundClientCashRef, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(FundClientCashRefPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FundClientCashRef";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientCashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FundClientCashRefPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundClientCashRef";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientCashRef.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FundClientCashRef.FundPK);
                        cmd.Parameters.AddWithValue("@FundCashRefPK", _FundClientCashRef.FundCashRefPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientCashRef.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();
                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "FundClientCashRef");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public int FundClientCashRef_Update(FundClientCashRef _FundClientCashRef, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundClientCashRef.FundClientCashRefPK, _FundClientCashRef.HistoryPK, "FundClientCashRef");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update FundClientCashRef set  status=2, Notes=@Notes,FundClientPK=@FundClientPK,
                                FundPK=@FundPK,FundCashRefPK=@FundCashRefPK,ApprovedUsersID=@ApprovedUsersID,
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate
                                where FundClientCashRefPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundClientCashRef.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundClientCashRef.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _FundClientCashRef.FundClientPK);
                            cmd.Parameters.AddWithValue("@FundPK", _FundClientCashRef.FundPK);
                            cmd.Parameters.AddWithValue("@FundCashRefPK", _FundClientCashRef.FundCashRefPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientCashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientCashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientCashRef set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientCashRefPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientCashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = @"Update FundClientCashRef set Notes=@Notes,FundClientPK=@FundClientPK,
                                FundPK=@FundPK,FundCashRefPK=@FundCashRefPK,
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate
                                where FundClientCashRefPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientCashRef.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundClientCashRef.Notes);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientCashRef.FundClientPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundClientCashRef.FundPK);
                                cmd.Parameters.AddWithValue("@FundCashRefPK", _FundClientCashRef.FundCashRefPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientCashRef.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FundClientCashRef.FundClientCashRefPK, "FundClientCashRef");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClientCashRef where FundClientCashRefPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientCashRef.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientCashRef.FundClientPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundClientCashRef.FundPK);
                                cmd.Parameters.AddWithValue("@FundCashRefPK", _FundClientCashRef.FundCashRefPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientCashRef.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClientCashRef set status= 4,Notes=@Notes where FundClientCashRefPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundClientCashRef.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientCashRef.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
        public void FundClientCashRef_Approved(FundClientCashRef _FundClientCashRef)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientCashRef set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where FundClientCashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientCashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientCashRef.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientCashRef set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientCashRefPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientCashRef.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public void FundClientCashRef_Reject(FundClientCashRef _FundClientCashRef)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientCashRef set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                            "where FundClientCashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientCashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientCashRef.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientCashRef set status= 2,LastUpdate=@LastUpdate where FundClientCashRefPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FundClientCashRef_Void(FundClientCashRef _FundClientCashRef)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientCashRef set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                            "where FundClientCashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientCashRef.FundClientCashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientCashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientCashRef.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }



        public int Check_DataFundClientCashRef(int _fundPK, int _fundClientPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramFundClient = "";
                        if (_fundPK != 0)
                        {
                            _paramFund = "And FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_fundClientPK != 0)
                        {
                            _paramFundClient = "And FundClientPK = @FundClientPK ";
                        }
                        else
                        {
                            _paramFundClient = "";
                        }

                        cmd.CommandText = @"
                        if Exists
                        (select * From FundClientCashRef where  status  = 2 " + _paramFund + _paramFundClient + @" )   
                        BEGIN 
                        Select 1 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END     ";

                        if (_fundPK != 0)
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        if (_fundClientPK != 0)
                        {
                            cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);

                            }
                            return 0;
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