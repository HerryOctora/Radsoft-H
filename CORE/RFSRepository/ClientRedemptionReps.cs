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
using System.Data;
using OfficeOpenXml.Drawing;

namespace RFSRepository
{
    public class ClientRedemptionReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();

        string _insertCommand = "INSERT INTO [dbo].[ClientRedemption] " +
                            "([ClientRedemptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[PaymentDate]," +
                            " [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Type],[BitRedemptionAll],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount]," +
                            " [RedemptionFeePercent],[RedemptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[UnitPosition],[BankRecipientPK],[TransferType],[FeeType],[ReferenceSInvest],";
        string _paramaterCommand = "@NAVDate,@ValueDate,@PaymentDate,@NAV,@FundPK,@FundClientPK,@CashRefPK,@CurrencyPK,Type=@Type," +
                            "@BitRedemptionAll,@Description,@CashAmount,@UnitAmount,@TotalCashAmount,@TotalUnitAmount,@RedemptionFeePercent,@RedemptionFeeAmount,@AgentPK,@AgentFeePercent,@AgentFeeAmount,@UnitPosition,@BankRecipientPK,@TransferType,@FeeType,@ReferenceSInvest,";

        private ClientRedemption setClientRedemption(SqlDataReader dr)
        {
            ClientRedemption M_ClientRedemption = new ClientRedemption();
            M_ClientRedemption.ClientRedemptionPK = Convert.ToInt32(dr["ClientRedemptionPK"]);
            M_ClientRedemption.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_ClientRedemption.Selected = Convert.ToBoolean(dr["Selected"]);
            M_ClientRedemption.Status = Convert.ToInt32(dr["Status"]);
            M_ClientRedemption.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_ClientRedemption.Type = Convert.ToInt32(dr["Type"]);
            M_ClientRedemption.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_ClientRedemption.Notes = Convert.ToString(dr["Notes"]);
            M_ClientRedemption.NAVDate = dr["NAVDate"].ToString();
            M_ClientRedemption.ValueDate = Convert.ToString(dr["ValueDate"]);
            M_ClientRedemption.PaymentDate = Convert.ToString(dr["PaymentDate"]);
            M_ClientRedemption.NAV = Convert.ToDecimal(dr["NAV"]);
            M_ClientRedemption.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_ClientRedemption.FundID = Convert.ToString(dr["FundID"]);
            M_ClientRedemption.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_ClientRedemption.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_ClientRedemption.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_ClientRedemption.CashRefPK = Convert.ToInt32(dr["CashRefPK"]);
            M_ClientRedemption.CashRefID = Convert.ToString(dr["CashRefID"]);
            M_ClientRedemption.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_ClientRedemption.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_ClientRedemption.BitRedemptionAll = Convert.ToBoolean(dr["BitRedemptionAll"]);
            M_ClientRedemption.Description = Convert.ToString(dr["Description"]);
            M_ClientRedemption.ReferenceSInvest = Convert.ToString(dr["ReferenceSInvest"]);
            M_ClientRedemption.CashAmount = Convert.ToDecimal(dr["CashAmount"]);
            M_ClientRedemption.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            M_ClientRedemption.TotalCashAmount = Convert.ToDecimal(dr["TotalCashAmount"]);
            M_ClientRedemption.TotalUnitAmount = Convert.ToDecimal(dr["TotalUnitAmount"]);
            M_ClientRedemption.RedemptionFeePercent = Convert.ToDecimal(dr["RedemptionFeePercent"]);
            M_ClientRedemption.RedemptionFeeAmount = Convert.ToDecimal(dr["RedemptionFeeAmount"]);
            M_ClientRedemption.FundName = Convert.ToString(dr["FundName"]);
            M_ClientRedemption.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_ClientRedemption.AgentID = Convert.ToString(dr["AgentID"]);
            M_ClientRedemption.AgentName = Convert.ToString(dr["AgentName"]);
            M_ClientRedemption.AgentFeePercent = Convert.ToDecimal(dr["AgentFeePercent"]);
            M_ClientRedemption.AgentFeeAmount = Convert.ToDecimal(dr["AgentFeeAmount"]);
            M_ClientRedemption.UnitPosition = Convert.ToDecimal(dr["UnitPosition"]);
            M_ClientRedemption.BankRecipientPK = Convert.ToInt32(dr["BankRecipientPK"]);
            M_ClientRedemption.BankRecipientDesc = Convert.ToString(dr["BankRecipientDesc"]);
            M_ClientRedemption.TransferType = Convert.ToInt32(dr["TransferType"]);
            M_ClientRedemption.TransferTypeDesc = Convert.ToString(dr["TransferTypeDesc"]);
            M_ClientRedemption.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_ClientRedemption.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);
            M_ClientRedemption.Posted = Convert.ToBoolean(dr["Posted"]);
            M_ClientRedemption.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_ClientRedemption.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_ClientRedemption.Revised = Convert.ToBoolean(dr["Revised"]);
            M_ClientRedemption.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_ClientRedemption.RevisedTime = dr["RevisedTime"].ToString();
            M_ClientRedemption.EntryUsersID = dr["EntryUsersID"].ToString();
            M_ClientRedemption.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_ClientRedemption.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_ClientRedemption.VoidUsersID = dr["VoidUsersID"].ToString();
            M_ClientRedemption.EntryTime = dr["EntryTime"].ToString();
            M_ClientRedemption.UpdateTime = dr["UpdateTime"].ToString();
            M_ClientRedemption.ApprovedTime = dr["ApprovedTime"].ToString();
            M_ClientRedemption.VoidTime = dr["VoidTime"].ToString();
            M_ClientRedemption.DBUserID = dr["DBUserID"].ToString();
            M_ClientRedemption.DBTerminalID = dr["DBTerminalID"].ToString();
            M_ClientRedemption.LastUpdate = dr["LastUpdate"].ToString();
            M_ClientRedemption.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            M_ClientRedemption.TransactionPK = dr["TransactionPK"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TransactionPK"]);
            M_ClientRedemption.IFUACode = dr["IFUACode"].ToString();
            M_ClientRedemption.FrontID = dr["FrontID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FrontID"]);
            return M_ClientRedemption;
        }

        public int ClientRedemption_Add(ClientRedemption _clientRedemption, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[IsBOTransaction],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(ClientRedemptionPk),0) + 1,1,@status," + _paramaterCommand + "1,@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From ClientRedemption";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = @"Declare @ClientRedemptionPK int   
                                    Select @ClientRedemptionPK = Max(ClientRedemptionPK) + 1 From ClientRedemption
                                    set @ClientRedemptionPK = isnull(@ClientRedemptionPK,1) 
                                " + _insertCommand + "[IsBOTransaction],[EntryUsersID],[EntryTime],[LastUpdate])" +
                                @"  
                                
                            Select @ClientRedemptionPK,1,@status," + _paramaterCommand + "1,@EntryUsersID,@EntryTime,@LastUpdate" +
                                @"

                                   Declare @LastNAV numeric(22,8)
		                           Declare @RedemptUnit numeric(22,8)
		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @FundPK and status = 2 and Date < @NAVDate
		                           ) and status = 2 and FundPK = @FundPK

		                           --set @LastNAV = isnull(@LastNAV,1)
--		                           set @RedemptUnit = 0
--		                           IF @CashAmount > 0 and @NAV = 0
--		                           BEGIN
--				                        set @RedemptUnit = @CashAmount / @LastNAV
--		                           END
--		                           ELSE
--		                           BEGIN
--				                        set @RedemptUnit = @UnitAmount
--		                           END

--                                    set @RedemptUnit = @RedemptUnit * -1

--                                    Declare @UnitPrevious numeric(22,8)

--                                    set @UnitPrevious = 0
--                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                    where FundClientPK = @FundClientPK and FundPK = @FundPK

--                                    set @UnitPrevious = isnull(@UnitPrevious,0)

--                                    update fundclientpositionsummary
--		                           set Unit = Unit + @RedemptUnit
--		                           where FundClientPK = @FundClientPK and FundPK = @FundPK
                                    
                               
--                                    --Buy = 1,
--                                    --Sell = 2,
--                                    --Adjustment = 3,
--                                    --SwitchingIn = 5,
--                                    --SwitchingOut = 6

--                                    insert into [FundClientPositionLog]
--                                               ([UserId]
--                                               ,[FundClientPK]
--                                               ,[TransactionPK]
--                                               ,[ClientTransactionPK]
--                                               ,[UnitPrevious]
--                                               ,[UnitChanges]
--                                               ,[UnitAfter]
--                                               ,[IsBoTransaction]
--                                               ,[TransactionType]
--                                               ,[Description]
--                                                ,[FundPK])
--                                    Select @EntryUsersID,@FundClientPK,NULL,@ClientRedemptionPK,@UnitPrevious,@RedemptUnit,@UnitPrevious + @RedemptUnit,1,2,'Add Redemption',@FundPK

                                "
                                ;
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Type", _clientRedemption.Type);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                        cmd.Parameters.AddWithValue("@ValueDate", _clientRedemption.ValueDate);
                        cmd.Parameters.AddWithValue("@PaymentDate", _clientRedemption.PaymentDate);
                        cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                        cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                        cmd.Parameters.AddWithValue("@CashRefPK", _clientRedemption.CashRefPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _clientRedemption.CurrencyPK);
                        cmd.Parameters.AddWithValue("@BitRedemptionAll", _clientRedemption.BitRedemptionAll);
                        cmd.Parameters.AddWithValue("@Description", _clientRedemption.Description);
                        cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientRedemption.ReferenceSInvest);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                        cmd.Parameters.AddWithValue("@TotalCashAmount", _clientRedemption.TotalCashAmount);
                        cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientRedemption.TotalUnitAmount);
                        cmd.Parameters.AddWithValue("@RedemptionFeePercent", _clientRedemption.RedemptionFeePercent);
                        cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _clientRedemption.RedemptionFeeAmount);
                        cmd.Parameters.AddWithValue("@AgentPK", _clientRedemption.AgentPK);
                        cmd.Parameters.AddWithValue("@AgentFeePercent", _clientRedemption.AgentFeePercent);
                        cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientRedemption.AgentFeeAmount);
                        cmd.Parameters.AddWithValue("@UnitPosition", _clientRedemption.UnitPosition);
                        cmd.Parameters.AddWithValue("@BankRecipientPK", _clientRedemption.BankRecipientPK);
                        cmd.Parameters.AddWithValue("@TransferType", _clientRedemption.TransferType);
                        cmd.Parameters.AddWithValue("@FeeType", _clientRedemption.FeeType);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _clientRedemption.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ClientRedemption");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ClientRedemption_Update(ClientRedemption _clientRedemption, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int _return = 0;
                int status = _host.Get_Status(_clientRedemption.ClientRedemptionPK, _clientRedemption.HistoryPK, "ClientRedemption");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update ClientRedemption set status=2, Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,PaymentDate=@PaymentDate, 
                                NAV=@NAV,FundPK=@FundPK,FundClientPK=@FundClientPK,CashRefPK=@CashRefPK,CurrencyPK=@CurrencyPK,BitRedemptionAll=@BitRedemptionAll,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,TotalCashAmount=@TotalCashAmount,TotalUnitAmount=@TotalUnitAmount, 
                                RedemptionFeePercent=@RedemptionFeePercent,RedemptionFeeAmount=@RedemptionFeeAmount,AgentPK=@AgentPK,AgentFeePercent=@AgentFeePercent,AgentFeeAmount=@AgentFeeAmount,UnitPosition=@UnitPosition,BankRecipientPK=@BankRecipientPK, TransferType = @TransferType,
                                FeeType=@FeeType,  ReferenceSInvest = @ReferenceSInvest,  Type=@Type,                            
                                ApprovedUsersID=@ApprovedUsersID,  
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate  
                                where ClientRedemptionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                            cmd.Parameters.AddWithValue("@Type", _clientRedemption.Type);
                            cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                            cmd.Parameters.AddWithValue("@ValueDate", _clientRedemption.ValueDate);
                            cmd.Parameters.AddWithValue("@PaymentDate", _clientRedemption.PaymentDate);
                            cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                            cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                            cmd.Parameters.AddWithValue("@CashRefPK", _clientRedemption.CashRefPK);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _clientRedemption.CurrencyPK);
                            cmd.Parameters.AddWithValue("@BitRedemptionAll", _clientRedemption.BitRedemptionAll);
                            cmd.Parameters.AddWithValue("@Description", _clientRedemption.Description);
                            cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientRedemption.ReferenceSInvest);
                            cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                            cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                            cmd.Parameters.AddWithValue("@TotalCashAmount", _clientRedemption.TotalCashAmount);
                            cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientRedemption.TotalUnitAmount);
                            cmd.Parameters.AddWithValue("@RedemptionFeePercent", _clientRedemption.RedemptionFeePercent);
                            cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _clientRedemption.RedemptionFeeAmount);
                            cmd.Parameters.AddWithValue("@AgentPK", _clientRedemption.AgentPK);
                            cmd.Parameters.AddWithValue("@AgentFeePercent", _clientRedemption.AgentFeePercent);
                            cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientRedemption.AgentFeeAmount);
                            cmd.Parameters.AddWithValue("@UnitPosition", _clientRedemption.UnitPosition);
                            cmd.Parameters.AddWithValue("@BankRecipientPK", _clientRedemption.BankRecipientPK);
                            cmd.Parameters.AddWithValue("@TransferType", _clientRedemption.TransferType);
                            cmd.Parameters.AddWithValue("@FeeType", _clientRedemption.FeeType);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClientRedemption set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ClientRedemptionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        _return = 0;
                    }
                    else
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                                   Declare @LastNAV numeric(22,8)
		                           Declare @RedemptUnit numeric(22,8)


                                    Declare @OldRedemptUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(100)
                                    Declare @TransactionPK nvarchar(200)
                                    Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@OldNAV = NAV,@TrxFrom = EntryUsersID,
                                    @TransactionPK = TransactionPK
                                    From ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                    and ClientTransactionPK = @PK and TransactionType = 2 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 2
                                    )

                                    Set @OldCashAmount = 0


		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
		                           ) and status = 2 and FundPK = @OldFundPK

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @OldRedemptUnit = 0
		                           IF @OldCashAmount > 0 and @oldNAV = 0
		                           BEGIN
				                        set @OldRedemptUnit = @OldCashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @OldRedemptUnit = @OldUnitAmount
		                           END

                                    --Declare @UnitPrevious numeric(22,8)
--                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

--                                    update fundclientpositionsummary
--		                           set Unit = Unit + @OldRedemptUnit
--		                           where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
--                                    --Buy = 1,
--                                    --Sell = 2,
--                                    --Adjustment = 3,
--                                    --SwitchingIn = 5,
--                                    --SwitchingOut = 6

--                                    insert into [FundClientPositionLog]
--                                               ([UserId]
--                                               ,[FundClientPK]
--                                               ,[TransactionPK]
--                                               ,[ClientTransactionPK]
--                                               ,[UnitPrevious]
--                                               ,[UnitChanges]
--                                               ,[UnitAfter]
--                                               ,[IsBoTransaction]
--                                               ,[TransactionType]
--                                               ,[Description]
--                                                ,[FundPK])
--                                    Select @UpdateUsersID,@OldFundClientPK, @TransactionPK,@PK,@UnitPrevious,@OldRedemptUnit,@UnitPrevious + @OldRedemptUnit
--                                    ,case when @TrxFrom = 'rdo' then 0 else 1 end
--                                    ,2,'Update Redemption Old Data Revise',@OldFundPK
                                ";
                            cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);


                            cmd.ExecuteNonQuery();
                        }


                        if (status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update ClientRedemption set Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,PaymentDate=@PaymentDate, 
                                NAV=@NAV,FundPK=@FundPK,FundClientPK=@FundClientPK,CashRefPK=@CashRefPK,CurrencyPK=@CurrencyPK,BitRedemptionAll=@BitRedemptionAll,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,TotalCashAmount=@TotalCashAmount,TotalUnitAmount=@TotalUnitAmount, 
                                RedemptionFeePercent=@RedemptionFeePercent,RedemptionFeeAmount=@RedemptionFeeAmount,AgentPK=@AgentPK,AgentFeePercent=@AgentFeePercent,AgentFeeAmount=@AgentFeeAmount,UnitPosition=@UnitPosition,BankRecipientPK=@BankRecipientPK,  TransferType = @TransferType,
                                FeeType=@FeeType,ReferenceSInvest = @ReferenceSInvest, Type=@Type,                        
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate  
                                where ClientRedemptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                                cmd.Parameters.AddWithValue("@Type", _clientRedemption.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientRedemption.ValueDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _clientRedemption.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                                cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPK", _clientRedemption.CashRefPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientRedemption.CurrencyPK);
                                cmd.Parameters.AddWithValue("@BitRedemptionAll", _clientRedemption.BitRedemptionAll);
                                cmd.Parameters.AddWithValue("@TransferType", _clientRedemption.TransferType);
                                cmd.Parameters.AddWithValue("@Description", _clientRedemption.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientRedemption.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmount", _clientRedemption.TotalCashAmount);
                                cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientRedemption.TotalUnitAmount);
                                cmd.Parameters.AddWithValue("@RedemptionFeePercent", _clientRedemption.RedemptionFeePercent);
                                cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _clientRedemption.RedemptionFeeAmount);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientRedemption.AgentPK);
                                cmd.Parameters.AddWithValue("@AgentFeePercent", _clientRedemption.AgentFeePercent);
                                cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientRedemption.AgentFeeAmount);
                                cmd.Parameters.AddWithValue("@UnitPosition", _clientRedemption.UnitPosition);
                                cmd.Parameters.AddWithValue("@BankRecipientPK", _clientRedemption.BankRecipientPK);
                                cmd.Parameters.AddWithValue("@FeeType", _clientRedemption.FeeType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            _return = 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_clientRedemption.ClientRedemptionPK, "ClientRedemption");
                                cmd.CommandText = _insertCommand + "TransactionPK,[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "@TransactionPK,EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ClientRedemption where ClientRedemptionPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Type", _clientRedemption.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientRedemption.ValueDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _clientRedemption.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                                cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPK", _clientRedemption.CashRefPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientRedemption.CurrencyPK);
                                cmd.Parameters.AddWithValue("@BitRedemptionAll", _clientRedemption.BitRedemptionAll);
                                cmd.Parameters.AddWithValue("@Description", _clientRedemption.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientRedemption.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmount", _clientRedemption.TotalCashAmount);
                                cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientRedemption.TotalUnitAmount);
                                cmd.Parameters.AddWithValue("@RedemptionFeePercent", _clientRedemption.RedemptionFeePercent);
                                cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _clientRedemption.RedemptionFeeAmount);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientRedemption.AgentPK);
                                cmd.Parameters.AddWithValue("@AgentFeePercent", _clientRedemption.AgentFeePercent);
                                cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientRedemption.AgentFeeAmount);
                                cmd.Parameters.AddWithValue("@UnitPosition", _clientRedemption.UnitPosition);
                                cmd.Parameters.AddWithValue("@BankRecipientPK", _clientRedemption.BankRecipientPK);
                                cmd.Parameters.AddWithValue("@TransferType", _clientRedemption.TransferType);
                                cmd.Parameters.AddWithValue("@FeeType", _clientRedemption.FeeType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@TransactionPK", _clientRedemption.TransactionPK);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ClientRedemption set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where ClientRedemptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }


                            _return = _newHisPK;
                        }
                    }


                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                   Declare @LastNAV numeric(22,8)
		                           Declare @RedemptUnit numeric(22,8)
                                    Declare @TrxFrom nvarchar(200)
                                    Declare  @TransactionPK nvarchar(200)
                                    Select @TrxFrom = EntryUsersID,  @TransactionPK = TransactionPK from ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                  Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @FundPK and status = 2 and Date < @NAVDate
		                           ) and status = 2 and FundPK = @FundPK

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @RedemptUnit = 0
		                           IF @CashAmount > 0 and @NAV = 0
		                           BEGIN
				                        set @RedemptUnit = @CashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @RedemptUnit = @UnitAmount
		                           END
                                   --set @RedemptUnit = @RedemptUnit * -1
--                                    Declare @UnitPrevious numeric(22,8)
--                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                    where FundClientPK = @FundClientPK and FundPK = @FundPK

--                                    update fundclientpositionsummary
--		                           set Unit = Unit + @RedemptUnit
--		                           where FundClientPK = @FundClientPK and FundPK = @FundPK
                               
--                                    --Buy = 1,
--                                    --Sell = 2,
--                                    --Adjustment = 3,
--                                    --SwitchingIn = 5,
--                                    --SwitchingOut = 6

