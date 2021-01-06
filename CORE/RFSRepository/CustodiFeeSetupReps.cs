using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class CustodiFeeSetupReps
    {
        Host _host = new Host();

        //1

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<SetCustodiFeeSetup> CustodiFeeSetup_GetDataCustodiFeeSetup(int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SetCustodiFeeSetup> L_setCustodiFeeSetup = new List<SetCustodiFeeSetup>();
                    using (SqlCommand cmd1 = DbCon.CreateCommand())
                    {
                        cmd1.CommandText = @"
                        select A.Status Status, case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, 
                        case when A.FundPK = 0 then 'ALL' else isnull(B.Name,'') end FundName, 
                        MV.DescOne FeeTypeDesc, MV1.DescOne CustodiFeeTypeDesc, isnull(FeePercent,0) MiFeePercent, 
                        isnull(AUMFrom,0) AUMFrom, isnull(AUMTo,0) AUMTo, isnull(B.FundPK,0) FundPK,A.* 
                        from CustodiFeeSetup A 
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) 
                        left join MasterValue MV on A.FeeType = MV.Code and MV.status in (1,2) and MV.ID = 'FundFeeType'
                        left join MasterValue MV1 on A.CustodiFeeType = MV1.Code and MV1.status in (1,2) and MV1.ID = 'CustodiFeeSetupType'
                        where A.FundPK  =  @FundPK and A.Status in (1,2)

                               ";
                        cmd1.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd1.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_setCustodiFeeSetup.Add(SetCustodiFeeSetup(dr));
                                }
                            }
                        }
                        return L_setCustodiFeeSetup;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private SetCustodiFeeSetup SetCustodiFeeSetup(SqlDataReader dr)
        {
            SetCustodiFeeSetup M_CustodiFeeSetup = new SetCustodiFeeSetup();
            M_CustodiFeeSetup.Status = Convert.ToInt32(dr["Status"]);
            M_CustodiFeeSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CustodiFeeSetup.Selected = Convert.ToBoolean(dr["Selected"]);
            M_CustodiFeeSetup.CustodiFeeSetupPK = Convert.ToInt32(dr["CustodiFeeSetupPK"]);
            M_CustodiFeeSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_CustodiFeeSetup.FundName = Convert.ToString(dr["FundName"]);
            M_CustodiFeeSetup.Date = Convert.ToString(dr["Date"]);
            M_CustodiFeeSetup.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_CustodiFeeSetup.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);
            M_CustodiFeeSetup.CustodiFeeType = Convert.ToInt32(dr["CustodiFeeType"]);
            M_CustodiFeeSetup.CustodiFeeTypeDesc = Convert.ToString(dr["CustodiFeeTypeDesc"]);
            M_CustodiFeeSetup.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_CustodiFeeSetup.AUMTo = Convert.ToDecimal(dr["AUMTo"]);
            M_CustodiFeeSetup.AUMFrom = Convert.ToDecimal(dr["AUMFrom"]);
            M_CustodiFeeSetup.FeePercent = Convert.ToDecimal(dr["FeePercent"]);
            return M_CustodiFeeSetup;
        }


        public int AddCustodiFeeSetup(CustodiFeeSetup _CustodiFeeSetup, bool _havePrivillege)
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
                            
                        Insert into CustodiFeeSetup(CustodiFeeSetupPK,HistoryPK,Status,FundPK,Date,AUMFrom,AUMTo,FeePercent,FeeType,CustodiFeeType,LastUpdate,LastUpdateDB,EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,UpdateUsersID,UpdateTime) 
                        Select isnull(max(CustodiFeeSetupPK),0) + 1,1,2,@FundPK,@Date,@AUMFrom,@AUMTo,@FeePercent,@FeeType,@CustodiFeeType,@LastUpdate,@LastUpdate,@UpdateUsersID,@UpdateTime,@UpdateUsersID,@UpdateTime,@UpdateUsersID,@UpdateTime from CustodiFeeSetup";

                        cmd.Parameters.AddWithValue("@FundPK", _CustodiFeeSetup.FundPK);
                        cmd.Parameters.AddWithValue("@Date", _CustodiFeeSetup.Date);
                        cmd.Parameters.AddWithValue("@FeeType", _CustodiFeeSetup.FeeType);
                        cmd.Parameters.AddWithValue("@CustodiFeeType", _CustodiFeeSetup.CustodiFeeType);
                        cmd.Parameters.AddWithValue("@AUMTo", _CustodiFeeSetup.AUMTo);
                        cmd.Parameters.AddWithValue("@AUMFrom", _CustodiFeeSetup.AUMFrom);
                        cmd.Parameters.AddWithValue("@FeePercent", _CustodiFeeSetup.FeePercent);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _CustodiFeeSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    return _host.Get_LastPKByLastUpate(_datetimeNow, "CustodiFeeSetup");
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void RejectDataBySelected(string param1, string param2)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustodiFeeSetup set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where Selected = 1";
                        cmd.Parameters.AddWithValue("@VoidUsersID", param1);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
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


        public void RejectedDataCustodiFeeSetupBySelected(string _usersID, string param2, int _fundPK)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CustodiFeeSetup set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where Selected = 1 and FundPK = @FundPK and status <> 3 ";
                        cmd.Parameters.AddWithValue("@VoidUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
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



        public bool CheckHassAddCopy(int _pk, string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from CustodiFeeSetup where FundPK = @PK and Status in (1,2) and Acq = @Date";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        cmd.Parameters.AddWithValue("@Date", _date);

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


        //public int CopyCustodiFeeSetup(CustodiFeeSetup _CustodiFeeSetup, bool _havePrivillege)
        //{
        //    try
        //    {
        //        DateTime _datetimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText = @"
        //                 Declare @MaxPK int
        //                    select @MaxPK = Max(CustodiFeeSetupPK) from CustodiFeeSetup
        //                    set @maxPK = isnull(@maxPK,0)

        //                --CREATE TABLE #CopyCustodiFeeSetup
        //                --(
        //                --selected bit,
        //                --RangeFrom decimal(12,0),
        //                --RangeTo decimal(12,0),
        //               -- DateAmortize datetime,
        //                --MiFeeAmount decimal(12,0),
        //               -- MiFeePercent decimal(12,0),
        //               -- FeeType int,
        //                --FundPK int,
        //               -- DateFrom datetime,
        //               -- DateCopy datetime
        //               -- )
        //               -- INSERT INTO #CopyCustodiFeeSetup
        //               -- SELECT Selected, RangeFrom, RangeTo, DateAmortize, MiFeeAmount, MiFeePercent, FeeType,FundPK,@DateFrom DateFrom,@DateTo DateCopy from CustodiFeeSetup where FundPK = @FundPK and Date = @DateFrom 
        //               -- GROUP BY Selected,RangeFrom,RangeTo,DateAmortize,MiFeeAmount,MiFeePercent,FeeType,FundPK

        //                INSERT INTO CustodiFeeSetup(CustodiFeeSetupPK,HistoryPK,Status,FundPK,Date,DateAmortize,RangeFrom,RangeTo,MiFeeAmount,MiFeePercent,FeeType,LastUpdate,LastUpdateDB,EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,UpdateUsersID,UpdateTime) 
        //                Select @MaxPK + ROW_NUMBER() OVER(ORDER BY A.CustodiFeeSetupPK ASC) CustodiFeeSetupPK,1,2,A.FundPK,@DateTo,A.DateAmortize,A.RangeFrom,A.RangeTo,A.MiFeeAmount,A.MiFeePercent,A.FeeType,@LastUpdate,@LastUpdate,@userID,@UpdateTime,@userID,@UpdateTime,@userID,@UpdateTime from CustodiFeeSetup A left JOIN CustodiFeeSetup B on A.FundPK = B.FundPK where A.FundPK = @FundPK and A.Date = @DateFrom
        //                GROUP BY A.CustodiFeeSetupPK,A.Selected,A.RangeFrom,A.RangeTo,A.DateAmortize,A.MiFeeAmount,A.MiFeePercent,A.FeeType,A.FundPK

        //                delete CustodiFeeSetup where FundPK IS NULL";

        //                cmd.Parameters.AddWithValue("@FundPK", _CustodiFeeSetup.FundPK);
        //                cmd.Parameters.AddWithValue("@DateFrom", _CustodiFeeSetup.Date);
        //                cmd.Parameters.AddWithValue("@DateTo", _CustodiFeeSetup.ValueDateCopy);
        //                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
        //                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
        //                cmd.Parameters.AddWithValue("@userID", _CustodiFeeSetup.EntryUsersID);

        //                cmd.ExecuteNonQuery();
        //            }
        //            return _host.Get_LastPKByLastUpate(_datetimeNow, "CustodiFeeSetup");
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}


        public bool CheckMaxValue(CheckAUMTo _CustodiFeeSetup)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    if (_CustodiFeeSetup.FeeType == 1 || _CustodiFeeSetup.FeeType == 2)
                    {
                        int _AUMTo = 0;
                        string _AUM = "";
                        if (_CustodiFeeSetup.AUMTo == true)
                        {
                            _AUMTo = 999;
                            _AUM = " and AUMTo >" + _AUMTo;

                        }
                        else
                        {
                            _AUMTo = 99999;
                            _AUM = " and AUMTo >" + _AUMTo;
                        }
                        DbCon.Open();
                        string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "select * from CustodiFeeSetup where FundPK = @PK and Status in (1,2) and Date = @Date" + _AUM + @" and FeeType = " + _CustodiFeeSetup.FeeType;
                            cmd.Parameters.AddWithValue("@PK", _CustodiFeeSetup.FundPK);
                            cmd.Parameters.AddWithValue("@Date", _CustodiFeeSetup.Date);
                            cmd.Parameters.AddWithValue("@AUMTo", _AUMTo);

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
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public string Add_Validate(CustodiFeeSetup _CustodiFeeSetup)
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
                        declare @FeeTypeDesc nvarchar(50)

                        select @FeeTypeDesc = DescOne from MasterValue where ID = 'FeeSetupType' and status = 2 and Code = @FeeType


                        declare @CustodiFeeSetup table
                        (
                        FundPK int,
                        Date datetime,
                        AUMFrom int,
                        AUMTo int,
                        FeeType int,
                        CustodiFeeType int
                        )


                        insert into @CustodiFeeSetup
                        select FundPK,Date,AUMFrom,AUMTo,FeeType,CustodiFeeType from CustodiFeeSetup 
                        where status = 2 and Date = @Date and FundPK = @FundPK


                        IF @FeeType not in (2,3)
                        BEGIN
	                        IF EXISTS(select * from @CustodiFeeSetup)
	                        BEGIN
		                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Another Data in this days !' Result
	                        END
                            ELSE
                            BEGIN
                                select 'FALSE' Result
                            END
                        END
                        ELSE
                        BEGIN
	                        IF NOT EXISTS(select * from @CustodiFeeSetup where FeeType <> @FeeType)
	                        BEGIN
		                        IF EXISTS(
		                        SELECT * FROM CustodiFeeSetup 
		                        WHERE (@AUMFrom BETWEEN AUMFrom AND AUMTo 
			                        OR @AUMTo BETWEEN AUMFrom AND AUMTo
			                        OR AUMTo BETWEEN @AUMTo AND @AUMFrom
			                        OR AUMFrom BETWEEN @AUMFrom AND @AUMTo) and FundPK = @FundPK and Date = @Date and FeeType = @FeeType and status = 2
		                        )
		                        BEGIN
			                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Data, Please Check AUM From and To !' Result
		                        END
		                        ELSE
		                        BEGIN
			                        select 'FALSE' Result
		                        END
	                        END
	                        ELSE
	                        BEGIN
		                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Another Type in this days !' Result
	                        END
                        END
                         ";

                        cmd.Parameters.AddWithValue("@Date", _CustodiFeeSetup.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _CustodiFeeSetup.FundPK);
                        cmd.Parameters.AddWithValue("@FeeType", _CustodiFeeSetup.FeeType);
                        cmd.Parameters.AddWithValue("@AUMFrom", _CustodiFeeSetup.AUMFrom);
                        cmd.Parameters.AddWithValue("@AUMTo", _CustodiFeeSetup.AUMTo);

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