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
using System.Data;
using OfficeOpenXml.Drawing;


namespace RFSRepository
{
    public class FundExposureReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundExposure] " +
                            "([FundExposurePK],[HistoryPK],[Status],[FundPK],[Type],[Parameter],[MinExposurePercent],[MaxExposurePercent],[WarningMinExposurePercent],[WarningMaxExposurePercent],[MaxValue],[MinValue],[WarningMaxValue],[WarningMinValue],";
        string _ParameterCommand = "@FundPK,@Type,@Parameter,@MinExposurePercent,@MaxExposurePercent,@WarningMinExposurePercent,@WarningMaxExposurePercent,@MaxValue,@MinValue,@WarningMaxValue,@WarningMinValue,";

        //2
        private FundExposure setFundExposure(SqlDataReader dr)
        {
            FundExposure M_FundExposure = new FundExposure();
            M_FundExposure.FundExposurePK = Convert.ToInt32(dr["FundExposurePK"]);
            M_FundExposure.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundExposure.Status = Convert.ToInt32(dr["Status"]);
            M_FundExposure.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundExposure.Notes = Convert.ToString(dr["Notes"]);
            M_FundExposure.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundExposure.FundID = Convert.ToString(dr["FundID"]);
            M_FundExposure.Type = Convert.ToInt32(dr["Type"]);
            M_FundExposure.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_FundExposure.Parameter = Convert.ToInt32(dr["Parameter"]);
            M_FundExposure.ParameterDesc = Convert.ToString(dr["ParameterDesc"]);
            M_FundExposure.MinExposurePercent = Convert.ToDecimal(dr["MinExposurePercent"]);
            M_FundExposure.MaxExposurePercent = Convert.ToDecimal(dr["MaxExposurePercent"]);
            M_FundExposure.WarningMinExposurePercent = Convert.ToDecimal(dr["WarningMinExposurePercent"]);
            M_FundExposure.WarningMaxExposurePercent = Convert.ToDecimal(dr["WarningMaxExposurePercent"]);
            M_FundExposure.MaxValue = Convert.ToDecimal(dr["MaxValue"]);
            M_FundExposure.MinValue = Convert.ToDecimal(dr["MinValue"]);
            M_FundExposure.WarningMaxValue = Convert.ToDecimal(dr["WarningMaxValue"]);
            M_FundExposure.WarningMinValue = Convert.ToDecimal(dr["WarningMinValue"]);
            M_FundExposure.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundExposure.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundExposure.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundExposure.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundExposure.EntryTime = dr["EntryTime"].ToString();
            M_FundExposure.UpdateTime = dr["UpdateTime"].ToString();
            M_FundExposure.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundExposure.VoidTime = dr["VoidTime"].ToString();
            M_FundExposure.DBUserID = dr["DBUserID"].ToString();
            M_FundExposure.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundExposure.LastUpdate = dr["LastUpdate"].ToString();
            M_FundExposure.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundExposure;
        }

        public bool Get_CheckExistingID(int _fundpk, int _type, int _parameter)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        {
                            cmd.CommandText = "select * from Fundexposure where FundPK = @fundpk and type = @type and parameter = @parameter  and Status in (1,2)";
                            cmd.Parameters.AddWithValue("@fundpk", _fundpk);
                            cmd.Parameters.AddWithValue("@type", _type);
                            cmd.Parameters.AddWithValue("@parameter", _parameter);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
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

		public List<FundExposure> FundExposure_Select(int _status)
		{

			try
			{
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					List<FundExposure> L_FundExposure = new List<FundExposure>();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{
						if (_status != 9)
						{
							cmd.CommandText = @" 
                            
                            Select case when FE.status=1 then 'PENDING' else Case When FE.status = 2 then 'APPROVED' 
							else Case when FE.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, MV.DescOne as TypeDesc,  
							case when FE.Type = 1 then D.DescOne 
							when FE.Type = 2 then isnull(IR.ID,'ALL')
							when FE.Type = 3 then isnull(S.Name,'ALL') 
							when FE.Type = 4 then IT.Name
							when FE.Type = 9 then isnull(B.Name,'ALL') 
							when FE.Type = 12 then cast(C.MinPrice as nvarchar(50)) 
							when FE.Type = 10 then isnull(B.Name,'ALL') 
							when FE.Type = 13 then isnull(I.Name,'ALL')
							when FE.Type = 14 then isnull(IX.Name,'ALL')
							when FE.Type = 20 then isnull(CP.Name,'ALL')
							when FE.Type = 24 then ''
							when FE.Type = 25 then MV1.DescOne
							when FE.Type = 27 then isnull(CP.Name,'ALL')
							when FE.Type = 32 then case when FE.Parameter = 1 then 'CAPITAL CLASSIFICATION 1' 
															when FE.Parameter = 2 then 'CAPITAL CLASSIFICATION 2' 
																when FE.Parameter = 3 then 'CAPITAL CLASSIFICATION 3' 
																	else 'CAPITAL CLASSIFICATION 4' end 
							else isnull(IR.ID,'ALL') 
							END  ParameterDesc,isnull(F.ID,'ALL') FundID,* 
							from FundExposure FE      
                            left join MasterValue MV on FE.Type = MV.Code and MV.ID ='ExposureType' and MV.status in (1,2)  
                            left join Holding H on FE.parameter = H.HoldingPK and H.status in (1,2)
                            left join Fund F on FE.FundPK = F.FundPK and F.status in (1,2)
                            left join Issuer I on FE.parameter = I.IssuerPK and I.status in (1,2)
                            left join Sector S on FE.parameter = S.SectorPK and S.status in (1,2)
                            left join [Index] IX on FE.parameter = IX.IndexPK and IX.status in (1,2)
                            left join InstrumentType IT on FE.parameter = IT.InstrumentTypePK and IT.status in (1,2)
                            left join Instrument IR on FE.parameter = IR.InstrumentPK and IR.status in (1,2)
                            left join Bank B on FE.parameter = B.BankPK and B.status in (1,2)
                            left join RangePrice C on FE.parameter = C.MinPrice and C.status in (1,2)
                            left join MasterValue D on FE.parameter = D.Code and D.ID ='instrumentGroupType' and D.status in (1,2) 
							left join Counterpart CP on FE.parameter = CP.CounterpartPK and CP.status in (1,2)
							left join MasterValue MV1 on FE.Parameter = MV1.Priority and MV1.ID ='BondRating' and MV1.status in (1,2)  
                            where FE.status = @status
                            ";
							cmd.Parameters.AddWithValue("@status", _status);
						}
						else
						{
							cmd.CommandText = @"  
							Select case when FE.status=1 then 'PENDING' else Case When FE.status = 2 then 'APPROVED' 
							else Case when FE.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, MV.DescOne as TypeDesc,  
							case when FE.Type = 1 then D.DescOne 
							when FE.Type = 2 then isnull(IR.ID,'ALL')
							when FE.Type = 3 then isnull(S.Name,'ALL') 
							when FE.Type = 4 then IT.Name
							when FE.Type = 9 then isnull(B.Name,'ALL') 
							when FE.Type = 12 then cast(C.MinPrice as nvarchar(50)) 
							when FE.Type = 10 then isnull(B.Name,'ALL') 
							when FE.Type = 13 then isnull(I.Name,'ALL')
							when FE.Type = 14 then isnull(IX.Name,'ALL')
							when FE.Type = 20 then isnull(CP.Name,'ALL')
							when FE.Type = 24 then ''
							when FE.Type = 25 then MV1.DescOne
							when FE.Type = 27 then isnull(CP.Name,'ALL')
							when FE.Type = 32 then case when FE.Parameter = 1 then 'CAPITAL CLASSIFICATION 1' 
															when FE.Parameter = 2 then 'CAPITAL CLASSIFICATION 2' 
																when FE.Parameter = 3 then 'CAPITAL CLASSIFICATION 3' 
																	else 'CAPITAL CLASSIFICATION 4' end 
							else isnull(IR.ID,'ALL') 
							END  ParameterDesc,isnull(F.ID,'ALL') FundID,* 
							from FundExposure FE     
                            left join MasterValue MV on FE.Type = MV.Code and MV.ID ='ExposureType' and MV.status in (1,2)  
                            left join Holding H on FE.parameter = H.HoldingPK and H.status in (1,2)
                            left join Fund F on FE.FundPK = F.FundPK and F.status in (1,2)
                            left join Issuer I on FE.parameter = I.IssuerPK and I.status in (1,2)
                            left join Sector S on FE.parameter = S.SectorPK and S.status in (1,2)
                            left join [Index] IX on FE.parameter = IX.IndexPK and IX.status in (1,2)
                            left join InstrumentType IT on FE.parameter = IT.InstrumentTypePK and IT.status in (1,2)
                            left join Instrument IR on FE.parameter = IR.InstrumentPK and IR.status in (1,2)
                            left join Bank B on FE.parameter = B.BankPK and B.status in (1,2)
                            left join RangePrice C on FE.parameter = C.MinPrice and C.status in (1,2)
                            left join MasterValue D on FE.parameter = D.Code and D.ID ='instrumentGroupType' and D.status in (1,2) 
							left join Counterpart CP on FE.parameter = CP.CounterpartPK and CP.status in (1,2)
							left join MasterValue MV1 on FE.Parameter = MV1.Priority and MV1.ID ='BondRating' and MV1.status in (1,2)  
";
						}
						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								while (dr.Read())
								{
									L_FundExposure.Add(setFundExposure(dr));
								}
							}
							return L_FundExposure;
						}
					}
				}
			}
			catch (Exception err)
			{
				throw err;
			}

		}

		public int FundExposure_Add(FundExposure _FundExposure, bool _havePrivillege)
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
                                 "Select isnull(max(FundExposurePk),0) + 1,1,@status," + _ParameterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From FundExposure";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundExposure.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FundExposurePk),0) + 1,1,@status," + _ParameterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From FundExposure";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _FundExposure.FundPK);
                        cmd.Parameters.AddWithValue("@Type", _FundExposure.Type);
                        cmd.Parameters.AddWithValue("@Parameter", _FundExposure.Parameter);
                        cmd.Parameters.AddWithValue("@MinExposurePercent", _FundExposure.MinExposurePercent);
                        cmd.Parameters.AddWithValue("@MaxExposurePercent", _FundExposure.MaxExposurePercent);
                        cmd.Parameters.AddWithValue("@WarningMinExposurePercent", _FundExposure.WarningMinExposurePercent);
                        cmd.Parameters.AddWithValue("@WarningMaxExposurePercent", _FundExposure.WarningMaxExposurePercent);
                        cmd.Parameters.AddWithValue("@MaxValue", _FundExposure.MaxValue);
                        cmd.Parameters.AddWithValue("@MinValue", _FundExposure.MinValue);
                        cmd.Parameters.AddWithValue("@WarningMaxValue", _FundExposure.WarningMaxValue);
                        cmd.Parameters.AddWithValue("@WarningMinValue", _FundExposure.WarningMinValue);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundExposure.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundExposure");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundExposure_Update(FundExposure _FundExposure, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundExposure.FundExposurePK, _FundExposure.HistoryPK, "FundExposure");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundExposure set status=2,Notes=@Notes,FundPK=@FundPK,Type=@Type,Parameter=@Parameter,MinExposurePercent=@MinExposurePercent,MaxExposurePercent=@MaxExposurePercent," +
                                "WarningMinExposurePercent=@WarningMinExposurePercent,WarningMaxExposurePercent=@WarningMaxExposurePercent,MaxValue = @MaxValue,MinValue = @MinValue,WarningMaxValue = @WarningMaxValue,WarningMinValue = @WarningMinValue,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where FundExposurePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundExposure.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                            cmd.Parameters.AddWithValue("@Notes", _FundExposure.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _FundExposure.FundPK);
                            cmd.Parameters.AddWithValue("@Type", _FundExposure.Type);
                            cmd.Parameters.AddWithValue("@Parameter", _FundExposure.Parameter);
                            cmd.Parameters.AddWithValue("@MinExposurePercent", _FundExposure.MinExposurePercent);
                            cmd.Parameters.AddWithValue("@MaxExposurePercent", _FundExposure.MaxExposurePercent);
                            cmd.Parameters.AddWithValue("@WarningMinExposurePercent", _FundExposure.WarningMinExposurePercent);
                            cmd.Parameters.AddWithValue("@WarningMaxExposurePercent", _FundExposure.WarningMaxExposurePercent);
                            cmd.Parameters.AddWithValue("@MaxValue", _FundExposure.MaxValue);
                            cmd.Parameters.AddWithValue("@MinValue", _FundExposure.MinValue);
                            cmd.Parameters.AddWithValue("@WarningMaxValue", _FundExposure.WarningMaxValue);
                            cmd.Parameters.AddWithValue("@WarningMinValue", _FundExposure.WarningMinValue);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundExposure.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundExposure.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundExposure set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundExposurePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundExposure.EntryUsersID);
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
                                cmd.CommandText = "Update FundExposure set Notes=@Notes,FundPK=@FundPK,Type=@Type,Parameter=@Parameter,MinExposurePercent=@MinExposurePercent,MaxExposurePercent=@MaxExposurePercent," +
                                "WarningMinExposurePercent=@WarningMinExposurePercent,WarningMaxExposurePercent=@WarningMaxExposurePercent,MaxValue = @MaxValue,MinValue = @MinValue,WarningMaxValue = @WarningMaxValue,WarningMinValue = @WarningMinValue, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where FundExposurePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundExposure.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                                cmd.Parameters.AddWithValue("@Notes", _FundExposure.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _FundExposure.FundPK);
                                cmd.Parameters.AddWithValue("@Type", _FundExposure.Type);
                                cmd.Parameters.AddWithValue("@Parameter", _FundExposure.Parameter);
                                cmd.Parameters.AddWithValue("@MinExposurePercent", _FundExposure.MinExposurePercent);
                                cmd.Parameters.AddWithValue("@MaxExposurePercent", _FundExposure.MaxExposurePercent);
                                cmd.Parameters.AddWithValue("@WarningMinExposurePercent", _FundExposure.WarningMinExposurePercent);
                                cmd.Parameters.AddWithValue("@WarningMaxExposurePercent", _FundExposure.WarningMaxExposurePercent);
                                cmd.Parameters.AddWithValue("@MaxValue", _FundExposure.MaxValue);
                                cmd.Parameters.AddWithValue("@MinValue", _FundExposure.MinValue);
                                cmd.Parameters.AddWithValue("@WarningMaxValue", _FundExposure.WarningMaxValue);
                                cmd.Parameters.AddWithValue("@WarningMinValue", _FundExposure.WarningMinValue);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundExposure.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FundExposure.FundExposurePK, "FundExposure");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _ParameterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundExposure where FundExposurePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundExposure.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundExposure.FundPK);
                                cmd.Parameters.AddWithValue("@Type", _FundExposure.Type);
                                cmd.Parameters.AddWithValue("@Parameter", _FundExposure.Parameter);
                                cmd.Parameters.AddWithValue("@MinExposurePercent", _FundExposure.MinExposurePercent);
                                cmd.Parameters.AddWithValue("@MaxExposurePercent", _FundExposure.MaxExposurePercent);
                                cmd.Parameters.AddWithValue("@WarningMinExposurePercent", _FundExposure.WarningMinExposurePercent);
                                cmd.Parameters.AddWithValue("@WarningMaxExposurePercent", _FundExposure.WarningMaxExposurePercent);
                                cmd.Parameters.AddWithValue("@MaxValue", _FundExposure.MaxValue);
                                cmd.Parameters.AddWithValue("@MinValue", _FundExposure.MinValue);
                                cmd.Parameters.AddWithValue("@WarningMaxValue", _FundExposure.WarningMaxValue);
                                cmd.Parameters.AddWithValue("@WarningMinValue", _FundExposure.WarningMinValue);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundExposure.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundExposure set status= 4,Notes=@Notes," +
                                    " LastUpdate=@lastupdate " +
                                    " where FundExposurePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundExposure.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundExposure.HistoryPK);
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

        public void FundExposure_Approved(FundExposure _FundExposure)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundExposure set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where FundExposurePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundExposure.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundExposure.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundExposure set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundExposurePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundExposure.ApprovedUsersID);
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

        public void FundExposure_Reject(FundExposure _FundExposure)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundExposure set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundExposurePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundExposure.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundExposure.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundExposure set status= 2,LastUpdate=@LastUpdate where FundExposurePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
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

        public void FundExposure_Void(FundExposure _FundExposure)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundExposure set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundExposurePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundExposure.FundExposurePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundExposure.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundExposure.VoidUsersID);
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

        public List<FundExposureCombo> FundExposure_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundExposureCombo> L_FundExposure = new List<FundExposureCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundExposurePK,ID + ' - ' + TypeDesc as ID, TypeDesc FROM [FundExposure]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundExposureCombo M_FundExposure = new FundExposureCombo();
                                    M_FundExposure.FundExposurePK = Convert.ToInt32(dr["FundExposurePK"]);
                                    M_FundExposure.Type = Convert.ToInt32(dr["Type"]);
                                    M_FundExposure.TypeDesc = Convert.ToString(dr["TypeDesc"]);
                                    M_FundExposure.Parameter = Convert.ToInt32(dr["Parameter"]);
                                    M_FundExposure.ParameterDesc = Convert.ToString(dr["ParameterDesc"]);
                                    L_FundExposure.Add(M_FundExposure);
                                }

                            }
                            return L_FundExposure;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }


        public List<FundExposureCombo> Get_ExposureIDByType(string _typeDesc)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundExposureCombo> L_FundExposure = new List<FundExposureCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_typeDesc == "HOLDING")
                        {
                            cmd.CommandText = "select HoldingPK as Parameter,Name ParameterDesc from [Holding] where status  = 2 union all select 0,'ALL' order by HoldingPK,Name";
                        }

                        else if (_typeDesc == "INSTRUMENT TYPE")
                        {
                            cmd.CommandText = "select InstrumentTypePK as Parameter,Name ParameterDesc from [InstrumentType] where status  = 2 order by InstrumentTypePK,Name";
                        }

                        else if (_typeDesc == "INSTRUMENT TYPE GROUP")
                        {
                            cmd.CommandText = @"Select A.Code Parameter,A.DescOne ParameterDesc from dbo.MasterValue A WHERE A.status = 2  AND A.ID = 'instrumentGroupType' ";
                        }

                        else if (_typeDesc == "ISSUER")
                        {
                            cmd.CommandText = "select IssuerPK as Parameter,Name ParameterDesc from [Issuer] where status  = 2 union all select 0,'ALL' order by IssuerPK,Name";
                        }

                        else if (_typeDesc == "SECTOR")
                        {
                            cmd.CommandText = "select SectorPK as Parameter,Name ParameterDesc from [Sector] where status  = 2 union all select 0,'ALL' order by SectorPK,Name";
                        }

                        else if (_typeDesc == "INDEX")
                        {
                            cmd.CommandText = "select IndexPK as Parameter,Name ParameterDesc from [Index] where status  = 2 union all select 0,'ALL' order by IndexPK,Name";
                        }
                        else if (_typeDesc == "ALL FUND PER BANK" || _typeDesc == "PER FUND PER BANK" || _typeDesc == "ISSUER ALL FUND")
                        {
                            cmd.CommandText = @"select BankPK as Parameter,Name ParameterDesc from Bank where status  = 2
                                                union all select 0,'ALL' order by BankPK,Name";
                        }
                        else if (_typeDesc == "EQUITY ALL FUND COMPARE TO MARKET CAP")
                        {
                            cmd.CommandText = @"select 0 Parameter,'ALL' ParameterDesc";
                        }
                        else if (_typeDesc == "PRICE")
                        {
                            cmd.CommandText = @"select MinPrice as Parameter,MinPrice ParameterDesc from RangePrice where status  = 2 ";
                        }
                        else if (_typeDesc == "SYARIAH ONLY")
                        {
                            cmd.CommandText = @"select 0 Parameter,'ALL' ParameterDesc";
                        }
                        else if (_typeDesc == "EQUITY")
                        {
                            cmd.CommandText = @"select I.InstrumentPK as Parameter, I.ID + ' - ' + I.Name ParameterDesc From instrument I left join InstrumentType IT 
                            on I.InstrumentTypePK=IT.InstrumentTypePK 
                            where I.status = 2 and IT.InstrumentTypePK in(1,4,16) 
                            union all select 0,'ALL' order by I.InstrumentPK";
                        }
                        else if (_typeDesc == "BOND")
                        {
                            cmd.CommandText = @"select I.InstrumentPK as Parameter, I.ID + ' - ' + I.Name ParameterDesc From instrument I left join InstrumentType IT 
                            on I.InstrumentTypePK=IT.InstrumentTypePK 
                            where I.status = 2 and IT.InstrumentTypePK in(2,3,15) 
                            union all select 0,'ALL' order by I.InstrumentPK";
                        }

                        else if (_typeDesc == "TOTAL PORTFOLIO PER FUND")
                        {
                            cmd.CommandText = @"select 0 Parameter,'ALL' ParameterDesc";
                        }

                        else if (_typeDesc == "TOTAL HOLDING PER FUND")
                        {
                            cmd.CommandText = "select HoldingPK as Parameter,Name ParameterDesc from [Holding] where status  = 2 union all select 0,'ALL' order by HoldingPK,Name";
                        }

                        else
                        {
                            cmd.CommandText = "select I.InstrumentPK as Parameter, I.ID + ' - ' + I.Name ParameterDesc From instrument I left join InstrumentType IT " +
                            "on I.InstrumentTypePK=IT.InstrumentTypePK " +
                            "where I.status = 2 and IT.Name = @Type order by I.ID ";
                        }
                        cmd.Parameters.AddWithValue("@Type", _typeDesc);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    FundExposureCombo M_FundExposure = new FundExposureCombo();
                                    M_FundExposure.Parameter = Convert.ToInt32(dr["Parameter"]);
                                    M_FundExposure.ParameterDesc = Convert.ToString(dr["ParameterDesc"]);
                                    L_FundExposure.Add(M_FundExposure);
                                }
                            }
                            return L_FundExposure;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

		//Ucup
		public List<FundExposureCombo> Get_ExposureIDByTypeForFund(string _typeDesc)
		{
			try
			{
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					List<FundExposureCombo> L_FundExposure = new List<FundExposureCombo>();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{

						if (_typeDesc == "HOLDING")
						{
							cmd.CommandText = "select HoldingPK as Parameter,Name ParameterDesc from [Holding] where status  = 2 union all select 0,'ALL' order by HoldingPK,Name";
						}

						else if (_typeDesc == "ALL FUND PER BANK" || _typeDesc == "EQUITY ALL FUND COMPARE TO MARKET CAP" || _typeDesc == "ISSUER ALL FUND" || _typeDesc == "TOTAL AUM ALL FUND")
						{
							cmd.CommandText = @"select 0 FundPK,'All' Name";
						}

						else if (_typeDesc == "EQUITY PER FUND COMPARE TO MARKET CAP")
						{
							cmd.CommandText = @"SELECT  isnull(FundPK,0) FundPK,ID + ' - ' + Name as ID, Name FROM [Fund] where status = 2 union all select 0,'All', 'All' order by FundPK,Name";
						}

						else if (_typeDesc == "INSTRUMENT TYPE GROUP" || _typeDesc == "BOND" || _typeDesc == "SECTOR" || _typeDesc == "INSTRUMENT TYPE" || _typeDesc == "EQUITY" || _typeDesc == "PER FUND PER BANK" ||
								 _typeDesc == "ISSUER" || _typeDesc == "INDEX" || _typeDesc == "SYARIAH ONLY" || _typeDesc == "TOTAL PORTFOLIO PER FUND" || _typeDesc == "TOTAL HOLDING PER FUND" || _typeDesc == "CAPITAL CLASSIFICATION")
						{
							cmd.CommandText = @"SELECT  isnull(FundPK,0) FundPK,ID + ' - ' + Name as ID, Name FROM [Fund] where status = 2 order by FundPK,Name";
						}

						else
						{
							cmd.CommandText = @"SELECT  isnull(FundPK,0) FundPK,ID + ' - ' + Name as ID, Name FROM [Fund] where status = 2 union all select 0,'All', '' order by FundPK,Name";
						}
						cmd.Parameters.AddWithValue("@Type", _typeDesc);

						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								while (dr.Read())
								{

									FundExposureCombo M_FundExposure = new FundExposureCombo();
									M_FundExposure.FundPK = Convert.ToInt32(dr["FundPK"]);
									M_FundExposure.Name = Convert.ToString(dr["Name"]);
									L_FundExposure.Add(M_FundExposure);
								}
							}
							return L_FundExposure;
						}
					}
				}
			}
			catch (Exception err)
			{
				throw err;
			}

		}

		//Ucup
		public List<FundExposureCombo> Get_ExposureIDByTypeForParameter(string _typeDesc)
		{
			try
			{
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					List<FundExposureCombo> L_FundExposure = new List<FundExposureCombo>();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{

						if (_typeDesc == "HOLDING")
						{
							cmd.CommandText = "select HoldingPK as Parameter,Name ParameterDesc from [Holding] where status  = 2 union all select 0,'ALL' order by HoldingPK,Name";
						}

						else if (_typeDesc == "INSTRUMENT TYPE GROUP")
						{
							cmd.CommandText = @"Select A.Code Parameter,A.DescOne ParameterDesc from dbo.MasterValue A WHERE A.status = 2  AND A.ID = 'instrumentGroupType'";
						}
						else if (_typeDesc == "INSTRUMENT TYPE")
						{
							cmd.CommandText = "select InstrumentTypePK as Parameter,Name ParameterDesc from [InstrumentType] where status  = 2 order by InstrumentTypePK,Name";
						}

						else if (_typeDesc == "SYARIAH ONLY" || _typeDesc == "TOTAL PORTFOLIO PER FUND" || _typeDesc == "DIRECT INVESTMENT" || _typeDesc == "LAND AND PROPERTY" || _typeDesc == "CAMEL SCORE BANK PER FUND" || _typeDesc == "INVESTMENT OTHER THAN DEPOSIT" || _typeDesc == "AFFILIATED INVESTMENT" || _typeDesc == "TOTAL AUM ALL FUND")
						{
							cmd.CommandText = @"select 0 Parameter,'ALL' ParameterDesc";
						}


						else if (_typeDesc == "EQUITY ALL FUND COMPARE TO MARKET CAP")
						{
							cmd.CommandText = @"select I.InstrumentPK as Parameter, I.ID + ' - ' + I.Name ParameterDesc From instrument I left join InstrumentType IT 
                            on I.InstrumentTypePK=IT.InstrumentTypePK 
                            where I.status = 2 and IT.InstrumentTypePK in(1,4,16) 
                            union all select 0,'ALL' order by I.InstrumentPK";
						}
						else if (_typeDesc == "BOND")
						{
							cmd.CommandText = @"select I.InstrumentPK as Parameter, I.ID + ' - ' + I.Name ParameterDesc From instrument I left join InstrumentType IT 
                            on I.InstrumentTypePK=IT.InstrumentTypePK 
                            where I.status = 2 and IT.InstrumentTypePK in(2,3,15) 
                            union all select 0,'ALL' order by I.InstrumentPK";
						}
						else if (_typeDesc == "SECTOR")
						{
							cmd.CommandText = "select SectorPK as Parameter,Name ParameterDesc from [Sector] where status  = 2 union all select 0,'ALL' order by SectorPK,Name";
						}
						else if (_typeDesc == "EQUITY")
						{
							cmd.CommandText = @"select I.InstrumentPK as Parameter, I.ID + ' - ' + I.Name ParameterDesc From instrument I left join InstrumentType IT 
                            on I.InstrumentTypePK=IT.InstrumentTypePK 
                            where I.status = 2 and IT.InstrumentTypePK in(1,4,16) 
                            union all select 0,'ALL' order by I.InstrumentPK";
						}
						else if (_typeDesc == "ALL FUND PER BANK")
						{
							cmd.CommandText = @"select BankPK as Parameter,Name ParameterDesc from Bank where status  = 2 union all select 0,'ALL' order by BankPK,Name";
						}
						else if (_typeDesc == "PER FUND PER BANK")
						{
							cmd.CommandText = @"select BankPK as Parameter,Name ParameterDesc from Bank where status  = 2 union all select 0,'ALL' order by BankPK,Name";
						}
						else if (_typeDesc == "ISSUER")
						{
							cmd.CommandText = "select IssuerPK as Parameter,Name ParameterDesc from [Issuer] where status  = 2 union all select 0,'ALL' order by IssuerPK,Name";
						}
						else if (_typeDesc == "INDEX")
						{
							cmd.CommandText = "select IndexPK as Parameter,Name ParameterDesc from [Index] where status  = 2 union all select 0,'ALL' order by IndexPK,Name";
						}
						else if (_typeDesc == "ISSUER ALL FUND")
						{
							cmd.CommandText = "select IssuerPK as Parameter,Name ParameterDesc from [Issuer] where status  = 2 union all select 0,'ALL' order by IssuerPK,Name";
						}
						else if (_typeDesc == "MUTUAL FUND PER COUNTERPART")
						{
							cmd.CommandText = @"select CounterpartPK as Parameter,Name ParameterDesc from [Counterpart] where status  = 2 union all select 0,'ALL' order by CounterpartPK,Name";
						}
						else if (_typeDesc == "EQUITY UNIVERSAL BASED ON INDEX")
						{
							cmd.CommandText = @"select IndexPK as Parameter,Name ParameterDesc from [Index] where status  = 2 union all select 0,'ALL' order by IndexPK,Name";
						}

						else if (_typeDesc == "BOND RATING")
						{
							cmd.CommandText = @"select Priority as Parameter,DescOne ParameterDesc from MasterValue where Id Like 'BondRating'";
						}
						else if (_typeDesc == "TOTAL FOREIGN PORTFOLIO PER FUND")
						{
							cmd.CommandText = @"select 0 Parameter,'ALL' ParameterDesc";
						}
						else if (_typeDesc == "KIK EBA PER COUNTERPART")
						{
							cmd.CommandText = @"select CounterpartPK as Parameter,Name ParameterDesc from [Counterpart] where status  = 2 union all select 0,'ALL' order by CounterpartPK,Name";
						}
						else if (_typeDesc == "TOTAL HOLDING PER FUND")
						{
							cmd.CommandText = "select HoldingPK as Parameter,Name ParameterDesc from [Holding] where status  = 2 union all select 0,'ALL' order by HoldingPK,Name";
						}
						else if (_typeDesc == "CAPITAL CLASSIFICATION")
						{
							cmd.CommandText = @"select 1 Parameter,'CAPITAL CLASSIFICATION 1' ParameterDesc
												union all
												select 2 Parameter,'CAPITAL CLASSIFICATION 2' ParameterDesc
												union all
												select 3 Parameter,'CAPITAL CLASSIFICATION 3' ParameterDesc
												union all
												select 4 Parameter,'CAPITAL CLASSIFICATION 4' ParameterDesc
												";
						}
						else
						{
							cmd.CommandText = "select I.InstrumentPK as Parameter, I.ID + ' - ' + I.Name ParameterDesc From instrument I left join InstrumentType IT " +
							"on I.InstrumentTypePK=IT.InstrumentTypePK " +
							"where I.status = 2 and IT.Name = @Type order by I.ID ";
						}
						cmd.Parameters.AddWithValue("@Type", _typeDesc);

						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								while (dr.Read())
								{

									FundExposureCombo M_FundExposure = new FundExposureCombo();
									M_FundExposure.Parameter = Convert.ToInt32(dr["Parameter"]);
									M_FundExposure.ParameterDesc = Convert.ToString(dr["ParameterDesc"]);
									L_FundExposure.Add(M_FundExposure);
								}
							}
							return L_FundExposure;
						}
					}
				}
			}
			catch (Exception err)
			{
				throw err;
			}

		}

		public List<FundExposureComboPreTrade> Get_FundExposurePreTrade(DateTime _date, int _fundPK)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundExposureComboPreTrade> L_FundExposure = new List<FundExposureComboPreTrade>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "FundExposurePreTrade";
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundExposureComboPreTrade M_FundExposure = new FundExposureComboPreTrade();
                                    M_FundExposure.ExposureType = dr["ExposureType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExposureType"]);
                                    M_FundExposure.ExposureID = dr["ExposureID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExposureID"]);
                                    M_FundExposure.MarketValue = dr["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MarketValue"]);
                                    M_FundExposure.ExposurePercent = dr["ExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["ExposurePercent"]);
                                    M_FundExposure.AlertMinExposure = dr["AlertMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMinExposure"]);
                                    M_FundExposure.AlertMaxExposure = dr["AlertMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["AlertMaxExposure"]);
                                    M_FundExposure.WarningMinExposure = dr["WarningMinExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["WarningMinExposure"]);
                                    M_FundExposure.WarningMaxExposure = dr["WarningMaxExposure"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["WarningMaxExposure"]);
                                    L_FundExposure.Add(M_FundExposure);
                                }

                            }
                            return L_FundExposure;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

		public FundExposure Validate_CheckExposure(DateTime _valuedate, int _instrumentPK, int _fundPK, decimal _amount, int _trxType, int _instrumentTypePK)
		{
			try
			{
				DateTime _dateTimeNow = DateTime.Now;
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{
						string _parameter = "";
						string _paramFund = "";
						string _flagDeposito = "";

						if (_instrumentTypePK == 2)
						{
							_flagDeposito = CheckDepositoMaturityUnderOneYear(_instrumentPK, _valuedate);
						}


						if (_instrumentTypePK == 1)
						{
							_parameter = " and ((Exposure = 1 and Parameter = 1) or (Exposure = 4 and (Parameter in (1,4,16))) or (Exposure in (3,5,13,14,15,16,17,18,19,25,26))) ";
						}
						else if (_instrumentTypePK == 2 && _flagDeposito == "Yes")
						{
							_parameter = " and ((Exposure = 1 and Parameter = 3) or (Exposure = 4 and (Parameter in (5))) or (Exposure in (9,10,13,15,16,32)))  ";
						}
						else if (_instrumentTypePK == 2 && _flagDeposito == "No")
						{
							_parameter = " and ((Exposure = 1 and Parameter = 2) or (Exposure = 4 and (Parameter in (2,3,9,12,13,14,15))) or (Exposure in (2,3,13,14,15,16,18,25,26))) ";
						}
						else
						{
							_parameter = " and ((Exposure = 1 and Parameter = 3) or (Exposure = 4 and (Parameter in (5))) or (Exposure in (9,10,13,15,16,32))) ";
						}

						if (Tools.ParamFundScheme)
						{
							_paramFund = " and FundPK = @FundPK ";
						}
						else
						{
							_paramFund = "";
						}


						cmd.CommandTimeout = 0;
						cmd.CommandText = @"

--declare @date date,
--        @instrumentPK int,
--		@fundPK int,
--		@Amount numeric(22,0),
--		@TrxType int,
--		@ClientCode nvarchar(20)

--set @date = '2020-08-26'
--set @instrumentPK = 230
--set @fundPK = 5
--set @Amount = 900000000000
--set @TrxType = 1
--set @ClientCode = '03'



TRUNCATE TABLE ZTEMP_FUNDEXPOSURE

--type di fundexposure 
-- 1 -> | INSTRUMENT TYPE GROUP | DONE
-- 2 | BOND | DONE
-- 3 | SECTOR | DONE
-- 4 | INSTRUMENT TYPE | DONE
-- 5 | EQUITY | DONE
-- 9 | ALL FUND PER BANK | DONE
-- 10 | PER FUND PER BANK | DONE
-- 13 | ISSUER | DONE
-- 14 | INDEX | DONE
-- 15 | SYARIAH ONLY - BELUM
-- 16 | TOTAL PORTFOLIO | DONE
-- 17 | ALL FUND EQUITY < Market CAP - BELUM
-- 18 | ISSUER ALL FUND | DONE
-- 19 | Equity PER FUND Compare to Market CAP || PILIH FUND DAN ALL || parameter =  ALL ONLY hardcode disable (0) ||  MAX DOANK yang muncul
-- 21 | MUTUAL FUND PER COUNTERPART |  PART SENDIRI
-- 22 | DIRECT INVESTMENT  | PART SENDIRI
-- 23 | LAND AND PROPERTY | PART SENDIRI

-- 24 | CAMEL SCORE BANK PER FUND | 
-- 25 | BOND RATING | DONE
-- 26 | TOTAL FOREIGN PORTFOLIO PER FUND |  DONE
-- 27 | KIK EBA PER COUNTERPART | DONE
-- 28 | INVESTMENT OTHER THAN DEPOSIT | DONE
-- 29 | AFFILIATED INVESTMENT | DONE
-- 30 | TOTAL AUM ALL FUND | DONE (EXPOSURE MONITORING ONLY)
-- 31 | TOTAL HOLDING PER FUND | DONE (EXPOSURE MONITORING ONLY)
-- 32 | CAPITAL CLASSIFICATION


--SETUP--
BEGIN
Declare @TrailsPK int
Declare @MaxDateEndDayFP datetime
Select @TrailsPK = EndDayTrailsFundPortfolioPK , @MaxDateEndDayFP = ValueDate From 
endDayTrailsFundPortfolio where 
valuedate = 
(
	Select max(ValueDate) from endDayTrailsFundPortfolio where
	valuedate < @Date  and status = 2  
	--and FundPK = @FundPK 
	" + _paramFund + @"
)
and status = 2  
" + _paramFund + @"
--and FundPK = @FundPK 


Declare @TotalMarketValue numeric(26,6)
Declare @TotalMarketValueAllFund numeric(26,6)
declare @TotalDirectInvestment numeric(26,6)
declare @TotalLandAndProperty numeric(26,6)
declare @FundID nvarchar(100)
declare @InstrumentID nvarchar(100)
Declare @paramCounterpartPK int
declare @paramBondRating int

--select @TotalDirectInvestment = sum(A.NetAmount - isnull(B.NetAmountSell,0)) from DirectInvestment A
--left join SellDirectInvestment B on A.DirectInvestmentPK = B.DirectInvestmentPK and B.status = 2
--where A.Status = 2 and Valuedate <= @Date and fundpk = @FundPK
--group by  A.FundPK,A.DirectInvestmentPK,A.ProjectName
--having sum(A.NetAmount - isnull(B.NetAmountSell,0)) > 0

set @TotalDirectInvestment = isnull(@TotalDirectInvestment,0)

--select @TotalLandAndProperty = sum(A.BuyNetAmount - isnull(A.SellNetAmount,0)) from LandAndProperty A
--where A.Status = 2 and BuyValueDate <= @Date and fundpk = @FundPK
--group by  A.FundPK,A.LandAndPropertyPK,A.Nama
--having sum(A.BuyNetAmount - isnull(A.SellNetAmount,0)) > 0

select @TotalMarketValue = aum From closeNav
where Date = (
	 select max(date) from CloseNAV where date <@Date
	 and FundPK = @FundPK and status = 2
)
and FundPK = @FundPK
and status = 2 

select @TotalMarketValueAllFund = SUM(ISNULL(aum,0)) From closeNav
where Date = (
	 select max(date) from CloseNAV where date < @Date AND status = 2
)
and status = 2 

SET @TotalMarketValueAllFund = ISNULL(@TotalMarketValueAllFund,1)

set @TotalMarketValue = isnull(@TotalMarketValue,1) 

select @FundID = ID from fund where fundpk = @fundPK and status = 2
select @InstrumentID = ID,@paramCounterpartPK = CounterpartPK from Instrument where InstrumentPK = @instrumentPK and status = 2

DECLARE @Exposure TABLE
(

Exposure INT,
ExposureID nvarchar(100) COLLATE DATABASE_DEFAULT,
Parameter INT,
ParameterDesc nvarchar(100) COLLATE DATABASE_DEFAULT,

MarketValue numeric(22,4),
ExposurePercent numeric(18,8),

MinExposurePercent numeric(18,8),
WarningMinExposure numeric(18,8),
AlertWarningMinExposure BIT,
AlertMinExposure bit,

MaxExposurePercent numeric(18,8),
WarningMaxExposure numeric(18,8),
AlertWarningMaxExposure BIT,
AlertMaxExposure BIT,

MinValue NUMERIC(22,4),
WarningMinValue NUMERIC(22,4),
AlertWarningMinValue BIT,
AlertMinValue BIT,

MaxValue NUMERIC(22,4),
WarningMaxValue NUMERIC(22,4),
AlertWarningMaxValue BIT,
AlertMaxValue bit

)


DECLARE @InvestmentPosition TABLE
(
	FundPK INT,
	InstrumentPK INT,
	Amount NUMERIC(22,4)
)

DECLARE @InvestmentPrice TABLE
(
	InstrumentPK INT,
	Price NUMERIC(22,4)
)

DECLARE @QInstrumentPK int
Declare Q Cursor For
	SELECT DISTINCT InstrumentPK FROM dbo.Investment A
	WHERE A.FundPK = @FundPK 
	AND A.ValueDate > @MaxDateEndDayFP AND A.ValueDate <= @Date
	and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3 
Open Q
Fetch Next From Q
INTO @QInstrumentPK
While @@FETCH_STATUS = 0  
BEGIN

	INSERT INTO @InvestmentPrice
	        ( InstrumentPK, Price )
	SELECT A.InstrumentPK,ISNULL(ClosePriceValue,0) FROM ClosePrice A
	left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
	WHERE A.status = 2 AND A.instrumentPK = @QInstrumentPK   and
    date =
	(
		SELECT MAX(Date) FROM dbo.ClosePrice WHERE status = 2 AND instrumentPK = @QInstrumentPK AND date <= @Date
	)
    union all
	select InstrumentPK,1 from Instrument where InstrumentPK = @QInstrumentPK and InstrumentTypePK in (5,10)

	Fetch Next From Q
	INTO @QInstrumentPK
End	
Close Q
Deallocate Q

INSERT INTO @InvestmentPosition
        ( FundPK, InstrumentPK, Amount )
SELECT FundPK,A.InstrumentPK, SUM(ISNULL(CASE WHEN A.DoneVolume > 0 and A.ValueDate != @date THEN A.DoneVolume * isnull(B.Price,0) * CASE WHEN A.TrxType = 2 THEN  -1 ELSE 1 END
ELSE A.DoneAmount * CASE WHEN A.TrxType = 2 THEN  -1 ELSE 1 END END,0)
/ case when A.InstrumentTypePK not in (1,4,5,6,16) then 100 else 1 end
) 
FROM Investment A
LEFT JOIN @InvestmentPrice B ON A.InstrumentPK = B.InstrumentPK 
WHERE  A.FundPK = @FundPK 
AND A.ValueDate > @MaxDateEndDayFP AND A.ValueDate <= @Date
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3 
GROUP BY FundPK,A.InstrumentPK


DECLARE @InvestmentPositionALLFund TABLE
(
	FundPK int,
	InstrumentPK INT,
	Amount NUMERIC(22,4)
)



INSERT INTO @InvestmentPositionALLFund
        ( FundPK,InstrumentPK, Amount )
SELECT A.FundPK,A.InstrumentPK, SUM(ISNULL(CASE WHEN A.DoneVolume > 0 THEN A.DoneVolume * isnull(B.Price,0) * CASE WHEN A.TrxType = 2 THEN  -1 ELSE 1 END 
ELSE A.DoneAmount * CASE WHEN A.TrxType = 2 THEN  -1 ELSE 1 END END,0)
/ case when A.InstrumentTypePK not in (1,4,5,6,16) then 100 else 1 end
) 
FROM Investment A
LEFT JOIN @InvestmentPrice B ON A.InstrumentPK = B.InstrumentPK 
WHERE   A.ValueDate > @MaxDateEndDayFP AND A.ValueDate <= @Date
and A.StatusInvestment <> 3 and A.StatusDealing <> 3 and A.StatusSettlement <> 3 
GROUP BY A.InstrumentPK,A.FundPK

END


--1--
BEGIN
Declare @PositionForExp1 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 1
	)
BEGIN
	INSERT INTO @PositionForExp1
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,1,ISNULL(E.DescOne,''),ISNULL(C.GroupType,0),ISNULL(D.DescOne,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON C.GroupType = D.Code AND D.id = 'InstrumentGroupType' AND D.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 1 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK 
		AND D.DescOne IS NOT NULL
		GROUP BY D.DescOne,C.GroupType,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp1 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp1 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END ) 
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END ) ) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp1
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,1,ISNULL(D.DescOne,''),ISNULL(B.GroupType,0),ISNULL(C.DescOne,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON case when A.InstrumentTypePK in (2,3,8,9,13,15) and A.MaturityDate >= DATEADD(year, -1, @Date) then 5 else A.InstrumentTypePK end = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue C ON B.GroupType = C.Code AND C.id = 'InstrumentGroupType' AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 1 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) 
		AND isnull(@InstrumentPK,0) <> 0
		AND C.DescOne IS NOT NULL
	END
	--
