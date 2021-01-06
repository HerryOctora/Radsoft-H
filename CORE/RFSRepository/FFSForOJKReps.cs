using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Data.OleDb;
using RFSRepository;
using OfficeOpenXml.Drawing.Chart;
using System.Xml;


namespace RFSRepository
{
    public class FFSForOJKReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = @"INSERT INTO [dbo].[FFSForOJK] 
                                 ([FFSForOJKPK],[HistoryPK],[Status],[Date],[FundPK],[TemplateType],[PeriodePenilaian],
                                 [DanaKegiatanSosial],[AkumulasiDanaCSR],[CSR],[Resiko1],[Resiko2],[Resiko3],[Resiko4],
                                 [Resiko5],[Resiko6],[Resiko7],[KlasifikasiResiko],[ManajerInvestasi],[TujuanInvestasi],[KebijakanInvestasi1],
                                 [KebijakanInvestasi2],[KebijakanInvestasi3],[ProfilBankCustodian],[AksesProspektus],[Resiko8],[Resiko9],[BitDistributedIncome],";
        string _paramaterCommand = @"@Date,@FundPK,@TemplateType,@PeriodePenilaian,@DanaKegiatanSosial,@AkumulasiDanaCSR,@CSR,
                                     @Resiko1,@Resiko2,@Resiko3,@Resiko4,@Resiko5,@Resiko6,@Resiko7,@KlasifikasiResiko,@ManajerInvestasi,
                                     @TujuanInvestasi,@KebijakanInvestasi1,@KebijakanInvestasi2,@KebijakanInvestasi3,@ProfilBankCustodian,@AksesProspektus,@Resiko8,@Resiko9,@BitDistributedIncome,";


        private FFSForOJK setFFSForOJK(SqlDataReader dr)
        {
            FFSForOJK M_FFSForOJK = new FFSForOJK();
            M_FFSForOJK.FFSForOJKPK = Convert.ToInt32(dr["FFSForOJKPK"]);
            M_FFSForOJK.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FFSForOJK.Status = Convert.ToInt32(dr["Status"]);
            M_FFSForOJK.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FFSForOJK.Notes = Convert.ToString(dr["Notes"]);

            M_FFSForOJK.Date = Convert.ToDateTime(dr["Date"]);
            M_FFSForOJK.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FFSForOJK.FundName = Convert.ToString(dr["FundName"]);
            M_FFSForOJK.TemplateType = Convert.ToInt32(dr["TemplateType"]);
            M_FFSForOJK.TemplateTypeDesc = Convert.ToString(dr["TemplateTypeDesc"]);

            M_FFSForOJK.PeriodePenilaian = Convert.ToString(dr["PeriodePenilaian"]);
            M_FFSForOJK.DanaKegiatanSosial = Convert.ToString(dr["DanaKegiatanSosial"]);
            M_FFSForOJK.AkumulasiDanaCSR = Convert.ToString(dr["AkumulasiDanaCSR"]);
            M_FFSForOJK.CSR = Convert.ToString(dr["CSR"]);
            M_FFSForOJK.Resiko1 = Convert.ToString(dr["Resiko1"]);

            M_FFSForOJK.Resiko2 = Convert.ToString(dr["Resiko2"]);
            M_FFSForOJK.Resiko3 = Convert.ToString(dr["Resiko3"]);
            M_FFSForOJK.Resiko4 = Convert.ToString(dr["Resiko4"]);
            M_FFSForOJK.Resiko5 = Convert.ToString(dr["Resiko5"]);
            M_FFSForOJK.Resiko6 = Convert.ToString(dr["Resiko6"]);

            M_FFSForOJK.Resiko7 = Convert.ToString(dr["Resiko7"]);
            M_FFSForOJK.Resiko8 = Convert.ToString(dr["Resiko8"]);
            M_FFSForOJK.Resiko9 = Convert.ToString(dr["Resiko9"]);
            M_FFSForOJK.BitDistributedIncome = Convert.ToBoolean(dr["BitDistributedIncome"]);

            M_FFSForOJK.KlasifikasiResiko = Convert.ToInt32(dr["KlasifikasiResiko"]);
            M_FFSForOJK.KlasifikasiResikoDesc = Convert.ToString(dr["KlasifikasiResikoDesc"]);
            M_FFSForOJK.ManajerInvestasi = Convert.ToString(dr["ManajerInvestasi"]);
            M_FFSForOJK.TujuanInvestasi = Convert.ToString(dr["TujuanInvestasi"]);

            M_FFSForOJK.KebijakanInvestasi1 = Convert.ToString(dr["KebijakanInvestasi1"]);
            M_FFSForOJK.KebijakanInvestasi2 = Convert.ToString(dr["KebijakanInvestasi2"]);
            M_FFSForOJK.KebijakanInvestasi3 = Convert.ToString(dr["KebijakanInvestasi3"]);
            M_FFSForOJK.ProfilBankCustodian = Convert.ToString(dr["ProfilBankCustodian"]);
            M_FFSForOJK.AksesProspektus = Convert.ToString(dr["AksesProspektus"]);

            M_FFSForOJK.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FFSForOJK.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FFSForOJK.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FFSForOJK.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FFSForOJK.EntryTime = dr["EntryTime"].ToString();
            M_FFSForOJK.UpdateTime = dr["UpdateTime"].ToString();
            M_FFSForOJK.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FFSForOJK.VoidTime = dr["VoidTime"].ToString();
            M_FFSForOJK.DBUserID = dr["DBUserID"].ToString();
            M_FFSForOJK.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FFSForOJK.LastUpdate = dr["LastUpdate"].ToString();
            M_FFSForOJK.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FFSForOJK;
        }

        public List<FFSForOJK> FFSForOJK_Select(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FFSForOJK> L_FFSForOJK = new List<FFSForOJK>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when FO.status=1 then 'PENDING' else Case When FO.status = 2 then 'APPROVED' else Case when FO.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.Name FundName,
                                                case when FO.TemplateType = 1 then 'With Benchmark' 
                                                else case when FO.TemplateType = 2 then 'WithoutBenchmark' else '' end end TemplateTypeDesc,
                                                case when FO.KlasifikasiResiko = 1 then 'Rendah'
                                                else case when FO.KlasifikasiResiko = 2 then 'Cukup Rendah'
                                                else case when FO.KlasifikasiResiko = 3 then 'Sedang' 
                                                else case when FO.KlasifikasiResiko = 4 then 'Cukup Tinggi' 
                                                else case when FO.KlasifikasiResiko = 5 then 'Tinggi' else ''  end end end end end  KlasifikasiResikoDesc
                                                ,FO.* from FFSForOJK FO left join 
                                                   Fund F on FO.FundPK = F.FundPK and F.status = 2 
                                                   where FO.status = @status and FO.Date between @DateFrom and @DateTo";
                            cmd.Parameters.AddWithValue("@status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when FO.status=1 then 'PENDING' else Case When FO.status = 2 then 'APPROVED' else Case when FO.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.Name FundName,
                                                case when FO.TemplateType = 1 then 'With Benchmark' 
                                                else case when FO.TemplateType = 2 then 'WithoutBenchmark' else '' end end TemplateTypeDesc,
                                                case when FO.KlasifikasiResiko = 1 then 'Rendah'
                                                else case when FO.KlasifikasiResiko = 2 then 'Cukup Rendah'
                                                else case when FO.KlasifikasiResiko = 3 then 'Sedang' 
                                                else case when FO.KlasifikasiResiko = 4 then 'Cukup Tinggi' 
                                                else case when FO.KlasifikasiResiko = 5 then 'Tinggi' else ''  end end end end end  KlasifikasiResikoDesc
                                                ,FO.* from FFSForOJK FO left join 
                                                   Fund F on FO.FundPK = F.FundPK and F.status = 2 
                                                   where FO.Date between @DateFrom and @DateTo
                                                   order by FundPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FFSForOJK.Add(setFFSForOJK(dr));
                                }
                            }
                            return L_FFSForOJK;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int FFSForOJK_Add(FFSForOJK _FFSForOJK, bool _havePrivillege)
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
                                 "Select isnull(max(FFSForOJKPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FFSForOJK";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSForOJK.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FFSForOJKPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FFSForOJK";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);

                        cmd.Parameters.AddWithValue("@Date", _FFSForOJK.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _FFSForOJK.FundPK);
                        cmd.Parameters.AddWithValue("@TemplateType ", _FFSForOJK.TemplateType);
                        cmd.Parameters.AddWithValue("@PeriodePenilaian ", _FFSForOJK.PeriodePenilaian);
                        cmd.Parameters.AddWithValue("@DanaKegiatanSosial ", _FFSForOJK.DanaKegiatanSosial);

                        cmd.Parameters.AddWithValue("@AkumulasiDanaCSR ", _FFSForOJK.AkumulasiDanaCSR);
                        cmd.Parameters.AddWithValue("@CSR", _FFSForOJK.CSR);
                        cmd.Parameters.AddWithValue("@Resiko1", _FFSForOJK.Resiko1);
                        cmd.Parameters.AddWithValue("@Resiko2", _FFSForOJK.Resiko2);
                        cmd.Parameters.AddWithValue("@Resiko3", _FFSForOJK.Resiko3);

                        cmd.Parameters.AddWithValue("@Resiko4", _FFSForOJK.Resiko4);
                        cmd.Parameters.AddWithValue("@Resiko5", _FFSForOJK.Resiko5);
                        cmd.Parameters.AddWithValue("@Resiko6", _FFSForOJK.Resiko6);
                        cmd.Parameters.AddWithValue("@Resiko7", _FFSForOJK.Resiko7);
                        cmd.Parameters.AddWithValue("@KlasifikasiResiko", _FFSForOJK.KlasifikasiResiko);

                        cmd.Parameters.AddWithValue("@ManajerInvestasi", _FFSForOJK.ManajerInvestasi);
                        cmd.Parameters.AddWithValue("@TujuanInvestasi", _FFSForOJK.TujuanInvestasi);
                        cmd.Parameters.AddWithValue("@KebijakanInvestasi1", _FFSForOJK.KebijakanInvestasi1);
                        cmd.Parameters.AddWithValue("@KebijakanInvestasi2", _FFSForOJK.KebijakanInvestasi2);
                        cmd.Parameters.AddWithValue("@KebijakanInvestasi3", _FFSForOJK.KebijakanInvestasi3);

                        cmd.Parameters.AddWithValue("@ProfilBankCustodian ", _FFSForOJK.ProfilBankCustodian);
                        cmd.Parameters.AddWithValue("@AksesProspektus ", _FFSForOJK.AksesProspektus);

                        cmd.Parameters.AddWithValue("@Resiko8", _FFSForOJK.Resiko8);
                        cmd.Parameters.AddWithValue("@Resiko9", _FFSForOJK.Resiko9);
                        cmd.Parameters.AddWithValue("@BitDistributedIncome", _FFSForOJK.BitDistributedIncome);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _FFSForOJK.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FFSForOJK");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FFSForOJK_Update(FFSForOJK _FFSForOJK, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FFSForOJK.FFSForOJKPK, _FFSForOJK.HistoryPK, "FFSForOJK"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update FFSForOJK set status=2, Notes=@Notes,Date=@Date,FundPK=@FundPK,TemplateType=@TemplateType,
                                PeriodePenilaian=@PeriodePenilaian,DanaKegiatanSosial=@DanaKegiatanSosial,AkumulasiDanaCSR=@AkumulasiDanaCSR,CSR=@CSR,
                                Resiko1=@Resiko1,Resiko2=@Resiko2,Resiko3=@Resiko3,Resiko4=@Resiko4,Resiko5=@Resiko5,Resiko6=@Resiko6,Resiko7=@Resiko7,
                                KlasifikasiResiko=@KlasifikasiResiko,ManajerInvestasi=@ManajerInvestasi,TujuanInvestasi=@TujuanInvestasi,KebijakanInvestasi1=@KebijakanInvestasi1,
                                KebijakanInvestasi2=@KebijakanInvestasi2,KebijakanInvestasi3=@KebijakanInvestasi3,ProfilBankCustodian=@ProfilBankCustodian,AksesProspektus=@AksesProspektus,
                                Resiko8=@Resiko8,Resiko9=@Resiko9,BitDistributedIncome=@BitDistributedIncome,
                                ApprovedUsersID=@ApprovedUsersID, 
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate 
                                where FFSForOJKPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FFSForOJK.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                            cmd.Parameters.AddWithValue("@Notes", _FFSForOJK.Notes);
                            cmd.Parameters.AddWithValue("@Date", _FFSForOJK.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _FFSForOJK.FundPK);
                            cmd.Parameters.AddWithValue("@TemplateType ", _FFSForOJK.TemplateType);
                            cmd.Parameters.AddWithValue("@PeriodePenilaian ", _FFSForOJK.PeriodePenilaian);
                            cmd.Parameters.AddWithValue("@DanaKegiatanSosial ", _FFSForOJK.DanaKegiatanSosial);

                            cmd.Parameters.AddWithValue("@AkumulasiDanaCSR ", _FFSForOJK.AkumulasiDanaCSR);
                            cmd.Parameters.AddWithValue("@CSR", _FFSForOJK.CSR);
                            cmd.Parameters.AddWithValue("@Resiko1", _FFSForOJK.Resiko1);
                            cmd.Parameters.AddWithValue("@Resiko2", _FFSForOJK.Resiko2);
                            cmd.Parameters.AddWithValue("@Resiko3", _FFSForOJK.Resiko3);

                            cmd.Parameters.AddWithValue("@Resiko4", _FFSForOJK.Resiko4);
                            cmd.Parameters.AddWithValue("@Resiko5", _FFSForOJK.Resiko5);
                            cmd.Parameters.AddWithValue("@Resiko6", _FFSForOJK.Resiko6);
                            cmd.Parameters.AddWithValue("@Resiko7", _FFSForOJK.Resiko7);
                            cmd.Parameters.AddWithValue("@KlasifikasiResiko", _FFSForOJK.KlasifikasiResiko);

                            cmd.Parameters.AddWithValue("@ManajerInvestasi", _FFSForOJK.ManajerInvestasi);
                            cmd.Parameters.AddWithValue("@TujuanInvestasi", _FFSForOJK.TujuanInvestasi);
                            cmd.Parameters.AddWithValue("@KebijakanInvestasi1", _FFSForOJK.KebijakanInvestasi1);
                            cmd.Parameters.AddWithValue("@KebijakanInvestasi2", _FFSForOJK.KebijakanInvestasi2);
                            cmd.Parameters.AddWithValue("@KebijakanInvestasi3", _FFSForOJK.KebijakanInvestasi3);

                            cmd.Parameters.AddWithValue("@ProfilBankCustodian", _FFSForOJK.ProfilBankCustodian);
                            cmd.Parameters.AddWithValue("@AksesProspektus", _FFSForOJK.AksesProspektus);

                            cmd.Parameters.AddWithValue("@Resiko8", _FFSForOJK.Resiko8);
                            cmd.Parameters.AddWithValue("@Resiko9", _FFSForOJK.Resiko9);
                            cmd.Parameters.AddWithValue("@BitDistributedIncome", _FFSForOJK.BitDistributedIncome);


                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSForOJK.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSForOJK.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FFSForOJK set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FFSForOJKPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FFSForOJK.EntryUsersID);
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
                                cmd.CommandText = @"Update FFSForOJK set Notes=@Notes,Date=@Date,FundPK=@FundPK,TemplateType=@TemplateType,
                                PeriodePenilaian=@PeriodePenilaian,DanaKegiatanSosial=@DanaKegiatanSosial,AkumulasiDanaCSR=@AkumulasiDanaCSR,CSR=@CSR,
                                Resiko1=@Resiko1,Resiko2=@Resiko2,Resiko3=@Resiko3,Resiko4=@Resiko4,Resiko5=@Resiko5,Resiko6=@Resiko6,Resiko7=@Resiko7,
                                KlasifikasiResiko=@KlasifikasiResiko,ManajerInvestasi=@ManajerInvestasi,TujuanInvestasi=@TujuanInvestasi,KebijakanInvestasi1=@KebijakanInvestasi1,
                                KebijakanInvestasi2=@KebijakanInvestasi2,KebijakanInvestasi3=@KebijakanInvestasi3,ProfilBankCustodian=@ProfilBankCustodian,AksesProspektus=@AksesProspektus,
                                Resiko8=@Resiko8,Resiko9=@Resiko9,BitDistributedIncome=@BitDistributedIncome,
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate 
                                where FFSForOJKPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSForOJK.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                                cmd.Parameters.AddWithValue("@Notes", _FFSForOJK.Notes);
                                cmd.Parameters.AddWithValue("@Date", _FFSForOJK.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _FFSForOJK.FundPK);
                                cmd.Parameters.AddWithValue("@TemplateType", _FFSForOJK.TemplateType);
                                cmd.Parameters.AddWithValue("@PeriodePenilaian", _FFSForOJK.PeriodePenilaian);
                                cmd.Parameters.AddWithValue("@DanaKegiatanSosial", _FFSForOJK.DanaKegiatanSosial);

