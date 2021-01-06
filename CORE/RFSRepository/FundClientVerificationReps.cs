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
    public class FundClientVerificationReps
    {
        Host _host = new Host();
        private FundClientVerification setFundClientVerification(SqlDataReader dr)
        {
            FundClientVerification M_model = new FundClientVerification();
            M_model.FundClientVerificationPK = Convert.ToInt32(dr["FundClientVerificationPK"]);
            M_model.Selected = Convert.ToBoolean(dr["Selected"]);
            M_model.TrxPK = Convert.ToInt32(dr["TrxPK"]);
            M_model.Name = Convert.ToString(dr["Name"]);
            M_model.ImgOri = Convert.ToString(dr["ImgOri"]);
            M_model.TransactionDate = Convert.ToString(dr["TransactionDate"]);
            M_model.TransactionID = Convert.ToString(dr["TransactionID"]);
            M_model.NAVDate = Convert.ToString(dr["NAVDate"]);
            M_model.FundID = Convert.ToString(dr["FundID"]);
            M_model.BankName = Convert.ToString(dr["BankName"]);
            M_model.AccountName = Convert.ToString(dr["AccountName"]);
            M_model.AccountNo = Convert.ToString(dr["AccountNo"]);
            M_model.Amount = Convert.ToDecimal(dr["Amount"]);
            M_model.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_model.Status = Convert.ToInt32(dr["Status"]);
            M_model.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_model.ApprovedTime = dr["ApprovedTime"].ToString();
            M_model.LastUpdate = dr["LastUpdate"].ToString();
            return M_model;
        }

        public List<FundClientVerification> FundClientVerification_Select(string _usersID, int _status)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientVerification> L_list = new List<FundClientVerification>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            Select  isnull(D.ClientSubscriptionPK,0) TrxPK,A.TransactionID,A.FundClientVerificationPK ID,C.ID + '- ' +
                            C.Name Name,A.TransactionDate,D.NAVDate,E.ID FundID,A.BankName,A.AccountName,A.AccountNo,A.Amount,ImgOri,* from FundClientVerification A
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                            left join ModoitTransaction B on A.TransactionID = B.MasterTransactionPK
                            left join ClientSubscription D on B.TransactionPK = D.TransactionPK and D.status in(1,2)
                            left join Fund E on E.FundPK = D.FundPK and E.status = 2
                            where A.status = @status and isnull(D.ClientSubscriptionPK,0) <> 0
                        ";
                        cmd.Parameters.AddWithValue("@Status", _status);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_list.Add(setFundClientVerification(dr));
                                }
                            }
                            return L_list;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void FundClientVerification_ApproveBySelected(string _usersID, string _permissionID)
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
                        Select @Time,@PermissionID,'FundClientVerification',FundcLientVerificationPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from FundClientVerification where Status = 1 and Selected  = 1 
                       
                        update FundClientVerification set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and FundClientVerificationPK in ( Select FundClientVerificationPK from FundClientVerification where Status = 1 and Selected  = 1 ) 
                        
                        --Update FundClientVerification set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        --where status = 4 and ID in (Select ID from FundClientVerification where Status = 4 and Selected  = 1)   

                        ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    //var data = BackOffice.ExtractNavTable(FoConnection.Authentication());
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

    }
}