END

END

--2--
BEGIN


Declare @PositionForExp2 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 2
	)
BEGIN
	INSERT INTO @PositionForExp2
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,2,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),@InstrumentID
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 2 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK 
		AND C.GroupType = 2 and B.InstrumentTypePK not in (2,12,13,14,15)
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp2 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp2 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp2
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,2,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),@InstrumentID
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON case when A.InstrumentTypePK in (2,3,8,9,13,15) and A.MaturityDate >= DATEADD(year, -1, @Date) then 5 else A.InstrumentTypePK end = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 2 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) 
		AND isnull(@InstrumentPK,0) <> 0
		AND B.GroupType = 2 and A.InstrumentTypePK not in (2,12,13,14,15)
	END
	--

END



END

--3--
BEGIN

Declare @PositionForExp3 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 3
	)
BEGIN
	INSERT INTO @PositionForExp3
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,3,ISNULL(E.DescOne,''),ISNULL(C.SectorPK,0),ISNULL(D.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.SubSector C ON B.SectorPK =  C.SubSectorPK AND C.status IN (1,2)
		LEFT JOIN Sector D ON C.SectorPK = D.SectorPK AND D.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 3 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK
		AND D.ID IS NOT NULL
		GROUP BY D.ID,C.SectorPK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp3 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp3 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp3
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,3,ISNULL(D.DescOne,''),ISNULL(B.SectorPK,0),ISNULL(C.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.SubSector B ON A.SectorPK =  B.SubSectorPK AND B.status IN (1,2)
		LEFT JOIN Sector C ON B.SectorPK = C.SectorPK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 3 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2)
		AND isnull(@InstrumentPK,0) <> 0
		AND C.ID IS NOT NULL
	END
	--
END


END

--4--
BEGIN
Declare @PositionForExp4 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 4
	)
