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
    public class ClientGroupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[ClientGroup] " +
                            "([ClientGroupPK],[HistoryPK],[Status],[ID],[Name],[ClientGroupFee],[Levels],[ParentPK]," +
                            "[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[ParentPK6],[ParentPK7],[ParentPK8],[ParentPK9],";
        string _paramaterCommand = "@ID,@Name,@ClientGroupFee,@Levels,@ParentPK,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@ParentPK6,@ParentPK7,@ParentPK8,@ParentPK9,";

        //2
        private ClientGroup setClientGroup(SqlDataReader dr)
        {
            ClientGroup M_clientGroup = new ClientGroup();
            M_clientGroup.ClientGroupPK = Convert.ToInt32(dr["ClientGroupPK"]);
            M_clientGroup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_clientGroup.Status = Convert.ToInt32(dr["Status"]);
            M_clientGroup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_clientGroup.Notes = Convert.ToString(dr["Notes"]);
            M_clientGroup.ID = Convert.ToString(dr["ID"]);
            M_clientGroup.Name = Convert.ToString(dr["Name"]);
            M_clientGroup.ClientGroupFee = Convert.ToDecimal(dr["ClientGroupFee"]);
            M_clientGroup.Levels = Convert.ToInt32(dr["Levels"]);
            M_clientGroup.ParentPK = Convert.ToInt32(dr["ParentPK"]);
            M_clientGroup.ParentID = dr["ParentID"].ToString();
            M_clientGroup.ParentName = dr["ParentName"].ToString();
            M_clientGroup.ParentPK1 = Convert.ToInt32(dr["ParentPK1"]);
            M_clientGroup.ParentPK2 = Convert.ToInt32(dr["ParentPK2"]);
            M_clientGroup.ParentPK3 = Convert.ToInt32(dr["ParentPK3"]);
            M_clientGroup.ParentPK4 = Convert.ToInt32(dr["ParentPK4"]);
            M_clientGroup.ParentPK5 = Convert.ToInt32(dr["ParentPK5"]);
            M_clientGroup.ParentPK6 = Convert.ToInt32(dr["ParentPK6"]);
            M_clientGroup.ParentPK7 = Convert.ToInt32(dr["ParentPK7"]);
            M_clientGroup.ParentPK8 = Convert.ToInt32(dr["ParentPK8"]);
            M_clientGroup.ParentPK9 = Convert.ToInt32(dr["ParentPK9"]);
            M_clientGroup.Depth = Convert.ToInt32(dr["Depth"]);
            M_clientGroup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_clientGroup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_clientGroup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_clientGroup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_clientGroup.EntryTime = dr["EntryTime"].ToString();
            M_clientGroup.UpdateTime = dr["UpdateTime"].ToString();
            M_clientGroup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_clientGroup.VoidTime = dr["VoidTime"].ToString();
            M_clientGroup.DBUserID = dr["DBUserID"].ToString();
            M_clientGroup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_clientGroup.LastUpdate = dr["LastUpdate"].ToString();
            M_clientGroup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_clientGroup;
        }

        public List<ClientGroup> ClientGroup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<ClientGroup> L_clientGroup = new List<ClientGroup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,ZA.ID ParentID,ZA.Name ParentName,* from ClientGroup A " +
                                " Left join ClientGroup ZA on A.ParentPK = ZA.ClientGroupPK and ZA.status in (1,2) " +
                                " where A.status = @status order by A.ClientGroupPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,ZA.ID ParentID,ZA.Name ParentName,* from ClientGroup A " +
                                " Left join ClientGroup ZA on A.ParentPK = ZA.ClientGroupPK and ZA.status in (1,2) " +
                                " order by A.ClientGroupPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_clientGroup.Add(setClientGroup(dr));
                                }
                            }
                            return L_clientGroup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ClientGroup_Add(ClientGroup _clientGroup, bool _havePrivillege)
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
                                 "Select isnull(max(ClientGroupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from ClientGroup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientGroup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                      
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(ClientGroupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from ClientGroup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _clientGroup.ID);
                        cmd.Parameters.AddWithValue("@Name", _clientGroup.Name);
                        cmd.Parameters.AddWithValue("@ClientGroupFee", _clientGroup.ClientGroupFee);
                        cmd.Parameters.AddWithValue("@Levels", _clientGroup.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _clientGroup.ParentPK);
                        cmd.Parameters.AddWithValue("@ParentPK1", _clientGroup.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _clientGroup.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _clientGroup.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _clientGroup.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _clientGroup.ParentPK5);
                        cmd.Parameters.AddWithValue("@ParentPK6", _clientGroup.ParentPK6);
                        cmd.Parameters.AddWithValue("@ParentPK7", _clientGroup.ParentPK7);
                        cmd.Parameters.AddWithValue("@ParentPK8", _clientGroup.ParentPK8);
                        cmd.Parameters.AddWithValue("@ParentPK9", _clientGroup.ParentPK9);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _clientGroup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ClientGroup");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ClientGroup_Update(ClientGroup _clientGroup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_clientGroup.ClientGroupPK, _clientGroup.HistoryPK, "ClientGroup");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClientGroup set status=2, Notes=@Notes,ID=@ID,Name=@Name,ClientGroupFee=@ClientGroupFee, " +
                                "Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where ClientGroupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientGroup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                            cmd.Parameters.AddWithValue("@Notes", _clientGroup.Notes);
                            cmd.Parameters.AddWithValue("@ID", _clientGroup.ID);
                            cmd.Parameters.AddWithValue("@Name", _clientGroup.Name);
                            cmd.Parameters.AddWithValue("@ClientGroupFee", _clientGroup.ClientGroupFee);
                            cmd.Parameters.AddWithValue("@Levels", _clientGroup.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _clientGroup.ParentPK);
                            cmd.Parameters.AddWithValue("@ParentPK1", _clientGroup.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _clientGroup.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _clientGroup.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _clientGroup.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _clientGroup.ParentPK5);
                            cmd.Parameters.AddWithValue("@ParentPK6", _clientGroup.ParentPK6);
                            cmd.Parameters.AddWithValue("@ParentPK7", _clientGroup.ParentPK7);
                            cmd.Parameters.AddWithValue("@ParentPK8", _clientGroup.ParentPK8);
                            cmd.Parameters.AddWithValue("@ParentPK9", _clientGroup.ParentPK9);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientGroup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientGroup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClientGroup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ClientGroupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _clientGroup.EntryUsersID);
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
                                cmd.CommandText = "Update ClientGroup set  Notes=@Notes,ID=@ID,Name=@Name,ClientGroupFee=@ClientGroupFee, " +
                                "Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where ClientGroupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientGroup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                                cmd.Parameters.AddWithValue("@Notes", _clientGroup.Notes);
                                cmd.Parameters.AddWithValue("@ID", _clientGroup.ID);
                                cmd.Parameters.AddWithValue("@Name", _clientGroup.Name);
                                cmd.Parameters.AddWithValue("@ClientGroupFee", _clientGroup.ClientGroupFee);
                                cmd.Parameters.AddWithValue("@Levels", _clientGroup.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _clientGroup.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _clientGroup.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _clientGroup.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _clientGroup.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _clientGroup.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _clientGroup.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _clientGroup.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _clientGroup.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _clientGroup.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _clientGroup.ParentPK9);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientGroup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_clientGroup.ClientGroupPK, "ClientGroup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ClientGroup where ClientGroupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientGroup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _clientGroup.ID);
                                cmd.Parameters.AddWithValue("@Name", _clientGroup.Name);
                                cmd.Parameters.AddWithValue("@ClientGroupFee", _clientGroup.ClientGroupFee);
                                cmd.Parameters.AddWithValue("@Levels", _clientGroup.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _clientGroup.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _clientGroup.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _clientGroup.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _clientGroup.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _clientGroup.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _clientGroup.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _clientGroup.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _clientGroup.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _clientGroup.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _clientGroup.ParentPK9);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientGroup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ClientGroup set status= 4,Notes=@Notes, " +
                                " lastupdate=@lastupdate where ClientGroupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _clientGroup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientGroup.HistoryPK);
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

        public void ClientGroup_Approved(ClientGroup _clientGroup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientGroup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where ClientGroupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _clientGroup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientGroup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClientGroup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ClientGroupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _clientGroup.ApprovedUsersID);
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

        public void ClientGroup_Reject(ClientGroup _clientGroup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientGroup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where ClientGroupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _clientGroup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _clientGroup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClientGroup set status= 2,LastUpdate=@LastUpdate where ClientGroupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
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

        public void ClientGroup_Void(ClientGroup _clientGroup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClientGroup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where ClientGroupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _clientGroup.ClientGroupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _clientGroup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _clientGroup.VoidUsersID);
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

        public List<ClientGroupCombo> ClientGroup_ComboRpt()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientGroupCombo> L_clientGroup = new List<ClientGroupCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT  ClientGroupPK,ID + ' - ' + Name as ID, Name FROM [ClientGroup]  where status = 2 union all select 0,'All', '' order by ClientGroupPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ClientGroupCombo M_clientGroup = new ClientGroupCombo();
                                    M_clientGroup.ClientGroupPK = Convert.ToInt32(dr["ClientGroupPK"]);
                                    M_clientGroup.ID = Convert.ToString(dr["ID"]);
                                    M_clientGroup.Name = Convert.ToString(dr["Name"]);
                                    L_clientGroup.Add(M_clientGroup);
                                }

                            }
                        }
                        return L_clientGroup;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<ClientGroupCombo> ClientGroup_Combo()
        {
   
             try
            {
                
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClientGroupCombo> L_clientGroup = new List<ClientGroupCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
               
              
                    cmd.CommandText = "SELECT  ClientGroupPK,ID + ' - ' + Name ID, Name FROM [ClientGroup]  where status = 2 order by ID,Name";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ClientGroupCombo M_clientGroup = new ClientGroupCombo();
                                M_clientGroup.ClientGroupPK = Convert.ToInt32(dr["ClientGroupPK"]);
                                M_clientGroup.ID = Convert.ToString(dr["ID"]);
                                M_clientGroup.Name = Convert.ToString(dr["Name"]);
                                L_clientGroup.Add(M_clientGroup);
                            }

                        }
                    }
                        return L_clientGroup;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            

        }

      

        public bool ClientGroup_UpdateParentAndDepth()
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE ClientGroup SET " +
                                                "ClientGroup.ParentPK1 = isnull(ClientGroup_1.ClientGroupPK,0), ClientGroup.ParentPK2 = isnull(ClientGroup_2.ClientGroupPK,0), " +
                                                "ClientGroup.ParentPK3 = isnull(ClientGroup_3.ClientGroupPK,0), ClientGroup.ParentPK4 = isnull(ClientGroup_4.ClientGroupPK,0), " +
                                                "ClientGroup.ParentPK5 = isnull(ClientGroup_5.ClientGroupPK,0), ClientGroup.ParentPK6 = isnull(ClientGroup_6.ClientGroupPK,0), " +
                                                "ClientGroup.ParentPK7 = isnull(ClientGroup_7.ClientGroupPK,0), ClientGroup.ParentPK8 = isnull(ClientGroup_8.ClientGroupPK,0), " +
                                                "ClientGroup.ParentPK9 = isnull(ClientGroup_9.ClientGroupPK,0)  " +
                                                "FROM ClientGroup " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_1 ON ClientGroup.ParentPK = ClientGroup_1.ClientGroupPK " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_2 ON ClientGroup_1.ParentPK = ClientGroup_2.ClientGroupPK " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_3 ON ClientGroup_2.ParentPK = ClientGroup_3.ClientGroupPK " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_4 ON ClientGroup_3.ParentPK = ClientGroup_4.ClientGroupPK " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_5 ON ClientGroup_4.ParentPK = ClientGroup_5.ClientGroupPK " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_6 ON ClientGroup_5.ParentPK = ClientGroup_6.ClientGroupPK " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_7 ON ClientGroup_6.ParentPK = ClientGroup_7.ClientGroupPK " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_8 ON ClientGroup_7.ParentPK = ClientGroup_8.ClientGroupPK " +
                                                "LEFT JOIN ClientGroup AS ClientGroup_9 ON ClientGroup_8.ParentPK = ClientGroup_9.ClientGroupPK Where ClientGroup.Status = 2 ";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select ClientGroupPK From ClientGroup Where Status = 2 and Groups = 1";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                try
                                {
                                    using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                                    {
                                        DbConSubQuery.Open();
                                        while (dr.Read())
                                        {
                                            using (SqlCommand cmdSubQuery = DbConSubQuery.CreateCommand())
                                            {
                                                cmdSubQuery.CommandText = "Update ClientGroup set Depth = @Depth, lastupdate=@lastupdate Where ClientGroupPK = @ClientGroupPK and Status = 2";
                                                cmdSubQuery.Parameters.AddWithValue("@Depth", GetClientGroupDepth(Convert.ToInt32(dr["ClientGroupPK"])));
                                                cmdSubQuery.Parameters.AddWithValue("@ClientGroupPK", Convert.ToInt32(dr["ClientGroupPK"]));
                                                cmdSubQuery.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                                cmdSubQuery.ExecuteNonQuery();
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
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private int GetClientGroupDepth(int _clientGroupPK)
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                    {
                        cmd.CommandText = "DECLARE @Depth INT,@Depth1 INT, @Depth2 INT, @Depth3 INT, @Depth4 INT, @Depth5 INT, " +
                                          "@Depth6 INT, @Depth7 INT, @Depth8 INT, @Depth9 INT, @Depth10 INT " +
                                          "SELECT @Depth1 = MAX(ClientGroup_1.ParentPK), @Depth2 = MAX(ClientGroup_2.ParentPK), " +
                                          "@Depth3 = MAX(ClientGroup_3.ParentPK), @Depth4 = MAX(ClientGroup_4.ParentPK), " +
                                          "@Depth5 = MAX(ClientGroup_5.ParentPK), @Depth6 = MAX(ClientGroup_6.ParentPK), " +
                                          "@Depth7 = MAX(ClientGroup_7.ParentPK), @Depth8 = MAX(ClientGroup_8.ParentPK), " +
                                          "@Depth9 = MAX(ClientGroup_9.ParentPK), @Depth10 = MAX(ClientGroup_10.ParentPK) " +
                                          "FROM ClientGroup AS ClientGroup_10 RIGHT JOIN (ClientGroup AS ClientGroup_9 " +
                                          "RIGHT JOIN (ClientGroup AS ClientGroup_8 RIGHT JOIN (ClientGroup AS ClientGroup_7 " +
                                          "RIGHT JOIN (ClientGroup AS ClientGroup_6 RIGHT JOIN (ClientGroup AS ClientGroup_5 " +
                                          "RIGHT JOIN (ClientGroup AS ClientGroup_4 RIGHT JOIN (ClientGroup AS ClientGroup_3 " +
                                          "RIGHT JOIN (ClientGroup AS ClientGroup_2 RIGHT JOIN (ClientGroup AS ClientGroup_1 " +
                                          "RIGHT JOIN ClientGroup ON ClientGroup_1.ParentPK = ClientGroup.ClientGroupPK) " +
                                          "ON ClientGroup_2.ParentPK = ClientGroup_1.ClientGroupPK) ON ClientGroup_3.ParentPK = ClientGroup_2.ClientGroupPK) " +
                                          "ON ClientGroup_4.ParentPK = ClientGroup_3.ClientGroupPK) ON ClientGroup_5.ParentPK = ClientGroup_4.ClientGroupPK) " +
                                          "ON ClientGroup_6.ParentPK = ClientGroup_5.ClientGroupPK) ON ClientGroup_7.ParentPK = ClientGroup_6.ClientGroupPK) " +
                                          "ON ClientGroup_8.ParentPK = ClientGroup_7.ClientGroupPK) ON ClientGroup_9.ParentPK = ClientGroup_8.ClientGroupPK) " +
                                          "ON ClientGroup_10.ParentPK = ClientGroup_9.ClientGroupPK  " +
                                          "WHERE ClientGroup.ClientGroupPK = @ClientGroupPK and ClientGroup.Status = 2 " +
                                          "IF @Depth1 IS NULL " +
                                          "SET @Depth = 0  " +
                                          "ELSE IF @Depth2 IS NULL " +
                                          "SET @Depth = 1 " +
                                          "ELSE IF @Depth3 IS NULL " +
                                          "SET @Depth = 2 " +
                                          "ELSE IF @Depth4 IS NULL " +
                                          "SET @Depth = 3 " +
                                          "ELSE IF @Depth5 IS NULL " +
                                          "SET @Depth = 4 " +
                                          "ELSE IF @Depth6 IS NULL " +
                                          "SET @Depth = 5 " +
                                          "ELSE IF @Depth7 IS NULL " +
                                          "SET @Depth = 6 " +
                                          "ELSE IF @Depth8 IS NULL " +
                                          "SET @Depth = 7 " +
                                          "ELSE IF @Depth9 IS NULL " +
                                          "SET @Depth = 8  " +
                                          "ELSE IF @Depth10 IS NULL " +
                                          "SET @Depth = 9  " +
                                          "ELSE " +
                                          "SET @Depth = 0 " +
                                          "select @depth depth";
                        cmd.Parameters.AddWithValue("@ClientGroupPK", _clientGroupPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["depth"]);
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



    }
}