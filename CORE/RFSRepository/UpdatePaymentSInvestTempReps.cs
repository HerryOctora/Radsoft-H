﻿using System;
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



namespace RFSRepository
{
    public class UpdatePaymentSInvestTempReps
    {
        Host _host = new Host();

        private UpdatePaymentSInvestTemp setUpdatePaymentSInvestTemp (SqlDataReader dr)
        {
            UpdatePaymentSInvestTemp M_Model = new UpdatePaymentSInvestTemp();
            M_Model.UpdatePaymentSInvestTempPK = Convert.ToInt32(dr["UpdatePaymentSInvestTempPK"]);
            M_Model.Selected = Convert.ToBoolean(dr["Selected"]);
            M_Model.TransactionType = Convert.ToString(dr["TransactionType"]);
            M_Model.TransactionDate = Convert.ToString(dr["TransactionDate"]);
            M_Model.RefNumber = Convert.ToString(dr["RefNumber"]);
            M_Model.SellingAgentCode = Convert.ToString(dr["SellingAgentCode"]);
            M_Model.SellingAgentName = dr["SellingAgentName"].ToString();
            M_Model.IFUA = dr["IFUA"].ToString();
            M_Model.FundCode = dr["FundCode"].ToString();
            M_Model.FundName = dr["FundName"].ToString();
            M_Model.AmountCash = Convert.ToDecimal(dr["AmountCash"]);
            M_Model.AmountUnit = Convert.ToDecimal(dr["AmountUnit"]);
            M_Model.FeePercent = Convert.ToDecimal(dr["FeePercent"]);
            M_Model.BICCode = dr["BICCode"].ToString();
            M_Model.BankAcc = dr["BankAcc"].ToString();
            M_Model.PaymentDate = Convert.ToString(dr["PaymentDate"]);
            M_Model.TransferType = dr["TransferType"].ToString();
            M_Model.TransferTypeDesc = dr["TransferTypeDesc"].ToString();
            M_Model.ReferenceNumber = dr["ReferenceNumber"].ToString();
            return M_Model;
        }



        public string UpdatePaymentSInvestTempImportSubsRed(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                //delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table trxaperd";
                        cmd2.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.trxaperd";
                    bulkCopy.WriteToServer(CreateDataTableFromUpdatePaymentSInvestSubsRedTempExcelFile(_fileSource));

                    _msg = "Import Update Payment SInvest Success";
                }