BEGIN
	INSERT INTO @PositionForExp4
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,4,ISNULL(E.DescOne,''),ISNULL(case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end,0),isnull(C.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON case when B.InstrumentTypePK in (2,3,8,9,13,15) and B.MaturityDate >= DATEADD(year, -1, A.Date) then 5 else B.InstrumentTypePK end = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 4 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK 
		AND C.ID IS NOT NULL
		GROUP BY C.ID,B.InstrumentTypePK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK,B.MaturityDate,A.Date

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp4 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp4 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp4
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,4,ISNULL(D.DescOne,''),ISNULL(case when A.InstrumentTypePK in (2,3,8,9,13,15) and A.MaturityDate >= DATEADD(year, -1, @Date) then 5 else A.InstrumentTypePK end,0),isnull(B.ID,'')
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON case when A.InstrumentTypePK in (2,3,8,9,13,15) and A.MaturityDate >= DATEADD(year, -1, @Date) then 5 else A.InstrumentTypePK end = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 4 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) 
		AND isnull(@InstrumentPK,0) <> 0
		AND B.ID IS NOT NULL
	END
	--
END

END

--5--
BEGIN
Declare @PositionForExp5 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 5
	)
BEGIN
	INSERT INTO @PositionForExp5
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,5,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),@InstrumentID
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 5 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK
		AND C.GroupType = 1
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp5 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp5 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp5
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,5,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),@InstrumentID
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 5 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) 
		AND isnull(@InstrumentPK,0) <> 0
		AND B.GroupType = 1
	END


	--