--                                    insert into [FundClientPositionLog]
--                                               ([UserId]
--                                               ,[FundClientPK]
--                                               ,[TransactionPK]
--                                               ,[ClientTransactionPK]
--                                               ,[UnitPrevious]
--                                               ,[UnitChanges]
--                                               ,[UnitAfter]
--                                               ,[IsBoTransaction]
--                                               ,[TransactionType]
--                                              ,[Description]
--                                                ,[FundPK])
--                                    Select @UpdateUsersID,@FundClientPK, @TransactionPK,@PK,@UnitPrevious,@RedemptUnit,@UnitPrevious + @RedemptUnit
--                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Update Redemption',@FundPK

                                ";
                        cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                        cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                        cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);

                        cmd.ExecuteNonQuery();
                    }
                    return _return;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        
        public void ClientRedemption_Approved(ClientRedemption _ClientRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientRedemption set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where ClientRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ClientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ClientRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _ClientRedemption.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClientRedemption set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ClientRedemptionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ClientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ClientRedemption.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
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

        public void ClientRedemption_Reject(ClientRedemption _ClientRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientRedemption set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where ClientRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ClientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ClientRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ClientRedemption.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClientRedemption set status= 2,LastUpdate=@lastUpdate where ClientRedemptionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ClientRedemption.ClientRedemptionPK);
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

        public void ClientRedemption_Void(ClientRedemption _ClientRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientRedemption set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where ClientRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ClientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ClientRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ClientRedemption.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
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

        public List<ClientRedemption> ClientRedemption_SelectClientRedemptionDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientRedemption> L_ClientRedemption = new List<ClientRedemption>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when R.Status=1 then 'PENDING' else case when R.Status = 2 then 'APPROVED' else case when R.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,  
	                        case when R.EntryUsersID = 'BKLP' then Z7.Name + ' - ' + z5.AccountNo else case when BankRecipientPK=1 then Z1.Name  else case when BankRecipientPK= 2 then Z2.Name  else case when  BankRecipientPK= 3 then Z3.Name else Z4.Name end end end end BankRecipientDesc, 
	                        CU.ID CurrencyID ,F.ID FundID,F.Name FundName, FC.ID FundClientID,FC.Name FundClientName, CR.ID CashRefID,A.ID AgentID,A.Name AgentName,MV.DescOne TransferTypeDesc,isnull(MV1.DescOne,'')  TypeDesc,Case when FeeType = 1 then 'Percent' else 'Amount' End FeeTypeDesc , FC.IFUACode IFUACode,R.TransactionPK FrontID,R.* from ClientRedemption R left join  
	                        Fund F on R.FundPK = F.FundPK and F.Status =2 left join  
	                        FundClient FC on R.FundClientPK = FC.FundClientPK and FC.Status in (1,2)  left join  
	                        FundCashRef CR on R.CashRefPK = CR.FundCashRefPK and CR.Status =2 left join  
	                        Agent A on R.AgentPK = A.AgentPK and A.Status =2 left join  
	                        MasterValue MV1 on R.Type = MV1.Code and MV1.Status =2 and MV1.ID ='SubscriptionType' left join 
	                        Currency CU on R.CurrencyPK = CU.CurrencyPK and CU.Status =2  left join MasterValue MV on R.TransferType = MV.Code and MV.id = 'TransferTypeRedemption' left join
	                        FundClientBankList Z on R.BankRecipientPK = Z.NoBank and Z.Status =2 and R.FundClientPK =  Z.FundClientPK left join
	                        Bank Z1 on FC.namabank1 = Z1.BankPK and  FC.FundClientPK =  R.FundClientPK and Z1.status in (1,2) left join
	                        Bank Z2 on FC.namabank2 = Z2.BankPK and  FC.FundClientPK =  R.FundClientPK and Z2.status in (1,2) left join
	                        Bank Z3 on FC.namabank3 = Z3.BankPK and  FC.FundClientPK =  R.FundClientPK and Z3.status in (1,2) left join 
	                        Bank Z4 on Z.BankPK = Z4.BankPK and  FC.FundClientPK =  Z.FundClientPK and Z4.status in (1,2) left join
							ZRDO_80_BANK Z5 on R.BankRecipientPK = Z5.BankID left join
							ZRDO_80_BANK_MAPPING Z6 on Z5.BankName = Z6.PartnerCode left join
							Bank Z7 on Z6.RadsoftCode = Z7.ID and Z7.Status in (1,2)
                            where R.ValueDate between @DateFrom and @DateTo and R.status = @Status ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when R.Status=1 then 'PENDING' else case when R.Status = 2 then 'APPROVED' else case when R.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,  
	                        case when R.EntryUsersID = 'BKLP' then Z7.Name + ' - ' + z5.AccountNo else case when BankRecipientPK=1 then Z1.Name  else case when BankRecipientPK= 2 then Z2.Name  else case when  BankRecipientPK= 3 then Z3.Name else Z4.Name end end end end BankRecipientDesc, 
	                        CU.ID CurrencyID ,F.ID FundID,F.Name FundName, FC.ID FundClientID,FC.Name FundClientName, CR.ID CashRefID,A.ID AgentID,A.Name AgentName,MV.DescOne TransferTypeDesc,isnull(MV1.DescOne,'')  TypeDesc,Case when FeeType = 1 then 'Percent' else 'Amount' End FeeTypeDesc , FC.IFUACode IFUACode,R.TransactionPK FrontID,R.* from ClientRedemption R left join  
	                        Fund F on R.FundPK = F.FundPK and F.Status =2 left join  
	                        FundClient FC on R.FundClientPK = FC.FundClientPK and FC.Status in (1,2)  left join  
	                        FundCashRef CR on R.CashRefPK = CR.FundCashRefPK and CR.Status =2 left join  
	                        Agent A on R.AgentPK = A.AgentPK and A.Status =2 left join  
	                        MasterValue MV1 on R.Type = MV1.Code and MV1.Status =2 and MV1.ID ='SubscriptionType' left join 
	                        Currency CU on R.CurrencyPK = CU.CurrencyPK and CU.Status =2  left join MasterValue MV on R.TransferType = MV.Code and MV.id = 'TransferTypeRedemption' left join
	                        FundClientBankList Z on R.BankRecipientPK = Z.NoBank and Z.Status =2 and R.FundClientPK =  Z.FundClientPK left join
	                        Bank Z1 on FC.namabank1 = Z1.BankPK and  FC.FundClientPK =  R.FundClientPK and Z1.status in (1,2) left join
	                        Bank Z2 on FC.namabank2 = Z2.BankPK and  FC.FundClientPK =  R.FundClientPK and Z2.status in (1,2) left join
	                        Bank Z3 on FC.namabank3 = Z3.BankPK and  FC.FundClientPK =  R.FundClientPK and Z3.status in (1,2) left join 
	                        Bank Z4 on Z.BankPK = Z4.BankPK and  FC.FundClientPK =  Z.FundClientPK and Z4.status in (1,2) left join
							ZRDO_80_BANK Z5 on R.BankRecipientPK = Z5.BankID left join
							ZRDO_80_BANK_MAPPING Z6 on Z5.BankName = Z6.PartnerCode left join
							Bank Z7 on Z6.RadsoftCode = Z7.ID and Z7.Status in (1,2)
                            where R.ValueDate between @DateFrom and @DateTo  ";
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ClientRedemption.Add(setClientRedemption(dr));
                                }
                            }
                            return L_ClientRedemption;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_Posting(ClientRedemption _clientRedemption)
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

                            @"Declare @RedemptionAcc int Declare @PayableSubsAcc int Declare @TotalCashAmount numeric(18,4) Declare @TotalFeeAmount numeric(18,4) Declare @PCashAmount numeric(18,4)  
                            Declare @ValueDate datetime Declare @PeriodPK int Declare @BankPK int Declare @BankCurrencyPK int Declare @FundClientID nvarchar(100)  
                            Declare @CashAmount numeric (18,4) Declare @UnitAmount numeric(22,4) 
                            // indasia dibuka
                            // RHB ditutup
                            //Declare @FundCashRefPK int Declare @FundJournalPK int
                            //Select @FundJournalPK = isnull(MAX(FundJournalPK) + 1,1) From FundJournal  
                            //Select @PCashAmount = CashAmount,@TotalCashAmount = TotalCashAMount,@TotalFeeAmount = AgentFeeAmount + RedemptionFeeAmount,@ValueDate =  PaymentDate, @FundCashRefPK = CashRefPK, @FundClientPK = @FundClientPK,  
                            //@FundClientID = B.ID From ClientRedemption A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2 where ClientRedemptionPK = @ClientRedemptionPK and A.Status = 2 and Posted = 0 
                            //Select @RedemptionAcc = Redemption,@PayableSubsAcc = payableRedemptionfee From Fundaccountingsetup where status = 2 
                            //select @BankPK = FundJournalAccountPK,@BankCurrencyPK = A.CurrencyPK from FundCashRef A  where A.FundCashRefPK = @FundCashRefPK and A.status = 2 
                            //Select @PeriodPK = PeriodPK From Period Where @ValueDate Between DateFrom and DateTo and Status = 2 
                            //INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
                            //Select	   @FundJournalPK, 1,2,'Posting From Redemption',@PeriodPK,@ValueDate,1,@ClientRedemptionPK,'Redemption', '','Redemption Client: ' + @FundClientID,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime 
                            //INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
                            //Select		@FundJournalPK,1,1,2,@BankPK,@BankCurrencyPK,@FundPK,@FundClientPK,'Redemption Client: ' + @FundClientID,'D',@PCashAmount,@PCashAmount,0,1,@PCashAmount,0,@PostedTime 
                            //INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
                            //IF @TotalFeeAmount > 0 BEGIN 
                            //INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
                            //Select	@FundJournalPK,3,1,2,@PayableSubsAcc,1,@FundPK,@FundClientPK,'Redemption Client: ' + @FundClientID,'C',@TotalFeeAmount,	0,@TotalFeeAmount,1,0,@TotalFeeAmount,@PostedTime  END  
                            
                            select @CashAmount = CashAmount, @UnitAmount = UnitAmount from ClientRedemption where ClientRedemptionPK = @ClientRedemptionPK and Status = 2  
                            select * from FundClientPosition  +
                            where Date = @Date and FundClientPK = @FundClientPK and FundPK = @FundPK  
                            if @@rowCount > 0  
                            begin  +
                            Update FundClientPosition set CashAmount = CashAmount - @CashAmount,  
                            UnitAmount = UnitAmount - @UnitAmount where Date = @Date and FundClientPK = @FundClientPK  
                            and FundPK = @FundPK  
                            end  
                            else  
                            begin  
                            Select * from FundClientPosition where Date <= @Date and year(date) = year(@Date)   
                            and FundClientPK = @FundClientPK and FundPK = @FundPK  
                            if @@RowCount > 0  
                            Begin  +
                            INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)   
                            Select @Date,@FundClientPK,@FundPK,CashAmount - @CashAmount,UnitAmount - @UnitAmount From FundClientPosition  
                            Where Date = (  
                            select MAX(Date) MaxDate from FundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK and Date <= @Date  
                            and year(date) = year(@Date)  
                            and FundClientPK = @FundClientPK and FundPK = @FundPK ) End else Begin  
                            INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)  
                            select @Date,@FundClientPK,@FundPK,@CashAmount,@UnitAmount  end end 

                            update clientRedemption  
                            set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime  
                            where ClientRedemptionPK = @ClientRedemptionPK and Status = 2  

                             
                            Declare @counterDate datetime 
                            set @counterDate = @Date 
                            while @counterDate < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK and year(date) = year(@Date))  
                            BEGIN  
                            set @counterDate = dateadd(\day\,1,@counterDate) \n  +
                            update fundClientPosition set UnitAmount = UnitAmount - @UnitAmount,CashAmount = CashAmount - @CashAmount  
                            where FundClientPK = @FundClientPK and FundPK = @FundPK and Date = @counterDate end  ";

                        cmd.Parameters.AddWithValue("@Date", _clientRedemption.ValueDate);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                        //cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.TotalCashAmount);
                        //cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.TotalUnitAmount);
                        cmd.Parameters.AddWithValue("@PostedBy", _clientRedemption.PostedBy);
                        cmd.Parameters.AddWithValue("@PostedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientRedemptionPK", _clientRedemption.ClientRedemptionPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_Revise(ClientRedemption _clientRedemption)
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
                                    update FundJournal set Status = 3,VoidTime = @RevisedTime,VoidUsersID = @RevisedBy  where Type = 3 and TrxNo = @ClientRedemptionPK 
                                    and Posted = 1 

                                    Declare @NAVDate date

                                    select @NAVDate = NAVDate from ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                    if exists(select * from FundClientPosition 
                                    where Date = @Date and FundClientPK = @FundClientPK and FundPK = @FundPK 
                                    )
                                    begin 
                                        Update FundClientPosition set CashAmount = CashAmount + @CashAmount, 
                                        UnitAmount = UnitAmount + @UnitAmount where Date = @NAVDate and FundClientPK = @FundClientPK 
                                        and FundPK = @FundPK 
                                    end 
                         
                                     Declare @MaxClientRedemptionPK int 
                       
                                     Select @MaxClientRedemptionPK = ISNULL(MAX(ClientRedemptionPK),0) + 1 From ClientRedemption   
                                     INSERT INTO [dbo].[ClientRedemption]  
                                     ([ClientRedemptionPK],[HistoryPK] ,[Status],[Notes], [NAVDate] ,[ValueDate],
                                     [PaymentDate],[BitRedemptionAll], [NAV] ,[FundPK], [FundClientPK] , [CashRefPK] ,[Description] ,
                                     [CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,[RedemptionFeePercent] ,[RedemptionFeeAmount] ,
                                     [AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK],
                                     [DepartmentPK],[Type],[BankRecipientPK],[TransferType],[FeeType],
                                     [EntryUsersID],[EntryTime],[LastUpdate],[TransactionPK],[IsFrontSync])
                       
                                     SELECT @MaxClientRedemptionPK,1,1,'Pending Revised' ,[NAVDate] ,
                                     [ValueDate],[PaymentDate],[BitRedemptionAll],[NAV] ,[FundPK],[FundClientPK] ,
                                     [CashRefPK] ,[Description] ,[CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,
                                     [RedemptionFeePercent] ,[RedemptionFeeAmount] ,[AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK],
                                     [DepartmentPK],[Type],[BankRecipientPK],[TransferType],[FeeType],
                                     [EntryUsersID],[EntryTime] , @RevisedTime,[TransactionPK],0
                                     FROM ClientRedemption   
                                     WHERE ClientRedemptionPK = @ClientRedemptionPK   and status = 2 and posted = 1  
                        
                        
                                update ClientRedemption 
                                set RevisedBy = @RevisedBy,RevisedTime = @RevisedTime,Revised = 1, status = 3 , IsFrontSync = 0
                                where ClientRedemptionPK = @ClientRedemptionPK and Status = 2 and posted = 1 
                        
                                Declare @counterDate datetime 
                                set @counterDate = @NAVDate
                                while @counterDate < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK )
                                BEGIN 
                                set @counterDate = dbo.fworkingday(@counterDate,1) 
                                update fundClientPosition set UnitAmount = UnitAmount + @UnitAmount,CashAmount = CashAmount + @CashAmount
                                where FundClientPK = @FundClientPK and FundPK = @FundPK and Date = @counterDate end 

                                  Declare @LastNAV numeric(22,8)
		                           Declare @RedemptUnit numeric(22,8)


                                    Declare @OldRedemptUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(200)
                                    Declare @TransactionPK nvarchar(200)
                                    Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@oldNAV = NAV,@TransactionPK = TransactionPK,@TrxFrom = EntryUsersID
                                    From ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                    and ClientTransactionPK = @PK and TransactionType = 2 and ID =
                                    (
                                        Select max(ID) from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                        and ClientTransactionPK = @PK and TransactionType = 2
                                    )

                                    Set @OldCashAmount = 0


		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
		                           ) and status = 2 and FundPK = @OldFundPK

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @OldRedemptUnit = 0
		                           IF @OldCashAmount > 0 and @oldNAV = 0
		                           BEGIN
				                        set @OldRedemptUnit = @OldCashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @OldRedemptUnit = @OldUnitAmount
		                           END

                                    --Declare @UnitPrevious numeric(22,8)
--                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
--                                    set @UnitPrevious = isnull(@UnitPrevious,0)

--                                    --Buy = 1,
--                                    --Sell = 2,
--                                    --Adjustment = 3,
--                                    --SwitchingIn = 5,
--                                    --SwitchingOut = 6

--                                    insert into [FundClientPositionLog]
--                                               ([UserId]
--                                               ,[FundClientPK]
--                                               ,[TransactionPK]
--                                               ,[ClientTransactionPK]
--                                               ,[UnitPrevious]
--                                               ,[UnitChanges]
--                                               ,[UnitAfter]
--                                               ,[IsBoTransaction]
--                                               ,[TransactionType]
--                                               ,[Description]
--                                                ,[FundPK])
--                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldRedemptUnit,@UnitPrevious + @OldRedemptUnit
--                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Revise Redemption Old Data Revise',@OldFundPK

--                                    insert into [FundClientPositionLog]
--                                               ([UserId]
--                                               ,[FundClientPK]
--                                               ,[TransactionPK]
--                                               ,[ClientTransactionPK]
--                                               ,[UnitPrevious]
--                                               ,[UnitChanges]
--                                               ,[UnitAfter]
--                                               ,[IsBoTransaction]
--                                               ,[TransactionType]
--                                               ,[Description]
--                                                ,[FundPK])
--                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@MaxClientRedemptionPK,@UnitPrevious + @OldRedemptUnit,@OldRedemptUnit * -1,@UnitPrevious + @OldRedemptUnit + (@OldRedemptUnit *-1)
--                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Revise Redemption',@OldFundPK



";

                        cmd.Parameters.AddWithValue("@Date", _clientRedemption.ValueDate);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.TotalCashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                        cmd.Parameters.AddWithValue("@RevisedBy", _clientRedemption.RevisedBy);
                        cmd.Parameters.AddWithValue("@RevisedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientRedemptionPK", _clientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.RevisedBy);
                        cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                        cmd.ExecuteNonQuery();
                    }


                }
            }



            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }


                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'ClientRedemption',ClientRedemptionPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramClientRedemptionSelected + @"

                                 update ClientRedemption set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                                 where status = 1 and ClientRedemptionPK in ( Select ClientRedemptionPK from ClientRedemption where ValueDate between @DateFrom and @DateTo 
                                 and Status = 1 " + paramClientRedemptionSelected + @" ) 

                                 Update ClientRedemption set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                                 where status = 4 and ClientRedemptionPK in (Select ClientRedemptionPK from ClientRedemption where ValueDate between @DateFrom and @DateTo 
                                 and Status = 4 " + paramClientRedemptionSelected + @" ) ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_UnApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'ClientRedemption',ClientRedemptionPK,1,'UnApprove by Selected Data',@UsersID,@IPAddress,@Time  from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0  " + paramClientRedemptionSelected + @"
                                 update ClientRedemption set status = 1,UpdateUsersID = @UsersID,UpdateTime = @Time,LastUpdate=@Time, Notes = 'Unapproved by selected' where ValueDate between @DateFrom and @DateTo  and status = 2 " + paramClientRedemptionSelected;

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'ClientRedemption',ClientRedemptionPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramClientRedemptionSelected + @"
                                         

                            Declare @PK int
                            Declare @HistoryPK int

                            Declare A Cursor For
	                            Select ClientRedemptionPK,historyPK from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1  " + paramClientRedemptionSelected + @"
                            Open A
                            Fetch next From A
                            into @PK,@HistoryPK
                            While @@Fetch_status = 0
                            Begin

				                             Declare @LastNAV numeric(22,8)
		                                    Declare @RedemptUnit numeric(22,8)


                                            Declare @OldRedemptUnit numeric(22,8)
                                            Declare @OldUnitAmount numeric(22,8)
                                            Declare @OldNAVDate datetime
                                            Declare @OldFundPK int
                                            Declare @OldFundClientPK int
                                            Declare @OldCashAmount numeric(24,4)
                                            Declare @TrxFrom nvarchar(200)
                                            Declare @TransactionPK nvarchar(200)
                                            Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate, @TrxFrom = EntryUsersID,@TransactionPK = TransactionPK
                                            From ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                            Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                           and ClientTransactionPK = @PK and TransactionType = 2 and ID =
                                            (
                                                Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                                 and ClientTransactionPK = @PK and TransactionType = 2
                                            )

                                            --Set @OldCashAmount = 0
	--			                            set @OldRedemptUnit = @OldUnitAmount

                                            --Declare @UnitPrevious numeric(22,8)
--                                            Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                            where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

--                                            update fundclientpositionsummary
--		                                    set Unit = Unit + @OldRedemptUnit
--		                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
--                                            --Buy = 1,
--                                            --Sell = 2,
--                                            --Adjustment = 3,
--                                            --SwitchingIn = 5,
--                                            --SwitchingOut = 6

--                                            insert into [FundClientPositionLog]
--                                                        ([UserId]
--                                                        ,[FundClientPK]
--                                                        ,[TransactionPK]
--                                                        ,[ClientTransactionPK]
--                                                        ,[UnitPrevious]
--                                                        ,[UnitChanges]
--                                                        ,[UnitAfter]
--                                                        ,[IsBoTransaction]
--                                                        ,[TransactionType]
--                                                        ,[Description]
--                                                        ,[FundPK])
--                                            Select @UsersID,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldRedemptUnit,@UnitPrevious + @OldRedemptUnit
--                                            ,Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Reject Redemption',@OldFundPK


                            fetch next From A into @PK,@HistoryPK
                            end
                            Close A
                            Deallocate A



                                          update ClientRedemption set status = 3,selected= 0 , VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 1 and ClientRedemptionPK in ( Select ClientRedemptionPK from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1  " + paramClientRedemptionSelected + @" ) 
                                          Update ClientRedemption set status= 2  where status = 4 and ClientRedemptionPK in (Select ClientRedemptionPK from  ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 4  " + paramClientRedemptionSelected + @") 





                        ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _parambitManageUR = "";

                        if (_bitManageUR == true)
                        {
                            _parambitManageUR = "Where A.status = 2   and A.Posted = 0 and A.ClientRedemptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 2 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = "Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @DateTo " + paramClientRedemptionSelected;
                        }



                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
       
    Declare @CClientRedemptionPK int
    Declare @CNAVDate datetime
    Declare @CPaymentDate datetime
    Declare @CTotalUnitAmount numeric(18,8)
    Declare @CUnitAmount numeric(18,8)
    Declare @CTotalCashAmount numeric(22,4)
    Declare @CCashAmount numeric(18,4)
    Declare @CAgentFeeAmount numeric(18,4)
    Declare @CRedemptionFeeAmount numeric(18,4)
    Declare @CFundClientPK int
    Declare @CFundClientID nvarchar(100) 
    Declare @CFundClientName nvarchar(300) 
    Declare @CFundCashRefPK	int
    Declare @CFundPK int
    Declare @CHistoryPK int
    Declare @TotalFeeAmount numeric(18,4)
    Declare @TempAmount numeric(18,4)
    
    Declare @RedemptionAcc int Declare @PayableRedemptionAcc int Declare @PayableRedemptionFee int
    Declare @PeriodPK int Declare @BankPK int Declare @BankCurrencyPK int 
    Declare @FundJournalPK int 



    DECLARE A CURSOR FOR 
    Select FundPK,ClientRedemptionPK,NAVDate,PaymentDate,isnull(sum(TotalUnitAmount),0)
	,isnull(sum(TotalCashAmount),0),isnull(sum(UnitAmount),0),isnull(sum(CashAmount),0),
    isnull(sum(AgentFeeAmount),0),isnull(sum(RedemptionFeeAmount),0),
    A.FundClientPK,B.ID,B.Name,CashRefPK,A.HistoryPK
	 
    From ClientRedemption A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
    " + _parambitManageUR + @"

	Group by FundPK,ClientRedemptionPK,NAVDate,PaymentDate,A.FundClientPK,B.ID,B.Name,CashRefPK,A.HistoryPK
	
    Open A
    Fetch Next From A
    Into @CFundPK,@CClientRedemptionPK,@CNAVDate,@CPaymentDate,@CTotalUnitAmount,@CTotalCashAmount,@CUnitAmount,@CCashAmount,@CAgentFeeAmount,@CRedemptionFeeAmount,
    @CFundClientPK,@CFundClientID,@CFundClientName,@CFundCashRefPK,@CHistoryPK

    While @@FETCH_STATUS = 0
    Begin

   Select @RedemptionAcc = Redemption,@PayableRedemptionAcc = PendingRedemption
,@PayableRedemptionFee = payableRedemptionfee    
 From Fundaccountingsetup 
    where status = 2  and FundPK = @CFundPK



    -- LOGIC INSERT KE JOURNAL
    If @CTotalCashAmount > 0
    Begin
		                                    
    Select @TotalFeeAmount =  @CRedemptionFeeAmount
    set @TotalFeeAmount = isnull(@TotalFeeAmount,0)
    select @BankPK = FundJournalAccountPK,@BankCurrencyPK = A.CurrencyPK from FundCashRef A  where A.FundCashRefPK = @CFundCashRefPK  and A.status = 2       
    Select @PeriodPK = PeriodPK From Period Where @DateFrom Between DateFrom and DateTo and Status = 2      
	
    set @BankPK = isnull(@BankPK,3)
    set @BankCurrencyPK = isnull(@BankCurrencyPK,1)                                         
    -- TSETTLED
                             

    select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Redemption Amount',@PeriodPK,dbo.fworkingday(@CNAVDate,1),3,@CClientRedemptionPK,'REDEMPTION', '','Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
	 
	 
	    INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@RedemptionAcc,1,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'D',@CCashAmount,@CCashAmount,0,1,@CCashAmount,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@PayableRedemptionAcc,1,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@CTotalCashAmount,0,@CTotalCashAmount,1,0,@CTotalCashAmount,@PostedTime 

    if @TotalFeeAmount > 0
    begin

        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,3,1,2,@PayableRedemptionFee,1,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@TotalFeeAmount,0,@TotalFeeAmount,1,0,@TotalFeeAmount,@PostedTime 
    end


    select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

    INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
    Select	   @FundJournalPK, 1,2,'Posting From Redemption',@PeriodPK,@CPaymentDate,3,@CClientRedemptionPK,'REDEMPTION', '','Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
    
	INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])    
    Select	   @FundJournalPK,1,1,2,@PayableRedemptionAcc,1,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'D',@CTotalCashAmount,@CTotalCashAmount,0,1,@CTotalCashAmount,0,@PostedTime 
 
    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
    Select	   @FundJournalPK,2,1,2,@BankPK,@BankCurrencyPK,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@CTotalCashAmount,0,@CTotalCashAmount,1,0,@CTotalCashAmount,@PostedTime 
			 
    End
	
	-- LOGIC FUND CLIENT POSITION
	if Exists(select top 1 * from FundClientPosition    
	where Date = @CNavDate and FundClientPK = @CFundClientPK and FundPK = @CFundPK)    
	begin    
		Update FundClientPosition set CashAmount = CashAmount  - @CCashAmount,    
		UnitAmount = UnitAmount - @CUnitAmount where Date = @CNavDate and FundClientPK = @CFundClientPK    
		and FundPK = @CFundPK    
	end    
	else    
	begin    
		if Exists(Select top 1 * from FundClientPosition where Date <= @CNavDate and year(date) = year(@DateFrom)     
		and FundClientPK = @CFundClientPK and FundPK = @CFundPK)    
 
		Begin    
			INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)     
			Select @CNavDate,@CFundClientPK,@CFundPK,CashAmount - @CCashAmount,UnitAmount - @CUnitAmount From FundClientPosition    
			Where Date = (    
				select MAX(Date) MaxDate from FundClientPosition where FundClientPK = @CFundClientPK and FundPK = @CFundPK and Date <= @CNavDate    
				and year(date) = year(@CNavDate)    
				)  and FundPK = @CFundPK and FundClientPK = @CFundClientPK
		End 
		else 
		Begin    
			INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)    
			select @CNavDate,@CFundClientPK,@CFundPK,@CCashAmount,@CTotalUnitAmount  
		end 
	end      

	update clientRedemption    
	set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime    
	where ClientRedemptionPK = @CClientRedemptionPK and Status = 2    

	   
	Declare @counterDate datetime      
	set @counterDate = @CNavDate      

    while @counterDate < (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails)    
	BEGIN    
	set @counterDate = dbo.fworkingday(@counterdate,1)      
	update fundClientPosition set UnitAmount = UnitAmount  - @CUnitAmount,CashAmount = CashAmount - @CCashAmount    
	where FundClientPK = @CFundClientPK and FundPK = @CFundPK and Date = @counterDate end    
	   
	
Fetch next From A 
Into @CFundPK,@CClientRedemptionPK,@CNAVDate,@CPaymentDate,@CTotalUnitAmount,
	    @CTotalCashAmount,@CUnitAmount,@CCashAmount,@CAgentFeeAmount,@CRedemptionFeeAmount,
	    @CFundClientPK,@CFundClientID,@CFundClientName,@CFundCashRefPK,@CHistoryPK
