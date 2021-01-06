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
using System.Data.OleDb;

namespace RFSRepository
{
    public class ClientSubscriptionReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();
        //1
        string _insertCommand = "INSERT INTO [dbo].[ClientSubscription] " +
                            "([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[TrxUnitPaymentTypePK],[TrxUnitPaymentProviderPK]," +
                            " [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount]," +
                            " [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[Type],[BitImmediateTransaction],[FeeType],[ReferenceSInvest],[Tenor],[InterestRate],[PaymentTerm],[SumberDana],[TransactionPromoPK],[OtherAmount],";
        string _paramaterCommand = "@NAVDate,@ValueDate,@TrxUnitPaymentTypePK,@TrxUnitPaymentProviderPK,@NAV,@FundPK,@FundClientPK,@CashRefPK,@CurrencyPK," +
                            "@Description,@CashAmount,@UnitAmount,@TotalCashAmount,@TotalUnitAmount,@SubscriptionFeePercent,@SubscriptionFeeAmount,@AgentPK,@AgentFeePercent,@AgentFeeAmount,@Type,@BitImmediateTransaction,@FeeType,@ReferenceSInvest,@Tenor,@InterestRate,@PaymentTerm,@SumberDana,@TransactionPromoPK,@OtherAmount,";
        //2
        private ClientSubscription setClientSubscription(SqlDataReader dr)
        {
            ClientSubscription M_ClientSubscription = new ClientSubscription();
            M_ClientSubscription.ClientSubscriptionPK = Convert.ToInt32(dr["ClientSubscriptionPK"]);
            M_ClientSubscription.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_ClientSubscription.Selected = Convert.ToBoolean(dr["Selected"]);
            M_ClientSubscription.Status = Convert.ToInt32(dr["Status"]);
            M_ClientSubscription.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_ClientSubscription.Notes = Convert.ToString(dr["Notes"]);
            M_ClientSubscription.Type = Convert.ToInt32(dr["Type"]);
            M_ClientSubscription.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_ClientSubscription.NAVDate = dr["NAVDate"].ToString();
            M_ClientSubscription.ValueDate = Convert.ToString(dr["ValueDate"]);
            //M_ClientSubscription.PaymentDate = Convert.ToString(dr["PaymentDate"]);
            M_ClientSubscription.FundName = Convert.ToString(dr["FundName"]);
            M_ClientSubscription.NAV = Convert.ToDecimal(dr["NAV"]);
            M_ClientSubscription.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_ClientSubscription.FundID = Convert.ToString(dr["FundID"]);
            M_ClientSubscription.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_ClientSubscription.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_ClientSubscription.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_ClientSubscription.ClientCategory = Convert.ToInt32(dr["ClientCategory"]);
            M_ClientSubscription.InvestorType = Convert.ToInt32(dr["InvestorType"]);
            M_ClientSubscription.CashRefPK = Convert.ToInt32(dr["CashRefPK"]);
            M_ClientSubscription.CashRefID = Convert.ToString(dr["CashRefID"].ToString());
            M_ClientSubscription.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_ClientSubscription.CurrencyID = Convert.ToString(dr["CurrencyID"].ToString());
            M_ClientSubscription.Description = Convert.ToString(dr["Description"]);
            M_ClientSubscription.ReferenceSInvest = dr["ReferenceSInvest"].ToString();
            M_ClientSubscription.CashAmount = Convert.ToDecimal(dr["CashAmount"]);
            M_ClientSubscription.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            M_ClientSubscription.TotalCashAmount = Convert.ToDecimal(dr["TotalCashAmount"]);
            M_ClientSubscription.TotalUnitAmount = Convert.ToDecimal(dr["TotalUnitAmount"]);
            M_ClientSubscription.SubscriptionFeePercent = Convert.ToDecimal(dr["SubscriptionFeePercent"]);
            M_ClientSubscription.SubscriptionFeeAmount = Convert.ToDecimal(dr["SubscriptionFeeAmount"]);
            M_ClientSubscription.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_ClientSubscription.AgentID = Convert.ToString(dr["AgentID"]);
            M_ClientSubscription.AgentName = Convert.ToString(dr["AgentName"]);
            M_ClientSubscription.AgentFeePercent = Convert.ToDecimal(dr["AgentFeePercent"]);
            M_ClientSubscription.AgentFeeAmount = Convert.ToDecimal(dr["AgentFeeAmount"]);
            M_ClientSubscription.BitImmediateTransaction = Convert.ToBoolean(dr["BitImmediateTransaction"]);
            M_ClientSubscription.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_ClientSubscription.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);
            M_ClientSubscription.SumberDana = dr["SumberDana"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SumberDana"]);
            M_ClientSubscription.SumberDanaDesc = dr["SumberDanaDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SumberDanaDesc"]);
            M_ClientSubscription.TransactionPromoPK = dr["TransactionPromoPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TransactionPromoPK"]);
            M_ClientSubscription.TransactionPromoID = dr["TransactionPromoID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TransactionPromoID"]);
            if (_host.CheckColumnIsExist(dr, "OtherAmount"))
            {
                M_ClientSubscription.OtherAmount = dr["OtherAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["OtherAmount"]);
            }
            M_ClientSubscription.Posted = Convert.ToBoolean(dr["Posted"]);
            M_ClientSubscription.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_ClientSubscription.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_ClientSubscription.Revised = Convert.ToBoolean(dr["Revised"]);
            M_ClientSubscription.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_ClientSubscription.RevisedTime = dr["RevisedTime"].ToString();
            M_ClientSubscription.EntryUsersID = dr["EntryUsersID"].ToString();
            M_ClientSubscription.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_ClientSubscription.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_ClientSubscription.VoidUsersID = dr["VoidUsersID"].ToString();
            M_ClientSubscription.EntryTime = dr["EntryTime"].ToString();
            M_ClientSubscription.UpdateTime = dr["UpdateTime"].ToString();
            M_ClientSubscription.ApprovedTime = dr["ApprovedTime"].ToString();
            M_ClientSubscription.VoidTime = dr["VoidTime"].ToString();
            M_ClientSubscription.DBUserID = dr["DBUserID"].ToString();
            M_ClientSubscription.DBTerminalID = dr["DBTerminalID"].ToString();
            M_ClientSubscription.LastUpdate = dr["LastUpdate"].ToString();
            M_ClientSubscription.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            M_ClientSubscription.TransactionPK = dr["TransactionPK"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TransactionPK"]);
            M_ClientSubscription.Tenor = dr["Tenor"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Tenor"]);
            M_ClientSubscription.TenorDesc = dr["TenorDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TenorDesc"]);
            M_ClientSubscription.InterestRate = dr["InterestRate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["InterestRate"]);
            M_ClientSubscription.PaymentTerm = dr["PaymentTerm"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PaymentTerm"]);
            M_ClientSubscription.PaymentTermDesc = dr["PaymentTermDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PaymentTermDesc"]);
            M_ClientSubscription.IFUACode = Convert.ToString(dr["IFUACode"]);
            M_ClientSubscription.FrontID = dr["FrontID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FrontID"]);
            M_ClientSubscription.TrxUnitPaymentTypePK = dr["TrxUnitPaymentTypePK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TrxUnitPaymentTypePK"]);
            M_ClientSubscription.TrxUnitPaymentTypeID = dr["TrxUnitPaymentTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TrxUnitPaymentTypeID"]);
            M_ClientSubscription.TrxUnitPaymentTypeName = dr["TrxUnitPaymentTypeName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TrxUnitPaymentTypeName"]);
            M_ClientSubscription.TrxUnitPaymentProviderPK = dr["TrxUnitPaymentProviderPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TrxUnitPaymentProviderPK"]);
            M_ClientSubscription.TrxUnitPaymentProviderID = dr["TrxUnitPaymentProviderID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TrxUnitPaymentProviderID"]);
            M_ClientSubscription.TrxUnitPaymentProviderName = dr["TrxUnitPaymentProviderName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TrxUnitPaymentProviderName"]);

            return M_ClientSubscription;
        }
        //3
        public List<ClientSubscription> ClientSubscription_Select(int _status)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientSubscription> L_ClientSubscription = new List<ClientSubscription>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select TP.ID TransactionPromoID,CU.ID CurrencyID,F.ID FundID, FC.ID FundClientID,FC.Name FundClientName, CR.ID CashRefID,A.ID AgentID,MV.DescOne TypeDesc,Case when FeeType = 1 then 'Percent' else 'Amount' End FeeTypeDesc ,
                                S.ID TrxUnitPaymentProviderID,S.Name TrxUnitPaymentProviderName, T.ID TrxUnitPaymentTypeID,T.Name TrxUnitPaymentTypeName,
								R.* from ClientSubscription R left join 
                                Fund F on R.FundPK = F.FundPK and F.Status =2 left join 
                                FundClient FC on R.FundClientPK = FC.FundClientPK and FC.Status =2  left join 
                                CashRef CR on R.CashRefPK = CR.CashRefPK and CR.Status =2 left join 
                                Agent A on R.AgentPK = A.AgentPK and A.Status =2 left join 
                                MasterValue MV on R.Type = MV.Code and MV.Status =2 and MV.ID ='SubscriptionType' left join 
                                TransactionPromo TP on R.TransactionPromoPK = TP.TransactionPromoPK and TP.Status =2 left join
								TrxUnitPaymentProvider S on R.TrxUnitPaymentProviderPK = S.TrxUnitPaymentProviderPK and S.status = 2 left join 
								TrxUnitPaymentType T on R.TrxUnitPaymentTypePK = T.TrxUnitPaymentTypePK and T.status = 2 left join 
                                Currency CU on R.CurrencyPK = CU.CurrencyPK and CU.Status =2 
                                where R.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select TP.ID TransactionPromoID,CU.ID CurrencyID,F.ID FundID, FC.ID FundClientID,FC.Name FundClientName, CR.ID CashRefID,A.ID AgentID,Case when FeeType = 1 then 'Percent' else 'Amount' End FeeTypeDesc ,
							S.ID TrxUnitPaymentProviderID,S.Name TrxUnitPaymentProviderName, T.ID TrxUnitPaymentTypeID,T.Name TrxUnitPaymentTypeName,
								R.* from ClientSubscription R left join 
                                Fund F on R.FundPK = F.FundPK and F.Status =2 left join 
                                FundClient FC on R.FundClientPK = FC.FundClientPK and FC.Status =2  left join 
                                CashRef CR on R.CashRefPK = CR.CashRefPK and CR.Status =2 left join 
                                Agent A on R.AgentPK = A.AgentPK and A.Status =2 left join
                                MasterValue MV on R.Type = MV.Code and MV.Status =2 and MV.ID ='SubscriptionType' left join 
                                TransactionPromo TP on R.TransactionPromoPK = TP.TransactionPromoPK and TP.Status =2 left join
								TrxUnitPaymentProvider S on R.TrxUnitPaymentProviderPK = S.TrxUnitPaymentProviderPK and S.status = 2 left join 
								TrxUnitPaymentType T on R.TrxUnitPaymentTypePK = T.TrxUnitPaymentTypePK and T.status = 2 left join  
                                Currency CU on R.CurrencyPK = CU.CurrencyPK and CU.Status =2 ";

                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ClientSubscription.Add(setClientSubscription(dr));
                                }
                            }
                            return L_ClientSubscription;
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
        public int ClientSubscription_Add(ClientSubscription _clientSubscription, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[IsBoTransaction],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(ClientSubscriptionPk),0) + 1,1,@status," + _paramaterCommand + "1,@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From ClientSubscription";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientSubscription.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[IsBoTransaction],[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(ClientSubscriptionPk),0) + 1,1,@status," + _paramaterCommand + "1,@EntryUsersID,@EntryTime,@LastUpdate From ClientSubscription";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Type", _clientSubscription.Type);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientSubscription.NAVDate);
                        cmd.Parameters.AddWithValue("@ValueDate", _clientSubscription.ValueDate);
                        cmd.Parameters.AddWithValue("@SumberDana", _clientSubscription.SumberDana);
                        //cmd.Parameters.AddWithValue("@PaymentDate", _clientSubscription.PaymentDate);
                        cmd.Parameters.AddWithValue("@NAV", _clientSubscription.NAV);
                        cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                        cmd.Parameters.AddWithValue("@CashRefPK", _clientSubscription.CashRefPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _clientSubscription.CurrencyPK);
                        cmd.Parameters.AddWithValue("@Description", _clientSubscription.Description);
                        cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSubscription.ReferenceSInvest);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientSubscription.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientSubscription.UnitAmount);
                        cmd.Parameters.AddWithValue("@TotalCashAmount", _clientSubscription.TotalCashAmount);
                        cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientSubscription.TotalUnitAmount);
                        cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _clientSubscription.SubscriptionFeePercent);
                        cmd.Parameters.AddWithValue("@SubscriptionFeeAmount", _clientSubscription.SubscriptionFeeAmount);
                        cmd.Parameters.AddWithValue("@AgentPK", _clientSubscription.AgentPK);
                        cmd.Parameters.AddWithValue("@AgentFeePercent", _clientSubscription.AgentFeePercent);
                        cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientSubscription.AgentFeeAmount);
                        cmd.Parameters.AddWithValue("@BitImmediateTransaction", _clientSubscription.BitImmediateTransaction);
                        cmd.Parameters.AddWithValue("@FeeType", _clientSubscription.FeeType);
                        cmd.Parameters.AddWithValue("@Tenor", _clientSubscription.Tenor);
                        cmd.Parameters.AddWithValue("@InterestRate", _clientSubscription.InterestRate);
                        if (_clientSubscription.OtherAmount == 0 || _clientSubscription.OtherAmount == null)
                        {
                            cmd.Parameters.AddWithValue("@OtherAmount", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@OtherAmount", _clientSubscription.OtherAmount);
                        }
                        cmd.Parameters.AddWithValue("@TransactionPromoPK", _clientSubscription.TransactionPromoPK);
                        cmd.Parameters.AddWithValue("@PaymentTerm", _clientSubscription.PaymentTerm);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _clientSubscription.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _clientSubscription.TrxUnitPaymentProviderPK);
                        cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _clientSubscription.TrxUnitPaymentTypePK);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ClientSubscription");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }
        //6
        public int ClientSubscription_Update(ClientSubscription _clientSubscription, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, "ClientSubscription");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update ClientSubscription set status=2, Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,TrxUnitPaymentProviderPK=@TrxUnitPaymentProviderPK,TrxUnitPaymentTypePK=@TrxUnitPaymentTypePK,
                                NAV=@NAV,FundPK=@FundPK,FundClientPK=@FundClientPK,CashRefPK=@CashRefPK,CurrencyPK=@CurrencyPK,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,TotalCashAmount=@TotalCashAmount,TotalUnitAmount=@TotalUnitAmount,
                                SubscriptionFeePercent=@SubscriptionFeePercent,SubscriptionFeeAmount=@SubscriptionFeeAmount,AgentPK=@AgentPK,AgentFeePercent=@AgentFeePercent,AgentFeeAmount=@AgentFeeAmount,Type=@Type,BitImmediateTransaction=@BitImmediateTransaction,
                                FeeType=@FeeType,ReferenceSInvest = @ReferenceSInvest, Tenor=@Tenor,InterestRate=@InterestRate,PaymentTerm=@PaymentTerm,SumberDana=@SumberDana,TransactionPromoPK=@TransactionPromoPK,OtherAmount=@OtherAmount,
                                ApprovedUsersID=@ApprovedUsersID, 
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where ClientSubscriptionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientSubscription.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _clientSubscription.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                            cmd.Parameters.AddWithValue("@Type", _clientSubscription.Type);
                            cmd.Parameters.AddWithValue("@NAVDate", _clientSubscription.NAVDate);
                            cmd.Parameters.AddWithValue("@ValueDate", _clientSubscription.ValueDate);
                            //cmd.Parameters.AddWithValue("@PaymentDate", _clientSubscription.PaymentDate);
                            cmd.Parameters.AddWithValue("@NAV", _clientSubscription.NAV);
                            cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                            cmd.Parameters.AddWithValue("@CashRefPK", _clientSubscription.CashRefPK);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _clientSubscription.CurrencyPK);
                            cmd.Parameters.AddWithValue("@Description", _clientSubscription.Description);
                            cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSubscription.ReferenceSInvest);
                            cmd.Parameters.AddWithValue("@CashAmount", _clientSubscription.CashAmount);
                            cmd.Parameters.AddWithValue("@UnitAmount", _clientSubscription.UnitAmount);
                            cmd.Parameters.AddWithValue("@TotalCashAmount", _clientSubscription.TotalCashAmount);
                            cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientSubscription.TotalUnitAmount);
                            cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _clientSubscription.SubscriptionFeePercent);
                            cmd.Parameters.AddWithValue("@SubscriptionFeeAmount", _clientSubscription.SubscriptionFeeAmount);
                            cmd.Parameters.AddWithValue("@AgentPK", _clientSubscription.AgentPK);
                            cmd.Parameters.AddWithValue("@AgentFeePercent", _clientSubscription.AgentFeePercent);
                            cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientSubscription.AgentFeeAmount);
                            cmd.Parameters.AddWithValue("@BitImmediateTransaction", _clientSubscription.BitImmediateTransaction);
                            cmd.Parameters.AddWithValue("@FeeType", _clientSubscription.FeeType);
                            cmd.Parameters.AddWithValue("@Tenor", _clientSubscription.Tenor);
                            cmd.Parameters.AddWithValue("@InterestRate", _clientSubscription.InterestRate);
                            cmd.Parameters.AddWithValue("@PaymentTerm", _clientSubscription.PaymentTerm);
                            cmd.Parameters.AddWithValue("@SumberDana", _clientSubscription.SumberDana);
                            cmd.Parameters.AddWithValue("@TransactionPromoPK", _clientSubscription.TransactionPromoPK);
                            if (_clientSubscription.OtherAmount == 0 || _clientSubscription.OtherAmount == null)
                            {
                                cmd.Parameters.AddWithValue("@OtherAmount", 0);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OtherAmount", _clientSubscription.OtherAmount);
                            }
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSubscription.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientSubscription.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _clientSubscription.TrxUnitPaymentProviderPK);
                            cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _clientSubscription.TrxUnitPaymentTypePK);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClientSubscription set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ClientSubscriptionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _clientSubscription.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = @"Update ClientSubscription set Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,TrxUnitPaymentProviderPK=@TrxUnitPaymentProviderPK,TrxUnitPaymentTypePK=@TrxUnitPaymentTypePK,
                                NAV=@NAV,FundPK=@FundPK,FundClientPK=@FundClientPK,CashRefPK=@CashRefPK,CurrencyPK=@CurrencyPK,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,TotalCashAmount=@TotalCashAmount,TotalUnitAmount=@TotalUnitAmount,
                                SubscriptionFeePercent=@SubscriptionFeePercent,SubscriptionFeeAmount=@SubscriptionFeeAmount,AgentPK=@AgentPK,AgentFeePercent=@AgentFeePercent,AgentFeeAmount=@AgentFeeAmount,Type=@Type,BitImmediateTransaction=@BitImmediateTransaction,
                                FeeType=@FeeType,ReferenceSInvest = @ReferenceSInvest, Tenor=@Tenor,InterestRate=@InterestRate,PaymentTerm=@PaymentTerm,SumberDana=@SumberDana,TransactionPromoPK=@TransactionPromoPK,OtherAmount=@OtherAmount,
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where ClientSubscriptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSubscription.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _clientSubscription.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                                cmd.Parameters.AddWithValue("@Type", _clientSubscription.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientSubscription.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientSubscription.ValueDate);
                                //cmd.Parameters.AddWithValue("@PaymentDate", _clientSubscription.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAV", _clientSubscription.NAV);
                                cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPK", _clientSubscription.CashRefPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientSubscription.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Description", _clientSubscription.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSubscription.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientSubscription.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientSubscription.UnitAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmount", _clientSubscription.TotalCashAmount);
                                cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientSubscription.TotalUnitAmount);
                                cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _clientSubscription.SubscriptionFeePercent);
                                cmd.Parameters.AddWithValue("@SubscriptionFeeAmount", _clientSubscription.SubscriptionFeeAmount);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientSubscription.AgentPK);
                                cmd.Parameters.AddWithValue("@AgentFeePercent", _clientSubscription.AgentFeePercent);
                                cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientSubscription.AgentFeeAmount);
                                cmd.Parameters.AddWithValue("@BitImmediateTransaction", _clientSubscription.BitImmediateTransaction);
                                cmd.Parameters.AddWithValue("@FeeType", _clientSubscription.FeeType);
                                cmd.Parameters.AddWithValue("@Tenor", _clientSubscription.Tenor);
                                cmd.Parameters.AddWithValue("@InterestRate", _clientSubscription.InterestRate);
                                cmd.Parameters.AddWithValue("@PaymentTerm", _clientSubscription.PaymentTerm);
                                cmd.Parameters.AddWithValue("@SumberDana", _clientSubscription.SumberDana);
                                cmd.Parameters.AddWithValue("@TransactionPromoPK", _clientSubscription.TransactionPromoPK);
                                if (_clientSubscription.OtherAmount == 0 || _clientSubscription.OtherAmount == null)
                                {
                                    cmd.Parameters.AddWithValue("@OtherAmount", 0);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OtherAmount", _clientSubscription.OtherAmount);
                                }
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSubscription.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _clientSubscription.TrxUnitPaymentProviderPK);
                                cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _clientSubscription.TrxUnitPaymentTypePK);
                                cmd.ExecuteNonQuery();
                            }

                            if (_clientSubscription.Notes == "Pending Revised")
                            {
                                using (SqlCommand cmd = DbCon.CreateCommand())
                                {
                                    cmd.CommandText = @"Declare @CashAtBankAcc int
                                    Declare @PendingSubscription int
                                    Declare @Subscription int
                                    Declare @PayableSubsAcc int
                                    Declare @FundClientName nvarchar(100)

                                    Declare @FundJournalPK int


                                    select @FundJournalPK = A.FundJournalPK from FundJournalDetail A
                                    left join FundJournal B on A.FundJournalPK = B.FundJournalPK  where TrxName = 'Pending Subscription'
                                    and ValueDate = @ValueDate and FundClientPK = @FundClientPK and FundPK = @FundPK

                                    Delete FundJournalDetail where FundJournalPK = @FundJournalPK 

                                    Select @FundClientName = Name From FundClient where FundClientPK = @FundClientPK and status in (1,2)

                                    Select @CashAtBankAcc = isnull(FundJournalAccountPK,3) From FundCashRef where Status = 2 and FundCashRefPK = @CashRefPK
                                    if (@CashAtBankAcc is null or @CashAtBankAcc  = '')
                                    BEGIN
                                    set @CashAtBankAcc = 3
                                    END

                                    Select @PendingSubscription = PendingSubscription,@Subscription = Subscription
                                    ,@PayableSubsAcc = payablesubscriptionfee
                                    From FundAccountingSetup 
                                    where Status = 2  and FundPK = @FundPK


                                    INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK]                  
                                    ,[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  
                                    Select  @FundJournalPK,1,1,2,@CashAtBankAcc,@CurrencyPK,@FundPK,0,@FundClientPK,'Subscription Fund Client : ' + @FundClientName,'D',@CashAmount,                   
                                    @CashAmount,0,1,@CashAmount,0,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @CashAtBankAcc and Status = 2                   

                                    INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK]                  
                                    ,[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  
                                    Select @FundJournalPK,2,1,2,@PendingSubscription,@CurrencyPK,@FundPK,0,@FundClientPK,'Subscription Fund Client : ' + @FundClientName,'C',@TotalCashAmount,                   
                                    0,@TotalCashAmount,1,0,@TotalCashAmount,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @PendingSubscription and Status = 2   


                                    INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK]                  
                                    ,[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  
                                    Select @FundJournalPK,3,1,2,@PayableSubsAcc,@CurrencyPK,@FundPK,0,@FundClientPK,'Subscription Fund Client : ' + @FundClientName,'C',@SubscriptionFeeAmount,                   
                                    0,@SubscriptionFeeAmount,1,0,@SubscriptionFeeAmount,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @PayableSubsAcc and Status = 2   

                                     ";
                                    cmd.Parameters.AddWithValue("@HistoryPK", _clientSubscription.HistoryPK);
                                    cmd.Parameters.AddWithValue("@Notes", _clientSubscription.Notes);
                                    cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                                    cmd.Parameters.AddWithValue("@Type", _clientSubscription.Type);
                                    cmd.Parameters.AddWithValue("@NAVDate", _clientSubscription.NAVDate);
                                    cmd.Parameters.AddWithValue("@ValueDate", _clientSubscription.ValueDate);
                                    //cmd.Parameters.AddWithValue("@PaymentDate", _clientSubscription.PaymentDate);
                                    cmd.Parameters.AddWithValue("@NAV", _clientSubscription.NAV);
                                    cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                                    cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                                    cmd.Parameters.AddWithValue("@CashRefPK", _clientSubscription.CashRefPK);
                                    cmd.Parameters.AddWithValue("@CurrencyPK", _clientSubscription.CurrencyPK);
                                    cmd.Parameters.AddWithValue("@Description", _clientSubscription.Description);
                                    cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSubscription.ReferenceSInvest);
                                    cmd.Parameters.AddWithValue("@CashAmount", _clientSubscription.CashAmount);
                                    cmd.Parameters.AddWithValue("@UnitAmount", _clientSubscription.UnitAmount);
                                    cmd.Parameters.AddWithValue("@TotalCashAmount", _clientSubscription.TotalCashAmount);
                                    cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientSubscription.TotalUnitAmount);
                                    cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _clientSubscription.SubscriptionFeePercent);
                                    cmd.Parameters.AddWithValue("@SubscriptionFeeAmount", _clientSubscription.SubscriptionFeeAmount);
                                    cmd.Parameters.AddWithValue("@AgentPK", _clientSubscription.AgentPK);
                                    cmd.Parameters.AddWithValue("@AgentFeePercent", _clientSubscription.AgentFeePercent);
                                    cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientSubscription.AgentFeeAmount);
                                    cmd.Parameters.AddWithValue("@BitImmediateTransaction", _clientSubscription.BitImmediateTransaction);
                                    cmd.Parameters.AddWithValue("@FeeType", _clientSubscription.FeeType);
                                    cmd.Parameters.AddWithValue("@Tenor", _clientSubscription.Tenor);
                                    cmd.Parameters.AddWithValue("@InterestRate", _clientSubscription.InterestRate);
                                    cmd.Parameters.AddWithValue("@PaymentTerm", _clientSubscription.PaymentTerm);
                                    cmd.Parameters.AddWithValue("@SumberDana", _clientSubscription.SumberDana);
                                    cmd.Parameters.AddWithValue("@TransactionPromoPK", _clientSubscription.TransactionPromoPK);
                                    cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSubscription.EntryUsersID);
                                    cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                    cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                    cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _clientSubscription.TrxUnitPaymentProviderPK);
                                    cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _clientSubscription.TrxUnitPaymentTypePK);

                                    cmd.ExecuteNonQuery();
                                }
                            }


                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_clientSubscription.ClientSubscriptionPK, "ClientSubscription");
                                cmd.CommandText = _insertCommand + "TransactionPK,[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "@TransactionPK,EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ClientSubscription where ClientSubscriptionPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSubscription.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Type", _clientSubscription.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientSubscription.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientSubscription.ValueDate);
                                //cmd.Parameters.AddWithValue("@PaymentDate", _clientSubscription.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAV", _clientSubscription.NAV);
                                cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPK", _clientSubscription.CashRefPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientSubscription.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Description", _clientSubscription.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSubscription.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientSubscription.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientSubscription.UnitAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmount", _clientSubscription.TotalCashAmount);
                                cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientSubscription.TotalUnitAmount);
                                cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _clientSubscription.SubscriptionFeePercent);
                                cmd.Parameters.AddWithValue("@SubscriptionFeeAmount", _clientSubscription.SubscriptionFeeAmount);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientSubscription.AgentPK);
                                cmd.Parameters.AddWithValue("@AgentFeePercent", _clientSubscription.AgentFeePercent);
                                cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientSubscription.AgentFeeAmount);
                                cmd.Parameters.AddWithValue("@BitImmediateTransaction", _clientSubscription.BitImmediateTransaction);
                                cmd.Parameters.AddWithValue("@FeeType", _clientSubscription.FeeType);
                                cmd.Parameters.AddWithValue("@Tenor", _clientSubscription.Tenor);
                                cmd.Parameters.AddWithValue("@InterestRate", _clientSubscription.InterestRate);
                                cmd.Parameters.AddWithValue("@PaymentTerm", _clientSubscription.PaymentTerm);
                                cmd.Parameters.AddWithValue("@SumberDana", _clientSubscription.SumberDana);
                                cmd.Parameters.AddWithValue("@TransactionPromoPK", _clientSubscription.TransactionPromoPK);
                                if (_clientSubscription.OtherAmount == 0 || _clientSubscription.OtherAmount == null)
                                {
                                    cmd.Parameters.AddWithValue("@OtherAmount", 0);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OtherAmount", _clientSubscription.OtherAmount);
                                }
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSubscription.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@TransactionPK", _clientSubscription.TransactionPK);
                                cmd.Parameters.AddWithValue("@TrxUnitPaymentProviderPK", _clientSubscription.TrxUnitPaymentProviderPK);
                                cmd.Parameters.AddWithValue("@TrxUnitPaymentTypePK", _clientSubscription.TrxUnitPaymentTypePK);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ClientSubscription set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where ClientSubscriptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _clientSubscription.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSubscription.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
        //7
        public void ClientSubscription_Approved(ClientSubscription _ClientSubscription)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientSubscription set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where ClientSubscriptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ClientSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ClientSubscription.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _ClientSubscription.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClientSubscription set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ClientSubscriptionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ClientSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ClientSubscription.ApprovedUsersID);
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
        public void ClientSubscription_Reject(ClientSubscription _ClientSubscription)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientSubscription set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where ClientSubscriptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ClientSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ClientSubscription.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ClientSubscription.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClientSubscription set status= 2,LastUpdate=@lastUpdate where ClientSubscriptionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ClientSubscription.ClientSubscriptionPK);
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
        public void ClientSubscription_Void(ClientSubscription _ClientSubscription)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientSubscription set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where ClientSubscriptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ClientSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ClientSubscription.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ClientSubscription.VoidUsersID);
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

        public List<ClientSubscription> ClientSubscription_SelectClientSubscriptionDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientSubscription> L_ClientSubscription = new List<ClientSubscription>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when R.Status=1 then 'PENDING' else case when R.Status = 2 then 'APPROVED' else case when R.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, CU.ID CurrencyID,F.ID FundID,F.Name FundName, FC.ID FundClientID,FC.Name FundClientName, CR.ID CashRefID,A.ID AgentID,A.Name AgentName,isnull(MV.DescOne,'') TypeDesc,Case when FeeType = 1 then 'Percent' else 'Amount' End FeeTypeDesc, 
                                case when isnull(R.Tenor,0) = 1 then '3' else case when isnull(R.Tenor,0) = 2 then '6' else case when isnull(R.Tenor,0) = 3 then '9' else  '' end end end  TenorDesc, 
                                case when isnull(R.PaymentTerm,0) = 1 then '3' else case when isnull(R.PaymentTerm,0) = 2 then '6' else case when isnull(R.PaymentTerm,0) = 3 then '9' else  ''  end end end PaymentTermDesc,
                                case when FC.InvestorType = 1 then isnull(MV1.DescOne,'') else case when FC.InvestorType = 2 then isnull(MV2.DescOne,'')  else '' end end SumberDanaDesc, FC.InvestorType ClientCategory,
                                isnull(TP.ID,0) TransactionPromoID, FC.InvestorType, FC.IFUACode,R.TransactionPK FrontID,
                                S.ID TrxUnitPaymentProviderID,S.Name TrxUnitPaymentProviderName, T.ID TrxUnitPaymentTypeID,T.Name TrxUnitPaymentTypeName,
                                R.* from ClientSubscription R left join 
                                Fund F on R.FundPK = F.FundPK and F.Status =2 left join 
                                FundClient FC on R.FundClientPK = FC.FundClientPK and FC.Status in (1,2)  left join
                                FundCashRef CR on R.CashRefPK = CR.FundCashRefPK and CR.Status =2 left join 
                                Agent A on R.AgentPK = A.AgentPK and A.Status =2 left join 
                                MasterValue MV on R.Type = MV.Code and MV.Status =2 and MV.ID ='SubscriptionType' left join 
                                MasterValue MV1 on R.SumberDana = MV1.Code and MV1.Status =2 and MV1.ID ='IncomeSourceIND' left join 
                                MasterValue MV2 on R.SumberDana = MV2.Code and MV2.Status =2 and MV2.ID ='IncomeSourceINS' left join 
                                TransactionPromo TP on R.TransactionPromoPK = TP.TransactionPromoPK and TP.Status =2 left join 
                                TrxUnitPaymentProvider S on R.TrxUnitPaymentProviderPK = S.TrxUnitPaymentProviderPK and S.status = 2 left join 
								TrxUnitPaymentType T on R.TrxUnitPaymentTypePK = T.TrxUnitPaymentTypePK and T.status = 2 left join  
                                Currency CU on R.CurrencyPK = CU.CurrencyPK and CU.Status =2 
                                where  R.status = @status and R.ValueDate between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        else
                        {
                            cmd.CommandText = @"	Select case when R.Status=1 then 'PENDING' else case when R.Status = 2 then 'APPROVED' else case when R.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, CU.ID CurrencyID,F.ID FundID,F.Name FundName, FC.ID FundClientID,FC.Name FundClientName, CR.ID CashRefID,A.ID AgentID,A.Name AgentName,isnull(MV.DescOne,'') TypeDesc,Case when FeeType = 1 then 'Percent' else 'Amount' End FeeTypeDesc, 
                                case when isnull(R.Tenor,0) = 1 then '3' else case when isnull(R.Tenor,0) = 2 then '6' else case when isnull(R.Tenor,0) = 3 then '9' else  '' end end end  TenorDesc, 
                                case when isnull(R.PaymentTerm,0) = 1 then '3' else case when isnull(R.PaymentTerm,0) = 2 then '6' else case when isnull(R.PaymentTerm,0) = 3 then '9' else  ''  end end end PaymentTermDesc,
                                case when FC.InvestorType = 1 then isnull(MV1.DescOne,'') else case when FC.InvestorType = 2 then isnull(MV2.DescOne,'')  else '' end end SumberDanaDesc, FC.InvestorType ClientCategory,
                                isnull(TP.ID,0) TransactionPromoID, FC.InvestorType, FC.IFUACode,R.TransactionPK FrontID,
                                S.ID TrxUnitPaymentProviderID,S.Name TrxUnitPaymentProviderName, T.ID TrxUnitPaymentTypeID,T.Name TrxUnitPaymentTypeName,
                                R.* from ClientSubscription R left join 
                                Fund F on R.FundPK = F.FundPK and F.Status =2 left join 
                                FundClient FC on R.FundClientPK = FC.FundClientPK and FC.Status in (1,2)  left join
                                FundCashRef CR on R.CashRefPK = CR.FundCashRefPK and CR.Status =2 left join 
                                Agent A on R.AgentPK = A.AgentPK and A.Status =2 left join 
                                MasterValue MV on R.Type = MV.Code and MV.Status =2 and MV.ID ='SubscriptionType' left join 
                                MasterValue MV1 on R.SumberDana = MV1.Code and MV1.Status =2 and MV1.ID ='IncomeSourceIND' left join 
                                MasterValue MV2 on R.SumberDana = MV2.Code and MV2.Status =2 and MV2.ID ='IncomeSourceINS' left join 
                                TransactionPromo TP on R.TransactionPromoPK = TP.TransactionPromoPK and TP.Status =2 left join 
                                TrxUnitPaymentProvider S on R.TrxUnitPaymentProviderPK = S.TrxUnitPaymentProviderPK and S.status = 2 left join 
								TrxUnitPaymentType T on R.TrxUnitPaymentTypePK = T.TrxUnitPaymentTypePK and T.status = 2 left join  
                                Currency CU on R.CurrencyPK = CU.CurrencyPK and CU.Status =2 
                                where R.ValueDate between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ClientSubscription.Add(setClientSubscription(dr));
                                }
                            }
                            return L_ClientSubscription;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientSubscription_Posting(ClientSubscription _clientSubscription)
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
                                Declare @SubscriptionAcc int Declare @PayableSubsAcc int 
	                            Declare @TotalCashAmount numeric(18,4) 
	                            Declare @TotalFeeAmount numeric(18,4) 
	                            Declare @PCashAmount numeric(18,4) 
                                Declare @ValueDate datetime 
	                            Declare @PeriodPK int 
	                            Declare @BankPK int Declare @BankCurrencyPK int 
	                            Declare @FundClientID nvarchar(100) 
                                Declare @FundCashRefPK int Declare @FundJournalPK int 
                                Declare @CashAmount numeric (22,4) Declare @UnitAmount numeric(22,4) 

                                --**********LOGIC INSERT KE FUND JOURNAL**************
                                --Select @FundJournalPK = isnull(MAX(FundJournalPK) + 1,1) From FundJournal  
                                --Select @PCashAmount = CashAmount,@TotalCashAmount = TotalCashAMount,@TotalFeeAmount = AgentFeeAmount + SubscriptionFeeAmount,@ValueDate =  PaymentDate, @FundCashRefPK = CashRefPK, @FundClientPK = @FundClientPK,
                                --@FundClientID = B.ID From ClientSubscription A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2 where ClientSubscriptionPK = @ClientSubscriptionPK and A.Status = 2 and Posted = 0 
                                --Select @SubscriptionAcc = Subscription,@PayableSubsAcc = payablesubscriptionfee From Fundaccountingsetup where status = 2 
                                --select @BankPK = FundJournalAccountPK,@BankCurrencyPK = A.CurrencyPK from FundCashRef A  where A.FundCashRefPK = @FundCashRefPK and A.status = 2 \n 
                                --Select @PeriodPK = PeriodPK From Period Where @ValueDate Between DateFrom and DateTo and Status = 2 
                                --INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
                                --Select	   @FundJournalPK, 1,2,'Posting From Subscription',@PeriodPK,@ValueDate,1,@ClientSubscriptionPK,'SUBSCRIPTION', '','Subscription Client: ' + @FundClientID,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime 
                                --INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
                                --Select		@FundJournalPK,1,1,2,@BankPK,@BankCurrencyPK,@FundPK,@FundClientPK,'Subscription Client: ' + @FundClientID,'D',@PCashAmount,@PCashAmount,0,1,@PCashAmount,0,@PostedTime 
                                --INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
                                --Select		@FundJournalPK,2,1,2,@SubscriptionAcc,1,@FundPK,@FundClientPK,'Subscription Client: ' + @FundClientID,'C',@TotalCashAmount,	0,@TotalCashAmount,1,0,@TotalCashAmount,@PostedTime 
                                --IF @TotalFeeAmount > 0 BEGIN 
                                --INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
                                --Select	@FundJournalPK,3,1,2,@PayableSubsAcc,1,@FundPK,@FundClientPK,'Subscription Client: ' + @FundClientID,'C',@TotalFeeAmount,	0,@TotalFeeAmount,1,0,@TotalFeeAmount,@PostedTime  END 
                            
                                select @CashAmount = TotalCashAmount, @UnitAmount = TotalUnitAmount 
	                            from ClientSubscription where ClientSubscriptionPK = @ClientSubscriptionPK and Status = 2

                                select * from FundClientPosition 
                                where Date = @Date and FundClientPK = @FundClientPK and FundPK = @FundPK 
                                if @@rowCount > 0 
                                begin 
                                Update FundClientPosition set CashAmount = CashAmount + @CashAmount, 
                                UnitAmount = UnitAmount + @UnitAmount where Date = @Date and FundClientPK = @FundClientPK 
                                and FundPK = @FundPK 
                                end 
                                else 
                                begin 
                                INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)  
                                Select @Date,@FundClientPK,@FundPK,@CashAmount,@UnitAmount 
	                            end

                                update clientsubscription 
                                set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
                                where ClientSubscriptionPK = @ClientSubscriptionPK and Status = 2 

                            
                                Declare @counterDate datetime 
                                set @counterDate = @Date 
                                while @counterDate < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK and year(date) = year(@Date))
                                BEGIN 
                                set @counterDate = dateadd(day,1,@counterDate)  
	                            if exists(select * from FundClientPosition where date = @counterDate and FundClientPK = @FundClientPK and FundPK = @FundPK)
	                            begin
		                            update fundClientPosition set UnitAmount = UnitAmount + @UnitAmount,CashAmount = CashAmount + @CashAmount 
		                            where FundClientPK = @FundClientPK and FundPK = @FundPK and Date = @counterDate end
	                            end
	                            else
	                            begin
		                            INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)  
		                            Select @Date,@FundClientPK,@FundPK,@CashAmount,@UnitAmount 
	                            end


	                             ";



                        cmd.Parameters.AddWithValue("@Date", _clientSubscription.ValueDate);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                        //cmd.Parameters.AddWithValue("@CashAmount", _clientSubscription.TotalCashAmount);
                        //cmd.Parameters.AddWithValue("@UnitAmount", _clientSubscription.TotalUnitAmount);
                        cmd.Parameters.AddWithValue("@PostedBy", _clientSubscription.PostedBy);
                        cmd.Parameters.AddWithValue("@PostedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _clientSubscription.ClientSubscriptionPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientSubscription_Revise(ClientSubscription _clientSubscription)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText =
                        @"

                        update FundJournal set Status = 3,VoidTime = @RevisedTime,VoidUsersID = @RevisedBy  where Type = 2 and TrxNo = @ClientSubscriptionPK 
                        and Posted = 1 

                        select * from FundClientPosition 
                        where Date = @Date and FundClientPK = @FundClientPK and FundPK = @FundPK 
                        if @@rowCount > 0 
                        begin 
                        Update FundClientPosition set CashAmount = CashAmount - @CashAmount, 
                        UnitAmount = UnitAmount - @UnitAmount where Date = @Date and FundClientPK = @FundClientPK 
                        and FundPK = @FundPK 
                        end 
                        
                         Declare @MaxClientSubscriptionPK int 
                        
                         Select @MaxClientSubscriptionPK = ISNULL(MAX(ClientSubscriptionPK),0) + 1 From ClientSubscription   
                         INSERT INTO [dbo].[ClientSubscription]  
                         ([ClientSubscriptionPK],[HistoryPK] ,[Status],[Notes], [NAVDate] ,[ValueDate],
                          [NAV] ,[FundPK], [FundClientPK] , [CashRefPK] , [Description] ,
                         [CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,[SubscriptionFeePercent] ,[SubscriptionFeeAmount] ,
                         [AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK],[DepartmentPK],[AutoDebitDate],[Type],
                         [EntryUsersID],[EntryTime],[LastUpdate],[BitImmediateTransaction],[FeeType],[TransactionPK],[IsFrontSync],[OtherAmount])
                        
                         SELECT @MaxClientSubscriptionPK,1,1,'Pending Revised' ,[NAVDate] ,
                         [ValueDate],[NAV] ,[FundPK],[FundClientPK] ,
                         [CashRefPK] ,[Description] ,[CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,
                         [SubscriptionFeePercent] ,[SubscriptionFeeAmount] ,[AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK],[DepartmentPK],[AutoDebitDate],[Type],
                         [EntryUsersID],[EntryTime] , @RevisedTime, [BitImmediateTransaction],[FeeType],[TransactionPK],0,isnull(OtherAmount,0)
                         FROM ClientSubscription  
                         WHERE ClientSubscriptionPK = @ClientSubscriptionPK   and status = 2 and posted = 1 
                       
IF(@ClientCode = 21)
BEGIN



--AGENT SUBSCRIPTION

Declare @MaxAgentSubsPK int
Declare @AgentSubscriptionPK int

select @AgentSubscriptionPK = AgentSubscriptionPK from AgentSubscription where ClientSubscriptionPK = @ClientSubscriptionPK and status = 2

select @MaxAgentSubsPK = Max(AgentSubscriptionPK) from AgentSubscription
set @MaxAgentSubsPK = isnull(@MaxAgentSubsPK,0)

update AgentSubscription set status = 3 where  ClientSubscriptionPK = @ClientSubscriptionPK 

Insert into AgentSubscription (AgentSubscriptionPK,HistoryPK,Status,ClientSubscriptionPK,AgentPK,AgentTrxPercent,AgentTrxAmount,
EntryUsersID,EntryTime,LastUpdate)

select @MaxAgentSubsPK + ROW_NUMBER() OVER(ORDER BY @MaxClientSubscriptionPK ASC) AgentSubscriptionPK,1,2,@MaxClientSubscriptionPK,AgentPK,AgentTrxPercent,AgentTrxAmount,
[EntryUsersID],[EntryTime],@RevisedTime from AgentSubscription WHERE  ClientSubscriptionPK = @ClientSubscriptionPK


--AGENT FEE SUBSCRIPTION


Declare @MaxAgentFeeSubsPK int
Declare @AgentFeeSubscriptionPK int

select @AgentFeeSubscriptionPK = AgentFeeSubscriptionPK from AgentFeeSubscription where ClientSubscriptionPK = @ClientSubscriptionPK and status = 2

select @MaxAgentFeeSubsPK = Max(AgentFeeSubscriptionPK) from AgentFeeSubscription
set @MaxAgentFeeSubsPK = isnull(@MaxAgentFeeSubsPK,0)

update AgentFeeSubscription set status = 3 where  ClientSubscriptionPK = @ClientSubscriptionPK 

Insert into AgentFeeSubscription (AgentFeeSubscriptionPK,HistoryPK,Status,ClientSubscriptionPK,AgentPK,AgentFeePercent,AgentFeeAmount,
EntryUsersID,EntryTime,LastUpdate)

select @MaxAgentFeeSubsPK + ROW_NUMBER() OVER(ORDER BY @MaxClientSubscriptionPK ASC) AgentFeeSubscriptionPK,1,2,@MaxClientSubscriptionPK,AgentPK,AgentFeePercent,AgentFeeAmount,
EntryUsersID,EntryTime,@RevisedTime from AgentFeeSubscription WHERE  ClientSubscriptionPK = @ClientSubscriptionPK

END


                        
                        update ClientSubscription 
                        set RevisedBy = @RevisedBy,RevisedTime = @RevisedTime,Revised = 1 , status = 3 , IsFrontSync = 0
                        where ClientSubscriptionPK = @ClientSubscriptionPK and Status = 2 and posted = 1 
                        
                        Declare @counterDate datetime " +
                        "set @counterDate = @Date  " +
                        "while @counterDate < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK) " +
                        "BEGIN " +
                        "set @counterDate = dbo.fworkingday(@counterDate,1) \n " +
                        "update fundClientPosition set UnitAmount = UnitAmount - @UnitAmount,CashAmount = CashAmount - @CashAmount " +
                        "where FundClientPK = @FundClientPK and FundPK = @FundPK and Date = @counterDate end "
                        + @"
                        
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
                                    Declare @TransactionPK nvarchar(200)

                                    Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@oldNAV = NAV
                                    ,@TrxFrom = EntryUsersID, @TransactionPK = TransactionPK
                                    From ClientSubscription where ClientSubscriptionPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                     and ClientTransactionPK = @PK and TransactionType = 1 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 1
                                    )


				                   --set @OldSubsUnit = @OldUnitAmount
		                      

--                                    Declare @UnitPrevious numeric(22,8)
--                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
--                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

--                                    set @UnitPrevious = isnull(@UnitPrevious,0)

--                                   update fundclientpositionsummary
--		                           set Unit = Unit + @OldSubsUnit
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
--                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldSubsUnit,@UnitPrevious + @OldSubsUnit
--                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,1,'Revise Subscription Old Data Revise',@OldFundPK
                        


                        ";

                        cmd.Parameters.AddWithValue("@Date", _clientSubscription.ValueDate);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientSubscription.TotalCashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientSubscription.TotalUnitAmount);
                        cmd.Parameters.AddWithValue("@RevisedBy", _clientSubscription.RevisedBy);
                        cmd.Parameters.AddWithValue("@RevisedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _clientSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientSubscription.HistoryPK);
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
        
        public List<UnitRegistryRpt> Get_PKFromUnitRegistry(string _batchType, DateTime _date, int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UnitRegistryRpt> L_UnitRegistryRpt = new List<UnitRegistryRpt>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _statusUnitRegistry = "";
                        if (_status == 1)
                        {
                            _statusUnitRegistry = "  Status = 2 and Posted = 1 and Revised = 0 ";
                        }
                        else if (_status == 2)
                        {
                            _statusUnitRegistry = "  Status = 2 and Posted = 1 and Revised = 1 ";
                        }
                        else if (_status == 3)
                        {
                            _statusUnitRegistry = "  Status = 2 and Posted = 0 and Revised = 0 ";
                        }
                        else if (_status == 4)
                        {
                            _statusUnitRegistry = "  Status = 1  ";
                        }
                        else if (_status == 5)
                        {
                            _statusUnitRegistry = "  Status = 3  ";
                        }
                        else if (_status == 6)
                        {
                            _statusUnitRegistry = "  (Status = 2 or Posted = 1) and Revised = 0  ";
                        }
                        else if (_status == 7)
                        {
                            _statusUnitRegistry = "  (Status = 1 Or Status = 2 or Posted = 1) and  Revised = 0  ";
                        }

                        cmd.CommandText = "SELECT  Client" + _batchType + "PK as UnitRegistryPK FROM Client" + _batchType + " where " + _statusUnitRegistry + " and ValueDate = @Date order by Client" + _batchType + "PK ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    UnitRegistryRpt M_UnitRegistryRpt = new UnitRegistryRpt();
                                    M_UnitRegistryRpt.UnitRegistryPK = Convert.ToInt32(dr["UnitRegistryPK"]);
                                    L_UnitRegistryRpt.Add(M_UnitRegistryRpt);
                                }
                            }
                            return L_UnitRegistryRpt;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Validate_AddClientSubscription(int _fundPK, int _fundClientPK, decimal _cashAmount)
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
                            select * from ClientSubscription where FundClientPK=@FundClientPK and FundPK=@FundPK and CashAmount = @CashAmount
                            and ValueDate between CAST(GETDATE() AS DATE) and CAST(GETDATE() AS DATE)
                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@CashAmount", _cashAmount);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["ClientSubscriptionPK"]);
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

        public void ClientSubscription_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'ClientSubscription',ClientSubscriptionPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSubscriptionSelected + @"
  
                            update C set C.status = 2 from ClientSubscription A
                            left join ModoitTransaction B on A.TransactionPK = B.TransactionPK
                            left join FundClientVerification C on B.MasterTransactionPK = C.TransactionID
                            where A.ClientSubscriptionPK in 
                            (
                            Select ClientSubscriptionPK from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSubscriptionSelected + @"
                            )
                            and A.EntryUsersID = 'RDO' and C.status = 1                              


                            update ClientSubscription set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time where status = 1 and ClientSubscriptionPK in ( Select ClientSubscriptionPK from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSubscriptionSelected + @" ) 
                            Update ClientSubscription set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where status = 4 and ClientSubscriptionPK in (Select ClientSubscriptionPK from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 4 and" + paramClientSubscriptionSelected + @") ";

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

        public void ClientSubscription_UnApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'ClientSubscription',ClientSubscriptionPK,1,'UnApprove by Selected Data',@UsersID,@IPAddress,@Time  from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 2 and " + paramClientSubscriptionSelected + @" and Posted = 0 
                                 update ClientSubscription set status = 1,UpdateUsersID = @UsersID,UpdateTime = @Time,LastUpdate=@Time, Notes = 'Unapproved by selected' where status = 2 and ValueDate between @DateFrom and @DateTo and " + paramClientSubscriptionSelected;

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

        public void ClientSubscription_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'ClientSubscription',ClientSubscriptionPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 1 and" + paramClientSubscriptionSelected + @" 
                                          update ClientSubscription set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 1 and ClientSubscriptionPK in ( Select ClientSubscriptionPK from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 1 and " + paramClientSubscriptionSelected + @"  ) 
                                          Update ClientSubscription set status= 2  where status = 4 and ClientSubscriptionPK in (Select ClientSubscriptionPK from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 4 and" + paramClientSubscriptionSelected + @" ) ";

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

        public void ClientSubscription_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = " And ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = " And ClientSubscriptionPK in (0) ";
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
                            _parambitManageUR = @"Where A.status = 2 and A.Posted = 0 and A.ClientSubscriptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 1 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = "Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto  " + paramClientSubscriptionSelected;
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        Declare @CClientSubscriptionPK int
                        Declare @CNAVDate datetime
                        Declare @CTotalUnitAmount numeric(18,8)
                        Declare @CUnitAmount numeric(18,8)
                        Declare @CTotalCashAmount numeric(22,4)
                        Declare @CCashAmount numeric(18,4)
                        Declare @CAgentFeeAmount numeric(18,4)
                        Declare @CSubscriptionFeeAmount numeric(18,4)
                        Declare @CFundClientPK int
                        Declare @CFundClientID nvarchar(100) 
                        Declare @CFundClientName nvarchar(300) 
                        Declare @CFundCashRefPK	int
                        Declare @CFundPK int
                        Declare @Type int
                        Declare @COtherAmount numeric(18,4)
                        Declare @FundType int
                        

                        Declare @TotalFeeAmount numeric(18,4)
                        Declare @TrxFrom nvarchar(200)
                        Declare @TransactionPK nvarchar(200)

                        Declare @SubscriptionAcc int Declare @PayableSubsAcc int Declare  @PendingSubscription int
                        Declare @PeriodPK int Declare @BankPK int Declare @BankCurrencyPK int Declare @OtherReceivable int
                        Declare @FundJournalPK int 
                        Declare @IssueDate datetime

                        

                        DECLARE A CURSOR FOR 
	                        Select FundPK,ClientSubscriptionPK,NAVDate,isnull(TotalUnitAmount,0),isnull(TotalCashAmount,0),isnull(UnitAmount,0),isnull(CashAmount,0),
	                        isnull(AgentFeeAmount,0),isnull(SubscriptionFeeAmount,0),
	                        A.FundClientPK,B.ID,B.Name,CashRefPK, A.EntryUsersID,A.TransactionPK, A.Type,isnull(OtherAmount,0)
	 
	                        From ClientSubscription A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            " + _parambitManageUR + @"
	
                        Open A
                        Fetch Next From A
                        Into @CFundPK,@CClientSubscriptionPK,@CNAVDate,@CTotalUnitAmount,@CTotalCashAmount,@CUnitAmount,@CCashAmount,@CAgentFeeAmount,@CSubscriptionFeeAmount,
	                            @CFundClientPK,@CFundClientID,@CFundClientName,@CFundCashRefPK,@TrxFrom,@TransactionPK,@Type,@COtherAmount

                        While @@FETCH_STATUS = 0
                        Begin
        
                            Select @SubscriptionAcc = Subscription,@PendingSubscription = PendingSubscription,@PayableSubsAcc = payablesubscriptionfee,@OtherReceivable = OtherReceivable From Fundaccountingsetup where status = 2   
                            and fundPK = @CFundPK

                            select @IssueDate = IssueDate,@FundType = Type from Fund where status in (1,2) and FundPK = @CFundPK

                            IF (@CNAVDate <> @IssueDate)
                            BEGIN

	                        -- LOGIC INSERT KE JOURNAL
		                        If @CTotalCashAmount > 0 and @Type not in(3,6)
		                        Begin
                                    DECLARE @BitPendingSubscription bit
                                    SELECT @BitPendingSubscription = ISNULL(BitPendingSubscription,1) FROM dbo.FundFee WHERE fundPK = @CFundPK
                                    AND date = 
                                    (
	                                    SELECT MAX(Date) FROM dbo.FundFee WHERE Date <= @CNAVDate AND FundPK = @CFundPK
	                                    AND status = 2
                                    ) and STATUS = 2
                                    IF(@BitPendingSubscription = 1)
                                    BEGIN
			                            Select @SubscriptionAcc = Subscription,@PendingSubscription = PendingSubscription,@PayableSubsAcc = payablesubscriptionfee 
                                        From Fundaccountingsetup where status = 2   and FundPK = @CFundPK
			                        END
                                    ELSE
                                    BEGIN
                                            Select @SubscriptionAcc = Subscription,@PayableSubsAcc = payablesubscriptionfee From Fundaccountingsetup where status = 2   
                                        and FundPK = @CFundPK
                                    END

                                        Select @FundJournalPK = isnull(MAX(FundJournalPK) +  1,1) From FundJournal       
			                            Select @TotalFeeAmount = @CSubscriptionFeeAmount
		   
			                            select @BankPK = isnull(FundJournalAccountPK,3),@BankCurrencyPK = A.CurrencyPK from FundCashRef A  where A.FundCashRefPK = @CFundCashRefPK  and A.status = 2       
                                        	
                                    IF (isnull(@BankPK,0) = 0)
                                    BEGIN
                                        select @BankPK = 3
                                    END
                                    ELSE
                                    BEGIN
                                        select @BankPK = isnull(@BankPK,3)
                                    END	

                                    IF(@BitPendingSubscription = 1)
	                                BEGIN
		                                set @BankPK = @PendingSubscription
	                                END	
	                            
                                    Select @PeriodPK = PeriodPK From Period Where @DateFrom Between DateFrom and DateTo and Status = 2      
			
			                        IF (@FundType = 10) -- ETF
			                        BEGIN
				                        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			                            Select	    @FundJournalPK, 1,2,'Posting From Subscription',@PeriodPK,@CNAVDate,2,@CClientSubscriptionPK,'SUBSCRIPTION', '','Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
			                            INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
			                            Select		@FundJournalPK,1,1,2,@BankPK,1,@CFundPK,@CFundClientPK,'Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,'D',@CTotalCashAmount,@CTotalCashAmount,0,1,@CTotalCashAmount,0,@PostedTime 
				                        INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
			                            Select		@FundJournalPK,2,1,2,@OtherReceivable,1,@CFundPK,@CFundClientPK,'Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,'D',@COtherAmount,@COtherAmount,0,1,@COtherAmount,0,@PostedTime 
			                            INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			                            Select		@FundJournalPK,3,1,2,@SubscriptionAcc,1,@CFundPK,@CFundClientPK,'Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@CTotalCashAmount + @COtherAmount,	0,@CTotalCashAmount + @COtherAmount,1,0,@CTotalCashAmount + @COtherAmount,@PostedTime 
			                        END
			                        ELSE
			                        BEGIN
				                        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			                            Select	   @FundJournalPK, 1,2,'Posting From Subscription',@PeriodPK,dbo.fworkingday(@CNAVDate,1),2,@CClientSubscriptionPK,'SUBSCRIPTION', '','Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
			                            INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
			                            Select		@FundJournalPK,1,1,2,@BankPK,1,@CFundPK,@CFundClientPK,'Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,'D',@CTotalCashAmount,@CTotalCashAmount,0,1,@CTotalCashAmount,0,@PostedTime 
			                            INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			                            Select		@FundJournalPK,2,1,2,@SubscriptionAcc,1,@CFundPK,@CFundClientPK,'Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@CTotalCashAmount,	0,@CTotalCashAmount,1,0,@CTotalCashAmount,@PostedTime 
			                        END

			    
		                        End
                            END
	
	                        -- LOGIC FUND CLIENT POSITION
	                        if Exists(select top 1 * from FundClientPosition    
	                        where Date = @CNavDate and FundClientPK = @CFundClientPK and FundPK = @CFundPK)    
	                        begin    
		                        Update FundClientPosition set CashAmount = CashAmount  + @CCashAmount,    
		                        UnitAmount = UnitAmount + @CTotalUnitAmount where Date = @CNavDate and FundClientPK = @CFundClientPK    
		                        and FundPK = @CFundPK    
	                        end    
	                        else    
	                        begin    
	   
			                        INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)    
			                        select @CNavDate,@CFundClientPK,@CFundPK,@CCashAmount,@CTotalUnitAmount  
		
	                        end      

	                        update clientsubscription    
	                        set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime    
	                        where ClientSubscriptionPK = @CClientSubscriptionPK and Status = 2    

	   
	                        Declare @counterDate datetime      
	                        set @counterDate = @CNavDate      
	                        while @counterDate < (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails Where status = 2)    
	                        BEGIN    
		                        set @counterDate = dbo.fworkingday(@counterDate,1)      
		                        if Exists(select top 1 * from FundClientPosition    
		                        where Date = @counterDate and FundClientPK = @CFundClientPK and FundPK = @CFundPK)    
		                        BEGIN
			                        update fundClientPosition set UnitAmount = UnitAmount  + @CTotalUnitAmount,CashAmount = CashAmount + @CCashAmount    
			                        where FundClientPK = @CFundClientPK and FundPK = @CFundPK and Date = @counterDate 
		                        END
		                        ELSE
		                        BEGIN
		                        INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)    
			                        select @counterDate,@CFundClientPK,@CFundPK,@CCashAmount,@CTotalUnitAmount 
		                        END
	                        END    
	   
	
                        Fetch next From A 
                        Into @CFundPK,@CClientSubscriptionPK,@CNAVDate,@CTotalUnitAmount,@CTotalCashAmount,@CUnitAmount,@CCashAmount,@CAgentFeeAmount,@CSubscriptionFeeAmount,
	                            @CFundClientPK,@CFundClientID,@CFundClientName,@CFundCashRefPK,@TrxFrom,@TransactionPK,@Type,@COtherAmount
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
        
        public void ClientSubscription_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
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
                                 Select @Time,@PermissionID,'ClientSubscription',ClientSubscriptionPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 2 and" + paramClientSubscriptionSelected + @" and Posted = 0 
                                 update ClientSubscription set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 2 and ClientSubscriptionPK in ( Select ClientSubscriptionPK from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 2 and" + paramClientSubscriptionSelected + @"  and Posted = 0) ";

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
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Exists(select * From ClientSubscription where Status = 1 and Type not in(3,6) and ValueDate between @ValueDateFrom and @ValueDateTo and " + paramClientSubscriptionSelected + @" and Posted = 0 and Revised = 0 and isnull(Nav,0) = 0 ) " +
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

        public bool Validate_UnApproveBySelected(DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Exists(select * From ClientSubscription where (Status = 2 and Posted = 1 and Revised = 0) and ValueDate between @ValueDateFrom and @ValueDateTo and " + paramClientSubscriptionSelected + @") " +
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


                        cmd.CommandText = @" Declare @NAVDate datetime
                                            Declare A Cursor For              
                                             Select distinct NAVDate From ClientSubscription Where status = 2 and Valuedate = @ValueDateFrom
                                            Open  A              
              
                                            Fetch Next From  A              
                                            into @NAVDate
                  
                                            While @@Fetch_Status = 0              
                                            BEGIN              
		                                            if not exists(Select * from EndDayTrails where status = 2 and Notes = 'Unit' and Valuedate = dbo.FWorkingDay(@NAVDate,-1))
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

        public int Validate_PostingNAVBySelected(DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" if Exists(select * From ClientSubscription where Status = 2 and Type not in(3,6,7) and ValueDate between @ValueDateFrom and @ValueDateTo and " + paramClientSubscriptionSelected + @" and Posted = 0 and Revised = 0 and isnull(Nav,0) = 0 ) " +
                        " BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

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
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Exists(select * From ClientSubscription where (Status = 2 and Posted = 1 and Revised = 0) and ValueDate between @ValueDateFrom and @ValueDateTo and  " + paramClientSubscriptionSelected + @") " +
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

        public Boolean ClientSubscriptionBatchForm(string _userID, ClientSubscription _clientSubscription)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" SELECT CU.ID CurrencyID,'Subscription' Type,ClientSubscriptionPK,BC.ContactPerson ContactPerson,
                                 BN.ID + ' - ' + BC.ID BankCustodiID,BC.Fax1 FaxNo,ValueDate Date,NavDate NavDate, 
                                 F.Name FundName,FC.id ClientID, Fc.Name ClientName,CS.NAV NAVAmount,CashAmount,unitamount UnitAmount, 
                                 CS.SubscriptionFeePercent FeePercent, CS.Description Remark,CS.TotalCashAmount NetAmount,
                                 0 SrHolder from ClientSubscription CS   
                                 left join FundClient FC ON CS.fundclientpk = FC.fundclientpk and FC.status = 2  
                                 left join Fund F ON CS.FundPK = F.FundPK  and F.status = 2   
                                 left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                                 left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                                 left join Currency CU ON CU.CurrencyPK = CS.CurrencyPK  and CU.status = 2  
                                 WHERE  CS.Status = @Status and CS.FundPK = @FundPK and CS.NAVDate = @NAVDate and CS.ClientSubscriptionPK = @ClientSubscriptionPK ";


                        cmd.Parameters.AddWithValue("@Status", _clientSubscription.Status);
                        cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientSubscription.NAVDate);
                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _clientSubscription.ClientSubscriptionPK);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "BatchFormSUBInstruction_" + _clientSubscription.NAVDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormSUBInstruction_" + _clientSubscription.NAVDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form SUB Instruction");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ClientSubscriptionPK = Convert.ToInt32(dr0["ClientSubscriptionPK"]);
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
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.FundName, r.Date, r.SettlementDate, r.ContactPerson, r.FaxNo, r.BankCustodiID } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;

                                    worksheet.Cells[incRowExcel, 1].Value = "SUBSCRIPTION BATCH FORM";
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

                                        worksheet.Cells[incRowExcel, 1].Value = "Please kindly receive our Subscription of unit holders as follow : ";
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
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



                                        worksheet.Cells[incRowExcel, 5].Value = "NAV Date";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "Gross IDR";
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

                                        worksheet.Cells[incRowExcel, 9].Value = "Net IDR";
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
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.SrHolder;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVDate;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.CashBalance;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 8].Formula = "F" + RowD + "*G" + RowD + " /100";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.NetAmount;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Remark;
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }
                                        worksheet.Cells["A" + _endRowDetail + ":K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 5].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 7;
                                        worksheet.Cells[incRowExcel, 1].Value = "Sutejo";
                                        worksheet.Cells[incRowExcel, 6].Value = "Fani Haryanto";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Admin & Settlement";
                                        worksheet.Cells[incRowExcel, 6].Value = "Head of FA & Settlement";
                                        incRowExcel++;

                                        worksheet.Row(incRowExcel).PageBreak = true;




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
                                    worksheet.Column(2).Width = 15;
                                    worksheet.Column(3).Width = 2;
                                    worksheet.Column(4).Width = 60;
                                    worksheet.Column(5).Width = 25;
                                    worksheet.Column(6).Width = 35;
                                    worksheet.Column(7).Width = 30;
                                    worksheet.Column(8).Width = 30;
                                    worksheet.Column(9).Width = 2;
                                    worksheet.Column(10).Width = 35;
                                    worksheet.Column(11).Width = 30;



                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&20 PT RHB Asset Management Indonesia - Subscription Batch";



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

        public bool Validate_DormantClientSubscription(int _fundClientPK)
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


        /* 10 AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart liad folder customize per Client ) */
        // Radsoft
        public string SInvestSubscriptionRpt_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {

            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
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
                                    + '|' + case when A.CashAmount = 0 then '' else cast(isnull(cast(A.CashAmount as decimal(22,2)),'')as nvarchar) end -- TotalCashAmount
                                    + '|' + case when A.TotalUnitAmount = 0 then '' else cast(isnull(Round(A.TotalUnitAmount,4),'')as nvarchar) end
                                    + '|' + 
                                    + '|' + case when A.FeeAmount = 0 then '' else cast(isnull(cast(A.FeeAmount as decimal(22,2)),'')as nvarchar) end -- 0
                                    + '|' + 
                                    + '|' + 
                                    + '|' + 
                                    + '|' + 
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BICode,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.AccNo,''))))
                                    + '|' +        
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                                    + '|' + RTRIM(LTRIM(isnull(A.Reference,'')))
                                    from (      
                                    Select CS.ValueDate,F.SInvestCode, '' SettlementDate,CS.SubscriptionFeePercent FeePercent,CS.SubscriptionFeeAmount FeeAmount,'1' Type,
                                    ROUND(CashAmount,2)CashAmount ,ROUND(TotalUnitAmount,4)TotalUnitAmount ,'' BICode, '' AccNo ,'' TransferType, 
                                    Case When ReferenceSInvest <> '' and ReferenceSInvest is not null then ReferenceSInvest else cast(ClientSubscriptionPK as nvarchar) end Reference
                                    ,FC.IFUACode  
                                    from ClientSubscription CS 
                                    left join Fund F on Cs.FundPK = F.fundPK and f.Status in (1,2)     
                                    left join FundClient FC on CS.FundClientPK = FC.FundClientPK and fc.Status in (1,2)
                                    where    
                                    ValueDate =  @ValueDate and " + paramClientSubscriptionSelected + @" and Cs.status = 2
                                    )A    
                                    Group by A.ValueDate,A.SInvestCode,A.FeePercent,A.BICode,A.AccNo,A.SettlementDate,A.FeeAmount,A.Type,A.CashAmount,A.TotalUnitAmount,A.TransferType,A.Reference,A.IFUACode
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
        
        // RHB
        public Boolean RegularForm(string _userID, DateTime _DateFrom, DateTime _DateTo, ClientSubscription _clientSubscription)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =

                            @"BEGIN    
                            SELECT 'Subscription' Type,ClientSubscriptionPK,D.ContactPerson ContactPerson,D.ID BankBranchID,D.Fax1 FaxNo,ValueDate Date,NavDate NavDate, C.Name FundName,
                            B.ID ClientID, B.Name ClientName,A.NAV NAVAmount,CashAmount,unitamount UnitAmount, A.SubscriptionFeePercent FeePercent,A.SubscriptionFeeAmount FeeAmount, 
                            A.NAVDate SettlementDate,A.Description Remark,A.TotalCashAmount NetAmount,A.SRP_INFO RekNo,isnull(A.AutoDebitDate,0) AutoDebitDate from ClientSubscription A    
                            left join FundClient B ON B.fundclientpk = A.fundclientpk and B.status = 2   
                            left join Fund C ON A.FundPK = C.FundPK  and C.status = 2     
                            left join BankBranch D ON C.BankBranchPK = D.BankBranchPK  and D.status = 2    
                            WHERE  A.Status  not in (3,4) and NAVDate between @DateFrom and @DateTo and A.type = 2  and  A.Selected = 1
                            END  ";
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "RegularForm_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "RegularForm_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Regular Form");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ClientSubscriptionPK = Convert.ToInt32(dr0["ClientSubscriptionPK"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                        rSingle.Nav = Convert.ToDecimal(dr0["NAVAmount"]);
                                        rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                        rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                        rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                        rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                        rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                        rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                        rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                        rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                        rSingle.BankCustodiID = Convert.ToString(dr0["BankBranchID"]);
                                        rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                        rSingle.RekNo = Convert.ToString(dr0["RekNo"]);
                                        rSingle.AutoDebitDate = Convert.ToDateTime(dr0["AutoDebitDate"]);
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                          from r in rList
                                          group r by new { r.FundName, r.Date, r.SettlementDate, r.ContactPerson, r.FaxNo, r.BankCustodiID, r.AutoDebitDate } into rGroup
                                          select rGroup;

                                    int incRowExcel = 1;

                                    //worksheet.Cells[incRowExcel, 1].Value = "SUBSCRIPTION REGULAR BATCH FORM";
                                    //worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                    //worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    //worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Font.Size = 28;
                                    //worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Font.Bold = true;



                                    //else if (_unitRegistryRpt.Type == "Redemption")
                                    //{
                                    //    worksheet.Cells[incRowExcel, 1].Value = "REDEMPTION BATCH FORM";
                                    //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                    //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    //    worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Font.Size = 28;

                                    //}



                                    foreach (var rsHeader in QueryByFundID)
                                    {




                                        incRowExcel = incRowExcel + 2;

                                        int RowZ = incRowExcel;
                                        incRowExcel = incRowExcel + 4;
                                        worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Value = "Date ";
                                        worksheet.Cells[incRowExcel, 10].Value = ": ";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.Date;
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Wisma Mulia, 19th Floor";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 9].Value = "To ";
                                        worksheet.Cells[incRowExcel, 10].Value = ": ";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.BankCustodiID;
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jl. Jend. Gatot Subroto No. 42";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 9].Value = "Attention ";
                                        worksheet.Cells[incRowExcel, 10].Value = ": ";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ContactPerson;
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jakarta 12710, Indonesia";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 9].Value = "Fax Number ";
                                        worksheet.Cells[incRowExcel, 10].Value = ": ";
                                        worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.FaxNo;
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
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

                                        worksheet.Cells[incRowExcel, 1].Value = "Please kindly receive our Subscription of unit holders as follow : ";
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                        worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Style.Font.Bold = true;
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Date For AutoDebit ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.AutoDebitDate;
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                        incRowExcel = incRowExcel + 2;


                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;


                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "Client";
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



                                        worksheet.Cells[incRowExcel, 5].Value = "NAV Date";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "Gross IDR";
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

                                        worksheet.Cells[incRowExcel, 9].Value = "Net IDR";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Merge = true;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["I" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 11].Value = "Remark";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        worksheet.Cells[incRowExcel, 12].Value = "Account";
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 12].Value = "No";
                                        worksheet.Cells[RowG, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[RowG, 12].Style.Font.Bold = true;

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


                                            //worksheet.Cells["A" + RowD + ":L" + RowD].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowD + ":L" + RowD].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVDate;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.CashBalance;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeeAmount;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.NetAmount;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Remark;
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.RekNo;
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }

                                        worksheet.Cells["A" + _endRowDetail + ":L" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 5].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 7;
                                        worksheet.Cells[incRowExcel, 1].Value = _clientSubscription.Signature1Desc;
                                        worksheet.Cells[incRowExcel, 6].Value = _clientSubscription.Signature2Desc;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Admin & Settlement";
                                        worksheet.Cells[incRowExcel, 6].Value = "Head of FA & Settlement";
                                        incRowExcel++;

                                        worksheet.Row(incRowExcel).PageBreak = true;
                                    }

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
                                    worksheet.Column(1).Width = 10;
                                    worksheet.Column(2).Width = 35;
                                    worksheet.Column(3).Width = 2;
                                    worksheet.Column(4).Width = 100;
                                    worksheet.Column(5).Width = 30;
                                    worksheet.Column(6).Width = 40;
                                    worksheet.Column(7).Width = 35;
                                    worksheet.Column(8).Width = 35;
                                    worksheet.Column(9).Width = 35;
                                    worksheet.Column(10).Width = 2;
                                    worksheet.Column(11).Width = 40;
                                    worksheet.Column(12).Width = 45;




                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION REGULAR \n &28&B Batch Form";



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
        public Boolean ClientSubscriptionBatchFormBySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientSubscription _clientSubscription)
        {

            try
            {

                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_clientSubscription.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientSubscription.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _clientSubscription.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" SELECT CU.ID CurrencyID,'Subscription' Type,ClientSubscriptionPK,
                            BC.ContactPerson ContactPerson,BN.ID + ' - ' + BC.ID BankCustodiID,
                            BC.Fax1 FaxNo,ValueDate Date,NavDate NavDate, 
                            F.Name FundName,FC.id ClientID, Fc.Name ClientName,
                            CS.NAV NAVAmount,CashAmount,unitamount UnitAmount, 
                            CS.SubscriptionFeePercent FeePercent,CS.SubscriptionFeeAmount FeeAmount, 
                            CS.NAVDate SettlementDate,CS.Description Remark,CS.TotalCashAmount NetAmount,
                            0 SrHolder from ClientSubscription CS   
                            left join FundClient FC ON CS.fundclientpk = FC.fundclientpk and FC.status = 2  
                            left join Fund F ON CS.FundPK = F.FundPK  and F.status = 2   
                            left join FundCashRef FCR ON CS.CashRefPK = FCR.FundCashRefPK  and FCR.status = 2   
                            left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                            left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                            left join Currency CU ON CU.CurrencyPK = CS.CurrencyPK  and CU.status = 2  
                            WHERE CS.NAVDate between @DateFrom and @DateTo and " + paramClientSubscriptionSelected + @" and CS.Type <> 3 and CS.status not in(3,4)
                        ";


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
                                string filePath = Tools.ReportsPath + "BatchFormSUBInstructionBySelected_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormSUBInstructionBySelected_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form SUB Instruction");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                    while (dr0.Read())
                                    {
                                        UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                        rSingle.ClientSubscriptionPK = Convert.ToInt32(dr0["ClientSubscriptionPK"]);
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
                                        rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.FundName, r.Date, r.SettlementDate, r.ContactPerson, r.FaxNo, r.BankCustodiID } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;

                                    foreach (var rsHeader in QueryByFundID)
                                    {




                                        incRowExcel = incRowExcel + 2;


                                        int RowZ = incRowExcel;
                                        incRowExcel = incRowExcel + 4;
                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
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
                                        worksheet.Row(incRowExcel).Height = 100;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyAddress();
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.WrapText = true;
                                        //worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;

                                        worksheet.Cells[incRowExcel, 8].Value = "To ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.BankCustodiID;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Phone No";
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyPhone();
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 8].Value = "Attention ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.ContactPerson;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Fax No";
                                        worksheet.Cells[incRowExcel, 3].Value = ": ";
                                        worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyFax();
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 8].Value = "Fax Number ";
                                        worksheet.Cells[incRowExcel, 9].Value = ": ";
                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.FaxNo;
                                        worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;



                                        incRowExcel++;


                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Please kindly receive our Subscription of unit holders as follow : ";
                                        worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = ":";
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
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



                                        worksheet.Cells[incRowExcel, 5].Value = "NAV Date";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "Gross IDR";
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

                                        worksheet.Cells[incRowExcel, 9].Value = "Net IDR";
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


                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.SrHolder;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVDate;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.CashBalance;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.FeePercent;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeeAmount;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.NetAmount;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Remark;
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }
                                        worksheet.Cells["A" + _endRowDetail + ":K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 5].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 7;
                                        int _RowA = incRowExcel;
                                        int _RowB = incRowExcel + 1;

                                        if (_clientSubscription.Signature1 != 1)
                                        {

                                            worksheet.Cells[_RowA, 3].Value = _host.Get_SignatureName(_clientSubscription.Signature1);
                                            worksheet.Cells["C" + _RowA + ":D" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 3].Value = _host.Get_PositionSignature(_clientSubscription.Signature1);
                                            worksheet.Cells["C" + _RowB + ":D" + _RowB].Merge = true;
                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 3].Value = "";
                                            worksheet.Cells["C" + _RowA + ":D" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 3].Value = "";
                                            worksheet.Cells["C" + _RowB + ":D" + _RowB].Merge = true;
                                        }


                                        if (_clientSubscription.Signature2 != 1)
                                        {
                                            worksheet.Cells[_RowA, 5].Value = _host.Get_SignatureName(_clientSubscription.Signature2);
                                            worksheet.Cells["E" + _RowA + ":F" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 5].Value = _host.Get_PositionSignature(_clientSubscription.Signature2);
                                            worksheet.Cells["E" + _RowB + ":F" + _RowB].Merge = true;


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 5].Value = "";
                                            worksheet.Cells["E" + _RowA + ":F" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 5].Value = "";
                                            worksheet.Cells["E" + _RowB + ":F" + _RowB].Merge = true;
                                        }

                                        if (_clientSubscription.Signature3 != 1)
                                        {
                                            worksheet.Cells[_RowA, 7].Value = _host.Get_SignatureName(_clientSubscription.Signature3);
                                            worksheet.Cells["G" + _RowA + ":H" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 7].Value = _host.Get_PositionSignature(_clientSubscription.Signature3);
                                            worksheet.Cells["G" + _RowB + ":H" + _RowB].Merge = true;


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 7].Value = "";
                                            worksheet.Cells["G" + _RowA + ":H" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 7].Value = "";
                                            worksheet.Cells["G" + _RowB + ":H" + _RowB].Merge = true;
                                        }

                                        if (_clientSubscription.Signature4 != 1)
                                        {
                                            worksheet.Cells[_RowA, 9].Value = _host.Get_SignatureName(_clientSubscription.Signature4);
                                            worksheet.Cells["I" + _RowA + ":J" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 9].Value = _host.Get_PositionSignature(_clientSubscription.Signature4);
                                            worksheet.Cells["I" + _RowB + ":J" + _RowB].Merge = true;
                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 9].Value = "";
                                            worksheet.Cells["I" + _RowA + ":J" + _RowA].Merge = true;
                                            worksheet.Cells[_RowB, 9].Value = "";
                                            worksheet.Cells["I" + _RowB + ":J" + _RowB].Merge = true;
                                        }




                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }
                                    //incRowExcel++;
                                    int _lastRow = incRowExcel;

                                    string _rangeA = "A5:K" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 28;
                                    }

                                    worksheet.DeleteRow(_lastRow);
                                    //worksheet.DeleteRow(_lastRow + 1);
                                    //worksheet.DeleteRow(_lastRow + 2);
                                    //worksheet.DeleteRow(_lastRow + 3);

                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                    worksheet.Column(1).Width = 10;
                                    worksheet.Column(2).Width = 15;
                                    worksheet.Column(3).Width = 2;
                                    worksheet.Column(4).Width = 100;
                                    worksheet.Column(5).Width = 30;
                                    worksheet.Column(6).Width = 40;
                                    worksheet.Column(7).Width = 35;
                                    worksheet.Column(8).Width = 35;
                                    worksheet.Column(9).Width = 2;
                                    worksheet.Column(10).Width = 40;
                                    worksheet.Column(11).Width = 35;

                                    //worksheet.Column(1).AutoFit();
                                    //worksheet.Column(2).AutoFit();
                                    //worksheet.Column(3).AutoFit();
                                    //worksheet.Column(4).AutoFit();
                                    //worksheet.Column(5).AutoFit();
                                    //worksheet.Column(6).AutoFit();
                                    //worksheet.Column(7).AutoFit();
                                    //worksheet.Column(8).AutoFit();
                                    //worksheet.Column(9).AutoFit();
                                    //worksheet.Column(10).AutoFit();
                                    //worksheet.Column(11).AutoFit();

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form \n";
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
                                    if (_clientSubscription.DownloadMode == "PDF")
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
        public Boolean ClientSubscriptionBatchFormBySelectedDataMandiri(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientSubscription _clientSubscription)
        {

            try
            {

                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_clientSubscription.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientSubscription.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _clientSubscription.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        SELECT F.Name FundName,Fc.Name InvestorName,
                        NavDate NavDate, ValueDate Date, FC.IFUACode IFUA, unitamount UnitTransaction,isnull(BN.Name,'') BankCustodianName,isnull(BC.Address,'') CustodianAddress,
                        CashAmount GrossAmount, CS.SubscriptionFeePercent FeePercent, CS.SubscriptionFeeAmount FeeAmount, 
                        CS.TotalCashAmount NetAmount, CS.Description Remark, CU.ID CurrencyID,
                        isnull(BN.ID,'') + ' - ' + isnull(BC.ID,'') BankCustodiID, isnull(BC.Address,'') Address, isnull(BC.Attn1,'') ContactPerson, isnull(BC.Fax1,'') FaxNo, isnull(BC.Address,'') CustodyAddress from ClientSubscription CS   
                        left join FundClient FC ON CS.fundclientpk = FC.fundclientpk and FC.status = 2  
                        left join Fund F ON CS.FundPK = F.FundPK  and F.status = 2   
                        left join FundCashRef FCR ON CS.CashRefPK = FCR.FundCashRefPK  and FCR.status = 2   
                        left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                        left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                        left join Currency CU ON CU.CurrencyPK = CS.CurrencyPK  and CU.status = 2   
                        WHERE --CS.NAVDate between @DateFrom and @DateTo and 
			" + paramClientSubscriptionSelected + @" and CS.Type = 1 and CS.status not in(3,4)

";

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
                                string filePath = Tools.ReportsPath + "BatchFormSUBInstructionBySelected_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormSUBInstructionBySelected_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form SUB Instruction");


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
                                        rList.Add(rSingle);

                                    }


                                    var QueryByFundID =
                                        from r in rList
                                        group r by new { r.NAVDate, r.Date, r.FundName, r.ContactPerson, r.FaxNo, r.BankCustodiID, r.CustodyAddress } into rGroup
                                        select rGroup;

                                    int incRowExcel = 0;
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

                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.BankCustodiID;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyAddress();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                        worksheet.Row(incRowExcel).Height = 60;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.CustodyAddress;
                                        worksheet.Cells[incRowExcel, 10].Style.WrapText = true;
                                        worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                        incRowExcel = incRowExcel + 3;
                                        worksheet.Cells[incRowExcel, 1].Value = "Batch Form-Subscription";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Value = "Attn : " + rsHeader.Key.ContactPerson;
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Value = "Fax No. : " + rsHeader.Key.ContactPerson;
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 3;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date : " + Convert.ToDateTime(rsHeader.Key.Date).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 10].Value = "Print Date : " + Convert.ToDateTime(rsHeader.Key.Date).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Nav Date : " + Convert.ToDateTime(rsHeader.Key.NAVDate).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 10].Value = "Time : " + Convert.ToDateTime(_dateTimeNow).ToString("HH:mm") + " WIB";
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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

                                        worksheet.Cells[incRowExcel, 10].Value = "Remark";
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                        worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 7].Value = "%";
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Value = "Amount";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells["A" + RowB + ":J" + RowG].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + RowB + ":J" + RowG].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

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
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.Remark;
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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


                                        worksheet.Cells["A" + _startRowDetail + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":J" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":J" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        incRowExcel = incRowExcel + 6;
                                        worksheet.Cells[incRowExcel, 9].Value = "Fund Manager Approval : ";
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 9].Value = "Date : " + Convert.ToDateTime(rsHeader.Key.Date).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 9].Value = "Checked By : ";
                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 10].Value = "Approve By : ";
                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        int _RowA = incRowExcel;
                                        int _RowB = incRowExcel + 5;
                                        incRowExcel = incRowExcel + 7;


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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 3, 10];
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(2).Width = 35;
                                    worksheet.Column(3).Width = 20;
                                    worksheet.Column(4).Width = 25;
                                    worksheet.Column(5).Width = 10;
                                    worksheet.Column(6).Width = 30;
                                    worksheet.Column(7).Width = 15;
                                    worksheet.Column(8).Width = 15;
                                    worksheet.Column(9).Width = 25;
                                    worksheet.Column(10).Width = 30;

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
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
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 RADSOFT";
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
        public ClientSubscriptionRecalculate ClientSubscription_Recalculate(ParamClientSubscriptionRecalculate _param)
        {
            try
            {

                //
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"

Declare @TypeCheck int
Select @TypeCheck = Type
From ClientSubscription where ClientSubscriptionPK = @ClientSubscriptionPK And Type in(3,6,7)

if @TypeCheck in (3,7)
BEGIN
    return
END



Declare @TotalCashAmount numeric(22,4)
Declare @CashAmount numeric(18,4)
Declare @UnitAmount numeric(22,8)
Declare @AgentFeePercent numeric(18,8)
Declare @SubsFeePercent numeric(18,8)
Declare @AgentFeeAmount numeric(18,4)
Declare @SubsFeeAmount numeric(18,4)
Declare @TotalUnitAmount numeric(22,8)
Declare @JournalRoundingMode int
Declare @NAVRoundingMode int 
Declare @JournalDecimalPlaces int 
Declare @NAVDecimalPlaces int
Declare @UnitDecimalPlaces int
Declare @Nav numeric(24,8)
Declare @UnitRoundingMode int
                        


Select  @JournalRoundingMode = isnull(C.JournalRoundingMode,3),
@JournalDecimalPlaces = ISNULL(C.JournalDecimalPlaces,4) 
from  Fund A 
Left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2
Left join Bank C on C.BankPK = B.BankPK and C.Status = 2 
where A.Status = 2

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


set @Cashamount = @paramCashAmount


if(@ClientCode = 14)
BEGIN

if @FeeType = 1 and @paramSubscriptionFeePercent > 0
BEGIN
	If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@Cashamount - (@Cashamount * @paramSubscriptionFeePercent/100),@JournalDecimalPlaces) 
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
	If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@Cashamount - (@Cashamount * @paramSubscriptionFeePercent/100),@JournalDecimalPlaces,1) END
	If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@Cashamount - (@Cashamount * @paramSubscriptionFeePercent/100),@JournalDecimalPlaces) END


	If @JournalRoundingMode = 1 BEGIN Set @SubsFeeAmount = round(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces) 
	IF @JournalDecimalPlaces = 0 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 1
	END
	IF @JournalDecimalPlaces = 2 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.01
	END
	IF @JournalDecimalPlaces = 4 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.0001
	END
	IF @JournalDecimalPlaces = 6 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.000001
	END
	IF @JournalDecimalPlaces = 8 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.00000001
	END
	END
	If @JournalRoundingMode = 2 BEGIN Set   @SubsFeeAmount = ROUND(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces,1) END
	If @JournalRoundingMode = 3 BEGIN Set   @SubsFeeAmount = ROUND(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces) END
	set @SubsFeePercent = @paramSubscriptionFeePercent
