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
    public class ClientSwitchingReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();
        //1
        string _insertCommand = "INSERT INTO [dbo].[ClientSwitching] " +
                            "([ClientSwitchingPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[PaymentDate]," +
                            "[NAVFundFrom],[NAVFundTo],[FundPKFrom],[FundPKTo],[FundClientPK],[CashRefPKFrom],[CashRefPKTo],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[SwitchingFeePercent],[SwitchingFeeAmount],[TotalCashAmountFundFrom],[TotalCashAmountFundTo],[TotalUnitAmountFundFrom], "+
                            "[TotalUnitAmountFundTo],[BitSwitchingAll],[TransferType],[FeeType],[FeeTypeMethod],[ReferenceSInvest],[AgentPK],[Type],";
        string _paramaterCommand = "@NAVDate,@ValueDate,@PaymentDate,@NAVFundFrom,@NAVFundTo,@FundPKFrom,@FundPKTo,@FundClientPK,@CashRefPKFrom,@CashRefPKTo,@CurrencyPK," +
                            "@Description,@CashAmount,@UnitAmount,@SwitchingFeePercent,@SwitchingFeeAmount,@TotalCashAmountFundFrom,@TotalCashAmountFundTo,@TotalUnitAmountFundFrom,@TotalUnitAmountFundTo,@BitSwitchingAll,@TransferType,@FeeType,@FeeTypeMethod,@ReferenceSInvest,@AgentPK,@Type,";


        //2
        private ClientSwitching setClientSwitching(SqlDataReader dr)
        {
            ClientSwitching M_ClientSwitching = new ClientSwitching();
            M_ClientSwitching.ClientSwitchingPK = Convert.ToInt32(dr["ClientSwitchingPK"]);
            M_ClientSwitching.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_ClientSwitching.Selected = Convert.ToBoolean(dr["Selected"]);
            M_ClientSwitching.Status = Convert.ToInt32(dr["Status"]);
            M_ClientSwitching.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_ClientSwitching.Notes = Convert.ToString(dr["Notes"]);
            M_ClientSwitching.Type = Convert.ToInt32(dr["Type"]);
            M_ClientSwitching.TypeDesc = Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TypeDesc"]);
            M_ClientSwitching.NAVDate = dr["NAVDate"].ToString();
            M_ClientSwitching.ValueDate = Convert.ToString(dr["ValueDate"]);
            M_ClientSwitching.PaymentDate = Convert.ToString(dr["PaymentDate"]);
            M_ClientSwitching.NAVFundFrom = Convert.ToDecimal(dr["NAVFundFrom"]);
            M_ClientSwitching.NAVFundTo = Convert.ToDecimal(dr["NAVFundTo"]);
            M_ClientSwitching.FundPKFrom = Convert.ToInt32(dr["FundPKFrom"]);
            M_ClientSwitching.FundIDFrom = Convert.ToString(dr["FundIDFrom"]);
            M_ClientSwitching.FundPKTo = Convert.ToInt32(dr["FundPKTo"]);
            M_ClientSwitching.FundIDTo = Convert.ToString(dr["FundIDTo"]);
            M_ClientSwitching.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_ClientSwitching.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_ClientSwitching.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_ClientSwitching.CashRefPKFrom = Convert.ToInt32(dr["CashRefPKFrom"]);
            M_ClientSwitching.CashRefIDFrom = Convert.ToString(dr["CashRefIDFrom"]);
            M_ClientSwitching.CashRefPKTo = Convert.ToInt32(dr["CashRefPKTo"]);
            M_ClientSwitching.CashRefIDTo = Convert.ToString(dr["CashRefIDTo"]);
            M_ClientSwitching.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_ClientSwitching.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_ClientSwitching.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_ClientSwitching.AgentID = Convert.ToString(dr["AgentID"]);
            M_ClientSwitching.TransferType = Convert.ToInt32(dr["TransferType"]);
            M_ClientSwitching.TransferTypeDesc = Convert.ToString(dr["TransferTypeDesc"]);
            M_ClientSwitching.Description = Convert.ToString(dr["Description"]);
            M_ClientSwitching.ReferenceSInvest = Convert.ToString(dr["ReferenceSInvest"]);
            M_ClientSwitching.CashAmount = Convert.ToDecimal(dr["CashAmount"]);
            M_ClientSwitching.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            M_ClientSwitching.FeeType = Convert.ToString(dr["FeeType"]);
            M_ClientSwitching.SwitchingFeePercent = Convert.ToDecimal(dr["SwitchingFeePercent"]);
            M_ClientSwitching.SwitchingFeeAmount = Convert.ToDecimal(dr["SwitchingFeeAmount"]);
            M_ClientSwitching.TotalCashAmountFundFrom = Convert.ToDecimal(dr["TotalCashAmountFundFrom"]);
            M_ClientSwitching.TotalCashAmountFundTo = Convert.ToDecimal(dr["TotalCashAmountFundTo"]);
            M_ClientSwitching.TotalUnitAmountFundFrom = Convert.ToDecimal(dr["TotalUnitAmount"]);
            M_ClientSwitching.TotalUnitAmountFundTo = Convert.ToDecimal(dr["TotalUnitAmountFundTo"]);
            M_ClientSwitching.BitSwitchingAll = Convert.ToBoolean(dr["BitSwitchingAll"]);
            M_ClientSwitching.FeeTypeMethod = Convert.ToInt32(dr["FeeTypeMethod"]);
            M_ClientSwitching.FeeTypeMethodDesc = Convert.ToString(dr["FeeTypeMethodDesc"]);
            M_ClientSwitching.FundNameFrom = Convert.ToString(dr["FundNameFrom"]);
            M_ClientSwitching.FundNameTo = Convert.ToString(dr["FundNameTo"]);
            M_ClientSwitching.Posted = Convert.ToBoolean(dr["Posted"]);
            M_ClientSwitching.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_ClientSwitching.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_ClientSwitching.Revised = Convert.ToBoolean(dr["Revised"]);
            M_ClientSwitching.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_ClientSwitching.RevisedTime = dr["RevisedTime"].ToString();
            M_ClientSwitching.EntryUsersID = dr["EntryUsersID"].ToString();
            M_ClientSwitching.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_ClientSwitching.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_ClientSwitching.VoidUsersID = dr["VoidUsersID"].ToString();
            M_ClientSwitching.EntryTime = dr["EntryTime"].ToString();
            M_ClientSwitching.UpdateTime = dr["UpdateTime"].ToString();
            M_ClientSwitching.ApprovedTime = dr["ApprovedTime"].ToString();
            M_ClientSwitching.VoidTime = dr["VoidTime"].ToString();
            M_ClientSwitching.DBUserID = dr["DBUserID"].ToString();
            M_ClientSwitching.DBTerminalID = dr["DBTerminalID"].ToString();
            M_ClientSwitching.LastUpdate = dr["LastUpdate"].ToString();
            M_ClientSwitching.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            M_ClientSwitching.TransactionPK = dr["TransactionPK"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TransactionPK"]);
            M_ClientSwitching.UserSwitchingPK = dr["UserSwitchingPK"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UserSwitchingPK"]);
            M_ClientSwitching.IFUACode = dr["IFUACode"].ToString();
            M_ClientSwitching.FrontID = dr["FrontID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FrontID"]);
            return M_ClientSwitching;
        }

        //3
        public List<ClientSwitching> ClientSwitching_Select(int _status)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientSwitching> L_clientSwitching = new List<ClientSwitching>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select F.ID CurrencyID,B.ID FundIDFrom,C.ID FundIDTo, D.ID FundClientID,D.Name FundClientName, E.ID CashRefIDFrom, G.ID CashRefIDTo,Case when FeeTypeMethod = 1 then 'Percent' else 'Amount' End FeeTypeMethodDesc , H.Name AgentID , MV.DescOne TypeDesc, A.* from ClientSwitching A 
                                left join Fund B on A.FundPKFrom = B.FundPK and B.Status =2 
                                left join Fund C on A.FundPKTo = C.FundPK and C.Status =2 
                                left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status =2  
                                left join CashRef E on A.CashRefPKFrom = E.CashRefPK and E.Status =2 
                                left join CashRef G on A.CashRefPKTo = G.CashRefPK and G.Status =2 
                                left join Currency F on A.CurrencyPK = F.CurrencyPK and F.Status =2 
                                left join Agent H on A.AgentPK = H.AgentPK and F.Status =2
                                left join MasterValue MV on A.Type = MV.Code and MV.Status =2 and MV.ID ='SubscriptionType' 
                                where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select F.ID CurrencyID,B.ID FundIDFrom,C.ID FundIDTo, D.ID FundClientID,D.Name FundClientName, E.ID CashRefIDFrom, G.ID CashRefIDTo,Case when FeeTypeMethod = 1 then 'Percent' else 'Amount' End FeeTypeMethodDesc , H.Name AgentID , MV.DescOne TypeDesc, A.* from ClientSwitching A 
                                left join Fund B on A.FundPKFrom = B.FundPK and B.Status =2 
                                left join Fund C on A.FundPKTo = C.FundPK and C.Status =2 
                                left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status =2  
                                left join CashRef E on A.CashRefPKFrom = E.CashRefPK and E.Status =2 
                                left join CashRef G on A.CashRefPKTo = G.CashRefPK and G.Status =2 
                                left join Currency F on A.CurrencyPK = F.CurrencyPK and F.Status =2
                                left join MasterValue MV on A.Type = MV.Code and MV.Status =2 and MV.ID ='SubscriptionType'   ";

                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_clientSwitching.Add(setClientSwitching(dr));
                                }
                            }
                            return L_clientSwitching;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //5
        public int ClientSwitching_Add(ClientSwitching _clientSwitching, bool _havePrivillege)
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
                                 "Select isnull(max(ClientSwitchingPk),0) + 1,1,@status," + _paramaterCommand + "1,@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From ClientSwitching";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = @"Declare @ClientSwitchingPK int   
                                    Select @ClientSwitchingPK = Max(ClientSwitchingPK) + 1 From ClientSwitching
                                    set @ClientSwitchingPK = isnull(@ClientSwitchingPK,1) 
                                " + _insertCommand + "[IsBOTransaction],[EntryUsersID],[EntryTime],[LastUpdate])" +
                                 @"  
                                
                            Select @ClientSwitchingPK,1,@status," + _paramaterCommand + "1,@EntryUsersID,@EntryTime,@LastUpdate" +
                                @"

                                   Declare @LastNAV numeric(22,8)
		                           Declare @SwitchingUnit numeric(22,8)
		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @FundPKFrom and status = 2 and Date < @NAVDate
		                           ) and status = 2 and FundPK = @FundPKFrom

		                           --set @LastNAV = isnull(@LastNAV,1)
--		                           set @SwitchingUnit = 0
--		                           IF @CashAmount > 0 and @NAVFundFrom = 0
--		                           BEGIN
--				                        set @SwitchingUnit = @CashAmount / @LastNAV
--		                           END
--		                           ELSE
--		                           BEGIN
--				                        set @SwitchingUnit = @UnitAmount
--		                           END

--                                    set @SwitchingUnit = @SwitchingUnit * -1

--                                    Declare @UnitPrevious numeric(22,8)
--                                    set @UnitPrevious = 0
--                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                    where FundClientPK = @FundClientPK and FundPK = @FundPKFrom

--                                    set @UnitPrevious = isnull(@UnitPrevious,0)

                                    --update fundclientpositionsummary
--		                           set Unit = Unit + @SwitchingUnit
--		                           where FundClientPK = @FundClientPK and FundPK = @FundPKFrom
                               
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
--                                    Select @EntryUsersID,@FundClientPK,NULL,@ClientSwitchingPK,@UnitPrevious,@SwitchingUnit,@UnitPrevious + @SwitchingUnit,1,6,'Add Switching out',@FundPKFrom

                                "

                                ;
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Type", _clientSwitching.Type);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                        cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                        cmd.Parameters.AddWithValue("@PaymentDate", _clientSwitching.PaymentDate);
                        cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                        cmd.Parameters.AddWithValue("@NAVFundTo", _clientSwitching.NAVFundTo);
                        cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                        cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                        cmd.Parameters.AddWithValue("@CashRefPKFrom", _clientSwitching.CashRefPKFrom);
                        cmd.Parameters.AddWithValue("@CashRefPKTo", _clientSwitching.CashRefPKTo);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _clientSwitching.CurrencyPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _clientSwitching.AgentPK);
                        cmd.Parameters.AddWithValue("@TransferType", _clientSwitching.TransferType);
                        cmd.Parameters.AddWithValue("@Description", _clientSwitching.Description);
                        cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSwitching.ReferenceSInvest);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                        cmd.Parameters.AddWithValue("@FeeType", _clientSwitching.FeeType);
                        cmd.Parameters.AddWithValue("@SwitchingFeePercent", _clientSwitching.SwitchingFeePercent);
                        cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _clientSwitching.SwitchingFeeAmount);
                        cmd.Parameters.AddWithValue("@TotalCashAmountFundFrom", _clientSwitching.TotalCashAmountFundFrom);
                        cmd.Parameters.AddWithValue("@TotalCashAmountFundTo", _clientSwitching.TotalCashAmountFundTo);
                        cmd.Parameters.AddWithValue("@TotalUnitAmountFundFrom", _clientSwitching.TotalUnitAmountFundFrom);
                        cmd.Parameters.AddWithValue("@TotalUnitAmountFundTo", _clientSwitching.TotalUnitAmountFundTo);
                        cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                        cmd.Parameters.AddWithValue("@FeeTypeMethod", _clientSwitching.FeeTypeMethod);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _clientSwitching.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ClientSwitching");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //6
        public int ClientSwitching_Update(ClientSwitching _clientSwitching, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int _return = 0;
                int status = _host.Get_Status(_clientSwitching.ClientSwitchingPK, _clientSwitching.HistoryPK, "ClientSwitching");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update ClientSwitching set status=2, Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,PaymentDate=@PaymentDate,Type=@Type,
                                NAVFundFrom=@NAVFundFrom,NAVFundTo=@NAVFundTo,FundPKFrom=@FundPKFrom,FundPKTo=@FundPKTo,FundClientPK=@FundClientPK,CashRefPKFrom=@CashRefPKFrom,CashRefPKTo=@CashRefPKTo,CurrencyPK=@CurrencyPK,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,SwitchingFeePercent=@SwitchingFeePercent,SwitchingFeeAmount=@SwitchingFeeAmount,TotalCashAmountFundFrom=@TotalCashAmountFundFrom,TotalCashAmountFundTo=@TotalCashAmountFundTo,TotalUnitAmountFundFrom=@TotalUnitAmountFundFrom,TotalUnitAmountFundTo=@TotalUnitAmountFundTo,BitSwitchingAll=@BitSwitchingAll,TransferType=@TransferType,FeeType = @FeeType,
                                FeeTypeMethod=@FeeTypeMethod,ReferenceSInvest = @ReferenceSInvest,                                
                                ApprovedUsersID=@ApprovedUsersID, 
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where ClientSwitchingPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                            cmd.Parameters.AddWithValue("@Type", _clientSwitching.Type);
                            cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                            cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                            cmd.Parameters.AddWithValue("@PaymentDate", _clientSwitching.PaymentDate);
                            cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                            cmd.Parameters.AddWithValue("@NAVFundTo", _clientSwitching.NAVFundTo);
                            cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                            cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                            cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                            cmd.Parameters.AddWithValue("@CashRefPKFrom", _clientSwitching.CashRefPKFrom);
                            cmd.Parameters.AddWithValue("@CashRefPKTo", _clientSwitching.CashRefPKTo);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _clientSwitching.CurrencyPK);
                            cmd.Parameters.AddWithValue("@AgentPK", _clientSwitching.AgentPK);
                            cmd.Parameters.AddWithValue("@TransferType", _clientSwitching.TransferType);
                            cmd.Parameters.AddWithValue("@Description", _clientSwitching.Description);
                            cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSwitching.ReferenceSInvest);
                            cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                            cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                            cmd.Parameters.AddWithValue("@FeeType", _clientSwitching.FeeType);
                            cmd.Parameters.AddWithValue("@SwitchingFeePercent", _clientSwitching.SwitchingFeePercent);
                            cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _clientSwitching.SwitchingFeeAmount);
                            cmd.Parameters.AddWithValue("@TotalCashAmountFundFrom", _clientSwitching.TotalCashAmountFundFrom);
                            cmd.Parameters.AddWithValue("@TotalCashAmountFundTo", _clientSwitching.TotalCashAmountFundTo);
                            cmd.Parameters.AddWithValue("@TotalUnitAmountFundFrom", _clientSwitching.TotalUnitAmountFundFrom);
                            cmd.Parameters.AddWithValue("@TotalUnitAmountFundTo", _clientSwitching.TotalUnitAmountFundTo);
                            cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                            cmd.Parameters.AddWithValue("@FeeTypeMethod", _clientSwitching.FeeTypeMethod);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClientSwitching set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ClientSwitchingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _clientSwitching.EntryUsersID);
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
		                           Declare @SwitchingUnit numeric(22,8)


                                    Declare @OldSwitchingUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(200) 
                                    declare @TransactionPK  nvarchar(200)
                                    Select @OldFundPK = FundPKFrom,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@OldNAV = NAVFundFrom, @TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK
                                    From ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                    and ClientTransactionPK = @PK and TransactionType = 6 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 6
                                    )

                                    Set @OldCashAmount = 0


		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
		                           ) and status = 2 and FundPK = @OldFundPK

		                           --set @LastNAV = isnull(@LastNAV,1)
--		                           set @OldSwitchingUnit = 0
--		                           IF @OldCashAmount > 0 and @oldNAV = 0
--		                           BEGIN
--				                        set @OldSwitchingUnit = @OldCashAmount / @LastNAV
--		                           END
--		                           ELSE
--		                           BEGIN
--				                        set @OldSwitchingUnit = @OldUnitAmount
--		                           END

--                                    Declare @UnitPrevious numeric(22,8)
--                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                    --update fundclientpositionsummary
--		                           set Unit = Unit + @OldSwitchingUnit
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
--                                    Select @UpdateUsersID,@OldFundClientPK,@TransactionPK ,@PK,@UnitPrevious,@OldSwitchingUnit,@UnitPrevious + @OldSwitchingUnit
--                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Update Switching Out Old Data Revise',@OldFundPK
                                ";
                            cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);


                            cmd.ExecuteNonQuery();
                        }



                        if (status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update ClientSwitching set Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,PaymentDate=@PaymentDate,AgentPK=@AgentPK,Type=@Type,
                                NAVFundFrom=@NAVFundFrom,NAVFundTo=@NAVFundTo,FundPKFrom=@FundPKFrom,FundPKTo=@FundPKTo,FundClientPK=@FundClientPK,CashRefPKFrom=@CashRefPKFrom,CashRefPKTo=@CashRefPKTo,CurrencyPK=@CurrencyPK,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,SwitchingFeePercent=@SwitchingFeePercent,SwitchingFeeAmount=@SwitchingFeeAmount,TotalCashAmountFundFrom=@TotalCashAmountFundFrom,TotalCashAmountFundTo=@TotalCashAmountFundTo,TotalUnitAmountFundFrom=@TotalUnitAmountFundFrom,TotalUnitAmountFundTo=@TotalUnitAmountFundTo,BitSwitchingAll=@BitSwitchingAll,TransferType=@TransferType,
                                FeeTypeMethod=@FeeTypeMethod,ReferenceSInvest = @ReferenceSInvest,FeeType = @FeeType,                        
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where ClientSwitchingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                                cmd.Parameters.AddWithValue("@Type", _clientSwitching.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _clientSwitching.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                                cmd.Parameters.AddWithValue("@NAVFundTo", _clientSwitching.NAVFundTo);
                                cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                                cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPKFrom", _clientSwitching.CashRefPKFrom);
                                cmd.Parameters.AddWithValue("@CashRefPKTo", _clientSwitching.CashRefPKTo);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientSwitching.CurrencyPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientSwitching.AgentPK);
                                cmd.Parameters.AddWithValue("@TransferType", _clientSwitching.TransferType);
                                cmd.Parameters.AddWithValue("@Description", _clientSwitching.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSwitching.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                                cmd.Parameters.AddWithValue("@FeeType", _clientSwitching.FeeType);
                                cmd.Parameters.AddWithValue("@SwitchingFeePercent", _clientSwitching.SwitchingFeePercent);
                                cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _clientSwitching.SwitchingFeeAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmountFundFrom", _clientSwitching.TotalCashAmountFundFrom);
                                cmd.Parameters.AddWithValue("@TotalCashAmountFundTo", _clientSwitching.TotalCashAmountFundTo);
                                cmd.Parameters.AddWithValue("@TotalUnitAmountFundFrom", _clientSwitching.TotalUnitAmountFundFrom);
                                cmd.Parameters.AddWithValue("@TotalUnitAmountFundTo", _clientSwitching.TotalUnitAmountFundTo);
                                cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                                cmd.Parameters.AddWithValue("@FeeTypeMethod", _clientSwitching.FeeTypeMethod);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_clientSwitching.ClientSwitchingPK, "ClientSwitching");
                                cmd.CommandText = _insertCommand + "UserSwitchingPK,TransactionPK,[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "@UserSwitchingPK,@TransactionPK,EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ClientSwitching where ClientSwitchingPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Type", _clientSwitching.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _clientSwitching.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                                cmd.Parameters.AddWithValue("@NAVFundTo", _clientSwitching.NAVFundTo);
                                cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                                cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPKFrom", _clientSwitching.CashRefPKFrom);
                                cmd.Parameters.AddWithValue("@CashRefPKTo", _clientSwitching.CashRefPKTo);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientSwitching.CurrencyPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientSwitching.AgentPK);
                                cmd.Parameters.AddWithValue("@TransferType", _clientSwitching.TransferType);
                                cmd.Parameters.AddWithValue("@Description", _clientSwitching.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSwitching.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                                cmd.Parameters.AddWithValue("@FeeType", _clientSwitching.FeeType);
                                cmd.Parameters.AddWithValue("@SwitchingFeePercent", _clientSwitching.SwitchingFeePercent);
                                cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _clientSwitching.SwitchingFeeAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmountFundFrom", _clientSwitching.TotalCashAmountFundFrom);
                                cmd.Parameters.AddWithValue("@TotalCashAmountFundTo", _clientSwitching.TotalCashAmountFundTo);
                                cmd.Parameters.AddWithValue("@TotalUnitAmountFundFrom", _clientSwitching.TotalUnitAmountFundFrom);
                                cmd.Parameters.AddWithValue("@TotalUnitAmountFundTo", _clientSwitching.TotalUnitAmountFundTo);
                                cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                                cmd.Parameters.AddWithValue("@FeeTypeMethod", _clientSwitching.FeeTypeMethod);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@TransactionPK", _clientSwitching.TransactionPK);
                                cmd.Parameters.AddWithValue("@UserSwitchingPK", _clientSwitching.UserSwitchingPK);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ClientSwitching set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where ClientSwitchingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
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
		                           Declare @SwitchingUnit numeric(22,8)
                                    Declare @TrxFrom nvarchar(200)
                                    declare @TransactionPK  nvarchar(200)
                                    Select @TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK from ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK
                                  Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @FundPKFrom and status = 2 and Date < @NAVDate
		                           ) and status = 2 and FundPK = @FundPKFrom

		                           --set @LastNAV = isnull(@LastNAV,1)