end
Close A
Deallocate A



                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@PostedBy", _usersID);
                        cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        
        public void ClientRedemption_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }


                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                 Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'ClientRedemption',ClientRedemptionPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 2  and Posted = 0 " + paramClientRedemptionSelected + @" 
                                 update ClientRedemption set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 2 and ClientRedemptionPK in ( Select ClientRedemptionPK from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 2  " + paramClientRedemptionSelected + @"  and Posted = 0) ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Validate_ApproveBySelected(DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        
                        cmd.CommandText = @" if Exists(select * From ClientRedemption where Status = 2 and Type <> 3 and ValueDate between @ValueDateFrom and @ValueDateTo " + paramClientRedemptionSelected +  @"  and Posted = 0 and Revised = 0 and isnull(Nav,0) = 0 ) 
                         BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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
        
        public bool Validate_UnApproveBySelected(DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" if Exists(select * From ClientRedemption where (Status = 2 and Posted = 1 and Revised = 0) and ValueDate between @ValueDateFrom and @ValueDateTo " + paramClientRedemptionSelected + @") 
                         BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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
        
        public bool Validate_VoidBySelected(DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" if Exists(select * From ClientRedemption where (Status = 2 and Posted = 1 and Revised = 0) and ValueDate between @ValueDateFrom and @ValueDateTo " + paramClientRedemptionSelected +  @") 
                         BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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
        
        public int Validate_PostingBySelected(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" Declare @NAVDate datetime
                                            Declare A Cursor For              
                                             Select distinct NAVDate From ClientRedemption Where status = 2 and Valuedate = @ValueDateFrom
                                            Open  A              
              
                                            Fetch Next From  A              
                                            into @NAVDate
                  
                                            While @@Fetch_Status = 0              
                                            BEGIN              
		                                            if not exists(Select * from EndDayTrails where status = 2 and Notes = 'Unit'  and Valuedate = dbo.FWorkingDay(@NAVDate,-1))
		                                            begin
			                                            select 1 result
		                                            end
			                                        else if (dbo.CheckTodayIsHoliday(@NAVDate) = 1)
		                                            begin
			                                            select 2 result
		                                            end
		                                            else
		                                            begin
			                                            select 0 result
		                                            end
                                            Fetch next From A                   
                                             Into @NAVDate
                                            end                  
                                            Close A                  
                                            Deallocate A ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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
        
        public int Validate_AddClientRedemption(int _fundPK, int _fundClientPK, decimal _cashAmount)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            select * from ClientRedemption where FundClientPK=@FundClientPK and FundPK=@FundPK and UnitAmount = @UnitAmount
                            and ValueDate between CAST(GETDATE() AS DATE) and CAST(GETDATE() AS DATE)
                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@UnitAmount", _cashAmount);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["ClientRedemptionPK"]);
                                }
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

        public bool Validate_DormantClientRedemption(int _fundClientPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            select  * from fundclient
                            where (isnull(dormantDate,'12/31/2099')  between '02/01/1900'and Getdate()) and BitIsSuspend = 0  and status = 2
                            and FundClientPK = @FundClientPK";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
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


        /* AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart ) */
        // Radsoft
        public string SInvestRedemptionRpt_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {

            try
            {


                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }


                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {


                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"
                        BEGIN  
                        SET NOCOUNT ON    
                        create table #Text(    
                        [ResultText] [nvarchar](1000)  NULL        
                        )   
                   
                        truncate table #Text    
                        insert into #Text     
                        select 'Transaction Date|Transaction Type|SA Code|Investor Fund Unit A/C No.|Fund Code|Amount (Nominal)|Amount (Unit)|Amount (All Units)|Fee (Nominal)|Fee (Unit)|Fee (%)|REDM Payment A/C Sequential Code|REDM Payment Bank BIC Code|REDM Payment Bank BI Member Code|REDM Payment A/C No.|Payment Date|Transfer Type|SA Reference No.' 
     
                        insert into #Text           
                        Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Type,''))))
                        + '|' + @CompanyID
                        + '|' + isnull(A.IFUACode,'')
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SInvestCode,''))))
                        + '|' + case when A.CashAmount = 0 then '' else cast(isnull(cast(A.CashAmount as decimal(22,2)),'')as nvarchar) end
                        + '|' + case when A.TotalUnitAmount = 0 then '' else cast(isnull(Round(A.TotalUnitAmount,4),'')as nvarchar) end
                        + '|' + case when A.BitRedemptionAll = 1 then 'Y' else '' end
                        + '|' + case when A.FeeType = 2 and A.FeeAmount > 0 then cast(isnull(cast(A.FeeAmount as decimal(22,2)),'')as nvarchar) else '' end
                        + '|' + 
                        + '|' + case when A.FeeType = 1 and A.FeePercent > 0 then cast(isnull(cast(A.FeePercent as decimal(10,2)),'')as nvarchar) else '' end
                        + '|' + 
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(case when A.BICode = 'INDOIDJAXXX' then A.BICode else A.SInvestID end,''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(case when A.BICode = 'INDOIDJAXXX' then '' else A.BICode end,''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.AccNo,''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), SettlementDate, 112) <> '19000101' then CONVERT(VARCHAR(10), SettlementDate, 112) else '' End),''))))          
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                        + '|' + RTRIM(LTRIM(isnull(A.Reference,'')))
                        from (      
                                    
                        Select CR.ValueDate,F.SInvestCode,CR.PaymentDate SettlementDate,CR.RedemptionFeePercent FeePercent,CR.RedemptionFeeAmount FeeAmount,'2' Type,
                        ROUND(CashAmount,2)CashAmount ,ROUND(TotalUnitAmount,4)TotalUnitAmount ,CR.BitRedemptionAll BitRedemptionAll , CR.FeeType FeeType, 

                        case when BankRecipientPK= 1 and B1.Country <> 'ID' then isnull(B1.SInvestID,'') else case when BankRecipientPK= 2 and B2.Country <> 'ID' then isnull(B2.SInvestID,'')  else
                        Case when BankRecipientPK= 3 and B3.Country <> 'ID' then isnull(B3.SInvestID,'') else case when   B4.Country <> 'ID' then isnull(B4.SInvestID,'')
                        else '' end end end end SInvestID,

                        case when BankRecipientPK= 1 and B1.Country = 'ID' then isnull(B1.BICode,'') else case when BankRecipientPK= 2 and B2.Country = 'ID' then isnull(B2.BICode,'')  else
                        Case when BankRecipientPK= 3 and B3.Country = 'ID' then isnull(B3.BICode,'') else case when  B4.Country = 'ID' then isnull(B4.BICode,'')
                        else '' end end end end BICode,

                        case when BankRecipientPK = 0 then  '' when BankRecipientPK=1 then FC.NomorRekening1 
                        else case when BankRecipientPK = 2 then FC.NomorRekening2  else 
                        case when BankRecipientPK = 3 then FC.NomorRekening3 else FCB.AccountNo end end end AccNo ,


                        TransferType TransferType
                        ,Case When ReferenceSInvest <> '' and ReferenceSInvest is not null then ReferenceSInvest else  cast(ClientRedemptionPK as nvarchar) end Reference,FC.IFUACode
                        from ClientRedemption CR


                        left join Fund F on CR.FundPK = F.fundPK and f.Status in (1,2)
                        left join FundClient FC on CR.FundClientPK = FC.FundClientPK and fc.Status in (1,2)      
                        left join Bank B1 ON FC.NamaBank1 = B1.BankPK  and B1.status in (1,2)   
                        left join Bank B2 ON FC.NamaBank2 = B2.BankPK  and B2.status in (1,2)    
                        left join Bank B3 ON FC.NamaBank3 = B3.BankPK  and B3.status in (1,2)   
                        Left Join FundClientBankList FCB on CR.BankRecipientPK = FCB.NoBank and FCB.status in (1,2)
                        and FCB.fundclientPK = Cr.FundClientPK
                        Left join Bank B4 on FCB.bankPK = B4.BankPK and B4.status in (1,2)
                        where ValueDate =  @ValueDate and Cr.status = 2 and CR.Description <> 'Redemption All Mature Fund' " + paramClientRedemptionSelected + @"
                        )A    
                        Group by A.ValueDate,A.SInvestCode,A.FeePercent,A.BICode,A.AccNo,A.SettlementDate,A.FeeAmount,A.Type,A.CashAmount,A.TotalUnitAmount,A.BitRedemptionAll,A.TransferType,A.Reference,A.IFUACode, A.FeeType, A.SInvestID
                        order by A.ValueDate Asc        
                        select * from #text  
                        END";

                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "SUBS_REDM_SWTC_Order_Upload_REDM_Order.txt";
                                FileInfo txtFile = new FileInfo(filePath);
                                if (txtFile.Exists)
                                {
                                    txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                }

                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                {
                                    while (dr.Read())
                                    {
                                        file.WriteLine(Convert.ToString(dr["ResultText"]));
                                    }
                                    return Tools.HtmlSinvestTextPath + "SUBS_REDM_SWTC_Order_Upload_REDM_Order.txt";
                                }

                            }
                            return null;
                        }

                    }
                }
            }

            catch (Exception err)
            {
                throw err;
            }

        }

        // RHB
        public Boolean ClientRedemptionUnitBatchFormBySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientRedemption _clientRedemption)
        {

            try
            {
                string paramClientRedemptionSelected = "";
                if (!_host.findString(_clientRedemption.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientRedemption.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _clientRedemption.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"   SELECT case when BankRecipientPK=1 then isnull(B1.Name,'') else case when BankRecipientPK= 2 then isnull(B2.Name,'')  else isnull(B3.Name,'') end  end BankName,  
                                   case when BankRecipientPK=1 then FC.NomorRekening1 else case when BankRecipientPK= 2 then FC.NomorRekening2  else FC.NomorRekening3 end  end RekNo, 
                                   case when BankRecipientPK=1 then FC.NamaNasabah1 else case when BankRecipientPK= 2 then FC.NamaNasabah2  
                                   else FC.NamaNasabah3 end  end AccountHolderName,CU.ID CurrencyID,'Redemption' Type,ClientRedemptionPK,
                                   BC.ContactPerson ContactPerson,isnull(BC.ID,'') BankCustodiID,BC.Fax1 FaxNo,ValueDate Date,NavDate NavDate, 
                                   F.Name FundName,FC.id ClientID, Fc.Name ClientName,CR.NAV NAVAmount,CashAmount,unitamount UnitAmount, 
                                   CR.RedemptionFeePercent FeePercent,CR.RedemptionFeeAmount FeeAmount, CR.PaymentDate SettlementDate,
                                   CR.Description Remark,CR.TotalCashAmount NetAmount,0 SrHolder from ClientRedemption CR   
                                   left join FundClient FC ON CR.fundclientpk = FC.fundclientpk  and FC.status = 2  
                                   left join Fund F ON CR.FundPK = F.FundPK  AND F.Status = 2 
                                   --left join FundCashRef FCR ON CR.CashRefPK = FCR.FundCashRefPK  and FCR.status = 2   
                                   left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2    
                                   left join Currency CU ON CU.CurrencyPK = CR.CurrencyPK  and CU.status = 2   
                                   left join Bank B1 ON FC.NamaBank1 = B1.BankPK  and B1.status = 2  
                                   left join Bank B2 ON FC.NamaBank2 = B2.BankPK  and B2.status = 2   
                                   left join Bank B3 ON FC.NamaBank3 = B3.BankPK  and B3.status = 2  
                                   WHERE  CR.NAVDate between @DateFrom and @DateTo   and CR.Status not in(3,4) and CR.Type <> 3 " + paramClientRedemptionSelected;


                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "BatchFormREDUnitInstructionBySelected_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormREDUnitInstructionBySelected_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "UnitRegistryReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form RED Unit Instruction");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ClientRedemptionPK = Convert.ToInt32(dr0["ClientRedemptionPK"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                        rSingle.Nav = Convert.ToDecimal(dr0["NAVAmount"]);
                                        rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                        rSingle.SrHolder = Convert.ToInt32(dr0["SrHolder"]);
                                        rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                        rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                        rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                        rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                        rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                        rSingle.BankCustodiID = Convert.ToString(dr0["BankCustodiID"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.RekNo = Convert.ToString(dr0["RekNo"]);
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rSingle.BankName = Convert.ToString(dr0["BankName"]);
                                        rSingle.AccountHolderName = Convert.ToString(dr0["AccountHolderName"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.FundName, r.Date, r.SettlementDate, r.NAVDate, r.ContactPerson, r.FaxNo, r.BankCustodiID } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;

                                    foreach (var rsHeader in QueryByFundID)
                                    {




                                        incRowExcel = incRowExcel + 2;

                                        int RowZ = incRowExcel;
                                        incRowExcel = incRowExcel + 4;
                                        worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 10].Value = "Date ";
                                        worksheet.Cells[incRowExcel, 11].Value = ": ";
                                        worksheet.Cells[incRowExcel, 12].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).Height = 100;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyAddress();
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.WrapText = true;

                                        worksheet.Cells[incRowExcel, 10].Value = "To ";
                                        worksheet.Cells[incRowExcel, 11].Value = ": ";
                                        worksheet.Cells[incRowExcel, 12].Value = rsHeader.Key.BankCustodiID;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Phone No";
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyPhone();
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 10].Value = "Attention ";
                                        worksheet.Cells[incRowExcel, 11].Value = ": ";
                                        worksheet.Cells[incRowExcel, 12].Value = rsHeader.Key.ContactPerson;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fax No";
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyFax();
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 10].Value = "Fax Number ";
                                        worksheet.Cells[incRowExcel, 11].Value = ": ";
                                        worksheet.Cells[incRowExcel, 12].Value = rsHeader.Key.FaxNo;


                                        incRowExcel++;


                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Please kindly receive our Redemption of unit holders as follow : ";
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":  ";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Trade Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "NAV Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.NAVDate;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        if (_clientRedemption.HideDate == false)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Payment Date ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.SettlementDate;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        }
                                        incRowExcel = incRowExcel + 2;


                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;


                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "Holder";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 2].Value = "ID";
                                        worksheet.Cells[RowG, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 2].Style.Font.Bold = true;


                                        worksheet.Cells[incRowExcel, 3].Value = "Name";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 5].Value = "Unit";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":F" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = "Fee";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells["G" + RowB + ":H" + RowB].Merge = true;
                                        worksheet.Cells["G" + RowB + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 7].Value = "%";
                                        worksheet.Cells[RowG, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                        worksheet.Cells[RowG, 8].Value = "Amount";
                                        worksheet.Cells[RowG, 8].Style.Font.Bold = true;
                                        worksheet.Cells[RowG, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 9].Value = "Benef Acc No";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Merge = true;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 11].Value = "";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 12].Value = "Remark";
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells["L" + RowB + ":L" + RowG].Merge = true;
                                        worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        incRowExcel++;

                                        // Row C = 4
                                        int RowC = incRowExcel;

                                        incRowExcel++;
                                        //area header

                                        int _no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            //Row D = 5
                                            int RowD = incRowExcel;
                                            int RowE = incRowExcel + 1;


                                            //ThickBox Border

                                            worksheet.Cells["A" + RowB + ":L" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":L" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.SrHolder;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientName;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.UnitBalance;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeeAmount;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 10].Value = "Bank Info : " + rsDetail.BankName + " A/C No. " + rsDetail.RekNo + " " + rsDetail.AccountHolderName;
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 10].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.Remark;
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 12].Style.WrapText = true;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }
                                        worksheet.Cells["A" + _endRowDetail + ":L" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 7;
                                        int _RowA = incRowExcel;
                                        int _RowB = incRowExcel + 1;

                                        if (_clientRedemption.Signature1 != 1)
                                        {

                                            worksheet.Cells[_RowA, 3].Value = _host.Get_SignatureName(_clientRedemption.Signature1);
                                            worksheet.Cells["C" + _RowA + ":D" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 3].Value = _host.Get_PositionSignature(_clientRedemption.Signature1);
                                            worksheet.Cells["C" + _RowB + ":D" + _RowB].Merge = true;
                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 3].Value = "";
                                            worksheet.Cells["C" + _RowA + ":D" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 3].Value = "";
                                            worksheet.Cells["C" + _RowB + ":D" + _RowB].Merge = true;
                                        }


                                        if (_clientRedemption.Signature2 != 1)
                                        {
                                            worksheet.Cells[_RowA, 5].Value = _host.Get_SignatureName(_clientRedemption.Signature2);
                                            worksheet.Cells["E" + _RowA + ":F" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 5].Value = _host.Get_PositionSignature(_clientRedemption.Signature2);
                                            worksheet.Cells["E" + _RowB + ":F" + _RowB].Merge = true;


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 5].Value = "";
                                            worksheet.Cells["E" + _RowA + ":F" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 5].Value = "";
                                            worksheet.Cells["E" + _RowB + ":F" + _RowB].Merge = true;
                                        }

                                        if (_clientRedemption.Signature3 != 1)
                                        {
                                            worksheet.Cells[_RowA, 7].Value = _host.Get_SignatureName(_clientRedemption.Signature3);
                                            worksheet.Cells["G" + _RowA + ":H" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 7].Value = _host.Get_PositionSignature(_clientRedemption.Signature3);
                                            worksheet.Cells["G" + _RowB + ":H" + _RowB].Merge = true;


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 7].Value = "";
                                            worksheet.Cells["G" + _RowA + ":H" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 7].Value = "";
                                            worksheet.Cells["G" + _RowB + ":H" + _RowB].Merge = true;
                                        }

                                        if (_clientRedemption.Signature4 != 1)
                                        {
                                            worksheet.Cells[_RowA, 9].Value = _host.Get_SignatureName(_clientRedemption.Signature4);
                                            worksheet.Cells["I" + _RowA + ":J" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 9].Value = _host.Get_PositionSignature(_clientRedemption.Signature4);
                                            worksheet.Cells["I" + _RowB + ":J" + _RowB].Merge = true;
                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 9].Value = "";
                                            worksheet.Cells["I" + _RowA + ":J" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 9].Value = "";
                                            worksheet.Cells["I" + _RowB + ":J" + _RowB].Merge = true;
                                        }

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }
                                    //incRowExcel++;
                                    int _lastRow = incRowExcel;

                                    string _rangeA = "A5:L" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 28;
                                    }

                                    worksheet.DeleteRow(_lastRow);

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 12];
                                    worksheet.Column(1).Width = 15;
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 2;
                                    worksheet.Column(4).Width = 65;
                                    worksheet.Column(5).Width = 30;
                                    worksheet.Column(6).Width = 40;
                                    worksheet.Column(7).Width = 35;
                                    worksheet.Column(8).Width = 35;
                                    worksheet.Column(9).Width = 2;
                                    worksheet.Column(10).Width = 65;
                                    worksheet.Column(11).Width = 2;
                                    worksheet.Column(12).Width = 50;



                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    //worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B REDEMPTION \n &28&B Batch Form";

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    package.Save();
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    return true;
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }

        }

        // RHB
        public ClientRedemptionRecalculate ClientRedemption_Recalculate(ParamClientRedemptionRecalculate _param)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
Declare @TypeCheck int
Select @TypeCheck = Type
From ClientRedemption where ClientRedemptionPK = @ClientRedemptionPK

if @TypeCheck = 3
BEGIN
    return
END	


	Declare @JournalRoundingMode int          
	Declare @NAVRoundingMode int 
	Declare @JournalDecimalPlaces int 
	Declare @NAVDecimalPlaces int
	Declare @UnitDecimalPlaces int
	Declare @Nav numeric(22,8)
	Declare @UnitRoundingMode int
	Declare @CashAmount numeric(22,4)
	Declare @UnitAmount numeric(18,8)
	Declare @RedemptionFeeAmount numeric(22,4)
	Declare @RedemptionFeePercent numeric(18,8)
	Declare @AgentFeeAmount numeric(22,4)
	Declare @AgentFeePercent numeric(18,8)

	Declare @TotalCashAmount numeric(22,4)
	Declare @TotalUnitAmount numeric(22,8)

	Select  @JournalRoundingMode = isnull(C.JournalRoundingMode,3),
	@JournalDecimalPlaces = ISNULL(C.JournalDecimalPlaces,4) 
	from  Fund A
	Left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2
	Left join Bank C on C.BankPK = B.BankPK and C.Status = 2 
	where B.Status = 2


	Select @NAVRoundingMode = NAVRoundingMode, @NAVDecimalPlaces = NAVDecimalPlaces,@UnitDecimalPlaces = UnitDecimalPlaces ,@UnitRoundingMode = UnitRoundingMode
	From Fund Where FundPK = @FundPK and Status = 2

	Select @NAV = Nav From CloseNAV Where Date = @NAVDate and Status = 2 and FundPK = @FundPK


	If @NAVRoundingMode = 1 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces) 
		IF @NAVDecimalPlaces = 0 BEGIN
			set @NAV = @NAV + 1
		END
		IF @NAVDecimalPlaces = 2 BEGIN
			set @NAV = @NAV + 0.01
		END
		IF @NAVDecimalPlaces = 4 BEGIN
			set @NAV = @NAV + 0.0001
		END
		IF @NAVDecimalPlaces = 6 BEGIN
			set @NAV = @NAV + 0.000001
		END
		IF @NAVDecimalPlaces = 8 BEGIN
			set @NAV = @NAV + 0.00000001
		END
	END
	If @NAVRoundingMode = 2 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces,1) END
	If @NAVRoundingMode = 3 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces) END

	if @paramCashAmount > 0
	BEGIN
		set @CashAmount = @paramCashAmount
		if isnull(@NAV,0) =  0
		BEGIN
			set @UnitAmount = 0
			set @TotalUnitAmount = 0
			set @RedemptionFeeAmount = @ParamRedemptionFeeAmount
			set @AgentFeeAmount = @paramAgentFeeAmount
			set @RedemptionFeePercent = @paramRedemptionFeePercent
			set @AgentFeePercent = @paramAgentFeePercent
SET @TotalCashAmount = @CashAmount
		END
		ELSE
		BEGIN
                if @ParamUnitAmount = 0
                BEGIN
					If @UnitRoundingMode = 1 BEGIN Set @UnitAmount = round(@CashAmount / @Nav,@UnitDecimalPlaces) 
					IF @UnitDecimalPlaces = 0 BEGIN
						set @UnitAmount = @UnitAmount + 1
					END
					IF @UnitDecimalPlaces = 2 BEGIN
						set @UnitAmount = @UnitAmount + 0.01
					END
					IF @UnitDecimalPlaces = 4 BEGIN
						set @UnitAmount = @UnitAmount + 0.0001
					END
					IF @UnitDecimalPlaces = 6 BEGIN
						set @UnitAmount = @UnitAmount + 0.000001
					END
					IF @UnitDecimalPlaces = 8 BEGIN
						set @UnitAmount = @UnitAmount + 0.00000001
					END
				END
				If @UnitRoundingMode = 2 BEGIN Set  @UnitAmount = ROUND(@CashAmount / @Nav,@UnitDecimalPlaces,1) END
				If @UnitRoundingMode = 3 BEGIN Set  @UnitAmount = ROUND(@CashAmount / @Nav,@UnitDecimalPlaces) END
                END

				If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@CashAmount,@JournalDecimalPlaces) 
				IF @JournalDecimalPlaces = 0 BEGIN set @TotalCashAmount = @TotalCashAmount + 1
				END
				IF @JournalDecimalPlaces = 2 BEGIN
					set @TotalCashAmount = @TotalCashAmount + 0.01
				END
				IF @JournalDecimalPlaces = 4 BEGIN
					set @TotalCashAmount = @TotalCashAmount + 0.0001
				END
				IF @JournalDecimalPlaces = 6 BEGIN
					set @TotalCashAmount = @TotalCashAmount + 0.000001
				END
				IF @JournalDecimalPlaces = 8 BEGIN
					set @TotalCashAmount = @TotalCashAmount + 0.00000001
				END
				END
				If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount,@JournalDecimalPlaces,1) END
				If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount,@JournalDecimalPlaces) END


				If @UnitRoundingMode = 1 BEGIN Set @TotalUnitAmount = round(@TotalCashAmount / @Nav,@UnitDecimalPlaces) 
				IF @UnitDecimalPlaces = 0 BEGIN set @TotalUnitAmount = @TotalUnitAmount + 1
			                        
				END
				IF @UnitDecimalPlaces = 2 BEGIN
					set @TotalUnitAmount = @TotalUnitAmount + 0.01
				END
				IF @UnitDecimalPlaces = 4 BEGIN
					set @TotalUnitAmount = @TotalUnitAmount + 0.0001
				END
				IF @UnitDecimalPlaces = 6 BEGIN
					set @TotalUnitAmount = @TotalUnitAmount + 0.000001
				END
				IF @UnitDecimalPlaces = 8 BEGIN
					set @TotalUnitAmount = @TotalUnitAmount + 0.00000001
				END
				END
				If @UnitRoundingMode = 2 BEGIN Set   @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces,1) END
				If @UnitRoundingMode = 3 BEGIN Set   @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces) END

			if @FeeType = 1 and @paramRedemptionFeePercent > 0
			BEGIN
					set @RedemptionFeePercent = @paramRedemptionFeePercent
					If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@CashAmount * (1 - @RedemptionFeePercent/100),@JournalDecimalPlaces) 
						IF @JournalDecimalPlaces = 0 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 1
						END
						IF @JournalDecimalPlaces = 2 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.01
						END
						IF @JournalDecimalPlaces = 4 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.0001
						END
						IF @JournalDecimalPlaces = 6 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.000001
						END
						IF @JournalDecimalPlaces = 8 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.00000001
						END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount * (1 - @RedemptionFeePercent/100),@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount * (1 - @RedemptionFeePercent/100),@JournalDecimalPlaces) END


					If @JournalRoundingMode = 1 BEGIN Set @RedemptionFeeAmount = round(@CashAmount - @TotalCashAmount,@JournalDecimalPlaces) 
						IF @JournalDecimalPlaces = 0 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 1
						END
						IF @JournalDecimalPlaces = 2 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.01
						END
						IF @JournalDecimalPlaces = 4 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.0001
						END
						IF @JournalDecimalPlaces = 6 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.000001
						END
						IF @JournalDecimalPlaces = 8 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.00000001
						END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @RedemptionFeeAmount = ROUND(@CashAmount - @TotalCashAmount,@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @RedemptionFeeAmount = ROUND(@CashAmount - @TotalCashAmount,@JournalDecimalPlaces) END
					
			END
			if @FeeType = 2 and @paramRedemptionFeeAmount > 0
			BEGIN
			    set @RedemptionFeeAmount = @paramRedemptionFeeAmount
				set @RedemptionFeePercent = (@RedemptionFeeAmount / @Cashamount) * 100
				set @TotalCashAmount = @Cashamount - @RedemptionFeeAmount
			END

			
			if @FeeType = 1 and @paramAgentFeePercent > 0
			BEGIN
					SET @AgentFeePercent = @paramAgentFeePercent
					If @JournalRoundingMode = 1 BEGIN Set @AgentFeeAmount = round(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces) 
					IF @JournalDecimalPlaces = 0 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 1
					END
					IF @JournalDecimalPlaces = 2 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.01
					END
					IF @JournalDecimalPlaces = 4 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.0001
					END
					IF @JournalDecimalPlaces = 6 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.000001
					END
					IF @JournalDecimalPlaces = 8 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.00000001
					END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces) END
			END

			if @FeeType = 2 and @paramAgentFeeAmount > 0
			BEGIN
				set @AgentFeeAmount = @paramAgentFeeAmount
				set @AgentFeePercent = @AgentFeeAmount / @RedemptionFeeAmount * 100
			END

		END
	END


if @paramUnitAmount > 0
	BEGIN
		set @UnitAmount = @paramUnitAmount
		if @NAV =  0
		BEGIN
			set @CashAmount = 0
			set @TotalCashAmount = 0
			set @RedemptionFeeAmount = @ParamRedemptionFeeAmount
			set @AgentFeeAmount = @paramAgentFeeAmount
			set @RedemptionFeePercent = @paramRedemptionFeePercent
			set @AgentFeePercent = @paramAgentFeePercent
		END
		ELSE
		BEGIN
			  If @JournalRoundingMode = 1 BEGIN 
				Set  @CashAmount = ROUND(@UnitAmount * @Nav,@JournalDecimalPlaces) 

				IF @JournalDecimalPlaces = 0 BEGIN
					set @CashAmount = @CashAmount + 1
				END
				IF @JournalDecimalPlaces = 2 BEGIN
					set @CashAmount = @CashAmount + 0.01
				END
				IF @JournalDecimalPlaces = 4 BEGIN
					set @CashAmount = @CashAmount + 0.0001
				END
				IF @JournalDecimalPlaces = 6 BEGIN
					set @CashAmount = @CashAmount + 0.000001
				END
				IF @JournalDecimalPlaces = 8 BEGIN
					set @CashAmount = @CashAmount + 0.00000001
				END
			END
			If @JournalRoundingMode = 2 BEGIN Set   @CashAmount = ROUND(@UnitAmount * @Nav,@JournalDecimalPlaces,1) END
			If @JournalRoundingMode = 3 BEGIN Set   @CashAmount = ROUND(@UnitAmount * @Nav,@JournalDecimalPlaces) END

			if @FeeType = 1 and @paramRedemptionFeePercent > 0
			BEGIN
					set @RedemptionFeePercent = @paramRedemptionFeePercent
					If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@CashAmount * (1 - @RedemptionFeePercent/100),@JournalDecimalPlaces) 
						IF @JournalDecimalPlaces = 0 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 1
						END
						IF @JournalDecimalPlaces = 2 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.01
						END
						IF @JournalDecimalPlaces = 4 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.0001
						END
						IF @JournalDecimalPlaces = 6 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.000001
						END
						IF @JournalDecimalPlaces = 8 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.00000001
						END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount * (1 - @RedemptionFeePercent/100),@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount * (1 - @RedemptionFeePercent/100),@JournalDecimalPlaces) END


					If @JournalRoundingMode = 1 BEGIN Set @RedemptionFeeAmount = round(@CashAmount - @TotalCashAmount,@JournalDecimalPlaces) 
						IF @JournalDecimalPlaces = 0 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 1
						END
						IF @JournalDecimalPlaces = 2 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.01
						END
						IF @JournalDecimalPlaces = 4 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.0001
						END
						IF @JournalDecimalPlaces = 6 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.000001
						END
						IF @JournalDecimalPlaces = 8 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.00000001
						END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @RedemptionFeeAmount = ROUND(@CashAmount - @TotalCashAmount,@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @RedemptionFeeAmount = ROUND(@CashAmount - @TotalCashAmount,@JournalDecimalPlaces) END
					
			END
			else if @FeeType = 2 and @paramRedemptionFeeAmount > 0
			BEGIN
				set @RedemptionFeeAmount = @paramRedemptionFeeAmount
				set @TotalCashAmount = @Cashamount - @RedemptionFeeAmount
				set @RedemptionFeePercent = (@RedemptionFeeAmount / @TotalCashAmount) * 100
			END
			else 
			BEGIN
				set @TotalCashAmount = @Cashamount
			END
			
			if @FeeType = 1 and @paramAgentFeePercent > 0
			BEGIN
					SET @AgentFeePercent = @paramAgentFeePercent
					If @JournalRoundingMode = 1 BEGIN Set @AgentFeeAmount = round(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces) 
					IF @JournalDecimalPlaces = 0 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 1
					END
					IF @JournalDecimalPlaces = 2 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.01
					END
					IF @JournalDecimalPlaces = 4 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.0001
					END
					IF @JournalDecimalPlaces = 6 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.000001
					END
					IF @JournalDecimalPlaces = 8 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.00000001
					END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces) END
			END

			if @FeeType = 2 and @paramAgentFeeAmount > 0
			BEGIN
				set @AgentFeeAmount = @paramAgentFeeAmount
				set @AgentFeePercent = @AgentFeeAmount / @RedemptionFeeAmount * 100
			END

		END
	END




	if @NAV > 0
	BEGIN
		If @UnitRoundingMode = 1 BEGIN Set @TotalUnitAmount = round(@TotalCashAmount / @Nav,@UnitDecimalPlaces) 
					IF @UnitDecimalPlaces = 0 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 1
					END
					IF @UnitDecimalPlaces = 2 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 0.01
					END
					IF @UnitDecimalPlaces = 4 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 0.0001
					END
					IF @UnitDecimalPlaces = 6 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 0.000001
					END
					IF @UnitDecimalPlaces = 8 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 0.00000001
					END
				END
		If @UnitRoundingMode = 2 BEGIN Set  @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces,1) END
		If @UnitRoundingMode = 3 BEGIN 	Set  @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces) END

	END
	ELSE
	BEGIN
		set @TotalUnitAmount = @UnitAmount
	END

	
