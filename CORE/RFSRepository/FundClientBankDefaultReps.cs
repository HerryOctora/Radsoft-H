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
    public class FundClientBankDefaultReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClientBankDefault] " +
                            "([FundClientBankDefaultPK],[HistoryPK],[Status],[FundClientPK],[BankRecipientPK],[FundPK]," +
                            " ";

        string _paramaterCommand = "@FundClientPK,@BankRecipientPK,@FundPK,";

        //2
        private FundClientBankDefault setFundClientBankDefault(SqlDataReader dr)
        {
            FundClientBankDefault M_FundClientBankDefault = new FundClientBankDefault();
            M_FundClientBankDefault.FundClientBankDefaultPK = Convert.ToInt32(dr["FundClientBankDefaultPK"]);
            M_FundClientBankDefault.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientBankDefault.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientBankDefault.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientBankDefault.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientBankDefault.FundClientPK = dr["FundClientPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientBankDefault.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_FundClientBankDefault.BankRecipientPK = Convert.ToInt32(dr["BankRecipientPK"]);
            M_FundClientBankDefault.BankRecipientDesc = Convert.ToString(dr["BankRecipientDesc"]);
            M_FundClientBankDefault.FundPK = dr["FundPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FundPK"]);
            M_FundClientBankDefault.FundID = dr["FundID"].ToString();
            M_FundClientBankDefault.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientBankDefault.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientBankDefault.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientBankDefault.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientBankDefault.EntryTime = dr["EntryTime"].ToString();
            M_FundClientBankDefault.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientBankDefault.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientBankDefault.VoidTime = dr["VoidTime"].ToString();
            M_FundClientBankDefault.DBUserID = dr["DBUserID"].ToString();
            M_FundClientBankDefault.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientBankDefault.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientBankDefault.LastUpdateDB = dr["LastUpdateDB"].ToString();
            return M_FundClientBankDefault;
        }

        public List<FundClientBankDefault> FundClientBankDefault_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientBankDefault> L_FundClientBankDefault = new List<FundClientBankDefault>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"
Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
BankRecipientPK,case when B.BankRecipientPK = 1 then isnull(G.Name,'') + ' - ' + isnull(E.NomorRekening1,'')  else case when B.BankRecipientPK= 2 then  
isnull(H.Name,'') + ' - ' + isnull(K.NomorRekening2,'')  else case when  B.BankRecipientPK= 3 then   isnull(I.Name,'') + ' - ' + isnull(L.NomorRekening3,'') 
else isnull(J.Name,'')  + ' - ' + isnull(F.AccountNo,'') end end   end BankRecipientDesc, 
C.FundPK,isnull(C.ID,'ALL') FundID, E.FundClientPK FundClientPK, E.ID + ' - ' + E.Name FundClientID,
B.* from FundClientBankDefault B      
left join Fund C on B.FundPK = C.FundPK and C.status in(1,2)   
left join FundClient E on B.FundClientPK = E.FundClientPK and E.status in(1,2)  
left join FundClient K on B.FundClientPK = K.FundClientPK and K.status in(1,2)  
left join FundClient L on B.FundClientPK = L.FundClientPK and L.status in(1,2)  
left join FundClientBankList F on B.FundClientPK = F.FundClientPK and F.Status in(1,2)  and B.bankRecipientPK = F.NoBank
left join Bank G on E.namabank1 = G.BankPK and G.Status in(1,2)
left join Bank H on K.namabank2 = H.BankPK and H.Status in(1,2) 
left join Bank I on L.namabank3 = I.BankPK and I.Status in(1,2) 
left join Bank J on F.BankPK = J.BankPK and J.Status in(1,2)
where B.status  = @status
                            ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
          Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