--		                           set @SwitchingUnit = 0
--		                           IF @CashAmount > 0 and @NAVFundFrom = 0
--		                           BEGIN
--				                        set @SwitchingUnit = @CashAmount / @LastNAV
--		                           END
--		                           ELSE
--		                           BEGIN
--				                        set @SwitchingUnit = @UnitAmount
--		                           END
--                                   set @SwitchingUnit = @SwitchingUnit * -1
--                                    Declare @UnitPrevious numeric(22,8)
--                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                    where FundClientPK = @FundClientPK and FundPK = @FundPKFrom

                                    --update fundclientpositionsummary
--		                           set Unit = Unit + @SwitchingUnit
--		                           where FundClientPK = @FundClientPK and FundPK = @FundPKFrom
                               
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
--                                    Select @UpdateUsersID,@FundClientPK,@TransactionPK ,@PK,@UnitPrevious,@SwitchingUnit,@UnitPrevious + @SwitchingUnit
--                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Update Switching Out',@FundPKFrom

                                ";
                        cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                        cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);

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

        //7
        public void ClientSwitching_Approved(ClientSwitching _clientSwitching)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientSwitching set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where ClientSwitchingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _clientSwitching.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientSwitching.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClientSwitching set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ClientSwitchingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _clientSwitching.ApprovedUsersID);
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

        //8
        public void ClientSwitching_Reject(ClientSwitching _clientSwitching)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientSwitching set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where ClientSwitchingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _clientSwitching.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _clientSwitching.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClientSwitching set status= 2,LastUpdate=@lastUpdate where ClientSwitchingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
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
        
        //9
        public void ClientSwitching_Void(ClientSwitching _clientSwitching)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientSwitching set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where ClientSwitchingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _clientSwitching.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _clientSwitching.VoidUsersID);
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

        // 10 AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )
        public List<ClientSwitching> ClientSwitching_SelectClientSwitchingDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientSwitching> L_clientSwitching = new List<ClientSwitching>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, isnull(MV1.DescOne,'') TypeDesc, F.ID CurrencyID,B.ID FundIDFrom,C.ID FundIDTo,B.Name FundNameFrom,C.Name FundNameTo, D.ID FundClientID,D.Name FundClientName, E.ID CashRefIDFrom,G.ID CashRefIDTo,MV.DescOne TransferTypeDesc,Case when FeeTypeMethod = 1 then 'Percent' else 'Amount' End FeeTypeMethodDesc , A.UnitAmount TotalUnitAmount, 
	                            A.NAVFundFrom NAVFrom, A.NAVFundTo NAVTo, isnull(H.Name,'') AgentID, D.IFUACode IFUACode,A.TransactionPK FrontID, A.* from ClientSwitching A 
	                            left join Fund B on A.FundPKFrom = B.FundPK and B.Status =2 
	                            left join Fund C on A.FundPKTo = C.FundPK and C.Status =2 
	                            left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)  
	                            left join FundCashRef E on A.CashRefPKFrom = E.FundCashRefPK and E.Status =2 
	                            left join FundCashRef G on A.CashRefPKTo = G.FundCashRefPK and G.Status =2 
	                            left join Currency F on A.CurrencyPK = F.CurrencyPK and F.Status =2 
	                            left join Agent H on A.AgentPK = H.AgentPK and H.Status =2 
	                            left join MasterValue MV on A.TransferType = MV.Code and MV.id = 'TransferTypeRedemption'
	                            left join MasterValue MV1 on A.Type = MV1.Code and MV1.Status =2 and MV1.ID ='SubscriptionType' 
                                where  A.status = @status and A.ValueDate between @DateFrom and @DateTo ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, isnull(MV1.DescOne,'') TypeDesc, F.ID CurrencyID,B.ID FundIDFrom,C.ID FundIDTo,B.Name FundNameFrom,C.Name FundNameTo, D.ID FundClientID,D.Name FundClientName, E.ID CashRefIDFrom,G.ID CashRefIDTo,MV.DescOne TransferTypeDesc,Case when FeeTypeMethod = 1 then 'Percent' else 'Amount' End FeeTypeMethodDesc , A.UnitAmount TotalUnitAmount, 
	                        A.NAVFundFrom NAVFrom, A.NAVFundTo NAVTo, isnull(H.Name,'') AgentID, D.IFUACode IFUACode,A.TransactionPK FrontID, A.* from ClientSwitching A 
	                        left join Fund B on A.FundPKFrom = B.FundPK and B.Status =2 
	                        left join Fund C on A.FundPKTo = C.FundPK and C.Status =2 
	                        left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)  
	                        left join FundCashRef E on A.CashRefPKFrom = E.FundCashRefPK and E.Status =2 
	                        left join FundCashRef G on A.CashRefPKTo = G.FundCashRefPK and G.Status =2 
	                        left join Currency F on A.CurrencyPK = F.CurrencyPK and F.Status =2 
	                        left join Agent H on A.AgentPK = H.AgentPK and H.Status =2 
	                        left join MasterValue MV on A.TransferType = MV.Code and MV.id = 'TransferTypeRedemption'
	                        left join MasterValue MV1 on A.Type = MV1.Code and MV1.Status =2 and MV1.ID ='SubscriptionType'  
                                where A.ValueDate between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_clientSwitching.Add(setClientSwitching(dr));
                                }
                            }
                            return L_clientSwitching;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public ClientSwitchingRecalculate ClientSwitching_Recalculate(ParamClientSwitchingRecalculate _param)
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
         

--declare @FundPKFrom int
--declare @NAVDate date
--declare @paramCashAmount numeric(22,8)
--declare @paramSwitchingFeeAmount numeric(22,8)
--declare @paramSwitchingFeePercent numeric(22,8)
--declare @FeeTypeMode int
--declare @paramUnitAmount numeric(22,8)
--declare @FundPKTo int
--declare @FeeType nvarchar(100)
--declare @paramSwitchingAll int
--declare @ClientCode nvarchar(100)

--set @FeeType = 'IN'
--set @FundPKFrom = 1
--set @FundPKTo = 42
--set @NAVDate = '2020-09-24'
--set @paramSwitchingFeePercent = 1
--set @paramSwitchingFeeAmount = 0
--set @paramUnitAmount = 395968.6770	
--set @paramCashAmount = 0
--set @FeeTypeMode = 1
--set @paramSwitchingAll = 0
--set @ClientCode = '01'


DECLARE @JournalRoundingMode int          
Declare @NAVRoundingModeFundFrom int 
Declare @NAVRoundingModeFundTo int 
Declare @JournalDecimalPlaces int 
Declare @NAVDecimalPlacesFundFrom int
Declare @NAVDecimalPlacesFundTo int
Declare @UnitDecimalPlacesFundFrom int
Declare @UnitDecimalPlacesFundTo int
Declare @NavFundFrom numeric(22,8)
Declare @NavFundTo numeric(22,8)
Declare @UnitRoundingModeFundFrom int
Declare @UnitRoundingModeFundTo int
Declare @CashAmount numeric(24,4)
Declare @UnitAmount numeric(18,8)
Declare @FinalFeeAmount numeric(22,4)
Declare @FinalFeePercent numeric(18,8)

Declare @TotalCashAmountFundFrom numeric(24,4)
Declare @TotalCashAmountFundTo numeric(22,4)
Declare @TotalUnitAmountFundFrom numeric(22,8)
Declare @TotalUnitAmountFundTo numeric(22,8)

declare @CashAmountTo numeric(22,4)

		select @JournalRoundingMode = isnull(C.JournalRoundingMode,3), @JournalDecimalPlaces = ISNULL(C.JournalDecimalPlaces,4) from Fund A
	    Left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2
	    Left join Bank C on B.BankPK = C.BankPK and C.Status = 2 
		where FundPK = @FundPKFrom

	    Select @NAVRoundingModeFundFrom = A.NAVRoundingMode, @NAVDecimalPlacesFundFrom= A.NAVDecimalPlaces,@UnitDecimalPlacesFundFrom = A.UnitDecimalPlaces 
	    ,@UnitRoundingModeFundFrom = A.UnitRoundingMode
	    From Fund A Where A.FundPK = @FundPKFrom and A.Status = 2
		
	    Select @NAVRoundingModeFundTo = A.NAVRoundingMode, @NAVDecimalPlacesFundTo= A.NAVDecimalPlaces,@UnitDecimalPlacesFundTo = A.UnitDecimalPlaces ,
	    @UnitRoundingModeFundTo = A.UnitRoundingMode
	    From Fund A Where A.FundPK = @FundPKTo and A.Status = 2

	    Select @NavFundFrom = isnull(Nav,0) From CloseNAV Where Date = @NAVDate and Status = 2 and FundPK = @FundPKFrom
	    Select @NavFundTo = isnull(Nav,0) From CloseNAV Where Date = @NAVDate and Status = 2 and FundPK = @FundPKTo

		--select @paramSwitchingFeePercent,@paramCashAmount cash,@paramUnitAmount unit,@NavFundFrom navfrom, @NavFundTo navto

		--handle rounding NAV dsni
	    If @NAVRoundingModeFundFrom = 1 
	    BEGIN 
		    Set  @NavFundFrom = ROUND(@NavFundFrom,@NAVDecimalPlacesFundFrom) 
		    IF @NAVDecimalPlacesFundFrom = 0 BEGIN
		    set @NavFundFrom = @NavFundFrom + 1
		    END

		    IF @NAVDecimalPlacesFundFrom = 2 BEGIN
		    set @NavFundFrom = @NavFundFrom + 0.01
		    END
		    IF @NAVDecimalPlacesFundFrom = 4 BEGIN
		    set @NavFundFrom = @NavFundFrom + 0.0001
		    END
		    IF @NAVDecimalPlacesFundFrom = 6 BEGIN
		    set @NavFundFrom = @NavFundFrom + 0.000001
		    END
		    IF @NAVDecimalPlacesFundFrom = 8 BEGIN
		    set @NavFundFrom = @NavFundFrom + 0.00000001
		    END
	    END
	    If @NAVRoundingModeFundFrom = 2 BEGIN Set  @NavFundFrom = ROUND(@NavFundFrom,@NAVDecimalPlacesFundFrom,1) END
	    If @NAVRoundingModeFundFrom = 3 BEGIN Set  @NavFundFrom = ROUND(@NavFundFrom,@NAVDecimalPlacesFundFrom) END


		If @NAVRoundingModeFundTo = 1 
		    BEGIN 
			    Set  @NavFundTo = ROUND(@NavFundTo,@NAVDecimalPlacesFundTo) 
			    IF @NAVDecimalPlacesFundTo = 0 BEGIN
			    set @NavFundTo = @NavFundTo + 1
			    END
			    IF @NAVDecimalPlacesFundTo = 2 BEGIN
			    set @NavFundTo = @NavFundTo + 0.01
			    END
			    IF @NAVDecimalPlacesFundTo = 4 BEGIN
			    set @NavFundTo = @NavFundTo + 0.0001
			    END
			    IF @NAVDecimalPlacesFundTo = 6 BEGIN
			    set @NavFundTo = @NavFundTo + 0.000001
			    END
			    IF @NAVDecimalPlacesFundTo = 8 BEGIN
			    set @NavFundTo = @NavFundTo + 0.00000001
			    END
		    END
		If @NAVRoundingModeFundTo = 2 BEGIN Set  @NavFundTo = ROUND(@NavFundTo,@NAVDecimalPlacesFundTo,1) END
		If @NAVRoundingModeFundTo = 3 BEGIN Set  @NavFundTo = ROUND(@NavFundTo,@NAVDecimalPlacesFundTo) END

		--jika cash & unit ke isi, pake cash sebagai patokan
		if @paramCashAmount > 0 and @paramSwitchingAll = 0
		BEGIN
			set @CashAmount = @paramCashAmount
			if @NavFundFrom > 0  and @NavFundTo > 0
			BEGIN
				If @UnitRoundingModeFundFrom = 1 
				BEGIN 
					Set @UnitAmount = round(@CashAmount / @NavFundFrom,@UnitDecimalPlacesFundFrom) 
					IF @UnitDecimalPlacesFundFrom = 0 BEGIN
					set @UnitAmount = @UnitAmount + 1
					END
					IF @UnitDecimalPlacesFundFrom = 2 BEGIN
					set @UnitAmount = @UnitAmount + 0.01
					END
					IF @UnitDecimalPlacesFundFrom = 4 BEGIN
					set @UnitAmount = @UnitAmount + 0.0001
					END
					IF @UnitDecimalPlacesFundFrom = 6 BEGIN
					set @UnitAmount = @UnitAmount + 0.000001
					END
					IF @UnitDecimalPlacesFundFrom = 8 BEGIN
					set @UnitAmount = @UnitAmount + 0.00000001
					END
				END
				If @UnitRoundingModeFundFrom = 2 BEGIN Set  @UnitAmount = ROUND(@CashAmount / @NavFundFrom,@UnitDecimalPlacesFundFrom,1) END
				If @UnitRoundingModeFundFrom = 3 BEGIN Set  @UnitAmount = ROUND(@CashAmount / @NavFundFrom,@UnitDecimalPlacesFundFrom) END



				if @FeeType = 'OUT'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

				if @FeeType = 'IN'
				BEGIN

					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						if @ClientCode = '08'
							set @FinalFeeAmount = case when @paramSwitchingFeeAmount > 0 then @paramSwitchingFeeAmount else (@CashAmount / (@FinalFeePercent/100 + 1))/100 end
						else
							set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom - @FinalFeeAmount
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						if @ClientCode = '08'
							set @FinalFeePercent = (@CashAmount / @FinalFeeAmount ) - 100
						else
							set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom  - @FinalFeeAmount
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

				--HANDLE UNIT
				BEGIN
					If @UnitRoundingModeFundFrom = 1 
					BEGIN 
						Set @TotalUnitAmountFundFrom = round(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom) 
						IF @UnitDecimalPlacesFundFrom = 0 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 1
						END
						IF @UnitDecimalPlacesFundFrom = 2 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.01
						END
						IF @UnitDecimalPlacesFundFrom = 4 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.0001
						END
						IF @UnitDecimalPlacesFundFrom = 6 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.000001
						END
						IF @UnitDecimalPlacesFundFrom = 8 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.00000001
						END
					END
					If @UnitRoundingModeFundFrom = 2 BEGIN Set  @TotalUnitAmountFundFrom = ROUND(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom,1) END
					If @UnitRoundingModeFundFrom = 3 BEGIN Set  @TotalUnitAmountFundFrom = ROUND(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom) END


					If @UnitRoundingModeFundTo = 1 
					BEGIN 
						Set @TotalUnitAmountFundTo = round(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo) 
						IF @UnitDecimalPlacesFundTo = 0 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 1
						END
						IF @UnitDecimalPlacesFundTo = 2 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.01
						END
						IF @UnitDecimalPlacesFundTo = 4 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.0001
						END
						IF @UnitDecimalPlacesFundTo = 6 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.000001
						END
						IF @UnitDecimalPlacesFundTo = 8 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.00000001
						END
					END
					If @UnitRoundingModeFundTo = 2 BEGIN Set  @TotalUnitAmountFundTo = ROUND(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo,1) END
					If @UnitRoundingModeFundTo = 3 BEGIN Set  @TotalUnitAmountFundTo = ROUND(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo) END
				END
			END
			ELSE
			BEGIN
				set @TotalUnitAmountFundFrom = 0
				set @TotalUnitAmountFundTo = 0
			    set @UnitAmount = 0

				if @FeeType = 'OUT'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END


				if @FeeType = 'IN'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						if @ClientCode = '08'
							set @FinalFeeAmount = case when @paramSwitchingFeeAmount > 0 then @paramSwitchingFeeAmount else (@CashAmount / (@FinalFeePercent/100 + 1))/100 end
						else
							set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom - @FinalFeeAmount
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						if @ClientCode = '08'
							set @FinalFeePercent = (@CashAmount / @FinalFeeAmount ) - 100
						else
							set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom  - @FinalFeeAmount
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

			END
		END
		ELSE IF @paramUnitAmount > 0 BEGIN
			
			if @NavFundFrom > 0  and @NavFundTo > 0
			BEGIN
				set @UnitAmount = @paramUnitAmount
				If @JournalRoundingMode = 1 BEGIN 
					Set  @CashAmount = ROUND(@UnitAmount * @NavFundFrom,@JournalDecimalPlaces) 

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
				If @JournalRoundingMode = 2 BEGIN Set   @CashAmount = ROUND(@UnitAmount * @NavFundFrom,@JournalDecimalPlaces,1) END
				If @JournalRoundingMode = 3 BEGIN Set   @CashAmount = ROUND(@UnitAmount * @NavFundFrom,@JournalDecimalPlaces) END

				if @FeeType = 'OUT'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END


				if @FeeType = 'IN'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						if @ClientCode = '08'
							set @FinalFeeAmount = case when @paramSwitchingFeeAmount > 0 then @paramSwitchingFeeAmount else (@CashAmount / (@FinalFeePercent/100 + 1))/100 end
						else
							set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom - @FinalFeeAmount
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						if @ClientCode = '08'
							set @FinalFeePercent = (@CashAmount / @FinalFeeAmount ) - 100
						else
							set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom  - @FinalFeeAmount
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

				--HANDLE UNIT
				BEGIN
					If @UnitRoundingModeFundFrom = 1 
					BEGIN 
						Set @TotalUnitAmountFundFrom = round(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom) 
						IF @UnitDecimalPlacesFundFrom = 0 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 1
						END
						IF @UnitDecimalPlacesFundFrom = 2 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.01
						END
						IF @UnitDecimalPlacesFundFrom = 4 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.0001
						END
						IF @UnitDecimalPlacesFundFrom = 6 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.000001
						END
						IF @UnitDecimalPlacesFundFrom = 8 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.00000001
						END
					END
					If @UnitRoundingModeFundFrom = 2 BEGIN Set  @TotalUnitAmountFundFrom = ROUND(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom,1) END
					If @UnitRoundingModeFundFrom = 3 BEGIN Set  @TotalUnitAmountFundFrom = ROUND(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom) END


					If @UnitRoundingModeFundTo = 1 
					BEGIN 
						Set @TotalUnitAmountFundTo = round(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo) 
						IF @UnitDecimalPlacesFundTo = 0 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 1
						END
						IF @UnitDecimalPlacesFundTo = 2 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.01
						END
						IF @UnitDecimalPlacesFundTo = 4 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.0001
						END
						IF @UnitDecimalPlacesFundTo = 6 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.000001
						END
						IF @UnitDecimalPlacesFundTo = 8 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.00000001
						END
					END
					If @UnitRoundingModeFundTo = 2 BEGIN Set  @TotalUnitAmountFundTo = ROUND(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo,1) END
					If @UnitRoundingModeFundTo = 3 BEGIN Set  @TotalUnitAmountFundTo = ROUND(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo) END
				END

			END
			ELSE 
			BEGIN
				IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
				BEGIN
					set @FinalFeePercent = @paramSwitchingFeePercent
					set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
				END
				ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
				BEGIN
					set @FinalFeeAmount = @paramSwitchingFeeAmount
					set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
				END
				set @UnitAmount = @paramUnitAmount
				set @CashAmount = 0
				set @TotalCashAmountFundFrom = 0
				set @TotalCashAmountFundTo = 0
				set @TotalUnitAmountFundFrom = 0
				set @TotalCashAmountFundTo = 0
			END
		END

