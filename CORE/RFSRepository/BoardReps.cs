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
    public class BoardReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Board] " +
                            "([BoardPK],[HistoryPK],[Status],[ID],[Name],[Type],";
        string _paramaterCommand = "@ID,@Name,@Type,";

        //2
        private Board setBoard(SqlDataReader dr)
        {
            Board M_Board = new Board();
            M_Board.BoardPK = Convert.ToInt32(dr["BoardPK"]);
            M_Board.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Board.Status = Convert.ToInt32(dr["Status"]);
            M_Board.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Board.Notes = Convert.ToString(dr["Notes"]);
            M_Board.ID = dr["ID"].ToString();
            M_Board.Name = dr["Name"].ToString();
            M_Board.Type = Convert.ToString(dr["Type"]);
            M_Board.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_Board.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Board.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Board.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Board.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Board.EntryTime = dr["EntryTime"].ToString();
            M_Board.UpdateTime = dr["UpdateTime"].ToString();
            M_Board.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Board.VoidTime = dr["VoidTime"].ToString();
            M_Board.DBUserID = dr["DBUserID"].ToString();
            M_Board.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Board.LastUpdate = dr["LastUpdate"].ToString();
            M_Board.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Board;
        }

        public List<Board> Board_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Board> L_Board = new List<Board>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,* from Board  B left join " +
                            " MasterValue MV on MV.Code = B.Type and MV.ID ='BoardType' and MV.Status = 2 " +
                            " where B.status = @status order by BoardPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,* from Board  B left join " +
                            " MasterValue MV on MV.Code = B.Type and MV.ID ='BoardType' and MV.Status = 2  order by BoardPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Board.Add(setBoard(dr));
                                }
                            }
                            return L_Board;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Board_Add(Board _board, bool _havePrivillege)
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
                                 "Select isnull(max(BoardPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From Board";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _board.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(BoardPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From Board";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _board.ID);
                        cmd.Parameters.AddWithValue("@Name", _board.Name);
                        cmd.Parameters.AddWithValue("@Type", _board.Type);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _board.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Board");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public int Board_Update(Board _board, bool _havePrivillege)
        {     
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_board.BoardPK, _board.HistoryPK, "board");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Board set status=2, Notes=@Notes,ID=@ID,Name=@Name,Type=@Type," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where BoardPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _board.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                            cmd.Parameters.AddWithValue("@ID", _board.ID);
                            cmd.Parameters.AddWithValue("@Notes", _board.Notes);
                            cmd.Parameters.AddWithValue("@Name", _board.Name);
                            cmd.Parameters.AddWithValue("@Type", _board.Type);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _board.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _board.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Board set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BoardPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _board.EntryUsersID);
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
                                cmd.CommandText = "Update Board set Notes=@Notes,ID=@ID,Name=@Name,Type=@Type," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where BoardPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _board.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                                cmd.Parameters.AddWithValue("@ID", _board.ID);
                                cmd.Parameters.AddWithValue("@Notes", _board.Notes);
                                cmd.Parameters.AddWithValue("@Name", _board.Name);
                                cmd.Parameters.AddWithValue("@Type", _board.Type);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _board.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_board.BoardPK, "Board");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Board where BoardPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _board.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _board.ID);
                                cmd.Parameters.AddWithValue("@Name", _board.Name);
                                cmd.Parameters.AddWithValue("@Type", _board.Type);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _board.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Board set status= 4,Notes=@Notes," +
                                    "LastUpdate=LastUpdate where BoardPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _board.Notes);
                                cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _board.HistoryPK);
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

        public void Board_Approved(Board _board)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Board set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where BoardPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                        cmd.Parameters.AddWithValue("@historyPK", _board.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _board.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Board set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BoardPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _board.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public void Board_Reject(Board _board)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Board set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BoardPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                        cmd.Parameters.AddWithValue("@historyPK", _board.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _board.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Board set status= 2,LastUpdate=@LastUpdate where BoardPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public void Board_Void(Board _board)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Board set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BoardPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _board.BoardPK);
                        cmd.Parameters.AddWithValue("@historyPK", _board.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _board.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
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

        public List<BoardCombo> Board_Combo()
        {
            
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BoardCombo> L_Board = new List<BoardCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  BoardPK,ID + ' - ' + Name ID, Name FROM [Board]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BoardCombo M_Board = new BoardCombo();
                                    M_Board.BoardPK = Convert.ToInt32(dr["BoardPK"]);
                                    M_Board.ID = Convert.ToString(dr["ID"]);
                                    M_Board.Name = Convert.ToString(dr["Name"]);
                                    L_Board.Add(M_Board);
                                }

                            }
                            return L_Board;
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