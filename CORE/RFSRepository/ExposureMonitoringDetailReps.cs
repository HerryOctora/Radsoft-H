using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class ExposureMonitoringDetailReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[ExposureMonitoringDetail] " +
                            "([ExposureMonitoringDetailPK],[HistoryPK],[Status],[FundPK],[BankCustodian],[KebijakanInvestasi],[Exposure]," +
                            "[TanggalPelanggaran],[Batasan],[NoSurat],[TanggalSurat],[TanggalTerimaSurat],[Remedy],[ExDate],[StatusExposure],[Remarks],";
        string _paramaterCommand = "@FundPK,@BankCustodian,@kebijakanInvestasi,@Exposure,@TanggalPelanggaran,@Batasan,@NoSurat,@TanggalSurat," +
                                    "@TanggalTerimaSurat,@Remedy,@ExDate,@StatusExposure,@Remarks,";


        private ExposureMonitoringDetail setExposureMonitoringDetail(SqlDataReader dr)
        {
            ExposureMonitoringDetail M_ExposureMonitoringDetail = new ExposureMonitoringDetail();
            M_ExposureMonitoringDetail.ExposureMonitoringDetailPK = Convert.ToInt32(dr["ExposureMonitoringDetailPK"]);
            M_ExposureMonitoringDetail.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_ExposureMonitoringDetail.Status = Convert.ToInt32(dr["Status"]);
            M_ExposureMonitoringDetail.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_ExposureMonitoringDetail.Notes = Convert.ToString(dr["Notes"]);
            //M_ExposureMonitoringDetail.Date = Convert.ToDateTime(dr["Date"]);
            M_ExposureMonitoringDetail.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_ExposureMonitoringDetail.FundName = Convert.ToString(dr["FundName"]);
            M_ExposureMonitoringDetail.BankCustodian = Convert.ToString(dr["BankCustodian"]);
            M_ExposureMonitoringDetail.KebijakanInvestasi = Convert.ToString(dr["KebijakanInvestasi"]);
            M_ExposureMonitoringDetail.Exposure = Convert.ToString(dr["Exposure"]);
            M_ExposureMonitoringDetail.TanggalPelanggaran = Convert.ToDateTime(dr["TanggalPelanggaran"]);
            M_ExposureMonitoringDetail.Batasan = Convert.ToString(dr["Batasan"]);
            M_ExposureMonitoringDetail.NoSurat = Convert.ToString(dr["NoSurat"]);
            M_ExposureMonitoringDetail.TanggalSurat = Convert.ToDateTime(dr["TanggalSurat"]);
            M_ExposureMonitoringDetail.TanggalTerimaSurat = Convert.ToDateTime(dr["TanggalTerimaSurat"]);
            M_ExposureMonitoringDetail.Remedy = Convert.ToInt32(dr["Remedy"]);
            M_ExposureMonitoringDetail.RemedyDesc = dr["RemedyDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RemedyDesc"]);
            M_ExposureMonitoringDetail.ExDate = Convert.ToDateTime(dr["ExDate"]);
            M_ExposureMonitoringDetail.StatusExposure = Convert.ToInt32(dr["StatusExposure"]);
            M_ExposureMonitoringDetail.Remarks = Convert.ToString(dr["Remarks"]);
            M_ExposureMonitoringDetail.FundName = Convert.ToString(dr["FundName"]);

            M_ExposureMonitoringDetail.EntryUsersID = dr["EntryUsersID"].ToString();
            M_ExposureMonitoringDetail.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_ExposureMonitoringDetail.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_ExposureMonitoringDetail.VoidUsersID = dr["VoidUsersID"].ToString();
            M_ExposureMonitoringDetail.EntryTime = dr["EntryTime"].ToString();
            M_ExposureMonitoringDetail.UpdateTime = dr["UpdateTime"].ToString();
            M_ExposureMonitoringDetail.ApprovedTime = dr["ApprovedTime"].ToString();
            M_ExposureMonitoringDetail.VoidTime = dr["VoidTime"].ToString();
            M_ExposureMonitoringDetail.DBUserID = dr["DBUserID"].ToString();
            M_ExposureMonitoringDetail.DBTerminalID = dr["DBTerminalID"].ToString();
            M_ExposureMonitoringDetail.LastUpdate = dr["LastUpdate"].ToString();
            M_ExposureMonitoringDetail.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_ExposureMonitoringDetail;
        }

        public List<ExposureMonitoringDetail> ExposureMonitoringDetail_Select(int _status, DateTime _date, int _statusExposure)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ExposureMonitoringDetail> L_ExposureMonitoringDetail = new List<ExposureMonitoringDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode == "20")
                        {
                            if (_status != 9)
                            {
                                cmd.CommandText = @"
                                Select case when EM.status=1 then 'PENDING' else Case When EM.status = 2 then 'APPROVED' else Case when EM.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.Name FundName,
                                case when EM.Remedy = 1 then '10 Days' when EM.Remedy = 2 then '20 Days' else '40 Days' end RemedyDesc, EM.* from ExposureMonitoringDetail EM left join
                                Fund A on EM.FundPK = A.FundPK and A.status = 2
                                where EM.StatusExposure = @StatusExposure and EM.status = @status

                                ";
                                cmd.Parameters.AddWithValue("@status", _status);
                                cmd.Parameters.AddWithValue("@Date", _date);
                                cmd.Parameters.AddWithValue("@StatusExposure", _statusExposure);
                            }
                            else
                            {
                                cmd.CommandText = @"Select case when EM.status=1 then 'PENDING' else Case When EM.status = 2 then 'APPROVED' else Case when EM.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.Name FundName, 
                                case when EM.Remedy = 1 then '10 Days' when EM.Remedy = 2 then '20 Days' else '40 Days' end RemedyDesc,EM.* from ExposureMonitoringDetail EM left join
                                Fund A on EM.FundPK = A.FundPK and A.status = 2";
                            }
                        }
                        else
                        {
                            if (_status != 9)
                            {
                                cmd.CommandText = @"Select case when EM.status=1 then 'PENDING' else Case When EM.status = 2 then 'APPROVED' else Case when EM.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.Name FundName, EM.* from ExposureMonitoringDetail EM left join
                             Fund A on EM.FundPK = A.FundPK and A.status = 2
                            where EM.Date = @Date and EM.StatusExposure = @StatusExposure and EM.status = @status";
                                cmd.Parameters.AddWithValue("@status", _status);
                                cmd.Parameters.AddWithValue("@Date", _date);
                                cmd.Parameters.AddWithValue("@StatusExposure", _statusExposure);
                            }
                            else
                            {
                                cmd.CommandText = @"Select case when EM.status=1 then 'PENDING' else Case When EM.status = 2 then 'APPROVED' else Case when EM.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.Name FundName, EM.* from ExposureMonitoringDetail EM left join
                            Fund A on EM.FundPK = A.FundPK and A.status = 2";
                            }
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ExposureMonitoringDetail.Add(setExposureMonitoringDetail(dr));
                                }
                            }
                            return L_ExposureMonitoringDetail;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ExposureMonitoringDetail_Add(ExposureMonitoringDetail _ExposureMonitoringDetail, bool _havePrivillege)
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
                                 "Select isnull(max(ExposureMonitoringDetailPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from ExposureMonitoringDetail";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _ExposureMonitoringDetail.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(ExposureMonitoringDetailPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from ExposureMonitoringDetail";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        //cmd.Parameters.AddWithValue("@Date", _ExposureMonitoringDetail.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _ExposureMonitoringDetail.FundPK);
                        cmd.Parameters.AddWithValue("@BankCustodian", _ExposureMonitoringDetail.BankCustodian);
                        cmd.Parameters.AddWithValue("@KebijakanInvestasi", _ExposureMonitoringDetail.KebijakanInvestasi);
                        cmd.Parameters.AddWithValue("@Exposure", _ExposureMonitoringDetail.Exposure);
                        cmd.Parameters.AddWithValue("@TanggalPelanggaran", _ExposureMonitoringDetail.TanggalPelanggaran);
                        cmd.Parameters.AddWithValue("@Batasan", _ExposureMonitoringDetail.Batasan);
                        cmd.Parameters.AddWithValue("@NoSurat", _ExposureMonitoringDetail.NoSurat);
                        cmd.Parameters.AddWithValue("@TanggalSurat", _ExposureMonitoringDetail.TanggalSurat);
                        cmd.Parameters.AddWithValue("@TanggalTerimaSurat", _ExposureMonitoringDetail.TanggalTerimaSurat);
                        cmd.Parameters.AddWithValue("@Remedy", _ExposureMonitoringDetail.Remedy);
                        cmd.Parameters.AddWithValue("@ExDate", _ExposureMonitoringDetail.ExDate);
                        cmd.Parameters.AddWithValue("@StatusExposure", _ExposureMonitoringDetail.StatusExposure);
                        cmd.Parameters.AddWithValue("@Remarks", _ExposureMonitoringDetail.Remarks);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _ExposureMonitoringDetail.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "ExposureMonitoringDetail");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ExposureMonitoringDetail_Update(ExposureMonitoringDetail _ExposureMonitoringDetail, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_ExposureMonitoringDetail.ExposureMonitoringDetailPK, _ExposureMonitoringDetail.HistoryPK, "ExposureMonitoringDetail"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update ExposureMonitoringDetail set status=2, Notes=@Notes,FundPK=@FundPK,
                                BankCustodian=@BankCustodian,KebijakanInvestasi=@KebijakanInvestasi,Exposure=@Exposure,TanggalPelanggaran=@TanggalPelanggaran,
                                Batasan=@Batasan,NoSurat=@NoSurat,TanggalSurat=@TanggalSurat,TanggalTerimaSurat=@TanggalTerimaSurat,Remedy=@Remedy,ExDate=@ExDate,StatusExposure=@StatusExposure,Remarks=@Remarks,
                                ApprovedUsersID=@ApprovedUsersID, 
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate 
                                where ExposureMonitoringDetailPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _ExposureMonitoringDetail.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                            cmd.Parameters.AddWithValue("@Notes", _ExposureMonitoringDetail.Notes);
                            //cmd.Parameters.AddWithValue("@Date", _ExposureMonitoringDetail.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _ExposureMonitoringDetail.FundPK);
                            cmd.Parameters.AddWithValue("@BankCustodian", _ExposureMonitoringDetail.BankCustodian);
                            cmd.Parameters.AddWithValue("@KebijakanInvestasi", _ExposureMonitoringDetail.KebijakanInvestasi);
                            cmd.Parameters.AddWithValue("@Exposure", _ExposureMonitoringDetail.Exposure);
                            cmd.Parameters.AddWithValue("@TanggalPelanggaran", _ExposureMonitoringDetail.TanggalPelanggaran);
                            cmd.Parameters.AddWithValue("@Batasan", _ExposureMonitoringDetail.Batasan);
                            cmd.Parameters.AddWithValue("@NoSurat", _ExposureMonitoringDetail.NoSurat);
                            cmd.Parameters.AddWithValue("@TanggalSurat", _ExposureMonitoringDetail.TanggalSurat);
                            cmd.Parameters.AddWithValue("@TanggalTerimaSurat", _ExposureMonitoringDetail.TanggalTerimaSurat);
                            cmd.Parameters.AddWithValue("@Remedy", _ExposureMonitoringDetail.Remedy);
                            cmd.Parameters.AddWithValue("@ExDate", _ExposureMonitoringDetail.ExDate);
                            cmd.Parameters.AddWithValue("@StatusExposure", _ExposureMonitoringDetail.StatusExposure);
                            cmd.Parameters.AddWithValue("@Remarks", _ExposureMonitoringDetail.Remarks);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _ExposureMonitoringDetail.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _ExposureMonitoringDetail.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ExposureMonitoringDetail set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ExposureMonitoringDetailPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _ExposureMonitoringDetail.EntryUsersID);
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
                                cmd.CommandText = @"Update ExposureMonitoringDetail set Notes=@Notes,FundPK=@FundPK,
                                BankCustodian=@BankCustodian,KebijakanInvestasi=@KebijakanInvestasi,Exposure=@Exposure,TanggalPelanggaran=@TanggalPelanggaran,
                                Batasan=@Batasan,NoSurat=@NoSurat,TanggalSurat=@TanggalSurat,TanggalTerimaSurat=@TanggalTerimaSurat,Remedy=@Remedy,ExDate=@ExDate,StatusExposure=@StatusExposure,Remarks=@Remarks,
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate 
                                where ExposureMonitoringDetailPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _ExposureMonitoringDetail.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                                cmd.Parameters.AddWithValue("@Notes", _ExposureMonitoringDetail.Notes);
                                //cmd.Parameters.AddWithValue("@Date", _ExposureMonitoringDetail.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _ExposureMonitoringDetail.FundPK);
                                cmd.Parameters.AddWithValue("@BankCustodian", _ExposureMonitoringDetail.BankCustodian);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi", _ExposureMonitoringDetail.KebijakanInvestasi);
                                cmd.Parameters.AddWithValue("@Exposure", _ExposureMonitoringDetail.Exposure);
                                cmd.Parameters.AddWithValue("@TanggalPelanggaran", _ExposureMonitoringDetail.TanggalPelanggaran);
                                cmd.Parameters.AddWithValue("@Batasan", _ExposureMonitoringDetail.Batasan);
                                cmd.Parameters.AddWithValue("@NoSurat", _ExposureMonitoringDetail.NoSurat);
                                cmd.Parameters.AddWithValue("@TanggalSurat", _ExposureMonitoringDetail.TanggalSurat);
                                cmd.Parameters.AddWithValue("@TanggalTerimaSurat", _ExposureMonitoringDetail.TanggalTerimaSurat);
                                cmd.Parameters.AddWithValue("@Remedy", _ExposureMonitoringDetail.Remedy);
                                cmd.Parameters.AddWithValue("@ExDate", _ExposureMonitoringDetail.ExDate);
                                cmd.Parameters.AddWithValue("@StatusExposure", _ExposureMonitoringDetail.StatusExposure);
                                cmd.Parameters.AddWithValue("@Remarks", _ExposureMonitoringDetail.Remarks);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _ExposureMonitoringDetail.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_ExposureMonitoringDetail.ExposureMonitoringDetailPK, "ExposureMonitoringDetail");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate],parameter,InstrumentPK)" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate,parameter,InstrumentPK  " +
                                "From ExposureMonitoringDetail where ExposureMonitoringDetailPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _ExposureMonitoringDetail.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                //cmd.Parameters.AddWithValue("@Date", _ExposureMonitoringDetail.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _ExposureMonitoringDetail.FundPK);
                                cmd.Parameters.AddWithValue("@BankCustodian", _ExposureMonitoringDetail.BankCustodian);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi", _ExposureMonitoringDetail.KebijakanInvestasi);
                                cmd.Parameters.AddWithValue("@Exposure", _ExposureMonitoringDetail.Exposure);
                                cmd.Parameters.AddWithValue("@TanggalPelanggaran", _ExposureMonitoringDetail.TanggalPelanggaran);
                                cmd.Parameters.AddWithValue("@Batasan", _ExposureMonitoringDetail.Batasan);
                                cmd.Parameters.AddWithValue("@NoSurat", _ExposureMonitoringDetail.NoSurat);
                                cmd.Parameters.AddWithValue("@TanggalSurat", _ExposureMonitoringDetail.TanggalSurat);
                                cmd.Parameters.AddWithValue("@TanggalTerimaSurat", _ExposureMonitoringDetail.TanggalTerimaSurat);
                                cmd.Parameters.AddWithValue("@Remedy", _ExposureMonitoringDetail.Remedy);
                                cmd.Parameters.AddWithValue("@ExDate", _ExposureMonitoringDetail.ExDate);
                                cmd.Parameters.AddWithValue("@StatusExposure", _ExposureMonitoringDetail.StatusExposure);
                                cmd.Parameters.AddWithValue("@Remarks", _ExposureMonitoringDetail.Remarks);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _ExposureMonitoringDetail.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ExposureMonitoringDetail set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where ExposureMonitoringDetailPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _ExposureMonitoringDetail.Notes);
                                cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _ExposureMonitoringDetail.HistoryPK);
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

        public void ExposureMonitoringDetail_Approved(ExposureMonitoringDetail _ExposureMonitoringDetail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ExposureMonitoringDetail set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where ExposureMonitoringDetailPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ExposureMonitoringDetail.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _ExposureMonitoringDetail.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ExposureMonitoringDetail set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ExposureMonitoringDetailPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ExposureMonitoringDetail.ApprovedUsersID);
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

        public void ExposureMonitoringDetail_Reject(ExposureMonitoringDetail _ExposureMonitoringDetail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ExposureMonitoringDetail set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where ExposureMonitoringDetailPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ExposureMonitoringDetail.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ExposureMonitoringDetail.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ExposureMonitoringDetail set status= 2,LastUpdate=@LastUpdate  where ExposureMonitoringDetailPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
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

        public void ExposureMonitoringDetail_Void(ExposureMonitoringDetail _ExposureMonitoringDetail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ExposureMonitoringDetail set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where ExposureMonitoringDetailPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ExposureMonitoringDetail.ExposureMonitoringDetailPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ExposureMonitoringDetail.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ExposureMonitoringDetail.VoidUsersID);
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

    }
}
