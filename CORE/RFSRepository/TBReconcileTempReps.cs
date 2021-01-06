using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Drawing;


namespace RFSRepository
{
    public class TBReconcileTempReps
    {
        Host _host = new Host();
        static readonly TBReconcileTempReps _tBReconcileTempReps = new TBReconcileTempReps();
        static readonly CloseNavReps _closeNAVReps = new CloseNavReps();
        public string TBReconcileTempImport(string _fileSource, string _userID, string _date)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "select * from EndDayTrails where status = 2 and ValueDate = @Date ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return "Import Cancel, Please Approve End Day Trails First on this Day";
                            }
                            else
                            {
                                using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                {
                                    DbCon1.Open();
                                    using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                    {
                                        //cmd1.CommandText = "select * from FundJournal where  ValueDate = @Date and TrxName = 'TB Reconcile' and (Status = 1 or (status = 2 and Posted = 1 and Reversed = 0) or (status = 2 and Posted = 0 and Reversed = 0)) ";
                                        //cmd1.Parameters.AddWithValue("@Date", _date);
                                        //using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                        //{
                                        //    if (dr1.HasRows)
                                        //    {
                                        //        return "TB Reconcile Already Import, Please Reject / Void Fund Journal First on this Day";
                                        //    }
                                        //    else
                                        //    {
                                        //delete data yang lama
                                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                                        {
                                            conn.Open();
                                            using (SqlCommand cmd2 = conn.CreateCommand())
                                            {
                                                cmd2.CommandText = "truncate table TBReconcileTemp";
                                                cmd2.ExecuteNonQuery();
                                            }
                                        }

                                        // import data ke temp dulu
                                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                                        {
                                            bulkCopy.DestinationTableName = "dbo.TBReconcileTemp";
                                            bulkCopy.WriteToServer(CreateDataTableFromTBReconcileTempExcelFile(_fileSource));

                                            _msg = "";
                                        }

                                        //// logic kalo Reconcile success
                                        //using (SqlConnection conn = new SqlConnection(Tools.conString))
                                        //{
                                        //    conn.Open();
                                        //    using (SqlCommand cmd2 = conn.CreateCommand())
                                        //    {
                                        //        cmd2.CommandType = CommandType.StoredProcedure;
                                        //        cmd2.CommandText = "FundTBReconcile";
                                        //        cmd2.Parameters.AddWithValue("@Date", _date);
                                        //        cmd2.Parameters.AddWithValue("@UsersID", _userID);
                                        //        cmd2.Parameters.AddWithValue("@LastUpdate", _now);
                                        //        cmd2.ExecuteNonQuery();

                                        //    }
                                        //    _msg = "TB Reconcile Done";

                                        //}
                                        //}
                                        //}
                                    }
                                }


                            }
                        }
                    }
                }


                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private DataTable CreateDataTableFromTBReconcileTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;
                    int Field;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "TBReconcileTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    //dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.String");
                    //dc.ColumnName = "Date";
                    //dc.Unique = false;
                    //dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AccountID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AccountName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund3";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund4";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund5";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund6";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund7";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund8";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund9";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund10";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund11";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund12";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund13";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund14";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund15";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund16";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund17";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund18";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund19";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fund20";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [TB RADSOFT$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {

                                odRdr.Read();
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["AccountID"] = Convert.ToString(odRdr[0]);
                                    dr["AccountName"] = Convert.ToString(odRdr[1]);
                                    dr["Fund1"] = Convert.ToString(odRdr[2]);
                                    dr["Fund2"] = Convert.ToString(odRdr[3]);
                                    dr["Fund3"] = Convert.ToString(odRdr[4]);
                                    dr["Fund4"] = Convert.ToString(odRdr[5]);
                                    dr["Fund5"] = Convert.ToString(odRdr[6]);
                                    dr["Fund6"] = Convert.ToString(odRdr[7]);
                                    dr["Fund7"] = Convert.ToString(odRdr[8]);
                                    dr["Fund8"] = Convert.ToString(odRdr[9]);
                                    dr["Fund9"] = Convert.ToString(odRdr[10]);
                                    dr["Fund10"] = Convert.ToString(odRdr[11]);
                                    dr["Fund11"] = Convert.ToString(odRdr[12]);
                                    dr["Fund12"] = Convert.ToString(odRdr[13]);
                                    dr["Fund13"] = Convert.ToString(odRdr[14]);
                                    dr["Fund14"] = Convert.ToString(odRdr[15]);
                                    dr["Fund15"] = Convert.ToString(odRdr[16]);
                                    dr["Fund16"] = Convert.ToString(odRdr[17]);
                                    dr["Fund17"] = Convert.ToString(odRdr[18]);
                                    dr["Fund18"] = Convert.ToString(odRdr[19]);
                                    dr["Fund19"] = Convert.ToString(odRdr[20]);
                                    dr["Fund20"] = Convert.ToString(odRdr[21]);

                                    if (dr["TBReconcileTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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
        public List<TBReconcileTemp> Get_EndDayTrailsMatching(string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TBReconcileTemp> L_TBReconcileTemp = new List<TBReconcileTemp>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            Declare @FundNo nvarchar(100)
	                        Declare @FundID nvarchar(100)
	                        Declare @AccountID nvarchar(100)
	                        Declare @Amount nvarchar(100)

	                        Create Table #Temp
                            (FundNo Nvarchar(100), FundID Nvarchar(100),AccountID Nvarchar(100),Amount Numeric(22,4))

                            Create Table #TempA
                            (FundID Nvarchar(100), AccountName Nvarchar(100),AmountSystem Numeric(22,4),AmountImport Numeric(22,4),Flag int)

                            Create Table #TempB
                            (FundID Nvarchar(100), AccountName Nvarchar(100),ParentName Nvarchar(100),AmountSystem Numeric(22,4),AmountImport Numeric(22,4))

                            Declare @Name1 nvarchar(100),@Name2 nvarchar(100),@Name3 nvarchar(100),@Name4 nvarchar(100),@Name5 nvarchar(100),@Name6 nvarchar(100),@Name7 nvarchar(100),@Name8 nvarchar(100),
                            @Name9 nvarchar(100),@Name10 nvarchar(100),@Name11 nvarchar(100),@Name12 nvarchar(100),@Name13 nvarchar(100),@Name14 nvarchar(100),@Name15 nvarchar(100),@Name16 nvarchar(100),
                            @Name17 nvarchar(100),@Name18 nvarchar(100),@Name19 nvarchar(100),@Name20 nvarchar(100)

                            select @Name1 = Fund1,@Name2 = Fund2,@Name3 = Fund3,@Name4 = Fund4,@Name5 = Fund5,@Name6 = Fund6,@Name7 = Fund7,@Name8 = Fund8,@Name9 = Fund9,@Name10 = Fund10,
                            @Name11 = Fund11,@Name12 = Fund12,@Name13 = Fund13,@Name14 = Fund14,@Name15 = Fund15,@Name16 = Fund16,@Name17 = Fund17,@Name18 = Fund18,@Name19 = Fund19,@Name20 = Fund20
                            From TBReconcileTemp where TBReconcileTempPK  = 1

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund1' Name,@Name1,AccountID,CONVERT(float, Fund1)  from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund1 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund2' Name,@Name2,AccountID,CONVERT(float, Fund2) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund2 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund3' Name,@Name3,AccountID,CONVERT(float, Fund3) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund3 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund4' Name,@Name4,AccountID,CONVERT(float, Fund4) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund4 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund5' Name,@Name5,AccountID,CONVERT(float, Fund5) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund5 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund6' Name,@Name6,AccountID,CONVERT(float, Fund6) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund6 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund7' Name,@Name7,AccountID,CONVERT(float, Fund7) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund7 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund8' Name,@Name8,AccountID,CONVERT(float, Fund8) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund8 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund9' Name,@Name9,AccountID,CONVERT(float, Fund9) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund9 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund10' Name,@Name10,AccountID,CONVERT(float, Fund10) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund10 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund11' Name,@Name11,AccountID,CONVERT(float, Fund11) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund11 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund12' Name,@Name12,AccountID,CONVERT(float, Fund12) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund12 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund13' Name,@Name13,AccountID,CONVERT(float, Fund13) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund13 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund14' Name,@Name14,AccountID,CONVERT(float, Fund14) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund14 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund15' Name,@Name15,AccountID,CONVERT(float, Fund15) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund15 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund16' Name,@Name16,AccountID,CONVERT(float, Fund16) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund16 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund17' Name,@Name17,AccountID,CONVERT(float, Fund17) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund17 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund18' Name,@Name18,AccountID,CONVERT(float, Fund18) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund18 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund19' Name,@Name19,AccountID,CONVERT(float, Fund19) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund19 <> '' and AccountID <> 'AccountID'

                            Insert into #Temp(FundNo,FundID,AccountID,Amount)
                            Select 'Fund20' Name,@Name20,AccountID,CONVERT(float, Fund20) from TBReconcileTemp A 
                            left join FundJournalAccount B on A.AccountID = B.ID and B.Status  = 2 where Fund20 <> '' and AccountID <> 'AccountID'


                            DECLARE A CURSOR FOR 
                            select distinct FundID from #Temp
                            Open A
                            Fetch Next From A
                            Into @FundID

                            While @@FETCH_STATUS = 0
                            BEGIN


                            IF Not Exists(Select * from FundJournalDetail A left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.Posted = 1 and B.Reversed = 0
                            left join Fund C on A.FundPK = C.FundPK and B.status  = 2 where C.ID = @FundID and FundJournalAccountPK <> 109) 
                            BEGIN
                            Insert into #TempA(FundID,AccountName,AmountSystem,AmountImport)
                            select C.ID FundID,B.Name AccountName,isnull(sum(BaseDebit-BaseCredit),0) AmountSystem,A.Amount AmountImport from #Temp A
                            left join FundJournalAccount B on B.ID = A.AccountID and B.status = 2
                            left join Fund C on A.FundID = C.ID and C.status = 2
                            left join FundJournalDetail D on C.FundPK = D.FundPK and B.FundJournalAccountPK = D.FundJournalAccountPK
                            left join FundJournal E on D.FundJournalPK = E.FundJournalPK and E.status = 2 and E.Posted = 1 
                            where C.ID = @FundID
                            Group By C.ID,B.Name,A.Amount
                            union all

                            Select  C.ID FundID,D.Name AccountName,isnull(sum(BaseDebit-BaseCredit),0) AmountSystem,0 AmountImport from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 and B.Posted = 1 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status = 2 
                            left join #Temp E on C.ID = E.FundID and  D.ID = E.AccountID 
                            where  C.ID = @FundID and
                            B.ValueDate = @ValueDate and A.Amount <> 0 and C.ID in
                            (select distinct FundID from #Temp) and D.ID not in
                            (select AccountID From #Temp)
                            group by C.ID ,D.Name

                            END
                            ELSE
                            DECLARE @StandartFundAdmin int
                            DECLARE B CURSOR FOR 
                            select StandartFundAdmin from Fund where ID = @FundID and status  = 2
                            Open B
                            Fetch Next From B
                            Into @StandartFundAdmin

                            While @@FETCH_STATUS = 0
                            BEGIN

                            IF (@StandartFundAdmin = 1)
                            BEGIN
                            Insert into #TempA(FundID,AccountName,AmountSystem,AmountImport,Flag)
                             --di system ada, di Import ada
                            select C.ID FundID,B.Name AccountName,isnull(sum(BaseDebit-BaseCredit),0) AmountSystem,A.Amount AmountImport,0 from #Temp A
                            left join FundJournalAccount B on B.ID = A.AccountID and B.status = 2
                            left join Fund C on A.FundID = C.ID and C.status = 2
                            left join FundJournalDetail D on C.FundPK = D.FundPK and B.FundJournalAccountPK = D.FundJournalAccountPK
                            left join FundJournal E on D.FundJournalPK = E.FundJournalPK and E.status = 2 and E.Posted = 1
                            where C.ID = @FundID and E.ValueDate <= @ValueDate
                            Group By C.ID,B.Name,A.Amount


                            union all
                             --di system ada, di Import Ga ada
                            Select  C.ID FundID,D.Name AccountName,isnull(sum(BaseDebit-BaseCredit),0) AmountSystem,0 AmountImport,0 from FundJournalDetail A
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 and B.Posted = 1 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2 
                            left join FundJournalAccount D on A.FundJournalAccountPK = D.FundJournalAccountPK and D.status = 2 
                            left join #Temp E on C.ID = E.FundID and  D.ID = E.AccountID
                            where  C.ID = @FundID and
                            B.ValueDate <= @ValueDate and A.Amount <> 0  
                            and D.ID not in
                            (select AccountID From #Temp where FundID = @FundID) and A.FundJournalAccountPK not in (127,189,196,195,199)
                            group by C.ID ,D.Name

                            union all

                            select C.ID FundID,B.Name AccountName,0 AmountSystem,A.Amount AmountImport,1 from #Temp A
                            left join FundJournalAccount B on B.ID = A.AccountID and B.status = 2
                            left join Fund C on A.FundID = C.ID and C.status = 2
                            where C.ID = @FundID  and B.ID not in
                            (select C.ID from FundJournalDetail A 
                            left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2 and B.Posted = 1
                            left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.Status = 2
                            where B.ValueDate = @valuedate)
                            END


                            Fetch next From B Into @StandartFundAdmin
                            END
                            Close B
                            Deallocate B


                            DECLARE @ParamAccountName nvarchar(100)
                            DECLARE C CURSOR FOR 
                            select AccountName from #TempA where FundID = @FundID
                            GROUP BY AccountName
                            HAVING COUNT(*) > 1

                            Open C
                            Fetch Next From C
                            Into @ParamAccountName

                            While @@FETCH_STATUS = 0
                            BEGIN

                            delete #TempA where AccountName = @ParamAccountName and Flag = 1 and FundID = @FundID

                            Fetch next From C Into @ParamAccountName
                            END
                            Close C
                            Deallocate C

                            Fetch next From A Into @FundID
                            END
                            Close A
                            Deallocate A


                            Truncate Table TBReconcile
                            Insert Into TBReconcile (ValueDate,FundID, AccountName,AmountSystem, AmountImportData,Difference)
                            select @ValueDate,FundID,AccountName,AmountSystem,AmountImport,sum(AmountSystem - AmountImport)Diff from #TempA
                            Group By FundID,AccountName,AmountSystem,AmountImport
                            --HAVING sum(AmountSystem - AmountImport) <> 0

                            select ValueDate,FundID, AccountName,AmountSystem, AmountImportData,Difference from TBReconcile A 
                            left join FundJournalAccount B on A.AccountName = B.Name and B.status  = 2
                            left join Fund C on A.FundID = C.ID and C.status  = 2 where B.ParentPK <> 0
                            order By C.ID,B.ID  ";


                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TBReconcileTemp M_TBReconcileTemp = new TBReconcileTemp();
                                    M_TBReconcileTemp.Date = _date.Equals(DBNull.Value) == true ? "" : Convert.ToString(_date);
                                    M_TBReconcileTemp.FundID = dr["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FundID"]);
                                    //M_TBReconcileTemp.AccountID = dr["AccountID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AccountID"]);
                                    M_TBReconcileTemp.AccountName = dr["AccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AccountName"]);
                                    M_TBReconcileTemp.AmountSystem = dr["AmountSystem"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AmountSystem"]);
                                    M_TBReconcileTemp.AmountImportData = dr["AmountImportData"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AmountImportData"]);
                                    M_TBReconcileTemp.Difference = dr["Difference"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Difference"]);
                                    L_TBReconcileTemp.Add(M_TBReconcileTemp);
                                }

                            }
                            return L_TBReconcileTemp;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public string TBReconcileTemp_InsertDifference(TBReconcileTemp _tbReconcileTemp)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //cmd.CommandText = "select * from FundJournal where  ValueDate = @Date and TrxName = 'TB Reconcile' and (Status = 1 or (status = 2 and Posted = 1 and Reversed = 0) or (status = 2 and Posted = 0 and Reversed = 0)) ";
                        //cmd.Parameters.AddWithValue("@Date", _tbReconcileTemp.Date);
                        //using (SqlDataReader dr = cmd.ExecuteReader())
                        //{
                        //    if (!dr.HasRows)
                        //    {
                        DateTime _dateTimeNow = DateTime.Now;
                        using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                        {

                            DbCon1.Open();
                            using (SqlCommand cmd1 = DbCon1.CreateCommand())
                            {
                                //                                            cmd1.CommandText = @"Declare @EndDayTrailsPK int
                                //                                            Declare @PPeriodPK int            
                                //                                            Declare @FundJournalPK int
                                // 
                                //                                            Select @EndDayTrailsPK =  EndDayTrailsPK From EndDayTrails where valuedate = @Date and status = 2         
                                //                                            select @PPeriodPK = PeriodPK from Period Where @Date Between DateFrom and DateTo and Status = 2                     
                                //                                            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal               
                                //            
                                //                                            INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                
                                //                                                ,[Description],[Posted],[EntryUsersID],[EntryTime],[LastUpdate])                
                                //                                             Select  @FundJournalPK, 1,2,'TB Reconcile',@PPeriodPK,@Date,11,@EndDayTrailsPK,'TB RECONCILE',                
                                //                                                  '','',1,@UsersID,@LastUpdate,@LastUpdate  
                                //
                                //
                                //                                            create table #TBReconcile
                                //                                            (
                                //                                            FundJournalAccountPK int,
                                //                                            FundPK int,           
                                //                                            DebitCredit nvarchar(1),
                                //                                            Amount numeric (22,2)  
                                //                                            )
                                //
                                //                                            INSERT INTO #TBReconcile ([FundJournalAccountPK],[FundPK],[DebitCredit],[Amount])  
                                //                                            select 
                                //                                            FundJournalAccountPK,C.FundPK, 
                                //                                            Case when (DIFFERENCE) < 0 then 'D' 
                                //                                            else 'C' End  DebitCredit,abs(DIFFERENCE) Amount            
                                //                                            from TBReconcile A             
                                //                                            left join FundJournalAccount B on A.AccountName = B.Name and B.status  = 2         
                                //                                            Left join Fund C on A.FundID = C.ID and C.Status = 2     
                                //                                            where  abs(DIFFERENCE) <> 0 
                                //                                            union all
                                //                                            select 
                                //                                            Case when B.Type = 1 then 195 
                                //                                            else Case when B.Type = 2 then 196 
                                //                                            else Case when B.Type = 3 then 127
                                //                                            else Case when B.Type = 4 then 199  End End End End
                                //                                             ,C.FundPK, 
                                //                                            Case when (DIFFERENCE) > 0 then 'D' 
                                //                                            else 'C' End  DebitCredit,abs(DIFFERENCE) Amount   from TBReconcile A             
                                //                                            left join FundJournalAccount B on A.AccountName = B.Name and B.status  = 2
                                //                                            Left join Fund C on A.FundID = C.ID and C.Status = 2 
                                //                                            where  abs(DIFFERENCE) <> 0 
                                //
                                //
                                //                                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                
                                //                                            ,[DetailDescription],[DebitCredit],[Amount],[LastUpdate]) 
                                //
                                //                                            select @FundJournalPK,ROW_NUMBER() over(order by FundJournalAccountPK),1,2,FundJournalAccountPK,1,FundPK,0,0,'',DebitCredit,Amount,getdate() from #TBReconcile
                                //                                            where FundJournalAccountPK not in (127,150,195,196,199)
                                //                                            group By FundPK,FundJournalAccountPK,DebitCredit,Amount
                                //
                                //                                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                
                                //                                            ,[DetailDescription],[DebitCredit],[Amount],[LastUpdate]) 
                                //
                                //                                            select @FundJournalPK,ROW_NUMBER() over(order by FundJournalAccountPK),1,2,FundJournalAccountPK,1,FundPK,0,0,'ADJUST',DebitCredit,Amount,getdate() from #TBReconcile
                                //                                            where FundJournalAccountPK in (127,150,195,196,199)                                            
                                //                                            group By FundPK,FundJournalAccountPK,DebitCredit,Amount
                                //                                            order by FundPK,FundJournalAccountPK
                                //
                                //                                            Update FundJournalDetail Set Debit = Amount,CurrencyRate = 1,BaseDebit = Amount  where FundJournalPK = @FundJournalPK            
                                //                                            and DebitCredit = 'D'            
                                //
                                //
                                //                                            Update FundJournalDetail Set Credit = Amount,CurrencyRate = 1,BaseCredit = Amount  where FundJournalPK = @FundJournalPK            
                                //                                            and DebitCredit = 'C'  ";

                                cmd1.CommandText = @"
                                            Declare @EndDayTrailsPK int
                                            Declare @PPeriodPK int            
                                            Declare @FundJournalPK int
 
                                            Select @EndDayTrailsPK =  EndDayTrailsPK From EndDayTrails where valuedate = @Date and status = 2         
                                            select @PPeriodPK = PeriodPK from Period Where @Date Between DateFrom and DateTo and Status = 2                     
                                            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal               
            
                                            INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                
                                                ,[Description],[Posted],[EntryUsersID],[EntryTime],[LastUpdate])                
                                             Select  @FundJournalPK, 1,2,'TB Reconcile',@PPeriodPK,@Date,11,@EndDayTrailsPK,'TB RECONCILE',                
                                                  '','',1,@UsersID,@LastUpdate,@LastUpdate  
                       
                                            create table #TBReconcile
                                            (
                                            FundJournalAccountPK int,
                                            FundPK int,           
                                            DebitCredit nvarchar(1),
                                            Amount numeric (22,2)  
                                            )

                                            INSERT INTO #TBReconcile ([FundJournalAccountPK],[FundPK],[DebitCredit],[Amount])  
                                            select 
                                            FundJournalAccountPK,C.FundPK, 
                                            Case when (DIFFERENCE) < 0 then 'D' 
                                            else 'C' End  DebitCredit,abs(DIFFERENCE) Amount            
                                            from TBReconcile A             
                                            left join FundJournalAccount B on A.AccountName = B.Name and B.status  = 2         
                                            Left join Fund C on A.FundID = C.ID and C.Status = 2     
                                            where  abs(DIFFERENCE) <> 0 
                                            union all
                                            select 
                                            Case when B.Type = 1 then 335 
                                            else Case when B.Type = 2 then 336 
                                            else Case when B.Type = 3 then 127
                                            else Case when B.Type = 4 then 337  End End End End
                                            ,C.FundPK, 
                                            Case when (DIFFERENCE) > 0 then 'D' 
                                            else 'C' End  DebitCredit,abs(DIFFERENCE) Amount   from TBReconcile A             
                                            left join FundJournalAccount B on A.AccountName = B.Name and B.status  = 2
                                            Left join Fund C on A.FundID = C.ID and C.Status = 2 
                                            where  abs(DIFFERENCE) <> 0 


                                            Declare @FundJournalAccountPK int,@FundPK int,@DebitCredit nvarchar(1),@Amount numeric(22,2)                                        

                                            DECLARE A CURSOR FOR 
                                            select FundJournalAccountPK,FundPK,DebitCredit,Amount from #TBReconcile

                                            Open A
                                            Fetch Next From A
                                            Into @FundJournalAccountPK,@FundPK,@DebitCredit,@Amount
                                            While @@FETCH_STATUS = 0
                                            BEGIN

                                            declare @AutoNo int
                                            Select @AutoNo = max(AutoNo) + 1 From FundJournalDetail where FundJournalPK = @FundJournalPK
                                            if isnull(@AutoNo,0) = 0 BEGIN  Select @AutoNo = isnull(max(AutoNo),0) + 1 From FundJournalDetail where FundJournalPK = @FundJournalPK END

                                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                
                                            ,[DetailDescription],[DebitCredit],[Amount],[LastUpdate]) 

                                            select @FundJournalPK,@AutoNo,1,2,@FundJournalAccountPK,1,@FundPK,0,0,'',@DebitCredit,@Amount,getdate() 
                                         
                                            Fetch next From A Into @FundJournalAccountPK,@FundPK,@DebitCredit,@Amount
                                            END
                                            Close A
                                            Deallocate A
                                            Update FundJournalDetail Set Debit = Amount,CurrencyRate = 1,BaseDebit = Amount  where FundJournalPK = @FundJournalPK            
                                            and DebitCredit = 'D'            

                                            Update FundJournalDetail Set Credit = Amount,CurrencyRate = 1,BaseCredit = Amount  where FundJournalPK = @FundJournalPK            
                                            and DebitCredit = 'C' ";
                                cmd1.CommandTimeout = 0;
                                cmd1.Parameters.AddWithValue("@Date", _tbReconcileTemp.Date);
                                cmd1.Parameters.AddWithValue("@UsersID", _tbReconcileTemp.UsersID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd1.ExecuteNonQuery();
                            }


                            //using (SqlCommand cmd1 = DbCon1.CreateCommand())
                            //{

                            //    cmd1.CommandType = CommandType.StoredProcedure;
                            //    cmd1.CommandText = "FundTBReconcile_EMCO";
                            //    cmd1.Parameters.AddWithValue("@Date", _tbReconcileTemp.Date);
                            //    cmd1.Parameters.AddWithValue("@UsersID", _tbReconcileTemp.UsersID);
                            //    cmd1.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            //    cmd1.ExecuteNonQuery();

                            //}


                        }
                        return "Insert Fund Journal Success";
                        //    }
                        //    else
                        //    {
                        //        return "Fund Journal Already Insert, Please Reject / Void Fund Journal First on this Day";
                        //    }
                        //}
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        //        public List<TrialBalance> Get_TrialBalance(string _date, int _fundPK)
        //        {
        //            try
        //            {
        //                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //                {
        //                    DbCon.Open();
        //                    List<TrialBalance> L_TrialBalance = new List<TrialBalance>();
        //                    using (SqlCommand cmd = DbCon.CreateCommand())
        //                    {
        //                        cmd.CommandText = @"
        //Declare @FirstDateOfYear datetime
        //Declare @ValueDateFrom datetime


        //select @FirstDateOfYear = DATEADD(yy, DATEDIFF(yy, 0, @ValueDateTo), 0)

        //set @ValueDateFrom =  @ValueDateTo

        //if @ValueDateFrom <= @FirstDateOfYear
        //begin
        //	set @ValueDateFrom = @FirstDateOfYear
        //end
        //Declare @BeginDate datetime

        //                    select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@ValueDateFrom,@FundPK) 

        //Select  case when A.Groups = 1 then isnull(A.Name,'') else '' end Header,A.ID ID
        //,case when A.Groups = 1 then '' else A.Name end Name
        //,isnull(cast(E.HeaderOrder as Nvarchar(50)) + '.' + E.Name,'') ParentName 
        //,D.ID CurrencyID,A.BitIsChange,A.Groups
        //,isnull(B.PreviousBaseBalance,0) PreviousBaseBalance
        //,isnull(B.Movement,0) Movement
        //,isnull(B.CurrentBaseBalance,0) CurrentBaseBalance
        //,isnull(B.BKBalance,0) BKBalance
        //from FundJournalAccount A
        //left join (


        //			SELECT C.ID, C.Name,    
        //			C.BitIsChange, C.groups,
        //			CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
        //			CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) 
        //			- CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4))  AS Movement,       
        //			CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance ,0 BKBalance
        //			FROM (      
        //			SELECT A.FundJournalAccountPK,       
        //			SUM(B.Balance) AS CurrentBalance,       
        //			SUM(B.BaseBalance) AS CurrentBaseBalance,      
        //			SUM(B.SumDebit) AS CurrentDebit,       
        //			SUM(B.SumCredit) AS CurrentCredit,       
        //			SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
        //			SUM(B.SumBaseCredit) AS CurrentBaseCredit      
        //			FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
        //			SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,       
        //			SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
        //			SUM(A.Debit) AS SumDebit,      
        //			SUM(A.Credit) AS SumCredit,      
        //			SUM(A.BaseDebit) AS SumBaseDebit,      
        //			SUM(A.BaseCredit) AS SumBaseCredit,      
        //			C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
        //			C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
        //			FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK      
        //			INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)    
        //			INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)      
        //			WHERE  B.ValueDate between @BeginDate and @ValueDateTo and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK AND B.Reversed = 0
        //			--and C.Depth < 3
        //			Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
        //			C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
        //			C.ParentPK7, C.ParentPK8, C.ParentPK9        
        //			) AS B        
        //			WHERE
        //			(B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
        //			OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
        //			OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
        //			OR B.ParentPK9 = A.FundJournalAccountPK)       and A.Status = 2
        //			Group BY A.FundJournalAccountPK       
        //			) AS A LEFT JOIN (       
        //			SELECT A.FundJournalAccountPK,        
        //			SUM(B.Balance) AS PreviousBalance,        
        //			SUM(B.BaseBalance) AS PreviousBaseBalance,       
        //			SUM(B.SumDebit) AS PreviousDebit,        
        //			SUM(B.SumCredit) AS PreviousCredit,        
        //			SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
        //			SUM(B.SumBaseCredit) AS PreviousBaseCredit       
        //			FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
        //			SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,        
        //			SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
        //			SUM(A.Debit) AS SumDebit,        
        //			SUM(A.Credit) AS SumCredit,        
        //			SUM(A.BaseDebit) AS SumBaseDebit,        
        //			SUM(A.BaseCredit) AS SumBaseCredit,        
        //			C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
        //			C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
        //			FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK        
        //			INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)   
        //			INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)    
        //			WHERE  B.ValueDate < @ValueDateFrom and B.ValueDate >= @BeginDate and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK AND B.Reversed = 0
        //			--and C.Depth < 3
        //			Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
        //			C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
        //			C.ParentPK7, C.ParentPK8, C.ParentPK9        
        //			) AS B        
        //			WHERE  (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
        //			OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
        //			OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
        //			OR B.ParentPK9 = A.FundJournalAccountPK)  and A.Status = 2
        //			Group BY A.FundJournalAccountPK       
        //			) AS B ON A.FundJournalAccountPK = B.FundJournalAccountPK        
        //			INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status = 2     


        //			WHERE C.Show=1 
        //		) B on A.ID = B.ID 
        //		left JOIN FundJournalAccount E ON A.ParentPK = E.FundJournalAccountPK   And E.Status in (1,2) 
        //		left JOIN Currency D ON A.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
        //		where A.status = 2 and	A.show = 1 
        //        --or (
        //		--ISNULL(B.PreviousBaseBalance, 0) <> 0
        //		--or ISNULL(B.Movement, 0) <> 0
        //		--or ISNULL(B.CurrentBaseBalance, 0) <> 0 ) 
        //		order by A.ID
        //            ";
        //                        cmd.CommandTimeout = 0;
        //                        cmd.Parameters.AddWithValue("@ValueDateTo", _date);
        //                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
        //                        using (SqlDataReader dr = cmd.ExecuteReader())
        //                        {
        //                            if (dr.HasRows)
        //                            {
        //                                while (dr.Read())
        //                                {
        //                                    TrialBalance M_TrialBalance = new TrialBalance();
        //                                    M_TrialBalance.Header = dr["Header"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Header"]);
        //                                    M_TrialBalance.ID = dr["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ID"]);
        //                                    M_TrialBalance.Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]);
        //                                    M_TrialBalance.ParentName = dr["ParentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ParentName"]);
        //                                    M_TrialBalance.Currency = dr["CurrencyID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CurrencyID"]);
        //                                    M_TrialBalance.BitIsChange = dr["BitIsChange"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitIsChange"]);
        //                                    M_TrialBalance.BitIsGroups = dr["groups"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["groups"]);
        //                                    M_TrialBalance.PreviousBaseBalance = dr["PreviousBaseBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["PreviousBaseBalance"]);
        //                                    M_TrialBalance.Movement = dr["Movement"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Movement"]);
        //                                    M_TrialBalance.CurrentBaseBalance = dr["CurrentBaseBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CurrentBaseBalance"]);
        //                                    M_TrialBalance.BKBalance = "";
        //                                    L_TrialBalance.Add(M_TrialBalance);
        //                                }

        //                            }
        //                            return L_TrialBalance;
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception err)
        //            {
        //                throw err;
        //            }

        //        }

        public List<TrialBalance> Get_TrialBalance(string _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TrialBalance> L_TrialBalance = new List<TrialBalance>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (Tools.ClientCode == "03")
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = "Get_TrialBalanceForTBRecon";
                        }
                        else
                        {
                            cmd.CommandText = @"
                            Declare @FirstDateOfYear datetime
                            Declare @ValueDateFrom datetime


                            select @FirstDateOfYear = DATEADD(yy, DATEDIFF(yy, 0, @ValueDateTo), 0)

                            set @ValueDateFrom =  @ValueDateTo

                            if @ValueDateFrom <= @FirstDateOfYear
                            begin
	                            set @ValueDateFrom = @FirstDateOfYear
                            end

                            CREATE TABLE #Balance 
                            (
                            ID nvarchar(100),
                            Name nvarchar(200),
                            BitIsChange bit,
                            Groups bit,
                            PreviousBaseBalance numeric(19,4),
                            Movement numeric(19,4),
                            CurrentBaseBalance numeric(19,4),
                            BKBalance  numeric(19,4)

                            )

                            CREATE CLUSTERED INDEX indx_Balance  ON #Balance (ID,Name,BitIsChange,Groups,PreviousBaseBalance,Movement,CurrentBaseBalance,BKBalance);


                            Declare @BeginDate datetime

                            select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@ValueDateFrom,@FundPK) 



                            insert into #Balance
                            SELECT C.ID, C.Name,    
			                            C.BitIsChange, C.groups,
			                            CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
			                            CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) 
			                            - CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4))  AS Movement,       
			                            CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance ,0 BKBalance
			                            FROM (      
			                            SELECT A.FundJournalAccountPK,       
			                            SUM(B.Balance) AS CurrentBalance,       
			                            SUM(B.BaseBalance) AS CurrentBaseBalance,      
			                            SUM(B.SumDebit) AS CurrentDebit,       
			                            SUM(B.SumCredit) AS CurrentCredit,       
			                            SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
			                            SUM(B.SumBaseCredit) AS CurrentBaseCredit      
			                            FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
			                            SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,       
			                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
			                            SUM(A.Debit) AS SumDebit,      
			                            SUM(A.Credit) AS SumCredit,      
			                            SUM(A.BaseDebit) AS SumBaseDebit,      
			                            SUM(A.BaseCredit) AS SumBaseCredit,      
			                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
			                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
			                            FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK      
			                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)    
			                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)      
			                            WHERE  B.ValueDate between @BeginDate and @ValueDateTo and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK AND B.Reversed = 0
			                            --and C.Depth < 3
			                            Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
			                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
			                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
			                            ) AS B        
			                            WHERE
			                            (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
			                            OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
			                            OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
			                            OR B.ParentPK9 = A.FundJournalAccountPK)       and A.Status = 2
			                            Group BY A.FundJournalAccountPK       
			                            ) AS A LEFT JOIN (       
			                            SELECT A.FundJournalAccountPK,        
			                            SUM(B.Balance) AS PreviousBalance,        
			                            SUM(B.BaseBalance) AS PreviousBaseBalance,       
			                            SUM(B.SumDebit) AS PreviousDebit,        
			                            SUM(B.SumCredit) AS PreviousCredit,        
			                            SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
			                            SUM(B.SumBaseCredit) AS PreviousBaseCredit       
			                            FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
			                            SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,        
			                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
			                            SUM(A.Debit) AS SumDebit,        
			                            SUM(A.Credit) AS SumCredit,        
			                            SUM(A.BaseDebit) AS SumBaseDebit,        
			                            SUM(A.BaseCredit) AS SumBaseCredit,        
			                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
			                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
			                            FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK        
			                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)   
			                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)    
			                            WHERE  B.ValueDate < @ValueDateFrom and B.ValueDate >= @BeginDate and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK AND B.Reversed = 0
			                            --and C.Depth < 3
			                            Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
			                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
			                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
			                            ) AS B        
			                            WHERE  (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
			                            OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
			                            OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
			                            OR B.ParentPK9 = A.FundJournalAccountPK)  and A.Status = 2
			                            Group BY A.FundJournalAccountPK       
			                            ) AS B ON A.FundJournalAccountPK = B.FundJournalAccountPK        
			                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status = 2    
			                            WHERE C.Show=1 


                            Select  case when A.Groups = 1 then isnull(A.Name,'') else '' end Header,A.ID ID
                            ,case when A.Groups = 1 then '' else A.Name end Name
                            ,isnull(cast(E.HeaderOrder as Nvarchar(50)) + '.' + E.Name,'') ParentName 
                            ,D.ID CurrencyID,A.BitIsChange,A.Groups
                            ,isnull(B.PreviousBaseBalance,0) PreviousBaseBalance
                            ,isnull(B.Movement,0) Movement
                            ,isnull(B.CurrentBaseBalance,0) CurrentBaseBalance
                            ,isnull(B.BKBalance,0) BKBalance
                            from FundJournalAccount A
                            left join (

		                            select * from #Balance

			                            ) B on A.ID COLLATE DATABASE_DEFAULT = B.ID COLLATE DATABASE_DEFAULT
		                            left JOIN FundJournalAccount E ON A.ParentPK = E.FundJournalAccountPK   And E.Status in (1,2) 
		                            left JOIN Currency D ON A.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
		                            where A.status = 2 and	A.show = 1 

		                            order by A.ID
                                        ";
                        }

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDateTo", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TrialBalance M_TrialBalance = new TrialBalance();
                                    M_TrialBalance.Header = dr["Header"].ToString();
                                    M_TrialBalance.ID = dr["ID"].ToString();
                                    M_TrialBalance.Name = dr["Name"].ToString();
                                    M_TrialBalance.ParentName = dr["ParentName"].ToString();
                                    M_TrialBalance.Currency = dr["CurrencyID"].ToString();
                                    M_TrialBalance.BitIsChange = Convert.ToBoolean(dr["BitIsChange"]);
                                    M_TrialBalance.BitIsGroups = Convert.ToBoolean(dr["Groups"]);
                                    M_TrialBalance.PreviousBaseBalance = Convert.ToDecimal(dr["PreviousBaseBalance"]);
                                    M_TrialBalance.Movement = Convert.ToDecimal(dr["Movement"]);
                                    M_TrialBalance.CurrentBaseBalance = Convert.ToDecimal(dr["CurrentBaseBalance"]);
                                    M_TrialBalance.BKBalance = "";
                                    L_TrialBalance.Add(M_TrialBalance);
                                }

                            }
                            return L_TrialBalance;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string TrialBalance_InsertDiffEndBalance(List<TrialBalance> _tb, int _fundPK, string _valueDate, string _userID, string _notes)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                int _FundJournalPK;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                            Declare @FundID nvarchar(300)
                            Declare @PPeriodPK int            
                            Declare @FundJournalPK int
                            Declare @CurReference nvarchar(100)     
							Declare @Type nvarchar(20)

                            Select @FundID = ID from Fund where status in (1,2) and FundPK = @FundPK
                            set @FundID = isnull(@FundID,'')
                            select @PPeriodPK = PeriodPK from Period Where @ValueDate Between DateFrom and DateTo and Status = 2 

                            if @ClientCode = '09'
	                            set @Type = 'TB'
                            else
	                            set @Type = 'ADJ'

							-- reference
							Declare @LastNo int						
							Declare @periodPK int  
  
							Select @PeriodPK = PeriodPK From Period Where Status = 2 and @ValueDate Between DateFrom and DateTo  
  
							-- FA =  Fixed Aset, PP = Prepaid  
  
							If @Type in ('FA','PP')  
							BEGIN  
								set @Type = 'CP'  
							END  

							if exists(Select Top 1 * from cashierReference where Type = @type And PeriodPK = @PeriodPK   
							and substring(right(reference,4),1,2) = month(@ValueDate)  )      
							BEGIN      
								   
								 Select @LastNo = max(No) + 1 From CashierReference where Type = @type And PeriodPK = @periodPK and     
								 substring(right(reference,4),1,2) = month(@ValueDate)      
    
								 Set @CurReference =  Cast(@LastNo as nvarchar(10)) + '/' +  Case when @type = 'CP' then 'OUT' else     
								 Case When @type = 'AR' then 'AR' else Case when @type = 'AP' then 'AP' else    
								 case when @type = 'ADJ' then 'ADJ' ELSE CASE WHEN @type = 'GJ' THEN 'GJ' else 'IN' END END END END END    
								 + '/' + REPLACE(RIGHT(CONVERT(VARCHAR(8), cast(@ValueDate as date), 3), 5) ,'/','')        
    
								 Update CashierReference Set Reference = @CurReference, No = @LastNo where Type = @type And PeriodPK = @periodPK    
								 and substring(right(reference,4),1,2) = month(@ValueDate)     
  
							END   
							ELSE BEGIN      
  
								 Set @CurReference = '1/'  +  Case when @type = 'CP' then 'OUT' else     
								 Case When @type = 'AR' then 'AR' else Case when @type = 'AP' then 'AP' else     
								 case when @type = 'ADJ' then 'ADJ' ELSE CASE WHEN @Type = 'GJ' THEN 'GJ' else 'IN' END END END END END + '/' + REPLACE(RIGHT(CONVERT(VARCHAR(8), cast(@ValueDate as date), 3), 5) ,'/','')        

								 Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)     
								 Select isnull(Max(CashierReferencePK),0) +  1,@periodPK,@type,@CurReference,1 from CashierReference     
  
							END      
  
				                    
                            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal               
                            set @FundJournalPK = isnull(@FundJournalPK,1)
			
                            INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                
                                ,[Description],[Posted],[EntryUsersID],[EntryTime],[LastUpdate]) 
                            Select  @FundJournalPK, 1,2,'TB Reconcile',@PPeriodPK,@ValueDate,11,0,'TB RECONCILE',@CurReference,@FundID + ' : ' +  @Notes,1,@UsersID,@LastUpdate,@LastUpdate

                            select @FundJournalPK Result


                                          ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@Notes", _notes);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            _FundJournalPK = Convert.ToInt32(dr["Result"]);
                        }

                    }

                    DbCon.Close();
                    DbCon.Open();
                    foreach (var _obj in _tb)
                    {
                        TrialBalance _m = new TrialBalance();
                        _m.ID = _obj.ID;
                        _m.CurrentBaseBalance = _obj.CurrentBaseBalance;
                        _m.BKBalance = _obj.BKBalance;

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"
                                DECLARE @AutoNo int
                                DECLARE @FundJournalAccountPK int
                                DECLARE @Type int
                                DECLARE @CurrencyPK int

                                select @FundJournalAccountPK = isnull(FundJournalAccountPK,0), @Type = isnull(Type,0) ,@CurrencyPK = isnull(CurrencyPK,0) from FundJournalAccount where ID = @ID and status = 2 
                                select @AutoNo = AutoNo from FundJournalDetail where FundJournalPK = @FundJournalPK

                                set @AutoNo = isnull(@AutoNo,0) + 1
                                set @FundJournalAccountPK = isnull(@FundJournalAccountPK,0)
                                set @Type = isnull(@Type,0)
                                set @CurrencyPK = isnull(@CurrencyPK,0)

                                INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                
                                ,[DetailDescription],[DebitCredit],[Amount],Debit,Credit,CurrencyRate,BaseDebit,BaseCredit,[LastUpdate]) 

                                select @FundJournalPK,@AutoNo,1,2,@FundJournalAccountPK,@CurrencyPK,@FundPK,0,0,'',
                                case when cast(@CurrentBaseBalance as numeric(22,4)) - cast(@BKBalance as numeric(22,4)) > 0 then 'C' else 'D' end
                                ,abs(cast(@CurrentBaseBalance as numeric(22,4))-cast(@BKBalance as numeric(22,4))),
                                case when cast(@CurrentBaseBalance as numeric(22,4)) - cast(@BKBalance as numeric(22,4)) > 0 then 0 else abs(cast(@CurrentBaseBalance as numeric(22,4))-cast(@BKBalance as numeric(22,4))) end,
                                case when cast(@CurrentBaseBalance as numeric(22,4)) - cast(@BKBalance as numeric(22,4)) > 0 then abs(cast(@CurrentBaseBalance as numeric(22,4))-cast(@BKBalance as numeric(22,4))) else 0 end,1
                                ,case when cast(@CurrentBaseBalance as numeric(22,4)) - cast(@BKBalance as numeric(22,4)) > 0 then 0 else abs(cast(@CurrentBaseBalance as numeric(22,4))-cast(@BKBalance as numeric(22,4))) end
                                ,case when cast(@CurrentBaseBalance as numeric(22,4)) - cast(@BKBalance as numeric(22,4)) > 0 then abs(cast(@CurrentBaseBalance as numeric(22,4))-cast(@BKBalance as numeric(22,4))) else 0 end,@LastUpdate
                                          ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ID", _m.ID);
                            cmd.Parameters.AddWithValue("@CurrentBaseBalance", _m.CurrentBaseBalance);
                            cmd.Parameters.AddWithValue("@BKBalance", _m.BKBalance);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@FundJournalPK", _FundJournalPK);
                            cmd.ExecuteNonQuery();

                        }
                    }

                    return "Success Save to Fund Journal";
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean TBRpt(string _userID, TBReconcileTempRpt _tbReconcileTemp)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                         
Declare @FirstDateOfYear datetime
Declare @ValueDateFrom datetime


