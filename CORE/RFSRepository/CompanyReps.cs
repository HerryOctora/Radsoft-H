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
    public class CompanyReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Company] " +
                            "([CompanyPK],[HistoryPK],[Status],[ID],[Name],[Address],[NPWP],[Phone],[Fax],[DirectorOne],[DirectorTwo],[DirectorThree],[PPEMinimalMKBD],[MIMinimalMKBD],[DefaultCurrencyPK],[MKBDCode],[NoIDPJK],";
        string _paramaterCommand = "@ID,@Name,@Address,@NPWP,@Phone,@Fax,@DirectorOne,@DirectorTwo,@DirectorThree,@PPEMinimalMKBD,@MIMinimalMKBD,@DefaultCurrencyPK,@MKBDCode,@NoIDPJK,";

        //2
        private Company setCompany(SqlDataReader dr)
        {
            Company M_Company = new Company();
            M_Company.CompanyPK = Convert.ToInt32(dr["CompanyPK"]);
            M_Company.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Company.Status = Convert.ToInt32(dr["Status"]);
            M_Company.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Company.Notes = Convert.ToString(dr["Notes"]);
            M_Company.ID = Convert.ToString(dr["ID"]);
            M_Company.Name = Convert.ToString(dr["Name"]);
            M_Company.Address = Convert.ToString(dr["Address"]);
            M_Company.NPWP = Convert.ToString(dr["NPWP"]);
            M_Company.Phone = Convert.ToString(dr["Phone"]);
            M_Company.Fax = Convert.ToString(dr["Fax"]);
            M_Company.DirectorOne = Convert.ToString(dr["DirectorOne"]);
            M_Company.DirectorTwo = Convert.ToString(dr["DirectorTwo"]);
            M_Company.DirectorThree = Convert.ToString(dr["DirectorThree"]);
            M_Company.PPEMinimalMKBD = Convert.ToDecimal(dr["PPEMinimalMKBD"]);
            M_Company.MIMinimalMKBD =  Convert.ToDecimal(dr["MIMinimalMKBD"]);
            M_Company.DefaultCurrencyPK =  Convert.ToInt32(dr["DefaultCurrencyPK"]);
            M_Company.DefaultCurrencyID = Convert.ToString(dr["DefaultCurrencyID"]);
            M_Company.MKBDCode = Convert.ToString(dr["MKBDCode"]);
            M_Company.NoIDPJK = Convert.ToString(dr["NoIDPJK"]);
            M_Company.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Company.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Company.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Company.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Company.EntryTime = dr["EntryTime"].ToString();
            M_Company.UpdateTime = dr["UpdateTime"].ToString();
            M_Company.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Company.VoidTime = dr["VoidTime"].ToString();
            M_Company.DBUserID = dr["DBUserID"].ToString();
            M_Company.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Company.LastUpdate = dr["LastUpdate"].ToString();
            M_Company.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Company;
        }

        public List<Company> Company_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Company> L_Company = new List<Company>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when C.status=1 then 'PENDING' else Case When C.status = 2 then 'APPROVED' else Case when C.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID DefaultCurrencyID,* From Company C left join " +
                            " Currency CR on C.DefaultCurrencyPK = CR.CurrencyPK and CR.Status = 2 " +
                            " where C.status = @status order by CompanyPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when C.status=1 then 'PENDING' else Case When C.status = 2 then 'APPROVED' else Case when C.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID DefaultCurrencyID,* From Company C left join " +
                            " Currency CR on C.DefaultCurrencyPK = CR.CurrencyPK and CR.Status = 2  order by CompanyPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Company.Add(setCompany(dr));
                                }
                            }
                            return L_Company;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public int Company_Add(Company _company, bool _havePrivillege)
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
                                 "Select isnull(max(CompanyPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from Company";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _company.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(CompanyPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from Company";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _company.ID);
                        cmd.Parameters.AddWithValue("@Name", _company.Name);
                        cmd.Parameters.AddWithValue("@Address", _company.Address);
                        cmd.Parameters.AddWithValue("@NPWP", _company.NPWP);
                        cmd.Parameters.AddWithValue("@Phone", _company.Phone);
                        cmd.Parameters.AddWithValue("@Fax", _company.Fax);
                        cmd.Parameters.AddWithValue("@DirectorOne", _company.DirectorOne);
                        cmd.Parameters.AddWithValue("@DirectorTwo", _company.DirectorTwo);
                        cmd.Parameters.AddWithValue("@DirectorThree", _company.DirectorThree);
                        cmd.Parameters.AddWithValue("@PPEMinimalMKBD", _company.PPEMinimalMKBD);
                        cmd.Parameters.AddWithValue("@MIMinimalMKBD", _company.MIMinimalMKBD);
                        cmd.Parameters.AddWithValue("@DefaultCurrencyPK", _company.DefaultCurrencyPK);
                        cmd.Parameters.AddWithValue("@MKBDCode", _company.MKBDCode);
                        cmd.Parameters.AddWithValue("@NoIDPJK", _company.NoIDPJK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _company.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Company");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Company_Update(Company _company, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_company.CompanyPK, _company.HistoryPK, "company");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Company set status = 2, Notes=@Notes,ID=@ID,Name=@Name,Address=@Address,NPWP=@NPWP," +
                                "Phone=@Phone,Fax=@Fax,DirectorOne=@DirectorOne,DirectorTwo=@DirectorTwo,DirectorThree=@DirectorThree,PPEMinimalMKBD=@PPEMinimalMKBD,MIMinimalMKBD=@MIMinimalMKBD,DefaultCurrencyPK=@DefaultCurrencyPK,MKBDCode=@MKBDCode,NoIDPJK=@NoIDPJK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CompanyPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _company.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                            cmd.Parameters.AddWithValue("@Notes", _company.Notes);
                            cmd.Parameters.AddWithValue("@ID", _company.ID);
                            cmd.Parameters.AddWithValue("@Name", _company.Name);
                            cmd.Parameters.AddWithValue("@Address", _company.Address);
                            cmd.Parameters.AddWithValue("@NPWP", _company.NPWP);
                            cmd.Parameters.AddWithValue("@Phone", _company.Phone);
                            cmd.Parameters.AddWithValue("@Fax", _company.Fax);
                            cmd.Parameters.AddWithValue("@DirectorOne", _company.DirectorOne);
                            cmd.Parameters.AddWithValue("@DirectorTwo", _company.DirectorTwo);
                            cmd.Parameters.AddWithValue("@DirectorThree", _company.DirectorThree);
                            cmd.Parameters.AddWithValue("@PPEMinimalMKBD", _company.PPEMinimalMKBD);
                            cmd.Parameters.AddWithValue("@MIMinimalMKBD", _company.MIMinimalMKBD);
                            cmd.Parameters.AddWithValue("@DefaultCurrencyPK", _company.DefaultCurrencyPK);
                            cmd.Parameters.AddWithValue("@MKBDCode", _company.MKBDCode);
                            cmd.Parameters.AddWithValue("@NoIDPJK", _company.NoIDPJK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _company.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _company.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Company set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CompanyPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _company.EntryUsersID);
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
                                cmd.CommandText = "Update Company set Notes=@Notes,ID=@ID,Name=@Name,Address=@Address,NPWP=@NPWP," +
                                    "Phone=@Phone,Fax=@Fax,DirectorOne=@DirectorOne,DirectorTwo=@DirectorTwo,DirectorThree=@DirectorThree,PPEMinimalMKBD=@PPEMinimalMKBD,MIMinimalMKBD=@MIMinimalMKBD,DefaultCurrencyPK=@DefaultCurrencyPK,MKBDCode=@MKBDCode,NoIDPJK=@NoIDPJK," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where CompanyPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _company.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                                cmd.Parameters.AddWithValue("@Notes", _company.Notes);
                                cmd.Parameters.AddWithValue("@ID", _company.ID);
                                cmd.Parameters.AddWithValue("@Name", _company.Name);
                                cmd.Parameters.AddWithValue("@Address", _company.Address);
                                cmd.Parameters.AddWithValue("@NPWP", _company.NPWP);
                                cmd.Parameters.AddWithValue("@Phone", _company.Phone);
                                cmd.Parameters.AddWithValue("@Fax", _company.Fax);
                                cmd.Parameters.AddWithValue("@DirectorOne", _company.DirectorOne);
                                cmd.Parameters.AddWithValue("@DirectorTwo", _company.DirectorTwo);
                                cmd.Parameters.AddWithValue("@DirectorThree", _company.DirectorThree);
                                cmd.Parameters.AddWithValue("@PPEMinimalMKBD", _company.PPEMinimalMKBD);
                                cmd.Parameters.AddWithValue("@MIMinimalMKBD", _company.MIMinimalMKBD);
                                cmd.Parameters.AddWithValue("@DefaultCurrencyPK", _company.DefaultCurrencyPK);
                                cmd.Parameters.AddWithValue("@MKBDCode", _company.MKBDCode);
                                cmd.Parameters.AddWithValue("@NoIDPJK", _company.NoIDPJK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _company.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_company.CompanyPK, "Company");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Company where CompanyPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _company.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _company.ID);
                                cmd.Parameters.AddWithValue("@Name", _company.Name);
                                cmd.Parameters.AddWithValue("@Address", _company.Address);
                                cmd.Parameters.AddWithValue("@NPWP", _company.NPWP);
                                cmd.Parameters.AddWithValue("@Phone", _company.Phone);
                                cmd.Parameters.AddWithValue("@Fax", _company.Fax);
                                cmd.Parameters.AddWithValue("@DirectorOne", _company.DirectorOne);
                                cmd.Parameters.AddWithValue("@DirectorTwo", _company.DirectorTwo);
                                cmd.Parameters.AddWithValue("@DirectorThree", _company.DirectorThree);
                                cmd.Parameters.AddWithValue("@PPEMinimalMKBD", _company.PPEMinimalMKBD);
                                cmd.Parameters.AddWithValue("@MIMinimalMKBD", _company.MIMinimalMKBD);
                                cmd.Parameters.AddWithValue("@DefaultCurrencyPK", _company.DefaultCurrencyPK);
                                cmd.Parameters.AddWithValue("@MKBDCode", _company.MKBDCode);
                                cmd.Parameters.AddWithValue("@NoIDPJK", _company.NoIDPJK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _company.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Company set status= 4,Notes=@Notes," +
                                    "LastUpdate=@lastupdate where CompanyPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _company.Notes);
                                cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _company.HistoryPK);
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

        public void Company_Approved(Company _company)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Company set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where CompanyPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                        cmd.Parameters.AddWithValue("@historyPK", _company.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _company.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Company set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CompanyPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _company.ApprovedUsersID);
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

        public void Company_Reject(Company _company)
        {
           try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Company set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CompanyPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                        cmd.Parameters.AddWithValue("@historyPK", _company.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _company.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Company set status= 2,LastUpdate=@LastUpdate where CompanyPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
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

        public void Company_Void(Company _company)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Company set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CompanyPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _company.CompanyPK);
                        cmd.Parameters.AddWithValue("@historyPK", _company.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _company.VoidUsersID);
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
    
    }
}