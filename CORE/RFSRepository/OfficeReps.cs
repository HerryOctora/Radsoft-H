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
    public class OfficeReps
    {
        Host _host = new Host();
       
        //1
        string _insertCommand = "INSERT INTO [dbo].[Office] " +
                            "([OfficePK],[HistoryPK],[Status],[ID],[Name],[Address],[CityPK],[Country],[ZipCode],[Phone],[FaxNo],[FaxServerName],[Email],[Manager]," +
                            "[CashRefPaymentPK],[PaymentInstruction],[Groups],[ParentPK],";
        string _paramaterCommand = "@ID,@name,@Address,@CityPK,@Country,@ZipCode,@Phone,@FaxNo,@FaxServerName,@Email,@Manager," +
                            "@CashRefPaymentPK,@PaymentInstruction,@Groups,@ParentPK,";

        //2
        private Office setOffice(SqlDataReader dr)
        {
            Office M_Office = new Office();
            M_Office.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_Office.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Office.Status = Convert.ToInt32(dr["Status"]);
            M_Office.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Office.Notes = Convert.ToString(dr["Notes"]);
            M_Office.ID = dr["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ID"]);
            M_Office.Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]);
            M_Office.Address = dr["Address"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Address"]);
            M_Office.CityPK = dr["CityPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CityPK"]);
            M_Office.CityDesc = dr["CityDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CityDesc"]);
            M_Office.Country = dr["Country"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Country"]);
            M_Office.CountryDesc = dr["CountryDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CountryDesc"]);
            M_Office.ZipCode = dr["ZipCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ZipCode"]);
            M_Office.Phone = dr["Phone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Phone"]);
            M_Office.FaxNo = dr["FaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FaxNo"]);
            M_Office.FaxServerName = dr["FaxServerName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FaxServerName"]);
            M_Office.Email = dr["Email"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Email"]);
            M_Office.Manager = dr["Manager"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Manager"]);
            M_Office.CashRefPaymentPK = dr["CashRefPaymentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CashRefPaymentPK"]);
            M_Office.CashRefPaymentID = dr["CashRefPaymentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CashRefPaymentID"]);
            M_Office.PaymentInstruction = dr["PaymentInstruction"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PaymentInstruction"]);
            M_Office.Groups = dr["Groups"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Groups"]);
            M_Office.ParentPK = dr["ParentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ParentPK"]);
            M_Office.ParentID = dr["ParentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ParentID"]);
            M_Office.EntryUsersID = dr["EntryUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryUsersID"]);
            M_Office.UpdateUsersID = dr["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateUsersID"]);
            M_Office.ApprovedUsersID = dr["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedUsersID"]);
            M_Office.VoidUsersID = dr["VoidUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidUsersID"]);
            M_Office.EntryTime = dr["EntryTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryTime"]);
            M_Office.UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateTime"]);
            M_Office.ApprovedTime = dr["ApprovedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedTime"]);
            M_Office.VoidTime = dr["VoidTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidTime"]);
            M_Office.DBUserID = dr["DBUserID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBUserID"]);
            M_Office.DBTerminalID = dr["DBTerminalID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBTerminalID"]);
            M_Office.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdate"]);
            M_Office.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Office;
        }

        public List<Office> Office_Select(int _status)
        {
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Office> L_Office = new List<Office>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select case when O.status=1 then 'PENDING' else Case When O.status = 2 then 'APPROVED' else Case when O.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID CashRefPaymentID,MV1.DescOne CityDesc,MV2.DescOne CountryDesc,ZO.ID ParentID, O.* from Office O left join  Office ZO on O.ParentPK = ZO.OfficePK and ZO.status in (1,2)  " +  
                            " left join MasterValue MV1 on O.CityPK = MV1.Code and MV1.ID = 'CityRHB' and MV1.status = 2 " + 
                            " left join MasterValue MV2 on O.Country = MV2.Code and MV2.ID = 'KSEICountry' and MV2.status = 2  " + 
                            " left join CashRef CR on O.CashRefPaymentPK = CR.CashRefPK and CR.status = 2  " +
                            " where O.status = @status order by OfficePK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " Select case when O.status=1 then 'PENDING' else Case When O.status = 2 then 'APPROVED' else Case when O.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,CR.ID CashRefPaymentID,MV1.DescOne CityDesc,MV2.DescOne CountryDesc,ZO.ID ParentID, O.* from Office O left join  Office ZO on O.ParentPK = ZO.OfficePK and ZO.status in (1,2)  " +
                            " left join MasterValue MV1 on O.CityPK = MV1.Code and MV1.ID = 'CityRHB' and MV1.status = 2 " +
                            " left join MasterValue MV2 on O.Country = MV2.Code and MV2.ID = 'KSEICountry' and MV2.status = 2  " +
                            " left join CashRef CR on O.CashRefPaymentPK = CR.CashRefPK and CR.status = 2  order by OfficePK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Office.Add(setOffice(dr));
                                }
                            }
                            return L_Office;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Office_Add(Office _office, bool _havePrivillege)
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
                                 "Select isnull(max(OfficePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from Office";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _office.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(OfficePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from Office";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _office.ID);
                        cmd.Parameters.AddWithValue("@Name", _office.Name);
                        cmd.Parameters.AddWithValue("@Address", _office.Address);
                        cmd.Parameters.AddWithValue("@CityPK", _office.CityPK);
                        cmd.Parameters.AddWithValue("@Country", _office.Country);
                        cmd.Parameters.AddWithValue("@ZipCode", _office.ZipCode);
                        cmd.Parameters.AddWithValue("@Phone", _office.Phone);
                        cmd.Parameters.AddWithValue("@FaxNo", _office.FaxNo);
                        cmd.Parameters.AddWithValue("@FaxServerName", _office.FaxServerName);
                        cmd.Parameters.AddWithValue("@Email", _office.Email);
                        cmd.Parameters.AddWithValue("@Manager", _office.Manager);
                        cmd.Parameters.AddWithValue("@CashRefPaymentPK", _office.CashRefPaymentPK);
                        cmd.Parameters.AddWithValue("@PaymentInstruction", _office.PaymentInstruction);
                        cmd.Parameters.AddWithValue("@Groups", _office.Groups);
                        cmd.Parameters.AddWithValue("@ParentPK", _office.ParentPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _office.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Office");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Office_Update(Office _office, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_office.OfficePK, _office.HistoryPK, "office");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = " Update Office set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                " Address=@Address,CityPK=@CityPK,Country=@Country,ZipCode=@ZipCode,Phone=@Phone, " +
                                " FaxNo=@FaxNo,FaxServerName=@FaxServerName,Email=@Email,Manager=@Manager,CashRefPaymentPK=@CashRefPaymentPK, " +
                                " PaymentInstruction=@PaymentInstruction,Groups=@Groups,ParentPK=@ParentPK, " +
                                " ApprovedUsersID=@ApprovedUsersID, " +
                                " ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                " where OfficePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _office.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                            cmd.Parameters.AddWithValue("@Notes", _office.Notes);
                            cmd.Parameters.AddWithValue("@ID", _office.ID);
                            cmd.Parameters.AddWithValue("@Name", _office.Name);
                            cmd.Parameters.AddWithValue("@Address", _office.Address);
                            cmd.Parameters.AddWithValue("@CityPK", _office.CityPK);
                            cmd.Parameters.AddWithValue("@Country", _office.Country);
                            cmd.Parameters.AddWithValue("@ZipCode", _office.ZipCode);
                            cmd.Parameters.AddWithValue("@Phone", _office.Phone);
                            cmd.Parameters.AddWithValue("@FaxNo", _office.FaxNo);
                            cmd.Parameters.AddWithValue("@FaxServerName", _office.FaxServerName);
                            cmd.Parameters.AddWithValue("@Email", _office.Email);
                            cmd.Parameters.AddWithValue("@Manager", _office.Manager);
                            cmd.Parameters.AddWithValue("@CashRefPaymentPK", _office.CashRefPaymentPK);
                            cmd.Parameters.AddWithValue("@PaymentInstruction", _office.PaymentInstruction);
                            cmd.Parameters.AddWithValue("@Groups", _office.Groups);
                            cmd.Parameters.AddWithValue("@ParentPK", _office.ParentPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _office.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _office.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Office set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where OfficePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _office.EntryUsersID);
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
                                cmd.CommandText = "Update Office set Notes=@Notes,ID=@ID,Name=@Name," +
                                    " Address=@Address,CityPK=@CityPK,Country=@Country,ZipCode=@ZipCode,Phone=@Phone, " +
                                    " FaxNo=@FaxNo,FaxServerName=@FaxServerName,Email=@Email,Manager=@Manager,CashRefPaymentPK=@CashRefPaymentPK, " +
                                    " PaymentInstruction=@PaymentInstruction,Groups=@Groups,ParentPK=@ParentPK, " +
                                    " UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    " where OfficePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _office.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                                cmd.Parameters.AddWithValue("@ID", _office.ID);
                                cmd.Parameters.AddWithValue("@Notes", _office.Notes);
                                cmd.Parameters.AddWithValue("@Name", _office.Name);
                                cmd.Parameters.AddWithValue("@Address", _office.Address);
                                cmd.Parameters.AddWithValue("@CityPK", _office.CityPK);
                                cmd.Parameters.AddWithValue("@Country", _office.Country);
                                cmd.Parameters.AddWithValue("@ZipCode", _office.ZipCode);
                                cmd.Parameters.AddWithValue("@Phone", _office.Phone);
                                cmd.Parameters.AddWithValue("@FaxNo", _office.FaxNo);
                                cmd.Parameters.AddWithValue("@FaxServerName", _office.FaxServerName);
                                cmd.Parameters.AddWithValue("@Email", _office.Email);
                                cmd.Parameters.AddWithValue("@Manager", _office.Manager);
                                cmd.Parameters.AddWithValue("@CashRefPaymentPK", _office.CashRefPaymentPK);
                                cmd.Parameters.AddWithValue("@PaymentInstruction", _office.PaymentInstruction);
                                cmd.Parameters.AddWithValue("@Groups", _office.Groups);
                                cmd.Parameters.AddWithValue("@ParentPK", _office.ParentPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _office.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_office.OfficePK, "Office");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Office where OfficePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _office.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _office.ID);
                                cmd.Parameters.AddWithValue("@Name", _office.Name);
                                cmd.Parameters.AddWithValue("@Address", _office.Address);
                                cmd.Parameters.AddWithValue("@CityPK", _office.CityPK);
                                cmd.Parameters.AddWithValue("@Country", _office.Country);
                                cmd.Parameters.AddWithValue("@ZipCode", _office.ZipCode);
                                cmd.Parameters.AddWithValue("@Phone", _office.Phone);
                                cmd.Parameters.AddWithValue("@FaxNo", _office.FaxNo);
                                cmd.Parameters.AddWithValue("@FaxServerName", _office.FaxServerName);
                                cmd.Parameters.AddWithValue("@Email", _office.Email);
                                cmd.Parameters.AddWithValue("@Manager", _office.Manager);
                                cmd.Parameters.AddWithValue("@CashRefPaymentPK", _office.CashRefPaymentPK);
                                cmd.Parameters.AddWithValue("@PaymentInstruction", _office.PaymentInstruction);
                                cmd.Parameters.AddWithValue("@Groups", _office.Groups);
                                cmd.Parameters.AddWithValue("@ParentPK", _office.ParentPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _office.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Office set status= 4,Notes=@Notes,"+
                                    "lastupdate=@lastupdate where OfficePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _office.Notes);
                                cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _office.HistoryPK);
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

        public void Office_Approved(Office _office)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Office set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where OfficePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                        cmd.Parameters.AddWithValue("@historyPK", _office.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _office.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Office set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where OfficePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _office.ApprovedUsersID);
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

        public void Office_Reject(Office _office)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Office set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where OfficePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                        cmd.Parameters.AddWithValue("@historyPK", _office.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _office.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Office set status= 2,lastupdate=@lastupdate where OfficePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
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

        public void Office_Void(Office _office)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Office set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where OfficePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _office.OfficePK);
                        cmd.Parameters.AddWithValue("@historyPK", _office.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _office.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )
        public List<OfficeCombo> Office_Combo()
        {
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OfficeCombo> L_Office = new List<OfficeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  OfficePK,ID + ' - ' + Name as ID, Name FROM [Office]   where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OfficeCombo M_Office = new OfficeCombo();
                                    M_Office.OfficePK = Convert.ToInt32(dr["OfficePK"]);
                                    M_Office.ID = Convert.ToString(dr["ID"]);
                                    M_Office.Name = Convert.ToString(dr["Name"]);
                                    L_Office.Add(M_Office);
                                }
                            }
                            return L_Office;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<OfficeCombo> Office_ComboRpt()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OfficeCombo> L_Office = new List<OfficeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  OfficePK,ID + ' - ' + Name as ID, Name FROM [Office]   where status = 2 union all select 0,'All', '' order by OfficePK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OfficeCombo M_Office = new OfficeCombo();
                                    M_Office.OfficePK = Convert.ToInt32(dr["OfficePK"]);
                                    M_Office.ID = Convert.ToString(dr["ID"]);
                                    M_Office.Name = Convert.ToString(dr["Name"]);
                                    L_Office.Add(M_Office);
                                }
                            }
                            return L_Office;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<OfficeCombo> Office_ComboGroupOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OfficeCombo> L_Office = new List<OfficeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  OfficePK,ID + ' - ' + Name as [ID], Name FROM [Office]  where Groups = 1 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OfficeCombo M_Office = new OfficeCombo();
                                    M_Office.OfficePK = Convert.ToInt32(dr["OfficePK"]);
                                    M_Office.ID = Convert.ToString(dr["ID"]);
                                    M_Office.Name = Convert.ToString(dr["Name"]);
                                    L_Office.Add(M_Office);
                                }
                            }
                            return L_Office;
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