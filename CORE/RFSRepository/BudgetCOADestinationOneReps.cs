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
using System.Text;
using System.Threading;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class BudgetCOADestinationOneReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BudgetCOADestinationOne] " +
                            "([BudgetCOADestinationOnePK],[HistoryPK],[Status],[COADestinationOnePK],[PeriodPK],[Amount],[MISCostCenterPK],";
        string _paramaterCommand = "@COADestinationOnePK,@PeriodPK,@Amount,@MISCostCenterPK,";

        //2

        private BudgetCOADestinationOne setBudgetCOADestinationOne(SqlDataReader dr)
        {
            BudgetCOADestinationOne M_BudgetCOADestinationOne = new BudgetCOADestinationOne();
            M_BudgetCOADestinationOne.BudgetCOADestinationOnePK = Convert.ToInt32(dr["BudgetCOADestinationOnePK"]);
            M_BudgetCOADestinationOne.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BudgetCOADestinationOne.Status = Convert.ToInt32(dr["Status"]);
            M_BudgetCOADestinationOne.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BudgetCOADestinationOne.Notes = Convert.ToString(dr["Notes"]);
            M_BudgetCOADestinationOne.COADestinationOnePK = Convert.ToInt32(dr["COADestinationOnePK"]);
            M_BudgetCOADestinationOne.COADestinationOneID = Convert.ToString(dr["COADestinationOneID"]);
            M_BudgetCOADestinationOne.COADestinationOneName = Convert.ToString(dr["COADestinationOneName"]);
            M_BudgetCOADestinationOne.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_BudgetCOADestinationOne.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_BudgetCOADestinationOne.Amount = Convert.ToDecimal(dr["Amount"]);
            M_BudgetCOADestinationOne.MISCostCenterPK = Convert.ToInt32(dr["MISCostCenterPK"]);
            M_BudgetCOADestinationOne.MISCostCenterID = Convert.ToString(dr["MISCostCenterID"]);
            M_BudgetCOADestinationOne.MISCostCenterName = Convert.ToString(dr["MISCostCenterName"]);
            M_BudgetCOADestinationOne.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BudgetCOADestinationOne.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BudgetCOADestinationOne.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BudgetCOADestinationOne.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BudgetCOADestinationOne.EntryTime = dr["EntryTime"].ToString();
            M_BudgetCOADestinationOne.UpdateTime = dr["UpdateTime"].ToString();
            M_BudgetCOADestinationOne.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BudgetCOADestinationOne.VoidTime = dr["VoidTime"].ToString();
            M_BudgetCOADestinationOne.DBUserID = dr["DBUserID"].ToString();
            M_BudgetCOADestinationOne.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BudgetCOADestinationOne.LastUpdate = dr["LastUpdate"].ToString();
            M_BudgetCOADestinationOne.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_BudgetCOADestinationOne;
        }

        public List<BudgetCOADestinationOne> BudgetCOADestinationOne_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BudgetCOADestinationOne> L_BudgetCOADestinationOne = new List<BudgetCOADestinationOne>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COADestinationOneID,B.Name COADestinationOneName,C.ID PeriodID,D.ID MISCostCenterID,D.Name MISCostCenterName, A.* from BudgetCOADestinationOne A 
                            left join COADestinationOne B on A.COADestinationOnePK = B.COADestinationOnePK and B.status = 2 
                            left join Period C on A.PeriodPK = C.PeriodPK  and C.status = 2
                            left join MISCostCenter D on A.MISCostCenterPK = D.MISCostCenterPK and D.status = 2
                            where A.status= @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COADestinationOneID,B.Name COADestinationOneName,C.ID PeriodID,D.ID MISCostCenterID,D.Name MISCostCenterName, A.* from BudgetCOADestinationOne A 
                            left join COADestinationOne B on A.COADestinationOnePK = B.COADestinationOnePK and B.status = 2 
                            left join Period C on A.PeriodPK = C.PeriodPK  and C.status = 2
                            left join MISCostCenter D on A.MISCostCenterPK = D.MISCostCenterPK and D.status = 2
                            order by COADestinationOnePK,PeriodPK,MISCostCenterPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BudgetCOADestinationOne.Add(setBudgetCOADestinationOne(dr));
                                }
                            }
                            return L_BudgetCOADestinationOne;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BudgetCOADestinationOne_Add(BudgetCOADestinationOne _BudgetCOADestinationOne, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(BudgetCOADestinationOnePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from BudgetCOADestinationOne";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BudgetCOADestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(BudgetCOADestinationOnePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from BudgetCOADestinationOne";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@COADestinationOnePK", _BudgetCOADestinationOne.COADestinationOnePK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _BudgetCOADestinationOne.PeriodPK); 
                        cmd.Parameters.AddWithValue("@Amount", _BudgetCOADestinationOne.Amount);
                        cmd.Parameters.AddWithValue("@MISCostCenterPK", _BudgetCOADestinationOne.MISCostCenterPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _BudgetCOADestinationOne.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "BudgetCOADestinationOne");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BudgetCOADestinationOne_Update(BudgetCOADestinationOne _BudgetCOADestinationOne, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_BudgetCOADestinationOne.BudgetCOADestinationOnePK, _BudgetCOADestinationOne.HistoryPK, "BudgetCOADestinationOne"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BudgetCOADestinationOne set status=2, Notes=@Notes,COADestinationOnePK=@COADestinationOnePK,PeriodPK=@PeriodPK,Amount=@Amount,MISCostCenterPK=@MISCostCenterPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where BudgetCOADestinationOnePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _BudgetCOADestinationOne.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                            cmd.Parameters.AddWithValue("@Notes", _BudgetCOADestinationOne.Notes);
                            cmd.Parameters.AddWithValue("@COADestinationOnePK", _BudgetCOADestinationOne.COADestinationOnePK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _BudgetCOADestinationOne.PeriodPK);
                            cmd.Parameters.AddWithValue("@Amount", _BudgetCOADestinationOne.Amount);
                            cmd.Parameters.AddWithValue("@MISCostCenterPK", _BudgetCOADestinationOne.MISCostCenterPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _BudgetCOADestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BudgetCOADestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BudgetCOADestinationOne set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BudgetCOADestinationOnePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _BudgetCOADestinationOne.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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
                                cmd.CommandText = "Update BudgetCOADestinationOne set Notes=@Notes,COADestinationOnePK=@COADestinationOnePK,PeriodPK=@PeriodPK,Amount=@Amount,MISCostCenterPK=@MISCostCenterPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where BudgetCOADestinationOnePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _BudgetCOADestinationOne.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                                cmd.Parameters.AddWithValue("@Notes", _BudgetCOADestinationOne.Notes);
                                cmd.Parameters.AddWithValue("@COADestinationOnePK", _BudgetCOADestinationOne.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetCOADestinationOne.PeriodPK);
                                cmd.Parameters.AddWithValue("@Amount", _BudgetCOADestinationOne.Amount);
                                cmd.Parameters.AddWithValue("@MISCostCenterPK", _BudgetCOADestinationOne.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BudgetCOADestinationOne.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_BudgetCOADestinationOne.BudgetCOADestinationOnePK, "BudgetCOADestinationOne");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BudgetCOADestinationOne where BudgetCOADestinationOnePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BudgetCOADestinationOne.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@COADestinationOnePK", _BudgetCOADestinationOne.COADestinationOnePK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetCOADestinationOne.PeriodPK);
                                cmd.Parameters.AddWithValue("@Amount", _BudgetCOADestinationOne.Amount);
                                cmd.Parameters.AddWithValue("@MISCostCenterPK", _BudgetCOADestinationOne.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BudgetCOADestinationOne.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BudgetCOADestinationOne set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where BudgetCOADestinationOnePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _BudgetCOADestinationOne.Notes);
                                cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BudgetCOADestinationOne.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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

        public void BudgetCOADestinationOne_Approved(BudgetCOADestinationOne _BudgetCOADestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BudgetCOADestinationOne set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where BudgetCOADestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _BudgetCOADestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _BudgetCOADestinationOne.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BudgetCOADestinationOne set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BudgetCOADestinationOnePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BudgetCOADestinationOne.ApprovedUsersID);
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

        public void BudgetCOADestinationOne_Reject(BudgetCOADestinationOne _BudgetCOADestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BudgetCOADestinationOne set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BudgetCOADestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _BudgetCOADestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BudgetCOADestinationOne.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BudgetCOADestinationOne set status= 2,LastUpdate=@LastUpdate  where BudgetCOADestinationOnePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
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

        public void BudgetCOADestinationOne_Void(BudgetCOADestinationOne _BudgetCOADestinationOne)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BudgetCOADestinationOne set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BudgetCOADestinationOnePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BudgetCOADestinationOne.BudgetCOADestinationOnePK);
                        cmd.Parameters.AddWithValue("@historyPK", _BudgetCOADestinationOne.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BudgetCOADestinationOne.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public string ImportBudgetCOADestinationOne(string _fileSource, string _userID, int _periodPK, int _costCenterPK)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    //delete data yang lama
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = "truncate table BudgetCOADestinationOneTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.BudgetCOADestinationOneTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromBudgetCOADestinationOneTempExcelFile(_fileSource));
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"
                            Delete BudgetCOADestinationOne where PeriodPK = @PeriodPK and  MISCostCenterPK = @CcPK

                            Declare @MaxPK int
                            select @MaxPK = Max(BudgetCOADestinationOnePK) from BudgetCOADestinationOne
                            set @maxPK = isnull(@maxPK,0)

                            Insert Into BudgetCOADestinationOne (BudgetCOADestinationOnePK,HistoryPK,status,Notes,COADestinationOnePK,PeriodPK,Amount,MISCostCenterPK)
                            select  @MaxPK + ROW_NUMBER() OVER(ORDER BY COADestinationOnePK ASC), 1,2,'',COADestinationOnePK,@PeriodPK,CONVERT(numeric(22,4),replace(replace(replace(RTRIM(LTRIM(Balance)),',',''),')',''),'(','-')) Amount,@CcPK
                            from BudgetCOADestinationOneTemp
                            
                            Select 'Import Success' A ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);
                            cmd1.Parameters.AddWithValue("@PeriodPK", _periodPK);
                            cmd1.Parameters.AddWithValue("@CcPK", _costCenterPK);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = Convert.ToString(dr["A"]);
                                    return _msg;
                                }
                                else
                                {
                                    _msg = "";
                                    return _msg;
                                }
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
        private DataTable CreateDataTableFromBudgetCOADestinationOneTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "BudgetCOADestinationOneTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "COADestinationOnePK";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Name";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Balance";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
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

                                    dr["COADestinationOnePK"] = Convert.ToInt32(odRdr[0]);
                                    dr["Name"] = Convert.ToString(odRdr[1]);
                                    dr["Balance"] = Convert.ToString(odRdr[2]);
                                    if (dr["BudgetCOADestinationOneTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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

    }
}