                                cmd.Parameters.AddWithValue("@AkumulasiDanaCSR ", _FFSForOJK.AkumulasiDanaCSR);
                                cmd.Parameters.AddWithValue("@CSR", _FFSForOJK.CSR);
                                cmd.Parameters.AddWithValue("@Resiko1", _FFSForOJK.Resiko1);
                                cmd.Parameters.AddWithValue("@Resiko2", _FFSForOJK.Resiko2);
                                cmd.Parameters.AddWithValue("@Resiko3", _FFSForOJK.Resiko3);

                                cmd.Parameters.AddWithValue("@Resiko4", _FFSForOJK.Resiko4);
                                cmd.Parameters.AddWithValue("@Resiko5", _FFSForOJK.Resiko5);
                                cmd.Parameters.AddWithValue("@Resiko6", _FFSForOJK.Resiko6);
                                cmd.Parameters.AddWithValue("@Resiko7", _FFSForOJK.Resiko7);
                                cmd.Parameters.AddWithValue("@KlasifikasiResiko", _FFSForOJK.KlasifikasiResiko);

                                cmd.Parameters.AddWithValue("@ManajerInvestasi", _FFSForOJK.ManajerInvestasi);
                                cmd.Parameters.AddWithValue("@TujuanInvestasi", _FFSForOJK.TujuanInvestasi);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi1", _FFSForOJK.KebijakanInvestasi1);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi2", _FFSForOJK.KebijakanInvestasi2);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi3", _FFSForOJK.KebijakanInvestasi3);

                                cmd.Parameters.AddWithValue("@ProfilBankCustodian", _FFSForOJK.ProfilBankCustodian);
                                cmd.Parameters.AddWithValue("@AksesProspektus", _FFSForOJK.AksesProspektus);

                                cmd.Parameters.AddWithValue("@Resiko8", _FFSForOJK.Resiko8);
                                cmd.Parameters.AddWithValue("@Resiko9", _FFSForOJK.Resiko9);
                                cmd.Parameters.AddWithValue("@BitDistributedIncome", _FFSForOJK.BitDistributedIncome);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSForOJK.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FFSForOJK.FFSForOJKPK, "FFSForOJK");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FFSForOJK where FFSForOJKPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSForOJK.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _FFSForOJK.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _FFSForOJK.FundPK);
                                cmd.Parameters.AddWithValue("@TemplateType", _FFSForOJK.TemplateType);
                                cmd.Parameters.AddWithValue("@PeriodePenilaian", _FFSForOJK.PeriodePenilaian);
                                cmd.Parameters.AddWithValue("@DanaKegiatanSosial", _FFSForOJK.DanaKegiatanSosial);

                                cmd.Parameters.AddWithValue("@AkumulasiDanaCSR ", _FFSForOJK.AkumulasiDanaCSR);
                                cmd.Parameters.AddWithValue("@CSR", _FFSForOJK.CSR);
                                cmd.Parameters.AddWithValue("@Resiko1", _FFSForOJK.Resiko1);
                                cmd.Parameters.AddWithValue("@Resiko2", _FFSForOJK.Resiko2);
                                cmd.Parameters.AddWithValue("@Resiko3", _FFSForOJK.Resiko3);

                                cmd.Parameters.AddWithValue("@Resiko4", _FFSForOJK.Resiko4);
                                cmd.Parameters.AddWithValue("@Resiko5", _FFSForOJK.Resiko5);
                                cmd.Parameters.AddWithValue("@Resiko6", _FFSForOJK.Resiko6);
                                cmd.Parameters.AddWithValue("@Resiko7", _FFSForOJK.Resiko7);
                                cmd.Parameters.AddWithValue("@KlasifikasiResiko", _FFSForOJK.KlasifikasiResiko);

                                cmd.Parameters.AddWithValue("@ManajerInvestasi", _FFSForOJK.ManajerInvestasi);
                                cmd.Parameters.AddWithValue("@TujuanInvestasi", _FFSForOJK.TujuanInvestasi);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi1", _FFSForOJK.KebijakanInvestasi1);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi2", _FFSForOJK.KebijakanInvestasi2);
                                cmd.Parameters.AddWithValue("@KebijakanInvestasi3", _FFSForOJK.KebijakanInvestasi3);

                                cmd.Parameters.AddWithValue("@ProfilBankCustodian", _FFSForOJK.ProfilBankCustodian);
                                cmd.Parameters.AddWithValue("@AksesProspektus", _FFSForOJK.AksesProspektus);

                                cmd.Parameters.AddWithValue("@Resiko8", _FFSForOJK.Resiko8);
                                cmd.Parameters.AddWithValue("@Resiko9", _FFSForOJK.Resiko9);
                                cmd.Parameters.AddWithValue("@BitDistributedIncome", _FFSForOJK.BitDistributedIncome);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSForOJK.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FFSForOJK set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where FFSForOJKPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FFSForOJK.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSForOJK.HistoryPK);
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

        public void FFSForOJK_Approved(FFSForOJK _FFSForOJK)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSForOJK set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FFSForOJKPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSForOJK.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSForOJK.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FFSForOJK set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FFSForOJKPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSForOJK.ApprovedUsersID);
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

        public void FFSForOJK_Reject(FFSForOJK _FFSForOJK)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSForOJK set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FFSForOJKPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSForOJK.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSForOJK.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FFSForOJK set status= 2,LastUpdate=@LastUpdate  where FFSForOJKPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
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

        public void FFSForOJK_Void(FFSForOJK _FFSForOJK)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSForOJK set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FFSForOJKPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSForOJK.FFSForOJKPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSForOJK.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSForOJK.VoidUsersID);
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


        public string GenerateFFS_OJK(string _userID, FFSSetup_OJKRpt _FFSSetup_OJKRpt)
        {
            try
            {

                string filePath = Tools.ReportsPath + "FundFactSheet_Report_FFS" + "_" + _userID + ".xlsx";
                string pdfPath = Tools.ReportsPath + "FundFactSheet_Report_FFS" + "_" + _userID + ".pdf";
                File.Copy(Tools.ReportsTemplatePath + "FFS\\" + "Template_FFS_OJK.xlsx", filePath, true);

                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets["INPUT"];
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets["NAB"];
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets["New FFS"];
                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets["DistributedIncome"];
                    ExcelWorksheet worksheet4 = package.Workbook.Worksheets["KINERJA5TAHUN"];
                    using (SqlConnection DbCon01 = new SqlConnection(Tools.conString))
                    {
                        DbCon01.Open();
                        using (SqlCommand cmd01 = DbCon01.CreateCommand())
                        {
                            cmd01.CommandText =
                            @"  
                                if(@ClientCode = '03' and @FundPK = 30)
                                BEGIN
	                                select * from CloseNAVInfovesta where  status  = 2 and Tanggal = (
	                                SELECT MAX(Tanggal) FROM dbo.CloseNAVInfovesta WHERE Tanggal <= @Date)
                                END
                                ELSE
                                BEGIN
	                                select * from CloseNav where  FundPK  = @FundPK and status  = 2 and Date = (
	                                SELECT MAX(Date) FROM dbo.CloseNav WHERE Date <= @Date 
	                                AND fundPK = @FundPK)
                                END
                                ";

                            cmd01.CommandTimeout = 0;
                            cmd01.Parameters.AddWithValue("@Date", _FFSSetup_OJKRpt.ParamListDate);
                            cmd01.Parameters.AddWithValue("@FundPK", _FFSSetup_OJKRpt.ParamFundPK);
                            cmd01.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                            using (SqlDataReader dr01 = cmd01.ExecuteReader())
                            {
                                if (dr01.HasRows)
                                {

                                    #region NAV,AUM,UNIT
                                    // NAV,AUM,UNIT
                                    using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon1.Open();
                                        using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                        {
                                            cmd1.CommandText =

                                            @"
--declare @date datetime
--declare @FundPK int
--declare @ClientCode nvarchar(50)

--set @date = '08/31/2020'
--set @FundPK = 1
--set @ClientCode = '21'


declare @TotalBenchmark int
select @TotalBenchmark = count(distinct IndexPK) from fundindex where status = 2 and FundPK = @FundPK


Declare @PeriodPK int
Declare @BegBalance numeric(22,4)
Declare @Movement numeric(22,4)
Declare @StartOfyear datetime
Declare @CSRBalance numeric(22,0)
Declare @LastDate datetime

select @PeriodPK = PeriodPK from Period where status = 2 and @Date between DateFrom and DateTo
select @StartOfyear = DATEADD(yy, DATEDIFF(yy, 0, @Date), 0)

select @BegBalance = sum(isnull(Amount,0)) from AgentCSRBegBalance where FundPK = @FundPK and PeriodPK = @PeriodPK 

Declare @TableMovement table
(
Date datetime,FundPK int,Amount numeric(18,4)
)


DECLARE A CURSOR FOR 
select distinct EOMONTH(Date) from ZDT_WorkingDays 
where Date between DATEADD(yy, DATEDIFF(yy, 0, @Date), 0) and @Date 
Open A
Fetch Next From A
Into @LastDate       
While @@FETCH_STATUS = 0
BEGIN  

Insert into @TableMovement(Date,FundPK,Amount)
select Date,FundPK,isnull(NetDanaProgram,0) from AgentCSRDataForCommissionRpt where FundPK = @FundPK and Date = @LastDate

	
Fetch next From A Into @LastDate              
END
Close A
Deallocate A

select @Movement = sum(isnull(Amount,0)) from @TableMovement

select @CSRBalance =  isnull(@BegBalance,0) +  isnull(@Movement,0)

declare @IndexID nvarchar(50)

if(@ClientCode = '21')
BEGIN    
    select top 1 @IndexID  = Name from FundIndex A
    left join FundIndex B on A.FundPK = B.FundPK and B.status = 2
    left join [Index] C on B.IndexPK = C.IndexPK and C.status = 2
    where A.FundPK = @FundPK
END
ELSE
BEGIN
    select top 1 @IndexID  = ID from FundIndex A
    left join FundIndex B on A.FundPK = B.FundPK and B.status = 2
    left join [Index] C on B.IndexPK = C.IndexPK and C.status = 2
    where A.FundPK = @FundPK
END

if(@ClientCode = '03' and @FundPK = 30)
BEGIN                                      
	select @Date FFSDate,C.Name FundName, A.AUM AUM,Z.PertumbuhanReturn Nav,sum(UnitAmount) Unit,
	REPLACE(CONVERT(NVARCHAR,C.EffectiveDate, 106), ' ', '-') EffectiveDate,C.OJKLetter,REPLACE(CONVERT(NVARCHAR,C.IssueDate, 106), ' ', '-') IssueDate,G.ID CurrencyID,C.MinSubs,CONVERT(varchar, CAST(C.MaxUnits AS money), 1) + ' UP' MaxUnits,
	C.SubscriptionFeePercent,C.RedemptionFeePercent,C.SwitchingFeePercent,
	[dbo].[FgetmanagementfeePercentfromfundfeesetup] (@date,A.FundPK) MFeePercent,
	[dbo].FGetCustodiFeePercentFromCustodiFeeSetup  (@date,A.FundPK) CustodiFeePercent,
	C.ISIN,F.Name BankCustodian,H.PeriodePenilaian,H.DanaKegiatanSosial,round(@CSRBalance,0) AkumulasiDanaCSR,
	H.CSR,H.Resiko1,H.Resiko2,H.Resiko3,H.Resiko4,H.Resiko5,H.Resiko6,H.Resiko7,H.Resiko8,H.Resiko9,
	H.KlasifikasiResiko,H.ManajerInvestasi,H.TujuanInvestasi,H.KebijakanInvestasi1,
	H.KebijakanInvestasi2,H.KebijakanInvestasi3,H.ProfilBankCustodian,H.AksesProspektus,H.KeteranganResiko,
	H.NamaKebijakanInvestasi1,H.NamaKebijakanInvestasi2,H.NamaKebijakanInvestasi3,@IndexID IndexID,
	Case when C.Type = 1 then 'Penjaminan'
	when C.Type = 2 then 'Pendapatan Tetap'
	when C.Type = 3 then 'Pasar Uang'
		when C.Type = 4 then 'Terproteksi'
			when C.Type = 5 then 'Ekuitas'
				when C.Type = 6 then 'Indeks'
	when C.Type = 7 then 'EBA'
	when C.Type = 8 then 'KPD'
	when C.Type = 9 then 'Campuran'	
	when C.Type = 10 then 'ETF'
		when C.Type = 11 then 'DIRE'
			else 'RDPT' end FundType,@TotalBenchmark TotalBenchmark
	from CloseNav A
	left join Fund C on A.FundPK = C.FundPK and C.status = 2
	left join MasterValue D on C.Type = D.Code and D.status = 2
	left join BankBranch E on C.BankBranchPK = E.BankBranchPK and E.status = 2
	left join Bank F on E.BankPK = F.BankPK and F.status = 2
	left join Currency G on C.CurrencyPK = G.CurrencyPK and G.status = 2
	left join CloseNAVInfovesta Z on G.status = 2 and Z.Tanggal  = (
	SELECT MAX(Tanggal) FROM dbo.CloseNAVInfovesta WHERE Tanggal <= @Date 
	and status = 2
	)
	left join FundClientPosition B on A.FundPK = B.FundPK and B.Date  = (
	SELECT MAX(Date) FROM dbo.FundClientPosition WHERE Date < @Date 
	AND fundPK = @FundPK
	)
	left join FFSForOJK H on A.FundPK = H.FundPK and H.Date  = (
	SELECT MAX(Date) FROM dbo.FFSForOJK WHERE Date <= @Date 
	AND fundPK = @FundPK  and status = 2
	) and H.status = 2
	where  A.FundPK  = @FundPK and A.status  = 2 
	and A.Date  = (
	SELECT MAX(Date) FROM dbo.CloseNav WHERE Date <= @Date 
	AND fundPK = @FundPK and status = 2
	)
	Group By A.AUM,A.NAV ,C.EffectiveDate,C.Type,F.Name,C.OJKLetter,C.IssueDate,G.ID,
	C.MinSubs,C.MaxUnits,C.SubscriptionFeePercent,C.RedemptionFeePercent,C.SwitchingFeePercent,
	A.FundPK,C.ISIN,H.PeriodePenilaian,H.DanaKegiatanSosial,H.AkumulasiDanaCSR,
	H.CSR,H.Resiko1,H.Resiko2,H.Resiko3,H.Resiko4,H.Resiko5,H.Resiko6,H.Resiko7,
	H.KlasifikasiResiko,H.ManajerInvestasi,H.TujuanInvestasi,KebijakanInvestasi1,
	H.KebijakanInvestasi2,H.KebijakanInvestasi3,H.ProfilBankCustodian,H.AksesProspektus,C.Name,H.KeteranganResiko,
	H.NamaKebijakanInvestasi1,H.NamaKebijakanInvestasi2,H.NamaKebijakanInvestasi3,Z.PertumbuhanReturn,H.Resiko8,H.Resiko9

END
ELSE
BEGIN
                                       
	select @Date FFSDate,C.Name FundName, A.AUM AUM,A.Nav Nav,sum(UnitAmount) Unit,
	REPLACE(CONVERT(NVARCHAR,C.EffectiveDate, 106), ' ', '-') EffectiveDate,C.OJKLetter,REPLACE(CONVERT(NVARCHAR,C.IssueDate, 106), ' ', '-') IssueDate,G.ID CurrencyID,C.MinSubs,CONVERT(varchar, CAST(C.MaxUnits AS money), 1) + ' UP' MaxUnits,
	C.SubscriptionFeePercent,C.RedemptionFeePercent,C.SwitchingFeePercent,
	case when @ClientCode = '21' then C.ManagementFeePercent else [dbo].[FgetmanagementfeePercentfromfundfeesetup] (@date,A.FundPK) end MFeePercent,
	[dbo].FGetCustodiFeePercentFromCustodiFeeSetup  (@date,A.FundPK) CustodiFeePercent,
	C.ISIN,F.Name BankCustodian,H.PeriodePenilaian,H.DanaKegiatanSosial,round(@CSRBalance,0) AkumulasiDanaCSR,
	H.CSR,H.Resiko1,H.Resiko2,H.Resiko3,H.Resiko4,H.Resiko5,H.Resiko6,H.Resiko7,H.Resiko8,H.Resiko9,
	H.KlasifikasiResiko,H.ManajerInvestasi,H.TujuanInvestasi,H.KebijakanInvestasi1,
	H.KebijakanInvestasi2,H.KebijakanInvestasi3,H.ProfilBankCustodian,H.AksesProspektus,H.KeteranganResiko,
	H.NamaKebijakanInvestasi1,H.NamaKebijakanInvestasi2,H.NamaKebijakanInvestasi3,@IndexID IndexID,
	Case when C.Type = 1 then 'Penjaminan'
	when C.Type = 2 then 'Pendapatan Tetap'
	when C.Type = 3 then 'Pasar Uang'
		when C.Type = 4 then 'Terproteksi'
			when C.Type = 5 then 'Ekuitas'
				when C.Type = 6 then 'Indeks'
	when C.Type = 7 then 'EBA'
	when C.Type = 8 then 'KPD'
	when C.Type = 9 then 'Campuran'	
	when C.Type = 10 then 'ETF'
		when C.Type = 11 then 'DIRE'
			else 'RDPT' end FundType,@TotalBenchmark TotalBenchmark
	from CloseNav A
	left join Fund C on A.FundPK = C.FundPK and C.status = 2
	left join MasterValue D on C.Type = D.Code and D.status = 2
	left join BankBranch E on C.BankBranchPK = E.BankBranchPK and E.status = 2
	left join Bank F on E.BankPK = F.BankPK and F.status = 2
	left join Currency G on C.CurrencyPK = G.CurrencyPK and G.status = 2
	left join FundClientPosition B on A.FundPK = B.FundPK and B.Date  = (
	SELECT MAX(Date) FROM dbo.FundClientPosition WHERE Date < @Date 
	AND fundPK = @FundPK
	)
	left join FFSForOJK H on A.FundPK = H.FundPK and H.Date  = (
	SELECT MAX(Date) FROM dbo.FFSForOJK WHERE Date <= @Date 
	AND fundPK = @FundPK  and status = 2
	) and H.status = 2
	where  A.FundPK  = @FundPK and A.status  = 2 
	and A.Date  = (
	SELECT MAX(Date) FROM dbo.CloseNav WHERE Date <= @Date 
	AND fundPK = @FundPK and status = 2
	)
	Group By A.AUM,A.NAV ,C.EffectiveDate,C.Type,F.Name,C.OJKLetter,C.IssueDate,G.ID,
	C.MinSubs,C.MaxUnits,C.SubscriptionFeePercent,C.RedemptionFeePercent,C.SwitchingFeePercent,
	A.FundPK,C.ISIN,H.PeriodePenilaian,H.DanaKegiatanSosial,H.AkumulasiDanaCSR,
	H.CSR,H.Resiko1,H.Resiko2,H.Resiko3,H.Resiko4,H.Resiko5,H.Resiko6,H.Resiko7,
	H.KlasifikasiResiko,H.ManajerInvestasi,H.TujuanInvestasi,KebijakanInvestasi1,
	H.KebijakanInvestasi2,H.KebijakanInvestasi3,H.ProfilBankCustodian,H.AksesProspektus,C.Name,H.KeteranganResiko,
	H.NamaKebijakanInvestasi1,H.NamaKebijakanInvestasi2,H.NamaKebijakanInvestasi3,H.Resiko8,H.Resiko9,C.ManagementFeePercent

END
         
                                            ";

                                            cmd1.CommandTimeout = 0;
                                            cmd1.Parameters.AddWithValue("@Date", _FFSSetup_OJKRpt.ParamListDate);
                                            cmd1.Parameters.AddWithValue("@FundPK", _FFSSetup_OJKRpt.ParamFundPK);
                                            cmd1.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                                            cmd1.ExecuteNonQuery();


                                            using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_OJKRpt> rList1 = new List<FFSSetup_OJKRpt>();
                                                while (dr1.Read())
                                                {
                                                    FFSSetup_OJKRpt rSingle1 = new FFSSetup_OJKRpt();
                                                    rSingle1.FFSDate = Convert.ToDateTime(dr1["FFSDate"]);
                                                    rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                    rSingle1.PeriodePenilaian = Convert.ToString(dr1["PeriodePenilaian"]);
                                                    rSingle1.DanaKegiatanSosial = Convert.ToString(dr1["DanaKegiatanSosial"]);
                                                    rSingle1.AkumulasiDanaCSR = Convert.ToString(dr1["AkumulasiDanaCSR"]);
                                                    rSingle1.CSR = Convert.ToString(dr1["CSR"]);
                                                    rSingle1.Resiko1 = Convert.ToString(dr1["Resiko1"]);
                                                    rSingle1.Resiko2 = Convert.ToString(dr1["Resiko2"]);
                                                    rSingle1.Resiko3 = Convert.ToString(dr1["Resiko3"]);
                                                    rSingle1.Resiko4 = Convert.ToString(dr1["Resiko4"]);
                                                    rSingle1.Resiko5 = Convert.ToString(dr1["Resiko5"]);
                                                    rSingle1.Resiko6 = Convert.ToString(dr1["Resiko6"]);
                                                    rSingle1.Resiko7 = Convert.ToString(dr1["Resiko7"]);
                                                    rSingle1.Resiko8 = Convert.ToString(dr1["Resiko8"]);
                                                    rSingle1.Resiko9 = Convert.ToString(dr1["Resiko9"]);
                                                    rSingle1.KlasifikasiResiko = Convert.ToInt32(dr1["KlasifikasiResiko"]);
                                                    rSingle1.ManajerInvestasi = Convert.ToString(dr1["ManajerInvestasi"]);
                                                    rSingle1.TujuanInvestasi = Convert.ToString(dr1["TujuanInvestasi"]);
                                                    rSingle1.NamaKebijakanInvestasi1 = Convert.ToString(dr1["NamaKebijakanInvestasi1"]);
                                                    rSingle1.NamaKebijakanInvestasi2 = Convert.ToString(dr1["NamaKebijakanInvestasi2"]);
                                                    rSingle1.NamaKebijakanInvestasi3 = Convert.ToString(dr1["NamaKebijakanInvestasi3"]);
                                                    rSingle1.KebijakanInvestasi1 = Convert.ToString(dr1["KebijakanInvestasi1"]);
                                                    rSingle1.KebijakanInvestasi2 = Convert.ToString(dr1["KebijakanInvestasi2"]);
                                                    rSingle1.KebijakanInvestasi3 = Convert.ToString(dr1["KebijakanInvestasi3"]);
                                                    rSingle1.ProfilBankCustodian = Convert.ToString(dr1["ProfilBankCustodian"]);
                                                    rSingle1.AksesProspektus = Convert.ToString(dr1["AksesProspektus"]);
                                                    rSingle1.KeteranganResiko = Convert.ToString(dr1["KeteranganResiko"]);
                                                    rSingle1.AUM = Convert.ToDecimal(dr1["AUM"]);
                                                    rSingle1.Nav = Convert.ToDecimal(dr1["Nav"]);
                                                    rSingle1.Unit = Convert.ToDecimal(dr1["Unit"]);
                                                    rSingle1.EffectiveDate = Convert.ToString(dr1["EffectiveDate"]);
                                                    rSingle1.OJKLetter = Convert.ToString(dr1["OJKLetter"]);
                                                    rSingle1.IssueDate = Convert.ToString(dr1["IssueDate"]);
                                                    rSingle1.CurrencyID = Convert.ToString(dr1["CurrencyID"]);
                                                    rSingle1.MinSubs = Convert.ToDecimal(dr1["MinSubs"]);
                                                    rSingle1.MaxUnits = Convert.ToString(dr1["MaxUnits"]);
                                                    rSingle1.SubscriptionFeePercent = Convert.ToDecimal(dr1["SubscriptionFeePercent"]);
                                                    rSingle1.RedemptionFeePercent = Convert.ToDecimal(dr1["RedemptionFeePercent"]);
                                                    rSingle1.SwitchingFeePercent = Convert.ToDecimal(dr1["SwitchingFeePercent"]);
                                                    rSingle1.MFeePercent = Convert.ToDecimal(dr1["MFeePercent"]);
                                                    rSingle1.CustodiFeePercent = Convert.ToDecimal(dr1["CustodiFeePercent"]);
                                                    rSingle1.ISIN = Convert.ToString(dr1["ISIN"]);
                                                    rSingle1.BankCustodian = Convert.ToString(dr1["BankCustodian"]);
                                                    rSingle1.FundType = Convert.ToString(dr1["FundType"]);
                                                    rSingle1.IndexID = Convert.ToString(dr1["IndexID"]);
                                                    rSingle1.TotalBenchmark = Convert.ToInt32(dr1["TotalBenchmark"]);
                                                    rList1.Add(rSingle1);

                                                }


                                                var QueryByFundID1 =
                                                    from r1 in rList1
                                                    group r1 by new { } into rGroup1
                                                    select rGroup1;



                                                foreach (var rsHeader1 in QueryByFundID1)
                                                {

                                                    foreach (var rsDetail1 in rsHeader1)
                                                    {
                                                        worksheet.Cells[1, 2].Value = rsDetail1.FundName;
                                                        worksheet.Cells[1, 3].Value = rsDetail1.TotalBenchmark;
                                                        worksheet.Cells[1, 5].Value = rsDetail1.FundType;
                                                        worksheet.Cells[2, 5].Value = rsDetail1.FFSDate;
                                                        worksheet.Cells[2, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        worksheet.Cells[2, 2].Value = rsDetail1.Nav;
                                                        worksheet.Cells[13, 2].Value = rsDetail1.EffectiveDate;
                                                        worksheet.Cells[13, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        worksheet.Cells[14, 2].Value = rsDetail1.OJKLetter;
                                                        worksheet.Cells[15, 2].Value = rsDetail1.IssueDate;
                                                        worksheet.Cells[15, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                        worksheet.Cells[16, 2].Value = rsDetail1.CurrencyID;
                                                        worksheet.Cells[17, 2].Value = rsDetail1.Nav;
                                                        worksheet.Cells[18, 2].Value = rsDetail1.AUM;
                                                        worksheet.Cells[19, 2].Value = rsDetail1.MinSubs;
                                                        worksheet.Cells[20, 2].Value = rsDetail1.MaxUnits;
                                                        worksheet.Cells[21, 2].Value = rsDetail1.SubscriptionFeePercent;
                                                        worksheet.Cells[22, 2].Value = rsDetail1.RedemptionFeePercent;
                                                        worksheet.Cells[23, 2].Value = rsDetail1.SwitchingFeePercent;
                                                        worksheet.Cells[24, 2].Value = rsDetail1.MFeePercent;
                                                        worksheet.Cells[25, 2].Value = rsDetail1.CustodiFeePercent;
                                                        worksheet.Cells[26, 2].Value = rsDetail1.BankCustodian;
                                                        worksheet.Cells[27, 2].Value = rsDetail1.ISIN;


                                                        worksheet.Cells[32, 2].Value = rsDetail1.PeriodePenilaian;
                                                        worksheet.Cells[33, 2].Value = rsDetail1.DanaKegiatanSosial;
                                                        worksheet.Cells[34, 2].Value = rsDetail1.AkumulasiDanaCSR;
                                                        worksheet.Cells[35, 2].Value = rsDetail1.CSR;
                                                        worksheet.Cells[36, 2].Value = rsDetail1.Resiko1;
                                                        worksheet.Cells[37, 2].Value = rsDetail1.Resiko2;
                                                        worksheet.Cells[38, 2].Value = rsDetail1.Resiko3;
                                                        worksheet.Cells[39, 2].Value = rsDetail1.Resiko4;
                                                        worksheet.Cells[40, 2].Value = rsDetail1.Resiko5;
                                                        worksheet.Cells[41, 2].Value = rsDetail1.Resiko6;
                                                        worksheet.Cells[42, 2].Value = rsDetail1.Resiko7;
                                                        worksheet.Cells[43, 2].Value = rsDetail1.KlasifikasiResiko;
                                                        worksheet.Cells[44, 2].Value = rsDetail1.ManajerInvestasi;
                                                        worksheet.Cells[45, 2].Value = rsDetail1.TujuanInvestasi;
                                                        worksheet.Cells[46, 2].Value = rsDetail1.KebijakanInvestasi1;
                                                        worksheet.Cells[47, 2].Value = rsDetail1.KebijakanInvestasi2;
                                                        worksheet.Cells[48, 2].Value = rsDetail1.KebijakanInvestasi3;
                                                        worksheet.Cells[49, 2].Value = rsDetail1.ProfilBankCustodian;
                                                        worksheet.Cells[50, 2].Value = rsDetail1.AksesProspektus;
                                                        worksheet.Cells[51, 2].Value = rsDetail1.KeteranganResiko;
                                                        worksheet.Cells[52, 2].Value = rsDetail1.Resiko8;
                                                        worksheet.Cells[53, 2].Value = rsDetail1.Resiko9;

                                                        worksheet.Cells[59, 2].Value = rsDetail1.NamaKebijakanInvestasi1;
                                                        worksheet.Cells[60, 2].Value = rsDetail1.NamaKebijakanInvestasi2;
                                                        worksheet.Cells[61, 2].Value = rsDetail1.NamaKebijakanInvestasi3;

                                                        worksheet.Cells[30, 1].Value = rsDetail1.IndexID;

                                                        if (rsDetail1.KlasifikasiResiko == 1)
                                                        {

                                                            Image img = Image.FromFile(Tools.ReportImageFFS_OJK1);
                                                            ExcelPicture pic = worksheet2.Drawings.AddPicture("", img);
                                                            pic.SetPosition(35, 0, 1, 0);
                                                        }
                                                        else if (rsDetail1.KlasifikasiResiko == 2)
                                                        {

                                                            Image img = Image.FromFile(Tools.ReportImageFFS_OJK2);
                                                            ExcelPicture pic = worksheet2.Drawings.AddPicture("", img);
                                                            pic.SetPosition(35, 0, 1, 0);
                                                        }
                                                        else if (rsDetail1.KlasifikasiResiko == 3)
                                                        {

                                                            Image img = Image.FromFile(Tools.ReportImageFFS_OJK3);
                                                            ExcelPicture pic = worksheet2.Drawings.AddPicture("", img);
                                                            pic.SetPosition(35, 0, 1, 0);
                                                        }
                                                        else if (rsDetail1.KlasifikasiResiko == 4)
                                                        {

                                                            Image img = Image.FromFile(Tools.ReportImageFFS_OJK4);
                                                            ExcelPicture pic = worksheet2.Drawings.AddPicture("", img);
                                                            pic.SetPosition(35, 0, 1, 0);
                                                        }
                                                        else
                                                        {

                                                            Image img = Image.FromFile(Tools.ReportImageFFS_OJK5);
                                                            ExcelPicture pic = worksheet2.Drawings.AddPicture("", img);
                                                            pic.SetPosition(35, 0, 1, 0);
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }

                                    #endregion

                                    #region Allocation of Investment
                                    // Allocation of Investment
                                    using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon2.Open();
                                        using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                        {
                                            if (Tools.ClientCode == "29")
                                            {
                                                cmd2.CommandText =

                                           @"
                                                                 
                                            
                                            --declare @date datetime
                                            --declare @fundpk int
                                            --set @date = '08/01/2020'
                                            --set @fundpk = 1

                                            declare @A Table
                                            (
	                                            InstrumentType nvarchar(50),
	                                            ExposurePercent numeric(22,8)
                                            )

                                            insert into @A
                                            select 'Equities' InstrumentType, Equities ExposurePercent from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                            where B.FundPK = @FundPK and  A.Date = 
                                            (select max(date) from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                             where date <= @Date and B.FundPK = @FundPK and status  = 2) and A.Equities > 0

                                            insert into @A
                                            select 'FixedIncome' InstrumentType, Equities ExposurePercent from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                            where B.FundPK = @FundPK and  A.Date = 
                                            (select max(date) from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                             where date <= @Date and B.FundPK = @FundPK and status  = 2) and A.FixedIncome > 0

                                            insert into @A
                                            select 'OtherInvestment' InstrumentType, Equities ExposurePercent from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                            where B.FundPK = @FundPK and  A.Date = 
                                            (select max(date) from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                             where date <= @Date and B.FundPK = @FundPK and status  = 2) and A.OtherInvestment > 0

                                            insert into @A
                                            select 'MoneyMarket' InstrumentType, Equities ExposurePercent from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                            where B.FundPK = @FundPK and  A.Date = 
                                            (select max(date) from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                             where date <= @Date and B.FundPK = @FundPK and status  = 2) and A.MoneyMarket > 0

                                            insert into @A
                                            select 'Cash' InstrumentType, Equities ExposurePercent from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                            where B.FundPK = @FundPK and  A.Date = 
                                            (select max(date) from FFSPortfolioAllocation A
                                            left join Fund B on A.FundCode = B.InternalCode and B.status = 2
                                             where date <= @Date and B.FundPK = @FundPK and status  = 2) and A.Cash > 0

                                             select * from  @A
                                             ";

                                            }
                                            else
                                            {
                                                cmd2.CommandText =

                                           @"
                                                                 
                                            declare @MV Table
                                            (
                                            Date datetime,
                                            FundPK int,
                                            MarketValue numeric(22,2)
                                            )
	
                                            declare @A Table
                                            (
                                            InstrumentType nvarchar(50),
                                            ExposurePercent numeric(22,8)
                                            )

                                            declare @Sum numeric(22,8)


                                            INSERT INTO @MV
                                            SELECT Date,FundPK,sum(AUM) CloseNAV FROM dbo.CloseNAV WHERE Date = 
                                            (
                                            select max(date) from CloseNAV  where date <= @Date and FundPK = @FundPK and status  = 2
                                            ) AND status = 2 AND FundPK = @FundPK
                                            group by Date,FundPK


                                            insert into @A
                                            select case when D.DescOne = 'BOND/FIXED INCOME' then 'Efek Utang' 
                                            else case when D.DescOne = 'EQUITY/SAHAM' then 'Efek Saham' else 'Pasar Uang' end end InstrumentType ,
                                            isnull(round(cast(sum(A.MarketValue/E.MarketValue) as numeric(22,8)),4),0) ExposurePercent  from FundPosition A
                                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                                            left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status in (1,2)  
                                            and A.FundPK  = @FundPK and A.status  = 2 and A.Date = 
                                            (select max(date) from FundPosition  where date <= @Date and FundPK = @FundPK and status  = 2)
                                            left join MasterValue D on C.GroupType = D.Code and D.status in (1,2) and D.ID = 'InstrumentGroupType'
                                            left join @MV E on A.FundPK = E.FundPK and A.Date = E.Date
                                            where D.DescOne is not null and B.InstrumentTypePK <> 5
                                            group by D.DescOne 

                                            select @Sum = sum(ExposurePercent) from @A

                                            IF (@Sum > 1)
                                            BEGIN
	                                            select InstrumentType,1 ExposurePercent from @A
                                            END
                                            ELSE
                                            BEGIN
	                                            if exists(select * from @A)
	                                            BEGIN
		                                            select * from @A
		                                            union all
		                                            select 'Pasar Uang',1 - sum(ExposurePercent) from @A 
	                                            END
	                                            ELSE
	                                            BEGIN
		                                            select 'Pasar Uang' InstrumentType,1 ExposurePercent
	                                            END
                                            END ";

                                            }

                                            cmd2.CommandTimeout = 0;
                                            cmd2.Parameters.AddWithValue("@Date", _FFSSetup_OJKRpt.ParamListDate);
                                            cmd2.Parameters.AddWithValue("@FundPK", _FFSSetup_OJKRpt.ParamFundPK);

                                            cmd2.ExecuteNonQuery();


                                            using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_OJKRpt> rList2 = new List<FFSSetup_OJKRpt>();
                                                while (dr2.Read())
                                                {
                                                    FFSSetup_OJKRpt rSingle2 = new FFSSetup_OJKRpt();
                                                    rSingle2.InstrumentType = Convert.ToString(dr2["InstrumentType"]);
                                                    rSingle2.ExposurePercent = Convert.ToDecimal(dr2["ExposurePercent"]);
                                                    rList2.Add(rSingle2);

                                                }

                                                var QueryByFundID2 =
                                                    from r2 in rList2
                                                    group r2 by new { } into rGroup2
                                                    select rGroup2;

                                                foreach (var rsHeader2 in QueryByFundID2)
                                                {
                                                    int incRowExcel = 13;
                                                    foreach (var rsDetail2 in rsHeader2)
                                                    {
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail2.InstrumentType;
                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail2.ExposurePercent;
                                                        incRowExcel++;
                                                    }
                                                }

                                            }
                                        }
                                    }

                                    #endregion

                                    #region Top 10 Holding
                                    // Top 5 Holding
                                    using (SqlConnection DbCon3 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon3.Open();
                                        using (SqlCommand cmd3 = DbCon3.CreateCommand())
                                        {
                                            cmd3.CommandText =

                                            @"
                                            declare @A table
                                            (
                                            InstrumentID nvarchar(100), MV numeric(22,2),InstrumentTypePK int
                                            )					


                                            if(@ClientCode = '03')
                                            BEGIN
	                                            insert into @A	
	                                            select top 10 InstrumentID,MV,InstrumentTypePK from (																								
                                                select  case when B.InstrumentTypePK <> 5 then A.InstrumentPK else A.BankPK end InstrumentPK,
                                                case when B.InstrumentTypePK <> 5 then D.ID + ' (' + B.ID + ')'  else D.ID + ' (' + C.ID + ')' end InstrumentID, 
                                                sum(MarketValue) MV,B.InstrumentTypePK from FundPosition A
                                                left join Instrument B on A.InstrumentPK =  B.InstrumentPK and B.status in (1,2)
                                                left join Bank C on B.BankPK =  C.BankPK and C.status in (1,2)
                                                left join Issuer D on case when B.InstrumentTypePK <> 5 then B.IssuerPK else C.IssuerPK end =  D.IssuerPK and D.status in (1,2)
                                                where A.FundPK  = @FundPK and A.status  = 2 and A.Date = 
                                                (select max(date) from FundPosition  where date <= @Date and FundPK = @FundPK and status  = 2) --and B.InstrumentTypePK = 5
                                                group by A.BankPK,C.ID,B.InstrumentTypePK,A.InstrumentPK,B.ID,D.ID
                                                ) E 
                                                order by MV Desc

                                            select InstrumentID from @A
                                            order by InstrumentID asc


                                            END
                                            if(@ClientCode = '29')
                                            BEGIN
	                                            select top 10 SecuritiesName InstrumentID from FFSTopHolding
	                                            order by Percentage desc
                                            END
                                            ELSE
                                            BEGIN
	                                            insert into @A	
	                                            select top 10 InstrumentID,MV,InstrumentTypePK from (																								
	                                            select  A.InstrumentPK,B.Name InstrumentID, sum(MarketValue) MV,B.InstrumentTypePK from FundPosition A
	                                            left join Instrument B on A.InstrumentPK =  B.InstrumentPK and B.status in (1,2)
	                                            where A.FundPK  = @FundPK and A.status  = 2 and A.Date = 
	                                            (select max(date) from FundPosition  where date <= @Date and FundPK = @FundPK and status  = 2) and B.InstrumentTypePK <> 5
	                                            group by A.InstrumentPK,B.Name,B.InstrumentTypePK
	                                            union all
	                                            select  A.BankPK,C.ID, sum(MarketValue) MV,B.InstrumentTypePK from FundPosition A
	                                            left join Instrument B on A.InstrumentPK =  B.InstrumentPK and B.status in (1,2)
	                                            left join Bank C on B.BankPK =  C.BankPK and C.status in (1,2)
	                                            where A.FundPK  = @FundPK and A.status  = 2 and A.Date = 
	                                            (select max(date) from FundPosition  where date <= @Date and FundPK = @FundPK and status  = 2) and B.InstrumentTypePK = 5
	                                            group by A.BankPK,C.ID,B.InstrumentTypePK
	                                            ) C
	                                            order by MV Desc

                                            select case when InstrumentTypePK = 5 then 'TD ' + InstrumentID else InstrumentID end InstrumentID from @A
                                            order by InstrumentID asc

                                            END


                                             ";

                                            cmd3.CommandTimeout = 0;
                                            cmd3.Parameters.AddWithValue("@Date", _FFSSetup_OJKRpt.ParamListDate);
                                            cmd3.Parameters.AddWithValue("@FundPK", _FFSSetup_OJKRpt.ParamFundPK);
                                            cmd3.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                                            cmd3.ExecuteNonQuery();


                                            using (SqlDataReader dr3 = cmd3.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_OJKRpt> rList3 = new List<FFSSetup_OJKRpt>();
                                                while (dr3.Read())
                                                {
                                                    FFSSetup_OJKRpt rSingle3 = new FFSSetup_OJKRpt();
                                                    rSingle3.InstrumentID = Convert.ToString(dr3["InstrumentID"]);
                                                    rList3.Add(rSingle3);

                                                }


                                                var QueryByFundID3 =
                                                    from r3 in rList3
                                                    group r3 by new { } into rGroup3
                                                    select rGroup3;

                                                foreach (var rsHeader3 in QueryByFundID3)
                                                {
                                                    int incRowExcel = 13;
                                                    foreach (var rsDetail3 in rsHeader3)
                                                    {
                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail3.InstrumentID;
                                                        incRowExcel++;
                                                    }
                                                }

                                            }
                                        }
                                    }

                                    #endregion

                                    #region Nav and Benchmark
                                    // Nav and Benchmark
                                    using (SqlConnection DbCon4 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon4.Open();
                                        using (SqlCommand cmd4 = DbCon4.CreateCommand())
                                        {
                                            cmd4.CommandText =

                                            @"
                                           
                                            --declare @date datetime
                                            --declare @FundPK int

                                            --set @date = '07/30/2020'
                                            --set @FundPK = 102


                                         Declare @SinceDate datetime
                                            Declare @IndexPK int

                                            select @SinceDate = IssueDate, @IndexPK = isnull(B.IndexPK,0) from Fund A
                                            left join FundIndex B on A.FundPK = B.FundPK and B.status in (1,2)
                                            where A.FundPK = @FundPK and A.status in (1,2) and MaturityDate >= @Date


                                            DECLARE @FFSFund TABLE 
                                            (
                                            Date DATETIME,
                                            FundPK INT
                                            )



                                            DECLARE @Result TABLE
                                            (
                                            Date DATETIME,
                                            FundPK INT,
                                            AUM numeric(32,2),
                                            Nav numeric(22,8),
                                            RateIndex  numeric(22,8)
                                            )



                                            INSERT INTO @FFSFund(Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  and A.Date = @SinceDate

                                            if (datediff(month,@SinceDate,@date) < 3)
                                            BEGIN
                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  
                                            END
                                            else if (datediff(month,@SinceDate,@date) between 3 and 6)
                                            BEGIN
                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  and A.Date = DATEADD(wk, DATEDIFF(wk,0,A.date), 0) and A.Date <> @date
	
                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  and A.Date = @date
                                            END
                                            else
                                            BEGIN
                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
	                                        AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  and A.Date = case when dbo.CheckTodayIsHoliday(EOMONTH(A.Date)) = 1 then dbo.fworkingday(EOMONTH(A.Date),-1) else EOMONTH(A.Date) end --and (EOMONTH(A.Date) <> EOMONTH(@SinceDate))
                                            END

                                            declare @FirstBenchmarkIndex numeric(18,6)
                                            select top 1 @FirstBenchmarkIndex = CloseInd from BenchmarkIndex where status = 2 and IndexPK = @IndexPK
                                            order by date asc


                                            
                                            if(@ClientCode = '03' and @FundPK = 30)
                                            BEGIN  
	                                            INSERT INTO @Result
	                                            ( Date, FundPK, AUM, Nav, RateIndex )
		
	                                            SELECT  A.Date,A.FundPK,isnull(B.AUM,0),isnull(D.PertumbuhanReturn,0),case when C.IndexPK is null then isnull(@FirstBenchmarkIndex,0) else isnull(C.CloseInd,0) end  from @FFSFund A
	                                            left join [CloseNAV] B  on A.FundPK = B.FundPK and B.status = 2       
	                                            and B.DATE = (select max(date) From CloseNAV where date <= A.Date and FundPK = @FundPK and status = 2 and Nav > 1)  
	                                            and A.FundPK= @FundPK   and status = 2  
	                                            left join BenchmarkIndex C on C.status = 2       
	                                            and C.DATE = (select max(date) From BenchmarkIndex where date <= A.Date and IndexPK = @IndexPK and status = 2 and CloseInd > 0)  
	                                            and C.IndexPK= @IndexPK
	                                            left join CloseNAVInfovesta D  on D.status = 2 and  D.Tanggal = (select max(Tanggal) From CloseNAVInfovesta where Tanggal <= A.Date and status = 2 and PertumbuhanReturn > 0)    
                                            END
                                            ELSE
                                            BEGIN
	                                            INSERT INTO @Result
	                                            ( Date, FundPK, AUM, Nav, RateIndex )
		
                                                SELECT  A.Date,A.FundPK,isnull(B.AUM,0),isnull(B.Nav,0),case when C.IndexPK is null then isnull(@FirstBenchmarkIndex,0) else isnull(C.CloseInd,0) end  from @FFSFund A
                                                left join [CloseNAV] B  on A.FundPK = B.FundPK and B.status = 2       
                                                and B.DATE = (select max(date) From CloseNAV where date <= A.Date and FundPK = @FundPK and status = 2 and Nav > 1)  
                                                and A.FundPK= @FundPK   and status = 2  
                                                left join BenchmarkIndex C on C.status = 2       
                                                and C.DATE = (select max(date) From BenchmarkIndex where date <= A.Date and IndexPK = @IndexPK and status = 2 and CloseInd > 0)  
                                                and C.IndexPK= @IndexPK
 
                                            END




                                            select distinct Date NavDate,FundPK,AUM,Nav,RateIndex from @Result ";

                                            cmd4.CommandTimeout = 0;
                                            cmd4.Parameters.AddWithValue("@Date", _FFSSetup_OJKRpt.ParamListDate);
                                            cmd4.Parameters.AddWithValue("@FundPK", _FFSSetup_OJKRpt.ParamFundPK);
                                            cmd4.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                                            cmd4.ExecuteNonQuery();


                                            using (SqlDataReader dr4 = cmd4.ExecuteReader())
                                            {

                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                //ATUR DATA GROUPINGNYA DULU
                                                List<FFSSetup_OJKRpt> rList4 = new List<FFSSetup_OJKRpt>();
                                                while (dr4.Read())
                                                {
                                                    FFSSetup_OJKRpt rSingle4 = new FFSSetup_OJKRpt();
                                                    rSingle4.NavDate = Convert.ToDateTime(dr4["NavDate"]);
                                                    rSingle4.AUM = Convert.ToDecimal(dr4["AUM"]);
                                                    rSingle4.Nav = Convert.ToDecimal(dr4["Nav"]);
                                                    rSingle4.RateIndex = Convert.ToDecimal(dr4["RateIndex"]);
                                                    rList4.Add(rSingle4);

                                                }


                                                var QueryByFundID4 =
                                                    from r4 in rList4
                                                    group r4 by new { } into rGroup4
                                                    select rGroup4;

                                                foreach (var rsHeader4 in QueryByFundID4)
                                                {
                                                    int incRowExcel = 1;
                                                    foreach (var rsDetail4 in rsHeader4)
                                                    {
                                                        worksheet1.Cells[incRowExcel, 1].Value = rsDetail4.NavDate;
                                                        worksheet1.Cells[incRowExcel, 2].Value = rsDetail4.Nav;
                                                        worksheet1.Cells[incRowExcel, 11].Value = rsDetail4.AUM;
                                                        worksheet1.Cells[incRowExcel, 14].Value = rsDetail4.NavDate;
                                                        worksheet1.Cells[incRowExcel, 15].Value = rsDetail4.RateIndex;
                                                        incRowExcel++;
                                                    }
                                                }

                                            }
                                        }
                                    }

                                    #endregion


                                    #region Distributed Income -- Sementara Custom 21 

                                    if (Tools.ClientCode == "21")
                                    {
                                        // Distributed Income
                                        using (SqlConnection DbCon5 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon5.Open();
                                            using (SqlCommand cmd5 = DbCon5.CreateCommand())
                                            {
                                                cmd5.CommandText =

                                                @"
                                                select case when dbo.CheckTodayIsHoliday(EOMONTH(ExDate)) = 1 then dbo.FWorkingDay(EOMONTH(ExDate),-1) else EOMONTH(ExDate) end Date,sum(CashAmount) CashAmount,sum(DistributedIncomePerUnit) Unit from DistributedIncome 
                                                where status  = 2 and Posted = 1 and FundPK = @FundPK and ValueDate <= @Date 
                                                and FundPK = 
                                                ( 
	                                                select FundPK from FFSForOJK where status = 2 and FundPK = @FundPK and BitDistributedIncome = 1
	                                                and Date = 
	                                                (
		                                                select max(date) from FFSForOJK where status = 2 and FundPK = @FundPK and Date <= @Date
	                                                )
                                                )
                                                group by EOMONTH(ExDate)
                                                 ";

                                                cmd5.CommandTimeout = 0;
                                                cmd5.Parameters.AddWithValue("@Date", _FFSSetup_OJKRpt.ParamListDate);
                                                cmd5.Parameters.AddWithValue("@FundPK", _FFSSetup_OJKRpt.ParamFundPK);
                                                cmd5.ExecuteNonQuery();


                                                using (SqlDataReader dr5 = cmd5.ExecuteReader())
                                                {

                                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<FFSSetup_OJKRpt> rList5 = new List<FFSSetup_OJKRpt>();
                                                    while (dr5.Read())
                                                    {
                                                        FFSSetup_OJKRpt rSingle5 = new FFSSetup_OJKRpt();
                                                        rSingle5.DistributedDate = Convert.ToDateTime(dr5["Date"]);
                                                        rSingle5.CashAmount = Convert.ToDecimal(dr5["CashAmount"]);
                                                        rSingle5.Unit = Convert.ToDecimal(dr5["Unit"]);
                                                        rList5.Add(rSingle5);

                                                    }


                                                    var QueryByFundID5 =
                                                        from r5 in rList5
                                                        group r5 by new { } into rGroup5
                                                        select rGroup5;

                                                    foreach (var rsHeader5 in QueryByFundID5)
                                                    {
                                                        int incRowExcel = 2;
                                                        foreach (var rsDetail5 in rsHeader5)
                                                        {
                                                            worksheet3.Cells[incRowExcel, 1].Value = rsDetail5.DistributedDate;
                                                            worksheet3.Cells[incRowExcel, 1].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                            worksheet3.Cells[incRowExcel, 2].Value = rsDetail5.CashAmount;
                                                            worksheet3.Cells[incRowExcel, 3].Value = rsDetail5.Unit;
                                                            incRowExcel++;
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }


                                    #endregion



                                    #region KINERJA 5 TAHUN -- Sementara Custom 21
                                    if (Tools.ClientCode == "21")
                                    {
                                        // Nav and Benchmark
                                        using (SqlConnection DbCon6 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon6.Open();
                                            using (SqlCommand cmd6 = DbCon6.CreateCommand())
                                            {
                                                cmd6.CommandText =

                                                @"
                                           
                                            --declare @date datetime
                                            --declare @FundPK int

                                            --set @date = '07/30/2020'
                                            --set @FundPK = 102


                                            Declare @SinceDate datetime
                                            Declare @IndexPK int

                                            select @SinceDate = IssueDate, @IndexPK = isnull(B.IndexPK,0) from Fund A
                                            left join FundIndex B on A.FundPK = B.FundPK and B.status in (1,2)
                                            where A.FundPK = @FundPK and A.status in (1,2) and MaturityDate >= @Date


                                            DECLARE @FFSFund TABLE 
                                            (
                                            Date DATETIME,
                                            FundPK INT
                                            )



                                            DECLARE @Result TABLE
                                            (
                                            Date DATETIME,
                                            FundPK INT,
                                            AUM numeric(32,2),
                                            Nav numeric(22,8),
                                            RateIndex  numeric(22,8)
                                            )



                                            INSERT INTO @FFSFund(Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  and A.Date = @SinceDate

                                            if (datediff(month,@SinceDate,@date) < 3)
                                            BEGIN
                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  
                                            END
                                            else if (datediff(month,@SinceDate,@date) between 3 and 6)
                                            BEGIN
                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  and A.Date = DATEADD(wk, DATEDIFF(wk,0,A.date), 0) and A.Date <> @date
	
                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
                                            AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  and A.Date = @date
                                            END
                                            else
                                            BEGIN
                                            INSERT INTO @FFSFund (Date,FundPK)
                                            SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
	                                        AND A.date BETWEEN @SinceDate AND @Date and FundPK = @FundPK  and A.Date = case when dbo.CheckTodayIsHoliday(EOMONTH(A.Date)) = 1 then dbo.fworkingday(EOMONTH(A.Date),-1) else EOMONTH(A.Date) end --and (EOMONTH(A.Date) <> EOMONTH(@SinceDate))
                                            END

                                            declare @FirstBenchmarkIndex numeric(18,6)
                                            select top 1 @FirstBenchmarkIndex = CloseInd from BenchmarkIndex where status = 2 and IndexPK = @IndexPK
                                            order by date asc


                                            
                                            if(@ClientCode = '03' and @FundPK = 30)
                                            BEGIN  
	                                            INSERT INTO @Result
	                                            ( Date, FundPK, AUM, Nav, RateIndex )
		
	                                            SELECT  A.Date,A.FundPK,isnull(B.AUM,0),isnull(D.PertumbuhanReturn,0),case when C.IndexPK is null then isnull(@FirstBenchmarkIndex,0) else isnull(C.CloseInd,0) end  from @FFSFund A
	                                            left join [CloseNAV] B  on A.FundPK = B.FundPK and B.status = 2       
	                                            and B.DATE = (select max(date) From CloseNAV where date <= A.Date and FundPK = @FundPK and status = 2 and Nav > 1)  
	                                            and A.FundPK= @FundPK   and status = 2  
	                                            left join BenchmarkIndex C on C.status = 2       
	                                            and C.DATE = (select max(date) From BenchmarkIndex where date <= A.Date and IndexPK = @IndexPK and status = 2 and CloseInd > 0)  
	                                            and C.IndexPK= @IndexPK
	                                            left join CloseNAVInfovesta D  on D.status = 2 and  D.Tanggal = (select max(Tanggal) From CloseNAVInfovesta where Tanggal <= A.Date and status = 2 and PertumbuhanReturn > 0)    
                                            END
                                            ELSE
                                            BEGIN
	                                            INSERT INTO @Result
	                                            ( Date, FundPK, AUM, Nav, RateIndex )
		
                                                SELECT  A.Date,A.FundPK,isnull(B.AUM,0),isnull(B.Nav,0),case when C.IndexPK is null then isnull(@FirstBenchmarkIndex,0) else isnull(C.CloseInd,0) end  from @FFSFund A
                                                left join [CloseNAV] B  on A.FundPK = B.FundPK and B.status = 2       
                                                and B.DATE = (select max(date) From CloseNAV where date <= A.Date and FundPK = @FundPK and status = 2 and Nav > 1)  
                                                and A.FundPK= @FundPK   and status = 2  
                                                left join BenchmarkIndex C on C.status = 2       
                                                and C.DATE = (select max(date) From BenchmarkIndex where date <= A.Date and IndexPK = @IndexPK and status = 2 and CloseInd > 0)  
                                                and C.IndexPK= @IndexPK
 
                                            END




                                            select distinct Date NavDate,FundPK,AUM,Nav,RateIndex from @Result
                                            where Date between  dateadd(year,-5,@date) and @date ";

                                                cmd6.CommandTimeout = 0;
                                                cmd6.Parameters.AddWithValue("@Date", _FFSSetup_OJKRpt.ParamListDate);
                                                cmd6.Parameters.AddWithValue("@FundPK", _FFSSetup_OJKRpt.ParamFundPK);
                                                cmd6.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                                                cmd6.ExecuteNonQuery();


                                                using (SqlDataReader dr6 = cmd6.ExecuteReader())
                                                {

                                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<FFSSetup_OJKRpt> rList6 = new List<FFSSetup_OJKRpt>();
                                                    while (dr6.Read())
                                                    {
                                                        FFSSetup_OJKRpt rSingle6 = new FFSSetup_OJKRpt();
                                                        rSingle6.NavDate = Convert.ToDateTime(dr6["NavDate"]);

                                                        rList6.Add(rSingle6);

                                                    }


                                                    var QueryByFundID6 =
                                                        from r6 in rList6
                                                        group r6 by new { } into rGroup6
                                                        select rGroup6;

                                                    foreach (var rsHeader6 in QueryByFundID6)
                                                    {
                                                        int incRowExcel = 3;
                                                        foreach (var rsDetail6 in rsHeader6)
                                                        {
                                                            worksheet4.Cells[incRowExcel, 2].Value = rsDetail6.NavDate;
                                                            incRowExcel++;
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }

                                    #endregion


                                    #region CUSTOM YoY NAV and Benchmark -- Sementara Custom 21
                                    if (Tools.ClientCode == "21")
                                    {
                                        // Nav and Benchmark
                                        using (SqlConnection DbCon8 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon8.Open();
                                            using (SqlCommand cmd8 = DbCon8.CreateCommand())
                                            {
                                                cmd8.CommandText =

                                                @"
                                            --declare @date datetime
--declare @FundPK int

--set @date = '10/27/2020'
--set @FundPK = 1


Declare @SinceDate datetime
Declare @IndexPK int

select @SinceDate = IssueDate, @IndexPK = isnull(B.IndexPK,0) from Fund A
left join FundIndex B on A.FundPK = B.FundPK and B.status in (1,2)
where A.FundPK = @FundPK and A.status in (1,2) and MaturityDate >= @Date



DECLARE @FFSFund TABLE 
(
Date DATETIME,
FundPK INT
)



DECLARE @Result TABLE
(
Date DATETIME,
FundPK INT,
AUM numeric(32,2),
Nav numeric(22,8),
RateIndex  numeric(22,8)
)


INSERT INTO @FFSFund (Date,FundPK)
SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,dbo.Fund WHERE status = 2
AND  FundPK = @FundPK  and A.Date = DATEADD(year, -1, @date) 


declare @FirstBenchmarkIndex numeric(18,6)
select top 1 @FirstBenchmarkIndex = CloseInd from BenchmarkIndex where status = 2 and IndexPK = @IndexPK
order by date asc

INSERT INTO @Result
( Date, FundPK, AUM, Nav, RateIndex )
		
SELECT  A.Date,A.FundPK,isnull(B.AUM,0),isnull(B.Nav,0),case when C.IndexPK is null then isnull(@FirstBenchmarkIndex,0) else isnull(C.CloseInd,0) end  from @FFSFund A
left join [CloseNAV] B  on A.FundPK = B.FundPK and B.status = 2       
and B.DATE = (select max(date) From CloseNAV where date <= A.Date and FundPK = @FundPK and status = 2 and Nav > 1)  
and A.FundPK= @FundPK   and status = 2  
left join BenchmarkIndex C on C.status = 2       
and C.DATE = (select max(date) From BenchmarkIndex where date <= A.Date and IndexPK = @IndexPK and status = 2 and CloseInd > 0)  
and C.IndexPK= @IndexPK

select distinct Date NavDate,FundPK,AUM,Nav,RateIndex from @Result


 ";

                                                cmd8.CommandTimeout = 0;
                                                cmd8.Parameters.AddWithValue("@FundPK", _FFSSetup_OJKRpt.ParamFundPK);
                                                cmd8.Parameters.AddWithValue("@Date", _FFSSetup_OJKRpt.ParamListDate);
                                                cmd8.ExecuteNonQuery();


                                                using (SqlDataReader dr8 = cmd8.ExecuteReader())
                                                {

                                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<FFSSetup_OJKRpt> rList8 = new List<FFSSetup_OJKRpt>();
                                                    while (dr8.Read())
                                                    {
                                                        FFSSetup_OJKRpt rSingle8 = new FFSSetup_OJKRpt();
                                                        rSingle8.NavDate = Convert.ToDateTime(dr8["NavDate"]);
                                                        rSingle8.Nav = Convert.ToDecimal(dr8["Nav"]);
                                                        rSingle8.RateIndex = Convert.ToDecimal(dr8["RateIndex"]);
                                                        rList8.Add(rSingle8);

                                                    }


                                                    var QueryByFundID8 =
                                                        from r8 in rList8
                                                        group r8 by new { } into rGroup8
                                                        select rGroup8;

                                                    foreach (var rsHeader8 in QueryByFundID8)
                                                    {
                                                        int incRowExcel = 14;
                                                        foreach (var rsDetail8 in rsHeader8)
                                                        {
                                                            worksheet4.Cells[incRowExcel, 8].Value = rsDetail8.NavDate;
                                                            worksheet4.Cells[incRowExcel, 9].Value = rsDetail8.Nav;
                                                            worksheet4.Cells[incRowExcel, 10].Value = rsDetail8.RateIndex;
                                                            incRowExcel++;
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }

                                    #endregion


                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                }
                                package.Save();
                                if (_FFSSetup_OJKRpt.DownloadMode == "PDF")
                                {
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                }
                                return filePath;

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


        public string Validate_CheckCopyFFSForOJK(int _fund, DateTime _dateto)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
                        if exists(select FundPK from FFSForOJK where status in (1,2) and FundPK = @FundPK and Date = @Date)
                        BEGIN
                        SELECT 'Copy Cancel, FFS For OJK Already Exists' as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END   ";

                        cmd.Parameters.AddWithValue("@FundPK", _fund);
                        cmd.Parameters.AddWithValue("@Date", _dateto);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return " ";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string Copy_FFSForOJK(ParamFFSForOJK _paramFFSForOJK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
                        declare @FFSForOJKPK int
                        Select @FFSForOJKPK = max(FFSForOJKPK) + 1 From FFSForOJK
                        set @FFSForOJKPK = isnull(@FFSForOJKPK,1)
  
                        INSERT INTO [dbo].[FFSForOJK]
                        ([FFSForOJKPK]
                        ,[HistoryPK]
                        ,[Status]
                        ,[Notes]
                        ,[Date]
                        ,[FundPK]
                        ,[TemplateType]
                        ,[PeriodePenilaian]
                        ,[DanaKegiatanSosial]
                        ,[AkumulasiDanaCSR]
                        ,[CSR]
                        ,[Resiko1]
                        ,[Resiko2]
                        ,[Resiko3]
                        ,[Resiko4]
                        ,[Resiko5]
                        ,[Resiko6]
                        ,[Resiko7]
                        ,[Resiko8]
                        ,[Resiko9]
                        ,[KlasifikasiResiko]
                        ,[ManajerInvestasi]
                        ,[TujuanInvestasi]
                        ,[KebijakanInvestasi1]
                        ,[KebijakanInvestasi2]
                        ,[KebijakanInvestasi3]
                        ,[ProfilBankCustodian]
                        ,[AksesProspektus]
                        ,[KeteranganResiko]
                        ,[NamaKebijakanInvestasi1]
                        ,[NamaKebijakanInvestasi2]
                        ,[NamaKebijakanInvestasi3]
                        ,[BitDistributedIncome]
                        ,[EntryUsersID]
                        ,[EntryTime]
                        ,[LastUpdate])

                        Select @FFSForOJKPK,1,1,'',@dateto,@FundPK,[TemplateType]
                        ,[PeriodePenilaian]
                        ,[DanaKegiatanSosial]
                        ,[AkumulasiDanaCSR]
                        ,[CSR]
                        ,[Resiko1]
                        ,[Resiko2]
                        ,[Resiko3]
                        ,[Resiko4]
                        ,[Resiko5]
                        ,[Resiko6]
                        ,[Resiko7]
                        ,[Resiko8]
                        ,[Resiko9]
                        ,[KlasifikasiResiko]
                        ,[ManajerInvestasi]
                        ,[TujuanInvestasi]
                        ,[KebijakanInvestasi1]
                        ,[KebijakanInvestasi2]
                        ,[KebijakanInvestasi3]
                        ,[ProfilBankCustodian]
                        ,[AksesProspektus]
                        ,[KeteranganResiko]
                        ,[NamaKebijakanInvestasi1]
                        ,[NamaKebijakanInvestasi2]
                        ,[NamaKebijakanInvestasi3]
                        ,[BitDistributedIncome]
                        ,@EntryUsersID,@TimeNow,@TimeNow
                        from FFSForOJK where FundPK = @FundPK and status = 2 and Date = @DateFrom
                      
                        select 'Copy Fund Accounting Setup Success' as Msg
                          ";
                        cmd.Parameters.AddWithValue("@FundPK", _paramFFSForOJK.ParamFund);
                        cmd.Parameters.AddWithValue("@DateFrom", _paramFFSForOJK.ParamDateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _paramFFSForOJK.ParamDateTo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _paramFFSForOJK.EntryUsersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Msg"]);

                            }
                            return "Data Not Copy";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string ImportPortfolioForFFS(string _fileSource, string _userID, string _date)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    //delete data yang lama
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = "delete FFSPortfolioAllocation where Date = @Date ";
                            cmd1.Parameters.AddWithValue("@Date", _date);
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.FFSPortfolioAllocation";
                        bulkCopy.WriteToServer(CreateDataTableFromFFSPortfolioAllocationExcelFile(_fileSource, _date));
                        //_msg = "Import Close Nav Success";
                    }
                    DbCon.Close();
                    DbCon.Open();
                    using (SqlConnection conn1 = new SqlConnection(Tools.conString))
                    {
                        conn1.Open();
                        using (SqlCommand cmd2 = conn1.CreateCommand())
                        {
                            cmd2.CommandText = "delete FFSTopHolding  where Date = @Date ";
                            cmd2.Parameters.AddWithValue("@Date", _date);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy1 = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy1.DestinationTableName = "dbo.FFSTopHolding";
                        bulkCopy1.WriteToServer(CreateDataTableFromFFSTopHoldingExcelFile(_fileSource, _date));
                        //_msg = "Import Close Nav Success";
                    }



                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"


                            update FFSPortfolioAllocation set Date = @Date where Date is null
                            delete FFSPortfolioAllocation where FundCode is null
                            
                            update FFSTopHolding set Date = @Date where Date is null
                            delete FFSTopHolding where FundCode is null

                            Select 'success'
                                                "
                                ;
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);
                            cmd1.Parameters.AddWithValue("@Date", _date);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = "Import Portfolio For FFS Success"; //Convert.ToString(dr[""]);
                                    return _msg;
                                }
                                else
                                {
                                    _msg = "";
                                    return _msg;
                                }
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
        private DataTable CreateDataTableFromFFSPortfolioAllocationExcelFile(string _path, string _date)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "FFSPortfolioAllocationPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Equities";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "FixedIncome";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "OtherInvestment";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "MoneyMarket";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Cash";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            string SheetName = "SELECT * FROM [% Portfolio Allocation$]";
                            odCmd.CommandText = SheetName;
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 2; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["FundCode"] = odRdr[0];
                                    dr["Equities"] = odRdr[8];
                                    dr["FixedIncome"] = odRdr[9];
                                    dr["OtherInvestment"] = odRdr[10];
                                    dr["MoneyMarket"] = odRdr[11];
                                    dr["Cash"] = odRdr[12];


                                    if (dr["FFSPortfolioAllocationPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }



                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromFFSTopHoldingExcelFile(string _path, string _date)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "FFSTopHoldingPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SecuritiesName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Percentage";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            string SheetName = "SELECT * FROM [Top Holding$]";
                            odCmd.CommandText = SheetName;
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 2; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["FundCode"] = odRdr[0];
                                    dr["SecuritiesName"] = odRdr[3];
                                    dr["Percentage"] = odRdr[8];



                                    if (dr["FFSTopHoldingPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }




    }
}