                //validasi custom CAM untuk exclude SACode yg tidak ada
                if (Tools.ClientCode == "21")
                {
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText =
                               @"  

                            declare @tableSACodeFundCLient table (
	                            SACode nvarchar(100)
                            )

                            declare @tableSACodeFailed table (
	                            SACode nvarchar(100)
                            )

                            declare @Msg nvarchar(max)
                            Declare @date datetime 
                            select top 1 @Date = TransactionDate From TrxAperd

                            insert into @tableSACodeFundCLient
                            select distinct SACode from FundClient where status = 2 and isnull(SACode,'') <> ''
                            union all 
							select ID from Company where status = 2

                            insert into @tableSACodeFailed
                            select distinct SACode from trxaperd where TransactionDate = @Date and SACode not in (
	                            select * from @tableSACodeFundCLient
                            )

                            if exists ( select * from @tableSACodeFailed )
                            begin
	                            select @Msg = COALESCE(@Msg + ', ', '') + SACode from @tableSACodeFailed

	                            select 'Selling agent codes not found on fund client<br/> Please check SAcode : ' + @Msg Result
                            end


                            ";
                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    return dr["Result"].ToString();
                                }
                            }

                        }
                        conn.Close();
                    }
                }


                // logic kalo Reconcile success
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd1 = conn.CreateCommand())
                    {
                        if (Tools.ClientCode == "03")
                        {
                            cmd1.CommandText =
                       @"  

Declare @date datetime 
select top 1 @Date = TransactionDate From TrxAperd

delete updatePaymentSinvestTemp where TransactionDate = @Date and TransactionType in ('Subscription','Redemption')

                            Declare @MaxPK int
                            select @MaxPK = Max(UpdatePaymentSInvestTempPK) from UpdatePaymentSInvestTemp
                            set @maxPK = isnull(@maxPK,0)

                            insert into UpdatePaymentSInvestTemp(UpdatePaymentSInvestTempPK,Selected,TransactionDate,TransactionType,RefNumber,SellingAgentCode,IFUA,FundCode,AmountCash,AmountUnit,FeePercent,BICCode,BankAcc,BankNo,PaymentDate,
                            TransferType,ReferenceNumber,FeeNominal)
                            select @MaxPK + ROW_NUMBER() OVER(ORDER BY transactiontype ASC) UpdatePaymentSInvestTempPK,0, TransactionDate, TransactionType, SAReference, SACode, IFUA, FundCode, AmountNominal, AmountUnit, FeePercent, BIMemberCode, 
                            PaymentName,PaymentNo,  
                             Case when PaymentDate <> '' Then PaymentDate else PaymentDate end

, TransferType, UploadReference,FeeNominal
                            from TrxAperd A
left join Fund B on A.FundCode = B.SinvestCode and B.status In (1,2)

where A.Status not in ('rejected','failed')";
                        }
                        else
                        {
                            cmd1.CommandText =
                           @"  

Declare @date datetime 
select top 1 @Date = TransactionDate From TrxAperd

delete updatePaymentSinvestTemp where TransactionDate = @Date and TransactionType in ('Subscription','Redemption')

                            Declare @MaxPK int
                            select @MaxPK = Max(UpdatePaymentSInvestTempPK) from UpdatePaymentSInvestTemp
                            set @maxPK = isnull(@maxPK,0)

                            insert into UpdatePaymentSInvestTemp(UpdatePaymentSInvestTempPK,Selected,TransactionDate,TransactionType,RefNumber,SellingAgentCode,IFUA,FundCode,AmountCash,AmountUnit,FeePercent,BICCode,BankAcc,BankNo,PaymentDate,
                            TransferType,ReferenceNumber,FeeNominal)
                            select @MaxPK + ROW_NUMBER() OVER(ORDER BY transactiontype ASC) UpdatePaymentSInvestTempPK,0, TransactionDate, TransactionType, SAReference, SACode, IFUA, FundCode, AmountNominal, AmountUnit, FeePercent, BIMemberCode, 
                            PaymentName,PaymentNo,  
                            Case when PaymentDate <> '' Then dbo.FWorkingDay(TransactionDate,B.DefaultPaymentDate) else TransactionDate end

, TransferType, UploadReference,FeeNominal
                            from TrxAperd A
left join Fund B on A.FundCode = B.SinvestCode and B.status In (1,2)

where A.Status not in ('rejected','failed')";
                        }
                        cmd1.ExecuteNonQuery();
                    }
                    _msg = "Import Success";

                }

                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        private DataTable  CreateDataTableFromUpdatePaymentSInvestSubsRedTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "TransactionDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TransactionType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ReferenceNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Status";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMFeeAmendment";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMPaymentDateAmendment";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SACode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SAName";
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
                    dc.ColumnName = "SID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);
                    
                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCCY";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "AmountNominal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "AmountUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AllUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "FeeNominal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "FeeUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "FeePercent";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TransferPath";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentCode";
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
                    dc.ColumnName = "BankName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "PaymentDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TransferType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "InputDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "UploadReference";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SAReference";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMStatus";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBStatus";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBCompletionStatus";
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
                                    //dr["TransactionType"] = odRdr[2];
                                    string _strTransactionDate = Convert.ToString(odRdr[1]);
                                    if (!string.IsNullOrEmpty(_strTransactionDate))
                                    {
                                        string _tgl = _strTransactionDate.Substring(6, 2);
                                        string _bln = _strTransactionDate.Substring(4, 2);
                                        string _thn = _strTransactionDate.Substring(0, 4);

                                        _strTransactionDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                                    }

                                    string _strInputDate = Convert.ToString(odRdr[34]);
                                    if (!string.IsNullOrEmpty(_strInputDate))
                                    {
                                        string _tglInputDate = _strInputDate.Substring(6, 2);
                                        string _blnInputDate = _strInputDate.Substring(4, 2);
                                        string _thnInputDate = _strInputDate.Substring(0, 4);

                                        _strInputDate = _blnInputDate + "/" + _tglInputDate + "/" + _thnInputDate; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                                    }

                                    string _strPaymentDate = Convert.ToString(odRdr[32]);
                                    if (!string.IsNullOrEmpty(_strPaymentDate))
                                    {
                                        string _tglInputDate = _strPaymentDate.Substring(6, 2);
                                        string _blnInputDate = _strPaymentDate.Substring(4, 2);
                                        string _thnInputDate = _strPaymentDate.Substring(0, 4);

                                        _strPaymentDate = _blnInputDate + "/" + _tglInputDate + "/" + _thnInputDate; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                                    }
                                    else {
                                        _strPaymentDate = "01/01/1900";
                                    }



                                        dr["TransactionDate"] = _strTransactionDate; //odRdr[1];
                                        dr["TransactionType"] = odRdr[2];
                                        dr["ReferenceNo"] = odRdr[3];
                                        dr["Status"] = odRdr[4];
                                        dr["IMFeeAmendment"] = odRdr[5];
                                        dr["IMPaymentDateAmendment"] = odRdr[6];
                                        dr["SACode"] = odRdr[7];
                                        dr["SAName"] = odRdr[8];
                                        dr["IFUA"] = odRdr[9];
                                        dr["Name"] = odRdr[10];
                                        dr["SID"] = odRdr[11];
                                        dr["FundCode"] = odRdr[12];
                                        dr["FundName"] = odRdr[13];
                                        dr["IMCode"] = odRdr[14];
                                        dr["IMName"] = odRdr[15];
                                        dr["CBCode"] = odRdr[16];
                                        dr["CBName"] = odRdr[17];
                                        dr["FundCCY"] = odRdr[18];
                                        dr["AmountNominal"] = Convert.ToDecimal(odRdr[19].ToString() == "" ? 0 : odRdr[19].Equals(DBNull.Value) == true ? 0 : odRdr[19]);
                                        dr["AmountUnit"] = Convert.ToDecimal(odRdr[20].ToString() == "" ? 0 : odRdr[20].Equals(DBNull.Value) == true ? 0 : odRdr[20]);
                                        dr["AllUnit"] = odRdr[21];
                                        dr["FeeNominal"] = Convert.ToDecimal(odRdr[22].ToString() == "" ? 0 : odRdr[22].Equals(DBNull.Value) == true ? 0 : odRdr[22]);
                                        dr["FeeUnit"] = Convert.ToDecimal(odRdr[23].ToString() == "" ? 0 : odRdr[23].Equals(DBNull.Value) == true ? 0 : odRdr[23]);
                                        dr["FeePercent"] = Convert.ToDecimal(odRdr[24].ToString() == "" ? 0 : odRdr[24].Equals(DBNull.Value) == true ? 0 : odRdr[24]);
                                        dr["TransferPath"] = odRdr[25];
                                        dr["PaymentCode"] = odRdr[26].ToString();
                                        dr["BICCode"] = odRdr[27];
                                        dr["BIMemberCode"] = odRdr[28];
                                        dr["BankName"] = odRdr[29];
                                        dr["PaymentNo"] = odRdr[30];
                                        dr["PaymentName"] = odRdr[31];
                                        //dr["PaymentDate"] = odRdr[32];
                                        dr["PaymentDate"] = _strPaymentDate;
                                        dr["TransferType"] = odRdr[33];
                                        dr["InputDate"] = _strInputDate;
                                        dr["UploadReference"] = odRdr[35];
                                        dr["SAReference"] = odRdr[36];
                                        dr["IMStatus"] = odRdr[37];
                                        dr["CBStatus"] = odRdr[38];
                                        dr["CBCompletionStatus"] = odRdr[39];


                                        if (dr["TransactionDate"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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

        public string UpdatePaymentSInvestTempImportSwitching(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                //delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table trxaperd";
                        cmd2.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.trxaperd";
                    bulkCopy.WriteToServer(CreateDataTableFromUpdatePaymentSInvestSwitchingTempExcelFile(_fileSource));

                    _msg = "";
                }

                //validasi custom CAM untuk exclude SACode yg tidak ada
                if (Tools.ClientCode == "21")
                {
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText =
                               @"  

                            declare @tableSACodeFundCLient table (
	                            SACode nvarchar(100)
                            )

                            declare @tableSACodeFailed table (
	                            SACode nvarchar(100)
                            )

                            declare @Msg nvarchar(max)
                            Declare @date datetime 
                            select top 1 @Date = TransactionDate From TrxAperd

                            insert into @tableSACodeFundCLient
                            select distinct SACode from FundClient where status = 2

                            insert into @tableSACodeFailed
                            select distinct SACode from trxaperd where TransactionDate = @Date and SACode not in (
	                            select * from @tableSACodeFundCLient
                            )

                            if exists ( select * from @tableSACodeFailed )
                            begin
	                            select @Msg = COALESCE(@Msg + ', ', '') + SACode from @tableSACodeFailed

	                            select 'Selling agent codes not found on fund client<br/> Please check SAcode : ' + @Msg Result
                            end


                            ";
                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    return dr["Result"].ToString();
                                }
                            }

                        }
                        conn.Close();
                    }
                }

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd1 = conn.CreateCommand())
                    {
                        cmd1.CommandText =
                        @"  

Declare @date datetime 
select top 1 @Date = TransactionDate From TrxAperd

delete updatePaymentSinvestTemp where TransactionDate = @Date and TransactionType in ('switching')

Declare @MaxPK int
select @MaxPK = Max(UpdatePaymentSInvestTempPK) from UpdatePaymentSInvestTemp
set @maxPK = isnull(@maxPK,0)

insert into UpdatePaymentSInvestTemp(UpdatePaymentSInvestTempPK,Selected,TransactionDate,TransactionType,RefNumber,
SellingAgentCode,IFUA,FundCode,AmountCash,AmountUnit,FeePercent,BICCode,BankAcc,BankNo,PaymentDate,
TransferType,ReferenceNumber,FeeNominal,InFundCode,InAmountCash,SwitchFeeCharge)

select @MaxPK + ROW_NUMBER() OVER(ORDER BY transactiontype ASC) UpdatePaymentSInvestTempPK,0, 
TransactionDate, TransactionType, SAReference, SACode, IFUA, SwitchOutFundCode,SwitchOutNominal , SwitchOutUnit, FeePercent, BIMemberCode, 
PaymentName,PaymentNo,
Case when PaymentDate <> '' Then dbo.FWorkingDay(TransactionDate,B.DefaultPaymentDate) else TransactionDate end,
TransferType, UploadReference,FeeNominal,FundCode,AmountNominal,SwitchingFeeChargeFund
from TrxAperd A
left join Fund B on A.SwitchOutFundCode = B.SinvestCode and B.status In (1,2)
where A.Status not in ('rejected','failed')





";


                        cmd1.ExecuteNonQuery();

                    }
                    _msg = "Import Success";

                }

                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromUpdatePaymentSInvestSwitchingTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "TransactionDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TransactionType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ReferenceNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Status";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMFeeAmendment";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMPaymentDateAmendment";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SACode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SAName";
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
                    dc.ColumnName = "SID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCCY";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "AmountNominal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "AmountUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "AllUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "FeeNominal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "FeeUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "FeePercent";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TransferPath";
                    dc.Unique = false;
                    dt.Columns.Add(dc);
                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentCode";
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
                    dc.ColumnName = "BankName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TransferType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "InputDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "UploadReference";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SAReference";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMStatus";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBStatus";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBCompletionStatus";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutIn";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchInAmountCCY";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundSubscriptionName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundSubsriptionNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutCBStatus";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutCompletionStatus";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutFundCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutFundName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutCBCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutCBName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutFundCCY";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "SwitchOutNominal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "SwitchOutUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchOutAllUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SwitchingFeeChargeFund";
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
                                    //dr["TransactionType"] = odRdr[2];
                                    string _strTransactionDate = Convert.ToString(odRdr[1]);
                                    if (!string.IsNullOrEmpty(_strTransactionDate))
                                    {
                                        string _tgl = _strTransactionDate.Substring(6, 2);
                                        string _bln = _strTransactionDate.Substring(4, 2);
                                        string _thn = _strTransactionDate.Substring(0, 4);

                                        _strTransactionDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                                    }

                                    string _strInputDate = Convert.ToString(odRdr[38]);
                                    if (!string.IsNullOrEmpty(_strInputDate))
                                    {
                                        string _tglInputDate = _strInputDate.Substring(6, 2);
                                        string _blnInputDate = _strInputDate.Substring(4, 2);
                                        string _thnInputDate = _strInputDate.Substring(0, 4);

                                        _strInputDate = _blnInputDate + "/" + _tglInputDate + "/" + _thnInputDate; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                                    }

                                    dr["TransactionDate"] = _strTransactionDate; //odRdr[1];
                                    dr["TransactionType"] = odRdr[2];
                                    dr["ReferenceNo"] = odRdr[3];
                                    dr["Status"] = odRdr[5];
                                    dr["IMFeeAmendment"] = odRdr[6];
                                    dr["IMPaymentDateAmendment"] = odRdr[7];
                                    dr["SACode"] = odRdr[8];
                                    dr["SAName"] = odRdr[9];
                                    dr["IFUA"] = odRdr[10];
                                    dr["Name"] = odRdr[11];
                                    dr["SID"] = odRdr[12];
                                    dr["FundCode"] = odRdr[27];
                                    dr["FundName"] = odRdr[28];
                                    dr["IMCode"] = odRdr[13];
                                    dr["IMName"] = odRdr[14];
                                    dr["CBCode"] = odRdr[29];
                                    dr["CBName"] = odRdr[30];
                                    dr["FundCCY"] = odRdr[31];
                                    dr["AmountNominal"] = Convert.ToDecimal(odRdr[33].ToString() == "" ? 0 : odRdr[33].Equals(DBNull.Value) == true ? 0 : odRdr[33]);
                                    dr["AmountUnit"] = 0;
                                    dr["AllUnit"] = 0;
                                    dr["FeeNominal"] = Convert.ToDecimal(odRdr[24].ToString() == "" ? 0 : odRdr[24].Equals(DBNull.Value) == true ? 0 : odRdr[24]);

                                    dr["FeeUnit"] = Convert.ToDecimal(odRdr[25].ToString() == "" ? 0 : odRdr[25].Equals(DBNull.Value) == true ? 0 : odRdr[25]);

                                    dr["FeePercent"] = Convert.ToDecimal(odRdr[26].ToString() == "" ? 0 : odRdr[26].Equals(DBNull.Value) == true ? 0 : odRdr[26]);
                                    dr["TransferPath"] = "";
                                    dr["PaymentCode"] = "";
                                    dr["BICCode"] = "";
                                    dr["BIMemberCode"] = "";
                                    dr["BankName"] = "";
                                    dr["PaymentNo"] = "";
                                    dr["PaymentName"] = "";
                                    dr["PaymentDate"] = odRdr[36];
                                    dr["TransferType"] = odRdr[37];
                                    dr["InputDate"] = _strInputDate;
                                    dr["UploadReference"] = odRdr[39];
                                    dr["SAReference"] = odRdr[40];
                                    dr["IMStatus"] = odRdr[41];
                                    dr["CBStatus"] = odRdr[43];
                                    dr["CBCompletionStatus"] = odRdr[45];
                                    dr["SwitchOutIn"] = odRdr[4];
                                    dr["SwitchInAmountCCY"] = odRdr[32];
                                    dr["FundSubscriptionName"] = odRdr[34];
                                    dr["FundSubsriptionNo"] = odRdr[35];
                                    dr["SwitchOutCBStatus"] = odRdr[42];
                                    dr["SwitchOutCompletionStatus"] = odRdr[44];
                                    dr["SwitchOutFundCode"] = odRdr[15];
                                    dr["SwitchOutFundName"] = odRdr[16];
                                    dr["SwitchOutCBCode"] = odRdr[17];
                                    dr["SwitchOutCBName"] = odRdr[18];

                                    dr["SwitchOutFundCCY"] = odRdr[19];
                                    dr["SwitchOutNominal"] = Convert.ToDecimal(odRdr[20].ToString() == "" ? 0 : odRdr[20].Equals(DBNull.Value) == true ? 0 : odRdr[20]);
                                    dr["SwitchOutUnit"] = Convert.ToDecimal(odRdr[21].ToString() == "" ? 0 : odRdr[21].Equals(DBNull.Value) == true ? 0 : odRdr[21]);
                                    dr["SwitchOutAllUnit"] = odRdr[22];
                                    dr["SwitchingFeeChargeFund"] = odRdr[23];
                                    
                                    if (dr["TransactionDate"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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



        public List<UpdatePaymentSInvestTemp> UpdatePaymentSInvestTemp_Select(DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UpdatePaymentSInvestTemp> L_Model = new List<UpdatePaymentSInvestTemp>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (Tools.ClientCode == "24")
                        {
                            cmd.CommandText = @"   
                            declare @paramDate date
                            declare @paramCompanyID nvarchar(100)

                            set @paramDate = @date
                            set @paramCompanyID = @CompanyID
                         
                            Select case when SellingAgentCode = @paramCompanyID  then Z.FundClientPK else C.FundClientPK end,D.agentPK,B.Name FundName,case when SellingAgentCode = @paramCompanyID  then Z.Name else C.Name end SellingAgentName  ,MV.DescOne TransferTypeDesc,Case when TransactionType = 'Subscription' then TransactionDate Else 
                            case when TransactionType = 'Redemption' 
                            then PaymentDate else PaymentDate End End PaymentDate,
                            A.UpdatePaymentSInvestTempPK,A.Selected,A.TransactionDate,A.TransactionType,A.RefNumber,A.SellingAgentCode,A.IFUA,A.FundCode,
                            A.AmountCash,A.AmountUnit,A.FeePercent,A.BICCode,A.BankAcc,A.TransferType,A.ReferenceNumber from UpdatePaymentSInvestTemp A 
                            Left join Fund B on A.FundCode = B.SinvestCode and B.status in (1,2)
                            Left join FundClient C on A.SellingAgentCode = C.SACode and C.Status in (1,2)
                            Left join FundClient Z on A.IFUA = Z.IFUACode and Z.Status in (1,2)
                            Left join Agent D on C.SellingAgentPK = D.AgentPK and D.Status in (1,2)
                            Left join MasterValue MV on A.TransferType = MV.DescOne and MV.Status in (1,2) and MV.ID  = 'TransferTypeRedemption'
                            where A.TransactionDate = @paramDate and A.IFUA <> ''
                            union all
                            Select C.FundClientPK,D.agentPK,B.Name FundName,C.Name SellingAgentName  ,MV.DescOne TransferTypeDesc,Case when TransactionType = 'Subscription' then TransactionDate Else 
                            case when TransactionType = 'Redemption' 
                            then PaymentDate else PaymentDate End End PaymentDate,
                            A.UpdatePaymentSInvestTempPK,A.Selected,A.TransactionDate,A.TransactionType,A.RefNumber,A.SellingAgentCode,A.IFUA,A.FundCode,
                            A.AmountCash,A.AmountUnit,A.FeePercent,A.BICCode,A.BankAcc,A.TransferType,A.ReferenceNumber from UpdatePaymentSInvestTemp A 
                            Left join Fund B on A.FundCode = B.SinvestCode and B.status in (1,2)
                            Left join FundClient C on A.SellingAgentCode = C.SACode and C.Status in (1,2)
                            Left join Agent D on C.SellingAgentPK = D.AgentPK and D.Status in (1,2)
                            Left join MasterValue MV on A.TransferType = MV.DescOne and MV.Status in (1,2) and MV.ID  = 'TransferTypeRedemption'
                            where A.TransactionDate = @paramDate and A.IFUA = '' ";
                        }
                        else
                        {
                            cmd.CommandText = @"
                            
 
                   
                            Select case when SellingAgentCode = @CompanyID then Z.FundClientPK else C.FundClientPK end,D.agentPK,B.Name FundName,case when SellingAgentCode = @CompanyID then Z.Name else C.Name end SellingAgentName  ,MV.DescOne TransferTypeDesc,Case when TransactionType = 'Subscription' then TransactionDate Else 
                            case when TransactionType = 'Redemption' 
                            then PaymentDate else PaymentDate End End PaymentDate,
                            A.UpdatePaymentSInvestTempPK,A.Selected,A.TransactionDate,A.TransactionType,A.RefNumber,A.SellingAgentCode,A.IFUA,A.FundCode,
                            A.AmountCash,A.AmountUnit,A.FeePercent,A.BICCode,A.BankAcc,A.TransferType,A.ReferenceNumber from UpdatePaymentSInvestTemp A 
                            Left join Fund B on A.FundCode = B.SinvestCode and B.status in (1,2)
                            Left join FundClient C on A.SellingAgentCode = C.SACode and C.Status in (1,2)
                            Left join FundClient Z on A.IFUA = Z.IFUACode and Z.Status in (1,2)
                            Left join Agent D on C.SellingAgentPK = D.AgentPK and D.Status in (1,2)
                            Left join MasterValue MV on A.TransferType = MV.DescOne and MV.Status in (1,2) and MV.ID  = 'TransferTypeRedemption'
                            where A.TransactionDate = @Date


                            ";
                        }



                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setUpdatePaymentSInvestTemp(dr));
                                }
                            }
                            return L_Model;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<UpdatePaymentSInvestTemp_SAandFund> UpdatePaymentSInvestTemp_SelectSA()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UpdatePaymentSInvestTemp_SAandFund> L_Model = new List<UpdatePaymentSInvestTemp_SAandFund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                                    Select distinct C.Name SellingAgentName from UpdatePaymentSInvestTemp A 
                                    Left join Fund B on A.FundCode = B.SinvestCode and B.status = 2
                                    Left join FundClient C on A.SellingAgentCode = C.SACode and C.Status = 2  
                                    where (C.Name is not null or C.Name <> '')
                            ";


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    UpdatePaymentSInvestTemp_SAandFund M_Model = new UpdatePaymentSInvestTemp_SAandFund();
                                    M_Model.SA = dr["SellingAgentName"].ToString();
                                    L_Model.Add(M_Model);
                                }
                            }
                            return L_Model;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<UpdatePaymentSInvestTemp_SAandFund> UpdatePaymentSInvestTemp_SelectFund()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UpdatePaymentSInvestTemp_SAandFund> L_Model = new List<UpdatePaymentSInvestTemp_SAandFund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                                    Select distinct B.Name FundName from UpdatePaymentSInvestTemp A 
                                    Left join Fund B on A.FundCode = B.SinvestCode and B.status = 2
                                    Left join FundClient C on A.SellingAgentCode = C.SACode and C.Status = 2  
                                    where (B.Name is not null or B.Name <> '')
                            ";


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    UpdatePaymentSInvestTemp_SAandFund M_Model = new UpdatePaymentSInvestTemp_SAandFund();
                                    M_Model.Fund = dr["FundName"].ToString();
                                    L_Model.Add(M_Model);
                                }
                            }
                            return L_Model;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void UpdatePaymentSInvestTemp_ChangePaymentDate_BySelected(DateTime _date, DateTime _trxDate,UpdatePaymentSInvestTemp _updatePaymentSInvestTemp)
        {
            try
            {
                string paramUpdatePaymentSInvestTempSelected = "";
                //paramUpdatePaymentSInvestTempSelected = "UpdatePaymentSInvestTempPK in (" + _updatePaymentSInvestTemp.UpdatePaymentSInvestTempSelected + ") ";
                if (!_host.findString(_updatePaymentSInvestTemp.UpdatePaymentSInvestTempSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_updatePaymentSInvestTemp.UpdatePaymentSInvestTempSelected))
                {
                    paramUpdatePaymentSInvestTempSelected = "  UpdatePaymentSInvestTempPK in (" + _updatePaymentSInvestTemp.UpdatePaymentSInvestTempSelected + ") ";
                }
                else
                {
                    paramUpdatePaymentSInvestTempSelected = "  UpdatePaymentSInvestTempPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            update UpdatePaymentSInvestTemp set PaymentDate = @Date where " + paramUpdatePaymentSInvestTempSelected + @" and TransactionDate = @TransactionDate
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@TransactionDate", _trxDate);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void UpdatePaymentSInvestTemp_ChangePaymentDate_BySAandFund(DateTime _date, DateTime _trxDate, UpdatePaymentSInvestTemp_SAandFund _m)
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
                            update A Set A.PaymentDate = @Date from UpdatePaymentSInvestTemp A
                            left join Fund B on A.FundCode = B.SInvestCode and B.status = 2
                            left join FundClient C on A.SellingAgentCode = C.SACode and C.status = 2
                            where B.name = @FundName and C.Name = @SellingAgentName and A.TransactionDate = @TransactionDate
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@TransactionDate", _trxDate);
                        cmd.Parameters.AddWithValue("@FundName", _m.Fund);
                        cmd.Parameters.AddWithValue("@SellingAgentName", _m.SA);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string GenerateToSInvest_BySelected(string _trxType, DateTime _trxDate,UpdatePaymentSInvestTemp _updatePaymentSInvestTemp)
        {

            try
            {
                string paramUpdatePaymentSInvestTempSelected = "";
                //paramUpdatePaymentSInvestTempSelected = "UpdatePaymentSInvestTempPK in (" + _updatePaymentSInvestTemp.UpdatePaymentSInvestTempSelected + ") ";
                if (!_host.findString(_updatePaymentSInvestTemp.UpdatePaymentSInvestTempSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_updatePaymentSInvestTemp.UpdatePaymentSInvestTempSelected))
                {
                    paramUpdatePaymentSInvestTempSelected = "  UpdatePaymentSInvestTempPK in (" + _updatePaymentSInvestTemp.UpdatePaymentSInvestTempSelected + ") ";
                }
                else
                {
                    paramUpdatePaymentSInvestTempSelected = "  UpdatePaymentSInvestTempPK in (0) ";
                }
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    if (_trxType != "Switching")
                    {
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
                                    Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), TransactionDate, 112),''))))
                                    + '|' + case when TransactionType = 'Subscription' then '1' else '2' end
                                    + '|' + isnull(ReferenceNo,'')
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SellingAgentCode,''))))
                                    + '|' + isnull(IFUA,'')
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FundCode,''))))
                                    + '|' + case when AmountCash = 0 then '' else cast(isnull(Round(AmountCash,2),'')as nvarchar) end
                                    + '|' + case when AmountUnit = 0 then '' else cast(isnull(Round(AmountUnit,4),'')as nvarchar) end
                                    + '|' + 
                                    + '|' + 
                                    + '|' + 
                                    + '|' + case when FeePercent = 0 then '' else cast(isnull(Round(FeePercent,2),'')as nvarchar) end
                                    + '|' + 
                                    + '|' + 
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BICCode,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(PaymentNo,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), Case when TransactionType = 'Subscription' then TransactionDate Else
                                    case when TransactionType = 'Redemption' then paymentDate else PaymentDate End End , 112) <> '19000101' then CONVERT(VARCHAR(10), Case when TransactionType = 'Subscription' then TransactionDate Else
                                    case when TransactionType = 'Redemption' then PaymentDate else PaymentDate End End, 112) else '' End),''))))            
                                    + '|' + case when TransferType = 'SKNBI' then '1' else case when TransferType = 'RTGS' then '2' else case when TransferType = 'N/A' then '3' Else '' End End End
                                    + '|' + isnull(RefNumber,'')
                                    from (   
                                    select A.RefNumber,B.ReferenceNo,A.TransactionDate,A.TransactionType,A.SellingAgentCode,A.IFUA,A.FundCode,A.AmountCash,A.AmountUnit,
                                    A.FeePercent,A.BICCode,A.BankAcc,A.PaymentDate,A.TransferType,A.ReferenceNumber,B.PaymentNo from UpdatePaymentSInvestTemp A
                                    left join TrxAperd B on A.RefNumber = B.SAReference and isnull(A.AmountUnit,0) = isnull(B.AmountUnit,0) 
                                    and isnull(A.AmountCash,0) = isnull(B.AmountNominal,0) and  isnull(A.FundCode,0) = isnull(B.FundCode,0) and isnull(A.IFUA,0) = isnull(B.IFUA,0)                                     
                                    where " + paramUpdatePaymentSInvestTempSelected + @" and A.TransactionType = 'Redemption' and A.TransactionDate = @TrxDate and SellingAgentCode not in
                                    (select ID from Company where status  = 2)
                                    )A    
                                    order by TransactionDate Asc     
                                    select * from #text                       
                                    END 



                        ";
                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            cmd.Parameters.AddWithValue("@TrxDate", _trxDate);

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    string filePath = Tools.SInvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_REDM_Order.rad";
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
                                        return Tools.HtmlSinvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_REDM_Order.rad";
                                    }

                                }
                                return null;
                            }
                        }
                    }

                    else
                    {
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
                                    Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), TransactionDate, 112),''))))
                                    + '|' + '3'
                                    + '|' + isnull(RefNumber,'')
                                    + '|' + @CompanyID
                                    + '|' + isnull(A.IFUA,'')
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundCode,''))))
                                    + '|' + case when A.AmountCash = 0 then '' else cast(isnull(cast(A.AmountCash as decimal(22,2)),'')as nvarchar) end
                                    + '|' + case when A.AmountUnit = 0 then '' else cast(isnull(cast(A.AmountUnit as decimal(22,4)),'') as nvarchar) end
                                    + '|' + ''
                                    + '|' + 
                                    + '|' + 
                                    + '|' + 
                                    + '|' + case when A.FeePercent = 0 then '' else cast(isnull(cast(A.FeePercent as decimal(10,2)),'')as nvarchar) end
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InFundCode,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), PaymentDate, 112) <> '19000101' then CONVERT(VARCHAR(10), PaymentDate, 112) else '' End),''))))          
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                                    + '|' + isnull(ReferenceNumber,'')
                                    from (   
                                    select * from UpdatePaymentSInvestTemp where selected = 1 and TransactionType = 'Switching' and TransactionDate = @TrxDate and SellingAgentCode not in
                                    (select ID from Company where status  = 2)
                                    )A    
                                    order by TransactionDate Asc     
                                    select * from #text          
                                    END 

                        ";
                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            cmd.Parameters.AddWithValue("@TrxDate", _trxDate);

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    string filePath = Tools.SInvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_Order.rad";
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
                                        return Tools.HtmlSinvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_Order.rad";
                                    }

                                }
                                return null;
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


        public string MoveToSubsRedemp(DateTime _trxDate, UpdatePaymentSInvestTemp _updatePaymentSInvestTemp)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (Tools.ClientCode == "24")
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText =
                               @"

