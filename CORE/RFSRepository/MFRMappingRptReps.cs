using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class MFRMappingRptReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[MFRMappingRpt] " +
                            "([MFRMappingRptPK],[HistoryPK],[Status],[MFRItemRptPK],[Row],[Col],[COADestinationOnePK],[Operator],";
        string _paramaterCommand = "@MFRItemRptPK,@Row,@Col,@COADestinationOnePK,@Operator,";

        //2

        private MFRMappingRpt setMFRMappingRpt(SqlDataReader dr)
        {
            MFRMappingRpt M_MFRMappingRpt = new MFRMappingRpt();
            M_MFRMappingRpt.MFRMappingRptPK = Convert.ToInt32(dr["MFRMappingRptPK"]);
            M_MFRMappingRpt.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_MFRMappingRpt.Status = Convert.ToInt32(dr["Status"]);
            M_MFRMappingRpt.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_MFRMappingRpt.Notes = Convert.ToString(dr["Notes"]);
            M_MFRMappingRpt.MFRItemRptPK = Convert.ToInt32(dr["MFRItemRptPK"]);
            M_MFRMappingRpt.MFRItemRptItemText = dr["MFRItemRptItemText"].ToString();
            M_MFRMappingRpt.Row = Convert.ToInt32(dr["Row"]);
            M_MFRMappingRpt.Col = Convert.ToInt32(dr["Col"]);
            M_MFRMappingRpt.COADestinationOnePK = Convert.ToInt32(dr["COADestinationOnePK"]);
            M_MFRMappingRpt.COADestinationOneID = dr["COADestinationOneID"].ToString();
            M_MFRMappingRpt.COADestinationOneName = dr["COADestinationOneName"].ToString();
            M_MFRMappingRpt.Operator = dr["Operator"].ToString();
            M_MFRMappingRpt.EntryUsersID = dr["EntryUsersID"].ToString();
            M_MFRMappingRpt.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_MFRMappingRpt.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_MFRMappingRpt.VoidUsersID = dr["VoidUsersID"].ToString();
            M_MFRMappingRpt.EntryTime = dr["EntryTime"].ToString();
            M_MFRMappingRpt.UpdateTime = dr["UpdateTime"].ToString();
            M_MFRMappingRpt.ApprovedTime = dr["ApprovedTime"].ToString();
            M_MFRMappingRpt.VoidTime = dr["VoidTime"].ToString();
            M_MFRMappingRpt.DBUserID = dr["DBUserID"].ToString();
            M_MFRMappingRpt.DBTerminalID = dr["DBTerminalID"].ToString();
            M_MFRMappingRpt.LastUpdate = dr["LastUpdate"].ToString();
            M_MFRMappingRpt.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_MFRMappingRpt;
        }

        public List<MFRMappingRpt> MFRMappingRpt_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MFRMappingRpt> L_MFRMappingRpt = new List<MFRMappingRpt>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ItemText MFRItemRptItemText,C.ID COADestinationOneID,C.Name COADestinationOneName, A.* from MFRMappingRpt A
                                                left join MFRItemRpt B on A.MFRItemRptPK = B.MFRItemRptPK and B.status = 2 
                                                left join COAFromSource C on A.COADestinationOnePK = C.COAFromSourcePK  and C.status = 2 
                                                where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ItemText MFRItemRptItemText,C.ID COADestinationOneID,C.Name COADestinationOneName, A.* from MFRMappingRpt A
                                                left join MFRItemRpt B on A.MFRItemRptPK = B.MFRItemRptPK and B.status = 2 
                                                left join COAFromSource C on A.COADestinationOnePK = C.COAFromSourcePK  and C.status = 2 
                                                order by MFRItemRptPK,COADestinationOnePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MFRMappingRpt.Add(setMFRMappingRpt(dr));
                                }
                            }
                            return L_MFRMappingRpt;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int MFRMappingRpt_Add(MFRMappingRpt _MFRMappingRpt, bool _havePrivillege)
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
                                 "Select isnull(max(MFRMappingRptPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from MFRMappingRpt";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFRMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(MFRMappingRptPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from MFRMappingRpt";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@MFRItemRptPK", _MFRMappingRpt.MFRItemRptPK);
                        cmd.Parameters.AddWithValue("@Row", _MFRMappingRpt.Row);
                        cmd.Parameters.AddWithValue("@Col", _MFRMappingRpt.Col);
                        cmd.Parameters.AddWithValue("@COADestinationOnePK", _MFRMappingRpt.COADestinationOnePK);
                        cmd.Parameters.AddWithValue("@Operator", _MFRMappingRpt.Operator);                        
                        cmd.Parameters.AddWithValue("@EntryUsersID", _MFRMappingRpt.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "MFRMappingRpt");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int MFRMappingRpt_Update(MFRMappingRpt _MFRMappingRpt, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_MFRMappingRpt.MFRMappingRptPK, _MFRMappingRpt.HistoryPK, "MFRMappingRpt");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MFRMappingRpt set status=2, Notes=@Notes,MFRItemRptPK=@MFRItemRptPK,Row=@Row,Col=@Col,COADestinationOnePK=@COADestinationOnePK,Operator=@Operator," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where MFRMappingRptPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _MFRMappingRpt.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                            cmd.Parameters.AddWithValue("@Notes", _MFRMappingRpt.Notes);
                            cmd.Parameters.AddWithValue("@MFRItemRptPK", _MFRMappingRpt.MFRItemRptPK);
                            cmd.Parameters.AddWithValue("@Row", _MFRMappingRpt.Row);
                            cmd.Parameters.AddWithValue("@Col", _MFRMappingRpt.Col);
                            cmd.Parameters.AddWithValue("@COADestinationOnePK", _MFRMappingRpt.COADestinationOnePK);
                            cmd.Parameters.AddWithValue("@Operator", _MFRMappingRpt.Operator); 
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _MFRMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFRMappingRpt.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update MFRMappingRpt set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MFRMappingRptPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _MFRMappingRpt.EntryUsersID);
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
                                cmd.CommandText = "Update MFRMappingRpt set Notes=@Notes,MFRItemRptPK=@MFRItemRptPK,Row=@Row,Col=@Col,COADestinationOnePK=@COADestinationOnePK,Operator=@Operator," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where MFRMappingRptPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFRMappingRpt.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                                cmd.Parameters.AddWithValue("@Notes", _MFRMappingRpt.Notes);
                                cmd.Parameters.AddWithValue("@MFRItemRptPK", _MFRMappingRpt.MFRItemRptPK);
                                cmd.Parameters.AddWithValue("@Row", _MFRMappingRpt.Row);
                                cmd.Parameters.AddWithValue("@Col", _MFRMappingRpt.Col);
                                cmd.Parameters.AddWithValue("@COADestinationOnePK", _MFRMappingRpt.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@Operator", _MFRMappingRpt.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MFRMappingRpt.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_MFRMappingRpt.MFRMappingRptPK, "MFRMappingRpt");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From MFRMappingRpt where MFRMappingRptPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFRMappingRpt.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@MFRItemRptPK", _MFRMappingRpt.MFRItemRptPK);
                                cmd.Parameters.AddWithValue("@Row", _MFRMappingRpt.Row);
                                cmd.Parameters.AddWithValue("@Col", _MFRMappingRpt.Col);
                                cmd.Parameters.AddWithValue("@COADestinationOnePK", _MFRMappingRpt.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@Operator", _MFRMappingRpt.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _MFRMappingRpt.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update MFRMappingRpt set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where MFRMappingRptPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _MFRMappingRpt.Notes);
                                cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _MFRMappingRpt.HistoryPK);
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

        public void MFRMappingRpt_Approved(MFRMappingRpt _MFRMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFRMappingRpt set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where MFRMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFRMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _MFRMappingRpt.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MFRMappingRpt set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MFRMappingRptPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFRMappingRpt.ApprovedUsersID);
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

        public void MFRMappingRpt_Reject(MFRMappingRpt _MFRMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFRMappingRpt set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MFRMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFRMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFRMappingRpt.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MFRMappingRpt set status= 2,LastUpdate=@LastUpdate where MFRMappingRptPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
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

        public void MFRMappingRpt_Void(MFRMappingRpt _MFRMappingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MFRMappingRpt set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MFRMappingRptPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MFRMappingRpt.MFRMappingRptPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MFRMappingRpt.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MFRMappingRpt.VoidUsersID);
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

        public int Validate_CheckRowMapping(int _MFRItemRptPK, int _row)
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
                            Declare @MFRMappingRptPK int
                            Declare @RowItem int
                            Declare @RowCursor int
                            IF EXISTS (Select MFRItemRptPK from MFRMappingRpt where MFRItemRptPK = @MFRItemRptPK and status  = 2)
                            BEGIN
	                            select @RowItem = Row from MFRMappingRpt where MFRItemRptPK = @MFRItemRptPK and status  = 2
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
			                        Select MFRMappingRptPK,Row From MFRMappingRpt where Row >= @Row and status = 2
	                                Open A
                                    Fetch Next From A
                                    Into @MFRMappingRptPK,@RowCursor
	                                While @@FETCH_STATUS = 0
                                    BEGIN
		                            Update MFRMappingRpt set Row = @RowCursor + 1 where Row = @Row and status  = 2
	                                Fetch next From A Into @MFRMappingRptPK,@RowCursor
                                    END
                                    Close A
                                    Deallocate A 
		                            Select 1 Result
	                            END
                            END
                           ";

                        cmd.Parameters.AddWithValue("@MFRItemRptPK", _MFRItemRptPK);
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