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
    public class SignatureReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Signature] " +
                            "([SignaturePK],[HistoryPK],[Status],[Name],[Position],[BitIsDefault1],[BitIsDefault2],[BitIsDefault3],[BitIsDefault4],";
        string _paramaterCommand = "@Name,@Position,@BitIsDefault1,@BitIsDefault2,@BitIsDefault3,@BitIsDefault4,";

        //2
        private Signature setSignature(SqlDataReader dr)
        {
            Signature M_Signature = new Signature();
            M_Signature.SignaturePK = Convert.ToInt32(dr["SignaturePK"]);
            M_Signature.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Signature.Status = Convert.ToInt32(dr["Status"]);
            M_Signature.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Signature.Notes = Convert.ToString(dr["Notes"]);
            M_Signature.Name = dr["Name"].ToString();
            M_Signature.Position = dr["Position"].ToString();
            M_Signature.BitIsDefault1 = Convert.ToBoolean(dr["BitIsDefault1"]);
            M_Signature.BitIsDefault2 = Convert.ToBoolean(dr["BitIsDefault2"]);
            M_Signature.BitIsDefault3 = Convert.ToBoolean(dr["BitIsDefault3"]);
            M_Signature.BitIsDefault4 = Convert.ToBoolean(dr["BitIsDefault4"]);
            M_Signature.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Signature.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Signature.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Signature.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Signature.EntryTime = dr["EntryTime"].ToString();
            M_Signature.UpdateTime = dr["UpdateTime"].ToString();
            M_Signature.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Signature.VoidTime = dr["VoidTime"].ToString();
            M_Signature.DBUserID = dr["DBUserID"].ToString();
            M_Signature.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Signature.LastUpdate = dr["LastUpdate"].ToString();
            M_Signature.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Signature;
        }

        public List<Signature> Signature_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Signature> L_signature = new List<Signature>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Signature " +
                            " where status = @status order by SignaturePK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Signature " +
                            " order by SignaturePK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_signature.Add(setSignature(dr));
                                }
                            }
                            return L_signature;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Signature_Add(Signature _signature, bool _havePrivillege)
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
                                 "Select isnull(max(SignaturePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From Signature";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _signature.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(SignaturePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From Signature";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Name", _signature.Name);
                        cmd.Parameters.AddWithValue("@Position", _signature.Position);
                        cmd.Parameters.AddWithValue("@BitIsDefault1", _signature.BitIsDefault1);
                        cmd.Parameters.AddWithValue("@BitIsDefault2", _signature.BitIsDefault2);
                        cmd.Parameters.AddWithValue("@BitIsDefault3", _signature.BitIsDefault3);
                        cmd.Parameters.AddWithValue("@BitIsDefault4", _signature.BitIsDefault4);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _signature.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Signature");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Signature_Update(Signature _signature, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_signature.SignaturePK, _signature.HistoryPK, "Signature");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Signature set status=2, Notes=@Notes,Name=@Name,Position=@Position,BitIsDefault1=@BitIsDefault1,BitIsDefault2=@BitIsDefault2,BitIsDefault3=@BitIsDefault3,BitIsDefault4=@BitIsDefault4," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where SignaturePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _signature.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _signature.Notes);
                            cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                            cmd.Parameters.AddWithValue("@Name", _signature.Name);
                            cmd.Parameters.AddWithValue("@Position", _signature.Position);
                            cmd.Parameters.AddWithValue("@BitIsDefault1", _signature.BitIsDefault1);
                            cmd.Parameters.AddWithValue("@BitIsDefault2", _signature.BitIsDefault2);
                            cmd.Parameters.AddWithValue("@BitIsDefault3", _signature.BitIsDefault3);
                            cmd.Parameters.AddWithValue("@BitIsDefault4", _signature.BitIsDefault4);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _signature.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _signature.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Signature set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SignaturePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _signature.EntryUsersID);
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
                                cmd.CommandText = "Update Signature set Notes=@Notes,Name=@Name,Position=@Position,BitIsDefault1=@BitIsDefault1,BitIsDefault2=@BitIsDefault2,BitIsDefault3=@BitIsDefault3,BitIsDefault4=@BitIsDefault4," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where SignaturePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _signature.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _signature.Notes);
                                cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                                cmd.Parameters.AddWithValue("@Name", _signature.Name);
                                cmd.Parameters.AddWithValue("@Position", _signature.Position);
                                cmd.Parameters.AddWithValue("@BitIsDefault1", _signature.BitIsDefault1);
                                cmd.Parameters.AddWithValue("@BitIsDefault2", _signature.BitIsDefault2);
                                cmd.Parameters.AddWithValue("@BitIsDefault3", _signature.BitIsDefault3);
                                cmd.Parameters.AddWithValue("@BitIsDefault4", _signature.BitIsDefault4);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _signature.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_signature.SignaturePK, "Signature");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Signature where SignaturePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _signature.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Name", _signature.Name);
                                cmd.Parameters.AddWithValue("@Position", _signature.Position);
                                cmd.Parameters.AddWithValue("@BitIsDefault1", _signature.BitIsDefault1);
                                cmd.Parameters.AddWithValue("@BitIsDefault2", _signature.BitIsDefault2);
                                cmd.Parameters.AddWithValue("@BitIsDefault3", _signature.BitIsDefault3);
                                cmd.Parameters.AddWithValue("@BitIsDefault4", _signature.BitIsDefault4);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _signature.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Signature set status= 4,Notes=@Notes," +
                                    "LastUpdate=LastUpdate where SignaturePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _signature.Notes);
                                cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _signature.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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

        public void Signature_Approved(Signature _signature)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Signature set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where SignaturePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                        cmd.Parameters.AddWithValue("@historyPK", _signature.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _signature.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Signature set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SignaturePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _signature.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Signature_Reject(Signature _signature)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Signature set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SignaturePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                        cmd.Parameters.AddWithValue("@historyPK", _signature.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _signature.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Signature set status= 2,LastUpdate=@LastUpdate where SignaturePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Signature_Void(Signature _signature)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Signature set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SignaturePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _signature.SignaturePK);
                        cmd.Parameters.AddWithValue("@historyPK", _signature.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _signature.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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

        public List<SignatureCombo> Get_DefaultSignature1Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SignatureCombo> L_signature = new List<SignatureCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  SignaturePK, Name FROM [Signature]  where status = 2 order by BitIsDefault1 desc";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SignatureCombo M_signature = new SignatureCombo();
                                    M_signature.SignaturePK = Convert.ToInt32(dr["SignaturePK"]);
                                    M_signature.Name = Convert.ToString(dr["Name"]);
                                    L_signature.Add(M_signature);
                                }

                            }
                            return L_signature;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<SignatureCombo> Get_DefaultSignature2Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SignatureCombo> L_signature = new List<SignatureCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  SignaturePK, Name FROM [Signature]  where status = 2 order by BitIsDefault2 desc";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SignatureCombo M_signature = new SignatureCombo();
                                    M_signature.SignaturePK = Convert.ToInt32(dr["SignaturePK"]);
                                    M_signature.Name = Convert.ToString(dr["Name"]);
                                    L_signature.Add(M_signature);
                                }

                            }
                            return L_signature;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<SignatureCombo> Get_DefaultSignature3Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SignatureCombo> L_signature = new List<SignatureCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  SignaturePK, Name FROM [Signature]  where status = 2 order by BitIsDefault3 desc";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SignatureCombo M_signature = new SignatureCombo();
                                    M_signature.SignaturePK = Convert.ToInt32(dr["SignaturePK"]);
                                    M_signature.Name = Convert.ToString(dr["Name"]);
                                    L_signature.Add(M_signature);
                                }

                            }
                            return L_signature;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<SignatureCombo> Get_DefaultSignature4Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SignatureCombo> L_signature = new List<SignatureCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  SignaturePK, Name FROM [Signature]  where status = 2 order by BitIsDefault4 desc";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SignatureCombo M_signature = new SignatureCombo();
                                    M_signature.SignaturePK = Convert.ToInt32(dr["SignaturePK"]);
                                    M_signature.Name = Convert.ToString(dr["Name"]);
                                    L_signature.Add(M_signature);
                                }

                            }
                            return L_signature;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }


        public Signature Get_PositionCombo(int _signaturePK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SignatureCombo> L_signature = new List<SignatureCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Position from Signature where status = 2 and SignaturePK = @PK";
                        cmd.Parameters.AddWithValue("@PK", _signaturePK);
                        cmd.ExecuteNonQuery();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new Signature()
                                {

                                    Position = Convert.ToString(dr["Position"])
                                };
                            }
                            else
                            {
                                return new Signature()
                                {

                                    Position = ""
                                };
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

        public bool Validate_Signature(bool _bitIsDefault1, bool _bitIsDefault2)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if @BitIsDefault1  = 1 " +
                                          "  BEGIN if Exists(select * From Signature where BitIsDefault1 = 1 and Status in (1,2)) " +
	                                      "  BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END END " +
                                          "  else if @BitIsDefault2 = 1 " +
                                          "  BEGIN if Exists(select * From Signature where BitIsDefault2 = 1 and Status in (1,2)) " +
	                                      "  BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END END " + 
                                          "  else BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@BitIsDefault1", _bitIsDefault1);
                        cmd.Parameters.AddWithValue("@BitIsDefault2", _bitIsDefault2);
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

    }
}