DECLARE @TransactionDate datetime
declare @PaymentDate datetime
declare @TransactionType nvarchar(500)
declare @FundCode nvarchar(500)
declare @FundPK int
declare @FundPKTo int
declare @SellingAgentCode nvarchar(500)
declare @FundClientPK int
declare @TotalCashAmount numeric(22,2)
declare @TotalCashAmountTo numeric(22,2)
declare @TotalUnitAmount numeric(22,4)
declare @FeePercent numeric(18,2)
declare @ClientRedemptionPK int
declare @AgentPK int
declare @AgentFeePercent numeric(18,2)
DECLARE @FeeAmount NUMERIC(18,2)
DECLARE @CurrencyPK INT
DECLARE @RefNumber NVARCHAR(200)
DECLARE @SwitchFeeCharge NVARCHAR(50)
Declare @CurrencyPKTo int


DECLARE A CURSOR FOR 

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountCash,0) > 0
and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0)>= 0)
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK

UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,0 TotalCashAmount,0 TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountUnit,0) > 0
and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0) >= 0)
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK



UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) > 0 or ISNULL(FeeNominal,0) > 0) and TransactionType in ('redemption') and A.IFUA <> ''
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK

UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0) >= 0) and A.TransactionType = 'subscription'
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0)


UNION ALL

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,sum(A.TotalCashAmount) TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
0 TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) = 0 and ISNULL(FeeNominal,0) = 0) and A.TransactionType = 'redemption' and A.IFUA <> ''
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit
) A where A.TotalCashAmount > 0 
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo


UNION ALL

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,0 TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
sum(A.TotalUnitAmount) TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) = 0 and ISNULL(FeeNominal,0) = 0) and A.TransactionType = 'redemption'  and A.IFUA <> ''
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit
) A where A.TotalUnitAmount > 0 
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo

UNION ALL -- BUAT APERD

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,sum(A.TotalCashAmount) TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
sum(A.TotalUnitAmount) TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and A.TransactionType = 'redemption'  and A.IFUA = ''
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit
) A where A.TotalUnitAmount > 0 and A.TotalCashAmount > 0
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo






Open A
Fetch Next From A
Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo



While @@FETCH_STATUS = 0
BEGIN

declare @FundCashRefPK int
declare @FundCashRefPKTo INT


SET @FundCashRefPKTo = NULL
SET @FundCashRefPK = null

select @FundCashRefPK = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPK 


select @FundCashRefPKTo = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPKTo 


if isnull(@TransactionType,0) = 'Redemption' 
	BEGIN

	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 2 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END
		Update ClientRedemption set status  =  3 where ValueDate = @TransactionDate and FundPK  = @FundPK and FundClientPK = @FundClientPK and status in (1,2) and posted = 0
	END
else if isnull(@TransactionType,0) = 'Switching' 
	BEGIN 


	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		Update ClientSwitching set status  =  3 where ValueDate = @TransactionDate and FundPKFrom  = @FundPK and FundPKTo  = @FundPKTo and FundClientPK = @FundClientPK and status in (1,2) and posted = 0
	END
ELSE
if isnull(@TransactionType,0) = 'Subscription' 
	BEGIN
	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 1 AND status IN (1,2)