END


END

--9--
BEGIN
Declare @PositionForExp9 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 9
	)
BEGIN
	INSERT INTO @PositionForExp9
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,9,ISNULL(E.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValueAllFund
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValueAllFund * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 9 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		LEFT JOIN Bank G ON B.BankPK = G.BankPK AND G.status IN (1,2)
		WHERE  Date = @MaxDateEndDayFP 
		AND C.GroupType = 3
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK,G.BankPK,G.ID
		


	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp9 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp9 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValueAllFund * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp9
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,9,ISNULL(D.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'')
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValueAllFund,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValueAllFund,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 9 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		LEFT JOIN Bank G ON A.BankPK = G.BankPK AND G.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2)
		AND B.GroupType = 3
		AND isnull(@InstrumentPK,0) <> 0
	END
	--

END


END

--10--
BEGIN
Declare @PositionForExp10 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 10
	)
BEGIN
	INSERT INTO @PositionForExp10
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,10,ISNULL(E.DescOne,''),ISNULL(G.BankPK,0), ISNULL(G.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 10 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		LEFT JOIN Bank G ON B.BankPK = G.BankPK AND G.status IN (1,2)
		WHERE  Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK
		AND C.GroupType = 3 AND A.FundPK = @FundPK
		AND ISNULL(G.ID,'') <> ''
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK,G.BankPK,G.ID

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp10 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp10 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp10
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,10,ISNULL(D.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'')
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 10 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		LEFT JOIN Bank G ON A.BankPK = G.BankPK AND G.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2)
		AND B.GroupType = 3
		AND ISNULL(G.ID,'')  <> ''
		AND isnull(@InstrumentPK,0) <> 0
	END
	--

END


END

--13--
BEGIN

Declare @PositionForExp13 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 13
	)
BEGIN
	INSERT INTO @PositionForExp13
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,13,ISNULL(E.DescOne,''),ISNULL(C.IssuerPK,0),ISNULL(C.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.Issuer C ON B.IssuerPK =  C.IssuerPK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 13 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK
		AND C.ID IS NOT NULL
		GROUP BY C.ID,C.IssuerPK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp13 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp13 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp13
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,13,ISNULL(D.DescOne,''),ISNULL(B.IssuerPK,0),ISNULL(B.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.Issuer B ON A.IssuerPK =  B.IssuerPK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 13 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2)
		AND isnull(@InstrumentPK,0) <> 0
		AND B.ID IS NOT NULL
	END
	--
END


END

--14--
BEGIN


DECLARE @InstrumentIndex TABLE
(
	InstrumentPK INT,
	[IndexPK] int
)

DECLARE @PInstrumentPK int
Declare P Cursor For
		SELECT DISTINCT InstrumentPK FROM dbo.FundPosition A WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK
Open P
Fetch Next From P
INTO @PInstrumentPK

While @@FETCH_STATUS = 0  
Begin
	
	INSERT INTO @InstrumentIndex
	        ( InstrumentPK, IndexPK )
	SELECT @InstrumentPK,IndexPK FROM dbo.InstrumentIndex WHERE Date = (
		SELECT MAX(Date) FROM dbo.InstrumentIndex WHERE status = 2 AND InstrumentPK = @PInstrumentPK
	)AND InstrumentPK = @InstrumentPK AND Status = 2 
	
	Fetch Next From P
	into @PInstrumentPK
End	
Close P
Deallocate P

Declare @PositionForExp14 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 14
	)
BEGIN
	INSERT INTO @PositionForExp14
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,14,ISNULL(E.DescOne,''),ISNULL(C.IndexPK,0),ISNULL(D.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN @InstrumentIndex C ON B.InstrumentPK =  C.InstrumentPK
		LEFT JOIN dbo.[Index] D ON C.IndexPK = D.IndexPK AND D.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 14 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK
		AND D.ID IS NOT NULL
		GROUP BY D.ID,C.IndexPK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK


	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp14 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp14 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp14
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,14,ISNULL(D.DescOne,''),ISNULL(C.IndexPK,0),ISNULL(C.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN @InstrumentIndex B ON A.InstrumentPK =  B.InstrumentPK
		LEFT JOIN dbo.[Index] C ON B.IndexPK = C.IndexPK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 14 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2)
		AND isnull(@InstrumentPK,0) <> 0
		AND C.ID IS NOT NULL
	END
	--
END


END

--16--
BEGIN
Declare @PositionForExp16 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,8),
	AUM numeric(22,8),
	ExposurePercent numeric(18,8)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 16
	)
BEGIN
	INSERT INTO @PositionForExp16
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,16,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),@InstrumentID
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 16 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK  
		AND B.ID IS NOT NULL
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	INSERT INTO @PositionForExp16
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,16,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),@InstrumentID
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.Amount,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.Amount,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.Investment A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 16 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND ValueDate = @date and TrxType <> 3 AND StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3  
		AND B.ID IS NOT NULL
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK
	

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp16 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp16 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp16
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,16,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),@InstrumentID
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 16 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2)  
		AND isnull(@InstrumentPK,0) <> 0
	END
	--
END

END

--18--
BEGIN

Declare @PositionForExp18 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)


IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = 0 AND status = 2
	AND Type = 18
	)
