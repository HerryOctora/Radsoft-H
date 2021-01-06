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



namespace RFSRepository
{
    public class FFSSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FFSSetup] " +
                            "([FFSSetupPK],[HistoryPK],[Status],[FundPK],[ValueDate],[Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10]," +
                            "[Col11],[Col12],[Col13],[Col14],[Col15],[Col16],[Col17],[Col18],[Col19],[Col20]," +
                            "[Col21],[Col22],[Col23],[Col24],[Col25],[Col26],[Col27],[TemplateType],[InceptionIndex],";

        string _paramaterCommand = "@FundPK,@ValueDate,@Col1,@Col2,@Col3,@Col4,@Col5,@Col6,@Col7,@Col8,@Col9,@Col10,@Col11,@Col12,@Col13,@Col14,@Col15," +
            "@Col16,@Col17,@Col18,@Col19,@Col20,@Col21,@Col22,@Col23,@Col24,@Col25,@Col26,@Col27,@TemplateType,@InceptionIndex,";

        //2
        private FFSSetup setFFSSetup(SqlDataReader dr)
        {
            FFSSetup M_FFSSetup = new FFSSetup();
            M_FFSSetup.FFSSetupPK = Convert.ToInt32(dr["FFSSetupPK"]);
            M_FFSSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FFSSetup.Status = Convert.ToInt32(dr["Status"]);
            M_FFSSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FFSSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FFSSetup.FundID = Convert.ToString(dr["FundID"]);
            M_FFSSetup.FundName = Convert.ToString(dr["FundName"]);
            M_FFSSetup.ValueDate = Convert.ToString(dr["ValueDate"]);

            M_FFSSetup.Col1 = dr["Col1"].ToString();
            M_FFSSetup.Col2 = dr["Col2"].ToString();
            M_FFSSetup.Col3 = dr["Col3"].ToString();
            M_FFSSetup.Col4 = dr["Col4"].ToString();
            M_FFSSetup.Col5 = dr["Col5"].ToString();
            M_FFSSetup.Col6 = dr["Col6"].ToString();
            M_FFSSetup.Col7 = dr["Col7"].ToString();
            M_FFSSetup.Col8 = dr["Col8"].ToString();
            M_FFSSetup.Col9 = dr["Col9"].ToString();
            M_FFSSetup.Col10 = dr["Col10"].ToString();

            M_FFSSetup.Col11 = dr["Col11"].ToString();
            M_FFSSetup.Col12 = dr["Col12"].ToString();
            M_FFSSetup.Col13 = dr["Col13"].ToString();
            M_FFSSetup.Col14 = dr["Col14"].ToString();
            M_FFSSetup.Col15 = dr["Col15"].ToString();
            M_FFSSetup.Col16 = dr["Col16"].ToString();
            M_FFSSetup.Col17 = dr["Col17"].ToString();
            M_FFSSetup.Col18 = dr["Col18"].ToString();
            M_FFSSetup.Col19 = dr["Col19"].ToString();
            M_FFSSetup.Col20 = dr["Col20"].ToString();

            M_FFSSetup.Col21 = dr["Col21"].ToString();
            M_FFSSetup.Col22 = dr["Col22"].ToString();
            M_FFSSetup.Col23 = dr["Col23"].ToString();
            M_FFSSetup.Col24 = dr["Col24"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Col24"]);
            M_FFSSetup.Col25 = dr["Col25"].ToString();
            M_FFSSetup.Col26 = dr["Col26"].ToString();
            M_FFSSetup.Col27 = dr["Col27"].ToString();

            M_FFSSetup.TemplateType = Convert.ToInt32(dr["TemplateType"]);
            M_FFSSetup.TemplateTypeDesc = dr["TemplateTypeDesc"].ToString();
            M_FFSSetup.InceptionIndex = Convert.ToDecimal(dr["InceptionIndex"]);

            M_FFSSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FFSSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FFSSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FFSSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FFSSetup.EntryTime = dr["EntryTime"].ToString();
            M_FFSSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_FFSSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FFSSetup.VoidTime = dr["VoidTime"].ToString();
            M_FFSSetup.DBUserID = dr["DBUserID"].ToString();
            M_FFSSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FFSSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_FFSSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FFSSetup;
        }

        public List<FFSSetup> FFSSetup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FFSSetup> L_FFSSetup = new List<FFSSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"

                                Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                case when A.TemplateType = 1 then 'Open End' when A.TemplateType = 2 then 'WithoutBenchmark' when A.TemplateType = 3 then 'Under1Year' else '' end TemplateTypeDesc,
                                B.ID FundID, B.Name FundName ,* from FFSSetup A
                                left join Fund B on A.FundPK = B.FundPK  and B.status in(1,2)                            
                                where A.status = @status
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                                Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                case when A.TemplateType = 1 then 'Open End' when A.TemplateType = 2 then 'WithoutBenchmark' when A.TemplateType = 3 then 'Under1Year' else '' end TemplateTypeDesc,
                                B.ID FundID, B.Name FundName ,* from FFSSetup A
                                left join Fund B on A.FundPK = B.FundPK  and B.status in(1,2)   
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FFSSetup.Add(setFFSSetup(dr));
                                }
                            }
                            return L_FFSSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FFSSetup_Add(FFSSetup _FFSSetup, bool _havePrivillege)
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
                                 "Select isnull(max(FFSSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FFSSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FFSSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FFSSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _FFSSetup.FundPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _FFSSetup.ValueDate);
                        cmd.Parameters.AddWithValue("@Col1", _FFSSetup.Col1);
                        cmd.Parameters.AddWithValue("@Col2", _FFSSetup.Col2);
                        cmd.Parameters.AddWithValue("@Col3", _FFSSetup.Col3);
                        cmd.Parameters.AddWithValue("@Col4", _FFSSetup.Col4);
                        cmd.Parameters.AddWithValue("@Col5", _FFSSetup.Col5);
                        cmd.Parameters.AddWithValue("@Col6", _FFSSetup.Col6);
                        cmd.Parameters.AddWithValue("@Col7", _FFSSetup.Col7);
                        cmd.Parameters.AddWithValue("@Col8", _FFSSetup.Col8);
                        cmd.Parameters.AddWithValue("@Col9", _FFSSetup.Col9);
                        cmd.Parameters.AddWithValue("@Col10", _FFSSetup.Col10);

                        cmd.Parameters.AddWithValue("@Col11", _FFSSetup.Col11);
                        cmd.Parameters.AddWithValue("@Col12", _FFSSetup.Col12);
                        cmd.Parameters.AddWithValue("@Col13", _FFSSetup.Col13);
                        cmd.Parameters.AddWithValue("@Col14", _FFSSetup.Col14);
                        cmd.Parameters.AddWithValue("@Col15", _FFSSetup.Col15);
                        cmd.Parameters.AddWithValue("@Col16", _FFSSetup.Col16);
                        cmd.Parameters.AddWithValue("@Col17", _FFSSetup.Col17);
                        cmd.Parameters.AddWithValue("@Col18", _FFSSetup.Col18);
                        cmd.Parameters.AddWithValue("@Col19", _FFSSetup.Col19);
                        cmd.Parameters.AddWithValue("@Col20", _FFSSetup.Col20);

                        cmd.Parameters.AddWithValue("@Col21", _FFSSetup.Col21);
                        cmd.Parameters.AddWithValue("@Col22", _FFSSetup.Col22);
                        cmd.Parameters.AddWithValue("@Col23", _FFSSetup.Col23);
                        cmd.Parameters.AddWithValue("@Col24", _FFSSetup.Col24);
                        cmd.Parameters.AddWithValue("@Col25", _FFSSetup.Col25);
                        cmd.Parameters.AddWithValue("@Col26", _FFSSetup.Col26);
                        cmd.Parameters.AddWithValue("@Col27", _FFSSetup.Col27);

                        cmd.Parameters.AddWithValue("@TemplateType", _FFSSetup.TemplateType);
                        cmd.Parameters.AddWithValue("@InceptionIndex", _FFSSetup.InceptionIndex);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _FFSSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FFSSetup");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FFSSetup_Update(FFSSetup _FFSSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_FFSSetup.FFSSetupPK, _FFSSetup.HistoryPK, "FFSSetup");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FFSSetup set status=2,Notes=@Notes,FundPK=@FundPK,ValueDate=@ValueDate," +
                                    "Col1=@Col1,Col2=@Col2,Col3=@Col3,Col4=@Col4,Col5=@Col5,Col6=@Col6,Col7=@Col7,Col8=@Col8,Col9=@Col9,Col10=@Col10," +
                                    "Col11=@Col11,Col12=@Col12,Col13=@Col13,Col14=@Col14,Col15=@Col15,Col16=@Col16,Col17=@Col17,Col18=@Col18,Col19=@Col19,Col20=@Col20," +
                                    "Col21=@Col21,Col22=@Col22,Col23=@Col23,Col24=@Col24,Col25=@Col25,Col26=@Col26,Col27=@Col27,TemplateType=@TemplateType,InceptionIndex=@InceptionIndex,"+
                                    "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where FFSSetupPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _FFSSetup.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _FFSSetup.FundPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _FFSSetup.ValueDate);
                            cmd.Parameters.AddWithValue("@Col1", _FFSSetup.Col1);
                            cmd.Parameters.AddWithValue("@Col2", _FFSSetup.Col2);
                            cmd.Parameters.AddWithValue("@Col3", _FFSSetup.Col3);
                            cmd.Parameters.AddWithValue("@Col4", _FFSSetup.Col4);
                            cmd.Parameters.AddWithValue("@Col5", _FFSSetup.Col5);
                            cmd.Parameters.AddWithValue("@Col6", _FFSSetup.Col6);
                            cmd.Parameters.AddWithValue("@Col7", _FFSSetup.Col7);
                            cmd.Parameters.AddWithValue("@Col8", _FFSSetup.Col8);
                            cmd.Parameters.AddWithValue("@Col9", _FFSSetup.Col9);
                            cmd.Parameters.AddWithValue("@Col10", _FFSSetup.Col10);

                            cmd.Parameters.AddWithValue("@Col11", _FFSSetup.Col11);
                            cmd.Parameters.AddWithValue("@Col12", _FFSSetup.Col12);
                            cmd.Parameters.AddWithValue("@Col13", _FFSSetup.Col13);
                            cmd.Parameters.AddWithValue("@Col14", _FFSSetup.Col14);
                            cmd.Parameters.AddWithValue("@Col15", _FFSSetup.Col15);
                            cmd.Parameters.AddWithValue("@Col16", _FFSSetup.Col16);
                            cmd.Parameters.AddWithValue("@Col17", _FFSSetup.Col17);
                            cmd.Parameters.AddWithValue("@Col18", _FFSSetup.Col18);
                            cmd.Parameters.AddWithValue("@Col19", _FFSSetup.Col19);
                            cmd.Parameters.AddWithValue("@Col20", _FFSSetup.Col20);

                            cmd.Parameters.AddWithValue("@Col21", _FFSSetup.Col21);
                            cmd.Parameters.AddWithValue("@Col22", _FFSSetup.Col22);
                            cmd.Parameters.AddWithValue("@Col23", _FFSSetup.Col23);
                            cmd.Parameters.AddWithValue("@Col24", _FFSSetup.Col24);
                            cmd.Parameters.AddWithValue("@Col25", _FFSSetup.Col25);
                            cmd.Parameters.AddWithValue("@Col26", _FFSSetup.Col26);
                            cmd.Parameters.AddWithValue("@Col27", _FFSSetup.Col27);

                            cmd.Parameters.AddWithValue("@TemplateType", _FFSSetup.TemplateType);
                            cmd.Parameters.AddWithValue("@InceptionIndex", _FFSSetup.InceptionIndex);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FFSSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FFSSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup.EntryUsersID);
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
                                cmd.CommandText = "Update FFSSetup set status=2,Notes=@Notes,FundPK=@FundPK,ValueDate=@ValueDate," +
                                    "Col1=@Col1,Col2=@Col2,Col3=@Col3,Col4=@Col4,Col5=@Col5,Col6=@Col6,Col7=@Col7,Col8=@Col8,Col9=@Col9,Col10=@Col10," +
                                    "Col11=@Col11,Col12=@Col12,Col13=@Col13,Col14=@Col14,Col15=@Col15,Col16=@Col16,Col17=@Col17,Col18=@Col18,Col19=@Col19,Col20=@Col20," +
                                    "Col21=@Col21,Col22=@Col22,Col23=@Col23,Col24=@Col24,Col25=@Col25,Col26=@Col26,Col27=@Col27,TemplateType=@TemplateType,InceptionIndex=@InceptionIndex,"+
                                    "ApprovedUsersID=@UpdateUsersID,ApprovedTime=@Updatetime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where FFSSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _FFSSetup.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _FFSSetup.FundPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _FFSSetup.ValueDate);
                                cmd.Parameters.AddWithValue("@Col1", _FFSSetup.Col1);
                                cmd.Parameters.AddWithValue("@Col2", _FFSSetup.Col2);
                                cmd.Parameters.AddWithValue("@Col3", _FFSSetup.Col3);
                                cmd.Parameters.AddWithValue("@Col4", _FFSSetup.Col4);
                                cmd.Parameters.AddWithValue("@Col5", _FFSSetup.Col5);
                                cmd.Parameters.AddWithValue("@Col6", _FFSSetup.Col6);
                                cmd.Parameters.AddWithValue("@Col7", _FFSSetup.Col7);
                                cmd.Parameters.AddWithValue("@Col8", _FFSSetup.Col8);
                                cmd.Parameters.AddWithValue("@Col9", _FFSSetup.Col9);
                                cmd.Parameters.AddWithValue("@Col10", _FFSSetup.Col10);

                                cmd.Parameters.AddWithValue("@Col11", _FFSSetup.Col11);
                                cmd.Parameters.AddWithValue("@Col12", _FFSSetup.Col12);
                                cmd.Parameters.AddWithValue("@Col13", _FFSSetup.Col13);
                                cmd.Parameters.AddWithValue("@Col14", _FFSSetup.Col14);
                                cmd.Parameters.AddWithValue("@Col15", _FFSSetup.Col15);
                                cmd.Parameters.AddWithValue("@Col16", _FFSSetup.Col16);
                                cmd.Parameters.AddWithValue("@Col17", _FFSSetup.Col17);
                                cmd.Parameters.AddWithValue("@Col18", _FFSSetup.Col18);
                                cmd.Parameters.AddWithValue("@Col19", _FFSSetup.Col19);
                                cmd.Parameters.AddWithValue("@Col20", _FFSSetup.Col20);

                                cmd.Parameters.AddWithValue("@Col21", _FFSSetup.Col21);
                                cmd.Parameters.AddWithValue("@Col22", _FFSSetup.Col22);
                                cmd.Parameters.AddWithValue("@Col23", _FFSSetup.Col23);
                                cmd.Parameters.AddWithValue("@Col24", _FFSSetup.Col24);
                                cmd.Parameters.AddWithValue("@Col25", _FFSSetup.Col25);
                                cmd.Parameters.AddWithValue("@Col26", _FFSSetup.Col26);
                                cmd.Parameters.AddWithValue("@Col27", _FFSSetup.Col27);

                                cmd.Parameters.AddWithValue("@TemplateType", _FFSSetup.TemplateType);
                                cmd.Parameters.AddWithValue("@InceptionIndex", _FFSSetup.InceptionIndex);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FFSSetup.FFSSetupPK, "FFSSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FFSSetup where FFSSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FFSSetup.FundPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _FFSSetup.ValueDate);
                                cmd.Parameters.AddWithValue("@Col1", _FFSSetup.Col1);
                                cmd.Parameters.AddWithValue("@Col2", _FFSSetup.Col2);
                                cmd.Parameters.AddWithValue("@Col3", _FFSSetup.Col3);
                                cmd.Parameters.AddWithValue("@Col4", _FFSSetup.Col4);
                                cmd.Parameters.AddWithValue("@Col5", _FFSSetup.Col5);
                                cmd.Parameters.AddWithValue("@Col6", _FFSSetup.Col6);
                                cmd.Parameters.AddWithValue("@Col7", _FFSSetup.Col7);
                                cmd.Parameters.AddWithValue("@Col8", _FFSSetup.Col8);
                                cmd.Parameters.AddWithValue("@Col9", _FFSSetup.Col9);
                                cmd.Parameters.AddWithValue("@Col10", _FFSSetup.Col10);

                                cmd.Parameters.AddWithValue("@Col11", _FFSSetup.Col11);
                                cmd.Parameters.AddWithValue("@Col12", _FFSSetup.Col12);
                                cmd.Parameters.AddWithValue("@Col13", _FFSSetup.Col13);
                                cmd.Parameters.AddWithValue("@Col14", _FFSSetup.Col14);
                                cmd.Parameters.AddWithValue("@Col15", _FFSSetup.Col15);
                                cmd.Parameters.AddWithValue("@Col16", _FFSSetup.Col16);
                                cmd.Parameters.AddWithValue("@Col17", _FFSSetup.Col17);
                                cmd.Parameters.AddWithValue("@Col18", _FFSSetup.Col18);
                                cmd.Parameters.AddWithValue("@Col19", _FFSSetup.Col19);
                                cmd.Parameters.AddWithValue("@Col20", _FFSSetup.Col20);

                                cmd.Parameters.AddWithValue("@Col21", _FFSSetup.Col21);
                                cmd.Parameters.AddWithValue("@Col22", _FFSSetup.Col22);
                                cmd.Parameters.AddWithValue("@Col23", _FFSSetup.Col23);
                                cmd.Parameters.AddWithValue("@Col24", _FFSSetup.Col24);
                                cmd.Parameters.AddWithValue("@Col25", _FFSSetup.Col25);
                                cmd.Parameters.AddWithValue("@Col26", _FFSSetup.Col26);
                                cmd.Parameters.AddWithValue("@Col27", _FFSSetup.Col27);

                                cmd.Parameters.AddWithValue("@TemplateType", _FFSSetup.TemplateType);
                                cmd.Parameters.AddWithValue("@InceptionIndex", _FFSSetup.InceptionIndex);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FFSSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FFSSetup set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where FFSSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FFSSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FFSSetup.HistoryPK);
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

        public void FFSSetup_Approved(FFSSetup _FFSSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FFSSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FFSSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FFSSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FFSSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup.ApprovedUsersID);
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

        public void FFSSetup_Reject(FFSSetup _FFSSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FFSSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FFSSetup set status= 2,lastupdate=@lastupdate where FFSSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
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

        public void FFSSetup_Void(FFSSetup _FFSSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FFSSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where FFSSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FFSSetup.FFSSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FFSSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FFSSetup.VoidUsersID);
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

        public void GenerateFFS_ForCloseNav(FFSSetup _FFSSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        DECLARE @DateFrom DATETIME

                        IF (@ClientCode = '03')
                        BEGIN
                        select @dateFrom = '08/01/18'
                        END
                        ELSE
                        BEGIN
                        select @dateFrom = IssueDate from Fund where status = 2 and FundPK = @FundPK
                        END


                        DELETE from ReturnNav where DateFromZWorkingDays between @dateFrom and @Dateto and FundPK = @FundPK

                        DECLARE @StartDate DATETIME
                        DECLARE @EndDate DATETIME
         



                        set @StartDate = @DateFrom
                        set @EndDate = @DateTo

                        declare @IssueDate datetime

                        select @IssueDate = IssueDate from Fund where status = 2 and FundPK = @FundPK


                        CREATE TABLE #FFSFund
                        (
                            Date DATETIME,
                            FundPK INT
                        )

                        DECLARE @FFSFundPK int
                        DECLARE @FFSDate datetime


                        CREATE CLUSTERED INDEX indx_FFSFund ON #FFSFund (Date,FundPK);

                        INSERT INTO #FFSFund (Date,FundPK)
                        SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A, dbo.Fund WHERE status = 2
                        AND A.date BETWEEN @StartDate AND @DateTo and FundPK = @FundPK


                        DECLARE @CloseNAV TABLE
                        (
                            Date DATETIME,
                            FundPK INT,
                            AUM numeric(32, 2),
                            Nav numeric(22, 8)
                        )

                        DECLARE @FFFundPK int
                        DECLARE @FFDate datetime
                        DECLARE @FFAUM numeric(32, 2)
                        DECLARE @FFNav numeric(22, 8)



                        INSERT INTO @CloseNAV
                        (Date, FundPK, AUM, Nav)


                        SELECT A.Date,A.FundPK,B.AUM,B.Nav from #FFSFund A
                        left join[CloseNAV] B on A.FundPK = B.FundPK and B.status = 2
                        where B.DATE = (select max(date) From CloseNAV where date <= A.Date and FundPK = @FundPK and status = 2 and Nav > 0)  
                        and A.FundPK = @FundPK   and status = 2



                        Insert into ReturnNav
                        SELECT A.Date DateFromZWorkingDays, @FundPK, B.Date,B.AUM,B.Nav,
                        C.Date DateLastMonth, C.Nav NavlastMonth,(B.Nav / C.Nav) -1  ReturnLastMonth,
                        D.Date Datelast3Month, D.Nav NavLast3Month,(B.Nav / D.Nav) -1  Return3Month,
                        G.Date Datelast6Month, G.Nav NavLast6Month,(B.Nav / G.Nav) -1  Return6Month,
                        E.Date DateLast1Year, E.Nav NavLast1Year,(B.Nav / E.Nav) -1  ReturnLast1Year,
                        F.Date DateYTD, F.Nav NavYTD,(B.Nav / F.Nav) -1 ReturnYTD,(B.Nav / 1000) - 1 ReturnInception,A.IsHoliday
                            FROM dbo.ZDT_WorkingDays A
                        LEFT JOIN @CloseNAV B ON B.Date = CASE WHEN A.IsHoliday = 1 THEN A.DTM1 else A.Date END
                        LEFT JOIN @CloseNAV C ON C.Date = case when dbo.CheckTodayIsHoliday(dateadd(month, -1, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -1, A.Date), -1) else dateadd(month, -1, A.Date)  end
                        LEFT JOIN @CloseNAV D ON D.Date = case when dbo.CheckTodayIsHoliday(dateadd(month, -3, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -3, A.Date), -1) else dateadd(month, -3, A.Date)  end
                        LEFT JOIN @CloseNAV E ON E.Date = case when dbo.CheckTodayIsHoliday(dateadd(month, -12, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -12, A.Date), -1) else dateadd(month, -12, A.Date)  end
                        LEFT JOIN @CloseNAV F ON F.Date = case when dbo.CheckTodayIsHoliday(DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0))) = 1 then dbo.FWorkingDay(DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0)), -1) else DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0)) end
                        LEFT JOIN @CloseNAV G ON G.Date = case when EOMONTH(@DateTo) = A.Date then case when dbo.CheckTodayIsHoliday(EOMONTH(dateadd(month, -6, A.Date))) = 1 then dbo.FWorkingDay(EOMONTH(dateadd(month, -6, A.Date)), -1) else EOMONTH(dateadd(month, -6, A.Date)) end else case when dbo.CheckTodayIsHoliday(dateadd(month, -6, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -6, A.Date), -1) else dateadd(month, -6, A.Date)  end end
                        where A.Date BETWEEN @DateFrom AND @DateTo AND B.FundPK = @FundPK ";



                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Dateto", _FFSSetup.ValueDateFrom);
                        cmd.Parameters.AddWithValue("@FundPK", _FFSSetup.FundPK);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public void GenerateFFS_ForIndex(FFSSetup _FFSSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        declare @dateFrom datetime
                        IF (@ClientCode = '03')
                        BEGIN
                        select @dateFrom = '08/01/18'
                        END
                        ELSE
                        BEGIN
                        select @dateFrom = IssueDate from Fund where status = 2 and FundPK = @FundPK
                        END

                        declare @IndexPK int

                        select @IndexPK = IndexPK from FundIndex where status in (1,2) and FundPK = @FundPK

                        declare @IncBenchmark numeric(22, 12)

                        DELETE from ReturnIndex where DateFromZWorkingDays between @dateFrom and @Dateto and IndexPK = @IndexPK and FundPK = @FundPK

                        DECLARE @StartDate datetime
                        Declare @EndDate DATETIME




                        set @StartDate = @DateFrom
                        set @EndDate = @DateTo


                        DECLARE @Benchmark TABLE
                        (
                            Date DATETIME,
                            FundPK INT,
                            IndexPK INT,
                            Rate numeric(22, 8)
                        )

                        DECLARE @FFIndexPK int
                        DECLARE @FFDate datetime
                        DECLARE @FFRate numeric(22, 8)



                        declare @IssueDate datetime

                        select @IssueDate = IssueDate from Fund where status = 2 and FundPK = @FundPK





                        Declare A Cursor For

                            SELECT DISTINCT Date,IndexPK,CloseInd FROM dbo.BenchmarkIndex
                            where date BETWEEN @StartDate AND @DateTo and IndexPK = @IndexPK and status = 2
                        Open A
                        Fetch Next From A
                        INTO @FFDate, @FFIndexPK, @FFRate


                        WHILE @@FETCH_STATUS = 0
                        BEGIN
                            INSERT INTO @Benchmark
                                    (Date, FundPK, IndexPK, Rate )


                            SELECT @FFDate, @FundPK, @FFIndexPK, @FFRate


                            FETCH NEXT FROM A

                            INTO @FFDate, @FFIndexPK, @FFRate
                        END
                        CLOSE A
                        DEALLOCATE A




                        Insert into ReturnIndex
                        SELECT A.Date DateFromZWorkingDays, @FundPK, @IndexPK, B.Date,B.Rate,
                        C.Date DateLastMonth, C.Rate RatelastMonth,case when C.Rate = 0 then 0 else (B.Rate / C.Rate) - 1 end ReturnLastMonth,
                           D.Date Datelast3Month, D.Rate RateLast3Month,case when D.Rate = 0  then 0 else (B.Rate / D.Rate) - 1 end Return3Month,
                             G.Date Datelast6Month, G.Rate RateLast6Month,case when case when dbo.CheckTodayIsHoliday(dateadd(month, -6, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -6, A.Date), -1) else dateadd(month, -6, A.Date)  end >= @IssueDate then case when G.Rate = 0 then 0 else (B.Rate / G.Rate) - 1 end else 0 end Return6Month,
                                E.Date DateLast1Year, E.Rate RateLast1Year,case when case when dbo.CheckTodayIsHoliday(dateadd(month, -12, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -12, A.Date), -1) else dateadd(month, -12, A.Date)  end >= @IssueDate then case when E.Rate = 0 then 0 else (B.Rate / E.Rate) - 1 end else 0 end ReturnLast1Year,
                                   F.Date DateYTD, F.Rate RateYTD,case when case when dbo.CheckTodayIsHoliday(DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0))) = 1 then dbo.FWorkingDay(DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0)), -1) else DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0)) end >= @IssueDate then case when F.Rate = 0 then 0 else (B.Rate / F.Rate) - 1 end else 0 end ReturnYTD,0 ReturnInception
                                     FROM dbo.ZDT_WorkingDays A
                        LEFT JOIN @Benchmark B ON B.Date = CASE WHEN A.IsHoliday = 1 THEN A.DTM1 else A.Date END
                        LEFT JOIN @Benchmark C ON C.Date = case when dbo.CheckTodayIsHoliday(dateadd(month, -1, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -1, A.Date), -1) else dateadd(month, -1, A.Date)  end
                        LEFT JOIN @Benchmark D ON D.Date = case when dbo.CheckTodayIsHoliday(dateadd(month, -3, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -3, A.Date), -1) else dateadd(month, -3, A.Date)  end
                        LEFT JOIN @Benchmark G ON G.Date = case when EOMONTH(@DateTo) = A.Date then case when dbo.CheckTodayIsHoliday(EOMONTH(dateadd(month, -6, A.Date))) = 1 then dbo.FWorkingDay(EOMONTH(dateadd(month, -6, A.Date)), -1) else EOMONTH(dateadd(month, -6, A.Date)) end else case when dbo.CheckTodayIsHoliday(dateadd(month, -6, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -6, A.Date), -1) else dateadd(month, -6, A.Date)  end end
                        LEFT JOIN @Benchmark E ON E.Date = case when dbo.CheckTodayIsHoliday(dateadd(month, -12, A.Date)) = 1 then dbo.FWorkingDay(dateadd(month, -12, A.Date), -1) else dateadd(month, -12, A.Date)  end
                        LEFT JOIN @Benchmark F ON F.Date = case when dbo.CheckTodayIsHoliday(DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0))) = 1 then dbo.FWorkingDay(DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0)), -1) else DATEADD(dd, -1, DATEADD(yy, DATEDIFF(yy, 0, A.Date), 0)) end

                        where A.Date BETWEEN @DateFrom AND @DateTo AND B.IndexPK = @IndexPK and B.FundPK = @FundPK ";


                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Dateto", _FFSSetup.ValueDateFrom);
                        cmd.Parameters.AddWithValue("@FundPK", _FFSSetup.FundPK);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        // IMPORT FFS SETUP

        private DataTable CreateDataTableFromFFSSetupTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;



                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col3";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col4";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col5";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col6";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col7";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col8";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col9";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col10";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col11";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col12";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col13";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col14";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col15";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col16";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col17";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col18";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col19";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col20";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col21";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col22";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col23";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col24";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col25";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col26";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Col27";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InceptionIndex";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Image";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TemplateType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 2;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;
                        while (i <= end.Row)

                        {
                            dr = dt.NewRow();
                            if (worksheet.Cells[i, 1].Value == null)
                                dr["Date"] = "";
                            else
                                dr["Date"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["FundID"] = "";
                            else
                                dr["FundID"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["Col1"] = "";
                            else
                                dr["Col1"] = worksheet.Cells[i, 3].Value.ToString();

                            if (worksheet.Cells[i, 4].Value == null)
                                dr["Col2"] = "";
                            else
                                dr["Col2"] = worksheet.Cells[i, 4].Value.ToString();

                            if (worksheet.Cells[i, 5].Value == null)
                                dr["Col3"] = "";
                            else
                                dr["Col3"] = worksheet.Cells[i, 5].Value.ToString();

                            if (worksheet.Cells[i, 6].Value == null)
                                dr["Col4"] = "";
                            else
                                dr["Col4"] = worksheet.Cells[i, 6].Value.ToString();

                            if (worksheet.Cells[i, 7].Value == null)
                                dr["Col5"] = "";
                            else
                                dr["Col5"] = worksheet.Cells[i, 7].Value.ToString();

                            if (worksheet.Cells[i, 8].Value == null)
                                dr["Col6"] = "";
                            else
                                dr["Col6"] = worksheet.Cells[i, 8].Value.ToString();

                            if (worksheet.Cells[i, 9].Value == null)
                                dr["Col7"] = "";
                            else
                                dr["Col7"] = worksheet.Cells[i, 9].Value.ToString();

                            if (worksheet.Cells[i, 10].Value == null)
                                dr["Col8"] = 0;
                            else
                                dr["Col8"] = worksheet.Cells[i, 10].Value.ToString();

                            if (worksheet.Cells[i, 11].Value == null)
                                dr["Col9"] = 0;
                            else
                                dr["Col9"] = worksheet.Cells[i, 11].Value.ToString();

                            if (worksheet.Cells[i, 12].Value == null)
                                dr["Col10"] = 0;
                            else
                                dr["Col10"] = worksheet.Cells[i, 12].Value.ToString();

                            if (worksheet.Cells[i, 13].Value == null)
                                dr["Col11"] = 0;
                            else
                                dr["Col11"] = worksheet.Cells[i, 13].Value.ToString();

                            if (worksheet.Cells[i, 14].Value == null)
                                dr["Col12"] = 0;
                            else
                                dr["Col12"] = worksheet.Cells[i, 14].Value.ToString();

                            if (worksheet.Cells[i, 15].Value == null)
                                dr["Col13"] = 0;
                            else
                                dr["Col13"] = worksheet.Cells[i, 15].Value.ToString();

                            if (worksheet.Cells[i, 16].Value == null)
                                dr["Col14"] = 0;
                            else
                                dr["Col14"] = worksheet.Cells[i, 16].Value.ToString();

                            if (worksheet.Cells[i, 17].Value == null)
                                dr["Col15"] = "";
                            else
                                dr["Col15"] = worksheet.Cells[i, 17].Value.ToString();

                            if (worksheet.Cells[i, 18].Value == null)
                                dr["Col16"] = "";
                            else
                                dr["Col16"] = worksheet.Cells[i, 18].Value.ToString();


                            if (worksheet.Cells[i, 19].Value == null)
                                dr["Col17"] = "";
                            else
                                dr["Col17"] = worksheet.Cells[i, 19].Value.ToString();

                            if (worksheet.Cells[i, 20].Value == null)
                                dr["Col18"] = "";
                            else
                                dr["Col18"] = worksheet.Cells[i, 20].Value.ToString();

                            if (worksheet.Cells[i, 21].Value == null)
                                dr["Col19"] = "";
                            else
                                dr["Col19"] = worksheet.Cells[i, 21].Value.ToString();

                            if (worksheet.Cells[i, 22].Value == null)
                                dr["Col20"] = "";
                            else
                                dr["Col20"] = worksheet.Cells[i, 22].Value.ToString();

                            if (worksheet.Cells[i, 23].Value == null)
                                dr["Col21"] = "";
                            else
                                dr["Col21"] = worksheet.Cells[i, 23].Value.ToString();

                            if (worksheet.Cells[i, 24].Value == null)
                                dr["Col22"] = "";
                            else
                                dr["Col22"] = worksheet.Cells[i, 24].Value.ToString();

                            if (worksheet.Cells[i, 25].Value == null)
                                dr["Col23"] = "";
                            else
                                dr["Col23"] = worksheet.Cells[i, 25].Value.ToString();

                            if (worksheet.Cells[i, 26].Value == null)
                                dr["Col24"] = "";
                            else
                                dr["Col24"] = worksheet.Cells[i, 26].Value.ToString();

                            if (worksheet.Cells[i, 27].Value == null)
                                dr["Col25"] = "";
                            else
                                dr["Col25"] = worksheet.Cells[i, 27].Value.ToString();

                            if (worksheet.Cells[i, 28].Value == null)
                                dr["Col26"] = "";
                            else
                                dr["Col26"] = worksheet.Cells[i, 28].Value.ToString();

                            if (worksheet.Cells[i, 29].Value == null)
                                dr["Col27"] = "";
                            else
                                dr["Col27"] = worksheet.Cells[i, 29].Value.ToString();

                            if (worksheet.Cells[i, 30].Value == null)
                                dr["InceptionIndex"] = "";
                            else
                                dr["InceptionIndex"] = worksheet.Cells[i, 30].Value.ToString();

                            if (worksheet.Cells[i, 31].Value == null)
                                dr["Image"] = "";
                            else
                                dr["Image"] = worksheet.Cells[i, 31].Value.ToString();

                            if (worksheet.Cells[i, 32].Value == null)
                                dr["TemplateType"] = "";
                            else
                                dr["TemplateType"] = worksheet.Cells[i, 32].Value.ToString();


                            if (dr["Date"].Equals(null) != true ||
                                dr["FundID"].Equals(null) != true ||
                                dr["Col1"].Equals(null) != true ||
                                dr["Col2"].Equals(null) != true ||
                                dr["Col3"].Equals(null) != true ||
                                dr["Col4"].Equals(null) != true ||
                                dr["Col5"].Equals(null) != true ||
                                dr["Col6"].Equals(null) != true ||
                                dr["Col7"].Equals(null) != true ||
                                dr["Col8"].Equals(null) != true ||
                                dr["Col9"].Equals(null) != true ||
                                dr["Col10"].Equals(null) != true ||
                                dr["Col11"].Equals(null) != true ||
                                dr["Col12"].Equals(null) != true ||
                                dr["Col13"].Equals(null) != true ||
                                dr["Col14"].Equals(null) != true ||
                                dr["Col15"].Equals(null) != true ||
                                dr["Col16"].Equals(null) != true ||
                                dr["Col17"].Equals(null) != true ||
                                dr["Col18"].Equals(null) != true ||
                                dr["Col19"].Equals(null) != true ||
                                dr["Col20"].Equals(null) != true ||
                                dr["Col21"].Equals(null) != true ||
                                dr["Col22"].Equals(null) != true ||
                                dr["Col23"].Equals(null) != true ||
                                dr["Col24"].Equals(null) != true ||
                                dr["Col25"].Equals(null) != true ||
                                dr["Col26"].Equals(null) != true ||
                                dr["Col27"].Equals(null) != true ||
                                dr["InceptionIndex"].Equals(null) != true ||
                                dr["Image"].Equals(null) != true ||
                                dr["TemplateType"].Equals(null) != true

                                )


                            { dt.Rows.Add(dr); }
                            i++;

                        }
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public string ImportFFSSetupFromExcel(string _fileSource, string _userID)
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
                            cmd1.CommandText = "truncate table FFSSetupTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.FFSSetupTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromFFSSetupTempExcelFile(_fileSource));

                    }


                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            DateTime _dateTimeNow = DateTime.Now;
                            cmd1.CommandText = @" 
                            declare @Date datetime
                            select @Date = Date from FFSSetupTemp
                    
                            update FFSSetup set status = 3 where date = @Date
                                                                    
                            Declare @FFSSetupPK int        
                            select @FFSSetupPK = isnull(max(FFSSetupPK),0) From FFSSetup    
                                                                   
                    

                                    
                            INSERT INTO FFSSetup(FFSSetupPK,HistoryPK,Status,Date,FundPK,Col1,Col2,Col3,Col4,Col5,Col6,Col7,Col8,Col9,Col10,
                            Col11,Col12,Col13,Col14,Col15,Col16,Col17,Col18,Col19,Col20,
                            Col21,Col22,Col23,Col24,Col25,Col26,Col27,InceptionIndex,Image,TemplateType,
                            EntryUsersID,Entrytime,LastUpdate)

                            SELECT ROW_NUMBER() over(order by B.FundPK) + @FFSSetupPK,1,2,A.Date,B.FundPK,Col1,Col2,Col3,Col4,Col5,Col6,Col7,Col8,Col9,Col10,
                            Col11,Col12,Col13,Col14,Col15,Col16,Col17,Col18,Col19,Col20,
                            Col21,Col22,Col23,isnull(Col24,0),Col25,Col26,Col27,InceptionIndex,Image,TemplateType,
                            @UserID,@Lastupdate,@LastUpdate from FFSSetupTemp A 
                            Left Join Fund B on A.FundID = B.ID and B.status in(1,2)


                            if (select count(*) from FFSSetup where date = @Date and status in (1,2)) = (SELECT count(*) from FFSSetupTemp A 
                            Left Join Fund B on A.FundID = B.ID and B.status in(1,2))
                            select 'Import FFSSetup Success' A
                            else
                            select 'Import FFSSetup failed! Please Check the Data Again!' A
                                            ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@Lastupdate", _dateTimeNow);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = Convert.ToString(dr["A"]);
                                }

                                return _msg;
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