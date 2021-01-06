using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class COADestinationOneMappingRptReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[COADestinationOneMappingRpt] " +
                            "([COADestinationOneMappingRptPK],[HistoryPK],[Status],[COADestinationOnePK],[Row],[Col],[Operator],";
        string _paramaterCommand = "@COADestinationOnePK,@Row,@Col,@Operator,";

        //2

        private COADestinationOneMappingRpt setCOADestinationOneMappingRpt(SqlDataReader dr)
        {
            COADestinationOneMappingRpt M_COADestinationOneMappingRpt = new COADestinationOneMappingRpt();
            M_COADestinationOneMappingRpt.COADestinationOneMappingRptPK = Convert.ToInt32(dr["COADestinationOneMappingRptPK"]);
            M_COADestinationOneMappingRpt.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_COADestinationOneMappingRpt.Status = Convert.ToInt32(dr["Status"]);
            M_COADestinationOneMappingRpt.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_COADestinationOneMappingRpt.Notes = Convert.ToString(dr["Notes"]);
            M_COADestinationOneMappingRpt.COADestinationOnePK = Convert.ToInt32(dr["COADestinationOnePK"]);
            M_COADestinationOneMappingRpt.COADestinationOneID = Convert.ToString(dr["COADestinationOneID"]);
            M_COADestinationOneMappingRpt.COADestinationOneName = Convert.ToString(dr["COADestinationOneName"]);
            M_COADestinationOneMappingRpt.Row = Convert.ToInt32(dr["Row"]);
            M_COADestinationOneMappingRpt.Col = Convert.ToInt32(dr["Col"]);
            M_COADestinationOneMappingRpt.Operator = Convert.ToString(dr["Operator"]);
            M_COADestinationOneMappingRpt.EntryUsersID = dr["EntryUsersID"].ToString();
            M_COADestinationOneMappingRpt.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_COADestinationOneMappingRpt.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_COADestinationOneMappingRpt.VoidUsersID = dr["VoidUsersID"].ToString();
            M_COADestinationOneMappingRpt.EntryTime = dr["EntryTime"].ToString();
            M_COADestinationOneMappingRpt.UpdateTime = dr["UpdateTime"].ToString();
            M_COADestinationOneMappingRpt.ApprovedTime = dr["ApprovedTime"].ToString();
            M_COADestinationOneMappingRpt.VoidTime = dr["VoidTime"].ToString();
            M_COADestinationOneMappingRpt.DBUserID = dr["DBUserID"].ToString();
            M_COADestinationOneMappingRpt.DBTerminalID = dr["DBTerminalID"].ToString();
            M_COADestinationOneMappingRpt.LastUpdate = dr["LastUpdate"].ToString();
            M_COADestinationOneMappingRpt.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_COADestinationOneMappingRpt;
        }

        public List<COADestinationOneMappingRpt> COADestinationOneMappingRpt_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<COADestinationOneMappingRpt> L_COADestinationOneMappingRpt = new List<COADestinationOneMappingRpt>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COADestinationOneID, B.Name COADestinationOneName,A.* from COADestinationOneMappingRpt A 
                            left join COADestinationOne B on A.COADestinationOnePK = B.COADestinationOnePK and B.status = 2 
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COADestinationOneID, B.Name COADestinationOneName,A.* from COADestinationOneMappingRpt A 
                            left join COADestinationOne B on A.COADestinationOnePK = B.COADestinationOnePK and B.status = 2
                            order by COADestinationOnePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_COADestinationOneMappingRpt.Add(setCOADestinationOneMappingRpt(dr));
                                }
                            }
                            return L_COADestinationOneMappingRpt;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COADestinationOneMappingRpt_Add(COADestinationOneMappingRpt _COADestinationOneMappingRpt, bool _havePrivillege)
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
                                 "Select isnull(max(COADestinationOneMappingRptPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from COADestinationOneMappingRpt";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationOneMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(COADestinationOneMappingRptPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from COADestinationOneMappingRpt";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@COADestinationOnePK", _COADestinationOneMappingRpt.COADestinationOnePK);
                        cmd.Parameters.AddWithValue("@Row", _COADestinationOneMappingRpt.Row);
                        cmd.Parameters.AddWithValue("@Col", _COADestinationOneMappingRpt.Col);
                        cmd.Parameters.AddWithValue("@Operator", _COADestinationOneMappingRpt.Operator);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _COADestinationOneMappingRpt.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "COADestinationOneMappingRpt");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COADestinationOneMappingRpt_Update(COADestinationOneMappingRpt _COADestinationOneMappingRpt, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_COADestinationOneMappingRpt.COADestinationOneMappingRptPK, _COADestinationOneMappingRpt.HistoryPK, "COADestinationOneMappingRpt"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COADestinationOneMappingRpt set status=2, Notes=@Notes,COADestinationOnePK=@COADestinationOnePK,Row=@Row,Col=@Col,Operator=@Operator," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COADestinationOneMappingRptPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationOneMappingRpt.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                            cmd.Parameters.AddWithValue("@Notes", _COADestinationOneMappingRpt.Notes);
                            cmd.Parameters.AddWithValue("@COADestinationOnePK", _COADestinationOneMappingRpt.COADestinationOnePK);
                            cmd.Parameters.AddWithValue("@Row", _COADestinationOneMappingRpt.Row);
                            cmd.Parameters.AddWithValue("@Col", _COADestinationOneMappingRpt.Col);
                            cmd.Parameters.AddWithValue("@Operator", _COADestinationOneMappingRpt.Operator);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationOneMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationOneMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COADestinationOneMappingRpt set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COADestinationOneMappingRptPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationOneMappingRpt.EntryUsersID);
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
                                cmd.CommandText = "Update COADestinationOneMappingRpt set Notes=@Notes,COADestinationOnePK=@COADestinationOnePK,Row=@Row,Col=@Col,Operator=@Operator," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where COADestinationOneMappingRptPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationOneMappingRpt.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                                cmd.Parameters.AddWithValue("@Notes", _COADestinationOneMappingRpt.Notes);
                                cmd.Parameters.AddWithValue("@COADestinationOnePK", _COADestinationOneMappingRpt.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@Row", _COADestinationOneMappingRpt.Row);
                                cmd.Parameters.AddWithValue("@Col", _COADestinationOneMappingRpt.Col);
                                cmd.Parameters.AddWithValue("@Operator", _COADestinationOneMappingRpt.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationOneMappingRpt.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_COADestinationOneMappingRpt.COADestinationOneMappingRptPK, "COADestinationOneMappingRpt");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From COADestinationOneMappingRpt where COADestinationOneMappingRptPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationOneMappingRpt.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@COADestinationOnePK", _COADestinationOneMappingRpt.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@Row", _COADestinationOneMappingRpt.Row);
                                cmd.Parameters.AddWithValue("@Col", _COADestinationOneMappingRpt.Col);
                                cmd.Parameters.AddWithValue("@Operator", _COADestinationOneMappingRpt.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationOneMappingRpt.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update COADestinationOneMappingRpt set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where COADestinationOneMappingRptPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _COADestinationOneMappingRpt.Notes);
                                cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationOneMappingRpt.HistoryPK);
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

        public void COADestinationOneMappingRpt_Approved(COADestinationOneMappingRpt _COADestinationOneMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationOneMappingRpt set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where COADestinationOneMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationOneMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationOneMappingRpt.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COADestinationOneMappingRpt set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COADestinationOneMappingRptPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationOneMappingRpt.ApprovedUsersID);
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

        public void COADestinationOneMappingRpt_Reject(COADestinationOneMappingRpt _COADestinationOneMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationOneMappingRpt set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COADestinationOneMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationOneMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationOneMappingRpt.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COADestinationOneMappingRpt set status= 2,LastUpdate=@LastUpdate  where COADestinationOneMappingRptPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
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

        public void COADestinationOneMappingRpt_Void(COADestinationOneMappingRpt _COADestinationOneMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationOneMappingRpt set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COADestinationOneMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationOneMappingRpt.COADestinationOneMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationOneMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationOneMappingRpt.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )
    }
}