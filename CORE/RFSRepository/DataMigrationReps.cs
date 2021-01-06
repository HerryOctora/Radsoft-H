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
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using SucorInvest.Connect;

namespace RFSRepository
{

    public class KYCAperdInd
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public string Col6 { get; set; }
        public string Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string Col10 { get; set; }

        public string Col11 { get; set; }
        public string Col12 { get; set; }
        public string Col13 { get; set; }
        public string Col14 { get; set; }
        public string Col15 { get; set; }
        public string Col16 { get; set; }
        public string Col17 { get; set; }
        public string Col18 { get; set; }
        public string Col19 { get; set; }
        public string Col20 { get; set; }

        public string Col21 { get; set; }
        public string Col22 { get; set; }
        public string Col23 { get; set; }
        public string Col24 { get; set; }
        public string Col25 { get; set; }
        public string Col26 { get; set; }
        public string Col27 { get; set; }
        public string Col28 { get; set; }
        public string Col29 { get; set; }
        public string Col30 { get; set; }

        public string Col31 { get; set; }
        public string Col32 { get; set; }
        public string Col33 { get; set; }
        public string Col34 { get; set; }
        public string Col35 { get; set; }
        public string Col36 { get; set; }
        public string Col37 { get; set; }
        public string Col38 { get; set; }
        public string Col39 { get; set; }
        public string Col40 { get; set; }

