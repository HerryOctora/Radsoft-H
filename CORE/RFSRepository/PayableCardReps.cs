using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class PayableCardReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[PayableCard] " +
                            "([PayableCardPK],[HistoryPK],[Status],[ConsigneePK],[PoNo],[Description],[TotalAmount],[TermDesc1],[AmountTerm1],[TermDesc2],[AmountTerm2],[TermDesc3],[AmountTerm3],[TermDesc4],[AmountTerm4],[TermDesc5],[AmountTerm5],[TotalPaid],";
        string _paramaterCommand = "@ConsigneePK,@PoNo,@Description,@TotalAmount,@TermDesc1,@AmountTerm1,@TermDesc2,@AmountTerm2,@TermDesc3,@AmountTerm3,@TermDesc4,@AmountTerm4,@TermDesc5,@AmountTerm5,@TotalPaid,";

        //2
        private PayableCard setPayableCard(SqlDataReader dr)
        {
            PayableCard M_PayableCard = new PayableCard();
            M_PayableCard.PayableCardPK = Convert.ToInt32(dr["PayableCardPK"]);
            M_PayableCard.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_PayableCard.Status = Convert.ToInt32(dr["Status"]);
            M_PayableCard.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_PayableCard.Notes = Convert.ToString(dr["Notes"]);

            M_PayableCard.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
            M_PayableCard.ConsigneeID = Convert.ToString(dr["ConsigneeID"]);
            M_PayableCard.PoNo = Convert.ToString(dr["PoNo"]);
            M_PayableCard.Description = Convert.ToString(dr["Description"]);
            M_PayableCard.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
            M_PayableCard.TermDesc1 = Convert.ToString(dr["TermDesc1"]);
            M_PayableCard.AmountTerm1 = Convert.ToDecimal(dr["AmountTerm1"]);
            M_PayableCard.TermDesc2 = Convert.ToString(dr["TermDesc2"]);
            M_PayableCard.AmountTerm2 = Convert.ToDecimal(dr["AmountTerm2"]);
            M_PayableCard.TermDesc3 = Convert.ToString(dr["TermDesc3"]);
            M_PayableCard.AmountTerm3 = Convert.ToDecimal(dr["AmountTerm3"]);
            M_PayableCard.TermDesc4 = Convert.ToString(dr["TermDesc4"]);
            M_PayableCard.AmountTerm4 = Convert.ToDecimal(dr["AmountTerm4"]);
            M_PayableCard.TermDesc5 = Convert.ToString(dr["TermDesc5"]);
            M_PayableCard.AmountTerm5 = Convert.ToDecimal(dr["AmountTerm5"]);
            M_PayableCard.TotalPaid = Convert.ToDecimal(dr["TotalPaid"]);
            M_PayableCard.EntryUsersID = dr["EntryUsersID"].ToString();
            M_PayableCard.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_PayableCard.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_PayableCard.VoidUsersID = dr["VoidUsersID"].ToString();
            M_PayableCard.EntryTime = dr["EntryTime"].ToString();
            M_PayableCard.UpdateTime = dr["UpdateTime"].ToString();
            M_PayableCard.ApprovedTime = dr["ApprovedTime"].ToString();
            M_PayableCard.VoidTime = dr["VoidTime"].ToString();
            M_PayableCard.DBUserID = dr["DBUserID"].ToString();
            M_PayableCard.DBTerminalID = dr["DBTerminalID"].ToString();
            M_PayableCard.LastUpdate = dr["LastUpdate"].ToString();
            M_PayableCard.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_PayableCard;
        }

        public List<PayableCard> PayableCard_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<PayableCard> L_PayableCard = new List<PayableCard>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when P.status=1 then 'PENDING' else Case When P.status = 2 then 'APPROVED' else Case when P.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,C.ID ConsigneeID, * from PayableCard P " +
                                                "left join Consignee C on P.ConsigneePK = C.ConsigneePK and C.status = 2 " +
                                                "where P.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when P.status=1 then 'PENDING' else Case When P.status = 2 then 'APPROVED' else Case when P.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,C.ID ConsigneeID, * from PayableCard P " +
                                              "left join Consignee C on P.ConsigneePK = C.ConsigneePK and C.status = 2 " +
                                              "order by PayableCardPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_PayableCard.Add(setPayableCard(dr));
                                }
                            }
                            return L_PayableCard;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int PayableCard_Add(PayableCard _PayableCard, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(PayableCardPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from PayableCard";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _PayableCard.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(PayableCardPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from PayableCard";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ConsigneePK", _PayableCard.ConsigneePK);
                        cmd.Parameters.AddWithValue("@PoNo", _PayableCard.PoNo);
                        cmd.Parameters.AddWithValue("@Description", _PayableCard.Description);
                        cmd.Parameters.AddWithValue("@TotalAmount", _PayableCard.TotalAmount);
                        cmd.Parameters.AddWithValue("@TermDesc1", _PayableCard.TermDesc1);
                        cmd.Parameters.AddWithValue("@AmountTerm1", _PayableCard.AmountTerm1);
                        cmd.Parameters.AddWithValue("@TermDesc2", _PayableCard.TermDesc2);
                        cmd.Parameters.AddWithValue("@AmountTerm2", _PayableCard.AmountTerm2);
                        cmd.Parameters.AddWithValue("@TermDesc3", _PayableCard.TermDesc3);
                        cmd.Parameters.AddWithValue("@AmountTerm3", _PayableCard.AmountTerm3);
                        cmd.Parameters.AddWithValue("@TermDesc4", _PayableCard.TermDesc4);
                        cmd.Parameters.AddWithValue("@AmountTerm4", _PayableCard.AmountTerm4);
                        cmd.Parameters.AddWithValue("@TermDesc5", _PayableCard.TermDesc5);
                        cmd.Parameters.AddWithValue("@AmountTerm5", _PayableCard.AmountTerm5);
                        cmd.Parameters.AddWithValue("@TotalPaid", _PayableCard.TotalPaid);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _PayableCard.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "PayableCard");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int PayableCard_Update(PayableCard _PayableCard, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_PayableCard.PayableCardPK, _PayableCard.HistoryPK, "PayableCard"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update PayableCard set status=2, Notes=@Notes,ConsigneePK=@ConsigneePK,PoNo=@PoNo,Description=@Description,TotalAmount=@TotalAmount,TermDesc1=@TermDesc1,AmountTerm1=@AmountTerm1,TermDesc2=@TermDesc2,AmountTerm2=@AmountTerm2,TermDesc3=@TermDesc3,AmountTerm3=@AmountTerm3,TermDesc4=@TermDesc4,AmountTerm4=@AmountTerm4,TermDesc5=@TermDesc5,AmountTerm5=@AmountTerm5,TotalPaid=@TotalPaid," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where PayableCardPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _PayableCard.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                            cmd.Parameters.AddWithValue("@Notes", _PayableCard.Notes);
                            cmd.Parameters.AddWithValue("@ConsigneePK", _PayableCard.ConsigneePK);
                            cmd.Parameters.AddWithValue("@PoNo", _PayableCard.PoNo);
                            cmd.Parameters.AddWithValue("@Description", _PayableCard.Description);
                            cmd.Parameters.AddWithValue("@TotalAmount", _PayableCard.TotalAmount);
                            cmd.Parameters.AddWithValue("@TermDesc1", _PayableCard.TermDesc1);
                            cmd.Parameters.AddWithValue("@AmountTerm1", _PayableCard.AmountTerm1);
                            cmd.Parameters.AddWithValue("@TermDesc2", _PayableCard.TermDesc2);
                            cmd.Parameters.AddWithValue("@AmountTerm2", _PayableCard.AmountTerm2);
                            cmd.Parameters.AddWithValue("@TermDesc3", _PayableCard.TermDesc3);
                            cmd.Parameters.AddWithValue("@AmountTerm3", _PayableCard.AmountTerm3);
                            cmd.Parameters.AddWithValue("@TermDesc4", _PayableCard.TermDesc4);
                            cmd.Parameters.AddWithValue("@AmountTerm4", _PayableCard.AmountTerm4);
                            cmd.Parameters.AddWithValue("@TermDesc5", _PayableCard.TermDesc5);
                            cmd.Parameters.AddWithValue("@AmountTerm5", _PayableCard.AmountTerm5);
                            cmd.Parameters.AddWithValue("@TotalPaid", _PayableCard.TotalPaid);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _PayableCard.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _PayableCard.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update PayableCard set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where PayableCardPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _PayableCard.EntryUsersID);
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
                                cmd.CommandText = "Update PayableCard set Notes=@Notes,ConsigneePK=@ConsigneePK,PoNo=@PoNo,Description=@Description,TotalAmount=@TotalAmount,TermDesc1=@TermDesc1,AmountTerm1=@AmountTerm1,TermDesc2=@TermDesc2,AmountTerm2=@AmountTerm2,TermDesc3=@TermDesc3,AmountTerm3=@AmountTerm3,TermDesc4=@TermDesc4,AmountTerm4=@AmountTerm4,TermDesc5=@TermDesc5,AmountTerm5=@AmountTerm5,TotalPaid=@TotalPaid," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where PayableCardPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _PayableCard.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                                cmd.Parameters.AddWithValue("@Notes", _PayableCard.Notes);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _PayableCard.ConsigneePK);
                                cmd.Parameters.AddWithValue("@PoNo", _PayableCard.PoNo);
                                cmd.Parameters.AddWithValue("@Description", _PayableCard.Description);
                                cmd.Parameters.AddWithValue("@TotalAmount", _PayableCard.TotalAmount);
                                cmd.Parameters.AddWithValue("@TermDesc1", _PayableCard.TermDesc1);
                                cmd.Parameters.AddWithValue("@AmountTerm1", _PayableCard.AmountTerm1);
                                cmd.Parameters.AddWithValue("@TermDesc2", _PayableCard.TermDesc2);
                                cmd.Parameters.AddWithValue("@AmountTerm2", _PayableCard.AmountTerm2);
                                cmd.Parameters.AddWithValue("@TermDesc3", _PayableCard.TermDesc3);
                                cmd.Parameters.AddWithValue("@AmountTerm3", _PayableCard.AmountTerm3);
                                cmd.Parameters.AddWithValue("@TermDesc4", _PayableCard.TermDesc4);
                                cmd.Parameters.AddWithValue("@AmountTerm4", _PayableCard.AmountTerm4);
                                cmd.Parameters.AddWithValue("@TermDesc5", _PayableCard.TermDesc5);
                                cmd.Parameters.AddWithValue("@AmountTerm5", _PayableCard.AmountTerm5);
                                cmd.Parameters.AddWithValue("@TotalPaid", _PayableCard.TotalPaid);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _PayableCard.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_PayableCard.PayableCardPK, "PayableCard");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From PayableCard where PayableCardPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _PayableCard.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _PayableCard.ConsigneePK);
                                cmd.Parameters.AddWithValue("@PoNo", _PayableCard.PoNo);
                                cmd.Parameters.AddWithValue("@Description", _PayableCard.Description);
                                cmd.Parameters.AddWithValue("@TotalAmount", _PayableCard.TotalAmount);
                                cmd.Parameters.AddWithValue("@TermDesc1", _PayableCard.TermDesc1);
                                cmd.Parameters.AddWithValue("@AmountTerm1", _PayableCard.AmountTerm1);
                                cmd.Parameters.AddWithValue("@TermDesc2", _PayableCard.TermDesc2);
                                cmd.Parameters.AddWithValue("@AmountTerm2", _PayableCard.AmountTerm2);
                                cmd.Parameters.AddWithValue("@TermDesc3", _PayableCard.TermDesc3);
                                cmd.Parameters.AddWithValue("@AmountTerm3", _PayableCard.AmountTerm3);
                                cmd.Parameters.AddWithValue("@TermDesc4", _PayableCard.TermDesc4);
                                cmd.Parameters.AddWithValue("@AmountTerm4", _PayableCard.AmountTerm4);
                                cmd.Parameters.AddWithValue("@TermDesc5", _PayableCard.TermDesc5);
                                cmd.Parameters.AddWithValue("@AmountTerm5", _PayableCard.AmountTerm5);
                                cmd.Parameters.AddWithValue("@TotalPaid", _PayableCard.TotalPaid);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _PayableCard.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update PayableCard set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where PayableCardPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _PayableCard.Notes);
                                cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _PayableCard.HistoryPK);
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

        public void PayableCard_Approved(PayableCard _PayableCard)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update PayableCard set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where PayableCardPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                        cmd.Parameters.AddWithValue("@historyPK", _PayableCard.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _PayableCard.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update PayableCard set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where PayableCardPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _PayableCard.ApprovedUsersID);
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

        public void PayableCard_Reject(PayableCard _PayableCard)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update PayableCard set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where PayableCardPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                        cmd.Parameters.AddWithValue("@historyPK", _PayableCard.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _PayableCard.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update PayableCard set status= 2,LastUpdate=@LastUpdate  where PayableCardPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
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

        public void PayableCard_Void(PayableCard _PayableCard)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update PayableCard set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where PayableCardPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _PayableCard.PayableCardPK);
                        cmd.Parameters.AddWithValue("@historyPK", _PayableCard.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _PayableCard.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

    }
}