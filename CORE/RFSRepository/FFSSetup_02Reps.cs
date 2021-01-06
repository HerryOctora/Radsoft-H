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
    public class FFSSetup_02Reps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FFSSetup_02] " +
                            "([FFSSetup_02PK],[HistoryPK],[Status],[FundPK],[TujuanStrategiInvestasi],[ValueDate],[MarketReview],[UngkapanSanggahan]," +
                            "[TanggalPerdana],[NilaiAktivaBersih],[TotalUnitPenyertaan],[NilaiAktivaBersihUnit],[FaktorResikoYangUtama],[ManfaatInvestasi],[ImbalJasaManajerInvestasi],[ImbalJasaBankKustodian],[BiayaPembelian],[BiayaPenjualan],[BiayaPengalihan],[BankKustodianPK],[KebijakanInvestasi],";

        string _paramaterCommand = "@FundPK,@TujuanStrategiInvestasi,@ValueDate,@MarketReview,@UngkapanSanggahan," +
                                    "@TanggalPerdana,@NilaiAktivaBersih,@TotalUnitPenyertaan,@NilaiAktivaBersihUnit,@FaktorResikoYangUtama,@ManfaatInvestasi,@ImbalJasaManajerInvestasi,@ImbalJasaBankKustodian,@BiayaPembelian,@BiayaPenjualan,@BiayaPengalihan,@BankKustodianPK,@KebijakanInvestasi,";

        //2
        private FFSSetup_02 setFFSSetup_02(SqlDataReader dr)
        {
            FFSSetup_02 M_FFSSetup_02 = new FFSSetup_02();
            M_FFSSetup_02.FFSSetup_02PK = Convert.ToInt32(dr["FFSSetup_02PK"]);
            M_FFSSetup_02.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FFSSetup_02.Status = Convert.ToInt32(dr["Status"]);
            M_FFSSetup_02.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FFSSetup_02.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FFSSetup_02.FundID = Convert.ToString(dr["FundID"]);
            M_FFSSetup_02.TujuanStrategiInvestasi = Convert.ToString(dr["TujuanStrategiInvestasi"]);
            M_FFSSetup_02.ValueDate = Convert.ToString(dr["ValueDate"]);
            M_FFSSetup_02.MarketReview = Convert.ToString(dr["MarketReview"]);
            M_FFSSetup_02.UngkapanSanggahan = Convert.ToString(dr["UngkapanSanggahan"]);
            
            M_FFSSetup_02.TanggalPerdana = Convert.ToString(dr["TanggalPerdana"]);
            M_FFSSetup_02.NilaiAktivaBersih = Convert.ToDecimal(dr["NilaiAktivaBersih"]);
            M_FFSSetup_02.TotalUnitPenyertaan = Convert.ToDecimal(dr["TotalUnitPenyertaan"]);
            M_FFSSetup_02.NilaiAktivaBersihUnit = Convert.ToDecimal(dr["NilaiAktivaBersihUnit"]);
            M_FFSSetup_02.FaktorResikoYangUtama = Convert.ToString(dr["FaktorResikoYangUtama"]);
            M_FFSSetup_02.ManfaatInvestasi = Convert.ToString(dr["ManfaatInvestasi"]);
            M_FFSSetup_02.ImbalJasaManajerInvestasi = Convert.ToDecimal(dr["ImbalJasaManajerInvestasi"]);
            M_FFSSetup_02.ImbalJasaBankKustodian = Convert.ToDecimal(dr["ImbalJasaBankKustodian"]);
            M_FFSSetup_02.BiayaPembelian = Convert.ToDecimal(dr["BiayaPembelian"]);
            M_FFSSetup_02.BiayaPenjualan = Convert.ToDecimal(dr["BiayaPenjualan"]);
            M_FFSSetup_02.BiayaPengalihan = Convert.ToDecimal(dr["BiayaPengalihan"]);
            M_FFSSetup_02.BankKustodianPK = Convert.ToInt32(dr["BankKustodianPK"]);
            M_FFSSetup_02.BankKustodianID = Convert.ToString(dr["BankKustodianID"]);
            M_FFSSetup_02.KebijakanInvestasi = Convert.ToString(dr["KebijakanInvestasi"]);
            //M_FFSSetup_02.BankAccount = Convert.ToString(dr["BankAccount"]);


            M_FFSSetup_02.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FFSSetup_02.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FFSSetup_02.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FFSSetup_02.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FFSSetup_02.EntryTime = dr["EntryTime"].ToString();
            M_FFSSetup_02.UpdateTime = dr["UpdateTime"].ToString();
            M_FFSSetup_02.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FFSSetup_02.VoidTime = dr["VoidTime"].ToString();
            M_FFSSetup_02.DBUserID = dr["DBUserID"].ToString();
            M_FFSSetup_02.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FFSSetup_02.LastUpdate = dr["LastUpdate"].ToString();
            M_FFSSetup_02.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FFSSetup_02;
        }

        public List<FFSSetup_02> FFSSetup_02_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FFSSetup_02> L_FFSSetup_02 = new List<FFSSetup_02>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"

                              Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundID,C.ID BankKustodianID, * from FFSSetup_02 A
                              left join Fund B on A.FundPK = B.FundPK and B.status in(1,2) 
                              left join Bank C on A.BankKustodianPK = C.BankPK and C.status in(1,2)                          
                              where A.status = @status 
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                             Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundID,C.ID BankKustodianID, * from FFSSetup_02 A
                             left join Fund B on A.FundPK = B.FundPK and B.status in(1,2) 
                              left join Bank C on A.BankKustodianPK = C.BankPK and C.status in(1,2)     
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FFSSetup_02.Add(setFFSSetup_02(dr));
                                }
                            }
                            return L_FFSSetup_02;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FFSSetup_02_Add(FFSSetup_02 _FFSSetup_02, bool _havePrivillege)
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
                                 "Select isnull(max(FFSSetup_02Pk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FFSSetup_02";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup_02.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FFSSetup_02Pk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FFSSetup_02";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _FFSSetup_02.FundPK);
                        cmd.Parameters.AddWithValue("@TujuanStrategiInvestasi", _FFSSetup_02.TujuanStrategiInvestasi);
                        cmd.Parameters.AddWithValue("@ValueDate", _FFSSetup_02.ValueDate);
                        cmd.Parameters.AddWithValue("@MarketReview", _FFSSetup_02.MarketReview);
                        cmd.Parameters.AddWithValue("@UngkapanSanggahan", _FFSSetup_02.UngkapanSanggahan);
                        cmd.Parameters.AddWithValue("@TanggalPerdana", _FFSSetup_02.TanggalPerdana);
                        cmd.Parameters.AddWithValue("@NilaiAktivaBersih", _FFSSetup_02.NilaiAktivaBersih);
                        cmd.Parameters.AddWithValue("@TotalUnitPenyertaan", _FFSSetup_02.TotalUnitPenyertaan);
                        cmd.Parameters.AddWithValue("@NilaiAktivaBersihUnit", _FFSSetup_02.NilaiAktivaBersihUnit);
                        cmd.Parameters.AddWithValue("@FaktorResikoYangUtama", _FFSSetup_02.FaktorResikoYangUtama);
                        cmd.Parameters.AddWithValue("@ManfaatInvestasi", _FFSSetup_02.ManfaatInvestasi);
                        cmd.Parameters.AddWithValue("@ImbalJasaManajerInvestasi", _FFSSetup_02.ImbalJasaManajerInvestasi);
                        cmd.Parameters.AddWithValue("@ImbalJasaBankKustodian", _FFSSetup_02.ImbalJasaBankKustodian);
                        cmd.Parameters.AddWithValue("@BiayaPembelian", _FFSSetup_02.BiayaPembelian);
                        cmd.Parameters.AddWithValue("@BiayaPenjualan", _FFSSetup_02.BiayaPenjualan);
                        cmd.Parameters.AddWithValue("@BiayaPengalihan", _FFSSetup_02.BiayaPengalihan);
                        cmd.Parameters.AddWithValue("@BankKustodianPK", _FFSSetup_02.BankKustodianPK);
                        cmd.Parameters.AddWithValue("@KebijakanInvestasi", _FFSSetup_02.KebijakanInvestasi);
                        //cmd.Parameters.AddWithValue("@BankAccount", _FFSSetup_02.BankAccount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FFSSetup_02.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FFSSetup_02");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FFSSetup_02_Update(FFSSetup_02 _FFSSetup_02, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_FFSSetup_02.FFSSetup_02PK, _FFSSetup_02.HistoryPK, "FFSSetup_02");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FFSSetup_02 set status=2,Notes=@Notes,FundPK=@FundPK,TujuanStrategiInvestasi=@TujuanStrategiInvestasi,ValueDate=@ValueDate," +
                                    "MarketReview=@MarketReview,UngkapanSanggahan=@UngkapanSanggahan," +
                                    "TanggalPerdana=@TanggalPerdana,NilaiAktivaBersih=@NilaiAktivaBersih,TotalUnitPenyertaan=@TotalUnitPenyertaan,NilaiAktivaBersihUnit=@NilaiAktivaBersihUnit,FaktorResikoYangUtama=@FaktorResikoYangUtama,ManfaatInvestasi=@ManfaatInvestasi,ImbalJasaManajerInvestasi=@ImbalJasaManajerInvestasi," +
                                    "ImbalJasaBankKustodian=@ImbalJasaBankKustodian,BiayaPembelian=@BiayaPembelian,BiayaPenjualan=@BiayaPenjualan,BiayaPengalihan=@BiayaPengalihan,BankKustodianPK=@BankKustodianPK,KebijakanInvestasi=@KebijakanInvestasi," +
                                    "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where FFSSetup_02PK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup_02.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                            cmd.Parameters.AddWithValue("@Notes", _FFSSetup_02.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _FFSSetup_02.FundPK);
                            cmd.Parameters.AddWithValue("@TujuanStrategiInvestasi", _FFSSetup_02.TujuanStrategiInvestasi);
                            cmd.Parameters.AddWithValue("@ValueDate", _FFSSetup_02.ValueDate);
                            cmd.Parameters.AddWithValue("@MarketReview", _FFSSetup_02.MarketReview);
                            cmd.Parameters.AddWithValue("@UngkapanSanggahan", _FFSSetup_02.UngkapanSanggahan);
                            cmd.Parameters.AddWithValue("@TanggalPerdana", _FFSSetup_02.TanggalPerdana);
                            cmd.Parameters.AddWithValue("@NilaiAktivaBersih", _FFSSetup_02.NilaiAktivaBersih);
                            cmd.Parameters.AddWithValue("@TotalUnitPenyertaan", _FFSSetup_02.TotalUnitPenyertaan);
                            cmd.Parameters.AddWithValue("@NilaiAktivaBersihUnit", _FFSSetup_02.NilaiAktivaBersihUnit);
                            cmd.Parameters.AddWithValue("@FaktorResikoYangUtama", _FFSSetup_02.FaktorResikoYangUtama);
                            cmd.Parameters.AddWithValue("@ManfaatInvestasi", _FFSSetup_02.ManfaatInvestasi);
                            cmd.Parameters.AddWithValue("@ImbalJasaManajerInvestasi", _FFSSetup_02.ImbalJasaManajerInvestasi);
                            cmd.Parameters.AddWithValue("@ImbalJasaBankKustodian", _FFSSetup_02.ImbalJasaBankKustodian);
                            cmd.Parameters.AddWithValue("@BiayaPembelian", _FFSSetup_02.BiayaPembelian);
                            cmd.Parameters.AddWithValue("@BiayaPenjualan", _FFSSetup_02.BiayaPenjualan);
                            cmd.Parameters.AddWithValue("@BiayaPengalihan", _FFSSetup_02.BiayaPengalihan);
                            cmd.Parameters.AddWithValue("@BankKustodianPK", _FFSSetup_02.BankKustodianPK);
                            cmd.Parameters.AddWithValue("@KebijakanInvestasi", _FFSSetup_02.KebijakanInvestasi);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup_02.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup_02.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FFSSetup_02 set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FFSSetup_02PK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup_02.EntryUsersID);
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
                                cmd.CommandText = "Update FFSSetup_02 set FundPK=@FundPK,TujuanStrategiInvestasi=@TujuanStrategiInvestasi,ValueDate=@ValueDate," +
                                    "MarketReview=@MarketReview,UngkapanSanggahan=@UngkapanSanggahan," +
                                    "TanggalPerdana=@TanggalPerdana,NilaiAktivaBersih=@NilaiAktivaBersih,TotalUnitPenyertaan=@TotalUnitPenyertaan,NilaiAktivaBersihUnit=@NilaiAktivaBersihUnit,FaktorResikoYangUtama=@FaktorResikoYangUtama,ManfaatInvestasi=@ManfaatInvestasi,ImbalJasaManajerInvestasi=@ImbalJasaManajerInvestasi," +
                                    "ImbalJasaBankKustodian=@ImbalJasaBankKustodian,BiayaPembelian=@BiayaPembelian,BiayaPenjualan=@BiayaPenjualan,BiayaPengalihan=@BiayaPengalihan,BankKustodianPK=@BankKustodianPK,KebijakanInvestasi=@KebijakanInvestasi," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where FFSSetup_02PK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup_02.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                                cmd.Parameters.AddWithValue("@Notes", _FFSSetup_02.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _FFSSetup_02.FundPK);
                                cmd.Parameters.AddWithValue("@TujuanStrategiInvestasi", _FFSSetup_02.TujuanStrategiInvestasi);
                                cmd.Parameters.AddWithValue("@ValueDate", _FFSSetup_02.ValueDate);
                                cmd.Parameters.AddWithValue("@MarketReview", _FFSSetup_02.MarketReview);
                                cmd.Parameters.AddWithValue("@UngkapanSanggahan", _FFSSetup_02.UngkapanSanggahan);
                                cmd.Parameters.AddWithValue("@TanggalPerdana", _FFSSetup_02.TanggalPerdana);
                                cmd.Parameters.AddWithValue("@NilaiAktivaBersih", _FFSSetup_02.NilaiAktivaBersih);
                                cmd.Parameters.AddWithValue("@TotalUnitPenyertaan", _FFSSetup_02.TotalUnitPenyertaan);
                                cmd.Parameters.AddWithValue("@NilaiAktivaBersihUnit", _FFSSetup_02.NilaiAktivaBersihUnit);
                                cmd.Parameters.AddWithValue("@FaktorResikoYangUtama", _FFSSetup_02.FaktorResikoYangUtama);
                                cmd.Parameters.AddWithValue("@ManfaatInvestasi", _FFSSetup_02.ManfaatInvestasi);
                                cmd.Parameters.AddWithValue("@ImbalJasaManajerInvestasi", _FFSSetup_02.ImbalJasaManajerInvestasi);
                                cmd.Parameters.AddWithValue("@ImbalJasaBankKustodian", _FFSSetup_02.ImbalJasaBankKustodian);
                                cmd.Parameters.AddWithValue("@BiayaPembelian", _FFSSetup_02.BiayaPembelian);
                                cmd.Parameters.AddWithValue("@BiayaPenjualan", _FFSSetup_02.BiayaPenjualan);
                                cmd.Parameters.AddWithValue("@BiayaPengalihan", _FFSSetup_02.BiayaPengalihan);
                                cmd.Parameters.AddWithValue("@BankKustodianPK", _FFSSetup_02.BankKustodianPK);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi", _FFSSetup_02.KebijakanInvestasi);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup_02.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FFSSetup_02.FFSSetup_02PK, "FFSSetup_02");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FFSSetup_02 where FFSSetup_02PK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup_02.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FFSSetup_02.FundPK);
                                cmd.Parameters.AddWithValue("@TujuanStrategiInvestasi", _FFSSetup_02.TujuanStrategiInvestasi);
                                cmd.Parameters.AddWithValue("@ValueDate", _FFSSetup_02.ValueDate);
                                cmd.Parameters.AddWithValue("@MarketReview", _FFSSetup_02.MarketReview);
                                cmd.Parameters.AddWithValue("@UngkapanSanggahan", _FFSSetup_02.UngkapanSanggahan);
                                cmd.Parameters.AddWithValue("@TanggalPerdana", _FFSSetup_02.TanggalPerdana);
                                cmd.Parameters.AddWithValue("@NilaiAktivaBersih", _FFSSetup_02.NilaiAktivaBersih);
                                cmd.Parameters.AddWithValue("@TotalUnitPenyertaan", _FFSSetup_02.TotalUnitPenyertaan);
                                cmd.Parameters.AddWithValue("@NilaiAktivaBersihUnit", _FFSSetup_02.NilaiAktivaBersihUnit);
                                cmd.Parameters.AddWithValue("@FaktorResikoYangUtama", _FFSSetup_02.FaktorResikoYangUtama);
                                cmd.Parameters.AddWithValue("@ManfaatInvestasi", _FFSSetup_02.ManfaatInvestasi);
                                cmd.Parameters.AddWithValue("@ImbalJasaManajerInvestasi", _FFSSetup_02.ImbalJasaManajerInvestasi);
                                cmd.Parameters.AddWithValue("@ImbalJasaBankKustodian", _FFSSetup_02.ImbalJasaBankKustodian);
                                cmd.Parameters.AddWithValue("@BiayaPembelian", _FFSSetup_02.BiayaPembelian);
                                cmd.Parameters.AddWithValue("@BiayaPenjualan", _FFSSetup_02.BiayaPenjualan);
                                cmd.Parameters.AddWithValue("@BiayaPengalihan", _FFSSetup_02.BiayaPengalihan);
                                cmd.Parameters.AddWithValue("@BankKustodianPK", _FFSSetup_02.BankKustodianPK);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi", _FFSSetup_02.KebijakanInvestasi);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup_02.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FFSSetup_02 set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where FFSSetup_02PK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FFSSetup_02.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup_02.HistoryPK);
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

        public void FFSSetup_02_Approved(FFSSetup_02 _FFSSetup_02)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup_02 set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FFSSetup_02PK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup_02.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup_02.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FFSSetup_02 set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FFSSetup_02PK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup_02.ApprovedUsersID);
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

        public void FFSSetup_02_Reject(FFSSetup_02 _FFSSetup_02)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup_02 set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FFSSetup_02PK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup_02.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup_02.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FFSSetup_02 set status= 2,lastupdate=@lastupdate where FFSSetup_02PK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
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

        public void FFSSetup_02_Void(FFSSetup_02 _FFSSetup_02)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup_02 set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where FFSSetup_02PK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_02.FFSSetup_02PK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup_02.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup_02.VoidUsersID);
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

        public string ImportImage(string _title, string _path, int _pk)
        {
            DateTime _dateTime = DateTime.Now;
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {


                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                        
                                        Update FFSSetup_02 set Title = @Title, Path=@Path where FFSSetup_02PK = @pk and status = 2

                                        Select 'Import Success' Msg
                                        ";
                        cmd.Parameters.AddWithValue("@Path", _path);
                        cmd.Parameters.AddWithValue("@Title", _title);
                        cmd.Parameters.AddWithValue("@pk", _pk);
                        using (SqlDataReader dr1 = cmd.ExecuteReader())
                        {
                            if (dr1.HasRows)
                            {
                                dr1.Read();
                                return Convert.ToString(dr1["Msg"]);

                            }

                        }
                        return "";
                    }



                }


            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public FundCombo GetFundID(int _pk)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCombo> L_Fund = new List<FundCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT SUBSTRING (Name, 12, 30) FundID FROM [Fund]  where status = 2 and FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _pk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new FundCombo()
                                {

                                    FundID = Convert.ToString(dr["FundID"]),
                                };

                            }
                            else
                            {
                                return new FundCombo()
                                {
                                    FundID = "",
                                };
                            }
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