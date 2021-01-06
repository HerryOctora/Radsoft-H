using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class PrepaidAllocationReps
    {
        Host _host = new Host();
        string _insertCommand = "INSERT INTO [dbo].[PrepaidAllocation] " +
                            "([PrepaidPK],[HistoryPK],[AutoNo],[Status],[DepartmentPK],[AllocationPercent],[LastUsersID],[LastUpdate]) ";
        string _paramaterCommand = "@DepartmentPK,@AllocationPercent,@LastUsersID,@LastUpdate ";

        private PrepaidAllocation setPrepaid(SqlDataReader dr)
        {
            PrepaidAllocation M_Prepaid = new PrepaidAllocation();
            M_Prepaid.PrepaidPK = Convert.ToInt32(dr["PrepaidPK"]);
            M_Prepaid.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Prepaid.Status = Convert.ToInt32(dr["Status"]);
            M_Prepaid.AutoNo = Convert.ToInt32(dr["AutoNo"]);
            M_Prepaid.Notes = Convert.ToString(dr["Notes"]);
            M_Prepaid.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_Prepaid.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_Prepaid.AllocationPercent = Convert.ToDecimal(dr["AllocationPercent"]);
            M_Prepaid.LastUsersID = dr["LastUsersID"].ToString();
            M_Prepaid.LastUpdate = dr["LastUpdate"].ToString();
            M_Prepaid.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Prepaid;
        }

        public List<PrepaidAllocation> PrepaidAllocation_Select(int _status, int _prepaidPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<PrepaidAllocation> L_PrepaidAllocation = new List<PrepaidAllocation>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select B.ID DepartmentID,* from PrepaidAllocation A Left join Department B " +
                             "On A.DepartmentPK = B.DepartmentPK and B.status = 2 Where PrepaidPK = @PrepaidPK and A.Status = @Status " +
                             "order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@PrepaidPK", _prepaidPK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from PrepaidAllocation Where PrepaidPK = @PrepaidPK order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@PrepaidPK", _prepaidPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_PrepaidAllocation.Add(setPrepaid(dr));
                                }
                            }
                            return L_PrepaidAllocation;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int PrepaidAllocation_Add(PrepaidAllocation _prepaidAllocation)
        {
            try
            {
                int _autoNo = 0;
                DateTime _now = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _autoNo = _host.Get_DetailNewAutoNo(_prepaidAllocation.PrepaidPK, "PrepaidAllocation", "PrepaidPK");
                        cmd.CommandText = " If Not Exists(select * from PrepaidAllocation where PrepaidPk = @PrepaidPK and DepartmentPk = @DepartmentPK ) BEGIN " +
                                 _insertCommand +
                                 " Select @PrepaidPK,1,isnull(Max(AutoNo),0) + 1,@status," + _paramaterCommand + " From PrepaidAllocation " +
                                 " Where PrepaidPK = @PrepaidPK END ELSE BEGIN Update PrepaidAllocation set AllocationPercent = @AllocationPercent Where PrepaidPK = @PrepaidPK and DepartmentPK = @DepartmentPK END";
                        cmd.Parameters.AddWithValue("@PrepaidPK", _prepaidAllocation.PrepaidPK);
                        cmd.Parameters.AddWithValue("@AllocationPercent", _prepaidAllocation.AllocationPercent);
                        cmd.Parameters.AddWithValue("@status", _prepaidAllocation.Status);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _prepaidAllocation.DepartmentPK);
                        cmd.Parameters.AddWithValue("@LastUsersID", _prepaidAllocation.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _now);
                        cmd.ExecuteNonQuery();
                        return _autoNo;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }



        public int PrepaidAllocation_Update(PrepaidAllocation _prepaidAllocation, bool _havePrivillege)
        {
            int _newHisPK;
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_prepaidAllocation.PrepaidPK, _prepaidAllocation.HistoryPK, "PrepaidAllocation");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update PrepaidAllocation " +
                            "Set DepartmentPK = @DepartmentPK,AllocationPerccent = @AllocationPercent,LastUsersID = @LastUsersID,LastUpdate @LastUpdate " +
                            "Where PrepaidPK = @PrepaidPK and AutoNo = @AutoNo ";

                            cmd.Parameters.AddWithValue("@DepartmentPK", _prepaidAllocation.DepartmentPK);
                            cmd.Parameters.AddWithValue("@AllocationPercent", _prepaidAllocation.AllocationPercent);
                            cmd.Parameters.AddWithValue("@PrepaidPK", _prepaidAllocation.PrepaidPK);
                            cmd.Parameters.AddWithValue("@AutoNo", _prepaidAllocation.AutoNo);
                            cmd.Parameters.AddWithValue("@LastUsersID", _prepaidAllocation.LastUsersID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Prepaid set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate where PrepaidPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _prepaidAllocation.PrepaidPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _prepaidAllocation.EntryUsersID);
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
                                cmd.CommandText = "Update PrepaidAllocation " +
                                "Set DepartmentPK = @DepartmentPK,AllocationPerccent = @AllocationPercent,LastUsersID = @LastUsersID,LastUpdate @LastUpdate " +
                                "Where PrepaidPK = @PrepaidPK and AutoNo = @AutoNo ";

                                cmd.Parameters.AddWithValue("@DepartmentPK", _prepaidAllocation.DepartmentPK);
                                cmd.Parameters.AddWithValue("@AllocationPercent", _prepaidAllocation.AllocationPercent);
                                cmd.Parameters.AddWithValue("@PrepaidPK", _prepaidAllocation.PrepaidPK);
                                cmd.Parameters.AddWithValue("@AutoNo", _prepaidAllocation.AutoNo);
                                cmd.Parameters.AddWithValue("@LastUsersID", _prepaidAllocation.LastUsersID);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_prepaidAllocation.PrepaidPK, "Prepaid");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                   "Select @PK,@HistoryPK,1," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate";
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _host.Get_NewHistoryPK(_prepaidAllocation.PrepaidPK, "Prepaid"));
                                cmd.Parameters.AddWithValue("@DepartmentPK", _prepaidAllocation.DepartmentPK);
                                cmd.Parameters.AddWithValue("@AllocationPercent", _prepaidAllocation.AllocationPercent);
                                cmd.Parameters.AddWithValue("@PK", _prepaidAllocation.PrepaidPK);
                                cmd.Parameters.AddWithValue("@AutoNo", _prepaidAllocation.AutoNo);
                                cmd.Parameters.AddWithValue("@LastUsersID", _prepaidAllocation.LastUsersID);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();

                            }
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Prepaid set status= 4,Notes=@Notes," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate where PrepaidPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _prepaidAllocation.Notes);
                                cmd.Parameters.AddWithValue("@PK", _prepaidAllocation.PrepaidPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _prepaidAllocation.HistoryPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _prepaidAllocation.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
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

        //public int PrepaidAllocation_Update(PrepaidAllocation _prepaidAllocation, bool _havePrivillege)
        //{
            
        //    try
        //    {
        //        int _newHisPK;
        //        DateTime _now = DateTime.Now;
        //        int status = _host.Get_Status(_prepaidAllocation.PrepaidPK, _prepaidAllocation.HistoryPK, "PrepaidAllocation");
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {

        //                cmd.CommandText = "Update PrepaidAllocation " +
        //                    "Set DepartmentPK = @DepartmentPK,AllocationPerccent = @AllocationPercent,LastUsersID = @LastUsersID,LastUpdate @LastUpdate " +
        //                    "Where PrepaidPK = @PrepaidPK and AutoNo = @AutoNo ";

        //                cmd.Parameters.AddWithValue("@DepartmentPK", _prepaidAllocation.DepartmentPK);
        //                cmd.Parameters.AddWithValue("@AllocationPercent", _prepaidAllocation.AllocationPercent);
        //                cmd.Parameters.AddWithValue("@PrepaidPK", _prepaidAllocation.PrepaidPK);
        //                cmd.Parameters.AddWithValue("@AutoNo", _prepaidAllocation.AutoNo);
        //                cmd.Parameters.AddWithValue("@LastUsersID", _prepaidAllocation.LastUsersID);
        //                cmd.Parameters.AddWithValue("@LastUpdate", _now);

        //                cmd.ExecuteNonQuery();
        //            }
        //            return 0;
        //        }
                


        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}

        public void PrepaidAllocation_Delete(PrepaidAllocation _prepaidAllocation)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "delete PrepaidAllocation where PrepaidPK = @PrepaidPK and AutoNo = @AutoNo ";
                        cmd.Parameters.AddWithValue("@PrepaidPK", _prepaidAllocation.PrepaidPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _prepaidAllocation.AutoNo);
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