        public string Col41 { get; set; }
        public string Col42 { get; set; }
        public string Col43 { get; set; }
        public string Col44 { get; set; }
        public string Col45 { get; set; }
        public string Col46 { get; set; }
        public string Col47 { get; set; }
        public string Col48 { get; set; }
        public string Col49 { get; set; }
        public string Col50 { get; set; }
    }


    public class KYCAperdIns
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public string Col6 { get; set; }
        public string Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string Col10 { get; set; }

        public string Col11 { get; set; }
        public string Col12 { get; set; }
        public string Col13 { get; set; }
        public string Col14 { get; set; }
        public string Col15 { get; set; }
        public string Col16 { get; set; }
        public string Col17 { get; set; }
        public string Col18 { get; set; }
        public string Col19 { get; set; }
        public string Col20 { get; set; }

        public string Col21 { get; set; }
        public string Col22 { get; set; }
        public string Col23 { get; set; }
        public string Col24 { get; set; }
        public string Col25 { get; set; }
        public string Col26 { get; set; }
        public string Col27 { get; set; }
        public string Col28 { get; set; }
        public string Col29 { get; set; }
        public string Col30 { get; set; }

        public string Col31 { get; set; }
        public string Col32 { get; set; }
        public string Col33 { get; set; }
        public string Col34 { get; set; }
        public string Col35 { get; set; }
        public string Col36 { get; set; }
        public string Col37 { get; set; }
        public string Col38 { get; set; }
        public string Col39 { get; set; }
        public string Col40 { get; set; }

        public string Col41 { get; set; }
        public string Col42 { get; set; }
        public string Col43 { get; set; }
        public string Col44 { get; set; }
        public string Col45 { get; set; }
        public string Col46 { get; set; }
        public string Col47 { get; set; }
        public string Col48 { get; set; }
        public string Col49 { get; set; }
        public string Col50 { get; set; }

        public string Col51 { get; set; }
        public string Col52 { get; set; }
        public string Col53 { get; set; }
        public string Col54 { get; set; }
        public string Col55 { get; set; }
        public string Col56 { get; set; }
        public string Col57 { get; set; }
        public string Col58 { get; set; }
        public string Col59 { get; set; }
        public string Col60 { get; set; }

        public string Col61 { get; set; }
        public string Col62 { get; set; }
        public string Col63 { get; set; }
        public string Col64 { get; set; }
        public string Col65 { get; set; }
        public string Col66 { get; set; }
        public string Col67 { get; set; }
        public string Col68 { get; set; }
    }

    public class DataMigrationReps
    {


        #region NON APERD Client TXT

        public string ImportNonAPERDFundClientInd(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table Z_CLIENT_IND";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.Z_CLIENT_IND";
                            bulkCopy.WriteToServer(CreateDataTableFromNonAPERDFundClientTempIndFileTxt(_fileSource));
                            _msg = "Import FundClient Individual Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"  
                                  Declare @SACode nvarchar(100)
select @SACode = ID From Company where status in (1,2)

declare @max int
select @max = max(fundClientPK) from fundclient
select @max = isnull(@max,0) 


insert into [dbo].[FundClient](
FundClientPK,HistoryPK,Status,ID,Name
,SACode,SellingAgentPK,SID,InvestorType,IFUACode
,NamaDepanInd,NamaTengahInd,NamaBelakangInd,Nationality,IdentitasInd1
,NoIdentitasInd1,ExpiredDateIdentitasInd1,NPWP,RegistrationNPWP,CountryOfBirth
,TempatLahir,TanggalLahir,JenisKelamin,Pendidikan,MotherMaidenName
,Agama,Pekerjaan,PenghasilanInd,StatusPerkawinan,SpouseName
,InvestorsRiskProfile,MaksudTujuanInd,SumberDanaInd,AssetOwner,OtherAlamatInd1
,OtherKodeKotaInd1,OtherKodePosInd1,AlamatInd1,KodeKotaInd1,KodePosInd1
,CountryofCorrespondence,AlamatInd2,KodeKotaInd2,KodePosInd2,CountryofDomicile
,TeleponRumah,TeleponSelular,fax,Email,StatementType
,FATCA,TIN,TINIssuanceCountry
,EntryTime,EntryUsersID,LastUpdate
)
select  
ROW_NUMBER() OVER(ORDER BY Name ASC) + @max,1,2,ISNULL(ClientCode,''),ISNULL(Name,'')
,Case when SACode = @SACode then '' else SACode end,0,SID,InvestorType,IFUA
,ISNULL(NamaDepanInd,''),ISNULL(NamaTengahInd,''),ISNULL(NamaBelakangInd,''),ISNULL(Nationality,''),IdentitasInd1
,NoIdentitasInd1,convert(datetime,convert(varchar(10),ExpiredDateIdentitasInd1,120)) ,NPWP,RegistrationNPWP,ISNULL(CountryOfBirth,'')
,ISNULL(TempatLahir,''),convert(datetime,convert(varchar(10),TanggalLahir,120)),ISNULL(JenisKelamin,0),ISNULL(Pendidikan,0),ISNULL(MotherMaidenName,'')
,isnull(Agama,0),ISNULL(Occupation,0),ISNULL(IncomeLevel,0),ISNULL(StatusPerkawinan,0),ISNULL(SpouseName,'')
,ISNULL(InvestorsRiskProfile,0),ISNULL(MaksudTujuanInd,0),ISNULL(SumberDanaInd,0),ISNULL(AssetOwner,0),ISNULL(OtherAlamatInd1,'')
,ISNULL(OtherKodeKotaInd1,0),ISNULL(OtherKodePosInd1,''),ISNULL(AlamatInd1,''),ISNULL(KodeKotaInd1,0),ISNULL(KodePosInd1,'')
,ISNULL(CountryofCorrespondence,''),ISNULL(AlamatInd2,''),ISNULL(KodeKotaInd2,0),ISNULL(KodePosInd2,''),ISNULL(CountryofDomicile,'')
,ISNULL(TeleponRumah,''),ISNULL(TeleponSelular,''),ISNULL(fax,''),ISNULL(Email,''),ISNULL(StatementType,0)
,ISNULL(FATCA,0),ISNULL(TIN,''),ISNULL(TINIssuanceCountry,'')
,OpeningDate,@UserID,@TimeNow
from Z_CLIENT_IND
where IFUA not in
(
	Select distinct ifuaCode from fundclient
)
                                ";
                                cmd1.CommandTimeout = 0;
                                cmd1.Parameters.AddWithValue("@UserID", _userID);
                                cmd1.Parameters.AddWithValue("@TimeNow", _dateTime);
                                cmd1.ExecuteNonQuery();
                            }
                            _msg = "Import FundClient Individual Success";

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

        private DataTable CreateDataTableFromNonAPERDFundClientTempIndFileTxt(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SACode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SID";
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
            dc.ColumnName = "ClientCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestorType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaDepanInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaTengahInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaBelakangInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Nationality";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IdentitasInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NPWP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RegistrationNPWP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryOfBirth";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TempatLahir";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TanggalLahir";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "JenisKelamin";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Pendidikan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MotherMaidenName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Agama";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Pekerjaan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PenghasilanInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "StatusPerkawinan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SpouseName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestorsRiskProfile";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MaksudTujuanInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SumberDanaInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetOwner";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherAlamatInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherKodeKotaInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherKodePosInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AlamatInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodeKotaInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodePosInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryofCorrespondence";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AlamatInd2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodeKotaInd2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodePosInd2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryofDomicile";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TeleponRumah";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TeleponSelular";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Fax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Email";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "StatementType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FATCA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TIN";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TINIssuanceCountry";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Status";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OpeningDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DeactivationDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                string _noIden = s[12].ToString();

                dr = dt.NewRow();
                dr["SACode"] = s[0];
                dr["SID"] = s[2];
                dr["IFUA"] = s[3];
                dr["Name"] = s[4];
                dr["ClientCode"] = s[5];
                dr["InvestorType"] = s[6];
                dr["NamaDepanInd"] = s[7];
                dr["NamaTengahInd"] = s[8];
                dr["NamaBelakangInd"] = s[9];
                dr["Nationality"] = s[10];
                dr["IdentitasInd1"] = s[11];
                dr["NoIdentitasInd1"] = _noIden;
                dr["ExpiredDateIdentitasInd1"] = s[13];
                dr["NPWP"] = s[14];
                dr["RegistrationNPWP"] = s[15];
                dr["CountryOfBirth"] = s[16];
                dr["TempatLahir"] = s[17];
                dr["TanggalLahir"] = s[18];
                dr["JenisKelamin"] = s[19];
                dr["Pendidikan"] = s[20];
                dr["MotherMaidenName"] = s[21];
                dr["Agama"] = s[22];
                dr["Pekerjaan"] = s[23];
                dr["PenghasilanInd"] = s[24];
                dr["StatusPerkawinan"] = s[25];
                dr["SpouseName"] = s[26];
                dr["InvestorsRiskProfile"] = s[27];
                dr["MaksudTujuanInd"] = s[28];
                dr["SumberDanaInd"] = s[29];
                dr["AssetOwner"] = s[30];
                dr["OtherAlamatInd1"] = s[31];
                dr["OtherKodeKotaInd1"] = s[32];
                dr["OtherKodePosInd1"] = s[33];
                dr["AlamatInd1"] = s[34];
                dr["KodeKotaInd1"] = s[35];
                dr["KodePosInd1"] = s[37];
                dr["CountryofCorrespondence"] = s[38];
                dr["AlamatInd2"] = s[39];
                dr["KodeKotaInd2"] = s[40];
                dr["KodePosInd2"] = s[42];
                dr["CountryofDomicile"] = s[43];
                dr["TeleponRumah"] = s[44];
                dr["TeleponSelular"] = s[45];
                dr["Fax"] = s[46];
                dr["Email"] = s[47];
                dr["StatementType"] = s[48];
                dr["FATCA"] = s[49];
                dr["TIN"] = s[50];
                dr["TINIssuanceCountry"] = s[51];
                dr["Status"] = s[52];
                dr["OpeningDate"] = s[53];
                dr["DeactivationDate"] = s[54];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }


        public string ImportNonAPERDFundClientIns(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table Z_CLIENT_INS";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.Z_CLIENT_INS";
                            bulkCopy.WriteToServer(CreateDataTableFromNonAPERDFundClientTempInsFileTxt(_fileSource));
                            _msg = "Import FundClient Institution Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"  
Declare @SACode nvarchar(100)
select @SACode = ID From Company where status in (1,2)

declare @max int
select @max = max(fundClientPK) from fundclient

set @max = isnull(@max,0)

insert into [dbo].[FundClient](
FundClientPK,HistoryPK,Status,ID,Name
,SACode,SellingAgentPK,SID,InvestorType
,IFUACode,NamaPerusahaan,CountryOfDomicile,NomorSIUP,SIUPExpirationDate
,NoSKD,ExpiredDateSKD,NPWP,RegistrationNPWP,CountryofEstablishment
,LokasiBerdiri,TanggalBerdiri,NomorAnggaran,Tipe,Karakteristik
,PenghasilanInstitusi,InvestorsRiskProfile,MaksudTujuanInstitusi,SumberDanaInstitusi,AssetOwner
,AlamatPerusahaan,KodeKotaIns,KodePosIns,CountryofCompany,TeleponBisnis
,Companyfax,CompanyMail,StatementType,NamaDepanIns1,NamaTengahIns1
,NamaBelakangIns1,Jabatan1,PhoneIns1,EmailIns1,Identitasins11
,NoIdentitasins11,ExpiredDateIdentitasIns11,IdentitasIns12,NoIdentitasIns12,Expireddateidentitasins12
,NamaDepanIns2,NamaTengahIns2,NamaBelakangIns2,Jabatan2,PhoneIns2
,EmailIns2,IdentitasIns21,NoIDentitasIns21,ExpiredDateIdentitasIns21,IdentitasIns22
,NoIdentitasins22,ExpiredDateIdentitasins22,AssetFor1Year,AssetFor2Year,AssetFor3Year
,OperatingProfitFor1Year,OperatingProfitFor2Year,OperatingProfitFor3Year,FATCA,TIN
,TINIssuanceCountry,GIIN,SubstantialOwnerName,SubstantialOwnerAddress,SubstantialOwnerTIN
,EntryTime,EntryUsersID,LastUpdate)

select ROW_NUMBER() OVER(ORDER BY Name ASC) + @max,1,2,ISNULL(ClientID,''),Name
,Case when SACode = @SACode then '' else SACode end,0,SID,InvestorType
,ISNULL(IFUA,''),ISNULL(NamaPerusahaan,''),ISNULL(Domisili,''),ISNULL(NomorSIUP,''),convert(datetime,convert(varchar(10),SIUPExpirationDate,120))
,ISNULL(NoSKD,''),convert(datetime,convert(varchar(10),ExpiredDateSKD,120)),ISNULL(NPWP,''),ISNULL(RegistrationNPWP,''),ISNULL(CountryofEstablishment,'')
,ISNULL(LokasiBerdiri,''),convert(datetime,convert(varchar(10),TanggalBerdiri,120)),ISNULL(NomorAnggaran,''),ISNULL(Tipe,0),ISNULL(Karakteristik,0)
,ISNULL(PenghasilanInstitusi,0),ISNULL(InvestorsRiskProfile,0),ISNULL(MaksudTujuanInstitusi,0),ISNULL(SumberDanaInstitusi,0),ISNULL(AssetOwner,0)
,ISNULL(AlamatPerusahaan,''),ISNULL(KodeKotaIns,0),ISNULL(KodePosIns,''),isnull(CountryofCompany,''),ISNULL(TeleponBisnis,'')
,ISNULL(Fax,''),ISNULL(Email,''),ISNULL(StatementType,0),ISNULL(NamaDepanIns1,''),ISNULL(NamaTengahIns1,'')
,ISNULL(NamaBelakangIns1,''),ISNULL(Jabatan1,''),ISNULL(PhoneIns1,''),ISNULL(EmailIns1,''),case when left(ExpiredDateIdentitasIns11,4) = '9998' then 7 else 1 end
,ISNULL(NoIdentitasins11,''),ExpiredDateIdentitasIns11,case when left(ExpiredDateIdentitasIns12,4) = '9998' then 7 else 1 end,ISNULL(NoIdentitasIns12,''),Expireddateidentitasins12
,ISNULL(NamaDepanIns2,''),ISNULL(NamaTengahIns2,''),ISNULL(NamaBelakangIns2,''),ISNULL(Jabatan2,''),ISNULL(PhoneIns2,'')
,ISNULL(EmailIns2,''),case when left(ExpiredDateIdentitasIns21,4) = '9998' then 7 else 1 end,ISNULL(NoIDentitasIns21,''),ExpiredDateIdentitasIns21,case when left(ExpiredDateIdentitasIns22,4) = '9998' then 7 else 1 end
,ISNULL(NoIdentitasins22,''),ExpiredDateIdentitasins22,ISNULL(AssetFor1Year,0),ISNULL(AssetFor2Year,0),ISNULL(AssetFor3Year,0)
,ISNULL(OperatingProfitFor1Year,0),ISNULL(OperatingProfitFor2Year,0),ISNULL(OperatingProfitFor3Year,0),ISNULL(FATCA,0),ISNULL(TIN,'')
,ISNULL(TINIssuanceCountry,''),ISNULL(GIIN,''),ISNULL(SubstantialOwnerName,''),ISNULL(SubstantialOwnerAddress,''),ISNULL(SubstantialOwnerTIN,'')
,OpeningDate,@UserID,@TimeNow
from Z_CLIENT_INS
where IFUA not in
(
	Select distinct ifuaCode from fundclient
)";
                                cmd1.CommandTimeout = 0;
                                cmd1.Parameters.AddWithValue("@UserID", _userID);
                                cmd1.Parameters.AddWithValue("@TimeNow", _dateTime);
                                cmd1.ExecuteNonQuery();
                            }
                            _msg = "Import FundClient Institution Success";

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

        private DataTable CreateDataTableFromNonAPERDFundClientTempInsFileTxt(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SACode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ClientCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestorType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaPerusahaan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Domisili";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NomorSIUP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SIUPExpirationDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoSKD";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateSKD";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NPWP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RegistrationNPWP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryofEstablishment";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "LokasiBerdiri";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TanggalBerdiri";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NomorAnggaran";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Tipe";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Karakteristik";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PenghasilanInstitusi";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestorsRiskProfile";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MaksudTujuanInstitusi";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SumberDanaInstitusi";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetOwner";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AlamatPerusahaan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodeKotaIns";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodePosIns";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryofCompany";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TeleponBisnis";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Fax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Email";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "StatementType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaDepanIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaTengahIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaBelakangIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Jabatan1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PhoneIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "EmailIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NPWPPerson1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasIns11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasIns11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IdentitasIns12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasIns12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaDepanIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaTengahIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaBelakangIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Jabatan2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PhoneIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "EmailIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NPWPPerson2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasIns21";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasIns21";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasIns22";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasIns22";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetFor1Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetFor2Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetFor3Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OperatingProfitFor1Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OperatingProfitFor2Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OperatingProfitFor3Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FATCA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TIN";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TINIssuanceCountry";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "GIIN";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SubstantialOwnerName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SubstantialOwnerAddress";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SubstantialOwnerTIN";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Status";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OpeningDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DeactivationDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                string _noIden = s[12].ToString();

                dr = dt.NewRow();
                dr["SACode"] = s[0];
                dr["SID"] = s[2];
                dr["IFUA"] = s[3];
                dr["ClientCode"] = s[4];
                dr["InvestorType"] = s[5];
                dr["NamaPerusahaan"] = s[6];
                dr["Domisili"] = s[7];
                dr["NomorSIUP"] = s[8];
                dr["SIUPExpirationDate"] = s[9];
                dr["NoSKD"] = s[10];
                dr["ExpiredDateSKD"] = s[11];
                dr["NPWP"] = s[12];
                dr["RegistrationNPWP"] = s[13];
                dr["CountryofEstablishment"] = s[14];
                dr["LokasiBerdiri"] = s[15];
                dr["TanggalBerdiri"] = s[16];
                dr["NomorAnggaran"] = s[17];
                dr["Tipe"] = s[18];
                dr["Karakteristik"] = s[19];
                dr["PenghasilanInstitusi"] = s[20];
                dr["InvestorsRiskProfile"] = s[21];
                dr["MaksudTujuanInstitusi"] = s[22];
                dr["SumberDanaInstitusi"] = s[23];
                dr["AssetOwner"] = s[24];
                dr["AlamatPerusahaan"] = s[25];
                dr["KodeKotaIns"] = s[26];
                dr["KodePosIns"] = s[27];
                dr["CountryofCompany"] = s[29];
                dr["TeleponBisnis"] = s[30];
                dr["Fax"] = s[31];
                dr["Email"] = s[32];
                dr["StatementType"] = s[33];
                dr["NamaDepanIns1"] = s[34];
                dr["NamaTengahIns1"] = s[35];
                dr["NamaBelakangIns1"] = s[36];
                dr["Jabatan1"] = s[37];
                dr["PhoneIns1"] = s[38];
                dr["EmailIns1"] = s[39];
                dr["NPWPPerson1"] = s[40];
                dr["NoIdentitasIns11"] = s[41];
                dr["ExpiredDateIdentitasIns11"] = s[42];
                dr["IdentitasIns12"] = s[43];
                dr["ExpiredDateIdentitasIns12"] = s[44];
                dr["NamaDepanIns2"] = s[45];
                dr["NamaTengahIns2"] = s[46];
                dr["NamaBelakangIns2"] = s[47];
                dr["Jabatan2"] = s[48];
                dr["PhoneIns2"] = s[49];
                dr["EmailIns2"] = s[50];
                dr["NPWPPerson2"] = s[51];
                dr["NoIdentitasIns21"] = s[52];
                dr["ExpiredDateIdentitasIns21"] = s[53];
                dr["NoIdentitasIns22"] = s[54];
                dr["ExpiredDateIdentitasIns22"] = s[55];
                dr["AssetFor1Year"] = s[56];
                dr["AssetFor2Year"] = s[57];
                dr["AssetFor3Year"] = s[58];
                dr["OperatingProfitFor1Year"] = s[59];
                dr["OperatingProfitFor2Year"] = s[60];
                dr["OperatingProfitFor3Year"] = s[61];
                dr["FATCA"] = s[62];
                dr["TIN"] = s[63];
                dr["TINIssuanceCountry"] = s[64];
                dr["GIIN"] = s[65];
                dr["SubstantialOwnerName"] = s[66];
                dr["SubstantialOwnerAddress"] = s[67];
                dr["SubstantialOwnerTIN"] = s[68];

                dr["Status"] = s[69];
                dr["OpeningDate"] = s[70];
                dr["DeactivationDate"] = s[71];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        #endregion

        #region NON APERD Client BANK Txt
        public string ImportNonAPERDFundClientBank(string _fileSource, string _userID)
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
                            cmd1.CommandText = "truncate table Z_CLIENT_BANK";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.Z_CLIENT_BANK";
                        bulkCopy.WriteToServer(CreateDataTableFromNonAPERDFundClientBankTempTxtFile(_fileSource));
                        //_msg = "Import Close Nav Success";
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"
                                  
                            UPDATE B SET  B.NamaBank1 = C.BankPK,B.NomorRekening1 = A.REDMPaymentNo,
                            B.BankBranchName1 = A.REDMPaymentBankBranch,B.NamaNasabah1 = A.REDMPaymentName,
                            B.MataUang1 = D.CurrencyPK
                            FROM dbo.Z_CLIENT_BANK A
                            LEFT JOIN Fundclient B on A.IFUA = B.IFUACode AND B.status IN (1,2)
                            LEFT JOIN Bank C ON A.REDMPaymentBankBIMemberCode = C.SInvestID AND C.status in (1,2)
                            LEFT JOIN Currency D ON D.ID = A.REDMPaymentCCY AND D.status IN (1,2)
                            WHERE REDMPaymentSequentialCode = 1
                            AND C.bankPK IS NOT NULL
        
                            UPDATE B SET  B.NamaBank2 = C.BankPK,B.NomorRekening2 = A.REDMPaymentNo,
                            B.BankBranchName2 = A.REDMPaymentBankBranch,B.NamaNasabah2 = A.REDMPaymentName,
                            B.MataUang2 = D.CurrencyPK
                            FROM dbo.Z_CLIENT_BANK A
                            LEFT JOIN Fundclient B on A.IFUA = B.IFUACode AND B.status IN (1,2)
                            LEFT JOIN Bank C ON A.REDMPaymentBankBIMemberCode = C.SInvestID AND C.status in (1,2)
                            LEFT JOIN Currency D ON D.ID = A.REDMPaymentCCY AND D.status IN (1,2)
                            WHERE REDMPaymentSequentialCode = 2
                            AND C.bankPK IS NOT NULL
        
                            UPDATE B SET  B.NamaBank3 = C.BankPK,B.NomorRekening3 = A.REDMPaymentNo,
                            B.BankBranchName3 = A.REDMPaymentBankBranch,B.NamaNasabah3 = A.REDMPaymentName,
                            B.MataUang3 = D.CurrencyPK
                            FROM dbo.Z_CLIENT_BANK A
                            LEFT JOIN Fundclient B on A.IFUA = B.IFUACode AND B.status IN (1,2)
                            LEFT JOIN Bank C ON A.REDMPaymentBankBIMemberCode = C.SInvestID AND C.status in (1,2)
                            LEFT JOIN Currency D ON D.ID = A.REDMPaymentCCY AND D.status IN (1,2)
                            WHERE REDMPaymentSequentialCode = 3
                            AND C.bankPK IS NOT null

                            select 'Import FundClient Bank Success' Result ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = Convert.ToString(dr["Result"]);
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

        private DataTable CreateDataTableFromNonAPERDFundClientBankTempTxtFile(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

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
            dc.ColumnName = "SID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ClientName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ClientCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentBankBICCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentBankBIMemberCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentBankName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentBankCountry";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentBankBranch";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentCCY";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "REDMPaymentSequentialCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Status";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OpeningDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DeactivationDate";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });


                dr = dt.NewRow();
                dr["SACode"] = s[0];
                dr["SAName"] = s[1];
                dr["SID"] = s[2];
                dr["IFUA"] = s[3];
                dr["ClientName"] = s[4];
                dr["ClientCode"] = s[5];
                dr["REDMPaymentBankBICCode"] = s[6];
                dr["REDMPaymentBankBIMemberCode"] = s[7];
                dr["REDMPaymentBankName"] = s[8];
                dr["REDMPaymentBankCountry"] = s[9];
                dr["REDMPaymentBankBranch"] = s[10];
                dr["REDMPaymentCCY"] = s[11];
                dr["REDMPaymentNo"] = s[12];
                dr["REDMPaymentName"] = s[13];
                dr["REDMPaymentSequentialCode"] = s[14];
                dr["Status"] = s[15];
                dr["OpeningDate"] = s[16];
                dr["DeactivationDate"] = s[17];



                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }
        #endregion

        #region NON APERD BALANCE TXT
        public string ImportBalancePosition(string _fileSource, string _userID)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table Z_CLIENT_BALANCE_POSITION";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.Z_CLIENT_BALANCE_POSITION";
                            bulkCopy.WriteToServer(CreateDataTableFromNonAPERDBalancepTempTxtFile(_fileSource));
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"       
      
        
                                DELETE dbo.FundClientPosition
                                WHERE Date
                                IN
                                (
        	                        SELECT DISTINCT BookingDate FROM dbo.Z_CLIENT_BALANCE_POSITION
                                )
        
       
        
                                INSERT INTO dbo.FundClientPosition
                                        ( 
                                        Date,FundClientPK,FundPK,CashAmount,UnitAmount,
                                        DBUserID,LastUpdate,LastUpdateDB,AvgNav,AUM
                                        )

                                SELECT BookingDate,C.FundClientPK,B.FundPK,
                                0,CAST(A.Balance AS NUMERIC(22,8)),
                                @UsersID,@LastUpdate,@LastUpdate,0,0
                                FROM Z_CLIENT_BALANCE_POSITION A
                                LEFT JOIN Fund B ON A.FundCode = B.SInvestCode AND B.status IN (1,2)
                                LEFT JOIN Fundclient C ON A.IFUA = C.IFUACode AND C.status IN (1,2)
        
   
        
                                        ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import Balance Position Done";

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

        private DataTable CreateDataTableFromNonAPERDBalancepTempTxtFile(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "BookingDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUAName";
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
            dc.ColumnName = "Balance";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Amount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "LastBalanceDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

           

            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });


                dr = dt.NewRow();

                dr["BookingDate"] = s[0];
                dr["IFUA"] = s[1];
                dr["IFUAName"] = s[2];
                dr["SID"] = s[3];
                dr["FundCode"] = s[4];
                dr["FundName"] = s[5];
                dr["IMCode"] = s[6];
                dr["IMName"] = s[7];
                dr["CBCode"] = s[8];
                dr["CBName"] = s[9];
                dr["SACode"] = s[10];
                dr["SAName"] = s[11];
                dr["Balance"] = s[12];
                dr["Amount"] = s[13];
                dr["LastBalanceDate"] = s[14];


                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }
        #endregion

        #region NON APERD TRANSACTION TXT
        public string TransactionSubsRedempText(string _fileSource, string _userID)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table Z_TRANSACTION";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.Z_TRANSACTION";
                            bulkCopy.WriteToServer(CreateDataTableFromSubsRedempTempTxtFile(_fileSource));
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"       
        
        UPDATE Z_TRANSACTION
        
        SET nominal = '0' WHERE nominal = ''
        
        UPDATE Z_TRANSACTION
        SET nominal = '0' WHERE nominal is null
        
        UPDATE Z_TRANSACTION
        SET Unit = '0' WHERE Unit = ''
        
        UPDATE Z_TRANSACTION
        SET Unit = '0' WHERE Unit is null
        
        UPDATE Z_TRANSACTION
        SET FeeUnit = '0' WHERE FeeUnit = ''
        
        UPDATE Z_TRANSACTION
        SET FeeUnit = '0' WHERE FeeUnit is null
        
        UPDATE Z_TRANSACTION
        SET FeeNominal = '0' WHERE FeeNominal = ''
        
        UPDATE Z_TRANSACTION
        SET FeeNominal = '0' WHERE FeeNominal is null
        
        DELETE dbo.ClientSubscription
        WHERE ValueDate
        IN
        (
        	SELECT DISTINCT TransactionDate FROM dbo.Z_TRANSACTION
        	WHERE TransactionType = 'subscription'
        ) and Description = 'Data Migration'
        
        DELETE dbo.ClientRedemption
        WHERE ValueDate
        IN
        (
        	SELECT DISTINCT TransactionDate FROM dbo.Z_TRANSACTION
        	WHERE TransactionType = 'Redemption'
        ) and Description = 'Data Migration'
        
        
        DECLARE @MaxPK INT
        
        SELECT @MaxPK = MAX(ClientSubscriptionPK) + 1 FROM dbo.ClientSubscription
        
        SET @MaxPK = ISNULL(@MaxPK,0)
        
        INSERT INTO dbo.ClientSubscription
                ( ClientSubscriptionPK ,
                  HistoryPK ,Selected ,Status ,Notes ,Type ,FeeType ,NAVDate ,
                  ValueDate ,NAV ,FundPK ,
                  FundClientPK ,CashRefPK ,Description ,
                  CashAmount ,UnitAmount ,TotalCashAmount ,
                  TotalUnitAmount ,SubscriptionFeePercent ,
                  SubscriptionFeeAmount ,AgentPK ,
                  AgentFeePercent ,AgentFeeAmount ,
                  DepartmentPK ,CurrencyPK ,
                  AutoDebitDate ,IsBOTransaction ,
                  BitSinvest ,Posted ,
                  EntryUsersID ,
                  EntryTime ,
                 LastUpdate ,
                  BitImmediateTransaction ,
                  TransactionPK ,IsFrontSync ,
                  ReferenceSInvest ,BankRecipientPK ,
                  Tenor ,InterestRate ,PaymentTerm ,
                  SumberDana ,TransactionPromoPK
                )
        SELECT @maxPK + ROW_NUMBER() OVER(ORDER BY TransactionDate ASC),1,0,1,'',1,
        Case when CAST(isnull(A.FeeNominal,0) AS NUMERIC(22,4)) > 0 then 2 else 1 end
        ,TransactionDate,TransactionDate,0,
        B.FundPK,C.FundClientPK,0,'Data Migration',CAST(A.Nominal AS NUMERIC(22,4)),0,CAST(A.Nominal AS NUMERIC(22,4)) - CAST(A.FeeNominal AS NUMERIC(22,4)),0,0,
        CAST(A.FeeNominal AS NUMERIC(22,4)) ,C.SellingAgentPK,0,0
        ,1,1,0,1,0,0,@UsersID,@LastUpdate,@LastUpdate,0,'',0,A.ReferenceNo,0,0,0,0,0,0
        FROM Z_TRANSACTION A
        LEFT JOIN Fund B ON A.FundCode = B.SInvestCode AND B.status IN (1,2)
        LEFT JOIN Fundclient C ON A.IFUA = C.IFUACode AND C.status IN (1,2)
        WHERE A.TransactionType = 1
        
        
        SELECT @MaxPK = MAX(ClientRedemptionPK) + 1 FROM dbo.ClientRedemption
        
        SET @MaxPK = ISNULL(@MaxPK,0)
        
        INSERT INTO dbo.ClientRedemption
                ( ClientRedemptionPK ,
                  HistoryPK ,Selected ,Status ,Notes ,Type ,FeeType ,NAVDate,
                  ValueDate ,PaymentDate,NAV ,FundPK ,
                  FundClientPK ,CashRefPK,BitRedemptionAll ,Description ,
                  CashAmount ,UnitAmount ,TotalCashAmount ,
                  TotalUnitAmount ,RedemptionFeePercent ,
                  RedemptionFeeAmount ,AgentPK ,
                  AgentFeePercent ,AgentFeeAmount ,
                  DepartmentPK ,CurrencyPK ,
                  IsBOTransaction ,
                  BitSinvest ,Posted ,
                  EntryUsersID ,
                  EntryTime ,
        		  LastUpdate ,
                  TransactionPK ,IsFrontSync ,
                  ReferenceSInvest ,BankRecipientPK,TransferType
                )
        
        
        SELECT @maxPK + ROW_NUMBER() OVER(ORDER BY TransactionDate ASC),1,0,1,'',1,
        Case when CAST(isnull(A.FeeNominal,0) AS NUMERIC(22,4)) > 0 then 2 else 1 end
        ,TransactionDate,TransactionDate,A.PaymentDate,0,
        B.FundPK,C.FundClientPK,0,CASE WHEN A.AllUnit = 'Y' THEN 1 ELSE 0 end,
        'Data Migration',CAST(A.Nominal AS NUMERIC(22,4)),CAST(A.Unit AS Decimal(22,4)),CAST(A.Nominal AS NUMERIC(22,4)) - CAST(A.FeeNominal AS NUMERIC(22,4)),0,0,A.FeeNominal,C.SellingAgentPK,0,0
        ,1,1,0,1,0,@UsersID,@LastUpdate,@LastUpdate,0,0,A.ReferenceNo,1,CASE WHEN A.TransferType = 'SKNBI' THEN 1 WHEN A.TransferType = 'RTGS' THEN 2 ELSE 3 end
        FROM Z_TRANSACTION A
        LEFT JOIN Fund B ON A.FundCode = B.SInvestCode AND B.status IN (1,2)
        LEFT JOIN Fundclient C ON A.IFUA = C.IFUACode AND C.status IN (1,2)
        WHERE A.TransactionType = 2 
        
        ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import Transaction Done";

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

        public string TransactionSwitchingText(string _fileSource, string _userID)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table Z_TRANSACTION_SWITCHING";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.Z_TRANSACTION_SWITCHING";
                            bulkCopy.WriteToServer(CreateDataTableFromSwitchingTempTxtFile(_fileSource));

                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"     
        
        
        UPDATE Z_TRANSACTION_SWITCHING
        SET SwitchOutNominal = '0' WHERE SwitchOutNominal = ''
        
        UPDATE Z_TRANSACTION_SWITCHING
        SET SwitchOutUnit = '0' WHERE SwitchOutUnit = ''
        
        UPDATE Z_TRANSACTION_SWITCHING
        SET FeePercent = '0' WHERE FeePercent = ''
        
        UPDATE Z_TRANSACTION_SWITCHING
        SET Nominal = '0' WHERE Nominal = ''
        
        DELETE dbo.ClientSwitching
        WHERE ValueDate
        IN
        (
        	SELECT DISTINCT TransactionDate FROM dbo.Z_TRANSACTION_SWITCHING
        
        ) and Description = 'Data Migration'
        
        DECLARE @MaxPK INT
        
        SELECT @MaxPK = MAX(ClientswitchingPK) + 1 FROM dbo.Clientswitching
        
        SET @MaxPK = ISNULL(@MaxPK,0)
        
        INSERT INTO dbo.ClientSwitching
                ( ClientSwitchingPK ,          HistoryPK ,
                  Selected ,          Status ,
                  Notes ,          FeeType ,
                  FeeTypeMode ,          NAVDate ,
                  ValueDate ,          PaymentDate ,
                  NAVFundFrom ,          NAVFundTo ,
                  FundPKFrom ,          FundPKTo ,
                  FundClientPK ,          CashRefPKFrom ,
                  CashRefPKTo ,          TransferType ,
                  Description ,          BitSwitchingAll ,
                  CashAmount ,          UnitAmount ,
                  SwitchingFeePercent ,          SwitchingFeeAmount ,
                  TotalCashAmountFundFrom ,          TotalCashAmountFundTo ,
                  TotalUnitAmountFundFrom ,          TotalUnitAmountFundTo ,
                  CurrencyPK ,          IsBoTransaction ,
                  BitSinvest ,          FeeTypeMethod ,
                  Posted ,         
                  EntryUsersID ,          EntryTime ,
               
                  LastUpdate ,               IsProcessed ,
                  UserSwitchingPK ,          IsFrontSync ,
                  TransactionPK ,          ReferenceSInvest ,
                  AgentPK ,          Type
                )
        SELECT @MaxPK + ROW_NUMBER() OVER(ORDER BY TransactionDate ASC),1,0,1,'',FeeChargeFund ,1,TransactionDate,TransactionDate,PaymentDate,
        0,0,B.FundPK,C.FundPK,D.FundClientPK,0,0,CASE WHEN A.TransferType = 'SKNBI' THEN 1 WHEN A.TransferType = 'RTGS' THEN 2 ELSE 3 END,
        'Data Migration',CASE WHEN A.SwitchOutAllUnit = 'Y' THEN 1 ELSE 0 END, CAST(A.SwitchOutNominal AS NUMERIC(18,4)),CAST(A.SwitchOutUnit AS NUMERIC(18,4)) ,
        CAST(A.FeePercent AS NUMERIC(18,4)),CAST(A.Nominal AS NUMERIC(18,4)),
        CAST(A.SwitchOutNominal AS NUMERIC(18,4)) - CAST(A.Nominal AS NUMERIC(18,4)),0,0,0,1,1,0,1,0,@UsersID,@LastUpdate,@LastUpdate,0,0,0,
        '',A.ReferenceNo,D.SellingAgentPK,1
        FROM Z_TRANSACTION_SWITCHING A
        LEFT JOIN Fund B ON A.SwitchOutFundCode = B.SInvestCode AND B.status IN (1,2)
        LEFT JOIN Fund C ON A.SwitchInFundCode = C.SInvestCode AND C.status IN (1,2)
        LEFT JOIN FundClient D ON A.IFUA = D.IFUACode AND D.status IN (1,2)
        ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import Transaction Done";

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

        private DataTable CreateDataTableFromSubsRedempTempTxtFile(string _fileName)
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
            dc.ColumnName = "IFUAName";
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
            dc.ColumnName = "FundCCY";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Nominal";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Unit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AllUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FeeNominal";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FeeUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
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
            dc.ColumnName = "RedmSeqCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RedmBICCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RedmBIMemberCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RedmBankName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RedmPaymentNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RedmPaymentName";
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
            dc.DataType = System.Type.GetType("System.String");
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

            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });


                dr = dt.NewRow();

                dr["TransactionDate"] = s[0];
                dr["TransactionType"] = s[1];
                dr["ReferenceNo"] = s[2];
                dr["SACode"] = s[3];
                dr["IFUA"] = s[4];
                dr["FundCode"] = s[5];
                dr["Nominal"] = s[6];
                dr["Unit"] = s[7];
                dr["AllUnit"] = s[8];
                dr["FeeNominal"] = s[9];
                dr["FeeUnit"] = s[10];
                dr["FeePercent"] = s[11];
                dr["RedmSeqCode"] = s[12];
                dr["RedmBICCode"] = s[13];
                dr["RedmBIMemberCode"] = s[14];
                dr["RedmPaymentNo"] = s[15];
                dr["PaymentDate"] = s[16];
                dr["TransferType"] = s[17];
                dr["SAReference"] = s[18];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        private DataTable CreateDataTableFromSwitchingTempTxtFile(string _fileName)
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
            dc.ColumnName = "ReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchOutIn";
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
            dc.ColumnName = "IFUAName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SID";
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
            dc.ColumnName = "SwitchOutFundCCY";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchOutNominal";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
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
            dc.ColumnName = "FeeChargeFund";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Nominal";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Unit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FeePercent";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInFundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInFundName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInCBCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInFundCCY";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInAmountCCY";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInNominal";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInFundSubName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInFundSubNo";
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
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InputDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "UploadReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SAReferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchOutCBStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInCBStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchOutCBCompletionStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SwitchInCBCompletionStatus";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });


                dr = dt.NewRow();
                dr["TransactionDate"] = s[0];
                dr["TransactionType"] = s[1];
                dr["ReferenceNo"] = s[2];
                dr["SACode"] = s[3];
                dr["IFUA"] = s[4];
                dr["SwitchOutFundCode"] = s[5];
                dr["SwitchOutNominal"] = s[6];
                dr["SwitchOutUnit"] = s[7];
                dr["SwitchOutAllUnit"] = s[8];
                dr["FeeChargeFund"] = s[9];
                dr["Nominal"] = s[10];
                dr["Unit"] = s[11];
                dr["FeePercent"] = s[12];
                dr["SwitchInFundCode"] = s[13];
                dr["PaymentDate"] = s[14];
                dr["TransferType"] = s[15];
                dr["SAReferenceNo"] = s[16];


                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        #endregion

        #region NON APERD DI
        public string ImportDistributedIncome(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table Z_CLIENT_DISTRIBUTED_INCOME";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.Z_CLIENT_DISTRIBUTED_INCOME";
                            bulkCopy.WriteToServer(CreateDataTableFromDistributedIncome(_fileSource));
                       
                        }


                        //logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"  

                                    DELETE dbo.DistributedIncome
                                    WHERE ValueDate
                                    IN
                                    (
        	                            SELECT DISTINCT CumDate FROM dbo.Z_CLIENT_DISTRIBUTED_INCOME
                                    ) and Notes = 'Data Migration'


                                    DECLARE @MaxPK INT
        
                                    SELECT @MaxPK = MAX(DistributedIncomePK) + 1 FROM dbo.DistributedIncome
        
                                    SET @MaxPK = ISNULL(@MaxPK,0)

                                    INSERT INTO DistributedIncome
                                    (
                                    [DistributedIncomePK],[HistoryPK],[Status],[Notes],[ValueDate],[ExDate],[PaymentDate],[FundPK],[Policy],
                                    [NAV],[DistributedIncomeType],[DistributedType],[CashAmount],[DistributedIncomePerUnit],[VariableAmount],
                                    [EntryUsersID],[EntryTime],[DBUserID],[LastUpdate],[LastUpdateDB]
                                    )

                                    select @maxPK + ROW_NUMBER() OVER(ORDER BY CumDate ASC),1,1,'Data Migration',CumDate,ExDate,PaymentDate,isnull(B.FundPK,0),C.Code,
                                    0,D.Code,1,0,A.DIUnit,0,
                                    @UserID,@TimeNow,@UserID,@TimeNow,@TimeNow                                   
                                    from Z_CLIENT_DISTRIBUTED_INCOME A
                                    left join Fund B on A.FundCode = B.SInvestCode and B.status in (1,2)                                   
                                    left join MasterValue C on A.Policy = C.DescOne and C.status in (1,2) and C.ID = 'DistributedIncomePolicy'
                                    left join MasterValue D on A.DataType = D.DescOne and D.status in (1,2) and D.ID = 'DistributedIncomeType'
                                
                                ";
                                cmd1.CommandTimeout = 0;
                                cmd1.Parameters.AddWithValue("@UserID", _userID);
                                cmd1.Parameters.AddWithValue("@TimeNow", _dateTime);
                                cmd1.ExecuteNonQuery();
                            }
                            _msg = "Import NON APERD DISTRIBUTED INCOME Success";

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

        private DataTable CreateDataTableFromDistributedIncome(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "No";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Status";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DataType";
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
            dc.ColumnName = "CumDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCCY";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DIUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Policy";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PaymentDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DINo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InputDate";
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


            using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2003(_fileName)))
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
        
                            dr["No"] = odRdr[0];
                            dr["Status"] = odRdr[1];
                            dr["DataType"] = odRdr[2];
                            dr["FundCode"] = odRdr[3];
                            dr["FundName"] = odRdr[4];
                            dr["IMCode"] = odRdr[5];
                            dr["IMName"] = odRdr[6];
                            dr["CBCode"] = odRdr[7];
                            dr["CBName"] = odRdr[8];
                            dr["CumDate"] = odRdr[9];
                            dr["ExDate"] = odRdr[10];
                            dr["FundCCY"] = odRdr[11];
                            dr["DIUnit"] = odRdr[12];
                            dr["Policy"] = odRdr[13];
                            dr["PaymentDate"] = odRdr[14];
                            dr["DINo"] = odRdr[15];
                            dr["InputDate"] = odRdr[16];
                            dr["IMStatus"] = odRdr[17];
                            dr["CBStatus"] = odRdr[18];

                            dt.Rows.Add(dr);

                        } while (odRdr.Read());
                    }
                }
                odConnection.Close();
            }


            return dt;
        }
        #endregion

        #region Schedule Of Cash Dividend
        public string ScheduleOfCashDividend(string _fileSource, string _userID)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table ZUPLOAD_DIVIDENSCHEDULE_TEMP";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZUPLOAD_DIVIDENSCHEDULE_TEMP";
                            bulkCopy.WriteToServer(CreateDataTableFromScheduleOfCashDividend(_fileSource));
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"       
        
                                        ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import Schedule Of Cash Dividend Done";

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

        private DataTable CreateDataTableFromScheduleOfCashDividend(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "No";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Issuer";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SecurityName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TypeofCA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CumDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RecordingDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DistributionDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExerciseRatio";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExerciseInstrument";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ProceedRatio";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ProceedInstrument";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Description";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RefferenceNo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "LetterDate";
            dc.Unique = false;
            dt.Columns.Add(dc);


            using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_fileSource)))
            {
                odConnection.Open();
                using (OleDbCommand odCmd = odConnection.CreateCommand())
                {
                    // _oldfilename = nama sheet yang ada di file excel yang diimport
                    odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                    using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                    {
                        // start counting from index = 1 --> skipping the header (index=0)
                        for (int i = 0; i < 2; i++)
                        {
                            odRdr.Read();
                        }
                        do
                        {
                            dr = dt.NewRow();

                            dr["No"] = odRdr[0];
                            dr["Issuer"] = odRdr[1];
                            dr["SecurityCode"] = odRdr[2];
                            dr["SecurityName"] = odRdr[3];
                            dr["TypeofCA"] = odRdr[4];
                            dr["CumDate"] = odRdr[5];
                            dr["RecordingDate"] = odRdr[6];
                            dr["DistributionDate"] = odRdr[7];
                            dr["ExerciseRatio"] = odRdr[8];
                            dr["ExerciseInstrument"] = odRdr[9];
                            dr["ProceedRatio"] = odRdr[10];
                            dr["ProceedInstrument"] = odRdr[11];
                            dr["Description"] = odRdr[12];
                            dr["RefferenceNo"] = odRdr[13];
                            dr["LetterDate"] = odRdr[14];


                            dt.Rows.Add(dr);
                        } while (odRdr.Read());
                    }
                }
                odConnection.Close();
            }

            return dt;
        }
        #endregion

        //APERD
        #region APERD Client TXT

        public string ImportAPERDFundClientInd(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table ZAPERD_CLIENT_IND_TEMP";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZAPERD_CLIENT_IND_TEMP";
                            bulkCopy.WriteToServer(CreateDataTableFromAPERDFundClientInd(_fileSource));
                            //_msg = "Import APERD INDIVIDUAL Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"  delete ZAPERD_CLIENT_IND where col4 in (select col4 from ZAPERD_CLIENT_IND_TEMP)

                                    insert into ZAPERD_CLIENT_IND
                                    select * from ZAPERD_CLIENT_IND_TEMP
                                ";
                                cmd1.CommandTimeout = 0;
                                cmd1.ExecuteNonQuery();
                            }
                            _msg = "Import APERD INDIVIDUAL Success";

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

        private DataTable CreateDataTableFromAPERDFundClientInd(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col6";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col7";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col8";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col9";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col10";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col13";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col14";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col15";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col16";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col17";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col18";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col19";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col20";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col21";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col22";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col23";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col24";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col25";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col26";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col27";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col28";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col29";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col30";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col31";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col32";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col33";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col34";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col35";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col36";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col37";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col38";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col39";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col40";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col41";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col42";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col43";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col44";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col45";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col46";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col47";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col48";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col49";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col50";
            dc.Unique = false;
            dt.Columns.Add(dc);



            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });

                dr = dt.NewRow();
                dr["Col1"] = s[0];
                dr["Col2"] = s[1];
                dr["Col3"] = s[2];
                dr["Col4"] = s[3];
                dr["Col5"] = s[4];
                dr["Col6"] = s[5];
                dr["Col7"] = s[6];
                dr["Col8"] = s[7];
                dr["Col9"] = s[8];
                dr["Col10"] = s[9];
                dr["Col11"] = s[10];
                dr["Col12"] = s[11];
                dr["Col13"] = s[12];
                dr["Col14"] = s[13];
                dr["Col15"] = s[14];
                dr["Col16"] = s[15];
                dr["Col17"] = s[16];
                dr["Col18"] = s[17];
                dr["Col19"] = s[18];
                dr["Col20"] = s[19];
                dr["Col21"] = s[20];
                dr["Col22"] = s[21];
                dr["Col23"] = s[22];
                dr["Col24"] = s[23];
                dr["Col25"] = s[24];
                dr["Col26"] = s[25];
                dr["Col27"] = s[26];
                dr["Col28"] = s[27];
                dr["Col29"] = s[28];
                dr["Col30"] = s[29];
                dr["Col31"] = s[30];
                dr["Col32"] = s[31];
                dr["Col33"] = s[32];
                dr["Col34"] = s[33];
                dr["Col35"] = s[34];
                dr["Col36"] = s[35];
                dr["Col37"] = s[36];
                dr["Col38"] = s[37];
                dr["Col39"] = s[38];
                dr["Col40"] = s[39];
                dr["Col41"] = s[40];
                dr["Col42"] = s[41];
                dr["Col43"] = s[42];
                dr["Col44"] = s[43];
                dr["Col45"] = s[44];
                dr["Col46"] = s[45];
                dr["Col47"] = s[46];
                dr["Col48"] = s[47];
                dr["Col49"] = s[48];
                dr["Col50"] = s[49];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }


        public string ImportAPERDFundClientIns(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table ZAPERD_CLIENT_INS_TEMP";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZAPERD_CLIENT_INS_TEMP";
                            bulkCopy.WriteToServer(CreateDataTableFromAPERDFundClientIns(_fileSource));
                            //_msg = "Import APERD INSTITUSI Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"  delete ZAPERD_CLIENT_INS where col4 in (select col4 from ZAPERD_CLIENT_INS_TEMP)

                                    insert into ZAPERD_CLIENT_INS
                                    select * from ZAPERD_CLIENT_INS_TEMP
                                                        ";
                                cmd1.CommandTimeout = 0;
                                cmd1.Parameters.AddWithValue("@UserID", _userID);
                                cmd1.Parameters.AddWithValue("@TimeNow", _dateTime);
                                cmd1.ExecuteNonQuery();
                            }
                            _msg = "Import APERD INSTITUSI Success";

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

        private DataTable CreateDataTableFromAPERDFundClientIns(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col6";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col7";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col8";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col9";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col10";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col13";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col14";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col15";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col16";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col17";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col18";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col19";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col20";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col21";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col22";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col23";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col24";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col25";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col26";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col27";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col28";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col29";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col30";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col31";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col32";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col33";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col34";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col35";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col36";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col37";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col38";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col39";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col40";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col41";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col42";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col43";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col44";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col45";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col46";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col47";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col48";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col49";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col50";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col51";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col52";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col53";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col54";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col55";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col56";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col57";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col58";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col59";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col60";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col61";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col62";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col63";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col64";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col65";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col66";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col67";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col68";
            dc.Unique = false;
            dt.Columns.Add(dc);




            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });

                dr = dt.NewRow();
                dr["Col1"] = s[0];
                dr["Col2"] = s[1];
                dr["Col3"] = s[2];
                dr["Col4"] = s[3];
                dr["Col5"] = s[4];
                dr["Col6"] = s[5];
                dr["Col7"] = s[6];
                dr["Col8"] = s[7];
                dr["Col9"] = s[8];
                dr["Col10"] = s[9];
                dr["Col11"] = s[10];
                dr["Col12"] = s[11];
                dr["Col13"] = s[12];
                dr["Col14"] = s[13];
                dr["Col15"] = s[14];
                dr["Col16"] = s[15];
                dr["Col17"] = s[16];
                dr["Col18"] = s[17];
                dr["Col19"] = s[18];
                dr["Col20"] = s[19];
                dr["Col21"] = s[20];
                dr["Col22"] = s[21];
                dr["Col23"] = s[22];
                dr["Col24"] = s[23];
                dr["Col25"] = s[24];
                dr["Col26"] = s[25];
                dr["Col27"] = s[26];
                dr["Col28"] = s[27];
                dr["Col29"] = s[28];
                dr["Col30"] = s[29];
                dr["Col31"] = s[30];
                dr["Col32"] = s[31];
                dr["Col33"] = s[32];
                dr["Col34"] = s[33];
                dr["Col35"] = s[34];
                dr["Col36"] = s[35];
                dr["Col37"] = s[36];
                dr["Col38"] = s[37];
                dr["Col39"] = s[38];
                dr["Col40"] = s[39];
                dr["Col41"] = s[40];
                dr["Col42"] = s[41];
                dr["Col43"] = s[42];
                dr["Col44"] = s[43];
                dr["Col45"] = s[44];
                dr["Col46"] = s[45];
                dr["Col47"] = s[46];
                dr["Col48"] = s[47];
                dr["Col49"] = s[48];
                dr["Col50"] = s[49];
                dr["Col51"] = s[50];
                dr["Col52"] = s[51];
                dr["Col53"] = s[52];
                dr["Col54"] = s[53];
                dr["Col55"] = s[54];
                dr["Col56"] = s[55];
                dr["Col57"] = s[56];
                dr["Col58"] = s[57];
                dr["Col59"] = s[58];
                dr["Col60"] = s[59];
                dr["Col61"] = s[60];
                dr["Col62"] = s[61];
                dr["Col63"] = s[62];
                dr["Col64"] = s[63];
                dr["Col65"] = s[64];
                dr["Col66"] = s[65];
                dr["Col67"] = s[66];
                dr["Col68"] = s[67];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        #endregion  

        #region APERD BALANCE TXT
        public string ImportAPERDBalancePosition(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table ZAPERD_BALANCE_POSITION";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZAPERD_BALANCE_POSITION";
                            bulkCopy.WriteToServer(CreateDataTableFromAPERDBalancePosition(_fileSource));
                            _msg = "Import APERD BALANCE Success";
                        }

                        // logic kalo Reconcile success
                        //                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        //                        {
                        //                            conn.Open();
                        //                            using (SqlCommand cmd1 = conn.CreateCommand())
                        //                            {
                        //                                cmd1.CommandText =
                        //                                @"  
                        //                                ";
                        //                                cmd1.CommandTimeout = 0;
                        //                                cmd1.Parameters.AddWithValue("@UserID", _userID);
                        //                                cmd1.Parameters.AddWithValue("@TimeNow", _dateTime);
                        //                                cmd1.ExecuteNonQuery();
                        //                            }
                        //                            _msg = "Import FundClient Individual Success";

                        //                        }



                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromAPERDBalancePosition(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col6";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col7";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col8";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col9";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col10";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col13";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col14";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });

                dr = dt.NewRow();
                dr["Col1"] = s[0];
                dr["Col2"] = s[1];
                dr["Col3"] = s[2];
                dr["Col4"] = s[3];
                dr["Col5"] = s[4];
                dr["Col6"] = s[5];
                dr["Col7"] = s[6];
                dr["Col8"] = s[7];
                dr["Col9"] = s[8];
                dr["Col10"] = s[9];
                dr["Col11"] = s[10];
                dr["Col12"] = s[11];
                dr["Col13"] = s[12];
                dr["Col14"] = s[13];
               
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }
        #endregion  

        #region APERD TRANSACTION TXT
        public string ImportAPERDSubRed(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table ZAPERD_SUBRED";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZAPERD_SUBRED";
                            bulkCopy.WriteToServer(CreateDataTableFromAPERDTrxSubRed(_fileSource));
                            _msg = "Import APERD TRX SUB RED Success";
                        }

                        // logic kalo Reconcile success
                        //                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        //                        {
                        //                            conn.Open();
                        //                            using (SqlCommand cmd1 = conn.CreateCommand())
                        //                            {
                        //                                cmd1.CommandText =
                        //                                @"  
                        //                                ";
                        //                                cmd1.CommandTimeout = 0;
                        //                                cmd1.Parameters.AddWithValue("@UserID", _userID);
                        //                                cmd1.Parameters.AddWithValue("@TimeNow", _dateTime);
                        //                                cmd1.ExecuteNonQuery();
                        //                            }
                        //                            _msg = "Import FundClient Individual Success";

                        //                        }



                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromAPERDTrxSubRed(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col6";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col7";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col8";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col9";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col10";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col13";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col14";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col15";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col16";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col17";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col18";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col19";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });

                dr = dt.NewRow();
                dr["Col1"] = s[0];
                dr["Col2"] = s[1];
                dr["Col3"] = s[2];
                dr["Col4"] = s[3];
                dr["Col5"] = s[4];
                dr["Col6"] = s[5];
                dr["Col7"] = s[6];
                dr["Col8"] = s[7];
                dr["Col9"] = s[8];
                dr["Col10"] = s[9];
                dr["Col11"] = s[10];
                dr["Col12"] = s[11];
                dr["Col13"] = s[12];
                dr["Col14"] = s[13];
                dr["Col15"] = s[14];
                dr["Col16"] = s[15];
                dr["Col17"] = s[16];
                dr["Col18"] = s[17];
                dr["Col19"] = s[18];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public string ImportAPERDSwitching(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table ZAPERD_SWITCHING";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZAPERD_SWITCHING";
                            bulkCopy.WriteToServer(CreateDataTableFromAPERDTrxSwitching(_fileSource));
                            _msg = "Import APERD TRX SWITCHING Success";
                        }

                        // logic kalo Reconcile success
                        //                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        //                        {
                        //                            conn.Open();
                        //                            using (SqlCommand cmd1 = conn.CreateCommand())
                        //                            {
                        //                                cmd1.CommandText =
                        //                                @"  
                        //                                ";
                        //                                cmd1.CommandTimeout = 0;
                        //                                cmd1.Parameters.AddWithValue("@UserID", _userID);
                        //                                cmd1.Parameters.AddWithValue("@TimeNow", _dateTime);
                        //                                cmd1.ExecuteNonQuery();
                        //                            }
                        //                            _msg = "Import FundClient Individual Success";

                        //                        }



                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromAPERDTrxSwitching(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col6";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col7";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col8";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col9";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col10";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col13";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col14";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col15";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col16";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col17";
            dc.Unique = false;
            dt.Columns.Add(dc);

        


            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });

                dr = dt.NewRow();
                dr["Col1"] = s[0];
                dr["Col2"] = s[1];
                dr["Col3"] = s[2];
                dr["Col4"] = s[3];
                dr["Col5"] = s[4];
                dr["Col6"] = s[5];
                dr["Col7"] = s[6];
                dr["Col8"] = s[7];
                dr["Col9"] = s[8];
                dr["Col10"] = s[9];
                dr["Col11"] = s[10];
                dr["Col12"] = s[11];
                dr["Col13"] = s[12];
                dr["Col14"] = s[13];
                dr["Col15"] = s[14];
                dr["Col16"] = s[15];
                dr["Col17"] = s[16];
              

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        #endregion  

        #region APERD DI
        public string ImportAPERDDistributedIncome(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table ZAPERD_DI";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZAPERD_DI";
                            bulkCopy.WriteToServer(CreateDataTableFromAPERDDistributedIncome(_fileSource));
                            _msg = "Import APERD DISTRIBUTED INCOME Success";
                        }

                        // logic kalo Reconcile success
                        //                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        //                        {
                        //                            conn.Open();
                        //                            using (SqlCommand cmd1 = conn.CreateCommand())
                        //                            {
                        //                                cmd1.CommandText =
                        //                                @"  
                        //                                ";
                        //                                cmd1.CommandTimeout = 0;
                        //                                cmd1.Parameters.AddWithValue("@UserID", _userID);
                        //                                cmd1.Parameters.AddWithValue("@TimeNow", _dateTime);
                        //                                cmd1.ExecuteNonQuery();
                        //                            }
                        //                            _msg = "Import FundClient Individual Success";

                        //                        }



                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromAPERDDistributedIncome(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col6";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col7";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col8";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col9";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col10";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col13";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col14";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col15";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col16";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col17";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col18";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Col19";
            dc.Unique = false;
            dt.Columns.Add(dc);


            using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2003(_fileName)))
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
                            dr["Col1"] = odRdr[0];
                            dr["Col2"] = odRdr[1];
                            dr["Col3"] = odRdr[2];
                            dr["Col4"] = odRdr[3];
                            dr["Col5"] = odRdr[4];
                            dr["Col6"] = odRdr[5];
                            dr["Col7"] = odRdr[6];
                            dr["Col8"] = odRdr[7];
                            dr["Col9"] = odRdr[8];
                            dr["Col10"] = odRdr[9];
                            dr["Col11"] = odRdr[10];
                            dr["Col12"] = odRdr[11];
                            dr["Col13"] = odRdr[12];
                            dr["Col14"] = odRdr[13];
                            dr["Col15"] = odRdr[14];
                            dr["Col16"] = odRdr[15];
                            dr["Col17"] = odRdr[16];
                            dr["Col18"] = odRdr[17];
                            dr["Col19"] = odRdr[18];

                            dt.Rows.Add(dr);

                        } while (odRdr.Read());
                    }
                }
                odConnection.Close();
            }

          
            return dt;
        }
        #endregion


        public Boolean KYCAperd_Ind(string _userID, KYCAperdInd _KYCAperdInd)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {




                        cmd.CommandText = @"
                        select * From ZAPERD_CLIENT_IND_TEMP
                       ";

                        cmd.CommandTimeout = 0;



                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "KYCAperdInd" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "KYCAperdInd" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "KYCAperd";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KYC Aperd Individu");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<KYCAperdInd> rList = new List<KYCAperdInd>();
                                    while (dr0.Read())
                                    {

                                        KYCAperdInd rSingle = new KYCAperdInd();
                                        rSingle.Col1 = Convert.ToString(dr0["Col1"]);
                                        rSingle.Col2 = Convert.ToString(dr0["Col2"]);
                                        rSingle.Col3 = Convert.ToString(dr0["Col3"]);
                                        rSingle.Col4 = Convert.ToString(dr0["Col4"]);
                                        rSingle.Col5 = Convert.ToString(dr0["Col5"]);
                                        rSingle.Col6 = Convert.ToString(dr0["Col6"]);
                                        rSingle.Col7 = Convert.ToString(dr0["Col7"]);
                                        rSingle.Col8 = Convert.ToString(dr0["Col8"]);
                                        rSingle.Col9 = Convert.ToString(dr0["Col9"]);
                                        rSingle.Col10 = Convert.ToString(dr0["Col10"]);

                                        rSingle.Col11 = Convert.ToString(dr0["Col11"]);
                                        rSingle.Col12 = Convert.ToString(dr0["Col12"]);
                                        rSingle.Col13 = Convert.ToString(dr0["Col13"]);
                                        rSingle.Col14 = Convert.ToString(dr0["Col14"]);
                                        rSingle.Col15 = Convert.ToString(dr0["Col15"]);
                                        rSingle.Col16 = Convert.ToString(dr0["Col16"]);
                                        rSingle.Col17 = Convert.ToString(dr0["Col17"]);
                                        rSingle.Col18 = Convert.ToString(dr0["Col18"]);
                                        rSingle.Col19 = Convert.ToString(dr0["Col19"]);
                                        rSingle.Col20 = Convert.ToString(dr0["Col20"]);

                                        rSingle.Col21 = Convert.ToString(dr0["Col21"]);
                                        rSingle.Col22 = Convert.ToString(dr0["Col22"]);
                                        rSingle.Col23 = Convert.ToString(dr0["Col23"]);
                                        rSingle.Col24 = Convert.ToString(dr0["Col24"]);
                                        rSingle.Col25 = Convert.ToString(dr0["Col25"]);
                                        rSingle.Col26 = Convert.ToString(dr0["Col26"]);
                                        rSingle.Col27 = Convert.ToString(dr0["Col27"]);
                                        rSingle.Col28 = Convert.ToString(dr0["Col28"]);
                                        rSingle.Col29 = Convert.ToString(dr0["Col29"]);
                                        rSingle.Col30 = Convert.ToString(dr0["Col30"]);

                                        rSingle.Col31 = Convert.ToString(dr0["Col31"]);
                                        rSingle.Col32 = Convert.ToString(dr0["Col32"]);
                                        rSingle.Col33 = Convert.ToString(dr0["Col33"]);
                                        rSingle.Col34 = Convert.ToString(dr0["Col34"]);
                                        rSingle.Col35 = Convert.ToString(dr0["Col35"]);
                                        rSingle.Col36 = Convert.ToString(dr0["Col36"]);
                                        rSingle.Col37 = Convert.ToString(dr0["Col37"]);
                                        rSingle.Col38 = Convert.ToString(dr0["Col38"]);
                                        rSingle.Col39 = Convert.ToString(dr0["Col39"]);
                                        rSingle.Col40 = Convert.ToString(dr0["Col40"]);

                                        rSingle.Col41 = Convert.ToString(dr0["Col41"]);
                                        rSingle.Col42 = Convert.ToString(dr0["Col42"]);
                                        rSingle.Col43 = Convert.ToString(dr0["Col43"]);
                                        rSingle.Col44 = Convert.ToString(dr0["Col44"]);
                                        rSingle.Col45 = Convert.ToString(dr0["Col45"]);
                                        rSingle.Col46 = Convert.ToString(dr0["Col46"]);
                                        rSingle.Col47 = Convert.ToString(dr0["Col47"]);
                                        rSingle.Col48 = Convert.ToString(dr0["Col48"]);
                                        rSingle.Col49 = Convert.ToString(dr0["Col49"]);
                                        rSingle.Col50 = Convert.ToString(dr0["Col50"]);








                                        rList.Add(rSingle);

                                    }



                                    var GroupByReference =
                                            from r in rList
                                            //orderby r ascending
                                            group r by new { } into rGroup
                                            select rGroup;

                                    int incRowExcel = 1;
                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        worksheet.Cells[incRowExcel, 1].Value = "Last Update";
                                        worksheet.Cells[incRowExcel, 2].Value = "SA Code";
                                        worksheet.Cells[incRowExcel, 3].Value = "SA Name";
                                        worksheet.Cells[incRowExcel, 4].Value = "SID";
                                        worksheet.Cells[incRowExcel, 5].Value = "Investor Type";
                                        worksheet.Cells[incRowExcel, 6].Value = "First Name";
                                        worksheet.Cells[incRowExcel, 7].Value = "Middle Name";
                                        worksheet.Cells[incRowExcel, 8].Value = "Last Name";
                                        worksheet.Cells[incRowExcel, 9].Value = "Country of Nationality";
                                        worksheet.Cells[incRowExcel, 10].Value = "Identification";

                                        worksheet.Cells[incRowExcel, 11].Value = "ID No.";
                                        worksheet.Cells[incRowExcel, 12].Value = "ID Expiration Date";
                                        worksheet.Cells[incRowExcel, 13].Value = "NPWP No.";
                                        worksheet.Cells[incRowExcel, 14].Value = "NPWP Registration Date";
                                        worksheet.Cells[incRowExcel, 15].Value = "Country of Birth";
                                        worksheet.Cells[incRowExcel, 16].Value = "Place of Birth";
                                        worksheet.Cells[incRowExcel, 17].Value = "Date of Birth";
                                        worksheet.Cells[incRowExcel, 18].Value = "Gender";
                                        worksheet.Cells[incRowExcel, 19].Value = "Educational Background";
                                        worksheet.Cells[incRowExcel, 20].Value = "Mother's Maiden Name";

                                        worksheet.Cells[incRowExcel, 21].Value = "Religion";
                                        worksheet.Cells[incRowExcel, 22].Value = "Occupation";
                                        worksheet.Cells[incRowExcel, 23].Value = "Income Level (IDR)";
                                        worksheet.Cells[incRowExcel, 24].Value = "Marital Status";
                                        worksheet.Cells[incRowExcel, 25].Value = "Spouse's Name";
                                        worksheet.Cells[incRowExcel, 26].Value = "Investor's Risk Profile";
                                        worksheet.Cells[incRowExcel, 27].Value = "Investment Objective";
                                        worksheet.Cells[incRowExcel, 28].Value = "Source of Fund";
                                        worksheet.Cells[incRowExcel, 29].Value = "Asset Owner";
                                        worksheet.Cells[incRowExcel, 30].Value = "KTP Address";

                                        worksheet.Cells[incRowExcel, 31].Value = "KTP City Code";
                                        worksheet.Cells[incRowExcel, 32].Value = "KTP Postal Code";
                                        worksheet.Cells[incRowExcel, 33].Value = "Correspondence Address";
                                        worksheet.Cells[incRowExcel, 34].Value = "Correspondence City Code";
                                        worksheet.Cells[incRowExcel, 35].Value = "Correspondence City Name";
                                        worksheet.Cells[incRowExcel, 36].Value = "Correspondence Postal Code";
                                        worksheet.Cells[incRowExcel, 37].Value = "Country of Correspondence";
                                        worksheet.Cells[incRowExcel, 38].Value = "Domicile Address";
                                        worksheet.Cells[incRowExcel, 39].Value = "Domicile City Code";
                                        worksheet.Cells[incRowExcel, 40].Value = "Domicile City Name";

                                        worksheet.Cells[incRowExcel, 41].Value = "Domicile Postal Code";
                                        worksheet.Cells[incRowExcel, 42].Value = "Country of Domicile";
                                        worksheet.Cells[incRowExcel, 43].Value = "Home Phone";
                                        worksheet.Cells[incRowExcel, 44].Value = "Mobile Phone";
                                        worksheet.Cells[incRowExcel, 45].Value = "Facsimile";
                                        worksheet.Cells[incRowExcel, 46].Value = "Email";
                                        worksheet.Cells[incRowExcel, 47].Value = "Statement Type";
                                        worksheet.Cells[incRowExcel, 48].Value = "FATCA (Status)";
                                        worksheet.Cells[incRowExcel, 49].Value = "TIN / Foreign TIN";
                                        worksheet.Cells[incRowExcel, 50].Value = "TIN / Foreign TIN Issuance Country";



                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 50].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 50].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 50].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 50].Style.VerticalAlignment = ExcelVerticalAlignment.Center;







                                        incRowExcel = incRowExcel + 1;

                                        int first = incRowExcel;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;

                                        foreach (var rsDetail in rsHeader)
                                        {
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 50].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.Col1;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.Col2;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.Col3;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Col4;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Col5;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Col6;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.Col7;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.Col8;
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.Col9;
                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.Col10;

                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Col11;
                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.Col12;
                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail.Col13;
                                            worksheet.Cells[incRowExcel, 14].Value = rsDetail.Col14;
                                            worksheet.Cells[incRowExcel, 15].Value = rsDetail.Col15;
                                            worksheet.Cells[incRowExcel, 16].Value = rsDetail.Col16;
                                            worksheet.Cells[incRowExcel, 17].Value = rsDetail.Col17;
                                            worksheet.Cells[incRowExcel, 18].Value = rsDetail.Col18;
                                            worksheet.Cells[incRowExcel, 19].Value = rsDetail.Col19;
                                            worksheet.Cells[incRowExcel, 20].Value = rsDetail.Col20;

                                            worksheet.Cells[incRowExcel, 21].Value = rsDetail.Col21;
                                            worksheet.Cells[incRowExcel, 22].Value = rsDetail.Col22;
                                            worksheet.Cells[incRowExcel, 23].Value = rsDetail.Col23;
                                            worksheet.Cells[incRowExcel, 24].Value = rsDetail.Col24;
                                            worksheet.Cells[incRowExcel, 25].Value = rsDetail.Col25;
                                            worksheet.Cells[incRowExcel, 26].Value = rsDetail.Col26;
                                            worksheet.Cells[incRowExcel, 27].Value = rsDetail.Col27;
                                            worksheet.Cells[incRowExcel, 28].Value = rsDetail.Col28;
                                            worksheet.Cells[incRowExcel, 29].Value = rsDetail.Col29;
                                            worksheet.Cells[incRowExcel, 30].Value = rsDetail.Col30;

                                            worksheet.Cells[incRowExcel, 31].Value = rsDetail.Col31;
                                            worksheet.Cells[incRowExcel, 32].Value = rsDetail.Col32;
                                            worksheet.Cells[incRowExcel, 33].Value = rsDetail.Col33;
                                            worksheet.Cells[incRowExcel, 34].Value = rsDetail.Col34;
                                            worksheet.Cells[incRowExcel, 35].Value = rsDetail.Col35;
                                            worksheet.Cells[incRowExcel, 36].Value = rsDetail.Col36;
                                            worksheet.Cells[incRowExcel, 37].Value = rsDetail.Col37;
                                            worksheet.Cells[incRowExcel, 38].Value = rsDetail.Col38;
                                            worksheet.Cells[incRowExcel, 39].Value = rsDetail.Col39;
                                            worksheet.Cells[incRowExcel, 40].Value = rsDetail.Col40;

                                            worksheet.Cells[incRowExcel, 41].Value = rsDetail.Col41;
                                            worksheet.Cells[incRowExcel, 42].Value = rsDetail.Col42;
                                            worksheet.Cells[incRowExcel, 43].Value = rsDetail.Col43;
                                            worksheet.Cells[incRowExcel, 44].Value = rsDetail.Col44;
                                            worksheet.Cells[incRowExcel, 45].Value = rsDetail.Col45;
                                            worksheet.Cells[incRowExcel, 46].Value = rsDetail.Col46;
                                            worksheet.Cells[incRowExcel, 47].Value = rsDetail.Col47;
                                            worksheet.Cells[incRowExcel, 48].Value = rsDetail.Col48;
                                            worksheet.Cells[incRowExcel, 49].Value = rsDetail.Col49;
                                            worksheet.Cells[incRowExcel, 50].Value = rsDetail.Col50;

                                            incRowExcel++;






                                        }


                                        _endRowDetail = incRowExcel + 3;

                                    }


                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 15];

                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.Column(10).AutoFit();

                                    worksheet.Column(11).AutoFit();
                                    worksheet.Column(12).AutoFit();
                                    worksheet.Column(13).AutoFit();
                                    worksheet.Column(14).AutoFit();
                                    worksheet.Column(15).AutoFit();
                                    worksheet.Column(16).AutoFit();
                                    worksheet.Column(17).AutoFit();
                                    worksheet.Column(18).AutoFit();
                                    worksheet.Column(19).AutoFit();
                                    worksheet.Column(20).AutoFit();

                                    worksheet.Column(21).AutoFit();
                                    worksheet.Column(22).AutoFit();
                                    worksheet.Column(23).AutoFit();
                                    worksheet.Column(24).AutoFit();
                                    worksheet.Column(25).AutoFit();
                                    worksheet.Column(26).AutoFit();
                                    worksheet.Column(27).AutoFit();
                                    worksheet.Column(28).AutoFit();
                                    worksheet.Column(29).AutoFit();
                                    worksheet.Column(30).AutoFit();

                                    worksheet.Column(31).AutoFit();
                                    worksheet.Column(32).AutoFit();
                                    worksheet.Column(33).AutoFit();
                                    worksheet.Column(34).AutoFit();
                                    worksheet.Column(35).AutoFit();
                                    worksheet.Column(36).AutoFit();
                                    worksheet.Column(37).AutoFit();
                                    worksheet.Column(38).AutoFit();
                                    worksheet.Column(39).AutoFit();
                                    worksheet.Column(40).AutoFit();

                                    worksheet.Column(41).AutoFit();
                                    worksheet.Column(42).AutoFit();
                                    worksheet.Column(43).AutoFit();
                                    worksheet.Column(44).AutoFit();
                                    worksheet.Column(45).AutoFit();
                                    worksheet.Column(46).AutoFit();
                                    worksheet.Column(47).AutoFit();
                                    worksheet.Column(48).AutoFit();
                                    worksheet.Column(49).AutoFit();
                                    worksheet.Column(50).AutoFit();




                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&12 KYC Aperd Ind";


                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();


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

        public Boolean KYCAperd_Ins(string _userID, KYCAperdIns _KYCAperdIns)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {




                        cmd.CommandText = @"
                        select * From ZAPERD_CLIENT_INS_TEMP
                       ";

                        cmd.CommandTimeout = 0;



                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "KYCAperdIns" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "KYCAperdIns" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "KYCAperd";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KYC Aperd Insividu");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<KYCAperdIns> rList = new List<KYCAperdIns>();
                                    while (dr0.Read())
                                    {

                                        KYCAperdIns rSingle = new KYCAperdIns();
                                        rSingle.Col1 = Convert.ToString(dr0["Col1"]);
                                        rSingle.Col2 = Convert.ToString(dr0["Col2"]);
                                        rSingle.Col3 = Convert.ToString(dr0["Col3"]);
                                        rSingle.Col4 = Convert.ToString(dr0["Col4"]);
                                        rSingle.Col5 = Convert.ToString(dr0["Col5"]);
                                        rSingle.Col6 = Convert.ToString(dr0["Col6"]);
                                        rSingle.Col7 = Convert.ToString(dr0["Col7"]);
                                        rSingle.Col8 = Convert.ToString(dr0["Col8"]);
                                        rSingle.Col9 = Convert.ToString(dr0["Col9"]);
                                        rSingle.Col10 = Convert.ToString(dr0["Col10"]);

                                        rSingle.Col11 = Convert.ToString(dr0["Col11"]);
                                        rSingle.Col12 = Convert.ToString(dr0["Col12"]);
                                        rSingle.Col13 = Convert.ToString(dr0["Col13"]);
                                        rSingle.Col14 = Convert.ToString(dr0["Col14"]);
                                        rSingle.Col15 = Convert.ToString(dr0["Col15"]);
                                        rSingle.Col16 = Convert.ToString(dr0["Col16"]);
                                        rSingle.Col17 = Convert.ToString(dr0["Col17"]);
                                        rSingle.Col18 = Convert.ToString(dr0["Col18"]);
                                        rSingle.Col19 = Convert.ToString(dr0["Col19"]);
                                        rSingle.Col20 = Convert.ToString(dr0["Col20"]);

                                        rSingle.Col21 = Convert.ToString(dr0["Col21"]);
                                        rSingle.Col22 = Convert.ToString(dr0["Col22"]);
                                        rSingle.Col23 = Convert.ToString(dr0["Col23"]);
                                        rSingle.Col24 = Convert.ToString(dr0["Col24"]);
                                        rSingle.Col25 = Convert.ToString(dr0["Col25"]);
                                        rSingle.Col26 = Convert.ToString(dr0["Col26"]);
                                        rSingle.Col27 = Convert.ToString(dr0["Col27"]);
                                        rSingle.Col28 = Convert.ToString(dr0["Col28"]);
                                        rSingle.Col29 = Convert.ToString(dr0["Col29"]);
                                        rSingle.Col30 = Convert.ToString(dr0["Col30"]);

                                        rSingle.Col31 = Convert.ToString(dr0["Col31"]);
                                        rSingle.Col32 = Convert.ToString(dr0["Col32"]);
                                        rSingle.Col33 = Convert.ToString(dr0["Col33"]);
                                        rSingle.Col34 = Convert.ToString(dr0["Col34"]);
                                        rSingle.Col35 = Convert.ToString(dr0["Col35"]);
                                        rSingle.Col36 = Convert.ToString(dr0["Col36"]);
                                        rSingle.Col37 = Convert.ToString(dr0["Col37"]);
                                        rSingle.Col38 = Convert.ToString(dr0["Col38"]);
                                        rSingle.Col39 = Convert.ToString(dr0["Col39"]);
                                        rSingle.Col40 = Convert.ToString(dr0["Col40"]);

                                        rSingle.Col41 = Convert.ToString(dr0["Col41"]);
                                        rSingle.Col42 = Convert.ToString(dr0["Col42"]);
                                        rSingle.Col43 = Convert.ToString(dr0["Col43"]);
                                        rSingle.Col44 = Convert.ToString(dr0["Col44"]);
                                        rSingle.Col45 = Convert.ToString(dr0["Col45"]);
                                        rSingle.Col46 = Convert.ToString(dr0["Col46"]);
                                        rSingle.Col47 = Convert.ToString(dr0["Col47"]);
                                        rSingle.Col48 = Convert.ToString(dr0["Col48"]);
                                        rSingle.Col49 = Convert.ToString(dr0["Col49"]);
                                        rSingle.Col50 = Convert.ToString(dr0["Col50"]);

                                        rSingle.Col51 = Convert.ToString(dr0["Col51"]);
                                        rSingle.Col52 = Convert.ToString(dr0["Col52"]);
                                        rSingle.Col53 = Convert.ToString(dr0["Col53"]);
                                        rSingle.Col54 = Convert.ToString(dr0["Col54"]);
                                        rSingle.Col55 = Convert.ToString(dr0["Col55"]);
                                        rSingle.Col56 = Convert.ToString(dr0["Col56"]);
                                        rSingle.Col57 = Convert.ToString(dr0["Col57"]);
                                        rSingle.Col58 = Convert.ToString(dr0["Col58"]);
                                        rSingle.Col59 = Convert.ToString(dr0["Col59"]);
                                        rSingle.Col60 = Convert.ToString(dr0["Col60"]);

                                        rSingle.Col61 = Convert.ToString(dr0["Col61"]);
                                        rSingle.Col62 = Convert.ToString(dr0["Col62"]);
                                        rSingle.Col63 = Convert.ToString(dr0["Col63"]);
                                        rSingle.Col64 = Convert.ToString(dr0["Col64"]);
                                        rSingle.Col65 = Convert.ToString(dr0["Col65"]);
                                        rSingle.Col66 = Convert.ToString(dr0["Col66"]);
                                        rSingle.Col67 = Convert.ToString(dr0["Col67"]);
                                        rSingle.Col68 = Convert.ToString(dr0["Col68"]);







                                        rList.Add(rSingle);

                                    }



                                    var GroupByReference =
                                            from r in rList
                                            //orderby r ascending
                                            group r by new { } into rGroup
                                            select rGroup;

                                    int incRowExcel = 1;
                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        worksheet.Cells[incRowExcel, 1].Value = "Last Update";
                                        worksheet.Cells[incRowExcel, 2].Value = "SA Code";
                                        worksheet.Cells[incRowExcel, 3].Value = "SA Name";
                                        worksheet.Cells[incRowExcel, 4].Value = "SID";
                                        worksheet.Cells[incRowExcel, 5].Value = "Investor Type";
                                        worksheet.Cells[incRowExcel, 6].Value = "Company Name";
                                        worksheet.Cells[incRowExcel, 7].Value = "Country of Domicile";
                                        worksheet.Cells[incRowExcel, 8].Value = "SIUP No.";
                                        worksheet.Cells[incRowExcel, 9].Value = "SIUP Expiration Date";
                                        worksheet.Cells[incRowExcel, 10].Value = "SKD No.";

                                        worksheet.Cells[incRowExcel, 11].Value = "SKD Expiration Date";
                                        worksheet.Cells[incRowExcel, 12].Value = "NPWP No.";
                                        worksheet.Cells[incRowExcel, 13].Value = "NPWP Registration Date";
                                        worksheet.Cells[incRowExcel, 14].Value = "Country of Establishment";
                                        worksheet.Cells[incRowExcel, 15].Value = "Place of Establishment";
                                        worksheet.Cells[incRowExcel, 16].Value = "Date of Establishment";
                                        worksheet.Cells[incRowExcel, 17].Value = "Articles of Association No.";
                                        worksheet.Cells[incRowExcel, 18].Value = "Company Type";
                                        worksheet.Cells[incRowExcel, 19].Value = "Company Characteristic";
                                        worksheet.Cells[incRowExcel, 20].Value = "Income Level (IDR)";

                                        worksheet.Cells[incRowExcel, 21].Value = "Investor's Risk Profile";
                                        worksheet.Cells[incRowExcel, 22].Value = "Investment Objective";
                                        worksheet.Cells[incRowExcel, 23].Value = "Source of Fund";
                                        worksheet.Cells[incRowExcel, 24].Value = "Asset Owner";
                                        worksheet.Cells[incRowExcel, 25].Value = "Company Address";
                                        worksheet.Cells[incRowExcel, 26].Value = "Company City Code";
                                        worksheet.Cells[incRowExcel, 27].Value = "Company City Name";
                                        worksheet.Cells[incRowExcel, 28].Value = "Company Postal Code";
                                        worksheet.Cells[incRowExcel, 29].Value = "Country of Company";
                                        worksheet.Cells[incRowExcel, 30].Value = "Office Phone";

                                        worksheet.Cells[incRowExcel, 31].Value = "Facsimile";
                                        worksheet.Cells[incRowExcel, 32].Value = "Email";
                                        worksheet.Cells[incRowExcel, 33].Value = "Statement Type";
                                        worksheet.Cells[incRowExcel, 34].Value = "Authorized Person 1 - First Name";
                                        worksheet.Cells[incRowExcel, 35].Value = "Authorized Person 1 - Middle Name";
                                        worksheet.Cells[incRowExcel, 36].Value = "Authorized Person 1 - Last Name";
                                        worksheet.Cells[incRowExcel, 37].Value = "Authorized Person 1 - Position";
                                        worksheet.Cells[incRowExcel, 38].Value = "Authorized Person 1 - Mobile Phone";
                                        worksheet.Cells[incRowExcel, 39].Value = "Authorized Person 1 - Email";
                                        worksheet.Cells[incRowExcel, 40].Value = "Authorized Person 1 - NPWP No.";

                                        worksheet.Cells[incRowExcel, 41].Value = "Authorized Person 1 - KTP No.";
                                        worksheet.Cells[incRowExcel, 42].Value = "Authorized Person 1 - KTP Expiration Date";
                                        worksheet.Cells[incRowExcel, 43].Value = "Authorized Person 1 - Passport No.";
                                        worksheet.Cells[incRowExcel, 44].Value = "Authorized Person 1 - Passport Expiration Date";
                                        worksheet.Cells[incRowExcel, 45].Value = "Authorized Person 2 - First Name";
                                        worksheet.Cells[incRowExcel, 46].Value = "Authorized Person 2 - Middle Name";
                                        worksheet.Cells[incRowExcel, 47].Value = "Authorized Person 2 - Last Name";
                                        worksheet.Cells[incRowExcel, 48].Value = "Authorized Person 2 - Position";
                                        worksheet.Cells[incRowExcel, 49].Value = "Authorized Person 2 - Mobile Phone";
                                        worksheet.Cells[incRowExcel, 50].Value = "Authorized Person 2 - Email";

                                        worksheet.Cells[incRowExcel, 51].Value = "Authorized Person 2 - NPWP No.";
                                        worksheet.Cells[incRowExcel, 52].Value = "Authorized Person 2 - KTP No.";
                                        worksheet.Cells[incRowExcel, 53].Value = "Authorized Person 2 - KTP Expiration Date";
                                        worksheet.Cells[incRowExcel, 54].Value = "Authorized Person 2 - Passport No.";
                                        worksheet.Cells[incRowExcel, 55].Value = "Authorized Person 2 - Passport Expiration Date";
                                        worksheet.Cells[incRowExcel, 56].Value = "Asset Information for the Past 3 Years (IDR) - Last Year";
                                        worksheet.Cells[incRowExcel, 57].Value = "Asset Information for the Past 3 Years (IDR) - 2 Years Ago";
                                        worksheet.Cells[incRowExcel, 58].Value = "Asset Information for the Past 3 Years (IDR) - 3 Years Ago";
                                        worksheet.Cells[incRowExcel, 59].Value = "Profit Information for the Past 3 Years (IDR) - Last Year";
                                        worksheet.Cells[incRowExcel, 60].Value = "Profit Information for the Past 3 Years (IDR) - 2 Years Ago";

                                        worksheet.Cells[incRowExcel, 61].Value = "Profit Information for the Past 3 Years (IDR) - 3 Years Ago";
                                        worksheet.Cells[incRowExcel, 62].Value = "FATCA (Status)";
                                        worksheet.Cells[incRowExcel, 63].Value = "TIN / Foreign TIN";
                                        worksheet.Cells[incRowExcel, 64].Value = "TIN / Foreign TIN Issuance Country";
                                        worksheet.Cells[incRowExcel, 65].Value = "GIIN";
                                        worksheet.Cells[incRowExcel, 66].Value = "Substantial U.S. Owner Name";
                                        worksheet.Cells[incRowExcel, 67].Value = "Substantial U.S. Owner Address";
                                        worksheet.Cells[incRowExcel, 68].Value = "Substantial U.S. Owner TIN";



                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 68].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 68].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 68].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 68].Style.VerticalAlignment = ExcelVerticalAlignment.Center;







                                        incRowExcel = incRowExcel + 1;

                                        int first = incRowExcel;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;

                                        foreach (var rsDetail in rsHeader)
                                        {
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 68].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.Col1;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.Col2;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.Col3;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Col4;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Col5;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Col6;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.Col7;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.Col8;
                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.Col9;
                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.Col10;

                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.Col11;
                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.Col12;
                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail.Col13;
                                            worksheet.Cells[incRowExcel, 14].Value = rsDetail.Col14;
                                            worksheet.Cells[incRowExcel, 15].Value = rsDetail.Col15;
                                            worksheet.Cells[incRowExcel, 16].Value = rsDetail.Col16;
                                            worksheet.Cells[incRowExcel, 17].Value = rsDetail.Col17;
                                            worksheet.Cells[incRowExcel, 18].Value = rsDetail.Col18;
                                            worksheet.Cells[incRowExcel, 19].Value = rsDetail.Col19;
                                            worksheet.Cells[incRowExcel, 20].Value = rsDetail.Col20;

                                            worksheet.Cells[incRowExcel, 21].Value = rsDetail.Col21;
                                            worksheet.Cells[incRowExcel, 22].Value = rsDetail.Col22;
                                            worksheet.Cells[incRowExcel, 23].Value = rsDetail.Col23;
                                            worksheet.Cells[incRowExcel, 24].Value = rsDetail.Col24;
                                            worksheet.Cells[incRowExcel, 25].Value = rsDetail.Col25;
                                            worksheet.Cells[incRowExcel, 26].Value = rsDetail.Col26;
                                            worksheet.Cells[incRowExcel, 27].Value = rsDetail.Col27;
                                            worksheet.Cells[incRowExcel, 28].Value = rsDetail.Col28;
                                            worksheet.Cells[incRowExcel, 29].Value = rsDetail.Col29;
                                            worksheet.Cells[incRowExcel, 30].Value = rsDetail.Col30;

                                            worksheet.Cells[incRowExcel, 31].Value = rsDetail.Col31;
                                            worksheet.Cells[incRowExcel, 32].Value = rsDetail.Col32;
                                            worksheet.Cells[incRowExcel, 33].Value = rsDetail.Col33;
                                            worksheet.Cells[incRowExcel, 34].Value = rsDetail.Col34;
                                            worksheet.Cells[incRowExcel, 35].Value = rsDetail.Col35;
                                            worksheet.Cells[incRowExcel, 36].Value = rsDetail.Col36;
                                            worksheet.Cells[incRowExcel, 37].Value = rsDetail.Col37;
                                            worksheet.Cells[incRowExcel, 38].Value = rsDetail.Col38;
                                            worksheet.Cells[incRowExcel, 39].Value = rsDetail.Col39;
                                            worksheet.Cells[incRowExcel, 40].Value = rsDetail.Col40;

                                            worksheet.Cells[incRowExcel, 41].Value = rsDetail.Col41;
                                            worksheet.Cells[incRowExcel, 42].Value = rsDetail.Col42;
                                            worksheet.Cells[incRowExcel, 43].Value = rsDetail.Col43;
                                            worksheet.Cells[incRowExcel, 44].Value = rsDetail.Col44;
                                            worksheet.Cells[incRowExcel, 45].Value = rsDetail.Col45;
                                            worksheet.Cells[incRowExcel, 46].Value = rsDetail.Col46;
                                            worksheet.Cells[incRowExcel, 47].Value = rsDetail.Col47;
                                            worksheet.Cells[incRowExcel, 48].Value = rsDetail.Col48;
                                            worksheet.Cells[incRowExcel, 49].Value = rsDetail.Col49;
                                            worksheet.Cells[incRowExcel, 50].Value = rsDetail.Col50;

                                            worksheet.Cells[incRowExcel, 51].Value = rsDetail.Col51;
                                            worksheet.Cells[incRowExcel, 52].Value = rsDetail.Col52;
                                            worksheet.Cells[incRowExcel, 53].Value = rsDetail.Col53;
                                            worksheet.Cells[incRowExcel, 54].Value = rsDetail.Col54;
                                            worksheet.Cells[incRowExcel, 55].Value = rsDetail.Col55;
                                            worksheet.Cells[incRowExcel, 56].Value = rsDetail.Col56;
                                            worksheet.Cells[incRowExcel, 57].Value = rsDetail.Col57;
                                            worksheet.Cells[incRowExcel, 58].Value = rsDetail.Col58;
                                            worksheet.Cells[incRowExcel, 59].Value = rsDetail.Col59;
                                            worksheet.Cells[incRowExcel, 60].Value = rsDetail.Col60;

                                            worksheet.Cells[incRowExcel, 61].Value = rsDetail.Col61;
                                            worksheet.Cells[incRowExcel, 62].Value = rsDetail.Col62;
                                            worksheet.Cells[incRowExcel, 63].Value = rsDetail.Col63;
                                            worksheet.Cells[incRowExcel, 64].Value = rsDetail.Col64;
                                            worksheet.Cells[incRowExcel, 65].Value = rsDetail.Col65;
                                            worksheet.Cells[incRowExcel, 66].Value = rsDetail.Col66;
                                            worksheet.Cells[incRowExcel, 67].Value = rsDetail.Col67;
                                            worksheet.Cells[incRowExcel, 68].Value = rsDetail.Col68;

                                            incRowExcel++;






                                        }


                                        _endRowDetail = incRowExcel + 3;

                                    }


                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 68];

                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.Column(10).AutoFit();

                                    worksheet.Column(11).AutoFit();
                                    worksheet.Column(12).AutoFit();
                                    worksheet.Column(13).AutoFit();
                                    worksheet.Column(14).AutoFit();
                                    worksheet.Column(15).AutoFit();
                                    worksheet.Column(16).AutoFit();
                                    worksheet.Column(17).AutoFit();
                                    worksheet.Column(18).AutoFit();
                                    worksheet.Column(19).AutoFit();
                                    worksheet.Column(20).AutoFit();

                                    worksheet.Column(21).AutoFit();
                                    worksheet.Column(22).AutoFit();
                                    worksheet.Column(23).AutoFit();
                                    worksheet.Column(24).AutoFit();
                                    worksheet.Column(25).AutoFit();
                                    worksheet.Column(26).AutoFit();
                                    worksheet.Column(27).AutoFit();
                                    worksheet.Column(28).AutoFit();
                                    worksheet.Column(29).AutoFit();
                                    worksheet.Column(30).AutoFit();

                                    worksheet.Column(31).AutoFit();
                                    worksheet.Column(32).AutoFit();
                                    worksheet.Column(33).AutoFit();
                                    worksheet.Column(34).AutoFit();
                                    worksheet.Column(35).AutoFit();
                                    worksheet.Column(36).AutoFit();
                                    worksheet.Column(37).AutoFit();
                                    worksheet.Column(38).AutoFit();
                                    worksheet.Column(39).AutoFit();
                                    worksheet.Column(40).AutoFit();

                                    worksheet.Column(41).AutoFit();
                                    worksheet.Column(42).AutoFit();
                                    worksheet.Column(43).AutoFit();
                                    worksheet.Column(44).AutoFit();
                                    worksheet.Column(45).AutoFit();
                                    worksheet.Column(46).AutoFit();
                                    worksheet.Column(47).AutoFit();
                                    worksheet.Column(48).AutoFit();
                                    worksheet.Column(49).AutoFit();
                                    worksheet.Column(50).AutoFit();

                                    worksheet.Column(51).AutoFit();
                                    worksheet.Column(52).AutoFit();
                                    worksheet.Column(53).AutoFit();
                                    worksheet.Column(54).AutoFit();
                                    worksheet.Column(55).AutoFit();
                                    worksheet.Column(56).AutoFit();
                                    worksheet.Column(57).AutoFit();
                                    worksheet.Column(58).AutoFit();
                                    worksheet.Column(59).AutoFit();
                                    worksheet.Column(60).AutoFit();

                                    worksheet.Column(61).AutoFit();
                                    worksheet.Column(62).AutoFit();
                                    worksheet.Column(63).AutoFit();
                                    worksheet.Column(64).AutoFit();
                                    worksheet.Column(65).AutoFit();
                                    worksheet.Column(66).AutoFit();
                                    worksheet.Column(67).AutoFit();
                                    worksheet.Column(68).AutoFit();



                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&12 KYC Aperd Ins";


                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();


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



    }


}