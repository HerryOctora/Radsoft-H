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
    public class AgentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Agent] " +
                                "([AgentPK],[HistoryPK],[Status],[ID],[Name],[Type],[AgentFee],[NoRek],[Phone],[Fax],[Email],[Address],[TaxID],[BankInformation],[BeneficiaryName],[Description],[MFeeMethod],[SharingFeeCalculation],[Groups],[Levels],[ParentPK]," +
                                "[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[ParentPK6],[ParentPK7],[ParentPK8],[ParentPK9],[DepartmentPK],[BitisAgentBank],[BitIsAgentCSR],[CompanyPositionSchemePK],[NPWPNo],[BitPPH23],[BitPPH21],[JoinDate],[BitPPN],[PPH23Percent],[WAPERDNo],[WAPERDExpiredDate],[AAJINo],[AAJIExpiredDate],";
        string _paramaterCommand = "@ID,@Name,@Type,@AgentFee,@NoRek,@Phone,@Fax,@Email,@Address,@TaxID,@BankInformation,@BeneficiaryName,@Description,@MFeeMethod,@SharingFeeCalculation,@Groups,@Levels,@ParentPK,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@ParentPK6," +
                                "@ParentPK7,@ParentPK8,@ParentPK9,@DepartmentPK,@BitisAgentBank,@BitIsAgentCSR,@CompanyPositionSchemePK,@NPWPNo,@BitPPH23,@BitPPH21,@JoinDate,@BitPPN,@PPH23Percent,@WAPERDNo,@WAPERDExpiredDate,@AAJINo,@AAJIExpiredDate,";
        //2
        private Agent setAgent(SqlDataReader dr)
        {
            Agent M_Agent = new Agent();
            M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_Agent.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Agent.Status = Convert.ToInt32(dr["Status"]);
            M_Agent.StatusDesc = (dr["StatusDesc"]).ToString();
            M_Agent.Notes = (dr["Notes"]).ToString();
            M_Agent.ID = (dr["ID"]).ToString();
            M_Agent.Name = (dr["Name"]).ToString();
            M_Agent.Type = Convert.ToInt32(dr["Type"]);
            M_Agent.TypeDesc = (dr["TypeDesc"]).ToString();
            M_Agent.AgentFee = Convert.ToDecimal(dr["AgentFee"]);
            M_Agent.NoRek = (dr["NoRek"]).ToString();
            M_Agent.Phone = (dr["Phone"]).ToString();
            M_Agent.Fax = (dr["Fax"]).ToString();
            M_Agent.Email = (dr["Email"]).ToString();
            M_Agent.Address = (dr["Address"]).ToString();
            M_Agent.TaxID = (dr["TaxID"]).ToString();
            M_Agent.BankInformation = dr["BankInformation"].ToString();
            M_Agent.BeneficiaryName = (dr["BeneficiaryName"]).ToString();
            M_Agent.Description = (dr["Description"]).ToString();
            M_Agent.JoinDate = dr["JoinDate"].Equals(DBNull.Value) == true ? "" : dr["JoinDate"].ToString();
            M_Agent.Groups = Convert.ToBoolean(dr["Groups"]);
            M_Agent.Levels = Convert.ToInt32(dr["Levels"]);
            M_Agent.ParentPK = Convert.ToInt32(dr["ParentPK"]);
            M_Agent.ParentID = dr["ParentID"].ToString();
            M_Agent.ParentName = dr["ParentName"].ToString();
            M_Agent.ParentPK1 = Convert.ToInt32(dr["ParentPK1"]);
            M_Agent.ParentPK2 = Convert.ToInt32(dr["ParentPK2"]);
            M_Agent.ParentPK3 = Convert.ToInt32(dr["ParentPK3"]);
            M_Agent.ParentPK4 = Convert.ToInt32(dr["ParentPK4"]);
            M_Agent.ParentPK5 = Convert.ToInt32(dr["ParentPK5"]);
            M_Agent.ParentPK6 = Convert.ToInt32(dr["ParentPK6"]);
            M_Agent.ParentPK7 = Convert.ToInt32(dr["ParentPK7"]);
            M_Agent.ParentPK8 = Convert.ToInt32(dr["ParentPK8"]);
            M_Agent.ParentPK9 = Convert.ToInt32(dr["ParentPK9"]);
            M_Agent.Depth = Convert.ToInt32(dr["Depth"]);
            M_Agent.DepartmentPK = dr["DepartmentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["DepartmentPK"]);
            M_Agent.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_Agent.BitisAgentBank = dr["BitisAgentBank"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitisAgentBank"]); //BitIsAgentCSR
            M_Agent.BitIsAgentCSR = dr["BitIsAgentCSR"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitIsAgentCSR"]);
            M_Agent.CompanyPositionSchemePK = dr["CompanyPositionSchemePK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CompanyPositionSchemePK"]);
            M_Agent.CompanyPositionSchemeID = Convert.ToString(dr["CompanyPositionSchemeID"]);
            M_Agent.NPWPNo = (dr["NPWPNo"]).ToString();
            M_Agent.BitPPH23 = dr["BitPPH23"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitPPH23"]);
            M_Agent.BitPPH21 = dr["BitPPH21"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitPPH21"]);

            M_Agent.MFeeMethod = Convert.ToInt32(dr["MFeeMethod"]);
            M_Agent.MFeeMethodDesc = dr["MFeeMethodDesc"].Equals(DBNull.Value) == true ? "" : dr["MFeeMethodDesc"].ToString();
            M_Agent.SharingFeeCalculation = dr["SharingFeeCalculation"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SharingFeeCalculation"]);
            M_Agent.SharingFeeCalculationDesc = dr["SharingFeeCalculationDesc"].Equals(DBNull.Value) == true ? "" : dr["SharingFeeCalculationDesc"].ToString();

            M_Agent.BitPPN = dr["BitPPN"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitPPN"]);
            M_Agent.PPH23Percent = dr["PPH23Percent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["PPH23Percent"]);

            M_Agent.WAPERDNo = dr["WAPERDNo"].Equals(DBNull.Value) == true ? "" : dr["WAPERDNo"].ToString();
            M_Agent.WAPERDExpiredDate = dr["WAPERDExpiredDate"].Equals(DBNull.Value) == true ? "" : dr["WAPERDExpiredDate"].ToString();
            M_Agent.AAJINo = dr["AAJINo"].Equals(DBNull.Value) == true ? "" : dr["AAJINo"].ToString();
            M_Agent.AAJIExpiredDate = dr["AAJIExpiredDate"].Equals(DBNull.Value) == true ? "" : dr["AAJIExpiredDate"].ToString();

            M_Agent.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Agent.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Agent.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Agent.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Agent.EntryTime = dr["EntryTime"].ToString();
            M_Agent.UpdateTime = dr["UpdateTime"].ToString();
            M_Agent.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Agent.VoidTime = dr["VoidTime"].ToString();
            M_Agent.DBUserID = dr["DBUserID"].ToString();
            M_Agent.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Agent.LastUpdate = dr["LastUpdate"].ToString();
            M_Agent.LastUpdateDB = (dr["LastUpdateDB"]).ToString();
            return M_Agent;
        }

        public List<Agent> Agent_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<Agent> L_Agent = new List<Agent>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,ZA.ID ParentID,ZA.Name ParentName,D.Name DepartmentID,F.ID + '-' + G.ID CompanyPositionSchemeID,
                                                case when A.MFeeMethod = 1 then 'Mark to Market' else case when A.MFeeMethod = 2 then 'NAV 1000' else '' end end MFeeMethodDesc,MV7.DescOne SharingFeeCalculationDesc,
                                                A.* from Agent A 
                                                left join MasterValue MV on A.Type = MV.Code and MV.Status=2 and MV.ID ='SDIClientType' 
												left join MasterValue MV7 on A.SharingFeeCalculation = MV7.Code and MV7.ID ='SharingFeeCalculation' and MV7.status = 2
                                                Left join Agent ZA on A.ParentPK = ZA.AgentPK and ZA.status in (1,2) 
                                                Left join Department D on A.DepartmentPK = D.DepartmentPK and D.status in (1,2) 
                                                left join CompanyPositionScheme E on A.CompanyPositionSchemePK = E.CompanyPositionSchemePK and E.status in (2)
                                                left join CategoryScheme F on E.CategorySchemePK = F.CategorySchemePK and F.status in (2)
                                                left join CompanyPosition G on E.CompanyPositionPK = G.CompanyPositionPK and G.status in (2)
                                                where A.status = @status order by A.AgentPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,ZA.ID ParentID,ZA.Name ParentName,D.Name DepartmentID,F.ID + '-' + G.ID CompanyPositionSchemeID,
                                                case when A.MFeeMethod = 1 then 'Mark to Market' else case when A.MFeeMethod = 2 then 'NAV 1000'  else '' end end MFeeMethodDesc,MV7.DescOne SharingFeeCalculationDesc,
                                                A.* from Agent A 
                                                left join MasterValue MV on A.Type = MV.Code and MV.Status=2 and MV.ID ='SDIClientType' 
                                                left join MasterValue MV7 on A.SharingFeeCalculation = MV7.Code and MV7.ID ='SharingFeeCalculation' and MV7.status = 2
                                                Left join Agent ZA on A.ParentPK = ZA.AgentPK and ZA.status in (1,2) 
                                                Left join Department D on A.DepartmentPK = D.DepartmentPK and D.status in (1,2) 
                                                left join CompanyPositionScheme E on A.CompanyPositionSchemePK = E.CompanyPositionSchemePK and E.status in (2)
                                                left join CategoryScheme F on E.CategorySchemePK = F.CategorySchemePK and F.status in (2)
                                                left join CompanyPosition G on E.CompanyPositionPK = G.CompanyPositionPK and G.status in (2)
                                                order by A.AgentPK  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Agent.Add(setAgent(dr));
                                }
                            }
                            return L_Agent;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Agent_Add(Agent _agent, bool _havePrivillege)
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
                                 "Select isnull(max(AgentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from Agent";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _agent.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(AgentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from Agent";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _agent.ID);
                        cmd.Parameters.AddWithValue("@Name", _agent.Name);
                        cmd.Parameters.AddWithValue("@Type", _agent.Type);
                        cmd.Parameters.AddWithValue("@AgentFee", _agent.AgentFee);
                        cmd.Parameters.AddWithValue("@NoRek", _agent.NoRek);
                        cmd.Parameters.AddWithValue("@Phone", _agent.Phone);
                        cmd.Parameters.AddWithValue("@Fax", _agent.Fax);
                        cmd.Parameters.AddWithValue("@Email", _agent.Email);
                        cmd.Parameters.AddWithValue("@Address", _agent.Address);
                        cmd.Parameters.AddWithValue("@TaxID", _agent.TaxID);
                        cmd.Parameters.AddWithValue("@BankInformation", _agent.BankInformation);
                        cmd.Parameters.AddWithValue("@BeneficiaryName", _agent.BeneficiaryName);
                        cmd.Parameters.AddWithValue("@Description", _agent.Description);
                        cmd.Parameters.AddWithValue("@JoinDate", _agent.JoinDate);
                        cmd.Parameters.AddWithValue("@Groups", _agent.Groups);
                        cmd.Parameters.AddWithValue("@Levels", _agent.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _agent.ParentPK);
                        cmd.Parameters.AddWithValue("@ParentPK1", _agent.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _agent.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _agent.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _agent.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _agent.ParentPK5);
                        cmd.Parameters.AddWithValue("@ParentPK6", _agent.ParentPK6);
                        cmd.Parameters.AddWithValue("@ParentPK7", _agent.ParentPK7);
                        cmd.Parameters.AddWithValue("@ParentPK8", _agent.ParentPK8);
                        cmd.Parameters.AddWithValue("@ParentPK9", _agent.ParentPK9);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _agent.DepartmentPK);
                        cmd.Parameters.AddWithValue("@BitisAgentBank", _agent.BitisAgentBank);
                        cmd.Parameters.AddWithValue("@BitIsAgentCSR", _agent.BitIsAgentCSR);
                        cmd.Parameters.AddWithValue("@CompanyPositionSchemePK", _agent.CompanyPositionSchemePK);
                        cmd.Parameters.AddWithValue("@NPWPNo", _agent.NPWPNo);
                        cmd.Parameters.AddWithValue("@BitPPH23", _agent.BitPPH23);
                        cmd.Parameters.AddWithValue("@BitPPH21", _agent.BitPPH21);
                        cmd.Parameters.AddWithValue("@BitPPN", _agent.BitPPN);
                        cmd.Parameters.AddWithValue("@PPH23Percent", _agent.PPH23Percent);
                        cmd.Parameters.AddWithValue("@MFeeMethod", _agent.MFeeMethod);
                        cmd.Parameters.AddWithValue("@SharingFeeCalculation", _agent.SharingFeeCalculation);

                        cmd.Parameters.AddWithValue("@WAPERDNo", _agent.WAPERDNo);
                        cmd.Parameters.AddWithValue("@WAPERDExpiredDate", _agent.WAPERDExpiredDate);
                        cmd.Parameters.AddWithValue("@AAJINo", _agent.AAJINo);
                        cmd.Parameters.AddWithValue("@AAJIExpiredDate", _agent.AAJIExpiredDate);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _agent.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Agent");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Agent_Update(Agent _agent, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_agent.AgentPK, _agent.HistoryPK, "agent");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Agent set status=2, Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,AgentFee=@AgentFee,NoRek=@NoRek,Phone=@Phone,Fax=@Fax,MFeeMethod=@MFeeMethod,SharingFeeCalculation=@SharingFeeCalculation, " +
                                "Email=@Email,Address=@Address,TaxID=@TaxID,BankInformation=@BankInformation,BeneficiaryName=@BeneficiaryName,Description=@Description,JoinDate=@JoinDate," +
                                "Groups=@Groups,Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,DepartmentPK=@DepartmentPK,BitisAgentBank=@BitisAgentBank,BitIsAgentCSR=@BitIsAgentCSR,CompanyPositionSchemePK=@CompanyPositionSchemePK,NPWPNo=@NPWPNo,BitPPH23=@BitPPH23,BitPPH21=@BitPPH21,BitPPN=@BitPPN,PPH23Percent=@PPH23Percent," +
                                "WAPERDNo=@WAPERDNo,WAPERDExpiredDate=@WAPERDExpiredDate,AAJINo=@AAJINo,AAJIExpiredDate=@AAJIExpiredDate," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where AgentPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _agent.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                            cmd.Parameters.AddWithValue("@Notes", _agent.Notes);
                            cmd.Parameters.AddWithValue("@ID", _agent.ID);
                            cmd.Parameters.AddWithValue("@Name", _agent.Name);
                            cmd.Parameters.AddWithValue("@Type", _agent.Type);
                            cmd.Parameters.AddWithValue("@AgentFee", _agent.AgentFee);
                            cmd.Parameters.AddWithValue("@NoRek", _agent.NoRek);
                            cmd.Parameters.AddWithValue("@Phone", _agent.Phone);
                            cmd.Parameters.AddWithValue("@Fax", _agent.Fax);
                            cmd.Parameters.AddWithValue("@Email", _agent.Email);
                            cmd.Parameters.AddWithValue("@Address", _agent.Address);
                            cmd.Parameters.AddWithValue("@TaxID", _agent.TaxID);
                            cmd.Parameters.AddWithValue("@BankInformation", _agent.BankInformation);
                            cmd.Parameters.AddWithValue("@BeneficiaryName", _agent.BeneficiaryName);
                            cmd.Parameters.AddWithValue("@Description", _agent.Description);
                            cmd.Parameters.AddWithValue("@JoinDate", _agent.JoinDate);
                            cmd.Parameters.AddWithValue("@Groups", _agent.Groups);
                            cmd.Parameters.AddWithValue("@Levels", _agent.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _agent.ParentPK);
                            cmd.Parameters.AddWithValue("@ParentPK1", _agent.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _agent.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _agent.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _agent.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _agent.ParentPK5);
                            cmd.Parameters.AddWithValue("@ParentPK6", _agent.ParentPK6);
                            cmd.Parameters.AddWithValue("@ParentPK7", _agent.ParentPK7);
                            cmd.Parameters.AddWithValue("@ParentPK8", _agent.ParentPK8);
                            cmd.Parameters.AddWithValue("@ParentPK9", _agent.ParentPK9);
                            cmd.Parameters.AddWithValue("@DepartmentPK", _agent.DepartmentPK);
                            cmd.Parameters.AddWithValue("@BitisAgentBank", _agent.BitisAgentBank);
                            cmd.Parameters.AddWithValue("@BitIsAgentCSR", _agent.BitIsAgentCSR);
                            cmd.Parameters.AddWithValue("@CompanyPositionSchemePK", _agent.CompanyPositionSchemePK);
                            cmd.Parameters.AddWithValue("@NPWPNo", _agent.NPWPNo);
                            cmd.Parameters.AddWithValue("@BitPPH23", _agent.BitPPH23);
                            cmd.Parameters.AddWithValue("@BitPPH21", _agent.BitPPH21);
                            cmd.Parameters.AddWithValue("@BitPPN", _agent.BitPPN);
                            cmd.Parameters.AddWithValue("@PPH23Percent", _agent.PPH23Percent);
                            cmd.Parameters.AddWithValue("@MFeeMethod", _agent.MFeeMethod);
                            cmd.Parameters.AddWithValue("@SharingFeeCalculation", _agent.SharingFeeCalculation);

                            cmd.Parameters.AddWithValue("@WAPERDNo", _agent.WAPERDNo);
                            cmd.Parameters.AddWithValue("@WAPERDExpiredDate", _agent.WAPERDExpiredDate);
                            cmd.Parameters.AddWithValue("@AAJINo", _agent.AAJINo);
                            cmd.Parameters.AddWithValue("@AAJIExpiredDate", _agent.AAJIExpiredDate);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _agent.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _agent.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Agent set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _agent.EntryUsersID);
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
                                cmd.CommandText = "Update Agent set  Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,AgentFee=@AgentFee,NoRek=@NoRek,Phone=@Phone,MFeeMethod=@MFeeMethod,SharingFeeCalculation=@SharingFeeCalculation,Fax=@Fax, " +
                                "Email=@Email,Address=@Address,TaxID=@TaxID,BankInformation=@BankInformation,BeneficiaryName=@BeneficiaryName,Description=@Description,JoinDate=@JoinDate," +
                                "Groups=@Groups,Levels=@Levels,ParentPK=@ParentPK,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,DepartmentPK=@DepartmentPK,BitisAgentBank=@BitisAgentBank,BitIsAgentCSR=@BitIsAgentCSR,CompanyPositionSchemePK=@CompanyPositionSchemePK,NPWPNo=@NPWPNo,BitPPH23=@BitPPH23,BitPPH21=@BitPPH21,BitPPN=@BitPPN,PPH23Percent=@PPH23Percent," +
                                "WAPERDNo=@WAPERDNo,WAPERDExpiredDate=@WAPERDExpiredDate,AAJINo=@AAJINo,AAJIExpiredDate=@AAJIExpiredDate," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where AgentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _agent.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                                cmd.Parameters.AddWithValue("@Notes", _agent.Notes);
                                cmd.Parameters.AddWithValue("@ID", _agent.ID);
                                cmd.Parameters.AddWithValue("@Name", _agent.Name);
                                cmd.Parameters.AddWithValue("@Type", _agent.Type);
                                cmd.Parameters.AddWithValue("@AgentFee", _agent.AgentFee);
                                cmd.Parameters.AddWithValue("@NoRek", _agent.NoRek);
                                cmd.Parameters.AddWithValue("@Phone", _agent.Phone);
                                cmd.Parameters.AddWithValue("@Fax", _agent.Fax);
                                cmd.Parameters.AddWithValue("@Email", _agent.Email);
                                cmd.Parameters.AddWithValue("@Address", _agent.Address);
                                cmd.Parameters.AddWithValue("@TaxID", _agent.TaxID);
                                cmd.Parameters.AddWithValue("@BankInformation", _agent.BankInformation);
                                cmd.Parameters.AddWithValue("@BeneficiaryName", _agent.BeneficiaryName);
                                cmd.Parameters.AddWithValue("@Description", _agent.Description);
                                cmd.Parameters.AddWithValue("@JoinDate", _agent.JoinDate);
                                cmd.Parameters.AddWithValue("@Groups", _agent.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _agent.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _agent.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _agent.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _agent.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _agent.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _agent.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _agent.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _agent.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _agent.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _agent.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _agent.ParentPK9);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _agent.DepartmentPK);
                                cmd.Parameters.AddWithValue("@BitisAgentBank", _agent.BitisAgentBank);
                                cmd.Parameters.AddWithValue("@BitIsAgentCSR", _agent.BitIsAgentCSR);
                                cmd.Parameters.AddWithValue("@CompanyPositionSchemePK", _agent.CompanyPositionSchemePK);
                                cmd.Parameters.AddWithValue("@NPWPNo", _agent.NPWPNo);
                                cmd.Parameters.AddWithValue("@BitPPH23", _agent.BitPPH23);
                                cmd.Parameters.AddWithValue("@BitPPH21", _agent.BitPPH21);
                                cmd.Parameters.AddWithValue("@BitPPN", _agent.BitPPN);
                                cmd.Parameters.AddWithValue("@PPH23Percent", _agent.PPH23Percent);
                                cmd.Parameters.AddWithValue("@MFeeMethod", _agent.MFeeMethod);
                                cmd.Parameters.AddWithValue("@SharingFeeCalculation", _agent.SharingFeeCalculation);

                                cmd.Parameters.AddWithValue("@WAPERDNo", _agent.WAPERDNo);
                                cmd.Parameters.AddWithValue("@WAPERDExpiredDate", _agent.WAPERDExpiredDate);
                                cmd.Parameters.AddWithValue("@AAJINo", _agent.AAJINo);
                                cmd.Parameters.AddWithValue("@AAJIExpiredDate", _agent.AAJIExpiredDate);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _agent.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_agent.AgentPK, "Agent");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Agent where AgentPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _agent.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _agent.ID);
                                cmd.Parameters.AddWithValue("@Name", _agent.Name);
                                cmd.Parameters.AddWithValue("@Type", _agent.Type);
                                cmd.Parameters.AddWithValue("@AgentFee", _agent.AgentFee);
                                cmd.Parameters.AddWithValue("@NoRek", _agent.NoRek);
                                cmd.Parameters.AddWithValue("@Phone", _agent.Phone);
                                cmd.Parameters.AddWithValue("@Fax", _agent.Fax);
                                cmd.Parameters.AddWithValue("@Email", _agent.Email);
                                cmd.Parameters.AddWithValue("@Address", _agent.Address);
                                cmd.Parameters.AddWithValue("@TaxID", _agent.TaxID);
                                cmd.Parameters.AddWithValue("@BankInformation", _agent.BankInformation);
                                cmd.Parameters.AddWithValue("@BeneficiaryName", _agent.BeneficiaryName);
                                cmd.Parameters.AddWithValue("@Description", _agent.Description);
                                cmd.Parameters.AddWithValue("@JoinDate", _agent.JoinDate);
                                cmd.Parameters.AddWithValue("@Groups", _agent.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _agent.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _agent.ParentPK);
                                cmd.Parameters.AddWithValue("@ParentPK1", _agent.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _agent.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _agent.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _agent.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _agent.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _agent.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _agent.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _agent.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _agent.ParentPK9);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _agent.DepartmentPK);
                                cmd.Parameters.AddWithValue("@BitisAgentBank", _agent.BitisAgentBank);
                                cmd.Parameters.AddWithValue("@BitIsAgentCSR", _agent.BitIsAgentCSR);
                                cmd.Parameters.AddWithValue("@CompanyPositionSchemePK", _agent.CompanyPositionSchemePK);
                                cmd.Parameters.AddWithValue("@NPWPNo", _agent.NPWPNo);
                                cmd.Parameters.AddWithValue("@BitPPH23", _agent.BitPPH23);
                                cmd.Parameters.AddWithValue("@BitPPH21", _agent.BitPPH21);
                                cmd.Parameters.AddWithValue("@BitPPN", _agent.BitPPN);
                                cmd.Parameters.AddWithValue("@PPH23Percent", _agent.PPH23Percent);
                                cmd.Parameters.AddWithValue("@MFeeMethod", _agent.MFeeMethod);
                                cmd.Parameters.AddWithValue("@SharingFeeCalculation", _agent.SharingFeeCalculation);

                                cmd.Parameters.AddWithValue("@WAPERDNo", _agent.WAPERDNo);
                                cmd.Parameters.AddWithValue("@WAPERDExpiredDate", _agent.WAPERDExpiredDate);
                                cmd.Parameters.AddWithValue("@AAJINo", _agent.AAJINo);
                                cmd.Parameters.AddWithValue("@AAJIExpiredDate", _agent.AAJIExpiredDate);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _agent.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Agent set status= 4,Notes=@Notes, " +
                                " lastupdate=@lastupdate where AgentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _agent.Notes);
                                cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _agent.HistoryPK);
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

        public void Agent_Approved(Agent _agent)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Agent set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where AgentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _agent.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _agent.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Agent set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _agent.ApprovedUsersID);
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

        public void Agent_Reject(Agent _agent)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Agent set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AgentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _agent.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _agent.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Agent set status= 2,LastUpdate=@LastUpdate where AgentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
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

        public void Agent_Void(Agent _agent)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Agent set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where AgentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _agent.AgentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _agent.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _agent.VoidUsersID);
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

        public List<AgentCombo> Agent_ComboRpt()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentCombo> L_Agent = new List<AgentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT  AgentPK,ID + ' - ' + Name as ID, Name FROM [Agent]  where status = 2 union all select 0,'All', '' order by AgentPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AgentCombo M_Agent = new AgentCombo();
                                    M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
                                    M_Agent.ID = Convert.ToString(dr["ID"]);
                                    M_Agent.Name = Convert.ToString(dr["Name"]);
                                    L_Agent.Add(M_Agent);
                                }

                            }
                        }
                        return L_Agent;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<AgentCombo> AgentLevelOne_ComboRpt()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentCombo> L_Agent = new List<AgentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT  AgentPK,ID + ' - ' + Name as ID, Name FROM [Agent]  where Levels = 1 and status = 2 union all select 0,'All', '' order by AgentPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AgentCombo M_Agent = new AgentCombo();
                                    M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
                                    M_Agent.ID = Convert.ToString(dr["ID"]);
                                    M_Agent.Name = Convert.ToString(dr["Name"]);
                                    L_Agent.Add(M_Agent);
                                }

                            }
                        }
                        return L_Agent;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AgentCombo> Agent_Combo()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentCombo> L_Agent = new List<AgentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT  AgentPK,ID + ' - ' + Name ID, Name FROM [Agent]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AgentCombo M_Agent = new AgentCombo();
                                    M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
                                    M_Agent.ID = Convert.ToString(dr["ID"]);
                                    M_Agent.Name = Convert.ToString(dr["Name"]);
                                    L_Agent.Add(M_Agent);
                                }

                            }
                        }
                        return L_Agent;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public AgentLookup Fund_LookupByAgentPK(int _agentPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select * from Agent where status = 2 and AgentPK = @AgentPK";
                        cmd.Parameters.AddWithValue("@AgentPK", _agentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            { // 5 Field
                                dr.Read();
                                AgentLookup M_Agent = new AgentLookup();
                                M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
                                M_Agent.ID = dr["ID"].ToString();
                                M_Agent.Name = dr["Name"].ToString();
                                M_Agent.Type = dr["Type"].ToString();
                                M_Agent.AgentFee = Convert.ToDecimal(dr["AgentFee"]);
                                return M_Agent;

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

        public List<AgentCombo> Agent_ComboGroupOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentCombo> L_Agent = new List<AgentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AgentPK,ID + ' - ' + Name as [ID], Name FROM [Agent]  where Groups = 1 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AgentCombo M_Agent = new AgentCombo();
                                    M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
                                    M_Agent.ID = Convert.ToString(dr["ID"]);
                                    M_Agent.Name = Convert.ToString(dr["Name"]);
                                    L_Agent.Add(M_Agent);
                                }
                            }
                            return L_Agent;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<AgentCombo> Agent_ComboChildOnly()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentCombo> L_Agent = new List<AgentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  AgentPK,ID + ' - ' + Name as [ID], Name FROM [Agent]  where Groups = 0 and status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AgentCombo M_Agent = new AgentCombo();
                                    M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
                                    M_Agent.ID = Convert.ToString(dr["ID"]);
                                    M_Agent.Name = Convert.ToString(dr["Name"]);
                                    L_Agent.Add(M_Agent);
                                }
                            }
                            return L_Agent;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public bool Agent_UpdateParentAndDepth()
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Agent SET " +
                                                "Agent.ParentPK1 = isnull(Agent_1.AgentPK,0), Agent.ParentPK2 = isnull(Agent_2.AgentPK,0), " +
                                                "Agent.ParentPK3 = isnull(Agent_3.AgentPK,0), Agent.ParentPK4 = isnull(Agent_4.AgentPK,0), " +
                                                "Agent.ParentPK5 = isnull(Agent_5.AgentPK,0), Agent.ParentPK6 = isnull(Agent_6.AgentPK,0), " +
                                                "Agent.ParentPK7 = isnull(Agent_7.AgentPK,0), Agent.ParentPK8 = isnull(Agent_8.AgentPK,0), " +
                                                "Agent.ParentPK9 = isnull(Agent_9.AgentPK,0)  " +
                                                "FROM Agent " +
                                                "LEFT JOIN Agent AS Agent_1 ON Agent.ParentPK = Agent_1.AgentPK and Agent_1.status in (1,2) " +
                                                "LEFT JOIN Agent AS Agent_2 ON Agent_1.ParentPK = Agent_2.AgentPK and Agent_2.status in (1,2)  " +
                                                "LEFT JOIN Agent AS Agent_3 ON Agent_2.ParentPK = Agent_3.AgentPK and Agent_3.status in (1,2)  " +
                                                "LEFT JOIN Agent AS Agent_4 ON Agent_3.ParentPK = Agent_4.AgentPK and Agent_4.status in (1,2)  " +
                                                "LEFT JOIN Agent AS Agent_5 ON Agent_4.ParentPK = Agent_5.AgentPK and Agent_5.status in (1,2)  " +
                                                "LEFT JOIN Agent AS Agent_6 ON Agent_5.ParentPK = Agent_6.AgentPK and Agent_6.status in (1,2)  " +
                                                "LEFT JOIN Agent AS Agent_7 ON Agent_6.ParentPK = Agent_7.AgentPK and Agent_7.status in (1,2)  " +
                                                "LEFT JOIN Agent AS Agent_8 ON Agent_7.ParentPK = Agent_8.AgentPK and Agent_8.status in (1,2)  " +
                                                "LEFT JOIN Agent AS Agent_9 ON Agent_8.ParentPK = Agent_9.AgentPK and Agent_9.status in (1,2) Where Agent.Status = 2 ";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select AgentPK From Agent Where Status = 2 and Groups = 1";
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
                                                cmdSubQuery.CommandText = "Update Agent set Depth = @Depth, lastupdate=@lastupdate Where AgentPK = @AgentPK and Status = 2";
                                                cmdSubQuery.Parameters.AddWithValue("@Depth", GetAgentDepth(Convert.ToInt32(dr["AgentPK"])));
                                                cmdSubQuery.Parameters.AddWithValue("@AgentPK", Convert.ToInt32(dr["AgentPK"]));
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

        private int GetAgentDepth(int _AgentPK)
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
                                          "SELECT @Depth1 = MAX(Agent_1.ParentPK), @Depth2 = MAX(Agent_2.ParentPK), " +
                                          "@Depth3 = MAX(Agent_3.ParentPK), @Depth4 = MAX(Agent_4.ParentPK), " +
                                          "@Depth5 = MAX(Agent_5.ParentPK), @Depth6 = MAX(Agent_6.ParentPK), " +
                                          "@Depth7 = MAX(Agent_7.ParentPK), @Depth8 = MAX(Agent_8.ParentPK), " +
                                          "@Depth9 = MAX(Agent_9.ParentPK), @Depth10 = MAX(Agent_10.ParentPK) " +
                                          "FROM Agent AS Agent_10 RIGHT JOIN (Agent AS Agent_9 " +
                                          "RIGHT JOIN (Agent AS Agent_8 RIGHT JOIN (Agent AS Agent_7 " +
                                          "RIGHT JOIN (Agent AS Agent_6 RIGHT JOIN (Agent AS Agent_5 " +
                                          "RIGHT JOIN (Agent AS Agent_4 RIGHT JOIN (Agent AS Agent_3 " +
                                          "RIGHT JOIN (Agent AS Agent_2 RIGHT JOIN (Agent AS Agent_1 " +
                                          "RIGHT JOIN Agent ON Agent_1.ParentPK = Agent.AgentPK) " +
                                          "ON Agent_2.ParentPK = Agent_1.AgentPK) ON Agent_3.ParentPK = Agent_2.AgentPK) " +
                                          "ON Agent_4.ParentPK = Agent_3.AgentPK) ON Agent_5.ParentPK = Agent_4.AgentPK) " +
                                          "ON Agent_6.ParentPK = Agent_5.AgentPK) ON Agent_7.ParentPK = Agent_6.AgentPK) " +
                                          "ON Agent_8.ParentPK = Agent_7.AgentPK) ON Agent_9.ParentPK = Agent_8.AgentPK) " +
                                          "ON Agent_10.ParentPK = Agent_9.AgentPK  " +
                                          "WHERE Agent.AgentPK = @AgentPK and Agent.Status = 2 " +
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
                        cmd.Parameters.AddWithValue("@AgentPK", _AgentPK);
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


        public int Agent_LookupByAgentPK(int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select * from Agent A Left join FundClient FC on A.AgentPK  = FC.SellingAgentPK and FC.status = 2 where A.status = 2 and FC.FundClientPK = @FundClientPK";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["AgentPK"]);
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

        public string Agent_GenerateNewAgentID()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        //RHB
                        cmd.CommandText =

                         " Declare @NewClientID  nvarchar(100)    " +
                         " Declare @MaxClientName  nvarchar(100)         " +
                         " Declare @LENdigit int               " +
                         " Declare @NewDigit Nvarchar(20)     " +
                         " select @MaxClientName =   SUBSTRING ( ID ,3 , 4 ) +1  from Agent where  status in (1,2) order by id " +
                         " select @LENdigit = LEN(@MaxClientName)            " +
                         " if @LENdigit = 1      " +
                         " BEGIN              " +
                         " set @NewDigit = '000' + CAST(@MaxClientName as nvarchar)       " +
                         " END                    " +
                         " if @LENdigit = 2         " +
                         " BEGIN                     " +
                         " set @NewDigit = '00' + CAST(@MaxClientName as nvarchar)       " +
                         " END               " +
                         " if @LENdigit = 3      " +
                         " BEGIN                     " +
                         " set @NewDigit = '0' + CAST(@MaxClientName as nvarchar)        " +
                         " END         " +
                         " ELSE BEGIN                     " +
                         " set @NewDigit = CAST(@MaxClientName as nvarchar)        " +
                         " END         " +
                         " set @NewClientID = CAST(@NewDigit as nvarchar)  " +
                         " Select @NewClientID   NewAgentID  ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["NewAgentID"]);
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


        public List<SetAgentFee> Agent_GetDataAgentFee(int _pk)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SetAgentFee> L_setAgentFee = new List<SetAgentFee>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID,B.Name AgentName,C.DescOne FeeTypeDesc,D.DescOne TypeTrxDesc,isnull(E.ID,'ALL') FundID,isnull(E.Name,'ALL') FundName, A.* from AgentFeeSetup A
                        left join Agent B on A.AgentPK = B.AgentPK and B.Status in (1,2)
                        left join MasterValue C on A.FeeType = C.Code and C.Status = 2 and C.ID = 'AgentFeeType'
                        left join MasterValue D on A.TypeTrx = D.Code and D.Status = 2 and D.ID = 'AgentFeeTrxType'
                        left join Fund E on A.FundPK = E.FundPK and E.Status in (1,2)
                        where A.AgentPK = @PK and A.status in (1,2)
                        order by AgentFeeSetupPK desc
                               ";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_setAgentFee.Add(setAgentFee(dr));
                                }
                            }
                        }
                        return L_setAgentFee;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private SetAgentFee setAgentFee(SqlDataReader dr)
        {
            SetAgentFee M_Agent = new SetAgentFee();
            M_Agent.Status = Convert.ToInt32(dr["Status"]);
            M_Agent.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Agent.Selected = Convert.ToBoolean(dr["Selected"]);
            M_Agent.AgentFeeSetupPK = Convert.ToInt32(dr["AgentFeeSetupPK"]);
            M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_Agent.Agent = Convert.ToString(dr["AgentName"]);
            M_Agent.FundName = Convert.ToString(dr["FundName"]);
            M_Agent.Date = Convert.ToString(dr["Date"]);
            M_Agent.DateAmortize = Convert.ToString(dr["DateAmortize"]);
            M_Agent.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_Agent.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);
            M_Agent.TypeTrx = Convert.ToInt32(dr["TypeTrx"]);
            M_Agent.TypeTrxDesc = Convert.ToString(dr["TypeTrxDesc"]);
            M_Agent.RangeTo = Convert.ToDecimal(dr["RangeTo"]);
            M_Agent.RangeFrom = Convert.ToDecimal(dr["RangeFrom"]);
            M_Agent.MIFeeAmount = Convert.ToDecimal(dr["MIFeeAmount"]);
            M_Agent.MIFeePercent = Convert.ToDecimal(dr["MIFeePercent"]);
            return M_Agent;
        }


        public void AddAgentFee(SetAgentFee _AgentFee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        declare @Agent int
                        select AgentPK,Name from agent where AgentPK = @AgentPK and status in(1,2)
                            
                        Insert into AgentFeeSetup(AgentFeeSetupPK,HistoryPK,Status,AgentPK,Date,DateAmortize,RangeFrom,RangeTo,MIFeeAmount,MIFeePercent,FeeType,TypeTrx,FundPK,LastUpdate,UpdateUsersID,UpdateTime) 
                        Select isnull(max(AgentFeeSetupPK),0) + 1,1,2,@AgentPK,@Date,@DateAmortize,@RangeFrom,@RangeTo,@MIFeeAmount,@MIFeePercent,@FeeType,@TypeTrx,@FundPK,@LastUpdate,@EntryUsersID,@EntryTime from AgentFeeSetup";

                        cmd.Parameters.AddWithValue("@AgentPK", _AgentFee.AgentPK);
                        cmd.Parameters.AddWithValue("@Date", _AgentFee.Date);
                        cmd.Parameters.AddWithValue("@DateAmortize", _AgentFee.DateAmortize);
                        cmd.Parameters.AddWithValue("@FeeType", _AgentFee.FeeType);
                        cmd.Parameters.AddWithValue("@TypeTrx", _AgentFee.TypeTrx);
                        cmd.Parameters.AddWithValue("@RangeTo", _AgentFee.RangeTo);
                        cmd.Parameters.AddWithValue("@RangeFrom", _AgentFee.RangeFrom);
                        cmd.Parameters.AddWithValue("@MIFeeAmount", _AgentFee.MIFeeAmount);
                        cmd.Parameters.AddWithValue("@MIFeePercent", _AgentFee.MIFeePercent);
                        cmd.Parameters.AddWithValue("@FundPK", _AgentFee.FundPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AgentFee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentFee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
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

        public void RejectedDataAgentFeeSetupBySelected(string _usersID, string param2, int _agentPK)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentFeeSetup set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where Selected = 1 and AgentPK = @AgentPK and status <> 3 ";
                        cmd.Parameters.AddWithValue("@VoidUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentPK);
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

        public bool CheckHassAdd(int _pk, string _date, int _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from AgentFeeSetup where AgentPK = @PK and Status in (1,2) and Date = @Date and FeeType <> @Type";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Type", _type);

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


        public bool CheckDataFlat(int _pk, string _date, int _type, int _agentPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select * from AgentFeeSetup where Status in (1,2) and Date = @Date and FeeType = 5 and FundPK = @PK and AgentPK = @AgentPK";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Type", _type);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentPK);

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

        public List<AgentCombo> AgentCSRFund_Combo()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentCombo> L_Agent = new List<AgentCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT  AgentPK,ID + ' - ' + Name ID, Name FROM [Agent]  where status in (1,2) and BitIsAgentCSR = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AgentCombo M_Agent = new AgentCombo();
                                    M_Agent.AgentPK = Convert.ToInt32(dr["AgentPK"]);
                                    M_Agent.ID = Convert.ToString(dr["ID"]);
                                    M_Agent.Name = Convert.ToString(dr["Name"]);
                                    L_Agent.Add(M_Agent);
                                }

                            }
                        }
                        return L_Agent;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }


        public List<AgentTreeSetup> Get_DataInformationAgentTreeSetup(string _fundID, int _agentPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentTreeSetup> L_model = new List<AgentTreeSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

                            SELECT DISTINCT Date,A.FundPK,ISNULL(B.ID,'ALL') FundID FROM AgentTreePercentageSetup A
                            LEFT JOIN FUND B ON A.FundPK = B.FundPK AND B.status IN (1,2)
                            WHERE A.ChildPK = @ChildPK order by A.Date Desc ";

                        //cmd.Parameters.AddWithValue("@FundPK", _fundID);
                        cmd.Parameters.AddWithValue("@ChildPK", _agentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    AgentTreeSetup M_model = new AgentTreeSetup();
                                    M_model.Date = Convert.ToString(dr["Date"]);
                                    M_model.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_model.FundID = Convert.ToString(dr["FundID"]);

                                    L_model.Add(M_model);


                                }
                            }
                            return L_model;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<AgentTreeSetup> Get_DataDetailAgentTreeSetup(DateTime _date, string _fundID, int _agentPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentTreeSetup> L_model = new List<AgentTreeSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"

                        SELECT A.Date,A.ChildPK,A.ParentPK,A.FundPK,ISNULL(D.ID,'ALL') FundID,ISNULL(C.ID,'') ParentID,C.Name ParentName,A.Levels,A.FeePercent FROM  dbo.AgentTreePercentageSetup A
                        LEFT JOIN Agent C ON A.ParentPK = C.AgentPK AND C.status IN (1,2)
                        LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
                        WHERE A.Date = @Date AND A.ChildPK = @ChildPK  And A.FundPK = @FundPK
                        ORDER BY D.ID ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundID);
                        cmd.Parameters.AddWithValue("@ChildPK", _agentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    AgentTreeSetup M_model = new AgentTreeSetup();
                                    M_model.Date = Convert.ToString(dr["Date"]);
                                    M_model.FundID = Convert.ToString(dr["FundID"]);
                                    M_model.ParentID = Convert.ToString(dr["ParentID"]);
                                    M_model.ParentName = Convert.ToString(dr["ParentName"]);
                                    M_model.Levels = Convert.ToInt32(dr["Levels"]);
                                    M_model.FeePercent = Convert.ToDecimal(dr["FeePercent"]);
                                    M_model.ChildPK = Convert.ToInt32(dr["ChildPK"]);
                                    M_model.ParentPK = Convert.ToInt32(dr["ParentPK"]);
                                    M_model.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    L_model.Add(M_model);


                                }
                            }
                            return L_model;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public void Generate_AgentTreeSetup(AgentTreeSetup _agentTreeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
    --DECLARE @TimeNow Datetime
--DECLARE @ChildPK INT
--DECLARE @Date DATETIME
--DECLARE @FundPK int

--SET @TimeNow = GETDATE()

--SET @ChildPK = 271
--SET @Date = '04/02/19'


DECLARE @ParentPK INT,@ParentPK1 INT,@ParentPK2 INT,@ParentPK3 INT,@ParentPK4 INT
,@ParentPK5 INT,@ParentPK6 INT,@ParentPK7 INT,@ParentPK8 INT,@ParentPK9 INT

SELECT @ParentPK = ParentPK,@ParentPK1 = A.ParentPK1,@ParentPK2 = A.ParentPK2,@ParentPK3 = A.ParentPK3
,@ParentPK4 = A.ParentPK4,@ParentPK5 = ParentPK5,@ParentPK6 = A.ParentPK6,@ParentPK7 = A.ParentPK7
,@ParentPK8 = A.ParentPK8,@ParentPK9 = A.ParentPK9
FROM Agent A
WHERE A.AgentPK = @ChildPK
AND A.status IN (1,2)





DELETE AgentTreePercentageSetup where Date = @Date and FundPK = @FundPK and ChildPK = @ChildPK


	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,@ChildPK,0,@Date,ISNULL(@fundPK,0),0,@TimeNow 

	
IF(ISNULL(@ParentPK1,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,1,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK1
END	

IF(ISNULL(@ParentPK2,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,2,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK2
END	

IF(ISNULL(@ParentPK3,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,3,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK3
END	

IF(ISNULL(@ParentPK4,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,4,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK4
END	

IF(ISNULL(@ParentPK5,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,5,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK5
END	

IF(ISNULL(@ParentPK6,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,6,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK6
END	

IF(ISNULL(@ParentPK7,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,7,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK7
END	

IF(ISNULL(@ParentPK8,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,8,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK8
END	


IF(ISNULL(@ParentPK9,0) <> 0)
BEGIN
	INSERT INTO dbo.AgentTreePercentageSetup
	        ( ChildPK ,
	          ParentPK ,
	          Levels ,
	          Date ,
	          FundPK ,
	          FeePercent ,
	          Lastupdate
	        )
	SELECT @ChildPK,A.AgentPK,9,@Date,ISNULL(@fundPK,0),0,@TimeNow FROM Agent A WHERE A.Status IN (1,2)
	AND A.AgentPK = @ParentPK9
END	


";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _agentTreeSetup.ParamDate);
                        cmd.Parameters.AddWithValue("@ChildPK", _agentTreeSetup.ParamAgent);
                        cmd.Parameters.AddWithValue("@FundPK", _agentTreeSetup.ParamFund);
                        cmd.Parameters.AddWithValue("@TimeNow", _datetimeNow);
                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public bool Validate_UpdateAgentTreeSetup(AgentTreeSetup _agentTreeSetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                            @"

                        --DECLARE @NewFeePercent NUMERIC(8,4)
                        --SET @NewFeePercent = 120


                        DECLARE @CurrentFeePercent NUMERIC(8,4)
                        SELECT @CurrentFeePercent =  SUM(ISNULL(FeePercent,0)) FROM dbo.AgentTreePercentageSetup WHERE date = @Date AND  ChildPK = @ChildPK

                        IF (ISNULL(@CurrentFeePercent,0) + @NewFeePercent) > 100
                        BEGIN
	                        SELECT 1 Result
                        END
                        ELSE
                        BEGIN
	                        SELECT 0 Result
                        END ";

                        cmd.Parameters.AddWithValue("@NewFeePercent", _agentTreeSetup.FeePercent);
                        cmd.Parameters.AddWithValue("@Date", _agentTreeSetup.Date);
                        cmd.Parameters.AddWithValue("@ChildPK", _agentTreeSetup.ChildPK);
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


        public void Update_AgentTreeSetup(AgentTreeSetup _agentTreeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                        @"

                        Update AgentTreePercentageSetup set FeePercent = @FeePercent, LastUpdate = @TimeNow where Date = @Date and ChildPK = @ChildPK
                        and ParentPK = @ParentPK and FundPK = @FundPK and Levels = @Levels";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@FeePercent", _agentTreeSetup.FeePercent);
                        cmd.Parameters.AddWithValue("@Date", _agentTreeSetup.Date);
                        cmd.Parameters.AddWithValue("@ChildPK", _agentTreeSetup.ChildPK);
                        cmd.Parameters.AddWithValue("@ParentPK", _agentTreeSetup.ParentPK);
                        cmd.Parameters.AddWithValue("@FundPK", _agentTreeSetup.FundPK);
                        cmd.Parameters.AddWithValue("@Levels", _agentTreeSetup.Levels);
                        cmd.Parameters.AddWithValue("@TimeNow", _datetimeNow);
                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string Validate_WaperdLicense(int _AgentPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            
                            select case when WAPERDExpiredDate is null then 0 
                            when WAPERDExpiredDate <= @ValueDate then 1 else 0 end Result 
                            from Agent where AgentPK = @AgentPK and status in (1,2)";

                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@AgentPK", _AgentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();
                            }
                            else
                            {
                                return "0";
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