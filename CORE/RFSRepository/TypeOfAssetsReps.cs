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
using OfficeOpenXml.Drawing;

namespace RFSRepository
{
    public class TypeOfAssetsReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[TypeOfAssets] " +
                            "([TypeOfAssetsPK],[HistoryPK],[Status],[ID],[Name],[FixedAssetAccountPK],[DepreciationExpAccountPK],[AccumulatedDeprAccountPK],[DepreciationPeriod],[PeriodUnit],";
        string _paramaterCommand = "@ID,@Name,@FixedAssetAccountPK,@DepreciationExpAccountPK,@AccumulatedDeprAccountPK,@DepreciationPeriod,@PeriodUnit,";
        private TypeOfAssets setTypeOfAssets(SqlDataReader dr)
        {
            TypeOfAssets M_TypeOfAssets = new TypeOfAssets();
            M_TypeOfAssets.TypeOfAssetsPK = Convert.ToInt32(dr["TypeOfAssetsPK"]);
            M_TypeOfAssets.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_TypeOfAssets.Status = Convert.ToInt32(dr["Status"]);
            //M_TypeOfAssets.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_TypeOfAssets.Notes = Convert.ToString(dr["Notes"]);

            M_TypeOfAssets.ID = Convert.ToString(dr["ID"]);
            M_TypeOfAssets.Name = Convert.ToString(dr["Name"]);
            M_TypeOfAssets.FixedAssetAccountPK = dr["FixedAssetAccountPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FixedAssetAccountPK"]);
            M_TypeOfAssets.FixedAssetAccountID = dr["FixedAssetAccountID"].ToString();
            M_TypeOfAssets.DepreciationExpAccountPK = dr["DepreciationExpAccountPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["DepreciationExpAccountPK"]);
            M_TypeOfAssets.DepreciationExpAccountID = dr["DepreciationExpAccountID"].ToString();
            M_TypeOfAssets.AccumulatedDeprAccountPK = dr["AccumulatedDeprAccountPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccumulatedDeprAccountPK"]);
            M_TypeOfAssets.AccumulatedDeprAccountID = dr["AccumulatedDeprAccountID"].ToString();
            M_TypeOfAssets.DepreciationPeriod = Convert.ToInt32(dr["DepreciationPeriod"]);
            M_TypeOfAssets.PeriodUnit = Convert.ToString(dr["PeriodUnit"]);
            M_TypeOfAssets.PeriodUnitDesc = Convert.ToString(dr["PeriodUnitDesc"]);


