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
    public class GenerateRebateReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[GenerateRebate] " +
                            "([GenerateRebatePK],[HistoryPK],[Status],[PeriodFrom],[PeriodTo],[FundPK],[FundClientPK],";
        string _paramaterCommand = "@PeriodFrom,@PeriodTo,@FundPK,@ClientPK,";

        //2
        private GenerateRebate setGenerateRebate(SqlDataReader dr)
        {
            GenerateRebate M_GenerateRebate = new GenerateRebate();
            M_GenerateRebate.GenerateRebatePK = Convert.ToInt32(dr["GenerateRebatePK"]);
            M_GenerateRebate.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_GenerateRebate.Status = Convert.ToInt32(dr["Status"]);
            M_GenerateRebate.Notes = Convert.ToString(dr["Notes"]);
            M_GenerateRebate.PeriodFrom = Convert.ToString(dr["PeriodFrom"]);
            M_GenerateRebate.PeriodTo = Convert.ToString(dr["PeriodTo"]);
            M_GenerateRebate.FundName = Convert.ToString(dr["FundName"]);
            M_GenerateRebate.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_GenerateRebate.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_GenerateRebate.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_GenerateRebate.FeeRebate = Convert.ToDecimal(dr["FeeRebate"]);
            M_GenerateRebate.ManagementFee = Convert.ToDecimal(dr["ManagementFee"]);
            M_GenerateRebate.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_GenerateRebate.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_GenerateRebate.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_GenerateRebate.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_GenerateRebate.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_GenerateRebate.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_GenerateRebate.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_GenerateRebate.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_GenerateRebate.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_GenerateRebate.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_GenerateRebate.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_GenerateRebate.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_GenerateRebate;
        }

        private GenerateRebate setGenerateRebateTemp(SqlDataReader dr)
        {
            GenerateRebate M_GenerateRebate = new GenerateRebate();
            M_GenerateRebate.GenerateRebateTempPK = Convert.ToInt32(dr["GenerateRebateTempPK"]);
            M_GenerateRebate.PeriodFrom = Convert.ToString(dr["PeriodFrom"]);
            M_GenerateRebate.PeriodTo = Convert.ToString(dr["PeriodTo"]);
            M_GenerateRebate.FundName = Convert.ToString(dr["FundName"]);
            M_GenerateRebate.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_GenerateRebate.FeeRebate = Convert.ToDecimal(dr["FeeRebate"]);
            M_GenerateRebate.ManagementFee = Convert.ToDecimal(dr["ManagementFee"]);
            M_GenerateRebate.Selected = dr["Selected"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Selected"]);
            return M_GenerateRebate;
        }

        private GenerateRebate setGenerateRebateDetailTemp(SqlDataReader dr)
        {
            GenerateRebate M_GenerateRebate = new GenerateRebate();
            M_GenerateRebate.Date = Convert.ToString(dr["Date"]);
            M_GenerateRebate.FundName = Convert.ToString(dr["FundName"]);
            M_GenerateRebate.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_GenerateRebate.FeeRebate = Convert.ToDecimal(dr["FeeRebate"]);
            M_GenerateRebate.ManagementFee = Convert.ToDecimal(dr["ManagementFee"]);
            M_GenerateRebate.AUM = Convert.ToDecimal(dr["AUM"]);
            return M_GenerateRebate;
        }

        public List<GenerateRebate> GenerateRebate_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GenerateRebate> L_GenerateRebate = new List<GenerateRebate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when G.status=1 then 'PENDING' else Case When G.status = 2 then 'APPROVED' else Case when G.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,*, F.ID + ' - ' + F.Name FundName,FC.Name FundClientName 
                                                from GenerateRebate G left join
                                                Fund F on F.FundPk = G.FundPK and F.Status = 2 left join
                                                FundClient FC on FC.FundClientPK = G.FundClientPK and FC.status = 2
                                                where G.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when G.status=1 then 'PENDING' else Case When G.status = 2 then 'APPROVED' else Case when G.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,*, F.ID + ' - ' + F.Name FundName,FC.Name FundClientName 
                                                from GenerateRebate G left join
                                                Fund F on F.FundPk = G.FundPK and F.Status = 2 left join
                                                FundClient FC on FC.FundClientPK = G.FundClientPK and FC.status = 2 
                                                order by GenerateRebatePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_GenerateRebate.Add(setGenerateRebate(dr));
                                }
                            }
                            return L_GenerateRebate;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public void GenerateRebate_Insert(string _EntryUsersID)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        
                            cmd.CommandText = @"Declare @GenerateRebateTempPK int
                                                Declare @FundClientPK int
                                                Declare @FundPK int
                                                Declare @PeriodFrom datetime
                                                Declare @PeriodTo datetime
                                                Declare @FeeRebate numeric(22,8)
                                                Declare @ManagementFee numeric(22,8)
                                                Declare @AutoNo int
                                                Declare @Date datetime
                                                Declare @AUM numeric(22,8)
                                                Declare @FeeRebateDetail numeric(22,8)
                                                Declare @ManagementFeeDetail numeric(22,8)

                                                Declare A cursor For
	                                                select GenerateRebateTempPK,FundClientPK,FundPK,PeriodFrom,PeriodTo,FeeRebate,ManagementFee from GenerateRebateTemp where selected = 1
                                                Open A
                                                Fetch Next from A
                                                into @GenerateRebateTempPK,@FundClientPK,@FundPK,@PeriodFrom,@PeriodTo,@FeeRebate,@ManagementFee

                                                WHILE @@FETCH_STATUS = 0  
                                                BEGIN 

	                                                INSERT INTO [dbo].[GenerateRebate]([GenerateRebatePK],[HistoryPK],[Status],[PeriodFrom],[PeriodTo],[FundPK],[FundClientPK],[FeeRebate],[ManagementFee],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)
	                                                select isnull(max(GenerateRebatePk),0) + 1,1,2,@PeriodFrom,@PeriodTo,@FundPK,@FundClientPK,@FeeRebate,@ManagementFee,@EntryUsersID,@EntryTime,@EntryUsersID,@EntryTime,@lastupdate from GenerateRebate
	
	                                                Declare B cursor For
		                                                select AutoNo,Date,AUM,FeeRebate,ManagementFee from GenerateRebateDetailTemp where GenerateRebateTempPK = @GenerateRebateTempPK
	                                                Open B
	                                                Fetch Next from B
	                                                into @AutoNo,@Date,@AUM,@FeeRebateDetail,@ManagementFeeDetail

	                                                WHILE @@FETCH_STATUS = 0  
	                                                BEGIN 
		                                                insert into GenerateRebateDetail(GenerateRebatePK,AutoNo,Date,AUM,FeeRebate,ManagementFee,LastUsersID,LastUpdate)
		                                                select @GenerateRebateTempPK,@AutoNo,@Date,@AUM,@FeeRebateDetail,@ManagementFeeDetail,@EntryUsersID,@LastUpdate

		                                                FETCH NEXT FROM B   
		                                                INTO @AutoNo,@Date,@AUM,@FeeRebateDetail,@ManagementFeeDetail
	                                                END   
	                                                CLOSE B
	                                                DEALLOCATE B  

	                                                FETCH NEXT FROM A   
                                                    INTO @GenerateRebateTempPK,@FundClientPK,@FundPK,@PeriodFrom,@PeriodTo,@FeeRebate,@ManagementFee
                                                END   
                                                CLOSE A
                                                DEALLOCATE A ";
                        cmd.Parameters.AddWithValue("@EntryUsersID", _EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int GenerateRebate_Update(GenerateRebate _GenerateRebate, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_GenerateRebate.GenerateRebatePK, _GenerateRebate.HistoryPK, "GenerateRebate");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update GenerateRebate set status=2,Notes=@Notes,FundPK=@FundPK,FundClientPK=@FundClientPK,PeriodFrom=@PeriodFrom,PeriodTo=@PeriodTo
                                ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                                where GenerateRebatePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _GenerateRebate.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                            cmd.Parameters.AddWithValue("@FundPK", _GenerateRebate.FundPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _GenerateRebate.FundClientPK);
                            cmd.Parameters.AddWithValue("@PeriodFrom", _GenerateRebate.PeriodFrom);
                            cmd.Parameters.AddWithValue("@PeriodTo", _GenerateRebate.PeriodTo);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _GenerateRebate.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _GenerateRebate.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update GenerateRebate set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where GenerateRebatePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _GenerateRebate.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
                                cmd.CommandText = @"Update GenerateRebate set status=2,Notes=@Notes,FundPK=@FundPK,FundClientPK=@FundClientPK,PeriodFrom=@PeriodFrom,PeriodTo=@PeriodTo
                                ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                                where GenerateRebatePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _GenerateRebate.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                                cmd.Parameters.AddWithValue("@FundPK", _GenerateRebate.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _GenerateRebate.FundClientPK);
                                cmd.Parameters.AddWithValue("@PeriodFrom", _GenerateRebate.PeriodFrom);
                                cmd.Parameters.AddWithValue("@PeriodTo", _GenerateRebate.PeriodTo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _GenerateRebate.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_GenerateRebate.GenerateRebatePK, "GenerateRebate");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From GenerateRebate where GenerateRebatePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _GenerateRebate.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _GenerateRebate.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _GenerateRebate.FundClientPK);
                                cmd.Parameters.AddWithValue("@PeriodFrom", _GenerateRebate.PeriodFrom);
                                cmd.Parameters.AddWithValue("@PeriodTo", _GenerateRebate.PeriodTo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _GenerateRebate.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update GenerateRebate set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where GenerateRebatePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _GenerateRebate.Notes);
                                cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _GenerateRebate.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public void GenerateRebate_Approved(GenerateRebate _GenerateRebate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GenerateRebate set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where GenerateRebatePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                        cmd.Parameters.AddWithValue("@historyPK", _GenerateRebate.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _GenerateRebate.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update GenerateRebate set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where GenerateRebatePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _GenerateRebate.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void GenerateRebate_Reject(GenerateRebate _GenerateRebate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GenerateRebate set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where GenerateRebatePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                        cmd.Parameters.AddWithValue("@historyPK", _GenerateRebate.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _GenerateRebate.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update GenerateRebate set status= 2,lastupdate=@lastupdate where GenerateRebatePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void GenerateRebate_Void(GenerateRebate _GenerateRebate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update GenerateRebate set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where GenerateRebatePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _GenerateRebate.GenerateRebatePK);
                        cmd.Parameters.AddWithValue("@historyPK", _GenerateRebate.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _GenerateRebate.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<GenerateRebate> GenerateRebate_Get(GenerateRebate _GenerateRebate)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GenerateRebate> L_GenerateRebate = new List<GenerateRebate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"    --Declare @DateFrom datetime
                                                    --Declare @DateTo datetime
                                                    --set @DateFrom = '2016-12-01'
                                                    --set @DateTo = '2016-12-31'

                                                    Declare @AgentPk int

                                                    --set @fundPK = 2
                                                    --set @FundClientPK = 1061

                                                    select @fundpk  = FundPK from Fund where status = 2 and FundPK = @FundPK
                                                    select @FundClientPK  = FundClientPK,@AgentPK = SellingAgentPK from FundClient where status = 2 and FundClientPK = @FundClientPK
                                                    Declare @date table(d datetime) 
                                                    Declare @d datetime

                                                    declare @max int
                                                    Declare @Counter int
                                                    select @max = isnull(max(GenerateRebatePK),0) + 1 from GenerateRebate
                                                    set @Counter = 0
                                                    set @d=@DateFrom

                                                    While @d<=@DateTo 
                                                    Begin
                                                    Insert into @date values (@d)
                                                    set @d=@d+1 
                                                    End 
                                                    truncate table GenerateRebateTemp
                                                    truncate table GenerateRebateDetailTemp

                                                    insert into GenerateRebateTemp(GenerateRebateTempPK,HistoryPK,Status,FundPK,FundClientPK,PeriodFrom,PeriodTo,ManagementFee,FeeRebate)
                                                    select @max,1,1,@FundPK FundPK,@FundClientPK FundClientPK,@dateFrom DateFrom, @DateTo DateTo, sum(ManagementFeeAmount) ManagementFee,sum(TrailFeeAmount) TrailFeeAmount from 
                                                    (Select F.Name FundClientName,E.ID FundID,d,
                                                    isnull(isnull(B.NAV,[dbo].[FgetCloseNav](dbo.fworkingday(d,-1),@FundPK)) * 
                                                    isnull([dbo].[Get_UnitAmountByFundPKandFundClientPK](dbo.fworkingday(d,-1),@fundclientpk,@FundPK),
                                                    [dbo].[Get_UnitAmountByFundPKandFundClientPK](dbo.fworkingday(d,-1),@fundclientpk,@FundPK)),0) AUM,    
                                                    isnull( [dbo].[FgetManagementFeePercent](@FundPK)*
                                                    ([dbo].[FgetCloseNav](dbo.fworkingday(d,-1),@FundPK) * 
                                                    [dbo].[Get_UnitAmountByFundPKandFundClientPK](dbo.fworkingday(d,-2),@fundclientpk,@FundPK)) / 365 / 100,0) ManagementFeeAmount, 
                                                    isnull( [dbo].[FgetManagementFeePercent](@FundPK)*
                                                    ([dbo].[FgetCloseNav](dbo.fworkingday(d,-1),@FundPK) * 
                                                    [dbo].[Get_UnitAmountByFundPKandFundClientPK](dbo.fworkingday(d,-2),@fundclientpk,@FundPK)) / 365 / 100,0) * [dbo].FGetFCRebatePercentByDate(d,@fundclientpk,@FundPK)
                                                    TrailFeeAmount    
                                                    from @date A    
                                                    left join CloseNAV B on A.d = B.Date and B.Status = 2 and B.FundPK  = @FundPK    
                                                    left join ClientSubscription C on A.d = C.NAVDate and C.status = 2  and C.Posted = 1 and C.FundClientPK = @FundClientPK    and C.FundPK = @FundPK
                                                    left join ClientRedemption D on A.d = D.NAVDate and D.Status = 2  and D.Posted = 1 and D.FundClientPK = @FundClientPK  and D.FundPK = @FundPK
                                                    left join Fund E on E.FundPK = @fundPK and E.status = 2
                                                    Left Join FundClient F on F.FundClientPK = @FundClientPK and F.Status = 2
                                                    left join FundClientPosition FCP on FCP.FundClientPK = F.FundClientPK and FCP.FundPK = E.FundPK and FCP.date = A.d
                                                    group by F.Name,E.ID,E.FundPK,F.FundClientPK,A.d,B.NAV,C.TotalUnitAmount,C.TotalCashAmount,D.TotalUnitAmount,D.TotalCashAmount,FCP.UnitAmount,F.SACode ) A
                                                    group by A.FundID

                                                    insert into GenerateRebateDetailTemp(GenerateRebateTempPK,AutoNo,Date,AUM,ManagementFee,FeeRebate)
                                                    Select @max,ROW_NUMBER() OVER(ORDER BY d ASC) + @Counter,d,
                                                    isnull(isnull(B.NAV,[dbo].[FgetCloseNav](dbo.fworkingday(d,-1),@FundPK)) * 
                                                    isnull([dbo].[Get_UnitAmountByFundPKandFundClientPK](dbo.fworkingday(d,-1),@fundclientpk,@FundPK),
                                                    [dbo].[Get_UnitAmountByFundPKandFundClientPK](dbo.fworkingday(d,-1),@fundclientpk,@FundPK)),0) AUM,    
                                                    isnull( [dbo].[FgetManagementFeePercent](@FundPK)*
                                                    ([dbo].[FgetCloseNav](dbo.fworkingday(d,-1),@FundPK) * 
                                                    [dbo].[Get_UnitAmountByFundPKandFundClientPK](dbo.fworkingday(d,-2),@fundclientpk,@FundPK)) / 365 / 100,0) ManagementFeeAmount, 
                                                    isnull( [dbo].[FgetManagementFeePercent](@FundPK)*
                                                    ([dbo].[FgetCloseNav](dbo.fworkingday(d,-1),@FundPK) * 
                                                    [dbo].[Get_UnitAmountByFundPKandFundClientPK](dbo.fworkingday(d,-2),@fundclientpk,@FundPK)) / 365 / 100,0) * [dbo].FGetFCRebatePercentByDate(d,@fundclientpk,@FundPK)
                                                    TrailFeeAmount    
                                                    from @date A    
                                                    left join CloseNAV B on A.d = B.Date and B.Status = 2 and B.FundPK  = @FundPK    
                                                    left join ClientSubscription C on A.d = C.NAVDate and C.status = 2  and C.Posted = 1 and C.FundClientPK = @FundClientPK    and C.FundPK = @FundPK
                                                    left join ClientRedemption D on A.d = D.NAVDate and D.Status = 2  and D.Posted = 1 and D.FundClientPK = @FundClientPK  and D.FundPK = @FundPK
                                                    left join Fund E on E.FundPK = @fundPK and E.status = 2
                                                    Left Join FundClient F on F.FundClientPK = @FundClientPK and F.Status = 2
                                                    left join FundClientPosition FCP on FCP.FundClientPK = F.FundClientPK and FCP.FundPK = E.FundPK and FCP.date = A.d
                                                    group by F.Name,E.ID,E.FundPK,F.FundClientPK,A.d,B.NAV,C.TotalUnitAmount,C.TotalCashAmount,D.TotalUnitAmount,D.TotalCashAmount,FCP.UnitAmount,F.SACode";
                        
                        cmd.Parameters.AddWithValue("@DateFrom", _GenerateRebate.PeriodFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _GenerateRebate.PeriodTo);
                        cmd.Parameters.AddWithValue("@FundPK", _GenerateRebate.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _GenerateRebate.FundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_GenerateRebate.Add(setGenerateRebate(dr));
                                }
                            }
                            return L_GenerateRebate;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<GenerateRebate> GenerateRebateDetail_Get(int _FundPK, int _FundClientPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GenerateRebate> L_GenerateRebate = new List<GenerateRebate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select F.ID + ' - ' + F.Name FundName,FC.Name FundClientName,G.* From GenerateRebateTemp G left join
                        Fund F on F.FundPk = G.FundPK and F.status = 2 left join
                        FundClient FC on FC.FundClientPK = G.FundClientPK and FC.Status = 2
                        where G.FundPK = @FundPK and G.FundClientPK = @FundClientPK";
                        cmd.Parameters.AddWithValue("@FundPK", _FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_GenerateRebate.Add(setGenerateRebateTemp(dr));
                                }
                            }
                            return L_GenerateRebate;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<GenerateRebate> GenerateRebateDetail_Show(int _GenerateRebatePK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GenerateRebate> L_GenerateRebate = new List<GenerateRebate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select F.ID + ' - ' + F.Name FundName,FC.Name FundClientName,GRD.* from GenerateRebateDetailTemp GRD left join
                        GenerateRebateTemp G on G.GenerateRebateTempPK = GRD.GenerateRebateTempPK left join
                        Fund F on F.FundPk = G.FundPK and F.status = 2 left join
                        FundClient FC on FC.FundClientPK = G.FundClientPK and FC.Status = 2
                        where GRD.GenerateRebateTempPK = @GenerateRebatePK";
                        cmd.Parameters.AddWithValue("@GenerateRebatePK", _GenerateRebatePK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_GenerateRebate.Add(setGenerateRebateDetailTemp(dr));
                                }
                            }
                            return L_GenerateRebate;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<GenerateRebate> GetRebateDetailByGenerateRebatePK(int _GenerateRebatePK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GenerateRebate> L_GenerateRebate = new List<GenerateRebate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select F.ID + ' - ' + F.Name FundName,FC.Name FundClientName,GRD.* from GenerateRebateDetail GRD left join
                        GenerateRebate G on G.GenerateRebatePK = GRD.GenerateRebatePK left join
                        Fund F on F.FundPk = G.FundPK and F.status = 2 left join
                        FundClient FC on FC.FundClientPK = G.FundClientPK and FC.Status = 2
                        where GRD.GenerateRebatePK = @GenerateRebatePK";
                        cmd.Parameters.AddWithValue("@GenerateRebatePK", _GenerateRebatePK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_GenerateRebate.Add(setGenerateRebateDetailTemp(dr));
                                }
                            }
                            return L_GenerateRebate;
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