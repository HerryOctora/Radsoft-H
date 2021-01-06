using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;


namespace RFSRepository
{
    public class InstrumentIndexReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[InstrumentIndex] " +
                            "([InstrumentIndexPK],[HistoryPK],[Status],[Date],[InstrumentPK],[IndexPK],[SharesForIndex],[LastPrice],[MarketCap],[Weight],";
        string _paramaterCommand = "@Date,@InstrumentPK,@IndexPK,@SharesForIndex,@LastPrice,@MarketCap,@Weight,";

        //2
        private InstrumentIndex setInstrumentIndex(SqlDataReader dr)
        {
            InstrumentIndex M_InstrumentIndex = new InstrumentIndex();
            M_InstrumentIndex.InstrumentIndexPK = Convert.ToInt32(dr["InstrumentIndexPK"]);
            M_InstrumentIndex.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_InstrumentIndex.Status = Convert.ToInt32(dr["Status"]);
            M_InstrumentIndex.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_InstrumentIndex.Notes = Convert.ToString(dr["Notes"]);
            M_InstrumentIndex.Date = Convert.ToString(dr["Date"]);
            M_InstrumentIndex.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_InstrumentIndex.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_InstrumentIndex.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_InstrumentIndex.IndexPK = Convert.ToInt32(dr["IndexPK"]);
            M_InstrumentIndex.IndexID = Convert.ToString(dr["IndexID"]);
            M_InstrumentIndex.SharesForIndex = Convert.ToDecimal(dr["SharesForIndex"]);
            M_InstrumentIndex.LastPrice = Convert.ToDecimal(dr["LastPrice"]);
            M_InstrumentIndex.MarketCap = Convert.ToDecimal(dr["MarketCap"]);
            M_InstrumentIndex.Weight = Convert.ToDecimal(dr["Weight"]);
            M_InstrumentIndex.EntryUsersID = dr["EntryUsersID"].ToString();
            M_InstrumentIndex.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_InstrumentIndex.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_InstrumentIndex.VoidUsersID = dr["VoidUsersID"].ToString();
            M_InstrumentIndex.EntryTime = dr["EntryTime"].ToString();
            M_InstrumentIndex.UpdateTime = dr["UpdateTime"].ToString();
            M_InstrumentIndex.ApprovedTime = dr["ApprovedTime"].ToString();
            M_InstrumentIndex.VoidTime = dr["VoidTime"].ToString();
            M_InstrumentIndex.DBUserID = dr["DBUserID"].ToString();
            M_InstrumentIndex.DBTerminalID = dr["DBTerminalID"].ToString();
            M_InstrumentIndex.LastUpdate = dr["LastUpdate"].ToString();
            M_InstrumentIndex.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_InstrumentIndex;
        }

