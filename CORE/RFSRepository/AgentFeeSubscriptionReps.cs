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
    public class AgentFeeSubscriptionReps
    {
        Host _host = new Host();


        //2
        private AgentFeeSubscription setAgentFeeSubscription(SqlDataReader dr)
        {
            AgentFeeSubscription M_AgentFeeSubscription = new AgentFeeSubscription();
            M_AgentFeeSubscription.AgentFeeSubscriptionPK = Convert.ToInt32(dr["AgentFeeSubscriptionPK"]);
            M_AgentFeeSubscription.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AgentFeeSubscription.Status = Convert.ToInt32(dr["Status"]);
            M_AgentFeeSubscription.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AgentFeeSubscription.ClientSubscriptionPK = Convert.ToInt32(dr["ClientSubscriptionPK"]);

            M_AgentFeeSubscription.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_AgentFeeSubscription.AgentID = Convert.ToString(dr["AgentID"]);
            M_AgentFeeSubscription.AgentName = Convert.ToString(dr["AgentName"]);
            M_AgentFeeSubscription.AgentFeeTrxPercent = Convert.ToDecimal(dr["AgentFeePercent"]);
            M_AgentFeeSubscription.AgentFeeTrxAmount = Convert.ToDecimal(dr["AgentFeeAmount"]);
            M_AgentFeeSubscription.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AgentFeeSubscription.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AgentFeeSubscription.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AgentFeeSubscription.EntryTime = dr["EntryTime"].ToString();
            M_AgentFeeSubscription.UpdateTime = dr["UpdateTime"].ToString();
            M_AgentFeeSubscription.VoidTime = dr["VoidTime"].ToString();
            M_AgentFeeSubscription.DBUserID = dr["DBUserID"].ToString();
            M_AgentFeeSubscription.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AgentFeeSubscription.LastUpdate = dr["LastUpdate"].ToString();
            M_AgentFeeSubscription.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AgentFeeSubscription;
        }

        public List<AgentFeeSubscription> Get_DataAgentFeeSubscriptionByClientSubscriptionPK(int _ClientSubscriptionPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<AgentFeeSubscription> L_AgentFeeSubscription = new List<AgentFeeSubscription>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID,B.Name AgentName,* from AgentFeeSubscription A
                        left join Agent B on A.AgentPK = B.AgentPK and B.status in (1,2)
                        where ClientSubscriptionPK = @ClientSubscriptionPK and A.status = 2 ";

                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _ClientSubscriptionPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AgentFeeSubscription.Add(setAgentFeeSubscription(dr));
                                }
                            }
                            return L_AgentFeeSubscription;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Add_AgentFeeSubscription(AgentFeeSubscription _agentFeeSubscription)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                        Declare @MaxPK int

                        select @MaxPK = Max(AgentFeeSubscriptionPK) from AgentFeeSubscription
                        set @maxPK = isnull(@maxPK,0)

                        Insert into AgentFeeSubscription (AgentFeeSubscriptionPK,HistoryPK,Status,ClientSubscriptionPK,AgentPK,AgentFeePercent,AgentFeeAmount,
                        EntryUsersID,EntryTime,LastUpdate)

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSubscriptionPK ASC) AgentFeeSubscriptionPK,1,2,@ClientSubscriptionPK,@AgentPK,@AgentFeeTrxPercent,@AgentFeeTrxPercent/100 * @NetAmount,
                        @EntryUsersID,@EntryTime,@LastUpdate

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSubscriptionPK ASC) Result

                        ";


                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _agentFeeSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentFeeSubscription.AgentPK);
                        cmd.Parameters.AddWithValue("@AgentFeeTrxPercent", _agentFeeSubscription.AgentFeeTrxPercent);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentFeeSubscription.NetAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentFeeSubscription.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

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


        public int Update_AgentFeeSubscription(AgentFeeSubscription _agentFeeSubscription)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Update AgentFeeSubscription set AgentFeePercent = @AgentFeeTrxPercent,AgentFeeAmount = @AgentFeeTrxPercent/100 * @NetAmount,
                        UpdateUsersID = @UpdateUsersID,UpdateTime = @UpdateTime,LastUpdate = @LastUpdate
                        where AgentFeeSubscriptionPK = @AgentFeeSubscriptionPK and status = 2



                        ";
                        cmd.Parameters.AddWithValue("@AgentFeeSubscriptionPK", _agentFeeSubscription.AgentFeeSubscriptionPK);
                        cmd.Parameters.AddWithValue("@AgentFeeTrxPercent", _agentFeeSubscription.AgentFeeTrxPercent);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentFeeSubscription.NetAmount);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _agentFeeSubscription.UpdateUsersID);
                        cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);


                        cmd.ExecuteNonQuery();
                    }
                    return 0;


                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Void_AgentFeeSubscription(AgentFeeSubscription _agentFeeSubscription)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update AgentFeeSubscription set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate 
                        where AgentFeeSubscriptionPK = @AgentFeeSubscriptionPK and status = 2
               
                        ";
                        cmd.Parameters.AddWithValue("@AgentFeeSubscriptionPK", _agentFeeSubscription.AgentFeeSubscriptionPK);

                        cmd.Parameters.AddWithValue("@VoidUsersID", _agentFeeSubscription.VoidUsersID);
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


        public bool Validate_MaxPercentAgentFeeSubscription(AgentFeeSubscription _agentFeeSubscription)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                            @"



                        DECLARE @CurrentFeePercent NUMERIC(8,4)
                        Declare @OldPercent numeric(18,4)
                        IF EXISTS(
                        select isnull(AgentFeePercent,0) from AgentFeeSubscription where AgentFeeSubscriptionPK = @AgentFeeSubscriptionPK and status = 2
                        )
                        BEGIN
	                        select @OldPercent = isnull(AgentFeePercent,0) from AgentFeeSubscription where AgentFeeSubscriptionPK = @AgentFeeSubscriptionPK and status = 2 and AgentPK <> 1
                        END
                        ELSE
                        BEGIN
	                        select @OldPercent = 0
                        END



                        SELECT @CurrentFeePercent =  SUM(ISNULL(AgentFeePercent,0)) - @OldPercent FROM dbo.AgentFeeSubscription WHERE ClientSubscriptionPK = @ClientSubscriptionPK and status = 2  and AgentPK <> 1

                        IF (ISNULL(@CurrentFeePercent,0) + @NewFeePercent) > 100
                        BEGIN
	                        SELECT 1 Result
                        END
                        ELSE
                        BEGIN
	                        SELECT 0 Result
                        END ";

                        cmd.Parameters.AddWithValue("@AgentFeeSubscriptionPK", _agentFeeSubscription.AgentFeeSubscriptionPK);
                        cmd.Parameters.AddWithValue("@NewFeePercent", _agentFeeSubscription.AgentFeeTrxPercent);
                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _agentFeeSubscription.ClientSubscriptionPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Insert_PercentAgentFeeSubscription(AgentFeeSubscription _agentFeeSubscription)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                            @"

                        Declare @AgentFeeHO numeric(18,8)
                        Declare @AgentFeePercent numeric(18,8)
                        Declare @MaxPK int

                        

                        select @MaxPK = Max(AgentFeeSubscriptionPK) from AgentFeeSubscription
                        set @maxPK = isnull(@maxPK,0)

                        IF NOT EXISTS (select * from AgentFeeSubscription where ClientSubscriptionPK  = @ClientSubscriptionPK and status = 2 and AgentPK = 1)
                        BEGIN
                            select @AgentFeePercent = isnull(sum(AgentFeePercent),0) from AgentFeeSubscription where ClientSubscriptionPK  = @ClientSubscriptionPK and status = 2
	                        select @AgentFeeHO = 100 - @AgentFeePercent	
                            Insert into AgentFeeSubscription (AgentFeeSubscriptionPK,HistoryPK,Status,ClientSubscriptionPK,AgentPK,AgentFeePercent,AgentFeeAmount,
                            EntryUsersID,EntryTime,LastUpdate)

                            select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSubscriptionPK ASC) AgentFeeSubscriptionPK,1,2,@ClientSubscriptionPK,1,@AgentFeeHO,@AgentFeeHO/100 * @NetAmount,
                            @EntryUsersID,@EntryTime,@LastUpdate

                        END
                        ELSE
                        BEGIN 
                            select @AgentFeePercent = isnull(sum(AgentFeePercent),0) from AgentFeeSubscription where ClientSubscriptionPK  = @ClientSubscriptionPK and status = 2 and AgentPK <> 1
	                        select @AgentFeeHO = 100 - @AgentFeePercent	

	                        Update AgentFeeSubscription set AgentFeePercent = @AgentFeeHO,AgentFeeAmount = @AgentFeeHO/100 * @NetAmount,
	                        UpdateUsersID = @UpdateUsersID,UpdateTime = @UpdateTime,LastUpdate = @LastUpdate
	                        where ClientSubscriptionPK = @ClientSubscriptionPK and status = 2 and AgentPK = 1

                        END
                        ";

                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _agentFeeSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentFeeSubscription.NetAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentFeeSubscription.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _agentFeeSubscription.UpdateUsersID);
                        cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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