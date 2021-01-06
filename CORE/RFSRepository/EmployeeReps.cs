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
    public class EmployeeReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = @"INSERT INTO [dbo].[Employee]
                           ([EmployeePK],[HistoryPK],[Status],[Name],[SKNumber],[SKDate],[SKFormat],[Position],[DateOfWork],[BranchCode],[ChiefCode],[SubRedempCode],
                           [JenisKelamin],[TanggalLahir],[NIK],[NPWP],[AlamatSesuaiKTP],[AlamatDomisili],[NoBPJSKetenagakerjaan],[NoBPJSKesehatan],[NoRekTabungan],[StatusPerkawinan],[JumlahAnak],[SKExpiredDate],
                           [NamaLisence],[ExpiredDateLisence],[StatusLisence],[BitReportToOJK],";

        string _paramaterCommand = @"@Name,@SKNumber,@SKDate,@SKFormat,@Position,@DateOfWork,@BranchCode,@ChiefCode,@SubRedempCode,
            @JenisKelamin,@TanggalLahir,@NIK,@NPWP,@AlamatSesuaiKTP,@AlamatDomisili,@NoBPJSKetenagakerjaan,@NoBPJSKesehatan,@NoRekTabungan,@StatusPerkawinan,@JumlahAnak,@SKExpiredDate,
            @NamaLisence,@ExpiredDateLisence,@StatusLisence,@BitReportToOJK,";

        //2
        private Employee setEmployee(SqlDataReader dr)
        {
            Employee M_Employee = new Employee();
            M_Employee.EmployeePK = Convert.ToInt32(dr["EmployeePK"]);
            M_Employee.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Employee.Status = Convert.ToInt32(dr["Status"]);
            M_Employee.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Employee.Notes = Convert.ToString(dr["Notes"]);
            M_Employee.Name = dr["Name"].ToString();
            M_Employee.SKNumber = dr["SKNumber"].ToString();
            M_Employee.SKDate = dr["SKDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SKDate"]);
            M_Employee.SKExpiredDate = dr["SKExpiredDate"].ToString();
            M_Employee.SKFormat = Convert.ToInt32(dr["SKFormat"]);
            M_Employee.SKFormatDesc = Convert.ToString(dr["SKFormatDesc"]);
            M_Employee.Position = Convert.ToInt32(dr["Position"]);
            M_Employee.PositionDesc = Convert.ToString(dr["PositionDesc"]);
            M_Employee.DateOfWork = dr["DateOfWork"].ToString();
            M_Employee.BranchCode = Convert.ToString(dr["BranchCode"]);
            //M_Employee.BranchCodeDesc = Convert.ToString(dr["BranchCodeDesc"]);
            M_Employee.ChiefCode = Convert.ToInt32(dr["ChiefCode"]);
            M_Employee.ChiefCodeDesc = Convert.ToString(dr["ChiefCodeDesc"]);
            M_Employee.SubRedempCode = dr["SubRedempCode"].ToString();
            M_Employee.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Employee.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Employee.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Employee.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Employee.EntryTime = dr["EntryTime"].ToString();
            M_Employee.UpdateTime = dr["UpdateTime"].ToString();
            M_Employee.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Employee.VoidTime = dr["VoidTime"].ToString();
            M_Employee.DBUserID = dr["DBUserID"].ToString();
            M_Employee.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Employee.LastUpdate = dr["LastUpdate"].ToString();
            M_Employee.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            M_Employee.JenisKelamin = dr["JenisKelamin"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["JenisKelamin"]);
            M_Employee.JenisKelaminDesc = dr["JenisKelaminDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["JenisKelaminDesc"]);
            M_Employee.TanggalLahir = dr["TanggalLahir"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TanggalLahir"]);
            M_Employee.NIK = dr["NIK"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NIK"]);
            M_Employee.NPWP = dr["NPWP"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NPWP"]);
            M_Employee.AlamatSesuaiKTP = dr["AlamatSesuaiKTP"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AlamatSesuaiKTP"]);
            M_Employee.AlamatDomisili = dr["AlamatDomisili"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AlamatDomisili"]);
            M_Employee.NoBPJSKetenagakerjaan = dr["NoBPJSKetenagakerjaan"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NoBPJSKetenagakerjaan"]);
            M_Employee.NoBPJSKesehatan = dr["NoBPJSKesehatan"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NoBPJSKesehatan"]);
            M_Employee.NoRekTabungan = dr["NoRekTabungan"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NoRekTabungan"]);
            M_Employee.StatusPerkawinan = dr["StatusPerkawinan"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["StatusPerkawinan"]);
            M_Employee.StatusPerkawinanDesc = dr["StatusPerkawinanDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["StatusPerkawinanDesc"]);
            M_Employee.JumlahAnak = dr["JumlahAnak"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["JumlahAnak"]);

            M_Employee.NamaLisence = dr["NamaLisence"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NamaLisence"]);
            M_Employee.ExpiredDateLisence = dr["ExpiredDateLisence"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExpiredDateLisence"]);
            M_Employee.StatusLisence = dr["StatusLisence"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["StatusLisence"]);
            M_Employee.StatusLisenceDesc = dr["StatusLisenceDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["StatusLisenceDesc"]);
            M_Employee.BitReportToOJK = dr["BitReportToOJK"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitReportToOJK"]);

            return M_Employee;
        }


        public List<Employee> Employee_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Employee> L_Employee = new List<Employee>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when E.status=1 then 'PENDING' else Case When E.status = 2 then 'APPROVED' else Case when E.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, 
                                CASE WHEN E.JenisKelamin = 1 THEN 'Laki-Laki' WHEN E.JenisKelamin = 2 THEN 'Perempuan' End as JenisKelaminDesc, 
                                CASE WHEN E.StatusPerkawinan = 1 THEN 'Kawin' WHEN E.StatusPerkawinan=2 THEN 'Belum Kawin' ELSE 'Lainnya' End As StatusPerkawinanDesc,  
                                CASE WHEN E.StatusLisence = 1 THEN 'Active' ELSE 'Expired' End As StatusLisenceDesc,
                                MV1.DescOne ChiefCodeDesc,MV2.DescOne SKFormatDesc,MV3.DescOne PositionDesc,convert(varchar,E.ExpiredDateLisence, 101) ExpiredDateLisence,E.* from Employee E left join  
                                MasterValue MV1 on E.ChiefCode = MV1.Code and MV1.ID ='CEOSigning' left join  
                                MasterValue MV2 on E.SKFormat = MV2.Code and MV2.ID ='FormatSK' left join  
                                MasterValue MV3 on E.Position = MV3.Code and MV3.ID ='Position'   
                                where E.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when E.status=1 then 'PENDING' else Case When E.status = 2 then 'APPROVED' else Case when E.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, 
                                CASE WHEN E.JenisKelamin = 1 THEN 'Laki-Laki' WHEN E.JenisKelamin = 2 THEN 'Perempuan' End as JenisKelaminDesc, 
                                CASE WHEN E.StatusPerkawinan = 1 THEN 'Kawin' WHEN E.StatusPerkawinan =2 THEN 'Belum Kawin' ELSE 'Lainnya' End As StatusPerkawinanDesc,
                                CASE WHEN E.StatusLisence = 1 THEN 'Active' ELSE 'Expired' End As StatusLisenceDesc,
                                MV1.DescOne ChiefCodeDesc,MV2.DescOne SKFormatDesc,MV3.DescOne PositionDesc,E.* from Employee E left join  
                                MasterValue MV1 on E.ChiefCode = MV1.Code and MV1.ID ='CEOSigning' left join  
                                MasterValue MV2 on E.SKFormat = MV2.Code and MV2.ID ='FormatSK' left join  
                                MasterValue MV3 on E.Position = MV3.Code and MV3.ID ='Position'   ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Employee.Add(setEmployee(dr));
                                }
                            }
                            return L_Employee;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int Employee_Add(Employee _employee, bool _havePrivillege)
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
                                 "Select isnull(max(EmployeePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from Employee";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _employee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(EmployeePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from Employee";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Name", _employee.Name);
                        cmd.Parameters.AddWithValue("@SKNumber", _employee.SKNumber);
                        cmd.Parameters.AddWithValue("@SKDate", _employee.SKDate);
                        cmd.Parameters.AddWithValue("@SKExpiredDate", _employee.SKExpiredDate);
                        cmd.Parameters.AddWithValue("@SKFormat", _employee.SKFormat);
                        cmd.Parameters.AddWithValue("@Position", _employee.Position);
                        cmd.Parameters.AddWithValue("@DateOfWork", _employee.DateOfWork);
                        cmd.Parameters.AddWithValue("@BranchCode", _employee.BranchCode);
                        cmd.Parameters.AddWithValue("@ChiefCode", _employee.ChiefCode);
                        cmd.Parameters.AddWithValue("@SubRedempCode", _employee.SubRedempCode);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _employee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@JenisKelamin", _employee.JenisKelamin);
                        cmd.Parameters.AddWithValue("@TanggalLahir", _employee.TanggalLahir);
                        cmd.Parameters.AddWithValue("@NIK", _employee.NIK);
                        cmd.Parameters.AddWithValue("@NPWP", _employee.NPWP);
                        cmd.Parameters.AddWithValue("@AlamatSesuaiKTP", _employee.AlamatSesuaiKTP);
                        cmd.Parameters.AddWithValue("@AlamatDomisili", _employee.AlamatDomisili);
                        cmd.Parameters.AddWithValue("@NoBPJSKetenagakerjaan", _employee.NoBPJSKetenagakerjaan);
                        cmd.Parameters.AddWithValue("@NoBPJSKesehatan", _employee.NoBPJSKesehatan);
                        cmd.Parameters.AddWithValue("@NoRekTabungan", _employee.NoRekTabungan);
                        cmd.Parameters.AddWithValue("@StatusPerkawinan", _employee.StatusPerkawinan);
                        cmd.Parameters.AddWithValue("@JumlahAnak", _employee.JumlahAnak);
                        cmd.Parameters.AddWithValue("@NamaLisence", _employee.NamaLisence);
                        cmd.Parameters.AddWithValue("@ExpiredDateLisence", _employee.ExpiredDateLisence);
                        cmd.Parameters.AddWithValue("@StatusLisence", _employee.StatusLisence);
                        cmd.Parameters.AddWithValue("@BitReportToOJK", _employee.BitReportToOJK);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Employee");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int Employee_Update(Employee _employee, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_employee.EmployeePK, _employee.HistoryPK, "employee");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update Employee set status=2,Notes=@Notes,Name=@Name,SKNumber=@SKNumber,SKDate=@SKDate, 
                                                SKFormat=@SKFormat,Position=@Position,DateOfWork=@DateOfWork,BranchCode=@BranchCode,ChiefCode=@ChiefCode,SubRedempCode=@SubRedempCode,
                                                JenisKelamin=@JenisKelamin,TanggalLahir=@TanggalLahir,NIK=@NIK,NPWP=@NPWP,AlamatSesuaiKTP=@AlamatSesuaiKTP,AlamatDomisili=@AlamatDomisili,NoBPJSKetenagakerjaan=@NoBPJSKetenagakerjaan, 
                                                NoBPJSKesehatan=@NoBPJSKesehatan,NoRekTabungan=@NoRekTabungan,StatusPerkawinan=@StatusPerkawinan,JumlahAnak=@JumlahAnak,NamaLisence=@NamaLisence,ExpiredDateLisence=@ExpiredDateLisence,
                                                StatusLisence=@StatusLisence,BitReportToOJK=@BitReportToOJK,
                                                ApprovedUsersID=@ApprovedUsersID,  
                                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate  
                                                where EmployeePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _employee.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                            cmd.Parameters.AddWithValue("@Notes", _employee.Notes);
                            cmd.Parameters.AddWithValue("@Name", _employee.Name);
                            cmd.Parameters.AddWithValue("@SKNumber", _employee.SKNumber);
                            cmd.Parameters.AddWithValue("@SKDate", _employee.SKDate);
                            cmd.Parameters.AddWithValue("@SKExpiredDate", _employee.SKExpiredDate);
                            cmd.Parameters.AddWithValue("@SKFormat", _employee.SKFormat);
                            cmd.Parameters.AddWithValue("@Position", _employee.Position);
                            cmd.Parameters.AddWithValue("@DateOfWork", _employee.DateOfWork);
                            cmd.Parameters.AddWithValue("@BranchCode", _employee.BranchCode);
                            cmd.Parameters.AddWithValue("@ChiefCode", _employee.ChiefCode);
                            cmd.Parameters.AddWithValue("@SubRedempCode", _employee.SubRedempCode);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _employee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _employee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.Parameters.AddWithValue("@JenisKelamin", _employee.JenisKelamin);
                            cmd.Parameters.AddWithValue("@TanggalLahir", _employee.TanggalLahir);
                            cmd.Parameters.AddWithValue("@NIK", _employee.NIK);
                            cmd.Parameters.AddWithValue("@NPWP", _employee.NPWP);
                            cmd.Parameters.AddWithValue("@AlamatSesuaiKTP", _employee.AlamatSesuaiKTP);
                            cmd.Parameters.AddWithValue("@AlamatDomisili", _employee.AlamatDomisili);
                            cmd.Parameters.AddWithValue("@NoBPJSKetenagakerjaan", _employee.NoBPJSKetenagakerjaan);
                            cmd.Parameters.AddWithValue("@NoBPJSKesehatan", _employee.NoBPJSKesehatan);
                            cmd.Parameters.AddWithValue("@NoRekTabungan", _employee.NoRekTabungan);
                            cmd.Parameters.AddWithValue("@StatusPerkawinan", _employee.StatusPerkawinan);
                            cmd.Parameters.AddWithValue("@JumlahAnak", _employee.JumlahAnak);
                            cmd.Parameters.AddWithValue("@NamaLisence", _employee.NamaLisence);
                            cmd.Parameters.AddWithValue("@ExpiredDateLisence", _employee.ExpiredDateLisence);
                            cmd.Parameters.AddWithValue("@StatusLisence", _employee.StatusLisence);
                            cmd.Parameters.AddWithValue("@BitReportToOJK", _employee.BitReportToOJK);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Employee set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where EmployeePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _employee.EntryUsersID);
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
                                cmd.CommandText = @"Update Employee set Notes=@Notes,Name=@Name,SKNumber=@SKNumber,SKDate=@SKDate,  
                                                    SKFormat=@SKFormat,Position=@Position,DateOfWork=@DateOfWork,BranchCode=@BranchCode,ChiefCode=@ChiefCode,SubRedempCode=@SubRedempCode, 
                                                    JenisKelamin=@JenisKelamin,TanggalLahir=@TanggalLahir,NIK=@NIK,NPWP=@NPWP,AlamatSesuaiKTP=@AlamatSesuaiKTP,AlamatDomisili=@AlamatDomisili,NoBPJSKetenagakerjaan=@NoBPJSKetenagakerjaan, 
                                                    NoBPJSKesehatan=@NoBPJSKesehatan,NoRekTabungan=@NoRekTabungan,StatusPerkawinan=@StatusPerkawinan,JumlahAnak=@JumlahAnak,NamaLisence=@NamaLisence,ExpiredDateLisence=@ExpiredDateLisence,
                                                StatusLisence=@StatusLisence,BitReportToOJK=@BitReportToOJK,
                                                    UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,lastupdate=@lastupdate  
                                                    where EmployeePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _employee.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                                cmd.Parameters.AddWithValue("@Notes", _employee.Notes);
                                cmd.Parameters.AddWithValue("@Name", _employee.Name);
                                cmd.Parameters.AddWithValue("@SKNumber", _employee.SKNumber);
                                cmd.Parameters.AddWithValue("@SKDate", _employee.SKDate);
                                cmd.Parameters.AddWithValue("@SKExpiredDate", _employee.SKExpiredDate);
                                cmd.Parameters.AddWithValue("@SKFormat", _employee.SKFormat);
                                cmd.Parameters.AddWithValue("@Position", _employee.Position);
                                cmd.Parameters.AddWithValue("@DateOfWork", _employee.DateOfWork);
                                cmd.Parameters.AddWithValue("@BranchCode", _employee.BranchCode);
                                cmd.Parameters.AddWithValue("@ChiefCode", _employee.ChiefCode);
                                cmd.Parameters.AddWithValue("@SubRedempCode", _employee.SubRedempCode);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _employee.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.Parameters.AddWithValue("@JenisKelamin", _employee.JenisKelamin);
                                cmd.Parameters.AddWithValue("@TanggalLahir", _employee.TanggalLahir);
                                cmd.Parameters.AddWithValue("@NIK", _employee.NIK);
                                cmd.Parameters.AddWithValue("@NPWP", _employee.NPWP);
                                cmd.Parameters.AddWithValue("@AlamatSesuaiKTP", _employee.AlamatSesuaiKTP);
                                cmd.Parameters.AddWithValue("@AlamatDomisili", _employee.AlamatDomisili);
                                cmd.Parameters.AddWithValue("@NoBPJSKetenagakerjaan", _employee.NoBPJSKetenagakerjaan);
                                cmd.Parameters.AddWithValue("@NoBPJSKesehatan", _employee.NoBPJSKesehatan);
                                cmd.Parameters.AddWithValue("@NoRekTabungan", _employee.NoRekTabungan);
                                cmd.Parameters.AddWithValue("@StatusPerkawinan", _employee.StatusPerkawinan);
                                cmd.Parameters.AddWithValue("@JumlahAnak", _employee.JumlahAnak);
                                cmd.Parameters.AddWithValue("@NamaLisence", _employee.NamaLisence);
                                cmd.Parameters.AddWithValue("@ExpiredDateLisence", _employee.ExpiredDateLisence);
                                cmd.Parameters.AddWithValue("@StatusLisence", _employee.StatusLisence);
                                cmd.Parameters.AddWithValue("@BitReportToOJK", _employee.BitReportToOJK);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_employee.EmployeePK, "Employee");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Employee where EmployeePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _employee.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _host.Get_NewHistoryPK(_employee.EmployeePK, "Employee"));
                                cmd.Parameters.AddWithValue("@Name", _employee.Name);
                                cmd.Parameters.AddWithValue("@SKNumber", _employee.SKNumber);
                                cmd.Parameters.AddWithValue("@SKDate", _employee.SKDate);
                                cmd.Parameters.AddWithValue("@SKExpiredDate", _employee.SKExpiredDate);
                                cmd.Parameters.AddWithValue("@SKFormat", _employee.SKFormat);
                                cmd.Parameters.AddWithValue("@Position", _employee.Position);
                                cmd.Parameters.AddWithValue("@DateOfWork", _employee.DateOfWork);
                                cmd.Parameters.AddWithValue("@BranchCode", _employee.BranchCode);
                                cmd.Parameters.AddWithValue("@ChiefCode", _employee.ChiefCode);
                                cmd.Parameters.AddWithValue("@SubRedempCode", _employee.SubRedempCode);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _employee.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.Parameters.AddWithValue("@JenisKelamin", _employee.JenisKelamin);
                                cmd.Parameters.AddWithValue("@TanggalLahir", _employee.TanggalLahir);
                                cmd.Parameters.AddWithValue("@NIK", _employee.NIK);
                                cmd.Parameters.AddWithValue("@NPWP", _employee.NPWP);
                                cmd.Parameters.AddWithValue("@AlamatSesuaiKTP", _employee.AlamatSesuaiKTP);
                                cmd.Parameters.AddWithValue("@AlamatDomisili", _employee.AlamatDomisili);
                                cmd.Parameters.AddWithValue("@NoBPJSKetenagakerjaan", _employee.NoBPJSKetenagakerjaan);
                                cmd.Parameters.AddWithValue("@NoBPJSKesehatan", _employee.NoBPJSKesehatan);
                                cmd.Parameters.AddWithValue("@NoRekTabungan", _employee.NoRekTabungan);
                                cmd.Parameters.AddWithValue("@StatusPerkawinan", _employee.StatusPerkawinan);
                                cmd.Parameters.AddWithValue("@JumlahAnak", _employee.JumlahAnak);
                                cmd.Parameters.AddWithValue("@NamaLisence", _employee.NamaLisence);
                                cmd.Parameters.AddWithValue("@ExpiredDateLisence", _employee.ExpiredDateLisence);
                                cmd.Parameters.AddWithValue("@StatusLisence", _employee.StatusLisence);
                                cmd.Parameters.AddWithValue("@BitReportToOJK", _employee.BitReportToOJK);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Employee set status= 4,Notes=@Notes ," +
                                    "lastupdate=@lastupdate where EmployeePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _employee.Notes);
                                cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _employee.HistoryPK);
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


        public void Employee_Approved(Employee _employee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Employee set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where EmployeePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _employee.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _employee.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Employee set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where EmployeePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _employee.ApprovedUsersID);
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

        public void Employee_Reject(Employee _employee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Employee set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,lastupdate=@lastupdate " +
                            "where EmployeePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _employee.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _employee.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Employee set status= 2,lastupdate=@lastupdate where EmployeePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
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

        public void Employee_Void(Employee _employee)
        {

            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Employee set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where EmployeePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _employee.EmployeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _employee.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _employee.VoidUsersID);
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

        //AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public string Employee_GenerateEmployeeText(Employee _employee)
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
                        if (Tools.ClientCode == "03")
                        {
                            cmd.CommandText = @"
                         BEGIN  
                                 SET NOCOUNT ON       

                                 create table #Text(             
                                 [ResultText] [nvarchar](1000)  NULL             
                                 )                  
 

                                insert into #Text                  
                                select RTRIM(LTRIM(isnull(Name,''))) + '|' + substring(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when SKNumber = '' then '9999' else  isnull(SKNumber,'0') end))),4,4)         
                                + '|' + RTRIM(LTRIM(case when dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR,SKDate,112) ,'0')) = '19000101' then '20000101' else  dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR,SKDate,112) ,'0')) end))        
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SKFormat,'0')))) + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Position,'0'))))                  
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR,DateOfWork,112),'0')))) + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when isnull(BranchCode,'000') = '' then '000' when isnull(BranchCode,'000') = 0 then '000' else isnull(BranchCode,'000') end))) + '|'     
                                + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(ChiefCode,'0'))))+ '|'+ RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SubRedempCode,'0'))))                
                                from Employee  where status = 2 and BitReportToOJK = 1     
                             
                                 select ResultText from #text                              
                      
                                 END  ";
                        }
                        else
                        {
                            cmd.CommandText = @"
                         BEGIN  
                                 SET NOCOUNT ON       

                                 create table #Text(             
                                 [ResultText] [nvarchar](1000)  NULL             
                                 )                  
 

                                insert into #Text                  
                                select RTRIM(LTRIM(isnull(Name,''))) + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when SKNumber = '' then '9999' else  isnull(SKNumber,'0') end)))
                                + '|' + RTRIM(LTRIM(case when dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR,SKDate,112) ,'0')) = '19000101' then '20000101' else  dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR,SKDate,112) ,'0')) end))        
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SKFormat,'0')))) + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Position,'0'))))                  
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR,DateOfWork,112),'0')))) + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when isnull(BranchCode,'000') = '' then '000' when isnull(BranchCode,'000') = 0 then '000' else isnull(BranchCode,'000') end))) + '|'     
                                + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(ChiefCode,'0'))))+ '|'+ RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SubRedempCode,'0'))))                
                                from Employee  where status = 2      
                             
                                 select ResultText from #text                              
                      
                                 END  ";
                        }




                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.ARIATextPath + "Employee.txt";
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
                                    return Tools.HtmlARIATextPath + "Employee.txt";
                                }

                            }
                            return null;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string Employee_CheckSKExpiredDate()
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        declare @Bodymessage nvarchar(max)
                        set @Bodymessage = ''

                        select @Bodymessage = @Bodymessage + cast(ROW_NUMBER() over (order by EmployeePK) as nvarchar) + '. ' + Name + ' (' + Convert(nvarchar,SKExpiredDate,103) + ') <br /> ' from Employee
                        where status in (1,2) and SKExpiredDate <> '1900-01-01'
                        and DATEDIFF(month,SKExpiredDate,getdate()) <= 3

                        select @Bodymessage Result
                        ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();
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


        public Boolean GenerateEmployeeReport(string _userID, EmployeeReport _EmployeeReport)
        {

            #region ExportJournal

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {




                        cmd.CommandText = @"
                    select Name Nama, NamaLisence, convert(varchar,ExpiredDateLisence, 101) ExpiredDateLisence From Employee where status = 2
";

                        cmd.CommandTimeout = 0;
                        //cmd.Parameters.AddWithValue("@Datefrom", _JournalExport.ValueDateFrom);
                        //cmd.Parameters.AddWithValue("@Dateto", _JournalExport.ValueDateTo);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "EmployeeReport" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "EmployeeReport" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "EmployeeReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee Report");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<EmployeeReport> rList = new List<EmployeeReport>();
                                    while (dr0.Read())
                                    {

                                        EmployeeReport rSingle = new EmployeeReport();
                                        rSingle.Nama = dr0["Nama"].ToString();
                                        rSingle.NamaLisence = dr0["NamaLisence"].ToString();
                                        rSingle.ExpiredDateLisence = dr0["ExpiredDateLisence"].ToString();







                                        rList.Add(rSingle);

                                    }


                                    var GroupByReference =
                                                from r in rList
                                                    //orderby r ascending
                                                group r by new { } into rGroup
                                                select rGroup;

                                    int incRowExcel = 1;

                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                    worksheet.Cells[incRowExcel, 1].Value = "Employee Report";


                                    incRowExcel = incRowExcel + 2;

                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                    worksheet.Cells[incRowExcel, 1].Value = "Nama";
                                    worksheet.Cells[incRowExcel, 2].Value = "Nama Lisence";
                                    worksheet.Cells[incRowExcel, 3].Value = "Expired Date";

                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 12;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




                                    foreach (var rsHeader in GroupByReference)
                                    {



                                        int first = incRowExcel;

                                        int no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;

                                        foreach (var rsDetail in rsHeader)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.Nama;

                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaLisence;

                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.ExpiredDateLisence;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MM/yyyy";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;



                                            no++;
                                            _endRowDetail = incRowExcel;


                                        }

                                        worksheet.Row(incRowExcel).PageBreak = true;



                                    }



                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 3];
                                    worksheet.Column(1).Width = 21;
                                    worksheet.Column(2).Width = 21;
                                    worksheet.Column(3).Width = 21;




                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Employee Report";


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
            #endregion


        }



    }
}