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
using System.Data.OleDb;
using SucorInvest.Connect;


namespace RFSRepository
{
    public class BrokerageCommissionReps
    {
        Host _host = new Host();


        private MSSales setSales(SqlDataReader dr)
        {
            MSSales M_MSSales = new MSSales();
            M_MSSales.ID = dr["ID"].ToString();
            M_MSSales.Name = dr["Name"].ToString();
            M_MSSales.Code = dr["Code"].ToString();
            return M_MSSales;
        }

        public List<MSSales> Sales_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MSSales> L_MSSales = new List<MSSales>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                             Select * from ZBC_MasterSalesTemp                            
                        ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MSSales.Add(setSales(dr));
                                }
                            }
                            return L_MSSales;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }



        private MSTransaction setTransaction(SqlDataReader dr)
        {
            MSTransaction M_MSTransaction = new MSTransaction();
            M_MSTransaction.Date = dr["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Date"]);
            M_MSTransaction.Sales = dr["Sales"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Sales"]);
            M_MSTransaction.No_Cust = dr["No_Cust"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["No_Cust"]);
            M_MSTransaction.Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]);
            M_MSTransaction.RecBy = dr["RecByPK"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RecByPK"]);
            M_MSTransaction.Buying = dr["Buying"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Buying"]);
            M_MSTransaction.Selling = dr["Selling"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Selling"]);
            M_MSTransaction.Netting = dr["Netting"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Netting"]);
            M_MSTransaction.Total_Trans = dr["Total_Trans"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Total_Trans"]);
            M_MSTransaction.Comm = dr["Comm"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Comm"]);
            M_MSTransaction.Vat = dr["Vat"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Vat"]);
            M_MSTransaction.Levy = dr["Levy"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Levy"]);
            M_MSTransaction.Other = dr["Other"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Other"]);
            M_MSTransaction.Pph = dr["Pph"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Pph"]);
            M_MSTransaction.Rebate = dr["Rebate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Rebate"]);
            M_MSTransaction.Net_Reb = dr["Net_Reb"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Net_Reb"]);
            M_MSTransaction.Expense = dr["Expense"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Expense"]);
            return M_MSTransaction;
        }

        public List<MSTransaction> Transaction_Select()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MSTransaction> L_MSSales = new List<MSTransaction>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                             Select b.Name Sales,* from ZBC_Transaction a left join ZBC_MasterSalesTemp b on a.SalesPK = b.code                            
                        ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MSSales.Add(setTransaction(dr));
                                }
                            }
                            return L_MSSales;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public string ImportMasterSales(string _fileSource, string _userID)
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
                            cmd1.CommandText = "truncate table ZBC_MasterSalesTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.ZBC_MasterSalesTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromMasterSalesTempTempExcelFile(_fileSource));
                        //_msg = "Import Close Nav Success";
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = " Declare @Date datetime " +
                                               " Declare @FundName nvarchar(200) " +
                                               " Declare A Cursor For        " +
                                               " SELECT CN.Date,CN.FundName     " +
                                               " FROM BrokerageCommissionTemp CN Left Join Fund F on F.Name = CN.FundName and F.status = 2       " +
                                                 "\n " +
                                               " Open A      " +
                                               " Fetch Next From A   " +
                                               " Into @Date,@FundName    " +
                                                 "\n " +
                                               " While @@FETCH_STATUS = 0    " +
                                               " Begin         " +
                                                   " DECLARE @BrokerageCommissionPK BigInt " +
                                                   " SELECT @BrokerageCommissionPK = isnull(Max(BrokerageCommissionPK),0) FROM BrokerageCommission \n " +
                                                   " Update CN set status  = 3 From BrokerageCommission CN Left Join Fund F on F.FundPK = CN.FundPK and F.status = 2 where F.Name  = @FundName and Date =  @Date " +
                                                   " INSERT INTO BrokerageCommission(BrokerageCommissionPK,HistoryPK,Status,Date,FundPK, " +
                                                   " AUM,Nav,EntryUsersID,EntryTime,LastUpdate) \n " +
                                                   " SELECT Row_number() over(order by BrokerageCommissionPK) + @BrokerageCommissionPK,1,1,convert(datetime, date, 101), " +
                                                   " F.FundPK,isnull(CN.AUM,0),isnull(CN.Nav,0),@UserID,@TimeNow,@TimeNow " +
                                                   " FROM BrokerageCommissionTemp CN Left Join Fund F on F.Name = CN.FundName and F.status = 2 where F.Name  = @FundName and Date =  @Date " +
                                                   " Select 'Import Success' A    " +
                                               " Fetch next From A         " +
                                               " Into @Date,@FundName            " +
                                               " End         " +
                                               " Close A         " +
                                               " Deallocate A   ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);

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
        private DataTable CreateDataTableFromMasterSalesTempTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Code";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Name";
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
                                    dr["Code"] = odRdr[0];
                                    dr["ID"] = odRdr[1];
                                    dr["Name"] = odRdr[2];

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

        
        public string ImportMasterTransaction(string _fileSource, string _userID, string _date)
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
                            cmd1.CommandText = "truncate table ZBC_Transaction_11_Temp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.ZBC_Transaction_11_Temp";
                        bulkCopy.WriteToServer(CreateDataTableFromMasterTransactionTempTempExcelFile(_fileSource, _date));
                        //_msg = "Import Close Nav Success";
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText =
                              @"
                                if Exists( Select * from ZBC_Transaction_11_Temp 
                                where Date in
                                (
	                                select Date from ZBC_Transaction 
                                ))
                                BEGIN

                                declare @Message nvarchar(max)
                                set @Message = 'Data Has Import '
                                select distinct @Message = CONVERT(varchar(20), @Message) +' , ' + CONVERT(VARCHAR(20), isnull(Date,0), 101)  from ZBC_Transaction_11_Temp 
	                                where Date in
		                                (
			                                select Date from ZBC_Transaction 
		                                )

	                                Select top 1  'false' result,@Message ResultDesc from ZBC_Transaction_11_Temp 
	                                where Date in
		                                (
			                                select Date from ZBC_Transaction
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
                                    _msg = "Import Transaction Success";
                                    using (SqlCommand cmd3 = conn.CreateCommand())
                                    {
                                        cmd3.CommandText =
                                        @"  
                                   
                                INSERT INTO [dbo].[ZBC_Transaction]
                                ([Date]
                                ,[SalesPK]
                                ,[RecByPK]
                                ,[No_Cust]
                                ,[Name]
                                ,[Buying]
                                ,[Selling]
                                ,[Netting]
                                ,[Total_Trans]
                                ,[Comm]
                                ,[Vat]
                                ,[Levy]
                                ,[Other]
                                ,[Pph]
                                ,[Rebate]
                                ,[Net_Reb]
                                ,[Expense])
    
	                        select Date,Sales,RecBy,No_Cust,Name,Buying,Selling,Netting,Total_Trans,Comm,Vat,Levy,Other,Pph,Rebate,Net_Reb,Expense from ZBC_Transaction_11_Temp";
                                        cmd3.Parameters.AddWithValue("@EntryUsersID", _userID);
                                        cmd3.Parameters.AddWithValue("@LastUpdate", _date);
                                        cmd3.ExecuteNonQuery();
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
        private DataTable CreateDataTableFromMasterTransactionTempTempExcelFile(string _path, string _date)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Sales";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No_Cust";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Name";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "RecBy";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Buying";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Selling";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Netting";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Total_Trans";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Comm";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Vat";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Levy";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Other";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Pph";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Rebate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Net_Reb";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Expense";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Vol_Buy";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Vol_sell";
                    dc.Unique = false;
                    dt.Columns.Add(dc);



                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [ALL$]";
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
                                    dr["Date"] = _date;
                                    dr["Sales"] = odRdr[0];
                                    dr["No_Cust"] = odRdr[2];
                                    dr["Name"] = odRdr[3];
                                    dr["RecBy"] = odRdr[4];
                                    dr["Buying"] = odRdr[5];
                                    dr["Selling"] = odRdr[6];
                                    dr["Netting"] = odRdr[7];
                                    dr["Total_Trans"] = odRdr[8];
                                    dr["Comm"] = odRdr[9];
                                    dr["Vat"] = odRdr[10];
                                    dr["Levy"] = odRdr[11];
                                    dr["Other"] = odRdr[12];
                                    dr["Pph"] = odRdr[13];
                                    dr["Rebate"] = odRdr[14];
                                    dr["Net_Reb"] = odRdr[15];
                                    dr["Expense"] = odRdr[16];
                                    dr["Vol_Buy"] = odRdr[17];
                                    dr["Vol_sell"] = odRdr[18];

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
    }
}