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
    public class ReksadanaInstrumentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[ReksadanaInstrument] " +
                            "([ReksadanaInstrumentPK],[HistoryPK],[Status],[Date],[ReksadanaPK],[InstrumentPK],[ExpiredDate]," + 
                            "[InterestPercent],[AvgPrice],[BuyVolume],[SellVolume1],[SellVolume2],[SellVolume3],[SellVolume4],[SellVolume5],[SellVolume6],[SellPrice1],[SellPrice2]," +
                            "[SellPrice3],[SellPrice4],[SellPrice5],[SellPrice6],[EndVolume],[Totaldays],";
        string _paramaterCommand = "@Date,@ReksadanaPK,@InstrumentPK,@ExpiredDate,@InterestPercent," + 
                                   "@AvgPrice,@BuyVolume,@SellVolume1,@SellVolume2,@SellVolume3,@SellVolume4,@SellVolume5,@SellVolume6,@SellPrice1,@SellPrice2,@SellPrice3,@SellPrice4," +
                                   "@SellPrice5,@SellPrice6,@EndVolume,@Totaldays,";

        //2
        private ReksadanaInstrument setReksadanaInstrument(SqlDataReader dr)
        {
            ReksadanaInstrument M_ReksadanaInstrument = new ReksadanaInstrument();
            M_ReksadanaInstrument.ReksadanaInstrumentPK = Convert.ToInt32(dr["ReksadanaInstrumentPK"]);
            M_ReksadanaInstrument.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_ReksadanaInstrument.Status = Convert.ToInt32(dr["Status"]);
            M_ReksadanaInstrument.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_ReksadanaInstrument.Notes = Convert.ToString(dr["Notes"]);
            M_ReksadanaInstrument.Date = dr["Date"].ToString();
            M_ReksadanaInstrument.ReksadanaPK = Convert.ToInt32(dr["ReksadanaPK"]);
            M_ReksadanaInstrument.ReksadanaID = Convert.ToString(dr["ReksadanaID"]);
            M_ReksadanaInstrument.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_ReksadanaInstrument.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_ReksadanaInstrument.ExpiredDate = dr["ExpiredDate"].ToString();

            M_ReksadanaInstrument.InterestPercent = Convert.ToDecimal(dr["InterestPercent"]);
            M_ReksadanaInstrument.AvgPrice = Convert.ToDecimal(dr["AvgPrice"]);
            M_ReksadanaInstrument.BuyVolume = Convert.ToDecimal(dr["BuyVolume"]);
            M_ReksadanaInstrument.SellVolume1 = Convert.ToDecimal(dr["SellVolume1"]);
            M_ReksadanaInstrument.SellVolume2 = Convert.ToDecimal(dr["SellVolume2"]);
            M_ReksadanaInstrument.SellVolume3 = Convert.ToDecimal(dr["SellVolume3"]);
            M_ReksadanaInstrument.SellVolume4 = Convert.ToDecimal(dr["SellVolume4"]);
            M_ReksadanaInstrument.SellVolume5 = Convert.ToDecimal(dr["SellVolume5"]);
            M_ReksadanaInstrument.SellVolume6 = Convert.ToDecimal(dr["SellVolume6"]);
            M_ReksadanaInstrument.SellPrice1 = Convert.ToDecimal(dr["SellPrice1"]);
            M_ReksadanaInstrument.SellPrice2 = Convert.ToDecimal(dr["SellPrice2"]);
            M_ReksadanaInstrument.SellPrice3 = Convert.ToDecimal(dr["SellPrice3"]);
            M_ReksadanaInstrument.SellPrice4 = Convert.ToDecimal(dr["SellPrice4"]);
            M_ReksadanaInstrument.SellPrice5 = Convert.ToDecimal(dr["SellPrice5"]);
            M_ReksadanaInstrument.SellPrice6 = Convert.ToDecimal(dr["SellPrice6"]);
            M_ReksadanaInstrument.EndVolume = Convert.ToDecimal(dr["EndVolume"]);
            M_ReksadanaInstrument.Totaldays = Convert.ToInt32(dr["Totaldays"]);

            M_ReksadanaInstrument.EntryUsersID = dr["EntryUsersID"].ToString();
            M_ReksadanaInstrument.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_ReksadanaInstrument.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_ReksadanaInstrument.VoidUsersID = dr["VoidUsersID"].ToString();
            M_ReksadanaInstrument.EntryTime = dr["EntryTime"].ToString();
            M_ReksadanaInstrument.UpdateTime = dr["UpdateTime"].ToString();
            M_ReksadanaInstrument.ApprovedTime = dr["ApprovedTime"].ToString();
            M_ReksadanaInstrument.VoidTime = dr["VoidTime"].ToString();
            M_ReksadanaInstrument.DBUserID = dr["DBUserID"].ToString();
            M_ReksadanaInstrument.DBTerminalID = dr["DBTerminalID"].ToString();
            M_ReksadanaInstrument.LastUpdate = dr["LastUpdate"].ToString();
            M_ReksadanaInstrument.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_ReksadanaInstrument;
        }

        public List<ReksadanaInstrument> ReksadanaInstrument_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReksadanaInstrument> L_ReksadanaInstrument = new List<ReksadanaInstrument>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID ReksadanaID, C.ID InstrumentID,* from ReksadanaInstrument  A left join
                            Instrument B on A.ReksadanaPK = B.InstrumentPK and B.Status in(1,2) left join 
                            Instrument C on A.InstrumentPK = C.InstrumentPK and  C.Status in(1,2) left join
							InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.InstrumentTypePK in(2,5,14) left join 
							InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.InstrumentTypePK in(4) 
                            where a.status = @status order by ReksadanaInstrumentPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID ReksadanaID, C.ID InstrumentID,* from ReksadanaInstrument  A left join
                            Instrument B on A.ReksadanaPK = B.InstrumentPK and B.Status in(1,2) left join 
                            Instrument C on A.InstrumentPK = C.InstrumentPK and  C.Status in(1,2) left join
							InstrumentType D on B.InstrumentTypePK = D.InstrumentTypePK and D.InstrumentTypePK in(2,5,14) left join 
							InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.InstrumentTypePK in(4) order by ReksadanaInstrumentPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ReksadanaInstrument.Add(setReksadanaInstrument(dr));
                                }
                            }
                            return L_ReksadanaInstrument;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ReksadanaInstrument_Add(ReksadanaInstrument _ReksadanaInstrument, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(ReksadanaInstrumentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From ReksadanaInstrument";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _ReksadanaInstrument.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(ReksadanaInstrumentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From ReksadanaInstrument";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _ReksadanaInstrument.Date);
                        cmd.Parameters.AddWithValue("@ReksadanaPK", _ReksadanaInstrument.ReksadanaPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _ReksadanaInstrument.InstrumentPK);
                        cmd.Parameters.AddWithValue("@ExpiredDate", _ReksadanaInstrument.ExpiredDate);

                        cmd.Parameters.AddWithValue("@InterestPercent", _ReksadanaInstrument.InterestPercent);
                        cmd.Parameters.AddWithValue("@AvgPrice", _ReksadanaInstrument.AvgPrice);
                        cmd.Parameters.AddWithValue("@BuyVolume", _ReksadanaInstrument.BuyVolume);
                        cmd.Parameters.AddWithValue("@SellVolume1", _ReksadanaInstrument.SellVolume1);
                        cmd.Parameters.AddWithValue("@SellVolume2", _ReksadanaInstrument.SellVolume2);
                        cmd.Parameters.AddWithValue("@SellVolume3", _ReksadanaInstrument.SellVolume3);
                        cmd.Parameters.AddWithValue("@SellVolume4", _ReksadanaInstrument.SellVolume4);
                        cmd.Parameters.AddWithValue("@SellVolume5", _ReksadanaInstrument.SellVolume5);
                        cmd.Parameters.AddWithValue("@SellVolume6", _ReksadanaInstrument.SellVolume6);
                        cmd.Parameters.AddWithValue("@SellPrice1", _ReksadanaInstrument.SellPrice1);
                        cmd.Parameters.AddWithValue("@SellPrice2", _ReksadanaInstrument.SellPrice2);
                        cmd.Parameters.AddWithValue("@SellPrice3", _ReksadanaInstrument.SellPrice3);
                        cmd.Parameters.AddWithValue("@SellPrice4", _ReksadanaInstrument.SellPrice4);
                        cmd.Parameters.AddWithValue("@SellPrice5", _ReksadanaInstrument.SellPrice5);
                        cmd.Parameters.AddWithValue("@SellPrice6", _ReksadanaInstrument.SellPrice6);
                        cmd.Parameters.AddWithValue("@EndVolume", _ReksadanaInstrument.EndVolume);
                        cmd.Parameters.AddWithValue("@Totaldays", _ReksadanaInstrument.Totaldays);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _ReksadanaInstrument.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ReksadanaInstrument");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ReksadanaInstrument_Update(ReksadanaInstrument _ReksadanaInstrument, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_ReksadanaInstrument.ReksadanaInstrumentPK, _ReksadanaInstrument.HistoryPK, "ReksadanaInstrument");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ReksadanaInstrument set status=2, Notes=@Notes,Date=@Date,ReksadanaPK=@ReksadanaPK,InstrumentPK=@InstrumentPK,ExpiredDate=@ExpiredDate," +
                                "InterestPercent=@InterestPercent,AvgPrice=@AvgPrice,BuyVolume=@BuyVolume,SellVolume1=@SellVolume1,SellVolume2=@SellVolume2,SellVolume3=@SellVolume3,SellVolume4=@SellVolume4,SellVolume5=@SellVolume5,SellVolume6=@SellVolume6," +
                                "SellPrice1=@SellPrice1,SellPrice2=@SellPrice2,SellPrice3=@SellPrice3,SellPrice4=@SellPrice4,SellPrice5=@SellPrice5,SellPrice6=@SellPrice6,EndVolume=@EndVolume,Totaldays=@Totaldays," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where ReksadanaInstrumentPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _ReksadanaInstrument.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                            cmd.Parameters.AddWithValue("@Notes", _ReksadanaInstrument.Notes);
                            cmd.Parameters.AddWithValue("@Date", _ReksadanaInstrument.Date);
                            cmd.Parameters.AddWithValue("@ReksadanaPK", _ReksadanaInstrument.ReksadanaPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _ReksadanaInstrument.InstrumentPK);
                            cmd.Parameters.AddWithValue("@ExpiredDate", _ReksadanaInstrument.ExpiredDate);

                            cmd.Parameters.AddWithValue("@InterestPercent", _ReksadanaInstrument.InterestPercent);
                            cmd.Parameters.AddWithValue("@AvgPrice", _ReksadanaInstrument.AvgPrice);
                            cmd.Parameters.AddWithValue("@BuyVolume", _ReksadanaInstrument.BuyVolume);
                            cmd.Parameters.AddWithValue("@SellVolume1", _ReksadanaInstrument.SellVolume1);
                            cmd.Parameters.AddWithValue("@SellVolume2", _ReksadanaInstrument.SellVolume2);
                            cmd.Parameters.AddWithValue("@SellVolume3", _ReksadanaInstrument.SellVolume3);
                            cmd.Parameters.AddWithValue("@SellVolume4", _ReksadanaInstrument.SellVolume4);
                            cmd.Parameters.AddWithValue("@SellVolume5", _ReksadanaInstrument.SellVolume5);
                            cmd.Parameters.AddWithValue("@SellVolume6", _ReksadanaInstrument.SellVolume6);
                            cmd.Parameters.AddWithValue("@SellPrice1", _ReksadanaInstrument.SellPrice1);
                            cmd.Parameters.AddWithValue("@SellPrice2", _ReksadanaInstrument.SellPrice2);
                            cmd.Parameters.AddWithValue("@SellPrice3", _ReksadanaInstrument.SellPrice3);
                            cmd.Parameters.AddWithValue("@SellPrice4", _ReksadanaInstrument.SellPrice4);
                            cmd.Parameters.AddWithValue("@SellPrice5", _ReksadanaInstrument.SellPrice5);
                            cmd.Parameters.AddWithValue("@SellPrice6", _ReksadanaInstrument.SellPrice6);
                            cmd.Parameters.AddWithValue("@EndVolume", _ReksadanaInstrument.EndVolume);
                            cmd.Parameters.AddWithValue("@Totaldays", _ReksadanaInstrument.Totaldays);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _ReksadanaInstrument.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _ReksadanaInstrument.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ReksadanaInstrument set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ReksadanaInstrumentPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _ReksadanaInstrument.EntryUsersID);
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
                                cmd.CommandText = "Update ReksadanaInstrument set Notes=@Notes,Date=@Date,ReksadanaPK=@ReksadanaPK,InstrumentPK=@InstrumentPK,ExpiredDate=@ExpiredDate," +
                                    "InterestPercent=@InterestPercent,AvgPrice=@AvgPrice,BuyVolume=@BuyVolume,SellVolume1=@SellVolume1,SellVolume2=@SellVolume2,SellVolume3=@SellVolume3,SellVolume4=@SellVolume4,SellVolume5=@SellVolume5,SellVolume6=@SellVolume6," +
                                    "SellPrice1=@SellPrice1,SellPrice2=@SellPrice2,SellPrice3=@SellPrice3,SellPrice4=@SellPrice4,SellPrice5=@SellPrice5,SellPrice6=@SellPrice6,EndVolume=@EndVolume,Totaldays=@Totaldays," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where ReksadanaInstrumentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _ReksadanaInstrument.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                                cmd.Parameters.AddWithValue("@Notes", _ReksadanaInstrument.Notes);
                                cmd.Parameters.AddWithValue("@Date", _ReksadanaInstrument.Date);
                                cmd.Parameters.AddWithValue("@ReksadanaPK", _ReksadanaInstrument.ReksadanaPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _ReksadanaInstrument.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ExpiredDate", _ReksadanaInstrument.ExpiredDate);

                                cmd.Parameters.AddWithValue("@InterestPercent", _ReksadanaInstrument.InterestPercent);
                                cmd.Parameters.AddWithValue("@AvgPrice", _ReksadanaInstrument.AvgPrice);
                                cmd.Parameters.AddWithValue("@BuyVolume", _ReksadanaInstrument.BuyVolume);
                                cmd.Parameters.AddWithValue("@SellVolume1", _ReksadanaInstrument.SellVolume1);
                                cmd.Parameters.AddWithValue("@SellVolume2", _ReksadanaInstrument.SellVolume2);
                                cmd.Parameters.AddWithValue("@SellVolume3", _ReksadanaInstrument.SellVolume3);
                                cmd.Parameters.AddWithValue("@SellVolume4", _ReksadanaInstrument.SellVolume4);
                                cmd.Parameters.AddWithValue("@SellVolume5", _ReksadanaInstrument.SellVolume5);
                                cmd.Parameters.AddWithValue("@SellVolume6", _ReksadanaInstrument.SellVolume6);
                                cmd.Parameters.AddWithValue("@SellPrice1", _ReksadanaInstrument.SellPrice1);
                                cmd.Parameters.AddWithValue("@SellPrice2", _ReksadanaInstrument.SellPrice2);
                                cmd.Parameters.AddWithValue("@SellPrice3", _ReksadanaInstrument.SellPrice3);
                                cmd.Parameters.AddWithValue("@SellPrice4", _ReksadanaInstrument.SellPrice4);
                                cmd.Parameters.AddWithValue("@SellPrice5", _ReksadanaInstrument.SellPrice5);
                                cmd.Parameters.AddWithValue("@SellPrice6", _ReksadanaInstrument.SellPrice6);
                                cmd.Parameters.AddWithValue("@EndVolume", _ReksadanaInstrument.EndVolume);
                                cmd.Parameters.AddWithValue("@Totaldays", _ReksadanaInstrument.Totaldays);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _ReksadanaInstrument.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_ReksadanaInstrument.ReksadanaInstrumentPK, "ReksadanaInstrument");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ReksadanaInstrument where ReksadanaInstrumentPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _ReksadanaInstrument.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _ReksadanaInstrument.Date);
                                cmd.Parameters.AddWithValue("@ReksadanaPK", _ReksadanaInstrument.ReksadanaPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _ReksadanaInstrument.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ExpiredDate", _ReksadanaInstrument.ExpiredDate);

                                cmd.Parameters.AddWithValue("@InterestPercent", _ReksadanaInstrument.InterestPercent);
                                cmd.Parameters.AddWithValue("@AvgPrice", _ReksadanaInstrument.AvgPrice);
                                cmd.Parameters.AddWithValue("@BuyVolume", _ReksadanaInstrument.BuyVolume);
                                cmd.Parameters.AddWithValue("@SellVolume1", _ReksadanaInstrument.SellVolume1);
                                cmd.Parameters.AddWithValue("@SellVolume2", _ReksadanaInstrument.SellVolume2);
                                cmd.Parameters.AddWithValue("@SellVolume3", _ReksadanaInstrument.SellVolume3);
                                cmd.Parameters.AddWithValue("@SellVolume4", _ReksadanaInstrument.SellVolume4);
                                cmd.Parameters.AddWithValue("@SellVolume5", _ReksadanaInstrument.SellVolume5);
                                cmd.Parameters.AddWithValue("@SellVolume6", _ReksadanaInstrument.SellVolume6);
                                cmd.Parameters.AddWithValue("@SellPrice1", _ReksadanaInstrument.SellPrice1);
                                cmd.Parameters.AddWithValue("@SellPrice2", _ReksadanaInstrument.SellPrice2);
                                cmd.Parameters.AddWithValue("@SellPrice3", _ReksadanaInstrument.SellPrice3);
                                cmd.Parameters.AddWithValue("@SellPrice4", _ReksadanaInstrument.SellPrice4);
                                cmd.Parameters.AddWithValue("@SellPrice5", _ReksadanaInstrument.SellPrice5);
                                cmd.Parameters.AddWithValue("@SellPrice6", _ReksadanaInstrument.SellPrice6);
                                cmd.Parameters.AddWithValue("@EndVolume", _ReksadanaInstrument.EndVolume);
                                cmd.Parameters.AddWithValue("@Totaldays", _ReksadanaInstrument.Totaldays);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _ReksadanaInstrument.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ReksadanaInstrument set status= 4,Notes=@Notes," +
                                    "LastUpdate=LastUpdate where ReksadanaInstrumentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _ReksadanaInstrument.Notes);
                                cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _ReksadanaInstrument.HistoryPK);
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

        public void ReksadanaInstrument_Approved(ReksadanaInstrument _ReksadanaInstrument)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ReksadanaInstrument set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where ReksadanaInstrumentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ReksadanaInstrument.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _ReksadanaInstrument.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ReksadanaInstrument set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ReksadanaInstrumentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ReksadanaInstrument.ApprovedUsersID);
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

        public void ReksadanaInstrument_Reject(ReksadanaInstrument _ReksadanaInstrument)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ReksadanaInstrument set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where ReksadanaInstrumentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ReksadanaInstrument.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ReksadanaInstrument.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ReksadanaInstrument set status= 2,LastUpdate=@LastUpdate where ReksadanaInstrumentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
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

        public void ReksadanaInstrument_Void(ReksadanaInstrument _ReksadanaInstrument)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ReksadanaInstrument set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where ReksadanaInstrumentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ReksadanaInstrument.ReksadanaInstrumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ReksadanaInstrument.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ReksadanaInstrument.VoidUsersID);
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