END
		Update ClientSubscription set status  =  3 where ValueDate = @TransactionDate and FundPK  = @FundPK and FundClientPK = @FundClientPK and status in (1,2) and posted = 0 and Type not in (6) 
	END


Fetch next From A Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo
END
Close A
Deallocate A    



DECLARE A CURSOR FOR 


select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo,A.RefNumber FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountCash,0) > 0
and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0)>= 0)
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK,A.RefNumber

UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,0 TotalCashAmount,0 TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo,A.RefNumber FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountUnit,0) > 0
and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0) >= 0)
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK,A.RefNumber



UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo,A.RefNumber FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) > 0 or ISNULL(FeeNominal,0) > 0) and TransactionType in ('redemption')  and A.IFUA <> ''
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK,A.RefNumber

UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo,A.RefNumber
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0) >= 0) and A.TransactionType = 'subscription'
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),A.RefNumber


UNION ALL

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,sum(A.TotalCashAmount) TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
0 TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo,A.RefNumber from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo,A.RefNumber
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) = 0 and ISNULL(FeeNominal,0) = 0) and A.TransactionType = 'redemption' and A.IFUA <> ''
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK, 
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit,A.RefNumber
) A where A.TotalCashAmount > 0
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo,A.RefNumber


UNION ALL

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,0 TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
sum(A.TotalUnitAmount) TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo,A.RefNumber from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo,A.RefNumber
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) = 0 and ISNULL(FeeNominal,0) = 0) and A.TransactionType = 'redemption' and A.IFUA <> ''
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit,A.RefNumber
) A where A.TotalUnitAmount > 0
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo,A.RefNumber


UNION ALL -- BUAT APERD

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,sum(A.TotalCashAmount) TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
sum(A.TotalUnitAmount) TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo,A.RefNumber from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo,A.RefNumber
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and A.TransactionType = 'redemption' and A.IFUA = ''
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit,A.RefNumber
) A where A.TotalUnitAmount > 0 and A.TotalCashAmount > 0 
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo,A.RefNumber


Open A
Fetch Next From A
Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo,@RefNumber



While @@FETCH_STATUS = 0
BEGIN


SET @FundCashRefPKTo = NULL
SET @FundCashRefPK = null

select @FundCashRefPK = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPK 


select @FundCashRefPKTo = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPKTo 


if isnull(@TransactionType,0) = 'Redemption' 
	BEGIN

	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 2 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		
		
		INSERT INTO ClientRedemption(ClientRedemptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,PaymentDate,NAV,
		FundPK,FundClientPK,CashRefPK,BitRedemptionAll, Description,CashAmount,UnitAmount,TotalCashAmount,
		TotalUnitAmount,RedemptionFeePercent,RedemptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,
		CurrencyPK,UnitPosition,BankRecipientPK,TransferType,IsBOTransaction,BitSInvest,TransactionPK,IsFrontSync,
		ReferenceSinvest,EntryUsersID,EntryTime,LastUpdate)
		
		SELECT TOP 1 isnull(ClientRedemptionPk,0) + 1,1,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,@TransactionDate,@TransactionDate,@PaymentDate,0,
		@FundPK,@FundClientPK,case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end ,0,case when @RefNumber = 'Transaction APERD Summary' then @RefNumber else 'Update From SInvest' end,
		ISNULL(@TotalCashAmount,0),ISNULL(@TotalUnitAmount,0),ISNULL(@TotalCashAmount,0) - ISNULL(@FeeAmount,0),
		ISNULL(@TotalUnitAmount,0),ISNULL(@FeePercent,0),ISNULL(@FeeAmount,0),ISNULL(@AgentPK,0),
		ISNULL(@AgentFeePercent,0),ISNULL(@TotalCashAmount * @AgentFeePercent/100,0),1,ISNULL(@CurrencyPk,0),0,0,case when @TotalCashAmount > 100000000 then 2 else 1 end,1,1,0,0,'',
		@UsersID,@Time,@Time from ClientRedemption
		group by ClientRedemptionPK
		ORDER BY ClientRedemptionPK desc


	

                            

	END
else if isnull(@TransactionType,0) = 'Switching' 
	BEGIN 


	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		
	    
		INSERT INTO ClientSwitching(ClientSwitchingPK,HistoryPK,Status,FeeTypeMethod,NAVDate,ValueDate,PaymentDate,
		NAVFundFrom,NAVFundTo,FundPKFrom,FundPKTo,FundClientPK,FeeType,CashRefPKFrom,CashRefPKTo, Description,
		CashAmount,UnitAmount,TotalCashAmountFundFrom,TotalCashAmountFundTo,TotalUnitAmountFundFrom,TotalUnitAmountFundTo,
		SwitchingFeePercent,SwitchingFeeAmount,CurrencyPK,TransferType,
        BitSwitchingAll,UserSwitchingPK,TransactionPK,IsBoTransaction,BitSInvest,FeeTypeMode,IsProcessed,IsFrontSync,
		ReferenceSinvest,AgentPK,EntryUsersID,EntryTime,LastUpdate)	
	    
		SELECT TOP 1 isnull(ClientSwitchingPk,0) + 1,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,@TransactionDate,@TransactionDate,@PaymentDate,0,0,
		@FundPK,@FundPKTo,@FundClientPK,ISNULL(@SwitchFeeCharge,'OUT'),case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end ,
		CASE when isnull(@FundCashRefPKTo,0) <> 0 then @FundCashRefPKTo else 1 end ,'Update From SInvest',@TotalCashAmount,
		@TotalUnitAmount,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,0,
		ISNULL(@FeePercent,0) ,ISNULL(@FeeAmount,0),@CurrencyPKTo,
        case when @TotalCashAmount > 100000000 then 2 else 1 end,0,0,0,1,1,1,0,0,'',@AgentPK,@UsersID,@Time,@Time from ClientSwitching
		group by ClientSwitchingPK
		ORDER BY ClientSwitchingPK desc

	

	END
ELSE
if isnull(@TransactionType,0) = 'Subscription' 
	BEGIN
	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 1 AND status IN (1,2)
END
		
	
		INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
		NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
		SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
		BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,EntryUsersID,EntryTime,LastUpdate)
	
		Select TOP 1 ISNULL(ClientSubscriptionPK,0) + 1,1,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,@TransactionDate,@TransactionDate,
		0,@FundPK,@FundClientPK,case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end,case when @RefNumber = 'Transaction APERD Summary' then @RefNumber else 'Update From SInvest' end,ISNULL(@TotalCashAmount,0),
		ISNULL(@TotalUnitAmount,0),ISNULL(@TotalCashAmount,0) - ISNULL(@FeeAmount,0),
		ISNULL(@TotalUnitAmount,0),case when ISNULL(@FeePercent,0) = 0 then (100 * @FeeAmount / (@TotalCashAmount - @FeeAmount)) else ISNULL(@FeePercent,0) end,ISNULL(@FeeAmount,0),@AgentPK,@AgentFeePercent,@TotalCashAmount * @AgentFeePercent,
		1,ISNULL(@CurrencyPK,0),null,1,1,0,0,0,'',@UsersID,@Time,@Time from ClientSubscription
		group by ClientSubscriptionPK
		ORDER BY ClientSubscriptionPK desc

	

	END


Fetch next From A Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo,@RefNumber
END
Close A
Deallocate A       
            
                         ";
                        }
                        else if (Tools.ClientCode == "29") // custom untuk fee nominal switching tidak di grouping
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText =
                               @"

--declare @CompanyID nvarchar(100)
--declare @TrxDate date
--declare @UsersID nvarchar(100)
--declare @Time datetime

--select @CompanyID = ID from Company where status = 2
--set @TrxDate = '2020-11-26'
--set @UsersID = 'admin'
--set @Time = getdate()

--delete ClientSubscription where ValueDate = @TrxDate
--delete ClientRedemption where ValueDate = @TrxDate
--delete ClientSwitching where ValueDate = @TrxDate


DECLARE @TransactionDate datetime
declare @PaymentDate datetime
declare @TransactionType nvarchar(500)
declare @FundCode nvarchar(500)
declare @FundPK int
declare @FundPKTo int
declare @SellingAgentCode nvarchar(500)
declare @FundClientPK int
declare @TotalCashAmount numeric(22,2)
declare @TotalCashAmountTo numeric(22,2)
declare @TotalUnitAmount numeric(22,4)
declare @FeePercent numeric(18,2)
declare @ClientRedemptionPK int
declare @AgentPK int
declare @AgentFeePercent numeric(18,2)
DECLARE @FeeAmount NUMERIC(18,2)
DECLARE @CurrencyPK INT
DECLARE @SwitchFeeCharge NVARCHAR(50)
Declare @CurrencyPKTo int


DECLARE A CURSOR FOR 

--SWITCHING WITH CASH AMOUNT
--FEE PERCENT DI CONVERT KE CASH AMOUNT, FEE PERCENT JADI 0
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode, sum(AmountCash) TotalCashAmount,
sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,0 FeePercent,sum(case when isnull(FeePercent,0) > 0 then FeePercent/100 * AmountCash else ISNULL(FeeNominal,0) end) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountCash,0) > 0
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK

UNION ALL

--SWITCHING WITH UNIT AMOUNT
--GROUP BY FEE PERCENT
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,0 TotalCashAmount,
0 TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountUnit,0) > 0
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK,FeePercent

UNION ALL