BEGIN
	INSERT INTO @PositionForExp18
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,18,ISNULL(E.DescOne,''),ISNULL(C.IssuerPK,0),ISNULL(C.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValueAllFund
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValueAllFund * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.Issuer C ON B.IssuerPK =  C.IssuerPK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 18 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE  Date = @MaxDateEndDayFP 
		AND C.ID IS NOT NULL
		GROUP BY C.ID,C.IssuerPK,A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	
	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp18 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp18 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValueAllFund * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp18
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,18,ISNULL(D.DescOne,''),ISNULL(B.IssuerPK,0),ISNULL(B.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValueAllFund,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValueAllFund,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.Issuer B ON A.IssuerPK =  B.IssuerPK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 18 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2)
		AND isnull(@InstrumentPK,0) <> 0
		AND B.ID IS NOT NULL
	END
	--
END


END

--19--
BEGIN


Declare @PositionForExp19 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(20,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 19
	)
BEGIN
	INSERT INTO @PositionForExp19
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,19,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(B.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
	    ,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 19 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK 
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp19 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp19 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp19
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,19,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 19 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) 
		AND isnull(@InstrumentPK,0) <> 0
	END
	--

END



END

--24--
BEGIN


Declare @PositionForExp24 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(20,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 24
	)
BEGIN
	INSERT INTO @PositionForExp24
				    ( FundPK ,
					    Exposure ,
					    ExposureDesc,
					    Parameter ,
					    ParameterDesc ,
					    InstrumentPK,
					    InstrumentID ,
					    MarketValue ,
					    AUM ,
					    ExposurePercent
				    )
	SELECT A.FundPK,24,ISNULL(E.DescOne,''),ISNULL(F.BankPK,0),isnull(F.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
		,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 24 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		left join Bank F on A.BankPK = F.BankPK and F.Status in (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK and B.InstrumentTypePK in (5,10)
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK,F.ID,F.BankPK

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp24 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp24 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp24
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,24,ISNULL(D.DescOne,''),ISNULL(A.BankPK,0),ISNULL(E.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 24 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		left join Bank E on A.BankPK = E.BankPK and E.Status in (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) and A.InstrumentTypePK in (5,10)
		AND isnull(@InstrumentPK,0) <> 0
	END
	--

END


END

----25--
--BEGIN


--Declare @PositionForExp25 TABLE
--(
--	FundPK INT,
--	Exposure INT,
--	ExposureDesc NVARCHAR(200),
--	Parameter INT,
--	ParameterDesc NVARCHAR(200),
--	InstrumentPK INT,
--	InstrumentID NVARCHAR(100),
--	MarketValue NUMERIC(22,4),
--	AUM numeric(22,4),
--	ExposurePercent numeric(20,4)
--)

--select @paramBondRating = parameter from dbo.FundExposure WHERE FundPK = @FundPK and status = 2
--AND Type = 25

--IF EXISTS(
--	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
--			AND Type = 25
--	)
--BEGIN
--	INSERT INTO @PositionForExp25
--	        ( FundPK ,
--	          Exposure ,
--			  ExposureDesc,
--	          Parameter ,
--			  ParameterDesc ,
--			  InstrumentPK,
--	          InstrumentID ,
--	          MarketValue ,
--	          AUM ,
--	          ExposurePercent
--	        )
--	SELECT A.FundPK,25,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),isnull(B.ID + ', Rating :' + B.BondRating,'') 
--	,ISNULL(A.InstrumentPK,0)
--	,ISNULL(B.ID ,'')
--	,SUM(ISNULL(A.MarketValue,0)) MarketValue
--	,@TotalMarketValue
--	,100 ExposurePercent
--	FROM dbo.FundPosition A
--	LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
--	LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
--	LEFT JOIN dbo.MasterValue E ON E.Code = 25 AND E.ID = 'ExposureType' AND E.status IN (1,2)
--	left join dbo.MasterValue F ON B.BondRating = F.Code AND F.ID = 'BondRating' AND F.status IN (1,2)
--	WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP and B.InstrumentTypePK in (2,3,8,9,13,15) and BondRating <> '' and F.Priority > @paramBondRating
--	group by A.FundPK,E.DescOne,A.InstrumentPK,B.ID,B.BondRating

--	-- HANDLE ORDER PRETRADE DISINI
--	IF EXISTS(
--		SELECT TOP 1 * FROM @PositionForExp25 WHERE InstrumentPK = @InstrumentPK
--	)
--	BEGIN
--		UPDATE @PositionForExp25 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
--		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
--		WHERE InstrumentPK = @InstrumentPK
--	END
--	ELSE
--	BEGIN
--		INSERT INTO @PositionForExp25
--		        ( FundPK ,
--		          Exposure ,
--		          ExposureDesc ,
--		          Parameter ,
--		          ParameterDesc ,
--		          InstrumentPK ,
--		          InstrumentID ,
--		          MarketValue ,
--		          AUM ,
--		          ExposurePercent
--		        )
--		SELECT @FundPK,25,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID + ', Rating :' + A.BondRating,'') 
--		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
--		,ISNULL(@Amount,0)
--		,ISNULL(@TotalMarketValue,0)
--		,100
--		FROM dbo.Instrument A
--		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
--		LEFT JOIN dbo.MasterValue D ON D.Code = 25 AND D.ID = 'ExposureType' AND D.status IN (1,2)
--		left join dbo.MasterValue F ON A.BondRating = F.Code AND F.ID = 'BondRating' AND F.status IN (1,2)
--		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) and A.InstrumentTypePK in (2,3,8,9,13,15) and A.BondRating <> '' and F.Priority > @paramBondRating
--		AND isnull(@InstrumentPK,0) <> 0
--	END
--	--

--END



--END

--26--
BEGIN


Declare @PositionForExp26 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(20,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 26
	)
BEGIN
	INSERT INTO @PositionForExp26
				    ( FundPK ,
					    Exposure ,
					    ExposureDesc,
					    Parameter ,
					    ParameterDesc ,
					    InstrumentPK,
					    InstrumentID ,
					    MarketValue ,
					    AUM ,
					    ExposurePercent
				    )
	SELECT A.FundPK,26,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),isnull(B.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
		,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 26 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK 
		and B.BitIsForeign = 1
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp26 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp26 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp26
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,26,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(B.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 26 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) and A.BitIsForeign = 1
		AND isnull(@InstrumentPK,0) <> 0
	END
	--

END


END

--27--
BEGIN


Declare @PositionForExp27 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(20,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 27
	)
BEGIN
	INSERT INTO @PositionForExp27
				    ( FundPK ,
					    Exposure ,
					    ExposureDesc,
					    Parameter ,
					    ParameterDesc ,
					    InstrumentPK,
					    InstrumentID ,
					    MarketValue ,
					    AUM ,
					    ExposurePercent
				    )
	SELECT A.FundPK,27,ISNULL(E.DescOne,''),ISNULL(B.CounterpartPK,0),isnull(F.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
		,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 27 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		left join Counterpart F on B.CounterpartPK = F.CounterpartPK and F.Status in (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK 
		and B.InstrumentTypePK in (8)
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK, B.CounterpartPK,F.ID

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp27 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp27 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp27
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,27,ISNULL(D.DescOne,''),ISNULL(A.CounterpartPK,0),ISNULL(E.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 27 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		left join Counterpart E on A.CounterpartPK = E.CounterpartPK and E.Status in (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) and B.InstrumentTypePK in (8)
		AND isnull(@InstrumentPK,0) <> 0
	END
	--

END


END

--28--
BEGIN


Declare @PositionForExp28 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(20,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 28
	)
BEGIN
	INSERT INTO @PositionForExp28
				    ( FundPK ,
					    Exposure ,
					    ExposureDesc,
					    Parameter ,
					    ParameterDesc ,
					    InstrumentPK,
					    InstrumentID ,
					    MarketValue ,
					    AUM ,
					    ExposurePercent
				    )
	SELECT A.FundPK,28,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),isnull(B.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
		,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue + @TotalDirectInvestment + @TotalLandAndProperty,0)) /  (@TotalMarketValue) * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 28 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		left join Counterpart F on B.CounterpartPK = F.CounterpartPK and F.Status in (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK and B.InstrumentTypePK not in (5,10)
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp28 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp28 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / (@TotalMarketValue) * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp28
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,28,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount + @TotalDirectInvestment + @TotalLandAndProperty,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 28 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) and A.InstrumentTypePK not in (5,10)
		AND isnull(@InstrumentPK,0) <> 0
	END
	--

END


END

--29--
BEGIN


Declare @PositionForExp29 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(20,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK and status = 2
	AND Type = 29
	)
BEGIN
	INSERT INTO @PositionForExp29
				    ( FundPK ,
					    Exposure ,
					    ExposureDesc,
					    Parameter ,
					    ParameterDesc ,
					    InstrumentPK,
					    InstrumentID ,
					    MarketValue ,
					    AUM ,
					    ExposurePercent
				    )
	SELECT A.FundPK,29,ISNULL(E.DescOne,''),ISNULL(A.InstrumentPK,0),isnull(B.ID,'')
		,ISNULL(A.InstrumentPK,0)
		,ISNULL(B.ID ,'')
		,SUM(ISNULL(A.MarketValue,0)) MarketValue
		,@TotalMarketValue
		, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
		LEFT JOIN dbo.MasterValue E ON E.Code = 29 AND E.ID = 'ExposureType' AND E.status IN (1,2)
		WHERE A.FundPK = @FundPK AND Date = @MaxDateEndDayFP AND TrailsPK = @TrailsPK 
		and B.Affiliated = 1
		GROUP BY A.FundPK,B.ID,E.DescOne,A.InstrumentPK

	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp29 WHERE InstrumentPK = @InstrumentPK
	)
	BEGIN
		UPDATE @PositionForExp29 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @InstrumentPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp29
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,29,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
		,ISNULL(@InstrumentPK,0),ISNULL(A.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 29 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2) and A.Affiliated = 1
		AND isnull(@InstrumentPK,0) <> 0
	END
	--

END


END




--32--
BEGIN
Declare @PositionForExp32 TABLE
(
	FundPK INT,
	Exposure INT,
	ExposureDesc NVARCHAR(200),
	Parameter INT,
	ParameterDesc NVARCHAR(200),
	InstrumentPK INT,
	InstrumentID NVARCHAR(100),
	MarketValue NUMERIC(22,4),
	AUM numeric(22,4),
	ExposurePercent numeric(18,4)
)

IF EXISTS(
	SELECT TOP 1 Type FROM dbo.FundExposure WHERE FundPK = @FundPK AND status = 2
	AND Type = 32
	)
BEGIN
	INSERT INTO @PositionForExp32
	        ( FundPK ,
	          Exposure ,
			  ExposureDesc,
	          Parameter ,
			  ParameterDesc ,
			  InstrumentPK,
	          InstrumentID ,
	          MarketValue ,
	          AUM ,
	          ExposurePercent
	        )
	SELECT A.FundPK,32,ISNULL(E.DescOne,''),ISNULL(G.CapitalClassification,0)
	,case when G.CapitalClassification = 1 then 'Capital Classification 1'
			when G.CapitalClassification = 2 then 'Capital Classification 2'
				when G.CapitalClassification = 3 then 'Capital Classification 3'
					else 'Capital Classification 4' end
	,isnull(G.BankPK,0)
	,isnull(G.ID,'')
	,SUM(ISNULL(A.MarketValue,0)) MarketValue
	,@TotalMarketValue
	, SUM(ISNULL(A.MarketValue,0)) /  @TotalMarketValue * 100 ExposurePercent
	FROM dbo.FundPosition A
	LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
	LEFT JOIN dbo.InstrumentType C ON B.InstrumentTypePK = C.InstrumentTypePK AND C.status IN (1,2)
	LEFT JOIN dbo.MasterValue E ON E.Code = 32 AND E.ID = 'ExposureType' AND E.status IN (1,2)
	LEFT JOIN Bank G ON B.BankPK = G.BankPK AND G.status IN (1,2)
	WHERE  Date = @MaxDateEndDayFP 
	AND C.GroupType = 3 and A.Status = 2
	AND ISNULL(G.CapitalClassification,0) <> 0 AND TrailsPK = @TrailsPK 
	-- PARAM DISINI
	AND A.FundPK = @FundPK 
	GROUP BY A.FundPK,B.ID,E.DescOne,G.CapitalClassification,G.BankPK,G.ID	

	declare @BankPK int
	select @BankPK = BankPK From Instrument where InstrumentPK  = @InstrumentPK
	-- HANDLE ORDER PRETRADE DISINI
	IF EXISTS(
		SELECT TOP 1 * FROM @PositionForExp32 WHERE InstrumentPK = @BankPK
	)
	BEGIN
		UPDATE @PositionForExp32 SET MarketValue = MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )
		,ExposurePercent = (MarketValue + (ISNULL(@Amount,0)  * CASE WHEN @trxType = 2 THEN  -1 ELSE 1 END )) / @TotalMarketValue * 100
		WHERE InstrumentPK = @BankPK
	END
	ELSE
	BEGIN
		INSERT INTO @PositionForExp32
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
		SELECT @FundPK,32,ISNULL(D.DescOne,''),ISNULL(G.CapitalClassification,0),
		case when G.CapitalClassification = 1 then 'Capital Classification 1' 
		when G.CapitalClassification = 2 then 'Capital Classification 2' 
			when G.CapitalClassification = 3 then 'Capital Classification 3' 
				else 'Capital Classification 4' end 
		,ISNULL(G.BankPK,0),ISNULL(G.ID,'')
		,ISNULL(@Amount,0)
		,ISNULL(@TotalMarketValue,0)
		,ISNULL(@Amount,0)/ISNULL(@TotalMarketValue,0) * 100 
		FROM dbo.Instrument A
		LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		LEFT JOIN dbo.MasterValue D ON D.Code = 32 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		LEFT JOIN Bank G ON A.BankPK = G.BankPK AND G.status IN (1,2)
		WHERE A.InstrumentPK = @InstrumentPK AND A.status IN (1,2)
		AND B.GroupType = 3
		AND ISNULL(G.ID,'')  <> ''
		AND isnull(@InstrumentPK,0) <> 0
		AND ISNULL(G.CapitalClassification,0) <> 0 

	END
	--

END


END



