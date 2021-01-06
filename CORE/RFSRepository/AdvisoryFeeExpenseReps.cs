using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class AdvisoryFeeExpenseReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AdvisoryFeeExpense] " +
                            "([HistoryPK],[Status],[AdvisoryFeePK],[DirectExpense],[DirectExpValue],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@AdvisoryFeePK,@DirectExpense,@DirectExpValue,@LastUsersID,@LastUpdate";

        //2
        private AdvisoryFeeExpense setAdvisoryFeeExpense(SqlDataReader dr)
        {
            AdvisoryFeeExpense M_AdvisoryFeeExpense = new AdvisoryFeeExpense();
            M_AdvisoryFeeExpense.AdvisoryFeeExpensePK = Convert.ToInt32(dr["AdvisoryFeeExpensePK"]);
            M_AdvisoryFeeExpense.AdvisoryFeePK = Convert.ToInt32(dr["AdvisoryFeePK"]);
            M_AdvisoryFeeExpense.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AdvisoryFeeExpense.Status = Convert.ToInt32(dr["Status"]);
            M_AdvisoryFeeExpense.DirectExpense = Convert.ToString(dr["DirectExpense"]);
            M_AdvisoryFeeExpense.DirectExpValue = Convert.ToDecimal(dr["DirectExpValue"]);
            M_AdvisoryFeeExpense.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_AdvisoryFeeExpense.LastUpdate = dr["LastUpdate"].ToString();
            M_AdvisoryFeeExpense.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_AdvisoryFeeExpense;
        }

        //3
        public List<AdvisoryFeeExpense> AdvisoryFeeExpense_Select(int _status, int _AdvisoryFeePK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AdvisoryFeeExpense> L_AdvisoryFeeExpense = new List<AdvisoryFeeExpense>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" select * from AdvisoryFeeExpense 
                            where AdvisoryFeePK = @AdvisoryFeePK and status = 2
                            order by AdvisoryFeeExpensePK Asc ";
                            cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeePK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from AdvisoryFeeExpense where AdvisoryFeePK = @AdvisoryFeePK order by AdvisoryFeeExpensePK Asc ";
                            cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeePK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AdvisoryFeeExpense.Add(setAdvisoryFeeExpense(dr));
                                }
                            }
                            return L_AdvisoryFeeExpense;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //4
        public int AdvisoryFeeExpense_Add(AdvisoryFeeExpense _AdvisoryFeeExpense)
        {
            try
            {
                int _AdvisoryFeeExpensePK = 0;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _AdvisoryFeeExpensePK = _host.Get_DetailNewAdvisoryFeeExpensePK(_AdvisoryFeeExpense.AdvisoryFeePK, "AdvisoryFeeExpense", "AdvisoryFeePK");
                        cmd.CommandText =
                                  "update AdvisoryFee set lastupdate = @Lastupdate where AdvisoryFeePK = @AdvisoryFeePK and status = 1 \n " +
                                  "update AdvisoryFee set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where AdvisoryFeePK = @AdvisoryFeePK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,2," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeExpense.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@status", _AdvisoryFeeExpense.Status);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeExpensePK", _AdvisoryFeeExpensePK);
                        cmd.Parameters.AddWithValue("@DirectExpense", _AdvisoryFeeExpense.DirectExpense);
                        cmd.Parameters.AddWithValue("@DirectExpValue", _AdvisoryFeeExpense.DirectExpValue);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeExpense.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                        return _AdvisoryFeeExpensePK;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Update
        public void AdvisoryFeeExpense_Update(AdvisoryFeeExpense _AdvisoryFeeExpense)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "update AdvisoryFee set status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate, lastupdate = @Lastupdate where AdvisoryFeePK = @AdvisoryFeePK and status = 2 \n " +
                         "Update AdvisoryFeeExpense " +
                        "Set LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                        "Where AdvisoryFeePK = @AdvisoryFeePK and AdvisoryFeeExpensePK = @AdvisoryFeeExpensePK";

                        cmd.Parameters.AddWithValue("@DirectExpense", _AdvisoryFeeExpense.DirectExpense);
                        cmd.Parameters.AddWithValue("@DirectExpValue", _AdvisoryFeeExpense.DirectExpValue);
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeExpense.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeExpensePK", _AdvisoryFeeExpense.AdvisoryFeeExpensePK);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeExpense.LastUsersID);
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

        public void AdvisoryFeeExpense_Delete(AdvisoryFeeExpense _AdvisoryFeeExpense)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                            @"update AdvisoryFee set  lastupdate = @Lastupdate where AdvisoryFeePK = @AdvisoryFeePK and status = 1 
                            update AdvisoryFee set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where AdvisoryFeePK = @AdvisoryFeePK and status = 2 
                            delete AdvisoryFeeExpense where AdvisoryFeePK = @AdvisoryFeePK and AdvisoryFeeExpensePK = @AdvisoryFeeExpensePK ";
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeExpense.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeExpensePK", _AdvisoryFeeExpense.AdvisoryFeeExpensePK);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeExpense.LastUsersID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool CheckAdvisoryFeeExpense(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from AdvisoryFeeExpense where AdvisoryFeePK = @PK and Status = 1";
                        cmd.Parameters.AddWithValue("@PK", _pk);

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


    }
}
