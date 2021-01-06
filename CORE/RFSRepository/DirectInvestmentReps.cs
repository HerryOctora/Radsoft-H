using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class DirectInvestmentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[DirectInvestment] " +
                            "([DirectInvestmentPK],[HistoryPK],[Status],[ProjectName],[ClientName],[AcqValue],[AcqSharePercentage],[StatusProject],[Remarks],";
        string _paramaterCommand = "@ProjectName,@ClientName,@AcqValue,@AcqSharePercentage,@StatusProject,@Remarks,";


        private DirectInvestment setDirectInvestment(SqlDataReader dr)
        {
            DirectInvestment M_DirectInvestment = new DirectInvestment();
            M_DirectInvestment.DirectInvestmentPK = Convert.ToInt32(dr["DirectInvestmentPK"]);
            M_DirectInvestment.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_DirectInvestment.Status = Convert.ToInt32(dr["Status"]);
            M_DirectInvestment.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_DirectInvestment.Notes = Convert.ToString(dr["Notes"]);
            M_DirectInvestment.ProjectName = dr["ProjectName"].ToString();
            M_DirectInvestment.ClientName = dr["ClientName"].ToString();
            M_DirectInvestment.AcqValue = Convert.ToDecimal(dr["AcqValue"]);
            M_DirectInvestment.AcqSharePercentage = Convert.ToDecimal(dr["AcqSharePercentage"]);
            M_DirectInvestment.StatusProject = Convert.ToInt32(dr["StatusProject"]);
            M_DirectInvestment.Remarks = dr["Remarks"].ToString();
            M_DirectInvestment.EntryUsersID = dr["EntryUsersID"].ToString();
            M_DirectInvestment.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_DirectInvestment.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_DirectInvestment.VoidUsersID = dr["VoidUsersID"].ToString();
            M_DirectInvestment.EntryTime = dr["EntryTime"].ToString();
            M_DirectInvestment.UpdateTime = dr["UpdateTime"].ToString();
            M_DirectInvestment.ApprovedTime = dr["ApprovedTime"].ToString();
            M_DirectInvestment.VoidTime = dr["VoidTime"].ToString();
            M_DirectInvestment.DBUserID = dr["DBUserID"].ToString();
            M_DirectInvestment.DBTerminalID = dr["DBTerminalID"].ToString();
            M_DirectInvestment.LastUpdate = dr["LastUpdate"].ToString();
            M_DirectInvestment.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_DirectInvestment;
        }

        public List<DirectInvestment> DirectInvestment_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DirectInvestment> L_DirectInvestment = new List<DirectInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from DirectInvestment where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from DirectInvestment order by ProjectName";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_DirectInvestment.Add(setDirectInvestment(dr));
                                }
                            }
                            return L_DirectInvestment;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public DirectInvestmentAddNew DirectInvestment_Add(DirectInvestment _DirectInvestment, bool _havePrivillege)
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
                            cmd.CommandText = "Declare @newDirectInvestmentPK int \n " +
                                 "Select @newDirectInvestmentPK = isnull(max(DirectInvestmentPk),0) + 1 from DirectInvestment \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate) " +
                                 "Select @newDirectInvestmentPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newDirectInvestmentPK newDirectInvestmentPK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _DirectInvestment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newDirectInvestmentPK int \n " +
                                 "Select @newDirectInvestmentPK = isnull(max(DirectInvestmentPk),0) + 1 from DirectInvestment \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newDirectInvestmentPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newDirectInvestmentPK newDirectInvestmentPK, 1 newHistoryPK ";
                        }


                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);

                        cmd.Parameters.AddWithValue("@ProjectName", _DirectInvestment.ProjectName);
                        cmd.Parameters.AddWithValue("@ClientName", _DirectInvestment.ClientName);
                        cmd.Parameters.AddWithValue("@AcqValue", _DirectInvestment.AcqValue);
                        cmd.Parameters.AddWithValue("@AcqSharePercentage", _DirectInvestment.AcqSharePercentage);
                        cmd.Parameters.AddWithValue("@StatusProject", _DirectInvestment.StatusProject);
                        cmd.Parameters.AddWithValue("@Remarks", _DirectInvestment.Remarks);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _DirectInvestment.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new DirectInvestmentAddNew()
                                {
                                    DirectInvestmentPK = Convert.ToInt32(dr["newDirectInvestmentPK"]),
                                    HistoryPK = Convert.ToInt32(dr["newHistoryPK"]),
                                    Message = "Insert DirectInvestment Header Success"
                                };
                            }
                            else
                            {
                                return null;
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

        public int DirectInvestment_Update(DirectInvestment _DirectInvestment, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_DirectInvestment.DirectInvestmentPK, _DirectInvestment.HistoryPK, "DirectInvestment");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update DirectInvestment set status=2, Notes=@Notes,ProjectName=@ProjectName,ClientName=@ClientName,AcqValue=@AcqValue,AcqSharePercentage=@AcqSharePercentage,StatusProject=@StatusProject,Remarks=@Remarks," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where DirectInvestmentPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _DirectInvestment.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                            cmd.Parameters.AddWithValue("@Notes", _DirectInvestment.Notes);

                            cmd.Parameters.AddWithValue("@ProjectName", _DirectInvestment.ProjectName);
                            cmd.Parameters.AddWithValue("@ClientName", _DirectInvestment.ClientName);
                            cmd.Parameters.AddWithValue("@AcqValue", _DirectInvestment.AcqValue);
                            cmd.Parameters.AddWithValue("@AcqSharePercentage", _DirectInvestment.AcqSharePercentage);
                            cmd.Parameters.AddWithValue("@StatusProject", _DirectInvestment.StatusProject);
                            cmd.Parameters.AddWithValue("@Remarks", _DirectInvestment.Remarks);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _DirectInvestment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _DirectInvestment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update DirectInvestment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where DirectInvestmentPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _DirectInvestment.EntryUsersID);
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
                                cmd.CommandText = "Update DirectInvestment set Notes=@Notes,ProjectName=@ProjectName,ClientName=@ClientName,AcqValue=@AcqValue,AcqSharePercentage=@AcqSharePercentage,StatusProject=@StatusProject,Remarks=@Remarks," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where DirectInvestmentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _DirectInvestment.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                                cmd.Parameters.AddWithValue("@Notes", _DirectInvestment.Notes);

                                cmd.Parameters.AddWithValue("@ProjectName", _DirectInvestment.ProjectName);
                                cmd.Parameters.AddWithValue("@ClientName", _DirectInvestment.ClientName);
                                cmd.Parameters.AddWithValue("@AcqValue", _DirectInvestment.AcqValue);
                                cmd.Parameters.AddWithValue("@AcqSharePercentage", _DirectInvestment.AcqSharePercentage);
                                cmd.Parameters.AddWithValue("@StatusProject", _DirectInvestment.StatusProject);
                                cmd.Parameters.AddWithValue("@Remarks", _DirectInvestment.Remarks);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _DirectInvestment.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_DirectInvestment.DirectInvestmentPK, "DirectInvestment");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From DirectInvestment where DirectInvestmentPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _DirectInvestment.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);

                                cmd.Parameters.AddWithValue("@ProjectName", _DirectInvestment.ProjectName);
                                cmd.Parameters.AddWithValue("@ClientName", _DirectInvestment.ClientName);
                                cmd.Parameters.AddWithValue("@AcqValue", _DirectInvestment.AcqValue);
                                cmd.Parameters.AddWithValue("@AcqSharePercentage", _DirectInvestment.AcqSharePercentage);
                                cmd.Parameters.AddWithValue("@StatusProject", _DirectInvestment.StatusProject);
                                cmd.Parameters.AddWithValue("@Remarks", _DirectInvestment.Remarks);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _DirectInvestment.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update DirectInvestment set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where DirectInvestmentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _DirectInvestment.DirectInvestmentPK);
                                cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _DirectInvestment.HistoryPK);
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

        public void DirectInvestment_Approved(DirectInvestment _DirectInvestment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DirectInvestment set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where DirectInvestmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _DirectInvestment.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _DirectInvestment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update DirectInvestment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where DirectInvestmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _DirectInvestment.ApprovedUsersID);
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

        public void DirectInvestment_Reject(DirectInvestment _DirectInvestment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DirectInvestment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where DirectInvestmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _DirectInvestment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _DirectInvestment.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update DirectInvestment set status= 2,LastUpdate=@LastUpdate where DirectInvestmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
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

        public void DirectInvestment_Void(DirectInvestment _DirectInvestment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DirectInvestment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where DirectInvestmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _DirectInvestment.DirectInvestmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _DirectInvestment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _DirectInvestment.VoidUsersID);
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



        public bool CheckDirectInvestmentStatus(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from EquityDirectInvestment where DirectInvestmentPK = @PK and Status = 1";
                        cmd.Parameters.AddWithValue("@PK", _pk);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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