--REDEMP CASH AMOUNT
--FEE PERCENT DI CONVERT KE CASH AMOUNT, FEE PERCENT JADI 0
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode, sum(AmountCash) TotalCashAmount,
sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,0 FeePercent,sum(case when isnull(FeePercent,0) > 0 then FeePercent/100 * AmountCash else ISNULL(FeeNominal,0) end) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'redemption' and isnull(AmountCash,0) > 0 and A.IFUA <> ''
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK

UNION ALL

--REDEMP WITH UNIT AMOUNT
--GROUP BY FEE PERCENT
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode, 0 TotalCashAmount,
0 TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'redemption' and isnull(AmountUnit,0) > 0 and A.IFUA <> ''
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK,FeePercent

UNION ALL

--REDEMP CASH AMOUNT
--FEE PERCENT DI CONVERT KE CASH AMOUNT, FEE PERCENT JADI 0
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,TransactionDate,TransactionType,FundCode,SellingAgentCode, sum(AmountCash) TotalCashAmount,
sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,0 FeePercent,sum(case when isnull(FeePercent,0) > 0 then FeePercent/100 * AmountCash else ISNULL(FeeNominal,0) end) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'subscription' and isnull(AmountCash,0) > 0 
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK


Open A
Fetch Next From A
Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo



While @@FETCH_STATUS = 0
BEGIN

declare @FundCashRefPK int
declare @FundCashRefPKTo INT


SET @FundCashRefPKTo = NULL
SET @FundCashRefPK = null

select @FundCashRefPK = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPK 


select @FundCashRefPKTo = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPKTo 


if isnull(@TransactionType,0) = 'Redemption' 
	BEGIN

	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 2 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END
		Update ClientRedemption set status  =  3 where ValueDate = @TransactionDate and FundPK  = @FundPK and FundClientPK = @FundClientPK and status in (1,2) and posted = 0
	END
else if isnull(@TransactionType,0) = 'Switching' 
	BEGIN 


	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		Update ClientSwitching set status  =  3 where ValueDate = @TransactionDate and FundPKFrom  = @FundPK and FundPKTo  = @FundPKTo and FundClientPK = @FundClientPK and status in (1,2) and posted = 0
	END
ELSE
if isnull(@TransactionType,0) = 'Subscription' 
	BEGIN
	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 1 AND status IN (1,2)
END
		Update ClientSubscription set status  =  3 where ValueDate = @TransactionDate and FundPK  = @FundPK and FundClientPK = @FundClientPK and status in (1,2) and posted = 0 and Type not in (6) 
	END


Fetch next From A Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo
END
Close A
Deallocate A    



DECLARE A CURSOR FOR 

--SWITCHING WITH CASH AMOUNT
--FEE PERCENT DI CONVERT KE CASH AMOUNT, FEE PERCENT JADI 0
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode, sum(AmountCash) TotalCashAmount,
sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,0 FeePercent,sum(case when isnull(FeePercent,0) > 0 then FeePercent/100 * AmountCash else ISNULL(FeeNominal,0) end) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountCash,0) > 0
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK

UNION ALL

--SWITCHING WITH UNIT AMOUNT
--GROUP BY FEE PERCENT
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,0 TotalCashAmount,
0 TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountUnit,0) > 0
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK,FeePercent

UNION ALL

--REDEMP CASH AMOUNT
--FEE PERCENT DI CONVERT KE CASH AMOUNT, FEE PERCENT JADI 0
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode, sum(AmountCash) TotalCashAmount,
sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,0 FeePercent,sum(case when isnull(FeePercent,0) > 0 then FeePercent/100 * AmountCash else ISNULL(FeeNominal,0) end) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'redemption' and isnull(AmountCash,0) > 0 and A.IFUA <> ''
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK

UNION ALL

--REDEMP WITH UNIT AMOUNT
--GROUP BY FEE PERCENT
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode, 0 TotalCashAmount,
0 TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'redemption' and isnull(AmountUnit,0) > 0 and A.IFUA <> ''
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK,FeePercent

UNION ALL

--REDEMP CASH AMOUNT
--FEE PERCENT DI CONVERT KE CASH AMOUNT, FEE PERCENT JADI 0
select C.FundPK,E.FundPK FundPKTo,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,TransactionDate,TransactionType,FundCode,SellingAgentCode, sum(AmountCash) TotalCashAmount,
sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,0 FeePercent,sum(case when isnull(FeePercent,0) > 0 then FeePercent/100 * AmountCash else ISNULL(FeeNominal,0) end) FeeNominal,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK CurrencyPKTo from UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'subscription' and isnull(AmountCash,0) > 0 
group by C.FundPK,E.FundPK,B.FundClientPK,D.AgentPK,AgentFee,TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode,C.CurrencyPK,SwitchFeeCharge,E.CurrencyPK

Open A
Fetch Next From A
Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo



While @@FETCH_STATUS = 0
BEGIN


SET @FundCashRefPKTo = NULL
SET @FundCashRefPK = null

select @FundCashRefPK = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPK 


select @FundCashRefPKTo = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPKTo 


if isnull(@TransactionType,0) = 'Redemption' 
	BEGIN

	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 2 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		
		
		INSERT INTO ClientRedemption(ClientRedemptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,PaymentDate,NAV,
		FundPK,FundClientPK,CashRefPK,BitRedemptionAll, Description,CashAmount,UnitAmount,TotalCashAmount,
		TotalUnitAmount,RedemptionFeePercent,RedemptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,
		CurrencyPK,UnitPosition,BankRecipientPK,TransferType,IsBOTransaction,BitSInvest,TransactionPK,IsFrontSync,
		ReferenceSinvest,EntryUsersID,EntryTime,LastUpdate)
		
		SELECT TOP 1 isnull(ClientRedemptionPk,0) + 1,1,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,@TransactionDate,@TransactionDate,@PaymentDate,0,
		@FundPK,@FundClientPK,case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end ,0,'Update From SInvest',
		ISNULL(@TotalCashAmount,0),ISNULL(@TotalUnitAmount,0),ISNULL(@TotalCashAmount,0) - ISNULL(@FeeAmount,0),
		ISNULL(@TotalUnitAmount,0),ISNULL(@FeePercent,0),ISNULL(@FeeAmount,0),ISNULL(@AgentPK,0),
		ISNULL(@AgentFeePercent,0),ISNULL(@TotalCashAmount * @AgentFeePercent/100,0),1,ISNULL(@CurrencyPk,0),0,0,case when @TotalCashAmount > 100000000 then 2 else 1 end,1,1,0,0,'',
		@UsersID,@Time,@Time from ClientRedemption
		group by ClientRedemptionPK
		ORDER BY ClientRedemptionPK desc


	

                            

	END
else if isnull(@TransactionType,0) = 'Switching' 
	BEGIN 


	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		
	    
		INSERT INTO ClientSwitching(ClientSwitchingPK,HistoryPK,Status,FeeTypeMode,NAVDate,ValueDate,PaymentDate,
		NAVFundFrom,NAVFundTo,FundPKFrom,FundPKTo,FundClientPK,FeeType,CashRefPKFrom,CashRefPKTo, Description,
		CashAmount,UnitAmount,TotalCashAmountFundFrom,TotalCashAmountFundTo,TotalUnitAmountFundFrom,TotalUnitAmountFundTo,
		SwitchingFeePercent,SwitchingFeeAmount,CurrencyPK,TransferType,
        BitSwitchingAll,UserSwitchingPK,TransactionPK,IsBoTransaction,BitSInvest,FeeTypeMethod,IsProcessed,IsFrontSync,
		ReferenceSinvest,AgentPK,EntryUsersID,EntryTime,LastUpdate)	
	    
		SELECT TOP 1 isnull(ClientSwitchingPk,0) + 1,1,1,1,@TransactionDate,@TransactionDate,@PaymentDate,0,0,
		@FundPK,@FundPKTo,@FundClientPK,ISNULL(@SwitchFeeCharge,'OUT'),case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end ,
		CASE when isnull(@FundCashRefPKTo,0) <> 0 then @FundCashRefPKTo else 1 end ,'Update From SInvest',@TotalCashAmount,
		@TotalUnitAmount,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,0,
		ISNULL(@FeePercent,0) ,ISNULL(@FeeAmount,0),@CurrencyPKTo,
        case when @TotalCashAmount > 100000000 then 2 else 1 end,0,0,0,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,0,0,
        '',@AgentPK,@UsersID,@Time,@Time from ClientSwitching
		group by ClientSwitchingPK
		ORDER BY ClientSwitchingPK desc

	

	END
ELSE
if isnull(@TransactionType,0) = 'Subscription' 
	BEGIN
	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 1 AND status IN (1,2)
END
		
	
		INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
		NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
		SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
		BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,EntryUsersID,EntryTime,LastUpdate)
	
		Select TOP 1 ISNULL(ClientSubscriptionPK,0) + 1,1,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,@TransactionDate,@TransactionDate,
		0,@FundPK,@FundClientPK,case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end,'Update From SInvest',ISNULL(@TotalCashAmount,0),
		ISNULL(@TotalUnitAmount,0),ISNULL(@TotalCashAmount,0) - ISNULL(@FeeAmount,0),
		ISNULL(@TotalUnitAmount,0),case when ISNULL(@FeePercent,0) = 0 then (100 * @FeeAmount / (@TotalCashAmount - @FeeAmount)) else ISNULL(@FeePercent,0) end,ISNULL(@FeeAmount,0),@AgentPK,@AgentFeePercent,@TotalCashAmount * @AgentFeePercent,
		1,ISNULL(@CurrencyPK,0),null,1,1,0,0,0,'',@UsersID,@Time,@Time from ClientSubscription
		group by ClientSubscriptionPK
		ORDER BY ClientSubscriptionPK desc

	

	END