/*
Update ClientSwitching set NavFundFrom=isnull(@NavFundFrom,0),NavFundTo=isnull(@NavFundTo,0), 
CashAmount = isnull(@CashAmount,0), TotalCashAmountFundFrom = isnull(@TotalCashAmountFundFrom,0),
TotalCashAmountFundTo = isnull(@TotalCashAmountFundTo,0), TotalUnitAmountFundFrom = isnull(@TotalUnitAmountFundFrom,0),
TotalUnitAmountFundTo = isnull(@TotalUnitAmountFundTo,0), UnitAmount = isnull(case when @UnitAmount > 0 then @UnitAmount else  @paramUnitAmount end,0), SwitchingFeeAmount = isnull(@FinalFeeAmount,0),
SwitchingFeePercent = isnull(@FinalFeePercent,0)
, UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
where ClientSwitchingpk = @ClientSwitchingPK and status = 1
*/
 
select  ISNULL(@FinalFeeAmount,0) SwitchingFeeAmount, 
ISNULL(@FinalFeePercent,0) SwitchingFeePercent,@NavFundFrom NavFundFrom,@NavFundTo NavFundTo, isnull(@CashAmount,0)CashAmount,
isnull(@TotalCashAmountFundFrom,0)TotalCashAmountFundFrom,isnull(@TotalCashAmountFundTo,0) TotalCashAmountFundTo, isnull(@TotalUnitAmountFundFrom,
case when @UnitAmount > 0 then @UnitAmount else  @paramUnitAmount end) TotalUnitAmountFundFrom,
isnull(@TotalUnitAmountFundTo,0) TotalUnitAmountFundTo,@unitAmount UnitAmount

";
                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _param.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@FeeType", _param.FeeType);
                        cmd.Parameters.AddWithValue("@FundPKFrom", _param.FundPKFrom);
                        cmd.Parameters.AddWithValue("@FundPKTo", _param.FundPKTo);
                        cmd.Parameters.AddWithValue("@NavDate", _param.NavDate);
                        cmd.Parameters.AddWithValue("@paramSwitchingFeePercent", _param.SwitchingFeePercent);
                        cmd.Parameters.AddWithValue("@paramSwitchingFeeAmount", _param.SwitchingFeeAmount);
                        cmd.Parameters.AddWithValue("@paramUnitAmount", _param.UnitAmount);
                        cmd.Parameters.AddWithValue("@paramCashAmount", _param.CashAmount);
                        cmd.Parameters.AddWithValue("@FeeTypeMode", _param.FeeTypeMode);
                        cmd.Parameters.AddWithValue("@paramSwitchingAll", _param.BitSwitchingAll);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _param.UpdateUsersID);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        if (_param.LastUpdate == null)
                        {
                            cmd.Parameters.AddWithValue("@Time", DateTime.Now);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("2", _param.LastUpdate);
                        }


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                ClientSwitchingRecalculate _ds = new ClientSwitchingRecalculate();
                                _ds.NavFundFrom = dr["NavFundFrom"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["NavFundFrom"]);
                                _ds.NavFundTo = dr["NavFundTo"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["NavFundTo"]);
                                _ds.CashAmount = dr["CashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CashAmount"]);
                                _ds.UnitAmount = dr["UnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["UnitAmount"]);
                                _ds.SwitchingFeePercent = dr["SwitchingFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SwitchingFeePercent"]);
                                _ds.SwitchingFeeAmount = dr["SwitchingFeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SwitchingFeeAmount"]);
                                _ds.TotalCashAmountFundFrom = dr["TotalCashAmountFundFrom"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["TotalCashAmountFundFrom"]);
                                _ds.TotalCashAmountFundTo = dr["TotalCashAmountFundTo"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["TotalCashAmountFundTo"]);
                                _ds.TotalUnitAmountFundFrom = dr["TotalUnitAmountFundFrom"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["TotalUnitAmountFundFrom"]);
                                _ds.TotalUnitAmountFundTo = dr["TotalUnitAmountFundTo"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["TotalUnitAmountFundTo"]);
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

        public void ClientSwitching_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'ClientSwitching',ClientSwitchingPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSwitchingSelected + @"
                                 update ClientSwitching set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time where status = 1 and ClientSwitchingPK in ( Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSwitchingSelected + @" ) 
                                 Update ClientSwitching set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where status = 4 and ClientSwitchingPK in (Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 4 and " + paramClientSwitchingSelected + @") ";

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

        public void ClientSwitching_UnApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'ClientSwitching',ClientSwitchingPK,1,'UnApprove by Selected Data',@UsersID,@IPAddress,@Time  from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 2 and " + paramClientSwitchingSelected + @" and Posted = 0 
                                 update ClientSwitching set status = 1,UpdateUsersID = @UsersID,UpdateTime = @Time,LastUpdate=@Time, Notes = 'Unapproved by selected' where ValueDate between @DateFrom and @DateTo and Status = 2 and " + paramClientSwitchingSelected;

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

        public void ClientSwitching_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'ClientSwitching',ClientSwitchingPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSwitchingSelected + @" 
                                         
                           Declare @PK int
                            Declare @HistoryPK int

                            Declare A Cursor For
	                            Select ClientSwitchingPK,historyPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSwitchingSelected + @" 
                            Open A
                            Fetch next From A
                            into @PK,@HistoryPK
                            While @@Fetch_status = 0
                            Begin

				                            Declare @LastNAV numeric(22,8)
		                                    Declare @SwitchingUnit numeric(22,8)


                                            Declare @OldSwitchingUnit numeric(22,8)
                                            Declare @OldUnitAmount numeric(22,8)
                                            Declare @OldNAVDate datetime
                                            Declare @OldFundPK int
                                            Declare @OldFundClientPK int
                                            Declare @OldCashAmount numeric(24,4)
                                            Declare @TrxFrom nvarchar(200)
                                            Declare @TransactionPK nvarchar(200)
                                            Select @OldFundPK = FundPKFrom,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate, @TrxFrom = EntryUsersID,@TransactionPK = TransactionPK
                                            From ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK

                                            Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                            and ClientTransactionPK = @PK and TransactionType = 6 and ID = 
                                        (
                                            Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                             and ClientTransactionPK = @PK and TransactionType = 6
                                        )

                                            --Set @OldCashAmount = 0
--				                            set @OldSwitchingUnit = @OldUnitAmount

--                                            Declare @UnitPrevious numeric(22,8)
--                                            Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                            where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

--                                            update fundclientpositionsummary
--		                                    set Unit = Unit + isnull(@OldSwitchingUnit,0)
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
--                                            Select @UsersID,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldSwitchingUnit,@UnitPrevious + @OldSwitchingUnit,
--                                            Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Reject Switching Out',@OldFundPK


                            fetch next From A into @PK,@HistoryPK
                            end
                            Close A
                            Deallocate A

                                          update ClientSwitching set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 1 and ClientSwitchingPK in ( Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSwitchingSelected + @"  ) 
                                          Update ClientSwitching set status= 2  where status = 4 and ClientSwitchingPK in (Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 4 and " + paramClientSwitchingSelected + @" ) ";

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

        public void ClientSwitching_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = " And ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = " And ClientSwitchingPK in (0) ";
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
                            _parambitManageUR = "Where A.status = 2   and A.Posted = 0 and A.ClientSwitchingPK in (select PK from ZManage_UR where Selected = 1 and Type = 3 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = "where  A.Status = 2 and Posted  = 0 and Revised  = 0  and ValueDate between @datefrom and @dateto "  + paramClientSwitchingSelected;
                        }


                        cmd.CommandText =
                           @"

declare @CashAmountFundFrom numeric(22,2)
declare @CashAmountFundTo numeric(22,2)
declare @SwitchingFeeAmount numeric(22,2)
declare @TotalCashAmountFundFrom numeric(22,2)
declare @TotalCashAmountFundTo numeric(22,2)
declare @TotalUnitAmountFundFrom numeric(22,4)
declare @TotalUnitAmountFundTo numeric(22,4)
declare @ValueDate datetime
declare @FundClientPK int
declare @FundPKFrom int
declare @FundPKTo int
declare @ClientSwitchingPK int
Declare @HistoryPK int
declare @TrxFrom nvarchar(200)
declare @TransactionPK nvarchar(200)
Declare @FeeType nvarchar(100)
Declare @FundClientID nvarchar(200)
Declare @FundClientName nvarchar(200)
Declare @NAVDate datetime
Declare @PaymentDate datetime
Declare @UnitAmount numeric(22,4)
Declare @BitPendingSwitchIn bit
declare @SwitchInDueDate int

declare @CashRefPKFrom int
declare @CashRefPKTo int
declare @BankPKFrom int
declare @BankPKTo int
declare @BankCurrencyPK int


Declare @SubscriptionAcc int Declare @PayableSubsAcc int Declare  @PendingSubscription int Declare @PendingSwitching int
Declare @RedemptionAcc int Declare @PayableRedemptionAcc int Declare @PayableSwitchingFee int

Declare @PeriodPK int 
Declare @FundjournalPK int

Select @PeriodPK = PeriodPK From Period Where @DateFrom Between DateFrom and DateTo and Status = 2     



Select @SubscriptionAcc = Subscription,@PendingSubscription = PendingSubscription,@PayableSubsAcc = payablesubscriptionfee 
,@RedemptionAcc = Redemption, @PayableRedemptionAcc = PendingRedemption
,@PayableSwitchingFee = PayableSwitchingFee, @PendingSwitching = PendingSwitching
From FundAccountingSetup where status in (1,2)   



DECLARE A CURSOR FOR 
select TotalCashAmountFundFrom,TotalCashAmountFundTo, TotalUnitAmountFundFrom,
TotalUnitAmountFundTo,FundPKFrom, FundPKTo,A.FundClientPK,ValueDate, ClientSwitchingPK, A.EntryUsersID ,TransactionPK, A.HistoryPK,FeeType
,SwitchingFeeAmount,PaymentDate,NAVDate,B.ID,B.Name,CashAmount,UnitAmount,CashRefPKFrom,CashRefPKTo
from ClientSwitching A
left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
" + _parambitManageUR + @"
--where  A.Status = 2 and A.Selected = 1 and Posted  = 0 and Revised  = 0  and ValueDate between @datefrom and @dateto
	
Open A
Fetch Next From A
Into @TotalCashAmountFundFrom,@TotalCashAmountFundTo, @TotalUnitAmountFundFrom,@TotalUnitAmountFundTo,@FundPKFrom, @FundPKTo,@FundClientPK
,@ValueDate,@ClientSwitchingPK,@TrxFrom, @TransactionPK,@HistoryPK,@FeeType,@SwitchingFeeAmount,@PaymentDate,@NAVDate
,@FundClientID,@FundClientName,@CashAmountFundFrom,@UnitAmount,@CashRefPKFrom,@CashRefPKTo


While @@FETCH_STATUS = 0
BEGIN

  select @BitPendingSwitchIn = BitPendingSwitchIn from FundFee where status in (1,2) and FundPK = @FundPKTo

  select @BankPKFrom = isnull(FundJournalAccountPK,3) from FundCashRef where FundCashRefPK = @CashRefPKFrom and status in (1,2)
  select @BankPKTo = isnull(FundJournalAccountPK,3) from FundCashRef where FundCashRefPK = @CashRefPKTo and status in (1,2)

  set @BankCurrencyPK = 1    

if @FeeType = 'OUT'
BEGIN
		set @NAVDate = dbo.fworkingday(@NAVDate,1)
		---------------- FUND OUT
	  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching Out',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
	 
	 
	    INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@RedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 

    if @SwitchingFeeAmount > 0
    begin

        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,3,1,2,@PayableSwitchingFee,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@SwitchingFeeAmount,0,@SwitchingFeeAmount,1,0,@SwitchingFeeAmount,@PostedTime 
    end

	  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching Out Payment Date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		  INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundFrom,@TotalCashAmountFundFrom,0,1,@TotalCashAmountFundFrom,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@BankPKFrom,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 



		------------FUND IN
		  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching in',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

        IF (@BitPendingSwitchIn = 1)
        BEGIN
            INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
            Select	   @FundJournalPK,1,1,2,@PendingSwitching,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundTo,@TotalCashAmountFundTo,0,1,@TotalCashAmountFundTo,0,@PostedTime 
	  
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
            Select	   @FundJournalPK,1,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundTo,@TotalCashAmountFundTo,0,1,@TotalCashAmountFundTo,0,@PostedTime 
        END


	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@SubscriptionAcc,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundTo,0,@TotalCashAmountFundTo,1,0,@TotalCashAmountFundTo,@PostedTime 

		  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching in payment date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		 INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@BankPKTo,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundTo,@TotalCashAmountFundTo,0,1,@TotalCashAmountFundTo,0,@PostedTime 
	  
        IF (@BitPendingSwitchIn = 1)
        BEGIN
            INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
            Select	   @FundJournalPK,2,1,2,@PendingSwitching,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundTo,0,@TotalCashAmountFundTo,1,0,@TotalCashAmountFundTo,@PostedTime 
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
            Select	   @FundJournalPK,2,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundTo,0,@TotalCashAmountFundTo,1,0,@TotalCashAmountFundTo,@PostedTime 
        END

END


IF @FeeType = 'IN'
BEGIN
    
        select @SwitchInDueDate = isnull(SwitchInDueDate,1) from Fund where status in (1,2) and FundPK = @FundPKTo
        IF (@SwitchInDueDate = 1)
        BEGIN
                    set @NAVDate = dbo.fworkingday(@NAVDate,1)
		            ---------------- FUND OUT
		            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

			        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			        Select	   @FundJournalPK, 1,2,'Posting Client Switching Out',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
	 
	 
			        INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
			        Select	   @FundJournalPK,1,1,2,@RedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundFrom,@TotalCashAmountFundFrom,0,1,@TotalCashAmountFundFrom,0,@PostedTime 
	  
	  
			        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			        Select	   @FundJournalPK,2,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 

		

		          select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

			        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			        Select	   @FundJournalPK, 1,2,'Posting Client Switching Out Payment Date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

			          INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
			        Select	   @FundJournalPK,1,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundFrom,@TotalCashAmountFundFrom,0,1,@TotalCashAmountFundFrom,0,@PostedTime 
	  
	  
			        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			        Select	   @FundJournalPK,2,1,2,@BankPKFrom,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 

			        ------------FUND IN
		          select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

                INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	            Select	   @FundJournalPK, 1,2,'Posting Client Switching in',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime


                IF (@BitPendingSwitchIn = 1)
                BEGIN
                    INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
                    Select	   @FundJournalPK,1,1,2,@PendingSwitching,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 	  
                END
                ELSE
                BEGIN
                    INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
                    Select	   @FundJournalPK,1,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 	  
                END 

	  
	            INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	            Select	   @FundJournalPK,2,1,2,@SubscriptionAcc,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 


		        IF @SwitchingFeeAmount > 0
                begin

                INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	            Select	   @FundJournalPK,3,1,2,@PayableSwitchingFee,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@SwitchingFeeAmount,0,@SwitchingFeeAmount,1,0,@SwitchingFeeAmount,@PostedTime 
                end


		          select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

                INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	            Select	   @FundJournalPK, 1,2,'Posting Client Switching in payment date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		         INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
                Select	   @FundJournalPK,1,1,2,@BankPKTo,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 
	  
	            IF (@BitPendingSwitchIn = 1)
                BEGIN
	                INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	                Select	   @FundJournalPK,2,1,2,@PendingSwitching,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@CashAmountFundFrom,0,@CashAmountFundFrom,1,0,@CashAmountFundFrom,@PostedTime 
                END
                ELSE
                BEGIN
                    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
                    Select	   @FundJournalPK,2,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@CashAmountFundFrom,0,@CashAmountFundFrom,1,0,@CashAmountFundFrom,@PostedTime 
                END


        END
        ELSE
        BEGIN
                    set @NAVDate = dbo.fworkingday(@NAVDate,1)
		            ---------------- FUND OUT
		            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

			        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			        Select	   @FundJournalPK, 1,2,'Posting Client Switching Out',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
	 
	 
			        INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
			        Select	   @FundJournalPK,1,1,2,@RedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 
	  
	  
			        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			        Select	   @FundJournalPK,2,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@CashAmountFundFrom,0,@CashAmountFundFrom,1,0,@CashAmountFundFrom,@PostedTime 

		

		            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

			        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			        Select	   @FundJournalPK, 1,2,'Posting Client Switching Out Payment Date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

			          INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
			        Select	   @FundJournalPK,1,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 
	  
	  
			        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			        Select	   @FundJournalPK,2,1,2,@BankPKFrom,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@CashAmountFundFrom,0,@CashAmountFundFrom,1,0,@CashAmountFundFrom,@PostedTime 


			        ------------FUND IN
		            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

                    INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	                Select	   @FundJournalPK, 1,2,'Posting Client Switching in',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime


                    IF (@BitPendingSwitchIn = 1)
                    BEGIN
                        INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
                        Select	   @FundJournalPK,1,1,2,@PendingSwitching,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 	  
                    END
                    ELSE
                    BEGIN
                        INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
                        Select	   @FundJournalPK,1,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 	  
                    END 

	  
	                INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	                Select	   @FundJournalPK,2,1,2,@SubscriptionAcc,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@CashAmountFundFrom,0,@CashAmountFundFrom,1,0,@CashAmountFundFrom,@PostedTime 


		              select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

                    INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	                Select	   @FundJournalPK, 1,2,'Posting Client Switching in payment date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		             INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
                    Select	   @FundJournalPK,1,1,2,@BankPKTo,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 
	  
	                IF (@BitPendingSwitchIn = 1)
                    BEGIN
	                    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	                    Select	   @FundJournalPK,2,1,2,@PendingSwitching,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@CashAmountFundFrom,0,@CashAmountFundFrom,1,0,@CashAmountFundFrom,@PostedTime 
                    END
                    ELSE
                    BEGIN
                        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
                        Select	   @FundJournalPK,2,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@CashAmountFundFrom,0,@CashAmountFundFrom,1,0,@CashAmountFundFrom,@PostedTime 
                    END

		            IF @SwitchingFeeAmount > 0
                    begin

                    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	                Select	   @FundJournalPK,3,1,2,@PayableSwitchingFee,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@SwitchingFeeAmount,0,@SwitchingFeeAmount,1,0,@SwitchingFeeAmount,@PostedTime 


                    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	                Select	   @FundJournalPK,4,1,2,@SubscriptionAcc,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@SwitchingFeeAmount,@SwitchingFeeAmount,0,1,@SwitchingFeeAmount,0,@PostedTime 
                    end
        
        END
        

END


if Exists(Select * from FundClientPosition where date = @ValueDate and FundClientPK = @FundClientPK and FundPK = @FundPKTo)
begin 
Update FundClientPosition set CashAmount = CashAmount + @TotalCashAmountFundTo, 
UnitAmount = UnitAmount + @TotalUnitAmountFundTo where Date = @ValueDate and FundClientPK = @FundClientPK 
and FundPK = @FundPKTo 
end 
else 
begin
INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount) 
select @ValueDate,@FundClientPK,@FundPKTo,@TotalCashAmountFundTo,@TotalUnitAmountFundTo 
end


Update FundClientPosition set  
UnitAmount = UnitAmount - @UnitAmount where Date = @ValueDate and FundClientPK = @FundClientPK 
and FundPK = @FundPKFrom 

        --Declare @LastNAV numeric(22,8)
--		Declare @SubsUnit numeric(22,8)

--        set @SubsUnit = 0		                        
--		set @SubsUnit = @TotalUnitAmountFundTo
		                          

--        Declare @UnitPrevious numeric(22,8)
--set @UnitPrevious = 0
--        Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--        where FundClientPK = @FundClientPK and FundPK = @FundPKTo
--        set @UnitPrevious = isnull(@UnitPrevious,0)
                               

--        if exists(
--            Select * from FundClientPositionSummary where FundClientPK = @FundClientPK and FundPK = @FundPKTo
--        )BEGIN
--        update fundclientpositionsummary
--		set Unit = Unit + @SubsUnit
--		where FundClientPK = @FundClientPK and FundPK = @FundPKTo
--        END
--        ELSE
--        BEGIN
--                Insert into FundClientPositionSummary (FundPK,FundClientPK,Unit)
--            Select @FundPKTo,@FundClientPK,@SubsUnit
--        END


                                
                               
