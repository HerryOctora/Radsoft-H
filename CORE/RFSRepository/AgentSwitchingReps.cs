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
    public class AgentSwitchingReps
    {
        Host _host = new Host();


        //2
        private AgentSwitching setAgentSwitching(SqlDataReader dr)
        {
            AgentSwitching M_AgentSwitching = new AgentSwitching();
            M_AgentSwitching.AgentSwitchingPK = Convert.ToInt32(dr["AgentSwitchingPK"]);
            M_AgentSwitching.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AgentSwitching.Status = Convert.ToInt32(dr["Status"]);
            M_AgentSwitching.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AgentSwitching.ClientSwitchingPK = Convert.ToInt32(dr["ClientSwitchingPK"]);

            M_AgentSwitching.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_AgentSwitching.AgentID = Convert.ToString(dr["AgentID"]);
            M_AgentSwitching.AgentName = Convert.ToString(dr["AgentName"]);
            M_AgentSwitching.AgentTrxPercent = Convert.ToDecimal(dr["AgentTrxPercent"]);
            M_AgentSwitching.AgentTrxAmount = Convert.ToDecimal(dr["AgentTrxAmount"]);
            M_AgentSwitching.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AgentSwitching.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AgentSwitching.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AgentSwitching.EntryTime = dr["EntryTime"].ToString();
            M_AgentSwitching.UpdateTime = dr["UpdateTime"].ToString();
            M_AgentSwitching.VoidTime = dr["VoidTime"].ToString();
            M_AgentSwitching.DBUserID = dr["DBUserID"].ToString();
            M_AgentSwitching.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AgentSwitching.LastUpdate = dr["LastUpdate"].ToString();
            M_AgentSwitching.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AgentSwitching;
        }

        public List<AgentSwitching> Get_DataAgentSwitchingByClientSwitchingPK(int _ClientSwitchingPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<AgentSwitching> L_AgentSwitching = new List<AgentSwitching>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID,B.Name AgentName,* from AgentSwitching A
                        left join Agent B on A.AgentPK = B.AgentPK and B.status in (1,2)
                        where ClientSwitchingPK = @ClientSwitchingPK and A.status = 2 ";

                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _ClientSwitchingPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AgentSwitching.Add(setAgentSwitching(dr));
                                }
                            }
                            return L_AgentSwitching;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Add_AgentSwitching(AgentSwitching _agentSwitching)
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

                        select @MaxPK = Max(AgentSwitchingPK) from AgentSwitching
                        set @maxPK = isnull(@maxPK,0)

                        Insert into AgentSwitching (AgentSwitchingPK,HistoryPK,Status,ClientSwitchingPK,AgentPK,AgentTrxPercent,AgentTrxAmount,
                        EntryUsersID,EntryTime,LastUpdate)

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSwitchingPK ASC) AgentSwitchingPK,1,2,@ClientSwitchingPK,@AgentPK,@AgentTrxPercent,@AgentTrxPercent/100 * @NetAmount,
                        @EntryUsersID,@EntryTime,@LastUpdate

                        select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSwitchingPK ASC) Result

                        ";


                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _agentSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentSwitching.AgentPK);
                        cmd.Parameters.AddWithValue("@AgentTrxPercent", _agentSwitching.AgentTrxPercent);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentSwitching.NetAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentSwitching.EntryUsersID);
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


        public int Update_AgentSwitching(AgentSwitching _agentSwitching)
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
                        Update AgentSwitching set AgentTrxPercent = @AgentTrxPercent,AgentTrxAmount = @AgentTrxPercent/100 * @NetAmount,
                        UpdateUsersID = @UpdateUsersID,UpdateTime = @UpdateTime,LastUpdate = @LastUpdate
                        where AgentSwitchingPK = @AgentSwitchingPK and status = 2



                        ";
                        cmd.Parameters.AddWithValue("@AgentSwitchingPK", _agentSwitching.AgentSwitchingPK);
                        cmd.Parameters.AddWithValue("@AgentTrxPercent", _agentSwitching.AgentTrxPercent);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentSwitching.NetAmount);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _agentSwitching.UpdateUsersID);
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

        public void Void_AgentSwitching(AgentSwitching _agentSwitching)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update AgentSwitching set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate 
                        where AgentSwitchingPK = @AgentSwitchingPK and status = 2
               
                        
                        ";
                        cmd.Parameters.AddWithValue("@AgentSwitchingPK", _agentSwitching.AgentSwitchingPK);

                        cmd.Parameters.AddWithValue("@VoidUsersID", _agentSwitching.VoidUsersID);
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


        public bool Validate_MaxPercentAgentSwitching(AgentSwitching _agentSwitching)
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
                        select isnull(AgentTrxpercent,0) from AgentSwitching where AgentSwitchingPK = @AgentSwitchingPK and status = 2
                        )
                        BEGIN
	                        select @OldPercent = isnull(AgentTrxpercent,0) from AgentSwitching where AgentSwitchingPK = @AgentSwitchingPK and status = 2 and AgentPK <> 1
                        END
                        ELSE
                        BEGIN
	                        select @OldPercent = 0
                        END



                        SELECT @CurrentFeePercent =  SUM(ISNULL(AgentTrxpercent,0)) - @OldPercent FROM dbo.AgentSwitching WHERE ClientSwitchingPK = @ClientSwitchingPK and status = 2 and AgentPK <> 1

                        IF (ISNULL(@CurrentFeePercent,0) + @NewFeePercent) > 100
                        BEGIN
	                        SELECT 1 Result
                        END
                        ELSE
                        BEGIN
	                        SELECT 0 Result
                        END ";

                        cmd.Parameters.AddWithValue("@AgentSwitchingPK", _agentSwitching.AgentSwitchingPK);
                        cmd.Parameters.AddWithValue("@NewFeePercent", _agentSwitching.AgentTrxPercent);
                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _agentSwitching.ClientSwitchingPK);
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

        public void Insert_PercentAgentSwitching(AgentSwitching _agentSwitching)
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

                        

                        select @MaxPK = Max(AgentSwitchingPK) from AgentSwitching
                        set @maxPK = isnull(@maxPK,0)

                        IF NOT EXISTS (select * from AgentSwitching where ClientSwitchingPK  = @ClientSwitchingPK and status = 2 and AgentPK = 1)
                        BEGIN
                            select @AgentPercent = isnull(sum(AgentTrxPercent),0) from AgentSwitching where ClientSwitchingPK  = @ClientSwitchingPK and status = 2
	                        select @AgentHO = 100 - @AgentPercent	
                            Insert into AgentSwitching (AgentSwitchingPK,HistoryPK,Status,ClientSwitchingPK,AgentPK,AgentTrxPercent,AgentTrxAmount,
                            EntryUsersID,EntryTime,LastUpdate)

                            select @MaxPK + ROW_NUMBER() OVER(ORDER BY @ClientSwitchingPK ASC) AgentSwitchingPK,1,2,@ClientSwitchingPK,1,@AgentHO,@AgentHO/100 * @NetAmount,
                            @EntryUsersID,@EntryTime,@LastUpdate

                        END
                        ELSE
                        BEGIN 
                            select @AgentPercent = isnull(sum(AgentTrxPercent),0) from AgentSwitching where ClientSwitchingPK  = @ClientSwitchingPK and status = 2 and AgentPK <> 1
	                        select @AgentHO = 100 - @AgentPercent	

	                        Update AgentSwitching set AgentTrxPercent = @AgentHO,AgentTrxAmount = @AgentHO/100 * @NetAmount,
	                        UpdateUsersID = @UpdateUsersID,UpdateTime = @UpdateTime,LastUpdate = @LastUpdate
	                        where ClientSwitchingPK = @ClientSwitchingPK and status = 2 and AgentPK = 1

                        END
                        ";

                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _agentSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@NetAmount", _agentSwitching.NetAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _agentSwitching.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _agentSwitching.UpdateUsersID);
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