END

if @FeeType = 2 and @paramSubscriptionFeeAmount > 0
BEGIN
	set @SubsFeeAmount = @paramSubscriptionFeeAmount
	set @TotalCashAmount = @Cashamount - @SubsFeeAmount
	set @SubsFeePercent = (@SubsFeeAmount / @TotalCashAmount) * 100
	
END

END
ELSE
BEGIN

if @FeeType = 1 and @paramSubscriptionFeePercent > 0
BEGIN
	If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@Cashamount / (1 + @paramSubscriptionFeePercent/100),@JournalDecimalPlaces) 
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
	If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@Cashamount / (1 + @paramSubscriptionFeePercent/100),@JournalDecimalPlaces,1) END
	If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@Cashamount / (1 + @paramSubscriptionFeePercent/100),@JournalDecimalPlaces) END


	If @JournalRoundingMode = 1 BEGIN Set @SubsFeeAmount = round(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces) 
	IF @JournalDecimalPlaces = 0 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 1
	END
	IF @JournalDecimalPlaces = 2 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.01
	END
	IF @JournalDecimalPlaces = 4 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.0001
	END
	IF @JournalDecimalPlaces = 6 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.000001
	END
	IF @JournalDecimalPlaces = 8 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.00000001
	END
	END
	If @JournalRoundingMode = 2 BEGIN Set   @SubsFeeAmount = ROUND(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces,1) END
	If @JournalRoundingMode = 3 BEGIN Set   @SubsFeeAmount = ROUND(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces) END
	set @SubsFeePercent = @paramSubscriptionFeePercent