--Update ClientRedemption set Nav=isnull(@Nav,0), CashAmount = isnull(@CashAmount,0), TotalCashAmount = isnull(@TotalCashAmount,0), TotalUnitAmount = isnull(@TotalUnitAmount,0), UnitAmount = isnull(@UnitAmount,0), UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
--where ClientRedemptionpk = @ClientRedemptionPK and status = 1


    Declare @IFUACode nvarchar(50)
    Declare @SACode nvarchar(50)
    Declare @Description nvarchar(100)

    select @IFUACode = isnull(B.IFUACode,''),@SACode = isnull(B.SACode,''),@Description = A.Description from ClientRedemption A
    left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
    where ClientRedemptionPK = @ClientRedemptionPK  and A.status = 1

    if (@ClientCode = '24' and @IFUACode = '' and @SACode <> '' and @Description = 'Transaction APERD Summary') -- APERD DAN CUSTOM BNI
    BEGIN
        select @Nav Nav,  CashAmount, TotalCashAmount, RedemptionFeeAmount,  RedemptionFeePercent,
        AgentFeeAmount,  AgentFeePercent,
        TotalUnitAmount, UnitAmount from  ClientRedemption 
        where ClientRedemptionPK = @ClientRedemptionPK and status = 1

    END
    ELSE
    BEGIN
        select @Nav Nav, @CashAmount CashAmount,@TotalCashAmount TotalCashAmount,@RedemptionFeeAmount RedemptionFeeAmount, @RedemptionFeePercent RedemptionFeePercent,
        @AgentFeeAmount AgentFeeAmount, @AgentFeePercent AgentFeePercent,
        @TotalUnitAmount TotalUnitAmount,@UnitAmount UnitAmount
    END


";

                        cmd.Parameters.AddWithValue("@ClientRedemptionPK", _param.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@FundPK", _param.FundPK);
                        cmd.Parameters.AddWithValue("@NavDate", _param.NavDate);
                        cmd.Parameters.AddWithValue("@paramRedemptionFeePercent", _param.RedemptionFeePercent);
                        cmd.Parameters.AddWithValue("@paramRedemptionFeeAmount", _param.RedemptionFeeAmount);
                        cmd.Parameters.AddWithValue("@paramAgentFeePercent", _param.AgentFeePercent);
                        cmd.Parameters.AddWithValue("@paramAgentFeeAmount", _param.AgentFeeAmount);
                        cmd.Parameters.AddWithValue("@paramUnitAmount", _param.UnitAmount);
                        cmd.Parameters.AddWithValue("@paramCashAmount", _param.CashAmount);
                        cmd.Parameters.AddWithValue("@FeeType", _param.FeeType);

                        cmd.Parameters.AddWithValue("@UpdateUsersID", _param.UpdateUsersID);

                        if (_param.LastUpdate == null)
                        {
                            cmd.Parameters.AddWithValue("@Time", DateTime.Now);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Time", _param.LastUpdate);
                        }
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                ClientRedemptionRecalculate _ds = new ClientRedemptionRecalculate();
                                _ds.Nav = dr["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Nav"]);
                                _ds.CashAmount = dr["CashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CashAmount"]);
                                _ds.UnitAmount = dr["UnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["UnitAmount"]);
                                _ds.RedemptionFeePercent = dr["RedemptionFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["RedemptionFeePercent"]);
                                _ds.AgentFeePercent = dr["AgentFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AgentFeePercent"]);
                                _ds.RedemptionFeeAmount = dr["RedemptionFeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["RedemptionFeeAmount"]);
                                _ds.AgentFeeAmount = dr["AgentFeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AgentFeeAmount"]);
                                _ds.TotalCashAmount = dr["TotalCashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["TotalCashAmount"]);
                                _ds.TotalUnitAmount = dr["TotalUnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["TotalUnitAmount"]);
                                return _ds;
                            }
                            return null;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        // RHB 
        public void ClientRedemption_GetNavBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                         
Declare @JournalRoundingMode int
Declare @NAVRoundingMode int 
Declare @JournalDecimalPlaces int 
Declare @NAVDecimalPlaces int
Declare @UnitDecimalPlaces int
Declare @Nav numeric(24,8)
Declare @NAVDate datetime
Declare @ClientRedemptionPK int
Declare @FundPK int
Declare @UnitRoundingMode int





                           
Declare @TotalCashAmount numeric(22,4)
Declare @CashAmount numeric(18,4)
Declare @AgentFeePercent numeric(18,8)
Declare @RedemptionFeePercent numeric(18,8)
Declare @AgentFeeAmount numeric(18,4)
Declare @RedemptionFeeAmount numeric(18,4)
Declare @UnitAmount numeric(22,8)
Declare @TotalUnitAmount numeric(22,8)
Declare @FeeType int 

Declare @IFUACode nvarchar(50)
Declare @SACode nvarchar(50)
Declare @Description nvarchar(50)

DECLARE A CURSOR FOR 
	Select ClientRedemptionPK From ClientRedemption 
	where ValueDate between @DateFrom and @DateTo
    and Status = 2 and Posted = 0 and Type <> 3   " + paramClientRedemptionSelected + @"
	--and ClientRedemptionPK = 8020
Open A
Fetch Next From A
Into @ClientRedemptionPK

While @@FETCH_STATUS = 0
Begin

Select @NAVDate = NAVDate,@FundPK = A.FundPK , @JournalRoundingMode = isnull(D.JournalRoundingMode,3),
@JournalDecimalPlaces = ISNULL(D.JournalDecimalPlaces,4), @AgentFeeAmount = A.AgentFeeAmount,
@AgentFeePercent = A.AgentFeePercent, @RedemptionFeeAmount = A.RedemptionFeeAmount , @RedemptionFeePercent = A.RedemptionFeePercent,
@UnitAmount = A.UnitAmount ,@CashAmount = A.CashAmount, @FeeType = FeeType, @IFUACode = isnull(E.IFUACode,''),@SACode = isnull(E.SACode,''),@Description = A.Description
from ClientRedemption A
Left join Fund B on A.FundPK = B.FundPK and B.Status = 2
Left join BankBranch C on B.BankBranchPK = C.BankBranchPK and B.status = 2
Left join Bank D on C.BankPK = D.BankPK and C.Status = 2 
Left join FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2) 
Where A.ClientRedemptionPK = @ClientRedemptionPK 



Select @NAVRoundingMode = NAVRoundingMode, @NAVDecimalPlaces = NAVDecimalPlaces,@UnitDecimalPlaces = UnitDecimalPlaces ,@UnitRoundingMode = UnitRoundingMode
From Fund Where FundPK = @FundPK and Status = 2
set @NAV = null
Select @NAV = Nav From CloseNAV Where Date = @NAVDate and Status = 2 and FundPK = @FundPK



	If @NAVRoundingMode = 1 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces) 
		IF @NAVDecimalPlaces = 0 BEGIN
			set @NAV = @NAV + 1
		END
		IF @NAVDecimalPlaces = 2 BEGIN
			set @NAV = @NAV + 0.01
		END
		IF @NAVDecimalPlaces = 4 BEGIN
			set @NAV = @NAV + 0.0001
		END
		IF @NAVDecimalPlaces = 6 BEGIN
			set @NAV = @NAV + 0.000001
		END
		IF @NAVDecimalPlaces = 8 BEGIN
			set @NAV = @NAV + 0.00000001
		END
	END
	If @NAVRoundingMode = 2 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces,1) END
	If @NAVRoundingMode = 3 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces) END

	


	if @CashAmount > 0 
	BEGIN
		set @CashAmount = @CashAmount
		if isnull(@NAV,0) =  0
		BEGIN
			set @UnitAmount = 0
			set @TotalUnitAmount = 0
			set @RedemptionFeeAmount = @RedemptionFeeAmount
			set @AgentFeeAmount = @AgentFeeAmount
			set @RedemptionFeePercent = @RedemptionFeePercent
			set @AgentFeePercent = @AgentFeePercent
		END
		ELSE
		BEGIN
                if @UnitAmount = 0  
                BEGIN
					If @UnitRoundingMode = 1 BEGIN Set @UnitAmount = round(@CashAmount / @Nav,@UnitDecimalPlaces) 
					IF @UnitDecimalPlaces = 0 BEGIN
						set @UnitAmount = @UnitAmount + 1
					END
					IF @UnitDecimalPlaces = 2 BEGIN
						set @UnitAmount = @UnitAmount + 0.01
					END
					IF @UnitDecimalPlaces = 4 BEGIN
						set @UnitAmount = @UnitAmount + 0.0001
					END
					IF @UnitDecimalPlaces = 6 BEGIN
						set @UnitAmount = @UnitAmount + 0.000001
					END
					IF @UnitDecimalPlaces = 8 BEGIN
						set @UnitAmount = @UnitAmount + 0.00000001
					END
				END
				If @UnitRoundingMode = 2 BEGIN Set  @UnitAmount = ROUND(@CashAmount / @Nav,@UnitDecimalPlaces,1) END
				If @UnitRoundingMode = 3 BEGIN Set  @UnitAmount = ROUND(@CashAmount / @Nav,@UnitDecimalPlaces) END
                END

				If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@CashAmount,@JournalDecimalPlaces) 
				IF @JournalDecimalPlaces = 0 BEGIN set @TotalCashAmount = @TotalCashAmount + 1
				END
				IF @JournalDecimalPlaces = 2 BEGIN
					set @TotalCashAmount = @TotalCashAmount + 0.01
				END
				IF @JournalDecimalPlaces = 4 BEGIN
					set @TotalCashAmount = @TotalCashAmount + 0.0001
				END
				IF @JournalDecimalPlaces = 6 BEGIN
					set @TotalCashAmount = @TotalCashAmount + 0.000001
				END
				IF @JournalDecimalPlaces = 8 BEGIN
					set @TotalCashAmount = @TotalCashAmount + 0.00000001
				END
				END
				If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount,@JournalDecimalPlaces,1) END
				If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount,@JournalDecimalPlaces) END



				If @UnitRoundingMode = 1 BEGIN Set @TotalUnitAmount = round(@TotalCashAmount / @Nav,@UnitDecimalPlaces) 
				IF @UnitDecimalPlaces = 0 BEGIN set @TotalUnitAmount = @TotalUnitAmount + 1
			                        
				END
				IF @UnitDecimalPlaces = 2 BEGIN
					set @TotalUnitAmount = @TotalUnitAmount + 0.01
				END
				IF @UnitDecimalPlaces = 4 BEGIN
					set @TotalUnitAmount = @TotalUnitAmount + 0.0001
				END
				IF @UnitDecimalPlaces = 6 BEGIN
					set @TotalUnitAmount = @TotalUnitAmount + 0.000001
				END
				IF @UnitDecimalPlaces = 8 BEGIN
					set @TotalUnitAmount = @TotalUnitAmount + 0.00000001
				END
				END
				If @UnitRoundingMode = 2 BEGIN Set   @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces,1) END
				If @UnitRoundingMode = 3 BEGIN Set   @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces) END

			if @FeeType = 1 and @RedemptionFeePercent > 0
			BEGIN

					set @RedemptionFeePercent = @RedemptionFeePercent
					If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@CashAmount * (1 - @RedemptionFeePercent/100),@JournalDecimalPlaces) 
						IF @JournalDecimalPlaces = 0 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 1
						END
						IF @JournalDecimalPlaces = 2 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.01
						END
						IF @JournalDecimalPlaces = 4 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.0001
						END
						IF @JournalDecimalPlaces = 6 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.000001
						END
						IF @JournalDecimalPlaces = 8 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.00000001
						END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount - (@CashAmount * @RedemptionFeePercent/100),@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount - (@CashAmount * @RedemptionFeePercent/100),@JournalDecimalPlaces) END


					If @JournalRoundingMode = 1 BEGIN Set @RedemptionFeeAmount = ROUND(@CashAmount * @RedemptionFeePercent/100,@JournalDecimalPlaces) 
						IF @JournalDecimalPlaces = 0 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 1
						END
						IF @JournalDecimalPlaces = 2 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.01
						END
						IF @JournalDecimalPlaces = 4 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.0001
						END
						IF @JournalDecimalPlaces = 6 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.000001
						END
						IF @JournalDecimalPlaces = 8 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.00000001
						END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @RedemptionFeeAmount = ROUND(@CashAmount * @RedemptionFeePercent/100,@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @RedemptionFeeAmount = ROUND(@CashAmount * @RedemptionFeePercent/100,@JournalDecimalPlaces) END
			
			END
			if @FeeType = 2 and @RedemptionFeeAmount > 0
			BEGIN
				set @RedemptionFeeAmount = @RedemptionFeeAmount
				set @TotalCashAmount = @Cashamount - @RedemptionFeeAmount
				set @RedemptionFeePercent = (@RedemptionFeeAmount / @Cashamount) * 100
			END

			
			if @FeeType = 1 and @AgentFeePercent > 0
			BEGIN
					SET @AgentFeePercent = @AgentFeePercent
					If @JournalRoundingMode = 1 BEGIN Set @AgentFeeAmount = round(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces) 
					IF @JournalDecimalPlaces = 0 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 1
					END
					IF @JournalDecimalPlaces = 2 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.01
					END
					IF @JournalDecimalPlaces = 4 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.0001
					END
					IF @JournalDecimalPlaces = 6 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.000001
					END
					IF @JournalDecimalPlaces = 8 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.00000001
					END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces) END
			END

			if @FeeType = 2 and @AgentFeeAmount > 0
			BEGIN
				set @AgentFeeAmount = @AgentFeeAmount
				set @AgentFeePercent = @AgentFeeAmount / @RedemptionFeeAmount * 100
			END

		END
	END


if @UnitAmount > 0
	BEGIN
		if @NAV =  0
		BEGIN
			set @CashAmount = 0
			set @TotalCashAmount = 0
			set @RedemptionFeeAmount = @RedemptionFeeAmount
			set @AgentFeeAmount = @AgentFeeAmount
			set @RedemptionFeePercent = @RedemptionFeePercent
			set @AgentFeePercent = @AgentFeePercent
		END
		ELSE
		BEGIN

			If @JournalRoundingMode = 1 BEGIN 
				Set  @CashAmount = ROUND(@UnitAmount * @Nav,@JournalDecimalPlaces) 
				
				IF @JournalDecimalPlaces = 0 BEGIN
					set @CashAmount = @CashAmount + 1
				END
				IF @JournalDecimalPlaces = 2 BEGIN
					set @CashAmount = @CashAmount + 0.01
				END
				IF @JournalDecimalPlaces = 4 BEGIN
					set @CashAmount = @CashAmount + 0.0001
				END
				IF @JournalDecimalPlaces = 6 BEGIN
					set @CashAmount = @CashAmount + 0.000001
				END
				IF @JournalDecimalPlaces = 8 BEGIN
					set @CashAmount = @CashAmount + 0.00000001
				END
			END
			If @JournalRoundingMode = 2 BEGIN Set   @CashAmount = ROUND(@UnitAmount * @Nav,@JournalDecimalPlaces,1) END
			If @JournalRoundingMode = 3 BEGIN Set   @CashAmount = ROUND(@UnitAmount * @Nav,@JournalDecimalPlaces) END

			if @FeeType = 1 and @RedemptionFeePercent > 0
			BEGIN
					set @RedemptionFeePercent = @RedemptionFeePercent
					If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = ROUND(@CashAmount - (@CashAmount * @RedemptionFeePercent/100),@JournalDecimalPlaces) 
						IF @JournalDecimalPlaces = 0 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 1
						END
						IF @JournalDecimalPlaces = 2 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.01
						END
						IF @JournalDecimalPlaces = 4 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.0001
						END
						IF @JournalDecimalPlaces = 6 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.000001
						END
						IF @JournalDecimalPlaces = 8 BEGIN
							set @TotalCashAmount = @TotalCashAmount + 0.00000001
						END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount - (@CashAmount * @RedemptionFeePercent/100),@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@CashAmount - (@CashAmount * @RedemptionFeePercent/100),@JournalDecimalPlaces) END


					If @JournalRoundingMode = 1 BEGIN Set @RedemptionFeeAmount = ROUND(@CashAmount * @RedemptionFeePercent/100,@JournalDecimalPlaces) 
						IF @JournalDecimalPlaces = 0 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 1
						END
						IF @JournalDecimalPlaces = 2 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.01
						END
						IF @JournalDecimalPlaces = 4 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.0001
						END
						IF @JournalDecimalPlaces = 6 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.000001
						END
						IF @JournalDecimalPlaces = 8 BEGIN
							set @RedemptionFeeAmount = @RedemptionFeeAmount + 0.00000001
						END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @RedemptionFeeAmount = ROUND(@CashAmount * @RedemptionFeePercent/100,@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @RedemptionFeeAmount = ROUND(@CashAmount * @RedemptionFeePercent/100,@JournalDecimalPlaces) END
					
			END
			else if @FeeType = 2 and @RedemptionFeeAmount > 0
			BEGIN
				set @RedemptionFeeAmount = @RedemptionFeeAmount
				set @TotalCashAmount = @Cashamount - @RedemptionFeeAmount
				set @RedemptionFeePercent = (@RedemptionFeeAmount / @Cashamount) * 100
			END
			else 
			BEGIN
				set @TotalCashAmount = @Cashamount
			END

			
			if @FeeType = 1 and @AgentFeePercent > 0
			BEGIN
					SET @AgentFeePercent = @AgentFeePercent
					If @JournalRoundingMode = 1 BEGIN Set @AgentFeeAmount = round(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces) 
					IF @JournalDecimalPlaces = 0 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 1
					END
					IF @JournalDecimalPlaces = 2 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.01
					END
					IF @JournalDecimalPlaces = 4 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.0001
					END
					IF @JournalDecimalPlaces = 6 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.000001
					END
					IF @JournalDecimalPlaces = 8 BEGIN
						set @AgentFeeAmount = @AgentFeeAmount + 0.00000001
					END
					END
					If @JournalRoundingMode = 2 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces,1) END
					If @JournalRoundingMode = 3 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @RedemptionFeeAmount,@JournalDecimalPlaces) END
			END

			if @FeeType = 2 and @AgentFeeAmount > 0
			BEGIN
				set @AgentFeeAmount = @AgentFeeAmount
				set @AgentFeePercent = @AgentFeeAmount / @RedemptionFeeAmount * 100
			END
		END
	END




	if @NAV > 0
	BEGIN
		If @UnitRoundingMode = 1 BEGIN Set @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces) 
					IF @UnitDecimalPlaces = 0 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 1
					END
					IF @UnitDecimalPlaces = 2 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 0.01
					END
					IF @UnitDecimalPlaces = 4 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 0.0001
					END
					IF @UnitDecimalPlaces = 6 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 0.000001
					END
					IF @UnitDecimalPlaces = 8 BEGIN
						set @TotalUnitAmount = @TotalUnitAmount + 0.00000001
					END
				END
		If @UnitRoundingMode = 2 BEGIN Set  @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces,1) END
		If @UnitRoundingMode = 3 BEGIN 	Set  @TotalUnitAmount = ROUND(@TotalCashAmount / @Nav,@UnitDecimalPlaces) END

	END
	ELSE
	BEGIN
		set @TotalUnitAmount = @UnitAmount
	END

	--select isnull(@RedemptionFeeAmount,0) RedemptionFeeAmount,isnull(@CashAmount,0) CashAmount,isnull(@TotalCashAmount,0) TotalCashAmount

    IF (@ClientCode = '24' and @IFUACode = '' and @SACode <> '' and @Description = 'Transaction APERD Summary') -- APERD DAN CUSTOM BNI
    BEGIN
        Update ClientRedemption set Nav=isnull(@Nav,0), UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
        where ClientRedemptionPK = @ClientRedemptionPK
    END
    ELSE
    BEGIN
        Update ClientRedemption set Nav=isnull(@Nav,0), CashAmount = isnull(@CashAmount,0), TotalCashAmount = isnull(@TotalCashAmount,0), RedemptionFeeAmount = isnull(@RedemptionFeeAmount,0), RedemptionFeePercent = isnull(@RedemptionFeePercent,0),
        AgentFeeAmount = isnull(@AgentFeeAmount,0), AgentFeePercent = isnull(@AgentFeePercent,0), TotalUnitAmount = isnull(@TotalUnitAmount,0), UnitAmount = isnull(@UnitAmount,0), UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
        where ClientRedemptionPK = @ClientRedemptionPK
    END