--        --Buy = 1,
--        --Sell = 2,
--        --Adjustment = 3,
--        --SwitchingIn = 5,
--        --SwitchingOut = 6

--        insert into [FundClientPositionLog]
--                    ([UserId]
--                    ,[FundClientPK]
--                    ,[TransactionPK]
--                    ,[ClientTransactionPK]
--                    ,[UnitPrevious]
--                    ,[UnitChanges]
--                    ,[UnitAfter]
--                    ,[IsBoTransaction]
--                    ,[TransactionType]
--                    ,[Description]
--                    ,[FundPK])
--        Select @PostedBy,@FundClientPK, @TransactionPK,@ClientSwitchingPK,@UnitPrevious,@SubsUnit,@UnitPrevious + @SubsUnit
--        ,Case when @TrxFrom = 'rdo' then 0 else 1 end,5,'Posting Switching In',@FundPKTo



		--Declare @SwitchingUnit numeric(22,8)


--        Declare @OldSwitchingUnit numeric(22,8)
--        Declare @OldUnitAmount numeric(22,8)
--        Declare @OldNAVDate datetime
--        Declare @OldFundPK int
--        Declare @OldFundClientPK int
--        Declare @OldCashAmount numeric(24,4)
--        Declare @OldNAV numeric(18,8)
                                   
--        Select @OldFundPK = FundPKFrom,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@OldNAV = NAVFundFrom, @TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK
--        From ClientSwitching where ClientSwitchingPK = @ClientSwitchingPK and HistoryPK = @HistoryPK

--set @OldUnitAmount =0

--        Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
--        and ClientTransactionPK = @ClientSwitchingPK and TransactionType = 6 and ID =
--        (
--            Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
--                                                and ClientTransactionPK = @ClientSwitchingPK and TransactionType = 6
--        )

--        Set @OldCashAmount = 0

--set @LastNAV =1
--		Select @LastNAV  = NAV
--		from CloseNAV where Date = 
--		(
--		Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
--		) and status = 2 and FundPK = @OldFundPK

--		set @LastNAV = isnull(@LastNAV,1)

--		set @OldSwitchingUnit = 0

--		IF @OldCashAmount > 0 and @oldNAV = 0
--		BEGIN
--			set @OldSwitchingUnit = @OldCashAmount / @LastNAV
--		END
--		ELSE
--		BEGIN
--			set @OldSwitchingUnit = @OldUnitAmount
--		END

--set @UnitPrevious = 0

--        Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--        where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

--        update fundclientpositionsummary
--		set Unit = Unit + isnull(@OldSwitchingUnit,0)
--		where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
--        Buy = 1,
--        Sell = 2,
--        Adjustment = 3,
--        SwitchingIn = 5,
--        SwitchingOut = 6

--        insert into [FundClientPositionLog]
--                    ([UserId]
--                    ,[FundClientPK]
--                    ,[TransactionPK]
--                    ,[ClientTransactionPK]
--                    ,[UnitPrevious]
--                    ,[UnitChanges]
--                    ,[UnitAfter]
--                    ,[IsBoTransaction]
--                    ,[TransactionType]
--                    ,[Description]
--                    ,[FundPK])
--        Select @PostedBy,@OldFundClientPK,@TransactionPK ,@ClientSwitchingPK,@UnitPrevious,@OldSwitchingUnit,@UnitPrevious + @OldSwitchingUnit
--        ,Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Posting Switching Out Old Data Revise',@OldFundPK

--		set  @SwitchingUnit = 0         
--		set @SwitchingUnit = @TotalUnitAmountFundFrom
		                          

--        set @SwitchingUnit = @SwitchingUnit * -1

--		set  @UnitPrevious = 0   
--        Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--        where FundClientPK = @FundClientPK and FundPK = @FundPKFrom

--        set @UnitPrevious = isnull(@UnitPrevious,0)

--        update fundclientpositionsummary
--		set Unit = Unit + isnull(@SwitchingUnit,0)
--		where FundClientPK = @FundClientPK and FundPK = @FundPKFrom
                               
--        Buy = 1,
--        Sell = 2,
--        Adjustment = 3,
--        SwitchingIn = 5,
--        SwitchingOut = 6

--        insert into [FundClientPositionLog]
--                    ([UserId]
--                    ,[FundClientPK]
--                    ,[TransactionPK]
--                    ,[ClientTransactionPK]
--                    ,[UnitPrevious]
--                    ,[UnitChanges]
--                    ,[UnitAfter]
--                    ,[IsBoTransaction]
--                    ,[TransactionType]
--                    ,[Description]
--                    ,[FundPK])
--        Select @PostedBy,@FundClientPK,@TransactionPK,@ClientSwitchingPK,@UnitPrevious,@SwitchingUnit,@UnitPrevious + @SwitchingUnit,
--    case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Posting Switching out',@FundPKFrom





Declare @counterDateFrom datetime    
Declare @counterDateTo datetime      
set @counterDateFrom = @ValueDate  
set @counterDateTo = @ValueDate    
                            
while @counterDateTo < (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails)    
BEGIN    
	set @counterDateTo = dateadd(day,0,dbo.FWorkingDay(@counterDateTo,1))     
 
	if Exists(Select *from FundClientPosition where date = @counterDateTo and FundClientPK = @FundClientPK and FundPK = @FundPKTo)
	begin 
	update fundClientPosition set UnitAmount = UnitAmount  + @TotalUnitAmountFundTo,CashAmount = CashAmount + @TotalCashAmountFundTo    
	where FundClientPK = @FundClientPK and FundPK = @FundPKTo and Date = @counterDateTo 
	end 
	else 
	begin
	INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount) 
	select @counterDateTo,@FundClientPK,@FundPKTo,@TotalCashAmountFundTo,@TotalUnitAmountFundTo 
	END
END

while @counterDateFrom < (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails)    
BEGIN    
set @counterDateFrom = dbo.FWorkingDay(@counterDateFrom,1)     
update fundClientPosition set UnitAmount = UnitAmount  - @UnitAmount,CashAmount = CashAmount - @TotalCashAmountFundFrom    
where FundClientPK = @FundClientPK and FundPK = @FundPKFrom and Date = @counterDateFrom 
END



update ClientSwitching 
set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
where ValueDate = @ValueDate and FundClientPK = @FundClientPK 
and FundPKFrom = @FundPKFrom and FundPKTo = @FundPKTo and Status = 2 and Posted  = 0 and Revised = 0 and ClientSwitchingPK = @ClientSwitchingPK

Fetch next From A 
Into @TotalCashAmountFundFrom,@TotalCashAmountFundTo, @TotalUnitAmountFundFrom,@TotalUnitAmountFundTo,@FundPKFrom, 
@FundPKTo,@FundClientPK,@ValueDate,@ClientSwitchingPK,@TrxFrom, @TransactionPK,@HistoryPK,@FeeType
,@SwitchingFeeAmount,@PaymentDate,@NAVDate
,@FundClientID,@FundClientName,@CashAmountFundFrom,@UnitAmount,@CashRefPKFrom,@CashRefPKTo
END
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

        public void ClientSwitching_Revise(string _usersID, ClientSwitching _clientSwitching)
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

			                update FundJournal set Status = 3,VoidTime = @RevisedTime,VoidUsersID = @RevisedBy  where Type = 13 and TrxNo = @ClientSwitchingPK 
			                and Posted = 1 

                            declare @TotalUnitAmountFundFrom numeric(22,4)
                            declare @TotalUnitAmountFundTo numeric(22,4)
                            declare @ValueDate datetime
                            declare @FundPKFrom int
                            declare @FundPKTo int
                            declare @MaxClientSwitchingPK int
                            declare @FundClientPK int

                            select @TotalUnitAmountFundFrom=unitamount,@TotalUnitAmountFundTo=TotalUnitAmountFundTo,
                            @ValueDate = NAVDate, @FundPKFrom = FundPKFrom, @FundPKTo = FundPKTo, @FundClientPK = FundClientPK
                            from ClientSwitching where  Status = 2 and clientSwitchingPK = @ClientSwitchingPK and Posted  = 1 and Revised  = 0 

                            if Exists(Select *from FundClientPosition where date = @ValueDate and FundClientPK = @FundClientPK and FundPK = @FundPKTo)
                            begin 
                            Update FundClientPosition set  
                            UnitAmount = UnitAmount - @TotalUnitAmountFundTo where Date = @ValueDate and FundClientPK = @FundClientPK 
                            and FundPK = @FundPKTo 
                            end 

                            if Exists(Select *from FundClientPosition where date = @ValueDate and FundClientPK = @FundClientPK and FundPK = @FundPKFrom)
                            begin 
                            Update FundClientPosition set  
                            UnitAmount = UnitAmount + @TotalUnitAmountFundFrom where Date = @ValueDate and FundClientPK = @FundClientPK 
                            and FundPK = @FundPKFrom 
                            end 
                            


                            Select @MaxClientSwitchingPK = ISNULL(MAX(ClientSwitchingPK),0) + 1 From ClientSwitching   
                            INSERT INTO [dbo].[ClientSwitching]  
                            ([ClientSwitchingPK],[HistoryPK] ,[Status],[Notes], [NAVDate] ,[ValueDate],
                            [PaymentDate], [NAVFundFrom] ,[NAVFundTo] ,[FundPKFrom],[FundPKTo], [FundClientPK] , [CashRefPKFrom] ,[CashRefPKTo] ,[BitSwitchingAll], [Description] ,
                            [CashAmount] ,[UnitAmount] ,[TotalCashAmountFundFrom] ,[TotalCashAmountFundTo] ,
                            [TotalUnitAmountFundFrom] ,[TotalUnitAmountFundTo] ,[SwitchingFeePercent] ,[SwitchingFeeAmount],[CurrencyPK],
                            [TransferType],[FeeType],[FeeTypeMethod],
                            [EntryUsersID],[EntryTime],[LastUpdate],IsProcessed,[IsFrontSync],userswitchingPK,TransactionPK)
                        
                            SELECT @MaxClientSwitchingPK,1,1,'Pending Revised' ,[NAVDate] ,
                            [ValueDate],[PaymentDate],[NAVFundFrom],[NAVFundTo] ,[FundPKFrom],[FundPKTo],[FundClientPK] ,
                            [CashRefPKFrom] ,[CashRefPKTo] ,[BitSwitchingAll],[Description] ,[CashAmount] ,[UnitAmount] ,[TotalCashAmountFundFrom] ,[TotalCashAmountFundTo] ,[TotalUnitAmountFundFrom] ,[TotalUnitAmountFundTo] ,
                            [SwitchingFeePercent] ,[SwitchingFeeAmount] ,[CurrencyPK],
                            [TransferType],[FeeType],[FeeTypeMethod],
                            [EntryUsersID],[EntryTime] ,@RevisedTime,0,0,userswitchingPK,TransactionPK 
                            FROM ClientSwitching  
                            where ClientSwitchingPK = @ClientSwitchingPK   and status = 2 and posted = 1 



                    
IF(@ClientCode = 21)
BEGIN



--AGENT SWIITCHING

Declare @MaxAgentSwitchPK int
Declare @AgentSwitchingPK int

select @AgentSwitchingPK = AgentSwitchingPK from AgentSwitching where ClientSwitchingPK = @ClientSwitchingPK and status = 2

select @MaxAgentSwitchPK = Max(AgentSwitchingPK) from AgentSwitching
set @MaxAgentSwitchPK = isnull(@MaxAgentSwitchPK,0)

update AgentSwitching set status = 3 where  ClientSwitchingPK = @ClientSwitchingPK 

Insert into AgentSwitching (AgentSwitchingPK,HistoryPK,Status,ClientSwitchingPK,AgentPK,AgentTrxPercent,AgentTrxAmount,
EntryUsersID,EntryTime,LastUpdate)

select @MaxAgentSwitchPK + ROW_NUMBER() OVER(ORDER BY @MaxClientSwitchingPK ASC) AgentSwitchingPK,1,2,@MaxClientSwitchingPK,AgentPK,AgentTrxPercent,AgentTrxAmount,
[EntryUsersID],[EntryTime],@RevisedTime from AgentSwitching WHERE ClientSwitchingPK = @ClientSwitchingPK


--AGENT FEE SWIITCHING


Declare @MaxAgentFeeSwitchPK int
Declare @AgentFeeSwitchingPK int

select @AgentFeeSwitchingPK = AgentFeeSwitchingPK from AgentFeeSwitching where ClientSwitchingPK = @ClientSwitchingPK and status = 2

select @MaxAgentFeeSwitchPK = Max(AgentFeeSwitchingPK) from AgentFeeSwitching
set @MaxAgentFeeSwitchPK = isnull(@MaxAgentFeeSwitchPK,0)

update AgentFeeSwitching set status = 3 where  ClientSwitchingPK = @ClientSwitchingPK 

Insert into AgentFeeSwitching (AgentFeeSwitchingPK,HistoryPK,Status,ClientSwitchingPK,AgentPK,AgentFeePercent,AgentFeeAmount,
EntryUsersID,EntryTime,LastUpdate)

select @MaxAgentFeeSwitchPK + ROW_NUMBER() OVER(ORDER BY @MaxClientSwitchingPK ASC) AgentFeeSwitchingPK,1,2,@MaxClientSwitchingPK,AgentPK,AgentFeePercent,AgentFeeAmount,
EntryUsersID,EntryTime,@RevisedTime from AgentFeeSwitching WHERE ClientSwitchingPK = @ClientSwitchingPK

