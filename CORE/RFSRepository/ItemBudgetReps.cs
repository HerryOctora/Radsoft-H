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
    public class ItemBudgetReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[ItemBudget] " +
                            "([ItemBudgetPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private ItemBudget setItemBudget(SqlDataReader dr)
        {
            ItemBudget M_ItemBudget = new ItemBudget();
            M_ItemBudget.ItemBudgetPK = Convert.ToInt32(dr["ItemBudgetPK"]);
            M_ItemBudget.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_ItemBudget.Status = Convert.ToInt32(dr["Status"]);
            M_ItemBudget.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_ItemBudget.Notes = Convert.ToString(dr["Notes"]);
            M_ItemBudget.ID = dr["ID"].ToString();
            M_ItemBudget.Name = dr["Name"].ToString();
            M_ItemBudget.EntryUsersID = dr["EntryUsersID"].ToString();
            M_ItemBudget.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_ItemBudget.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_ItemBudget.VoidUsersID = dr["VoidUsersID"].ToString();
            M_ItemBudget.EntryTime = dr["EntryTime"].ToString();
            M_ItemBudget.UpdateTime = dr["UpdateTime"].ToString();
            M_ItemBudget.ApprovedTime = dr["ApprovedTime"].ToString();
            M_ItemBudget.VoidTime = dr["VoidTime"].ToString();
            M_ItemBudget.DBUserID = dr["DBUserID"].ToString();
            M_ItemBudget.DBTerminalID = dr["DBTerminalID"].ToString();
            M_ItemBudget.LastUpdate = dr["LastUpdate"].ToString();
            M_ItemBudget.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_ItemBudget;
        }

        public List<ItemBudget> ItemBudget_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ItemBudget> L_ItemBudget = new List<ItemBudget>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from ItemBudget where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from ItemBudget order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ItemBudget.Add(setItemBudget(dr));
                                }
                            }
                            return L_ItemBudget;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ItemBudget_Add(ItemBudget _ItemBudget, bool _havePrivillege)
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
                                 "Select isnull(max(ItemBudgetPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From ItemBudget";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _ItemBudget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(ItemBudgetPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From ItemBudget";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _ItemBudget.ID);
                        cmd.Parameters.AddWithValue("@Name", _ItemBudget.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _ItemBudget.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "ItemBudget");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int ItemBudget_Update(ItemBudget _ItemBudget, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_ItemBudget.ItemBudgetPK, _ItemBudget.HistoryPK, "ItemBudget");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ItemBudget set status=2, Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where ItemBudgetPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _ItemBudget.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                            cmd.Parameters.AddWithValue("@ID", _ItemBudget.ID);
                            cmd.Parameters.AddWithValue("@Notes", _ItemBudget.Notes);
                            cmd.Parameters.AddWithValue("@Name", _ItemBudget.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _ItemBudget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _ItemBudget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ItemBudget set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where ItemBudgetPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _ItemBudget.EntryUsersID);
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
                                cmd.CommandText = "Update ItemBudget set  Notes=@Notes,ID=@ID,Name=@Name," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where ItemBudgetPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _ItemBudget.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                                cmd.Parameters.AddWithValue("@ID", _ItemBudget.ID);
                                cmd.Parameters.AddWithValue("@Notes", _ItemBudget.Notes);
                                cmd.Parameters.AddWithValue("@Name", _ItemBudget.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _ItemBudget.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_ItemBudget.ItemBudgetPK, "ItemBudget");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ItemBudget where ItemBudgetPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _ItemBudget.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _ItemBudget.ID);
                                cmd.Parameters.AddWithValue("@Name", _ItemBudget.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _ItemBudget.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ItemBudget set status= 4,Notes=@Notes," +
                                    "lastupdate=@lastupdate where ItemBudgetPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _ItemBudget.Notes);
                                cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _ItemBudget.HistoryPK);
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

        public void ItemBudget_Approved(ItemBudget _ItemBudget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ItemBudget set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where ItemBudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ItemBudget.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _ItemBudget.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ItemBudget set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where ItemBudgetPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ItemBudget.ApprovedUsersID);
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

        public void ItemBudget_Reject(ItemBudget _ItemBudget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ItemBudget set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where ItemBudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ItemBudget.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ItemBudget.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ItemBudget set status= 2,lastupdate=@lastupdate where ItemBudgetPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
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

        public void ItemBudget_Void(ItemBudget _ItemBudget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ItemBudget set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where ItemBudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _ItemBudget.ItemBudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _ItemBudget.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _ItemBudget.VoidUsersID);
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

        public List<ItemBudgetCombo> ItemBudget_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ItemBudgetCombo> L_ItemBudget = new List<ItemBudgetCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  ItemBudgetPK,ID + ' - ' + Name as ID, Name FROM [ItemBudget]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ItemBudgetCombo M_ItemBudget = new ItemBudgetCombo();
                                    M_ItemBudget.ItemBudgetPK = Convert.ToInt32(dr["ItemBudgetPK"]);
                                    M_ItemBudget.ID = Convert.ToString(dr["ID"]);
                                    M_ItemBudget.Name = Convert.ToString(dr["Name"]);
                                    L_ItemBudget.Add(M_ItemBudget);
                                }
                            }
                            return L_ItemBudget;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<ItemBudgetCombo> ItemBudget_ComboRpt()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ItemBudgetCombo> L_ItemBudget = new List<ItemBudgetCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  ItemBudgetPK,ID + ' - ' + Name as ID, Name FROM [ItemBudget]  where status = 2 Union All Select 0 ItemBudgetPK,'ALL' ID,'' Name order by ItemBudgetPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ItemBudgetCombo M_ItemBudget = new ItemBudgetCombo();
                                    M_ItemBudget.ItemBudgetPK = Convert.ToInt32(dr["ItemBudgetPK"]);
                                    M_ItemBudget.ID = Convert.ToString(dr["ID"]);
                                    M_ItemBudget.Name = Convert.ToString(dr["Name"]);
                                    L_ItemBudget.Add(M_ItemBudget);
                                }
                            }
                            return L_ItemBudget;
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