Fetch next From A Into @ClientRedemptionPK
end
Close A
Deallocate A
                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        // RHB
        public Boolean ClientRedemptionAmountBatchFormBySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientRedemption _clientRedemption)
        {
            try
            {
                string paramClientRedemptionSelected = "";
                if (!_host.findString(_clientRedemption.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientRedemption.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " and ClientRedemptionPK in (" + _clientRedemption.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " and ClientRedemptionPK in (0) ";
                }
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =

                        @"                  
                            SELECT F.Name NamaFund,CS.ReferenceSInvest Ref,CS.ValueDate date,BN.Name ToNamaBank,BC.Address ToAlamat, BC.ContactPerson ToAttention,BC.Fax1 FaxNumber,CS.ValueDate TradeDate,dbo.FWorkingDay(CS.NAVDate,1) SettlementDate,
			    CS.ClientRedemptionPK FormNo,FC.ID CustomerID,BN.ID CustodianID,FC.SID CustomerSIDNumber,FC.name,CS.CashAmount NominalAmount,CS.RedemptionFeePercent FeePercent,RedemptionFeeAmount FeeAmount,CS.NAV NAV,CS.TotalCashAmount NetAmount,CS.UnitAmount NoOfUnits,CS.CashAmount RedeemedNominalAmount,CS.UnitAmount RedeemedNoOfUnits,PaymentDate DateFundToBeTransferred from ClientRedemption CS   
                            left join FundClient FC ON CS.fundclientpk = FC.fundclientpk and FC.status = 2  
                            left join Fund F ON CS.FundPK = F.FundPK  and F.status = 2   
                            left join FundCashRef FCR ON CS.CashRefPK = FCR.FundCashRefPK  and FCR.status = 2   
                            left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                            left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                            left join Currency CU ON CU.CurrencyPK = CS.CurrencyPK  and CU.status = 2  
                            WHERE CS.NAVDate between @DateFrom and @DateTo 
			    and CS.Type <> 3 and CS.status not in(3,4)" + paramClientRedemptionSelected;

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "BatchFormREDAmountInstructionBySelected" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormREDAmountInstructionBySelected" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "Report";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Redemption Batch Form");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<ClientRedemptionBatchForm> rList = new List<ClientRedemptionBatchForm>();
                                    while (dr0.Read())
                                    {
                                        ClientRedemptionBatchForm rSingle = new ClientRedemptionBatchForm();
                                        rSingle.NamaFund = Convert.ToString(dr0["NamaFund"]);
                                        rSingle.Ref = Convert.ToString(dr0["Ref"]);
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.ToNamaBank = Convert.ToString(dr0["ToNamaBank"]);
                                        rSingle.ToAlamat = Convert.ToString(dr0["ToAlamat"]);
                                        rSingle.ToAttention = Convert.ToString(dr0["ToAttention"]);
                                        rSingle.FaxNumber = Convert.ToString(dr0["FaxNumber"]);
                                        rSingle.TradeDate = Convert.ToDateTime(dr0["TradeDate"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                        rSingle.FormNo = Convert.ToInt32(dr0["FormNo"]);
                                        rSingle.CustomerID = Convert.ToString(dr0["CustomerID"]);
                                        rSingle.CustomerSIDNumber = Convert.ToString(dr0["CustomerSIDNumber"]);
                                        rSingle.Name = Convert.ToString(dr0["Name"]);
                                        rSingle.RedeemedNominalAmount = Convert.ToDecimal(dr0["RedeemedNominalAmount"]);
                                        rSingle.RedeemedNoOfUnits = Convert.ToDecimal(dr0["RedeemedNoOfUnits"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                        rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rSingle.DateFundToBeTransferred = Convert.ToDateTime(dr0["DateFundToBeTransferred"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.Ref, r.NamaFund, r.Date, r.ToNamaBank, r.ToAlamat, r.ToAttention, r.FaxNumber, r.TradeDate, r.SettlementDate } into rGroup
                                        select rGroup;

                                    int incRowExcel = 0;


                                    foreach (var rsHeader in QueryByFundID)
                                    {


                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "REDEMPTION";
                                        worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "BATCH FORM";
                                        worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel = incRowExcel + 2;

                                        int RowZ = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.NamaFund;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 9].Value = "Ref";
                                        worksheet.Cells[incRowExcel, 10].Value = ":";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.Ref;
                                        incRowExcel++;
                                        //worksheet.Cells[incRowExcel, 1].Value = "Wisma Indosemen 3rd Floor Jl.Jen.Sudirman Kav. 70-71";
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyAddress();
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;

                                        worksheet.Cells[incRowExcel, 9].Value = "Date";
                                        worksheet.Cells[incRowExcel, 10].Value = ":";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "dd-MMM-yyyy";
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["M" + incRowExcel + ":N" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jakarta 12910";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;

                                        worksheet.Cells[incRowExcel, 9].Value = "To";
                                        worksheet.Cells[incRowExcel, 10].Value = ":";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ToNamaBank;
                                        incRowExcel++;
                                        RowZ = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ToAlamat;
                                        worksheet.Cells[incRowExcel, 11, incRowExcel, 13].Style.WrapText = true;
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                        worksheet.Cells["K" + incRowExcel + ":M" + RowZ].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Phone No. : " + "";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyPhone();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fax No. : " + "";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyFax();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 9].Value = "Attention";
                                        worksheet.Cells[incRowExcel, 10].Value = ":";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ToAttention;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 9].Value = "Fax Number";
                                        worksheet.Cells[incRowExcel, 10].Value = ":";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.FaxNumber;
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Please Kindly Receive Our Redemption Of Unit Holders As Follow:";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Trade Date:";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TradeDate;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Settlement Date:";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.SettlementDate;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                        incRowExcel = incRowExcel + 2;
                                        //incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;


                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "Form No.";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 3].Value = "Customer ID";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                        worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 4].Value = "Customer SID Number";
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                        worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 5].Value = "Name";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "Redeemed";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells["F" + RowB + ":G" + RowB].Merge = true;
                                        worksheet.Cells["F" + RowB + ":G" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["F" + RowB + ":G" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 6].Value = "Nominal Amount";
                                        worksheet.Cells[RowG, 6].Style.Font.Bold = true;
                                        worksheet.Cells["F" + RowG + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 7].Value = "No. Of Units";
                                        worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                        worksheet.Cells["G" + RowG + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 8].Value = "Fee";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells["H" + RowB + ":J" + RowB].Merge = true;
                                        worksheet.Cells["H" + RowB + ":J" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["H" + RowB + ":J" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 8].Value = "%";
                                        worksheet.Cells[RowG, 8].Style.Font.Bold = true;
                                        worksheet.Cells["H" + RowG + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 9].Value = "Amount";
                                        worksheet.Cells[RowG, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowG + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["I" + RowG + ":J" + RowG].Merge = true;

                                        worksheet.Cells[incRowExcel, 11].Value = "NAV";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 12].Value = "Net Amount";
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells["L" + RowB + ":L" + RowG].Merge = true;
                                        worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 13].Value = "Date Of Fund TO Be Transferred";
                                        worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                        worksheet.Cells["M" + RowB + ":M" + RowG].Merge = true;
                                        worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;





                                        incRowExcel++;

                                        // Row C = 4
                                        int RowC = incRowExcel;

                                        incRowExcel++;
                                        //area header

                                        int _no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            //Row D = 5
                                            int RowD = incRowExcel;
                                            int RowE = incRowExcel + 1;
                                            int RowH = RowB + 2;
                                            int RowI = RowB + 3;


                                            //ThickBox Border

                                            worksheet.Cells["A" + RowB + ":M" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":M" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":M" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":M" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.FormNo;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.CustomerID;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.CustomerSIDNumber;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Name;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.RedeemedNominalAmount;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.RedeemedNoOfUnits;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#.############";
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeeAmount;
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.NAV;
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#.########";
                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.NetAmount;
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail.DateFundToBeTransferred;
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }


                                        worksheet.Cells["A" + _endRowDetail + ":M" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 11].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                        worksheet.Cells[incRowExcel, 12].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#.############";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#.########";
                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;

                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 10].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 11].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 12].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 12].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 12].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        incRowExcel += 2;
                                        worksheet.Cells[incRowExcel, 10].Value = "Investment Manager Approval";
                                        worksheet.Cells["J" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        //worksheet.Cells[incRowExcel, 10].Value = "Date";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.TradeDate;
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "dd-MMM-yyyy";
                                        worksheet.Cells["J" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 10].Value = "Inputted";
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 10].Value = "Approved";
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel = incRowExcel + 6;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                        worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "Approved By";
                                        incRowExcel++;

                                        int _RowA = incRowExcel;
                                        int _RowB = incRowExcel + 7;
                                        if (_clientRedemption.Signature1 != 0)
                                        {
                                            worksheet.Cells["A" + _RowA + ":B" + _RowA].Merge = true;
                                            worksheet.Cells[_RowA, 1].Value = _host.Get_PositionSignature(_clientRedemption.Signature1);

                                            worksheet.Cells["A" + _RowB + ":B" + _RowB].Merge = true;
                                            worksheet.Cells[_RowB, 1].Value = "( " + _host.Get_SignatureName(_clientRedemption.Signature1) + " )";


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 1].Value = "";

                                            worksheet.Cells[_RowB, 1].Value = "";

                                        }


                                        if (_clientRedemption.Signature2 != 0)
                                        {
                                            worksheet.Cells["C" + _RowA + ":D" + _RowA].Merge = true;
                                            worksheet.Cells[_RowA, 3].Value = _host.Get_PositionSignature(_clientRedemption.Signature2);

                                            worksheet.Cells["C" + _RowB + ":D" + _RowB].Merge = true;
                                            worksheet.Cells[_RowB, 3].Value = "( " + _host.Get_SignatureName(_clientRedemption.Signature2) + " )";

                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 3].Value = "";

                                            worksheet.Cells[_RowB, 3].Value = "";

                                        }

                                        if (_clientRedemption.Signature3 != 0)
                                        {
                                            worksheet.Cells[_RowA, 5].Value = _host.Get_PositionSignature(_clientRedemption.Signature3);

                                            worksheet.Cells[_RowB, 5].Value = "( " + _host.Get_SignatureName(_clientRedemption.Signature3) + " )";

                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 5].Value = "";
                                            worksheet.Cells[_RowA, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 5].Value = "";
                                            worksheet.Cells[_RowB, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }

                                        if (_clientRedemption.Signature4 != 0)
                                        {
                                            worksheet.Cells["F" + _RowA + ":H" + _RowA].Merge = true;
                                            worksheet.Cells[_RowA, 6].Value = _host.Get_PositionSignature(_clientRedemption.Signature4);

                                            worksheet.Cells["F" + _RowB + ":H" + _RowB].Merge = true;
                                            worksheet.Cells[_RowB, 6].Value = "( " + _host.Get_SignatureName(_clientRedemption.Signature4) + " )";


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 6].Value = "";

                                            worksheet.Cells[_RowB, 6].Value = "";

                                        }


                                        incRowExcel = incRowExcel + 8;
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }
                                    //incRowExcel++;
                                    int _lastRow = incRowExcel;

                                    string _rangeA = "A5:M" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 12;
                                    }

                                    worksheet.DeleteRow(_lastRow);

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 13];
                                    worksheet.Column(1).Width = 16;
                                    worksheet.Column(2).Width = 14;
                                    worksheet.Column(3).Width = 14;
                                    worksheet.Column(4).Width = 22;
                                    worksheet.Column(5).Width = 45;
                                    worksheet.Column(6).Width = 21;
                                    worksheet.Column(7).Width = 21;
                                    worksheet.Column(8).Width = 7;
                                    worksheet.Column(9).Width = 14;
                                    worksheet.Column(10).Width = 2;
                                    worksheet.Column(11).Width = 21;
                                    worksheet.Column(12).Width = 21;
                                    worksheet.Column(13).Width = 32;


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B REDEMPTION \n &28&B Batch Form";

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    package.Save();
                                    if (_clientRedemption.DownloadMode == "PDF")
                                    {
                                        Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    }
                                    return true;
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }

        }


        // MANDIRI
        public Boolean ClientRedemptionAmountBatchFormBySelectedDataMandiri(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientRedemption _clientRedemption)
        {

            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_clientRedemption.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientRedemption.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _clientRedemption.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"SELECT isnull(BC.ContactPerson,'') ContactPerson, isnull(BC.Fax1,'') FaxNo, Fc.Name InvestorName, NavDate NAVDate, FC.IFUACode IFUA, unitamount UnitTransaction,
                        CU.ID CurrencyID, CR.RedemptionFeePercent FeePercent, CR.RedemptionFeeAmount FeeAmount, CashAmount GrossAmount, 
                        CR.TotalCashAmount NetAmount, CR.PaymentDate PaymentDate, CR.Description Remark,isnull(BN.Name,'') BankCustodianName, isnull(BC.Address,'') CustodyAddress,ValueDate Date, F.Name FundName,CR.Description Remark 
                        ,isnull(A.BankAccountName,'') AccountNameDebet, isnull(A.BankAccountNo,'') AccountNumberDebet, isnull(C.Name,'') BankDebet, 
                        case when isnull(BankRecipientPK,0) = 1 then isnull(FC.NamaNasabah1,'') when isnull(BankRecipientPK,0) = 2 then isnull(NamaNasabah2,'') 
                        when isnull(BankRecipientPK,0) = 3 then isnull(NamaNasabah3,'') 
                        when isnull(BankRecipientPK,0) > 3 then isnull(D.AccountName,'') 
                        end AccountNameKredit , case when BankRecipientPK = 1 then isnull(FC.NomorRekening1,'') when BankRecipientPK = 2 then isnull(FC.NomorRekening2,'')
                        when BankRecipientPK = 3 then isnull(FC.NomorRekening3,'')
                        when BankRecipientPK > 3 then isnull(E.Name,'')
                        end AccountNumberKredit,
                         case when BankRecipientPK = 1 then isnull(B1.Name,'') when BankRecipientPK = 2 then isnull(B2.Name,'')
                        when BankRecipientPK = 3 then isnull(B3.Name,'')
                        when BankRecipientPK > 3 then isnull(D.AccountNo,'') 
                        end  BankKredit from ClientRedemption CR   
                        left join FundClient FC ON CR.fundclientpk = FC.fundclientpk  and FC.status in (1,2)  
                        left join Fund F ON CR.FundPK = F.FundPK  AND F.Status in (1,2) 
                        left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status in (1,2)   
                        left join Bank BN on BC.BankPK = BN.BankPK and BN.status in (1,2)
                        left join Currency CU ON CU.CurrencyPK = CR.CurrencyPK  and CU.status in (1,2)
                        left join Bank B1 ON FC.NamaBank1 = B1.BankPK  and B1.status in (1,2)  
                        left join Bank B2 ON FC.NamaBank2 = B2.BankPK  and B2.status in (1,2) 
                        left join Bank B3 ON FC.NamaBank3 = B3.BankPK  and B3.status in (1,2)  
                        left join FundCashRef A on CR.CashRefPK =A.FundCashRefPK and A.status in (1,2)
                        Left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B. status in (1,2)
                        Left join Bank C on B.BankPK = C.BankPK and C.status in (1,2) 
                        left join fundclientBankList D on CR.BankRecipientPK = D.NoBank and CR.FundClientPK = D.FundClientPK and D.status in(1,2)
                        left join Bank E on D.BankPK = E.BankPK and E.status in (1,2)
                        WHERE  CR.NAVDate between @DateFrom and @DateTo 
                        " + paramClientRedemptionSelected + @"
                        and CR.Status not in(3,4) ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "BatchFormREDAmountInstructionBySelected_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormREDAmountInstructionBySelected_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "UnitRegistryReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form RED Unit Instruction");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                        rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                        rSingle.InvestorName = Convert.ToString(dr0["InvestorName"]);
                                        rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.IFUA = Convert.ToString(dr0["IFUA"]);
                                        rSingle.UnitTransaction = Convert.ToDecimal(dr0["UnitTransaction"]);
                                        rSingle.GrossAmount = Convert.ToDecimal(dr0["GrossAmount"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rSingle.PaymentDate = Convert.ToDateTime(dr0["PaymentDate"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rSingle.BankCustodiID = Convert.ToString(dr0["BankCustodianName"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.CustodyAddress = Convert.ToString(dr0["CustodyAddress"]);
                                        rSingle.AccountNameDebet = dr0["AccountNameDebet"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AccountNameDebet"]);
                                        rSingle.AccountNumberDebet = dr0["AccountNumberDebet"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AccountNumberDebet"]);
                                        rSingle.BankDebet = dr0["BankDebet"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankDebet"]);
                                        rSingle.AccountNameKredit = dr0["AccountNameKredit"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AccountNameKredit"]);
                                        rSingle.AccountNumberKredit = dr0["AccountNumberKredit"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AccountNumberKredit"]);
                                        rSingle.BankKredit = dr0["BankKredit"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankKredit"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.NAVDate, r.Date, r.FundName, r.ContactPerson, r.FaxNo, r.BankCustodiID, r.CustodyAddress, r.AccountNameDebet, r.AccountNumberDebet, r.BankDebet, r.AccountNameKredit, r.AccountNumberKredit, r.BankKredit } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;
                                    int _startRowDetail = 0;
                                    int _endRowDetail = 0;

                                    foreach (var rsHeader in QueryByFundID)
                                    {

                                        incRowExcel = incRowExcel + 2;

                                        int RowZ = incRowExcel;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.BankCustodiID;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyAddress();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                        worksheet.Row(incRowExcel).Height = 60;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.CustodyAddress;
                                        worksheet.Cells[incRowExcel, 11].Style.WrapText = true;
                                        worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                        incRowExcel = incRowExcel + 3;
                                        worksheet.Cells[incRowExcel, 1].Value = "Batch Form-Redemption";
                                        worksheet.Cells[incRowExcel, 11].Value = "Attn : " + rsHeader.Key.ContactPerson;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Value = "Fax No. : " + rsHeader.Key.FaxNo;
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 3;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date : " + Convert.ToDateTime(rsHeader.Key.Date).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 11].Value = "Print Date : " + Convert.ToDateTime(rsHeader.Key.Date).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Nav Date : " + Convert.ToDateTime(rsHeader.Key.NAVDate).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 11].Value = "Time : " + Convert.ToDateTime(_dateTimeNow).ToString("HH:mm") + " WIB";
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 3;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        _startRowDetail = incRowExcel;
                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "Investor Name";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        worksheet.Cells[incRowExcel, 3].Value = "IFUA";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                        worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        worksheet.Cells[incRowExcel, 4].Value = "Unit Transaction";
                                        worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                        worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 5].Value = "CCY";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "Gross Transaction Amount";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = "Fee";
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 9].Value = "Net Transaction Amount";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                        worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 10].Value = "Payment Date";
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                        worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 11].Value = "Remark";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 7].Value = "%";
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Amount";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells["A" + RowB + ":K" + RowG].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + RowB + ":K" + RowG].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                        worksheet.Cells["A" + _startRowDetail + ":K" + _startRowDetail].Style.Font.Size = 14;

                                        incRowExcel++;

                                        // Row C = 4
                                        int RowC = incRowExcel;
                                        //area header

                                        int _no = 1;
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.InvestorName;
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.IFUA;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.UnitTransaction;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.CurrencyID;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.GrossAmount;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeeAmount;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(F" + incRowExcel + "-H" + incRowExcel + ")";
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 10].Value = Convert.ToDateTime(rsDetail.PaymentDate).ToString("dd-MMMM-yyyy");
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Remark;
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Size = 13;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";




                                        worksheet.Cells["A" + _startRowDetail + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                        incRowExcel = incRowExcel + 4;
                                        worksheet.Cells[incRowExcel, 9].Value = "Fund Manager Approval : ";
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 9].Value = "Date : " + Convert.ToDateTime(rsHeader.Key.Date).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 9].Value = "Checked By : ";
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 10].Value = "Approve By : ";
                                        int _RowA = incRowExcel;
                                        int _RowB = incRowExcel + 5;

                                        worksheet.Cells[incRowExcel, 2].Value = "Debet";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "Account Name";
                                        worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.AccountNameDebet;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "Account Number";
                                        worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.AccountNumberDebet;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "Bank";
                                        worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.BankDebet;

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Kredit";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "Account Name";
                                        worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.AccountNameKredit;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "Account Number";
                                        worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.AccountNumberKredit;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Value = "Bank";
                                        worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.BankKredit;





                                        incRowExcel = incRowExcel + 5;


                                        worksheet.Cells["I" + _RowA + ":J" + _RowA].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _RowB + ":J" + _RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _RowA + ":J" + _RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _RowA + ":J" + _RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Row(incRowExcel).PageBreak = true;
                                        incRowExcel++;

                                    }

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(2).Width = 30;
                                    worksheet.Column(3).Width = 23;
                                    worksheet.Column(4).Width = 20;
                                    worksheet.Column(5).Width = 10;
                                    worksheet.Column(6).Width = 33;
                                    worksheet.Column(7).Width = 15;
                                    worksheet.Column(8).Width = 15;
                                    worksheet.Column(9).Width = 30;
                                    worksheet.Column(10).Width = 23;
                                    worksheet.Column(11).Width = 26;


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "\n &20&B REDEMPTION \n &20&B Batch Form";

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    package.Save();
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    return true;
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }

        }

        public Boolean ClientRedemption_PaymentReportListing(string _userID, ClientRedemption _clientRedemption)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" SELECT FC.NamaBank1 BankName,FC.NamaNasabah1 AccountHolderName,CU.ID CurrencyID,FC.NomorRekening1 RekNo,'Redemption' Type,ClientRedemptionPK,BC.ContactPerson ContactPerson,BN.ID + ' - ' + BC.ID BankCustodiID,BC.Fax1 FaxNo,ValueDate Date,NavDate NavDate,PaymentDate PaymentDate, F.Name FundName,FC.id ClientID, Fc.Name ClientName,CR.NAV NAVAmount,CashAmount,unitamount UnitAmount, CR.RedemptionFeePercent FeePercent,CR.RedemptionFeeAmount FeeAmount, CR.PaymentDate SettlementDate,CR.Description Remark,CR.TotalCashAmount NetAmount,0 SrHolder from ClientRedemption CR   
                                 left join FundClient FC ON CR.fundclientpk = FC.fundclientpk  and FC.status = 2  
                                 left join Fund F ON CR.FundPK = F.FundPK  AND F.Status = 2 
                                 left join FundCashRef FCR ON CR.CashRefPK = FCR.FundCashRefPK  and FCR.status = 2   
                                 left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                                 left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                                 left join Currency CU ON CU.CurrencyPK = CR.CurrencyPK  and CU.status = 2   
                                 WHERE  CR.PaymentDate between @DateFrom and @DateTo and CR.status = 2 order by FundName ";

                        cmd.Parameters.AddWithValue("@DateFrom", _clientRedemption.ParamPaymentDateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _clientRedemption.ParamPaymentDateTo);
                        cmd.Parameters.AddWithValue("@FundFrom", _clientRedemption.ParamFundIDFrom);
                        cmd.Parameters.AddWithValue("@FundTo", _clientRedemption.ParamFundIDTo);


                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "BatchFormREDUnitInstructionBypaymentDate" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormREDUnitInstructionBypaymentDate" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }


                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "UnitRegistryReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Redemption By Payment Date");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                        rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["PaymentDate"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                        rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);


                                        rList.Add(rSingle);

                                    }

                                    var GroupByFundID =
                                     from r in rList
                                     orderby r.SettlementDate, r.FundPK ascending
                                     group r by new { r.SettlementDate } into rGroup
                                     select rGroup;

                                    int incRowExcel = 0;
                                    int _rowEndBalance = 0;
                                    foreach (var rsHeader in GroupByFundID)
                                    {
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Payment Date : ";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.SettlementDate;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "MM/dd/yyyy";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        _rowEndBalance = incRowExcel;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "No.";
                                        worksheet.Cells[incRowExcel, 2].Value = "Nav Date";
                                        worksheet.Cells[incRowExcel, 3].Value = "Fund";
                                        worksheet.Cells[incRowExcel, 4].Value = "Client";
                                        worksheet.Cells[incRowExcel, 5].Value = "Cash";
                                        worksheet.Cells[incRowExcel, 6].Value = "Unit";
                                        string _range = "A" + incRowExcel + ":F" + incRowExcel;

                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        _range = "A" + incRowExcel + ":F" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }

                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        //int _endRowDetail = 0;
                                        //end area header
                                        var _fundName = "";
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            _range = "A" + incRowExcel + ":F" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 11;
                                            }
                                            if (_fundName != rsDetail.FundName)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
                                            }

                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.NAVDate;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "MM/dd/yyyy";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientName;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.CashBalance;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.UnitBalance;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";

                                            //worksheet.Cells[incRowExcel, 7].Calculate();
                                            _rowEndBalance = incRowExcel;

                                            incRowExcel++;
                                            _range = "A" + incRowExcel + ":F" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 11;
                                            }

                                            //_endRowDetail = incRowExcel;
                                            _no++;
                                            _fundName = rsDetail.FundName;

                                        }
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }

                                    string _rangeDetail = "A:F";

                                    using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 11;
                                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    }



                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 6];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();

                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Redemption By Payment Date";


                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    if (_clientRedemption.DownloadMode == "PDF")
                                    {
                                        Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    }
                                    return true;
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }
        }
        
        public Boolean ClientRedemptionAmountBatchForm(string _userID, ClientRedemption _ClientRedemption)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" SELECT FC.NamaBank1 BankName,FC.NamaNasabah1 AccountHolderName,CU.ID CurrencyID,FC.NomorRekening1 RekNo,'Redemption' Type,ClientRedemptionPK,BC.ContactPerson ContactPerson, BN.ID + ' - ' + BC.ID BankCustodiID,
                                     BC.Fax1 FaxNo,ValueDate Date,NavDate NavDate,
                                     F.Name FundName,FC.id ClientID, Fc.Name ClientName,CR.NAV NAVAmount,CashAmount,unitamount UnitAmount, CR.RedemptionFeePercent FeePercent, CR.PaymentDate SettlementDate,CR.Description Remark,CR.TotalCashAmount NetAmount,0 SrHolder from ClientRedemption CR   
                                     left join FundClient FC ON CR.fundclientpk = FC.fundclientpk  and FC.status = 2 
                                     left join Fund F ON CR.FundPK = F.FundPK  AND F.Status = 2 
                                     left join FundCashRef FCR ON CR.CashRefPK = FCR.FundCashRefPK  and FCR.status = 2  
                                     left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                                     left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                                     left join Currency CU ON CU.CurrencyPK = CR.CurrencyPK  and CU.status = 2   
                                     WHERE  CR.Status = @Status and CR.FundPK = @FundPK and CR.NAVDate = @NAVDate and CR.ClientRedemptionPK = @ClientRedemptionPK ";


                        cmd.Parameters.AddWithValue("@Status", _ClientRedemption.Status);
                        cmd.Parameters.AddWithValue("@FundPK", _ClientRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@NAVDate", _ClientRedemption.NAVDate);
                        cmd.Parameters.AddWithValue("@ClientRedemptionPK", _ClientRedemption.ClientRedemptionPK);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "BatchFormREDAmountInstruction_" + _ClientRedemption.NAVDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormREDAmountInstruction_" + _ClientRedemption.NAVDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "UnitRegistryReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form RED Amount Instruction");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ClientRedemptionPK = Convert.ToInt32(dr0["ClientRedemptionPK"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                        rSingle.Nav = Convert.ToDecimal(dr0["NAVAmount"]);
                                        rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                        rSingle.SrHolder = Convert.ToInt32(dr0["SrHolder"]);
                                        rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                        rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                        rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                        rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                        rSingle.BankCustodiID = Convert.ToString(dr0["BankCustodiID"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.RekNo = Convert.ToString(dr0["RekNo"]);
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rSingle.BankName = Convert.ToString(dr0["BankName"]);
                                        rSingle.AccountHolderName = Convert.ToString(dr0["AccountHolderName"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.FundName, r.Date, r.SettlementDate, r.NAVDate, r.ContactPerson, r.FaxNo, r.BankCustodiID } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;

                                    worksheet.Cells[incRowExcel, 1].Value = "REDEMPTION BATCH FORM";
                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Size = 28;
                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;



                                    //else if (_unitRegistryRpt.Type == "Redemption")
                                    //{
                                    //    worksheet.Cells[incRowExcel, 1].Value = "REDEMPTION BATCH FORM";
                                    //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                    //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Size = 28;

                                    //}



                                    foreach (var rsHeader in QueryByFundID)
                                    {




                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        int RowZ = incRowExcel;
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Date ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Wisma Mulia, 19th Floor";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "To ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.BankCustodiID;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jl. Jend. Gatot Subroto No. 42";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Attention ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.ContactPerson;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jakarta 12710, Indonesia";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Fax Number ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.FaxNo;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Phone No";
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyPhone();
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fax No";
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyFax();
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        incRowExcel++;


                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Please kindly receive our Redemption of unit holders as follow : ";
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Trade Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "NAV Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.NAVDate;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Settlement Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.SettlementDate;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 2;


                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;


                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "Holder";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 2].Value = "ID";
                                        worksheet.Cells[RowG, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 2].Style.Font.Bold = true;


                                        worksheet.Cells[incRowExcel, 3].Value = "Name";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 5].Value = "Currency";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "Amount";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = "Fee";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells["G" + RowB + ":H" + RowB].Merge = true;
                                        worksheet.Cells["G" + RowB + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 7].Value = "%";
                                        worksheet.Cells[RowG, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                        worksheet.Cells[RowG, 8].Value = "Amount";
                                        worksheet.Cells[RowG, 8].Style.Font.Bold = true;
                                        worksheet.Cells[RowG, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 9].Value = "Benef Acc No";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Merge = true;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 11].Value = "Remark";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        incRowExcel++;

                                        // Row C = 4
                                        int RowC = incRowExcel;

                                        incRowExcel++;
                                        //area header

                                        int _no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            //Row D = 5
                                            int RowD = incRowExcel;
                                            int RowE = incRowExcel + 1;


                                            //ThickBox Border

                                            worksheet.Cells["A" + RowB + ":K" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":K" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            //worksheet.Cells["A" + RowD + ":K" + RowD].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowD + ":K" + RowD].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.SrHolder;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.CurrencyID;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.UnitBalance * rsDetail.Nav;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 8].Formula = "F" + RowD + "*G" + RowD + " /100";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 10].Value = "Bank Info : " + rsDetail.BankName + " A/C No. " + rsDetail.RekNo + " " + rsDetail.AccountHolderName;
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 10].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Remark;
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }
                                        worksheet.Cells["A" + _endRowDetail + ":K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 5].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 7;
                                        worksheet.Cells[incRowExcel, 1].Value = "Sutejo";
                                        worksheet.Cells[incRowExcel, 6].Value = "Fani Haryanto";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Admin & Settlement";
                                        worksheet.Cells[incRowExcel, 6].Value = "Head of FA & Settlement";

                                        incRowExcel++;


                                    }
                                    //incRowExcel++;
                                    int _lastRow = incRowExcel;

                                    string _rangeA = "A5:K" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 22;
                                    }

                                    worksheet.DeleteRow(_lastRow);
                                    //worksheet.DeleteRow(_lastRow + 1);
                                    //worksheet.DeleteRow(_lastRow + 2);
                                    //worksheet.DeleteRow(_lastRow + 3);

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(2).Width = 25;
                                    worksheet.Column(3).Width = 2;
                                    worksheet.Column(4).Width = 60;
                                    worksheet.Column(5).Width = 20;
                                    worksheet.Column(6).Width = 35;
                                    worksheet.Column(7).Width = 25;
                                    worksheet.Column(8).Width = 30;
                                    worksheet.Column(9).Width = 2;
                                    worksheet.Column(10).Width = 35;
                                    worksheet.Column(11).Width = 30;



                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&20 PT RHB Asset Management Indonesia - Redemption Batch";



                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    package.Save();
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    return true;
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }

        }
        
        public Boolean ClientRedemptionUnitBatchForm(string _userID, ClientRedemption _ClientRedemption)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" SELECT FC.NamaBank1 BankName,FC.NamaNasabah1 AccountHolderName,CU.ID CurrencyID,
                                   FC.NomorRekening1 RekNo,'Redemption' Type,ClientRedemptionPK,BC.ContactPerson ContactPerson,
                                   BN.ID + ' - ' + BC.ID BankCustodiID,BC.Fax1 FaxNo,ValueDate Date,NavDate NavDate, F.Name FundName,
                                   FC.id ClientID, Fc.Name ClientName,CR.NAV NAVAmount,CashAmount,unitamount UnitAmount, CR.RedemptionFeePercent FeePercent, 
                                   CR.PaymentDate SettlementDate,CR.Description Remark,CR.TotalCashAmount NetAmount,0 SrHolder from ClientRedemption CR   
                                   left join FundClient FC ON CR.fundclientpk = FC.fundclientpk  and FC.status = 2 
                                   left join Fund F ON CR.FundPK = F.FundPK  AND F.Status = 2
                                   left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                                   left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                                   left join Currency CU ON CU.CurrencyPK = CR.CurrencyPK  and CU.status = 2  
                                   WHERE  CR.Status = @Status and CR.FundPK = @FundPK and CR.NAVDate = @NAVDate and CR.ClientRedemptionPK = @ClientRedemptionPK ";


                        cmd.Parameters.AddWithValue("@Status", _ClientRedemption.Status);
                        cmd.Parameters.AddWithValue("@FundPK", _ClientRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@NAVDate", _ClientRedemption.NAVDate);
                        cmd.Parameters.AddWithValue("@ClientRedemptionPK", _ClientRedemption.ClientRedemptionPK);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "BatchFormREDUnitInstruction_" + _ClientRedemption.NAVDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormREDUnitInstruction_" + _ClientRedemption.NAVDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "UnitRegistryReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form RED Unit Instruction");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ClientRedemptionPK = Convert.ToInt32(dr0["ClientRedemptionPK"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                        rSingle.Nav = Convert.ToDecimal(dr0["NAVAmount"]);
                                        rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                        rSingle.SrHolder = Convert.ToInt32(dr0["SrHolder"]);
                                        rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                        rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                        rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                        rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                        rSingle.BankCustodiID = Convert.ToString(dr0["BankCustodiID"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.RekNo = Convert.ToString(dr0["RekNo"]);
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rSingle.BankName = Convert.ToString(dr0["BankName"]);
                                        rSingle.AccountHolderName = Convert.ToString(dr0["AccountHolderName"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.FundName, r.Date, r.SettlementDate, r.NAVDate, r.ContactPerson, r.FaxNo, r.BankCustodiID } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;

                                    worksheet.Cells[incRowExcel, 1].Value = "REDEMPTION BATCH FORM";
                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Size = 28;
                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;



                                    //else if (_unitRegistryRpt.Type == "Redemption")
                                    //{
                                    //    worksheet.Cells[incRowExcel, 1].Value = "REDEMPTION BATCH FORM";
                                    //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                    //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    //    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Size = 28;

                                    //}



                                    foreach (var rsHeader in QueryByFundID)
                                    {




                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        int RowZ = incRowExcel;
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Date ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Wisma Mulia, 19th Floor";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "To ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.BankCustodiID;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jl. Jend. Gatot Subroto No. 42";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Attention ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.ContactPerson;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jakarta 12710, Indonesia";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Fax Number ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.FaxNo;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Phone No";
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyPhone();
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fax No";
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyFax();
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        incRowExcel++;


                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Please kindly receive our Redemption of unit holders as follow : ";
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Trade Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "NAV Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.NAVDate;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Settlement Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.SettlementDate;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 2;


                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;


                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "Holder";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 2].Value = "ID";
                                        worksheet.Cells[RowG, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 2].Style.Font.Bold = true;


                                        worksheet.Cells[incRowExcel, 3].Value = "Name";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 5].Value = "Unit";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":F" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = "Fee";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells["G" + RowB + ":H" + RowB].Merge = true;
                                        worksheet.Cells["G" + RowB + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 7].Value = "%";
                                        worksheet.Cells[RowG, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                        worksheet.Cells[RowG, 8].Value = "Amount";
                                        worksheet.Cells[RowG, 8].Style.Font.Bold = true;
                                        worksheet.Cells[RowG, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 9].Value = "Benef Acc No";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Merge = true;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 11].Value = "Remark";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        incRowExcel++;

                                        // Row C = 4
                                        int RowC = incRowExcel;

                                        incRowExcel++;
                                        //area header

                                        int _no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            //Row D = 5
                                            int RowD = incRowExcel;
                                            int RowE = incRowExcel + 1;


                                            //ThickBox Border

                                            worksheet.Cells["A" + RowB + ":K" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":K" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            //worksheet.Cells["A" + RowD + ":K" + RowD].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowD + ":K" + RowD].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.SrHolder;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.UnitBalance;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 8].Formula = "F" + RowD + "*G" + RowD + " /100";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 10].Value = "Bank Info : " + rsDetail.BankName + " A/C No. " + rsDetail.RekNo + " " + rsDetail.AccountHolderName;
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 10].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Remark;
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 11].Style.WrapText = true;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }
                                        worksheet.Cells["A" + _endRowDetail + ":K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 7;
                                        worksheet.Cells[incRowExcel, 1].Value = "Sutejo";
                                        worksheet.Cells[incRowExcel, 6].Value = "Fani Haryanto";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Admin & Settlement";
                                        worksheet.Cells[incRowExcel, 6].Value = "Head of FA & Settlement";

                                        incRowExcel++;


                                    }
                                    //incRowExcel++;
                                    int _lastRow = incRowExcel;

                                    string _rangeA = "A5:K" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 22;
                                    }

                                    worksheet.DeleteRow(_lastRow);
                                    //worksheet.DeleteRow(_lastRow + 1);
                                    //worksheet.DeleteRow(_lastRow + 2);
                                    //worksheet.DeleteRow(_lastRow + 3);

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(2).Width = 25;
                                    worksheet.Column(3).Width = 2;
                                    worksheet.Column(4).Width = 60;
                                    worksheet.Column(5).Width = 20;
                                    worksheet.Column(6).Width = 35;
                                    worksheet.Column(7).Width = 25;
                                    worksheet.Column(8).Width = 30;
                                    worksheet.Column(9).Width = 2;
                                    worksheet.Column(10).Width = 35;
                                    worksheet.Column(11).Width = 30;



                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&20 PT RHB Asset Management Indonesia - Redemption Batch";



                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    package.Save();
                                    //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    return true;
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }

        }

        //GetTransferType
        public decimal ClientRedemption_GetTransferType(string _unitAmount, DateTime _navDate, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            declare @maxDate date
                            select @maxDate = max(date) from CloseNAV where status = 2 and date <= @NAVDate

                            Select Nav * @UnitAmount CashAmount from CloseNAV where status = 2 and FundPK = @FundPK and date = @maxDate
                               ";
                        cmd.Parameters.AddWithValue("@UnitAmount", _unitAmount);
                        cmd.Parameters.AddWithValue("@NAVDate", _navDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["CashAmount"]);

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

        public bool Validate_CheckMinimumBalance(decimal _cashAmount, decimal _unitAmount, DateTime _valueDate, int _fundPK, int _fundClientPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
//                        if (_unitAmount == 0)
//                        {
//                            cmd.CommandText = @"
//                            create table #UnitAmountTemps
//                            (
//                            UnitAmount decimal(18,4),
//                            fundpk int,
//							fundclientpk int
//                            )
//                            insert into #UnitAmountTemps
//                            select UnitAmount - (select @CashAmount/isnull([dbo].[FgetLastCloseNav] (@ValueDate - 1,@FundPK),0) Unit) UnitAmount,FundPK,FundClientPK from FundClientPosition where FundPK = @FundPK and FundclientPK = @FundClientPK 
//                            and Date =                  
//
//                            (                  
//                            Select max(date) From FundClientPosition where FundPK = @FundPK and FundclientPK = @FundClientPK             
//                            )                  
//
//							select b.UnitAmount,a.RemainingBalanceUnit,case when b.UnitAmount = 0 then 0 else case when RemainingBalanceUnit < b.UnitAmount then 0 else case when RemainingBalanceUnit > b.UnitAmount then 1 end end end hasil from Fund a left join #UnitAmountTemps b on a.FundPK = b.fundpk where b.FundPK = @FundPK
//                            ";

//                        }
//                        else
//                        {
//                            cmd.CommandText = @"
//                            create table #UnitAmountTemps
//                            (
//                            UnitAmount decimal(18,4),
//                            fundpk int,
//							fundclientpk int
//                            )
//                            insert into #UnitAmountTemps
//                            select UnitAmount - @UnitAmount  UnitAmount,FundPK,FundClientPK from FundClientPosition where FundPK = @FundPK and FundclientPK = @FundClientPK
//                            and Date =                  
//
//                            (                  
//                            Select max(date) From FundClientPosition where FundPK = @FundPK and FundclientPK = @FundClientPK             
//                            ) 
//
//							
//						    select b.UnitAmount,a.RemainingBalanceUnit,case when b.UnitAmount = 0 then 0 else case when RemainingBalanceUnit < b.UnitAmount then 0 else case when RemainingBalanceUnit > b.UnitAmount then 1 end end end hasil from Fund a left join #UnitAmountTemps b on a.FundPK = b.fundpk where b.FundPK = @FundPK
//							";

                            cmd.CommandText = @"
                            Declare @RemainingBalance numeric(18,4)
                            Declare @MaxEDT datetime
                            Select @MaxEDT = max(Valuedate) from EndDayTrails where status = 2 and ValueDate <= @Date

                            set @MaxEDT = dbo.FWorkingDay(@MaxEDT,1)


                            create table #UnitAmountTemps
                            (
                            UnitAmount decimal(18,4),
                            fundpk int,
                            fundclientpk int
                            )

                            insert into #UnitAmountTemps
                            Select isnull(A.UnitAmount,0) - isnull(B.UnitAmount,0) - isnull(C.UnitAmount,0) Unit,A.FundPK,A.FundClientPK
                            FROM FundClientPosition A
                            left join (
	                            SELECT FundClientPK,FundPK, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,FundPK) end,0)) UnitAmount FROM ClientRedemption  
	                            WHERE status not in (3,4) and posted = 0 and Revised = 0 AND FundPK = @FundPK AND FundClientPK = @FundClientPK
	                            group by FundClientPK,FundPK
                            ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 

                            left join 
                            (
	                            SELECT FundClientPK,FundPKFrom, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,FundPKFrom) end,0)) UnitAmount FROM ClientSwitching  
	                            WHERE status not in (3,4) and posted = 0 and Revised = 0 AND FundPKFrom = @FundPK AND FundCLientPK = @FundClientPK
	                            group by FundClientPK,FundPKFrom
                            ) C on A.FundClientPK = C.FundClientPK and A.FundPK = C.FundPKFrom 
                            where Date = @MaxEDT  AND A.FundPK = @FundPK AND A.FundClientPK = @FundClientPK

                            select @RemainingBalance = UnitAmount from #UnitAmountTemps

                            if((@RemainingBalance - @UnitAmount) < 0)
                            BEGIN
                                select 1 Result
                            END
                            ELSE
                            BEGIN
                                select 0 Result
                            END
                            ";
                        //}

                        cmd.Parameters.AddWithValue("@CashAmount", _cashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _unitAmount);
                        cmd.Parameters.AddWithValue("@Date", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
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

        public Boolean ClientRedemptionAmountBatchFormBySelectedDataTaspen(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientRedemption _clientRedemption)
        {

            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_clientRedemption.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientRedemption.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _clientRedemption.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        SELECT CS.FundPK,F.Name FundName,Fc.Name InvestorName,
                        NavDate NavDate, ValueDate Date, FC.IFUACode IFUA, unitamount UnitTransaction,BN.Name BankCustodianName,BC.Address CustodianAddress,
                        CashAmount GrossAmount, CS.RedemptionFeePercent FeePercent, CS.RedemptionFeeAmount FeeAmount, 
                        CS.TotalCashAmount NetAmount, CS.Description Remark, CU.ID CurrencyID,
                        isnull(BC.ID,'') BankCustodiID, isnull(BC.Address,'') Address, isnull(BC.Attn1,'') ContactPerson, isnull(BC.Fax1,'') FaxNo, isnull(BC.Address,'') CustodyAddress,isnull(FCR.BankAccountNo,0) BankAccountNo from ClientRedemption CS   
                        left join FundClient FC ON CS.fundclientpk = FC.fundclientpk and FC.status = 2  
                        left join Fund F ON CS.FundPK = F.FundPK  and F.status = 2   
                        left join FundCashRef FCR ON CS.CashRefPK = FCR.FundCashRefPK  and FCR.status = 2   
                        left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                        left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                        left join Currency CU ON CU.CurrencyPK = CS.CurrencyPK  and CU.status = 2   
                        WHERE CS.NAVDate between @DateFrom and @DateTo  and CS.Type = 1 and CS.status not in(3,4) " + paramClientRedemptionSelected;

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "BatchFormREDAmountInstructionBySelected_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormREDAmountInstructionBySelected_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "UnitRegistryReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form RED Instruction");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                        rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                        rSingle.InvestorName = Convert.ToString(dr0["InvestorName"]);
                                        rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.IFUA = Convert.ToString(dr0["IFUA"]);
                                        rSingle.UnitTransaction = Convert.ToDecimal(dr0["UnitTransaction"]);
                                        rSingle.GrossAmount = Convert.ToDecimal(dr0["GrossAmount"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.BankCustodiID = Convert.ToString(dr0["BankCustodianName"]);
                                        rSingle.CustodyAddress = Convert.ToString(dr0["CustodyAddress"]);
                                        rSingle.BankAccountNo = Convert.ToDecimal(dr0["BankAccountNo"]);

                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.BankCustodiID } into rGroup
                                        select rGroup;

                                    int incRowExcel = 0;
                                    //int _startRowDetail = 0;



                                    incRowExcel = incRowExcel + 2;

                                    worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(_dateFrom).ToString("dd/MMM/yyyy");
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "Nomor ";
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[incRowExcel, 3].Value = ":";
                                    worksheet.Cells[incRowExcel, 4].Value = "SRT - " + "1234" + " / Dir.1/TL/" + Convert.ToDateTime(_dateFrom).ToString("MMyyyy");
                                    worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "Sifat ";
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[incRowExcel, 3].Value = ":";
                                    worksheet.Cells[incRowExcel, 4].Value = "Penting";
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "Lampiran ";
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[incRowExcel, 3].Value = ":";
                                    worksheet.Cells[incRowExcel, 4].Value = " - ";
                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "Kepada Yth, ";
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "Bapak Sigit Winarto";
                                    worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                    worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "Vice President";
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.Font.Bold = true;
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "Head Of Capital Market Oprations Departement ";
                                    worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = " Mandiri Custodian";
                                    worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                    worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "PLaza Mandiri lt 1, Lobbi barat Jl.jendral Gatot Subroto Kav.36-38 Jakarta 12190";
                                    worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                    worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.WrapText = true;
                                    worksheet.Row(incRowExcel).Height = 68;
                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 1].Value = "Perihal ";
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[incRowExcel, 3].Value = ":";
                                    worksheet.Cells[incRowExcel, 4].Value = "Instruksi redemption Unit Link";
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Font.Bold = true;
                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Row(incRowExcel).Height = 45;
                                    worksheet.Cells[incRowExcel, 1].Value = "Dengan ini kami instruksikan agar bank mandiri kustodian dapat melaksanakan transaksi redemption pada unit link PT Asuransi Jiwa Taspen dengan rincian sebagai berikut:";
                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.WrapText = true;
                                    worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                    incRowExcel = incRowExcel + 2;

                                    foreach (var rsHeader in QueryByFundID)
                                    {

                                        int RowF = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "Nama Fund";
                                        worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "No. Rekening Dana";
                                        worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 8].Value = "Nominal";
                                        worksheet.Cells["H" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.WrapText = true;
                                        worksheet.Row(incRowExcel).Height = 40;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Font.Bold = true;
                                        incRowExcel++;


                                        worksheet.Cells["A" + RowF + ":I" + RowF].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF + ":I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        int RowD = incRowExcel;
                                        int _no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.BankAccountNo;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "###";
                                            worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                            worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.GrossAmount;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Style.WrapText = true;
                                            worksheet.Cells["H" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.WrapText = true;
                                            worksheet.Row(incRowExcel).Height = 30;

                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;
                                        }


                                        //incRowExcel++;
                                        //worksheet.Cells[incRowExcel, 1].Value = "*) Coret yang tidak perlu";
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terimakasih.";
                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT. ASURANSI JIWA TASPEN";
                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Direksi,";
                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;

                                        incRowExcel = incRowExcel + 4;
                                        worksheet.Cells[incRowExcel, 3].Value = _host.Get_SignatureName(_clientRedemption.Signature1);
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_SignatureName(_clientRedemption.Signature2);
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;


                                        worksheet.Cells[incRowExcel, 3].Value = _host.Get_PositionSignature(_clientRedemption.Signature1);
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_PositionSignature(_clientRedemption.Signature2);
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 7].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        incRowExcel++;


                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }

                                    int _lastRow = incRowExcel;

                                    string _rangeA = "A:I" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 18;
                                    }

                                    worksheet.DeleteRow(_lastRow);

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 9];
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 5;
                                    worksheet.Column(4).Width = 20;
                                    worksheet.Column(5).Width = 30;
                                    worksheet.Column(6).Width = 30;
                                    worksheet.Column(7).Width = 15;
                                    worksheet.Column(8).Width = 15;
                                    worksheet.Column(9).Width = 25;
                                    //worksheet.Column(10).Width = 30;

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "\n   &20&B SUBSCRIPTION \n &20&B Batch Form \n";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border


                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    package.Save();
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    return true;
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }

        }

        public decimal Get_UnitRedemptionAll(int _fundPK, int _fundclientPK, DateTime _date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"

                        select UnitAmount from FundClientPosition 
                        where FundPK = @FundPK and FundClientPK = @FundClientPK 
                        and Date = 
                        (
                        select max(Date) from FundClientPosition where Date <= @Date
                        )


                               ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundclientPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["UnitAmount"]);

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

        private ClientRedemptionHoldingPeriod setClientRedemptionHoldingPeriod(SqlDataReader dr)
        {
            ClientRedemptionHoldingPeriod M_ClientRedemptionHoldingPeriod = new ClientRedemptionHoldingPeriod();

            M_ClientRedemptionHoldingPeriod.FundID = Convert.ToString(dr["FundID"]);
            M_ClientRedemptionHoldingPeriod.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_ClientRedemptionHoldingPeriod.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
            M_ClientRedemptionHoldingPeriod.RedempDate = Convert.ToDateTime(dr["RedempDate"]);
            M_ClientRedemptionHoldingPeriod.HoldingPeriod = Convert.ToInt32(dr["HoldingPeriod"]);
            M_ClientRedemptionHoldingPeriod.TotalSubs = Convert.ToDecimal(dr["TotalSubs"]);
            M_ClientRedemptionHoldingPeriod.TakenOut = Convert.ToDecimal(dr["TakenOut"]);
            M_ClientRedemptionHoldingPeriod.Remaining = Convert.ToDecimal(dr["Remaining"]);
            M_ClientRedemptionHoldingPeriod.TotalFeeAmount = Convert.ToDecimal(dr["TotalFeeAmount"]);
            M_ClientRedemptionHoldingPeriod.RedempFeePercent = Convert.ToDecimal(dr["RedempFeePercent"]);
            

            return M_ClientRedemptionHoldingPeriod;
        }

        public List<ClientRedemptionHoldingPeriod> Init_DataHoldingPeriod(string _fundPK, string _fundClientPK, DateTime _date)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientRedemptionHoldingPeriod> L_ClientRedemptionHoldingPeriod = new List<ClientRedemptionHoldingPeriod>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramFundClient = "";

                        if (!_host.findString(_fundPK.ToLower(), "0", ",") && !string.IsNullOrEmpty(_fundPK))
                        {
                            _paramFund = " And D.FundPK in ( " + _fundPK + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        if (!_host.findString(_fundClientPK.ToLower(), "0", ",") && !string.IsNullOrEmpty(_fundClientPK))
                        {
                            _paramFundClient = "And E.FundClientPK in ( " + _fundClientPK + " ) ";
                        }
                        else
                        {
                            _paramFundClient = "";
                        }
                        cmd.CommandText = @"
                        
                        ;WITH cte AS (SELECT ValueDate,FundPK,FundClientPK,TotalCashAmount, SUM(TotalCashAmount) OVER ( PARTITION BY FundPk,FundClientPk ORDER BY ValueDate ASC) as CumQty 
                        FROM ClientSubscription WHERE status in (1,2) and TotalCashAmount >0 and notes <> 'CUTOFF' 
                        group by FundPK,FundClientPK,ValueDate,TotalCashAmount)

                        SELECT  D.ID FundID,E.Name FundClientName,ValueDate,@ValueDate Redempdate,TotalCashAmount TotalSubs,isnull(TakenQty,0) TotalRedemp,isnull(C.RedempFeePercent,0) RedempFeePercent,
                            CASE
                                WHEN CumQty<isnull(TakenQty,0) THEN TotalCashAmount
                                    ELSE case when isnull(TakenQty,0) -(CumQty-TotalCashAmount) < 0 then 0 else isnull(TakenQty,0) -(CumQty-TotalCashAmount) end
                            END AS TakenOut,
	                        CASE
                                WHEN CumQty<isnull(TakenQty,0) THEN 0
                                    ELSE case when isnull(TakenQty,0) -(CumQty-TotalCashAmount) < 0 then TotalCashAmount else (CumQty) - isnull(TakenQty,0) end
                            END AS Remaining,
	                        CASE
                                WHEN CumQty<isnull(TakenQty,0) THEN 0
                                    ELSE case when DATEDIFF(month,ValueDate,@Valuedate) <= C.HoldingPeriod then case when isnull(TakenQty,0) -(CumQty-TotalCashAmount) < 0  then TotalCashAmount * isnull(C.RedempFeePercent,0) else ((CumQty) - isnull(TakenQty,0))* isnull(C.RedempFeePercent,0) end else 0 end
                            END AS TotalFeeAmount,
	                        case when DATEDIFF(month,ValueDate,@Valuedate) > C.HoldingPeriod then 'FREE TO REDEMP' else 'REDEMP WITH TAX' end StatusRedemption,
	                        DATEDIFF(month,ValueDate,@Valuedate) HoldingPeriod
                         FROM cte A
                         left join 
                         (
	                        select FundClientPK,FundPK,sum(TotalCashAmount) TakenQty from ClientRedemption where status in (1,2)
	                        group by FundClientPk,FundPK
                         ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
                         left join BackLoadSetup C on A.FundPK = C.FundPK and C.Status in (1,2)
                         left join Fund D on A.FundPK = D.FundPK and D.Status in (1,2)
                         left join FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
                         where 1 = 1 " + _paramFund + _paramFundClient + @" 
                         group by D.ID,E.Name,ValueDate,CumQty,TotalCashAmount,B.TakenQty,C.HoldingPeriod,C.RedempFeePercent
                         order by D.ID,E.Name,ValueDate
                        ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ClientRedemptionHoldingPeriod.Add(setClientRedemptionHoldingPeriod(dr));
                                }
                            }
                            return L_ClientRedemptionHoldingPeriod;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<ClientRedemptionHoldingPeriod> Get_DataHoldingPeriod(int _fundPK, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientRedemptionHoldingPeriod> L_ClientRedemptionHoldingPeriod = new List<ClientRedemptionHoldingPeriod>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        declare @dateFrom datetime
                        declare @Dateto datetime

                        set @dateFrom = '01/01/2000'
                        set @Dateto = '12/31/2099'




                        DECLARE @SubsRedemp TABLE
                        (
	                        ClientSubscriptionPK INT,
	                        ClientRedemptionPK INT,
	                        RedRemainingBalance NUMERIC(22,4),
	                        UnitRemainingToRedemp NUMERIC(22,4)
                        )

                        INSERT INTO @SubsRedemp
                                ( ClientSubscriptionPK ,
                                  ClientRedemptionPK ,
                                  RedRemainingBalance,
		                          UnitRemainingToRedemp
                                )
                        VALUES  ( 0 , -- ClientSubscriptionPK - int
                                  0 , -- ClientRedemptionPK - int
                                  0 , -- RedRemainingBalance - numeric
		                          0
                                )
                        --INSERT INTO @Substemp
                        --SELECT * FROM @SubsTemp

                        DECLARE @CClientSubscriptionPK INT
                        DECLARE @CNAVDate DATETIME
                        DECLARE @CFundPK INT
                        DECLARE @CFundClientPK INT
                        DECLARE @CUnitAmount NUMERIC(22,4)

                        DECLARE @CounterUnit NUMERIC(22,4)
                        DECLARE @RedempUnit NUMERIC(22,4)
                        DECLARE @ClientRedemptionPK INT
                        DECLARE @UnitBefore NUMERIC(22,4)
                        DECLARE @CounterUnitBefore NUMERIC(22,4)

                        DECLARE @MaxClientRedemptionPK INT

                        DECLARE @MaxMappingClientRedemptionPK INT

                        Declare A Cursor For
		                        SELECT  ClientSubscriptionPK,NAVDate,FundPK,FundClientPK,TotalUnitAmount FROM dbo.ClientSubscription
		                        WHERE Posted = 1 AND status = 2 AND Revised = 0 AND type <> 3 
		                        and FundClientPK = 
		                        (
			                        select distinct FundClientPK from ClientRedemption 
			                        where status not in (3,4) and valuedate between @dateFrom and @dateto
			                        and Selected = 1
		                        ) 
		                        and FundPK =
		                        (
			                        select distinct FundPK from ClientRedemption 
			                        where status not in (3,4) and valuedate between @dateFrom and @dateto
			                        and Selected = 1
		
		                        )
		                        ORDER BY NAVDate ASC
                        Open A
                        Fetch Next From A
                        INTO @CClientSubscriptionPK,@CNAVDate,@CFundPK,@CFundClientPK,@CUnitAmount
                        While @@FETCH_STATUS = 0  
                        BEGIN

	                        SELECT @MaxClientRedemptionPK = MAX(ClientRedemptionPK) FROM dbo.ClientRedemption
	                        WHERE FundPK = @CFundPK AND FundClientPK = @CFundClientPK AND Status NOT IN (3,4)  


	                        SET @MaxClientRedemptionPK = ISNULL(@MaxClientRedemptionPK,-1)

	                        SELECT @MaxMappingClientRedemptionPK = MAX(ClientRedemptionPK) FROM @SubsRedemp

	                        SET @CounterUnit = 0
	                        SET @RedempUnit = 0
	                        SET @UnitBefore = 0
	                        SET @CounterUnit = @CUnitAmount

	                        WHILE @CounterUnit > 0 AND @MaxMappingClientRedemptionPK <= @MaxClientRedemptionPK 
	                        BEGIN
	                        --select * from @SubsRedemp
	                        IF EXISTS(
		                        SELECT * FROM @SubsRedemp WHERE clientredemptionPK = @MaxClientRedemptionPK
		                        AND RedRemainingBalance = 0
	                        )
	                        BEGIN
		                        SET @CounterUnit = -1
	                        END
	                        ELSE
	                        BEGIN
		                        SELECT TOP 1 @RedempUnit = CASE WHEN B.ClientRedemptionPK IS NOT NULL THEN B.RedRemainingBalance 
		                        ELSE  
		
			                        CASE WHEN A.CashAmount > 0 and	ISNULL(A.UnitAmount,0) = 0 AND ISNULL(A.NAV,0) = 0 
				                        AND ISNULL(D.NAV,0) > 0
					                        THEN A.CashAmount / D.Nav  ELSE A.UnitAmount 
			                        END 

		                        END, @ClientRedemptionPK = A.ClientRedemptionPK 
		                        FROM dbo.ClientRedemption A
		                        LEFT JOIN @SubsRedemp B ON A.ClientRedemptionPK = B.ClientRedemptionPK
		                        LEFT JOIN dbo.ZDT_WorkingDays C ON A.NAVDate = C.Date
		                        LEFT JOIN dbo.CloseNAV D ON A.FundPK = D.FundPK AND D.Date = C.DTM1 AND D.status IN (1,2)
		                        WHERE A.FundPK = @CFundPK AND A.FundClientPK = @CFundClientPK AND A.Status NOT IN (3,4) 
		                        AND A.ClientRedemptionPK NOT IN (
			                        SELECT ISNULL(ClientRedemptionPK,0) FROM @SubsRedemp 
			                        WHERE RedRemainingBalance = 0
		                        ) AND Type <> 3
		                        ORDER BY NAVDate ASC

		                        --SELECT @CounterUnit CounterUnitBefore
		                        SET @UnitBefore = CASE WHEN @CounterUnitBefore < 0  THEN ABS(@CounterUnitBefore) ELSE 0 end
		
		                        SET @CounterUnit = @CounterUnit - @RedempUnit
			
	                        --SELECT @RedempUnit redempUnit,@CounterUnit CounterUnitAfer,@ClientRedemptionPK
		
		                        IF @CounterUnit <= 0 
		                        BEGIN
			                        INSERT INTO @SubsRedemp
			                                ( ClientSubscriptionPK ,
			                                  ClientRedemptionPK,
					                          RedRemainingBalance,
					                          UnitRemainingToRedemp
			                                )
			                        SELECT @CClientSubscriptionPK,@ClientRedemptionPK, ABS(@CounterUnit)	,@UnitBefore
		                        END	
		                        ELSE
		                        BEGIN
			                        INSERT INTO @SubsRedemp
			                                ( ClientSubscriptionPK ,
			                                  ClientRedemptionPK,
					                          RedRemainingBalance,UnitRemainingToRedemp
			                                )
			                        SELECT @CClientSubscriptionPK,@ClientRedemptionPK, 0,@UnitBefore
		                        END

			                        SET @CounterUnitBefore = @CounterUnit

		                        SELECT @MaxClientRedemptionPK = MAX(ClientRedemptionPK) FROM dbo.ClientRedemption
	                        WHERE FundPK = @CFundPK AND FundClientPK = @CFundClientPK AND Status NOT IN (3,4)  

	                        SELECT @MaxMappingClientRedemptionPK = MAX(ClientRedemptionPK) FROM @SubsRedemp
	                        END

	                        END


	                        Fetch Next From A 
	                        INTO @CClientSubscriptionPK,@CNAVDate,@CFundPK,@CFundClientPK,@CUnitAmount
                        End	
                        Close A
                        Deallocate A

                        --select * from @SubsRedemp

                        DECLARE @BackLoadTable TABLE
                        (
	                        ClientSubscriptionPK int,
	                        ClientRedemptionPK INT,
	                        RedRemainingBalance NUMERIC(22,4),
	                        SubsDate DATETIME,
	                        RedDate DATETIME,
	                        ClearDate DATETIME,

	                        HoldingPeriod int,
	                        FeePercent NUMERIC(18,4),
	                        SubsUnitAmount NUMERIC(22,4),
	                        RedUnitAmount NUMERIC(22,4),
	                        UnitRemainingToRedemp NUMERIC(22,4),
	
	                        RedempUnitFee NUMERIC(22,4)
                        )

                        DECLARE @ZClientSubscriptionPK INT
                        DECLARE @ZClientRedemptionPK int
                        DECLARE @ZSubsDate DATETIME
                        DECLARE @ZRedDate DATETIME
                        DECLARE @ZFundPK INT
                        DECLARE @ZRemRedBalance NUMERIC(22,4)
                        DECLARE @ZRedUnitAmount NUMERIC(22,4)
                        DECLARE @ZUnitToRedemp NUMERIC(22,4)
                        DECLARE @ZSubsUnitAmount NUMERIC(22,4)

                        Declare A Cursor For
	                        SELECT B.NAVDate,C.NAVDate,A.ClientSubscriptionPK,A.ClientRedemptionPK,C.FundPK 
	                        ,A.RedRemainingBalance,C.UnitAmount,A.UnitRemainingToRedemp,B.TotalUnitAmount
	                        FROM @SubsRedemp A
	                        LEFT JOIN dbo.ClientSubscription B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK AND B.status NOT IN (3,4)
	                        LEFT JOIN dbo.ClientRedemption C ON A.ClientRedemptionPK = C.ClientRedemptionPK AND C.status NOT IN (3,4)
	                        WHERE A.ClientSubscriptionPK > 0 
	                        and B.FundClientPK = 
		                        (
			                        select distinct FundClientPK from ClientRedemption 
			                        where status not in (3,4) and valuedate between @dateFrom and @dateto
			                        and Selected = 1
		                        ) 
		                        and B.FundPK =
		                        (
			                        select distinct FundPK from ClientRedemption 
			                        where status not in (3,4) and valuedate between @dateFrom and @dateto
			                        and Selected = 1
		
		                        )

			                        and C.FundClientPK = 
		                        (
			                        select distinct FundClientPK from ClientRedemption 
			                        where status not in (3,4) and valuedate between @dateFrom and @dateto
			                        and Selected = 1
		                        ) 
		                        and C.FundPK =
		                        (
			                        select distinct FundPK from ClientRedemption 
			                        where status not in (3,4) and valuedate between @dateFrom and @dateto
			                        and Selected = 1
		
		                        )
                        Open A
                        Fetch Next From A
                        INTO @ZSubsDate,@ZRedDate,@ZClientSubscriptionPK,@ZClientRedemptionPK,@ZFundPK,@ZRemRedBalance,@ZRedUnitAmount,@ZUnitToRedemp,@ZSubsUnitAmount

                        While @@FETCH_STATUS = 0  
                        Begin
    
	                        INSERT INTO @BackLoadTable
	                                ( SubsDate,
			                          ClientSubscriptionPK ,
	                                  ClientRedemptionPK ,
	                                  RedRemainingBalance ,
	                                  HoldingPeriod ,
	                                  FeePercent ,
	                                  SubsUnitAmount ,
	                                  RedUnitAmount ,
	                                  UnitRemainingToRedemp ,
	                                  RedDate ,
	                                  ClearDate ,
	                                  RedempUnitFee
	                                )
	
	                        SELECT  TOP 1 @ZSubsDate, @ZClientSubscriptionPK ClientSubscriptionPK
	                        ,@ZClientRedemptionPK ClientRedemptionPK
	                        ,@ZRemRedBalance RedRemBalance
	                        ,HoldingPeriod
	                        ,RedempFeePercent
	                        ,@ZSubsUnitAmount SubsUnitAmount
	                        ,@ZRedUnitAmount RedUnitAmount
	                        ,@ZUnitToRedemp UnitRemainingToRedemp
	                        ,@ZRedDate RedDate
	                        ,DATEADD(MONTH,HoldingPeriod,@ZSubsDate) ClearDate
	                        ,CASE WHEN @ZRedDate <= DATEADD(MONTH,HoldingPeriod,@ZSubsDate) THEN 		
		                        --CASE WHEN @ZUnitToRedemp = 0 THEN @ZRedUnitAmount * RedempFeePercent / 100 
		                        CASE WHEN @ZUnitToRedemp = 0 THEN IIF (@ZSubsUnitAmount <= @ZRedUnitAmount,  @ZSubsUnitAmount, @ZRedUnitAmount)  * RedempFeePercent / 100 
			                        ELSE @ZUnitToRedemp * RedempFeePercent / 100 END
	                        ELSE 0 
	                        END RedempUnitFee	
	                        FROM dbo.BackLoadSetup WHERE date =
	                        (
		                        SELECT MAX(Date) FROM dbo.BackLoadSetup WHERE FundPK = @ZFundPK AND Date <= @ZRedDate AND status = 2
	                        ) AND status  = 2 AND FundPK = @ZFundPK
	
	                        ORDER BY HoldingPeriod desc
	
	                        FETCH Next From A 
	                        INTO @ZSubsDate,@ZRedDate,@ZClientSubscriptionPK,@ZClientRedemptionPK,@ZFundPK,@ZRemRedBalance,@ZRedUnitAmount,@ZUnitToRedemp,@ZSubsUnitAmount
                        End	
                        Close A
                        Deallocate A



                        Declare @RedempFee table
                        (
	                        ClientRedemptionPK int,
	                        FeePercent numeric(18,4)
                        )

                        insert into @RedempFee
                        select A.ClientRedemptionPK,sum(isnull(B.redempUnitFee,0))/A.UnitAmount * 100 FeePercent  from ClientRedemption A
                        inner join @BackLoadTable B on A.ClientRedemptionPK = B.ClientRedemptionPK
                        group by A.ClientRedemptionPK,A.UnitAmount

                        update A set RedemptionFeePercent = B.FeePercent  from ClientRedemption A
                        inner join @RedempFee B on A.ClientRedemptionPK = B.ClientRedemptionPK


                        --SELECT * FROM @BackLoadTable

                        --SELECT * FROM dbo.BackLoadSetup

                        -- PARAM FundClient sama Fund
                        SELECT C.ID FundID,C.Name FundName,D.Name FundClientName,RedRemainingBalance,SubsDate,RedDate,
                        ClearDate,HoldingPeriod,FeePercent,SubsUnitAmount,RedUnitAmount,UnitRemainingToRedemp,RedempUnitFee FROM @BackLoadTable A
                        LEFT JOIN dbo.ClientSubscription B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK AND B.status = 2 AND posted = 1
                        left join Fund C on B.FundPK = C.FundPK and C.status in (1,2)
                        left join FundClient D on B.FundClientPK = D.FundClientPK and D.status in (1,2)
                        WHERE B.FundPK = @FundPK  AND B.FundClientPK = @FundClientPK
                        ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ClientRedemptionHoldingPeriod.Add(setClientRedemptionHoldingPeriod(dr));
                                }
                            }
                            return L_ClientRedemptionHoldingPeriod;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Update_RedemptionFeeHoldingPeriod(DateTime _date)
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
                        declare @dateFrom datetime
