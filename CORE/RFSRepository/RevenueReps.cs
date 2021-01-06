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
using System.Data.Odbc;
using System.Data.OleDb;


namespace RFSRepository
{
    public class RevenueReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Revenue] " +
                            @"([RevenuePK],[HistoryPK],[Status],[ReportPeriodPK],[PeriodPK],[ReksadanaTypePK],[AgentPK],
                            [InstrumentPK],[DepartmentPK],[New],[Characteristic],[MGTFee],[January],[February],
                            [March],[April],[May],[June],[July],[August],[September],
                            [October],[November],[December],";
        string _paramaterCommand = @"@ReportPeriodPK,@PeriodPK,@ReksadanaTypePK,@AgentPK,@InstrumentPK,@DepartmentPK,@New,@Characteristic,@MGTFee,@January,@February,@March,@April,@May
                                    ,@June,@July,@August,@September,@October,@November,@December,";

        //2
        private Revenue setRevenue(SqlDataReader dr)
        {
            Revenue M_Revenue = new Revenue();
            M_Revenue.RevenuePK = Convert.ToInt32(dr["RevenuePK"]);
            M_Revenue.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Revenue.Status = Convert.ToInt32(dr["Status"]);
            M_Revenue.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Revenue.Notes = Convert.ToString(dr["Notes"]);
            M_Revenue.ReportPeriodID = dr["ReportPeriodID"].Equals(DBNull.Value) == true ? "" : dr["ReportPeriodID"].ToString();
            M_Revenue.ReportPeriodPK = dr["ReportPeriodPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ReportPeriodPK"]);
            M_Revenue.PeriodID = dr["PeriodID"].Equals(DBNull.Value) == true ? "" : dr["PeriodID"].ToString();
            M_Revenue.PeriodPK = dr["PeriodPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PeriodPK"]);
            M_Revenue.ReksadanaTypeID = dr["ReksadanaTypeID"].Equals(DBNull.Value) == true ? "" : dr["ReksadanaTypeID"].ToString();
            M_Revenue.ReksadanaTypePK = dr["ReksadanaTypePK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ReksadanaTypePK"]);
            M_Revenue.AgentID = dr["AgentID"].Equals(DBNull.Value) == true ? "" : dr["AgentID"].ToString();
            M_Revenue.AgentPK = dr["AgentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgentPK"]);
            M_Revenue.InstrumentID = dr["InstrumentID"].Equals(DBNull.Value) == true ? "" : dr["InstrumentID"].ToString();
            M_Revenue.InstrumentPK = dr["InstrumentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentPK"]);
            M_Revenue.DepartmentID = dr["DepartmentID"].Equals(DBNull.Value) == true ? "" : dr["DepartmentID"].ToString();
            M_Revenue.DepartmentPK = dr["DepartmentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["DepartmentPK"]);
            M_Revenue.CharacteristicID = dr["CharacteristicID"].Equals(DBNull.Value) == true ? "" : dr["CharacteristicID"].ToString();
            M_Revenue.Characteristic = dr["Characteristic"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Characteristic"]);
            M_Revenue.New = Convert.ToBoolean(dr["New"]);
            M_Revenue.MGTFee = Convert.ToDecimal(dr["MGTFee"]);
            M_Revenue.January = Convert.ToDecimal(dr["January"]);
            M_Revenue.February = Convert.ToDecimal(dr["February"]);
            M_Revenue.March = Convert.ToDecimal(dr["March"]);
            M_Revenue.April = Convert.ToDecimal(dr["April"]);
            M_Revenue.May = Convert.ToDecimal(dr["May"]);
            M_Revenue.June = Convert.ToDecimal(dr["June"]);
            M_Revenue.July = Convert.ToDecimal(dr["July"]);
            M_Revenue.August = Convert.ToDecimal(dr["August"]);
            M_Revenue.September = Convert.ToDecimal(dr["September"]);
            M_Revenue.October = Convert.ToDecimal(dr["October"]);
            M_Revenue.November = Convert.ToDecimal(dr["November"]);
            M_Revenue.December = Convert.ToDecimal(dr["December"]);
            M_Revenue.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Revenue.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Revenue.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Revenue.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Revenue.EntryTime = dr["EntryTime"].ToString();
            M_Revenue.UpdateTime = dr["UpdateTime"].ToString();
            M_Revenue.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Revenue.VoidTime = dr["VoidTime"].ToString();
            M_Revenue.DBUserID = dr["DBUserID"].ToString();
            M_Revenue.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Revenue.LastUpdate = dr["LastUpdate"].ToString();
            M_Revenue.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Revenue;
        }

        public List<Revenue> Revenue_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Revenue> L_Revenue = new List<Revenue>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,ab.PeriodPK ReportPeriodPK,ab.ID ReportPeriodID,b.PeriodPK, b.ID PeriodID, c.DescOne ReksadanaTypeID, d.AgentPK, D.Name AgentID,e.InstrumentPK, e.ID InstrumentID,New,f.DescOne CharacteristicID,G.ID DepartmentID,MGTFee,January,February,March,April,May,June,July,August,September,October,November,December,* from Revenue A
                                        left join Period b on a.PeriodPK = b.PeriodPK and B.status in(1,2)
                                        left join Period ab on a.ReportPeriodPK = ab.PeriodPK and ab.status in(1,2)
										left join MasterValue c on a.ReksadanaTypePK = c.Code and c.id = 'FundType' and c.Status in(1,2)
										left join agent d on a.AgentPK = d.AgentPK and d.Status in(1,2)
										left join Instrument e on a.InstrumentPK = e.InstrumentPK and e.Status in(1,2)
                                        left join Department G on A.DepartmentPK = G.DepartmentPK and G.Status in(1,2)
										left join MasterValue f on a.Characteristic = f.code and f.ID = 'CompanyCharacteristic' and f.Status in(1,2) where A.status = @status order by RevenuePK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,ab.PeriodPK ReportPeriodPK,ab.ID ReportPeriodID,b.PeriodPK, b.ID PeriodID, c.DescOne ReksadanaTypeID, d.AgentPK, D.Name AgentID,e.InstrumentPK, e.ID InstrumentID,New,f.DescOne CharacteristicID,G.ID DepartmentID,MGTFee,January,February,March,April,May,June,July,August,September,October,November,December,* from Revenue A
                                        left join Period b on a.PeriodPK = b.PeriodPK and B.status in(1,2)
                                        left join Period ab on a.ReportPeriodPK = ab.PeriodPK and ab.status in(1,2)
										left join MasterValue c on a.ReksadanaTypePK = c.Code and c.id = 'FundType' and c.Status in(1,2)
										left join agent d on a.AgentPK = d.AgentPK and d.Status in(1,2)
										left join Instrument e on a.InstrumentPK = e.InstrumentPK and e.Status in(1,2)
                                        left join Department G on A.DepartmentPK = G.DepartmentPK and G.Status in(1,2)
										left join MasterValue f on a.Characteristic = f.code and f.ID = 'CompanyCharacteristic' and f.Status in(1,2)  order by RevenuePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Revenue.Add(setRevenue(dr));
                                }
                            }
                            return L_Revenue;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Revenue_Add(Revenue _Revenue, bool _havePrivillege)
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
                                 "Select isnull(max(RevenuePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From Revenue";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _Revenue.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(RevenuePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From Revenue";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ReportPeriodPK", _Revenue.ReportPeriodPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _Revenue.PeriodPK);
                        cmd.Parameters.AddWithValue("@ReksadanaTypePK", _Revenue.ReksadanaTypePK);
                        cmd.Parameters.AddWithValue("@AgentPK", _Revenue.AgentPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _Revenue.InstrumentPK);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _Revenue.DepartmentPK);
                        cmd.Parameters.AddWithValue("@New", _Revenue.New);
                        cmd.Parameters.AddWithValue("@Characteristic", _Revenue.Characteristic);
                        cmd.Parameters.AddWithValue("@MGTFee", _Revenue.MGTFee);
                        cmd.Parameters.AddWithValue("@January", _Revenue.January);
                        cmd.Parameters.AddWithValue("@February", _Revenue.February);
                        cmd.Parameters.AddWithValue("@March", _Revenue.March);
                        cmd.Parameters.AddWithValue("@April", _Revenue.April);
                        cmd.Parameters.AddWithValue("@May", _Revenue.May);
                        cmd.Parameters.AddWithValue("@June", _Revenue.June);
                        cmd.Parameters.AddWithValue("@July", _Revenue.July);
                        cmd.Parameters.AddWithValue("@August", _Revenue.August);
                        cmd.Parameters.AddWithValue("@September", _Revenue.September);
                        cmd.Parameters.AddWithValue("@October", _Revenue.October);
                        cmd.Parameters.AddWithValue("@November", _Revenue.November);
                        cmd.Parameters.AddWithValue("@December", _Revenue.December);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _Revenue.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Revenue");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int Revenue_Update(Revenue _Revenue, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_Revenue.RevenuePK, _Revenue.HistoryPK, "Revenue");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Revenue set status=2, Notes=@Notes,ReportPeriodPK=@ReportPeriodPK,PeriodPK=@PeriodPK,ReksadanaTypePK=@ReksadanaTypePK,AgentPK=@AgentPK,InstrumentPK=@InstrumentPK,DepartmentPK = @DepartmentPK,New=@New,Characteristic=@Characteristic,MGTFee=@MGTFee,January=@January,February=@February,March=@March," +
                                "April=@April,May=@May,June=@June,July=@July,August=@August,September=@September,October=@October,November=@November,December=@December,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where RevenuePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _Revenue.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                            cmd.Parameters.AddWithValue("@Notes", _Revenue.Notes);
                            cmd.Parameters.AddWithValue("@ReportPeriodPK", _Revenue.ReportPeriodPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _Revenue.PeriodPK);
                            cmd.Parameters.AddWithValue("@ReksadanaTypePK", _Revenue.ReksadanaTypePK);
                            cmd.Parameters.AddWithValue("@AgentPK", _Revenue.AgentPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _Revenue.InstrumentPK);
                            cmd.Parameters.AddWithValue("@DepartmentPK", _Revenue.DepartmentPK);
                            cmd.Parameters.AddWithValue("@New", _Revenue.New);
                            cmd.Parameters.AddWithValue("@Characteristic", _Revenue.Characteristic);
                            cmd.Parameters.AddWithValue("@MGTFee", _Revenue.MGTFee);
                            cmd.Parameters.AddWithValue("@January", _Revenue.January);
                            cmd.Parameters.AddWithValue("@February", _Revenue.February);
                            cmd.Parameters.AddWithValue("@March", _Revenue.March);
                            cmd.Parameters.AddWithValue("@April", _Revenue.April);
                            cmd.Parameters.AddWithValue("@May", _Revenue.May);
                            cmd.Parameters.AddWithValue("@June", _Revenue.June);
                            cmd.Parameters.AddWithValue("@July", _Revenue.July);
                            cmd.Parameters.AddWithValue("@August", _Revenue.August);
                            cmd.Parameters.AddWithValue("@September", _Revenue.September);
                            cmd.Parameters.AddWithValue("@October", _Revenue.October);
                            cmd.Parameters.AddWithValue("@November", _Revenue.November);
                            cmd.Parameters.AddWithValue("@December", _Revenue.December);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _Revenue.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _Revenue.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Revenue set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where RevenuePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _Revenue.EntryUsersID);
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
                                cmd.CommandText = "Update Revenue set  Notes=@Notes,ReportPeriodPK=@ReportPeriodPK,PeriodPK=@PeriodPK,ReksadanaTypePK=@ReksadanaTypePK,AgentPK=@AgentPK,InstrumentPK=@InstrumentPK,DepartmentPK =@DepartmentPK,New=@New,Characteristic=@Characteristic,MGTFee=@MGTFee,January=@January,February=@February,March=@March," +
                                    "April=@April,May=@May,June=@June,July=@July,August=@August,September=@September,October=@October,November=@November,December=@December,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where RevenuePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _Revenue.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                                cmd.Parameters.AddWithValue("@Notes", _Revenue.Notes);
                                cmd.Parameters.AddWithValue("@ReportPeriodPK", _Revenue.ReportPeriodPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _Revenue.PeriodPK);
                                cmd.Parameters.AddWithValue("@ReksadanaTypePK", _Revenue.ReksadanaTypePK);
                                cmd.Parameters.AddWithValue("@AgentPK", _Revenue.AgentPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _Revenue.InstrumentPK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _Revenue.DepartmentPK);
                                cmd.Parameters.AddWithValue("@New", _Revenue.New);
                                cmd.Parameters.AddWithValue("@Characteristic", _Revenue.Characteristic);
                                cmd.Parameters.AddWithValue("@MGTFee", _Revenue.MGTFee);
                                cmd.Parameters.AddWithValue("@January", _Revenue.January);
                                cmd.Parameters.AddWithValue("@February", _Revenue.February);
                                cmd.Parameters.AddWithValue("@March", _Revenue.March);
                                cmd.Parameters.AddWithValue("@April", _Revenue.April);
                                cmd.Parameters.AddWithValue("@May", _Revenue.May);
                                cmd.Parameters.AddWithValue("@June", _Revenue.June);
                                cmd.Parameters.AddWithValue("@July", _Revenue.July);
                                cmd.Parameters.AddWithValue("@August", _Revenue.August);
                                cmd.Parameters.AddWithValue("@September", _Revenue.September);
                                cmd.Parameters.AddWithValue("@October", _Revenue.October);
                                cmd.Parameters.AddWithValue("@November", _Revenue.November);
                                cmd.Parameters.AddWithValue("@December", _Revenue.December);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _Revenue.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_Revenue.RevenuePK, "Revenue");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Revenue where RevenuePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _Revenue.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ReportPeriodPK", _Revenue.ReportPeriodPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _Revenue.PeriodPK);
                                cmd.Parameters.AddWithValue("@ReksadanaTypePK", _Revenue.ReksadanaTypePK);
                                cmd.Parameters.AddWithValue("@AgentPK", _Revenue.AgentPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _Revenue.InstrumentPK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _Revenue.DepartmentPK);
                                cmd.Parameters.AddWithValue("@New", _Revenue.New);
                                cmd.Parameters.AddWithValue("@Characteristic", _Revenue.Characteristic);
                                cmd.Parameters.AddWithValue("@MGTFee", _Revenue.MGTFee);
                                cmd.Parameters.AddWithValue("@January", _Revenue.January);
                                cmd.Parameters.AddWithValue("@February", _Revenue.February);
                                cmd.Parameters.AddWithValue("@March", _Revenue.March);
                                cmd.Parameters.AddWithValue("@April", _Revenue.April);
                                cmd.Parameters.AddWithValue("@May", _Revenue.May);
                                cmd.Parameters.AddWithValue("@June", _Revenue.June);
                                cmd.Parameters.AddWithValue("@July", _Revenue.July);
                                cmd.Parameters.AddWithValue("@August", _Revenue.August);
                                cmd.Parameters.AddWithValue("@September", _Revenue.September);
                                cmd.Parameters.AddWithValue("@October", _Revenue.October);
                                cmd.Parameters.AddWithValue("@November", _Revenue.November);
                                cmd.Parameters.AddWithValue("@December", _Revenue.December);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _Revenue.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Revenue set status= 4,Notes=@Notes," +
                                    "lastupdate=@lastupdate where RevenuePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _Revenue.Notes);
                                cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _Revenue.HistoryPK);
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

        public void Revenue_Approved(Revenue _Revenue)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Revenue set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where RevenuePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                        cmd.Parameters.AddWithValue("@historyPK", _Revenue.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _Revenue.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Revenue set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where RevenuePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Revenue.ApprovedUsersID);
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

        public void Revenue_Reject(Revenue _Revenue)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Revenue set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where RevenuePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                        cmd.Parameters.AddWithValue("@historyPK", _Revenue.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Revenue.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Revenue set status= 2,lastupdate=@lastupdate where RevenuePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
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

        public void Revenue_Void(Revenue _Revenue)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Revenue set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where RevenuePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Revenue.RevenuePK);
                        cmd.Parameters.AddWithValue("@historyPK", _Revenue.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Revenue.VoidUsersID);
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

        public bool CheckHassAdd(int _InstrumentPK, int _agentPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update revenue set Status = 3 where AgentPK = @AgentPK
										    and InstrumentPK = @InstrumentPK
                                            and status = 2 ";
                        cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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

        public string RevenueTemp(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                //delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table RevenueTemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.RevenueTemp";
                    bulkCopy.WriteToServer(CreateDataTableRevenueTempExcelFile(_fileSource));

                    _msg = "";
                }

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd3 = conn.CreateCommand())
                    {
                        _msg = "Success Import Revenue";
                        cmd3.CommandText =
                        @"  

                                        delete revenue where agentpk in (select agentpk from revenuetemp a left join Agent b on a.SalesName = b.Name and b.Status in(1,2))
                                        and PeriodPK in (select PeriodPK from revenuetemp a left join Period b on a.Period = b.id and b.Status in(1,2))


										--update revenue set Status = 3 where agentpk in (select agentpk from revenuetemp a left join Agent b on a.SalesName = b.Name and b.Status in(1,2))
										--and Instrumentpk in (select Instrumentpk from revenuetemp a left join Instrument b on a.InstrumentCode = b.id and b.Status in(1,2))

                                        Declare @revenuePK int
                                        Declare @PeriodPK int
                                        Declare @code int
                                        Declare @AgentPK int
										Declare @InstrumentPK int
                                        Declare @DepartmentPK int
                                        Declare @New nvarchar(50)
                                        Declare @Characteristic int
                                        Declare @MGTFee numeric(18,4)
                                        Declare @Jan numeric(18,4)
                                        Declare @Feb numeric(18,4)
                                        Declare @Mar numeric(18,4)
                                        Declare @Apr numeric(18,4)
                                        Declare @May numeric(18,4)
                                        Declare @Jun numeric(18,4)
                                        Declare @Jul numeric(18,4)
                                        Declare @Aug numeric(18,4)
                                        Declare @Sep numeric(18,4)
                                        Declare @Okt numeric(18,4)
                                        Declare @Nov numeric(18,4)
                                        Declare @Dec numeric(18,4)

                                        Declare A Cursor For
                                        select b.PeriodPK, c.code, d.AgentPK,F.InstrumentPK,New,G.Code,MGTFee,Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Okt,Nov,Dec from revenuetemp A
                                        left join Period b on a.Period = b.ID and B.status in(1,2)
                                        left join MasterValue c on a.TypeInstrument = c.DescOne and c.id = 'FundType' and c.Status in(1,2)
                                        left join agent d on a.SalesName = d.Name and d.Status in(1,2)
                                        left join BRIDGE.dbo.STG_TProductMap E on A.InstrumentCode = E.ProductCode
                                        left join Instrument F on E.RadsoftCode = F.id and F.Status in(1,2)
                                        left join MasterValue G on a.Characteristic = G.DescOne and f.ID = 'CompanyCharacteristic' and G.Status in(1,2)
                                        left join Department H on A.DepartmentPK = H.DepartmentPK and H.Status in(1,2)


                                        Open A
                                        Fetch next From A
                                        into @PeriodPK, 
                                         @code ,
                                         @AgentPK, 
                                         @InstrumentPK, 
                                         @DepartmentPK,
                                         @New ,
                                         @Characteristic ,
                                         @MGTFee ,
                                         @Jan ,
                                         @Feb ,
                                         @Mar ,
                                         @Apr ,
                                         @May ,
                                         @Jun ,
                                         @Jul ,
                                         @Aug ,
                                         @Sep ,
                                         @Okt ,
                                         @Nov ,
                                         @Dec 
                                        While @@Fetch_status = 0
                                        BEGIN

										--IF EXISTS(Select * from Revenue where AgentPK = @AgentPK and PeriodPK = @PeriodPK and status in (1,2))
                                        --BEGIN
                                        --DELETE Revenue where AgentPK = @AgentPK and PeriodPK = @PeriodPK and status in (1,2)
                                        --END



                                        Select @revenuePK = max(revenuepk) + 1 from revenue
                                        set @revenuePK = isnull(@revenuePK,1)

                                        insert into 
                                        [dbo].[Revenue](
										[RevenuePK] ,
										[HistoryPK] ,
										[Status] ,
										[Notes] ,
										[PeriodPK] ,
										[ReksadanaTypePK] ,
										[AgentPK] ,
										[InstrumentPK] ,
                                        [DepartmentPK] ,
										[New] ,
										[Characteristic] ,
										[MGTFee] ,
										[January] ,
										[February] ,
										[March] ,
										[April] ,
										[May] ,
										[June] ,
										[July] ,
										[August] ,
										[September] ,
										[October] ,
										[November] ,
										[December] ,
										[EntryUsersID] ,
										[EntryTime] ,
										[ApprovedUsersID] ,
										[ApprovedTime] ,
										[LastUpdate] 
                                        )

                                        select @revenuePK,1,2,'',
                                         @PeriodPK, 
                                         @code ,
                                         @AgentPK, 
                                         @InstrumentPK, 
                                         @DepartmentPK, 
                                         @New ,
                                         @Characteristic ,
                                         @MGTFee ,
                                         @Jan ,
                                         @Feb ,
                                         @Mar ,
                                         @Apr ,
                                         @May ,
                                         @Jun ,
                                         @Jul ,
                                         @Aug ,
                                         @Sep ,
                                         @Okt ,
                                         @Nov ,
                                         @Dec ,
                                         @EntryUsersID ,
                                         @LastUpdate ,
                                         @EntryUsersID ,
                                         @LastUpdate ,
                                         @LastUpdate  

										select 'Success Import' Result
                                 

                                        fetch next From A into 
                                         @PeriodPK, 
                                         @code ,
                                         @AgentPK, 
                                         @InstrumentPK, 
                                         @DepartmentPK, 
                                         @New ,
                                         @Characteristic ,
                                         @MGTFee ,
                                         @Jan ,
                                         @Feb ,
                                         @Mar ,
                                         @Apr ,
                                         @May ,
                                         @Jun ,
                                         @Jul ,
                                         @Aug ,
                                         @Sep ,
                                         @Okt ,
                                         @Nov ,
                                         @Dec 
                                        end
                                        Close A
                                        Deallocate A
                                ";
                        cmd3.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd3.Parameters.AddWithValue("@LastUpdate", _now);
                        cmd3.ExecuteNonQuery();
                    }

                }

                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableRevenueTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Period";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Department";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TypeInstrument";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SalesName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "New";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Characteristic";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "MGTFee";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Jan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Feb";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Mar";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Apr";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "May";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Jun";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Jul";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Aug";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Sep";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Okt";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Nov";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Dec";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();
                                    //dr["TransactionType"] = odRdr[2];

                                    dr["No"] = odRdr[0];
                                    dr["Period"] = odRdr[1];
                                    dr["Department"] = odRdr[2];
                                    dr["TypeInstrument"] = odRdr[3];
                                    dr["SalesName"] = odRdr[4];
                                    dr["InstrumentCode"] = odRdr[5];
                                    dr["InstrumentName"] = odRdr[6];
                                    dr["New"] = odRdr[7];
                                    dr["Characteristic"] = odRdr[8];
                                    dr["MGTFee"] = odRdr[9];
                                    dr["Jan"] = odRdr[10];
                                    dr["Feb"] = odRdr[11];
                                    dr["Mar"] = odRdr[12];
                                    dr["Apr"] = odRdr[13];
                                    dr["May"] = odRdr[14];
                                    dr["Jun"] = odRdr[15];
                                    dr["Jul"] = odRdr[16];
                                    dr["Aug"] = odRdr[17];
                                    dr["Sep"] = odRdr[18];
                                    dr["Okt"] = odRdr[19];
                                    dr["Nov"] = odRdr[20];
                                    dr["Dec"] = odRdr[21];
                                    //dr["FeeNominal"] = Convert.ToDecimal(odRdr[24].ToString() == "" ? 0 : odRdr[24].Equals(DBNull.Value) == true ? 0 : odRdr[24]);

                                    if (dr["No"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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