END


                            update ClientSwitching 
                            set RevisedBy = @RevisedBy,RevisedTime = @RevisedTime,Revised = 1,Lastupdate = @RevisedTime, status = 3 ,IsFrontSync = 0
                            where  clientSwitchingPK = @ClientSwitchingPK and Status = 2 and Revised = 0 and Posted  = 1

                            Declare @counterDateFrom datetime    
                            Declare @counterDateTo datetime      
                            set @counterDateFrom = @ValueDate  
                            set @counterDateTo = @ValueDate  

                            while @counterDateTo < 
                            (select max(date) from fundClientPosition where FundClientPK = @FundClientPK)    
                            BEGIN 
                                set @counterDateTo = dbo.FWorkingDay(@counterDateTo,1)    
		                        update fundClientPosition set UnitAmount = UnitAmount  - @TotalUnitAmountFundTo    
		                        where FundClientPK = @FundClientPK and FundPK = @FundPKTo and Date = @counterDateTo 
	                            
                            END

                            while @counterDateFrom < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK)    
                            BEGIN    
	                            set @counterDateFrom = dbo.FWorkingDay(@counterDateFrom,1)    
	                            update fundClientPosition set UnitAmount = UnitAmount  + @TotalUnitAmountFundFrom 
	                            where FundClientPK = @FundClientPK and FundPK = @FundPKFrom and Date = @counterDateFrom 
                            END

                                    Declare @LastNAV numeric(22,8)
		                           Declare @SubsUnit numeric(22,8)


                                    Declare @OldSubsUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(200)
                                    declare @TransactionPK  nvarchar(200)
                                    Select @OldFundPK = FundPKFrom,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@oldNAV = NAVFundFrom,@TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK
                                    From ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                     and ClientTransactionPK = @PK and TransactionType = 6 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 6
                                    )

				                   set @OldSubsUnit = @OldUnitAmount
		                      

                                   -- Declare @UnitPrevious numeric(22,8)
                                   -- Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                   -- where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                   --update fundclientpositionsummary
		                           --set Unit = Unit + @OldSubsUnit
		                           --where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    --insert into [FundClientPositionLog]
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
--                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldSubsUnit,@UnitPrevious + @OldSubsUnit,
--                                    Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Revise Switching Out Old Data Revise',@OldFundPK

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
--                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@MaxClientSwitchingPK,@UnitPrevious + @OldSubsUnit,@OldSubsUnit * -1,@UnitPrevious + @OldSubsUnit + (@OldSubsUnit *-1)
--                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Revise Switching Out',@OldFundPK


                                    Select @OldFundPK = FundPKTo,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@oldNAV = NAVFundTo,@TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK
                                    From ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                    and ClientTransactionPK = @PK and TransactionType = 5 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 5
                                    )


				                   set @OldSubsUnit = @OldUnitAmount
		                      

                                  --  Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                  --  where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                  -- update fundclientpositionsummary
		                          -- set Unit = Unit + @OldSubsUnit
		                          -- where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    --insert into [FundClientPositionLog]
 --                                              ([UserId]
 --                                              ,[FundClientPK]
 --                                              ,[TransactionPK]
 --                                              ,[ClientTransactionPK]
 --                                              ,[UnitPrevious]
 --                                              ,[UnitChanges]
 --                                              ,[UnitAfter]
 --                                              ,[IsBoTransaction]
 --                                              ,[TransactionType]
 --                                              ,[Description]
 --                                               ,[FundPK])
 --                                   Select @RevisedBy,@OldFundClientPK,@TransactionPK ,@PK,@UnitPrevious,@OldSubsUnit,@UnitPrevious + @OldSubsUnit
 --                                   ,Case when @TrxFrom = 'rdo' then 0 else 1 end,5,'Revise Switching IN Old Data Revise',@OldFundPK

 --                                   insert into [FundClientPositionLog]
 --                                              ([UserId]
 --                                              ,[FundClientPK]
 --                                              ,[TransactionPK]
 --                                              ,[ClientTransactionPK]
 --                                              ,[UnitPrevious]
 --                                              ,[UnitChanges]
 --                                              ,[UnitAfter]
 --                                              ,[IsBoTransaction]
 --                                              ,[TransactionType]
 --                                              ,[Description]
 --                                               ,[FundPK])
 --                                   Select @RevisedBy,@OldFundClientPK,@TransactionPK ,@MaxClientSwitchingPK,@UnitPrevious + @OldSubsUnit,@OldSubsUnit * -1,@UnitPrevious + @OldSubsUnit + (@OldSubsUnit *-1)
 --                                   ,Case when @TrxFrom = 'rdo' then 0 else 1 end,5,'Revise Switching IN',@OldFund

                        ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@RevisedBy", _usersID);
                        cmd.Parameters.AddWithValue("@RevisedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
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

        public void ClientSwitching_ReviseBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, ClientSwitching _clientSwitching)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_clientSwitching.ClientSwitchingSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientSwitching.ClientSwitchingSelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _clientSwitching.ClientSwitchingSelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =

                            @"--declare @datefrom datetime
                            --declare @dateto datetime
                            --set @datefrom ='10/12/16'
                            --set @dateto = '10/12/16'

                            declare @TotalCashAmountFundFrom numeric(22,2)
                            declare @TotalCashAmountFundTo numeric(22,2)
                            declare @TotalUnitAmountFundFrom numeric(22,4)
                            declare @TotalUnitAmountFundTo numeric(22,4)
                            declare @ValueDate datetime
                            declare @FundClientPK int
                            declare @FundPKFrom int
                            declare @FundPKTo int
                            declare @MaxClientSwitchingPK int
                            declare @ClientSwitchingPK int

                            DECLARE A CURSOR FOR 
                            select TotalCashAmountFundFrom,TotalCashAmountFundTo, TotalUnitAmountFundFrom,TotalUnitAmountFundTo,FundPKFrom, FundPKTo,FundClientPK,ValueDate,ClientSwitchingPK 
                            from ClientSwitching where  Status = 2 and " + paramClientSwitchingSelected + @" and Posted  = 1 and Revised  = 0  and ValueDate between @DateFrom and @DateTo
	
                            Open A
                            Fetch Next From A
                            Into @TotalCashAmountFundFrom,@TotalCashAmountFundTo, @TotalUnitAmountFundFrom,@TotalUnitAmountFundTo,@FundPKFrom, @FundPKTo,@FundClientPK,@ValueDate,@ClientSwitchingPK

                            While @@FETCH_STATUS = 0
                            BEGIN

                            if Exists(Select *from FundClientPosition where date = @ValueDate and FundClientPK = @FundClientPK and FundPK = @FundPKTo)
                            begin 
                            Update FundClientPosition set CashAmount = CashAmount - @TotalCashAmountFundTo, 
                            UnitAmount = UnitAmount - @TotalUnitAmountFundTo where Date = @ValueDate and FundClientPK = @FundClientPK 
                            and FundPK = @FundPKTo 
                            end 

                            Update FundClientPosition set  
                            UnitAmount = UnitAmount + @TotalUnitAmountFundFrom where Date = @ValueDate and FundClientPK = @FundClientPK 
                            and FundPK = @FundPKFrom 

                            Select @MaxClientSwitchingPK = ISNULL(MAX(ClientSwitchingPK),0) + 1 From ClientSwitching   
                            INSERT INTO [dbo].[ClientSwitching]  
                            ([ClientSwitchingPK],[HistoryPK] ,[Status],[Notes], [NAVDate] ,[ValueDate],
                            [PaymentDate], [NAVFundFrom] ,[NAVFundTo] ,[FundPKFrom],[FundPKTo], [FundClientPK] , [CashRefPKFrom] ,[CashRefPKTo] ,[BitSwitchingAll], [Description] ,
                            [CashAmount] ,[UnitAmount] ,[TotalCashAmountFundFrom] ,[TotalCashAmountFundTo] ,
                            [TotalUnitAmountFundFrom] ,[TotalUnitAmountFundTo] ,[SwitchingFeePercent] ,[SwitchingFeeAmount],[CurrencyPK],
                            [TransferType],[FeeType],
                            [EntryUsersID],[EntryTime],[LastUpdate])
                        
                            SELECT @MaxClientSwitchingPK,1,1,'Pending Revised' ,[NAVDate] ,
                            [ValueDate],[PaymentDate],[NAVFundFrom],[NAVFundTo] ,[FundPKFrom],[FundPKTo],[FundClientPK] ,
                            [CashRefPKFrom] ,[CashRefPKTo] ,[BitSwitchingAll],[Description] ,[CashAmount] ,[UnitAmount] ,[TotalCashAmountFundFrom] ,[TotalCashAmountFundTo] ,[TotalUnitAmountFundFrom] ,[TotalUnitAmountFundTo] ,
                            [SwitchingFeePercent] ,[SwitchingFeeAmount] ,[CurrencyPK],
                            [TransferType],[FeeType],
                            [EntryUsersID],[EntryTime] , '' 
                            FROM ClientSwitching  
                            where ClientSwitchingPK = @ClientSwitchingPK


                            update ClientSwitching 
                            set RevisedBy = '',RevisedTime = '',Revised = 1,Lastupdate = '' 
                            where ValueDate = @ValueDate and FundClientPK = @FundClientPK 
                            and FundPKFrom = @FundPKFrom and FundPKTo = @FundPKTo and Status = 2 and Revised = 0 and Posted  = 1

                            Declare @counterDateFrom datetime    
                            Declare @counterDateTo datetime      
                            set @counterDateFrom = @ValueDate  
                            set @counterDateTo = @ValueDate    
                            while @counterDateTo < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPKTo and year(date) = year(@ValueDate))    
                            BEGIN    
                            set @counterDateTo = dateadd(day,0,dbo.FWorkingDay(@counterDateTo,1))     
 
                             if Exists(Select *from FundClientPosition where date = @counterDateTo and FundClientPK = @FundClientPK and FundPK = @FundPKTo)
                            begin 
                            update fundClientPosition set UnitAmount = UnitAmount  - @TotalUnitAmountFundTo,CashAmount = CashAmount - @TotalCashAmountFundTo    
                            where FundClientPK = @FundClientPK and FundPK = @FundPKTo and Date = @counterDateTo 
                            end 
    
                            while @counterDateFrom < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPKFrom and year(date) = year(@ValueDate))    
                            BEGIN    
                            set @counterDateFrom = dbo.FWorkingDay(@counterDateFrom,1)    

                            update fundClientPosition set UnitAmount = UnitAmount  + @TotalUnitAmountFundFrom,CashAmount = CashAmount + @TotalCashAmountFundFrom    
                            where FundClientPK = @FundClientPK and FundPK = @FundPKFrom and Date = @counterDateFrom end 

                            END


                            Fetch next From A Into @TotalCashAmountFundFrom,@TotalCashAmountFundTo, @TotalUnitAmountFundFrom,@TotalUnitAmountFundTo,@FundPKFrom, @FundPKTo,@FundClientPK,@ValueDate,@ClientSwitchingPK
                            END
                            Close A
                            Deallocate A 


                            ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@RevisedBy", _usersID);
                        cmd.Parameters.AddWithValue("@RevisedTime", _datetimeNow);
                        cmd.ExecuteNonQuery();
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
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Exists(select * From ClientSwitching where (Status = 2 and Posted = 1 and Revised = 0) and ValueDate between @ValueDateFrom and @ValueDateTo and " + paramClientSwitchingSelected + @") " +
                        " BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

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


                        cmd.CommandText = @" if Not Exists(select * From EndDayTrails where Status =  2 and Notes = 'Unit' and ValueDate = dbo.fworkingday(@ValueDateFrom, -1)) 
                         BEGIN 
                            Select 1 Result 
                        END 
			           
                        ELSE 
                        BEGIN  
                            Select 0 Result  
                        END
                          ";

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

        public bool Validate_VoidBySelected(DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Exists(select * From ClientSwitching where (Status = 2 and Posted = 1 and Revised = 0) and ValueDate between @ValueDateFrom and @ValueDateTo and " + paramClientSwitchingSelected + @") " +
                        " BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

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

        public void ClientSwitching_GetNavBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
    
	
	

--declare @UpdateUsersID nvarchar(20)
--    declare @Time datetime
--    declare @datefrom date
--    declare @dateto date
--    declare @ClientCode nvarchar(20)

--    set @UpdateUsersID = 'admin'
--    set @Time = getdate()
--    set @datefrom = '2020-08-19'
--    set @dateto = @datefrom
--    set @ClientCode = '08'

    Declare @FeeTypeMode int
    Declare @JournalRoundingMode int          
    Declare @NAVRoundingModeFundFrom int 
    Declare @NAVRoundingModeFundTo int 
    Declare @JournalDecimalPlaces int 
    Declare @NAVDecimalPlacesFundFrom int
    Declare @NAVDecimalPlacesFundTo int
    Declare @UnitDecimalPlacesFundFrom int
    Declare @UnitDecimalPlacesFundTo int
    Declare @NavFundFrom numeric(22,8)
    Declare @NavFundTo numeric(22,8)
    Declare @UnitRoundingModeFundFrom int
    Declare @UnitRoundingModeFundTo int
    Declare @CashAmount numeric(22,4)
    Declare @UnitAmount numeric(18,8)
    Declare @SwitchingFeeAmount numeric(22,4)
    Declare @SwitchingFeePercent numeric(18,8)
    Declare @ClientSwitchingPK int
    Declare @NAVDate datetime
    Declare @FundPKFrom int
    Declare @FundPKTo int
    Declare @paramUnitAmount numeric(22,4)
    Declare @paramCashAmount numeric(22,4)
    Declare @paramSwitchingFeePercent numeric(18,8)
    Declare @paramSwitchingFeeAmount numeric(22,4)
    Declare @FeeType nvarchar(5)
    Declare @CashAmountTo numeric(22,4)
    Declare @TotalCashAmountFundFrom numeric(22,4)
    Declare @TotalCashAmountFundTo numeric(22,4)
    Declare @TotalUnitAmountFundFrom numeric(22,8)
    Declare @TotalUnitAmountFundTo numeric(22,8)
    Declare @paramTotalCashFundFrom numeric(22,8)
    Declare @paramTotalCashFundTo numeric(22,8)
    Declare @FinalFeePercent numeric(18,8)
    Declare @FinalFeeAmount numeric(22,4)
	Declare @paramSwitchingAll int


    DECLARE A CURSOR FOR 
    Select ClientSwitchingPK From ClientSwitching 
    where ValueDate between @DateFrom and @DateTo
    and Status = 2 and Posted = 0  
    and " + paramClientSwitchingSelected + @"
	        --and ClientSwitchingPK = 7762
    Open A
    Fetch Next From A
    Into @ClientSwitchingPK

    While @@FETCH_STATUS = 0
    Begin
	
	    set @NAVFundFrom = 0
	    set @NavFundTo = 0
		set @UnitAmount = 0
		set @CashAmount = 0
		set @TotalCashAmountFundFrom = 0
		set @TotalUnitAmountFundFrom = 0
		set @TotalCashAmountFundTo = 0
		set @TotalUnitAmountFundTo = 0
		set @SwitchingFeeAmount = 0
		set @SwitchingFeePercent = 0
		set @paramCashAmount = 0
		set @paramUnitAmount = 0
		set @FinalFeeAmount = 0
		set @FinalFeePercent = 0
		set @paramSwitchingFeePercent = 0
		set @paramSwitchingFeeAmount = 0
		set @FeeType = ''
		set @FeeTypeMode = 0
		set @paramSwitchingAll = 0

	    Select @NAVDate = NAVDate,@FundPKFrom = A.FundPKFrom,@FundPKTo = A.FundPKTo ,@paramUnitAmount = A.UnitAmount , @JournalRoundingMode = isnull(D.JournalRoundingMode,3),
	    @JournalDecimalPlaces = ISNULL(D.JournalDecimalPlaces,4), @paramSwitchingFeeAmount = A.SwitchingFeeAmount , @paramSwitchingFeePercent = A.SwitchingFeePercent,
	    @paramCashAmount = A.CashAmount, @FeeType = FeeType, @FeeTypeMode = isnull(FeeTypeMethod,1), @paramSwitchingAll = isnull(BitSwitchingAll,0)
	    from ClientSwitching A
	    Left join Fund B on A.FundPKFrom = B.FundPK and B.Status = 2
	    Left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.status = 2
	    Left join Bank D on C.BankPK = D.BankPK and D.Status = 2 
	    Where A.ClientSwitchingPK = @ClientSwitchingPK and a.status = 2 and A.Posted = 0

	    Select @NAVRoundingModeFundFrom = A.NAVRoundingMode, @NAVDecimalPlacesFundFrom= A.NAVDecimalPlaces,@UnitDecimalPlacesFundFrom = A.UnitDecimalPlaces 
	    ,@UnitRoundingModeFundFrom = A.UnitRoundingMode
	    From Fund A Where A.FundPK = @FundPKFrom and A.Status = 2
		
	    Select @NAVRoundingModeFundTo = A.NAVRoundingMode, @NAVDecimalPlacesFundTo= A.NAVDecimalPlaces,@UnitDecimalPlacesFundTo = A.UnitDecimalPlaces ,
	    @UnitRoundingModeFundTo = A.UnitRoundingMode
	    From Fund A Where A.FundPK = @FundPKTo and A.Status = 2

	    Select @NavFundFrom = isnull(Nav,0) From CloseNAV Where Date = @NAVDate and Status = 2 and FundPK = @FundPKFrom
	    Select @NavFundTo = isnull(Nav,0) From CloseNAV Where Date = @NAVDate and Status = 2 and FundPK = @FundPKTo

		--select @ClientSwitchingPK,@paramSwitchingFeePercent,@paramCashAmount cash,@paramUnitAmount unit,@NavFundFrom navfrom, @NavFundTo navto

		--handle rounding NAV dsni
	    If @NAVRoundingModeFundFrom = 1 
	    BEGIN 
		    Set  @NavFundFrom = ROUND(@NavFundFrom,@NAVDecimalPlacesFundFrom) 
		    IF @NAVDecimalPlacesFundFrom = 0 BEGIN
		    set @NavFundFrom = @NavFundFrom + 1
		    END

		    IF @NAVDecimalPlacesFundFrom = 2 BEGIN
		    set @NavFundFrom = @NavFundFrom + 0.01
		    END
		    IF @NAVDecimalPlacesFundFrom = 4 BEGIN
		    set @NavFundFrom = @NavFundFrom + 0.0001
		    END
		    IF @NAVDecimalPlacesFundFrom = 6 BEGIN
		    set @NavFundFrom = @NavFundFrom + 0.000001
		    END
		    IF @NAVDecimalPlacesFundFrom = 8 BEGIN
		    set @NavFundFrom = @NavFundFrom + 0.00000001
		    END
	    END
	    If @NAVRoundingModeFundFrom = 2 BEGIN Set  @NavFundFrom = ROUND(@NavFundFrom,@NAVDecimalPlacesFundFrom,1) END
	    If @NAVRoundingModeFundFrom = 3 BEGIN Set  @NavFundFrom = ROUND(@NavFundFrom,@NAVDecimalPlacesFundFrom) END


		If @NAVRoundingModeFundTo = 1 
		    BEGIN 
			    Set  @NavFundTo = ROUND(@NavFundTo,@NAVDecimalPlacesFundTo) 
			    IF @NAVDecimalPlacesFundTo = 0 BEGIN
			    set @NavFundTo = @NavFundTo + 1
			    END
			    IF @NAVDecimalPlacesFundTo = 2 BEGIN
			    set @NavFundTo = @NavFundTo + 0.01
			    END
			    IF @NAVDecimalPlacesFundTo = 4 BEGIN
			    set @NavFundTo = @NavFundTo + 0.0001
			    END
			    IF @NAVDecimalPlacesFundTo = 6 BEGIN
			    set @NavFundTo = @NavFundTo + 0.000001
			    END
			    IF @NAVDecimalPlacesFundTo = 8 BEGIN
			    set @NavFundTo = @NavFundTo + 0.00000001
			    END
		    END
		If @NAVRoundingModeFundTo = 2 BEGIN Set  @NavFundTo = ROUND(@NavFundTo,@NAVDecimalPlacesFundTo,1) END
		If @NAVRoundingModeFundTo = 3 BEGIN Set  @NavFundTo = ROUND(@NavFundTo,@NAVDecimalPlacesFundTo) END



		--jika cash & unit ke isi, pake cash sebagai patokan
		if @paramCashAmount > 0 and @paramSwitchingAll = 0
		BEGIN
			set @CashAmount = @paramCashAmount
			if @NavFundFrom > 0  and @NavFundTo > 0
			BEGIN
				If @UnitRoundingModeFundFrom = 1 
				BEGIN 
					Set @UnitAmount = round(@CashAmount / @NavFundFrom,@UnitDecimalPlacesFundFrom) 
					IF @UnitDecimalPlacesFundFrom = 0 BEGIN
					set @UnitAmount = @UnitAmount + 1
					END
					IF @UnitDecimalPlacesFundFrom = 2 BEGIN
					set @UnitAmount = @UnitAmount + 0.01
					END
					IF @UnitDecimalPlacesFundFrom = 4 BEGIN
					set @UnitAmount = @UnitAmount + 0.0001
					END
					IF @UnitDecimalPlacesFundFrom = 6 BEGIN
					set @UnitAmount = @UnitAmount + 0.000001
					END
					IF @UnitDecimalPlacesFundFrom = 8 BEGIN
					set @UnitAmount = @UnitAmount + 0.00000001
					END
				END
				If @UnitRoundingModeFundFrom = 2 BEGIN Set  @UnitAmount = ROUND(@CashAmount / @NavFundFrom,@UnitDecimalPlacesFundFrom,1) END
				If @UnitRoundingModeFundFrom = 3 BEGIN Set  @UnitAmount = ROUND(@CashAmount / @NavFundFrom,@UnitDecimalPlacesFundFrom) END



				if @FeeType = 'OUT'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

				if @FeeType = 'IN'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						if @ClientCode = '08'
							set @FinalFeeAmount = case when @paramSwitchingFeeAmount > 0 then @paramSwitchingFeeAmount else @CashAmount - (@CashAmount / (@FinalFeePercent + 100)*100) end
						else
							set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom - @FinalFeeAmount
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						if @ClientCode = '08'
							set @FinalFeePercent = (@CashAmount / @FinalFeeAmount ) - 100
						else
							set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom  - @FinalFeeAmount
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

				--HANDLE UNIT
				BEGIN
					If @UnitRoundingModeFundFrom = 1 
					BEGIN 
						Set @TotalUnitAmountFundFrom = round(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom) 
						IF @UnitDecimalPlacesFundFrom = 0 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 1
						END
						IF @UnitDecimalPlacesFundFrom = 2 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.01
						END
						IF @UnitDecimalPlacesFundFrom = 4 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.0001
						END
						IF @UnitDecimalPlacesFundFrom = 6 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.000001
						END
						IF @UnitDecimalPlacesFundFrom = 8 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.00000001
						END
					END
					If @UnitRoundingModeFundFrom = 2 BEGIN Set  @TotalUnitAmountFundFrom = ROUND(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom,1) END
					If @UnitRoundingModeFundFrom = 3 BEGIN Set  @TotalUnitAmountFundFrom = ROUND(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom) END


					If @UnitRoundingModeFundTo = 1 
					BEGIN 
						Set @TotalUnitAmountFundTo = round(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo) 
						IF @UnitDecimalPlacesFundTo = 0 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 1
						END
						IF @UnitDecimalPlacesFundTo = 2 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.01
						END
						IF @UnitDecimalPlacesFundTo = 4 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.0001
						END
						IF @UnitDecimalPlacesFundTo = 6 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.000001
						END
						IF @UnitDecimalPlacesFundTo = 8 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.00000001
						END
					END
					If @UnitRoundingModeFundTo = 2 BEGIN Set  @TotalUnitAmountFundTo = ROUND(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo,1) END
					If @UnitRoundingModeFundTo = 3 BEGIN Set  @TotalUnitAmountFundTo = ROUND(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo) END
				END
			END
			ELSE
			BEGIN
				set @TotalUnitAmountFundFrom = 0
				set @TotalUnitAmountFundTo = 0
			    set @UnitAmount = 0

				if @FeeType = 'OUT'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

				if @FeeType = 'IN'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						if @ClientCode = '08'
							set @FinalFeeAmount = case when @paramSwitchingFeeAmount > 0 then @paramSwitchingFeeAmount else @CashAmount - (@CashAmount / (@FinalFeePercent + 100)*100) end
						else
							set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom - @FinalFeeAmount
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						if @ClientCode = '08'
							set @FinalFeePercent = (@CashAmount / @FinalFeeAmount ) - 100
						else
							set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom  - @FinalFeeAmount
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

			END
		END
		ELSE IF @paramUnitAmount > 0 BEGIN
			
			if @NavFundFrom > 0  and @NavFundTo > 0
			BEGIN
				set @UnitAmount = @paramUnitAmount
				If @JournalRoundingMode = 1 BEGIN 
					Set  @CashAmount = ROUND(@UnitAmount * @NavFundFrom,@JournalDecimalPlaces) 

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
				If @JournalRoundingMode = 2 BEGIN Set   @CashAmount = ROUND(@UnitAmount * @NavFundFrom,@JournalDecimalPlaces,1) END
				If @JournalRoundingMode = 3 BEGIN Set   @CashAmount = ROUND(@UnitAmount * @NavFundFrom,@JournalDecimalPlaces) END

				if @FeeType = 'OUT'
				BEGIN
					select @CashAmount,@FeeTypeMode,@paramSwitchingFeePercent,@paramSwitchingFeeAmount
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount - @FinalFeeAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

				if @FeeType = 'IN'
				BEGIN
					IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
					BEGIN
						set @FinalFeePercent = @paramSwitchingFeePercent
						if @ClientCode = '08'
							set @FinalFeeAmount = case when @paramSwitchingFeeAmount > 0 then @paramSwitchingFeeAmount else @CashAmount - (@CashAmount / (@FinalFeePercent + 100)*100) end
						else
							set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom - @FinalFeeAmount
					END
					ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
					BEGIN
						set @FinalFeeAmount = @paramSwitchingFeeAmount
						if @ClientCode = '08'
							set @FinalFeePercent = (@CashAmount / @FinalFeeAmount ) - 100
						else
							set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom  - @FinalFeeAmount
					END
					ELSE
					BEGIN
						Set @TotalCashAmountFundFrom =  @CashAmount
						set @TotalCashAmountFundTo = @TotalCashAmountFundFrom
					
					END
				END

				--HANDLE UNIT
				BEGIN
					If @UnitRoundingModeFundFrom = 1 
					BEGIN 
						Set @TotalUnitAmountFundFrom = round(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom) 
						IF @UnitDecimalPlacesFundFrom = 0 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 1
						END
						IF @UnitDecimalPlacesFundFrom = 2 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.01
						END
						IF @UnitDecimalPlacesFundFrom = 4 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.0001
						END
						IF @UnitDecimalPlacesFundFrom = 6 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.000001
						END
						IF @UnitDecimalPlacesFundFrom = 8 BEGIN
						set @TotalUnitAmountFundFrom = @TotalUnitAmountFundFrom + 0.00000001
						END
					END
					If @UnitRoundingModeFundFrom = 2 BEGIN Set  @TotalUnitAmountFundFrom = ROUND(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom,1) END
					If @UnitRoundingModeFundFrom = 3 BEGIN Set  @TotalUnitAmountFundFrom = ROUND(@TotalCashAmountFundFrom / @NavFundFrom,@UnitDecimalPlacesFundFrom) END


					If @UnitRoundingModeFundTo = 1 
					BEGIN 
						Set @TotalUnitAmountFundTo = round(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo) 
						IF @UnitDecimalPlacesFundTo = 0 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 1
						END
						IF @UnitDecimalPlacesFundTo = 2 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.01
						END
						IF @UnitDecimalPlacesFundTo = 4 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.0001
						END
						IF @UnitDecimalPlacesFundTo = 6 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.000001
						END
						IF @UnitDecimalPlacesFundTo = 8 BEGIN
						set @TotalUnitAmountFundTo = @TotalUnitAmountFundTo + 0.00000001
						END
					END
					If @UnitRoundingModeFundTo = 2 BEGIN Set  @TotalUnitAmountFundTo = ROUND(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo,1) END
					If @UnitRoundingModeFundTo = 3 BEGIN Set  @TotalUnitAmountFundTo = ROUND(@TotalCashAmountFundTo / @NavFundTo,@UnitDecimalPlacesFundTo) END
				END

			END
			ELSE 
			BEGIN
				IF @FeeTypeMode = 1 and @paramSwitchingFeePercent > 0
				BEGIN
					set @FinalFeePercent = @paramSwitchingFeePercent
					set @FinalFeeAmount = @CashAmount * @FinalFeePercent / 100
				END
				ELSE IF @FeeTypeMode = 2 and @paramSwitchingFeeAmount > 0
				BEGIN
					set @FinalFeeAmount = @paramSwitchingFeeAmount
					set @FinalFeePercent = (@FinalFeeAmount / @CashAmount) * 100
				END
				set @UnitAmount = @paramUnitAmount
				set @CashAmount = 0
				set @TotalCashAmountFundFrom = 0
				set @TotalCashAmountFundTo = 0
				set @TotalUnitAmountFundFrom = 0
				set @TotalCashAmountFundTo = 0
			END
		END

	    Update ClientSwitching set NavFundFrom=isnull(@NavFundFrom,0),NavFundTo=isnull(@NavFundTo,0), 
	    CashAmount = isnull(@CashAmount,0), TotalCashAmountFundFrom = isnull(@TotalCashAmountFundFrom,0),
	    TotalCashAmountFundTo = isnull(@TotalCashAmountFundTo,0), TotalUnitAmountFundFrom = isnull(@TotalUnitAmountFundFrom,0),
	    TotalUnitAmountFundTo = isnull(@TotalUnitAmountFundTo,0), UnitAmount = isnull(@UnitAMount,0), SwitchingFeeAmount = isnull(@FinalFeeAmount,0),
	    SwitchingFeePercent = isnull(@FinalFeePercent,0)
	    , UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
	    where ClientSwitchingpk = @ClientSwitchingPK and status = 2 and Posted = 0


	 --   select  ISNULL(@FinalFeeAmount,0) SwitchingFeeAmount, 
	 --   ISNULL(@FinalFeePercent,0) SwitchingFeePercent,@NavFundFrom NavFundFrom,@NavFundTo NavFundTo, isnull(@CashAmount,0)CashAmount
		--,@UnitAmount unitAmount,
	 --   isnull(@TotalCashAmountFundFrom,0)TotalCashAmountFundFrom,isnull(@TotalCashAmountFundTo,0) TotalCashAmountFundTo
		--, isnull(@TotalUnitAmountFundFrom,0) TotalUnitAmountFundFrom,
	 --   isnull(@TotalUnitAmountFundTo,0) TotalUnitAmountFundTo,@paramUnitAmount paramUnitAmount,@ClientSwitchingPK, @FeeTypeMode
 
    Fetch next From A Into @ClientSwitchingPK
    end
    Close A
    Deallocate A
                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _usersID);
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

        public string SInvestSwitchingRpt_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {

            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }


                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                                    BEGIN  
                                    SET NOCOUNT ON 
                                    --drop table #text   
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
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundFromSinvest,''))))
                                    + '|' + case when A.CashAmount = 0 then '' else cast(isnull(cast(A.CashAmount as decimal(22,2)),'')as nvarchar) end
                                    + '|' + case when A.UnitAmount = 0 then '' else cast(isnull(cast(A.UnitAmount as decimal(22,4)),'') as nvarchar) end
                                    + '|' + case when A.BitSwitchingAll = 1 then 'Y' else '' end
                                    + '|' + case when A.Feetype = 'OUT' then '1' else '2' end
                                    + '|' + 
                                    + '|' + 
                                    + '|' + case when A.FeePercent = 0 then '' else cast(isnull(cast(A.FeePercent as decimal(10,2)),'')as nvarchar) end
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundToSinvest,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), SettlementDate, 112) <> '19000101' then CONVERT(VARCHAR(10), SettlementDate, 112) else '' End),''))))          
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) 

                                    from (   
  
                                    Select CW.FundPKFrom,CW.FundPKTo,CW.ValueDate,F.SInvestCode FundFromSinvest, F1.SInvestCode FundToSinvest,CW.PaymentDate SettlementDate,CW.SwitchingFeePercent FeePercent,CW.SwitchingFeeAmount FeeAmount,'3' Type,ROUND(CashAmount,2)CashAmount ,ROUND(UnitAmount,4)UnitAmount ,CW.BitSwitchingAll BitSwitchingAll,TransferType TransferType
                                    ,Case When ReferenceSInvest <> '' and ReferenceSInvest is not null then ReferenceSInvest else cast(ClientSwitchingPK as nvarchar) end Reference,FC.IFUACode,CW.FeeType
                                    from ClientSwitching CW 
                                    left join Fund F on CW.FundPKFrom = F.fundPK and f.Status=2 
                                    left join fund F1 on CW.FundPKTo = F1.FundPK and F1.status = 2 
                                    left join FundClient FC on CW.FundClientPK = FC.FundClientPK and fc.Status=2      
                                    where ValueDate =   @ValueDate and " + paramClientSwitchingSelected + @" and Cw.status = 2
                                    )A    
                                    Group by A.FundPKFrom,A.FundPKTo,A.ValueDate,A.FundFromSinvest,A.FundToSinvest,A.FeePercent,A.SettlementDate,A.FeeAmount,A.Type,A.CashAmount,A.UnitAmount,A.BitSwitchingAll,A.TransferType,A.Reference,A.IFUACode,A.FeeType
                                    order by A.ValueDate Asc
                                    select * from #text          
                                    END ";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_Order.txt";
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
                                    return Tools.HtmlSinvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_Order.txt";
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
        //mekel
        public Boolean ClientSwitchingBatchFormBySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientSwitching _clientSwitching)
        {
            try
            {

                string paramClientSwitchingSelected = "";
                if (!_host.findString(_clientSwitching.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientSwitching.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _clientSwitching.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =

                        @" BEGIN
                            SELECT 'Switching' Type,ClientSwitchingPK,D.ContactPerson ContactPerson,D.ID BankBranchID,D.Fax1 FaxNo,ValueDate Date,NavDate NavDate, C.ID FundIDFrom,C1.ID FundIDTo, C.Name FundNameFrom,C1.Name FundNameTo,B.ID ClientID, 
                            B.Name ClientName,A.NAVFundFrom NAVFundFrom,A.NAVFundTo NAVFundTo,CashAmount CashFundFrom,unitamount UnitAmount, A.SwitchingFeePercent FeePercent,A.SwitchingFeeAmount FeeAmount, 
                            A.PaymentDate SettlementDate,A.Description Remark,A.TotalUnitAmountFundFrom TotalUnitFundFrom,A.TotalCashAmountFundFrom TotalCashFundFrom,A.TotalCashAmountFundTo TotalCashFundTo,B.NPWP NPWP,  E.Name Agent,F.Name Mgt  from ClientSwitching A    
                            left join FundClient B ON B.fundclientpk = A.fundclientpk and B.status = 2   
                            left join Fund C ON A.FundPKFrom = C.FundPK  and C.status = 2  
                            left join Fund C1 ON A.FundPKTo = C1.FundPK  and C1.status = 2     
                            left join BankBranch D ON C.BankBranchPK = D.BankBranchPK  and D.status = 2
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.Status = 2
                            left join Agent F on E.ParentPK = F.AgentPK and F.Status = 2  
                            WHERE   A.status not in (3,4) and NAVDate between @DateFrom and @DateTo and " + paramClientSwitchingSelected + @" and A.Type <> 3 
                            END  ";
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
                                string filePath = Tools.ReportsPath + "BatchFormSWITCHInstructionBySelected" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormSWITCHInstructionBySelected" + "_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Switching Report");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ClientSwitchingPK = Convert.ToInt32(dr0["ClientSwitchingPK"]);
                                        rSingle.FundIDFrom = Convert.ToString(dr0["FundIDFrom"]);
                                        rSingle.FundIDTo = Convert.ToString(dr0["FundIDTo"]);
                                        rSingle.FundNameFrom = Convert.ToString(dr0["FundNameFrom"]);
                                        rSingle.FundNameTo = Convert.ToString(dr0["FundNameTo"]);
                                        rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                        rSingle.NAVFundFrom = Convert.ToDecimal(dr0["NAVFundFrom"]);
                                        rSingle.NAVFundTo = Convert.ToDecimal(dr0["NAVFundTo"]);
                                        rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                        rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                        rSingle.TotalUnitFundFrom = Convert.ToDecimal(dr0["TotalUnitFundFrom"]);
                                        rSingle.CashFundFrom = Convert.ToDecimal(dr0["CashFundFrom"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                        rSingle.TotalCashFundFrom = Convert.ToDecimal(dr0["TotalCashFundFrom"]);
                                        rSingle.TotalCashFundTo = Convert.ToDecimal(dr0["TotalCashFundTo"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                        rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                        rSingle.BankCustodiID = Convert.ToString(dr0["BankBranchID"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                        rSingle.Agent = Convert.ToString(dr0["Agent"]);
                                        rSingle.Mgt = Convert.ToString(dr0["Mgt"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.FundNameFrom, r.NAVDate, r.ContactPerson, r.FaxNo, r.BankCustodiID } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;


                                    foreach (var rsHeader in QueryByFundID)
                                    {


                                        incRowExcel = incRowExcel + 2;

                                        incRowExcel = incRowExcel + 2;

                                        int RowZ = incRowExcel;
                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 9].Value = "To ";
                                        worksheet.Cells[incRowExcel, 10].Value = ": ";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.BankCustodiID;
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).Height = 200;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyAddress();
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.WrapText = true;

                                        worksheet.Cells[incRowExcel, 11].Value = "Custodian Services";
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 9].Value = "Attention ";
                                        worksheet.Cells[incRowExcel, 10].Value = ": ";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ContactPerson;
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 9].Value = "Fax ";
                                        worksheet.Cells[incRowExcel, 10].Value = ": ";
                                        worksheet.Cells[incRowExcel, 11].Value = _host.Get_CompanyFax();
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 1].Value = "Tel : ";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyPhone();
                                        worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 3].Value = "Fax : ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyFax();
                                        worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 5].Value = "SWITCHING BATCH FORM";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Size = 65;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 2;

                                        //incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundNameFrom;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "NAV Date ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.NAVDate;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                        incRowExcel++;


                                        incRowExcel = incRowExcel + 1;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Row(RowB).Height = 100;
                                        worksheet.Row(RowG).Height = 100;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "CUSTOMER ID";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 5].Value = "SWITCH FROM";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":K" + RowB].Merge = true;
                                        worksheet.Cells["E" + RowB + ":J" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":J" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 5].Value = "FUND";
                                        worksheet.Cells[RowG, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowG + ":E" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowG + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowG + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 6].Value = "NO. OF UNITS";
                                        worksheet.Cells[RowG, 6].Style.Font.Bold = true;
                                        worksheet.Cells["F" + RowG + ":F" + RowG].Merge = true;
                                        worksheet.Cells["F" + RowG + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["F" + RowG + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 7].Value = "NOMINAL AMOUNT";
                                        worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                        worksheet.Cells["G" + RowG + ":G" + RowG].Merge = true;
                                        worksheet.Cells["G" + RowG + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["G" + RowG + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 8].Value = "% FEE";
                                        worksheet.Cells[RowG, 8].Style.Font.Bold = true;
                                        worksheet.Cells["H" + RowG + ":H" + RowG].Merge = true;
                                        worksheet.Cells["H" + RowG + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["H" + RowG + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 9].Value = "AMOUNT FEE";
                                        worksheet.Cells[RowG, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowG + ":I" + RowG].Merge = true;
                                        worksheet.Cells["I" + RowG + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["I" + RowG + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 10].Value = "NET AMOUNT";
                                        worksheet.Cells[RowG, 10].Style.Font.Bold = true;
                                        worksheet.Cells["J" + RowG + ":K" + RowG].Merge = true;
                                        worksheet.Cells["J" + RowG + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["J" + RowG + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 12].Value = "SWITCH TO";
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells["L" + RowB + ":M" + RowB].Merge = true;
                                        worksheet.Cells["L" + RowB + ":L" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["L" + RowB + ":L" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 12].Value = "FUND";
                                        worksheet.Cells[RowG, 12].Style.Font.Bold = true;
                                        worksheet.Cells["L" + RowG + ":L" + RowG].Merge = true;
                                        worksheet.Cells["L" + RowG + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["L" + RowG + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[RowG, 13].Value = "NET AMOUNT";
                                        worksheet.Cells[RowG, 13].Style.Font.Bold = true;
                                        worksheet.Cells["M" + RowG + ":M" + RowG].Merge = true;
                                        worksheet.Cells["M" + RowG + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["M" + RowG + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 14].Value = "REMARKS";
                                        worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                        worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                        worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



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

                                            worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.FundIDFrom;
                                            worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.TotalUnitFundFrom;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.CashFundFrom;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeeAmount;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.TotalCashFundFrom;
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.FundIDTo;
                                            //worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalCashFundTo;
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 14].Value = rsDetail.Remark;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }


                                        worksheet.Cells["A" + _endRowDetail + ":N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                        worksheet.Cells[incRowExcel, 11].Calculate();
                                        worksheet.Cells[incRowExcel, 13].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;

                                        worksheet.Cells[incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
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
                                        worksheet.Cells[incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 14].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        incRowExcel = incRowExcel + 10;
                                        worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 20;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1].Value = _clientSwitching.Signature1Desc;
                                        worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 5].Value = _clientSwitching.Signature2Desc;
                                        worksheet.Cells["H" + incRowExcel + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 8].Value = _clientSwitching.Signature3Desc;
                                        worksheet.Cells["L" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 12].Value = _clientSwitching.Signature4Desc;
                                        incRowExcel++;

                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }
                                    //incRowExcel++;
                                    int _lastRow = incRowExcel;

                                    string _rangeA = "A5:N" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 50;
                                    }

                                    worksheet.DeleteRow(_lastRow);

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
                                    worksheet.Column(1).Width = 25;
                                    worksheet.Column(2).Width = 75;
                                    worksheet.Column(3).Width = 25;
                                    worksheet.Column(4).Width = 80;
                                    worksheet.Column(5).Width = 95;
                                    worksheet.Column(6).Width = 60;
                                    worksheet.Column(7).Width = 90;
                                    worksheet.Column(8).Width = 65;
                                    worksheet.Column(9).Width = 65;
                                    worksheet.Column(10).Width = 5;
                                    worksheet.Column(11).Width = 65;
                                    worksheet.Column(12).Width = 65;
                                    worksheet.Column(13).Width = 65;
                                    worksheet.Column(14).Width = 55;


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
        
        public Boolean ClientSwitchingBatchFormBySelectedDataMandiri(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientSwitching _clientSwitching)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_clientSwitching.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientSwitching.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _clientSwitching.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }


                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" 
                        BEGIN
                        SELECT isnull(BN.ID,'') + ' - ' + isnull(D.ID,'') BankCustodiID, G.ID CurrencyID, D.Address CustodyAddress, isnull(D.ContactPerson,'') ContactPerson, isnull(D.Fax1,'') FaxNo, B.Name InvestorName, NavDate NavDate, ValueDate Date, B.IFUACode IFUA,
                        C.Name FundFrom,C1.Name FundTo, unitamount UnitTransaction, CashAmount GrossAmount, A.SwitchingFeePercent FeePercent,A.SwitchingFeeAmount FeeAmount,A.Description Remark  from ClientSwitching A    
                        left join FundClient B ON B.fundclientpk = A.fundclientpk and B.status = 2   
                        left join Fund C ON A.FundPKFrom = C.FundPK  and C.status = 2  
                        left join Fund C1 ON A.FundPKTo = C1.FundPK  and C1.status = 2     
                        left join BankBranch D ON C.BankBranchPK = D.BankBranchPK  and D.status = 2
                        left join Bank BN on D.BankPK = BN.BankPK and BN.status = 2
                        left join Agent E on B.SellingAgentPK = E.AgentPK and E.Status = 2
                        left join Agent F on E.ParentPK = F.AgentPK and F.Status = 2  
                        left join Currency G on A.CurrencyPK = G.CurrencyPK and G.Status = 2  
                        WHERE A.status not in (3,4) and NAVDate between @DateFrom and @DateTo and " + paramClientSwitchingSelected + @"
                        END ";
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
                                string filePath = Tools.ReportsPath + "BatchFormSWITCHInstructionBySelected" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormSWITCHInstructionBySelected" + "_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Switching Report");


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
                                        rSingle.FundFrom = Convert.ToString(dr0["FundFrom"]);
                                        rSingle.FundTo = Convert.ToString(dr0["FundTo"]);
                                        rSingle.UnitTransaction = Convert.ToDecimal(dr0["UnitTransaction"]);
                                        rSingle.GrossAmount = Convert.ToDecimal(dr0["GrossAmount"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rSingle.BankCustodiID = Convert.ToString(dr0["BankCustodiID"]);
                                        rSingle.CustodyAddress = Convert.ToString(dr0["CustodyAddress"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.NAVDate, r.Date, r.FundName, r.ContactPerson, r.FaxNo, r.BankCustodiID, r.CustodyAddress } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;
                                    int _startRowDetail = 0;
                                    int _endRowDetail = 0;
                                    foreach (var rsHeader in QueryByFundID)
                                    {
                                        incRowExcel++;

                                        int RowZ = incRowExcel;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
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
                                        worksheet.Cells[incRowExcel, 1].Value = "Batch Form-Switching";
                                        worksheet.Cells[incRowExcel, 11].Value = "Attn : " + rsHeader.Key.ContactPerson;
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 11].Value = "Fax No. : " + rsHeader.Key.ContactPerson;
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


                                        worksheet.Cells[incRowExcel, 4].Value = "Fund";
                                        worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        
                                        worksheet.Cells[incRowExcel, 6].Value = "Unit Transaction";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = "CCY";
                                        worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 8].Value = "Gross Transaction Amount";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                        worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 9].Value = "Fee";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                        worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 11].Value = "Net Transaction Amount";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 4].Value = "Switch From";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;

                                        worksheet.Cells[incRowExcel, 5].Value = "Switch To";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;

                                        worksheet.Cells[incRowExcel, 9].Value = "PCT";
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;

                                        worksheet.Cells[incRowExcel, 10].Value = "Amount";
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells["A" + RowB + ":K" + RowG].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + RowB + ":K" + RowG].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                        incRowExcel++;

                                        //area header

                                        int _no = 1;
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.InvestorName;
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.IFUA;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.FundFrom;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.FundTo;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.UnitTransaction;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.CurrencyID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.GrossAmount;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.FeeAmount;
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(H" + incRowExcel + "-J" + incRowExcel + ")";
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                            
                                            _endRowDetail = incRowExcel;

                                            _no++;
                                            incRowExcel++;

                                            worksheet.Cells["A" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        }


                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 11].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                        incRowExcel = incRowExcel + 6;

                                        worksheet.Cells[incRowExcel, 1].Value = "Checked By";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        
                                        worksheet.Cells[incRowExcel, 4].Value = "Approved By";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 10;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;


                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Row(incRowExcel).PageBreak = true;
                                        incRowExcel++;

                                    }
                                    //incRowExcel++;
                                    int _lastRow = incRowExcel;


                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(2).Width = 35;
                                    worksheet.Column(3).Width = 20;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(5).Width = 30;
                                    worksheet.Column(6).Width = 25;
                                    worksheet.Column(7).Width = 10;
                                    worksheet.Column(8).Width = 30;
                                    worksheet.Column(9).Width = 20;
                                    worksheet.Column(10).Width = 30;
                                    worksheet.Column(11).Width = 30;


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B REDEMPTION \n &28&B Batch Form";

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "\n   &20&B SWITCHING \n &20&B Batch Form \n";
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

        public bool Validate_ApproveBySelected(DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
                }
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" if Exists(select * From ClientSwitching where Status = 2 and ValueDate between @ValueDateFrom and @ValueDateTo and " + paramClientSwitchingSelected + @" and Posted = 0 and Revised = 0 
                         and (isnull(NavFundFrom,0) = 0 or isnull(NavFundTo,0) = 0  or isnull(TotalCashAmountFundFrom,0) = 0 or isnull(TotalCashAmountFundTo,0) = 0))
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

        //GetTransferType
        public decimal ClientSwitching_GetTransferType(string _unitAmount, DateTime _navDate, int _fundPKFrom)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                             Select Distinct isnull([dbo].[FgetLastCloseNav] (@NAVDate,@FundPK),0) * @UnitAmount CashAmount from CloseNAV where status = 2 group by fundPK
                               ";
                        cmd.Parameters.AddWithValue("@UnitAmount", _unitAmount);
                        cmd.Parameters.AddWithValue("@NAVDate", _navDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPKFrom);
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
//
//							
//						    select b.UnitAmount,a.RemainingBalanceUnit,case when b.UnitAmount = 0 then 0 else case when RemainingBalanceUnit < b.UnitAmount then 0 else case when RemainingBalanceUnit > b.UnitAmount then 1 end end end hasil from Fund a left join #UnitAmountTemps b on a.FundPK = b.fundpk where b.FundPK = @FundPK
//							";

//                        }

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

        public void ClientSwitching_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = "  ClientSwitchingPK in (0) ";
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
                                 Select @Time,@PermissionID,'ClientSwitching',ClientSwitchingPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 2 and " + paramClientSwitchingSelected + @" and Posted = 0 
                                 update ClientSwitching set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 2 and ClientSwitchingPK in ( Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 2 and " + paramClientSwitchingSelected + @" and Posted = 0) ";

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

        public string Validate_CheckDescription(DateTime _dateFrom, DateTime _dateTo, string _table, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramSelected = " and " + _table + "PK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramSelected = " and " + _table + "PK in (0) ";
                }

                if (Tools.ClientCode != "08")
                {
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"
                        Create Table #DescriptionTemp
                        (PK nvarchar(50))
                        
                        Insert Into #DescriptionTemp(PK)
                        select " + _table + @"PK from " + _table + @" where ValueDate between @DateFrom and @DateTo and status = 1 " + paramSelected + @"
                        and (Description is null or Description = '')

                        if exists(select " + _table + @"PK from " + _table + @" where ValueDate between @DateFrom and @DateTo and status = 1 " + paramSelected + @"
                        and (Description is null or Description = ''))
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + PK
                        FROM #DescriptionTemp
                        SELECT 'Reject Cancel, Please Check Description in SysNo : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END    ";

                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    return Convert.ToString(dr["Result"]);

                                }
                                return "";
                            }
                        }
                    }
                }
                else
                {
                    return "";
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Validate_CheckClientAPERD(DateTime _dateFrom, DateTime _dateTo, string _table, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramSelected = "  CS.ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramSelected = "  CS.ClientSwitchingPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                        DECLARE @combinedString VARCHAR(MAX)
                      Create Table #ClientSubsTemp
                        (PK nvarchar(50))
                        
                        Insert Into #ClientSubsTemp(PK)
                        select CS.ClientSwitchingPK from ClientSwitching CS
						left join FundClient F on CS.FundClientPK = F.FundClientPK and F.status in (1,2)
						left join AgentSwitching B on CS.ClientSwitchingPK = B.ClientSwitchingPK and B.Status in (1,2) 
						where ValueDate between @DateFrom and @DateTo and CS.status = 1 and " + paramSelected + @"
                        and (F.SACode != '') and (B.AgentPK = 1 or B.AgentPK is null) 

                        if not exists( select * from AgentSwitching A
						where ClientSwitchingPK in ( select * from #ClientSubsTemp
                        ) and status in (1,2) and A.AgentPK <> 1 and A.Status = 2 )
                        BEGIN
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + PK
                        FROM #ClientSubsTemp
                        SELECT case when @combinedString is null then '' else 'Approved Cancel, Please Check AgentTrailerFee in SysNo : ' + @combinedString end as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientSwitching_PostingJournalBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = " And ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = " And ClientSwitchingPK in (0) ";
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
                            _parambitManageUR = " and A.status = 2   and A.Posted = 0 and A.ClientSwitchingPK in (select PK from ZManage_UR where Selected = 1 and Type = 3 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = " and A.Status = 2 and Posted  = 0 and Revised  = 0  and ValueDate between @datefrom and @dateto " + paramClientSwitchingSelected;
                        }


                        cmd.CommandText =
                           @"
                            --declare @datefrom date
--declare @dateto date

--set @datefrom = '2020-04-02'
--set @dateto = '2020-04-02'

--drop table #ClientSwitchIn
--drop table #ClientSwitchOut
--drop table #JournalHeader
--drop table #JournalDetail


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

	                            CREATE TABLE #ClientSwitchIn
	                            (
		                            FundJournalPK int,
		                            NAVDate datetime,
		                            PaymentDate datetime,
		                            Reference nvarchar(100),
		                            ClientSwitchingPK int,
		                            FundPKFrom int,
		                            FundPKTo int,
		                            FundClientPK int,
		                            FundCashRefPK int,
		                            TransactionPK nvarchar(200),
		                            Type int,
		                            TotalUnitAmountFrom numeric(19,6),
		                            TotalUnitAmountTo numeric(19,6),
		                            TotalCashAmountFrom numeric(19,4),
		                            TotalCashAmountTo numeric(19,4),
		                            UnitAmount numeric(19,4),
		                            CashAmount numeric(19,4),
		                            SwitchingFeeAmount numeric(19,4),
		                            BitPendingSwitching int,
		                            SwitchInDueDate int
	                            )

	                            CREATE CLUSTERED INDEX indx_ClientSwitchIn ON #ClientSwitchIn (FundJournalPK,FundPKFrom,FundPKTo,FundClientPK);

	                            CREATE TABLE #ClientSwitchOut
	                            (
		                            FundJournalPK int,
		                            NAVDate datetime,
		                            PaymentDate datetime,
		                            Reference nvarchar(100),
		                            ClientSwitchingPK int,
		                            FundPKFrom int,
		                            FundPKTo int,
		                            FundClientPK int,
		                            FundCashRefPK int,
		                            TransactionPK nvarchar(200),
		                            Type int,
		                            TotalUnitAmountFrom numeric(19,6),
		                            TotalUnitAmountTo numeric(19,6),
		                            TotalCashAmountFrom numeric(19,4),
		                            TotalCashAmountTo numeric(19,4),
		                            UnitAmount numeric(19,4),
		                            CashAmount numeric(19,4),
		                            SwitchingFeeAmount numeric(19,4),
		                            BitPendingSwitching int
	                            )

	                            CREATE CLUSTERED INDEX indx_ClientSwitchOut ON #ClientSwitchOut (FundJournalPK,FundPKFrom,FundPKTo,FundClientPK);

                            END

                            --JOURNAL
                            BEGIN
	                            insert into #ClientSwitchOut
	                            (
		                            FundJournalPK,
		                            NAVDate,
		                            PaymentDate,
		                            Reference,
		                            ClientSwitchingPK,
		                            FundPKFrom,
		                            FundPKTo,
		                            FundClientPK,
		                            FundCashRefPK,
		                            TransactionPK,
		                            Type,
		                            TotalUnitAmountFrom,
		                            TotalUnitAmountTo,
		                            TotalCashAmountFrom,
		                            TotalCashAmountTo,
		                            UnitAmount,
		                            CashAmount,
		                            SwitchingFeeAmount,
		                            BitPendingSwitching
	                            )
	                            Select ROW_NUMBER() OVER	(ORDER BY A.ClientSwitchingPK ASC) Number ,
			                            A.NAVDate,
			                            A.PaymentDate,
			                            '',
			                            A.ClientSwitchingPK ,
			                            A.FundPKFrom ,
			                            A.FundPKTo ,
			                            A.FundClientPK ,
			                            A.CashRefPKFrom,
			                            A.TransactionPK,
			                            A.Type,
			                            isnull(A.TotalUnitAmountFundFrom,0),
			                            isnull(A.TotalUnitAmountFundTo,0),
			                            isnull(A.TotalCashAmountFundFrom,0),
			                            isnull(A.TotalCashAmountFundTo,0),
			                            isnull(A.UnitAmount,0),
			                            isnull(A.CashAmount,0),
			                            isnull(A.SwitchingFeeAmount,0),
			                            isnull(C.BitPendingSwitchIn,0)
	                            From ClientSwitching A 
	                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	                            LEFT JOIN FundFee C on A.FundPKFrom = C.FundPK and C.status in (1,2) and date = 
	                            (
		                            select max(date) from FundFee where fundpk = A.FundPKFrom and status = 2
	                            )
	                            Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto
	                            and A.FeeType = 'OUT'  and A.UnitAmount > 0 and A.TotalUnitAmountFundTo > 0
	                            " + _parambitManageUR + @"
	                            order by Number asc

	                            insert into #ClientSwitchIn
	                            (
		                            FundJournalPK,
		                            NAVDate,
		                            PaymentDate,
		                            Reference,
		                            ClientSwitchingPK,
		                            FundPKFrom,
		                            FundPKTo,
		                            FundClientPK,
		                            FundCashRefPK,
		                            TransactionPK,
		                            Type,
		                            TotalUnitAmountFrom,
		                            TotalUnitAmountTo,
		                            TotalCashAmountFrom,
		                            TotalCashAmountTo,
		                            UnitAmount,
		                            CashAmount,
		                            SwitchingFeeAmount,
		                            BitPendingSwitching,
		                            SwitchInDueDate
	                            )
	                            Select ROW_NUMBER() OVER	(ORDER BY A.ClientSwitchingPK ASC) Number ,
			                            A.NAVDate,
			                            A.PaymentDate,
			                            '',
			                            A.ClientSwitchingPK ,
			                            A.FundPKFrom ,
			                            A.FundPKTo ,
			                            A.FundClientPK ,
			                            A.CashRefPKFrom,
			                            A.TransactionPK,
			                            A.Type,
			                            isnull(A.TotalUnitAmountFundFrom,0),
			                            isnull(A.TotalUnitAmountFundTo,0),
			                            isnull(A.TotalCashAmountFundFrom,0),
			                            isnull(A.TotalCashAmountFundTo,0),
			                            isnull(A.UnitAmount,0),
			                            isnull(A.CashAmount,0),
			                            isnull(A.SwitchingFeeAmount,0),
			                            isnull(C.BitPendingSwitchIn,0),
			                            isnull(D.SwitchInDueDate,0)
	                            From ClientSwitching A 
	                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	                            LEFT JOIN FundFee C on A.FundPKFrom = C.FundPK and C.status in (1,2) and date = 
	                            (
		                            select max(date) from FundFee where fundpk = A.FundPKFrom and status = 2
	                            )
	                            LEFT JOIN FUND D on A.FundPKTo = D.FundPK and D.Status in (1,2) 
	                            Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto
	                            and A.FeeType = 'IN'  and A.UnitAmount > 0 and A.TotalUnitAmountFundTo > 0
	                            " + _parambitManageUR + @"
	                            order by Number asc
	
	                            --JOURNAL OUT
	                            BEGIN
		                            Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		                            Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		                            --FUND OUT
		                            BEGIN
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

			                            Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Client Switching Out, Fee Type OUT'
					                            ,@PeriodPK,dbo.fworkingday(A.NAVDate ,1),13,A.ClientSwitchingPK,'SWITCHING',isnull(A.Reference,''),'Switching Client: ' + B.ID + ' - ' + B.Name 
			                            From #ClientSwitchOut A  
			                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
			                            where A.TotalCashAmountFrom > 0 
		
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
			                            ,B.Redemption,isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
			                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'D',A.CashAmount
			                            ,A.CashAmount,0
			                            ,1
			                            ,A.CashAmount,0
			                            From #ClientSwitchOut A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.Redemption = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
			                            Where A.TotalCashAmountFrom > 0

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
			                            ,B.PendingRedemption,isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
			                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'C',A.TotalCashAmountFrom
			                            ,0,A.TotalCashAmountFrom
			                            ,1
			                            ,0,A.TotalCashAmountFrom
			                            From #ClientSwitchOut A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.PendingRedemption = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
			                            Where A.TotalCashAmountFrom > 0

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
			                            ,B.PayableSwitchingFee,isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
			                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'C',A.SwitchingFeeAmount
			                            ,0,A.SwitchingFeeAmount
			                            ,1
			                            ,0,A.SwitchingFeeAmount
			                            From #ClientSwitchOut A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.PayableSwitchingFee = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
			                            Where A.SwitchingFeeAmount > 0

			                            --FUND OUT Payable ke BANK
			                            begin
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

				                            Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Client Switching Out Payment Date, Fee Type OUT'
						                            ,@PeriodPK,A.PaymentDate,13,A.ClientSwitchingPK,'SWITCHING',isnull(A.Reference,''),'Switching Client: ' + B.ID + ' - ' + B.Name 
				                            From #ClientSwitchOut A  
				                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
				                            where A.TotalCashAmountFrom > 0

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
				                            ,B.PendingRedemption,isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
				                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'D',A.TotalCashAmountFrom
				                            ,A.TotalCashAmountFrom,0
				                            ,1
				                            ,A.TotalCashAmountFrom,0
				                            From #ClientSwitchOut A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
				                            LEFT JOIN FundJournalAccount C ON B.PendingRedemption = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
				                            Where A.TotalCashAmountFrom > 0

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
				                            ,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
				                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'C',A.TotalCashAmountFrom
				                            ,0,A.TotalCashAmountFrom
				                            ,1
				                            ,0,A.TotalCashAmountFrom
				                            From #ClientSwitchOut A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
				                            LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status = 2
				                            LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
				                            Where A.TotalCashAmountFrom > 0
			                            END
		                            END

		                            --FUND IN
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

			                            Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Client Switching in A, Fee Type OUT'
					                            ,@PeriodPK,dbo.fworkingday(A.NAVDate ,1),13,A.ClientSwitchingPK,'SWITCHING',isnull(A.Reference,''),'Switching Client: ' + B.ID + ' - ' + B.Name 
			                            From #ClientSwitchOut A  
			                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
			                            where A.TotalCashAmountTo > 0 
		
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
			                            ,case when A.BitPendingSwitching = 1 then B.PendingSwitching else B.PendingSubscription end,case when A.BitPendingSwitching = 1 then isnull(C.CurrencyPK,1) else D.CurrencyPK end,A.FundPKTo,A.FundClientPK,0
			                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'D',A.TotalCashAmountTo
			                            ,A.TotalCashAmountTo,0
			                            ,1
			                            ,A.TotalCashAmountTo,0
			                            From #ClientSwitchOut A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKTo = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.PendingSwitching = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundJournalAccount D ON B.PendingSubscription = D.FundJournalAccountPK AND D.status IN (1,2)
			                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
			                            Where A.TotalCashAmountTo > 0

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
			                            ,B.Subscription,isnull(C.CurrencyPK,1),A.FundPKTo,A.FundClientPK,0
			                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'C',A.TotalCashAmountTo
			                            ,0,A.TotalCashAmountTo
			                            ,1
			                            ,0,A.TotalCashAmountTo
			                            From #ClientSwitchOut A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKTo = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.Subscription = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
			                            Where A.TotalCashAmountTo > 0

			                            --FUND IN PAYMENT
			                            begin
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

				                            Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Client Switching in payment date, Fee Type OUT'
						                            ,@PeriodPK,A.PaymentDate,13,A.ClientSwitchingPK,'SWITCHING',isnull(A.Reference,''),'Switching Client: ' + B.ID + ' - ' + B.Name 
				                            From #ClientSwitchOut A  
				                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
				                            where A.TotalCashAmountTo > 0

				
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
				                            ,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPKTo,A.FundClientPK,0
				                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'D',A.TotalCashAmountTo
				                            ,A.TotalCashAmountTo,0
				                            ,1
				                            ,A.TotalCashAmountTo,0
				                            From #ClientSwitchOut A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
				                            LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status = 2
				                            LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
				                            Where A.TotalCashAmountTo > 0

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
			                            ,case when A.BitPendingSwitching = 1 then B.PendingSwitching else B.PendingSubscription end,case when A.BitPendingSwitching = 1 then isnull(C.CurrencyPK,1) else D.CurrencyPK end,A.FundPKTo,A.FundClientPK,0
			                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'C',A.TotalCashAmountTo
			                            ,0,TotalCashAmountTo
			                            ,1
			                            ,0,TotalCashAmountTo
			                            From #ClientSwitchOut A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKTo = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.PendingSwitching = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundJournalAccount D ON B.PendingSubscription = D.FundJournalAccountPK AND D.status IN (1,2)
			                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
			                            Where A.TotalCashAmountTo > 0

			                            END
		                            END
	                            END

	                            --JOURNAL IN
	                            BEGIN
		                            Select @FundJournalPKTemp = max(fundjournalPK) From #JournalHeader
		                            Select @FundJournalPKTemp = isnull(@FundJournalPKTemp,0)

		                            --FUND OUT
		                            BEGIN
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

			                            Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Client Switching Out, Fee Type IN'
					                            ,@PeriodPK,dbo.fworkingday(A.NAVDate ,1),13,A.ClientSwitchingPK,'SWITCHING',isnull(A.Reference,''),'Switching Client: ' + B.ID + ' - ' + B.Name 
			                            From #ClientSwitchIn A  
			                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
			                            where A.TotalCashAmountFrom > 0 
		
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
			                            ,B.Redemption,isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
			                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'D',case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end
			                            ,case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end,0
			                            ,1
			                            ,case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end,0
			                            From #ClientSwitchIn A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.Redemption = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
			                            Where A.TotalCashAmountFrom > 0

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
			                            ,B.PendingRedemption,isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
			                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'C',A.TotalCashAmountFrom
			                            ,0,A.TotalCashAmountFrom
			                            ,1
			                            ,0,A.TotalCashAmountFrom
			                            From #ClientSwitchIn A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.PendingRedemption = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
			                            Where A.TotalCashAmountFrom > 0

			                            --FUND OUT Payable ke BANK
			                            begin
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

				                            Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Client Switching Out Payment Date, Fee Type IN'
						                            ,@PeriodPK,A.PaymentDate,13,A.ClientSwitchingPK,'SWITCHING',isnull(A.Reference,''),'Switching Client: ' + B.ID + ' - ' + B.Name 
				                            From #ClientSwitchIn A  
				                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
				                            where A.TotalCashAmountFrom > 0

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
				                            ,B.PendingRedemption,isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
				                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'D',case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end
				                            ,case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end,0
				                            ,1
				                            ,case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end,0
				                            From #ClientSwitchIn A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
				                            LEFT JOIN FundJournalAccount C ON B.PendingRedemption = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
				                            Where A.TotalCashAmountFrom > 0

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
				                            ,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
				                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'C',case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end
				                            ,0,case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end
				                            ,1
				                            ,0,case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end
				                            From #ClientSwitchIn A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
				                            LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status = 2
				                            LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
				                            Where A.TotalCashAmountFrom > 0
			                            END
		                            END

		                            --FUND IN
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

			                            Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Client Switching in B, Fee Type IN'
					                            ,@PeriodPK,dbo.fworkingday(A.NAVDate ,1),13,A.ClientSwitchingPK,'SWITCHING',isnull(A.Reference,''),'Switching Client: ' + B.ID + ' - ' + B.Name 
			                            From #ClientSwitchIn A  
			                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
			                            where A.TotalCashAmountTo > 0 
		
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
			                            ,case when A.BitPendingSwitching = 1 then B.PendingSwitching else B.PendingSubscription end,case when A.BitPendingSwitching = 1 then isnull(C.CurrencyPK,1) else D.CurrencyPK end,A.FundPKTo,A.FundClientPK,0
			                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'D',case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end
			                            ,case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end,0
			                            ,1
			                            ,case when A.SwitchInDueDate = 1 then A.TotalCashAmountFrom else A.CashAmount end,0
			                            From #ClientSwitchIn A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKTo = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.PendingSwitching = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundJournalAccount D ON B.PendingSubscription = D.FundJournalAccountPK AND D.status IN (1,2)
			                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
			                            Where A.TotalCashAmountTo > 0

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
			                            ,B.Subscription,isnull(C.CurrencyPK,1),A.FundPKTo,A.FundClientPK,0
			                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'C',case when A.SwitchInDueDate = 1 then A.TotalCashAmountTo else A.CashAmount end
			                            ,0,case when A.SwitchInDueDate = 1 then A.TotalCashAmountTo else A.CashAmount end
			                            ,1
			                            ,0,case when A.SwitchInDueDate = 1 then A.TotalCashAmountTo else A.CashAmount end
			                            From #ClientSwitchIn A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKTo = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.Subscription = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
			                            Where A.TotalCashAmountTo > 0

			                            --jika ada fee dan SwitchInDueDate = 1
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
			                            ,B.PayableSwitchingFee,isnull(C.CurrencyPK,1),A.FundPKFrom,A.FundClientPK,0
			                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'C',A.SwitchingFeeAmount
			                            ,0,A.SwitchingFeeAmount
			                            ,1
			                            ,0,A.SwitchingFeeAmount
			                            From #ClientSwitchIn A  
			                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
			                            LEFT JOIN FundJournalAccount C ON B.PayableSwitchingFee = C.FundJournalAccountPK AND C.status IN (1,2)
			                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
			                            Where A.SwitchingFeeAmount > 0 and A.SwitchInDueDate = 1

			                            --FUND IN PAYMENT
			                            begin
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

				                            Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting Client Switching in payment date B, Fee Type IN'
						                            ,@PeriodPK,A.PaymentDate,13,A.ClientSwitchingPK,'SWITCHING',isnull(A.Reference,''),'Switching Client: ' + B.ID + ' - ' + B.Name 
				                            From #ClientSwitchIn A  
				                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
				                            where A.TotalCashAmountTo > 0

				
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
				                            ,isnull(D.FundJournalAccountPK,@DefaultBankAccountPK),isnull(C.CurrencyPK,1),A.FundPKTo,A.FundClientPK,0
				                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'D',A.CashAmount
				                            ,A.CashAmount,0
				                            ,1
				                            ,A.CashAmount,0
				                            From #ClientSwitchIn A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
				                            LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status = 2
				                            LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
				                            Where A.CashAmount > 0

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
				                            ,case when A.BitPendingSwitching = 1 then B.PendingSwitching else B.PendingSubscription end,case when A.BitPendingSwitching = 1 then isnull(C.CurrencyPK,1) else D.CurrencyPK end,A.FundPKTo,A.FundClientPK,0
				                            ,'Switching Client: ' + E.ID + ' - ' + E.Name,'C',A.CashAmount
				                            ,0,CashAmount
				                            ,1
				                            ,0,CashAmount
				                            From #ClientSwitchIn A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKTo = B.FundPK AND B.status = 2
				                            LEFT JOIN FundJournalAccount C ON B.PendingSwitching = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundJournalAccount D ON B.PendingSubscription = D.FundJournalAccountPK AND D.status IN (1,2)
				                            LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
				                            Where A.CashAmount > 0

				                            --jika ada fee dan SwitchInDueDate <> 1
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
				                            ,B.PayableSwitchingFee,isnull(C.CurrencyPK,1),A.FundPKTo,A.FundClientPK,0
				                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'C',A.SwitchingFeeAmount
				                            ,0,A.SwitchingFeeAmount
				                            ,1
				                            ,0,A.SwitchingFeeAmount
				                            From #ClientSwitchIn A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
				                            LEFT JOIN FundJournalAccount C ON B.PayableSwitchingFee = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
				                            Where A.SwitchingFeeAmount > 0 and A.SwitchInDueDate <> 1

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
				                            Select A.FundJournalPK + @FundJournalPKTemp,4,1,2
				                            ,B.Subscription,isnull(C.CurrencyPK,1),A.FundPKTo,A.FundClientPK,0
				                            ,'Switching Client: ' + D.ID + ' - ' + D.Name,'D',A.SwitchingFeeAmount
				                            ,A.SwitchingFeeAmount,0
				                            ,1
				                            ,A.SwitchingFeeAmount,0
				                            From #ClientSwitchIn A  
				                            LEFT JOIN dbo.FundAccountingSetup B ON A.FundPKFrom = B.FundPK AND B.status = 2
				                            LEFT JOIN FundJournalAccount C ON B.Subscription = C.FundJournalAccountPK AND C.status IN (1,2)
				                            LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
				                            Where A.SwitchingFeeAmount > 0 and A.SwitchInDueDate <> 1

			                            END
		                            END
	                            END

                            END

                            --LOGIC FUND CLIENT POSITION
	                        while @counterDate <= (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails Where status = 2 and Notes = 'Unit')
	                        begin
							
								set @yesterday = dbo.FWorkingDay(@counterDate,-1)

								--FIRST SUB (FEE IN FUND TO)
								INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
								select distinct @counterDate,A.FundClientPK,A.FundPKTo,0,0  from #ClientSwitchIn A
								left join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKTo and B.Date = @counterDate
								where A.NAVDate between @datefrom and @counterDate and B.FundPK is null 

								--FIRST SUB (FEE OUT FUND TO)
								INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
								select distinct @counterDate,A.FundClientPK,A.FundPKTo,0,0  from #ClientSwitchOut A
								left join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKTo and B.Date = @counterDate
								where A.NAVDate between @datefrom and @counterDate and B.FundPK is null

								-- FEE IN FUND TO (SUB)
								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPKTo,TotalCashAmountTo,TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchIn
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
									select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSwitchIn A
									inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKTo
									where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchIn
										where NAVDate = @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchIn
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end

								-- FEE OUT FUND TO (SUB)
								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPKTo,TotalCashAmountTo,TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchOut
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
										select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSwitchOut A
										inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKTo
										where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchOut
										where NAVDate = @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchOut
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end

								-- FEE IN FUND FROM (RED)
								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPKFrom,TotalCashAmountTo,TotalUnitAmountTo From (
										select FundClientPk,FundPKFrom,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchIn
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
									select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSwitchIn A
									inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKFrom
									where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount - A.TotalUnitAmountFrom From (
										select FundClientPk,FundPKFrom,sum(TotalUnitAmountFrom) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchIn
										where NAVDate = @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									UPDATE B Set B.UnitAmount = B.UnitAmount - A.TotalUnitAmountFrom From (
										select FundClientPk,FundPKFrom,sum(TotalUnitAmountFrom) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchIn
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end

								-- FEE OUT FUND FROM (RED)
								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPKFrom,TotalUnitAmountFrom,TotalCashAmountFrom From (
										select FundClientPk,FundPKFrom,sum(UnitAmount) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchOut
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
									select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSwitchOut A
									inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKFrom
									where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount - A.TotalUnitAmountFrom From (
										select FundClientPk,FundPKFrom,sum(UnitAmount) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchOut
										where NAVDate = @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									UPDATE B Set B.UnitAmount = B.UnitAmount - A.TotalUnitAmountFrom From (
										select FundClientPk,FundPKFrom,sum(UnitAmount) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchOut
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end

								
		                        set @counterDate = dbo.fworkingday(@counterDate,1)
	                        end

                            --UPDATE STATUS CLIENT SWITCHING
                            BEGIN
	                            update A set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
	                            FROM ClientSwitching A
	                            INNER JOIN #ClientSwitchIn B ON A.ClientSwitchingPK = B.ClientSwitchingPK and A.FundPKFrom = B.FundPKFrom and A.FundPKTo = B.FundPKTo and A.FundClientPK = B.FundClientPK
	                            where Status = 2 and Posted  = 0 and Revised = 0 

	                            update A set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
	                            FROM ClientSwitching A
	                            INNER JOIN #ClientSwitchOut B ON A.ClientSwitchingPK = B.ClientSwitchingPK and A.FundPKFrom = B.FundPKFrom and A.FundPKTo = B.FundPKTo and A.FundClientPK = B.FundClientPK
	                            where Status = 2 and Posted  = 0 and Revised = 0 
                            END

                            --insert into journal
	                        declare @MaxFundJournalPK int

	                        select @MaxFundJournalPK = isnull(max(FundJournalPK),0) from FundJournal


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
                            ";
                        cmd.CommandTimeout = 0;
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

        public void ClientSwitching_PostingUnitBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = " And ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = " And ClientSwitchingPK in (0) ";
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
                            _parambitManageUR = " and  A.status = 2   and A.Posted = 0 and A.ClientSwitchingPK in (select PK from ZManage_UR where Selected = 1 and Type = 3 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = " and A.Status = 2 and Posted  = 0 and Revised  = 0  and ValueDate between @datefrom and @dateto " + paramClientSwitchingSelected;
                        }


                        cmd.CommandText =
                           @"
                            --declare @datefrom date
                            --declare @dateto date
                            --declare @PostedBy nvarchar(20)
                            --declare @PostedTime datetime

                            --set @datefrom = '2020-05-28'
                            --set @dateto = @datefrom
                            ----set @dateto = '2020-05-26'
                            --set @PostedBy = 'admin'
                            --set @PostedTime = getdate()

                            --drop table #ClientSwitchIn
                            --drop table #ClientSwitchOut



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

	                            CREATE TABLE #ClientSwitchIn
	                            (
		                            FundJournalPK int,
		                            NAVDate datetime,
		                            PaymentDate datetime,
		                            Reference nvarchar(100),
		                            ClientSwitchingPK int,
		                            FundPKFrom int,
		                            FundPKTo int,
		                            FundClientPK int,
		                            FundCashRefPK int,
		                            TransactionPK nvarchar(200),
		                            Type int,
		                            TotalUnitAmountFrom numeric(19,6),
		                            TotalUnitAmountTo numeric(19,6),
		                            TotalCashAmountFrom numeric(19,4),
		                            TotalCashAmountTo numeric(19,4),
		                            UnitAmount numeric(19,4),
		                            CashAmount numeric(19,4),
		                            SwitchingFeeAmount numeric(19,4),
		                            BitPendingSwitching int,
		                            SwitchInDueDate int
	                            )

	                            CREATE CLUSTERED INDEX indx_ClientSwitchIn ON #ClientSwitchIn (FundJournalPK,FundPKFrom,FundPKTo,FundClientPK);

	                            CREATE TABLE #ClientSwitchOut
	                            (
		                            FundJournalPK int,
		                            NAVDate datetime,
		                            PaymentDate datetime,
		                            Reference nvarchar(100),
		                            ClientSwitchingPK int,
		                            FundPKFrom int,
		                            FundPKTo int,
		                            FundClientPK int,
		                            FundCashRefPK int,
		                            TransactionPK nvarchar(200),
		                            Type int,
		                            TotalUnitAmountFrom numeric(19,6),
		                            TotalUnitAmountTo numeric(19,6),
		                            TotalCashAmountFrom numeric(19,4),
		                            TotalCashAmountTo numeric(19,4),
		                            UnitAmount numeric(19,4),
		                            CashAmount numeric(19,4),
		                            SwitchingFeeAmount numeric(19,4),
		                            BitPendingSwitching int
	                            )

	                            CREATE CLUSTERED INDEX indx_ClientSwitchOut ON #ClientSwitchOut (FundJournalPK,FundPKFrom,FundPKTo,FundClientPK);

                            END

                            insert into #ClientSwitchOut
	                            (
		                            FundJournalPK,
		                            NAVDate,
		                            PaymentDate,
		                            Reference,
		                            ClientSwitchingPK,
		                            FundPKFrom,
		                            FundPKTo,
		                            FundClientPK,
		                            FundCashRefPK,
		                            TransactionPK,
		                            Type,
		                            TotalUnitAmountFrom,
		                            TotalUnitAmountTo,
		                            TotalCashAmountFrom,
		                            TotalCashAmountTo,
		                            UnitAmount,
		                            CashAmount,
		                            SwitchingFeeAmount,
		                            BitPendingSwitching
	                            )
	                            Select ROW_NUMBER() OVER	(ORDER BY A.ClientSwitchingPK ASC) Number ,
			                            A.NAVDate,
			                            A.PaymentDate,
			                            '',
			                            A.ClientSwitchingPK ,
			                            A.FundPKFrom ,
			                            A.FundPKTo ,
			                            A.FundClientPK ,
			                            A.CashRefPKFrom,
			                            A.TransactionPK,
			                            A.Type,
			                            isnull(A.TotalUnitAmountFundFrom,0),
			                            isnull(A.TotalUnitAmountFundTo,0),
			                            isnull(A.TotalCashAmountFundFrom,0),
			                            isnull(A.TotalCashAmountFundTo,0),
			                            isnull(A.UnitAmount,0),
			                            isnull(A.CashAmount,0),
			                            isnull(A.SwitchingFeeAmount,0),
			                            isnull(C.BitPendingSwitchIn,0)
	                            From ClientSwitching A 
	                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	                            LEFT JOIN FundFee C on A.FundPKFrom = C.FundPK and C.status in (1,2) and date = 
	                            (
		                            select max(date) from FundFee where fundpk = A.FundPKFrom and status = 2
	                            )
	                            Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto
	                            and A.FeeType = 'OUT' AND isnull(A.NAVFundFrom,0) > 0 and A.UnitAmount > 0 and A.TotalUnitAmountFundTo > 0
	                            " + _parambitManageUR + @"
	                            order by Number asc

	                            insert into #ClientSwitchIn
	                            (
		                            FundJournalPK,
		                            NAVDate,
		                            PaymentDate,
		                            Reference,
		                            ClientSwitchingPK,
		                            FundPKFrom,
		                            FundPKTo,
		                            FundClientPK,
		                            FundCashRefPK,
		                            TransactionPK,
		                            Type,
		                            TotalUnitAmountFrom,
		                            TotalUnitAmountTo,
		                            TotalCashAmountFrom,
		                            TotalCashAmountTo,
		                            UnitAmount,
		                            CashAmount,
		                            SwitchingFeeAmount,
		                            BitPendingSwitching,
		                            SwitchInDueDate
	                            )
	                            Select ROW_NUMBER() OVER	(ORDER BY A.ClientSwitchingPK ASC) Number ,
			                            A.NAVDate,
			                            A.PaymentDate,
			                            '',
			                            A.ClientSwitchingPK ,
			                            A.FundPKFrom ,
			                            A.FundPKTo ,
			                            A.FundClientPK ,
			                            A.CashRefPKFrom,
			                            A.TransactionPK,
			                            A.Type,
			                            isnull(A.TotalUnitAmountFundFrom,0),
			                            isnull(A.TotalUnitAmountFundTo,0),
			                            isnull(A.TotalCashAmountFundFrom,0),
			                            isnull(A.TotalCashAmountFundTo,0),
			                            isnull(A.UnitAmount,0),
			                            isnull(A.CashAmount,0),
			                            isnull(A.SwitchingFeeAmount,0),
			                            isnull(C.BitPendingSwitchIn,0),
			                            isnull(D.SwitchInDueDate,0)
	                            From ClientSwitching A 
	                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	                            LEFT JOIN FundFee C on A.FundPKFrom = C.FundPK and C.status in (1,2) and date = 
	                            (
		                            select max(date) from FundFee where fundpk = A.FundPKFrom and status = 2
	                            )
	                            LEFT JOIN FUND D on A.FundPKTo = D.FundPK and D.Status in (1,2) 
	                            Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto
	                            and A.FeeType = 'IN' AND isnull(A.NAVFundTo,0) > 0 and A.UnitAmount > 0 and A.TotalUnitAmountFundTo > 0
	                            " + _parambitManageUR + @"
	                            order by Number asc


							--LOGIC FUND CLIENT POSITION
	                        while @counterDate <= (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails Where status = 2 and Notes = 'Unit')
	                        begin
							
								set @yesterday = dbo.FWorkingDay(@counterDate,-1)

								--FIRST SUB (FEE IN FUND TO)
								INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
								select distinct @counterDate,A.FundClientPK,A.FundPKTo,0,0  from #ClientSwitchIn A
								left join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKTo and B.Date = @counterDate
								where A.NAVDate between @datefrom and @counterDate and B.FundPK is null 

								--FIRST SUB (FEE OUT FUND TO)
								INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
								select distinct @counterDate,A.FundClientPK,A.FundPKTo,0,0  from #ClientSwitchOut A
								left join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKTo and B.Date = @counterDate
								where A.NAVDate between @datefrom and @counterDate and B.FundPK is null

								-- FEE IN FUND TO (SUB)
								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPKTo,TotalCashAmountTo,TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchIn
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
									select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSwitchIn A
									inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKTo
									where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchIn
										where NAVDate = @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchIn
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end

								-- FEE OUT FUND TO (SUB)
								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPKTo,TotalCashAmountTo,TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchOut
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
										select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSwitchOut A
										inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKTo
										where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchOut
										where NAVDate = @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPKTo,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchOut
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKTo
									)A INNER JOIN FundClientPosition B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end

								-- FEE IN FUND FROM (RED)
								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPKFrom,TotalCashAmountTo,TotalUnitAmountTo From (
										select FundClientPk,FundPKFrom,sum(TotalUnitAmountTo) TotalUnitAmountTo,sum(TotalCashAmountTo) TotalCashAmountTo from #ClientSwitchIn
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
									select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSwitchIn A
									inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKFrom
									where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount - A.TotalUnitAmountFrom From (
										select FundClientPk,FundPKFrom,sum(TotalUnitAmountFrom) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchIn
										where NAVDate = @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									UPDATE B Set B.UnitAmount = B.UnitAmount - A.TotalUnitAmountFrom From (
										select FundClientPk,FundPKFrom,sum(TotalUnitAmountFrom) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchIn
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end

								-- FEE OUT FUND FROM (RED)
								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPKFrom,TotalUnitAmountFrom,TotalCashAmountFrom From (
										select FundClientPk,FundPKFrom,sum(UnitAmount) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchOut
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
									select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSwitchOut A
									inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPKFrom
									where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount - A.TotalUnitAmountFrom From (
										select FundClientPk,FundPKFrom,sum(UnitAmount) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchOut
										where NAVDate = @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									UPDATE B Set B.UnitAmount = B.UnitAmount - A.TotalUnitAmountFrom From (
										select FundClientPk,FundPKFrom,sum(UnitAmount) TotalUnitAmountFrom,sum(TotalCashAmountFrom) TotalCashAmountFrom from #ClientSwitchOut
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPKFrom
									)A INNER JOIN FundClientPosition B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end

								
		                        set @counterDate = dbo.fworkingday(@counterDate,1)
	                        end

                            --UPDATE STATUS CLIENT SWITCHING
                            BEGIN
	                            update A set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
	                            FROM ClientSwitching A
	                            INNER JOIN #ClientSwitchIn B ON A.ClientSwitchingPK = B.ClientSwitchingPK and A.FundPKFrom = B.FundPKFrom and A.FundPKTo = B.FundPKTo and A.FundClientPK = B.FundClientPK
	                            where Status = 2 and Posted  = 0 and Revised = 0 

	                            update A set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
	                            FROM ClientSwitching A
	                            INNER JOIN #ClientSwitchOut B ON A.ClientSwitchingPK = B.ClientSwitchingPK and A.FundPKFrom = B.FundPKFrom and A.FundPKTo = B.FundPKTo and A.FundClientPK = B.FundClientPK
	                            where Status = 2 and Posted  = 0 and Revised = 0 
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

        public List<ValidateClientSwitching> ValidateClientSwitching(ClientSwitching _clientSwitching)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                List<ValidateClientSwitching> L_ValidateClientSubscription = new List<ValidateClientSwitching>();
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            
                        


--declare @LastUpdate datetime
--declare @ClientCode nvarchar(100)
--declare @FundPK int
--declare @FundPKTo int
--declare @UnitAmount numeric(22,8)
--declare @CashAmount numeric(22,8)
--declare @ValueDate date
--declare @BitSwitchingAll int
--declare @FundClientPK int

--set @LastUpdate = getdate()
--set @ClientCode = '10'
--set @FundPK = 1
--set @FundPKTo = 58
--set @UnitAmount = 0
--set @CashAmount = 4490000
--set @ValueDate = '2020-10-14'
--set @BitSwitchingAll = 0
--set @FundClientPK = 8

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
declare @CloseNAV numeric(22,8)
declare @MinBalSwiAmount numeric(22,8)
Declare @paramUnitAmount numeric(32,8)

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

--2. check min Switch To amount
begin
	select @CloseNAV = Nav from CloseNAV where status = 2 and FundPK = @FundPKTo and Date = (
		select max(date) from CloseNAV where status = 2 and FundPK = @FundPKTo and date <= @ValueDate
	)

	if @CashAmount > 0
		set @paramCashAmount = @CashAmount
	else
		set @paramCashAmount = @UnitAmount * @CloseNAV

	select @MinBalSwiAmount = isnull(MinBalSwitchToAmt,0) from Fund where status = 2 and FundPK = @FundPKTo

	if @paramCashAmount < @MinBalSwiAmount and @MinBalSwiAmount <> 0
	begin
		insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
		select 'Minimum switching to amount < cash amount ,minimum switching to amount : ' + format(@MinBalSwiAmount,'N') + ', cash amount : ' + format(@paramCashAmount,'N') ,'',0,0
	end

end

--3. check min Switch From amount
begin
	select @CloseNAV = Nav from CloseNAV where status = 2 and FundPK = @FundPK and Date = (
		select max(date) from CloseNAV where status = 2 and FundPK = @FundPK and date <= @ValueDate
	)

	if @CashAmount > 0
		set @paramCashAmount = @CashAmount
	else
		set @paramCashAmount = @UnitAmount * @CloseNAV

	select @MinBalSwiAmount = isnull(MinBalSwitchAmt,0) from Fund where status = 2 and FundPK = @FundPK

	if @paramCashAmount < @MinBalSwiAmount and @MinBalSwiAmount <> 0
	begin
		insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
		select 'Minimum switching from amount < cash amount ,minimum switching from amount : ' + format(@MinBalSwiAmount,'N') + ', cash amount : ' + format(@paramCashAmount,'N') ,'',0,0
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

	if @UnitAmount = 0 
		set @paramUnitAmount = case when @CloseNAV = 0 then 0 else @CashAmount / @CloseNAV end
	else
		set @paramUnitAmount = @UnitAmount

    set @RemainingBalance = isnull(@RemainingBalance,0)

	set @paramUnitAmount = @RemainingBalance - @paramUnitAmount

	if @paramUnitAmount < @RemainingBalanceUnit and @RemainingBalanceUnit <> 0 and @BitSwitchingAll = 0
	begin
		insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
		select 'This transaction less than remaining balance unit, minimum unit : ' + format(@RemainingBalanceUnit,'N') + ', remaining unit amount : ' + format(@paramUnitAmount,'N'),'',1,1
	end

end

select ROW_NUMBER() over(order by Reason) No,* from @TableReason

                        ";
                        cmd.Parameters.AddWithValue("@FundPK", _clientSwitching.FundPKFrom);
                        cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                        cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                        cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ValidateClientSwitching M_ValidateClientSubscription = new ValidateClientSwitching();
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






    }
}