declare @Dateto datetime

set @dateFrom = '01/01/2000'
set @Dateto = '12/31/2099'




DECLARE @SubsRedemp TABLE
(
	ClientSubscriptionPK INT,
	ClientRedemptionPK INT,
	RedRemainingBalance NUMERIC(22,4),
	UnitRemainingToRedemp NUMERIC(22,4)
)

INSERT INTO @SubsRedemp
        ( ClientSubscriptionPK ,
          ClientRedemptionPK ,
          RedRemainingBalance,
		  UnitRemainingToRedemp
        )
VALUES  ( 0 , -- ClientSubscriptionPK - int
          0 , -- ClientRedemptionPK - int
          0 , -- RedRemainingBalance - numeric
		  0
        )
--INSERT INTO @Substemp
--SELECT * FROM @SubsTemp

DECLARE @CClientSubscriptionPK INT
DECLARE @CNAVDate DATETIME
DECLARE @CFundPK INT
DECLARE @CFundClientPK INT
DECLARE @CUnitAmount NUMERIC(22,4)

DECLARE @CounterUnit NUMERIC(22,4)
DECLARE @RedempUnit NUMERIC(22,4)
DECLARE @ClientRedemptionPK INT
DECLARE @UnitBefore NUMERIC(22,4)
DECLARE @CounterUnitBefore NUMERIC(22,4)

