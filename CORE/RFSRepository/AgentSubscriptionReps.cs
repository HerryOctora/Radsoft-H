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
    public class AgentSubscriptionReps
    {
        Host _host = new Host();


        //2
        private AgentSubscription setAgentSubscription(SqlDataReader dr)
        {
            AgentSubscription M_AgentSubscription = new AgentSubscription();
            M_AgentSubscription.AgentSubscriptionPK = Convert.ToInt32(dr["AgentSubscriptionPK"]);
            M_AgentSubscription.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AgentSubscription.Status = Convert.ToInt32(dr["Status"]);
            M_AgentSubscription.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AgentSubscription.ClientSubscriptionPK = Convert.ToInt32(dr["ClientSubscriptionPK"]);

            M_AgentSubscription.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_AgentSubscription.AgentID = Convert.ToString(dr["AgentID"]);
            M_AgentSubscription.AgentName = Convert.ToString(dr["AgentName"]);
            M_AgentSubscription.AgentTrxPercent = Convert.ToDecimal(dr["AgentTrxPercent"]);
            M_AgentSubscription.AgentTrxAmount = Convert.ToDecimal(dr["AgentTrxAmount"]);
            M_AgentSubscription.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AgentSubscription.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AgentSubscription.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AgentSubscription.EntryTime = dr["EntryTime"].ToString();
            M_AgentSubscription.UpdateTime = dr["UpdateTime"].ToString();
            M_AgentSubscription.VoidTime = dr["VoidTime"].ToString();
            M_AgentSubscription.DBUserID = dr["DBUserID"].ToString();
            M_AgentSubscription.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AgentSubscription.LastUpdate = dr["LastUpdate"].ToString();
            M_AgentSubscription.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AgentSubscription;
        }

        public List<AgentSubscription> Get_DataAgentSubscriptionByClientSubscriptionPK(int _ClientSubscriptionPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<AgentSubscription> L_AgentSubscription = new List<AgentSubscription>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID,B.Name AgentName,* from AgentSubscription A
                        left join Agent B on A.AgentPK = B.AgentPK and B.status in (1,2)
                        where ClientSubscriptionPK = @ClientSubscriptionPK and A.status = 2 ";

                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _ClientSubscriptionPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AgentSubscription.Add(setAgentSubscription(dr));
                                }
                            }
                            return L_AgentSubscription;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Add_AgentSubscription(AgentSubscription _agentSubscription)
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

                        select @MaxPK = Max(AgentSubscriptionPK) from AgentSubscription
                        set @maxPK = isnull(@maxPK,0)

                        Insert into AgentSubscription (AgentSubscriptionPK,HistoryPK,Status,ClientSubscriptionPK,AgentPK,AgentTrxPercent,AgentTrxAmount,
                        EntryUsersID,EntryTime,LastUpdate)

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSubscriptionPK ASC) AgentSubscriptionPK,1,2,@ClientSubscriptionPK,@AgentPK,@AgentTrxPercent,@AgentTrxPercent/100 * @NetAmount,
                        @EntryUsersID,@EntryTime,@LastUpdate

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSubscriptionPK ASC) Result

                        ";


                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _agentSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentSubscription.AgentPK);
                        cmd.Parameters.AddWithValue("@AgentTrxPercent", _agentSubscription.AgentTrxPercent);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentSubscription.NetAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentSubscription.EntryUsersID);
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


        public int Update_AgentSubscription(AgentSubscription _agentSubscription)
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
                        Update AgentSubscription set AgentTrxPercent = @AgentTrxPercent,AgentTrxAmount = @AgentTrxPercent/100 * @NetAmount,
                        UpdateUsersID = @UpdateUsersID,UpdateTime = @UpdateTime,LastUpdate = @LastUpdate
                        where AgentSubscriptionPK = @AgentSubscriptionPK and status = 2



                        ";
                        cmd.Parameters.AddWithValue("@AgentSubscriptionPK", _agentSubscription.AgentSubscriptionPK);
                        cmd.Parameters.AddWithValue("@AgentTrxPercent", _agentSubscription.AgentTrxPercent);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentSubscription.NetAmount);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _agentSubscription.UpdateUsersID);
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

        public void Void_AgentSubscription(AgentSubscription _agentSubscription)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update AgentSubscription set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate 
                        where AgentSubscriptionPK = @AgentSubscriptionPK and status = 2
               
                        
                        ";
                        cmd.Parameters.AddWithValue("@AgentSubscriptionPK", _agentSubscription.AgentSubscriptionPK);

                        cmd.Parameters.AddWithValue("@VoidUsersID", _agentSubscription.VoidUsersID);
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


        public bool Validate_MaxPercentAgentSubscription(AgentSubscription _agentSubscription)
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
                        select isnull(AgentTrxpercent,0) from AgentSubscription where AgentSubscriptionPK = @AgentSubscriptionPK and status = 2
                        )
                        BEGIN
	                        select @OldPercent = isnull(AgentTrxpercent,0) from AgentSubscription where AgentSubscriptionPK = @AgentSubscriptionPK and status = 2 and AgentPK <> 1
                        END
                        ELSE
                        BEGIN
	                        select @OldPercent = 0
                        END



                        SELECT @CurrentFeePercent =  SUM(ISNULL(AgentTrxpercent,0)) - @OldPercent FROM dbo.AgentSubscription WHERE ClientSubscriptionPK = @ClientSubscriptionPK and status = 2 and AgentPK <> 1

                        IF (ISNULL(@CurrentFeePercent,0) + @NewFeePercent) > 100
                        BEGIN
	                        SELECT 1 Result
                        END
                        ELSE
                        BEGIN
	                        SELECT 0 Result
                        END ";

                        cmd.Parameters.AddWithValue("@AgentSubscriptionPK", _agentSubscription.AgentSubscriptionPK);
                        cmd.Parameters.AddWithValue("@NewFeePercent", _agentSubscription.AgentTrxPercent);
                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _agentSubscription.ClientSubscriptionPK);
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

        public void Insert_PercentAgentSubscription(AgentSubscription _agentSubscription)
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

                        Declare @AgentHO numeric(18,8)
                        Declare @AgentPercent numeric(18,8)
                        Declare @MaxPK int

                        

                        select @MaxPK = Max(AgentSubscriptionPK) from AgentSubscription
                        set @maxPK = isnull(@maxPK,0)

                        IF NOT EXISTS (select * from AgentSubscription where ClientSubscriptionPK  = @ClientSubscriptionPK and status = 2 and AgentPK = 1)
                        BEGIN
                            select @AgentPercent = isnull(sum(AgentTrxPercent),0) from AgentSubscription where ClientSubscriptionPK  = @ClientSubscriptionPK and status = 2
	                        select @AgentHO = 100 - @AgentPercent	
                            Insert into AgentSubscription (AgentSubscriptionPK,HistoryPK,Status,ClientSubscriptionPK,AgentPK,AgentTrxPercent,AgentTrxAmount,
                            EntryUsersID,EntryTime,LastUpdate)

                            select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSubscriptionPK ASC) AgentSubscriptionPK,1,2,@ClientSubscriptionPK,1,@AgentHO,@AgentHO/100 * @NetAmount,
                            @EntryUsersID,@EntryTime,@LastUpdate

                        END
                        ELSE
                        BEGIN 
                            select @AgentPercent = isnull(sum(AgentTrxPercent),0) from AgentSubscription where ClientSubscriptionPK  = @ClientSubscriptionPK and status = 2 and AgentPK <> 1
	                        select @AgentHO = 100 - @AgentPercent	

	                        Update AgentSubscription set AgentTrxPercent = @AgentHO,AgentTrxAmount = @AgentHO/100 * @NetAmount,
	                        UpdateUsersID = @UpdateUsersID,UpdateTime = @UpdateTime,LastUpdate = @LastUpdate
	                        where ClientSubscriptionPK = @ClientSubscriptionPK and status = 2 and AgentPK = 1

                        END
                        ";

                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _agentSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentSubscription.NetAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentSubscription.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _agentSubscription.UpdateUsersID);
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