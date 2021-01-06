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
    public class DashboardReps
    {
        Host _host = new Host();
        private Dashboard setDashboard(SqlDataReader dr)
        {
            Dashboard M_Dashboard = new Dashboard();
            M_Dashboard.TableName = dr["TableName"].ToString();
            M_Dashboard.Description = dr["Description"].ToString();
            M_Dashboard.Reason = dr["Reason"].ToString();
            M_Dashboard.NoSystem = Convert.ToInt32(dr["NoSystem"]);
            return M_Dashboard;
        }

        private Dashboard_TotalPendingTransaction setDashboard_TotalPendingTransaction(SqlDataReader dr)
        {
            Dashboard_TotalPendingTransaction M_Dashboard = new Dashboard_TotalPendingTransaction();
            M_Dashboard.TableName = dr["TableName"].ToString();
            M_Dashboard.TotalPending = Convert.ToInt32(dr["TotalPending"]);
            return M_Dashboard;
        }

        public List<Dashboard> Dashboard_Select()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Dashboard> L_Dashboard = new List<Dashboard>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"


                            Select 'HighRiskMonitoring' TableName,A.fundClientPK NoSystem, B.ID + '-' + B.Name Description,cast(Date as nvarchar(12)) + ' - ' + Reason Reason
                            from highRiskMonitoring A
                            left join FundClient B on A.fundClientPK = B.FundClientPk and B.status = 2
                            where A.status = 1


                            UNION ALL

                            Select 'ClientSubscription' TableName,  A.ClientSubscriptionPK NoSystem, B.ID + '-' + B.Name Description,
                            isnull(cast(ValueDate as nvarchar(12)) + ' - Pending Subscription Amount:' + cast(CashAmount as nvarchar(20)),'') Reason
                            From ClientSubscription A left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
                            where A.status = 1

                            UNION ALL

                            Select 'ClientRedemption' TableName,  A.ClientRedemptionPK NoSystem, isnull(B.ID,'') + '-' + isnull(B.Name,'') Description,
                            isnull(cast(ValueDate as nvarchar(12)) + ' - Pending Redemption Unit:' + cast(UnitAmount as nvarchar(20)),'') Reason
                            From ClientRedemption A left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
                            where A.status = 1

                            UNION ALL

                            Select 'FundClient' TableName,FundClientPK NoSystem, 'This client have position but pending status in master' Description  
                            ,'PENDING IN FUND CLIENT' reason
                            from FundClientPosition A
                            where Date = 
                            (
	                            Select max(date) from fundclientPosition 
                            ) and FundClientPK not in
                            (
	                            Select FundClientPK from FundClient where status = 2
                            )

                            UNION ALL

                            Select 'FundClient' TableName,  A.FundClientPK NoSystem, isnull(A.ID,'ID NOT GENERATED YET') + '-' + isnull(A.Name,'') Description,
                            'PENDING IN FUND CLIENT' Reason
                            From FundClient A 
                            where A.status = 1

                            UNION ALL

                            Select  'FundClient' TableName,FundClientPK NoSystem,ID + '-' + Name Description, 'KTP EXPIRED ' + cast(ExpiredDateIdentitasInd1 as nvarchar(12)) Reason
                            from fundclient 
                            Where ExpiredDateIdentitasInd1 is not null and ExpiredDateIdentitasInd1 <> ''
                            and ExpiredDateIdentitasInd1 <= getdate()
                            and status = 2

                        ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Dashboard.Add(setDashboard(dr));
                                }
                            }
                            return L_Dashboard;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<Dashboard_TotalPendingTransaction> Dashboard_TotalPendingTransaction_Select()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Dashboard_TotalPendingTransaction> L_Dashboard = new List<Dashboard_TotalPendingTransaction>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Select 'HighRiskMonitoring' TableName,count(HighriskmonitoringPK) TotalPending from HighRiskMonitoring
                        where status = 1
                        UNION ALL
                        Select 'ClientSubscription' TableName,count(ClientSubscriptionPK) TotalPending from ClientSubscription
                        where status = 1
                        UNION ALL
                        Select 'ClientRedemption' TableName,count(ClientRedemptionPK) TotalPending from ClientRedemption
                        where status = 1
                        UNION ALL
                        Select 'ClientSwitching' TableName,count(ClientRedemptionPK) TotalPending from ClientRedemption
                        where status = 1
                        UNION ALL
                        Select 'Investment' TableName,count(InvestmentPK) TotalPending from Investment
                        where statusInvestment = 1
                        UNION ALL
                        Select 'Dealing' TableName,count(InvestmentPK) TotalPending from Investment
                        where statusDealing = 1
                        UNION ALL
                        Select 'Settlement' TableName,count(InvestmentPK) TotalPending from Investment
                        where statusSettlement = 1
                        UNION ALL
                        Select 'FundJournal' TableName,count(FundJournalPK) TotalPending from FundJournal
                        where status = 1
                        UNION ALL
                        Select 'Cashier' TableName,count(CashierPK) TotalPending from Cashier
                        where status = 1
                        UNION ALL
                        Select 'Journal' TableName,count(JournalPK) TotalPending from Journal
                        where status = 1
                        ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Dashboard.Add(setDashboard_TotalPendingTransaction(dr));
                                }
                            }
                            return L_Dashboard;
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