            M_TypeOfAssets.EntryUsersID = dr["EntryUsersID"].ToString();
            M_TypeOfAssets.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_TypeOfAssets.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_TypeOfAssets.VoidUsersID = dr["VoidUsersID"].ToString();
            M_TypeOfAssets.EntryTime = dr["EntryTime"].ToString();
            M_TypeOfAssets.UpdateTime = dr["UpdateTime"].ToString();
            M_TypeOfAssets.ApprovedTime = dr["ApprovedTime"].ToString();
            M_TypeOfAssets.VoidTime = dr["VoidTime"].ToString();
            M_TypeOfAssets.DBUserID = dr["DBUserID"].ToString();
            M_TypeOfAssets.DBTerminalID = dr["DBTerminalID"].ToString();
            M_TypeOfAssets.LastUpdate = dr["LastUpdate"].ToString();
            M_TypeOfAssets.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_TypeOfAssets;
        }

        public List<TypeOfAssets> TypeOfAssets_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TypeOfAssets> L_TypeOfAssets = new List<TypeOfAssets>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"select A.*,isnull(B1.ID,'')+ ' - '+isnull(B1.Name,'')   FixedAssetAccountID,isnull(B2.ID,'')+ '-'+isnull(B2.Name,'') DepreciationExpAccountID,isnull(B3.ID,'')+ '-'+isnull(B3.Name,'') AccumulatedDeprAccountID,MV.DescOne PeriodUnitDesc from TypeOfAssets A
                                                    left join Account B1 on A.FixedAssetAccountPK = B1.AccountPK and B1.Status in (1,2)
                                                    left join Account B2 on A.DepreciationExpAccountPK = B2.AccountPK and B2.Status in (1,2)
                                                    left join Account B3 on A.AccumulatedDeprAccountPK = B3.AccountPK  and B3.Status in (1,2)
													left join MasterValue MV on A.PeriodUnit = MV.Code and MV.Status = 2 and MV.ID = 'PeriodUnit'


                                                    where A.status = @status ";

                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @" select A.*,isnull(B1.ID,'')+ ' - '+isnull(B1.Name,'')   FixedAssetAccountID,isnull(B2.ID,'')+ '-'+isnull(B2.Name,'') DepreciationExpAccountID,isnull(B3.ID,'')+ '-'+isnull(B3.Name,'') AccumulatedDeprAccountID,MV.DescOne PeriodUnitDesc from TypeOfAssets A
                                                   left join Account B1 on A.FixedAssetAccountPK = B1.AccountPK and B1.Status in (1,2)
                                                    left join Account B2 on A.DepreciationExpAccountPK = B2.AccountPK and B2.Status in (1,2)
                                                    left join Account B3 on A.AccumulatedDeprAccountPK = B3.AccountPK  and B3.Status in (1,2)
													left join MasterValue MV on A.PeriodUnit = MV.Code and MV.Status = 2 and MV.ID = 'PeriodUnit'
                                                   ";

                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_TypeOfAssets.Add(setTypeOfAssets(dr));
                                }
                            }
                            return L_TypeOfAssets;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int TypeOfAssets_Add(TypeOfAssets _TypeOfAssets, bool _havePrivillege)
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
                                 "Select isnull(max(TypeOfAssetsPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from TypeOfAssets";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _TypeOfAssets.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(TypeOfAssetsPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from TypeOfAssets";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _TypeOfAssets.ID);
                        cmd.Parameters.AddWithValue("@Name", _TypeOfAssets.Name);
                        cmd.Parameters.AddWithValue("@FixedAssetAccountPK", _TypeOfAssets.FixedAssetAccountPK);
                        cmd.Parameters.AddWithValue("@DepreciationExpAccountPK", _TypeOfAssets.DepreciationExpAccountPK);
                        cmd.Parameters.AddWithValue("@AccumulatedDeprAccountPK", _TypeOfAssets.AccumulatedDeprAccountPK);
                        cmd.Parameters.AddWithValue("@DepreciationPeriod", _TypeOfAssets.DepreciationPeriod);
                        cmd.Parameters.AddWithValue("@PeriodUnit", _TypeOfAssets.PeriodUnit);
                       
                        cmd.Parameters.AddWithValue("@EntryUsersID", _TypeOfAssets.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "TypeOfAssets");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int TypeOfAssets_Update(TypeOfAssets _TypeOfAssets, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_TypeOfAssets.TypeOfAssetsPK, _TypeOfAssets.HistoryPK, "TypeOfAssets");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TypeOfAssets set status=2,Notes=@Notes," +
                                "ID=@ID,Name=@Name,FixedAssetAccountPK=@FixedAssetAccountPK,DepreciationExpAccountPK=@DepreciationExpAccountPK,AccumulatedDeprAccountPK=@AccumulatedDeprAccountPK,DepreciationPeriod=@DepreciationPeriod,PeriodUnit=@PeriodUnit" +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where TypeOfAssetsPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _TypeOfAssets.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
                            cmd.Parameters.AddWithValue("@Notes", _TypeOfAssets.Notes);
                            cmd.Parameters.AddWithValue("@ID", _TypeOfAssets.ID);
                            cmd.Parameters.AddWithValue("@Name", _TypeOfAssets.Name);
                            cmd.Parameters.AddWithValue("@FixedAssetAccountPK", _TypeOfAssets.FixedAssetAccountPK);
                            cmd.Parameters.AddWithValue("@DepreciationExpAccountPK", _TypeOfAssets.DepreciationExpAccountPK);
                            cmd.Parameters.AddWithValue("@AccumulatedDeprAccountPK", _TypeOfAssets.AccumulatedDeprAccountPK);
                            cmd.Parameters.AddWithValue("@DepreciationPeriod", _TypeOfAssets.DepreciationPeriod);
                            cmd.Parameters.AddWithValue("@PeriodUnit", _TypeOfAssets.PeriodUnit);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _TypeOfAssets.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            //cmd.Parameters.AddWithValue("@ApprovedUsersID", _TypeOfAssets.EntryUsersID);
                            //cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update TypeOfAssets set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where TypeOfAssetsPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _TypeOfAssets.EntryUsersID);
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
                                cmd.CommandText = "Update TypeOfAssets set Notes=@Notes," +
                                "ID=@ID,Name=@Name,FixedAssetAccountPK=@FixedAssetAccountPK,DepreciationExpAccountPK=@DepreciationExpAccountPK,AccumulatedDeprAccountPK=@AccumulatedDeprAccountPK,DepreciationPeriod=@DepreciationPeriod,PeriodUnit=@PeriodUnit," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where TypeOfAssetsPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _TypeOfAssets.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);

                                cmd.Parameters.AddWithValue("@Notes", _TypeOfAssets.Notes);
                                cmd.Parameters.AddWithValue("@ID", _TypeOfAssets.ID);
                                cmd.Parameters.AddWithValue("@Name", _TypeOfAssets.Name);
                                cmd.Parameters.AddWithValue("@FixedAssetAccountPK", _TypeOfAssets.FixedAssetAccountPK);
                                cmd.Parameters.AddWithValue("@DepreciationExpAccountPK", _TypeOfAssets.DepreciationExpAccountPK);
                                cmd.Parameters.AddWithValue("@AccumulatedDeprAccountPK", _TypeOfAssets.AccumulatedDeprAccountPK);
                                cmd.Parameters.AddWithValue("@DepreciationPeriod", _TypeOfAssets.DepreciationPeriod);
                                cmd.Parameters.AddWithValue("@PeriodUnit", _TypeOfAssets.PeriodUnit);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TypeOfAssets.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_TypeOfAssets.TypeOfAssetsPK, "TypeOfAssets");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From TypeOfAssets where TypeOfAssetsPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TypeOfAssets.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _TypeOfAssets.ID);
                                cmd.Parameters.AddWithValue("@Name", _TypeOfAssets.Name);
                                cmd.Parameters.AddWithValue("@FixedAssetAccountPK", _TypeOfAssets.FixedAssetAccountPK);
                                cmd.Parameters.AddWithValue("@DepreciationExpAccountPK", _TypeOfAssets.DepreciationExpAccountPK);
                                cmd.Parameters.AddWithValue("@AccumulatedDeprAccountPK", _TypeOfAssets.AccumulatedDeprAccountPK);
                                cmd.Parameters.AddWithValue("@DepreciationPeriod", _TypeOfAssets.DepreciationPeriod);
                                cmd.Parameters.AddWithValue("@PeriodUnit", _TypeOfAssets.PeriodUnit);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _TypeOfAssets.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update TypeOfAssets set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where TypeOfAssetsPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _TypeOfAssets.Notes);
                                cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _TypeOfAssets.HistoryPK);
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

        public void TypeOfAssets_Approved(TypeOfAssets _TypeOfAssets)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TypeOfAssets set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where TypeOfAssetsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TypeOfAssets.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _TypeOfAssets.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TypeOfAssets set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where TypeOfAssetsPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TypeOfAssets.ApprovedUsersID);
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
        public void TypeOfAssets_Reject(TypeOfAssets _TypeOfAssets)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TypeOfAssets set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where TypeOfAssetsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TypeOfAssets.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TypeOfAssets.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update TypeOfAssets set status= 2,lastupdate=@lastupdate where TypeOfAssetsPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
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
        public void TypeOfAssets_Void(TypeOfAssets _TypeOfAssets)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update TypeOfAssets set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where TypeOfAssetsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _TypeOfAssets.TypeOfAssetsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _TypeOfAssets.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _TypeOfAssets.VoidUsersID);
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
        private TypeOfAssets setTypeOfAssetsCombo(SqlDataReader dr)
        {
            TypeOfAssets M_TypeOfAssets = new TypeOfAssets();
            M_TypeOfAssets.FixedAssetAccountPK = Convert.ToInt32(dr["FixedAssetAccountPK"]);
            M_TypeOfAssets.DepreciationExpAccountPK = Convert.ToInt32(dr["DepreciationExpAccountPK"]);
            M_TypeOfAssets.AccumulatedDeprAccountPK = Convert.ToInt32(dr["AccumulatedDeprAccountPK"]);
            M_TypeOfAssets.DepreciationPeriod = Convert.ToInt32(dr["DepreciationPeriod"]);
            M_TypeOfAssets.PeriodUnit = Convert.ToString(dr["PeriodUnit"]);
            return M_TypeOfAssets;
        }

        public TypeOfAssets Get_InformationTypeOfAssets(int _TypeOfAssetsPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select FixedAssetAccountPK,DepreciationExpAccountPK,AccumulatedDeprAccountPK,DepreciationPeriod,PeriodUnit from TypeOfAssets   
                             where TypeOfAssetsPK = @TypeOfAssetsPK    ";
                        cmd.Parameters.AddWithValue("@TypeOfAssetsPK", _TypeOfAssetsPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setTypeOfAssetsCombo(dr);
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