END

if @FeeType = 2 and @paramSubscriptionFeeAmount > 0
BEGIN
	set @SubsFeeAmount = @paramSubscriptionFeeAmount
	set @TotalCashAmount = @Cashamount - @SubsFeeAmount
	set @SubsFeePercent = (@SubsFeeAmount / @TotalCashAmount) * 100
	
END

END


if @FeeType = 1 and @paramAgentFeePercent > 0
BEGIN
		If @JournalRoundingMode = 1 BEGIN Set @AgentFeeAmount = round(@paramAgentFeePercent / 100 * @SubsFeeAmount,@JournalDecimalPlaces) 
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
		If @JournalRoundingMode = 2 BEGIN Set   @AgentFeeAmount = ROUND(@paramAgentFeePercent / 100 * @SubsFeeAmount,@JournalDecimalPlaces,1) END
		If @JournalRoundingMode = 3 BEGIN Set   @AgentFeeAmount = ROUND(@paramAgentFeePercent / 100 * @SubsFeeAmount,@JournalDecimalPlaces) END
		SET @AgentFeePercent = @paramAgentFeePercent
END

if @FeeType = 2 and @paramAgentFeeAmount > 0
BEGIN
	set @AgentFeeAmount = @paramAgentFeeAmount
	set @AgentFeePercent = @AgentFeeAmount / @SubsFeeAmount * 100