Fetch next From A Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo
END
Close A
Deallocate A     
            
                         ";
                        }
                        else
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText =
                               @"

DECLARE @TransactionDate datetime
declare @PaymentDate datetime
declare @TransactionType nvarchar(500)
declare @FundCode nvarchar(500)
declare @FundPK int
declare @FundPKTo int
declare @SellingAgentCode nvarchar(500)
declare @FundClientPK int
declare @TotalCashAmount numeric(22,2)
declare @TotalCashAmountTo numeric(22,2)
declare @TotalUnitAmount numeric(22,4)
declare @FeePercent numeric(18,2)
declare @ClientRedemptionPK int
declare @AgentPK int
declare @AgentFeePercent numeric(18,2)
DECLARE @FeeAmount NUMERIC(18,2)
DECLARE @CurrencyPK INT
DECLARE @RefNumber NVARCHAR(200)
DECLARE @SwitchFeeCharge NVARCHAR(50)
Declare @CurrencyPKTo int


DECLARE A CURSOR FOR 

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountCash,0) > 0
and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0)>= 0)
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK

UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,0 TotalCashAmount,0 TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountUnit,0) > 0
and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0) >= 0)
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK



UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) > 0 or ISNULL(FeeNominal,0) > 0) and TransactionType in ('redemption')
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK

UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0) >= 0) and A.TransactionType = 'subscription'
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0)


UNION ALL

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,sum(A.TotalCashAmount) TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
0 TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) = 0 and ISNULL(FeeNominal,0) = 0) and A.TransactionType = 'redemption'
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit
) A where A.TotalCashAmount > 0
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo


UNION ALL

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,0 TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
sum(A.TotalUnitAmount) TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) = 0 and ISNULL(FeeNominal,0) = 0) and A.TransactionType = 'redemption'
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit
) A where A.TotalUnitAmount > 0
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo








Open A
Fetch Next From A
Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo



While @@FETCH_STATUS = 0
BEGIN

declare @FundCashRefPK int
declare @FundCashRefPKTo INT


SET @FundCashRefPKTo = NULL
SET @FundCashRefPK = null

select @FundCashRefPK = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPK 


select @FundCashRefPKTo = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPKTo 


if isnull(@TransactionType,0) = 'Redemption' 
	BEGIN

	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 2 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END
		Update ClientRedemption set status  =  3 where ValueDate = @TransactionDate and FundPK  = @FundPK and FundClientPK = @FundClientPK and status in (1,2) and posted = 0
	END
else if isnull(@TransactionType,0) = 'Switching' 
	BEGIN 


	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		Update ClientSwitching set status  =  3 where ValueDate = @TransactionDate and FundPKFrom  = @FundPK and FundPKTo  = @FundPKTo and FundClientPK = @FundClientPK and status in (1,2) and posted = 0
	END
ELSE
if isnull(@TransactionType,0) = 'Subscription' 
	BEGIN
	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 1 AND status IN (1,2)
END
		Update ClientSubscription set status  =  3 where ValueDate = @TransactionDate and FundPK  = @FundPK and FundClientPK = @FundClientPK and status in (1,2) and posted = 0  and Type not in (6) 
	END


Fetch next From A Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo
END
Close A
Deallocate A    



DECLARE A CURSOR FOR 


select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,0 TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountCash,0) > 0
and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0)>= 0)
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK

UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,0 TotalCashAmount,0 TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate  and TransactionType = 'switching' and isnull(AmountUnit,0) > 0
and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0) >= 0)
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK



UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,TransactionType,
FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,
SwitchFeeCharge,E.CurrencyPK CurrencyPKTo FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) > 0 or ISNULL(FeeNominal,0) > 0) and TransactionType in ('redemption')
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
BICCode,PaymentDate,TransferType,InFundCode,InAmountCash,BankNo,SwitchFeeCharge,E.CurrencyPK

UNION ALL

select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) >= 0 or ISNULL(FeeNominal,0) >= 0) and A.TransactionType = 'subscription'
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0)


UNION ALL

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,sum(A.TotalCashAmount) TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
0 TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) = 0 and ISNULL(FeeNominal,0) = 0) and A.TransactionType = 'redemption'
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit
) A where A.TotalCashAmount > 0
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo


UNION ALL

Select A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,0 TotalCashAmount,
sum(A.TotalCashAmountTo)  TotalCashAmountTo, 
sum(A.TotalUnitAmount) TotalUnitAmount,A.FeePercent,sum(A.FeeNominal) FeeNominal,A.CurrencyPK,A.SwitchFeeCharge,A.CurrencyPKTo from 
(
select C.FundPK,E.FundPK FundPKTo,FundClientPK,AgentPK,AgentFee,TransactionDate,Case when TransactionType = 'Subscription' then TransactionDate  else  PaymentDate End  PaymentDate,
TransactionType,FundCode,SellingAgentCode,sum(AmountCash) TotalCashAmount,sum(ISNULL(InAmountCash,0)) TotalCashAmountTo,sum(ISNULL(AmountUnit,0)) TotalUnitAmount,
isnull(FeePercent,0) FeePercent,sum(ISNULL(FeeNominal,0)) FeeNominal,C.CurrencyPK,isnull(A.SwitchFeeCharge,0) SwitchFeeCharge,isnull(E.CurrencyPK,0) CurrencyPKTo
FROM UpdatePaymentSInvestTemp A
left join FundClient B on A.SellingAgentCode  = B.SACode and B.status  in (1,2)
left join Fund C on A.FundCode = C.SInvestCode and C.status  in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  in (1,2) 
left join Fund E on A.InFundCode = E.SInvestCode and E.status  in (1,2)
where  A.SellingAgentCode <> @CompanyID and TransactionDate = @TrxDate and (isnull(FeePercent,0) = 0 and ISNULL(FeeNominal,0) = 0) and A.TransactionType = 'redemption'
group by C.FundPK,E.FundPK,FundClientPK,AgentPK,AgentFee,TransactionDate,TransactionType,FundCode,SellingAgentCode,FeePercent,C.CurrencyPK,
PaymentDate,TransferType,ReferenceNumber,isnull(A.SwitchFeeCharge,0),isnull(E.CurrencyPK,0),AmountCash,AmountUnit
) A where A.TotalUnitAmount > 0
group by 
A.fundPK,A.fundPKto,A.FundClientPK,A.AgentPK,A.AgentFee,A.TransactionDate,A.PaymentDate,A.TransactionType,A.FundCode,A.SellingAgentCode,
A.FeePercent,A.CurrencyPK ,A.SwitchFeeCharge,A.CurrencyPKTo



Open A
Fetch Next From A
Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo



While @@FETCH_STATUS = 0
BEGIN


SET @FundCashRefPKTo = NULL
SET @FundCashRefPK = null

select @FundCashRefPK = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPK 


select @FundCashRefPKTo = FundCashRefPK from FundClientCashRef  
where status = 2 and FundClientPK = @FundClientPK and FundPK = @FundPKTo 


if isnull(@TransactionType,0) = 'Redemption' 
	BEGIN

	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 2 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		
		
		INSERT INTO ClientRedemption(ClientRedemptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,PaymentDate,NAV,
		FundPK,FundClientPK,CashRefPK,BitRedemptionAll, Description,CashAmount,UnitAmount,TotalCashAmount,
		TotalUnitAmount,RedemptionFeePercent,RedemptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,
		CurrencyPK,UnitPosition,BankRecipientPK,TransferType,IsBOTransaction,BitSInvest,TransactionPK,IsFrontSync,
		ReferenceSinvest,EntryUsersID,EntryTime,LastUpdate)
		
		SELECT TOP 1 isnull(ClientRedemptionPk,0) + 1,1,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,@TransactionDate,@TransactionDate,@PaymentDate,0,
		@FundPK,@FundClientPK,case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end ,0,'Update From SInvest',
		ISNULL(@TotalCashAmount,0),ISNULL(@TotalUnitAmount,0),ISNULL(@TotalCashAmount,0) - ISNULL(@FeeAmount,0),
		ISNULL(@TotalUnitAmount,0),ISNULL(@FeePercent,0),ISNULL(@FeeAmount,0),ISNULL(@AgentPK,0),
		ISNULL(@AgentFeePercent,0),ISNULL(@TotalCashAmount * @AgentFeePercent/100,0),1,ISNULL(@CurrencyPk,0),0,0,case when @TotalCashAmount > 100000000 then 2 else 1 end,1,1,0,0,'',
		@UsersID,@Time,@Time from ClientRedemption
		group by ClientRedemptionPK
		ORDER BY ClientRedemptionPK desc


	

                            

	END
else if isnull(@TransactionType,0) = 'Switching' 
	BEGIN 


	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 2 AND status IN (1,2)
