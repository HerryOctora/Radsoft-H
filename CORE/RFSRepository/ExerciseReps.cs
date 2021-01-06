using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class ExerciseReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Exercise] " +
                            "([ExercisePK],[HistoryPK],[Status],[Date],[DistributionDate],[Type],[InstrumentPK],[FundPK],[InstrumentRightsPK],[BalanceRights],[BalanceExercise],[Price],";
        string _paramaterCommand = "@Date,@DistributionDate,@Type,@InstrumentPK,@FundPK,@InstrumentRightsPK,@BalanceRights,@BalanceExercise,@Price,";

        private Exercise setExercise(SqlDataReader dr)
        {
            Exercise M_Exercise = new Exercise();
            M_Exercise.ExercisePK = Convert.ToInt32(dr["ExercisePK"]);
            M_Exercise.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Exercise.Status = Convert.ToInt32(dr["Status"]);
            M_Exercise.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Exercise.Notes = Convert.ToString(dr["Notes"]);
            M_Exercise.Type = Convert.ToInt32(dr["Type"]);
            M_Exercise.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_Exercise.Date = dr["Date"].ToString();
            M_Exercise.DistributionDate = dr["DistributionDate"].ToString();
            M_Exercise.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_Exercise.FundID = Convert.ToString(dr["FundID"]);
            M_Exercise.BalanceRights = Convert.ToDecimal(dr["BalanceRights"]);
            M_Exercise.BalanceExercise = Convert.ToDecimal(dr["BalanceExercise"]);
            M_Exercise.Price = Convert.ToDecimal(dr["Price"]);
            M_Exercise.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_Exercise.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_Exercise.InstrumentRightsPK = Convert.ToInt32(dr["InstrumentRightsPK"]);
            M_Exercise.InstrumentRightsID = Convert.ToString(dr["InstrumentRightsID"]);
            M_Exercise.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Exercise.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Exercise.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Exercise.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Exercise.EntryTime = dr["EntryTime"].ToString();
            M_Exercise.UpdateTime = dr["UpdateTime"].ToString();
            M_Exercise.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Exercise.VoidTime = dr["VoidTime"].ToString();
            M_Exercise.DBUserID = dr["DBUserID"].ToString();
            M_Exercise.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Exercise.LastUpdate = dr["LastUpdate"].ToString();
            M_Exercise.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Exercise;
        }

        public List<Exercise> Exercise_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Exercise> L_Exercise = new List<Exercise>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