END




if @NAV > 0
BEGIN
	    IF isnull(@TotalCashAmount,0) = 0
		BEGIN
			set @TotalCashAmount = @CashAmount
		END


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
If @UnitRoundingMode = 3 BEGIN 	Set  @UnitAmount = ROUND(@CashAmount / @Nav,@UnitDecimalPlaces) END



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
	set @UnitAmount = 0
	set @TotalUnitAmount = 0
	set @TotalCashAmount = isnull(@TotalCashAmount,@CashAmount)
END


if isnull(@ClientSubscriptionPK,0) <> 0
BEGIN
    Declare @IFUACode nvarchar(50)
    Declare @SACode nvarchar(50)
    Declare @Description nvarchar(100)

    select @IFUACode = isnull(B.IFUACode,''),@SACode = isnull(B.SACode,''),@Description = A.Description from ClientSubscription A
    left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
    where ClientSubscriptionPK = @ClientSubscriptionPK  and A.status = 1

    if (@ClientCode = '24' and @IFUACode = '' and @SACode <> '' and @Description = 'Transaction APERD Summary') -- APERD DAN CUSTOM BNI
    BEGIN
	    Update ClientSubscription set Nav=isnull(@Nav,0),UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
	    where ClientSubscriptionPK = @ClientSubscriptionPK and status = 1

        select @Nav Nav,  CashAmount,  TotalCashAmount,  SubscriptionFeeAmount,  SubscriptionFeePercent ,
        AgentFeeAmount,  AgentFeePercent,  TotalUnitAmount,  UnitAmount from ClientSubscription 
        where ClientSubscriptionPK = @ClientSubscriptionPK and status = 1
    
    END
    ELSE
    BEGIN
	    Update ClientSubscription set Nav=isnull(@Nav,0), CashAmount = isnull(@CashAmount,0), TotalCashAmount = isnull(@TotalCashAmount,0), SubscriptionFeeAmount = isnull(@SubsFeeAmount,0), SubscriptionFeePercent = isnull(@SubsFeePercent,0),
	    AgentFeeAmount = isnull(@AgentFeeAmount,0), AgentFeePercent = isnull(@AgentFeePercent,0), TotalUnitAmount = isnull(@TotalUnitAmount,0), UnitAmount = isnull(@UnitAmount,0), UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
	    where ClientSubscriptionPK = @ClientSubscriptionPK and status = 1

        select @Nav Nav, @CashAmount CashAmount, @TotalCashAmount TotalCashAmount, @SubsFeeAmount SubscriptionFeeAmount, @SubsFeePercent SubscriptionFeePercent ,
        @AgentFeeAmount AgentFeeAmount, @AgentFeePercent AgentFeePercent, @TotalUnitAmount TotalUnitAmount, @UnitAmount UnitAmount 
    END

 
END
ELSE
BEGIN
    select @Nav Nav, @CashAmount CashAmount, @TotalCashAmount TotalCashAmount, @SubsFeeAmount SubscriptionFeeAmount, @SubsFeePercent SubscriptionFeePercent ,
    @AgentFeeAmount AgentFeeAmount, @AgentFeePercent AgentFeePercent, @TotalUnitAmount TotalUnitAmount, @UnitAmount UnitAmount  

END

                        ";


                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _param.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@FundPK", _param.FundPK);
                        cmd.Parameters.AddWithValue("@NavDate", _param.NavDate);
                        cmd.Parameters.AddWithValue("@paramSubscriptionFeePercent", _param.SubscriptionFeePercent);
                        cmd.Parameters.AddWithValue("@paramSubscriptionFeeAmount", _param.SubscriptionFeeAmount);
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
                                ClientSubscriptionRecalculate _ds = new ClientSubscriptionRecalculate();
                                _ds.Nav = dr["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Nav"]);
                                _ds.UnitAmount = dr["UnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["UnitAmount"]);
                                _ds.SubsFeePercent = dr["SubscriptionFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SubscriptionFeePercent"]);
                                _ds.AgentFeePercent = dr["AgentFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AgentFeePercent"]);
                                _ds.SubsFeeAmount = dr["SubscriptionFeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SubscriptionFeeAmount"]);
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
        public void ClientSubscription_GetNavBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
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
Declare @ClientSubscriptionPK int
Declare @FundPK int
Declare @UnitRoundingMode int

--Select * from ClientSubscription where ClientSubscriptionPK = 1
-- update clientSubscription set SubscriptionFeePercent = 0,SubscriptionFeeAmount = 9803.22 where ClientSubscriptionPK = 1

Declare @TotalCashAmount numeric(22,4)
Declare @CashAmount numeric(18,4)
Declare @AgentFeePercent numeric(18,8)
Declare @SubscriptionFeePercent numeric(18,8)
Declare @AgentFeeAmount numeric(18,4)
Declare @SubsFeeAmount numeric(18,4)
Declare @UnitAmount numeric(22,8)
Declare @TotalUnitAmount numeric(22,8)
Declare @FeeType int

Declare @IFUACode nvarchar(50)
Declare @SACode nvarchar(50)
Declare @Description nvarchar(50)

DECLARE A CURSOR FOR 
	Select ClientSubscriptionPK From ClientSubscription 
	where ValueDate between @DateFrom and @DateTo
	and  " + paramClientSubscriptionSelected + @" and Status = 2 and Posted = 0 and type not in(3,6,7)
	--and ClientsubscriptionPK = 1
Open A
Fetch Next From A
Into @ClientSubscriptionPK

While @@FETCH_STATUS = 0
Begin

Select @NAVDate = NAVDate,@FundPK = A.FundPK , @JournalRoundingMode = isnull(D.JournalRoundingMode,3),
@JournalDecimalPlaces = ISNULL(D.JournalDecimalPlaces,4), @AgentFeeAmount = A.AgentFeeAmount,
@AgentFeePercent = A.AgentFeePercent, @SubsFeeAmount = A.SubscriptionFeeAmount , @SubscriptionFeePercent = A.SubscriptionFeePercent,
@CashAmount = A.CashAmount, @FeeType = FeeType, @IFUACode = isnull(E.IFUACode,''),@SACode = isnull(E.SACode,''),@Description = A.Description
from ClientSubscription A
Left join Fund B on A.FundPK = B.FundPK and B.Status = 2
Left join BankBranch C on B.BankBranchPK = C.BankBranchPK and B.status = 2
Left join Bank D on C.BankPK = D.BankPK and C.Status = 2 
Left join FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2) 
Where A.ClientSubscriptionPK = @ClientSubscriptionPK 

set @NAVRoundingMode = 0
set @NAVDecimalPlaces = 0
set @UnitDecimalPlaces = 0
set @UnitRoundingMode = 0
Select @NAVRoundingMode = NAVRoundingMode, @NAVDecimalPlaces = NAVDecimalPlaces,@UnitDecimalPlaces = UnitDecimalPlaces ,@UnitRoundingMode = UnitRoundingMode
From Fund Where FundPK = @FundPK and Status = 2


