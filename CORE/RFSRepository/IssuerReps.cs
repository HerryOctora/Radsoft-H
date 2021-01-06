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
    public class IssuerReps
    {
        Host _host = new Host();
       
        //1
        string _insertCommand = "INSERT INTO [dbo].[Issuer] " +
                            "([IssuerPK],[HistoryPK],[Status],[ID],[Name],[HoldingPK],";
        string _paramaterCommand = "@ID,@Name,@HoldingPK,";

        //2
        private Issuer setIssuer(SqlDataReader dr)
        {
            Issuer M_Issuer = new Issuer();
            M_Issuer.IssuerPK = Convert.ToInt32(dr["IssuerPK"]);
            M_Issuer.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Issuer.Status = Convert.ToInt32(dr["Status"]);
            M_Issuer.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Issuer.Notes = Convert.ToString(dr["Notes"]);
            M_Issuer.ID = dr["ID"].ToString();
            M_Issuer.Name = dr["Name"].ToString();
            M_Issuer.HoldingPK = Convert.ToInt32(dr["HoldingPK"]);
            M_Issuer.HoldingID = Convert.ToString(dr["HoldingID"]);
            M_Issuer.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Issuer.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Issuer.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Issuer.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Issuer.EntryTime = dr["EntryTime"].ToString();
            M_Issuer.UpdateTime = dr["UpdateTime"].ToString();
            M_Issuer.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Issuer.VoidTime = dr["VoidTime"].ToString();
            M_Issuer.DBUserID = dr["DBUserID"].ToString();
            M_Issuer.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Issuer.LastUpdate = dr["LastUpdate"].ToString();
            M_Issuer.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Issuer;
        }

        public List<Issuer> Issuer_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Issuer> L_Issuer = new List<Issuer>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when I.status=1 then 'PENDING' else Case When I.status = 2 then 'APPROVED' else Case when I.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,H.ID HoldingID,I.* from Issuer I left join " +
                            "Holding H on I.HoldingPK = H.HoldingPK and H.status = 2 " +
                            "where I.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when I.status=1 then 'PENDING' else Case When I.status = 2 then 'APPROVED' else Case when I.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,H.ID HoldingID,I.* from Issuer I left join " +
                            "Holding H on I.HoldingPK = H.HoldingPK and H.status = 2 " +
                            "order by ID,HoldingPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Issuer.Add(setIssuer(dr));
                                }
                            }
                            return L_Issuer;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int Issuer_Add(Issuer _issuer, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(IssuerPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from Issuer";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _issuer.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(IssuerPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from Issuer";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@HoldingPK", _issuer.HoldingPK);
                        cmd.Parameters.AddWithValue("@ID", _issuer.ID);
                        cmd.Parameters.AddWithValue("@Name", _issuer.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _issuer.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Issuer");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
          
        }

        public int Issuer_Update(Issuer _issuer, bool _havePrivillege)
        {
            
           try
            {
                int _newHisPK;
                int status = _host.Get_Status(_issuer.IssuerPK, _issuer.HistoryPK, "issuer");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Issuer set status=2, Notes=@Notes,HoldingPK=@HoldingPK,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where IssuerPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _issuer.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                            cmd.Parameters.AddWithValue("@Notes", _issuer.Notes);
                            cmd.Parameters.AddWithValue("@HoldingPK", _issuer.HoldingPK);
                            cmd.Parameters.AddWithValue("@ID", _issuer.ID);
                            cmd.Parameters.AddWithValue("@Name", _issuer.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _issuer.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _issuer.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Issuer set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where IssuerPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _issuer.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = "Update Issuer set  Notes=@Notes,HoldingPK=@HoldingPK,ID=@ID,Name=@Name," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where IssuerPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _issuer.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                                cmd.Parameters.AddWithValue("@Notes", _issuer.Notes);
                                cmd.Parameters.AddWithValue("@HoldingPK", _issuer.HoldingPK);
                                cmd.Parameters.AddWithValue("@ID", _issuer.ID);
                                cmd.Parameters.AddWithValue("@Name", _issuer.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _issuer.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_issuer.IssuerPK, "Issuer");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Issuer where IssuerPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _issuer.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HoldingPK", _issuer.HoldingPK);
                                cmd.Parameters.AddWithValue("@ID", _issuer.ID);
                                cmd.Parameters.AddWithValue("@Name", _issuer.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _issuer.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Issuer set status= 4,Notes=@Notes,"+
                                    "LastUpdate=@LastUpdate where IssuerPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _issuer.Notes);
                                cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _issuer.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
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

        public void Issuer_Approved(Issuer _issuer)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;                
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Issuer set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where IssuerPK = @PK and historypk = @historypk";
                        cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                        cmd.Parameters.AddWithValue("@historyPK", _issuer.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _issuer.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Issuer set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where IssuerPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _issuer.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public void Issuer_Reject(Issuer _issuer)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Issuer set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where IssuerPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                        cmd.Parameters.AddWithValue("@historyPK", _issuer.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _issuer.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime",_dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Issuer set status= 2,LastUpdate=@LastUpdate where IssuerPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public void Issuer_Void(Issuer _issuer)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Issuer set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where IssuerPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _issuer.IssuerPK);
                        cmd.Parameters.AddWithValue("@historyPK", _issuer.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _issuer.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
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

        public List<IssuerCombo> Issuer_Combo()
        {
           
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<IssuerCombo> L_Issuer = new List<IssuerCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  IssuerPK,ID + ' - ' + Name ID, Name FROM [Issuer]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    IssuerCombo M_Issuer = new IssuerCombo();
                                    M_Issuer.IssuerPK = Convert.ToInt32(dr["IssuerPK"]);
                                    M_Issuer.ID = Convert.ToString(dr["ID"]);
                                    M_Issuer.Name = Convert.ToString(dr["Name"]);
                                    L_Issuer.Add(M_Issuer);
                                }

                            }
                            return L_Issuer;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }
  
        public IssuerLookup Issuer_LookupByIssuerPK(int _IssuerPK)
        {

           try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select H.ID + ' - ' + H.Name HoldingID,H.Name HoldingName,I.* from Issuer I left join " +
                        "Holding H on I.HoldingPK = H.HoldingPK " +
                        "where I.status = 2 and IssuerPK = @IssuerPK";
                        cmd.Parameters.AddWithValue("@IssuerPK", _IssuerPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            { // 10 Field
                                dr.Read();
                                IssuerLookup M_Issuer = new IssuerLookup();
                                M_Issuer.IssuerPK = Convert.ToInt32(dr["IssuerPK"]);
                                M_Issuer.ID = dr["ID"].ToString();
                                M_Issuer.Name = dr["Name"].ToString();
                                M_Issuer.HoldingPK = Convert.ToInt32(dr["HoldingPK"]);
                                M_Issuer.HoldingID = Convert.ToString(dr["HoldingID"]);
                                M_Issuer.HoldingName = Convert.ToString(dr["HoldingName"]);
                                return M_Issuer;

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
     
    }
}