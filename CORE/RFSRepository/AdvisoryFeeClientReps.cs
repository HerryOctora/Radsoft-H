using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class AdvisoryFeeClientReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AdvisoryFeeClient] " +
                            "([HistoryPK],[Status],[AdvisoryFeePK],[ClientName],[ClientDescription],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@AdvisoryFeePK,@ClientName,@ClientDescription,@LastUsersID,@LastUpdate";

        //2
        private AdvisoryFeeClient setAdvisoryFeeClient(SqlDataReader dr)
        {
            AdvisoryFeeClient M_AdvisoryFeeClient = new AdvisoryFeeClient();
            M_AdvisoryFeeClient.AdvisoryFeeClientPK = Convert.ToInt32(dr["AdvisoryFeeClientPK"]);
            M_AdvisoryFeeClient.AdvisoryFeePK = Convert.ToInt32(dr["AdvisoryFeePK"]);
            M_AdvisoryFeeClient.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AdvisoryFeeClient.Status = Convert.ToInt32(dr["Status"]);
            M_AdvisoryFeeClient.ClientName = Convert.ToString(dr["ClientName"]);
            M_AdvisoryFeeClient.ClientDescription = Convert.ToString(dr["ClientDescription"]);
            M_AdvisoryFeeClient.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_AdvisoryFeeClient.LastUpdate = dr["LastUpdate"].ToString();
            M_AdvisoryFeeClient.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_AdvisoryFeeClient;
        }

        //3
        public List<AdvisoryFeeClient> AdvisoryFeeClient_Select(int _status, int _AdvisoryFeePK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AdvisoryFeeClient> L_AdvisoryFeeClient = new List<AdvisoryFeeClient>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @" select * from AdvisoryFeeClient 
                            where AdvisoryFeePK = @AdvisoryFeePK and status = 2
                            order by AdvisoryFeeClientPK Asc ";
                            cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeePK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from AdvisoryFeeClient where AdvisoryFeePK = @AdvisoryFeePK order by AdvisoryFeeClientPK Asc ";
                            cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeePK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AdvisoryFeeClient.Add(setAdvisoryFeeClient(dr));
                                }
                            }
                            return L_AdvisoryFeeClient;
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
        public int AdvisoryFeeClient_Add(AdvisoryFeeClient _AdvisoryFeeClient) 
        {
            try
            {
                int _AdvisoryFeeClientPK = 0;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _AdvisoryFeeClientPK = _host.Get_DetailNewAdvisoryFeeClientPK(_AdvisoryFeeClient.AdvisoryFeePK, "AdvisoryFeeClient", "AdvisoryFeePK");
                        cmd.CommandText =
                                  "update AdvisoryFee set lastupdate = @Lastupdate where AdvisoryFeePK = @AdvisoryFeePK and status = 1 \n " +
                                  "update AdvisoryFee set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where AdvisoryFeePK = @AdvisoryFeePK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,2," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeClient.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@status", _AdvisoryFeeClient.Status);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeClientPK", _AdvisoryFeeClientPK);
                        cmd.Parameters.AddWithValue("@ClientName", _AdvisoryFeeClient.ClientName);
                        cmd.Parameters.AddWithValue("@ClientDescription", _AdvisoryFeeClient.ClientDescription);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeClient.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                        return _AdvisoryFeeClientPK;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Update
        public void AdvisoryFeeClient_Update(AdvisoryFeeClient _AdvisoryFeeClient)
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
                         "Update AdvisoryFeeClient " +
                        "Set ClientName = @ClientName , ClientDescription = @ClientDescription,  LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                        "Where AdvisoryFeePK = @AdvisoryFeePK and AdvisoryFeeClientPK = @AdvisoryFeeClientPK";

                        cmd.Parameters.AddWithValue("@ClientName", _AdvisoryFeeClient.ClientName);
                        cmd.Parameters.AddWithValue("@ClientDescription", _AdvisoryFeeClient.ClientDescription);
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeClient.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeClientPK", _AdvisoryFeeClient.AdvisoryFeeClientPK);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeClient.LastUsersID);
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

        public void AdvisoryFeeClient_Delete(AdvisoryFeeClient _AdvisoryFeeClient)
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
                            delete AdvisoryFeeClient where AdvisoryFeePK = @AdvisoryFeePK and AdvisoryFeeClientPK = @AdvisoryFeeClientPK ";
                        cmd.Parameters.AddWithValue("@AdvisoryFeePK", _AdvisoryFeeClient.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@AdvisoryFeeClientPK", _AdvisoryFeeClient.AdvisoryFeeClientPK);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _AdvisoryFeeClient.LastUsersID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool CheckAdvisoryFeeClient(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from AdvisoryFeeClient where AdvisoryFeePK = @PK and Status = 1";
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
