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
    public class AgentFeeSwitchingReps
    {
        Host _host = new Host();


        //2
        private AgentFeeSwitching setAgentFeeSwitching(SqlDataReader dr)
        {
            AgentFeeSwitching M_AgentFeeSwitching = new AgentFeeSwitching();
            M_AgentFeeSwitching.AgentFeeSwitchingPK = Convert.ToInt32(dr["AgentFeeSwitchingPK"]);
            M_AgentFeeSwitching.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AgentFeeSwitching.Status = Convert.ToInt32(dr["Status"]);
            M_AgentFeeSwitching.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AgentFeeSwitching.ClientSwitchingPK = Convert.ToInt32(dr["ClientSwitchingPK"]);

            M_AgentFeeSwitching.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_AgentFeeSwitching.AgentID = Convert.ToString(dr["AgentID"]);
            M_AgentFeeSwitching.AgentName = Convert.ToString(dr["AgentName"]);
            M_AgentFeeSwitching.AgentFeeTrxPercent = Convert.ToDecimal(dr["AgentFeePercent"]);
            M_AgentFeeSwitching.AgentFeeTrxAmount = Convert.ToDecimal(dr["AgentFeeAmount"]);
            M_AgentFeeSwitching.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AgentFeeSwitching.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AgentFeeSwitching.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AgentFeeSwitching.EntryTime = dr["EntryTime"].ToString();
            M_AgentFeeSwitching.UpdateTime = dr["UpdateTime"].ToString();
            M_AgentFeeSwitching.VoidTime = dr["VoidTime"].ToString();
            M_AgentFeeSwitching.DBUserID = dr["DBUserID"].ToString();
            M_AgentFeeSwitching.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AgentFeeSwitching.LastUpdate = dr["LastUpdate"].ToString();
            M_AgentFeeSwitching.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AgentFeeSwitching;
        }

        public List<AgentFeeSwitching> Get_DataAgentFeeSwitchingByClientSwitchingPK(int _ClientSwitchingPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<AgentFeeSwitching> L_AgentFeeSwitching = new List<AgentFeeSwitching>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID,B.Name AgentName,* from AgentFeeSwitching A
                        left join Agent B on A.AgentPK = B.AgentPK and B.status in (1,2)
                        where ClientSwitchingPK = @ClientSwitchingPK and A.status = 2 ";

                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _ClientSwitchingPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AgentFeeSwitching.Add(setAgentFeeSwitching(dr));
                                }
                            }
                            return L_AgentFeeSwitching;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Add_AgentFeeSwitching(AgentFeeSwitching _agentFeeSwitching)
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

                        select @MaxPK = Max(AgentFeeSwitchingPK) from AgentFeeSwitching
                        set @maxPK = isnull(@maxPK,0)

                        Insert into AgentFeeSwitching (AgentFeeSwitchingPK,HistoryPK,Status,ClientSwitchingPK,AgentPK,AgentFeePercent,AgentFeeAmount,
                        EntryUsersID,EntryTime,LastUpdate)

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSwitchingPK ASC) AgentFeeSwitchingPK,1,2,@ClientSwitchingPK,@AgentPK,@AgentFeeTrxPercent,@AgentFeeTrxPercent/100 * @NetAmount,
                        @EntryUsersID,@EntryTime,@LastUpdate

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSwitchingPK ASC) Result

                        ";


                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _agentFeeSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentFeeSwitching.AgentPK);
                        cmd.Parameters.AddWithValue("@AgentFeeTrxPercent", _agentFeeSwitching.AgentFeeTrxPercent);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentFeeSwitching.NetAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentFeeSwitching.EntryUsersID);
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


        public int Update_AgentFeeSwitching(AgentFeeSwitching _agentFeeSwitching)
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
                        Update AgentFeeSwitching set AgentFeePercent = @AgentFeeTrxPercent,AgentFeeAmount = @AgentFeeTrxPercent/100 * @NetAmount,
                        UpdateUsersID = @UpdateUsersID,UpdateTime = @UpdateTime,LastUpdate = @LastUpdate
                        where AgentFeeSwitchingPK = @AgentFeeSwitchingPK and status = 2



                        ";
                        cmd.Parameters.AddWithValue("@AgentFeeSwitchingPK", _agentFeeSwitching.AgentFeeSwitchingPK);
                        cmd.Parameters.AddWithValue("@AgentFeeTrxPercent", _agentFeeSwitching.AgentFeeTrxPercent);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentFeeSwitching.NetAmount);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _agentFeeSwitching.UpdateUsersID);
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

        public void Void_AgentFeeSwitching(AgentFeeSwitching _agentFeeSwitching)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update AgentFeeSwitching set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate 
                        where AgentFeeSwitchingPK = @AgentFeeSwitchingPK and status = 2
               
                        ";
                        cmd.Parameters.AddWithValue("@AgentFeeSwitchingPK", _agentFeeSwitching.AgentFeeSwitchingPK);

                        cmd.Parameters.AddWithValue("@VoidUsersID", _agentFeeSwitching.VoidUsersID);
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


        public bool Validate_MaxPercentAgentFeeSwitching(AgentFeeSwitching _agentFeeSwitching)
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
                        select isnull(AgentFeePercent,0) from AgentFeeSwitching where AgentFeeSwitchingPK = @AgentFeeSwitchingPK and status = 2
                        )
                        BEGIN
	                        select @OldPercent = isnull(AgentFeePercent,0) from AgentFeeSwitching where AgentFeeSwitchingPK = @AgentFeeSwitchingPK and status = 2 and AgentPK <> 1
                        END
                        ELSE
                        BEGIN
	                        select @OldPercent = 0
                        END



                        SELECT @CurrentFeePercent =  SUM(ISNULL(AgentFeePercent,0)) - @OldPercent FROM dbo.AgentFeeSwitching WHERE ClientSwitchingPK = @ClientSwitchingPK and status = 2  and AgentPK <> 1

                        IF (ISNULL(@CurrentFeePercent,0) + @NewFeePercent) > 100
                        BEGIN
	                        SELECT 1 Result
                        END
                        ELSE
                        BEGIN
	                        SELECT 0 Result
                        END ";

                        cmd.Parameters.AddWithValue("@AgentFeeSwitchingPK", _agentFeeSwitching.AgentFeeSwitchingPK);
                        cmd.Parameters.AddWithValue("@NewFeePercent", _agentFeeSwitching.AgentFeeTrxPercent);
                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _agentFeeSwitching.ClientSwitchingPK);
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

        public void Insert_PercentAgentFeeSwitching(AgentFeeSwitching _agentFeeSwitching)
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

                        

                        select @MaxPK = Max(AgentFeeSwitchingPK) from AgentFeeSwitching
                        set @maxPK = isnull(@maxPK,0)

                        IF NOT EXISTS (select * from AgentFeeSwitching where ClientSwitchingPK  = @ClientSwitchingPK and status = 2 and AgentPK = 1)
                        BEGIN
                            select @AgentFeePercent = isnull(sum(AgentFeePercent),0) from AgentFeeSwitching where ClientSwitchingPK  = @ClientSwitchingPK and status = 2
	                        select @AgentFeeHO = 100 - @AgentFeePercent	
                            Insert into AgentFeeSwitching (AgentFeeSwitchingPK,HistoryPK,Status,ClientSwitchingPK,AgentPK,AgentFeePercent,AgentFeeAmount,
                            EntryUsersID,EntryTime,LastUpdate)

                            select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSwitchingPK ASC) AgentFeeSwitchingPK,1,2,@ClientSwitchingPK,1,@AgentFeeHO,@AgentFeeHO/100 * @NetAmount,
                            @EntryUsersID,@EntryTime,@LastUpdate

                        END
                        ELSE
                        BEGIN 
                            select @AgentFeePercent = isnull(sum(AgentFeePercent),0) from AgentFeeSwitching where ClientSwitchingPK  = @ClientSwitchingPK and status = 2 and AgentPK <> 1
	                        select @AgentFeeHO = 100 - @AgentFeePercent	

	                        Update AgentFeeSwitching set AgentFeePercent = @AgentFeeHO,AgentFeeAmount = @AgentFeeHO/100 * @NetAmount,
	                        UpdateUsersID = @UpdateUsersID,UpdateTime = @UpdateTime,LastUpdate = @LastUpdate
	                        where ClientSwitchingPK = @ClientSwitchingPK and status = 2 and AgentPK = 1

                        END
                        ";

                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _agentFeeSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentFeeSwitching.NetAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentFeeSwitching.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _agentFeeSwitching.UpdateUsersID);
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