-- HANDLE DATA INVESTMENT ALL FUND
BEGIN
	DECLARE @EInstrumentPK INT
    DECLARE @EAmount NUMERIC(22,4)
	DECLARE @EFundPK INT

	DECLARE E Cursor For
			SELECT InstrumentPK,Amount,FundPK FROM @InvestmentPositionALLFund 
	Open E
	Fetch Next From E
	INTO @EInstrumentPK,@EAmount,@EFundPK
	While @@FETCH_STATUS = 0  
	BEGIN


		--9--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp9 WHERE InstrumentPK = @InstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp9 SET MarketValue = MarketValue + ISNULL(@EAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@EAmount,0)) / @TotalMarketValueAllFund * 100
			WHERE InstrumentPK = @EInstrumentPK AND FundPK = @FundPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp9
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @EFundPK,9,ISNULL(D.DescOne,''),ISNULL(G.BankPK,0),ISNULL(G.ID,'') 
			,ISNULL(@EInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@EAmount,0)
			,ISNULL(@TotalMarketValueAllFund,0)
			,ISNULL(@EAmount,0)/ISNULL(@TotalMarketValueAllFund,0) * 100 
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 9 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			LEFT JOIN Bank G ON A.BankPK = G.BankPK AND G.status IN (1,2)
			WHERE A.InstrumentPK = @EInstrumentPK AND A.status IN (1,2)
			AND isnull(@EInstrumentPK,0) <> 0
			AND B.GroupType = 3
		END


		--18--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp18 WHERE InstrumentPK = @InstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp18 SET MarketValue = MarketValue + ISNULL(@EAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@EAmount,0)) / @TotalMarketValueAllFund * 100
			WHERE InstrumentPK = @EInstrumentPK AND FundPK = @FundPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp18
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @EFundPK,18,ISNULL(D.DescOne,''),ISNULL(A.IssuerPK,0),ISNULL(B.ID,'') 
			,ISNULL(@EInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@EAmount,0)
			,ISNULL(@TotalMarketValueAllFund,0)
			,ISNULL(@EAmount,0)/ISNULL(@TotalMarketValueAllFund,0) * 100 
			FROM dbo.Instrument A
			LEFT JOIN dbo.Issuer B ON A.IssuerPK =  B.IssuerPK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 18 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @EInstrumentPK AND A.status IN (1,2)
			AND isnull(@EInstrumentPK,0) <> 0
			AND ISNULL(B.ID,'')  <> ''
		
		END

	FETCH Next From E
		into @EInstrumentPK,@EAmount,@EFundPK
	End	
	Close E
	Deallocate E

END

