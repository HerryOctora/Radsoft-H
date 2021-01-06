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
    public class ClientDashboardReps
    {
        Host _host = new Host();

        private ClientDashboard setHistoricalTransaction(SqlDataReader dr)
        {
            ClientDashboard M_ClientDashboard = new ClientDashboard();
            M_ClientDashboard.Status = dr["Status"].ToString();
            M_ClientDashboard.Date = dr["NAVDate"].ToString();
            M_ClientDashboard.TransactionType = dr["TrxType"].ToString();
            M_ClientDashboard.Fund = dr["FundID"].ToString();
            M_ClientDashboard.Amount = Convert.ToDecimal(dr["Amount"]);
            M_ClientDashboard.NAB = Convert.ToDecimal(dr["NAV"]);
            M_ClientDashboard.Unit = Convert.ToDecimal(dr["UnitAmount"]);
            M_ClientDashboard.Description = dr["Description"].ToString();
            return M_ClientDashboard;
        }

        public List<ClientDashboard> ClientDashboard_HistoricalTransactionByDate(string _dateFrom, string _dateTo, ClientDashboard _clientDashboard)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientDashboard> L_ClientDashboard = new List<ClientDashboard>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramName = "";
                        string _paramEmail = "";
                        string _paramPhoneNo = "";
                        string _paramKtpNo = "";

                        if (_clientDashboard.Name != null)
                        {
                            _paramName = " And C.Name like '%" + _clientDashboard.Name + "%' ";
                        }
                        else
                        {
                            _paramName = "";
                        }

                        if (_clientDashboard.Email != null)
                        {
                            _paramEmail = " And C.Email like '%" + _clientDashboard.Email + "%' ";
                        }
                        else
                        {
                            _paramEmail = "";
                        }

                        if (_clientDashboard.PhoneNo != null)
                        {
                            _paramPhoneNo = " And C.TeleponSelular like '%" + _clientDashboard.PhoneNo + "%' ";
                        }
                        else
                        {
                            _paramPhoneNo = "";
                        }

                        if (_clientDashboard.KTPNo != null)
                        {
                            _paramKtpNo = " And C.NoIdentitasInd1 like '%" + _clientDashboard.KTPNo + "%' ";
                        }
                        else
                        {
                            _paramKtpNo = "";
                        }

                        cmd.CommandText = @"
                            Select Case when A.status = 1 then 'PENDING' when A.status = 2 and posted = 0 then 'APPROVED' when A.status = 2 and A.posted = 1 then 'POSTED'
	                        when A.status = 2 and A.posted = 1 and A.Revised = 1 then 'REVISED' when A.status = 3 then 'HISTORY' end [Status],
	                        NAVDate,'Subscription' TrxType,
	                        B.ID FundID,TotalCashAmount Amount,A.NAV,A.TotalUnitAmount UnitAmount,A.Description
                            from ClientSubscription A
	                        left join fund B on A.FundPK = B.FundPK and B.status in (1,2)
	                        left join fundclient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
	                        where A.NAVDate Between @DateFrom and @DateTo  
                            " + _paramName + _paramEmail + _paramPhoneNo + _paramKtpNo + @" 
	                        UNION ALL

	                        Select Case when A.status = 1 then 'PENDING' when A.status = 2 and posted = 0 then 'APPROVED' when A.status = 2 and A.posted = 1 then 'POSTED'
	                        when A.status = 2 and A.posted = 1 and A.Revised = 1 then 'REVISED' when A.status = 3 then 'HISTORY' end [Status],
	                        NAVDate,'Redemption' TrxType,
	                        B.ID FundID,CashAmount Amount,A.NAV,A.UnitAmount UnitAmount,A.Description
                            from ClientRedemption A
	                        left join fund B on A.FundPK = B.FundPK and B.status in (1,2)
	                        left join fundclient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
	                        where A.NAVDate Between @DateFrom and @DateTo  
                            " + _paramName + _paramEmail + _paramPhoneNo + _paramKtpNo + @" 

	                        UNION ALL

	                        Select Case when A.status = 1 then 'PENDING' when A.status = 2 and posted = 0 then 'APPROVED' when A.status = 2 and A.posted = 1 then 'POSTED'
	                        when A.status = 2 and A.posted = 1 and A.Revised = 1 then 'REVISED' when A.status = 3 then 'HISTORY' end [Status],
	                        NAVDate,'Switch In' TrxType,
	                        B.ID FundID,TotalCashAmountFundTo Amount,A.NAVFundTo NAV,A.TotalUnitAmountFundTo UnitAmount,A.Description
                            from ClientSwitching A
	                        left join fund B on A.FundPKTo = B.FundPK and B.status in (1,2)
	                        left join fundclient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
	                        where A.NAVDate Between @DateFrom and @DateTo 
                            " + _paramName + _paramEmail + _paramPhoneNo + _paramKtpNo + @"  

	
	                        UNION ALL

	                        Select Case when A.status = 1 then 'PENDING' when A.status = 2 and posted = 0 then 'APPROVED' when A.status = 2 and A.posted = 1 then 'POSTED'
	                        when A.status = 2 and A.posted = 1 and A.Revised = 1 then 'REVISED' when A.status = 3 then 'HISTORY' end [Status],
	                        NAVDate,'Switch Out' TrxType,
	                        B.ID FundID,CashAmount Amount,A.NAVFundFrom NAV,A.UnitAmount UnitAmount,A.Description
                            from ClientSwitching A
	                        left join fund B on A.FundPKFrom = B.FundPK and B.status in (1,2)
	                        left join fundclient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
	                        where A.NAVDate Between @DateFrom and @DateTo  
                            " + _paramName + _paramEmail + _paramPhoneNo + _paramKtpNo + @" 

	                        order by A.NAVDate asc

                                                     
                            ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ClientDashboard.Add(setHistoricalTransaction(dr));
                                }
                            }
                            return L_ClientDashboard;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }




        private ClientDashboard setSummaryTransaction(SqlDataReader dr)
        {
            ClientDashboard M_ClientDashboard = new ClientDashboard();
            M_ClientDashboard.Fund = dr["FundID"].ToString();
            M_ClientDashboard.Unit = Convert.ToDecimal(dr["UnitAmount"]);
            M_ClientDashboard.Avg = Convert.ToDecimal(dr["AvgPrice"]);
            M_ClientDashboard.NAB = Convert.ToDecimal(dr["NAV"]);
            M_ClientDashboard.TotalBuy = Convert.ToDecimal(dr["TotalPembelian"]);
            M_ClientDashboard.ProfitLoss = Convert.ToDecimal(dr["LabaRugi"]);
            M_ClientDashboard.ProfitLossPercent = Convert.ToDecimal(dr["LabaRugiPercent"]);
            return M_ClientDashboard;
        }

        public List<ClientDashboard> ClientDashboard_SummaryTransactionByDate(string _dateFrom, string _dateTo, ClientDashboard _clientDashboard)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientDashboard> L_ClientDashboard = new List<ClientDashboard>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramName = "";
                        string _paramEmail = "";
                        string _paramPhoneNo = "";
                        string _paramKtpNo = "";

                        if (_clientDashboard.Name != null)
                        {
                            _paramName = " And D.Name like '%" + _clientDashboard.Name + "%' ";
                        }
                        else
                        {
                            _paramName = "";
                        }

                        if (_clientDashboard.Email != null)
                        {
                            _paramEmail = " or D.Email like '%" + _clientDashboard.Email + "%' ";
                        }
                        else
                        {
                            _paramEmail = "";
                        }

                        if (_clientDashboard.PhoneNo != null)
                        {
                            _paramPhoneNo = " or D.TeleponSelular like '%" + _clientDashboard.PhoneNo + "%' ";
                        }
                        else
                        {
                            _paramPhoneNo = "";
                        }

                        if (_clientDashboard.KTPNo != null)
                        {
                            _paramKtpNo = " or D.NoIdentitasInd1 like '%" + _clientDashboard.KTPNo + "%' ";
                        }
                        else
                        {
                            _paramKtpNo = "";
                        }

                        cmd.CommandText = @"
                        Select B.ID FundID,A.UnitAmount,dbo.FGetAVGForFundClientPosition(@DateTo,A.FundClientPK,A.FundPK) AvgPrice,
                        isnull(A.UnitAmount * dbo.FGetAVGForFundClientPosition(@DateTo,A.FundClientPK,A.FundPK),0) TotalPembelian, isnull(C.NAV,0) NAV,
                        isnull((C.NAV - dbo.FGetAVGForFundClientPosition(@DateTo,A.FundClientPK,A.FundPK) ) * A.UnitAmount,0) LabaRugi,
case when dbo.FGetAVGForFundClientPosition(@DateTo,A.FundClientPK,A.FundPK) > 0 then                         
(isnull(C.NAV,0) - dbo.FGetAVGForFundClientPosition(@DateTo,A.FundClientPK,A.FundPK))/dbo.FGetAVGForFundClientPosition(@DateTo,A.FundClientPK,A.FundPK) * 100 else 0 end LabaRugiPercent

                        from FundClientPosition A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join CloseNAV C on A.FUndPK = C.FundPK and C.Date = @DateTo and C.status in (1,2)
                        left join fundclient D on A.FundClientPK = D.FundClientPK and D.status in (1,2)
                        where A.Date = @DateTo" + _paramName + _paramEmail + _paramPhoneNo + _paramKtpNo;
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ClientDashboard.Add(setSummaryTransaction(dr));
                                }
                            }
                            return L_ClientDashboard;
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