END

		
	    
		INSERT INTO ClientSwitching(ClientSwitchingPK,HistoryPK,Status,FeeTypeMethod,NAVDate,ValueDate,PaymentDate,
		NAVFundFrom,NAVFundTo,FundPKFrom,FundPKTo,FundClientPK,FeeType,CashRefPKFrom,CashRefPKTo, Description,
		CashAmount,UnitAmount,TotalCashAmountFundFrom,TotalCashAmountFundTo,TotalUnitAmountFundFrom,TotalUnitAmountFundTo,
		SwitchingFeePercent,SwitchingFeeAmount,CurrencyPK,TransferType,
        BitSwitchingAll,UserSwitchingPK,TransactionPK,IsBoTransaction,BitSInvest,FeeTypeMode,IsProcessed,IsFrontSync,
		ReferenceSinvest,AgentPK,EntryUsersID,EntryTime,LastUpdate)	
	    
		SELECT TOP 1 isnull(ClientSwitchingPk,0) + 1,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,@TransactionDate,@TransactionDate,@PaymentDate,0,0,
		@FundPK,@FundPKTo,@FundClientPK,ISNULL(@SwitchFeeCharge,'OUT'),case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end ,
		CASE when isnull(@FundCashRefPKTo,0) <> 0 then @FundCashRefPKTo else 1 end ,'Update From SInvest',@TotalCashAmount,
		@TotalUnitAmount,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,0,
		ISNULL(@FeePercent,0) ,ISNULL(@FeeAmount,0),@CurrencyPKTo,
        case when @TotalCashAmount > 100000000 then 2 else 1 end,0,0,0,1,1,1,0,0,'',@AgentPK,@UsersID,@Time,@Time from ClientSwitching
		group by ClientSwitchingPK
		ORDER BY ClientSwitchingPK desc

	

	END
ELSE
if isnull(@TransactionType,0) = 'Subscription' 
	BEGIN
	
IF(ISNULL(@FundCashRefPK,0) = 0)
BEGIN
	SELECT @FundCashRefPK = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPK AND type = 1 AND status IN (1,2)
END

IF(ISNULL(@FundCashRefPKTo,0) = 0)
BEGIN
	SELECT @FundCashRefPKTo = FundCashRefPK FROM FundCashRef WHERE fundPK = @FundPKTo AND type = 1 AND status IN (1,2)
END
		
	
		INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,FeeType,NAVDate,ValueDate,
		NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,
		SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,AutoDebitDate,IsBoTransaction,
		BitSinvest,BitImmediateTransaction,TransactionPK,IsFrontSync,ReferenceSinvest,EntryUsersID,EntryTime,LastUpdate)
	
		Select TOP 1 ISNULL(ClientSubscriptionPK,0) + 1,1,1,1,case when ISNULL(@FeeAmount,0) > 0 then 2 else 1 end,@TransactionDate,@TransactionDate,
		0,@FundPK,@FundClientPK,case when isnull(@FundCashRefPK,0) <> 0 then @FundCashRefPK else 1 end,'Update From SInvest',ISNULL(@TotalCashAmount,0),
		ISNULL(@TotalUnitAmount,0),ISNULL(@TotalCashAmount,0) - ISNULL(@FeeAmount,0),
		ISNULL(@TotalUnitAmount,0),case when ISNULL(@FeePercent,0) = 0 then (100 * @FeeAmount / (@TotalCashAmount - @FeeAmount)) else ISNULL(@FeePercent,0) end,ISNULL(@FeeAmount,0),@AgentPK,@AgentFeePercent,@TotalCashAmount * @AgentFeePercent,
		1,ISNULL(@CurrencyPK,0),null,1,1,0,0,0,'',@UsersID,@Time,@Time from ClientSubscription
		group by ClientSubscriptionPK
		ORDER BY ClientSubscriptionPK desc

	

	END


Fetch next From A Into @FundPK,@FundPKTo,@FundClientPK,@AgentPK,@AgentFeePercent,@TransactionDate,@PaymentDate,@TransactionType,
@FundCode,@SellingAgentCode,@TotalCashAmount,@TotalCashAmountTo,@TotalUnitAmount,@FeePercent,@FeeAmount,@CurrencyPK
,@SwitchFeeCharge,@CurrencyPKTo
END
Close A
Deallocate A       
            
                         ";
                        }


                        cmd.Parameters.AddWithValue("@UsersID", _updatePaymentSInvestTemp.EntryUsersID);
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.Parameters.AddWithValue("@TrxDate", _trxDate);
                        cmd.ExecuteNonQuery();


                    }
                }
                return "Move To Subs Redemp Success";

            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public UpdatePaymentSInvestTempCombo ValidationMoveToSubsRedemp(string _usersID, string _date)
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
                        @"
                        declare @Transaction table
                        (
	                        TransactionDate datetime,
	                        TransactionType nvarchar(50),
	                        FundCode nvarchar(50),
	                        FundClientCode nvarchar(50)
                        )

                        declare @validate table
                        (
                        TrxType nvarchar(100),
                        Fund nvarchar(50),
                        FundClient nvarchar(50),
                        Result nvarchar(50)
                        )


                        insert into @Transaction
                        select distinct TransactionDate,TransactionType,FundCode,SellingAgentCode from UpdatePaymentSInvestTemp
                        where TransactionDate = @ParamTransactionDate and Selected = 1
                        group by TransactionDate,PaymentDate,TransactionType,FundCode,SellingAgentCode

                        insert into @validate
                        select top 1 Description TrxType,FundCode Fund,FundClientCode FundClient, 1 Result from ( 
                        select 'Subscription' Description,FundCode,FundClientCode from ClientSubscription A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
                        left join @Transaction D on C.SACode = D.FundClientCode and B.SInvestCode = D.FundCode
                        where D.TransactionType = 'Subscription' and A.ValueDate = @ParamTransactionDate and posted = 1 and A.status = 2 and A.Revised = 0
                        union all
                        select 'Redemption' Description,FundCode,FundClientCode from ClientRedemption A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
                        left join @Transaction D on C.SACode = D.FundClientCode and B.SInvestCode = D.FundCode
                        where D.TransactionType = 'Redemption' and A.ValueDate = @ParamTransactionDate and posted = 1 and A.status = 2 and A.Revised = 0
                        union all
                        select 'Switching' Description,FundCode,FundClientCode from ClientRedemption A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
                        left join @Transaction D on C.SACode = D.FundClientCode and B.SInvestCode = D.FundCode
                        where D.TransactionType = 'Switching' and A.ValueDate = @ParamTransactionDate and posted = 1 and A.status = 2 and A.Revised = 0
                        )
                        A


                        if not exists(select * from @validate)
                        BEGIN
	                        select 0 TrxType,0 Fund,0 FundClient,0 Result
                        END
                        ELSE
                        BEGIN
	                        select * from @validate
                        END

                         ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ParamTransactionDate", _date);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new UpdatePaymentSInvestTempCombo()
                                {

                                    Result = Convert.ToString(dr["Result"]),
                                    Fund = Convert.ToString(dr["Fund"]),
                                    FundClient = Convert.ToString(dr["FundClient"]),
                                    TrxType = Convert.ToString(dr["TrxType"]),

                                };
                            }
                            else
                            {
                                return null;
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
        public Boolean UpdatePaymentSInvest_ValidateDataSACode()
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
                            select SellingAgentCode from UpdatePaymentSInvestTemp where SellingAgentCode not in (Select distinct isnull(SACode,'') from FundClient B where status in (1,2)) ";
                        //cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        return true;

                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean UpdatePaymentSInvest_TruncateTable()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Truncate Table UpdatePaymentSInvestTemp";
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string ImportAPERDSummary(string _fileSource, string _userID)
        {
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table APERDSummaryTemp";
                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.APERDSummaryTemp";
                bulkCopy.WriteToServer(CreateDataTableFromAPERDSummaryExcelFile(_fileSource));
            }

            try
            {
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
Declare @date datetime 
select top 1 @Date = convert(varchar(10), cast(TransactionDate AS date),101) From APERDSummaryTemp

delete updatePaymentSinvestTemp where TransactionDate = @Date and TransactionType in ('Subscription','Redemption')

Declare @MaxPK int
select @MaxPK = Max(UpdatePaymentSInvestTempPK) from UpdatePaymentSInvestTemp
set @maxPK = isnull(@maxPK,0)

insert into UpdatePaymentSInvestTemp(UpdatePaymentSInvestTempPK,Selected,TransactionDate,TransactionType,RefNumber,SellingAgentCode,IFUA,FundCode,AmountCash,AmountUnit,FeePercent,BICCode,BankAcc,BankNo,PaymentDate,
TransferType,ReferenceNumber,FeeNominal)
select @MaxPK + ROW_NUMBER() OVER(ORDER BY transactiontype ASC) UpdatePaymentSInvestTempPK,0, convert(varchar(10), cast(TransactionDate AS date),101), case when TransactionType = 1 then 'Subscription' when TransactionType = 2 then 'Redemption' else 'Switching' end,
 'Transaction APERD Summary', SACode, '', FundCode, GrossTransactionAmount, NumberOfUnits, 0, '', '','',  convert(varchar(10), cast(TransactionDate AS date),101), '', '',TransactionFee
from APERDSummaryTemp A
left join Fund B on A.FundCode = B.SinvestCode and B.status In (1,2)
where A.TransactionType not in (3,4,5) and A.SACode <> @CompanyID
                                                ";
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@Lastupdate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.ExecuteNonQuery();
                        return "Import APERDSummary Success";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromAPERDSummaryExcelFile(string _path)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TransactionDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TransactionType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SACode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SAName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NumberOfUnits";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NAVPerUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "GrossTransactionAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "TransactionFee";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NetTransactionAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(Tools.TxtFilePath + _path);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                dr = dt.NewRow();
                dr["TransactionDate"] = s[0];
                dr["TransactionType"] = s[1];
                dr["FundCode"] = s[2];
                dr["FundName"] = s[3];
                dr["IMCode"] = s[4];
                dr["IMName"] = s[5];
                dr["CBCode"] = s[6];
                dr["CBName"] = s[7];
                dr["SACode"] = s[8];
                dr["SAName"] = s[9];
                dr["NumberOfUnits"] = s[10];
                dr["NAVPerUnit"] = s[11];
                dr["GrossTransactionAmount"] = s[12];
                dr["TransactionFee"] = s[13];
                dr["NetTransactionAmount"] = s[14];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }




    }
}