DECLARE @MaxClientRedemptionPK INT

DECLARE @MaxMappingClientRedemptionPK INT

Declare A Cursor For
		SELECT  ClientSubscriptionPK,NAVDate,FundPK,FundClientPK,TotalUnitAmount FROM dbo.ClientSubscription
		WHERE Posted = 1 AND status = 2 AND Revised = 0 AND type <> 3 
		and FundClientPK = 
		(
			select distinct FundClientPK from ClientRedemption 
			where status not in (3,4) and valuedate between @dateFrom and @dateto
			and Selected = 1
		) 
		and FundPK =
		(
			select distinct FundPK from ClientRedemption 
			where status not in (3,4) and valuedate between @dateFrom and @dateto
			and Selected = 1
		
		)
		ORDER BY NAVDate ASC
Open A
Fetch Next From A
INTO @CClientSubscriptionPK,@CNAVDate,@CFundPK,@CFundClientPK,@CUnitAmount
While @@FETCH_STATUS = 0  
BEGIN

	SELECT @MaxClientRedemptionPK = MAX(ClientRedemptionPK) FROM dbo.ClientRedemption
	WHERE FundPK = @CFundPK AND FundClientPK = @CFundClientPK AND Status NOT IN (3,4)  


	SET @MaxClientRedemptionPK = ISNULL(@MaxClientRedemptionPK,-1)

	SELECT @MaxMappingClientRedemptionPK = MAX(ClientRedemptionPK) FROM @SubsRedemp

	SET @CounterUnit = 0
	SET @RedempUnit = 0
	SET @UnitBefore = 0
	SET @CounterUnit = @CUnitAmount

	WHILE @CounterUnit > 0 AND @MaxMappingClientRedemptionPK <= @MaxClientRedemptionPK 
	BEGIN
	--select * from @SubsRedemp
	IF EXISTS(
		SELECT * FROM @SubsRedemp WHERE clientredemptionPK = @MaxClientRedemptionPK
		AND RedRemainingBalance = 0
	)
	BEGIN
		SET @CounterUnit = -1
	END
	ELSE
	BEGIN
		SELECT TOP 1 @RedempUnit = CASE WHEN B.ClientRedemptionPK IS NOT NULL THEN B.RedRemainingBalance 
		ELSE  
		
			CASE WHEN A.CashAmount > 0 and	ISNULL(A.UnitAmount,0) = 0 AND ISNULL(A.NAV,0) = 0 
				AND ISNULL(D.NAV,0) > 0
					THEN A.CashAmount / D.Nav  ELSE A.UnitAmount 
			END 

		END, @ClientRedemptionPK = A.ClientRedemptionPK 
		FROM dbo.ClientRedemption A
		LEFT JOIN @SubsRedemp B ON A.ClientRedemptionPK = B.ClientRedemptionPK
		LEFT JOIN dbo.ZDT_WorkingDays C ON A.NAVDate = C.Date
		LEFT JOIN dbo.CloseNAV D ON A.FundPK = D.FundPK AND D.Date = C.DTM1 AND D.status IN (1,2)
		WHERE A.FundPK = @CFundPK AND A.FundClientPK = @CFundClientPK AND A.Status NOT IN (3,4) 
		AND A.ClientRedemptionPK NOT IN (
			SELECT ISNULL(ClientRedemptionPK,0) FROM @SubsRedemp 
			WHERE RedRemainingBalance = 0
		) AND Type <> 3
		ORDER BY NAVDate ASC

		--SELECT @CounterUnit CounterUnitBefore
		SET @UnitBefore = CASE WHEN @CounterUnitBefore < 0  THEN ABS(@CounterUnitBefore) ELSE 0 end
		
		SET @CounterUnit = @CounterUnit - @RedempUnit
			
	--SELECT @RedempUnit redempUnit,@CounterUnit CounterUnitAfer,@ClientRedemptionPK
		
		IF @CounterUnit <= 0 
		BEGIN
			INSERT INTO @SubsRedemp
			        ( ClientSubscriptionPK ,
			          ClientRedemptionPK,
					  RedRemainingBalance,
					  UnitRemainingToRedemp
			        )
			SELECT @CClientSubscriptionPK,@ClientRedemptionPK, ABS(@CounterUnit)	,@UnitBefore
		END	
		ELSE
		BEGIN
			INSERT INTO @SubsRedemp
			        ( ClientSubscriptionPK ,
			          ClientRedemptionPK,
					  RedRemainingBalance,UnitRemainingToRedemp
			        )
			SELECT @CClientSubscriptionPK,@ClientRedemptionPK, 0,@UnitBefore
		END

			SET @CounterUnitBefore = @CounterUnit

		SELECT @MaxClientRedemptionPK = MAX(ClientRedemptionPK) FROM dbo.ClientRedemption
	WHERE FundPK = @CFundPK AND FundClientPK = @CFundClientPK AND Status NOT IN (3,4)  

	SELECT @MaxMappingClientRedemptionPK = MAX(ClientRedemptionPK) FROM @SubsRedemp
	END

	END


	Fetch Next From A 
	INTO @CClientSubscriptionPK,@CNAVDate,@CFundPK,@CFundClientPK,@CUnitAmount
End	
Close A
Deallocate A

--select * from @SubsRedemp

DECLARE @BackLoadTable TABLE
(
	ClientSubscriptionPK int,
	ClientRedemptionPK INT,
	RedRemainingBalance NUMERIC(22,4),
	SubsDate DATETIME,
	RedDate DATETIME,
	ClearDate DATETIME,

	HoldingPeriod int,
	FeePercent NUMERIC(18,4),
	SubsUnitAmount NUMERIC(22,4),
	RedUnitAmount NUMERIC(22,4),
	UnitRemainingToRedemp NUMERIC(22,4),
	
	RedempUnitFee NUMERIC(22,4)
)

DECLARE @ZClientSubscriptionPK INT
DECLARE @ZClientRedemptionPK int
DECLARE @ZSubsDate DATETIME
DECLARE @ZRedDate DATETIME
DECLARE @ZFundPK INT
DECLARE @ZRemRedBalance NUMERIC(22,4)
DECLARE @ZRedUnitAmount NUMERIC(22,4)
DECLARE @ZUnitToRedemp NUMERIC(22,4)
DECLARE @ZSubsUnitAmount NUMERIC(22,4)

Declare A Cursor For
	SELECT B.NAVDate,C.NAVDate,A.ClientSubscriptionPK,A.ClientRedemptionPK,C.FundPK 
	,A.RedRemainingBalance,C.UnitAmount,A.UnitRemainingToRedemp,B.TotalUnitAmount
	FROM @SubsRedemp A
	LEFT JOIN dbo.ClientSubscription B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK AND B.status NOT IN (3,4)
	LEFT JOIN dbo.ClientRedemption C ON A.ClientRedemptionPK = C.ClientRedemptionPK AND C.status NOT IN (3,4)
	WHERE A.ClientSubscriptionPK > 0 
	and B.FundClientPK = 
		(
			select distinct FundClientPK from ClientRedemption 
			where status not in (3,4) and valuedate between @dateFrom and @dateto
			and Selected = 1
		) 
		and B.FundPK =
		(
			select distinct FundPK from ClientRedemption 
			where status not in (3,4) and valuedate between @dateFrom and @dateto
			and Selected = 1
		
		)

			and C.FundClientPK = 
		(
			select distinct FundClientPK from ClientRedemption 
			where status not in (3,4) and valuedate between @dateFrom and @dateto
			and Selected = 1
		) 
		and C.FundPK =
		(
			select distinct FundPK from ClientRedemption 
			where status not in (3,4) and valuedate between @dateFrom and @dateto
			and Selected = 1
		
		)
Open A
Fetch Next From A
INTO @ZSubsDate,@ZRedDate,@ZClientSubscriptionPK,@ZClientRedemptionPK,@ZFundPK,@ZRemRedBalance,@ZRedUnitAmount,@ZUnitToRedemp,@ZSubsUnitAmount

While @@FETCH_STATUS = 0  
Begin
    
	INSERT INTO @BackLoadTable
	        ( SubsDate,
			  ClientSubscriptionPK ,
	          ClientRedemptionPK ,
	          RedRemainingBalance ,
	          HoldingPeriod ,
	          FeePercent ,
	          SubsUnitAmount ,
	          RedUnitAmount ,
	          UnitRemainingToRedemp ,
	          RedDate ,
	          ClearDate ,
	          RedempUnitFee
	        )
	
	SELECT  TOP 1 @ZSubsDate, @ZClientSubscriptionPK ClientSubscriptionPK
	,@ZClientRedemptionPK ClientRedemptionPK
	,@ZRemRedBalance RedRemBalance
	,HoldingPeriod
	,RedempFeePercent
	,@ZSubsUnitAmount SubsUnitAmount
	,@ZRedUnitAmount RedUnitAmount
	,@ZUnitToRedemp UnitRemainingToRedemp
	,@ZRedDate RedDate
	,DATEADD(MONTH,HoldingPeriod,@ZSubsDate) ClearDate
	,CASE WHEN @ZRedDate <= DATEADD(MONTH,HoldingPeriod,@ZSubsDate) THEN 		
		--CASE WHEN @ZUnitToRedemp = 0 THEN @ZRedUnitAmount * RedempFeePercent / 100 
		CASE WHEN @ZUnitToRedemp = 0 THEN IIF (@ZSubsUnitAmount <= @ZRedUnitAmount,  @ZSubsUnitAmount, @ZRedUnitAmount)  * RedempFeePercent / 100 
			ELSE @ZUnitToRedemp * RedempFeePercent / 100 END
	ELSE 0 
	END RedempUnitFee	
	FROM dbo.BackLoadSetup WHERE date =
	(
		SELECT MAX(Date) FROM dbo.BackLoadSetup WHERE FundPK = @ZFundPK AND Date <= @ZRedDate AND status = 2
	) AND status  = 2 AND FundPK = @ZFundPK
	
	ORDER BY HoldingPeriod desc
	
	FETCH Next From A 
	INTO @ZSubsDate,@ZRedDate,@ZClientSubscriptionPK,@ZClientRedemptionPK,@ZFundPK,@ZRemRedBalance,@ZRedUnitAmount,@ZUnitToRedemp,@ZSubsUnitAmount
End	
Close A
Deallocate A



Declare @RedempFee table
(
	ClientRedemptionPK int,
	FeePercent numeric(18,4)
)

insert into @RedempFee
select A.ClientRedemptionPK,sum(isnull(B.redempUnitFee,0))/A.UnitAmount * 100 FeePercent  from ClientRedemption A
inner join @BackLoadTable B on A.ClientRedemptionPK = B.ClientRedemptionPK
group by A.ClientRedemptionPK,A.UnitAmount

update A set RedemptionFeePercent = B.FeePercent  from ClientRedemption A
inner join @RedempFee B on A.ClientRedemptionPK = B.ClientRedemptionPK
where A.ClientRedemptionPK in 
(
select ClientRedemptionPK from ClientRedemption where Selected = 1 and ValueDate =  @CValueDate and status = 2 
) ";


                        

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@CValueDate", _date);
                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void InsertClientRedemptionAll_MaturedFund(string _usersID, int _fundPK, DateTime _date)
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

                            @"declare @FundClientPK int
                            declare @UnitAmount numeric(22,8)
                            declare @PaymentDate datetime
                            declare @ClientRedemptionPK int

                            declare @FCP table
                            (
                             FundClientPK int,
                             UnitAmount numeric(18,8)
                            )

                            insert into @FCP
                            SELECT FundClientPK, UnitAmount from FundClientPosition WHERE Date = 
                            (
                            SELECT MAX(date) FROM FundClientPosition WHERE Date <= @Date AND FundPK = @FundPK
                            ) AND FundPK = @FundPK and UnitAmount > 1


                            select @PaymentDate = dbo.FWorkingDay(@Date,DefaultPaymentDate) from Fund where status = 2 and FundPk = @FundPK

                             

                            Declare A Cursor For
                            select FundClientPK,UnitAmount from @FCP
                            Open A
                            Fetch Next From A
                            INTO @FundClientPK,@UnitAmount

                            While @@FETCH_STATUS = 0  
                            Begin

                            select @ClientRedemptionPk = isnull(max(ClientRedemptionPk),0) + 1 From ClientRedemption 
	
                            INSERT INTO ClientRedemption(ClientRedemptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,PaymentDate,NAV,
                            FundPK,FundClientPK,CashRefPK,BitRedemptionAll, Description,CashAmount,UnitAmount,TotalCashAmount,
                            TotalUnitAmount,RedemptionFeePercent,RedemptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,
                            CurrencyPK,UnitPosition,BankRecipientPK,TransferType,IsBOTransaction,BitSInvest,TransactionPK,IsFrontSync,
                            ReferenceSinvest,EntryUsersID,EntryTime,LastUpdate)
		
                            SELECT isnull(@ClientRedemptionPk,0) + 1,1,1,1,1,@Date,@Date,@PaymentDate,0,
                            @FundPK,@FundClientPK,1 ,1,'Redemption All Mature Fund',0,ISNULL(@UnitAmount,0),0,
                            ISNULL(@UnitAmount,0),0,0,0,0,0,0,
                            1,0,1,1,1,0,0,0,
                            '',@EntryUsersID,@EntryTime,@LastUpdate


                            Fetch next From A                   
                            INTO @FundClientPK,@UnitAmount        
                            END                  
                            Close A                  
                            Deallocate A
                             ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
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

        public Boolean CashInstruction_BySelectedData(string _userID, DateTime _DateFrom, DateTime _DateTo, ClientRedemption _clientRedemption)
        {

            //TRIAL BALANCE 




            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_clientRedemption.ClientRedemptionSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientRedemption.ClientRedemptionSelected))
                {
                    paramClientRedemptionSelected = "  ClientRedemptionPK in (" + _clientRedemption.ClientRedemptionSelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = "  ClientRedemptionPK in (0) ";
                }
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {





                        cmd.CommandText = @"
                          select case when isnull(D.ID,'') = '' or isnull(B.BankAccountNo,'') = '' then '' else D.ID + ' - ' +B.BankAccountNo end RekeningPengirim,
case when A.BankRecipientPK = 1 then isnull(B1.ID + ' - ' + E.NomorRekening1,'') when A.BankRecipientPK = 2 then isnull(B2.ID + ' - ' + E.NomorRekening2,'') when A.BankRecipientPK = 3 then isnull(B3.ID + ' - ' + E.NomorRekening3,'') else isnull(F.AccountNo,'') end RekeningTujuan,
TotalCashAmount Amount,A.Description Keterangan from ClientRedemption A
left join CashRef B on A.CashRefPK = B.CashRefPK
left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
left join Bank D on C.BankPK = D.BankPK and D.Status in (1,2)
left join FundClient E on A.FundClientPK = E.FundClientPK and E.status in (1,2)
left join Bank B1 on E.NamaBank1 = B1.BankPK and B1.status in (1,2)
left join Bank B2 on E.NamaBank2 = B2.BankPK and B2.status in (1,2)
left join Bank B3 on E.NamaBank3 = B3.BankPK and B3.status in (1,2)
left join FundClientBankList F on A.BankRecipientPK = F.NoBank and A.FundClientPK = F.FundClientPK and F.status in (1,2)
left join Bank B4 on F.BankPK = B4.BankPK and B4.status in (1,2)

where A.ValueDate = @Valuedate and " + paramClientRedemptionSelected
;

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDate", _DateFrom);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "CashInstruction" + "_" + _userID + ".xlsx";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "Cash Instruction";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Cash Instruction");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashInstruction> rList = new List<CashInstruction>();
                                    while (dr0.Read())
                                    {

                                        CashInstruction rSingle = new CashInstruction();

                                        rSingle.RekeningPengirim = Convert.ToString(dr0["RekeningPengirim"]);
                                        rSingle.RekeningTujuan = Convert.ToString(dr0["RekeningTujuan"]);
                                        rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                        rSingle.Keterangan = Convert.ToString(dr0["Keterangan"]);





                                        rList.Add(rSingle);

                                    }



                                    var GroupByReference =
                                            from r in rList
                                                //orderby r ascending
                                            group r by new { r } into rGroup
                                            select rGroup;

                                    int incRowExcel = 0;

                                    incRowExcel++;


                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                    worksheet.Cells[incRowExcel, 1].Value = "Rekening Pengirim";
                                    worksheet.Cells[incRowExcel, 2].Value = "Rekening Penerima";
                                    worksheet.Cells[incRowExcel, 3].Value = "Amount";
                                    worksheet.Cells[incRowExcel, 4].Value = "Keterangan";

                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Font.Size = 12;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;



                                    foreach (var rsHeader in GroupByReference)
                                    {





                                        incRowExcel = incRowExcel + 1;

                                        int first = incRowExcel;

                                        int no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;

                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.RekeningPengirim;
                                            worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.RekeningTujuan;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Keterangan;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;



                                        }



                                        //worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        //worksheet.Cells[incRowExcel, 5].Calculate();
                                        //int last = incRowExcel - 1;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;




                                        //foreach (var rsHeader in GroupByReference)
                                        //{

                                    }


                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel + 8, 8];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();




                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Cash Instruction";


                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();


                                    return true;
                                }

                            }
                        }
                    }
                }
            }

            catch (Exception err)
            {
                return false;
                throw err;
            }

        }

        public bool Validate_InsertClientRedemptionAllMaturedFund(int _fundPK, DateTime _date)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" IF Exists(select * From ClientRedemption where Status in (1,2) and ValueDate = @Date and FundPK = @FundPK) 
                         BEGIN Select 1 Result END ELSE  BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
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

        public void ClientRedemption_PostingJournalBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _parambitManageUR = "";

                        if (_bitManageUR == true)
                        {
                            _parambitManageUR = " and A.status = 2   and A.Posted = 0 and A.ClientRedemptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 2 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = " and A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @DateTo " + paramClientRedemptionSelected;
                        }



                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
       