set @NAV = 0
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



if(@ClientCode = 14)
BEGIN
if @FeeType = 1 and @SubscriptionFeePercent > 0
BEGIN
	If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@Cashamount - (@Cashamount * @SubscriptionFeePercent/100),@JournalDecimalPlaces) 
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
	If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@Cashamount - (@Cashamount * @SubscriptionFeePercent/100),@JournalDecimalPlaces,1) END
	If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@Cashamount - (@Cashamount * @SubscriptionFeePercent/100),@JournalDecimalPlaces) END


	If @JournalRoundingMode = 1 BEGIN Set @SubsFeeAmount = round(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces) 
	IF @JournalDecimalPlaces = 0 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 1
	END
	IF @JournalDecimalPlaces = 2 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.01
	END
	IF @JournalDecimalPlaces = 4 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.0001
	END
	IF @JournalDecimalPlaces = 6 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.000001
	END
	IF @JournalDecimalPlaces = 8 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.00000001
	END
	END
	If @JournalRoundingMode = 2 BEGIN Set   @SubsFeeAmount = ROUND(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces,1) END
	If @JournalRoundingMode = 3 BEGIN Set   @SubsFeeAmount = ROUND(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces) END
	set @SubscriptionFeePercent = @SubscriptionFeePercent
END


Else if @FeeType = 2 and @SubsFeeAmount > 0
BEGIN
	set @TotalCashAmount = @Cashamount - @SubsFeeAmount
	set @SubscriptionFeePercent = (@SubsFeeAmount / @TotalCashAmount) * 100
END
Else
BEGIN
	set @TotalCashAmount = @Cashamount
END


END
ELSE
BEGIN
if @FeeType = 1 and @SubscriptionFeePercent > 0
BEGIN
	If @JournalRoundingMode = 1 BEGIN Set @TotalCashAmount = round(@Cashamount / (1 + @SubscriptionFeePercent/100),@JournalDecimalPlaces) 
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
	If @JournalRoundingMode = 2 BEGIN Set   @TotalCashAmount = ROUND(@Cashamount / (1 + @SubscriptionFeePercent/100),@JournalDecimalPlaces,1) END
	If @JournalRoundingMode = 3 BEGIN Set   @TotalCashAmount = ROUND(@Cashamount / (1 + @SubscriptionFeePercent/100),@JournalDecimalPlaces) END


	If @JournalRoundingMode = 1 BEGIN Set @SubsFeeAmount = round(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces) 
	IF @JournalDecimalPlaces = 0 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 1
	END
	IF @JournalDecimalPlaces = 2 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.01
	END
	IF @JournalDecimalPlaces = 4 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.0001
	END
	IF @JournalDecimalPlaces = 6 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.000001
	END
	IF @JournalDecimalPlaces = 8 BEGIN
		set @SubsFeeAmount = @SubsFeeAmount + 0.00000001
	END
	END
	If @JournalRoundingMode = 2 BEGIN Set   @SubsFeeAmount = ROUND(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces,1) END
	If @JournalRoundingMode = 3 BEGIN Set   @SubsFeeAmount = ROUND(@Cashamount - @TotalCashAmount,@JournalDecimalPlaces) END
	set @SubscriptionFeePercent = @SubscriptionFeePercent
END


Else if @FeeType = 2 and @SubsFeeAmount > 0
BEGIN
	set @TotalCashAmount = @Cashamount - @SubsFeeAmount
	set @SubscriptionFeePercent = (@SubsFeeAmount / @TotalCashAmount) * 100
END
Else
BEGIN
	set @TotalCashAmount = @Cashamount
END


END



if @FeeType = 1 and @AgentFeePercent > 0
BEGIN
		If @JournalRoundingMode = 1 BEGIN Set @AgentFeeAmount = round(@AgentFeePercent / 100 * @SubsFeeAmount,@JournalDecimalPlaces) 
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
		If @JournalRoundingMode = 2 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @SubsFeeAmount,@JournalDecimalPlaces,1) END
		If @JournalRoundingMode = 3 BEGIN Set   @AgentFeeAmount = ROUND(@AgentFeePercent / 100 * @SubsFeeAmount,@JournalDecimalPlaces) END
END

if @FeeType = 2 and @AgentFeeAmount > 0
BEGIN
	set @AgentFeePercent = @AgentFeeAmount / @SubsFeeAmount * 100
END




if @NAV > 0
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
If @UnitRoundingMode = 3 BEGIN 	Set  @UnitAmount = ROUND(@CashAmount / @Nav,@UnitDecimalPlaces) END



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
	set @UnitAmount = 0
	set @TotalUnitAmount = 0
	set @TotalUnitAmount = 0
	set @TotalCashAmount = isnull(@TotalCashAmount,@CashAmount)
END

IF (@ClientCode = '24' and @IFUACode = '' and @SACode <> '' and @Description = 'Transaction APERD Summary') -- APERD DAN CUSTOM BNI
BEGIN

    Update ClientSubscription set Nav=isnull(@Nav,0), UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
    where ClientSubscriptionPK = @ClientSubscriptionPK
END
ELSE
BEGIN
    Update ClientSubscription set Nav=isnull(@Nav,0), CashAmount = isnull(@CashAmount,0), TotalCashAmount = isnull(@TotalCashAmount,0), SubscriptionFeeAmount = isnull(@SubsFeeAmount,0), SubscriptionFeePercent = isnull(@SubscriptionFeePercent,0),
    AgentFeeAmount = isnull(@AgentFeeAmount,0), AgentFeePercent = isnull(@AgentFeePercent,0), TotalUnitAmount = isnull(@TotalUnitAmount,0), UnitAmount = isnull(@UnitAmount,0), UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
    where ClientSubscriptionPK = @ClientSubscriptionPK
END





