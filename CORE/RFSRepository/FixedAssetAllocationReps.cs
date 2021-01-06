using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class FixedAssetAllocationReps
    {
        Host _host = new Host();
        string _insertCommand = "INSERT INTO [dbo].[FixedAssetAllocation] " +
                            "([FixedAssetPK],[HistoryPK],[AutoNo],[Status],[DepartmentPK],[AllocationPercent],[LastUsersID],[LastUpdate]) ";
        string _paramaterCommand = "@DepartmentPK,@AllocationPercent,@LastUsersID,@LastUpdate ";

        private FixedAssetAllocation setFixedAsset(SqlDataReader dr)
        {
            FixedAssetAllocation M_FixedAsset = new FixedAssetAllocation();
            M_FixedAsset.FixedAssetPK = Convert.ToInt32(dr["FixedAssetPK"]);
            M_FixedAsset.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FixedAsset.Status = Convert.ToInt32(dr["Status"]);
            M_FixedAsset.AutoNo = Convert.ToInt32(dr["AutoNo"]);
            M_FixedAsset.Notes = Convert.ToString(dr["Notes"]);
            M_FixedAsset.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_FixedAsset.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_FixedAsset.AllocationPercent = Convert.ToDecimal(dr["AllocationPercent"]);
            M_FixedAsset.LastUsersID = dr["LastUsersID"].ToString();
            M_FixedAsset.LastUpdate = dr["LastUpdate"].ToString();
            M_FixedAsset.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FixedAsset;
        }

        public List<FixedAssetAllocation> FixedAssetAllocation_Select(int _status, int _FixedAssetPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FixedAssetAllocation> L_FixedAssetAllocation = new List<FixedAssetAllocation>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select B.ID DepartmentID,* from FixedAssetAllocation A Left join Department B "+
                             "On A.DepartmentPK = B.DepartmentPK and B.status = 2 Where FixedAssetPK = @FixedAssetPK and A.Status = @Status " +
                             "order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@FixedAssetPK", _FixedAssetPK);
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from FixedAssetAllocation Where FixedAssetPK = @FixedAssetPK order by AutoNo Asc ";
                            cmd.Parameters.AddWithValue("@FixedAssetPK", _FixedAssetPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FixedAssetAllocation.Add(setFixedAsset(dr));
                                }
                            }
                            return L_FixedAssetAllocation;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FixedAssetAllocation_Add(FixedAssetAllocation _FixedAssetAllocation)
        {
            try
            {
                int _autoNo = 0;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        _autoNo = _host.Get_DetailNewAutoNo(_FixedAssetAllocation.FixedAssetPK, "FixedAssetAllocation", "FixedAssetPK");
                        cmd.CommandText = " If Not Exists(select * from FixedAssetAllocation where FixedAssetPk = @FixedAssetPK and DepartmentPk = @DepartmentPK ) BEGIN " +
                                 _insertCommand +
                                 " Select @FixedAssetPK,1,isnull(Max(AutoNo),0) + 1,@status," + _paramaterCommand + " From FixedAssetAllocation " +
                                 " Where FixedAssetPK = @FixedAssetPK END ELSE BEGIN Update FixedAssetAllocation set AllocationPercent = @AllocationPercent Where FixedAssetPK = @FixedAssetPK and DepartmentPK = @DepartmentPK END";
                        cmd.Parameters.AddWithValue("@FixedAssetPK", _FixedAssetAllocation.FixedAssetPK);
                        cmd.Parameters.AddWithValue("@AllocationPercent", _FixedAssetAllocation.AllocationPercent);
                        cmd.Parameters.AddWithValue("@status", _FixedAssetAllocation.Status);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _FixedAssetAllocation.DepartmentPK);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FixedAssetAllocation.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                        return _autoNo;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FixedAssetAllocation_Update(FixedAssetAllocation _FixedAssetAllocation, bool _havePrivillege)
        {
            try
            {
                DateTime _now = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "Update FixedAssetAllocation " +
                            "Set DepartmentPK = @DepartmentPK,AllocationPerccent = @AllocationPercent,LastUsersID = @LastUsersID,LastUpdate @LastUpdate " +
                            "Where FixedAssetPK = @FixedAssetPK and AutoNo = @AutoNo ";

                        cmd.Parameters.AddWithValue("@DepartmentPK", _FixedAssetAllocation.DepartmentPK);
                        cmd.Parameters.AddWithValue("@AllocationPercent", _FixedAssetAllocation.AllocationPercent);
    
                    cmd.Parameters.AddWithValue("@FixedAssetPK", _FixedAssetAllocation.FixedAssetPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _FixedAssetAllocation.AutoNo);
                        cmd.Parameters.AddWithValue("@LastUsersID", _FixedAssetAllocation.LastUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _now);

                        cmd.ExecuteNonQuery();
                    }
                    return 0;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FixedAssetAllocation_Delete(FixedAssetAllocation _FixedAssetAllocation)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "delete FixedAssetAllocation where FixedAssetPK = @FixedAssetPK and AutoNo = @AutoNo ";
                        cmd.Parameters.AddWithValue("@FixedAssetPK", _FixedAssetAllocation.FixedAssetPK);
                        cmd.Parameters.AddWithValue("@AutoNo", _FixedAssetAllocation.AutoNo);
                        cmd.ExecuteNonQuery();
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