        public List<InstrumentIndex> InstrumentIndex_Select(int _status)
        {
            
              try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentIndex> L_InstrumentIndex = new List<InstrumentIndex>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID InstrumentID,G.Name InstrumentName, R.ID IndexID,GR.* from InstrumentIndex GR left join    
                            Instrument G on GR.InstrumentPK = G.InstrumentPK and G.status = 2 left join    
                            [Index] R on GR.IndexPK = R.IndexPK  and R.status = 2    
                            where GR.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,G.ID InstrumentID,G.Name InstrumentName, R.ID IndexID,GR.* from InstrumentIndex GR left join    
                            Instrument G on GR.InstrumentPK = G.InstrumentPK and G.status = 2 left join    
                            [Index] R on GR.IndexPK = R.IndexPK  and R.status = 2 ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InstrumentIndex.Add(setInstrumentIndex(dr));
                                }
                            }
                            return L_InstrumentIndex;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int InstrumentIndex_Add(InstrumentIndex _InstrumentIndex, bool _havePrivillege)
        {
              try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(InstrumentIndexPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from InstrumentIndex";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(InstrumentIndexPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from InstrumentIndex";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _InstrumentIndex.Date);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentIndex.InstrumentPK);
                        cmd.Parameters.AddWithValue("@IndexPK", _InstrumentIndex.IndexPK);                      
                        cmd.Parameters.AddWithValue("@SharesForIndex", _InstrumentIndex.SharesForIndex);
                        cmd.Parameters.AddWithValue("@LastPrice", _InstrumentIndex.LastPrice);
                        cmd.Parameters.AddWithValue("@MarketCap", _InstrumentIndex.MarketCap);
                        cmd.Parameters.AddWithValue("@Weight", _InstrumentIndex.Weight);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _InstrumentIndex.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "InstrumentIndex");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int InstrumentIndex_Update(InstrumentIndex _InstrumentIndex, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_InstrumentIndex.InstrumentIndexPK, _InstrumentIndex.HistoryPK, "InstrumentIndex");;
                DateTime _datetimeNow =  DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InstrumentIndex set status=2,Notes=@Notes,Date=@Date,InstrumentPK=@InstrumentPK,IndexPK=@IndexPK,SharesForIndex=@SharesForIndex," +
                                "LastPrice=@LastPrice,MarketCap=@MarketCap,Weight=@Weight,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where InstrumentIndexPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentIndex.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                            cmd.Parameters.AddWithValue("@Notes", _InstrumentIndex.Notes);
                            cmd.Parameters.AddWithValue("@Date", _InstrumentIndex.Date);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentIndex.InstrumentPK);
                            cmd.Parameters.AddWithValue("@IndexPK", _InstrumentIndex.IndexPK);                            
                            cmd.Parameters.AddWithValue("@SharesForIndex", _InstrumentIndex.SharesForIndex);
                            cmd.Parameters.AddWithValue("@LastPrice", _InstrumentIndex.LastPrice);
                            cmd.Parameters.AddWithValue("@MarketCap", _InstrumentIndex.MarketCap);
                            cmd.Parameters.AddWithValue("@Weight", _InstrumentIndex.Weight);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InstrumentIndex set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InstrumentIndexPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentIndex.EntryUsersID);
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
                                cmd.CommandText = "Update InstrumentIndex set Notes=@Notes,InstrumentPK=@InstrumentPK,IndexPK=@IndexPK,Date=@Date,SharesForIndex=@SharesForIndex," +
                                "LastPrice=@LastPrice,MarketCap=@MarketCap,Weight=@Weight,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where InstrumentIndexPK=@PK and historyPK=@HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentIndex.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                                cmd.Parameters.AddWithValue("@Notes", _InstrumentIndex.Notes);
                                cmd.Parameters.AddWithValue("@Date", _InstrumentIndex.Date);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentIndex.InstrumentPK);
                                cmd.Parameters.AddWithValue("@IndexPK", _InstrumentIndex.IndexPK);                             
                                cmd.Parameters.AddWithValue("@SharesForIndex", _InstrumentIndex.SharesForIndex);
                                cmd.Parameters.AddWithValue("@LastPrice", _InstrumentIndex.LastPrice);
                                cmd.Parameters.AddWithValue("@MarketCap", _InstrumentIndex.MarketCap);
                                cmd.Parameters.AddWithValue("@Weight", _InstrumentIndex.Weight);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentIndex.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_InstrumentIndex.InstrumentIndexPK, "InstrumentIndex");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From InstrumentIndex where InstrumentIndexPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentIndex.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _InstrumentIndex.Date);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentIndex.InstrumentPK);
                                cmd.Parameters.AddWithValue("@IndexPK", _InstrumentIndex.IndexPK);                               
                                cmd.Parameters.AddWithValue("@SharesForIndex", _InstrumentIndex.SharesForIndex);
                                cmd.Parameters.AddWithValue("@LastPrice", _InstrumentIndex.LastPrice);
                                cmd.Parameters.AddWithValue("@MarketCap", _InstrumentIndex.MarketCap);
                                cmd.Parameters.AddWithValue("@Weight", _InstrumentIndex.Weight);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentIndex.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update InstrumentIndex set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where InstrumentIndexPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _InstrumentIndex.Notes);
                                cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentIndex.HistoryPK);
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

        public void InstrumentIndex_Approved(InstrumentIndex _InstrumentIndex)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentIndex set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where InstrumentIndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentIndex.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InstrumentIndex set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InstrumentIndexPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentIndex.ApprovedUsersID);
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

        public void InstrumentIndex_Reject(InstrumentIndex _InstrumentIndex)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentIndex set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where InstrumentIndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentIndex.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InstrumentIndex set status= 2,LastUpdate=@LastUpdate  where InstrumentIndexPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
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

        public void InstrumentIndex_Void(InstrumentIndex _InstrumentIndex)
        {
            try
            {
                DateTime _datetimeNow =  DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentIndex set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where InstrumentIndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentIndex.InstrumentIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentIndex.VoidUsersID);
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

        public string ImportInstrumentIndex(string _fileName, string _date, string specName, string _userID)
        {
            string _msg = "";
            string IndexID = specName.Substring(0, specName.Length - 10);
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //Validasi
                        cmd.CommandText =
                            @"select * from [index] where ID = @ID";
                        //cmd.Parameters.AddWithValue("@ID", _dateTime.ToString("MM/dd/yyyy"));
                        cmd.Parameters.AddWithValue("@ID", IndexID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return "Import Cancel, Your index is not Registered";
                            }
                            else
                            {
                                //delete data yang lama
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText = @"DECLARE @IndexPK int
                                                                    SELECT @IndexPK = IndexPK from [index] where ID=@ID
                                                                    DELETE FROM InstrumentIndex where [Date] = @Date and IndexPK = @IndexPK
                                                          truncate table InstrumentIndexTemp 
                                            ";
                                        cmd1.Parameters.AddWithValue("@Date", _date);
                                        cmd1.Parameters.AddWithValue("@ID", IndexID);
                                        cmd1.ExecuteNonQuery();
                                    }
                                }

                                // import data ke temp dulu
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                                {
                                    bulkCopy.DestinationTableName = "dbo.InstrumentIndexTemp";
                                    bulkCopy.WriteToServer(CreateDataTableFromInstrumentIndexTempExcelFile(_fileName, _date, IndexID));
                                    _msg = "Import index Success";
                                }

                                // logic kalo Reconcile success
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        DateTime _datetimeNow = DateTime.Now;
                                        cmd1.CommandText = @"
                                        DECLARE @MaxPK INT

                                        SELECT @MaxPK = MAX(InstrumentIndexPK) + 1 FROM dbo.InstrumentIndex
                                        SET @MaxPK = ISNULL(@MaxPK,1)
                                        INSERT INTO dbo.InstrumentIndex
                                                ( InstrumentIndexPK ,
                                                  HistoryPK ,
                                                  Status ,
                                                  Notes ,
                                                  Date ,
                                                  InstrumentPK ,
                                                  IndexPK ,
                                                  SharesForIndex ,
                                                  LastPrice ,
                                                  MarketCap ,
                                                  Weight ,
                                                  EntryUsersID ,
                                                  EntryTime ,
                                                  LastUpdate 
                                                )
                                        SELECT @MaxPK + Row_number() over(order by InstrumentID),1,2,'',Date,
                                        B.InstrumentPK,C.IndexPK,A.SharesForIndex,A.Lastprice,A.MarketCap,A.Weight,
                                        @UserID,@Date,@Date
                                        FROM dbo.InstrumentIndexTemp A
                                        INNER JOIN Instrument B ON A.InstrumentID = B.ID AND B.status = 2
                                        INNER JOIN [index] C ON A.IndexID = C.ID AND C.status = 2
                                        ";
                                        cmd1.Parameters.AddWithValue("@Date", _datetimeNow);
                                        cmd1.Parameters.AddWithValue("@UserID",_userID);
                                
                                        cmd1.ExecuteNonQuery();

                                    }
                                    _msg = "Import Instrument index Done";

                                }

                            }
                        }
                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromInstrumentIndexTempExcelFile(string _path, string _date, string IndexID)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IndexID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "SharesForIndex";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "MarketCap";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Weight";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [" + IndexID + "$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 3; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    //dr["Date"] = odRdr[0];
                                    dr["Date"] = _date;
                                    dr["InstrumentID"] = odRdr[1];
                                    dr["IndexID"] = IndexID;
                                    dr["SharesForIndex"] = odRdr[3];
                                    dr["LastPrice"] = odRdr[4];
                                    dr["MarketCap"] = odRdr[5];
                                    dr["Weight"] = odRdr[6];

                                    dt.Rows.Add(dr);
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
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