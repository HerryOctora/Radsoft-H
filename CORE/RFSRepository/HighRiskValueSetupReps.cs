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
    public class HighRiskValueSetupReps
    {
        Host _host = new Host();

        //1
      
        private HighRiskValueSetup setHighRiskValueSetup(SqlDataReader dr)
        {
            HighRiskValueSetup M_HighRiskValueSetup = new HighRiskValueSetup();
            M_HighRiskValueSetup.MasterValuePK = Convert.ToInt32(dr["MasterValuePK"]);
            M_HighRiskValueSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_HighRiskValueSetup.Status = Convert.ToInt32(dr["Status"]);
            M_HighRiskValueSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_HighRiskValueSetup.Notes = Convert.ToString(dr["Notes"]);
            M_HighRiskValueSetup.ID = dr["ID"].ToString();
            M_HighRiskValueSetup.Type = dr["Type"].ToString();
            M_HighRiskValueSetup.Code = dr["Code"].ToString();
            M_HighRiskValueSetup.DescOne = dr["DescOne"].ToString();
            M_HighRiskValueSetup.DescTwo = dr["DescTwo"].ToString();
            M_HighRiskValueSetup.Priority = Convert.ToInt32(dr["Priority"]);
            M_HighRiskValueSetup.IsHighRisk = Convert.ToBoolean(dr["IsHighRisk"]);
            M_HighRiskValueSetup.RiskCDD = Convert.ToInt32(dr["RiskCDD"]);
            M_HighRiskValueSetup.RiskCDDDesc = Convert.ToString(dr["RiskCDDDesc"]);
            M_HighRiskValueSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_HighRiskValueSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_HighRiskValueSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_HighRiskValueSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_HighRiskValueSetup.EntryTime = dr["EntryTime"].ToString();
            M_HighRiskValueSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_HighRiskValueSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_HighRiskValueSetup.VoidTime = dr["VoidTime"].ToString();
            M_HighRiskValueSetup.DBUserID = dr["DBUserID"].ToString();
            M_HighRiskValueSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_HighRiskValueSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_HighRiskValueSetup.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_HighRiskValueSetup;
        }

        public List<HighRiskValueSetup> HighRiskValueSetup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<HighRiskValueSetup> L_HighRiskValueSetup = new List<HighRiskValueSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne RiskCDDDesc,* from MasterValue A
                            left join Mastervalue B on A.RiskCDD = B.Code and B.ID = 'RiskCDD' and B.Status = 2
                            where A.id in('HrPEP','HrBusiness','SDICountry','SDIProvince','Occupation','HRBusiness','FundType') and A.status = @status order by A.MasterValuePK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne RiskCDDDesc,* from MasterValue A
                            left join Mastervalue B on A.RiskCDD = B.Code and B.ID = 'RiskCDD' and B.Status = 2
                            where A.id in('HrPEP','HrBusiness','SDICountry','SDIProvince','Occupation','HRBusiness','FundType')  order by A.MasterValuePK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_HighRiskValueSetup.Add(setHighRiskValueSetup(dr));
                                }
                            }
                            return L_HighRiskValueSetup;
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