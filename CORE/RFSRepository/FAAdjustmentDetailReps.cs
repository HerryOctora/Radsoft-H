using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class FAAdjustmentDetailReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[FAAdjustmentDetail] " +
                            "([HistoryPK],[FAAdjustmentPK],[AutoNo],[Status],[FundPK],[FACOAAdjustmentPK],[FundJournalAccountPK],[DebitCredit],[Amount],[LastUsersID],[LastUpdate])";


        string _paramaterCommand = "@FundPK,@FACOAAdjustmentPK,@FundJournalAccountPK,@DebitCredit,@Amount,@LastUsersID,@LastUpdate";

        //2
        private FAAdjustmentDetail setFAAdjustmentDetail(SqlDataReader dr)
        {
            FAAdjustmentDetail M_FAAdjustmentDetail = new FAAdjustmentDetail();
            M_FAAdjustmentDetail.FAAdjustmentPK = Convert.ToInt32(dr["FAAdjustmentPK"]);
            M_FAAdjustmentDetail.AutoNo = Convert.ToInt32(dr["AutoNo"]);
            M_FAAdjustmentDetail.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FAAdjustmentDetail.Status = Convert.ToInt32(dr["Status"]);
            M_FAAdjustmentDetail.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FAAdjustmentDetail.FundID = Convert.ToString(dr["FundID"]);
            M_FAAdjustmentDetail.FundName = Convert.ToString(dr["FundName"]);
            M_FAAdjustmentDetail.FACOAAdjustmentPK = Convert.ToInt32(dr["FACOAAdjustmentPK"]);
            M_FAAdjustmentDetail.FACOAAdjustmentID = Convert.ToString(dr["FACOAAdjustmentID"]);
            M_FAAdjustmentDetail.FACOAAdjustmentName = Convert.ToString(dr["FACOAAdjustmentName"]);
            M_FAAdjustmentDetail.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_FAAdjustmentDetail.FundJournalAccountID = Convert.ToString(dr["FundJournalAccountID"]);
            M_FAAdjustmentDetail.FundJournalAccountName = Convert.ToString(dr["FundJournalAccountName"]);
            M_FAAdjustmentDetail.DebitCredit = Convert.ToString(dr["DebitCredit"]);
            M_FAAdjustmentDetail.Amount = Convert.ToDecimal(dr["Amount"]);
            M_FAAdjustmentDetail.LastUsersID = Convert.ToString(dr["LastUsersID"]);
            M_FAAdjustmentDetail.LastUpdate = dr["LastUpdate"].ToString();
            M_FAAdjustmentDetail.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FAAdjustmentDetail;
        }

        //3
        public List<FAAdjustmentDetail> FAAdjustmentDetail_Select(int _status, int _FAAdjustmentPK)
        {
            
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FAAdjustmentDetail> L_FAAdjustmentDetail = new List<FAAdjustmentDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select B.ID FACOAAdjustmentID, B.Name FACOAAdjustmentName,C.ID FundJournalAccountID,C.Name FundJournalAccountName,D.ID FundID,D.Name FundName,A.* from FAAdjustmentDetail A 
                            left join FACOAAdjustment B on A.FACOAAdjustmentPK = B.FACOAAdjustmentPK and B.Status = 2
                            left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.Status = 2  
                            left join Fund D on A.FundPK = D.FundPK and D.Status = 2 
                            where FAAdjustmentPK = @FAAdjustmentPK and A.Status = @Status         
                            order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustmentPK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select B.ID FACOAAdjustmentID, B.Name FACOAAdjustmentName,C.ID FundJournalAccountID,C.Name FundJournalAccountName,D.ID FundID,D.Name FundName,A.* from FAAdjustmentDetail A 
                            left join FACOAAdjustment B on A.FACOAAdjustmentPK = B.FACOAAdjustmentPK and B.Status = 2
                            left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.Status = 2  
                            left join Fund D on A.FundPK = D.FundPK and D.Status = 2 
                            where FAAdjustmentPK = @FAAdjustmentPK       
                            order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustmentPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FAAdjustmentDetail.Add(setFAAdjustmentDetail(dr));
                                }
                            }
                            return L_FAAdjustmentDetail;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        //4
        public int FAAdjustmentDetail_Add(FAAdjustmentDetail _FAAdjustmentDetail)
        {
            try
            {
                int _autoNo = 0;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _autoNo = _host.Get_DetailNewAutoNo(_FAAdjustmentDetail.FAAdjustmentPK,"FAAdjustmentDetail","FAAdjustmentPK");
                        cmd.CommandText =
                                  "update FAAdjustment set lastupdate = @Lastupdate where FAAdjustmentPK = @FAAdjustmentPK and status = 1 \n " +
                                  "update FAAdjustment set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where FAAdjustmentPK = @FAAdjustmentPK and status = 2 \n " +
                                    _insertCommand +
                                 " Select 1,@FAAdjustmentPK,@AutoNo,@status," + _paramaterCommand;
                        cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustmentDetail.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@status", _FAAdjustmentDetail.Status);
                        cmd.Parameters.AddWithValue("@AutoNo", _autoNo);
                        cmd.Parameters.AddWithValue("@FundPK", _FAAdjustmentDetail.FundPK);
                        cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FAAdjustmentDetail.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FAAdjustmentDetail.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@DebitCredit", _FAAdjustmentDetail.DebitCredit);
                        cmd.Parameters.AddWithValue("@Amount", _FAAdjustmentDetail.Amount);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FAAdjustmentDetail.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();
                        return _autoNo;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        //Update
        public void FAAdjustmentDetail_Update(FAAdjustmentDetail _FAAdjustmentDetail)
        {
             try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "update FAAdjustment set status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate, lastupdate = @Lastupdate where FAAdjustmentPK = @FAAdjustmentPK and status = 2 \n "+
                            "Update FAAdjustmentDetail " +
                            "Set FundPK=@FundPK,FACOAAdjustmentPK = @FACOAAdjustmentPK,FundJournalAccountPK = @FundJournalAccountPK,DebitCredit = @DebitCredit, " +
                            "Amount = @Amount,LastUsersID = @LastUsersID,LastUpdate=@lastupdate " +
                            "Where FAAdjustmentPK = @FAAdjustmentPK and AutoNo = @AutoNo ";

                        cmd.Parameters.AddWithValue("@FundPK", _FAAdjustmentDetail.FundPK);
                        cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FAAdjustmentDetail.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FAAdjustmentDetail.FundJournalAccountPK);         
                        cmd.Parameters.AddWithValue("@DebitCredit", _FAAdjustmentDetail.DebitCredit);
                        cmd.Parameters.AddWithValue("@Amount", _FAAdjustmentDetail.Amount);
                        cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustmentDetail.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _FAAdjustmentDetail.AutoNo);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FAAdjustmentDetail.LastUsersID);
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

        public void FAAdjustmentDetail_Delete(FAAdjustmentDetail _FAAdjustmentDetail)
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
                            "update FAAdjustment set  lastupdate = @Lastupdate where FAAdjustmentPK = @FAAdjustmentPK and status = 1 \n " +
                            "update FAAdjustment set lastupdate = @Lastupdate, status = 1, updateUsersID = @LastUsersID,UpdateTime = @LastUpdate where FAAdjustmentPK = @FAAdjustmentPK and status = 2 \n " +
                            "delete FAAdjustmentDetail where FAAdjustmentPK = @FAAdjustmentPK and FACOAAdjustmentPK = @FACOAAdjustmentPK ";
                        cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustmentDetail.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FAAdjustmentDetail.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _FAAdjustmentDetail.AutoNo);
                        cmd.Parameters.AddWithValue("@Lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FAAdjustmentDetail.LastUsersID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Insert_FAAdjustmentDealingMapping(FAAdjustmentDetail _FAAdjustmentDetail)
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
                        
                        Declare @AutoNo int
                        Declare @FundJournalAccountPK int
                        Declare @DebitOrCredit nvarchar(1)
                        Declare @FundJournalAccountPercent numeric (22,4)

                        IF EXISTS
                        (select * from FAAdjustmentDetail A 
                         left join FAAdjustment B on A.FAAdjustmentPK = B.FAAdjustmentPK 
                         where A.FAAdjustmentPK = @FAAdjustmentPK and A.FACOAAdjustmentPK = @FACOAAdjustmentPK)

                        BEGIN
	                        Select 'Account Already Exists' Result
                        END
                        ELSE
                        BEGIN
	                        --drop Table #Temp
	                        Create Table #Temp
	                        (
		                        FundPK int,FACOAAdjustmentPK int,FundJournalAccountPK int,DebitOrCredit nvarchar(1),Amount numeric (22,4),FundJournalAccountPercent numeric (22,4)
	                        )
	                        insert into #Temp (FundPK,FACOAAdjustmentPK,FundJournalAccountPK,DebitOrCredit,Amount,FundJournalAccountPercent)
	                        Select FundPK,FACOAAdjustmentPK,FundJournalAccountPK,DebitOrCredit,@Amount Amount,FundJournalAccountPercent/100 From FACOAMapping 
	                        where FundPK = @FundPK and FACOAAdjustmentPK = @FACOAAdjustmentPK and status = 2

	                        DECLARE A CURSOR FOR 
	                        select FundPK,FACOAAdjustmentPK,FundJournalAccountPK,
	                        case when Amount < 0 and DebitOrCredit = 'D' then 'C' 
	                        else case when Amount < 0 and DebitOrCredit = 'C' then 'D'
	                        else case when Amount > 0 and DebitOrCredit = 'D' then 'D'
	                        else case when Amount > 0 and DebitOrCredit = 'C' then 'C' End End End ENd DebitOrCredit,FundJournalAccountPercent from #Temp
	                        Open A
	                        Fetch Next From A
	                        Into @FundPK,@FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,@FundJournalAccountPercent

	                        While @@FETCH_STATUS = 0
	                        BEGIN
		                        select @AutoNo = isnull(Max(AutoNo),0) From FAAdjustmentDetail where FAAdjustmentPK = @FAAdjustmentPK
		                        set @AutoNo = @AutoNo + 1
		                        Insert Into FAAdjustmentDetail (FAAdjustmentPK,AutoNo,HistoryPK,Status,FundPK,FACOAAdjustmentPK,FundJournalAccountPK,DebitCredit,Amount)
		                        select @FAAdjustmentPK,@AutoNo,1,2,@FundPK,@FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,isnull(abs(@Amount * @FundJournalAccountPercent),0)

	                        Fetch next From A Into @FundPK,@FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,@FundJournalAccountPercent
	                        END
	                        Close A
	                        Deallocate A 
	                        Select 'Insert FA Adjustment Success' Result
                        END  ";

                        cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustmentDetail.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FAAdjustmentDetail.FundPK);
                        cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FAAdjustmentDetail.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@Amount", _FAAdjustmentDetail.Amount);

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


        public string Update_FAAdjustmentDealingMapping(FAAdjustmentDetail _FAAdjustmentDetail)
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
             
                        Declare @AutoNo int
                        Declare @FundJournalAccountPK int
                        Declare @DebitOrCredit nvarchar(1)
                        Declare @FundJournalAccountPercent numeric (22,4)

                        --drop Table #Temp
                        Create Table #Temp
                        (
	                        FundPK int,FACOAAdjustmentPK int,FundJournalAccountPK int,DebitOrCredit nvarchar(1),Amount numeric (22,4),FundJournalAccountPercent numeric (22,4)
                        )
                        insert into #Temp (FundPK,FACOAAdjustmentPK,FundJournalAccountPK,DebitOrCredit,Amount,FundJournalAccountPercent)
                        Select FundPK,FACOAAdjustmentPK,FundJournalAccountPK,DebitOrCredit,@Amount Amount,FundJournalAccountPercent/100 From FACOAMapping 
                        where FundPK = @FundPK and FACOAAdjustmentPK = @FACOAAdjustmentPK and status = 2

                        DECLARE A CURSOR FOR 
                        select FundPK,FACOAAdjustmentPK,FundJournalAccountPK,
                        case when Amount < 0 and DebitOrCredit = 'D' then 'C' 
                        else case when Amount < 0 and DebitOrCredit = 'C' then 'D'
                        else case when Amount > 0 and DebitOrCredit = 'D' then 'D'
                        else case when Amount > 0 and DebitOrCredit = 'C' then 'C' End End End ENd DebitOrCredit,FundJournalAccountPercent from #Temp
                        Open A
                        Fetch Next From A
                        Into @FundPK,@FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,@FundJournalAccountPercent

                        While @@FETCH_STATUS = 0
                        BEGIN
	                        update FAAdjustmentDetail set  Amount =  isnull(abs(@Amount * @FundJournalAccountPercent),0),DebitCredit = @DebitOrCredit 
	                        where FAAdjustmentPK = @FAAdjustmentPK and  FACOAAdjustmentPK = @FACOAAdjustmentPK  and FundJournalAccountPK = @FundJournalAccountPK
                        Fetch next From A Into @FundPK,@FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,@FundJournalAccountPercent
                        END
                        Close A
                        Deallocate A 
                        Select 'Update FA Adjustment Success' Result  ";

                        cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustmentDetail.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FAAdjustmentDetail.FundPK);
                        cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FAAdjustmentDetail.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@Amount", _FAAdjustmentDetail.Amount);

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
    }
}