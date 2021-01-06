using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class FFSSetup_21Reps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FFSSetup_21] " +
                            "([FFSSetup_21PK],[HistoryPK],[Status],[Date],[FundPK],[Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Col11],[Col12],[Col13],[Col14],[Col15],[Col16],[Col17],[Col18],[Col19],[Col20],[Col21],[Col22],[Col23],[Col24],";
        string _paramaterCommand = "@Date,@FundPK,@Col1,@Col2,@Col3,@Col4,@Col5,@Col6,@Col7,@Col8,@Col9,@Col10,@Col11,@Col12,@Col13,@Col14,@Col15,@Col16,@Col17,@Col18,@Col19,@Col20,@Col21,@Col22,@Col23,@Col24,";

        //2
        private FFSSetup_21 setFFSSetup_21(SqlDataReader dr)
        {
            FFSSetup_21 M_FFSSetup_21 = new FFSSetup_21();
            M_FFSSetup_21.FFSSetup_21PK = Convert.ToInt32(dr["FFSSetup_21PK"]);
            M_FFSSetup_21.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FFSSetup_21.Status = Convert.ToInt32(dr["Status"]);
            M_FFSSetup_21.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FFSSetup_21.Date = Convert.ToString(dr["Date"]);
            M_FFSSetup_21.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FFSSetup_21.FundID = Convert.ToString(dr["FundID"]);
            M_FFSSetup_21.FundName = Convert.ToString(dr["FundName"]);
            M_FFSSetup_21.Col1 = Convert.ToString(dr["Col1"]);
            M_FFSSetup_21.Col2 = Convert.ToString(dr["Col2"]);
            M_FFSSetup_21.Col3 = Convert.ToString(dr["Col3"]);
            M_FFSSetup_21.Col4 = Convert.ToString(dr["Col4"]);
            M_FFSSetup_21.Col5 = Convert.ToString(dr["Col5"]);
            M_FFSSetup_21.Col6 = Convert.ToString(dr["Col6"]);
            M_FFSSetup_21.Col7 = Convert.ToString(dr["Col7"]);
            M_FFSSetup_21.Col8 = Convert.ToString(dr["Col8"]);
            M_FFSSetup_21.Col9 = Convert.ToString(dr["Col9"]);
            M_FFSSetup_21.Col10 = Convert.ToString(dr["Col10"]);
            M_FFSSetup_21.Col11 = Convert.ToString(dr["Col11"]);
            M_FFSSetup_21.Col12 = Convert.ToString(dr["Col12"]);
            M_FFSSetup_21.Col13 = Convert.ToString(dr["Col13"]);
            M_FFSSetup_21.Col14 = Convert.ToString(dr["Col14"]);
            M_FFSSetup_21.Col15 = Convert.ToString(dr["Col15"]);
            M_FFSSetup_21.Col16 = Convert.ToString(dr["Col16"]);
            M_FFSSetup_21.Col17 = Convert.ToString(dr["Col17"]);
            M_FFSSetup_21.Col18 = Convert.ToString(dr["Col18"]);
            M_FFSSetup_21.Col19 = Convert.ToString(dr["Col19"]);
            M_FFSSetup_21.Col20 = Convert.ToString(dr["Col20"]);
            M_FFSSetup_21.Col21 = Convert.ToString(dr["Col21"]);
            M_FFSSetup_21.Col22 = Convert.ToString(dr["Col22"]);
            M_FFSSetup_21.Col23 = Convert.ToString(dr["Col23"]);
            M_FFSSetup_21.Col24 = Convert.ToInt32(dr["Col24"]);
            M_FFSSetup_21.Col24Desc = Convert.ToString(dr["Col24Desc"]);
            M_FFSSetup_21.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FFSSetup_21.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FFSSetup_21.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FFSSetup_21.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FFSSetup_21.EntryTime = dr["EntryTime"].ToString();
            M_FFSSetup_21.UpdateTime = dr["UpdateTime"].ToString();
            M_FFSSetup_21.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FFSSetup_21.VoidTime = dr["VoidTime"].ToString();
            M_FFSSetup_21.DBUserID = dr["DBUserID"].ToString();
            M_FFSSetup_21.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FFSSetup_21.LastUpdate = dr["LastUpdate"].ToString();
            M_FFSSetup_21.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FFSSetup_21;
        }

        //3
        public List<FFSSetup_21> FFSSetup_21_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FFSSetup_21> L_FFSSetup_21 = new List<FFSSetup_21>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundID,B.Name FundName,
                                                case when Col24 = 1 then 'Lowest' else Case When Col24 = 2 then 'Lower' else Case when Col24 = 3 then 'Moderate' else Case when Col24 = 4 then 'Higher' else 'Highest' END END END END Col24Desc,* from FFSSetup_21 A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundID,B.Name FundName,
                                                case when Col24 = 1 then 'Lowest' else Case When Col24 = 2 then 'Lower' else Case when Col24 = 3 then 'Moderate' else Case when Col24 = 4 then 'Higher' else 'Highest' END END END END Col24Desc,* from FFSSetup_21 A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FFSSetup_21.Add(setFFSSetup_21(dr));
                                }
                            }
                            return L_FFSSetup_21;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //5
        public int FFSSetup_21_Add(FFSSetup_21 _FFSSetup_21, bool _havePrivillege)
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
                                 "Select isnull(max(FFSSetup_21PK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,LastUpdate=@LastUpdate from FFSSetup_21";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup_21.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(FFSSetup_21PK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FFSSetup_21";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _FFSSetup_21.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _FFSSetup_21.FundPK);
                        cmd.Parameters.AddWithValue("@Col1", _FFSSetup_21.Col1);
                        cmd.Parameters.AddWithValue("@Col2", _FFSSetup_21.Col2);
                        cmd.Parameters.AddWithValue("@Col3", _FFSSetup_21.Col3);
                        cmd.Parameters.AddWithValue("@Col4", _FFSSetup_21.Col4);
                        cmd.Parameters.AddWithValue("@Col5", _FFSSetup_21.Col5);
                        cmd.Parameters.AddWithValue("@Col6", _FFSSetup_21.Col6);
                        cmd.Parameters.AddWithValue("@Col7", _FFSSetup_21.Col7);
                        cmd.Parameters.AddWithValue("@Col8", _FFSSetup_21.Col8);
                        cmd.Parameters.AddWithValue("@Col9", _FFSSetup_21.Col9);
                        cmd.Parameters.AddWithValue("@Col10", _FFSSetup_21.Col10);
                        cmd.Parameters.AddWithValue("@Col11", _FFSSetup_21.Col11);
                        cmd.Parameters.AddWithValue("@Col12", _FFSSetup_21.Col12);
                        cmd.Parameters.AddWithValue("@Col13", _FFSSetup_21.Col13);
                        cmd.Parameters.AddWithValue("@Col14", _FFSSetup_21.Col14);
                        cmd.Parameters.AddWithValue("@Col15", _FFSSetup_21.Col15);
                        cmd.Parameters.AddWithValue("@Col16", _FFSSetup_21.Col16);
                        cmd.Parameters.AddWithValue("@Col17", _FFSSetup_21.Col17);
                        cmd.Parameters.AddWithValue("@Col18", _FFSSetup_21.Col18);
                        cmd.Parameters.AddWithValue("@Col19", _FFSSetup_21.Col19);
                        cmd.Parameters.AddWithValue("@Col20", _FFSSetup_21.Col20);
                        cmd.Parameters.AddWithValue("@Col21", _FFSSetup_21.Col21);
                        cmd.Parameters.AddWithValue("@Col22", _FFSSetup_21.Col22);
                        cmd.Parameters.AddWithValue("@Col23", _FFSSetup_21.Col23);
                        cmd.Parameters.AddWithValue("@Col24", _FFSSetup_21.Col24);    
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FFSSetup_21.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "FFSSetup_21");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        //6
        public int FFSSetup_21_Update(FFSSetup_21 _FFSSetup_21, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FFSSetup_21.FFSSetup_21PK, _FFSSetup_21.HistoryPK, "FFSSetup_21");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FFSSetup_21 set status=2,Date=@Date,FundPK=@FundPK,Col1=@Col1,Col2=@Col2,Col3=@Col3,Col4=@Col4,Col5=@Col5,Col6=@Col6," +
                            "Col7=@Col7,Col8=@Col8,Col9=@Col9,Col10=@Col10,Col11=@Col11,Col12=@Col12,Col13=@Col13,Col14=@Col14,Col15=@Col15,Col16=@Col16,Col17=@Col17,Col18=@Col18, " + 
                            "Col19=@Col19,Col20=@Col20,Col21=@Col21,Col22=@Col22,Col23=@Col23,Col24=@Col24," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where FFSSetup_21PK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup_21.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                            cmd.Parameters.AddWithValue("@Date", _FFSSetup_21.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _FFSSetup_21.FundPK);
                            cmd.Parameters.AddWithValue("@Col1", _FFSSetup_21.Col1);
                            cmd.Parameters.AddWithValue("@Col2", _FFSSetup_21.Col2);
                            cmd.Parameters.AddWithValue("@Col3", _FFSSetup_21.Col3);
                            cmd.Parameters.AddWithValue("@Col4", _FFSSetup_21.Col4);
                            cmd.Parameters.AddWithValue("@Col5", _FFSSetup_21.Col5);
                            cmd.Parameters.AddWithValue("@Col6", _FFSSetup_21.Col6);
                            cmd.Parameters.AddWithValue("@Col7", _FFSSetup_21.Col7);
                            cmd.Parameters.AddWithValue("@Col8", _FFSSetup_21.Col8);
                            cmd.Parameters.AddWithValue("@Col9", _FFSSetup_21.Col9);
                            cmd.Parameters.AddWithValue("@Col10", _FFSSetup_21.Col10);
                            cmd.Parameters.AddWithValue("@Col11", _FFSSetup_21.Col11);
                            cmd.Parameters.AddWithValue("@Col12", _FFSSetup_21.Col12);
                            cmd.Parameters.AddWithValue("@Col13", _FFSSetup_21.Col13);
                            cmd.Parameters.AddWithValue("@Col14", _FFSSetup_21.Col14);
                            cmd.Parameters.AddWithValue("@Col15", _FFSSetup_21.Col15);
                            cmd.Parameters.AddWithValue("@Col16", _FFSSetup_21.Col16);
                            cmd.Parameters.AddWithValue("@Col17", _FFSSetup_21.Col17);
                            cmd.Parameters.AddWithValue("@Col18", _FFSSetup_21.Col18);
                            cmd.Parameters.AddWithValue("@Col19", _FFSSetup_21.Col19);
                            cmd.Parameters.AddWithValue("@Col20", _FFSSetup_21.Col20);
                            cmd.Parameters.AddWithValue("@Col21", _FFSSetup_21.Col21);
                            cmd.Parameters.AddWithValue("@Col22", _FFSSetup_21.Col22);
                            cmd.Parameters.AddWithValue("@Col23", _FFSSetup_21.Col23);
                            cmd.Parameters.AddWithValue("@Col24", _FFSSetup_21.Col24);    
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup_21.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup_21.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FFSSetup_21 set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where FFSSetup_21PK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup_21.EntryUsersID);
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
                                cmd.CommandText = "Update FFSSetup_21 set Date=@Date,FundPK=@FundPK,Col1=@Col1,Col2=@Col2,Col3=@Col3,Col4=@Col4,Col5=@Col5,Col6=@Col6," +
                            "Col7=@Col7,Col8=@Col8,Col9=@Col9,Col10=@Col10,Col11=@Col11,Col12=@Col12,Col13=@Col13,Col14=@Col14,Col15=@Col15,Col16=@Col16,Col17=@Col17,Col18=@Col18, " +
                            "Col19=@Col19,Col20=@Col20,Col21=@Col21,Col22=@Col22,Col23=@Col23,Col24=@Col24,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where FFSSetup_21PK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup_21.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                                cmd.Parameters.AddWithValue("@Date", _FFSSetup_21.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _FFSSetup_21.FundPK);
                                cmd.Parameters.AddWithValue("@Col1", _FFSSetup_21.Col1);
                                cmd.Parameters.AddWithValue("@Col2", _FFSSetup_21.Col2);
                                cmd.Parameters.AddWithValue("@Col3", _FFSSetup_21.Col3);
                                cmd.Parameters.AddWithValue("@Col4", _FFSSetup_21.Col4);
                                cmd.Parameters.AddWithValue("@Col5", _FFSSetup_21.Col5);
                                cmd.Parameters.AddWithValue("@Col6", _FFSSetup_21.Col6);
                                cmd.Parameters.AddWithValue("@Col7", _FFSSetup_21.Col7);
                                cmd.Parameters.AddWithValue("@Col8", _FFSSetup_21.Col8);
                                cmd.Parameters.AddWithValue("@Col9", _FFSSetup_21.Col9);
                                cmd.Parameters.AddWithValue("@Col10", _FFSSetup_21.Col10);
                                cmd.Parameters.AddWithValue("@Col11", _FFSSetup_21.Col11);
                                cmd.Parameters.AddWithValue("@Col12", _FFSSetup_21.Col12);
                                cmd.Parameters.AddWithValue("@Col13", _FFSSetup_21.Col13);
                                cmd.Parameters.AddWithValue("@Col14", _FFSSetup_21.Col14);
                                cmd.Parameters.AddWithValue("@Col15", _FFSSetup_21.Col15);
                                cmd.Parameters.AddWithValue("@Col16", _FFSSetup_21.Col16);
                                cmd.Parameters.AddWithValue("@Col17", _FFSSetup_21.Col17);
                                cmd.Parameters.AddWithValue("@Col18", _FFSSetup_21.Col18);
                                cmd.Parameters.AddWithValue("@Col19", _FFSSetup_21.Col19);
                                cmd.Parameters.AddWithValue("@Col20", _FFSSetup_21.Col20);
                                cmd.Parameters.AddWithValue("@Col21", _FFSSetup_21.Col21);
                                cmd.Parameters.AddWithValue("@Col22", _FFSSetup_21.Col22);
                                cmd.Parameters.AddWithValue("@Col23", _FFSSetup_21.Col23);
                                cmd.Parameters.AddWithValue("@Col24", _FFSSetup_21.Col24);    
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup_21.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FFSSetup_21.FFSSetup_21PK, "FFSSetup_21");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FFSSetup_21 where FFSSetup_21PK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup_21.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _FFSSetup_21.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _FFSSetup_21.FundPK);
                                cmd.Parameters.AddWithValue("@Col1", _FFSSetup_21.Col1);
                                cmd.Parameters.AddWithValue("@Col2", _FFSSetup_21.Col2);
                                cmd.Parameters.AddWithValue("@Col3", _FFSSetup_21.Col3);
                                cmd.Parameters.AddWithValue("@Col4", _FFSSetup_21.Col4);
                                cmd.Parameters.AddWithValue("@Col5", _FFSSetup_21.Col5);
                                cmd.Parameters.AddWithValue("@Col6", _FFSSetup_21.Col6);
                                cmd.Parameters.AddWithValue("@Col7", _FFSSetup_21.Col7);
                                cmd.Parameters.AddWithValue("@Col8", _FFSSetup_21.Col8);
                                cmd.Parameters.AddWithValue("@Col9", _FFSSetup_21.Col9);
                                cmd.Parameters.AddWithValue("@Col10", _FFSSetup_21.Col10);
                                cmd.Parameters.AddWithValue("@Col11", _FFSSetup_21.Col11);
                                cmd.Parameters.AddWithValue("@Col12", _FFSSetup_21.Col12);
                                cmd.Parameters.AddWithValue("@Col13", _FFSSetup_21.Col13);
                                cmd.Parameters.AddWithValue("@Col14", _FFSSetup_21.Col14);
                                cmd.Parameters.AddWithValue("@Col15", _FFSSetup_21.Col15);
                                cmd.Parameters.AddWithValue("@Col16", _FFSSetup_21.Col16);
                                cmd.Parameters.AddWithValue("@Col17", _FFSSetup_21.Col17);
                                cmd.Parameters.AddWithValue("@Col18", _FFSSetup_21.Col18);
                                cmd.Parameters.AddWithValue("@Col19", _FFSSetup_21.Col19);
                                cmd.Parameters.AddWithValue("@Col20", _FFSSetup_21.Col20);
                                cmd.Parameters.AddWithValue("@Col21", _FFSSetup_21.Col21);
                                cmd.Parameters.AddWithValue("@Col22", _FFSSetup_21.Col22);
                                cmd.Parameters.AddWithValue("@Col23", _FFSSetup_21.Col23);
                                cmd.Parameters.AddWithValue("@Col24", _FFSSetup_21.Col24);    
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup_21.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FFSSetup_21 set status= 4," + //Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where FFSSetup_21PK = @PK and historyPK = @HistoryPK";
                                //cmd.Parameters.AddWithValue("@Notes", _FFSSetup_21.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup_21.HistoryPK);
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

        //7
        public void FFSSetup_21_Approved(FFSSetup_21 _FFSSetup_21)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup_21 set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where FFSSetup_21PK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup_21.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup_21.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FFSSetup_21 set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where FFSSetup_21PK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup_21.ApprovedUsersID);
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


        //8
        public void FFSSetup_21_Reject(FFSSetup_21 _FFSSetup_21)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup_21 set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where FFSSetup_21PK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup_21.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup_21.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FFSSetup_21 set status= 2,LastUpdate=@lastUpdate where FFSSetup_21PK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
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

        //9
        public void FFSSetup_21_Void(FFSSetup_21 _FFSSetup_21)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup_21 set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where FFSSetup_21PK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup_21.FFSSetup_21PK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup_21.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup_21.VoidUsersID);
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


    }
}