select @FirstDateOfYear = DATEADD(yy, DATEDIFF(yy, 0, @ValueDateTo), 0)

set @ValueDateFrom =  @ValueDateTo

if @ValueDateFrom <= @FirstDateOfYear
begin
	set @ValueDateFrom = @FirstDateOfYear
end
Declare @BeginDate datetime

                    select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@ValueDateFrom,@FundPK) 

Select  case when A.Groups = 1 then isnull(A.Name,'') else '' end Header,A.ID ID
,case when A.Groups = 1 then '' else A.Name end Name
,isnull(cast(E.HeaderOrder as Nvarchar(50)) + '.' + E.Name,'') ParentName 
,D.ID CurrencyID,A.BitIsChange,A.Groups
,isnull(B.PreviousBaseBalance,0) PreviousBaseBalance
,isnull(B.Movement,0) Movement
,isnull(B.CurrentBaseBalance,0) CurrentBaseBalance
,isnull(B.BKBalance,0) BKBalance
from FundJournalAccount A
left join (


			SELECT C.ID, C.Name,    
			C.BitIsChange, C.groups,
			CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
			CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) 
			- CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4))  AS Movement,       
			CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance ,0 BKBalance
			FROM (      
			SELECT A.FundJournalAccountPK,       
			SUM(B.Balance) AS CurrentBalance,       
			SUM(B.BaseBalance) AS CurrentBaseBalance,      
			SUM(B.SumDebit) AS CurrentDebit,       
			SUM(B.SumCredit) AS CurrentCredit,       
			SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
			SUM(B.SumBaseCredit) AS CurrentBaseCredit      
			FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
			SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,       
			SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
			SUM(A.Debit) AS SumDebit,      
			SUM(A.Credit) AS SumCredit,      
			SUM(A.BaseDebit) AS SumBaseDebit,      
			SUM(A.BaseCredit) AS SumBaseCredit,      
			C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
			C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
			FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK      
			INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)    
			INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)      
			WHERE  B.ValueDate between @BeginDate and @ValueDateTo and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK
			--and C.Depth < 3
			Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
			C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
			C.ParentPK7, C.ParentPK8, C.ParentPK9        
			) AS B        
			WHERE
			(B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
			OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
			OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
			OR B.ParentPK9 = A.FundJournalAccountPK)       and A.Status = 2
			Group BY A.FundJournalAccountPK       
			) AS A LEFT JOIN (       
			SELECT A.FundJournalAccountPK,        
			SUM(B.Balance) AS PreviousBalance,        
			SUM(B.BaseBalance) AS PreviousBaseBalance,       
			SUM(B.SumDebit) AS PreviousDebit,        
			SUM(B.SumCredit) AS PreviousCredit,        
			SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
			SUM(B.SumBaseCredit) AS PreviousBaseCredit       
			FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
			SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,        
			SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
			SUM(A.Debit) AS SumDebit,        
			SUM(A.Credit) AS SumCredit,        
			SUM(A.BaseDebit) AS SumBaseDebit,        
			SUM(A.BaseCredit) AS SumBaseCredit,        
			C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
			C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
			FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK        
			INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)   
			INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)    
			WHERE  B.ValueDate < @ValueDateFrom and B.ValueDate >= @BeginDate and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK
			--and C.Depth < 3
			Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
			C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
			C.ParentPK7, C.ParentPK8, C.ParentPK9        
			) AS B        
			WHERE  (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
			OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
			OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
			OR B.ParentPK9 = A.FundJournalAccountPK)  and A.Status = 2
			Group BY A.FundJournalAccountPK       
			) AS B ON A.FundJournalAccountPK = B.FundJournalAccountPK        
			INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status = 2     
		
		
			WHERE C.Show=1 
		) B on A.ID = B.ID 
		left JOIN FundJournalAccount E ON A.ParentPK = E.FundJournalAccountPK   And E.Status in (1,2) 
		left JOIN Currency D ON A.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
		where A.status = 2 and	A.show = 1 
        --or (
		--ISNULL(B.PreviousBaseBalance, 0) <> 0
		--or ISNULL(B.Movement, 0) <> 0
		--or ISNULL(B.CurrentBaseBalance, 0) <> 0 ) 
		order by A.ID
                            ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@FundPK", _tbReconcileTemp.FundPK);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _tbReconcileTemp.ValueDate);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "TBReconcileTemp" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "TBReconcileTemp" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "TBReconcileTemp";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TBReconcileTemp");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<TBReconcileTempRpt> rList = new List<TBReconcileTempRpt>();
                                    while (dr0.Read())
                                    {
                                        TBReconcileTempRpt rSingle = new TBReconcileTempRpt();
                                        //rSingle.FundID = dr0["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundID"]);
                                        //rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                        //rSingle.Asset = dr0["Asset"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Asset"]);
                                        //rSingle.Liabilities = dr0["Liabilities"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Liabilities"]);
                                        //rSingle.Aum = dr0["AUM"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AUM"]);
                                        //rSingle.Unit = dr0["Unit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Unit"]);
                                        //rSingle.Nav = dr0["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Nav"]);
                                        rSingle.Header = dr0["Header"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Header"]);
                                        rSingle.Name = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                        rSingle.PrevBalance = dr0["PreviousBaseBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PreviousBaseBalance"]);
                                        rSingle.Movement = dr0["Movement"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Movement"]);
                                        rSingle.EndBalance = dr0["CurrentBaseBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CurrentBaseBalance"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByTitle =
                                        from r in rList
                                        group r by new { r.Asset, r.Liabilities, r.Aum, r.Unit, r.Nav } into rGroup
                                        select rGroup;

                                    int incRowExcel = 1;

                                    foreach (var rsHeader in GroupByTitle)
                                    {

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "DATE";
                                        worksheet.Cells[incRowExcel, 2].Value = " : ";
                                        worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_tbReconcileTemp.ValueDate).ToString("dd-MMM-yyyy");
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "FUND";
                                        worksheet.Cells[incRowExcel, 2].Value = " : ";
                                        worksheet.Cells[incRowExcel, 3].Value = _tbReconcileTemp.FundID;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "ASSET";
                                        worksheet.Cells[incRowExcel, 2].Value = " : ";
                                        worksheet.Cells[incRowExcel, 3].Value = _closeNAVReps.CloseNav_GetTotalAccountBalanceByFundPK(_tbReconcileTemp.FundPK, 1, Convert.ToString(_tbReconcileTemp.ValueDate));
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LIABILITIES";
                                        worksheet.Cells[incRowExcel, 2].Value = " : ";
                                        worksheet.Cells[incRowExcel, 3].Value = _closeNAVReps.CloseNav_GetTotalAccountBalanceByFundPK(_tbReconcileTemp.FundPK, 63, Convert.ToString(_tbReconcileTemp.ValueDate));
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "AUM";
                                        worksheet.Cells[incRowExcel, 2].Value = " : ";
                                        worksheet.Cells[incRowExcel, 3].Value = _closeNAVReps.CloseNav_GetTotalAUMByFundPK(_tbReconcileTemp.FundPK, 1, 63, Convert.ToString(_tbReconcileTemp.ValueDate));
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "UNIT";
                                        worksheet.Cells[incRowExcel, 2].Value = " : ";
                                        worksheet.Cells[incRowExcel, 3].Value = _closeNAVReps.CloseNav_GetTotalUnitByFundPK(_tbReconcileTemp.FundPK, Convert.ToString(_tbReconcileTemp.ValueDate));
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "NAV";
                                        worksheet.Cells[incRowExcel, 2].Value = " : ";
                                        worksheet.Cells[incRowExcel, 3].Value = _closeNAVReps.CloseNav_GetNAVProjectionByFundPK(_tbReconcileTemp.FundPK, Convert.ToString(_tbReconcileTemp.ValueDate));
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                        incRowExcel = incRowExcel + 3;

                                        worksheet.Cells[incRowExcel, 1].Value = "Header";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "Name";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "Prev Balance";
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4].Value = "Movement";
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "End Balance";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        //end area header
                                        incRowExcel++;
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.Header;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.PrevBalance;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Movement;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.EndBalance;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";


                                            _endRowDetail = incRowExcel;
                                            incRowExcel++;
                                        }

                                        worksheet.Cells["A" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["A" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        incRowExcel++;


                                        worksheet.Cells[incRowExcel, 1].Value = "TOTAL ";
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                        worksheet.Cells[incRowExcel, 4].Calculate();
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";



                                        incRowExcel = incRowExcel + 3;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN

                                    //worksheet.Cells.AutoFitColumns(0);
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 5];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();


                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 TB Reconcile Temp";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);
                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    if (_tbReconcileTemp.DownloadMode == "PDF")
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

        public string TBReconcileTemp_GetEDTInformation(int _fundPK, string _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                        DECLARE @TimeCP DATETIME
                        DECLARE @TimeEDT DATETIME


                        SELECT TOP 1  @TimeEDT =  EntryTime FROM dbo.EndDayTrails WHERE status = 2 
                        AND FundPK = @FundPK
                        AND ValueDate <= @Date
                        ORDER BY EndDayTrailsPK DESC

                        IF @TimeEDT IS NULL
                        BEGIN
	
	                        SELECT TOP 1  @TimeEDT =  EntryTime FROM dbo.EndDayTrails WHERE status = 2 
	                        AND ValueDate <= @Date
	                        ORDER BY EndDayTrailsPK DESC
                        END

                        SELECT TOP 1 @TimeCP = EntryTime FROM dbo.ClosePrice 
                        WHERE status = 2 AND date <= @Date
                        ORDER BY ClosePricePK desc


                        IF @TimeEDT < @TimeCP AND @TimeCP IS NOT NULL AND @TimeEDT IS NOT NULL
                        BEGIN
	                        SELECT 'need to EDT again, because close Price time is higher than edt time' Result
	                        RETURN	
                        END

                        IF @TimeEDT < @TimeCP AND @TimeCP IS NOT NULL AND @TimeEDT IS NOT NULL
                        BEGIN
	                        SELECT 'need to EDT again, because close Price time is higher than edt time' Result
	                        RETURN	
                        END

                        IF @TimeEDT > @TimeCP AND CONVERT(TIME,@TimeEDT) < CONVERT(TIME,'17:00:00') AND CONVERT(DATE,@TimeEDT) = CONVERT(DATE,@Date)
                        BEGIN
	                        SELECT 'need to EDT again, because EDT time is before 17:00:00' Result
                        END

                    ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();

                            }
                            else
                            {
                                return "";
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

        public List<DetailMovement> TBReconcile_GetDetailMovement(DateTime _date, int _fundPK, string _accountID)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DetailMovement> L_setDetailMovement = new List<DetailMovement>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"

                        select C.Name,B.Description,BaseDebit,BaseCredit from FundJournalDetail A 
                        left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2
                        left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.status = 2
                        where ValueDate = @Date and B.Posted = 1 and B.Reversed = 0 and C.ID = @AccountID and A.FundPK = @FundPK

                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@AccountID", _accountID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_setDetailMovement.Add(setDetailMovement(dr));
                                }
                            }
                        }
                        return L_setDetailMovement;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private DetailMovement setDetailMovement(SqlDataReader dr)
        {
            DetailMovement M_DetailMovement = new DetailMovement();
            M_DetailMovement.Name = Convert.ToString(dr["Name"]);
            M_DetailMovement.Description = Convert.ToString(dr["Description"]);
            M_DetailMovement.BaseCredit = Convert.ToDecimal(dr["BaseCredit"]);
            M_DetailMovement.BaseDebit = Convert.ToDecimal(dr["BaseDebit"]);
            return M_DetailMovement;
        }

        public string TBReconcile_AddTBReconcileLog(string _usersID, DateTime _date, string _fundID, TBReconcileLog _tBReconcileLog)
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
                          if exists (select * From TBReconcileLog Where Date=cast(@ValueDate as date) and FundPK=@FundPK and UsersID <> @UsersID and @LastUpdate  Between LockTime and ReleaseTime)
                            begin 
	                            select 'Cannot process this fund, <b>' + UsersID + '</b> is doing this fund' Result From TBReconcileLog where @LastUpdate Between LockTime and ReleaseTime and FundPK=@FundPK and UsersID <> @UsersID
                            end
                            else if not exists (select * From TBReconcileLog Where Date=cast(@ValueDate as date) and FundPK=@FundPK and @LastUpdate  Between LockTime and ReleaseTime)
                            begin 
	                            insert into TBReconcileLog (Date,FundPK,UsersID,LockTime,ReleaseTime)
                                select cast(@ValueDate as date),@FundPK,@UsersID,@LastUpdate,dateadd(minute,15,@lastupdate)

                                select '' Result
                            end
                            else
                                select '' Result
                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundID);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();
                            }
                            else
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

        public string TBReconcile_UpdateReleaseTime(string _usersID, DateTime _date, string _fundID, TBReconcileLog _tBReconcileLog)
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
                        Update TBReconcileLog
                        Set ReleaseTime = @LastUpdate Where FundPK = @FundPK and Date = @ValueDate and UsersID = @UsersID and @LastUpdate Between LockTime and ReleaseTime

                        select ''
                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundID);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();
                            }
                            else
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

        public string TBReconcile_ValidateSaveToJournal(string _usersID, DateTime _date, string _fundID, TBReconcileLog _tBReconcileLog)
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
                          if exists (select * From TBReconcileLog Where Date=cast(@ValueDate as date) and FundPK=@FundPK and UsersID <> @UsersID and @LastUpdate  Between LockTime and ReleaseTime)
                            begin 
	                            select 'Use Time Has Expired For This Fund' Result
                            end
                            else
                                select '' Result
                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundID);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();
                            }
                            else
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

        public Boolean Validate_AlreadyTbReconcile(int _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        IF EXISTS(
                            Select top 1 '1' From FundJournal A
                             left join FundJournalDetail B on A.FundJournalPK = B.FundJournalPK and B.status = 2 
                             Where A.Status = 2 and A.Posted = 1 and A.Reversed = 0 and A.ValueDate = @Date and A.TrxName = 'TB Reconcile' and B.FundPK = @FundPK
                        )
                        BEGIN
                            select 1 Result
                        END
                        ELSE
                        BEGIN
                            select 0 Result
                        END
                        ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);
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

        public string TBReconcile_ReleaseLock(string _UsersID, DateTime _date, string _fundID)
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
                        Update TBReconcileLog
                        Set ReleaseTime = @LastUpdate Where FundPK = @FundPK and Date = @ValueDate and @LastUpdate Between LockTime and ReleaseTime and UsersID = @UsersID

                        select ''
                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@UsersID", _UsersID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();
                            }
                            else
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

        public List<TBReconcileStatus> Get_DataByTBStatus(DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TBReconcileStatus> L_TrialBalance = new List<TBReconcileStatus>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"

                            declare @ValueDateTo datetime

                            set @ValueDateTo = @ValueDate
    
                            declare @ListFund table (
	                            FundPK int,
	                            StatusReconcile nvarchar(100),
	                            StatusOpen nvarchar(100),
	                            UsersID nvarchar(100)
                            )

                            declare @ListFundRecon table (
	                            FundPK int
                            )

                            declare @ListFundStatus table (
	                            FundPK int,
	                            UsersID nvarchar(100)
                            )

                            insert into @ListFund(FundPK)
                            select distinct FundPK from Fund where status = 2 and MaturityDate >= @ValueDateTo

                            insert into @ListFundRecon(FundPK)
                            select distinct B.FundPK from FundJournal A
                            left join FundJournalDetail B on A.FundJournalPK = B.FundJournalPK
                            where TrxName = 'TB Reconcile' and ValueDate = @ValueDateTo and A.status = 2

                            update A set A.StatusReconcile = case when B.FundPK is null then 'Not Reconciled' else 'Reconciled' end
                            from @ListFund A 
                            left join @ListFundRecon B on A.FundPK = B.FundPK

                            insert into @ListFundStatus(FundPK,UsersID)
                            select distinct FundPK,UsersID from TBReconcileLog where 
                            getdate() between LockTime and ReleaseTime
                            --cast(Date as date) = @ValueDateTo

                            update A set A.StatusOpen = case when B.FundPK is null then 'Not Open' else 'Open' end, A.UsersID = isnull(B.UsersID,'')
                            from @ListFund A 
                            left join @ListFundStatus B on A.FundPK = B.FundPK

                            select B.ID FundID,StatusReconcile,StatusOpen,UsersID from @ListFund A
                            left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                            order by B.ID

                                        ";


                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TBReconcileStatus M_TrialBalance = new TBReconcileStatus();
                                    M_TrialBalance.FundID = dr["FundID"].ToString();
                                    M_TrialBalance.StatusReconcile = dr["StatusReconcile"].ToString();
                                    M_TrialBalance.StatusOpen = dr["StatusOpen"].ToString();
                                    M_TrialBalance.UsersID = dr["UsersID"].ToString();
                                    L_TrialBalance.Add(M_TrialBalance);
                                }

                            }
                            return L_TrialBalance;
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