BankRecipientPK,case when B.BankRecipientPK = 1 then isnull(G.Name,'') + ' - ' + isnull(E.NomorRekening1,'')  else case when B.BankRecipientPK= 2 then  
isnull(H.Name,'') + ' - ' + isnull(K.NomorRekening2,'')  else case when  B.BankRecipientPK= 3 then   isnull(I.Name,'') + ' - ' + isnull(L.NomorRekening3,'') 
else isnull(J.Name,'')  + ' - ' + isnull(F.AccountNo,'') end end   end BankRecipientDesc, 
C.FundPK,isnull(C.ID,'ALL') FundID, E.FundClientPK FundClientPK, E.ID + ' - ' + E.Name FundClientID,
B.* from FundClientBankDefault B      
left join Fund C on B.FundPK = C.FundPK and C.status in(1,2)   
left join FundClient E on B.FundClientPK = E.FundClientPK and E.status in(1,2)  
left join FundClient K on B.FundClientPK = K.FundClientPK and K.status in(1,2)  
left join FundClient L on B.FundClientPK = L.FundClientPK and L.status in(1,2)  
left join FundClientBankList F on B.FundClientPK = F.FundClientPK and F.Status in(1,2)  and B.bankRecipientPK = F.NoBank
left join Bank G on E.namabank1 = G.BankPK and G.Status in(1,2)
left join Bank H on K.namabank2 = H.BankPK and H.Status in(1,2) 
left join Bank I on L.namabank3 = I.BankPK and I.Status in(1,2) 
left join Bank J on F.BankPK = J.BankPK and J.Status in(1,2)

                        ";



                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientBankDefault.Add(setFundClientBankDefault(dr));
                                }
                            }
                            return L_FundClientBankDefault;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientBankDefault_Add(FundClientBankDefault _FundClientBankDefault, bool _havePrivillege)
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
                                 "Select isnull(max(FundClientBankDefaultPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FundClientBankDefault";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientBankDefault.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundClientBankDefaultPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FundClientBankDefault";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientBankDefault.FundClientPK);
                        cmd.Parameters.AddWithValue("@BankRecipientPK", _FundClientBankDefault.BankRecipientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _FundClientBankDefault.FundPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientBankDefault.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundClientBankDefault");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientBankDefault_Update(FundClientBankDefault _FundClientBankDefault, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_FundClientBankDefault.FundClientBankDefaultPK, _FundClientBankDefault.HistoryPK, "FundClientBankDefault");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientBankDefault set status=2,Notes=@Notes,FundClientPK=@FundClientPK,BankRecipientPK=@BankRecipientPK,FundPK=@FundPK," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where FundClientBankDefaultPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FundClientBankDefault.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundClientBankDefault.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _FundClientBankDefault.FundClientPK);
                            cmd.Parameters.AddWithValue("@BankRecipientPK", _FundClientBankDefault.BankRecipientPK);
                            cmd.Parameters.AddWithValue("@FundPK", _FundClientBankDefault.FundPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientBankDefault.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientBankDefault.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientBankDefault set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundClientBankDefaultPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientBankDefault.EntryUsersID);
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
                                cmd.CommandText = "Update FundClientBankDefault set Notes=@Notes,FundClientPK=@FundClientPK,BankRecipientPK=@BankRecipientPK,FundPK=@FundPK," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where FundClientBankDefaultPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientBankDefault.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundClientBankDefault.Notes);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientBankDefault.FundClientPK);
                                cmd.Parameters.AddWithValue("@BankRecipientPK", _FundClientBankDefault.BankRecipientPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundClientBankDefault.FundPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientBankDefault.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FundClientBankDefault.FundClientBankDefaultPK, "FundClientBankDefault");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClientBankDefault where FundClientBankDefaultPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientBankDefault.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientBankDefault.FundClientPK);
                                cmd.Parameters.AddWithValue("@BankRecipientPK", _FundClientBankDefault.BankRecipientPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundClientBankDefault.FundPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientBankDefault.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClientBankDefault set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where FundClientBankDefaultPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundClientBankDefault.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientBankDefault.HistoryPK);
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

        public void FundClientBankDefault_Approved(FundClientBankDefault _FundClientBankDefault)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientBankDefault set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FundClientBankDefaultPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientBankDefault.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientBankDefault.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientBankDefault set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundClientBankDefaultPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientBankDefault.ApprovedUsersID);
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

        public void FundClientBankDefault_Reject(FundClientBankDefault _FundClientBankDefault)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientBankDefault set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FundClientBankDefaultPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientBankDefault.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientBankDefault.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientBankDefault set status= 2,lastupdate=@lastupdate where FundClientBankDefaultPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
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

        public void FundClientBankDefault_Void(FundClientBankDefault _FundClientBankDefault)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientBankDefault set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where FundClientBankDefaultPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientBankDefault.FundClientBankDefaultPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientBankDefault.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientBankDefault.VoidUsersID);
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

        private FundClientBankDefault setFundClientBankDefaultCombo(SqlDataReader dr)
        {
            FundClientBankDefault M_FundClientBankDefault = new FundClientBankDefault();
            M_FundClientBankDefault.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientBankDefault.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientBankDefault.BankRecipientPK = Convert.ToInt32(dr["BankRecipientPK"]);
            M_FundClientBankDefault.BankRecipientDesc = Convert.ToString(dr["BankRecipientDesc"]);
            M_FundClientBankDefault.FundPK = dr["FundPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FundPK"]);
            M_FundClientBankDefault.FundID = dr["FundID"].ToString();
            M_FundClientBankDefault.FundName = dr["FundName"].ToString();
            return M_FundClientBankDefault;
        }

        public List<FundClientBankDefault> FundClientBankDefault_GetDataCombo(int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientBankDefault> L_FundClientBankDefault = new List<FundClientBankDefault>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                    
                        Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                  BankRecipientPK,case when B.BankRecipientPK = 1 then G.Name + ' - ' + E.NomorRekening1  else case when B.BankRecipientPK= 2 then  H.Name + ' - ' + K.NomorRekening2  else case when  B.BankRecipientPK= 3 then   I.Name + ' - ' + L.NomorRekening3 else F.AccountName + ' - ' + J.Name end end end BankRecipientDesc, 
                                  C.FundPK,isnull(C.ID,'ALL') FundID, C.Name FundName, E.FundClientPK FundClientPK, E.ID + ' - ' + E.Name FundClientID,
								  B.* from FundClientBankDefault B      
                                  left join Fund C on B.FundPK = C.FundPK and C.status in(1,2)   
                                  left join FundClient E on B.FundClientPK = E.FundClientPK and E.status in(1,2)  
                                  left join FundClient K on B.FundClientPK = K.FundClientPK and K.status in(1,2)  
                                  left join FundClient L on B.FundClientPK = L.FundClientPK and L.status in(1,2)  
                                  left join FundClientBankList F on B.FundClientPK = F.FundClientPK and F.Status in(1,2)
								  left join Bank G on E.namabank1 = G.BankPK and G.Status in(1,2)
								  left join Bank H on K.namabank2 = H.BankPK and H.Status in(1,2) 
								  left join Bank I on L.namabank3 = I.BankPK and I.Status in(1,2) 
								  left join Bank J on F.BankPK = J.BankPK and J.Status in(1,2)
                                  where B.status in(1,2) and B.FundClientPK = @FundClientPK  order by FundClientBankDefaultPK 
                            ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientBankDefault.Add(setFundClientBankDefaultCombo(dr));
                                }
                            }
                            return L_FundClientBankDefault;
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