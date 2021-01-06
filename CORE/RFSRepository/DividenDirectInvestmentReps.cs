using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class DividenDirectInvestmentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[DividenDirectInvestment] " +
                            "([HistoryPK],[Status],[DirectInvestmentPK],[DividenRatio],[Profit],[Dividen],[DateOfDeclaration],[DateOfBooking],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@DirectInvestmentPK,@DividenRatio,@Profit,@Dividen,@DateOfDeclaration,@DateOfBooking,@LastUsersID,@LastUpdate";

        //2
        private DividenDirectInvestment setDividenDirectInvestment(SqlDataReader dr)
        {
            DividenDirectInvestment M_DividenDirectInvestment = new DividenDirectInvestment();
            M_DividenDirectInvestment.DividenDirectInvestmentPK = Convert.ToInt32(dr["DividenDirectInvestmentPK"]);
            M_DividenDirectInvestment.DirectInvestmentPK = Convert.ToInt32(dr["DirectInvestmentPK"]);
            M_DividenDirectInvestment.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_DividenDirectInvestment.Status = Convert.ToInt32(dr["Status"]);
            M_DividenDirectInvestment.DividenRatio = Convert.ToDecimal(dr["DividenRatio"]);
            M_DividenDirectInvestment.Profit = Convert.ToDecimal(dr["Profit"]);
            M_DividenDirectInvestment.Dividen = Convert.ToDecimal(dr["Dividen"]);
            M_DividenDirectInvestment.DateOfDeclaration = Convert.ToDateTime(dr["DateOfDeclaration"]);
            M_DividenDirectInvestment.DateOfBooking = Convert.ToDateTime(dr["DateOfBooking"]);

            M_DividenDirectInvestment.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_DividenDirectInvestment.LastUpdate = dr["LastUpdate"].ToString();
            M_DividenDirectInvestment.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_DividenDirectInvestment;
        }

        //3
        public List<DividenDirectInvestment> DividenDirectInvestment_Select(int _status, int _DirectInvestmentPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DividenDirectInvestment> L_DividenDirectInvestment = new List<DividenDirectInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" select * from DividenDirectInvestment 
                            where DirectInvestmentPK = @DirectInvestmentPK and status = 2
                            order by DividenDirectInvestmentPK Asc ";
                            cmd.Parameters.AddWithValue("@DirectInvestmentPK", _DirectInvestmentPK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from DividenDirectInvestment where DirectInvestmentPK = @DirectInvestmentPK order by DividenDirectInvestmentPK Asc ";
                            cmd.Parameters.AddWithValue("@DirectInvestmentPK", _DirectInvestmentPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_DividenDirectInvestment.Add(setDividenDirectInvestment(dr));
                                }
                            }
                            return L_DividenDirectInvestment;
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
        public int DividenDirectInvestment_Add(DividenDirectInvestment _DividenDirectInvestment)
        {
            try
            {
                int _DividenDirectInvestmentPK = 0;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _DividenDirectInvestmentPK = _host.Get_DetailNewDividenDirectInvestmentPK(_DividenDirectInvestment.DirectInvestmentPK, "DividenDirectInvestment", "DirectInvestmentPK");
                        cmd.CommandText =
                                  "update DirectInvestment set lastupdate = @Lastupdate where DirectInvestmentPK = @DirectInvestmentPK and status = 1 \n " +
                                  "update DirectInvestment set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where DirectInvestmentPK = @DirectInvestmentPK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,2," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@DirectInvestmentPK", _DividenDirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@status", _DividenDirectInvestment.Status);
                        cmd.Parameters.AddWithValue("@DividenDirectInvestmentPK", _DividenDirectInvestmentPK);

                        cmd.Parameters.AddWithValue("@DividenRatio", _DividenDirectInvestment.DividenRatio);
                        cmd.Parameters.AddWithValue("@Profit", _DividenDirectInvestment.Profit);
                        cmd.Parameters.AddWithValue("@Dividen", _DividenDirectInvestment.Dividen);
                        cmd.Parameters.AddWithValue("@DateOfDeclaration", _DividenDirectInvestment.DateOfDeclaration);
                        cmd.Parameters.AddWithValue("@DateOfBooking", _DividenDirectInvestment.DateOfBooking);

                        cmd.Parameters.AddWithValue("@LastUsersID", _DividenDirectInvestment.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                        return _DividenDirectInvestmentPK;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Update
        public void DividenDirectInvestment_Update(DividenDirectInvestment _DividenDirectInvestment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "update DirectInvestment set status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate, lastupdate = @Lastupdate where DirectInvestmentPK = @DirectInvestmentPK and status = 2 \n " +
                         "Update DividenDirectInvestment " +
                        "Set DividenRatio = @DividenRatio , Profit = @Profit,Dividen = @Dividen,DateOfDeclaration = @DateOfDeclaration,DateOfBooking = @DateOfBooking,  LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                        "Where DirectInvestmentPK = @DirectInvestmentPK and DividenDirectInvestmentPK = @DividenDirectInvestmentPK";

                        cmd.Parameters.AddWithValue("@DividenRatio", _DividenDirectInvestment.DividenRatio);
                        cmd.Parameters.AddWithValue("@Profit", _DividenDirectInvestment.Profit);
                        cmd.Parameters.AddWithValue("@Dividen", _DividenDirectInvestment.Dividen);
                        cmd.Parameters.AddWithValue("@DateOfDeclaration", _DividenDirectInvestment.DateOfDeclaration);
                        cmd.Parameters.AddWithValue("@DateOfBooking", _DividenDirectInvestment.DateOfBooking);
                        cmd.Parameters.AddWithValue("@DirectInvestmentPK", _DividenDirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@DividenDirectInvestmentPK", _DividenDirectInvestment.DividenDirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@LastUsersID", _DividenDirectInvestment.LastUsersID);
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

        public void DividenDirectInvestment_Delete(DividenDirectInvestment _DividenDirectInvestment)
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
                            delete DividenDirectInvestment where DirectInvestmentPK = @DirectInvestmentPK and DividenDirectInvestmentPK = @DividenDirectInvestmentPK ";
                        cmd.Parameters.AddWithValue("@DirectInvestmentPK", _DividenDirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@DividenDirectInvestmentPK", _DividenDirectInvestment.DividenDirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _DividenDirectInvestment.LastUsersID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public bool CheckDividenDirectInvestment(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from DividenDirectInvestment where DirectInvestmentPK = @PK and Status = 1";
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
