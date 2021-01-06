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
    public class ImportFxdReps
    {

        private DataTable CreateDataTableFromFxdTempExcelFile(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Date";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Code";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentMoneyMarket";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentOtherDebt";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentEquity";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentWarranRight";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Cash";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DividendReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ReceivableonSecuritySold";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PrepaidTax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalAssets";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PayableonSecurityPurchase";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherPayable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalLiabilities";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalNetAssets";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Subscription";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Redemption";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RetainedEarning";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DistributedIncome";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "UnrealizedGainLoss";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RealizedGainLoss";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NetIncome";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalNAV";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OutstandingUnitIssued";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NAVUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(Tools.TxtFilePath + _fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {


                string[] s = input.Split(new char[] { '|' });
                string _strDate = Convert.ToString(s[0]);
                if (!string.IsNullOrEmpty(_strDate))
                {
                    string _tgl = _strDate.Substring(6, 2);
                    string _bln = _strDate.Substring(4, 2);
                    string _thn = _strDate.Substring(0, 4);

                    _strDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                }

                dr = dt.NewRow();
                dr["Date"] = _strDate;
                dr["Code"] = s[1];
                dr["InvestmentMoneyMarket"] = s[2];
                dr["InvestmentOtherDebt"] = s[3];
                dr["InvestmentEquity"] = s[4];
                dr["InvestmentWarranRight"] = s[5];
                dr["Cash"] = s[6];
                dr["DividendReceivable"] = s[7];
                dr["InterestReceivable"] = s[8];
                dr["ReceivableonSecuritySold"] = s[9];
                dr["OtherReceivable"] = s[10];
                dr["PrepaidTax"] = s[11];
                dr["TotalAssets"] = s[12];
                dr["PayableonSecurityPurchase"] = s[13];
                dr["OtherPayable"] = s[14];
                dr["TotalLiabilities"] = s[15];
                dr["TotalNetAssets"] = s[16];
                dr["Subscription"] = s[17];
                dr["Redemption"] = s[18];
                dr["RetainedEarning"] = s[19];
                dr["DistributedIncome"] = s[20];
                dr["UnrealizedGainLoss"] = s[21];
                dr["RealizedGainLoss"] = s[22];
                dr["NetIncome"] = s[23];
                dr["TotalNAV"] = s[24];
                dr["OutstandingUnitIssued"] = s[25];
                dr["NAVUnit"] = s[26];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }
        public string FxdImport(string _fileSource, string _userID)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table FxdTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.FxdTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromFxdTempExcelFile(_fileSource));
                            _msg = "Update Fxd Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"  
                                DECLARE @Fxd11BalancePK INT

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                SET @Fxd11BalancePK = ISNULL(@Fxd11BalancePK,0)

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,1,B.FundPK,A.InvestmentMoneyMarket 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.InvestmentMoneyMarket <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,2,B.FundPK,A.InvestmentOtherDebt 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.InvestmentOtherDebt <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,3,B.FundPK,A.InvestmentEquity 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.InvestmentEquity <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,4,B.FundPK,A.InvestmentWarranRight 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.InvestmentWarranRight <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,5,B.FundPK,A.Cash 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.Cash <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,6,B.FundPK,A.DividendReceivable 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.DividendReceivable <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,7,B.FundPK,A.InterestReceivable 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.InterestReceivable <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,8,B.FundPK,A.ReceivableonSecuritySold 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.ReceivableonSecuritySold <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,9,B.FundPK,A.OtherReceivable 
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.OtherReceivable <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,10,B.FundPK,A.PrepaidTax
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.PrepaidTax <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,11,B.FundPK,A.TotalAssets
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.TotalAssets <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,12,B.FundPK,A.PayableonSecurityPurchase
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.PayableonSecurityPurchase <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,13,B.FundPK,A.OtherPayable
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.OtherPayable <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,14,B.FundPK,A.TotalLiabilities	
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.TotalLiabilities <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,15,B.FundPK,A.TotalNetAssets	
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.TotalNetAssets <> 0 and B.FundPK is not null


                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,16,B.FundPK,A.Subscription	
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.Subscription <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,17,B.FundPK,A.Redemption	
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.Redemption <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,18,B.FundPK,A.RetainedEarning	
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.RetainedEarning <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,19,B.FundPK,A.DistributedIncome	
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.DistributedIncome <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,20,B.FundPK,A.UnrealizedGainLoss	
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.UnrealizedGainLoss <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,21,B.FundPK,A.RealizedGainLoss	
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.RealizedGainLoss <> 0 and B.FundPK is not null

                                SELECT @Fxd11BalancePK = MAX(Fxd11BalancePK) FROM dbo.Fxd11Balance 

                                INSERT INTO dbo.Fxd11Balance
                                        ( Fxd11BalancePK ,
                                          Date ,
                                          Fxd11AccountPK ,
                                          FundPK ,
                                          Balance
                                        )
                                SELECT @Fxd11BalancePK + 1,
                                A.Date,22,B.FundPK,A.NetIncome
                                FROM dbo.FxdTemp A
                                LEFT JOIN Fund B ON A.Code = B.NKPDName AND B.status IN (1,2)
                                WHERE A.NetIncome <> 0 and B.FundPK is not null
                                ";
                                cmd1.Parameters.AddWithValue("@EntryUsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _dateTime);
                                cmd1.ExecuteNonQuery();
                            }
                            _msg = "Import FXD Done";

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


        private DataTable CreateDataTableFromFxdTempExcelFile14(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Date";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Code";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BookCost";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Nominal";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MaturityDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FairMarketPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalValue";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PercentValueAsset";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AccruedInterest";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(Tools.TxtFilePath + _fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {


                string[] s = input.Split(new char[] { '|' });
                string _strDate = Convert.ToString(s[0]);
                if (!string.IsNullOrEmpty(_strDate))
                {
                    string _tgl = _strDate.Substring(6, 2);
                    string _bln = _strDate.Substring(4, 2);
                    string _thn = _strDate.Substring(0, 4);

                    _strDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                }

                string _maturityDate = Convert.ToString(s[6]);
                if (!string.IsNullOrEmpty(_maturityDate))
                {
                    string _tgl = _maturityDate.Substring(6, 2);
                    string _bln = _maturityDate.Substring(4, 2);
                    string _thn = _maturityDate.Substring(0, 4);

                    _maturityDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                }

                dr = dt.NewRow();
                dr["Date"] = _strDate;
                dr["Code"] = s[1];
                dr["SecurityCode"] = s[2];
                dr["SecurityType"] = s[3];
                dr["BookCost"] = s[4];
                dr["Nominal"] = s[5];
                dr["MaturityDate"] = _maturityDate;
                dr["FairMarketPrice"] = s[7];
                dr["TotalValue"] = s[8];
                dr["PercentValueAsset"] = s[9];
                dr["AccruedInterest"] = s[10];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }
        public string FxdImport14(string _fileSource, string _userID)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table Fxd14Temp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.Fxd14Temp";
                            bulkCopy.WriteToServer(CreateDataTableFromFxdTempExcelFile14(_fileSource));
                            _msg = "Update Fxd Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"  
                                   
                               update C set C.NAV = A.NavUnit, C.UpdateTime = @LastUpdate , UpdateUsersID = @EntryUsersID, LastUpdate = @LastUpdate, LastUpdateDB = @LastUpdate
                                from FxdTemp A 
                                left join Fund B on A.Code = B.NKPDName
                                left join CloseNAV C on B.FundPK = C.FundPK
                                where isnull(C.Date,'') = A.Date and C.FundPK = B.FundPK
                               
                                and B.status in (1,2)

                                ";
                                cmd1.Parameters.AddWithValue("@EntryUsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _dateTime);
                                cmd1.ExecuteNonQuery();
                            }
                            _msg = "Import FXD Done";

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

        public bool CheckData()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                        DbCon.Open();
                        string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"IF EXISTS(SELECT * FROM FundJournalAccount WHERE status IN (1,2) AND ISNULL(Fxd11AccountPK,0) = 0 AND Type IN (1,2))
                            BEGIN
	                            SELECT 1 Result
                            END
                            ELSE
                            BEGIN
	                            SELECT 0 Result
                            END";

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


        public List<ImportFxd> Get_TrialBalance(string _date, int _fundpk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ImportFxd> L_ImportFxd = new List<ImportFxd>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        DECLARE @FirstDateOfYear datetime
                        Declare @ValueDateFrom datetime


                        select @FirstDateOfYear = DATEADD(yy, DATEDIFF(yy, 0, @ValueDateTo), 0)

                        set @ValueDateFrom =  @ValueDateTo

                        if @ValueDateFrom <= @FirstDateOfYear
                        begin
	                        set @ValueDateFrom = @FirstDateOfYear
                        end

                        Declare @PeriodPK int
                        Select @PeriodPK = PeriodPK from Period where @ValueDateTo between DateFrom and DateTo and status = 2


                        --Select  case when A.Groups = 1 then isnull(A.Name,'') else '' end Header,A.ID ID
                        --,case when A.Groups = 1 then '' else A.Name end Name
                        --,isnull(cast(E.HeaderOrder as Nvarchar(50)) + '.' + E.Name,'') ParentName 
                        --,D.ID CurrencyID,A.BitIsChange,A.Groups
                        --,isnull(B.PreviousBaseBalance,0) PreviousBaseBalance
                        --,isnull(B.Movement,0) Movement
                        --,isnull(B.CurrentBaseBalance,0) CurrentBaseBalance
                        --,isnull(B.BKBalance,0) BKBalance
                        --from FundJournalAccount A

                       
                        CREATE TABLE #Result
                        (
	                        Fxd11AccountPK INT,
	                        ID NVARCHAR(500),
	                        SystemBalance NUMERIC(22,4)
                        )

                        INSERT INTO #Result
                                ( Fxd11AccountPK,ID, SystemBalance )
                        Select  A.Fxd11AccountPK,F.Name
                        ,SUM(ISNULL(B.CurrentBaseBalance,0)) CurrentBaseBalance
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
			                        WHERE  B.ValueDate <= @ValueDateTo and B.PeriodPK = @PeriodPK and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK
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
			                        WHERE  B.ValueDate < @ValueDateFrom  and B.PeriodPK = @PeriodPK   and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK
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
		                        LEFT JOIN dbo.Fxd11Account F ON A.Fxd11AccountPK = F.Fxd11AccountPK AND F.status IN (1,2)
	
		                        where A.status = 2 and	A.show = 1 or (
		                         ISNULL(B.PreviousBaseBalance, 0) <> 0
		                        or ISNULL(B.Movement, 0) <> 0
		                        or ISNULL(B.CurrentBaseBalance, 0) <> 0 ) 
		                        GROUP BY F.Name,A.Fxd11AccountPK
		
                        CREATE TABLE #ResultFinal
                        (
	                        Fxd11AccountPK INT,
	                        ID NVARCHAR(500),
	                        SystemBalance NUMERIC(22,4),
	                        FxdBalance NUMERIC(22,4)
                        )

                        INSERT INTO #ResultFinal
                                ( Fxd11AccountPK ,
                                  ID ,
                                  SystemBalance ,
                                  FxdBalance
                                )


                        SELECT A.Fxd11AccountPK,A.ID,A.SystemBalance,ISNULL(B.Balance,0) FROM #Result A
                        LEFT JOIN Fxd11Balance B ON A.Fxd11AccountPK = B.Fxd11AccountPK AND B.Date = @ValueDateTo AND B.FundPK = @FundPK
                        WHERE A.Fxd11AccountPK IS NOT NULL

                        INSERT INTO #ResultFinal
                                ( Fxd11AccountPK ,
                                  ID ,
                                  SystemBalance ,
                                  FxdBalance
                                )
                        SELECT A.Fxd11AccountPK,B.Name,0,A.Balance FROM Fxd11Balance A
                        LEFT JOIN Fxd11Account B ON A.Fxd11AccountPK = B.Fxd11AccountPK AND B.status IN (1,2)
                        WHERE A.Fxd11AccountPK NOT IN
                        (
	                        SELECT Fxd11AccountPK FROM #ResultFinal
                        ) AND Date = @ValueDateTo AND FundPK = @FundPK

                        SELECT Fxd11AccountPK,ID,SystemBalance,FxdBalance, SystemBalance -FxdBalance Diff FROM #ResultFinal ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDateTo", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundpk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ImportFxd M_ImportFxd = new ImportFxd();
                                    M_ImportFxd.Fxd11AccountPK = dr["Fxd11AccountPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Fxd11AccountPK"]);
                                    M_ImportFxd.ID = dr["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ID"]);
                                    M_ImportFxd.SystemBalance = dr["SystemBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SystemBalance"]);
                                    M_ImportFxd.FxdBalance = dr["FxdBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["FxdBalance"]);
                                    M_ImportFxd.Diff = dr["Diff"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Diff"]);
                                    L_ImportFxd.Add(M_ImportFxd);
                                }

                            }
                            return L_ImportFxd;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<ImportFxd> Get_TrialBalanceFxdReconDetail(int _fxd11AccountPK, int _fundpk, string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ImportFxd> L_ImportFxd = new List<ImportFxd>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        DECLARE @FirstDateOfYear datetime
Declare @ValueDateFrom datetime


select @FirstDateOfYear = DATEADD(yy, DATEDIFF(yy, 0, @ValueDateTo), 0)

set @ValueDateFrom =  @ValueDateTo

if @ValueDateFrom <= @FirstDateOfYear
begin
	set @ValueDateFrom = @FirstDateOfYear
end

Declare @PeriodPK int
Select @PeriodPK = PeriodPK from Period where @ValueDateTo between DateFrom and DateTo and status = 2

Select  case when A.Groups = 1 then isnull(A.Name,'') else '' end Header,A.ID ID
,case when A.Groups = 1 then '' else A.Name end Name
,isnull(cast(E.HeaderOrder as Nvarchar(50)) + '.' + E.Name,'') ParentName 
,D.ID CurrencyID,A.BitIsChange,A.Groups
,isnull(B.CurrentBaseBalance,0) CurrentBaseBalance
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
			WHERE  B.ValueDate <= @ValueDateTo and B.PeriodPK = @PeriodPK and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK
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
			WHERE  B.ValueDate < @ValueDateFrom  and B.PeriodPK = @PeriodPK   and B.posted=1 and B.Status = 2 and A.FundPK = @FundPK
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
		LEFT JOIN dbo.Fxd11Account F ON A.Fxd11AccountPK = F.Fxd11AccountPK AND F.status IN (1,2)
	
		where A.status = 2 and	A.show = 1 	AND (A.Fxd11AccountPK = @Fxd11AccountPK )  ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@FundPK", _fundpk);
                        cmd.Parameters.AddWithValue("@Fxd11AccountPK", _fxd11AccountPK);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ImportFxd M_ImportFxd = new ImportFxd();
                                    M_ImportFxd.ID = dr["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ID"]);
                                    M_ImportFxd.Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]);
                                    M_ImportFxd.CurrentBaseBalance = dr["CurrentBaseBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CurrentBaseBalance"]);
                                    L_ImportFxd.Add(M_ImportFxd);
                                }

                            }
                            return L_ImportFxd;
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