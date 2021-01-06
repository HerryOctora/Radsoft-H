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
using System.Text;
using System.Threading;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class ManageInvestmentReps
    {
        Host _host = new Host();


        //2
        private ManageInvestment setManageInvestment(SqlDataReader dr)
        {
            ManageInvestment M_ManageInvestment = new ManageInvestment();
            M_ManageInvestment.PK = Convert.ToInt32(dr["PK"]);
            M_ManageInvestment.Date = Convert.ToDateTime(dr["Date"]);
            M_ManageInvestment.SettledDate = Convert.ToDateTime(dr["SettledDate"]);
            M_ManageInvestment.Selected = Convert.ToBoolean(dr["Selected"]);
            M_ManageInvestment.Type = Convert.ToInt32(dr["Type"]);
            M_ManageInvestment.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_ManageInvestment.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_ManageInvestment.FundID = Convert.ToString(dr["FundID"]);
            M_ManageInvestment.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_ManageInvestment.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_ManageInvestment.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
            M_ManageInvestment.InstrumentTypeID = Convert.ToString(dr["InstrumentTypeID"]);
            M_ManageInvestment.TrxTypeID = Convert.ToString(dr["TrxTypeID"]);
            M_ManageInvestment.DoneVolume = Convert.ToDecimal(dr["DoneVolume"]);
            M_ManageInvestment.DonePrice = Convert.ToDecimal(dr["DonePrice"]);
            M_ManageInvestment.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
            return M_ManageInvestment;
        }


        public List<ManageInvestment> Get_DataApplyParameterInvestment()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ManageInvestment> L_ManageInvestment = new List<ManageInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"select PK,Selected,Date,SettledDate,A.Type,
                        case when A.Type = 1 then 'OMS Investment' else case when A.Type = 2 then 'Dealing' else 'Settlement' end end TypeDesc,TrxTypeID,
                        A.FundPK, B.ID FundID,A.InstrumentPK,C.ID InstrumentID,A.InstrumentTypePK,D.ID InstrumentTypeID,DoneVolume,DonePrice,TotalAmount from ZMANAGE_INVESTMENT A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                        left join InstrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status in (1,2)
                        ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ManageInvestment.Add(setManageInvestment(dr));
                                }
                            }
                            return L_ManageInvestment;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<ManageInvestment> Get_SysNo(ManageInvestment _manageInvestment)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ManageInvestment> L_ManageInvestment = new List<ManageInvestment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";

                        if (!_host.findString(_manageInvestment.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_manageInvestment.FundFrom))
                        {
                            _paramFund = "And FundPK in ( " + _manageInvestment.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        
                        cmd.CommandText = @"        
                     
                        truncate table ZMANAGE_INVESTMENT
                        IF(@Type = 1)
                        BEGIN
	                        Insert into ZMANAGE_INVESTMENT(PK,Selected,Date,SettledDate,Type,FundPK,InstrumentPK,InstrumentTypePK,TrxTypeID,DoneVolume,DonePrice,TotalAmount)
	                        select InvestmentPK,0,ValueDate,SettlementDate,1,FundPK,InstrumentPK,InstrumentTypePK,TrxTypeID,DoneVolume,DonePrice,TotalAmount from Investment
	                        where ValueDate between @DateFrom and @DateTo  and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 " + _paramFund + @"
                        END
                        ELSE IF (@Type = 2)
                        BEGIN
	                        Insert into ZMANAGE_INVESTMENT(PK,Selected,Date,SettledDate,Type,FundPK,InstrumentPK,InstrumentTypePK,TrxTypeID,DoneVolume,DonePrice,TotalAmount)
	                        select DealingPK,0,ValueDate,SettlementDate,2,FundPK,InstrumentPK,InstrumentTypePK,TrxTypeID,DoneVolume,DonePrice,TotalAmount from Investment
	                        where ValueDate between @DateFrom and @DateTo  and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 " + _paramFund + @"
                        END
                        ELSE 
                        BEGIN
	                        Insert into ZMANAGE_INVESTMENT(PK,Selected,Date,SettledDate,Type,FundPK,InstrumentPK,InstrumentTypePK,TrxTypeID,DoneVolume,DonePrice,TotalAmount)
	                        select SettlementPK,0,ValueDate,SettlementDate,3,FundPK,InstrumentPK,InstrumentTypePK,TrxTypeID,DoneVolume,DonePrice,TotalAmount from Investment
	                        where ValueDate between @DateFrom and @DateTo  and StatusInvestment <> 3 and StatusDealing <> 3 and StatusSettlement <> 3 " + _paramFund + @"
                        END ";

                        cmd.Parameters.AddWithValue("@DateFrom", _manageInvestment.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _manageInvestment.DateTo);
                        cmd.Parameters.AddWithValue("@Type", _manageInvestment.Type);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ManageInvestment.Add(setManageInvestment(dr));
                                }
                            }
                            return L_ManageInvestment;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public void SelectDeselectData(bool _toggle, int _PK, int _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "Update ZMANAGE_INVESTMENT set Selected = @Toggle where PK  = @PK and Type = @Type ";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@Type", _type);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public void SelectDeselectAllData(bool _toggle)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ZMANAGE_INVESTMENT set Selected = @Toggle ";
                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public void Update_SettledDate(DateTime _settledDate)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Declare @PK int
                        Declare @Type int

                        DECLARE A CURSOR FOR 
                        select PK,Type from ZMANAGE_INVESTMENT where selected = 1
        	
                        Open A
                        Fetch Next From A
                        Into @PK,@Type
        
                        While @@FETCH_STATUS = 0
                        BEGIN
	                        IF (@Type = 1)
	                        BEGIN
		                        update Investment set SettlementDate = @SettledDate where InvestmentPK = @PK
	                        END
	                        ELSE IF (@Type = 2)
	                        BEGIN
		                        update Investment set SettlementDate = @SettledDate where DealingPK = @PK
	                        END
	                        ELSE
	                        BEGIN
		                        update Investment set SettlementDate = @SettledDate where SettlementPK = @PK
	                        END

                        Fetch next From A Into @PK,@Type
                        END
                        Close A
                        Deallocate A 
                        ";

                        cmd.Parameters.AddWithValue("@SettledDate", _settledDate);
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
