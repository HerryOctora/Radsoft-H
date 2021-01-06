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
using System.Data;
using OfficeOpenXml.Drawing;
using System.Data.OleDb;



namespace RFSRepository
{
    public class FiFoBondPositionReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FiFoBondPosition] " +
                            "([FiFoBondPositionPK],[HistoryPK],[Status],[FundPK],[InstrumentPK],[AcqPrice],[AcqVolume],[AcqDate],[CutoffDate],";
        string _paramaterCommand = "@FundPK,@InstrumentPK,@AcqPrice,@AcqVolume,@AcqDate,@CutoffDate,";

        //2
        private FiFoBondPosition setFiFoBondPosition(SqlDataReader dr)
        {
            FiFoBondPosition M_FiFoBondPosition = new FiFoBondPosition();
            M_FiFoBondPosition.FiFoBondPositionPK = Convert.ToInt32(dr["FiFoBondPositionPK"]);
            M_FiFoBondPosition.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FiFoBondPosition.Status = Convert.ToInt32(dr["Status"]);
            M_FiFoBondPosition.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FiFoBondPosition.Notes = Convert.ToString(dr["Notes"]);
            M_FiFoBondPosition.AcqDate = Convert.ToString(dr["AcqDate"]);
            M_FiFoBondPosition.CutoffDate = Convert.ToString(dr["CutoffDate"]);
            M_FiFoBondPosition.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FiFoBondPosition.FundID = Convert.ToString(dr["FundID"]);
            M_FiFoBondPosition.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_FiFoBondPosition.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_FiFoBondPosition.AcqPrice = Convert.ToDecimal(dr["AcqPrice"]);
            M_FiFoBondPosition.AcqVolume = Convert.ToDecimal(dr["AcqVolume"]);
            M_FiFoBondPosition.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FiFoBondPosition.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FiFoBondPosition.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FiFoBondPosition.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FiFoBondPosition.EntryTime = dr["EntryTime"].ToString();
            M_FiFoBondPosition.UpdateTime = dr["UpdateTime"].ToString();
            M_FiFoBondPosition.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FiFoBondPosition.VoidTime = dr["VoidTime"].ToString();
            M_FiFoBondPosition.DBUserID = dr["DBUserID"].ToString();
            M_FiFoBondPosition.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FiFoBondPosition.LastUpdate = dr["LastUpdate"].ToString();
            M_FiFoBondPosition.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FiFoBondPosition;
        }

        public List<FiFoBondPosition> FiFoBondPosition_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FiFoBondPosition> L_FiFoBondPosition = new List<FiFoBondPosition>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when FBP.status=1 then 'PENDING' else Case When FBP.status = 2 then 'APPROVED' else 
                                                Case when FBP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.ID FundID,I.ID InstrumentID,FBP.* 
                                                from FiFoBondPosition FBP 
                                                left join Fund F on F.FundPK = FBP.FundPK and F.Status in (1,2)
                                                left join Instrument I on I.InstrumentPK = FBP.InstrumentPK and I.Status in (1,2)
                                                where FBP.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when FBP.status=1 then 'PENDING' else Case When FBP.status = 2 then 'APPROVED' else 
                                                Case when FBP.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.ID FundID,I.ID InstrumentID,FBP.* 
                                                from FiFoBondPosition FBP 
                                                left join Fund F on F.FundPK = FBP.FundPK and F.Status in (1,2)
                                                left join Instrument I on I.InstrumentPK = FBP.InstrumentPK and I.Status in (1,2)
                                                order by AcqDate";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FiFoBondPosition.Add(setFiFoBondPosition(dr));
                                }
                            }
                            return L_FiFoBondPosition;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FiFoBondPosition_Add(FiFoBondPosition _FiFoBondPosition, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(FiFoBondPositionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FiFoBondPosition";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FiFoBondPosition.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FiFoBondPositionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FiFoBondPosition";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@AcqDate", _FiFoBondPosition.AcqDate);
                        cmd.Parameters.AddWithValue("@CutoffDate", _FiFoBondPosition.CutoffDate);
                        cmd.Parameters.AddWithValue("@FundPK", _FiFoBondPosition.FundPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _FiFoBondPosition.InstrumentPK);
                        cmd.Parameters.AddWithValue("@AcqPrice", _FiFoBondPosition.AcqPrice);
                        cmd.Parameters.AddWithValue("@AcqVolume", _FiFoBondPosition.AcqVolume);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FiFoBondPosition.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FiFoBondPosition");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FiFoBondPosition_Update(FiFoBondPosition _FiFoBondPosition, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FiFoBondPosition.FiFoBondPositionPK, _FiFoBondPosition.HistoryPK, "FiFoBondPosition");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update FiFoBondPosition set status=2, Notes=@Notes,AcqDate=@AcqDate,FundPK=@FundPK,InstrumentPK=@InstrumentPK,CutoffDate = @CutoffDate,
                                                AcqPrice=@AcqPrice,AcqVolume=@AcqVolume,ApprovedUsersID=@ApprovedUsersID,
                                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate
                                                where FiFoBondPositionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FiFoBondPosition.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                            cmd.Parameters.AddWithValue("@Notes", _FiFoBondPosition.Notes);

                            cmd.Parameters.AddWithValue("@AcqDate", _FiFoBondPosition.AcqDate);
                            cmd.Parameters.AddWithValue("@CutoffDate", _FiFoBondPosition.CutoffDate);
                            cmd.Parameters.AddWithValue("@FundPK", _FiFoBondPosition.FundPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _FiFoBondPosition.InstrumentPK);
                            cmd.Parameters.AddWithValue("@AcqPrice", _FiFoBondPosition.AcqPrice);
                            cmd.Parameters.AddWithValue("@AcqVolume", _FiFoBondPosition.AcqVolume);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FiFoBondPosition.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FiFoBondPosition.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FiFoBondPosition set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FiFoBondPositionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FiFoBondPosition.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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
                                cmd.CommandText = @"Update FiFoBondPosition set status=2, Notes=@Notes,AcqDate=@AcqDate,FundPK=@FundPK,InstrumentPK=@InstrumentPK,CutoffDate = @CutoffDate,
                                                    AcqPrice=@AcqPrice,AcqVolume=@AcqVolume,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate
                                                    where FiFoBondPositionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FiFoBondPosition.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                                cmd.Parameters.AddWithValue("@Notes", _FiFoBondPosition.Notes);

                                cmd.Parameters.AddWithValue("@AcqDate", _FiFoBondPosition.AcqDate);
                                cmd.Parameters.AddWithValue("@CutoffDate", _FiFoBondPosition.CutoffDate);
                                cmd.Parameters.AddWithValue("@FundPK", _FiFoBondPosition.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _FiFoBondPosition.InstrumentPK);
                                cmd.Parameters.AddWithValue("@AcqPrice", _FiFoBondPosition.AcqPrice);
                                cmd.Parameters.AddWithValue("@AcqVolume", _FiFoBondPosition.AcqVolume);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FiFoBondPosition.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FiFoBondPosition.FiFoBondPositionPK, "FiFoBondPosition");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FiFoBondPosition where FiFoBondPositionPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FiFoBondPosition.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@AcqDate", _FiFoBondPosition.AcqDate);
                                cmd.Parameters.AddWithValue("@CutoffDate", _FiFoBondPosition.CutoffDate);
                                cmd.Parameters.AddWithValue("@FundPK", _FiFoBondPosition.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _FiFoBondPosition.InstrumentPK);
                                cmd.Parameters.AddWithValue("@AcqPrice", _FiFoBondPosition.AcqPrice);
                                cmd.Parameters.AddWithValue("@AcqVolume", _FiFoBondPosition.AcqVolume);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FiFoBondPosition.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FiFoBondPosition set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where FiFoBondPositionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FiFoBondPosition.FiFoBondPositionPK);
                                cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FiFoBondPosition.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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

        public void FiFoBondPosition_Approved(FiFoBondPosition _FiFoBondPosition)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FiFoBondPosition set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FiFoBondPositionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FiFoBondPosition.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FiFoBondPosition.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FiFoBondPosition set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FiFoBondPositionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FiFoBondPosition.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FiFoBondPosition_Reject(FiFoBondPosition _FiFoBondPosition)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FiFoBondPosition set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FiFoBondPositionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FiFoBondPosition.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FiFoBondPosition.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FiFoBondPosition set status= 2,LastUpdate=@LastUpdate where FiFoBondPositionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FiFoBondPosition_Void(FiFoBondPosition _FiFoBondPosition)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FiFoBondPosition set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FiFoBondPositionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FiFoBondPosition.FiFoBondPositionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FiFoBondPosition.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FiFoBondPosition.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<setFifoBondPosition> FiFoBondPosition_Combo(int _FundPK, int _InstrumentPK)
        {

            try
            {
                string _paramFund, _paramInstrument;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<setFifoBondPosition> L_FifoBondPosition = new List<setFifoBondPosition>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_FundPK != 0)
                            _paramFund = " and A.FundPK = " + _FundPK;
                        else
                            _paramFund = "";

                        if (_InstrumentPK != 0)
                            _paramInstrument = " and A.InstrumentPK = " + _InstrumentPK;
                        else
                            _paramInstrument = "";

                        cmd.CommandText = @"
                            select B.ID FundName,C.ID Instrument,AcqDate,AcqPrice,sum(A.RemaningVolume) Volume,FiFoBondPositionPK, statusAS from (
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,case when Remainingvolume is null then AcqVolume else RemainingVolume end RemaningVolume, FifoBondPositionPK,1 statusAS from FiFoBondPosition where (RemainingVolume is null and InvestmentPK = 0) or RemainingVolume != 0
                            union all
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,RemainingVolume, InvestmentBuyPK,2 from FifoBondPositionTemp where RemainingVolume != 0
                            ) A 
                            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
							where 1 = 1 " + _paramFund + _paramInstrument + @"
                            group by B.ID,C.ID,AcqDate,AcqPrice,A.FiFoBondPositionPK,statusAS
							order by AcqDate,FiFoBondPositionPK asc
                            ";

                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    setFifoBondPosition M_FifoBondPosition = new setFifoBondPosition();
                                    M_FifoBondPosition.FundName = Convert.ToString(dr["FundName"]);
                                    M_FifoBondPosition.Instrument = Convert.ToString(dr["Instrument"]);
                                    M_FifoBondPosition.Volume = Convert.ToDecimal(dr["Volume"]);
                                    M_FifoBondPosition.AcqPrice = Convert.ToDecimal(dr["AcqPrice"]);
                                    M_FifoBondPosition.AcqDate = Convert.ToDateTime(dr["AcqDate"]);
                                    L_FifoBondPosition.Add(M_FifoBondPosition);
                                }

                            }
                            return L_FifoBondPosition;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<setHistorical> Historical_Combo(int _FundPK, DateTime _DateFrom)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<setHistorical> L_Historical = new List<setHistorical>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_FundPK == 0)
                        {
                            cmd.CommandText = @"
                   select Date,FundPK,FundID,InstrumentID,AcqDate, AcqPrice, AcqVolume from FifoBondHistorical
                   where Date= @Date
                            ";
                        }
                        else
                        {
                            cmd.CommandText = @"
                  select Date,FundPK,FundID,InstrumentID,AcqDate, AcqPrice, AcqVolume from FifoBondHistorical
                  where Date = @Date and FundPK = @FundPK
                            ";
                            cmd.Parameters.AddWithValue("@FundPK", _FundPK);
                        }
                        cmd.Parameters.AddWithValue("@Date", _DateFrom);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    setHistorical M_Historical = new setHistorical();
                                    M_Historical.Date = Convert.ToDateTime(dr["Date"]);
                                    M_Historical.FundID = Convert.ToString(dr["FundID"]);
                                    M_Historical.Instrument = Convert.ToString(dr["InstrumentID"]);
                                    M_Historical.AcqVolume = Convert.ToDecimal(dr["AcqVolume"]);
                                    M_Historical.AcqPrice = Convert.ToDecimal(dr["AcqPrice"]);
                                    M_Historical.AcqDate = Convert.ToDateTime(dr["AcqDate"]);
                                    L_Historical.Add(M_Historical);
                                }

                            }
                            return L_Historical;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }


        public Boolean FifoBondPosition_Rpt(string _userID, int _fundPK, int _instrumentPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        if (_fundPK == 0 && _instrumentPK == 0)
                        {
                            cmd.CommandText = @"
                            select B.ID FundName,C.ID Instrument,AcqDate,AcqPrice,sum(A.RemaningVolume) Volume from (
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,case when Remainingvolume is null then AcqVolume else RemainingVolume end RemaningVolume from FiFoBondPosition where (RemainingVolume is null and InvestmentPK = 0) or RemainingVolume != 0
                            union all
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,RemainingVolume from FifoBondPositionTemp where RemainingVolume != 0
                             ) A 
                            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            group by B.ID,C.ID,AcqDate,AcqPrice
                            ";
                        }
                        else if (_fundPK != 0 && _instrumentPK == 0)
                        {
                            cmd.CommandText = @"
                            select B.ID FundName,C.ID Instrument,AcqDate,AcqPrice,sum(A.RemaningVolume) Volume from (
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,case when Remainingvolume is null then AcqVolume else RemainingVolume end RemaningVolume from FiFoBondPosition where (RemainingVolume is null and InvestmentPK = 0) or RemainingVolume != 0
                            union all
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,RemainingVolume from FifoBondPositionTemp where RemainingVolume != 0
                            ) A 
                            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            where A.FundPK = @FundPK
                            group by B.ID,C.ID,AcqDate,AcqPrice
                            ";
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        else if (_fundPK == 0 && _instrumentPK != 0)
                        {
                            cmd.CommandText = @"
                            select B.ID FundName,C.ID Instrument,AcqDate,AcqPrice,sum(A.RemaningVolume) Volume from (
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,case when Remainingvolume is null then AcqVolume else RemainingVolume end RemaningVolume from FiFoBondPosition where (RemainingVolume is null and InvestmentPK = 0) or RemainingVolume != 0
                            union all
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,RemainingVolume from FifoBondPositionTemp where RemainingVolume != 0
                            ) A 
                            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            where A.InstrumentPK = @InstrumentPK
                            group by B.ID,C.ID,AcqDate,AcqPrice
                            ";
                            cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        }
                        else
                        {
                            cmd.CommandText = @"
                            select B.ID FundName,C.ID Instrument,AcqDate,AcqPrice,sum(A.RemaningVolume) Volume from (
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,case when Remainingvolume is null then AcqVolume else RemainingVolume end RemaningVolume from FiFoBondPosition where (RemainingVolume is null and InvestmentPK = 0) or RemainingVolume != 0
                            union all
                            select FundPK,InstrumentPK,AcqDate,AcqPrice,RemainingVolume from FifoBondPositionTemp where RemainingVolume != 0
                            ) A 
                            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2) 
                            where A.FundPK = @FundPK and A.InstrumentPK = @InstrumentPK
                            group by B.ID,C.ID,AcqDate,AcqPrice
                            ";
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        }

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "FifoBondPositionRpt" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "FifoBondPositionRpt" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FifoBondPositionReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Fifo Bond Position Rpt");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<FiFoBondPosition> rList = new List<FiFoBondPosition>();
                                    while (dr0.Read())
                                    {
                                        FiFoBondPosition rSingle = new FiFoBondPosition();

                                        //select B.ID FundName,C.ID Instrument,AcqDate,AcqPrice,sum(A.RemaningVolume) Volume
                                        rSingle.FundID = Convert.ToString(dr0["FundName"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["Instrument"]);
                                        rSingle.Date = Convert.ToDateTime(dr0["AcqDate"]);
                                        rSingle.AcqPrice = Convert.ToDecimal(dr0["AcqPrice"]);
                                        rSingle.AcqVolume = Convert.ToDecimal(dr0["Volume"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByFundID =
                                         from r in rList
                                         orderby r.FundID
                                         group r by new { } into rGroup
                                         select rGroup;



                                    int incRowExcel = 1;

                                    foreach (var rsHeader in GroupByFundID)
                                    {


                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "FUND";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "Instrument";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Value = "Volume";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "Price";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Value = "Date";
                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                        //THICK BOX HEADER
                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":F" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;



                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            //area detail

                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.AcqVolume;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.AcqPrice;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Date;
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;





                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;





                                        }

                                        //_range = "A" + incRowExcel + ":F" + incRowExcel;
                                        //using (ExcelRange s = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        //{
                                        //    s.Style.Font.Size = 11;
                                        //    s.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                        //}

                                        //THICK BOX DETAIL
                                        _endRowDetail = incRowExcel - 1;
                                        //worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        //worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        //worksheet.Cells[incRowExcel, 3].Value = "Total :";
                                        //worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                        //worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                        //worksheet.Cells[incRowExcel, 4].Calculate();


                                    }

                                    incRowExcel = incRowExcel + 2;



                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 6];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();

                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&34 FIFO BOND POSITION RPT";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    return true;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }

        }




    }
}
