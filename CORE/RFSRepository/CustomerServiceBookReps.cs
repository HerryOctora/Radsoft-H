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
    public class CustomerServiceBookReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CustomerServiceBook] " +
                            "([CustomerServiceBookPK],[HistoryPK],[Status],[ClientType],[FundClientPK],[AskLine],[Message],[ClientName],[Email],[Solution],[Phone],[StatusMessage],[InternalComment],[IsNeedToReport],";

        string _paramaterCommand = "@ClientType,@FundClientPK,@AskLine,@Message,@ClientName,@Email,@Solution,@Phone,@StatusMessage,@InternalComment,@IsNeedToReport,";

        //2
        private CustomerServiceBook setCustomerServiceBook(SqlDataReader dr)
        {
            CustomerServiceBook M_CustomerServiceBook = new CustomerServiceBook();
            M_CustomerServiceBook.CustomerServiceBookPK = Convert.ToInt32(dr["CustomerServiceBookPK"]);
            M_CustomerServiceBook.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CustomerServiceBook.Status = Convert.ToInt32(dr["Status"]);
            M_CustomerServiceBook.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CustomerServiceBook.Notes = Convert.ToString(dr["Notes"]);
            M_CustomerServiceBook.ClientType = Convert.ToInt32(dr["ClientType"]);
            M_CustomerServiceBook.ClientTypeDesc = Convert.ToString(dr["ClientTypeDesc"]);
            M_CustomerServiceBook.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_CustomerServiceBook.AskLine = Convert.ToInt32(dr["AskLine"]);
            M_CustomerServiceBook.AskLineDesc = Convert.ToString(dr["AskLineDesc"]);
            M_CustomerServiceBook.Message = Convert.ToString(dr["Message"]);
            M_CustomerServiceBook.ClientName = Convert.ToString(dr["ClientName"]);
            M_CustomerServiceBook.Email = Convert.ToString(dr["Email"]);
            M_CustomerServiceBook.Solution = Convert.ToString(dr["Solution"]);
            M_CustomerServiceBook.Phone = Convert.ToString(dr["Phone"]);
            M_CustomerServiceBook.StatusMessage = Convert.ToInt32(dr["StatusMessage"]);
            M_CustomerServiceBook.StatusMessageDesc = Convert.ToString(dr["StatusMessageDesc"]);
            M_CustomerServiceBook.InternalComment = Convert.ToString(dr["InternalComment"]);
            M_CustomerServiceBook.IsNeedToReport = Convert.ToBoolean(dr["IsNeedToReport"]);
            M_CustomerServiceBook.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CustomerServiceBook.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CustomerServiceBook.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CustomerServiceBook.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CustomerServiceBook.DoneUsersID = dr["DoneUsersID"].ToString();
            M_CustomerServiceBook.UnDoneUsersID = dr["UnDoneUsersID"].ToString();
            M_CustomerServiceBook.DoneTime = dr["DoneTime"].ToString();
            M_CustomerServiceBook.UnDoneTime = dr["UnDoneTime"].ToString();
            M_CustomerServiceBook.EntryTime = dr["EntryTime"].ToString();
            M_CustomerServiceBook.UpdateTime = dr["UpdateTime"].ToString();
            M_CustomerServiceBook.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CustomerServiceBook.VoidTime = dr["VoidTime"].ToString();
            M_CustomerServiceBook.DBUserID = dr["DBUserID"].ToString();
            M_CustomerServiceBook.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CustomerServiceBook.LastUpdate = dr["LastUpdate"].ToString();
            M_CustomerServiceBook.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CustomerServiceBook;
        }

        public List<CustomerServiceBook> CustomerServiceBook_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustomerServiceBook> L_CustomerServiceBook = new List<CustomerServiceBook>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"

                            Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            case when A.ClientType=1 then A.ClientName else F.ID + ' - ' + F.Name end ClientName, MV2.DescOne ClientTypeDesc,MV4.DescOne AskLineDesc,MV5.DescOne StatusMessageDesc ,* from CustomerServiceBook A
                            left join MasterValue MV2 on A.ClientType = MV2.Code and MV2.ID = 'ClientType' and MV2.status = 2 
                            left join MasterValue MV4 on A.AskLine = MV4.Code and MV4.ID = 'AskLine' and MV4.status = 2 
                            left join MasterValue mv5 on A.StatusMessage = mv5.code and  mv5.ID = 'StatusMessage' and mv5.status = 2  
                            left join FundClient F on F.FundclientPK = A.FundClientPK and F.Status = 2                    
                             where A.status = @status 
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                            Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            case when A.ClientType=1 then A.ClientName else F.ID + ' - ' + F.Name end ClientName, MV2.DescOne ClientTypeDesc,MV4.DescOne AskLineDesc,MV5.DescOne StatusMessageDesc ,* from CustomerServiceBook A
                            left join MasterValue MV2 on A.ClientType = MV2.Code and MV2.ID = 'ClientType' and MV2.status = 2 
                            left join MasterValue MV4 on A.AskLine = MV4.Code and MV4.ID = 'AskLine' and MV4.status = 2 
                            left join MasterValue mv5 on A.StatusMessage = mv5.code and  mv5.ID = 'StatusMessage' and mv5.status = 2  
                            left join FundClient F on F.FundclientPK = A.FundClientPK and F.Status = 2     
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CustomerServiceBook.Add(setCustomerServiceBook(dr));
                                }
                            }
                            return L_CustomerServiceBook;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CustomerServiceBook_Add(CustomerServiceBook _CustomerServiceBook, bool _havePrivillege)
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
                                 "Select isnull(max(CustomerServiceBookPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from CustomerServiceBook";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CustomerServiceBook.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(CustomerServiceBookPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from CustomerServiceBook";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundClientPK", _CustomerServiceBook.FundClientPK);
                        cmd.Parameters.AddWithValue("@ClientType", _CustomerServiceBook.ClientType);
                        cmd.Parameters.AddWithValue("@AskLine", _CustomerServiceBook.AskLine);
                        cmd.Parameters.AddWithValue("@Message", _CustomerServiceBook.Message);
                        cmd.Parameters.AddWithValue("@ClientName", _CustomerServiceBook.ClientName);
                        cmd.Parameters.AddWithValue("@Email", _CustomerServiceBook.Email);
                        cmd.Parameters.AddWithValue("@Solution", _CustomerServiceBook.Solution);
                        cmd.Parameters.AddWithValue("@Phone", _CustomerServiceBook.Phone);
                        cmd.Parameters.AddWithValue("@StatusMessage", _CustomerServiceBook.StatusMessage);
                        cmd.Parameters.AddWithValue("@InternalComment", _CustomerServiceBook.InternalComment);
                        cmd.Parameters.AddWithValue("@IsNeedToReport", _CustomerServiceBook.IsNeedToReport);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _CustomerServiceBook.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CustomerServiceBook");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CustomerServiceBook_Update(CustomerServiceBook _CustomerServiceBook, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_CustomerServiceBook.CustomerServiceBookPK, _CustomerServiceBook.HistoryPK, "CustomerServiceBook");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CustomerServiceBook set status=2,Notes=@Notes,FundClientPK=@FundClientPK,ClientType=@ClientType,AskLine=@AskLine,Message=@Message,ClientName=@ClientName,Email=@Email,Solution=@Solution,Phone=@Phone,StatusMessage=@StatusMessage,InternalComment=@InternalComment,IsNeedToReport=@IsNeedToReport," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where CustomerServiceBookPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _CustomerServiceBook.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                            cmd.Parameters.AddWithValue("@Notes", _CustomerServiceBook.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _CustomerServiceBook.FundClientPK);
                            cmd.Parameters.AddWithValue("@ClientType", _CustomerServiceBook.ClientType);
                            cmd.Parameters.AddWithValue("@AskLine", _CustomerServiceBook.AskLine);
                            cmd.Parameters.AddWithValue("@Message", _CustomerServiceBook.Message);
                            cmd.Parameters.AddWithValue("@ClientName", _CustomerServiceBook.ClientName);
                            cmd.Parameters.AddWithValue("@Email", _CustomerServiceBook.Email);
                            cmd.Parameters.AddWithValue("@Solution", _CustomerServiceBook.Solution);
                            cmd.Parameters.AddWithValue("@Phone", _CustomerServiceBook.Phone);
                            cmd.Parameters.AddWithValue("@StatusMessage", _CustomerServiceBook.StatusMessage);
                            cmd.Parameters.AddWithValue("@InternalComment", _CustomerServiceBook.InternalComment);
                            cmd.Parameters.AddWithValue("@IsNeedToReport", _CustomerServiceBook.IsNeedToReport);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _CustomerServiceBook.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CustomerServiceBook.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CustomerServiceBook set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CustomerServiceBookPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _CustomerServiceBook.EntryUsersID);
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
                                cmd.CommandText = "Update CustomerServiceBook set Notes=@Notes,FundClientPK=@FundClientPK,ClientType=@ClientType,AskLine=@AskLine,Message=@Message,ClientName=@ClientName,Email=@Email,Solution=@Solution,Phone=@Phone,StatusMessage=@StatusMessage,InternalComment=@InternalComment,IsNeedToReport=@IsNeedToReport," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where CustomerServiceBookPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _CustomerServiceBook.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                                cmd.Parameters.AddWithValue("@Notes", _CustomerServiceBook.Notes);
                                cmd.Parameters.AddWithValue("@FundClientPK", _CustomerServiceBook.FundClientPK);
                                cmd.Parameters.AddWithValue("@ClientType", _CustomerServiceBook.ClientType);
                                cmd.Parameters.AddWithValue("@AskLine", _CustomerServiceBook.AskLine);
                                cmd.Parameters.AddWithValue("@Message", _CustomerServiceBook.Message);
                                cmd.Parameters.AddWithValue("@ClientName", _CustomerServiceBook.ClientName);
                                cmd.Parameters.AddWithValue("@Email", _CustomerServiceBook.Email);
                                cmd.Parameters.AddWithValue("@Solution", _CustomerServiceBook.Solution);
                                cmd.Parameters.AddWithValue("@Phone", _CustomerServiceBook.Phone);
                                cmd.Parameters.AddWithValue("@StatusMessage", _CustomerServiceBook.StatusMessage);
                                cmd.Parameters.AddWithValue("@InternalComment", _CustomerServiceBook.InternalComment);
                                cmd.Parameters.AddWithValue("@IsNeedToReport", _CustomerServiceBook.IsNeedToReport);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CustomerServiceBook.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_CustomerServiceBook.CustomerServiceBookPK, "CustomerServiceBook");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CustomerServiceBook where CustomerServiceBookPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CustomerServiceBook.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _CustomerServiceBook.FundClientPK);
                                cmd.Parameters.AddWithValue("@ClientType", _CustomerServiceBook.ClientType);
                                cmd.Parameters.AddWithValue("@ClientName", _CustomerServiceBook.ClientName);
                                cmd.Parameters.AddWithValue("@AskLine", _CustomerServiceBook.AskLine);
                                cmd.Parameters.AddWithValue("@Message", _CustomerServiceBook.Message);
                                cmd.Parameters.AddWithValue("@Email", _CustomerServiceBook.Email);
                                cmd.Parameters.AddWithValue("@Solution", _CustomerServiceBook.Solution);
                                cmd.Parameters.AddWithValue("@Phone", _CustomerServiceBook.Phone);
                                cmd.Parameters.AddWithValue("@StatusMessage", _CustomerServiceBook.StatusMessage);
                                cmd.Parameters.AddWithValue("@InternalComment", _CustomerServiceBook.InternalComment);
                                cmd.Parameters.AddWithValue("@IsNeedToReport", _CustomerServiceBook.IsNeedToReport);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CustomerServiceBook.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CustomerServiceBook set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where CustomerServiceBookPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _CustomerServiceBook.Notes);
                                cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CustomerServiceBook.HistoryPK);
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

        public void CustomerServiceBook_Approved(CustomerServiceBook _CustomerServiceBook)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustomerServiceBook set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where CustomerServiceBookPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CustomerServiceBook.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _CustomerServiceBook.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CustomerServiceBook set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CustomerServiceBookPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CustomerServiceBook.ApprovedUsersID);
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

        public void CustomerServiceBook_Reject(CustomerServiceBook _CustomerServiceBook)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustomerServiceBook set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CustomerServiceBookPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CustomerServiceBook.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CustomerServiceBook.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CustomerServiceBook set status= 2,lastupdate=@lastupdate where CustomerServiceBookPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
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

        public void CustomerServiceBook_Void(CustomerServiceBook _CustomerServiceBook)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustomerServiceBook set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where CustomerServiceBookPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CustomerServiceBook.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CustomerServiceBook.VoidUsersID);
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


        public void CustomerServiceBook_CheckDone(CustomerServiceBook _CustomerServiceBook)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_CustomerServiceBook.Param == 2)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustomerServiceBook set StatusMessage = 2, DoneUsersID = @DoneUsersID, DoneTime = @DoneTime, LastUpdate = @LastUpdate " +
                            "where CustomerServiceBookPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CustomerServiceBook.HistoryPK);
                        cmd.Parameters.AddWithValue("@DoneUsersID", _CustomerServiceBook.DoneUsersID);
                        cmd.Parameters.AddWithValue("@DoneTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    }
                    else if (_CustomerServiceBook.Param == 3)
                    {
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustomerServiceBook set StatusMessage = 3, UnDoneUsersID = @UnDoneUsersID,UnDoneTime = @UnDoneTime, LastUpdate = @LastUpdate  " +
                            "where CustomerServiceBookPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CustomerServiceBook.CustomerServiceBookPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CustomerServiceBook.HistoryPK);
                        cmd.Parameters.AddWithValue("@UnDoneUsersID", _CustomerServiceBook.UnDoneUsersID);
                        cmd.Parameters.AddWithValue("@UnDoneTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    }
                    
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public GetCustomerHistory CustomerServiceBook_GetCustomerCombo(int _fundclientPK, int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GetCustomerHistory> L_CustomerServiceBook = new List<GetCustomerHistory>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                            cmd.CommandText = @"
                            Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            case when A.ClientType=1 then A.ClientName else F.ID + ' - ' + F.Name end ClientName, MV2.DescOne ClientTypeDesc,MV4.DescOne AskLineDesc,MV5.DescOne StatusMessageDesc 
                            ,* from CustomerServiceBook A
                            left join MasterValue MV2 on A.ClientType = MV2.Code and MV2.ID = 'ClientType' and MV2.status = 2 
                            left join MasterValue MV4 on A.AskLine = MV4.Code and MV4.ID = 'AskLine' and MV4.status = 2 
                            left join MasterValue mv5 on A.StatusMessage = mv5.code and  mv5.ID = 'StatusMessage' and mv5.status = 2  
                            left join FundClient F on F.FundclientPK = A.FundClientPK and F.Status in (1,2)                    
                            where A.status = @status and A.FundclientPK = @FundclientPK
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                            cmd.Parameters.AddWithValue("@FundclientPK", _fundclientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                    return new GetCustomerHistory()
                                    {
                                        CustomerServiceBookPK = Convert.ToInt32(dr["CustomerServiceBookPK"]),
                                        HistoryPK = Convert.ToInt32(dr["HistoryPK"]),
                                        Status = Convert.ToInt32(dr["Status"]),
                                        StatusDesc = Convert.ToString(dr["StatusDesc"]),
                                        FundClientDesc = Convert.ToString(dr["ClientName"]),
                                        ClientName = Convert.ToString(dr["ClientName"]),
                                        ClientType = Convert.ToInt32(dr["ClientType"]),
                                        ClientTypeDesc = Convert.ToString(dr["ClientTypeDesc"]),
                                        AskLine = Convert.ToInt32(dr["AskLine"]),
                                        AskLineDesc = Convert.ToString(dr["AskLineDesc"]),
                                        StatusMessage = Convert.ToInt32(dr["StatusMessage"]),
                                        StatusMessageDesc = Convert.ToString(dr["StatusMessageDesc"]),
                                        FundClientPK = Convert.ToInt32(dr["FundClientPK"]),
                                        Message = Convert.ToString(dr["Message"]),
                                        Email = Convert.ToString(dr["Email"]),
                                        Solution = Convert.ToString(dr["Solution"]),
                                        Phone = Convert.ToString(dr["Phone"]),
                                        InternalComment = Convert.ToString(dr["InternalComment"]),
                                        DoneUsersID = Convert.ToString(dr["DoneUsersID"]),
                                        DoneTime = Convert.ToString(dr["DoneTime"]),
                                        UnDoneUsersID = Convert.ToString(dr["UnDoneUsersID"]),
                                        UnDoneTime = Convert.ToString(dr["UnDoneTime"]),
                                        EntryUsersID = Convert.ToString(dr["EntryUsersID"]),
                                        EntryTime = Convert.ToString(dr["EntryTime"]),
                                    };
                            }
                           else
                            {
                                return new GetCustomerHistory()
                                {
                                    CustomerServiceBookPK = 0,
                                    HistoryPK = 0,
                                    Status = 0,
                                    StatusDesc = "",
                                    FundClientDesc = "",
                                    ClientName = "",
                                    ClientType = 0,
                                    ClientTypeDesc = "",
                                    AskLine = 0,
                                    AskLineDesc = "",
                                    StatusMessage = 0,
                                    StatusMessageDesc = "",
                                    FundClientPK = 0,
                                    Message = "",
                                    Email = "",
                                    Solution = "",
                                    Phone = "",
                                    InternalComment = "",
                                    DoneUsersID = "",
                                    DoneTime = "",
                                    UnDoneUsersID = "",
                                    UnDoneTime = "",
                                    EntryUsersID = "",
                                    EntryTime = "",
                                };
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