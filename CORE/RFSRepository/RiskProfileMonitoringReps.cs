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
    public class RiskProfileMonitoringReps
    {
        Host _host = new Host();

        //2
        private RiskProfileMonitoring setRiskProfileMonitoring(SqlDataReader dr)
        {
            RiskProfileMonitoring M_RiskProfileMonitoring = new RiskProfileMonitoring();
            M_RiskProfileMonitoring.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_RiskProfileMonitoring.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_RiskProfileMonitoring.FundID = Convert.ToString(dr["FundID"]);
            M_RiskProfileMonitoring.FundRiskProfile = Convert.ToString(dr["FundRiskProfile"]);
            M_RiskProfileMonitoring.InvestorRiskProfile = Convert.ToString(dr["InvestorRiskProfile"]);
            M_RiskProfileMonitoring.UnitPosition = Convert.ToDecimal(dr["UnitPosition"]);
            M_RiskProfileMonitoring.CashPosition = Convert.ToDecimal(dr["CashPosition"]);
            M_RiskProfileMonitoring.SID = Convert.ToString(dr["SID"]);
            M_RiskProfileMonitoring.IFUA = Convert.ToString(dr["IFUA"]);
            return M_RiskProfileMonitoring;
        }

        public List<RiskProfileMonitoring> RiskProfileMonitoring_SelectRiskProfileMonitoringByDate(string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RiskProfileMonitoring> L_RiskProfileMonitoring = new List<RiskProfileMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"
                                                       
                            select  isnull(F.ID,'') FundID,B.ID FundClientID,B.Name FundClientName,isnull(B.SID,'') SID,isnull(B.IFUACode,'') IFUA,isnull(D.DescOne,'') InvestorRiskProfile, isnull(E.DescOne,'') FundRiskProfile, A.UnitAmount UnitPosition, 
                            A.UnitAmount * isnull(G.Nav,0) CashPosition
                            from  fundClientPosition A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
                            left join FundRiskProfile C on C.FundPK = A.FundPK and C.status in (1,2)
                            left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status in (1,2)
                            left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status in (1,2)
                            left join Fund F on C.FundPK = F.FundPK and F.status in (1,2)
                            left join CloseNAV G on F.FundPK = G.FundPK and G.status = 2 and G.Date = @Date
                            where isnull(B.InvestorsRiskProfile,0) < isnull(C.RiskProfilePK,0)
                            and isnull(B.InvestorsRiskProfile,0) <> 0
                            and A.Date = @Date and A.UnitAmount > 0.1

                        ";


                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RiskProfileMonitoring.Add(setRiskProfileMonitoring(dr));
                                }
                            }
                            return L_RiskProfileMonitoring;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //public void RiskProfileMonitoring_Suspend(RiskProfileMonitoring _RiskProfileMonitoring)
        //{
        //    try
        //    {
        //        DateTime _dateTimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                if (_RiskProfileMonitoring.Status == 2)
        //                {
        //                    cmd.CommandText = " INSERT INTO [FundClient] ([FundClientPK],[HistoryPK],[Status],[StatusFundClientSDI],[StatusFundClientBank],[StatusFundClientTradingSetup], " +
        //                    " [StatusFundClientTransactionAttributes],[StatusHighRiskMonitoring],[Notes],[ID],[Name],[FundClientCategoryPK],[FundClientType],[AccountFundClientCode], " +
        //                    " [BitIsGroup],[ParentPK],[SDIPK],[DepartmentPK],[OfficePK],[AgentPK],[BitIsSuspend],[EntryUsersID],[EntryTime], " +
        //                    " [LastUpdate] ) " +
        //                    " Select @PK,@NewHistoryPK,1,[StatusFundClientSDI],1,1, " +
        //                    " 1,[StatusHighRiskMonitoring],@NewNotes,[ID],[Name],[FundClientCategoryPK],[FundClientType],[AccountFundClientCode], " +
        //                    " [BitIsGroup],[ParentPK],[SDIPK],[DepartmentPK],[OfficePK],[AgentPK],1,@EntryUsersID,@EntryTime, " +
        //                    " @LastUpdate from FundClient where FundClientPK= @PK and historyPK = @HistoryPK " +
        //                    " Update FundClient set status = 4, Notes=@Notes, " +
        //                    " UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime, lastupdate=@lastupdate where FundClientPK = @PK and historyPK = @HistoryPK";
        //                    cmd.Parameters.AddWithValue("@PK", _RiskProfileMonitoring.FundClientPK);
        //                    cmd.Parameters.AddWithValue("@NewHistoryPK", _host.Get_NewHistoryPK(_RiskProfileMonitoring.FundClientPK, "FundClient"));
        //                    cmd.Parameters.AddWithValue("@HistoryPK", _RiskProfileMonitoring.HistoryPK);
        //                    cmd.Parameters.AddWithValue("@NewNotes", "Generate By Dormant Account (Status Suspend = Yes)");
        //                    cmd.Parameters.AddWithValue("@Notes", "Pending Data By Dormant Account");
        //                    cmd.Parameters.AddWithValue("@EntryUsersID", _RiskProfileMonitoring.EntryUsersID);
        //                    cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
        //                    cmd.Parameters.AddWithValue("@UpdateUsersID", _RiskProfileMonitoring.EntryUsersID);
        //                    cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
        //                    cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
        //                    cmd.ExecuteNonQuery();
        //                }
        //                else
        //                {
        //                    cmd.CommandText = " update FundClient set BitIsSuspend = 1,LastUpdate = @LastUpdate " +
        //                                      " where FundClientPK = @PK and historypk = @historyPK";
        //                    cmd.Parameters.AddWithValue("@PK", _RiskProfileMonitoring.FundClientPK);
        //                    cmd.Parameters.AddWithValue("@historyPK", _RiskProfileMonitoring.HistoryPK);
        //                    cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}

        
        //public void RiskProfileMonitoring_Activated(RiskProfileMonitoring _RiskProfileMonitoring)
        //{
        //    try
        //    {
        //        DateTime _dateTimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {

        //                cmd.CommandText = " update FundClient set status = 2,LastUpdate = @LastUpdate " +
        //                                  " where FundClientPK = @PK and historypk = @historyPK";
        //                cmd.Parameters.AddWithValue("@PK", _RiskProfileMonitoring.FundClientPK);
        //                cmd.Parameters.AddWithValue("@historyPK", _RiskProfileMonitoring.HistoryPK);
        //                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
        //                cmd.ExecuteNonQuery();

        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}

    }
}