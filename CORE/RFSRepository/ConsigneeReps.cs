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
    public class ConsigneeReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Consignee] " +
                            "([ConsigneePK],[HistoryPK],[Status],[ID],[Name],[NoRek],[Phone],[Fax],[Email],[Address],[TaxID],[BankInformation],[BeneficiaryName],[Description],";
        string _paramaterCommand = "@ID,@Name,@NoRek,@Phone,@Fax,@Email,@Address,@TaxID,@BankInformation,@BeneficiaryName,@Description,";

        //2
        private Consignee setConsignee(SqlDataReader dr)
        {
            Consignee M_Consignee = new Consignee();
            M_Consignee.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
            M_Consignee.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Consignee.Status = Convert.ToInt32(dr["Status"]);
            M_Consignee.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Consignee.Notes = Convert.ToString(dr["Notes"]);
            M_Consignee.ID = dr["ID"].ToString();
            M_Consignee.Name = dr["Name"].ToString();
            M_Consignee.NoRek = dr["NoRek"].ToString();
            M_Consignee.Phone = dr["Phone"].ToString();
            M_Consignee.Fax = dr["Fax"].ToString();
            M_Consignee.Email = dr["Email"].ToString();
            M_Consignee.Address = dr["Address"].ToString();
            M_Consignee.TaxID = dr["TaxID"].ToString();
            M_Consignee.BankInformation = dr["BankInformation"].ToString();
            M_Consignee.BeneficiaryName = dr["BeneficiaryName"].ToString();
            M_Consignee.Description = dr["Description"].ToString();
            M_Consignee.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Consignee.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Consignee.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Consignee.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Consignee.EntryTime = dr["EntryTime"].ToString();
            M_Consignee.UpdateTime = dr["UpdateTime"].ToString();
            M_Consignee.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Consignee.VoidTime = dr["VoidTime"].ToString();
            M_Consignee.DBUserID = dr["DBUserID"].ToString();
            M_Consignee.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Consignee.LastUpdate = dr["LastUpdate"].ToString();
            M_Consignee.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Consignee;
        }

        public List<Consignee> Consignee_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Consignee> L_consignee = new List<Consignee>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Consignee where status = @status order by ConsigneePK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Consignee order by ConsigneePK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_consignee.Add(setConsignee(dr));
                                }
                            }
                            return L_consignee;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Consignee_Add(Consignee _consignee, bool _havePrivillege)
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
                                 "Select isnull(max(ConsigneePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from Consignee";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _consignee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(ConsigneePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from Consignee";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _consignee.ID);
                        cmd.Parameters.AddWithValue("@Name", _consignee.Name);
                        cmd.Parameters.AddWithValue("@NoRek", _consignee.NoRek);
                        cmd.Parameters.AddWithValue("@Phone", _consignee.Phone);
                        cmd.Parameters.AddWithValue("@Fax", _consignee.Fax);
                        cmd.Parameters.AddWithValue("@Email", _consignee.Email);
                        cmd.Parameters.AddWithValue("@Address", _consignee.Address);
                        cmd.Parameters.AddWithValue("@TaxID", _consignee.TaxID);
                        cmd.Parameters.AddWithValue("@BankInformation", _consignee.BankInformation);
                        cmd.Parameters.AddWithValue("@BeneficiaryName", _consignee.BeneficiaryName);
                        cmd.Parameters.AddWithValue("@Description", _consignee.Description);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _consignee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Consignee");

                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Consignee_Update(Consignee _consignee, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_consignee.ConsigneePK, _consignee.HistoryPK, "Consignee");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Consignee set status=2, Notes=@Notes,ID=@ID,Name=@Name,NoRek=@NoRek,Phone=@Phone,Fax=@Fax, " +
                                "Email=@Email,Address=@Address,TaxID=@TaxID,BankInformation=@BankInformation,BeneficiaryName=@BeneficiaryName,Description=@Description," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where ConsigneePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _consignee.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                            cmd.Parameters.AddWithValue("@Notes", _consignee.Notes);
                            cmd.Parameters.AddWithValue("@ID", _consignee.ID);
                            cmd.Parameters.AddWithValue("@Name", _consignee.Name);
                            cmd.Parameters.AddWithValue("@NoRek", _consignee.NoRek);
                            cmd.Parameters.AddWithValue("@Phone", _consignee.Phone);
                            cmd.Parameters.AddWithValue("@Fax", _consignee.Fax);
                            cmd.Parameters.AddWithValue("@Email", _consignee.Email);
                            cmd.Parameters.AddWithValue("@Address", _consignee.Address);
                            cmd.Parameters.AddWithValue("@TaxID", _consignee.TaxID);
                            cmd.Parameters.AddWithValue("@BankInformation", _consignee.BankInformation);
                            cmd.Parameters.AddWithValue("@BeneficiaryName", _consignee.BeneficiaryName);
                            cmd.Parameters.AddWithValue("@Description", _consignee.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _consignee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _consignee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Consignee set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ConsigneePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _consignee.EntryUsersID);
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
                                cmd.CommandText = "Update Consignee set Notes=@Notes,ID=@ID,Name=@Name,NoRek=@NoRek,Phone=@Phone,Fax=@Fax, " +
                                "Email=@Email,Address=@Address,TaxID=@TaxID,BankInformation=@BankInformation,BeneficiaryName=@BeneficiaryName,Description=@Description," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where ConsigneePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _consignee.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                                cmd.Parameters.AddWithValue("@Notes", _consignee.Notes);
                                cmd.Parameters.AddWithValue("@ID", _consignee.ID);
                                cmd.Parameters.AddWithValue("@Name", _consignee.Name);
                                cmd.Parameters.AddWithValue("@NoRek", _consignee.NoRek);
                                cmd.Parameters.AddWithValue("@Phone", _consignee.Phone);
                                cmd.Parameters.AddWithValue("@Fax", _consignee.Fax);
                                cmd.Parameters.AddWithValue("@Email", _consignee.Email);
                                cmd.Parameters.AddWithValue("@Address", _consignee.Address);
                                cmd.Parameters.AddWithValue("@TaxID", _consignee.TaxID);
                                cmd.Parameters.AddWithValue("@BankInformation", _consignee.BankInformation);
                                cmd.Parameters.AddWithValue("@BeneficiaryName", _consignee.BeneficiaryName);
                                cmd.Parameters.AddWithValue("@Description", _consignee.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _consignee.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_consignee.ConsigneePK, "Consignee");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Consignee where ConsigneePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _consignee.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _consignee.ID);
                                cmd.Parameters.AddWithValue("@Name", _consignee.Name);
                                cmd.Parameters.AddWithValue("@NoRek", _consignee.NoRek);
                                cmd.Parameters.AddWithValue("@Phone", _consignee.Phone);
                                cmd.Parameters.AddWithValue("@Fax", _consignee.Fax);
                                cmd.Parameters.AddWithValue("@Email", _consignee.Email);
                                cmd.Parameters.AddWithValue("@Address", _consignee.Address);
                                cmd.Parameters.AddWithValue("@TaxID", _consignee.TaxID);
                                cmd.Parameters.AddWithValue("@BankInformation", _consignee.BankInformation);
                                cmd.Parameters.AddWithValue("@BeneficiaryName", _consignee.BeneficiaryName);
                                cmd.Parameters.AddWithValue("@Description", _consignee.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _consignee.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Consignee set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where ConsigneePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _consignee.Notes);
                                cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _consignee.HistoryPK);
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

        public void Consignee_Approved(Consignee _consignee)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Consignee set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where ConsigneePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                        cmd.Parameters.AddWithValue("@historyPK", _consignee.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _consignee.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Consignee set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ConsigneePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _consignee.ApprovedUsersID);
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

        public void Consignee_Reject(Consignee _consignee)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Consignee set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where ConsigneePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                        cmd.Parameters.AddWithValue("@historyPK", _consignee.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _consignee.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Consignee set status= 2,LastUpdate=@lastUpdate where ConsigneePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
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

        public void Consignee_Void(Consignee _consignee)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Consignee set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where ConsigneePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _consignee.ConsigneePK);
                        cmd.Parameters.AddWithValue("@historyPK", _consignee.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _consignee.VoidUsersID);
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


        public List<ConsigneeCombo> Consignee_Combo()
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ConsigneeCombo> L_Consignee = new List<ConsigneeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  ConsigneePK,ID +' - '+ Name ID, Name FROM [Consignee]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ConsigneeCombo M_Consignee = new ConsigneeCombo();
                                    M_Consignee.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
                                    M_Consignee.ID = Convert.ToString(dr["ID"]);
                                    M_Consignee.Name = Convert.ToString(dr["Name"]);
                                    L_Consignee.Add(M_Consignee);
                                }

                            }
                            return L_Consignee;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<ConsigneeCombo> Consignee_ComboRpt()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ConsigneeCombo> L_Consignee = new List<ConsigneeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  ConsigneePK,ID +' - '+ Name ID, Name FROM [Consignee]  where status = 2 union all select 0,'All', '' order by ConsigneePK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ConsigneeCombo M_Consignee = new ConsigneeCombo();
                                    M_Consignee.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
                                    M_Consignee.ID = Convert.ToString(dr["ID"]);
                                    M_Consignee.Name = Convert.ToString(dr["Name"]);
                                    L_Consignee.Add(M_Consignee);
                                }

                            }
                            return L_Consignee;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<TypeOfAssetsCombo> TypeOfAssets_Combo()
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TypeOfAssetsCombo> L_Consignee = new List<TypeOfAssetsCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  TypeOfAssetsPK,ID +' - '+ Name ID, Name FROM [TypeOfAssets]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TypeOfAssetsCombo M_Consignee = new TypeOfAssetsCombo();
                                    M_Consignee.TypeOfAssetsPK = Convert.ToInt32(dr["TypeOfAssetsPK"]);
                                    M_Consignee.ID = Convert.ToString(dr["ID"]);
                                    M_Consignee.Name = Convert.ToString(dr["Name"]);
                                    L_Consignee.Add(M_Consignee);
                                }

                            }
                            return L_Consignee;
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