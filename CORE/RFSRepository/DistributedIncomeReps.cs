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
    public class DistributedIncomeReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();
        //1
        string _insertCommand = "INSERT INTO [dbo].[DistributedIncome] " +
                            "([DistributedIncomePK],[HistoryPK],[Status],[ValueDate],[ExDate],[PaymentDate],[FundPK],[Policy],[DistributedIncomeType],[Nav],[CashAmount],[DistributedIncomePerUnit],[DistributedType],[VariableAmount],";
        string _paramaterCommand = "@ValueDate,@ExDate,@PaymentDate,@FundPK,@Policy,@DistributedIncomeType,@Nav,@CashAmount,@DistributedIncomePerUnit,@DistributedType,@VariableAmount,";


        //2
        private DistributedIncome setDistributedIncome(SqlDataReader dr)
        {
            DistributedIncome M_DistributedIncome = new DistributedIncome();
            M_DistributedIncome.DistributedIncomePK = Convert.ToInt32(dr["DistributedIncomePK"]);
            M_DistributedIncome.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_DistributedIncome.Selected = Convert.ToBoolean(dr["Selected"]);
            M_DistributedIncome.Status = Convert.ToInt32(dr["Status"]);
            M_DistributedIncome.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_DistributedIncome.Notes = Convert.ToString(dr["Notes"]);
            M_DistributedIncome.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
            M_DistributedIncome.ExDate = Convert.ToDateTime(dr["ExDate"]);
            M_DistributedIncome.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
            M_DistributedIncome.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_DistributedIncome.FundID = Convert.ToString(dr["FundID"]);
            M_DistributedIncome.Policy = Convert.ToInt32(dr["Policy"]);
            M_DistributedIncome.PolicyDesc = Convert.ToString(dr["PolicyDesc"]);
            M_DistributedIncome.FundName = Convert.ToString(dr["FundName"]);
            M_DistributedIncome.DistributedIncomeType = Convert.ToInt32(dr["DistributedIncomeType"]);
            M_DistributedIncome.DistributedIncomeTypeDesc = Convert.ToString(dr["DistributedIncomeTypeDesc"]);
            M_DistributedIncome.DistributedType = Convert.ToInt32(dr["DistributedType"]);
            M_DistributedIncome.DistributedTypeDesc = Convert.ToString(dr["DistributedTypeDesc"]);
            M_DistributedIncome.Nav = Convert.ToDecimal(dr["Nav"]);
            M_DistributedIncome.CashAmount = Convert.ToDecimal(dr["CashAmount"]);
            M_DistributedIncome.DistributedIncomePerUnit = Convert.ToDecimal(dr["DistributedIncomePerUnit"]);
            M_DistributedIncome.VariableAmount = Convert.ToDecimal(dr["VariableAmount"]);
            M_DistributedIncome.Posted = Convert.ToBoolean(dr["Posted"]);
            M_DistributedIncome.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_DistributedIncome.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_DistributedIncome.Revised = Convert.ToBoolean(dr["Revised"]);
            M_DistributedIncome.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_DistributedIncome.RevisedTime = dr["RevisedTime"].ToString();
            M_DistributedIncome.EntryUsersID = dr["EntryUsersID"].ToString();
            M_DistributedIncome.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_DistributedIncome.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_DistributedIncome.VoidUsersID = dr["VoidUsersID"].ToString();
            M_DistributedIncome.EntryTime = dr["EntryTime"].ToString();
            M_DistributedIncome.UpdateTime = dr["UpdateTime"].ToString();
            M_DistributedIncome.ApprovedTime = dr["ApprovedTime"].ToString();
            M_DistributedIncome.VoidTime = dr["VoidTime"].ToString();
            M_DistributedIncome.DBUserID = dr["DBUserID"].ToString();
            M_DistributedIncome.DBTerminalID = dr["DBTerminalID"].ToString();
            M_DistributedIncome.LastUpdate = dr["LastUpdate"].ToString();
            M_DistributedIncome.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_DistributedIncome;
        }

        //3
        public List<DistributedIncome> DistributedIncome_Select(int _status)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DistributedIncome> L_distributedIncome = new List<DistributedIncome>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select B.ID FundID,case when A.DistributedType == 1 then 'By Total Cash' else case when A.DistributedType == 2 then 'By Per Unit Per Cash' end end DistributedTypeDesc,A.* from DistributedIncome A 
                                left join  Fund B on A.FundPK = B.FundPK and B.Status =2 
                                where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select B.ID FundID,case when A.DistributedType == 1 then 'By Total Cash' else case when A.DistributedType == 2 then 'By Per Unit Per Cash' end end DistributedTypeDesc,A.* from DistributedIncome A  
                                left join Fund B on A.FundPK = B.FundPK and B.Status =2  ";

                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_distributedIncome.Add(setDistributedIncome(dr));
                                }
                            }
                            return L_distributedIncome;
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
        public int DistributedIncome_Add(DistributedIncome _distributedIncome, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(DistributedIncomePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From DistributedIncome";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _distributedIncome.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(DistributedIncomePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From DistributedIncome";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ValueDate", _distributedIncome.ValueDate);
                        cmd.Parameters.AddWithValue("@ExDate", _distributedIncome.ExDate);
                        cmd.Parameters.AddWithValue("@PaymentDate", _distributedIncome.PaymentDate);
                        cmd.Parameters.AddWithValue("@FundPK", _distributedIncome.FundPK);
                        cmd.Parameters.AddWithValue("@Policy", _distributedIncome.Policy);
                        cmd.Parameters.AddWithValue("@DistributedIncomeType", _distributedIncome.DistributedIncomeType);
                        cmd.Parameters.AddWithValue("@Nav", _distributedIncome.Nav); 
                        cmd.Parameters.AddWithValue("@CashAmount", _distributedIncome.CashAmount);
                        cmd.Parameters.AddWithValue("@DistributedIncomePerUnit", _distributedIncome.DistributedIncomePerUnit);
                        cmd.Parameters.AddWithValue("@DistributedType", _distributedIncome.DistributedType);
                        cmd.Parameters.AddWithValue("@VariableAmount", _distributedIncome.VariableAmount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _distributedIncome.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "DistributedIncome");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //6
        public int DistributedIncome_Update(DistributedIncome _distributedIncome, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_distributedIncome.DistributedIncomePK, _distributedIncome.HistoryPK, "DistributedIncome");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update DistributedIncome set status=2, Notes=@Notes,ValueDate=@ValueDate,ExDate=@ExDate,PaymentDate=@PaymentDate,DistributedType=@DistributedType,
                                FundPK=@FundPK,Policy=@Policy,DistributedIncomeType=@DistributedIncomeType,Nav=@Nav,CashAmount=@CashAmount,DistributedIncomePerUnit=@DistributedIncomePerUnit,VariableAmount=@VariableAmount,
                                ApprovedUsersID=@ApprovedUsersID, 
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where DistributedIncomePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _distributedIncome.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _distributedIncome.Notes);
                            cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                            cmd.Parameters.AddWithValue("@ValueDate", _distributedIncome.ValueDate);
                            cmd.Parameters.AddWithValue("@ExDate", _distributedIncome.ExDate);
                            cmd.Parameters.AddWithValue("@PaymentDate", _distributedIncome.PaymentDate);
                            cmd.Parameters.AddWithValue("@FundPK", _distributedIncome.FundPK);
                            cmd.Parameters.AddWithValue("@Policy", _distributedIncome.Policy);
                            cmd.Parameters.AddWithValue("@DistributedIncomeType", _distributedIncome.DistributedIncomeType);
                            cmd.Parameters.AddWithValue("@Nav", _distributedIncome.Nav); 
                            cmd.Parameters.AddWithValue("@CashAmount", _distributedIncome.CashAmount);
                            cmd.Parameters.AddWithValue("@DistributedIncomePerUnit", _distributedIncome.DistributedIncomePerUnit);
                            cmd.Parameters.AddWithValue("@DistributedType", _distributedIncome.DistributedType);
                            cmd.Parameters.AddWithValue("@VariableAmount", _distributedIncome.VariableAmount);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _distributedIncome.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _distributedIncome.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update DistributedIncome set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where DistributedIncomePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _distributedIncome.EntryUsersID);
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
                                cmd.CommandText = @"Update DistributedIncome set Notes=@Notes,ValueDate=@ValueDate,ExDate=@ExDate,PaymentDate=@PaymentDate,DistributedType=@DistributedType,
                                FundPK=@FundPK,Policy=@Policy,DistributedIncomeType=@DistributedIncomeType,Nav=@Nav,CashAmount=@CashAmount,DistributedIncomePerUnit=@DistributedIncomePerUnit,VariableAmount=@VariableAmount,
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where DistributedIncomePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _distributedIncome.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _distributedIncome.Notes);
                                cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                                cmd.Parameters.AddWithValue("@ValueDate", _distributedIncome.ValueDate);
                                cmd.Parameters.AddWithValue("@ExDate", _distributedIncome.ExDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _distributedIncome.PaymentDate);
                                cmd.Parameters.AddWithValue("@FundPK", _distributedIncome.FundPK);
                                cmd.Parameters.AddWithValue("@Policy", _distributedIncome.Policy);
                                cmd.Parameters.AddWithValue("@DistributedIncomeType", _distributedIncome.DistributedIncomeType);
                                cmd.Parameters.AddWithValue("@Nav", _distributedIncome.Nav); 
                                cmd.Parameters.AddWithValue("@CashAmount", _distributedIncome.CashAmount);
                                cmd.Parameters.AddWithValue("@DistributedIncomePerUnit", _distributedIncome.DistributedIncomePerUnit);
                                cmd.Parameters.AddWithValue("@DistributedType", _distributedIncome.DistributedType);
                                cmd.Parameters.AddWithValue("@VariableAmount", _distributedIncome.VariableAmount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _distributedIncome.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_distributedIncome.DistributedIncomePK, "DistributedIncome");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From DistributedIncome where DistributedIncomePK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _distributedIncome.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _distributedIncome.ValueDate);
                                cmd.Parameters.AddWithValue("@ExDate", _distributedIncome.ExDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _distributedIncome.PaymentDate);
                                cmd.Parameters.AddWithValue("@FundPK", _distributedIncome.FundPK);
                                cmd.Parameters.AddWithValue("@Policy", _distributedIncome.Policy);
                                cmd.Parameters.AddWithValue("@DistributedIncomeType", _distributedIncome.DistributedIncomeType);
                                cmd.Parameters.AddWithValue("@Nav", _distributedIncome.Nav); 
                                cmd.Parameters.AddWithValue("@CashAmount", _distributedIncome.CashAmount);
                                cmd.Parameters.AddWithValue("@DistributedIncomePerUnit", _distributedIncome.DistributedIncomePerUnit);
                                cmd.Parameters.AddWithValue("@DistributedType", _distributedIncome.DistributedType);
                                cmd.Parameters.AddWithValue("@VariableAmount", _distributedIncome.VariableAmount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _distributedIncome.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update DistributedIncome set status= 4,Notes=@Notes," +
                                    " LastUpdate=@LastUpdate where DistributedIncomePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _distributedIncome.Notes);
                                cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _distributedIncome.HistoryPK);
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
        public void DistributedIncome_Approved(DistributedIncome _distributedIncome)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DistributedIncome set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where DistributedIncomePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                        cmd.Parameters.AddWithValue("@historyPK", _distributedIncome.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _distributedIncome.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update DistributedIncome set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where DistributedIncomePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _distributedIncome.ApprovedUsersID);
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
        public void DistributedIncome_Reject(DistributedIncome _distributedIncome)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DistributedIncome set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where DistributedIncomePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                        cmd.Parameters.AddWithValue("@historyPK", _distributedIncome.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _distributedIncome.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update DistributedIncome set status= 2,LastUpdate=@lastUpdate where DistributedIncomePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
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
        public void DistributedIncome_Void(DistributedIncome _distributedIncome)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DistributedIncome set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where DistributedIncomePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _distributedIncome.DistributedIncomePK);
                        cmd.Parameters.AddWithValue("@historyPK", _distributedIncome.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _distributedIncome.VoidUsersID);
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


        public List<DistributedIncome> DistributedIncome_SelectDistributedIncomeDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DistributedIncome> L_distributedIncome = new List<DistributedIncome>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, B.ID FundID,B.Name FundName,C.DescOne PolicyDesc, D.DescOne DistributedIncomeTypeDesc,case when A.DistributedType = 1 then 'By Total Cash' else case when A.DistributedType = 2 then 'By Per Unit Per Cash' end end DistributedTypeDesc,A.* from DistributedIncome A 
                                Left Join Fund B on A.FundPK = B.FundPK and B.Status =2  
                                Left Join MasterValue C on A.Policy = C.Code and C.Status =2  and C.ID = 'DistributedIncomePolicy'
                                Left Join MasterValue D on A.DistributedIncomeType = D.Code and D.Status =2  and D.ID = 'DistributedIncomeType' 
                                where  A.status = @status and A.ValueDate between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, B.ID FundID,B.Name FundName,C.DescOne PolicyDesc,D.DescOne DistributedIncomeTypeDesc,case when A.DistributedType = 1 then 'By Total Cash' else case when A.DistributedType = 2 then 'By Per Unit Per Cash' end end DistributedTypeDesc,A.* from DistributedIncome A 
                                Left Join Fund B on A.FundPK = B.FundPK and B.Status =2  
                                Left Join MasterValue C on A.Policy = C.Code and C.Status =2  and C.ID = 'DistributedIncomePolicy' 
                                Left Join MasterValue D on A.DistributedIncomeType = D.Code and D.Status =2  and D.ID = 'DistributedIncomeType'
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
                                    L_distributedIncome.Add(setDistributedIncome(dr));
                                }
                            }
                            return L_distributedIncome;
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
        public string SInvestDistributedIncomeRpt_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
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
                                    select ''   
                                    insert into #Text         
                                    Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.DistributedIncomeType,''))))
                                    + '|' + @CompanyID
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SInvestCode,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ValueDate, 112) <> '19000101' then CONVERT(VARCHAR(10), ValueDate, 112) else '' End),''))))          
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ExDate, 112) <> '19000101' then CONVERT(VARCHAR(10), ExDate, 112) else '' End),''))))          
                                    + '|' + case when A.DistributedIncomePerUnit = 0 then '' else cast(isnull(Round(A.DistributedIncomePerUnit,12),'')as nvarchar(100)) end
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Policy,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), PaymentDate, 112) <> '19000101' then CONVERT(VARCHAR(10), PaymentDate, 112) else '' End),''))))          
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,''))))
 
                                    from (      
                                    Select DI.ValueDate,F.SInvestCode, DI.Policy,DI.ExDate, ROUND(DistributedIncomePerUnit,12)DistributedIncomePerUnit, DistributedIncomeType , PaymentDate, DistributedIncomePK Reference
                                    from DistributedIncome DI 
                                    left join Fund F on DI.FundPK = F.fundPK and f.Status=2         
                                    where ValueDate =  @ValueDate and selected = 1 and DI.status in (1,2)
                                    )A    
                                    Group by A.ValueDate,A.SInvestCode,A.DistributedIncomeType,A.DistributedIncomePerUnit,A.Policy,A.Reference, A.ExDate, A.PaymentDate
                                    order by A.ValueDate Asc
                                    select * from #text          
                                    END ";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "Distributed_Income_Data_Upload.txt";
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
                                    return Tools.HtmlSinvestTextPath + "Distributed_Income_Data_Upload.txt";
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

        public void DistributedIncome_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo)
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

                        declare @UnitDecimalPlaces int,@RPeriodPK int
                        declare @RDate datetime,@RFundPK int,@RPolicy int,@RCurrencyPK int,@RFundClientPK int,@RFundCashRefPK int
                        declare @RDistributeCash numeric(22,2),@RDistributeUnit numeric(22,8)
                        declare @RDistributedIncomePK int,@RPaymentDate datetime,@RNav numeric(22,8)
                        declare @DistributedIncomeAcc int, @DistributedIncomePayAcc int, @CashAtBankAcc int, @FundJournalPK int

                        declare @FCP table
                        (
                        Date datetime,
                        FundPK int,
                        FundClientPK int,
                        UnitAmount numeric(22,8)
                        )

                        Declare @DI table
                        (
                        DistributedIncomePK int,
                        FundPK int,
                        ValueDate datetime,
                        ExDate datetime,
                        Policy int,
                        CashAmount numeric(22,8),
                        PaymentDate datetime,
                        Nav numeric(18,8),
                        DistributedIncomePerUnit numeric(22,8),
                        VariableAmount numeric(22,8),
                        PostedBy nvarchar(50)
                        )

                        insert into @FCP
                        select Date,A.FundPK,FundClientPK,UnitAmount from FundClientPosition A
                        left join DistributedIncome B on A.FundPK = B.FundPK and A.Date = B.ValueDate and B.ValueDate = @datefrom and B.status = 2 and B.Posted = 0 and B.Revised = 0
                        where A.Date = @datefrom and B.FundPK is not null and B.Selected = 1

                        Insert into @DI
                        select DistributedIncomePK,FundPK,ValueDate,ExDate,Policy,CashAmount,PaymentDate,Nav,DistributedIncomePerUnit,VariableAmount,PostedBy from DistributedIncome 
                        where selected = 1 and status = 2 and Posted = 0 and Revised = 0 and ValueDate = @DateFrom

                        Declare B Cursor For 

                        select DistributedIncomePK,PeriodPK,ExDate,PaymentDate,C.Nav,A.FundPK,B.FundClientPK,C.Policy,D.CurrencyPK,E.FundCashRefPK,
                        case when Policy = 3 and sum(A.UnitAmount * C.NAV) > C.VariableAmount 
                        then sum(A.UnitAmount * DistributedIncomePerUnit) 
                        else case when Policy in (1,2) then sum(A.UnitAmount * DistributedIncomePerUnit) else 0 end end DistributeCash,
                        case when Policy = 3 and sum(A.UnitAmount * C.NAV) <= C.VariableAmount 
                        then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end)  
                        else case when Policy in (1,2) then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end) else 0 end end DistributeUnit
                        from @FCP A
                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
                        left join @DI C on A.FundPK = C.FundPK and A.Date = C.ValueDate
                        left join Fund D on A.FundPK = D.FundPK and D.Status in (1,2)
                        left join FundCashRef E on A.FundPK = E.FundPK and E.Status = 2 and E.FundCashRefPK in ( select top 1 FundCashRefPK From FundCashRef F where E.FundPK = F.FundPK and F.Status = 2 )
                        left join Period F on C.ExDate between DateFrom and DateTo and F.Status in (1,2)
                        where A.UnitAmount > 0 and A.Date = C.ValueDate
                        group by DistributedIncomePK,PeriodPK,ExDate,PaymentDate,C.Nav,A.FundPK,B.FundClientPK,C.Policy,D.CurrencyPK,E.FundCashRefPK,A.UnitAmount,C.VariableAmount
                        order by B.FundClientPK asc

                        Open B
                        Fetch Next From B
                        Into @RDistributedIncomePK,@RPeriodPK,@RDate,@RPaymentDate,@RNav,@RFundPK,@RFundClientPK,@RPolicy,@RCurrencyPK,@RFundCashRefPK,@RDistributeCash,@RDistributeUnit
        
                        While @@FETCH_STATUS = 0
                        BEGIN

                        select @RDistributedIncomePK,@RPeriodPK,@RDate,@RPaymentDate,@RNav,@RFundPK,@RFundClientPK,@RPolicy,@RCurrencyPK,@RFundCashRefPK,@RDistributeCash,@RDistributeUnit

                        select @UnitDecimalPlaces  = UnitDecimalPlaces From Fund Where FundPK = @RFundPK and Status = 2
                        Select @DistributedIncomeAcc = DistributedIncomeAcc From FundAccountingSetup where Status = 2  and FundPK = @RFundPK    
                        Select @DistributedIncomePayAcc = DistributedIncomePayableAcc From FundAccountingSetup where Status = 2  and FundPK = @RFundPK                
                        Select @CashAtBankAcc = FundJournalAccountPK From FundCashRef where Status = 2 and FundPK = @RFundPK  and Type  = 3  

                        IF (@RPolicy = 1)
                        BEGIN 

                            Select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 from FundJournal
                            -- T-0
                            INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
                            ,[Description],[Posted],[EntryUsersID],[EntryTime],[LastUpdate])                  
                
                            Select  @FundJournalPK, 1,2,'Posting Distributed Income No: ' + CAST(@RDistributedIncomePK as nvarchar(15)),@RPeriodPK,@RDate,12,@RDistributedIncomePK,'Distributed Income',                  
                            '','T0 Distributed Income' ,1,@PostedBy,@LastUpdate,@LastUpdate                  
                       
                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
                            ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 
                       
                            Select  @FundJournalPK,1,1,2,@DistributedIncomeAcc,CurrencyPK,@RFundPK,0,0,'T0 Distributed Income','D',@RDistributeCash,                   
                            @RDistributeCash,0,1,@RDistributeCash,0,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @DistributedIncomeAcc and Status = 2   
		
                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
                            ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 
                       
                            Select  @FundJournalPK,1,1,2,@DistributedIncomePayAcc,CurrencyPK,@RFundPK,0,0,'T0 Distributed Income','C',@RDistributeCash,                   
                            0,@RDistributeCash,1,0,@RDistributeCash,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @DistributedIncomePayAcc and Status = 2  
		
                            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal   

                            --T -Settle                                 
                            INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
                            ,[Description],[Posted],[EntryUsersID],[EntryTime],[LastUpdate])                  
                
                            Select  @FundJournalPK, 1,2,'Posting Distributed Income No: ' + CAST(@RDistributedIncomePK as nvarchar(15)),@RPeriodPK,@RPaymentDate,12,@RDistributedIncomePK,'Distributed Income',                  
                            '','T-Settle Distributed Income' ,1,@PostedBy,@LastUpdate,@LastUpdate                  
                       
                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
                            ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 
                       
                            Select  @FundJournalPK,1,1,2,@DistributedIncomePayAcc,CurrencyPK,@RFundPK,0,0,'T-Settle Distributed Income','D',@RDistributeCash,                   
                            @RDistributeCash,0,1,@RDistributeCash,0,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @DistributedIncomePayAcc and Status = 2   
		
                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
                            ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 
                       
                            Select  @FundJournalPK,1,1,2,@CashAtBankAcc,CurrencyPK,@RFundPK,0,0,'T-Settle Distributed Income','C',@RDistributeCash,                   
                            0,@RDistributeCash,1,0,@RDistributeCash,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @CashAtBankAcc and Status = 2 

	                        INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
	                        NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
	                        SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
	                        BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,Posted,EntryUsersID,EntryTime,LastUpdate)
	
	                        Select isnull(max(ClientSubscriptionPK),0) + 1,1,1,7,1,@RDate,@RDate,
	                        @RNav,@RFundPK,@RFundClientPK,@RFundCashRefPK,'Distributed Income',ISNULL(@RDistributeCash,0),
	                        0,ISNULL(@RDistributeCash,0),
	                        0,0,0,0,0,0,
	                        1,ISNULL(@RCurrencyPK,0),null,1,1,0,0,0,'',0,@PostedBy,@PostedTime,@PostedTime from ClientSubscription


                        END
                        ELSE IF (@RPolicy = 2) -- CLIENT CODE 24 DIBUAT HARDCODE UNIT DECIMAL PLACES 3
                        BEGIN 
	                        INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
	                        NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
	                        SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
	                        BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,Posted,EntryUsersID,EntryTime,LastUpdate)
	
	                        Select isnull(max(ClientSubscriptionPK),0) + 1,1,1,6,1,@RDate,@RDate,
	                        @RNav,@RFundPK,@RFundClientPK,@RFundCashRefPK,'Distributed Income',ISNULL(@RDistributeCash,0),
	                        case when @ClientCode = '24' then round(ISNULL(@RDistributeUnit,0),3) else round(ISNULL(@RDistributeUnit,0),@UnitDecimalPlaces) end,ISNULL(@RDistributeCash,0),
	                        case when @ClientCode = '24' then round(ISNULL(@RDistributeUnit,0),3) else round(ISNULL(@RDistributeUnit,0),@UnitDecimalPlaces) end,0,0,0,0,0,
	                        1,ISNULL(@RCurrencyPK,0),null,1,1,0,0,0,'',0,@PostedBy,@PostedTime,@PostedTime from ClientSubscription
                        END
                        ELSE
                        BEGIN 
	                        INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
	                        NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
	                        SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
	                        BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,Posted,EntryUsersID,EntryTime,LastUpdate)
	
	                        Select isnull(max(ClientSubscriptionPK),0) + 1,1,1,6,1,@RDate,@RDate,
	                        @RNav,@RFundPK,@RFundClientPK,@RFundCashRefPK,'Distributed Income',ISNULL(@RDistributeCash,0),
	                        case when @ClientCode = '24' then round(ISNULL(@RDistributeUnit,0),3) else round(ISNULL(@RDistributeUnit,0),@UnitDecimalPlaces) end,ISNULL(@RDistributeCash,0),
	                        case when @ClientCode = '24' then round(ISNULL(@RDistributeUnit,0),3) else round(ISNULL(@RDistributeUnit,0),@UnitDecimalPlaces) end,0,0,0,0,0,
	                        1,ISNULL(@RCurrencyPK,0),null,1,1,0,0,0,'',0,@PostedBy,@PostedTime,@PostedTime from ClientSubscription
                        END

                        Fetch next From B Into @RDistributedIncomePK,@RPeriodPK,@RDate,@RPaymentDate,@RNav,@RFundPK,@RFundClientPK,@RPolicy,@RCurrencyPK,@RFundCashRefPK,@RDistributeCash,@RDistributeUnit
                        END
                        Close B
                        Deallocate B 


                        update DistributedIncome  
                        set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime  
                        where selected = 1 and status = 2 and Posted = 0 and Revised = 0 and ValueDate between @DateFrom and @DateTo

                        Update DistributedIncome set selected = 0 ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@PostedBy", _usersID);
                        cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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

        public void DistributedIncome_ReviseBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =

                            @"declare @MaxDistributedIncomePK int
                            declare @DistributedIncomePK int
                            declare @CashAmount numeric(22,2)
                            declare @DistributedIncomePerUnit numeric(22,8)
                            declare @Policy int
                            declare @FundPK int
                            declare @Valuedate datetime
                            declare @ExDate datetime
                            declare @PaymentDate datetime
                            declare @FundJournalPK int
                            declare @DistributedIncomeAcc int
                            declare @DistributedIncomePayAcc int
                            declare @CashAtBankAcc int
                            declare @PeriodPK int 
                            declare @FundClientPK int 


                            DECLARE A CURSOR FOR 
                            select DistributedIncomePK,CashAmount,DistributedIncomePerUnit, Policy,FundPK,ValueDate,ExDate,PaymentDate,FundPK
                            from DistributedIncome where  Status = 2 and Selected = 1 and Posted  = 1 and Revised  = 0  and ValueDate between @DateFrom and @DateTo
	
                            Open A
                            Fetch Next From A
                            Into @DistributedIncomePK,@CashAmount,@DistributedIncomePerUnit, @Policy,@FundPK,@ValueDate, @ExDate,@PaymentDate,@FundPK

                            While @@FETCH_STATUS = 0
                            BEGIN
   
                            Select @DistributedIncomeAcc = DistributedIncomeAcc From FundAccountingSetup where Status = 2  and FundPK = @FundPK    
                            Select @DistributedIncomePayAcc = DistributedIncomePayableAcc From FundAccountingSetup where Status = 2  and FundPK = @FundPK                
                            Select @CashAtBankAcc = FundJournalAccountPK From FundCashRef where Status = 2 and FundPK = @FundPK  and Type  = 3  
                            Select @PeriodPK = PeriodPK From Period where @ValueDate Between DateFrom and DateTo 
                            IF (@Policy = 1)
	                            BEGIN
		                            Select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 from FundJournal
		                            -- T-0
		                            INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                            ,[Description],[Posted],[EntryUsersID],[EntryTime],[LastUpdate])                  
                
		                            Select  @FundJournalPK, 1,2,'Revise Distributed Income No: ' + CAST(@DistributedIncomePK as nvarchar(15)),@PeriodPK,@ValueDate,5,@DistributedIncomePK,'Distributed Income',                  
		                            '','Revise T0 Distributed Income' ,1,@UsersID,@LastUpdate,@LastUpdate                  
                       
		                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
		                            ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 
                       
		                            Select  @FundJournalPK,1,1,2,@DistributedIncomePayAcc,CurrencyPK,@FundPK,0,0,'Revise T0 Distributed Income','D',@CashAmount,                   
		                            @CashAmount,0,1,@CashAmount,0,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @DistributedIncomePayAcc and Status = 2   
		
		                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
		                            ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 
                       
		                            Select  @FundJournalPK,1,1,2,@DistributedIncomeAcc,CurrencyPK,@FundPK,0,0,'Revise T0 Distributed Income','C',@CashAmount,                   
		                            0,@CashAmount,1,0,@CashAmount,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @DistributedIncomeAcc and Status = 2  
		
		                            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal   

		                            --T -Settle                                 
                                   INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                            ,[Description],[Posted],[EntryUsersID],[EntryTime],[LastUpdate])                  
                
		                            Select  @FundJournalPK, 1,2,'Revise Distributed Income No: ' + CAST(@DistributedIncomePK as nvarchar(15)),@PeriodPK,@PaymentDate,12,@DistributedIncomePK,'Distributed Income',                  
		                            '','Revise T-Settle Distributed Income' ,1,@UsersID,@LastUpdate,@LastUpdate                  
                       
		                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
		                            ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 
                       
		                            Select  @FundJournalPK,1,1,2,@CashAtBankAcc,CurrencyPK,@FundPK,0,0,'Revise T-Settle Distributed Income','D',@CashAmount,                   
		                            @CashAmount,0,1,@CashAmount,0,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @CashAtBankAcc and Status = 2   
		
		                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
		                            ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 
                       
		                            Select  @FundJournalPK,1,1,2,@DistributedIncomePayAcc,CurrencyPK,@FundPK,0,0,'Revise T-Settle Distributed Income','C',@CashAmount,                   
		                            0,@CashAmount,1,0,@CashAmount,@LastUpdate From FundJournalAccount Where FundJournalAccountPK = @DistributedIncomePayAcc and Status = 2 
	                            END
	                            ELSE
	                            BEGIN
		                            DECLARE B CURSOR FOR 
		                            select FundClientPK,FundPK from FundClientPosition 
		                            where Date = @Valuedate and FundPK = @FundPK
	
		                            Open B
		                            Fetch Next From B
		                            Into @FundClientPK,@FundPK
		                            While @@FETCH_STATUS = 0
		                            BEGIN

		                            --select @FundClientPK,@FundPK
		                            Update FundClientPosition set UnitAmount = UnitAmount - @DistributedIncomePerUnit,LastUpdate = @LastUpdate
		                            where Date = @Valuedate and FundClientPK = @FundClientPK and FundPK = @FundPK
		                            Fetch next From B Into @FundClientPK,@FundPK
		                            END
		                            Close B
		                            Deallocate B 

	                            END

	                            Select @MaxDistributedIncomePK = ISNULL(MAX(DistributedIncomePK),0) + 1 From DistributedIncome   
	                            INSERT INTO [dbo].[DistributedIncome]  
	                            ([DistributedIncomePK],[HistoryPK] ,[Status],[Notes] ,[ValueDate],[ExDate],
	                            [PaymentDate], [FundPK], [Policy], [NAV] , [DistributedIncomeType], 
	                            [CashAmount] ,[DistributedIncomePerUnit] ,[Posted] ,
	                            [EntryUsersID],[EntryTime],[LastUpdate])
                        
	                            SELECT @MaxDistributedIncomePK,1,1,'Pending Revised'  ,
	                            [ValueDate],[ExDate],[PaymentDate], [FundPK], [Policy], [NAV] , [DistributedIncomeType], 
	                            [CashAmount] ,[DistributedIncomePerUnit],0,
	                            [EntryUsersID],[EntryTime] , @LastUpdate 
	                            FROM DistributedIncome  
	                            where DistributedIncomePK = @DistributedIncomePK


	                            update DistributedIncome 
	                            set RevisedBy = @UsersID,RevisedTime = @LastUpdate,Revised = 1,Lastupdate = @LastUpdate 
	                            where ValueDate = @ValueDate 
	                            and FundPK = @FundPK and Status = 2 and Revised = 0 and Posted  = 1

                            Fetch next From A Into @DistributedIncomePK,@CashAmount,@DistributedIncomePerUnit, @Policy,@FundPK,@ValueDate, @ExDate,@PaymentDate,@FundPK
                            END
                            Close A
                            Deallocate A 

                            Update DistributedIncome set selected = 0
                            ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
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


        public bool Validate_PostingBySelected(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" if Not Exists(select * From EndDayTrails where Status =  2 and ValueDate = dbo.fworkingday(@ValueDateFrom, -1)) 
                         BEGIN Select 1 Result END ELSE BEGIN  
                         if Exists(select * From DistributedIncome where ValueDate between @ValueDateFrom and @ValueDateTo and (status = 2 and Posted = 1 and Revised = 0) and selected = 1) 
                         BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END END ";

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


        public List<DistributedIncome> Init_DataPreviewDistributedIncomeByFundPK(DateTime _date, int _fundPK, int _policy)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DistributedIncome> L_DistributedIncome = new List<DistributedIncome>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


//                            cmd.CommandText = @" 
//
//                            CREATE TABLE #A
//                            (
//                            FundClientName nvarchar(100),IFUA nvarchar(50),SID nvarchar(50),
//                            BegUnit numeric(22,8),A numeric(22,2),DistributeCash numeric(22,2),DistributeUnit numeric(22,8)
//                            )
//
//                            insert into #A(FundClientName,IFUA,SID,BegUnit,A,DistributeCash,DistributeUnit)
//                            select B.Name FundClientName,B.IFUACode IFUA,B.SID SID, A.UnitAmount BegUnit,sum(A.UnitAmount * DistributedIncomePerUnit),
//                            case when @Policy = 3 and sum(A.UnitAmount * DistributedIncomePerUnit) > C.VariableAmount 
//                            then sum(A.UnitAmount * DistributedIncomePerUnit) 
//                            else case when @Policy = 1 then sum(A.UnitAmount * DistributedIncomePerUnit) else 0 end end DistributeCash,
//
//                            case when @Policy = 3 and sum(A.UnitAmount * DistributedIncomePerUnit) <= C.VariableAmount 
//                            then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end)  
//                            else case when @Policy = 2 then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end) else 0 end end DistributeUnit 
//                            from FundClientPosition A
//                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
//                            left join DistributedIncome C on A.FundPK = C.FundPK and C.status = 2
//                            where A.FundPK = @FundPK and A.Date = @Date and C.Policy = @Policy and A.UnitAmount > 0  and C.ExDate = @Date
//                            group by B.Name ,B.IFUACode ,B.SID , A.UnitAmount,C.VariableAmount
//                            order by B.Name asc
//
//                            select FundClientName,IFUA,SID,BegUnit,DistributeCash,DistributeUnit from #A order by FundClientName asc
//
//                            ";

                        cmd.CommandText = @" 
                        CREATE TABLE #A
                        (
                        FundClientName nvarchar(100),IFUA nvarchar(50),SID nvarchar(50),
                        BegUnit numeric(22,8),A numeric(22,2),DistributeCash numeric(22,2),DistributeUnit numeric(22,8)
                        )

                        insert into #A(FundClientName,IFUA,SID,BegUnit,A,DistributeCash,DistributeUnit)
                        select B.Name FundClientName,B.IFUACode IFUA,B.SID SID, A.UnitAmount BegUnit,sum(A.UnitAmount),
                        case when @Policy = 3 and sum(A.UnitAmount * C.NAV) > C.VariableAmount 
                        then sum(A.UnitAmount * DistributedIncomePerUnit) 
                        else case when @Policy = 1 then sum(A.UnitAmount * DistributedIncomePerUnit) else 0 end end DistributeCash,

                        case when @Policy = 3 and sum(A.UnitAmount * C.NAV) <= C.VariableAmount 
                        then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end)  
                        else case when @Policy = 2 then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end) else 0 end end DistributeUnit 
                        from FundClientPosition A
                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
                        left join DistributedIncome C on A.FundPK = C.FundPK and C.status = 2
                        where A.FundPK = @FundPK and A.Date = @Date and C.Policy = @Policy and A.UnitAmount > 0  and C.ExDate = @Date
                        group by B.Name ,B.IFUACode ,B.SID , A.UnitAmount,C.VariableAmount
                        order by B.Name asc

                        select FundClientName,IFUA,SID,BegUnit,DistributeCash,DistributeUnit from #A order by FundClientName asc
                        ";

                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@Policy", _policy);
               

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    DistributedIncome M_DistributedIncome = new DistributedIncome();
                                    M_DistributedIncome.FundClientName = Convert.ToString(dr["FundClientName"]);
                                    M_DistributedIncome.IFUA = Convert.ToString(dr["IFUA"]);
                                    M_DistributedIncome.SID = Convert.ToString(dr["SID"]);
                                    M_DistributedIncome.BegUnit = Convert.ToDecimal(dr["BegUnit"]);
                                    M_DistributedIncome.DistributeCash = Convert.ToDecimal(dr["DistributeCash"]);
                                    M_DistributedIncome.DistributeUnit = Convert.ToDecimal(dr["DistributeUnit"]);
                                    L_DistributedIncome.Add(M_DistributedIncome);

                                }
                            }

                            return L_DistributedIncome;
                        }
                    }
                }
            }

            catch (Exception err)
            {
                throw err;
            }
        }



        public void DistributedIncome_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                        Select @Time,@PermissionID,'DistributedIncome',DistributedIncomePK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from DistributedIncome where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
                       
                        update DistributedIncome set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and DistributedIncomePK in ( Select DistributedIncomePK from DistributedIncome where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) 
                        
                        Update DistributedIncome set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        where status = 4 and DistributedIncomePK in (Select DistributedIncomePK from DistributedIncome where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1)   

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

        public void DistributedIncome_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      

                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                        Select @Time,@PermissionID,'DistributedIncome',DistributedIncomePK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from DistributedIncome where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1    

                        update DistributedIncome set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                        where status = 1 and DistributedIncomePK in ( Select DistributedIncomePK from DistributedIncome where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 )      

                        Update DistributedIncome set status= 2  
                        where status = 4 and DistributedIncomePK in (Select DistributedIncomePK from DistributedIncome where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1)  
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

        public void DistributedIncome_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'DistributedIncome',DistributedIncomePK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from DistributedIncome where ValueDate between @DateFrom and @DateTo and Status = 2  and Selected  = 1 " +
                                          "\n update DistributedIncome set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where DistributedIncomePK in ( Select DistributedIncomePK from DistributedIncome where ValueDate between @DateFrom and @DateTo and Status = 2 and Selected  = 1 ) \n " +
                                          " " +
                                          "";
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


        public void DistributedIncome_Posting(DistributedIncome _distributedIncome)
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

                            declare @UnitDecimalPlaces int
                            declare @RDate datetime,@RFundPK int,@RPolicy int,@RCurrencyPK int,@RFundClientPK int,@RFundCashRefPK int
                            declare @RDistributeCash numeric(22,2),@RDistributeUnit numeric(22,8)

                            DECLARE A CURSOR FOR 
                            --select Date,A.FundPK,B.FundClientPK,C.Policy,D.CurrencyPK,E.FundCashRefPK,
                            --case when Policy = 3 and sum(A.UnitAmount) > C.VariableAmount 
                            --then sum(A.UnitAmount * DistributedIncomePerUnit) 
                            --else case when Policy = 1 then sum(A.UnitAmount * DistributedIncomePerUnit) else 0 end end DistributeCash,
                            --case when Policy = 3 and sum(A.UnitAmount) <= C.VariableAmount 
                            --then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end)  
                            --else case when Policy = 2 then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end) else 0 end end DistributeUnit 
                            --from FundClientPosition A
                            --left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
                            --left join DistributedIncome C on A.FundPK = C.FundPK and C.status = 2 and A.Date = C.ExDate
                            --left join Fund D on A.FundPK = D.FundPK and D.status = 2
                            --left join FundCashRef E on A.FundPK = E.FundPK and E.Status = 2 and E.FundCashRefPK in ( select top 1 FundCashRefPK From FundCashRef F where E.FundPK = F.FundPK and F.Status = 2 )
                            --where A.UnitAmount > 0 and C.DistributedIncomePK = @DistributedIncomePK and C.HistoryPK = @HistoryPK and C.Posted = 0 and C.status = 2 and C.Revised = 0  and B.SACode = ''
                            --group by Date,A.FundPK,B.FundClientPK,C.Policy,D.CurrencyPK,E.FundCashRefPK,A.UnitAmount,C.VariableAmount
                            --order by B.FundClientPK a


                            select ExDate,A.FundPK,B.FundClientPK,C.Policy,D.CurrencyPK,E.FundCashRefPK,
                            case when Policy = 3 and sum(A.UnitAmount * C.NAV) > C.VariableAmount 
                            then sum(A.UnitAmount * DistributedIncomePerUnit) 
                            else case when Policy = 1 then sum(A.UnitAmount * DistributedIncomePerUnit) else 0 end end DistributeCash,
                            case when Policy = 3 and sum(A.UnitAmount * C.NAV) <= C.VariableAmount 
                            then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end)  
                            else case when Policy = 2 then sum(A.UnitAmount * DistributedIncomePerUnit/case when C.NAV = 0 then 1 else C.NAV end) else 0 end end DistributeUnit 
                            from FundClientPosition A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status in (1,2)
                            left join DistributedIncome C on A.FundPK = C.FundPK and C.status = 2 and A.Date = C.ValueDate
                            left join Fund D on A.FundPK = D.FundPK and D.status = 2
                            left join FundCashRef E on A.FundPK = E.FundPK and E.Status = 2 and E.FundCashRefPK in ( select top 1 FundCashRefPK From FundCashRef F where E.FundPK = F.FundPK and F.Status = 2 )
                            where A.UnitAmount > 0 and C.DistributedIncomePK = @DistributedIncomePK and C.HistoryPK = @HistoryPK and C.Posted = 0 and C.status = 2 and C.Revised = 0  and B.SACode = ''
                            group by ExDate,A.FundPK,B.FundClientPK,C.Policy,D.CurrencyPK,E.FundCashRefPK,A.UnitAmount,C.VariableAmount
                            order by B.FundClientPK asc

                            Open A
                            Fetch Next From A
                            Into @RDate,@RFundPK,@RFundClientPK,@RPolicy,@RCurrencyPK,@RFundCashRefPK,@RDistributeCash,@RDistributeUnit
        
                            While @@FETCH_STATUS = 0
                            BEGIN

                            select @UnitDecimalPlaces  = UnitDecimalPlaces From Fund Where FundPK = @RFundPK and Status = 2

                            IF (@RPolicy = 1)
                            BEGIN 
                                INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
                                NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
                                SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
                                BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,Posted,EntryUsersID,EntryTime,LastUpdate)
	
                                Select isnull(max(ClientSubscriptionPK),0) + 1,1,1,6,1,@RDate,@RDate,
                                0,@RFundPK,@RFundClientPK,@RFundCashRefPK,'Distributed Income',ISNULL(@RDistributeCash,0),
                                0,ISNULL(@RDistributeCash,0),
                                0,0,0,0,0,0,
                                1,ISNULL(@RCurrencyPK,0),null,1,1,0,0,0,'',0,@PostedBy,@PostedTime,@PostedTime from ClientSubscription
                            END
                            ELSE IF (@RPolicy = 2)
                            BEGIN 
                                INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
                                NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
                                SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
                                BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,Posted,EntryUsersID,EntryTime,LastUpdate)
	
                                Select isnull(max(ClientSubscriptionPK),0) + 1,1,1,6,1,@RDate,@RDate,
                                0,@RFundPK,@RFundClientPK,@RFundCashRefPK,'Distributed Income',0,
                                round(ISNULL(@RDistributeUnit,0),@UnitDecimalPlaces),0,
                                round(ISNULL(@RDistributeUnit,0),@UnitDecimalPlaces),0,0,0,0,0,
                                1,ISNULL(@RCurrencyPK,0),null,1,1,0,0,0,'',0,@PostedBy,@PostedTime,@PostedTime from ClientSubscription
                            END
                            ELSE
                            BEGIN 
                                INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
                                NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
                                SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
                                BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,Posted,EntryUsersID,EntryTime,LastUpdate)
	
                                Select isnull(max(ClientSubscriptionPK),0) + 1,1,1,6,1,@RDate,@RDate,
                                0,@RFundPK,@RFundClientPK,@RFundCashRefPK,'Distributed Income',ISNULL(@RDistributeCash,0),
                                round(ISNULL(@RDistributeUnit,0),@UnitDecimalPlaces),ISNULL(@RDistributeCash,0),
                                round(ISNULL(@RDistributeUnit,0),@UnitDecimalPlaces),0,0,0,0,0,
                                1,ISNULL(@RCurrencyPK,0),null,1,1,0,0,0,'',0,@PostedBy,@PostedTime,@PostedTime from ClientSubscription
                            END

                            Fetch next From A Into @RDate,@RFundPK,@RFundClientPK,@RPolicy,@RCurrencyPK,@RFundCashRefPK,@RDistributeCash,@RDistributeUnit
                            END
                            Close A
                            Deallocate A 

                            update DistributedIncome  
                            set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime  
                            where DistributedIncomePK = @DistributedIncomePK and HistoryPK = @HistoryPK and Status = 2
                              ";

                        cmd.Parameters.AddWithValue("@DistributedIncomePK", _distributedIncome.DistributedIncomePK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _distributedIncome.HistoryPK);
                        cmd.Parameters.AddWithValue("@PostedBy", _distributedIncome.PostedBy);
                        cmd.Parameters.AddWithValue("@PostedTime", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public bool Validate_Posting(DateTime _exDate, int _fundPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Exists(select * from FundClientPosition where Date = @ExDate and FundPK = @FundPK) " +
                        " BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@ExDate", _exDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
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



        public void DistributedIncome_Revise(DistributedIncome _distributedIncome)
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
                            
                        Declare @ExDate datetime
                        Declare @FundPK int
                        Declare @Policy int
                        Declare @MaxDistributedIncomePK int    

                        select @ExDate = ExDate, @FundPK = FundPK,@Policy = Policy from DistributedIncome where DistributedIncomePK = @DistributedIncomePK 
                        and HistoryPK = @HistoryPK and status = 2 and Posted = 1 and Revised = 0


                        update ClientSubscription set Notes = 'Revised Distributed Income', status = 3, VoidUsersID = @UsersID, VoidTime = @LastUpdate 
                        where ValueDate = @ExDate and Type in (6,7) and FundPK = @FundPK

                        IF (@Policy = 1)
                        BEGIN
                            update FundJournal set status = 3, Posted = 0,VoidusersID = @UsersID, VoidTime = @LastUpdate, LastUpdate = @LastUpdate where Type = 12 and TrxNo = @DistributedIncomePK
                        END
                     
                        Select @MaxDistributedIncomePK = ISNULL(MAX(DistributedIncomePK),0) + 1 From DistributedIncome   
						 
                        Insert into DistributedIncome([DistributedIncomePK],[HistoryPK],[Status],[Notes]
                        ,[ValueDate],[ExDate],[PaymentDate],[FundPK],[Policy],[NAV],[DistributedIncomeType]
                        ,[CashAmount],[DistributedIncomePerUnit],[EntryUsersID],[EntryTime],[LastUpdate]) 

                        SELECT @MaxDistributedIncomePK,1,1,'Pending Revised'
                        ,[ValueDate],[ExDate],[PaymentDate],[FundPK],[Policy],[NAV],[DistributedIncomeType]
                        ,[CashAmount],[DistributedIncomePerUnit],[EntryUsersID],[EntryTime],@LastUpdate 
                        FROM DistributedIncome  
                        WHERE DistributedIncomePK = @DistributedIncomePK   and status = 2 and posted = 1 

                        update DistributedIncome  
                        set status = 3,Revised = 1 , RevisedBy = @UsersID,RevisedTime = @LastUpdate,Posted = 1,Lastupdate = @LastUpdate  
                        where DistributedIncomePK = @DistributedIncomePK and HistoryPK = @HistoryPK and Status = 2
                        
	                  
                        ";

                        cmd.Parameters.AddWithValue("@DistributedIncomePK", _distributedIncome.DistributedIncomePK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _distributedIncome.HistoryPK);
                        cmd.Parameters.AddWithValue("@UsersID", _distributedIncome.RevisedBy);
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


        public string SInvestDistributedIncome_ForSA(string _userID, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Declare @FundPK int
                        Declare @ExDate datetime
                        Declare @Policy int

                        Declare @FCP table
                        (
                        FundPK int,
                        FundClientPK int
                        )


                        Declare A Cursor For 

                        select FundPK,ExDate,Policy from DistributedIncome 
                        where selected = 1 and status in (1,2) and ValueDate between @DateFrom and @DateTo

                        Open A                  
                        Fetch Next From A                  
                        Into @FundPK,@ExDate,@Policy
                        While @@FETCH_STATUS = 0                  
                        Begin   

                        insert into @FCP
                        select FundPK,FundClientPK from FundClientPosition 
                        where  Date = @ExDate and FundPK = @FundPK and UnitAmount > 0

                        Fetch next From A                   
                        Into @FundPK,@ExDate,@Policy
                        END        
                        Close A                  
                        Deallocate A


                            BEGIN  
                        SET NOCOUNT ON    
                        create table #Text(    
                        [ResultText] [nvarchar](1000)  NULL        
                        )                      
                        truncate table #Text     
                        insert into #Text     
                        select ''   
                        insert into #Text         
                        Select  @CompanyID -- SACode
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(B.SInvestCode,'')))) -- FundCode
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(C.IFUACode,'')))) -- IFUACode
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(@Policy,'')))) --Option
 
                        from @FCP A 
                        left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)    
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status in (1,2)     
                        order by B.SInvestCode,C.IFUACode asc  

                        select * from #text          
                        END

                            ";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "Distributed_Income_Data_Option_Upload.txt";
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
                                    return Tools.HtmlSinvestTextPath + "Distributed_Income_Data_Option_Upload.txt";
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


        public bool Validate_VoidBySelected(DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramDistributedIncomeSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramDistributedIncomeSelected = " And DistributedIncomePK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramDistributedIncomeSelected = " And DistributedIncomePK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" if Exists(select * From DistributedIncome where (Status = 2 and Posted = 1 and Revised = 0) and ValueDate between @ValueDateFrom and @ValueDateTo " + paramDistributedIncomeSelected + @") 
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



        public int Validate_PostingNAVBySelected(DateTime _dateFrom, DateTime _dateTo)
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
                        if Exists(
                        select * From DistributedIncome where Status = 2 and ValueDate between @ValueDateFrom and @ValueDateTo 
                        and Posted = 0 and Revised = 0 and isnull(Nav,0) = 0 and Selected = 1  and Policy <> 1) 
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



        public void DistributedIncome_GetNavBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {

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
Declare @DistributedIncomePK int
Declare @FundPK int
Declare @UnitRoundingMode int


Declare @IFUACode nvarchar(50)
Declare @SACode nvarchar(50)
Declare @Description nvarchar(50)

DECLARE A CURSOR FOR 
	Select DistributedIncomePK From DistributedIncome 
	where ValueDate between @DateFrom and @DateTo
	and  selected = 1 and Status = 2 and Posted = 0 
Open A
Fetch Next From A
Into @DistributedIncomePK

While @@FETCH_STATUS = 0
Begin

Select @NAVDate = ExDate,@FundPK = A.FundPK , @JournalRoundingMode = isnull(D.JournalRoundingMode,3),
@JournalDecimalPlaces = ISNULL(D.JournalDecimalPlaces,4)
from DistributedIncome A
Left join Fund B on A.FundPK = B.FundPK and B.Status = 2
Left join BankBranch C on B.BankBranchPK = C.BankBranchPK and B.status = 2
Left join Bank D on C.BankPK = D.BankPK and C.Status = 2 
Where A.DistributedIncomePK = @DistributedIncomePK 

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


Update DistributedIncome set Nav=isnull(@Nav,0), UpdateUsersID = @UpdateUsersID, UpdateTime = @Time , LastUpdate = @Time
where DistributedIncomePK = @DistributedIncomePK

Fetch next From A Into @DistributedIncomePK
end
Close A
Deallocate A


                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
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



        public bool ValidateGenerateCheckSubsRedemp(DateTime _valueDate)
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

                            Declare  @Fund Table
                            (
                            FundPK int 
                            )

                            insert into @Fund
                            select FundPK from DistributedIncome where Valuedate = @DateFrom  and Posted = 0 and Status = 2 and Revised =0 and Selected = 1

                            if exists(select A.FundPK from ClientSubscription A
                            left join @Fund B on A.FundPK = B.FundPK 
                            where ValueDate = @Datefrom  and (Status = 1 or (status = 2 and Posted = 0)) and A.FundPK is not null)
                            BEGIN
                            select 1 Result
                            END
                            ELSE
                            BEGIN
                            select 0 Result
                            END ";

                        cmd.Parameters.AddWithValue("@DateFrom", _valueDate);
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

        public string PTPByDistributedIncomeRpt_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
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
                                    select ''   
                                    insert into #Text         
                                    Select  @CompanyID
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SInvestCode,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ValueDate, 112) <> '19000101' then CONVERT(VARCHAR(10), ValueDate, 112) else '' End),''))))          
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ExDate, 112) <> '19000101' then CONVERT(VARCHAR(10), ExDate, 112) else '' End),''))))          
                                    + '|' + case when A.CashAmount = 0 then '' else cast(isnull(floor(Round(A.CashAmount,0)),'')as nvarchar(100)) end
 
                                    from (      
                                    Select F.SInvestCode,DI.ValueDate,DI.ExDate, ROUND(CashAmount,12)CashAmount
                                    from DistributedIncome DI 
                                    left join Fund F on DI.FundPK = F.fundPK and f.Status=2         
                                    where ValueDate =  @ValueDate and selected = 1 and DI.status in (1,2)
                                    )A    
                                    Group by A.SInvestCode,A.ValueDate,A.ExDate, A.CashAmount
                                    order by A.SInvestCode Asc
                                    select * from #text          
                                    END ";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "Distributed_Income_PTP.txt";
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
                                    return Tools.HtmlSinvestTextPath + "Distributed_Income_PTP.txt";
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



    }
}