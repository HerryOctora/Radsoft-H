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
    public class HoldingPeriodReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();
        private ClientRedemptionHoldingPeriod setClientRedemptionHoldingPeriod(SqlDataReader dr)
        {
            ClientRedemptionHoldingPeriod M_ClientRedemptionHoldingPeriod = new ClientRedemptionHoldingPeriod();

            M_ClientRedemptionHoldingPeriod.FundID = Convert.ToString(dr["FundID"]);
            M_ClientRedemptionHoldingPeriod.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_ClientRedemptionHoldingPeriod.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
            M_ClientRedemptionHoldingPeriod.TotalSubs = Convert.ToDecimal(dr["TotalSubs"]);
            M_ClientRedemptionHoldingPeriod.TakenOut = Convert.ToDecimal(dr["TakenOut"]);
            M_ClientRedemptionHoldingPeriod.Remaining = Convert.ToDecimal(dr["Remaining"]);
            M_ClientRedemptionHoldingPeriod.RedempUnit = Convert.ToDecimal(dr["RedemptUnit"]);
            M_ClientRedemptionHoldingPeriod.RedempFeePercent = Convert.ToDecimal(dr["RedempFeePercent"]);
            M_ClientRedemptionHoldingPeriod.TotalFeeAmount = Convert.ToDecimal(dr["TotalFeeAmount"]);
            M_ClientRedemptionHoldingPeriod.HoldingPeriod = Convert.ToInt32(dr["HoldingPeriod"]);
            M_ClientRedemptionHoldingPeriod.UnitDecimalPlaces = Convert.ToInt32(dr["UnitDecimalPlaces"]);


            return M_ClientRedemptionHoldingPeriod;
        }
        public List<ClientRedemptionHoldingPeriod> Init_DataHoldingPeriod(string _fundPK, string _fundClientPK, DateTime _date, decimal _Unit)
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
                            _paramFund = " And A.FundPK in ( " + _fundPK + " ) ";
                        }
                        else
                        {
                            _paramFund = " And A.FundPK in (0)";
                        }

                        if (!_host.findString(_fundClientPK.ToLower(), "0", ",") && !string.IsNullOrEmpty(_fundClientPK))
                        {
                            _paramFundClient = "And A.FundClientPK in ( " + _fundClientPK + " ) ";
                        }
                        else
                        {
                            _paramFundClient = "And A.FundClientPK in (0)";
                        }

                        cmd.CommandText = @"
                        declare @CutOffDate date
                        set @CutOffDate = '2018-12-28'

                        ;WITH cte AS (
                        SELECT ValueDate,FundPK,FundClientPK,TotalUnitAmount, SUM(TotalUnitAmount) OVER ( PARTITION BY FundPk,FundClientPk ORDER BY ValueDate ASC) as CumQty from (
                        select Date ValueDate,FundPK,FundClientPK,UnitAmount TotalUnitAmount from FundClientPosition 
                        where date = @CutOffDate and UnitAmount > 0
                        union all
                        SELECT ValueDate,FundPK,FundClientPK,TotalUnitAmount
                        FROM ClientSubscription WHERE status in (1,2) and TotalUnitAmount>0 and ValueDate > @CutOffDate AND isnull(notes,'') <> 'CUTOFF'
                        )A
                        group by FundPK,FundClientPK,ValueDate,TotalUnitAmount
                        )

 
                         SELECT  D.ID FundID,E.Name FundClientName,ValueDate,TotalUnitAmount TotalSubs,
                            CASE
                                WHEN CumQty<isnull(TakenQty,0) THEN TotalUnitAmount
                                    ELSE case when isnull(TakenQty,0) -(CumQty-TotalUnitAmount) < 0 then 0 else isnull(TakenQty,0) -(CumQty-TotalUnitAmount) end
                            END AS TakenOut,
	                        CASE
                                WHEN CumQty<isnull(TakenQty,0)  THEN 0
                                    ELSE case when isnull(TakenQty,0) -(CumQty-TotalUnitAmount) < 0 then TotalUnitAmount else (CumQty) - isnull(TakenQty,0) end
                            END AS Remaining,
	                        CASE
                                WHEN CumQty<(isnull(TakenQty,0) + @Amount) THEN TotalUnitAmount - case when isnull(TakenQty,0) -(CumQty-TotalUnitAmount) > 0 then isnull(TakenQty,0) -(CumQty-TotalUnitAmount) else 0 end
                                    ELSE case when (isnull(TakenQty,0) + @Amount) -(CumQty-TotalUnitAmount) < 0 then 0 else ((isnull(TakenQty,0) + @Amount) -(CumQty-TotalUnitAmount))  end
                            END AS RedemptUnit,
	                        case when DATEDIFF(month,ValueDate,@Valuedate) <= C.HoldingPeriod then isnull(C.RedempFeePercent,0) else 0 end RedempFeePercent,
	                        CASE
                                WHEN CumQty<(isnull(TakenQty,0) + @Amount) THEN (TotalUnitAmount - case when isnull(TakenQty,0) -(CumQty-TotalUnitAmount) > 0 then (isnull(TakenQty,0) -(CumQty-TotalUnitAmount) )  else 0 end ) * case when DATEDIFF(month,ValueDate,@Valuedate) <= C.HoldingPeriod then C.RedempFeePercent else 0 end / 100
                                    ELSE case when (isnull(TakenQty,0) + @Amount) -(CumQty-TotalUnitAmount) < 0 then 0 else ((isnull(TakenQty,0) + @Amount) -(CumQty-TotalUnitAmount)) * case when DATEDIFF(month,ValueDate,@Valuedate) <= C.HoldingPeriod then isnull(C.RedempFeePercent,0) else 0 end / 100  end
                            END AS TotalFeeAmount,
	                        DATEDIFF(month,ValueDate,@Valuedate) HoldingPeriod,D.UnitDecimalPlaces
                         FROM cte A
                         left join 
                         (
	                        select FundClientPK,FundPK,sum(UnitAmount) TakenQty from ClientRedemption where status in (1,2) and ValueDate > @CutOffDate
	                        group by FundClientPk,FundPK
                         ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
                         left join BackLoadSetup C on A.FundPK = C.FundPK and C.status in (1,2)
						 left join Fund D on A.FundPk = D.FundPk and D.Status in (1,2)
						 left join FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
                         where CASE
                                WHEN CumQty<isnull(TakenQty,0) THEN 0
                                    ELSE case when isnull(TakenQty,0) -(CumQty-TotalUnitAmount) < 0 then TotalUnitAmount else (CumQty) - isnull(TakenQty,0) end
                            END > 0  
	                        and 
	                        case WHEN CumQty<(isnull(TakenQty,0) + @Amount) THEN TotalUnitAmount - case when isnull(TakenQty,0) -(CumQty-TotalUnitAmount) > 0 then isnull(TakenQty,0) -(CumQty-TotalUnitAmount) else 0 end
                                    ELSE case when (isnull(TakenQty,0) + @Amount) -(CumQty-TotalUnitAmount) < 0 then 0 else ((isnull(TakenQty,0) + @Amount) -(CumQty-TotalUnitAmount))  end end > 0
	                        " + _paramFund + _paramFundClient + @"
							--and A.FundPk = @FundPk and A.FundClientPK = @FundClientPK

                         group by A.FundPK,A.FundClientPK,ValueDate,CumQty,TotalUnitAmount,B.TakenQty,C.HoldingPeriod,C.RedempFeePercent,D.ID,E.Name,D.UnitDecimalPlaces

                        ";

                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@Amount", _Unit);

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

        public string AddClientRedemption(List<ClientRedemptionHoldingPeriod> _ClientRedemptionHoldingPeriod, string _UserId)
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
                                                delete ClientRedemptionByHoldingPeriod

                                          ";
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();

                    }

                    foreach (var _obj in _ClientRedemptionHoldingPeriod)
                    {
                        ClientRedemptionHoldingPeriod _m = new ClientRedemptionHoldingPeriod();
                        _m.FundPK = _obj.FundPK;
                        _m.FundClientPK = _obj.FundClientPK;
                        _m.RedempDate = _obj.RedempDate;
                        _m.CashRefPK = _obj.CashRefPK;
                        _m.RedempUnit = _obj.RedempUnit;
                        _m.RedempFeePercent = _obj.RedempFeePercent;
                        _m.TotalFeeAmount = _obj.TotalFeeAmount;

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {


                            cmd.CommandText = @"
                                                insert into ClientRedemptionByHoldingPeriod(FundPK,FundClientPK,RedempDate,CashRefPK,RedempUnit,FeePercent,FeeAmount)
                                                select @FundPK,@FundClientPK,@RedempDate,@CashRefPK,@RedempUnit,@RedempFeePercent,@TotalFeeAmount

                                          ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@FundPK", _m.FundPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _m.FundClientPK);
                            cmd.Parameters.AddWithValue("@RedempDate", _m.RedempDate);
                            cmd.Parameters.AddWithValue("@CashRefPK", _m.CashRefPK);
                            cmd.Parameters.AddWithValue("@RedempUnit", _m.RedempUnit);
                            cmd.Parameters.AddWithValue("@RedempFeePercent", _m.RedempFeePercent);
                            cmd.Parameters.AddWithValue("@TotalFeeAmount", _m.TotalFeeAmount);
                            cmd.ExecuteNonQuery();

                        }
                    }

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" 
                                                declare @CloseNAV table
                                                (
	                                                NAVDate date,
	                                                FundPK int,
	                                                Nav numeric(22,4)
                                                )

                                                declare @MaxClientRedemption int
                                                select @MaxClientRedemption = max(ClientRedemptionPK) from ClientRedemption

                                                declare @MaxEDT datetime
                                                Select @MaxEDT = max(Valuedate) from EndDayTrails where status = 2 and ValueDate <= (select distinct redempdate from ClientRedemptionByHoldingPeriod)
												set @MaxEDT = dbo.FWorkingDay(@MaxEDT,1)

												DECLARE @Yesterday DATE
												set @Yesterday = dbo.FWorkingDay((select distinct redempdate from ClientRedemptionByHoldingPeriod),-1)

												DECLARE @ParamUnitRedemptionAll numeric(22,4)
												set @ParamUnitRedemptionAll = 10000

                                                insert into @CloseNAV
                                                select maxdate,A.fundpk,B.NAV from (
                                                select max(date) maxdate,FundPK from CloseNAV 
                                                where status = 2 and date <= (
	                                                select distinct redempdate from ClientRedemptionByHoldingPeriod)
                                                group by FundPK
                                                )A
                                                left join CloseNAV B on A.FundPK = B.FundPK and A.maxdate = B.Date and b.status = 2
					
                                                INSERT INTO [dbo].[ClientRedemption] 
                                                            ([ClientRedemptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[PaymentDate],
                                                            [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Type],[BitRedemptionAll],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],
                                                            [RedemptionFeePercent],[RedemptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[UnitPosition],[BankRecipientPK],[TransferType],[FeeType],[ReferenceSInvest],
			                                                [IsBOTransaction],[EntryUsersID],[EntryTime],[LastUpdate])

                                                select ROW_NUMBER() over (order by A.fundpk) + @MaxClientRedemption ClientRedemptionPK,1 HistoryPK,1 Status,A.redempdate NAVDate,A.redempdate ValueDate,dbo.FWorkingDay(A.redempdate,B.DefaultPaymentDate) PaymentDate,
                                                0 NAV,A.fundpk,A.fundclientpk,A.cashrefpk,D.CurrencyPK,1 Type,case when I.FundPK is null then 0 when (sum(A.redempunit)- isnull(J.UnitAmount,0) )* H.Nav <= @ParamUnitRedemptionAll then 1 else 0 end BitRedemptionAll,'GENERATE FROM HOLDING PERIOD' Description,0 CashAmount,sum(A.redempunit) UnitAmount,0 TotalCashAmount,sum(A.redempunit)-sum(A.FeeAmount) TotalunitAmount,
                                                A.feepercent RedemptionFeePercent,0 RedemptionFeeAmount,C.SellingAgentPK Agentpk,0 AgentFeePercent,0 AgentFeeAmount,isnull(E.Unit,0) UnitPosition,
                                                case when F.BankRecipientPK is not null then F.BankRecipientPK when G1.BankPK is not null then 1 when G2.BankPK is not null then 2 when G3.BankPk is not null then 3 else isnull(G4.NoBank,0) end BankRecipientPK, 
                                                CASE WHEN sum(A.redempunit) * H.Nav < 100000000 then 1 when sum(A.redempunit) * H.Nav >= 100000000 then 2 else 0 end TransferType,1 FeeType, '' ReferenceSInvest,
                                                1 IsBOTransaction,@UsersID EntryUsersID,@LastUpdate EntryTime,@LastUpdate LastUpdate
                                                from ClientRedemptionByHoldingPeriod A
                                                left join Fund B on A.FundPk = B.FundPK and B.status in (1,2)
                                                left join FundClient C on A.FundClientpk = C.FundClientPK and C.status in (1,2)
                                                left join FundCashRef D on A.CashRefPK = D.FundCashRefPK and D.status in (1,2)
                                                left join (
                                                    Select isnull(A.UnitAmount,0) - isnull(B.UnitAmount,0) - isnull(C.UnitAmount,0) Unit,A.FundPK,A.FundClientPositionPK
                                                    FROM FundClientPosition A
                                                    left join (
	                                                    SELECT A.FundClientPK,A.FundPK, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,A.FundPK) end,0)) UnitAmount FROM ClientRedemption  A
		                                                inner join ClientRedemptionByHoldingPeriod B on A.FundPk = B.FundPK and A.FundClientPK = B.FundClientPK
	                                                    WHERE status not in (3,4) and posted = 0 and Revised = 0 
	                                                    group by A.FundClientPK,A.FundPK
                                                    ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 

                                                    left join 
                                                    (
	                                                    SELECT A.FundClientPK,A.FundPKFrom, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,A.FundPKFrom) end,0)) UnitAmount FROM ClientSwitching  A
		                                                inner join ClientRedemptionByHoldingPeriod B on A.FundPKFrom = B.FundPK and A.FundClientPK = B.FundClientPK
	                                                    WHERE status not in (3,4) and posted = 0 and Revised = 0 
	                                                    group by A.FundClientPK,A.FundPKFrom
                                                    ) C on A.FundClientPK = C.FundClientPK and A.FundPK = C.FundPKFrom 
                                                    where Date = @MaxEDT 
                                                ) E on A.FundPK = E.FundPK and A.FundClientPK = E.FundPK
                                                left join fundclientbankDefault F on A.FundPk = F.FundPK and A.FundClientPK = F.FundClientPK and F.status = 2
                                                LEFT JOIN BANK G1 ON C.NamaBank1 = G1.BankPK AND G1.Status = 2 
                                                LEFT JOIN BANK G2 ON C.NamaBank2 = G2.BankPK AND G2.Status = 2
                                                LEFT JOIN BANK G3 ON C.NamaBank3 = G3.BankPK AND G3.Status = 2
                                                LEFT JOIN FundClientBankList G4 ON C.NamaBank1 = G1.BankPK AND G1.Status = 2
                                                LEFT JOIN @CloseNAV H ON A.FundPK = H.FundPK  
												LEFT JOIN (
													SELECT DISTINCT FundPK, FundClientPK FROM ClientSubscription WHERE ValueDate in ( @Yesterday, (select distinct redempdate from ClientRedemptionByHoldingPeriod))
												) I ON A.FundPK = I.FundPK and A.FundPK = I.FundClientPK
												LEFT JOIN FundClientPosition J on A.FundPK = J.FundPK and A.FundClientPK = J.FundClientPK and J.Date = @Yesterday

			                                                group by A.FundPK,A.FundClientPK,A.redempdate,A.cashrefpk,A.feepercent,B.DefaultPaymentDate,D.CurrencyPK,C.SellingAgentPK,E.Unit,F.BankRecipientPK,G1.BankPK,G2.BankPK,G3.BankPK,G4.NoBank,H.Nav,I.FundPK,J.UnitAmount

";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@UsersID", _UserId);
                        cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }


                    return "Insert into ClientRedemption Success";
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


    }

    
}