case when A.Type=1 then 'RIGHTS' else  'WARRANT'END TypeDesc,
                            B.InstrumentPK,B.ID InstrumentID,C.ID InstrumentRightsID,D.ID FundID , A.* from Exercise A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status  = 2
							left join Instrument C on A.InstrumentRightsPK = C.InstrumentPK and C.Status = 2
							left join Fund D on A.FundPK = D.FundPK and D.Status = 2  
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
case when A.Type=1 then 'RIGHTS' else  'WARRANT'END TypeDesc,
                            B.InstrumentPK,B.ID InstrumentID,C.ID InstrumentRightsID,D.ID FundID , A.* from Exercise A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status  = 2
							left join Instrument C on A.InstrumentRightsPK = C.InstrumentPK and C.Status = 2
							left join Fund D on A.FundPK = D.FundPK and D.Status = 2  
                            where A.status = 2 Order by ExercisePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Exercise.Add(setExercise(dr));
                                }
                            }
                            return L_Exercise;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public int Exercise_Add(Exercise _Exercise, bool _havePrivillege)
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
                                 "Select isnull(max(ExercisePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from Exercise";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _Exercise.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(ExercisePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from Exercise";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _Exercise.Date);
                        cmd.Parameters.AddWithValue("@DistributionDate", _Exercise.DistributionDate);
                        cmd.Parameters.AddWithValue("@FundPK", _Exercise.FundPK);
                        cmd.Parameters.AddWithValue("@Type", _Exercise.Type);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _Exercise.InstrumentPK);
                        cmd.Parameters.AddWithValue("@InstrumentRightsPK", _Exercise.InstrumentRightsPK);
                        cmd.Parameters.AddWithValue("@BalanceRights", _Exercise.BalanceRights);
                        cmd.Parameters.AddWithValue("@BalanceExercise", _Exercise.BalanceExercise);
                        cmd.Parameters.AddWithValue("@Price", _Exercise.Price);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _Exercise.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Exercise");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Exercise_Update(Exercise _Exercise, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_Exercise.ExercisePK, _Exercise.HistoryPK, "Exercise");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Exercise set status=2, Notes=@Notes,Date=@Date,DistributionDate=@DistributionDate,Type=@Type,FundPK=@FundPK,InstrumentRightsPK=@InstrumentRightsPK,InstrumentPK=@InstrumentPK,BalanceRights=@BalanceRights,BalanceExercise=@BalanceExercise,Price=@Price, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where ExercisePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _Exercise.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                            cmd.Parameters.AddWithValue("@Notes", _Exercise.Notes);
                            cmd.Parameters.AddWithValue("@Type", _Exercise.Type);
                            cmd.Parameters.AddWithValue("@Date", _Exercise.Date);
                            cmd.Parameters.AddWithValue("@DistributionDate", _Exercise.DistributionDate);
                            cmd.Parameters.AddWithValue("@FundPK", _Exercise.FundPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _Exercise.InstrumentPK);
                            cmd.Parameters.AddWithValue("@InstrumentRightsPK", _Exercise.InstrumentRightsPK);
                            cmd.Parameters.AddWithValue("@BalanceRights", _Exercise.BalanceRights);
                            cmd.Parameters.AddWithValue("@BalanceExercise", _Exercise.BalanceExercise);
                            cmd.Parameters.AddWithValue("@Price", _Exercise.Price);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _Exercise.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _Exercise.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Exercise set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ExercisePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _Exercise.EntryUsersID);
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
                                cmd.CommandText = "Update Exercise set Notes=@Notes,Date=@Date,DistributionDate=@DistributionDate,Type=@Type,FundPK=@FundPK,InstrumentPK=@InstrumentPK,InstrumentRightsPK=@InstrumentRightsPK,BalanceRights=@BalanceRights,BalanceExercise=@BalanceExercise,Price=@Price, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where ExercisePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _Exercise.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                                cmd.Parameters.AddWithValue("@Notes", _Exercise.Notes);
                                cmd.Parameters.AddWithValue("@Type", _Exercise.Type);
                                cmd.Parameters.AddWithValue("@Date", _Exercise.Date);
                                cmd.Parameters.AddWithValue("@DistributionDate", _Exercise.DistributionDate);
                                cmd.Parameters.AddWithValue("@FundPK", _Exercise.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _Exercise.InstrumentPK);
                                cmd.Parameters.AddWithValue("@InstrumentRightsPK", _Exercise.InstrumentRightsPK);
                                cmd.Parameters.AddWithValue("@BalanceRights", _Exercise.BalanceRights);
                                cmd.Parameters.AddWithValue("@BalanceExercise", _Exercise.BalanceExercise);
                                cmd.Parameters.AddWithValue("@Price", _Exercise.Price);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _Exercise.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            //ini untuk entrier
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_Exercise.ExercisePK, "Exercise");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From Exercise where ExercisePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _Exercise.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _Exercise.Date);
                                cmd.Parameters.AddWithValue("@DistributionDate", _Exercise.DistributionDate);
                                cmd.Parameters.AddWithValue("@Type", _Exercise.Type);
                                cmd.Parameters.AddWithValue("@FundPK", _Exercise.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _Exercise.InstrumentPK);
                                cmd.Parameters.AddWithValue("@InstrumentRightsPK", _Exercise.InstrumentRightsPK);
                                cmd.Parameters.AddWithValue("@BalanceRights", _Exercise.BalanceRights);
                                cmd.Parameters.AddWithValue("@BalanceExercise", _Exercise.BalanceExercise);
                                cmd.Parameters.AddWithValue("@Price", _Exercise.Price);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _Exercise.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Exercise set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where ExercisePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _Exercise.Notes);
                                cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _Exercise.HistoryPK);
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

        public void Exercise_Approved(Exercise _Exercise)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Exercise set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where Exercisepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                        cmd.Parameters.AddWithValue("@historyPK", _Exercise.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _Exercise.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Exercise set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ExercisePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Exercise.ApprovedUsersID);
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

        public void Exercise_Reject(Exercise _Exercise)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Exercise set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where Exercisepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                        cmd.Parameters.AddWithValue("@historyPK", _Exercise.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Exercise.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Exercise set status= 2,LastUpdate=@LastUpdate  where ExercisePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
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

        public void Exercise_Void(Exercise _Exercise)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Exercise set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where Exercisepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Exercise.ExercisePK);
                        cmd.Parameters.AddWithValue("@historyPK", _Exercise.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Exercise.VoidUsersID);
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
    }


}