-- HANDLE DATA INVESTMENT PER FUND
BEGIN

	DECLARE @maxCap NUMERIC(22,4)
	DECLARE @WInstrumentPK INT
    DECLARE @WAmount NUMERIC(22,4)
	DECLARE W Cursor For
			SELECT InstrumentPK,Amount FROM @InvestmentPosition WHERE fundPK = @FundPK
	Open W
	Fetch Next From W
	INTO @WInstrumentPK,@WAmount
	
	While @@FETCH_STATUS = 0  
	BEGIN
	
		--1--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp1 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp1 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

		
			INSERT INTO @PositionForExp1
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,1,ISNULL(D.DescOne,''),ISNULL(B.GroupType,0),ISNULL(C.DescOne,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue C ON B.GroupType = C.Code AND C.id = 'InstrumentGroupType' AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 1 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
			AND C.DescOne IS NOT NULL
		END

		--2--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp2 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp2 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp2
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,2,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 2 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND B.GroupType = 2 and A.InstrumentTypePK not in (2,12,13,14,15)
			AND isnull(@WInstrumentPK,0) <> 0
		END
		
		--3--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp3 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp3 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp3
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,3,ISNULL(D.DescOne,''),ISNULL(B.SectorPK,0),ISNULL(C.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.SubSector B ON A.SectorPK =  B.SubSectorPK AND B.status IN (1,2)
			LEFT JOIN Sector C ON B.SectorPK = C.SectorPK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 3 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
		END

		--4--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp4 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp4 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp4
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,4,ISNULL(D.DescOne,''),ISNULL(A.InstrumentTypePK,0),ISNULL(B.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 4 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0 AND isnull(B.ID,'') <> ''
		END

		--5--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp5 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp5 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp5
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,5,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 5 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
			AND B.GroupType = 1
		END

		--10--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp10 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp10 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp10
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,10,ISNULL(D.DescOne,''),ISNULL(G.BankPK,0), 'FUND : ' + @FundID  + ', BANK : ' + ISNULL(G.ID,'')
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 10 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			LEFT JOIN Bank G ON A.BankPK = G.BankPK AND G.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND ISNULL(G.ID,'')  <> ''
			AND isnull(@WInstrumentPK,0) <> 0
		END

		--13--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp13 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp13 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp13
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,13,ISNULL(D.DescOne,''),ISNULL(B.IssuerPK,0),ISNULL(B.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.Issuer B ON A.IssuerPK = B.IssuerPK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 13 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
			AND ISNULL(B.ID,'')  <> ''
		END

		--14--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp14 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp14 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN
			INSERT INTO @PositionForExp14
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,14,ISNULL(D.DescOne,''),ISNULL(B.IndexPK,0),ISNULL(C.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN @InstrumentIndex B ON A.InstrumentPK =  B.InstrumentPK
			LEFT JOIN dbo.[Index] C ON B.IndexPK = C.IndexPK AND C.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 14 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND C.ID IS NOT null
			AND isnull(@WInstrumentPK,0) <> 0
			AND ISNULL(C.ID,'')  <> ''
		END

		----16--
		--IF EXISTS(
		--		SELECT TOP 1 * FROM @PositionForExp16 WHERE InstrumentPK = @WInstrumentPK
		--)
		--BEGIN
		--	UPDATE @PositionForExp16 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
		--	,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
		--	WHERE InstrumentPK = @WInstrumentPK
		--END
		--ELSE
		--BEGIN

		
		--	INSERT INTO @PositionForExp16
		--        ( FundPK ,
		--          Exposure ,
		--          ExposureDesc ,
		--          Parameter ,
		--          ParameterDesc ,
		--          InstrumentPK ,
		--          InstrumentID ,
		--          MarketValue ,
		--          AUM ,
		--          ExposurePercent
		--        )
		--	SELECT @FundPK,16,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
		--	,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
		--	,ISNULL(@WAmount,0)
		--	,ISNULL(@TotalMarketValue,0)
		--	,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
		--	FROM dbo.Instrument A
		--	LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		--	LEFT JOIN dbo.MasterValue D ON D.Code = 16 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		--	WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
		--	AND isnull(@WInstrumentPK,0) <> 0
		--END

		--19--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp19 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp19 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

			SELECT @maxCap = marketCAP FROM dbo.InstrumentIndex WHERE date  =
			(
				SELECT MAX(date) FROM instrumentIndex WHERE status = 2 AND Date<= @date AND InstrumentPK = @WInstrumentPK
			)AND status = 2 AND instrumentPK = @WInstrumentPK

			INSERT INTO @PositionForExp19
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,19,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,ISNULL(@WAmount,0)/case when isnull(@maxCap,0) = 0 then 1 else  ISNULL(@maxCap,@WAmount) end * 100 
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 19 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND B.GroupType = 2
			AND isnull(@WInstrumentPK,0) <> 0
		END

		--24--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp24 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp24 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

		
			INSERT INTO @PositionForExp24
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,24,ISNULL(D.DescOne,''),ISNULL(A.BankPK,0),ISNULL(E.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 24 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			LEFT JOIN dbo.Bank E ON A.BankPK = E.BankPK AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)
			AND isnull(@WInstrumentPK,0) <> 0
		END

		----25--
		--IF EXISTS(
		--		SELECT TOP 1 * FROM @PositionForExp25 WHERE InstrumentPK = @WInstrumentPK
		--)
		--BEGIN
		--	UPDATE @PositionForExp25 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
		--	,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
		--	WHERE InstrumentPK = @WInstrumentPK
		--END
		--ELSE
		--BEGIN
		--	select @paramBondRating = parameter from dbo.FundExposure WHERE FundPK = @FundPK and status = 2
		--	AND Type = 25


		--	INSERT INTO @PositionForExp25
		--        ( FundPK ,
		--          Exposure ,
		--          ExposureDesc ,
		--          Parameter ,
		--          ParameterDesc ,
		--          InstrumentPK ,
		--          InstrumentID ,
		--          MarketValue ,
		--          AUM ,
		--          ExposurePercent
		--        )
		--	SELECT @FundPK,25,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID + ', Rating :' + A.BondRating,'') 
		--	,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
		--	,ISNULL(@WAmount,0)
		--	,ISNULL(@TotalMarketValue,0)
		--	,100
		--	FROM dbo.Instrument A
		--	LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
		--	LEFT JOIN dbo.MasterValue D ON D.Code = 25 AND D.ID = 'ExposureType' AND D.status IN (1,2)
		--	left join dbo.MasterValue F ON A.BondRating = F.Code AND F.ID = 'BondRating' AND F.status IN (1,2)
		--	WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and A.InstrumentTypePK in (2,3,8,9,13,15) and A.BondRating <> '' and F.Priority > @paramBondRating
		--	AND isnull(@WInstrumentPK,0) <> 0

		--END

		--26--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp26 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp26 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

		
			INSERT INTO @PositionForExp26
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,26,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 26 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and A.BitIsForeign = 1
			AND isnull(@WInstrumentPK,0) <> 0
		END

		--27--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp27 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp27 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

		
			INSERT INTO @PositionForExp27
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,27,ISNULL(D.DescOne,''),ISNULL(A.CounterpartPK,0),ISNULL(E.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 27 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			left join Counterpart E on A.CounterpartPK = E.CounterpartPK and E.Status in (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2)  and A.InstrumentTypePK = 8
			AND isnull(@WInstrumentPK,0) <> 0
		END

		--28--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp28 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp28 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

		
			INSERT INTO @PositionForExp28
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,28,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount + @TotalDirectInvestment + @TotalLandAndProperty,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 28 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and A.InstrumentTypePK not in (5,10)
			AND isnull(@WInstrumentPK,0) <> 0
		END
		
		--29--
		IF EXISTS(
				SELECT TOP 1 * FROM @PositionForExp29 WHERE InstrumentPK = @WInstrumentPK
		)
		BEGIN
			UPDATE @PositionForExp29 SET MarketValue = MarketValue + ISNULL(@WAmount,0)
			,ExposurePercent = (MarketValue + ISNULL(@WAmount,0)) / @TotalMarketValue * 100
			WHERE InstrumentPK = @WInstrumentPK
		END
		ELSE
		BEGIN

		
			INSERT INTO @PositionForExp29
		        ( FundPK ,
		          Exposure ,
		          ExposureDesc ,
		          Parameter ,
		          ParameterDesc ,
		          InstrumentPK ,
		          InstrumentID ,
		          MarketValue ,
		          AUM ,
		          ExposurePercent
		        )
			SELECT @FundPK,29,ISNULL(D.DescOne,''),ISNULL(A.InstrumentPK,0),ISNULL(A.ID,'') 
			,ISNULL(@WInstrumentPK,0),ISNULL(A.ID,'')
			,ISNULL(@WAmount,0)
			,ISNULL(@TotalMarketValue,0)
			,case when @TotalMarketValue = 0 then 0 else ISNULL(@WAmount,0)/ISNULL(@TotalMarketValue,0) * 100 end
			FROM dbo.Instrument A
			LEFT JOIN dbo.InstrumentType B ON A.InstrumentTypePK = B.InstrumentTypePK AND B.status IN (1,2)
			LEFT JOIN dbo.MasterValue D ON D.Code = 29 AND D.ID = 'ExposureType' AND D.status IN (1,2)
			WHERE A.InstrumentPK = @WInstrumentPK AND A.status IN (1,2) and A.BitIsForeign = 1
			AND isnull(@WInstrumentPK,0) <> 0
		END
		
		FETCH Next From W
		into @WInstrumentPK,@WAmount
	End	
	Close W
	Deallocate W



END


-- PROSES EXPOSURE AKHIR
BEGIN

DECLARE @CType INT
DECLARE @CParameter INT
DECLARE @CMinExp numeric(18,4)
DECLARE @CMaxExp numeric(18,4)
DECLARE @CWarningMinExp numeric(18,4)
DECLARE @CWarningMaxExp numeric(18,4)
DECLARE @CMinVal NUMERIC(22,4)
DECLARE @CMaxVal NUMERIC(22,4)
DECLARE @CWarningMinVal numeric(18,4)
DECLARE @CWarningMaxVal numeric(18,4)

Declare A Cursor For
	SELECT CAST(Type AS INT) Type,Parameter,MinExposurePercent,MaxExposurePercent 
	,WarningMinExposurePercent,WarningMaxExposurePercent,MinValue
	,MaxValue,WarningMinValue,WarningMaxValue
	FROM dbo.FundExposure WHERE status = 2 
	AND (FundPK = @FundPK or FundPK = 0) 
	ORDER BY Type asc
Open A
Fetch Next From A
INTO @CType,@CParameter,@CMinExp,@CMaxExp,@CWarningMinExp,@CWarningMaxExp,@CMinVal,@CMaxVal
,@CWarningMinVal,@CWarningMaxVal
While @@FETCH_STATUS = 0  
Begin

	IF @CType = 1
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )

		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp1
		WHERE Parameter = @CParameter 
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 2
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp2
		WHERE Parameter = @CParameter and InstrumentPK = @InstrumentPK
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 2 AND @CParameter = 0
	BEGIN
	INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
	SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp2
		WHERE InstrumentPK = @InstrumentPK
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 3
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp3
		WHERE Parameter = @CParameter 
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 3 AND @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc  + ' | ALL PARAM ',
		Parameter,ParameterDesc 
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp3
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 4
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp4
		WHERE Parameter = @CParameter
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 5
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp5
		WHERE Parameter = @CParameter and InstrumentPK = @InstrumentPK
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 5 AND @CParameter = 0
	BEGIN
	INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
	SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp5
		WHERE InstrumentPK = @InstrumentPK
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 9
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp9
		WHERE Parameter = @CParameter 
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 9 AND @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp9
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 10
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp10
		WHERE Parameter = @CParameter
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 10 AND @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp10 
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 13
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp13
		WHERE Parameter = @CParameter and InstrumentPK = @InstrumentPK
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 13 AND @CParameter = 0 -- CUSTOM 20, UNTUK TIPE ISSUER DAN PARAMETER ALL, ISSUERPK 419 DI EXCLUDE
	BEGIN
		IF (@ClientCode = '20')
		BEGIN
			INSERT INTO @Exposure
					( 
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp13 A
			left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
			WHERE A.InstrumentPK = @InstrumentPK and B.IssuerPK <> 419
			GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
		END
		ELSE
		BEGIN
			INSERT INTO @Exposure
					( 
					  Exposure,
					  ExposureID ,
					  Parameter ,
					  ParameterDesc ,
					  MarketValue ,
					  ExposurePercent ,
					  MinExposurePercent ,
					  WarningMinExposure ,
					  AlertWarningMinExposure,
					  AlertMinExposure ,
					  MaxExposurePercent ,
					  WarningMaxExposure ,
					  AlertWarningMaxExposure ,
					  AlertMaxExposure ,
					  MinValue ,
					  WarningMinValue ,
					  AlertWarningMinValue ,
					  AlertMinValue ,
					  MaxValue ,
					  WarningMaxValue ,
					  AlertWarningMaxValue ,
					  AlertMaxValue
					)
			SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
			Parameter,ParameterDesc
			,SUM(ISNULL(MarketValue,0)) MarketValue
			, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
			,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
			FROM @PositionForExp13
			WHERE InstrumentPK = @InstrumentPK
			GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
		END
		
	END


	IF @CType = 14
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp14
		WHERE Parameter = @CParameter
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 14 AND @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp14
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 16
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )

		SELECT Exposure,ExposureDesc,
		0,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp16
		GROUP BY Exposure,ExposureDesc,ParameterDesc
	END

	IF @CType = 18
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp18
		WHERE Parameter = @CParameter
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 18 and @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp18
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 19 AND @CParameter = 0
	BEGIN
	
			SELECT @maxCap = marketCAP FROM dbo.InstrumentIndex WHERE date  =
			(
				SELECT MAX(date) FROM instrumentIndex WHERE indexPK = 4 AND status = 2 AND Date<= @date AND InstrumentPK = @InstrumentPK
			)AND indexPK = 4 AND status = 2 AND instrumentPK = @InstrumentPK


	INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
	SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / isnull(@maxCap,1) * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp19 
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	--IF @CType = 24
	--BEGIN
	--	INSERT INTO @Exposure
	--	        ( 
	--			  Exposure,
	--			  ExposureID ,
	--	          Parameter ,
	--			  ParameterDesc ,
	--	          MarketValue ,
	--	          ExposurePercent ,
	--	          MinExposurePercent ,
	--	          WarningMinExposure ,
	--			  AlertWarningMinExposure,
	--	          AlertMinExposure ,
	--	          MaxExposurePercent ,
	--	          WarningMaxExposure ,
	--			  AlertWarningMaxExposure ,
	--	          AlertMaxExposure ,
	--	          MinValue ,
	--	          WarningMinValue ,
	--			  AlertWarningMinValue ,
	--	          AlertMinValue ,
	--	          MaxValue ,
	--	          WarningMaxValue ,
	--			  AlertWarningMaxValue ,
	--	          AlertMaxValue
	--	        )
	--	SELECT Exposure,ExposureDesc,
	--	Parameter,ParameterDesc
	--	,SUM(ISNULL(MarketValue,0)) MarketValue
	--	, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
	--	,0,0,0,0,isnull(C.ExposurePercent,0),0,0,0,0,0,0,0,0,0,0,0
	--	FROM @PositionForExp24 A
	--	left join Bank B on A.Parameter = B.BankPK and B.Status in (1,2)
	--	left join CamelMapping C on B.CamelScore between C.FromValue and C.ToValue and A.FundPK = C.FundPK
	--	WHERE Parameter = @CParameter
	--	GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,C.ExposurePercent
	--END

	--IF @CType = 24 and @CParameter = 0
	--BEGIN
	--	INSERT INTO @Exposure
	--	        ( 
	--			  Exposure,
	--			  ExposureID ,
	--	          Parameter ,
	--			  ParameterDesc ,
	--	          MarketValue ,
	--	          ExposurePercent ,
	--	          MinExposurePercent ,
	--	          WarningMinExposure ,
	--			  AlertWarningMinExposure,
	--	          AlertMinExposure ,
	--	          MaxExposurePercent ,
	--	          WarningMaxExposure ,
	--			  AlertWarningMaxExposure ,
	--	          AlertMaxExposure ,
	--	          MinValue ,
	--	          WarningMinValue ,
	--			  AlertWarningMinValue ,
	--	          AlertMinValue ,
	--	          MaxValue ,
	--	          WarningMaxValue ,
	--			  AlertWarningMaxValue ,
	--	          AlertMaxValue
	--	        )
	--	SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
	--	Parameter,ParameterDesc
	--	,SUM(ISNULL(MarketValue,0)) MarketValue
	--	, SUM(ISNULL(MarketValue,0)) / @TotalMarketValueAllFund * 100 ExposurePercent
	--	,0,0,0,0,isnull(C.ExposurePercent,0),0,0,0,0,0,0,0,0,0,0,0
	--	FROM @PositionForExp24 A
	--	left join Bank B on A.Parameter = B.BankPK and B.Status in (1,2)
	--	left join CamelMapping C on B.CamelScore between C.FromValue and C.ToValue and A.FundPK = C.FundPK
	--	GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc,C.ExposurePercent
	--END

	IF @CType = 25
	BEGIN
		IF EXISTS (
			select * from Instrument A
			left join MasterValue B on A.BondRating = B.DescOne and B.Status in (1,2) and B.ID = 'BondRating'
			where A.Status in (1,2) and B.Priority > @CParameter and InstrumentPK = @instrumentPK
		)
		BEGIN

		INSERT INTO @Exposure
					( 
						Exposure,
						ExposureID ,
						Parameter ,
						ParameterDesc ,
						MarketValue ,
						ExposurePercent ,
						MinExposurePercent ,
						WarningMinExposure ,
						AlertWarningMinExposure,
						AlertMinExposure ,
						MaxExposurePercent ,
						WarningMaxExposure ,
						AlertWarningMaxExposure ,
						AlertMaxExposure ,
						MinValue ,
						WarningMinValue ,
						AlertWarningMinValue ,
						AlertMinValue ,
						MaxValue ,
						WarningMaxValue ,
						AlertWarningMaxValue ,
						AlertMaxValue
					)

			SELECT 25,'BOND RATING',
			@InstrumentPK,ID + ' ,Rating :' + isnull(BondRating,0)
			,@Amount
			,100 ExposurePercent
			,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0 from Instrument where status in (1,2) and InstrumentPK  = @InstrumentPK
			
		END
	END
	
	--IF @CType = 26
	--BEGIN
	--	INSERT INTO @Exposure
	--	        ( 
	--			  Exposure,
	--			  ExposureID ,
	--	          Parameter ,
	--			  ParameterDesc ,
	--	          MarketValue ,
	--	          ExposurePercent ,
	--	          MinExposurePercent ,
	--	          WarningMinExposure ,
	--			  AlertWarningMinExposure,
	--	          AlertMinExposure ,
	--	          MaxExposurePercent ,
	--	          WarningMaxExposure ,
	--			  AlertWarningMaxExposure ,
	--	          AlertMaxExposure ,
	--	          MinValue ,
	--	          WarningMinValue ,
	--			  AlertWarningMinValue ,
	--	          AlertMinValue ,
	--	          MaxValue ,
	--	          WarningMaxValue ,
	--			  AlertWarningMaxValue ,
	--	          AlertMaxValue
	--	        )
	--	SELECT Exposure,ExposureDesc,
	--	Parameter,ParameterDesc
	--	,SUM(ISNULL(MarketValue,0)) MarketValue
	--	, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
	--	,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
	--	FROM @PositionForExp26
	--	WHERE Parameter = @CParameter
	--	GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	--END

	IF @CType = 26 and @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		0,''
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp26
		GROUP BY Exposure,ExposureDesc
	END

	IF @CType = 27
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp27
		WHERE Parameter = @CParameter
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 27 and @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp27
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END
	
	IF @CType = 28
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp28
		WHERE Parameter = @CParameter
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 28 and @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp28
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END
	
	IF @CType = 29
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp29
		WHERE Parameter = @CParameter
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	IF @CType = 29 and @CParameter = 0
	BEGIN
		INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )
		SELECT Exposure,ExposureDesc + ' | ALL PARAM ',
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp29
		GROUP BY Parameter,ParameterDesc,Exposure,ExposureDesc
	END


	IF @CType = 32
	BEGIN
		INSERT INTO @Exposure
				( 
					Exposure,
					ExposureID ,
					Parameter ,
					ParameterDesc ,
					MarketValue ,
					ExposurePercent ,
					MinExposurePercent ,
					WarningMinExposure ,
					AlertWarningMinExposure,
					AlertMinExposure ,
					MaxExposurePercent ,
					WarningMaxExposure ,
					AlertWarningMaxExposure ,
					AlertMaxExposure ,
					MinValue ,
					WarningMinValue ,
					AlertWarningMinValue ,
					AlertMinValue ,
					MaxValue ,
					WarningMaxValue ,
					AlertWarningMaxValue ,
					AlertMaxValue
				)

		SELECT Exposure,ExposureDesc,
		Parameter,ParameterDesc
		,SUM(ISNULL(MarketValue,0)) MarketValue
		, SUM(ISNULL(MarketValue,0)) / @TotalMarketValue * 100 ExposurePercent
		,@CMinExp,@CWarningMinExp,0,0,@CMaxExp,@CWarningMaxExp,0,0,@CMinVal,@CWarningMinVal,0,0,@CMaxVal,@CWarningMaxVal,0,0
		FROM @PositionForExp32
		WHERE Parameter = @CParameter
		GROUP BY FundPK,Parameter,ParameterDesc,Exposure,ExposureDesc
	END

	Fetch Next From A 
	INTO @CType,@CParameter,@CMinExp,@CMaxExp,@CWarningMinExp,@CWarningMaxExp,@CMinVal,@CMaxVal
	,@CWarningMinVal,@CWarningMaxVal 
End	
Close A
Deallocate A

END

IF EXISTS(
select * from FundExposure where Type = 15 and FundPK = @FundPK and status = 2
)

BEGIN
-- SYARIAH ONLY

	IF NOT EXISTS (
	select InstrumentPK from InstrumentSyariah where status in (1,2) and InstrumentPK = @InstrumentPK 
    and date = (select max(date) from InstrumentSyariah where status in (1,2))
    union all
    select InstrumentPK from Instrument A 
    left join Bank B on A.BankPK = B.BankPK and B.status in (1,2) 
    where BitSyariah = 1 and A.Status in (1,2) and InstrumentPK = @InstrumentPK
	)
	BEGIN

	INSERT INTO @Exposure
		        ( 
				  Exposure,
				  ExposureID ,
		          Parameter ,
				  ParameterDesc ,
		          MarketValue ,
		          ExposurePercent ,
		          MinExposurePercent ,
		          WarningMinExposure ,
				  AlertWarningMinExposure,
		          AlertMinExposure ,
		          MaxExposurePercent ,
		          WarningMaxExposure ,
				  AlertWarningMaxExposure ,
		          AlertMaxExposure ,
		          MinValue ,
		          WarningMinValue ,
				  AlertWarningMinValue ,
		          AlertMinValue ,
		          MaxValue ,
		          WarningMaxValue ,
				  AlertWarningMaxValue ,
		          AlertMaxValue
		        )

		SELECT 15,'SYARIAH ONLY',
		@InstrumentPK,ID
		,@TotalMarketValue MarketValue
		,100
		,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0 from Instrument where status in (1,2) and InstrumentPK  = @InstrumentPK
	END
END


update @Exposure 
SET AlertMinExposure = 1 WHERE ExposurePercent < MinExposurePercent AND MinExposurePercent > 0
update @Exposure 
SET AlertWarningMinExposure = 1 WHERE ExposurePercent < WarningMinExposure AND WarningMinExposure > 0
UPDATE @Exposure
SET AlertMaxExposure = 1 WHERE ExposurePercent > MaxExposurePercent AND MaxExposurePercent > 0
UPDATE @Exposure
SET AlertWarningMaxExposure = 1 WHERE ExposurePercent > WarningMaxExposure AND WarningMaxExposure > 0
UPDATE @Exposure
SET AlertMinValue = 1 WHERE MarketValue < MinValue AND MinValue > 0
UPDATE @Exposure
SET AlertWarningMinValue = 1 WHERE MarketValue < WarningMinValue AND WarningMinValue > 0
UPDATE @Exposure
SET AlertMaxValue = 1 WHERE MarketValue > MaxValue AND MaxValue > 0
UPDATE @Exposure
SET AlertWarningMaxValue = 1 WHERE MarketValue > WarningMaxValue AND WarningMaxValue > 0

--SELECT * FROM @PositionForExp18
--SELECT * FROM @Exposure




--1 =  warning MAX exposure
--2 = MAX exposure
--3 = warning MIN exposure
--4 = alert MIN exposure
--5 = warning MAX value
--6 = alert MAX value
--7 = warning MIN value
--8 = alert min value


----1. Sum PVR Deposito
--select sum(marketvalue) from fundposition where  status =2 and date = '7/9/19' and Category = 'Deposit Normal'--and bankpk =98

----2. Sum PVR Untuk cek Sector
--select sum(marketvalue) from fundposition where status =2 and date = '7/9/19' and instrumentpk in
--(select InstrumentPK from instrument where SectorPK in (select subsectorPK from subsector where sectorpk = 9)) and fundpk = 4 

----3. Sum PVR untuk Cek Index
--select Sum(marketvalue) from fundposition where fundpk = 4 and instrumentpk in
--(select InstrumentPK from InstrumentIndex where indexpk = 1) and status =2 and date = '7/9/19' 


-- Type = Type Exposure yang ijo2 atas, parameter = grouptype di instrumenttype
--select * from instrumenttype
--select * from mastervalue order by mastervaluepk desc where id ='InstrumentGroupType'
--select * from fund where status =2 --4
--SELECT * FROM dbo.MasterValue WHERE id = 'ExposureType'
--select * from FundExposure where fundpk = 1 and FundExposurePK =160
--update  FundExposure set parameter = 0 where FundPK  =4 and status =2 and fundexposurePK = 159

IF(@TrxType in (1,3))
BEGIN
    INSERT INTO ZTEMP_FUNDEXPOSURE
	select  Exposure,ExposureID,Parameter,ParameterDesc,Case when  AlertWarningMaxExposure = 1 and AlertMaxExposure = 0 then 1 else case when AlertMaxExposure = 1  then 2
	else case when  AlertWarningMaxValue = 1 and AlertMaxValue = 0 then 5 else case when AlertMaxValue = 1  then 6 
	end end end end     AlertExposure,
	ExposurePercent,WarningMaxExposure,MaxExposurePercent,WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue from @Exposure
	where 
	Case when  AlertWarningMaxExposure = 1 and AlertMaxExposure = 0 then 1 else case when AlertMaxExposure = 1  then 2
	else case when  AlertWarningMaxValue = 1 and AlertMaxValue = 0 then 5 else case when AlertMaxValue = 1  then 6 
	end end end end   > 0 
	" + _parameter + @"
	order by AlertExposure desc

	if(@ClientCode = 20)
	BEGIN
		select  
		Case when Exposure = 10 and (AlertWarningMaxExposure = 1 or AlertMaxExposure = 1) then 10
				when  AlertWarningMaxExposure = 1 and AlertMaxExposure = 0 then 1 
					when AlertMaxExposure = 1  then 2
						when  AlertWarningMaxValue = 1 and AlertMaxValue = 0 then 5 
							when AlertMaxValue = 1  then 6 
		end     AlertExposure
		from @Exposure
		where 
		Case when  AlertWarningMaxExposure = 1 and AlertMaxExposure = 0 then 1 else case when AlertMaxExposure = 1  then 2
		else case when  AlertWarningMaxValue = 1 and AlertMaxValue = 0 then 5 else case when AlertMaxValue = 1  then 6 
		end end end end   > 0 
		" + _parameter + @"
		order by AlertExposure desc
	END
	ELSE
	BEGIN
		select  Case when  AlertWarningMaxExposure = 1 and AlertMaxExposure = 0 then 1 else case when AlertMaxExposure = 1  then 2
		else case when  AlertWarningMaxValue = 1 and AlertMaxValue = 0 then 5 else case when AlertMaxValue = 1  then 6 
		end end end end     AlertExposure
		from @Exposure
		where 
		Case when  AlertWarningMaxExposure = 1 and AlertMaxExposure = 0 then 1 else case when AlertMaxExposure = 1  then 2
		else case when  AlertWarningMaxValue = 1 and AlertMaxValue = 0 then 5 else case when AlertMaxValue = 1  then 6 
		end end end end   > 0 
		" + _parameter + @"
		order by AlertExposure desc
	END

END
ELSE
BEGIN
    INSERT INTO ZTEMP_FUNDEXPOSURE  
    select  Exposure,ExposureID,Parameter,ParameterDesc,
	Case when  AlertWarningMinExposure = 1 and AlertMinExposure = 0  then 3 else case when AlertMinExposure = 1  then 4 
	else case when  AlertWarningMinValue = 1 and AlertMinValue = 0 then 7 else case when AlertMinValue = 1  then 8 
	end end end end     AlertExposure,
	ExposurePercent,WarningMaxExposure,MaxExposurePercent,WarningMinExposure,MinExposurePercent,MarketValue,WarningMaxValue,MaxValue,WarningMinValue,MinValue from @Exposure
	where 
	Case when  AlertWarningMinExposure = 1 and AlertMinExposure = 0   then 3 else case when AlertMinExposure = 1  then 4 
	else case when AlertWarningMinValue = 1 and AlertMinValue = 0  then 7 else case when AlertMinValue = 1  then 8 
	end end end end     > 0  
	" + _parameter + @"
	order by AlertExposure desc


	if(@ClientCode = 20)
	BEGIN
		select 
		Case when Exposure = 10 and  (AlertWarningMinExposure = 1 or AlertMinExposure = 1) then 10
				when  AlertWarningMinExposure = 1 and AlertMinExposure = 0 then 3 
					when AlertMinExposure = 1  then 4 
						when  AlertWarningMinValue = 1 and AlertMinValue = 0 then 7 
							when AlertMinValue = 1  then 8 
		end AlertExposure
		from @Exposure
		where 
		Case when  AlertWarningMinExposure = 1 and AlertMinExposure = 0 then 3 else case when AlertMinExposure = 1  then 4 
		else case when  AlertWarningMinValue = 1 and AlertMinValue = 0 then 7 else case when AlertMinValue = 1  then 8 
		end end end end     > 0  
		" + _parameter + @"
		order by AlertExposure desc
	END
	ELSE
	BEGIN
		select  
		Case when  AlertWarningMinExposure = 1 and AlertMinExposure = 0 then 3 else case when AlertMinExposure = 1  then 4 
		else case when  AlertWarningMinValue = 1 and AlertMinValue = 0 then 7 else case when AlertMinValue = 1  then 8 
		end end end end     AlertExposure
		from @Exposure
		where 
		Case when  AlertWarningMinExposure = 1 and AlertMinExposure = 0 then 3 else case when AlertMinExposure = 1  then 4 
		else case when  AlertWarningMinValue = 1 and AlertMinValue = 0 then 7 else case when AlertMinValue = 1  then 8 
		end end end end     > 0  
		" + _parameter + @"
		order by AlertExposure desc
	END

    
END

--select * from ZTEMP_FUNDEXPOSURE
           
                           ";

						cmd.Parameters.AddWithValue("@date", _valuedate);
						cmd.Parameters.AddWithValue("@instrumentPK", _instrumentPK);
						cmd.Parameters.AddWithValue("@fundPK", _fundPK);
						cmd.Parameters.AddWithValue("@Amount", _amount);
						cmd.Parameters.AddWithValue("@TrxType", _trxType);
						cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								dr.Read();
								return new FundExposure()
								{

									AlertExposure = dr["AlertExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AlertExposure"]),

								};
							}
							else
							{
								return new FundExposure()
								{
									AlertExposure = 0,

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

		public List<ValidateFundExposure> Get_DataInformationFundExposure()
		{

			try
			{
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					List<ValidateFundExposure> L_FundExposure = new List<ValidateFundExposure>();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{

						cmd.CommandText = @"  
                            Select * from ZTEMP_FUNDEXPOSURE ";

						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								while (dr.Read())
								{
									L_FundExposure.Add(setValidateFundExposure(dr));
								}
							}
							return L_FundExposure;
						}
					}
				}
			}
			catch (Exception err)
			{
				throw err;
			}

		}

		private ValidateFundExposure setValidateFundExposure(SqlDataReader dr)
		{
			ValidateFundExposure M_FundExposure = new ValidateFundExposure();

			M_FundExposure.Exposure = dr["Exposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Exposure"]);
			M_FundExposure.ExposureID = dr["ExposureID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExposureID"]);
			M_FundExposure.Parameter = dr["Parameter"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Parameter"]);
			M_FundExposure.ParameterDesc = dr["ParameterDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ParameterDesc"]);
			M_FundExposure.AlertExposure = dr["AlertExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AlertExposure"]);
			M_FundExposure.ExposurePercent = dr["ExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["ExposurePercent"]);
			M_FundExposure.WarningMaxExposure = dr["WarningMaxExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMaxExposure"]);
			M_FundExposure.MaxExposurePercent = dr["MaxExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MaxExposurePercent"]);
			M_FundExposure.WarningMinExposure = dr["WarningMinExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMinExposure"]);
			M_FundExposure.MinExposurePercent = dr["MinExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinExposurePercent"]);
			M_FundExposure.MarketValue = dr["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MarketValue"]);
			M_FundExposure.WarningMaxValue = dr["WarningMaxValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMaxValue"]);
			M_FundExposure.MaxValue = dr["MaxValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MaxValue"]);
			M_FundExposure.WarningMinValue = dr["WarningMinValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMinValue"]);
			M_FundExposure.MinValue = dr["MinValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinValue"]);
			return M_FundExposure;
		}

		public bool Get_ValidateDataInformationFundExposure()
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        IF EXISTS(select * from ZTEMP_FUNDEXPOSURE where AlertExposure in (2,4,6,8))
                        BEGIN
                            select 1 Result
                        END
                        ELSE
                        BEGIN
                             select 0 Result
                        END ";

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

        public List<ValidateFundExposure> Get_DataInformationFundExposureFromOMSEquity()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ValidateFundExposure> L_FundExposure = new List<ValidateFundExposure>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                            Select * from ZTEMP_FUNDEXPOSURE_IMPORT ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundExposure.Add(setValidateFundExposure(dr));
                                }
                            }
                            return L_FundExposure;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

		public List<ValidateFundExposure> Get_DataInformationFundExposureForCustom20(int _fundPK)
		{

			try
			{
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					List<ValidateFundExposure> L_FundExposure = new List<ValidateFundExposure>();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{

						cmd.CommandText = @"  
                          Select B.ID FundID,* from ZTEMP_FUNDEXPOSURE A
						  left join Fund B on B.status in (1,2) and B.FundPK = @FundPK ";

						cmd.Parameters.AddWithValue("@FundPK", _fundPK);
						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								while (dr.Read())
								{
									L_FundExposure.Add(setValidateFundExposureCustom20(dr));
								}
							}
							return L_FundExposure;
						}
					}
				}
			}
			catch (Exception err)
			{
				throw err;
			}

		}

		private ValidateFundExposure setValidateFundExposureCustom20(SqlDataReader dr)
		{
			ValidateFundExposure M_FundExposure = new ValidateFundExposure();

			M_FundExposure.Exposure = dr["Exposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Exposure"]);
			M_FundExposure.ExposureID = dr["ExposureID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExposureID"]);
			M_FundExposure.Parameter = dr["Parameter"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Parameter"]);
			M_FundExposure.ParameterDesc = dr["ParameterDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ParameterDesc"]);
			M_FundExposure.AlertExposure = dr["AlertExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AlertExposure"]);
			M_FundExposure.ExposurePercent = dr["ExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["ExposurePercent"]);
			M_FundExposure.WarningMaxExposure = dr["WarningMaxExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMaxExposure"]);
			M_FundExposure.MaxExposurePercent = dr["MaxExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MaxExposurePercent"]);
			M_FundExposure.WarningMinExposure = dr["WarningMinExposure"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMinExposure"]);
			M_FundExposure.MinExposurePercent = dr["MinExposurePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinExposurePercent"]);
			M_FundExposure.MarketValue = dr["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MarketValue"]);
			M_FundExposure.WarningMaxValue = dr["WarningMaxValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMaxValue"]);
			M_FundExposure.MaxValue = dr["MaxValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MaxValue"]);
			M_FundExposure.WarningMinValue = dr["WarningMinValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["WarningMinValue"]);
			M_FundExposure.MinValue = dr["MinValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinValue"]);
			M_FundExposure.FundID = dr["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FundID"]);
			return M_FundExposure;
		}

		public List<ValidateFundExposure> Get_DataInformationFundExposureFromOMSTimeDeposit()
		{

			try
			{
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();
					List<ValidateFundExposure> L_FundExposure = new List<ValidateFundExposure>();
					using (SqlCommand cmd = DbCon.CreateCommand())
					{

						cmd.CommandText = @"  
                            Select * from ZTEMP_FUNDEXPOSURE_IMPORT_DEPOSITO ";

						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								while (dr.Read())
								{
									L_FundExposure.Add(setValidateFundExposure(dr));
								}
							}
							return L_FundExposure;
						}
					}
				}
			}
			catch (Exception err)
			{
				throw err;
			}
		}

		public string CheckDepositoMaturityUnderOneYear(int _instrumentPK, DateTime _date)
		{
			try
			{
				using (SqlConnection DbCon = new SqlConnection(Tools.conString))
				{
					DbCon.Open();

					using (SqlCommand cmd = DbCon.CreateCommand())
					{


						cmd.CommandText = @"

declare @MaturityDate datetime

select @MaturityDate = MaturityDate from Instrument where InstrumentPK  = @InstrumentPK and status in (1,2) 

if (@MaturityDate >= DATEADD(year, -1, @Date))
BEGIN
	select 'Yes' Result
END
ELSE
BEGIN
	select 'No' Result
END
							
";
						cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
						cmd.Parameters.AddWithValue("@Date", _date);
						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (!dr.Read())
							{
								return "";
							}
							else
							{
								return Convert.ToString(dr["Result"]);

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