Fetch next From A Into @ClientSubscriptionPK
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

        public bool CheckHassAdd(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from ClientSubscription where ClientSubscriptionPK = @PK and Status in (1,2) and (Tenor <> 0 or InterestRate <> 0 or PaymentTerm <> 0)";
                        cmd.Parameters.AddWithValue("@PK", _pk);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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

        private DataTable CreateDataTableFromTransactionPromoExcelFile(string _fileSource)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Nama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IFUA";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Nominal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "FeeSubs";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "NominalPembelian";
                    dc.Unique = false;
                    dt.Columns.Add(dc);



                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Produk";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoRekProduk";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TanggalPendebetan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ActualTanggalPendebetan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Status";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_fileSource)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["Nama"] = odRdr[1];
                                    dr["IFUA"] = odRdr[2];
                                    dr["Nominal"] = odRdr[3];
                                    dr["FeeSubs"] = odRdr[4];
                                    dr["NominalPembelian"] = odRdr[5];
                                    dr["Produk"] = odRdr[6];
                                    dr["NoRekProduk"] = odRdr[7];
                                    dr["TanggalPendebetan"] = odRdr[8];
                                    dr["ActualTanggalPendebetan"] = odRdr[9];
                                    dr["Status"] = odRdr[10];


                                    if (dr["Nama"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        
        public string TransactionPromoImport(string _fileSource, string _userID)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table MandiriRegulerInstruction";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.MandiriRegulerInstruction";
                            bulkCopy.WriteToServer(CreateDataTableFromTransactionPromoExcelFile(_fileSource));
                        }

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                              @"
                                if Exists( Select * from MandiriRegulerInstruction 
                                where IFUA not in
                                (
	                                select IFUACode from FundClient where status in (1,2)
                                ))
                                BEGIN

                                declare @Message nvarchar(max)
                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + IFUA  from MandiriRegulerInstruction 
	                                where IFUA not in
		                                (
			                                select IFUACode from FundClient where status in (1,2)
		                                )

	                                Select top 1  'false' result,@Message ResultDesc from MandiriRegulerInstruction 
	                                where IFUA not in
		                                (
			                                select IFUACode from FundClient where status in (1,2)
		                                )
                                END
                                ELSE
                                BEGIN
	                                select 'true' result,'' ResultDesc
                                END
                                ";
                                using (SqlDataReader dr = cmd1.ExecuteReader())
                                {
                                    dr.Read();
                                    if (Convert.ToString(dr["Result"]) == "false")
                                    {
                                        _msg = Convert.ToString(dr["ResultDesc"]);

                                    }
                                    else
                                    {
                                        conn.Close();
                                        conn.Open();
                                        string Total;
                                        using (SqlCommand cmd2 = conn.CreateCommand())
                                        {
                                            cmd2.CommandText =
                                          @"
                                            if Exists( Select * from MandiriRegulerInstruction 
                                            where Produk not in
                                            (
	                                            select ID from Fund where status in (1,2)
                                            ))
                                            BEGIN

                                            declare @Message nvarchar(max)
                                            set @Message = ' '
                                            select distinct @Message = @Message +' , ' + Produk  from MandiriRegulerInstruction 
	                                            where Produk not in
		                                            (
			                                            select ID from Fund where status in (1,2)
		                                            )

	                                            Select top 1  'false' result,@Message ResultDesc from MandiriRegulerInstruction 
	                                            where Produk not in
		                                            (
			                                            select ID from Fund where status in (1,2)
		                                            )
                                            END
                                            ELSE
                                            BEGIN
	                                            select 'true' result,'' ResultDesc
                                            END";
                                            using (SqlDataReader dr01 = cmd2.ExecuteReader())
                                            {
                                                dr01.Read();
                                                if (Convert.ToString(dr01["Result"]) == "false")
                                                {
                                                    _msg = Convert.ToString(dr01["ResultDesc"]);

                                                }
                                                else
                                                {
                                                    conn.Close();
                                                    conn.Open();
                                                    _msg = "Import Regular Instruction Done";
                                                    using (SqlCommand cmd4 = conn.CreateCommand())
                                                    {


                                                        cmd4.CommandText =
                                                      @"
                                                                        Declare @ClientSubscriptionPK int
                                                                        select @ClientSubscriptionPK = isnull(max(ClientSubscriptionPK),0)  + 1 from ClientSubscription
                                                                        set @ClientSubscriptionPK = isnull(@ClientSubscriptionPK,1)

                                                                        INSERT INTO [dbo].[ClientSubscription]    
                                                                        ([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],   
                                                                        [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],   
                                                                        [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[Type],[AutoDebitDate],[ReferenceSInvest],[EntryUsersID],[EntryTime],[LastUpdate])   

                                                                       select @ClientSubscriptionPK + ROW_NUMBER() OVER(ORDER BY A.Nama ASC),1,1,A.ActualTanggalPendebetan,A.ActualTanggalPendebetan,
                                                                        0,B.FundPK,C.FundClientPK,0,B.CurrencyPK,'Reguler Instruction',isnull(A.NominalPendebetan,0),
                                                                        0,isnull(A.NominalPembelian,0),0,0,isnull(A.FeeSubs,0),C.SellingAgentPK,0,0,5,isnull(cast (A.TanggalPendebetan as integer),0),'Auto Debit',@UsersID,@LastUpdate,@LastUpdate    
                                                                        From MandiriRegulerInstruction A
                                                                        Left join Fund B on A.Produk = B.ID and B.status in (1,2)
                                                                        left join FundClient C on A.IFUA = C.IFUACode and C.status in (1,2)
                                                                        --left join FundCashRef D on A.NorekProduk = D.ID and D.Status in (1,2)
                                                                        Where A.Status = 'SUKSES'";
                                                        cmd4.Parameters.AddWithValue("@UsersID", _userID);
                                                        cmd4.Parameters.AddWithValue("@Lastupdate", _dateTime);
                                                        cmd4.ExecuteNonQuery();

                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                                return _msg;

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

        private DataTable CreateDataTableFromPromoMoInvestExcelFile(string _fileSource)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    //dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.String");
                    //dc.ColumnName = "No";
                    //dc.Unique = false;
                    //dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TrxDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IFUA";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Name";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Type";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Product";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Amount";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Unit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FeeAmount";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BitRedemptionAll";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BICCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BIMemberCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoRek";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Notes";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IDEvent";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_fileSource)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [TemplateBaru$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i < 2; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();
                                    dr["TrxDate"] = odRdr[1];
                                    dr["IFUA"] = odRdr[2];
                                    dr["Name"] = odRdr[3];
                                    dr["Type"] = odRdr[4];
                                    dr["Product"] = odRdr[5];
                                    dr["Amount"] = odRdr[6];
                                    dr["Unit"] = odRdr[7];
                                    dr["FeeAmount"] = odRdr[8];
                                    dr["BitRedemptionAll"] = odRdr[9];
                                    dr["BICCode"] = odRdr[10];
                                    dr["BIMemberCode"] = odRdr[11];
                                    dr["NoRek"] = odRdr[12];
                                    dr["Notes"] = odRdr[13];
                                    dr["IDEvent"] = odRdr[14];


                                    if (dr["Name"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string PromoMoInvestImport(string _fileSource, string _userID)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table PromoMoInvestTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.PromoMoInvestTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromPromoMoInvestExcelFile(_fileSource));
                        }

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                              @"
                                if Exists( Select * from PromoMoInvestTemp 
                                where IFUA not in
                                (
                                select IFUACode from FundClient where status in (1,2)
                                ))
                                BEGIN

                                declare @Message nvarchar(max)
                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + IFUA  from PromoMoInvestTemp 
                                where IFUA not in
                                (
                                select IFUACode from FundClient where status in (1,2)
                                )

                                Select top 1  'false' result,@Message ResultDesc from PromoMoInvestTemp 
                                where IFUA not in
                                (
                                select IFUACode from FundClient where status in (1,2)
                                )
                                END
                                ELSE
                                BEGIN
                                select 'true' result,'' ResultDesc
                                END
                                ";
                                cmd1.CommandTimeout = 0;
                                using (SqlDataReader dr = cmd1.ExecuteReader())
                                {
                                    dr.Read();
                                    if (Convert.ToString(dr["Result"]) == "false")
                                    {
                                        _msg = Convert.ToString(dr["ResultDesc"]);

                                    }
                                    else
                                    {
                                        conn.Close();
                                        conn.Open();
                                        using (SqlCommand cmd2 = conn.CreateCommand())
                                        {
                                            cmd2.CommandText =
                                          @"
			                                if Exists( Select * from PromoMoInvestTemp 
                                            where Product not in
                                            (
                                            select ID from Fund where status in (1,2)
                                            ))
                                            BEGIN

                                            declare @Message nvarchar(max)
                                            set @Message = ' '
                                            select distinct @Message = @Message +' , ' + Product  from PromoMoInvestTemp 
                                            where Product not in
                                            (
                                            select ID from Fund where status in (1,2)
                                            )

                                            Select top 1  'false' result,@Message ResultDesc from PromoMoInvestTemp 
                                            where Product not in
                                            (
                                            select ID from Fund where status in (1,2)
                                            )
                                            END
                                            ELSE
                                            BEGIN
                                            select 'true' result,'' ResultDesc
                                            END";
                                            cmd2.CommandTimeout = 0;
                                            using (SqlDataReader dr01 = cmd2.ExecuteReader())
                                            {
                                                dr01.Read();
                                                if (Convert.ToString(dr01["Result"]) == "false")
                                                {
                                                    _msg = Convert.ToString(dr01["ResultDesc"]);

                                                }
                                                else
                                                {
                                                    conn.Close();
                                                    conn.Open();
                                                    using (SqlCommand cmd3 = conn.CreateCommand())
                                                    {
                                                        cmd3.CommandText =
                                                      @"
                                                        if Exists( Select * from PromoMoInvestTemp 
                                                        where IDEvent not in
                                                        (
                                                        select ID from TransactionPromo where status in (1,2)
                                                        ))
                                                        BEGIN

                                                        declare @Message nvarchar(max)
                                                        set @Message = ' '
                                                        select distinct @Message = @Message +' , ' + Product  from PromoMoInvestTemp 
                                                        where IDEvent not in
                                                        (
                                                        select ID from TransactionPromo where status in (1,2)
                                                        )

                                                        Select top 1  'false' result,@Message ResultDesc from PromoMoInvestTemp 
                                                        where IDEvent not in
                                                        (
                                                        select ID from TransactionPromo where status in (1,2)
                                                        )
                                                        END
                                                        ELSE
                                                        BEGIN
                                                        select 'true' result,'' ResultDesc
                                                        END";
                                                        cmd3.CommandTimeout = 0;
                                                        using (SqlDataReader dr02 = cmd3.ExecuteReader())
                                                        {
                                                            dr02.Read();
                                                            if (Convert.ToString(dr02["Result"]) == "false")
                                                            {
                                                                _msg = Convert.ToString(dr02["ResultDesc"]);

                                                            }
                                                            else
                                                            {

                                                                conn.Close();
                                                                conn.Open();

                                                                using (SqlCommand cmd4 = conn.CreateCommand())
                                                                {
                                                                    // VALIDASI CUTOFF TIME SUBSCRIPTION
                                                                    cmd4.CommandText =
                                                                  @"
                                                        
                                                                    Declare @FundPK int
                                                                    Declare @StringTimeNow nvarchar(8)
                                                                    Declare @CutoffTime decimal(22,0)
                                                                    Declare @DecTimeNow decimal(22,0)


                                                                    Declare @Validate table
                                                                    (
                                                                    Result nvarchar(50)
                                                                    )

                                                                    DECLARE A CURSOR FOR 
	                                                                    Select B.FundPK from PromoMoInvestTemp A
	                                                                    left join Fund B on A.Product = B.ID and B.status in (1,2)
                                                                    Open A
                                                                    Fetch Next From A
                                                                    Into @FundPK

                                                                    While @@FETCH_STATUS = 0
                                                                    Begin



	                                                                    SELECT @StringTimeNow = REPLACE(substring(CONVERT(nvarchar(8),@TimeNow,108),0,9),':','')

	                                                                    select @CutoffTime = case when CutOffTime = '' then 0  else cast(CutOffTime as decimal(22,0)) end from Fund where FundPK = @FundPK and status in (1,2)

	                                                                    select @DecTimeNow = cast(@StringTimeNow as decimal(22,0))

	                                                                    IF (@DecTimeNow > @CutoffTime)
	                                                                    BEGIN
		                                                                    insert into @Validate
		                                                                    select 'false'
	                                                                    END


                                                                    Fetch next From A 
                                                                    Into @FundPK
                                                                    END
                                                                    Close A
                                                                    Deallocate A


                                                                    
                                                                    IF NOT EXISTS(select Result from @Validate)
                                                                    BEGIN
                                                                    select 'true' Result
                                                                    END
                                                                    ELSE
                                                                    BEGIN
                                                                    select Result from @Validate
                                                                    END";

                                                                    cmd4.CommandTimeout = 0;
                                                                    cmd4.Parameters.AddWithValue("@TimeNow", _dateTime);
                                                                    using (SqlDataReader dr03 = cmd4.ExecuteReader())
                                                                    {
                                                                        dr03.Read();
                                                                        if (Convert.ToString(dr03["Result"]) == "false")
                                                                        {
                                                                            _msg = "Transaction has passed the cut-off time !";

                                                                        }
                                                                        else
                                                                        {
                                                                            _msg = "Import Promo Mo Invest Done";
                                                                        }
                                                                        conn.Close();
                                                                        conn.Open();


                                                                        using (SqlCommand cmd5 = conn.CreateCommand())
                                                                        {
                                                                            cmd5.CommandText =
                                                                          @"
                                                                    
Declare @ClientSubscriptionPK int
Declare @ClientRedemptionPK INT
DECLARE @FundClientBankDefaultPK int
declare @aBankPK int

Declare @TrxDate datetime,@FundClientPK int,@Type  nvarchar(50),@FundPK int,@Amount numeric(22,2),@InvestorType int,
@Unit numeric(22,8),@FeeAmount numeric(22,2),@BitRedemptionAll nvarchar(50),@BICCode  nvarchar(50),@BIMemberCode  nvarchar(50),
@NoRek  nvarchar(50),@Notes  nvarchar(100),@IDEvent varchar(50),@CurrencyPK int,@AgentPK int,@BankPK INT,@PaymentDate datetime

DECLARE A CURSOR FOR 
select TrxDate,B.FundClientPK,A.Type,C.FundPK,Amount,A.Unit,A.FeeAmount,A.BitRedemptionAll,A.BICCode,
A.BIMemberCode,A.NoRek,A.Notes,A.IDEvent,C.CurrencyPK,B.SellingAgentPK,D.BankPK 
,dbo.FWorkingDay(TrxDate,ISNULL(C.DefaultPaymentDate,0)),B.InvestorType
FROM PromoMoInvestTemp A
left join FundClient B on A.IFUA = B.IFUACode and B.status in (1,2)
left join Fund C on A.Product = C.ID and C.status in (1,2)
left join Bank D on A.BIMemberCode = D.SInvestID and D.status in (1,2)


Open A
Fetch Next From A
Into @TrxDate,@FundClientPK,@Type,@FundPK,@Amount,@Unit,@FeeAmount,@BitRedemptionAll,@BICCode,
@BIMemberCode,@NoRek,@Notes,@IDEvent,@CurrencyPK,@AgentPK,@BankPK,@PaymentDate,@InvestorType

While @@FETCH_STATUS = 0
BEGIN


	IF (@Type = 'SUB')
	BEGIN

        IF(@IDEvent <> '')
        BEGIN
		        select @ClientSubscriptionPK = isnull(max(ClientSubscriptionPK),0)  + 1 from ClientSubscription
		        set @ClientSubscriptionPK = isnull(@ClientSubscriptionPK,1)

		        INSERT INTO [dbo].[ClientSubscription]    
		        ([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],   
		        [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],   
		        [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[Type],[AutoDebitDate],[ReferenceSInvest],[EntryUsersID],[EntryTime],[LastUpdate],TransactionPromoPK,SumberDana)   

		        select @ClientSubscriptionPK + ROW_NUMBER() OVER(ORDER BY @FundClientPK ASC),1,1,@TrxDate,@TrxDate,
		        0,@FundPK,@FundClientPK,[dbo].[FgetFundCashRefPKByFundPK](@FundPK),@CurrencyPK,@Notes,isnull(@Amount,0),
		        0,isnull(@Amount,0),0,0,isnull(@FeeAmount,0),@AgentPK,0,0,5,0,'Transaction Promo',@UsersID,@LastUpdate,@LastUpdate,isnull(TransactionPromoPK,0), case when @InvestorType = 1 then 10 else 5 end
		        from TransactionPromo where ID = @IDEvent
        END
        ELSE
        BEGIN
		        select @ClientSubscriptionPK = isnull(max(ClientSubscriptionPK),0)  + 1 from ClientSubscription
		        set @ClientSubscriptionPK = isnull(@ClientSubscriptionPK,1)

		        INSERT INTO [dbo].[ClientSubscription]    
		        ([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],   
		        [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],   
		        [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[Type],[AutoDebitDate],[ReferenceSInvest],[EntryUsersID],[EntryTime],[LastUpdate],TransactionPromoPK,SumberDana)   

		        select @ClientSubscriptionPK + ROW_NUMBER() OVER(ORDER BY @FundClientPK ASC),1,1,@TrxDate,@TrxDate,
		        0,@FundPK,@FundClientPK,[dbo].[FgetFundCashRefPKByFundPK](@FundPK),@CurrencyPK,@Notes,isnull(@Amount,0),
		        0,isnull(@Amount,0),0,0,isnull(@FeeAmount,0),@AgentPK,0,0,1,0,'Transaction Promo',@UsersID,@LastUpdate,@LastUpdate,0 , case when @InvestorType = 1 then 10 else 5 end
        END

	END
	ELSE
	BEGIN
		select @ClientRedemptionPK = isnull(max(ClientRedemptionPK),0)  + 1 from ClientRedemption
		set @ClientRedemptionPK = isnull(@ClientRedemptionPK,1)
		
		SET @FundClientBankDefaultPK = 0
		
		if ((select FundPK from FundClientBankDefault WHERE status = 2 AND FundClientPK = @FundClientPK) = 0)
			SELECT @FundClientBankDefaultPK = BankRecipientPK FROM dbo.FundClientBankDefault WHERE status = 2 AND FundClientPK = @FundClientPK AND FundPK = 0
		else
			SELECT @FundClientBankDefaultPK = BankRecipientPK FROM dbo.FundClientBankDefault WHERE status = 2 AND FundClientPK = @FundClientPK AND FundPK = @FundPK
        set @FundClientBankDefaultPK = case when @FundClientBankDefaultPK = 0 then 1 else @FundClientBankDefaultPK end
		SET @FundClientBankDefaultPK = isnull(@FundClientBankDefaultPK,1)

		if (@BICCode is not null or @BICCode != '')
		begin
			select @aBankPK = BankPK from Bank where BICode = @BICCode and status in (1,2)

			if exists ( select * from FundClient where FundClientPK = @FundClientPK and NamaBank1 = @aBankPK and NomorRekening1 = @NoRek and status in (1,2))
				set @FundClientBankDefaultPK = 1
			else if exists ( select * from FundClient where FundClientPK = @FundClientPK and NamaBank2 = @aBankPK and NomorRekening2 = @NoRek and status in (1,2))
				set @FundClientBankDefaultPK = 2
			else if exists ( select * from FundClient where FundClientPK = @FundClientPK and NamaBank3 = @aBankPK and NomorRekening3 = @NoRek and status in (1,2))
				set @FundClientBankDefaultPK = 3
			else if exists (select * from FundClientBankList where FundClientPk = @FundClientPK and BankPK = @aBankPK and AccountNo = @NoRek and status in (1,2))
				set @FundClientBankDefaultPK = (select NoBank from FundClientBankList where FundClientPk = @FundClientPK and BankPK = @aBankPK and AccountNo = @NoRek and status in (1,2))
		end

		if (@BIMemberCode is not null or @BIMemberCode != '')
		begin
			select @aBankPK = BankPK from Bank where BICode = @BIMemberCode and status in (1,2)

			if exists ( select * from FundClient where FundClientPK = @FundClientPK and NamaBank1 = @aBankPK and NomorRekening1 = @NoRek and status in (1,2))
				set @FundClientBankDefaultPK = 1
			else if exists ( select * from FundClient where FundClientPK = @FundClientPK and NamaBank2 = @aBankPK and NomorRekening2 = @NoRek and status in (1,2))
				set @FundClientBankDefaultPK = 2
			else if exists ( select * from FundClient where FundClientPK = @FundClientPK and NamaBank3 = @aBankPK and NomorRekening3 = @NoRek and status in (1,2))
				set @FundClientBankDefaultPK = 3
			else if exists (select * from FundClientBankList where FundClientPk = @FundClientPK and BankPK = @aBankPK and AccountNo = @NoRek and status in (1,2))
				set @FundClientBankDefaultPK = (select NoBank from FundClientBankList where FundClientPk = @FundClientPK and BankPK = @aBankPK and AccountNo = @NoRek and status in (1,2))
		end


		INSERT INTO [dbo].[ClientRedemption] 
		([ClientRedemptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[PaymentDate],
		[NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Type],[BitRedemptionAll],[Description],[CashAmount],
		[UnitAmount],[TotalCashAmount],[TotalUnitAmount],
		[RedemptionFeePercent],[RedemptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[UnitPosition],[BankRecipientPK],[TransferType],[FeeType],[ReferenceSInvest],[EntryUsersID],[EntryTime],[LastUpdate])

		select @ClientRedemptionPK + ROW_NUMBER() OVER(ORDER BY @FundClientPK ASC),1,1,@TrxDate,@TrxDate,@PaymentDate,
		0,@FundPK,@FundClientPK,[dbo].[FgetFundCashRefPKByFundPK](@FundPK),@CurrencyPK,1,case when @BitRedemptionAll = 'Y' then 1 else 0 end,@Notes,isnull(@Amount,0),
		case when @BitRedemptionAll = 'Y' then dbo.FgetLastUnitAmount_Promo(@TrxDate,@FundPK,@FundClientPK) else isnull(@Unit,0) end,isnull(@Amount,0),case when @BitRedemptionAll = 'Y' then  dbo.FgetLastUnitAmount_Promo(@TrxDate,@FundPK,@FundClientPK)  else isnull(@Unit,0) end,
		0,isnull(@FeeAmount,0),@AgentPK,0,0,dbo.FgetLastUnitAmount_Promo(@TrxDate,@FundPK,@FundClientPK),@FundClientBankDefaultPK,case when @Amount > 100000000 then 2 else 1 end ,1,'Transaction Promo',@UsersID,@LastUpdate,@LastUpdate 

	END
	

Fetch next From A Into @TrxDate,@FundClientPK,@Type,@FundPK,@Amount,@Unit,@FeeAmount,@BitRedemptionAll,@BICCode,
@BIMemberCode,@NoRek,@Notes,@IDEvent,@CurrencyPK,@AgentPK,@BankPK,@PaymentDate,@InvestorType
END
Close A
Deallocate A 

    ";
                                                                            cmd5.CommandTimeout = 0;
                                                                            cmd5.Parameters.AddWithValue("@UsersID", _userID);
                                                                            cmd5.Parameters.AddWithValue("@Lastupdate", _dateTime);
                                                                            cmd5.ExecuteNonQuery();

                                                                        }
                                                                        //}
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }

                                                }
                                            }
                                        }

                                    }
                                }
                                return _msg;

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

        public Boolean ClientSubscriptionBatchFormBySelectedDataTaspen(string _userID, DateTime _dateFrom, DateTime _dateTo, ClientSubscription _clientSubscription)
        {

            try
            {

                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_clientSubscription.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientSubscription.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _clientSubscription.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
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
                        CashAmount GrossAmount, CS.SubscriptionFeePercent FeePercent, CS.SubscriptionFeeAmount FeeAmount, 
                        CS.TotalCashAmount NetAmount, CS.Description Remark, CU.ID CurrencyID,
                        isnull(BC.ID,'') BankCustodiID, isnull(BC.Address,'') Address, isnull(BC.Attn1,'') ContactPerson, isnull(BC.Fax1,'') FaxNo, isnull(BC.Address,'') CustodyAddress,FCR.BankAccountNo  from ClientSubscription CS   
                        left join FundClient FC ON CS.fundclientpk = FC.fundclientpk and FC.status = 2  
                        left join Fund F ON CS.FundPK = F.FundPK  and F.status = 2   
                        left join FundCashRef FCR ON CS.CashRefPK = FCR.FundCashRefPK  and FCR.status = 2   
                        left join BankBranch BC ON F.BankBranchPK = BC.BankBranchPK  and BC.status = 2   
                        left join Bank BN on BC.BankPK = BN.BankPK and BN.status = 2
                        left join Currency CU ON CU.CurrencyPK = CS.CurrencyPK  and CU.status = 2   
                        WHERE CS.NAVDate between @DateFrom and @DateTo and " + paramClientSubscriptionSelected + @" and CS.Type = 1 and CS.status not in(3,4)
                        --order by CS.FundPK ";

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
                                string filePath = Tools.ReportsPath + "BatchFormSUBInstructionBySelected_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "BatchFormSUBInstructionBySelected_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Batch Form SUB Instruction");


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
                                    worksheet.Cells[incRowExcel, 4].Value = "Instruksi subscription Unit Link";
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Font.Bold = true;
                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Row(incRowExcel).Height = 45;
                                    worksheet.Cells[incRowExcel, 1].Value = "Dengan ini kami instruksikan agar bank mandiri kustodian dapat melaksanakan transaksi subscription pada unit link PT Asuransi Jiwa Taspen dengan rincian sebagai berikut:";
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
                                        worksheet.Cells[incRowExcel, 3].Value = _host.Get_SignatureName(_clientSubscription.Signature1);
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_SignatureName(_clientSubscription.Signature2);
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;


                                        worksheet.Cells[incRowExcel, 3].Value = _host.Get_PositionSignature(_clientSubscription.Signature1);
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_PositionSignature(_clientSubscription.Signature2);
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

        public int ClientSubscriptionWithInterest_Update(ClientSubscription _clientSubscription, bool _havePrivillege)
        {
            try
            {
     
                int status = _host.Get_Status(_clientSubscription.ClientSubscriptionPK, _clientSubscription.HistoryPK, "ClientSubscription");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Update ClientSubscription set Tenor=@Tenor,InterestRate=@InterestRate,PaymentTerm=@PaymentTerm,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where ClientSubscriptionPK = @PK and historyPK = @HistoryPK";
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientSubscription.HistoryPK);
                        cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                        
                        cmd.Parameters.AddWithValue("@Tenor", _clientSubscription.Tenor);
                        cmd.Parameters.AddWithValue("@InterestRate", _clientSubscription.InterestRate);
                        cmd.Parameters.AddWithValue("@PaymentTerm", _clientSubscription.PaymentTerm); 
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSubscription.EntryUsersID);
                        cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

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


        #region Upload Subcription
        public string ImportUploadSubcription(string _fileSource, string _userID)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table ZUpload_Subs";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZUpload_Subs";
                            bulkCopy.WriteToServer(CreateDataTableFromUploadSubcription(_fileSource));
                            _msg = "Import Subcription Success";
                        }

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                              @"

                                delete ZUpload_Subs where [no] is null
                                 
                                declare @Message nvarchar(max)

                                if Exists( Select * from [ZUpload_Subs] 
                                where ISNUMERIC(unit) = 0 OR ISNUMERIC(FeeAmount) = 0 )
                                BEGIN

                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + [No] from [ZUpload_Subs] 
                                where ISNUMERIC(unit) = 0 OR ISNUMERIC(FeeAmount) = 0

                                Select top 1  'false' result,'Wrong Number format Amount or Unit For No: ' + @Message ResultDesc
                                return
                                END


                                if Exists( Select * from [ZUpload_Subs] 
                                where ISDATE(Date) = 0 )
                                BEGIN

                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + [No] from [ZUpload_Subs] 
                                where ISDATE(Date) = 0

                                Select top 1  'false' result,'Format Date is wrong For No: ' + @Message ResultDesc
                                return
                                END


                                if Exists( Select * from [ZUpload_Subs] 
                                where IFUA not in
                                (
                                select IFUACode from FundClient where status in (1,2)
                                ))
                                BEGIN

                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + IFUA  from [ZUpload_Subs] 
                                where IFUA not in
                                (
                                select IFUACode from FundClient where status in (1,2)
                                )

                                Select top 1  'false' result,'No IFUA For data: ' + @Message ResultDesc
                                return
                                END


                                if Exists( Select * from [ZUpload_Subs] 
                                where product not in
                                (
                                select ID from Fund where status in (1,2)
                                ))
                                BEGIN

                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + Product from [ZUpload_Subs] 
                                where Product not in
                                (
                                select ID from Fund where status in (1,2)
                                )

                                Select top 1  'false' result,'No Fund For data: ' + @Message ResultDesc
                                return
                                END


                                if Exists( Select * from [ZUpload_Subs] 
                                where ISNULL(Date,'') = '' )
                                BEGIN

                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + [No] from [ZUpload_Subs] 
                                where ISNULL(Date,'') = '' 

                                Select top 1  'false' result,'No Date For No: ' + @Message ResultDesc
                                return
                                END

                                if Exists( Select * from [ZUpload_Subs] 
                                where ISNULL(Date,'') = '' )
                                BEGIN

                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + [No] from [ZUpload_Subs] 
                                where ISNULL(Date,'') = '' 

                                Select top 1  'false' result,'No Date For No: ' + @Message ResultDesc
                                return
                                END



                                SELECT 'true' result,'' ResultDesc

                                ";
                                using (SqlDataReader dr = cmd1.ExecuteReader())
                                {
                                    dr.Read();
                                    if (Convert.ToString(dr["Result"]) == "false")
                                    {
                                        _msg = Convert.ToString(dr["ResultDesc"]);
                                        return _msg;
                                    }
                                    else
                                    {
                                        conn.Close();
                                        conn.Open();
                                        using (SqlCommand cmd2 = conn.CreateCommand())
                                        {
                                            cmd2.CommandText =
                                            @" 
                                                                    

                                            DECLARE @PK INT

                                            SELECT @PK = MAX(ClientSubscriptionPK) FROM dbo.ClientSubscription

                                            SET @PK = ISNULL(@PK,0)

                                            INSERT INTO [dbo].[ClientSubscription]    
                                            ([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],   
                                            [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],   
                                            [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[Type]
                                            ,[AutoDebitDate],[ReferenceSInvest],[EntryUsersID],[EntryTime],[LastUpdate],TransactionPromoPK,Notes)   

                                            SELECT @PK + ROW_NUMBER() OVER	(ORDER BY B.FundClientPK ASC) 
                                            ,1,1,CONVERT(DATE,A.Date),CONVERT(DATE,A.Date),0,C.FundPK,B.FundClientPK,[dbo].[FgetFundCashRefPKByFundPK](C.FundPK)
                                            ,C.CurrencyPK,'data from upload subs, ' + A.Notes,CAST(A.Amount AS NUMERIC(24,4)),CAST(A.Unit AS NUMERIC(24,4)),CAST(A.Amount AS NUMERIC(24,4)),CAST(A.Unit AS NUMERIC(24,4))
                                            ,0,CAST(A.FeeAmount AS NUMERIC(24,4)),B.SellingAgentPK,0,0,1,0,'',@UsersID,@LastUpdate,@LastUpdate,0,A.Notes
                                            FROM dbo.ZUpload_Subs A
                                            INNER JOIN FundClient B ON A.Ifua = B.IFUACode AND B.status IN (1,2)
                                            INNER JOIN Fund C ON A.Product = C.ID AND C.status IN (1,2)


                                                                                ";
                                            cmd2.Parameters.AddWithValue("@UsersID", _userID);
                                            cmd2.Parameters.AddWithValue("@Lastupdate", _dateTime);
                                            cmd2.ExecuteNonQuery();


                                        }

                                    }
                                }
                                return _msg;

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

        private DataTable CreateDataTableFromUploadSubcription(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "No";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Date";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Ifua";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Name";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Product";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Amount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Unit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FeeAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Notes";
            dc.Unique = false;
            dt.Columns.Add(dc);




            using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_fileName)))
            {
                odConnection.Open();
                using (OleDbCommand odCmd = odConnection.CreateCommand())
                {
                    // _oldfilename = nama sheet yang ada di file excel yang diimport
                    odCmd.CommandText = "SELECT * FROM [Subcription$]";
                    using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                    {
                        // start counting from index = 1 --> skipping the header (index=0)
                        for (int i = 1; i <= 1; i++)
                        {
                            odRdr.Read();
                        }
                        do
                        {
                            dr = dt.NewRow();
                            dr["No"] = odRdr[0];
                            dr["Date"] = odRdr[1];
                            dr["Ifua"] = odRdr[2];
                            dr["Name"] = odRdr[3];
                            dr["Product"] = odRdr[4];
                            dr["Amount"] = odRdr[5];
                            dr["Unit"] = odRdr[6];
                            dr["FeeAmount"] = odRdr[7];
                            dr["Notes"] = odRdr[8];

                            dt.Rows.Add(dr);

                        } while (odRdr.Read());
                    }
                }
                odConnection.Close();
            }


            return dt;
        }
        #endregion

        public Boolean CashInstruction_BySelectedData(string _userID, DateTime _DateFrom, DateTime _DateTo, ClientSubscription _clientSubscription)
        {



            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_clientSubscription.ClientSubscriptionSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_clientSubscription.ClientSubscriptionSelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _clientSubscription.ClientSubscriptionSelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {




                        cmd.CommandText = @"
                           select case when isnull(H.ID,'') = '' or isnull(G.BankAccountNo,'') = '' then '' else isnull(H.ID,'') + ' - ' + isnull(G.BankAccountNo,'') end RekeningPengirim
,case when isnull(E.ID,'') = '' or isnull(C.BankAccountNo,'') = '' then '' else isnull(E.ID,'') + ' - ' + isnull(C.BankAccountNo,'') end RekeningTujuan, 
TotalCashAmount Amount,Description Keterangan from ClientSubscription A
left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
left join CashRef C on A.CashRefPK = C.CashRefPK and C.status in (1,2)
left join BankBranch D on C.BankBranchPK = D.BankBranchPK and D.Status in (1,2)
left join Bank E on D.BankPK = E.BankPK and E.status in (1,2)
left join FundClientBankDefault F on A.FundClientPK = F.FundClientPK and A.FundPK = F.FundPK and F.status in (1,2)
left join BankBranch G on F.BankRecipientPK = G.BankBranchPK and G.status in (1,2)
left join Bank H on G.BankPK = H.BankPK and H.Status in (1,2)
where A.ValueDate = @Valuedate and " + paramClientSubscriptionSelected
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
                    paramSelected = "  CS.ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramSelected = "  CS.ClientSubscriptionPK in (0) ";
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
                        select CS.ClientSubscriptionPK from ClientSubscription CS
						left join FundClient F on CS.FundClientPK = F.FundClientPK and F.status in (1,2)
						left join AgentSubscription B on CS.ClientSubscriptionPK = B.ClientSubscriptionPK and B.Status in (1,2) 
						where ValueDate between @DateFrom and @DateTo and CS.status = 1 and " + paramSelected + @"
                        and (F.SACode != '') and (B.AgentPK = 1 or B.AgentPK is null) 

                        if not exists( select * from AgentSubscription A
						where ClientSubscriptionPK in ( select * from #ClientSubsTemp
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

        public void ClientSubscription_PostingUnitBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = " And ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = " And ClientSubscriptionPK in (0) ";
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
                            _parambitManageUR = @" and A.status = 2 and A.Posted = 0 and A.ClientSubscriptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 1 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = " and A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto  " + paramClientSubscriptionSelected;
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        --declare @datefrom date
                        --declare @dateto date
                        --declare @PostedBy nvarchar(20)
                        --declare @PostedTime datetime

                        --set @datefrom = '2020-05-28'
                        --set @dateto = @datefrom
                        --set @PostedBy = 'admin'
                        --set @PostedTime = getdate()

                        --drop table #ClientSubs





                        CREATE TABLE #ClientSubs
                        (
	                        FundJournalPK int,
	                        NAVDate datetime,
	                        Reference nvarchar(100),
	                        ClientSubscriptionPK int,
	                        FundPK int,
	                        FundType int,
	                        FundClientPK int,
	                        FundCashRefPK int,
	                        TransactionPK nvarchar(200),
	                        Type int,
	                        TotalUnitAmount numeric(19,6),
	                        TotalCashAmount numeric(19,4),
	                        UnitAmount numeric(19,4),
	                        CashAmount numeric(19,4),
	                        OtherAmount numeric(19,4),
	                        SubscriptionFeeAmount numeric(19,4),
	                        BitPendingSubscription int,
	                        BitFirstNAV int
                        )

                        CREATE CLUSTERED INDEX indx_ClientSubs ON #ClientSubs (FundJournalPK,FundPK,FundClientPK);


                        Declare @counterDate datetime    
						Declare @yesterday datetime     
						set @counterDate = @DateFrom 

                        insert into #ClientSubs
	                        (
		                        FundJournalPK,
		                        NAVDate,
		                        Reference,
		                        ClientSubscriptionPK,
		                        FundPK,
		                        FundType,
		                        FundClientPK,
		                        FundCashRefPK,
		                        TransactionPK,
		                        Type,
		                        TotalUnitAmount,
		                        TotalCashAmount,
		                        UnitAmount,
		                        CashAmount,
		                        OtherAmount,
		                        SubscriptionFeeAmount,
		                        BitPendingSubscription,
		                        BitFirstNAV
	                        )
	                        Select ROW_NUMBER() OVER	(ORDER BY A.ClientSubscriptionPK ASC) Number ,
			                        A.NAVDate ,
			                        '',
			                        A.ClientSubscriptionPK ,
			                        A.FundPK ,
			                        B.Type ,
			                        A.FundClientPK ,
			                        A.CashRefPK,
			                        A.TransactionPK,
			                        A.Type,
			                        isnull(A.TotalUnitAmount,0),
			                        isnull(A.TotalCashAmount,0),
			                        isnull(A.UnitAmount,0),
			                        isnull(A.CashAmount,0),
			                        isnull(A.OtherAmount,0),
			                        isnull(A.SubscriptionFeeAmount,0),
			                        ISNULL(BitPendingSubscription,0),
			                        case when B.IssueDate = A.NAVDate then 1 else 0 end BitFirstNav
	                        From ClientSubscription A  
	                        LEFT JOIN dbo.FUND B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	                        LEFT JOIN dbo.FundFee C on A.FundPK = C.FundPK and C.Status in (1,2) and date = 
                            (
	                            SELECT MAX(Date) FROM dbo.FundFee WHERE Date <= A.NAVDate AND FundPK = A.FundPK
	                            AND status = 2
                            )
	                        Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto
	                        and (( isnull(A.NAV,0) > 0 and A.Type not in (3,6) ) or A.Type in (3,6)) and ((A.TotalUnitAmount > 0 and A.Type <> 7) or A.Type = 7)
	                        " + _parambitManageUR + @"
	                        order by Number asc


	                        --LOGIC FUND CLIENT POSITION
	                        while @counterDate <= (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails Where status = 2 and Notes = 'Unit')
	                        begin
							
								set @yesterday = dbo.FWorkingDay(@counterDate,-1)

								--FIRST SUB
								INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
								select distinct @counterDate,A.FundClientPK,A.FundPK,0,0  from #ClientSubs A
								left join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPK and B.Date = @counterDate
								where A.NAVDate between @datefrom and @counterDate and B.FundPK is null 


								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPK,TotalCashAmountTo,TotalUnitAmountTo From (
										select FundClientPk,FundPK,sum(TotalUnitAmount) TotalUnitAmountTo,sum(TotalCashAmount) TotalCashAmountTo from #ClientSubs
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPK
									)A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									--jika gak ada FCP di counterdate
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
									select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSubs A
									inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPK
									where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPK,sum(TotalUnitAmount) TotalUnitAmountTo,sum(TotalCashAmount) TotalCashAmountTo from #ClientSubs
										where NAVDate = @counterDate
										group by FundClientPk,FundPK
									)A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									--proses logic update terakhir 
									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPK,sum(TotalUnitAmount) TotalUnitAmountTo,sum(TotalCashAmount) TotalCashAmountTo from #ClientSubs
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPK
									)A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								
		                        set @counterDate = dbo.fworkingday(@counterDate,1)
	                        end

							-- UPDATE STATUS CLIENT SUBSCRIPTION
	                        BEGIN
		                        update A set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
		                        FROM ClientSubscription A
		                        INNER JOIN #ClientSubs B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK
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

        public void ClientSubscription_PostingJournalBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = " And ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = " And ClientSubscriptionPK in (0) ";
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
                            _parambitManageUR = @" and A.status = 2 and A.Posted = 0 and A.ClientSubscriptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 1 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = " and A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto  " + paramClientSubscriptionSelected;
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

	                        CREATE TABLE #ClientSubs
	                        (
		                        FundJournalPK int,
		                        NAVDate datetime,
		                        Reference nvarchar(100),
		                        ClientSubscriptionPK int,
		                        FundPK int,
		                        FundType int,
		                        FundClientPK int,
		                        FundCashRefPK int,
		                        TransactionPK nvarchar(200),
		                        Type int,
		                        TotalUnitAmount numeric(19,6),
		                        TotalCashAmount numeric(19,4),
		                        UnitAmount numeric(19,4),
		                        CashAmount numeric(19,4),
		                        OtherAmount numeric(19,4),
		                        SubscriptionFeeAmount numeric(19,4),
		                        BitPendingSubscription int,
		                        BitFirstNAV int
	                        )

	                        CREATE CLUSTERED INDEX indx_ClientSubs ON #ClientSubs (FundJournalPK,FundPK,FundClientPK);

                        END

                        --JOURNAL
                        BEGIN
	                        insert into #ClientSubs
	                        (
		                        FundJournalPK,
		                        NAVDate,
		                        Reference,
		                        ClientSubscriptionPK,
		                        FundPK,
		                        FundType,
		                        FundClientPK,
		                        FundCashRefPK,
		                        TransactionPK,
		                        Type,
		                        TotalUnitAmount,
		                        TotalCashAmount,
		                        UnitAmount,
		                        CashAmount,
		                        OtherAmount,
		                        SubscriptionFeeAmount,
		                        BitPendingSubscription,
		                        BitFirstNAV
	                        )
	                        Select ROW_NUMBER() OVER	(ORDER BY A.ClientSubscriptionPK ASC) Number ,
			                        A.NAVDate ,
			                        '',
			                        A.ClientSubscriptionPK ,
			                        A.FundPK ,
			                        B.Type ,
			                        A.FundClientPK ,
			                        A.CashRefPK,
			                        A.TransactionPK,
			                        A.Type,
			                        isnull(A.TotalUnitAmount,0),
			                        isnull(A.TotalCashAmount,0),
			                        isnull(A.UnitAmount,0),
			                        isnull(A.CashAmount,0),
			                        isnull(A.OtherAmount,0),
			                        isnull(A.SubscriptionFeeAmount,0),
			                        ISNULL(BitPendingSubscription,0),
			                        case when B.IssueDate = A.NAVDate then 1 else 0 end BitFirstNav
	                        From ClientSubscription A  
	                        LEFT JOIN dbo.FUND B ON A.FundPK = B.FundPK AND B.status IN (1,2)
	                        LEFT JOIN dbo.FundFee C on A.FundPK = C.FundPK and C.Status in (1,2) and date = 
                            (
	                            SELECT MAX(Date) FROM dbo.FundFee WHERE Date <= A.NAVDate AND FundPK = A.FundPK
	                            AND status = 2
                            )
	                        Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto and ((A.TotalUnitAmount > 0 and A.Type not in (3,6,7)) or A.Type in (3,6,7))
	                        " + _parambitManageUR + @"
	                        order by Number asc

	                        --SELAIN ETF T1
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

		                        Select A.FundJournalPK + @FundJournalPKTemp,1,2,'Posting From Subscription'
				                        ,@PeriodPK,dbo.FWorkingDay(A.NAVDate,1),2,A.ClientSubscriptionPK,'SUBSCRIPTION',isnull(A.Reference,''),'Subscription Client: ' + B.ID + ' - ' + B.Name 
		                        From #ClientSubs A  
		                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
		                        left join Fund C on A.FundPK = C.FundPK and C.Status in (1,2)
		                        --where A.FundType <> 10
		                        where A.TotalCashAmount > 0 and A.BitFirstNav = 0 and A.Type not in (3,7)
		
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
		                        ,case when A.BitPendingSubscription = 1 then B.PendingSubscription else isnull(D.FundJournalAccountPK,@DefaultBankAccountPK) end,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		                        ,'Subscription Client: ' + E.ID + ' - ' + E.Name,'D',A.CashAmount
		                        ,A.CashAmount,0
		                        ,1
		                        ,A.CashAmount,0
		                        From #ClientSubs A  
		                        LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		                        LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status = 2
		                        LEFT JOIN FundJournalAccount C ON case when A.BitPendingSubscription = 1 then B.PendingSubscription 
					                        else D.FundJournalAccountPK end = C.FundJournalAccountPK AND C.status IN (1,2)
		                        LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
		                        Where A.FundType <> 10 and A.TotalCashAmount > 0 and A.BitFirstNav = 0 and A.Type not in (3,7)

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
		                        ,B.Subscription,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		                        ,'Subscription Client: ' + D.ID + ' - ' + D.Name,'C',A.TotalCashAmount
		                        ,0,A.TotalCashAmount
		                        ,1
		                        ,0,A.TotalCashAmount
		                        From #ClientSubs A  
		                        LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		                        LEFT JOIN FundJournalAccount C ON B.Subscription = C.FundJournalAccountPK AND C.status IN (1,2)
		                        LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
		                        Where A.FundType <> 10 and A.TotalCashAmount > 0 and A.BitFirstNav = 0 and A.Type not in (3,7)

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
		                        ,B.PayableSubscriptionFee,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		                        ,'Fee Subscription Client: ' + D.ID + ' - ' + D.Name,'C',A.SubscriptionFeeAmount
		                        ,0,A.SubscriptionFeeAmount
		                        ,1
		                        ,0,A.SubscriptionFeeAmount
		                        From #ClientSubs A  
		                        LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		                        LEFT JOIN FundJournalAccount C ON B.PayableSubscriptionFee = C.FundJournalAccountPK AND C.status IN (1,2)
		                        LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
		                        Where A.FundType <> 10 and A.TotalCashAmount > 0 and A.BitFirstNav = 0 and A.Type not in (3,7)
	                        END

	                        --ETF
	                        BEGIN
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
		                        ,case when A.BitPendingSubscription = 1 then B.PendingSubscription else isnull(D.FundJournalAccountPK,@DefaultBankAccountPK) end,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		                        ,'Subscription Client: ' + E.ID + ' - ' + E.Name,'D',A.TotalCashAmount
		                        ,A.TotalCashAmount,0
		                        ,1
		                        ,A.TotalCashAmount,0
		                        From #ClientSubs A  
		                        LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		                        LEFT JOIN FundCashRef D on A.FundCashRefPK = D.FundCashRefPK and D.Status = 2
		                        LEFT JOIN FundJournalAccount C ON D.FundJournalAccountPK = C.FundJournalAccountPK AND C.status IN (1,2)
		                        LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
		                        Where A.FundType = 10 and A.TotalCashAmount > 0  and A.Type not in (3,7)

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
		                        ,B.Subscription,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		                        ,'Subscription Client: ' + D.ID + ' - ' + D.Name,'C',A.TotalCashAmount + A.OtherAmount
		                        ,0,A.TotalCashAmount + A.OtherAmount
		                        ,1
		                        ,0,A.TotalCashAmount + A.OtherAmount
		                        From #ClientSubs A  
		                        LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		                        LEFT JOIN FundJournalAccount C ON B.Subscription = C.FundJournalAccountPK AND C.status IN (1,2)
		                        LEFT JOIN FundClient D on A.FundClientPK = D.FundClientPK and D.Status in (1,2)
		                        Where A.FundType = 10 and A.TotalCashAmount + A.OtherAmount > 0 and A.Type not in (3,7)

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
		                        ,B.OtherReceivable,C.CurrencyPK,A.FundPK,A.FundClientPK,0
		                        ,'Subscription Client: ' + E.ID + ' - ' + E.Name,'D',A.OtherAmount
		                        ,A.OtherAmount,0
		                        ,1
		                        ,A.OtherAmount,0
		                        From #ClientSubs A  
		                        LEFT JOIN dbo.FundAccountingSetup B ON A.FundPK = B.FundPK AND B.status = 2
		                        LEFT JOIN FundJournalAccount C ON B.OtherReceivable = C.FundJournalAccountPK AND C.status IN (1,2)
		                        LEFT JOIN FundClient E on A.FundClientPK = E.FundClientPK and E.Status in (1,2)
		                        Where A.FundType = 10 and A.OtherAmount > 0 and A.Type not in (3,7)
	                        END

	                        --LOGIC FUND CLIENT POSITION
	                        while @counterDate <= (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails Where status = 2 and Notes = 'Unit')
	                        begin
							
								set @yesterday = dbo.FWorkingDay(@counterDate,-1)

								--FIRST SUB
								INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
								select distinct @counterDate,A.FundClientPK,A.FundPK,0,0  from #ClientSubs A
								left join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPK and B.Date = @counterDate
								where A.NAVDate between @datefrom and @counterDate and B.FundPK is null 


								if not exists (
									Select @counterDate,A.FundClientPK,A.FundPK,TotalCashAmountTo,TotalUnitAmountTo From (
										select FundClientPk,FundPK,sum(TotalUnitAmount) TotalUnitAmountTo,sum(TotalCashAmount) TotalCashAmountTo from #ClientSubs
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPK
									)A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								)
								begin
									--jika gak ada FCP di counterdate
									INSERT INTO FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)
									select @counterDate,B.FundClientPK,B.FundPK,B.CashAmount,B.UnitAmount from #ClientSubs A
									inner join FundClientPosition B on A.FundClientPK = B.FundClientPK and B.FundPK = A.FundPK
									where A.NAVDate between @datefrom and @counterDate and B.Date = @yesterday

									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPK,sum(TotalUnitAmount) TotalUnitAmountTo,sum(TotalCashAmount) TotalCashAmountTo from #ClientSubs
										where NAVDate = @counterDate
										group by FundClientPk,FundPK
									)A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								else
								begin
									--proses logic update terakhir 
									UPDATE B Set B.UnitAmount = B.UnitAmount + A.TotalUnitAmountTo From (
										select FundClientPk,FundPK,sum(TotalUnitAmount) TotalUnitAmountTo,sum(TotalCashAmount) TotalCashAmountTo from #ClientSubs
										where NAVDate between @datefrom and @counterDate
										group by FundClientPk,FundPK
									)A INNER JOIN FundClientPosition B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK and B.Date = @counterDate
								end
								
		                        set @counterDate = dbo.fworkingday(@counterDate,1)
	                        end

	                        -- UPDATE STATUS CLIENT SUBSCRIPTION
	                        BEGIN
		                        update A set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
		                        FROM ClientSubscription A
		                        INNER JOIN #ClientSubs B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK
		                        where Status = 2 
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

        public string Validate_CheckHighRiskMonitoringForNikko(UnitRegistryFund _paramUnitRegistryBySelected)
        {
            try
            {

                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = " and ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = " and ClientSubscriptionPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
--declare @Datefrom date
--declare @dateto date
--declare @UsersID nvarchar(20)
--declare @lastupdate datetime

--set @Datefrom = '2020-06-03'
--set @dateto = @Datefrom
--set @UsersID = 'admin'
--set @lastupdate = getdate()

--setup variable
                        begin
	                        declare @SubsAmount table (
		                        FundClientPK int,
		                        CashAmount numeric(32,4)
	                        )

	                        declare @SubsAmount1Month table (
		                        FundClientPK int,
		                        CashAmount numeric(32,4)
	                        )

	                        declare @RedAmount table (
		                        FundClientPK int,
		                        CashAmount numeric(32,4)
	                        )

	                        declare @RedAmount1Month table (
		                        FundClientPK int,
		                        CashAmount numeric(32,4)
	                        )

	                        declare @LastNAV table (
		                        FundPK int,
		                        CloseNAV numeric(22,8),
		                        date date
	                        )

	                        declare @MaxFCP table (
		                        FundClientPK int,
		                        date date
	                        )

	                        declare @LastFCP table (
		                        FundClientPK int,
		                        CashAmount numeric(32,4)
	                        )

	                        declare @ListClientBreach table (
		                        FundClientPK int,
		                        Reference nvarchar(100),
		                        AmountTransaksi numeric(32,4),
		                        CashAmount numeric(32,4),
		                        CashAmount1Month numeric(32,4)
	                        )

	                        declare @FundClientPK int
	                        declare @CashAmount numeric(32,4)
	                        declare @CashAmount1Month numeric(32,4)
	                        declare @AmountTransaksi numeric(32,4)
	                        declare @LastMonth date
	                        declare @PenghasilanInd int
	                        declare @PenghasilanIns int
	                        declare @OfficePosition int
	                        declare @InvestorType int
	                        declare @AssetFor1Year int
	                        declare @SpouseAnnualIncome int
	                        declare @IncomeInformation int
	                        declare @Reference nvarchar(100)
	                        declare @MaxHighRiskMonitoringPK int
	                        declare @msg nvarchar(1000)
                        end


                        set @LastMonth = dateadd(day,-30,@Datefrom)

                        insert into @LastNAV (date,FundPK)
                        select max(date),FundPK from CloseNAV
                        where status = 2 and date <= @datefrom
                        group by FundPK

                        update A set A.CloseNAV = isnull(B.Nav,0)
                        from @LastNAV A
                        left join CloseNAV B on A.date = B.Date and A.FundPK = B.FundPK and B.Status = 2

                        insert into @SubsAmount(CashAmount,FundClientPK)
                        select sum(CashAmount) CashAmount, FundClientPK from ClientSubscription
                        where ValueDate between @datefrom and @dateto and posted = 0 and status = 2
                        group by FundClientPK

                        insert into @RedAmount(FundClientPK,CashAmount)
                        select FundClientPK, sum (case when A.CashAmount > 0 then A.CashAmount else A.UnitAmount * B.CloseNAV end) from ClientRedemption A
                        left join @LastNAV B on A.FundPK = B.FundPK
                        where ValueDate between @datefrom and @dateto and posted = 0 and status = 2
                        group by FundClientPK

                        --last month
                        insert into @SubsAmount1Month(CashAmount,FundClientPK)
                        select sum(CashAmount) CashAmount, FundClientPK from ClientSubscription
                        where ValueDate between @LastMonth and @datefrom and posted = 1 and status = 2
                        group by FundClientPK

                        insert into @RedAmount1Month(FundClientPK,CashAmount)
                        select FundClientPK, sum (case when A.CashAmount > 0 then A.CashAmount else A.UnitAmount * B.CloseNAV end) from ClientRedemption A
                        left join @LastNAV B on A.FundPK = B.FundPK
                        where ValueDate between @LastMonth and @datefrom and posted = 1 and status = 2
                        group by FundClientPK

                        insert into @MaxFCP(FundClientPK,date)
                        select FundClientPK, max(date) from FundClientPosition
                        where date <= @datefrom
                        group by FundClientPK

                        insert into @LastFCP(FundClientPK,CashAmount)
                        select A.FundClientPK, sum(UnitAmount * AvgNAV) from FundClientPosition A
                        left join @MaxFCP B on A.FundClientPK = B.FundClientPK and A.Date = B.date
                        group by A.FundClientPK

                        insert into @ListClientBreach(FundClientPK,Reference,AmountTransaksi,CashAmount,CashAmount1Month)
                        select A.FundClientPK,A.ReferenceSInvest,sum(isnull(TotalCashAmount,0)),sum(isnull(TotalCashAmount,0) + isnull(B.CashAmount,0) + isnull(C.CashAmount,0) - isnull(D.CashAmount,0)),
                        sum(isnull(TotalCashAmount,0) + isnull(E.CashAmount,0) - isnull(F.CashAmount,0)) from ClientSubscription A
                        left join @LastFCP B on A.FundClientPK = B.FundClientPK
                        left join @SubsAmount C on A.FundClientPK = C.FundClientPK
                        left join @RedAmount D on A.FundClientPK = D.FundClientPK
                        left join @SubsAmount1Month E on A.FundClientPK = E.FundClientPK
                        left join @RedAmount1Month F on A.FundClientPK = F.FundClientPK
                        where A.status = 1 and A.posted = 0 and ValueDate between @datefrom and @dateto " + paramClientSubscriptionSelected + @"
                        
                        --and A.ClientSubscriptionPK in ( 88 ) 

                        group by A.FundClientPK,A.ReferenceSInvest


                        DECLARE A CURSOR
                        FOR 

                        select A.FundClientPK,CashAmount,CashAmount1Month,AmountTransaksi, isnull(InvestorType,0),isnull(IncomeInformation,0),isnull(PenghasilanInd,0),isnull(SpouseAnnualIncome,0),isnull(AssetFor1Year,0),Reference from @ListClientBreach A
                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
                        where CashAmount > 90000000

                        OPEN A;

                        FETCH NEXT FROM A INTO @FundClientPK, @CashAmount,@CashAmount1Month,@AmountTransaksi, @InvestorType, @IncomeInformation, @PenghasilanInd,@SpouseAnnualIncome, @AssetFor1Year, @Reference

                        WHILE @@FETCH_STATUS = 0
                            BEGIN

		                        if @InvestorType = 1
		                        begin
			                        if @IncomeInformation = 0
			                        begin
				                        if @AmountTransaksi > case when @SpouseAnnualIncome = 1 then 0.3 * 10000000 when @SpouseAnnualIncome = 2 then 0.3 * 50000000 when @SpouseAnnualIncome = 3 then 0.3 * 100000000 when @SpouseAnnualIncome = 4 then 0.3 * 500000000 when @SpouseAnnualIncome = 5 then 0.3 * 1000000000 end and @SpouseAnnualIncome <> 0
				                        begin
					                        --insert into highrisk monitoring tipe 2 individu
					                        set @msg = COALESCE(@msg + ', ', '') + @Reference
					                        select @MaxHighRiskMonitoringPK = max(HighRiskMonitoringPK) from HighRiskMonitoring
					                        set @MaxHighRiskMonitoringPK = isnull(@MaxHighRiskMonitoringPK,0) + 1

					                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
					                        Select @MaxHighRiskMonitoringPK,1,1,0,97,@FundClientPK,@datefrom,'Subscription amount is ' + cast(@AmountTransaksi as nvarchar) + ', more than 30% of this Client Annual Income, Ref : ' + @Reference,@UsersID,@LastUpdate,@LastUpdate
				                        end
				                        else
				                        begin
					                        if @CashAmount1Month > case when @SpouseAnnualIncome = 1 then 0.3 * 10000000 when @SpouseAnnualIncome = 2 then 0.3 * 50000000 when @SpouseAnnualIncome = 3 then 0.3 * 100000000 when @SpouseAnnualIncome = 4 then 0.3 * 500000000 when @SpouseAnnualIncome = 5 then 0.3 * 1000000000 end and @SpouseAnnualIncome <> 0
					                        begin
						                        --insert into highrisk monitoring tipe 3 individu
						                        set @msg = COALESCE(@msg + ', ', '') + @Reference
						                        select @MaxHighRiskMonitoringPK = max(HighRiskMonitoringPK) from HighRiskMonitoring
						                        set @MaxHighRiskMonitoringPK = isnull(@MaxHighRiskMonitoringPK,0) + 1

						                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
						                        Select @MaxHighRiskMonitoringPK,1,1,0,97,@FundClientPK,@datefrom,'Subscription total amount is ' + cast(@CashAmount1Month as nvarchar) + ' in last 30 days, more than 30% of this Client Annual Income, Ref : ' + @Reference,@UsersID,@LastUpdate,@LastUpdate
					                        end
				                        end
			                        end
			                        else
			                        begin
				                        if @AmountTransaksi > case when @PenghasilanInd = 1 then 0.3 * 10000000 when @PenghasilanInd = 2 then 0.3 * 50000000 when @PenghasilanInd = 3 then 0.3 * 100000000 when @PenghasilanInd = 4 then 0.3 * 500000000 when @PenghasilanInd = 5 then 0.3 * 1000000000 end and @PenghasilanInd <> 0
				                        begin
					                        --insert into highrisk monitoring tipe 2 individu
					                        set @msg = COALESCE(@msg + ', ', '') + @Reference
					                        select @MaxHighRiskMonitoringPK = max(HighRiskMonitoringPK) from HighRiskMonitoring
					                        set @MaxHighRiskMonitoringPK = isnull(@MaxHighRiskMonitoringPK,0) + 1

					                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
					                        Select @MaxHighRiskMonitoringPK,1,1,0,97,@FundClientPK,@datefrom,'Subscription amount is ' + cast(@AmountTransaksi as nvarchar) + ', more than 30% of this Client Annual Income, Ref : ' + @Reference,@UsersID,@LastUpdate,@LastUpdate
				                        end
				                        else
				                        begin
					                        if @CashAmount1Month > case when @PenghasilanInd = 1 then 0.3 * 10000000 when @PenghasilanInd = 2 then 0.3 * 50000000 when @PenghasilanInd = 3 then 0.3 * 100000000 when @PenghasilanInd = 4 then 0.3 * 500000000 when @PenghasilanInd = 5 then 0.3 * 1000000000 end and @PenghasilanInd <> 0
					                        begin
						                        --insert into highrisk monitoring tipe 3 individu
						                        set @msg = COALESCE(@msg + ', ', '') + @Reference
						                        select @MaxHighRiskMonitoringPK = max(HighRiskMonitoringPK) from HighRiskMonitoring
						                        set @MaxHighRiskMonitoringPK = isnull(@MaxHighRiskMonitoringPK,0) + 1

						                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
						                        Select @MaxHighRiskMonitoringPK,1,1,0,97,@FundClientPK,@datefrom,'Subscription total amount is ' + cast(@CashAmount1Month as nvarchar) + ' in last 30 days, more than 30% of this Client Annual Income, Ref : ' + @Reference,@UsersID,@LastUpdate,@LastUpdate
					                        end
				                        end
			                        end
		                        end
		                        else
		                        begin
			                        if @AmountTransaksi > case when @AssetFor1Year = 1 then 0.3 * 100000000000 when @AssetFor1Year = 2 then 0.3 * 500000000000 when @AssetFor1Year = 3 then 0.3 * 1000000000000 when @AssetFor1Year = 4 then 0.3 * 5000000000000  end and @AssetFor1Year not in (0,5)
			                        begin
				                        --insert into highriskmonitoring tipe 2 institusi
				                        set @msg = COALESCE(@msg + ', ', '') + @Reference
				                        select @MaxHighRiskMonitoringPK = max(HighRiskMonitoringPK) from HighRiskMonitoring
				                        set @MaxHighRiskMonitoringPK = isnull(@MaxHighRiskMonitoringPK,0) + 1

				                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
				                        Select @MaxHighRiskMonitoringPK,1,1,0,97,@FundClientPK,@datefrom,'Subscription amount is ' + cast(@AmountTransaksi as nvarchar) + ', more than 30% of this Client Asset from Last Year, Ref : ' + @Reference,@UsersID,@LastUpdate,@LastUpdate
			                        end
			                        else
			                        begin
				                        if @CashAmount1Month > case when @AssetFor1Year = 1 then 0.3 * 100000000000 when @AssetFor1Year = 2 then 0.3 * 500000000000 when @AssetFor1Year = 3 then 0.3 * 1000000000000 when @AssetFor1Year = 4 then 0.3 * 5000000000000  end and @AssetFor1Year not in (0,5)
				                        begin
					                        --insert into highriskmonitoring tipe 3 institusi
					                        set @msg = COALESCE(@msg + ', ', '') + @Reference
					                        select @MaxHighRiskMonitoringPK = max(HighRiskMonitoringPK) from HighRiskMonitoring
					                        set @MaxHighRiskMonitoringPK = isnull(@MaxHighRiskMonitoringPK,0) + 1

					                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
					                        Select @MaxHighRiskMonitoringPK,1,1,0,97,@FundClientPK,@datefrom,'Subscription amount is ' + cast(@CashAmount1Month as nvarchar) + ' in last 30 days, more than 30% of this Client Asset from Last Year, Ref : ' + @Reference,@UsersID,@LastUpdate,@LastUpdate
				                        end
			                        end
		                        end

                                FETCH NEXT FROM A INTO @FundClientPK, @CashAmount,@CashAmount1Month,@AmountTransaksi, @InvestorType, @IncomeInformation, @PenghasilanInd,@SpouseAnnualIncome, @AssetFor1Year, @Reference
                            END;

                        CLOSE A;

                        DEALLOCATE A;


                        if @msg != ''
	                        set @msg = 'Reference : ' + @msg + ' breach, please check HighRiskMonitoring for details'

                        select @msg Result


                        ";

                        cmd.Parameters.AddWithValue("@DateFrom", _paramUnitRegistryBySelected.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _paramUnitRegistryBySelected.DateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _paramUnitRegistryBySelected.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

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

        public List<ValidateClientSubscription> Validate_Custom08(string _UsersID, DateTime _ValueDate, decimal _cashAmount, int _FundPK, int _fundClientPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                List<ValidateClientSubscription> L_ValidateClientSubscription = new List<ValidateClientSubscription>();
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            --declare @ValueDate date
--declare @FundPK int
--declare @Amount numeric(22,4)
--declare @FundClientPK int
--declare @UsersID nvarchar(100)
--declare @lastupdate datetime
--declare @ClientCode nvarchar(100)

--set @ValueDate = '2020-10-08'
--set @FundPK = 42
--SET @FundClientPK = 1403
--set @Amount = 99999999999999
--set @UsersID = 'admin'
--set @lastupdate = getdate()
--set @ClientCode = '08'

--drop table #Reason

declare @paramPersenTotalAsset int

set @paramPersenTotalAsset = 10

declare @tableReason table (
	Reason nvarchar(max)
)

--validasi MaxUnitFundAndIncomePerAnnum
begin

CREATE Table #Reason (Result int, Reason nvarchar(max))

    Declare @MaxUnit numeric (18,6)
    Declare @UnitAmount numeric (18,6)
    Declare @LastNav numeric (18,6)
    select @LastNav = [dbo].[FgetLastCloseNav](@ValueDate,@FundPK)
    select @LastNav = case when @LastNav = 0 then 1000 else @LastNav end 

    select @UnitAmount = sum(Unit) from (
    select sum(UnitAmount) Unit from fundclientPosition where FundPK = @FundPK and Date = @ValueDate
    union all
    select sum(CashAmount/@LastNav) Unit from ClientSubscription where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientRedemption where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKFrom = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKTo = @FundPK
    union all 
    Select @Amount/@LastNav Unit     
                       
    ) A
    Select @MaxUnit = MaxUnits from Fund where FundPK = @FundPK and status in (1,2)

    IF (@UnitAmount >= @MaxUnit)
    BEGIN
	    Insert into #Reason (Result,Reason)
	    select 1 Result,'Total Unit Subscription Fund : ' + CONVERT(varchar, CAST(@UnitAmount AS money), 1) + ' and Max Unit Fund : ' +  CONVERT(varchar, CAST(@MaxUnit AS money), 1) Reason
    END
    
	DECLARE @IncomePerAnnum numeric(32,0)
    select @IncomePerAnnum = IncomePerAnnum from (
    select case when Code = 1 then 9999999 
    else case when Code = 2 then 50000000 
    else case when Code = 3 then 100000000 
    else case when Code = 4 then 500000000
    else case when Code = 5 then 1000000000
    else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
    left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
    where FundClientPK = @FundClientPK and A.Status =2 and ClientCategory = 1
    union all
    select  case when Code = 1 then 99999999999 
    else case when Code = 2 then 500000000000 
    else case when Code = 3 then 1000000000000
    else case when Code = 4 then 5000000000000
    else case when Code = 5 then 6000000000000
    else  9000000000000 end end end end END * 0.8 IncomePerAnnum from FundClient A 
    left join MasterValue B on A.AssetFor1Year = B.Code and B.ID = 'AssetIns' and B.Status in (1,2)
    where FundClientPK = @FundClientPK and A.Status = 2 and ClientCategory = 2
    ) A


	DECLARE @TrxSubsAYear NUMERIC(22,0)
	DECLARE @TrxRedempAYear NUMERIC(22,0)
	DECLARE @ClientCategory INT 
	DECLARE @DateMinOne DATETIME
		DECLARE @DateMinTwo DATETIME
		DECLARE @TotalAUMInsti NUMERIC(22,0)
	SELECT @ClientCategory = ClientCategory FROM FundClient WHERE FundClientPK = @FundClientPK AND Status IN (1,2)

	IF(@ClientCategory = 1)
	BEGIN
		SELECT @TrxSubsAYear = SUM(ISNULL(CashAmount,0)) FROM dbo.ClientSubscription A WHERE status <> 3 AND Year(A.ValueDate) = Year(@ValueDate)
		AND FundClientPK = @FundClientPK
		SELECT @TrxRedempAYear = SUM(ISNULL(CashAmount,0))  FROM dbo.ClientRedemption A WHERE status <> 3 AND Year(A.ValueDate) = Year(@ValueDate)
		AND FundClientPK = @FundClientPK

		
	END

	ELSE
	BEGIN
		
		SET @DateMinOne = dbo.FWorkingDay(@ValueDate,-1)
		SET @DateMinTwo = dbo.FWorkingDay(@ValueDate,-2)
		SELECT @TotalAUMInsti = SUM(ISNULL(A.UnitAmount,0) * ISNULL(@LastNav,0)) FROM FundClientPosition A 
		WHERE A.Date = @DateMinTwo
		AND A.FundClientPK = @FundClientPK

		
	END
	SET @TrxSubsAYear = ISNULL(@TrxSubsAYear,0)
		SET @TrxRedempAYear = ISNULL(@TrxRedempAYear,0)
		SET @TotalAUMInsti = ISNULL(@TotalAUMInsti,0)

    IF (@Amount + @TrxSubsAYear - @TrxRedempAYear + @TotalAUMInsti > @IncomePerAnnum)
    BEGIN
    Declare @Reason nvarchar(500)
    Declare @PK int
                        
    set @Reason = 'Total Amount Subscription For this Year : ' + CONVERT(varchar, CAST(@Amount + @TrxSubsAYear - @TrxRedempAYear + @TotalAUMInsti AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

    Insert into #Reason (Result,Reason)
    select 1 Result, @Reason


    END


    IF EXISTS(select Result,Reason from #Reason)
    BEGIN
	    DECLARE @combinedString VARCHAR(MAX)
	    SELECT @combinedString = COALESCE(@combinedString + '; ', '') + Reason
	    FROM #Reason

		insert into @tableReason
	    SELECT @combinedString as Reason
    END

end


--validasi InvestorAndFundRiskProfile
begin
	set @Reason = ''

	select  @Reason =  'Risk Profile and FundProfile Not Match :' + B.Name + '  Product ID:' + 
    F.ID  + '  ClientID:' +B.ID + '   InvestorRiskProfile:' +isnull(D.DescOne,'') + '   ProductRiskProfile:' + isnull(E.DescOne,'') 
    from  fundClient B 
    left join FundRiskProfile C on C.FundPK = @FundPK
    left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status = 2
    left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status = 2
    left join Fund F on C.FundPK = F.FundPK and F.status = 2
    where isnull(B.InvestorsRiskProfile,0) <> isnull(C.RiskProfilePK,0)
    and isnull(B.InvestorsRiskProfile,0) <> 0
    and B.FundClientPK = @FundClientPK

    IF (@ClientCode <> '02')
    BEGIN
    IF len(@Reason) > 0
        BEGIN
				insert into @tableReason
                Select @Reason
        END
    END
end

--validasi Check Client Subscription
begin

	if exists (
		select * from ClientSubscription where FundClientPK=@FundClientPK and FundPK=@FundPK and CashAmount = @Amount
		and ValueDate between CAST(GETDATE() AS DATE) and CAST(GETDATE() AS DATE)
	)
	begin
		insert into @tableReason
		select 'You already input same data for this day'
	end

end         

--validasi Check jumlah transaksi > 100jt
begin
	if @Amount > 100000000
	begin
		insert into @tableReason
		select 'This transaction more than 100 Mil, Please Check/Verify with CS/AML Officer'
	end
end

--validasi check total asset last year
begin
declare @AssetFor1Year int
declare @AmountForAsset numeric(32,4)

select @AssetFor1Year = AssetFor1Year,@AmountForAsset = 
case when AssetFor1Year = 1 then 100000000000
	 when AssetFor1Year = 2 then 500000000000
	 when AssetFor1Year = 3 then 1000000000000
	 when AssetFor1Year = 4 then 5000000000000
end
from FundClient where fundclientpk = @FundClientPK and status = 2

if @AssetFor1Year in (1,2,3,4)
begin
	if @Amount > @AmountForAsset * @paramPersenTotalAsset /100
	begin
		insert into @tableReason
		select 'This transaction more than 10% from total asset, Please Check/Verify with CS/AML Officer'
	end
end

end

--validasi Capital Paid IN
begin
    set @combinedString = ''  

    if (@Amount >= (select CapitalPaidIn*0.05 from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2)) 
    or ((select CapitalPaidIn*0.05 from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2) = null)
    begin
	    set @combinedString = @combinedString + 'Cash Amount is over 5% CAPITAL PAID IN, MAX : ' + (select cast(cast(CapitalPaidIn*0.05 as numeric(18,0)) as nvarchar) from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2) + ', '
    end

    if (@Amount > (select case when Pekerjaan = 1 then 2000000000 when Pekerjaan = 2 then 2000000000 else 0 end from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 1)) 
    begin
	    set @combinedString = @combinedString + 'TotalCashAmount is over 2B'
    end

    if (@combinedString != '')
    begin
	    set @combinedString = SUBSTRING (@combinedString,1,len(@combinedString) - 1)
		
		insert into @tableReason
	    select @combinedString Result
    end

end

select Reason,'' Notes from @tableReason            
                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FundPK);
                        cmd.Parameters.AddWithValue("@Amount", _cashAmount);
                        cmd.Parameters.AddWithValue("@ValueDate", _ValueDate);
                        cmd.Parameters.AddWithValue("@UsersID", _UsersID);
                        cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ValidateClientSubscription M_ValidateClientSubscription = new ValidateClientSubscription();
                                    M_ValidateClientSubscription.Reason = dr["Reason"].ToString();
                                    M_ValidateClientSubscription.Notes = "";
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


                        cmd.CommandText = @" Declare @NAVDate datetime
                                                Declare A Cursor For              
                                                    Select distinct NAVDate From ClientSubscription Where status = 2 and Valuedate = @ValueDateFrom
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

        public List<ValidateClientSubscription> ValidateClientSubscription(DateTime _valueDate, decimal _cashAmount, int _FundPK, int _fundClientPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                List<ValidateClientSubscription> L_ValidateClientSubscription = new List<ValidateClientSubscription>();
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
declare @TableReason table
(
	Reason nvarchar(max),
	Notes nvarchar(100),
	InsertHighRisk int,
	Validate int
)

Declare @Nav table
(
    FundPK int,
    CloseNav numeric(18,8)
)

Create Table #Reason (Result int, Reason nvarchar(max))

Declare @StringTimeNow nvarchar(8)
Declare @CutoffTime decimal(22,0)
Declare @DecTimeNow decimal(22,0)
Declare @paramCashAmount numeric(32,8)
declare @CloseNAV numeric(22,8)
DECLARE @IncomePerAnnum numeric(32,0)

Declare @CFundPK int
Declare @VarAmount numeric(18,4)
DECLARE @combinedString VARCHAR(MAX)
Declare @Reason nvarchar(500)


declare @paramPersenTotalAsset int -- RHB
set @paramPersenTotalAsset = 10

-- InsertHighRisk 1 > Insert ke High Risk Monitoring
-- Validate dipakai untuk validasi mandatory untuk Notes



--1. Income Exposure (AURORA) 
IF (@ClientCode = '02')
BEGIN

    DECLARE A CURSOR FOR 

    select distinct FundPK from FundClientPosition where Date =(
	    Select Max(Date) from FundClientPosition where Date <= @ValueDate and FundClientPK = @FundClientPK
    ) and FundClientPK = @FundClientPK

    Open A
    Fetch Next From A
    Into @CFundPK

    While @@FETCH_STATUS = 0
    BEGIN

    insert into @Nav (FundPK,CloseNav)
    select FundPK,Nav from CloseNAV
    where Date =(
	    Select Max(Date) from CloseNAV where status = 2 and Date <= @ValueDate and FundPK = @FundPK
    ) and status = 2 and FundPK = @FundPK

    Fetch next From A Into @CFundPK
    END
    Close A
    Deallocate A 



    select @VarAmount = sum(UnitAmount * CloseNav) + @Amount from FundClientPosition A
    left join @Nav B on A.FundPK = B.FundPK
    where A.Date =(
	    Select Max(Date) from FundClientPosition where Date <= @ValueDate and A.FundPK = B.FundPK
    ) and FundClientPK = @FundClientPK



    select @IncomePerAnnum = IncomePerAnnum * 3 from (
    select case when Code = 1 then 9999999 
    else case when Code = 2 then 50000000 
    else case when Code = 3 then 100000000 
    else case when Code = 4 then 500000000
    else case when Code = 5 then 1000000000
    else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
    left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
    where FundClientPK = @FundClientPK and A.Status =2 and InvestorType = 1
    union all
    select  case when Code = 1 then 999999999 
    else case when Code = 2 then 5000000000 
    else case when Code = 3 then 10000000000
    else case when Code = 4 then 50000000000
    else case when Code = 5 then 100000000000
    else  99900000000 end end end end end IncomePerAnnum from FundClient A 
    left join MasterValue B on A.PenghasilanInstitusi = B.Code and B.ID = 'IncomeINS' and B.Status in (1,2)
    where FundClientPK = @FundClientPK and A.Status = 2 and InvestorType = 2
    ) A

    IF (@VarAmount > @IncomePerAnnum)
    BEGIN

                        
	    set @Reason = 'Total Amount Subscription : ' + CONVERT(varchar, CAST(@VarAmount AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

	    Insert into #Reason (Result,Reason)
	    select 1 Result, @Reason

    END



    IF EXISTS(select Result,Reason from #Reason)
    BEGIN

	    SELECT @combinedString = COALESCE(@combinedString + '; ', '') + Reason
	    FROM #Reason

        insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	    select @combinedString,'',1,0
    END

END

  
                            



--2. check CutOffTime Custom 10
begin
    if (@ClientCode = '10')
    BEGIN
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
    END

end

--3. check data double input (ALL)
begin
	if exists(
	select * from ClientRedemption where FundClientPK=@FundClientPK and FundPK=@FundPK and CashAmount = @CashAmount
    and ValueDate between CAST(GETDATE() AS DATE) and CAST(GETDATE() AS DATE) )
	begin
		insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
		select 'You already input same data for this day','',0,0
	end
end

--4. Check Max Unit Fund And Income Per Annum (kecuali custom 20)

if (@ClientCode = '08')
BEGIN
              
    --CREATE Table #Reason (Result int, Reason nvarchar(max))

    Declare @MaxUnit numeric (18,6)
    Declare @UnitAmount numeric (18,6)
    Declare @LastNav numeric (18,6)
    select @LastNav = [dbo].[FgetLastCloseNav](@ValueDate,@FundPK)
    select @LastNav = case when @LastNav = 0 then 1000 else @LastNav end 

    select @UnitAmount = sum(Unit) from (
    select sum(UnitAmount) Unit from fundclientPosition where FundPK = @FundPK and Date = @ValueDate
    union all
    select sum(CashAmount/@LastNav) Unit from ClientSubscription where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientRedemption where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKFrom = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKTo = @FundPK
    union all 
    Select @Amount/@LastNav Unit     
                       
    ) A
    Select @MaxUnit = MaxUnits from Fund where FundPK = @FundPK and status in (1,2)

    IF (@UnitAmount >= @MaxUnit)
    BEGIN
	    Insert into #Reason (Result,Reason)
	    select 1 Result,'Total Unit Subscription Fund : ' + CONVERT(varchar, CAST(@UnitAmount AS money), 1) + ' and Max Unit Fund : ' +  CONVERT(varchar, CAST(@MaxUnit AS money), 1) Reason
    END
    

    select @IncomePerAnnum = IncomePerAnnum from (
    select case when Code = 1 then 9999999 
    else case when Code = 2 then 50000000 
    else case when Code = 3 then 100000000 
    else case when Code = 4 then 500000000
    else case when Code = 5 then 1000000000
    else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
    left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
    where FundClientPK = @FundClientPK and A.Status =2 and ClientCategory = 1
    union all
    select  case when Code = 1 then 99999999999 
    else case when Code = 2 then 500000000000 
    else case when Code = 3 then 1000000000000
    else case when Code = 4 then 5000000000000
    else case when Code = 5 then 6000000000000
    else  9000000000000 end end end end END * 0.8 IncomePerAnnum from FundClient A 
    left join MasterValue B on A.AssetFor1Year = B.Code and B.ID = 'AssetIns' and B.Status in (1,2)
    where FundClientPK = @FundClientPK and A.Status = 2 and ClientCategory = 2
    ) A


	DECLARE @TrxSubsAYear NUMERIC(22,0)
	DECLARE @TrxRedempAYear NUMERIC(22,0)
	DECLARE @ClientCategory INT 
	DECLARE @DateMinOne DATETIME
		DECLARE @DateMinTwo DATETIME
		DECLARE @TotalAUMInsti NUMERIC(22,0)
	SELECT @ClientCategory = ClientCategory FROM FundClient WHERE FundClientPK = @FundClientPK AND Status IN (1,2)

	IF(@ClientCategory = 1)
	BEGIN
		SELECT @TrxSubsAYear = SUM(ISNULL(CashAmount,0)) FROM dbo.ClientSubscription A WHERE status <> 3 AND Year(A.ValueDate) = Year(@ValueDate)
		AND FundClientPK = @FundClientPK
		SELECT @TrxRedempAYear = SUM(ISNULL(CashAmount,0))  FROM dbo.ClientRedemption A WHERE status <> 3 AND Year(A.ValueDate) = Year(@ValueDate)
		AND FundClientPK = @FundClientPK

		
	END

	ELSE
	BEGIN
		
		SET @DateMinOne = dbo.FWorkingDay(@ValueDate,-1)
		SET @DateMinTwo = dbo.FWorkingDay(@ValueDate,-2)
		SELECT @TotalAUMInsti = SUM(ISNULL(A.UnitAmount,0) * ISNULL(@LastNav,0)) FROM FundClientPosition A 
		WHERE A.Date = @DateMinTwo
		AND A.FundClientPK = @FundClientPK

		
	END
	SET @TrxSubsAYear = ISNULL(@TrxSubsAYear,0)
		SET @TrxRedempAYear = ISNULL(@TrxRedempAYear,0)
		SET @TotalAUMInsti = ISNULL(@TotalAUMInsti,0)

    IF (@Amount + @TrxSubsAYear - @TrxRedempAYear + @TotalAUMInsti > (@IncomePerAnnum*12))
    BEGIN

                        
    set @Reason = 'Total Amount Subscription For this Year : ' + CONVERT(varchar, CAST(@Amount + @TrxSubsAYear - @TrxRedempAYear + @TotalAUMInsti AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

    insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	select @Reason,'',1,0


    END

END
ELSE IF (@ClientCode not in ('08','20'))
BEGIN


    --Declare @MaxUnit numeric (18,6)
    --Declare @UnitAmount numeric (18,6)
    --Declare @LastNav numeric (18,6)
    select @LastNav = [dbo].[FgetLastCloseNav](@ValueDate,@FundPK)
    select @LastNav = case when @LastNav = 0 then 1000 else @LastNav end 

    select @UnitAmount = sum(Unit) from (
    select sum(UnitAmount) Unit from fundclientPosition where FundPK = @FundPK and Date = @ValueDate
    union all
    select sum(CashAmount/@LastNav) Unit from ClientSubscription where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientRedemption where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKFrom = @FundPK
    union all
    select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKTo = @FundPK
    union all 
    Select @Amount/@LastNav Unit     
                       
    ) A
    Select @MaxUnit = MaxUnits from Fund where FundPK = @FundPK and status in (1,2)

    IF (@UnitAmount >= @MaxUnit)
    BEGIN
	    Insert into #Reason (Result,Reason)
	    select 1 Result,'Total Unit Subscription Fund : ' + CONVERT(varchar, CAST(@UnitAmount AS money), 1) + ' and Max Unit Fund : ' +  CONVERT(varchar, CAST(@MaxUnit AS money), 1) Reason
    END
    --declare @IncomePerAnnum numeric(32,0)
    select @IncomePerAnnum = IncomePerAnnum from (
    select case when Code = 1 then 9999999 
    else case when Code = 2 then 50000000 
    else case when Code = 3 then 100000000 
    else case when Code = 4 then 500000000
    else case when Code = 5 then 1000000000
    else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
    left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
    where FundClientPK = @FundClientPK and A.Status =2 and InvestorType = 1
    union all
    select  case when Code = 1 then 999999999 
    else case when Code = 2 then 5000000000 
    else case when Code = 3 then 10000000000
    else case when Code = 4 then 50000000000
    else case when Code = 5 then 100000000000
    else  99900000000 end end end end end IncomePerAnnum from FundClient A 
    left join MasterValue B on A.PenghasilanInstitusi = B.Code and B.ID = 'IncomeINS' and B.Status in (1,2)
    where FundClientPK = @FundClientPK and A.Status = 2 and InvestorType = 2
    ) A

    IF (@Amount > @IncomePerAnnum)
    BEGIN

                        
    set @Reason = 'Amount Subscription : ' + CONVERT(varchar, CAST(@Amount AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

    insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	select @Reason,'',1,0

    END

    
END


-- 5. Check Investor And Fund Risk Profile (kecuali Custom 02 dan custom 20)

IF (@ClientCode in ('08'))
BEGIN

    select  @Reason =  'Risk Profile and FundProfile Not Match :' + B.Name + '  Product ID:' + 
    F.ID  + '  ClientID:' +B.ID + '   InvestorRiskAppetite:' +isnull(D.DescOne,'') + '   ProductRiskProfile:' + isnull(E.DescOne,'') 
    from  fundClient B 
    left join FundRiskProfile C on C.FundPK = @FundPK
    left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status = 2
    left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status = 2
    left join Fund F on C.FundPK = F.FundPK and F.status = 2
    where isnull(B.InvestorsRiskProfile,0) < isnull(C.RiskProfilePK,0)
    and isnull(B.InvestorsRiskProfile,0) <> 0
    and B.FundClientPK = @FundClientPK

    IF len(@Reason) > 0
    BEGIN
        insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	    select @Reason,'',1,0
    END

END
ELSE IF (@ClientCode not in ('02','08','20'))
BEGIN


    select  @Reason =  'Risk Profile and FundProfile Not Match :' + B.Name + '  Product ID:' + 
    F.ID  + '  ClientID:' +B.ID + '   InvestorRiskProfile:' +isnull(D.DescOne,'') + '   ProductRiskProfile:' + isnull(E.DescOne,'') 
    from  fundClient B 
    left join FundRiskProfile C on C.FundPK = @FundPK
    left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status = 2
    left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status = 2
    left join Fund F on C.FundPK = F.FundPK and F.status = 2
    where isnull(B.InvestorsRiskProfile,0) <> isnull(C.RiskProfilePK,0)
    and isnull(B.InvestorsRiskProfile,0) <> 0
    and B.FundClientPK = @FundClientPK

    IF len(@Reason) > 0
    BEGIN
        insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	    select @Reason,'',1,0
    END

END

-- 6. Check Investor non APERD > 100 milion (BNI)
IF (@ClientCode = '24')
BEGIN

    select  @Reason =  'Client Subscription > 100 M'  +', SID Client :'+ B.SID
    from  fundClient B 
    where B.FundClientPK = @FundClientPK and isnull(SACode,'') = '' and B.status in (1,2) 
    and @Amount > 100000000

    IF len(@Reason) > 0
    BEGIN
        insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	    select @Reason,'',1,0
    END

END


-- 7. validasi Check jumlah transaksi > 100jt (RHB)
IF (@ClientCode = '08')
BEGIN
declare @InvestorType nvarchar(500)

    select @InvestorType = (InvestorType)
    from FundClient where fundclientpk = @FundClientPK and status = 2
	if @Amount > 100000000 and @InvestorType <> 2
	begin
        insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	    select 'This transaction more than IDR 100 Mil, Please Check/Verify with CS/AML Officer','',1,1
	end
END

-- 8. validasi check total asset last year (RHB)
IF (@ClientCode = '08')
BEGIN
    declare @AmountForAsset numeric(32,4)

    select @AmountForAsset = 
    isnull(TotalAsset,0)
    from FundClient where fundclientpk = @FundClientPK and status = 2 and InvestorType = 2

    if @Amount > @AmountForAsset * @paramPersenTotalAsset /100
	begin
        insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	    select 'This transaction more than 10% from total asset, Please Check/Verify with CS/AML Officer','',1,1

	end

END

-- 9. validasi Capital Paid IN (RHB)
IF (@ClientCode = '08')
BEGIN
    set @combinedString = ''  

    if (@Amount >= (select CapitalPaidIn*0.05 from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2 and TotalAsset = 0)) 
    or ((select CapitalPaidIn*0.05 from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2) = null)
    begin
	    set @combinedString = @combinedString + 'Cash Amount is over 5% CAPITAL PAID IN, MAX : ' + (select cast(cast(CapitalPaidIn*0.05 as numeric(18,0)) as nvarchar) from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2) + ', '
    end

    if (@Amount > (select case when Pekerjaan = 1 then 2000000000 when Pekerjaan = 2 then 2000000000 else 0 end from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 1)) 
    begin
	    set @combinedString = @combinedString + ' Total Cash Amount is over 2 Billion '
    end

    if (@combinedString != '')
    begin

        insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	    select @combinedString,'',1,0

    end

END


-- 10. validasi Check minimum trx < Min Subs di table Fund (ALL CLIENT)

Declare @MinSubscription numeric(19,4)
select @MinSubscription = MinSubs from Fund where status in (1,2) and FundPK = @FundPK

if @Amount < @MinSubscription and @MinSubscription <> 0
begin
    insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	select 'This transaction less than Min Subscription, please check Fund First ! ','',1,0
end



-- 11. Validasi Subs utk Lebih Besar dari Annual Income bisa dicicil (BERDIKARI)
IF (@ClientCode = '33')
BEGIN
    declare @FirstOfYear datetime
    declare @CurrentAmount numeric(19,4)
    declare @IncomeAmount numeric(19,4)

    select @DateMinOne = dbo.FWorkingDay(@ValueDate,-1)
    select @FirstOfYear = DATEADD(yy, DATEDIFF(yy, 0, @ValueDate), 0)

    CREATE TABLE #Trx
    (
	    Amount numeric(19,4)
    )


    insert into #Trx
    select sum(isnull(CashAmount,0)) Amount from ClientSubscription 
    where status = 2 and FundClientPK = @FundClientPK and ValueDate between @FirstOfYear and @DateMinOne

    insert into #Trx
    select sum(isnull(CashAmount,0)) * - 1 Amount from ClientRedemption
    where status = 2 and FundClientPK = @FundClientPK and ValueDate between @FirstOfYear and @DateMinOne

    insert into #Trx
    select sum(isnull(CashAmount,0)) from ClientSubscription 
    where status in (1,2) and FundClientPK = @FundClientPK and ValueDate = @ValueDate

    insert into #Trx
    select sum(isnull(CashAmount,0)) * - 1 from ClientRedemption
    where status in (1,2) and FundClientPK = @FundClientPK and ValueDate = @ValueDate

    select @CurrentAmount = sum(isnull(Amount,0)) + isnull(@Amount,0) from #Trx

    select @IncomeAmount = 
    case when B.Code = 1 then 10000000
		    when B.Code = 2 then 50000000
			    when B.Code = 3 then 100000000
				    when B.Code = 4 then 500000000
					    when B.Code = 5 then 1000000000
						    else 100000000000 end from FundClient A
    left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.status = 2
    where FundClientPK = @FundClientPK and A.status in (1,2)


    select @InvestorType = InvestorType From FundClient where status in (1,2) and FundClientPk = @FundClientPK

    if @CurrentAmount >= @IncomeAmount and @InvestorType = 1
    begin
        insert into @TableReason(Reason,Notes,InsertHighRisk,Validate)
	    select 'Client has subscribed more than his annual income in sum on this period. ','',1,0
    end

END
---------------------------------------------------------------------


select ROW_NUMBER() over(order by Reason) No,* from @TableReason

                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FundPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@CashAmount", _cashAmount);
                        cmd.Parameters.AddWithValue("@Amount", _cashAmount);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ValidateClientSubscription M_ValidateClientSubscription = new ValidateClientSubscription();
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