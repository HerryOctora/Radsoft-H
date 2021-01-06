using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class RegulerInstructionReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();
        //1
        string _insertCommand = "INSERT INTO [dbo].[RegulerInstruction] " +
                            "([RegulerInstructionPK],[HistoryPK],[Status],[FundClientPK],[FundPK],[FundCashRefPK],[TrxType],[AutoDebitDate],[GrossAmount],[FeePercent],[FeeAmount],[NetAmount],[StartingDate],[ExpirationDate],[BankRecipientPK], ";
        string _paramaterCommand = "@FundClientPK,@FundPK,@FundCashRefPK,@TrxType,@AutoDebitDate,@GrossAmount,@FeePercent,@FeeAmount,@NetAmount,@StartingDate,@ExpirationDate,@BankRecipientPK, ";

        //2
        private RegulerInstruction setRegulerInstruction(SqlDataReader dr)
        {
            RegulerInstruction M_RegulerInstruction = new RegulerInstruction();
            M_RegulerInstruction.RegulerInstructionPK = Convert.ToInt32(dr["RegulerInstructionPK"]);
            M_RegulerInstruction.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RegulerInstruction.Status = Convert.ToInt32(dr["Status"]);
            M_RegulerInstruction.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RegulerInstruction.Notes = Convert.ToString(dr["Notes"]);
            M_RegulerInstruction.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_RegulerInstruction.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_RegulerInstruction.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_RegulerInstruction.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_RegulerInstruction.FundID = Convert.ToString(dr["FundID"]);
            M_RegulerInstruction.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
            M_RegulerInstruction.FundCashRefID = Convert.ToString(dr["FundCashRefID"]);
            M_RegulerInstruction.TrxType = Convert.ToInt32(dr["TrxType"]);
            M_RegulerInstruction.TrxTypeDesc = Convert.ToString(dr["TrxTypeDesc"]);
            M_RegulerInstruction.AutoDebitDate = Convert.ToInt32(dr["AutoDebitDate"]);
            M_RegulerInstruction.GrossAmount = Convert.ToDecimal(dr["GrossAmount"]);
            M_RegulerInstruction.FeePercent = Convert.ToDecimal(dr["FeePercent"]);
            M_RegulerInstruction.FeeAmount = Convert.ToDecimal(dr["FeeAmount"]);
            M_RegulerInstruction.NetAmount = Convert.ToDecimal(dr["NetAmount"]);
            M_RegulerInstruction.StartingDate = dr["StartingDate"].ToString();
            M_RegulerInstruction.ExpirationDate = dr["ExpirationDate"].ToString();
            M_RegulerInstruction.BankRecipientPK = Convert.ToInt32(dr["BankRecipientPK"]);
            M_RegulerInstruction.BankRecipientDesc = Convert.ToString(dr["BankRecipientDesc"]);
            M_RegulerInstruction.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RegulerInstruction.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RegulerInstruction.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RegulerInstruction.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RegulerInstruction.EntryTime = dr["EntryTime"].ToString();
            M_RegulerInstruction.UpdateTime = dr["UpdateTime"].ToString();
            M_RegulerInstruction.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RegulerInstruction.VoidTime = dr["VoidTime"].ToString();
            M_RegulerInstruction.DBUserID = dr["DBUserID"].ToString();
            M_RegulerInstruction.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RegulerInstruction.LastUpdate = dr["LastUpdate"].ToString();
            M_RegulerInstruction.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_RegulerInstruction;
        }

        public List<RegulerInstruction> RegulerInstruction_Select(int _status)
        {
          
          try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RegulerInstruction> L_regulerInstruction = new List<RegulerInstruction>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when RI.status=1 then 'PENDING' else Case When RI.status = 2 then 'APPROVED' else Case when RI.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                              case when BankRecipientPK=1 then FC.NamaBank1 else case when BankRecipientPK= 2 then FC.NamaBank2  else FC.namaBank3 end  end BankRecipientDesc,                               
                              case when RI.TrxType = 1 then 'Subscription' else  'Redemption' END TrxTypeDesc, FC.ID FundClientID,FC.Name FundClientName,F.ID FundID,FCR.ID FundCashRefID,RI.* from RegulerInstruction RI left join
                              FundClient FC on RI.FundClientPK = FC.FundClientPK and FC.status = 2 left join 
                              Fund F on RI.FundPK = F.FundPK and F.status = 2 left join 
                              FundCashRef FCR on RI.FundCashRefPK = FCR.FundCashRefPK and FCR.status = 2  
                              where RI.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when RI.status=1 then 'PENDING' else Case When RI.status = 2 then 'APPROVED' else Case when RI.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                              case when BankRecipientPK=1 then FC.NamaBank1 else case when BankRecipientPK= 2 then FC.NamaBank2  else FC.namaBank3 end  end BankRecipientDesc,                               
                              case when RI.TrxType=1 then 'Subscription' else  'Redemption' END TrxTypeDesc, FC.ID FundClientID,FC.Name FundClientName,F.ID FundID,FCR.ID FundCashRefID, RI.* from RegulerInstruction RI left join 
                              FundClient FC on RI.FundClientPK = FC.FundClientPK and FC.status = 2 left join 
                              Fund F on RI.FundPK = F.FundPK and F.status = 2 left join 
                              FundCashRef FCR on RI.FundCashRefPK = FCR.FundCashRefPK and FCR.status = 2  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_regulerInstruction.Add(setRegulerInstruction(dr));
                                }
                            }
                            return L_regulerInstruction;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RegulerInstruction_Add(RegulerInstruction _regulerInstruction, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(RegulerInstructionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from RegulerInstruction";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _regulerInstruction.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(RegulerInstructionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from RegulerInstruction";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundClientPK", _regulerInstruction.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _regulerInstruction.FundPK);
                        cmd.Parameters.AddWithValue("@FundCashRefPK", _regulerInstruction.FundCashRefPK);
                        cmd.Parameters.AddWithValue("@TrxType", _regulerInstruction.TrxType);
                        cmd.Parameters.AddWithValue("@AutoDebitDate", _regulerInstruction.AutoDebitDate);
                        cmd.Parameters.AddWithValue("@GrossAmount", _regulerInstruction.GrossAmount);
                        cmd.Parameters.AddWithValue("@FeePercent", _regulerInstruction.FeePercent);
                        cmd.Parameters.AddWithValue("@FeeAmount", _regulerInstruction.FeeAmount);
                        cmd.Parameters.AddWithValue("@NetAmount", _regulerInstruction.NetAmount);
                        cmd.Parameters.AddWithValue("@StartingDate", _regulerInstruction.StartingDate);
                        cmd.Parameters.AddWithValue("@ExpirationDate", _regulerInstruction.ExpirationDate);
                        cmd.Parameters.AddWithValue("@BankRecipientPK", _regulerInstruction.BankRecipientPK);      
                        cmd.Parameters.AddWithValue("@EntryUsersID", _regulerInstruction.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RegulerInstruction");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

  

        public int RegulerInstruction_Update(RegulerInstruction _regulerInstruction, bool _havePrivillege)
        {
          
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_regulerInstruction.RegulerInstructionPK, _regulerInstruction.HistoryPK, "RegulerInstruction");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RegulerInstruction set status=2,Notes=@Notes,FundClientPK=@FundClientPK,FundPK=@FundPK,FundCashRefPK=@FundCashRefPK,AutoDebitDate=@AutoDebitDate,TrxType=@TrxType, " +
                              "GrossAmount=@GrossAmount,FeePercent=@FeePercent,FeeAmount=@FeeAmount,NetAmount=@NetAmount,BankRecipientPK=@BankRecipientPK,StartingDate=@StartingDate, ExpirationDate=@ExpirationDate," +
                              "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,lastupdate=@lastupdate " +
                              "where RegulerInstructionPK = @PK and  historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _regulerInstruction.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                            cmd.Parameters.AddWithValue("@Notes", _regulerInstruction.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _regulerInstruction.FundClientPK);
                            cmd.Parameters.AddWithValue("@FundPK", _regulerInstruction.FundPK);
                            cmd.Parameters.AddWithValue("@FundCashRefPK", _regulerInstruction.FundCashRefPK);
                            cmd.Parameters.AddWithValue("@TrxType", _regulerInstruction.TrxType);
                            cmd.Parameters.AddWithValue("@AutoDebitDate", _regulerInstruction.AutoDebitDate);
                            cmd.Parameters.AddWithValue("@GrossAmount", _regulerInstruction.GrossAmount);
                            cmd.Parameters.AddWithValue("@FeePercent", _regulerInstruction.FeePercent);
                            cmd.Parameters.AddWithValue("@FeeAmount", _regulerInstruction.FeeAmount);
                            cmd.Parameters.AddWithValue("@NetAmount", _regulerInstruction.NetAmount);
                            cmd.Parameters.AddWithValue("@StartingDate", _regulerInstruction.StartingDate);
                            cmd.Parameters.AddWithValue("@ExpirationDate", _regulerInstruction.ExpirationDate);
                            cmd.Parameters.AddWithValue("@BankRecipientPK", _regulerInstruction.BankRecipientPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _regulerInstruction.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _regulerInstruction.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RegulerInstruction set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime ,lastupdate=@lastupdate where RegulerInstructionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _regulerInstruction.EntryUsersID);
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
                                cmd.CommandText = "Update RegulerInstruction set Notes=@Notes,FundClientPK=@FundClientPK,FundPK=@FundPK,FundCashRefPK=@FundCashRefPK,TrxType=@TrxType,AutoDebitDate=@AutoDebitDate," +
                                  "GrossAmount=@GrossAmount,FeePercent=@FeePercent,FeeAmount=@FeeAmount,NetAmount=@NetAmount,StartingDate=@StartingDate, ExpirationDate=@ExpirationDate,BankRecipientPK=@BankRecipientPK," +
                                  "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,lastupdate=@lastupdate " +
                                  "where RegulerInstructionPK = @PK and  historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _regulerInstruction.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                                cmd.Parameters.AddWithValue("@Notes", _regulerInstruction.Notes);
                                cmd.Parameters.AddWithValue("@FundClientPK", _regulerInstruction.FundClientPK);
                                cmd.Parameters.AddWithValue("@FundPK", _regulerInstruction.FundPK);
                                cmd.Parameters.AddWithValue("@FundCashRefPK", _regulerInstruction.FundCashRefPK);
                                cmd.Parameters.AddWithValue("@TrxType", _regulerInstruction.TrxType);
                                cmd.Parameters.AddWithValue("@AutoDebitDate", _regulerInstruction.AutoDebitDate);
                                cmd.Parameters.AddWithValue("@GrossAmount", _regulerInstruction.GrossAmount);
                                cmd.Parameters.AddWithValue("@FeePercent", _regulerInstruction.FeePercent);
                                cmd.Parameters.AddWithValue("@FeeAmount", _regulerInstruction.FeeAmount);
                                cmd.Parameters.AddWithValue("@NetAmount", _regulerInstruction.NetAmount);
                                cmd.Parameters.AddWithValue("@StartingDate", _regulerInstruction.StartingDate);
                                cmd.Parameters.AddWithValue("@ExpirationDate", _regulerInstruction.ExpirationDate);
                                cmd.Parameters.AddWithValue("@BankRecipientPK", _regulerInstruction.BankRecipientPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _regulerInstruction.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_regulerInstruction.RegulerInstructionPK, "RegulerInstruction");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From RegulerInstruction where RegulerInstructionPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _regulerInstruction.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _regulerInstruction.FundClientPK);
                                cmd.Parameters.AddWithValue("@FundPK", _regulerInstruction.FundPK);
                                cmd.Parameters.AddWithValue("@FundCashRefPK", _regulerInstruction.FundCashRefPK);
                                cmd.Parameters.AddWithValue("@TrxType", _regulerInstruction.TrxType);
                                cmd.Parameters.AddWithValue("@AutoDebitDate", _regulerInstruction.AutoDebitDate);
                                cmd.Parameters.AddWithValue("@GrossAmount", _regulerInstruction.GrossAmount);
                                cmd.Parameters.AddWithValue("@FeePercent", _regulerInstruction.FeePercent);
                                cmd.Parameters.AddWithValue("@FeeAmount", _regulerInstruction.FeeAmount);
                                cmd.Parameters.AddWithValue("@NetAmount", _regulerInstruction.NetAmount);
                                cmd.Parameters.AddWithValue("@StartingDate", _regulerInstruction.StartingDate);
                                cmd.Parameters.AddWithValue("@ExpirationDate", _regulerInstruction.ExpirationDate);
                                cmd.Parameters.AddWithValue("@BankRecipientPK", _regulerInstruction.BankRecipientPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _regulerInstruction.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RegulerInstruction set status= 4,Notes=@Notes, " +
                                    " lastupdate=@lastupdate where RegulerInstructionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _regulerInstruction.Notes);
                                cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _regulerInstruction.HistoryPK);
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

        public void RegulerInstruction_Approved(RegulerInstruction _regulerInstruction)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RegulerInstruction set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,lastupdate=@lastupdate " +
                            "where RegulerInstructionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _regulerInstruction.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _regulerInstruction.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RegulerInstruction set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime ,lastupdate=@lastupdate where RegulerInstructionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _regulerInstruction.ApprovedUsersID);
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

        public void RegulerInstruction_Reject(RegulerInstruction _regulerInstruction)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RegulerInstruction set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,lastupdate=@lastupdate " +
                            "where RegulerInstructionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _regulerInstruction.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _regulerInstruction.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RegulerInstruction set status= 2,lastupdate=@lastupdate where RegulerInstructionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
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

        public void RegulerInstruction_Void(RegulerInstruction _regulerInstruction)
        {
               try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RegulerInstruction set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where RegulerInstructionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _regulerInstruction.RegulerInstructionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _regulerInstruction.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _regulerInstruction.VoidUsersID);
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

      

        public List<RegulerInstruction> Get_AutoDebitDateFromRegulerInstruction()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RegulerInstruction> L_RegulerInstruction = new List<RegulerInstruction>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "SELECT  distinct AutoDebitDate from RegulerInstruction where status = 2 ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    RegulerInstruction M_RegulerInstruction = new RegulerInstruction();
                                    M_RegulerInstruction.AutoDebitDate = Convert.ToInt32(dr["AutoDebitDate"]);
                                    L_RegulerInstruction.Add(M_RegulerInstruction);
                                }
                            }

                            return L_RegulerInstruction;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string Generate_DataRegulerInstructionToClientSubscription(RegulerInstruction _regulerInstruction)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                      DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " declare @ValueDate datetime " +
                        " select @ValueDate = '' + cast(month(getdate()) as nvarchar(2)) + '' + '/' + cast(@AutoDebitDate as nvarchar(2)) + '/' + cast(year(getdate()) as nvarchar(4)) + '' " +
                        " select * from ClientSubscription where ValueDate = [dbo].[FNextWorkingDayRHB](@ValueDate,1) and Status in (1,2) and Type = 2";

                        cmd.Parameters.AddWithValue("@AutoDebitDate", _regulerInstruction.AutoDebitDate);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return "Generate Cancel, Already Generate";
                            }
                            else
                            {
                                using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                {
                                    DbCon1.Open();
                                    using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                    {

                                        cmd1.CommandText =
                                        " Declare @FundClientPK int " +
                                        " Declare @AgentPK int " +
                                        " Declare @FundPK int " +
                                        " Declare @GrossAmount numeric(22,4) " +
                                        " Declare @FeePercent numeric(18,8) " +
                                        " Declare @FeeAmount numeric(18,4) " +
                                        " Declare @NetAmount numeric(22,4) " +
                                        " Declare @BankName nvarchar(50) " +
                                        " Declare @BankAccountNo nvarchar(50) " +
                                        " Declare @ClientSubscriptionPK int " +
                                        " declare @ValueDate datetime " +
                                          " \n  " +
                                        " select @ValueDate = '' + cast(month(getdate()) as nvarchar(2)) + '' + '/' + cast(@AutoDebitDate as nvarchar(2)) + '/' + cast(year(getdate()) as nvarchar(4)) + '' " +
                                        " DECLARE A CURSOR FOR  " +
                                        " 	Select R.FundClientPK,FundPK,AutoDebitDate,FC.SellingAgentPK From RegulerInstruction R left join FundClient FC on R.FundClientPK = FC.FundClientPK and FC.status = 2 " +
                                        " 	where AutoDebitDate = @AutoDebitDate and R.status = 2 order by FC.Name " +
                                        " Open A " +
                                        " Fetch Next From A " +
                                        " Into @FundClientPK,@FundPK,@AutoDebitDate,@AgentPK " +
                                            " \n  " +
                                        " While @@FETCH_STATUS = 0 " +
                                            " \n " +
                                        " Begin " +
                                        " select @ClientSubscriptionPK = isnull(max(ClientSubscriptionPK),0)+ 1 from ClientSubscription " +
                                        " INSERT INTO [dbo].[ClientSubscription] " +
                                        "([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate]," +
                                        " [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount]," +
                                        " [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[SRP_INFO],[Type],[AutoDebitDate],[EntryUsersID],[EntryTime],[LastUpdate])" +
                                        " select @ClientSubscriptionPK,1,1,[dbo].[FNextWorkingDayRHB](@ValueDate,1),[dbo].[FNextWorkingDayRHB](@ValueDate,1)," +
                                        " 0,FundPK,@FundClientPK,FundCashRefPK,1,BankName,GrossAmount,0,NetAmount,0, " +
                                        " FeePercent,FeeAmount,@AgentPK,0,0,BankAccountNo,2,@ValueDate,@EntryUsersID,@EntryTime,@LastUpdate " +
                                        " From RegulerInstruction where AutoDebitDate = @AutoDebitDate and FundClientPK =@FundClientPK and FundPK = @FundPK and status = 2 " +
                                        " Fetch next From A Into @FundClientPK,@FundPK,@AutoDebitDate,@AgentPK " +
                                        " end " +
                                        " Close A " +
                                        " Deallocate A ";

                                        cmd1.Parameters.AddWithValue("@AutoDebitDate", _regulerInstruction.AutoDebitDate);
                                        cmd1.Parameters.AddWithValue("@EntryUsersID", _regulerInstruction.EntryUsersID);
                                        cmd1.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                                        cmd1.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                        cmd1.ExecuteNonQuery();


                                    }
                                }
                                return "Generate Regular Insctruction Success";
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
    }
}




