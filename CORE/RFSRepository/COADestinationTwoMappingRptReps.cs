using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class COADestinationTwoMappingRptReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[COADestinationTwoMappingRpt] " +
                            "([COADestinationTwoMappingRptPK],[HistoryPK],[Status],[COADestinationTwoPK],[Row],[Col],[Operator],";
        string _paramaterCommand = "@COADestinationTwoPK,@Row,@Col,@Operator,";

        //2

        private COADestinationTwoMappingRpt setCOADestinationTwoMappingRpt(SqlDataReader dr)
        {
            COADestinationTwoMappingRpt M_COADestinationTwoMappingRpt = new COADestinationTwoMappingRpt();
            M_COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK = Convert.ToInt32(dr["COADestinationTwoMappingRptPK"]);
            M_COADestinationTwoMappingRpt.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_COADestinationTwoMappingRpt.Status = Convert.ToInt32(dr["Status"]);
            M_COADestinationTwoMappingRpt.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_COADestinationTwoMappingRpt.Notes = Convert.ToString(dr["Notes"]);
            M_COADestinationTwoMappingRpt.COADestinationTwoPK = Convert.ToInt32(dr["COADestinationTwoPK"]);
            M_COADestinationTwoMappingRpt.COADestinationTwoID = dr["COADestinationTwoID"].ToString();
            M_COADestinationTwoMappingRpt.COADestinationtwoName = dr["COADestinationTwoName"].ToString();
            M_COADestinationTwoMappingRpt.Row = Convert.ToInt32(dr["Row"]);
            M_COADestinationTwoMappingRpt.Col = Convert.ToInt32(dr["Col"]);
            M_COADestinationTwoMappingRpt.Operator = dr["Operator"].ToString();
            M_COADestinationTwoMappingRpt.EntryUsersID = dr["EntryUsersID"].ToString();
            M_COADestinationTwoMappingRpt.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_COADestinationTwoMappingRpt.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_COADestinationTwoMappingRpt.VoidUsersID = dr["VoidUsersID"].ToString();
            M_COADestinationTwoMappingRpt.EntryTime = dr["EntryTime"].ToString();
            M_COADestinationTwoMappingRpt.UpdateTime = dr["UpdateTime"].ToString();
            M_COADestinationTwoMappingRpt.ApprovedTime = dr["ApprovedTime"].ToString();
            M_COADestinationTwoMappingRpt.VoidTime = dr["VoidTime"].ToString();
            M_COADestinationTwoMappingRpt.DBUserID = dr["DBUserID"].ToString();
            M_COADestinationTwoMappingRpt.DBTerminalID = dr["DBTerminalID"].ToString();
            M_COADestinationTwoMappingRpt.LastUpdate = dr["LastUpdate"].ToString();
            M_COADestinationTwoMappingRpt.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_COADestinationTwoMappingRpt;
        }

        public List<COADestinationTwoMappingRpt> COADestinationTwoMappingRpt_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<COADestinationTwoMappingRpt> L_COADestinationTwoMappingRpt = new List<COADestinationTwoMappingRpt>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COADestinationTwoID,B.Name COADestinationTwoName, A.* from COADestinationTwoMappingRpt A
                                                left join COADestinationTwo B on A.COADestinationTwoPK = B.COADestinationTwoPK and B.status = 2 
                                                where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COADestinationTwoID,B.Name COADestinationTwoName, A.* from COADestinationTwoMappingRpt A
                                                left join COADestinationTwo B on A.COADestinationTwoPK = B.COADestinationTwoPK and B.status = 2 
                                                order by COADestinationTwoPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_COADestinationTwoMappingRpt.Add(setCOADestinationTwoMappingRpt(dr));
                                }
                            }
                            return L_COADestinationTwoMappingRpt;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COADestinationTwoMappingRpt_Add(COADestinationTwoMappingRpt _COADestinationTwoMappingRpt, bool _havePrivillege)
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
                                 "Select isnull(max(COADestinationTwoMappingRptPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from COADestinationTwoMappingRpt";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationTwoMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(COADestinationTwoMappingRptPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from COADestinationTwoMappingRpt";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@COADestinationTwoPK", _COADestinationTwoMappingRpt.COADestinationTwoPK);
                        cmd.Parameters.AddWithValue("@Row", _COADestinationTwoMappingRpt.Row);
                        cmd.Parameters.AddWithValue("@Col", _COADestinationTwoMappingRpt.Col);
                        cmd.Parameters.AddWithValue("@Operator", _COADestinationTwoMappingRpt.Operator);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _COADestinationTwoMappingRpt.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "COADestinationTwoMappingRpt");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int COADestinationTwoMappingRpt_Update(COADestinationTwoMappingRpt _COADestinationTwoMappingRpt, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK, _COADestinationTwoMappingRpt.HistoryPK, "COADestinationTwoMappingRpt");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COADestinationTwoMappingRpt set status=2, Notes=@Notes,COADestinationTwoPK=@COADestinationTwoPK,Row=@Row,Col=@Col,Operator=@Operator," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COADestinationTwoMappingRptPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationTwoMappingRpt.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                            cmd.Parameters.AddWithValue("@Notes", _COADestinationTwoMappingRpt.Notes);
                            cmd.Parameters.AddWithValue("@COADestinationTwoPK", _COADestinationTwoMappingRpt.COADestinationTwoPK);
                            cmd.Parameters.AddWithValue("@Row", _COADestinationTwoMappingRpt.Row);
                            cmd.Parameters.AddWithValue("@Col", _COADestinationTwoMappingRpt.Col);
                            cmd.Parameters.AddWithValue("@Operator", _COADestinationTwoMappingRpt.Operator);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationTwoMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationTwoMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update COADestinationTwoMappingRpt set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COADestinationTwoMappingRptPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationTwoMappingRpt.EntryUsersID);
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
                                cmd.CommandText = "Update COADestinationTwoMappingRpt set Notes=@Notes,COADestinationTwoPK=@COADestinationTwoPK,Row=@Row,Col=@Col,Operator=@Operator," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where COADestinationTwoMappingRptPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationTwoMappingRpt.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                                cmd.Parameters.AddWithValue("@Notes", _COADestinationTwoMappingRpt.Notes);
                                cmd.Parameters.AddWithValue("@COADestinationTwoPK", _COADestinationTwoMappingRpt.COADestinationTwoPK);
                                cmd.Parameters.AddWithValue("@Row", _COADestinationTwoMappingRpt.Row);
                                cmd.Parameters.AddWithValue("@Col", _COADestinationTwoMappingRpt.Col);
                                cmd.Parameters.AddWithValue("@Operator", _COADestinationTwoMappingRpt.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationTwoMappingRpt.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK, "COADestinationTwoMappingRpt");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From COADestinationTwoMappingRpt where COADestinationTwoMappingRptPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationTwoMappingRpt.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@COADestinationTwoPK", _COADestinationTwoMappingRpt.COADestinationTwoPK);
                                cmd.Parameters.AddWithValue("@Row", _COADestinationTwoMappingRpt.Row);
                                cmd.Parameters.AddWithValue("@Col", _COADestinationTwoMappingRpt.Col);
                                cmd.Parameters.AddWithValue("@Operator", _COADestinationTwoMappingRpt.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _COADestinationTwoMappingRpt.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update COADestinationTwoMappingRpt set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where COADestinationTwoMappingRptPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _COADestinationTwoMappingRpt.Notes);
                                cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _COADestinationTwoMappingRpt.HistoryPK);
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

        public void COADestinationTwoMappingRpt_Approved(COADestinationTwoMappingRpt _COADestinationTwoMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationTwoMappingRpt set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where COADestinationTwoMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationTwoMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _COADestinationTwoMappingRpt.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COADestinationTwoMappingRpt set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where COADestinationTwoMappingRptPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationTwoMappingRpt.ApprovedUsersID);
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

        public void COADestinationTwoMappingRpt_Reject(COADestinationTwoMappingRpt _COADestinationTwoMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationTwoMappingRpt set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COADestinationTwoMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationTwoMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationTwoMappingRpt.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update COADestinationTwoMappingRpt set status= 2,LastUpdate=@LastUpdate where COADestinationTwoMappingRptPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
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

        public void COADestinationTwoMappingRpt_Void(COADestinationTwoMappingRpt _COADestinationTwoMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update COADestinationTwoMappingRpt set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where COADestinationTwoMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _COADestinationTwoMappingRpt.COADestinationTwoMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _COADestinationTwoMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _COADestinationTwoMappingRpt.VoidUsersID);
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

        public int Validate_CheckRowMapping(int _COADestinationTwoPK, int _row)
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
                            Declare @COADestinationTwoMappingRptPK int
                            Declare @RowItem int
                            Declare @RowCursor int
                            IF EXISTS (Select COADestinationTwoPK from COADestinationTwoMappingRpt where COADestinationTwoPK = @COADestinationTwoPK and status  = 2)
                            BEGIN
	                            select @RowItem = Row from COADestinationTwoMappingRpt where COADestinationTwoPK = @COADestinationTwoPK and status  = 2
	                            IF (@RowItem = @Row) 
	                            BEGIN
		                            Select 1 Result
	                            END
	                            ELSE
	                            BEGIN
	                                Select 2 Result
	                            END
                            END
                            ELSE
                            BEGIN
	                            BEGIN
	                                DECLARE A CURSOR FOR 
			                        Select COADestinationTwoPK,Row From COADestinationTwoMappingRpt where Row >= @Row and status = 2
	                                Open A
                                    Fetch Next From A
                                    Into @COADestinationTwoPK,@RowCursor
	                                While @@FETCH_STATUS = 0
                                    BEGIN
		                            Update COADestinationTwoMappingRpt set Row = @RowCursor + 1 where Row = @RowCursor and status  = 2 and COADestinationTwoPK = @COADestinationTwoPK
	                                Fetch next From A Into @COADestinationTwoPK,@RowCursor
                                    END
                                    Close A
                                    Deallocate A 
		                            Select 1 Result
	                            END
                            END
                           ";

                        cmd.Parameters.AddWithValue("@COADestinationTwoPK", _COADestinationTwoPK);
                        cmd.Parameters.AddWithValue("@Row", _row);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);

                            }
                            return 0;
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