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
using SucorInvest.Connect;


namespace RFSRepository
{
    public class FundClientPositionEndYearReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClientPositionEndYear] " +
                            "([FundClientPositionEndYearPK],[HistoryPK],[Status],[FundClientPK],[FundPK],[PeriodPK],[AvgNav],[UnitAmount],";
        string _paramaterCommand = "@FundClientPK,@FundPK,@PeriodPK,@AvgNav,@UnitAmount,";

        //2
        private FundClientPositionEndYear setFundClientPositionEndYear(SqlDataReader dr)
        {
            FundClientPositionEndYear M_FundClientPositionEndYear = new FundClientPositionEndYear();
            M_FundClientPositionEndYear.FundClientPositionEndYearPK = Convert.ToInt32(dr["FundClientPositionEndYearPK"]);
            M_FundClientPositionEndYear.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientPositionEndYear.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientPositionEndYear.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientPositionEndYear.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientPositionEndYear.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundClientPositionEndYear.FundID = Convert.ToString(dr["FundID"]);
            M_FundClientPositionEndYear.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientPositionEndYear.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_FundClientPositionEndYear.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_FundClientPositionEndYear.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_FundClientPositionEndYear.AvgNav = Convert.ToDecimal(dr["AvgNav"]);
            M_FundClientPositionEndYear.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            M_FundClientPositionEndYear.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientPositionEndYear.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientPositionEndYear.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientPositionEndYear.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientPositionEndYear.EntryTime = dr["EntryTime"].ToString();
            M_FundClientPositionEndYear.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientPositionEndYear.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientPositionEndYear.VoidTime = dr["VoidTime"].ToString();
            M_FundClientPositionEndYear.DBUserID = dr["DBUserID"].ToString();
            M_FundClientPositionEndYear.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientPositionEndYear.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientPositionEndYear.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundClientPositionEndYear;
        }


        public List<FundClientPositionEndYear> FundClientPositionEndYear_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientPositionEndYear> L_FundClientPositionEndYear = new List<FundClientPositionEndYear>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                              @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundID, C.ID FundClientID, D.ID PeriodID,
                                A.* from FundClientPositionEndYear A left join 
                              Fund B on A.FundPK = B.FundPK and B.status = 2 left join 
							  FundClient C on A.fundclientpk = c.fundclientpk and c.status in(1,2) left join
							  Period D on A.PeriodPK = D.PeriodPK and D.Status in(1,2)
                              where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                              @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundID, C.ID FundClientID, D.ID PeriodID,
                              A.* from FundClientPositionEndYear A left join 
                              Fund B on A.FundPK = B.FundPK and B.status = 2 left join 
							  FundClient C on A.fundclientpk = c.fundclientpk and c.status in(1,2) left join
							  Period D on A.PeriodPK = D.PeriodPK and D.Status in(1,2) ";
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientPositionEndYear.Add(setFundClientPositionEndYear(dr));
                                }
                            }
                            return L_FundClientPositionEndYear;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int FundClientPositionEndYear_Add(FundClientPositionEndYear _FundClientPositionEndYear, bool _havePrivillege)
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
                                 "Select isnull(max(FundClientPositionEndYearPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FundClientPositionEndYear";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientPositionEndYear.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FundClientPositionEndYearPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundClientPositionEndYear";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _FundClientPositionEndYear.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPositionEndYear.FundClientPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _FundClientPositionEndYear.PeriodPK);
                        cmd.Parameters.AddWithValue("@AvgNav", _FundClientPositionEndYear.AvgNav);
                        cmd.Parameters.AddWithValue("@UnitAmount", _FundClientPositionEndYear.UnitAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientPositionEndYear.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();
                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "FundClientPositionEndYear");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int FundClientPositionEndYear_Update(FundClientPositionEndYear _FundClientPositionEndYear, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundClientPositionEndYear.FundClientPositionEndYearPK, _FundClientPositionEndYear.HistoryPK, "FundClientPositionEndYear");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientPositionEndYear set  status=2, Notes=@Notes,Date=@Date,FundClientPK=@FundClientPK,FundPK=@FundPK,PeriodPK=@PeriodPK,AvgNav=@AvgNav,UnitAmount=@UnitAmount," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where FundClientPositionEndYearPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundClientPositionEndYear.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundClientPositionEndYear.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _FundClientPositionEndYear.FundPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPositionEndYear.FundClientPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _FundClientPositionEndYear.PeriodPK);
                            cmd.Parameters.AddWithValue("@AvgNav", _FundClientPositionEndYear.AvgNav);
                            cmd.Parameters.AddWithValue("@UnitAmount", _FundClientPositionEndYear.UnitAmount);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientPositionEndYear.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientPositionEndYear.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientPositionEndYear set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientPositionEndYearPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientPositionEndYear.EntryUsersID);
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
                                cmd.CommandText = "Update FundClientPositionEndYear set   Notes=@Notes,FundClientPK=@FundClientPK,FundPK=@FundPK,PeriodPK=@PeriodPK,AvgNav=@AvgNav,UnitAmount=@UnitAmount," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundClientPositionEndYearPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientPositionEndYear.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundClientPositionEndYear.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _FundClientPositionEndYear.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPositionEndYear.FundClientPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _FundClientPositionEndYear.PeriodPK);
                                cmd.Parameters.AddWithValue("@AvgNav", _FundClientPositionEndYear.AvgNav);
                                cmd.Parameters.AddWithValue("@UnitAmount", _FundClientPositionEndYear.UnitAmount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientPositionEndYear.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FundClientPositionEndYear.FundClientPositionEndYearPK, "FundClientPositionEndYear");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClientPositionEndYear where FundClientPositionEndYearPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientPositionEndYear.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundClientPositionEndYear.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPositionEndYear.FundClientPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _FundClientPositionEndYear.PeriodPK);
                                cmd.Parameters.AddWithValue("@AvgNav", _FundClientPositionEndYear.AvgNav);
                                cmd.Parameters.AddWithValue("@UnitAmount", _FundClientPositionEndYear.UnitAmount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientPositionEndYear.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClientPositionEndYear set status= 4,Notes=@Notes where FundClientPositionEndYearPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundClientPositionEndYear.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientPositionEndYear.HistoryPK);
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

        public void FundClientPositionEndYear_Approved(FundClientPositionEndYear _FundClientPositionEndYear)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientPositionEndYear set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where FundClientPositionEndYearPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientPositionEndYear.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientPositionEndYear.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientPositionEndYear set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientPositionEndYearPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientPositionEndYear.ApprovedUsersID);
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

        public void FundClientPositionEndYear_Reject(FundClientPositionEndYear _FundClientPositionEndYear)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientPositionEndYear set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                            "where FundClientPositionEndYearPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientPositionEndYear.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientPositionEndYear.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientPositionEndYear set status= 2,LastUpdate=@LastUpdate where FundClientPositionEndYearPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
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

        public void FundClientPositionEndYear_Void(FundClientPositionEndYear _FundClientPositionEndYear)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientPositionEndYear set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                            "where FundClientPositionEndYearPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientPositionEndYear.FundClientPositionEndYearPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientPositionEndYear.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientPositionEndYear.VoidUsersID);
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




        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

    }
}