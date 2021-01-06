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
    public class BondRedemptionReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BondRedemption] " +
                            "([BondRedemptionPK],[HistoryPK],[Status],[FundPK],[Date],[InstrumentPK],[Balance],";
        string _paramaterCommand = "@FundPK,@Date,@InstrumentPK,@Balance,";

        //2
        private BondRedemption setBondRedemption(SqlDataReader dr)
        {
            BondRedemption M_BondRedemption = new BondRedemption();
            M_BondRedemption.BondRedemptionPK = Convert.ToInt32(dr["BondRedemptionPK"]);
            M_BondRedemption.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BondRedemption.Status = Convert.ToInt32(dr["Status"]);
            M_BondRedemption.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BondRedemption.Notes = Convert.ToString(dr["Notes"]);
            M_BondRedemption.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_BondRedemption.FundID = Convert.ToString(dr["FundID"]);
            M_BondRedemption.Date = Convert.ToDateTime(dr["Date"]);
            M_BondRedemption.Balance = Convert.ToDecimal(dr["Balance"]);
            M_BondRedemption.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_BondRedemption.InstrumentID = dr["InstrumentID"].ToString();
            M_BondRedemption.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BondRedemption.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BondRedemption.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BondRedemption.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BondRedemption.EntryTime = dr["EntryTime"].ToString();
            M_BondRedemption.UpdateTime = dr["UpdateTime"].ToString();
            M_BondRedemption.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BondRedemption.VoidTime = dr["VoidTime"].ToString();
            M_BondRedemption.DBUserID = dr["DBUserID"].ToString();
            M_BondRedemption.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BondRedemption.LastUpdate = dr["LastUpdate"].ToString();
            M_BondRedemption.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_BondRedemption;
        }

        public List<BondRedemption> BondRedemption_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<BondRedemption> L_BondRedemption = new List<BondRedemption>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.ID FundID,I.ID InstrumentID,* from BondRedemption A " +
                                " left join Fund F on A.FundPK = F.FundPK and F.Status in(1,2)" +
                                " Left join Instrument I on A.InstrumentPK = I.InstrumentPK and I.status in (1,2) " +
                                " where A.status = @status order by A.BondRedemptionPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.ID FundID,I.ID InstrumentID,* from BondRedemption A " +
                                " left join Fund F on A.FundPK = F.FundPK and F.Status in(1,2)" +
                                " Left join Instrument I on A.InstrumentPK = I.InstrumentPK and I.status in (1,2) " +
                                " order by A.BondRedemptionPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BondRedemption.Add(setBondRedemption(dr));
                                }
                            }
                            return L_BondRedemption;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BondRedemption_Add(BondRedemption _BondRedemption, bool _havePrivillege)
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
                                 "Select isnull(max(BondRedemptionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from BondRedemption";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BondRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(BondRedemptionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from BondRedemption";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _BondRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@Date", _BondRedemption.Date);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _BondRedemption.InstrumentPK);
                        cmd.Parameters.AddWithValue("@Balance", _BondRedemption.Balance);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _BondRedemption.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "BondRedemption");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BondRedemption_Update(BondRedemption _BondRedemption, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_BondRedemption.BondRedemptionPK, _BondRedemption.HistoryPK, "BondRedemption");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BondRedemption set status=2, Notes=@Notes, FundPK=@FundPK,Date=@Date,InstrumentPK=@InstrumentPK,Balance=@Balance, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where BondRedemptionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _BondRedemption.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                            cmd.Parameters.AddWithValue("@Notes", _BondRedemption.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _BondRedemption.FundPK);
                            cmd.Parameters.AddWithValue("@Date", _BondRedemption.Date);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _BondRedemption.InstrumentPK);
                            cmd.Parameters.AddWithValue("@Balance", _BondRedemption.Balance);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _BondRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BondRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BondRedemption set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BondRedemptionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _BondRedemption.EntryUsersID);
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
                                cmd.CommandText = "Update BondRedemption set  Notes=@Notes, FundPK=@FundPK,Date=@Date,InstrumentPK=@InstrumentPK,Balance=@Balance, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where BondRedemptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _BondRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                                cmd.Parameters.AddWithValue("@Notes", _BondRedemption.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _BondRedemption.FundPK);
                                cmd.Parameters.AddWithValue("@Date", _BondRedemption.Date);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _BondRedemption.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Balance", _BondRedemption.Balance);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BondRedemption.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_BondRedemption.BondRedemptionPK, "BondRedemption");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BondRedemption where BondRedemptionPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BondRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _BondRedemption.FundPK);
                                cmd.Parameters.AddWithValue("@Date", _BondRedemption.Date);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _BondRedemption.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Balance", _BondRedemption.Balance);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BondRedemption.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BondRedemption set status= 4,Notes=@Notes, " +
                                " lastupdate=@lastupdate where BondRedemptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _BondRedemption.Notes);
                                cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BondRedemption.HistoryPK);
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

        public void BondRedemption_Approved(BondRedemption _BondRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BondRedemption set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where BondRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BondRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _BondRedemption.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BondRedemption set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BondRedemptionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BondRedemption.ApprovedUsersID);
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

        public void BondRedemption_Reject(BondRedemption _BondRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BondRedemption set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BondRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BondRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BondRedemption.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BondRedemption set status= 2,LastUpdate=@LastUpdate where BondRedemptionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
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

        public void BondRedemption_Void(BondRedemption _BondRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BondRedemption set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where BondRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BondRedemption.BondRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BondRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BondRedemption.VoidUsersID);
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