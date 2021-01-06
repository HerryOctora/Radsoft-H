using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
   public class FundAvgPriorityReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundAvgPriority] " +
                            "([FundAvgPriorityPK],[HistoryPK],[Status],[FundPK],[InstrumentPK],[Date],[Priority],";
        string _paramaterCommand = "@FundPK,@InstrumentPK,@Date,@Priority,";

        //2
        private FundAvgPriority setFundAvgPriority(SqlDataReader dr)
        {
            FundAvgPriority M_FundAvgPriority = new FundAvgPriority();
            M_FundAvgPriority.FundAvgPriorityPK = Convert.ToInt32(dr["FundAvgPriorityPK"]);
            M_FundAvgPriority.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundAvgPriority.Status = Convert.ToInt32(dr["Status"]);
            M_FundAvgPriority.StatusDesc = dr["StatusDesc"].ToString();
            M_FundAvgPriority.Notes = Convert.ToString(dr["Notes"]);

            M_FundAvgPriority.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundAvgPriority.FundID = dr["FundID"].ToString();
            M_FundAvgPriority.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_FundAvgPriority.InstrumentID = dr["InstrumentID"].ToString();
            M_FundAvgPriority.Date = Convert.ToDateTime(dr["Date"]);
            M_FundAvgPriority.Priority = Convert.ToInt32(dr["Priority"]);
            M_FundAvgPriority.PriorityID = dr["PriorityID"].ToString();

            M_FundAvgPriority.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundAvgPriority.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundAvgPriority.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundAvgPriority.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundAvgPriority.EntryTime = dr["EntryTime"].ToString();
            M_FundAvgPriority.UpdateTime = dr["UpdateTime"].ToString();
            M_FundAvgPriority.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundAvgPriority.VoidTime = dr["VoidTime"].ToString();
            M_FundAvgPriority.DBUserID = dr["DBUserID"].ToString();
            M_FundAvgPriority.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundAvgPriority.LastUpdate = dr["LastUpdate"].ToString();
            M_FundAvgPriority.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundAvgPriority;
        }

        public List<FundAvgPriority> FundAvgPriority_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundAvgPriority> L_FundAvgPriority = new List<FundAvgPriority>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"

                                Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                B.ID FundID,C.ID InstrumentID,case when A.Priority = 1 then 'BUY' when A.Priority = 2 then 'SELL' else '' end PriorityID,* from FundAvgPriority A
                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)                         
                                where A.status = @status 
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                                Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                B.ID FundID,C.ID InstrumentID,case when A.Priority = 1 then 'BUY' when A.Priority = 2 then 'SELL' else '' end PriorityID,* from FundAvgPriority A
                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2) 
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundAvgPriority.Add(setFundAvgPriority(dr));
                                }
                            }
                            return L_FundAvgPriority;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundAvgPriority_Add(FundAvgPriority _FundAvgPriority, bool _havePrivillege)
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
                                 "Select isnull(max(FundAvgPriorityPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FundAvgPriority";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundAvgPriority.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FundAvgPriorityPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundAvgPriority";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);

                        cmd.Parameters.AddWithValue("@FundPK", _FundAvgPriority.FundPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _FundAvgPriority.InstrumentPK);
                        cmd.Parameters.AddWithValue("@Date", _FundAvgPriority.Date);
                        cmd.Parameters.AddWithValue("@Priority", _FundAvgPriority.Priority);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundAvgPriority.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundAvgPriority");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundAvgPriority_Update(FundAvgPriority _FundAvgPriority, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundAvgPriority.FundAvgPriorityPK, _FundAvgPriority.HistoryPK, "FundAvgPriority");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundAvgPriority set status=2, Notes=@Notes,FundPK=@FundPK,InstrumentPK=@InstrumentPK,Date=@Date,Priority=@Priority," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundAvgPriorityPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundAvgPriority.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundAvgPriority.Notes);

                            cmd.Parameters.AddWithValue("@FundPK", _FundAvgPriority.FundPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _FundAvgPriority.InstrumentPK);
                            cmd.Parameters.AddWithValue("@Date", _FundAvgPriority.Date);
                            cmd.Parameters.AddWithValue("@Priority", _FundAvgPriority.Priority);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundAvgPriority.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundAvgPriority.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundAvgPriority set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundAvgPriorityPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundAvgPriority.EntryUsersID);
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
                                cmd.CommandText = "Update FundAvgPriority set Notes=@Notes,Date=@Date,RangeFrom=@RangeFrom,RangeTo=@RangeTo,Percentage=@Percentage," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundAvgPriorityPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundAvgPriority.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundAvgPriority.Notes);

                                cmd.Parameters.AddWithValue("@FundPK", _FundAvgPriority.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _FundAvgPriority.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Date", _FundAvgPriority.Date);
                                cmd.Parameters.AddWithValue("@Priority", _FundAvgPriority.Priority);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundAvgPriority.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FundAvgPriority.FundAvgPriorityPK, "FundAvgPriority");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundAvgPriority where FundAvgPriorityPK=@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundAvgPriority.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);

                                cmd.Parameters.AddWithValue("@FundPK", _FundAvgPriority.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _FundAvgPriority.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Date", _FundAvgPriority.Date);
                                cmd.Parameters.AddWithValue("@Priority", _FundAvgPriority.Priority);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundAvgPriority.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundAvgPriority set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where FundAvgPriorityPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundAvgPriority.FundAvgPriorityPK);
                                cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundAvgPriority.HistoryPK);
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

        public void FundAvgPriority_Approved(FundAvgPriority _FundAvgPriority)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundAvgPriority set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FundAvgPriorityPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundAvgPriority.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundAvgPriority.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundAvgPriority set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundAvgPriorityPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundAvgPriority.ApprovedUsersID);
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

        public void FundAvgPriority_Reject(FundAvgPriority _FundAvgPriority)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundAvgPriority set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundAvgPriorityPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundAvgPriority.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundAvgPriority.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundAvgPriority set status= 2,LastUpdate=@LastUpdate where FundAvgPriorityPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
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

        public void FundAvgPriority_Void(FundAvgPriority _FundAvgPriority)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundAvgPriority set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundAvgPriorityPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundAvgPriority.FundAvgPriorityPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundAvgPriority.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundAvgPriority.VoidUsersID);
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

        //public List<GroupsCombo> Groups_Combo()
        //{

        //    try
        //    {
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            List<GroupsCombo> L_Groups = new List<GroupsCombo>();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText = "SELECT  GroupsPK,ID +' - '+ Name ID, Name FROM [Groups]  where status = 2 order by GroupsPK";
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    if (dr.HasRows)
        //                    {
        //                        while (dr.Read())
        //                        {
        //                            GroupsCombo M_Groups = new GroupsCombo();
        //                            M_Groups.GroupsPK = Convert.ToInt32(dr["GroupsPK"]);
        //                            M_Groups.ID = Convert.ToString(dr["ID"]);
        //                            M_Groups.Name = Convert.ToString(dr["Name"]);
        //                            L_Groups.Add(M_Groups);
        //                        }

        //                    }
        //                    return L_Groups;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }


        //}



    }
}
