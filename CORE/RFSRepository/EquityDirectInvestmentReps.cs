using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class EquityDirectInvestmentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[EquityDirectInvestment] " +
                            "([HistoryPK],[Status],[DirectInvestmentPK],[Profit],[TotalAmount],[BookingDate],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@DirectInvestmentPK,@Profit,@TotalAmount,@BookingDate,@LastUsersID,@LastUpdate";

        //2
        private EquityDirectInvestment setEquityDirectInvestment(SqlDataReader dr)
        {
            EquityDirectInvestment M_EquityDirectInvestment = new EquityDirectInvestment();
            M_EquityDirectInvestment.EquityDirectInvestmentPK = Convert.ToInt32(dr["EquityDirectInvestmentPK"]);
            M_EquityDirectInvestment.DirectInvestmentPK = Convert.ToInt32(dr["DirectInvestmentPK"]);
            M_EquityDirectInvestment.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_EquityDirectInvestment.Status = Convert.ToInt32(dr["Status"]);
            M_EquityDirectInvestment.Profit = Convert.ToInt32(dr["Profit"]);
            M_EquityDirectInvestment.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
            M_EquityDirectInvestment.BookingDate = Convert.ToDateTime(dr["BookingDate"]);
            M_EquityDirectInvestment.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_EquityDirectInvestment.LastUpdate = dr["LastUpdate"].ToString();
            M_EquityDirectInvestment.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_EquityDirectInvestment;
        }

        //3
        public List<EquityDirectInvestment> EquityDirectInvestment_Select(int _status, int _directInvestmentPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<EquityDirectInvestment> L_EquityDirectInvestment = new List<EquityDirectInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" select * from EquityDirectInvestment 
                            where DirectInvestmentPK = @DirectInvestmentPK and status = 2
                            order by EquityDirectInvestmentPK Asc ";
                            cmd.Parameters.AddWithValue("@DirectInvestmentPK", _directInvestmentPK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from EquityDirectInvestment where DirectInvestmentPK = @DirectInvestmentPK order by EquityDirectInvestmentPK Asc ";
                            cmd.Parameters.AddWithValue("@DirectInvestmentPK", _directInvestmentPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_EquityDirectInvestment.Add(setEquityDirectInvestment(dr));
                                }
                            }
                            return L_EquityDirectInvestment;
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
        public int EquityDirectInvestment_Add(EquityDirectInvestment _equityDirectInvestment)
        {
            try
            {
                int _equityDirectInvestmentPK = 0;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _equityDirectInvestmentPK = _host.Get_DetailNewEquityDirectInvestmentPK(_equityDirectInvestment.DirectInvestmentPK, "EquityDirectInvestment", "DirectInvestmentPK");
                        cmd.CommandText =
                                  "update DirectInvestment set lastupdate = @Lastupdate where DirectInvestmentPK = @DirectInvestmentPK and status = 1 \n " +
                                  "update DirectInvestment set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where DirectInvestmentPK = @DirectInvestmentPK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,2," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@DirectInvestmentPK", _equityDirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@status", _equityDirectInvestment.Status);
                        cmd.Parameters.AddWithValue("@EquityDirectInvestmentPK", _equityDirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@Profit", _equityDirectInvestment.Profit);
                        cmd.Parameters.AddWithValue("@TotalAmount", _equityDirectInvestment.TotalAmount);
                        cmd.Parameters.AddWithValue("@BookingDate", _equityDirectInvestment.BookingDate);
                        cmd.Parameters.AddWithValue("@LastUsersID", _equityDirectInvestment.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                        return _equityDirectInvestmentPK;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Update
        public void EquityDirectInvestment_Update(EquityDirectInvestment _equityDirectInvestment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "update DirectInvestment set status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate, lastupdate = @Lastupdate where directInvestmentPK = @DirectInvestmentPK and status = 2 \n " +
                         "Update EquityDirectInvestment " +
                        "Set Profit = @Profit,TotalAmount = @TotalAmount ,BookingDate = @BookingDate, LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                        "Where directInvestmentPK = @DirectInvestmentPK and EquityDirectInvestmentPK = @EquityDirectInvestmentPK";

                        cmd.Parameters.AddWithValue("@Profit", _equityDirectInvestment.Profit);
                        cmd.Parameters.AddWithValue("@TotalAmount", _equityDirectInvestment.TotalAmount);
                        cmd.Parameters.AddWithValue("@BookingDate", _equityDirectInvestment.BookingDate);
                        cmd.Parameters.AddWithValue("@DirectInvestmentPK", _equityDirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@EquityDirectInvestmentPK", _equityDirectInvestment.EquityDirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@LastUsersID", _equityDirectInvestment.LastUsersID);
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

        public void EquityDirectInvestment_Delete(EquityDirectInvestment _equityDirectInvestment)
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
                            @"update DirectInvestment set  lastupdate = @Lastupdate where DirectInvestmentPK = @DirectInvestmentPK and status = 1 
                            update DirectInvestment set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where DirectInvestmentPK = @DirectInvestmentPK and status = 2 
                            delete EquityDirectInvestment where DirectInvestmentPK = @DirectInvestmentPK and EquityDirectInvestmentPK = @EquityDirectInvestmentPK ";
                        cmd.Parameters.AddWithValue("@DirectInvestmentPK", _equityDirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@EquityDirectInvestmentPK", _equityDirectInvestment.EquityDirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _equityDirectInvestment.LastUsersID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public bool CheckEquityDirectInvestment(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from EquityDirectInvestment where DirectInvestmentPK = @PK and Status = 1";
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