--TEMP TABLE & VARIABEL GLOBAL
BEGIN

	Declare @DefaultBankAccountPK int
	set @DefaultBankAccountPK = 3

	DECLARE @FundJournalPKTemp INT
	SET @FundJournalPKTemp = 0

	Declare @PeriodPK int                  
	Select @PeriodPK = PeriodPK From Period where @DateFrom Between DateFrom and DateTo  and status = 2 

	declare @MaxFundJournalPK int
	select @MaxFundJournalPK = isnull(max(FundJournalPK),0) from FundJournal

    declare @NextWorkingDay date
    set @NextWorkingDay = dbo.FWorkingDay(@DateFrom,1)

    Declare @counterDate datetime    
	Declare @yesterday datetime     
	set @counterDate = @DateFrom
	

	CREATE TABLE #JournalHeader 
	(
		FundJournalPK INT,
		HistoryPK INT,
		Status INT,
		Notes NVARCHAR(1000),
		PeriodPK INT,
		ValueDate DATETIME,
		Type nvarchar(50),
		TrxNo INT,
		TrxName nvarchar(150),
		Reference nvarchar(75),
		Description nvarchar(1500)
	)

	CREATE CLUSTERED INDEX indx_JournalHeader ON #JournalHeader (FundJournalPK);

	CREATE TABLE #JournalDetail 
	(
		FundJournalPK INT,
		AutoNo INT,
		HistoryPK BIGINT,
		Status INT,
		FundJournalAccountPK  INT,
		CurrencyPK INT,
		FundPK INT,
		FundClientPK INT,
		InstrumentPK INT,
		DetailDescription nvarchar(1500),
		DebitCredit char(1),
		Amount NUMERIC(19,4),
		Debit NUMERIC(19,4),
		Credit NUMERIC(19,4),
		CurrencyRate NUMERIC(19,4),
		BaseDebit NUMERIC(19,4),
		BaseCredit NUMERIC(19,4)
	)

	CREATE CLUSTERED INDEX indx_JournalDetail ON #JournalDetail (FundJournalPK,AutoNo);

	CREATE TABLE #ClientRED
	(
		FundJournalPK int,
		NAVDate datetime,
		PaymentDate datetime,
		Reference nvarchar(100),
		ClientRedemptionPK int,
		FundPK int,
		FundClientPK int,
		FundCashRefPK int,
		TransactionPK nvarchar(200),
		Type int,
		TotalUnitAmount numeric(19,6),
		TotalCashAmount numeric(19,4),
		UnitAmount numeric(19,4),
		CashAmount numeric(19,4),
		RedemptionFeeAmount numeric(19,4)
	)

	CREATE CLUSTERED INDEX indx_ClientRED ON #ClientRED (FundJournalPK,FundPK,FundClientPK);

END

--JOURNAL
BEGIN
	insert into #ClientRED
	(
		FundJournalPK,
		NAVDate,
		PaymentDate,
		Reference,
		ClientRedemptionPK,
		FundPK,
		FundClientPK,
		FundCashRefPK,
		TransactionPK,
		Type,
		TotalUnitAmount,
		TotalCashAmount,
		UnitAmount,
		CashAmount,
		RedemptionFeeAmount
	)
	Select ROW_NUMBER() OVER	(ORDER BY A.ClientRedemptionPK ASC) Number ,
			A.NAVDate ,
			A.PaymentDate,
			'',
			A.ClientRedemptionPK ,
			A.FundPK ,
			A.FundClientPK ,
			A.CashRefPK,
			A.TransactionPK,
			A.Type,
			isnull(A.TotalUnitAmount,0),
			isnull(A.TotalCashAmount,0),
			isnull(A.UnitAmount,0),
			isnull(A.CashAmount,0),
			isnull(A.RedemptionFeeAmount,0)
	From ClientRedemption A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto
    and (( isnull(A.NAV,0) > 0 and A.Type <> 3 ) or A.Type = 3)  and A.UnitAmount > 0
	" + _parambitManageUR + @"
	order by Number asc

	--JOURNAL
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
							HistoryPK ,
							Status ,
							Notes ,
							PeriodPK ,
							ValueDate ,
							Type ,
							TrxNo ,
							TrxName ,
							Reference ,
							Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting From Redemption'
				,@PeriodPK,dbo.Fworkingday(A.NAVDate,1),3,A.ClientRedemptionPK,'REDEMPTION',isnull(A.Reference,''),'Redemption Client: ' + B.ID + ' - ' + B.Name 
		From #ClientRED A  
		left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
		--where A.FundType <> 10
		where A.TotalCashAmount > 0 and A.Type <> 3
		
		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,Redemption,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		,'Redemption Client: ' + E.ID + ' - ' + E.Name,'D',A.CashAmount
		,A.CashAmount,0
		,1
		,A.CashAmount,0
		From #ClientRED A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.Redemption = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
		Where A.TotalCashAmount > 0 and A.Type <> 3

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,B.PendingRedemption,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		,'Redemption Client: ' + D.ID + ' - ' + D.Name,'C',A.TotalCashAmount
		,0,A.TotalCashAmount
		,1
		,0,A.TotalCashAmount
		From #ClientRED A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PendingRedemption = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
		Where A.TotalCashAmount > 0 and A.Type <> 3

		--Jika ada fee
		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,3,1,2
		,B.payableRedemptionfee,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		,'Redemption Client: ' + D.ID + ' - ' + D.Name,'C',A.RedemptionFeeAmount
		,0,A.RedemptionFeeAmount
		,1
		,0,A.RedemptionFeeAmount
		From #ClientRED A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.payableRedemptionfee = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
		Where A.RedemptionFeeAmount > 0 and A.Type <> 3
	END

	--Journal Payable ke BANK (PAYMENT)
	BEGIN
		Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		INSERT INTO #JournalHeader
						( FundJournalPK ,
							HistoryPK ,
							Status ,
							Notes ,
							PeriodPK ,
							ValueDate ,
							Type ,
							TrxNo ,
							TrxName ,
							Reference ,
							Description
						)

		Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Payment From Redemption'
				,@PeriodPK,A.PaymentDate,3,A.ClientRedemptionPK,'REDEMPTION',isnull(A.Reference,''),'Redemption Client: ' + B.ID + ' - ' + B.Name 
		From #ClientRED A  
		left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
		--where A.FundType <> 10
		where A.TotalCashAmount > 0 and A.Type <> 3

		INSERT INTO #JournalDetail
						( FundJournalPK ,
							AutoNo ,
							HistoryPK ,
							Status ,
							FundJournalAccountPK ,
							CurrencyPK ,
							FundPK ,
							FundClientPK ,
							InstrumentPK ,
							DetailDescription ,
							DebitCredit ,
							Amount ,
							Debit ,
							Credit ,
							CurrencyRate ,
							BaseDebit ,
							BaseCredit
						)
		Select A.FundJournalPK + @FundJournalPKTemp,1,1,2
		,B.PendingRedemption,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		,'Redemption Client: ' + D.ID + ' - ' + D.Name,'D',A.TotalCashAmount
		,A.TotalCashAmount,0
		,1
		,A.TotalCashAmount,0
		From #ClientRED A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundJournalAccount C ON B.PendingRedemption = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
		Where A.TotalCashAmount > 0 and A.Type <> 3

		INSERT INTO #JournalDetail
					( FundJournalPK ,
						AutoNo ,
						HistoryPK ,
						Status ,
						FundJournalAccountPK ,
						CurrencyPK ,
						FundPK ,
						FundClientPK ,
						InstrumentPK ,
						DetailDescription ,
						DebitCredit ,
						Amount ,
						Debit ,
						Credit ,
						CurrencyRate ,
						BaseDebit ,
						BaseCredit
					)
		Select A.FundJournalPK + @FundJournalPKTemp,2,1,2
		,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPK,A.FundClientPK,0
		,'Redemption Client: ' + E.ID + ' - ' + E.Name,'C',A.TotalCashAmount
		,0,A.TotalCashAmount
		,1
		,0,A.TotalCashAmount
		From #ClientRED A  
		LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status = 2
		LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
		Where A.TotalCashAmount > 0 and A.Type <> 3
	END

	--LOGIC FUND CLIENT POSITION
    while @counterDate <= (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails Where status = 2 and Notes = 'Unit')
    begin
							
	    set @yesterday = dbo.FWorkingDay(@counterDate,-1)

	    if not exists (
		    Select @counterDate,A.FundClientPK,A.FundPK,A.CashAmount,A.UnitAmount From (
			    select FundClientPk,FundPK,sum(UnitAmount) UnitAmount,sum(CashAmount) CashAmount from #ClientRED
			    where NAVDate between @datefrom and @counterDate
			    group by FundClientPk,FundPK
		    )A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
	    )
	    begin
		    INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
		    select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientRED A
		    inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPK
		    where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

		    UPDATE B Set B.UnitAmount = B.UnitAmount - A.UnitAmount From (
			    select FundClientPk,FundPK,sum(UnitAmount) UnitAmount,sum(CashAmount) CashAmount from #ClientRED
			    where NAVDate = @counterDate
			    group by FundClientPk,FundPK
		    )A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
	    end
	    else
	    begin
		    UPDATE B Set B.UnitAmount = B.UnitAmount - A.UnitAmount From (
			    select FundClientPk,FundPK,sum(UnitAmount) UnitAmount,sum(CashAmount) CashAmount from #ClientRED
			    where NAVDate between @datefrom and @counterDate
			    group by FundClientPk,FundPK
		    )A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
	    end

								
	    set @counterDate = dbo.fworkingday(@counterDate,1)
    end
	
	-- UPDATE STATUS CLIENT REDEMPTION
	BEGIN
		update A set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
		FROM ClientRedemption A
		INNER JOIN #ClientRED B ON A.ClientRedemptionPK = B.ClientRedemptionPK
		where Status = 2 
	END

	--insert into journal
	Insert Into FundJournal(FundJournalPK,HistoryPK,Status,Notes,PeriodPK,ValueDate,Type,TrxNo,TrxName,Reference,Description,
	Posted,PostedBy,PostedTime,EntryUsersID,EntryTime,LastUpdate)

	select 	@MaxFundJournalPK + FundJournalPK,1,2,Notes,PeriodPK,ValueDate,Type,TrxNo,TrxName,Reference,Description,
	1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
	from #JournalHeader


	Insert Into FundJournalDetail(FundJournalPK,AutoNo,HistoryPK,Status,FundJournalAccountPK,CurrencyPK,FundPK,FundClientPK,InstrumentPK,DetailDescription,DebitCredit,
	Amount,Debit,Credit,CurrencyRate,BaseDebit,BaseCredit,LastUsersID,LastUpdate)

	select 	@MaxFundJournalPK + FundJournalPK,AutoNo,HistoryPK,2,FundJournalAccountPK,CurrencyPK,FundPK,FundClientPK,InstrumentPK,DetailDescription,DebitCredit,
	Amount,Debit,Credit,CurrencyRate,BaseDebit,BaseCredit,@PostedBy,@PostedTime
	from #JournalDetail

END
                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@PostedBy", _usersID);
                        cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_PostingUnitOnlyBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _parambitManageUR = "";

                        if (_bitManageUR == true)
                        {
                            _parambitManageUR = " and A.status = 2   and A.Posted = 0 and A.ClientRedemptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 2 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = " and A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @DateTo " + paramClientRedemptionSelected;
                        }



                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
       
                        --declare @datefrom date
                        --declare @dateto date
                        --declare @PostedBy nvarchar(20)
                        --declare @PostedTime datetime

                        --set @datefrom = '2020-05-28'
                        --set @dateto = @datefrom
                        ----set @dateto = '2020-05-26'
                        --set @PostedBy = 'admin'
                        --set @PostedTime = getdate()

                        --drop table #ClientRED



                        --TEMP TABLE & VARIABEL GLOBAL
                        BEGIN

	                        Declare @DefaultBankAccountPK int
	                        set @DefaultBankAccountPK = 3

	                        DECLARE @FundJournalPKTemp INT
	                        SET @FundJournalPKTemp = 0

	                        Declare @PeriodPK int                  
	                        Select @PeriodPK = PeriodPK From Period where @DateFrom Between DateFrom and DateTo  and status = 2 

	                        Declare @counterDate datetime    
	                        Declare @yesterday datetime     
	                        set @counterDate = @DateFrom
	
	                        CREATE TABLE #ClientRED
	                        (
		                        FundJournalPK int,
		                        NAVDate datetime,
		                        PaymentDate datetime,
		                        Reference nvarchar(100),
		                        ClientRedemptionPK int,
		                        FundPK int,
		                        FundClientPK int,
		                        FundCashRefPK int,
		                        TransactionPK nvarchar(200),
		                        Type int,
		                        TotalUnitAmount numeric(19,6),
		                        TotalCashAmount numeric(19,4),
		                        UnitAmount numeric(19,4),
		                        CashAmount numeric(19,4),
		                        RedemptionFeeAmount numeric(19,4)
	                        )

	                        CREATE CLUSTERED INDEX indx_ClientRED ON #ClientRED (FundJournalPK,FundPK,FundClientPK);

                        END

                        insert into #ClientRED
                        (
	                        FundJournalPK,
	                        NAVDate,
	                        PaymentDate,
	                        Reference,
	                        ClientRedemptionPK,
	                        FundPK,
	                        FundClientPK,
	                        FundCashRefPK,
	                        TransactionPK,
	                        Type,
	                        TotalUnitAmount,
	                        TotalCashAmount,
	                        UnitAmount,
	                        CashAmount,
	                        RedemptionFeeAmount
                        )
                        Select ROW_NUMBER() OVER	(ORDER BY A.ClientRedemptionPK ASC) Number ,
		                        A.NAVDate ,
		                        A.PaymentDate,
		                        '',
		                        A.ClientRedemptionPK ,
		                        A.FundPK ,
		                        A.FundClientPK ,
		                        A.CashRefPK,
		                        A.TransactionPK,
		                        A.Type,
		                        isnull(A.TotalUnitAmount,0),
		                        isnull(A.TotalCashAmount,0),
		                        isnull(A.UnitAmount,0),
		                        isnull(A.CashAmount,0),
		                        isnull(A.RedemptionFeeAmount,0)
                        From ClientRedemption A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                        Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto
                        and (( isnull(A.NAV,0) > 0 and A.Type <> 3 ) or A.Type = 3) and A.UnitAmount > 0
                        " + _parambitManageUR + @"
                        order by Number asc


                        --LOGIC FUND CLIENT POSITION
                        while @counterDate <= (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails Where status = 2 and Notes = 'Unit')
                        begin
							
	                        set @yesterday = dbo.FWorkingDay(@counterDate,-1)

	                        if not exists (
		                        Select @counterDate,A.FundClientPK,A.FundPK,A.CashAmount,A.UnitAmount From (
			                        select FundClientPk,FundPK,sum(UnitAmount) UnitAmount,sum(CashAmount) CashAmount from #ClientRED
			                        where NAVDate between @datefrom and @counterDate
			                        group by FundClientPk,FundPK
		                        )A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
	                        )
	                        begin
		                        INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
		                        select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientRED A
		                        inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPK
		                        where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

		                        UPDATE B Set B.UnitAmount = B.UnitAmount - A.UnitAmount From (
			                        select FundClientPk,FundPK,sum(UnitAmount) UnitAmount,sum(CashAmount) CashAmount from #ClientRED
			                        where NAVDate = @counterDate
			                        group by FundClientPk,FundPK
		                        )A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
	                        end
	                        else
	                        begin
		                        UPDATE B Set B.UnitAmount = B.UnitAmount - A.UnitAmount From (
			                        select FundClientPk,FundPK,sum(UnitAmount) UnitAmount,sum(CashAmount) CashAmount from #ClientRED
			                        where NAVDate between @datefrom and @counterDate
			                        group by FundClientPk,FundPK
		                        )A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
	                        end

								
	                        set @counterDate = dbo.fworkingday(@counterDate,1)
                        end
	
                        -- UPDATE STATUS CLIENT REDEMPTION
                        BEGIN
	                        update A set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
	                        FROM ClientRedemption A
	                        INNER JOIN #ClientRED B ON A.ClientRedemptionPK = B.ClientRedemptionPK
	                        where Status = 2 
                        END

                        
                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@PostedBy", _usersID);
                        cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<ValidateClientRedemption> ValidateClientRedemption(DateTime _valueDate, decimal _cashAmount, decimal _unitAmount, int _FundPK, int _fundClientPK, bool _bitRedempAll)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                List<ValidateClientRedemption> L_ValidateClientSubscription = new List<ValidateClientRedemption>();
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            



--declare @LastUpdate datetime
--declare @ClientCode nvarchar(100)
--declare @FundPK int
--declare @FundClientPK int
--declare @UnitAmount numeric(22,8)
--declare @CashAmount numeric(22,8)
--declare @ValueDate date
--declare @BitRedempAll int

--set @LastUpdate = getdate()
--set @ClientCode = '10'
--set @FundPK = 1
--set @FundClientPK = 8
--set @UnitAmount = 10
--set @CashAmount = 0 
--set @ValueDate = '2020-11-25'
--set @BitRedempAll = 0



declare @TableReason table
(
	Reason nvarchar(max),
	Notes nvarchar(100),
	InsertHighRisk int,
	Validate int
)

Declare @StringTimeNow nvarchar(8)
Declare @CutoffTime decimal(22,0)
Declare @DecTimeNow decimal(22,0)
Declare @paramCashAmount numeric(32,8)
Declare @paramUnitAmount numeric(32,8)
declare @CloseNAV numeric(22,8)
declare @MinBalRedAmount numeric(22,8)

-- InsertHighRisk 1 > Insert ke High Risk Monitoring
-- Validate dipakai untuk validasi mandatory untuk Notes

--1. check CutOffTime
begin
	SELECT @StringTimeNow = REPLACE(substring(CONVERT(nvarchar(8),@LastUpdate,108),0,9),':','')
	if @ClientCode = '10'
		select @CutoffTime = case when CutOffTime = '' then 0  else cast(CutOffTime as decimal(22,0)) end from Fund where FundPK = @FundPK and status in (1,2)
	else
		select @CutoffTime = case when isnull(EntryApproveTimeCutoff,'') = '' then 999999  else cast(EntryApproveTimeCutoff as decimal(22,0)) end from Fund where FundPK = @FundPK and status in (1,2)

	select @DecTimeNow = cast(@StringTimeNow as decimal(22,0))

	IF (@DecTimeNow > @CutoffTime)
	BEGIN
		insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
		select 'Transaction has passed the cut-off time','',0,0
	END
end

--2. check data double input
begin
	if exists(
	select * from ClientRedemption where FundClientPK=@FundClientPK and FundPK=@FundPK and UnitAmount = @UnitAmount
    and ValueDate between CAST(GETDATE() AS DATE) and CAST(GETDATE() AS DATE) )
	begin
		insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
		select 'You already input same data for this day','',0,0
	end
end

--3. check min redemp amount
begin
	select @CloseNAV = Nav from CloseNAV where status = 2 and fundpk = @FundPK and Date = (
		select max(date) from CloseNAV where status = 2 and fundpk = @FundPK and date <= @ValueDate
	)

	if @CashAmount > 0
		set @paramCashAmount = @CashAmount
	else
		set @paramCashAmount = @UnitAmount * @CloseNAV

	select @MinBalRedAmount = isnull(MinBalRedsAmt,0) from Fund where status = 2 and fundpk = @FundPK

	if @paramCashAmount < @MinBalRedAmount and @MinBalRedAmount <> 0
	begin
		insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
		select 'Minimum redemption amount < cash amount ,minimum redemption amount : ' + format(@MinBalRedAmount,'N') + ', cash amount : ' + format(@paramCashAmount,'N') ,'',0,0
	end

end

-- 4. validasi Check minimum trx <  Min remaining unit di table Fund (ALL CLIENT)
begin
	Declare @MaxEDT datetime
    Declare @RemainingBalance numeric(18,4)
    Select @MaxEDT = max(Valuedate) from EndDayTrails where status = 2 and ValueDate <= @ValueDate

	Select @RemainingBalance = isnull(A.UnitAmount,0) - isnull(B.UnitAmount,0) - isnull(C.UnitAmount,0)
    FROM FundClientPosition A
    left join (
	    SELECT FundClientPK,FundPK, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,FundPK) end,0)) UnitAmount FROM ClientRedemption  
	    WHERE status not in (3,4) and posted = 0 and Revised = 0 AND FundPK = @FundPK AND FundClientPK = @FundClientPK
	    group by FundClientPK,FundPK
    ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 

    left join 
    (
	    SELECT FundClientPK,FundPKFrom, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,FundPKFrom) end,0)) UnitAmount FROM ClientSwitching  
	    WHERE status not in (3,4) and posted = 0 and Revised = 0 AND FundPKFrom = @FundPK AND FundCLientPK = @FundClientPK
	    group by FundClientPK,FundPKFrom
    ) C on A.FundClientPK = C.FundClientPK and A.FundPK = C.FundPKFrom 
    where Date = @MaxEDT  AND A.FundPK = @FundPK AND A.FundClientPK = @FundClientPK

	Declare @RemainingBalanceUnit numeric(19,4)
	select @RemainingBalanceUnit = RemainingBalanceUnit from Fund where status in (1,2) and FundPK = @FundPK

	select @CloseNAV = Nav from CloseNAV where status = 2 and fundpk = @FundPK and Date = (
		select max(date) from CloseNAV where status = 2 and fundpk = @FundPK and date < @ValueDate
	)

    set @RemainingBalance = isnull(@RemainingBalance,0)

	if @UnitAmount = 0 
		set @paramUnitAmount = case when @CloseNAV = 0 then 0 else @CashAmount / @CloseNAV end
	else
		set @paramUnitAmount = @UnitAmount

	set @paramUnitAmount = @RemainingBalance - @paramUnitAmount

	if @paramUnitAmount < @RemainingBalanceUnit and @RemainingBalanceUnit <> 0 and @BitRedempAll = 0
	begin
		insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
		select 'This transaction less than remaining balance unit, minimum unit : ' + format(@RemainingBalanceUnit,'N')  + ', remaining unit amount : ' + format(@paramUnitAmount,'N'),'',1,1
	end

end


select ROW_NUMBER() over(order by Reason) No,* from @TableReason

                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FundPK);
                        cmd.Parameters.AddWithValue("@UnitAmount", _unitAmount);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@CashAmount", _cashAmount);
                        cmd.Parameters.AddWithValue("@BitRedempAll", _bitRedempAll);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ValidateClientRedemption M_ValidateClientSubscription = new ValidateClientRedemption();
                                    M_ValidateClientSubscription.No = Convert.ToInt32(dr["No"]);
                                    M_ValidateClientSubscription.Reason = dr["Reason"].ToString();
                                    M_ValidateClientSubscription.Notes = "";
                                    M_ValidateClientSubscription.InsertHighRisk = Convert.ToInt32(dr["InsertHighRisk"]);
                                    M_ValidateClientSubscription.Validate = Convert.ToInt32(dr["Validate"]);
                                    L_ValidateClientSubscription.Add(M_ValidateClientSubscription);
                                }
                            }
                            return L_ValidateClientSubscription;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Validate_EDTUnitBySelected(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"Declare @NAVDate datetime
                                                Declare A Cursor For              
                                                    Select distinct NAVDate From ClientRedemption Where status = 2 and Valuedate = @ValueDateFrom
                                                Open  A              
              
                                                Fetch Next From  A              
                                                into @NAVDate
                  
                                                While @@Fetch_Status = 0              
                                                BEGIN              
		                                                if not exists(Select * from EndDayTrails where status = 2 and Notes = 'Unit' and Valuedate = @NAVDate)
		                                                begin
			                                                select 1 result
		                                                end		
		                                                else
		                                                begin
			                                                select 0 result
		                                                end
                                                Fetch next From A                   
                                                    Into @NAVDate
                                                end                  
                                                Close A                  
                                                Deallocate A ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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


    }
}