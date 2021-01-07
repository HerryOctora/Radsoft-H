using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;

namespace RFSRepository
{
    public class Host
    {

        public bool CheckOnlyOneWordInString(string _param)
        {
            string[] array = _param.Trim().Split(' ');
            if (array.Length == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean findString(String baseString, String strinfToFind, String separator)
        {
            foreach (String str in baseString.Split(separator.ToCharArray()))
            {
                if (str.Equals(strinfToFind))
                {
                    return true;
                }
            }
            return false;
        }
        
        public bool IsFileReady(string _path)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(_path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return true;
            }
            catch
            {
                return false;
            }
            finally // don't forget close the stream, if you wish to use this file later
            {
                if (stream != null)
                    stream.Close();
            }
        }

        public bool CheckColumnIsExist(IDataReader _dr, string _columnName)
        {
            return _dr.GetSchemaTable()
                   .Rows
                   .OfType<DataRow>()
                   .Any(row => row["ColumnName"].ToString().ToUpper() == _columnName.ToUpper());

        }
        
        public void SelectDeselectData(string _tableName, bool _toggle, int _PK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update [" + _tableName + "] set Selected = @Toggle where " + _tableName + "PK  = @PK ";
                        cmd.Parameters.AddWithValue("@PK", _PK);
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

        public void SelectDeselectAllData(string _tableName, bool _toggle)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update [" + _tableName + "] set Selected = @Toggle ";
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
        
        public void SelectDeselectDataInvestment(string _tableName, bool _toggle, int _PK, string _inv, string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_type == "None")
                        {

                            cmd.CommandText = "Update " + _tableName + " set Selected = @Toggle where " + _inv + "PK  = @PK";
                        }
                        else
                        {
                            int _typePK = 0;
                            if (_type == "EQUITY")
                            {
                                _typePK = 1;
                            }
                            else if (_type == "BOND")
                            {
                                _typePK = 2;
                            }
                            else
                            {
                                _typePK = 3;
                            }
                            cmd.CommandText = "Update [" + _tableName + "] set Selected = @Toggle where " + _inv + "PK  = @PK and InstrumentTypePK = @Type";
                            cmd.Parameters.AddWithValue("@Type", _typePK);
                        }

                        cmd.Parameters.AddWithValue("@PK", _PK);
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

        public void SelectDeselectAllDataByDateInvestment(string _tableName, bool _toggle, DateTime _DateFrom, DateTime _DateTo, string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_type == "None")
                        {
                            cmd.CommandText = "Update [" + _tableName + "] set Selected = @Toggle where valueDate Between @DateFrom and @DateTo ";
                        }
                        else
                        {
                            int _typePK = 0;
                            if (_type == "EQUITY")
                            {
                                _typePK = 1;
                            }
                            else if (_type == "BOND")
                            {
                                _typePK = 2;
                            }
                            else
                            {
                                _typePK = 3;
                            }
                            cmd.CommandText = "Update [" + _tableName + "] set Selected = @Toggle where valueDate Between @DateFrom and @DateTo and InstrumentTypePK = @Type ";
                            cmd.Parameters.AddWithValue("@Type", _typePK);
                        }

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _DateTo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataCashier(string _tableName, bool _toggle, int _PK, string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update [" + _tableName + "] set Selected = @Toggle where " + _tableName + "PK  = @PK and Type =@Type";
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

        public void SelectDeselectAllDataByDate(string _tableName, bool _toggle, DateTime _DateFrom, DateTime _DateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    string _valuedate = "ValueDate";
                    if (_tableName == "FixedAsset")
                    {
                        _valuedate = "BuyValueDate";
                    }
                    if (_tableName == "Order")
                    {
                        _valuedate = "OrderDate";
                    }
                    if (_tableName == "ClosePrice" || _tableName == "CloseNav" || _tableName == "AUM" || _tableName == "HaircutMKBD" || _tableName == "UpdateClosePrice" || _tableName == "HighRiskMonitoring" || _tableName == "InstrumentSyariah")
                    {
                        _valuedate = "Date";
                    }

                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update [" + _tableName + "] set Selected = @Toggle where " + _valuedate + " Between @DateFrom and @DateTo ";
                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectAllDataByDateCashier(string _tableName, bool _toggle, DateTime _DateFrom, DateTime _DateTo, string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update [" + _tableName + "] set Selected = @Toggle where valueDate Between @DateFrom and @DateTo and Type =@Type ";
                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _DateTo);
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

        public decimal GetBankBalanceByBankAccountNoAndDate(string _bankaccountno, string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            "select isnull(sum(a.FundBalance), 0) as Balance \n " +
                            "from CashPosition a \n " +
                                "left join Client b on b.Status = 2 and a.ClientID = b.ID --and b.StatusClientBank = 2 \n " +
                                "left join ClientBank c on c.Status = b.StatusClientBank and c.ClientPK = b.ClientPK \n " +
                            "where a.Status = 2 and c.FundBankAccountNo = @BankAccountNo and a.Date <= @Date";
                        cmd.Parameters.AddWithValue("@BankAccountNo", _bankaccountno);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Balance"]);
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

        public int Get_UsersPK(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select UsersPK From Users Where ID = @UsersID and status = 2";
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["UsersPK"]);
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

        public string Get_UsersName(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select isnull(Name, '') as Name from Users where ID = @UsersID and Status = 2";
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_UsersMail(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select isnull(email, '') as email from Users where ID = left(@usersID,charindex(' ',@usersID) - 1) and Status in (1,2)";
                        cmd.Parameters.AddWithValue("@usersID", _userID);
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["email"]);
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

        public int Get_UsersDivisi(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select isnull(Divisi, 0) as Divisi from Users where ID = @UsersID and Status = 2";
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["Divisi"]);
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

        public int Get_Status(int _PK, long _historyPK, string _table)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select status From [" + _table + "] Where " + _table + "PK =@PK and historyPK = @HistoryPK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _historyPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["status"]);
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

        public string Get_LastUpdate(int _PK, long _historyPK, string _table)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string param = _table + "PK";
                        if (_table == "Dealing")
                        {
                            param = "DealingPK";
                            _table = "Investment";
                        }
                        if (_table == "Settlement")
                        {
                            param = "SettlementPK";
                            _table = "Investment";
                        }
                        
                        cmd.CommandText = "select LastUpdate from [" + _table + "] where " + param + "= @PK " +
                            "and historyPK = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        cmd.Parameters.AddWithValue("@historyPK", _historyPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["LastUpdate"]);

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

        public DateTime Get_LastTrxDateFromTrxClient()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select [TrxDate] from [TrxClient] group by [TrxDate] order by [TrxDate] desc ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return DateTime.Now;
                            }
                            else
                            {
                                return Convert.ToDateTime(dr["TrxDate"]);
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


        public int Get_NewHistoryPK(int _PK, string _table)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        /*query dibawa sama ama bgini 
                         *  declare @sql nvarchar(300)
                            declare @table nvarchar(50)
                            set @table = 'users'
                            set @sql = 'select max(historypk)+1 newHistoryPK from '+ @table +' where '+@table +'PK = 1'
                            exec(@sql) 
                         */

                        string param = _table + "PK";
                        if (_table == "Dealing")
                        {
                            param = "DealingPK";
                            _table = "Investment";
                        }
                        if (_table == "Settlement")
                        {
                            param = "SettlementPK";
                            _table = "Investment";
                        }
                   
                        cmd.CommandText = "select max(historypk)+1 newHistoryPK from [" + _table + "] where " + param + "= @PK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["NewHistoryPK"]);

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



        public int Get_DetailNewAutoNo(int _PK, string _table, string _param)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        /*query dibawa sama ama bgini 
                         *  declare @sql nvarchar(300)
                            declare @table nvarchar(50)
                            set @table = 'users'
                            set @sql = 'select max(historypk)+1 newHistoryPK from '+ @table +' where '+@table +'PK = 1'
                            exec(@sql) 
                         */

                        cmd.CommandText = "select isnull(max(AutoNo),0) + 1 AutoNo from [" + _table + "] where " + _param + "= @PK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["AutoNo"]);

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

        public int Get_LastPKByLastUpate(DateTime _lastUpdate, string _table)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        /*query dibawa sama ama bgini 
                         *  declare @sql nvarchar(300)
                            declare @table nvarchar(50)
                            set @table = 'users'
                            set @sql = 'select max(historypk)+1 newHistoryPK from '+ @table +' where '+@table +'PK = 1'
                            exec(@sql) 
                         */
                        cmd.CommandText = "select top 1 " + _table + "PK  NoPK" + " from [" + _table + "] where LastUpdate =  @LastUpdate";
                        cmd.Parameters.AddWithValue("@LastUpdate", _lastUpdate);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["NoPK"]);

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

        public bool Get_Permission(string _userID, string _permissionID)
        {
            try
            {
                long permissionPK = Get_PermissionPK(_permissionID);
                int usersPK = Get_UsersPK(_userID);
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "if exists (" +
                        "select * from rolesusers ru " +
                        "left join rolespermission rp " +
                            "on ru.rolesPK  = rp.rolesPK " +
                        "left join permission p " +
                            "on rp.permissionPK = p.permissionPK " +
                        "where ru.[status] = 2 and rp.[status] = 2 and p.[status] = 2 " +
                        "and p.permissionPK = @permissionPK and ru.usersPK = @usersPK " +
                        ") or Exists ( " +
                        "select * from GroupsUsers gu " +
                        "left join GroupsRoles gr " +
                            "on gu.groupsPK = gr.groupsPK " +
                        "left join RolesPermission rp " +
                            "on gr.rolesPK = rp.rolesPK " +
                        "left join RolesUsers ru " +
                            "on rp.rolesPK= ru.rolesPK " +
                        "left join permission p " +
                            "on rp.permissionPK = p.permissionPK " +
                        "where gu.[status] = 2 and gr.[status] = 2 and  rp.[status] = 2 and ru.[status] = 2 and p.[status] = 2 " +
                        "and p.PermissionPK = @permissionPK and ru.usersPK = @usersPK) " +
                        "Begin select 1 [permission] end";

                        cmd.Parameters.AddWithValue("@permissionPK", permissionPK);
                        cmd.Parameters.AddWithValue("@usersPK", usersPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return false;
                            }
                            else
                            {
                                return Convert.ToBoolean(dr["permission"]);
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

        public bool Get_Privillege(string _userID, string _permissionID)
        {
            try
            {
                long permissionPK = Get_PermissionPK(_permissionID);
                int usersPK = Get_UsersPK(_userID);
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "if exists (" +
                        "select * from rolesusers ru " +
                        "left join rolespermission rp " +
                            "on ru.rolesPK  = rp.rolesPK " +
                        "left join permission p " +
                            "on rp.permissionPK = p.permissionPK " +
                        "left join roles r  " +
                            "on r.rolesPK = rp.rolesPK " +
                        "where ru.[status] = 2 and rp.[status] = 2 and p.[status] = 2 and r.[status] = 2 " +
                        "and r.privillege = 1 and p.permissionPK = @permissionPK and ru.usersPK = @usersPK " +
                        ") or Exists( " +
                        "select * from GroupsUsers gu " +
                        "left join GroupsRoles gr " +
                        "   on gu.groupsPK = gr.groupsPK " +
                        "left join RolesPermission rp " +
                            "on gr.rolesPK = rp.rolesPK " +
                        "left join RolesUsers ru " +
                            "on rp.rolesPK= ru.rolesPK " +
                        "left join permission p " +
                            "on rp.permissionPK = p.permissionPK " +
                        "left join [roles] r " +
                            "on r.rolesPK = rp.rolesPK " +
                        "where gu.[status] = 2 and gr.[status] = 2 and  rp.[status] = 2 and ru.[status] = 2 and p.[status] = 2 " +
                        "and r.[status] = 2 and r.privillege = 1 and p.permissionPK = @permissionPK and ru.usersPK = @usersPK) " +
                        "Begin select 1 [privillege] end";

                        cmd.Parameters.AddWithValue("@permissionPK", permissionPK);
                        cmd.Parameters.AddWithValue("@usersPK", usersPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return false;
                            }
                            else
                            {
                                return Convert.ToBoolean(dr["privillege"]);
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

        public long Get_PermissionPK(string _permissionID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select PermissionPK From Permission Where ID= @PermissionID and status = 2";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt64(dr["PermissionPK"]);
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

        public string Get_MISCostCenterID(string _misCostCenterPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select ID From MISCostCenter Where MisCostCenterPK=" + _misCostCenterPK.ToString() + " and status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["ID"]);
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

        public Boolean Check_Holiday(DateTime _date)
        {
            DateTime Date = _date;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "Select * From MarketHoliday Where Date=@date and status = 2";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read() || _date.DayOfWeek == DayOfWeek.Saturday || _date.DayOfWeek == DayOfWeek.Sunday)
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

        public DateTime Check_Pastday(DateTime _date)
        {
            DateTime Date = _date;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " declare @ValueDate datetime " +
                        " if (datename(dw,@a - 1) = 'Sunday') " +
                        " begin select 'a' end else begin select 'b' end ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read() || _date.DayOfWeek == DayOfWeek.Saturday || _date.DayOfWeek == DayOfWeek.Sunday)
                            {
                                Date = NextWorkingDay(_date, -1);
                                return Date;
                            }
                            else
                            {
                                return Date;
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

        public DateTime GetWorkingDay(DateTime _date, int _days)
        {
            int counter = 0;
            DateTime resultDate = _date;
            while (counter < Math.Abs(_days))
            {
                resultDate = NextWorkingDay(resultDate, _days);
                counter++;
            }
            return resultDate;
        }


        public DateTime NextWorkingDay(DateTime _date, int _days)
        {
            DateTime resultDate;


            if (_days < 0)
            {
                _date = _date.AddDays(-1);
            }
            else
            {
                _date = _date.AddDays(1);
            }
            resultDate = _date;
            if (_date.DayOfWeek == DayOfWeek.Saturday || _date.DayOfWeek == DayOfWeek.Sunday)
            {
                resultDate = NextWorkingDay(resultDate, _days);
            }
            else
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Select * From MarketHoliday Where Date=@Date and status = 2";
                            cmd.Parameters.AddWithValue("@Date", _date);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (!dr.Read())
                                {
                                    resultDate = _date;
                                    return resultDate;
                                }
                                else
                                {
                                    resultDate = NextWorkingDay(resultDate, _days);
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

            return resultDate;
        }

        public DateTime GetServerDateTime()
        {
            return DateTime.Now;
        }

        public DateTime Get_ValueDateByMKBDTrails(int _mkbdTrailsPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select  ValueDate from mkbdtrails where status not in (3,4)and MKBDTrailsPK = " + _mkbdTrailsPK;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return DateTime.Now;
                            }
                            else
                            {
                                return Convert.ToDateTime(dr["ValueDate"]);
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

        public DateTime Get_ValueDateByDailyRptTrails(int _PK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select ValueDate from DailyRptTrails where Status = 2 and DailyRptTrailsPK = @DailyRptTrailsPK";
                        cmd.Parameters.AddWithValue("@DailyRptTrailsPK", _PK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return DateTime.Now;
                            }
                            else
                            {
                                return Convert.ToDateTime(dr["ValueDate"]);
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

        public string Get_CompanyName()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 Name From Company where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_CompanyID()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 ID From Company where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["ID"]);
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

        public string Get_BankCustodianName(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select top 1 B.Name From BankBranch BC 
                        Left Join Fund F on BC.BankBranchPK = F.BankBranchPK and F.Status = 2
                        Left Join Bank B on BC.BankPK = B.BankPK and B.Status = 2
                        where BC.status = 2 and F.FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_BankBranchAttendance(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select top 1 BC.Attn1 Name From BankBranch BC 
                        Left Join Fund F on BC.BankBranchPK = F.BankBranchPK and F.Status = 2
                        Left Join Bank B on BC.BankPK = B.BankPK and B.Status = 2
                        where BC.status = 2 and F.FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_BankBranchFax(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select top 1 BC.Fax1 Name From BankBranch BC 
                        Left Join Fund F on BC.BankBranchPK = F.BankBranchPK and F.Status = 2
                        Left Join Bank B on BC.BankPK = B.BankPK and B.Status = 2
                        where BC.status = 2 and F.FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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


        public string Get_BankBranchPhone(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select top 1 BC.Phone1 Name From BankBranch BC 
                        Left Join Fund F on BC.BankBranchPK = F.BankBranchPK and F.Status = 2
                        Left Join Bank B on BC.BankPK = B.BankPK and B.Status = 2
                        where BC.status = 2 and F.FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_FundName(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 Name From Fund " +
                        " where status = 2 and FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_DepartmentID(int _departmentPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select ID From Department where departmentPK = @DepartmentPK";
                        cmd.Parameters.AddWithValue("@DepartmentPK", _departmentPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["ID"]);
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

        public string Get_FundType(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 MV.DescOne Type From Fund F Left Join MasterValue MV on MV.Code = F.Type and MV.ID = 'FundType' and MV.Status = 2 " +
                        " where F.status = 2 and F.FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Type"]);
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

        public string Get_CurrencyID(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 C.ID From Currency C Left Join Fund F on C.CurrencyPK = F.CurrencyPK and F.status =2 " +
                        " where C.status = 2 and F.FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["ID"]);
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


        public decimal Get_FundUnitPosition(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " Select isnull(SUM(FCP.UnitAmount),0) TotalUnit From FundClientPosition FCP  " +
                         " where FCP.FundPK = @fundPK and Date = dbo.fworkingday(@Date,-1) ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["TotalUnit"]);
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

        public decimal Get_AccountBalanceForNav(string _fundPK, DateTime _date, int _row)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                              Select case when B.Groups = 1 then sum(isnull(dbo.[FGetGroupAccountFundJournalBalanceByFundPK](@Date,A.FundJournalAccountPK,@FundPK),0))  
                             else sum(isnull(dbo.[FGetAccountFundJournalBalanceByFundPK](@Date,A.FundJournalAccountPK,@FundPK),0)) end Amount  From NAVmappingReport A   
                             left join FundJournalAccount B on  A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status	 =  2   
                             left join Currency C on C.CurrencyPK = B.CurrencyPK and  C.status = 2  
                             where Row = @Row  
                             Group By ROW,Description,B.Groups    
                             order by Row 

                        ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Row", _row);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Amount"]);
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


        public string Get_DirectorName()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 DirectorOne From Company where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["DirectorOne"]);
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

        public string Get_ReferenceSubsRedemp(DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select replace(convert(nvarchar(6), @valuedate, 105), '-', '') + cast(right(year(@ValueDate), 2) as nvarchar) Reference";
                        cmd.Parameters.AddWithValue("@valuedate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Reference"]);
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

        public int Get_IdleTimeMinutes()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select IdleTimeMinutes from SecuritySetup where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt16(dr["IdleTimeMinutes"]);
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
        

        public string User_ChangePassword(string _userID, string _password)
        {
            try
            {
                // Check Panjang Password Dulu
                SecuritySetupReps _secSetupReps = new SecuritySetupReps();
                SecuritySetup _secSetupM = new SecuritySetup();
                _secSetupM = _secSetupReps.SecuritySetup_SelectApprovedOnly();

                string _msg = "";
                int _minPassLength = _secSetupM.MinimumPasswordLength;
                if (_password.Length < _minPassLength)
                {
                    _msg = "Password Length minimum is " + _minPassLength;
                }
                else
                {
                    if (_secSetupM.BitReusedPassword)
                    {
                        UsersReps _usersReps = new UsersReps();
                        Users _usersM = new Users();
                        _usersM = _usersReps.Users_SelectByUserID(_userID);

                        if (_secSetupM.PasswordExpireLevel >= 1 && string.Compare(Cipher.Encrypt(_password), _usersM.Password) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 2 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword1) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 3 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword2) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 4 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword3) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 5 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword4) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 6 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword5) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 7 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword6) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 8 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword7) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 9 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword8) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 10 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword9) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                        if (_secSetupM.PasswordExpireLevel >= 11 && string.Compare(Cipher.Encrypt(_password), _usersM.PrevPassword10) == 0)
                        {
                            _msg = "Password already Used before";
                        }
                    }
                    else
                    {
                        UsersPasswordHistoryReps _uphReps = new UsersPasswordHistoryReps();
                        UsersPasswordHistory _uphM = new UsersPasswordHistory();
                        _uphM = _uphReps.UsersPasswordHistory_SelectByUsersPKandPassword(Get_UsersPK(_userID), Cipher.Encrypt(_password));

                        if (_uphM != null)
                        {
                            _msg = "Password already used before, please input Another Password";
                        }
                    }

                    if (_msg.Length > 0)
                    {
                        return _msg;
                    }

                    // Check Password Mode

                    /* 
                    * 1 = Mengandung Character
                    * 2 = Mengandung Angka 
                    * 3 = Mengandung angka dan Character
                    * 4 = Mengandung angka dan Character dan Simbol
                    * 5 = Mengandung angka dan Character dan Simbol [Complexity]
                    */

                    if (_secSetupM.PasswordCharacterType == 1)
                    {
                        if (!Tools.IsContainLetter(_password))
                        {
                            _msg = "Password Must Contain Character [Aa-Zz]";
                        }
                    }
                    if (_secSetupM.PasswordCharacterType == 2)
                    {
                        if (!Tools.IsContainDigit(_password))
                        {
                            _msg = "Password Must Contain Number [0-9]";
                        }
                    }
                    if (_secSetupM.PasswordCharacterType == 3)
                    {
                        if (!Tools.IsContainLetter(_password))
                        {
                            _msg = "Password Must Contain Alpha  [Aa-Zz] and Numeric  [0-9]";
                        }
                        if (!Tools.IsContainDigit(_password))
                        {
                            _msg = "Password Must Contain Alpha  [Aa-Zz] and Numeric  [0-9]";
                        }
                    }
                    if (_secSetupM.PasswordCharacterType == 4)
                    {
                        if (!Tools.IsContainNonAlphaNumeric(_password))
                        {
                            _msg = "Password Must Contain Alpha  [Aa-Zz] and Numeric  [0-9] and Non AlphaNumeric [!@#$%]";
                        }
                    }
                    if (_secSetupM.PasswordCharacterType == 5)
                    {
                        if (!Tools.IsContainLetter(_password))
                        {
                            _msg = "Password Must Contain Alpha  [Aa-Zz] and Numeric  [0-9] and Non AlphaNumeric [!@#$%]";
                        }
                        if (!Tools.IsContainDigit(_password))
                        {
                            _msg = "Password Must Contain Alpha  [Aa-Zz] and Numeric  [0-9] and Non AlphaNumeric [!@#$%]";
                        }
                        if (!Tools.IsContainNonAlphaNumeric(_password))
                        {
                            _msg = "Password Must Contain Alpha  [Aa-Zz] and Numeric  [0-9] and Non AlphaNumeric [!@#$%]";
                        }
                    }
                }

                if (_msg.Length == 0)
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = " IF Exists( " +
                               " Select * from UsersAccessTrail Where UsersPK = @UsersPK) " +
                               " BEGIN Update UsersAccessTrail Set ChangePasswordTimeLast = GetDate(),ChangePasswordFreq = isnull(ChangePasswordFreq,0) + 1 " +
                               " Where UsersPK = @UsersPK END " +
                               " ELSE BEGIN Insert into UsersAccessTrail (UsersPK,ChangePasswordTimeLast,ChangePasswordFreq) " +
                               " Select @UsersPK,GetDate(),1 END " +
                               " DECLARE @PasswordExpireDays INT " +
                               " SELECT @PasswordExpireDays = [PasswordExpireDays] FROM SecuritySetup WHERE Status = 2 " +
                               " UPDATE [Users] SET [PrevPassword10] = [PrevPassword9] " +
                               " , [PrevPassword9] = [PrevPassword8], [PrevPassword8] = [PrevPassword7], [PrevPassword7] = [PrevPassword6]  " +
                               " , [PrevPassword6] = [PrevPassword5], [PrevPassword5] = [PrevPassword4], [PrevPassword4] = [PrevPassword3] " +
                               " , [PrevPassword3] = [PrevPassword2], [PrevPassword2] = [PrevPassword1], [PrevPassword1] = [Password] " +
                               " , [Password] = @Password, [BitPasswordReset] = 0 " +
                               " , [ExpirePasswordDate] = DATEADD(dd, @PasswordExpireDays, GETDATE()) " +
                               " where UsersPK = @UsersPK and status = 2 " +
                               " Insert into UsersPasswordHistory(Date,Password,UsersPK) " +
                               " Select getDate(),@Password,@UsersPK ";

                            cmd.Parameters.AddWithValue("@UsersPK", Get_UsersPK(_userID));
                            cmd.Parameters.AddWithValue("@Password", Cipher.Encrypt(_password));
                            cmd.ExecuteReader();
                            _msg = "True";
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

        public string Get_UserSessionID(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select SessionID From users where ID = @UserID";
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["SessionID"]);
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

        public string Get_UserPassword(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select password From users where ID = @UserID";
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Cipher.Decrypt(Convert.ToString(dr["password"]));
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

        public string Get_DefaultUserPassword()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select DefaultPassword From SecuritySetup where status = 2 ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["DefaultPassword"]);
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

        public string Get_CompanyAddress()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 Address From Company where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Address"]);
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

        public string Get_CompanyPhone()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 replace(Phone,'_','') Phone From Company where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Phone"]);
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

        public string Get_CompanyFax()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 replace(Fax,'_','') Fax From Company where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Fax"]);
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

        public string Get_DateBatchReport()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 SettlementDate From ClientSubscription where status = 2 and clientsubscriptionPK=1";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["SettlementDate"]);
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

        public string Get_BankCustodianID()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select top 1 ID From BankCustodian where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["ID"]);
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

        public bool CheckChangePassword(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @PasswordExpireDays int " +
                            "Declare @DayChangePassword Datetime " +
                            "Declare @BitPasswordReset Bit " +
                            "Declare @ExpirePasswordDate Datetime " +
                            "Select @BitPasswordReset = BitPasswordReset, @ExpirePasswordDate=ExpirePasswordDate from Users Where UsersPK = @UsersPK " +
                            "IF @ExpirePasswordDate <= GetDate() BEGIN Select 1 MustReset return END " +
                            "IF @BitPasswordReset = 1 " +
                            "BEGIN Select 1 MustReset Return END " +
                            "ELSE BEGIN Select @PasswordExpireDays = PasswordExpireDays From SecuritySetup Where Status = 2 " +
                            "Select @DayChangePassword = DateAdd(day,@PasswordExpireDays,ChangePasswordTimeLast) from UsersAccessTrail Where UsersPK = @UsersPK " +
                            "IF @DayChangePassword <= GetDate() BEGIN Select 1 MustReset END ELSE BEGIN Select 0 MustReset END END";

                        cmd.Parameters.AddWithValue("@UsersPK", Get_UsersPK(_userID));
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return false;
                            }
                            else
                            {
                                return Convert.ToBoolean(dr["MustReset"]);
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

        public bool Get_CheckExistingID(string _ID, string _table)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_table == "SDI")
                        {
                            cmd.CommandText = "select SDIPK from [" + _table + "] where SDIPK = @SDIPK and Status in (1,2)";
                            cmd.Parameters.AddWithValue("@SDIPK", _ID);
                        }
                        else if (_table == "TemplatePosting")
                        {
                            cmd.CommandText = "select Action from " + _table + " where Action = @Action and Status in (1,2)";
                            cmd.Parameters.AddWithValue("@Action", _ID);
                        }
                        else if (_table == "BankInterestSetup")
                        {
                            cmd.CommandText = "select BankBranchPK from " + _table + " where BankBranchPK = @BankBranchPK and Status in (1,2)";
                            cmd.Parameters.AddWithValue("@BankBranchPK", _ID);
                        }
                        //if (_table == "AccountBudget")
                        //{
                        //    cmd.CommandText = "select Version from " + _table + " where Version = @Version and Status in (1,2)";
                        //    cmd.Parameters.AddWithValue("@Version", _ID);
                        //}
                        //if (_table == "MKBDSetup")
                        //{
                        //    cmd.CommandText = "select RLName from " + _table + " where RLName = @RLName and Status in (1,2)";
                        //    cmd.Parameters.AddWithValue("@RLName", _ID);
                        //}
                        else if (_table == "CustodianMKBDMapping")
                        {
                            cmd.CommandText = "select CustodianPK from " + _table + " where CustodianPK = @CustodianPK and Status in (1,2)";
                            cmd.Parameters.AddWithValue("@CustodianPK", _ID);
                        }

                        else
                        {
                            cmd.CommandText = "select ID from [" + _table + "] where ID = @ID and Status in (1,2)";
                            cmd.Parameters.AddWithValue("@ID", _ID);
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




        public bool Get_CheckAlreadyHasApproved(string _table)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "select * from " + _table + " where status = 2 ";
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

        public bool Get_ValidateImport(string _table, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "select * from " + _table + " where status = 2 and ValueDate = @Date ";
                        cmd.Parameters.AddWithValue("@Date", _date);
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

        public class CompareData
        {
            public string FieldName { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }
            public string Similarity { get; set; }
        }

        public CompareData setCompareData(SqlDataReader dr)
        {
            CompareData M_cp = new CompareData();
            M_cp.FieldName = Convert.ToString(dr["FieldName"]);
            M_cp.OldValue = Convert.ToString(dr["OldValue"]);
            M_cp.NewValue = Convert.ToString(dr["NewValue"]);
            M_cp.Similarity = Convert.ToString(dr["Similarity"]);

            return M_cp;
        }

        public List<CompareData> CompareData_Select(string _tableName, string _PK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CompareData> L_cp = new List<CompareData>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CompareDataTableByPK";
                        cmd.Parameters.AddWithValue("@TableName", _tableName);
                        cmd.Parameters.AddWithValue("@PK", _PK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_cp.Add(setCompareData(dr));
                                }
                            }
                            return L_cp;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public decimal Get_UnitAmountByFundPKandFundClientPK(int _fundPK, int _fundClientPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_fundPK != 0)
                        {
                            cmd.CommandText = "  select UnitAmount from FundClientPosition " +
                            " where DATE  = (select MAX(Date) MaxDate from FundClientPosition where FundPK = @FundPK and FundClientPK = @FundClientPK and Date <= @Date ) and FundPK = @FundPK and FundClientPK = @FundClientPK ";
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        else
                        {
                            cmd.CommandText = "  select sum(UnitAmount) UnitAmount from FundClientPosition " +
                            " where DATE  = (select MAX(Date) MaxDate from FundClientPosition where Date <= @Date ) and FundClientPK = @FundClientPK ";
                        }

                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToDecimal(dr["UnitAmount"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_AVGPriceByFundPKandFundClientPK(int _fundPK, int _fundClientPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_fundPK != 0)
                        {

                            cmd.CommandText = "select isnull([dbo].[FGetAVGForFundClientPosition] (@Date,@FundClientPK,@FundPK),0) AvgPrice";
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        else {
                            return 0;
                        }
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToDecimal(dr["AvgPrice"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public decimal Get_CloseNAVByFundPK(int _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_fundPK != 0)
                        {
                            cmd.CommandText = "select isnull([dbo].[FgetLastCloseNav] (@Date,@FundPK),0) CloseNAV";
                            cmd.Parameters.AddWithValue("@Date", _date);
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        else
                        {
                            return 0;
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToDecimal(dr["CloseNAV"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_CashAmountByFundPKandFundClientPK(int _fundPK, int _fundClientPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_fundPK != 0)
                        {
                            cmd.CommandText = "  select CashAmount from FundClientPosition " +
                            " where DATE  = (select MAX(Date) MaxDate from FundClientPosition where FundPK = @FundPK and Date <= @Date ) and FundPK = @FundPK and FundClientPK = @FundClientPK ";
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }
                        else
                        {
                            cmd.CommandText = "  select sum(CashAmount) CashAmount from FundClientPosition " +
                            " where DATE  = (select MAX(Date) MaxDate from FundClientPosition where Date <= @Date ) and FundClientPK = @FundClientPK ";
                        }

                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return Convert.ToDecimal(dr["CashAmount"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_FundUnitAmountByFundPK(int _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "  select sum(UnitAmount) UnitAmount from FundClientPosition " +
                        " where DATE  = (select MAX(Date) MaxDate from FundClientPosition where FundPK = @FundPK and Date <= @Date ) and FundPK = @FundPK ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return dr["UnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["UnitAmount"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_FundCashAmountByFundPK(int _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "  select sum(CashAmount) CashAmount from FundClientPosition " +
                        " where DATE  = (select MAX(Date) MaxDate from FundClientPosition where FundPK = @FundPK and Date <= @Date ) and FundPK = @FundPK ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return dr["CashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CashAmount"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Get_SignatureName(int _signaturePK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select Name From Signature where SignaturePK = @SignaturePK and status = 2 ";
                        cmd.Parameters.AddWithValue("@SignaturePK", _signaturePK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_PositionSignature(int _signaturePK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select Position From Signature where SignaturePK = @SignaturePK and status = 2 ";
                        cmd.Parameters.AddWithValue("@SignaturePK", _signaturePK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Position"]);
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


        public void SelectDeselectDataEquity(bool _toggle, int _investmentPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }

                        cmd.CommandText = @"Update Investment set  " + _selected + @" = @Toggle
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and InstrumentTypePK = 1 and TrxType = @TrxType and InvestmentPK = @InvestmentPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@InvestmentPK", _investmentPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectAllDataEquity(bool _toggle, DateTime _DateFrom, DateTime _DateTo, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle 
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and valueDate Between @DateFrom and @DateTo and InstrumentTypePK = 1 and TrxType = @TrxType ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataDealingEquity(bool _toggle, int _dealingPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and InstrumentTypePK = 1 and TrxType = @TrxType and DealingPK = @DealingPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@DealingPK", _dealingPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataDealingBond(bool _toggle, int _dealingPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
//                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
//                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
//                        and InstrumentTypePK in (2,3,9,13,15) and TrxType = @TrxType and DealingPK = @DealingPK ";

                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                        where InstrumentTypePK in (2,3,9,13,15) and TrxType = @TrxType and DealingPK = @DealingPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@DealingPK", _dealingPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataDealingTimeDeposit(bool _toggle, int _dealingPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
                        if (_trxType == 1)
                        {
                            cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                            where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                            and InstrumentTypePK in (5) and TrxType in (1,3) and DealingPK = @DealingPK ";
                        }
                        else
                        {
                            cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                            where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                            and InstrumentTypePK in (5) and TrxType = @TrxType and DealingPK = @DealingPK ";
                        }


                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@DealingPK", _dealingPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataSettlementEquity(bool _toggle, int _settlementPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }

                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and InstrumentTypePK = 1 and TrxType = @TrxType and SettlementPK = @SettlementPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@SettlementPK", _settlementPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public void SelectDeselectDataSettlementBond(bool _toggle, int _settlementPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
//                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
//                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
//                        and InstrumentTypePK in (2,3,9,13,15) and TrxType = @TrxType and SettlementPK = @SettlementPK ";

                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                        where InstrumentTypePK in (2,3,9,13,15) and TrxType = @TrxType and SettlementPK = @SettlementPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@SettlementPK", _settlementPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataSettlementTimeDeposit(bool _toggle, int _settlementPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
                        if (_trxType == 1)
                        {
                            cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                            where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                            and InstrumentTypePK in (5) and TrxType in (1,3) and SettlementPK = @SettlementPK ";
                        }
                        else
                        {
                            cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                            where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                            and InstrumentTypePK in (5) and TrxType = @TrxType and SettlementPK = @SettlementPK ";
                        }


                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@SettlementPK", _settlementPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public decimal GetDealingNetBuySellEquity(string _date, string _fundPK, string _counterpartPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramCounterpart = "";

                        if (_fundPK != "All")
                        {
                            _paramFund = "and B.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_counterpartPK != "All")
                        {
                            _paramCounterpart = "and C.ID = left(@ParamCounterpartIDFrom,charindex('-',@ParamCounterpartIDFrom) - 1) ";
                        }
                        else
                        {
                            _paramCounterpart = "";
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        Create table #NetBuySell
                        (
	                        Amount numeric(22,4)
                        )
                        Insert into #NetBuySell (Amount) 

                        Select sum(DoneAmount * -1) from Investment A 
                        left join Fund B on A.FundPK  = B.FundPK and B.status  = 2
                        left join Counterpart C on A.CounterpartPK  = C.CounterpartPK and C.status  = 2
                        where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusInvestment  = 2 and Orderstatus in ('M','P') and TrxType  = 1 and InstrumentTypePK = 1 

                        Insert into #NetBuySell (Amount) 

                        Select DoneAmount from Investment A
                        left join Fund B on A.FundPK  = B.FundPK and B.status  = 2
                        left join Counterpart C on A.CounterpartPK  = C.CounterpartPK and C.status  = 2
                        where ValueDate = @Date " + _paramFund + _paramCounterpart + @" and StatusInvestment  = 2 and Orderstatus  in ('M','P') and TrxType  = 2 and InstrumentTypePK = 1

                        Select isnull(SUM(Amount),0) Amount from #NetBuySell
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        if (_fundPK != "All")
                        {
                            cmd.Parameters.AddWithValue("@ParamFundIDFrom", _fundPK);
                        }
                        if (_counterpartPK != "All")
                        {
                            cmd.Parameters.AddWithValue("@ParamCounterpartIDFrom", _counterpartPK);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Amount"]);
                            }
                            return 0;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public void SelectDeselectDataBond(bool _toggle, int _investmentPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }

//                        cmd.CommandText = @"Update Investment set  " + _selected + @" = @Toggle
//                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
//                        and InstrumentTypePK in (2,3,9,13,15) and TrxType = @TrxType and InvestmentPK = @InvestmentPK ";

                        cmd.CommandText = @"Update Investment set  " + _selected + @" = @Toggle
                        where InstrumentTypePK in (2,3,9,13,15) and TrxType = @TrxType and InvestmentPK = @InvestmentPK ";


                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@InvestmentPK", _investmentPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectAllDataBond(bool _toggle, DateTime _DateFrom, DateTime _DateTo, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        string _pk = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                            _pk = "InvestmentPK <> 0";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                            _pk = "DealingPK <> 0";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                            _pk = "SettlementPK <> 0";
                        }
//                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle 
//                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
//                        and valueDate Between @DateFrom and @DateTo and InstrumentTypePK in (2,3,9,13,15) and TrxType = @TrxType ";

                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle 
                        where valueDate Between @DateFrom and @DateTo and InstrumentTypePK in (2,3,9,13,15) and TrxType = @TrxType and " + _pk;

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string GetAlphabet(int _incRowExcel)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"; WITH cte AS
                         (
                         SELECT 1 x, CHAR(65) y
                         UNION ALL
                         SELECT x + 1, CHAR(x + 65)
                        FROM cte
                        WHERE x < 26
                        ), cte2 AS
                        (
                        SELECT CAST(x AS BIGINT) x, CAST(y AS VARCHAR(10)) y
                        FROM cte
                        UNION ALL
                        SELECT(a.x * 26) + b.x, CAST(a.y + b.y AS VARCHAR(10))
                        FROM cte2 a
                        CROSS JOIN cte b
                        WHERE(a.x * 26) + b.x <= @incRowExcel
                        )

                        SELECT Y Alpha
                        FROM cte2 where X = @incRowExcel
                        ORDER BY x ";
                        cmd.Parameters.AddWithValue("@incRowExcel", _incRowExcel);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Alpha"]);
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

        public decimal getDoneAmountByFundAndCounterpart(DateTime _dateFrom, DateTime _dateTo, string _fundPK, string _counterpartPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramCounterpart = "";

                        if (_fundPK != "0")
                        {
                            _paramFund = "And B.FundPK in ( " + _fundPK + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_counterpartPK != "0")
                        {
                            _paramCounterpart = "And C.CounterpartPK in ( " + _counterpartPK + " ) ";
                        }
                        else
                        {
                            _paramCounterpart = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @" select sum(doneamount) DoneAmount from investment A 
                        left join Fund B on A.FundPK  = B.FundPK and B.status  = 2
                        left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status = 2
                        where valuedate between @DateFrom and @DateTo " + _paramFund + _paramCounterpart + @"and StatusSettlement = 2 ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@FundFrom", _paramFund);
                        cmd.Parameters.AddWithValue("@CounterpartFrom", _paramCounterpart);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["DoneAmount"]);
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


        public decimal getCommissionAmountByFundAndCounterpart(DateTime _dateFrom, DateTime _dateTo, string _fundPK, string _counterpartPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _paramCounterpart = "";
                        if (_fundPK != "0")
                        {
                            _paramFund = "And B.FundPK in ( " + _fundPK + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_counterpartPK != "0")
                        {
                            _paramCounterpart = "And C.CounterpartPK in ( " + _counterpartPK + " ) ";
                        }
                        else
                        {
                            _paramCounterpart = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @" select sum(A.CommissionAmount) CommissionAmount from (
                        select case when TrxTypeID = 'BUY' then sum (TotalAmount - DoneAmount) else (sum (TotalAmount - DoneAmount) * -1) End CommissionAmount from investment A 
                        left join Fund B on A.FundPK  = B.FundPK and B.status  = 2
                        left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status = 2
                        where valuedate between @valuedateFrom and @valueDateTo  " + _paramFund + _paramCounterpart + " and StatusSettlement = 2 Group By TrxTypeID) A";

                        //if (_fundID != "All")
                        //{
                        //    cmd.Parameters.AddWithValue("@FundFrom", _fundID);
                        //}
                        //if (_counterpartID != "All")
                        //{
                        //    cmd.Parameters.AddWithValue("@CounterpartFrom", _counterpartID);
                        //}
                        cmd.Parameters.AddWithValue("@valuedateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@valueDateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@FundFrom", _paramFund);
                        cmd.Parameters.AddWithValue("@CounterpartFrom", _paramCounterpart);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["CommissionAmount"]);
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


        public void SelectDeselectDataTimeDeposit(bool _toggle, int _investmentPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        string _trxTypeID = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }

                        if (_trxType == 4)
                        {
                            _trxTypeID = " and TrxType in (1,2,3)";
                        }
                        else if (_trxType == 1)
                        {
                            _trxTypeID = " and TrxType in (1,3)";
                        }
                        else
                        {
                            _trxTypeID = "and TrxType = @TrxType";
                        }
                        cmd.CommandText = @"Update Investment set  " + _selected + @" = @Toggle
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and InstrumentTypePK in (5,10) " + _trxTypeID + " and InvestmentPK = @InvestmentPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        if (_trxType != 4)
                        {
                            cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        }
                        cmd.Parameters.AddWithValue("@InvestmentPK", _investmentPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectAllDataTimeDeposit(bool _toggle, DateTime _DateFrom, DateTime _DateTo, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        string _trxTypeID = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
                        if (_trxType == 4)
                        {
                            _trxTypeID = " and TrxType in (1,2,3)";
                        }
                        else if (_trxType == 1)
                        {
                            _trxTypeID = " and TrxType in (1,3)";
                        }
                        else
                        {
                            _trxTypeID = "and TrxType = @TrxType";
                        }
                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle 
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and valueDate Between @DateFrom and @DateTo and InstrumentTypePK in (5,10) " + _trxTypeID;

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                        if (_trxType != 4)
                        {
                            cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public void SelectDeselectDataReksadana(bool _toggle, int _investmentPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }

                        cmd.CommandText = @"Update Investment set  " + _selected + @" = @Toggle
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and InstrumentTypePK = 6 and TrxType = @TrxType and InvestmentPK = @InvestmentPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@InvestmentPK", _investmentPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectAllDataReksadana(bool _toggle, DateTime _DateFrom, DateTime _DateTo, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle 
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and valueDate Between @DateFrom and @DateTo and InstrumentTypePK = 6 and TrxType = @TrxType ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@DateFrom", _DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _DateTo);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataDealingReksadana(bool _toggle, int _dealingPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }
                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and InstrumentTypePK = 6 and TrxType = @TrxType and DealingPK = @DealingPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@DealingPK", _dealingPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void SelectDeselectDataSettlementReksadana(bool _toggle, int _settlementPK, int _trxType, string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }

                        cmd.CommandText = @"Update Investment set " + _selected + @" = @Toggle
                        where statusInvestment <> 3 and statusDealing <> 3 and statusSettlement <> 3  
                        and InstrumentTypePK = 6 and TrxType = @TrxType and SettlementPK = @SettlementPK ";

                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@TrxType", _trxType);
                        cmd.Parameters.AddWithValue("@SettlementPK", _settlementPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Reset_SelectedInvestment(string _roles)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _selected = "";
                        if (_roles == "Investment")
                        {
                            _selected = "SelectedInvestment";
                        }
                        else if (_roles == "Dealing")
                        {
                            _selected = "SelectedDealing";
                        }
                        else
                        {
                            _selected = "SelectedSettlement";
                        }

                        cmd.CommandText = @"Update Investment set " + _selected + @" = 0 ";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public string IntToLetters(int value)
        {
            string result = string.Empty;
            while (--value >= 0)
            {
                result = (char)('A' + value % 26) + result;
                value /= 26;
            }
            return result;
        }

        public int Get_RowTotalCashAmountSubsRedempByDate(string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                        select Count (C.Name) TotalRow From
                        (
                        Select A.Name,Sum(A.CashAmount) NettSales from (
                        Select B.Name,C.ID FundID, A.CashAmount from ClientSubscription A
                        Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                        left join Fund C on A.FundPK = C.FundPK and C.status = 2
                        where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate = @Date
                        UNION ALL
                        Select B.Name,C.ID FundID, A.CashAmount * -1 from ClientRedemption A
                        Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                        left join Fund C on A.FundPK = C.FundPK and C.status = 2
                        where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate =  @Date
                        )A
                        Group By A.Name)C";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["TotalRow"]);
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

        public int Get_RowTotalCashAmountSubsRedempByDateFromTo(string _dateFrom, string _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                        select Count (C.Name) TotalRow From
                        (
                        Select A.Name,Sum(A.CashAmount) NettSales from (
                        Select B.Name,C.ID FundID, A.CashAmount from ClientSubscription A
                        Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                        left join Fund C on A.FundPK = C.FundPK and C.status = 2
                        where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate between @DateFrom and @DateTo
                        UNION ALL
                        Select B.Name,C.ID FundID, A.CashAmount * -1 from ClientRedemption A
                        Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                        left join Fund C on A.FundPK = C.FundPK and C.status = 2
                        where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate between @DateFrom and @DateTo
                        )A
                        Group By A.Name)C";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["TotalRow"]);
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

        public int Get_RowTotalFundDanaKelolaan(string _dateFrom, string _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                        Create table #dateMonth
                        (
                        DatePosition datetime
                        )

                        Declare @datecounter datetime
                        set @datecounter = @DateFrom
                        While @datecounter <= @DateTo
                        BEGIN
                        insert into #dateMonth
                        select @datecounter
                        set @datecounter = dateadd(month,1,@Datecounter)
                        END


                        DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
                        @query  AS NVARCHAR(MAX)

                        select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(CONVERT(VARCHAR(11),DatePosition,6)) +',0) ' + QUOTENAME(CONVERT(VARCHAR(11),DatePosition,6)) 
                        from #dateMonth
                        Order by DatePosition
                        FOR XML PATH(''), TYPE
                        ).value('.', 'NVARCHAR(MAX)') 
                        ,1,1,'')

                        select @cols = STUFF((SELECT ',' + QUOTENAME(CONVERT(VARCHAR(11),DatePosition,6)) 
                        from #dateMonth
                        Order by DatePosition
                        FOR XML PATH(''), TYPE
                        ).value('.', 'NVARCHAR(MAX)') 
                        ,1,1,'')


                        set @query = 'SELECT count(Name) TotalRow from 
                        (
                        Select B.Name,sum(A.UnitAmount)TotalUnit, CONVERT(VARCHAR(11),A.Date,6) Tgl from FundClientPosition A left join Fund B on A.FundPK = B.FundPK and B.status = 2
                        where Date in (
                        Select DatePosition from #dateMonth
                        )
                        group by B.Name, A.Date 
                        ) x
                        pivot 
                        (
                        AVG(TotalUnit)
                        for Tgl in (' + @cols + ')
                        ) p 
                        '
                        Exec(@query)";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["TotalRow"]);
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

        public string Validate_CheckDescription(DateTime _dateFrom, DateTime _dateTo, string _table)
        {
            try
            {
                if(Tools.ClientCode != "08")
                {
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"
                        Create Table #DescriptionTemp
                        (PK nvarchar(50))
                        
                        Insert Into #DescriptionTemp(PK)
                        select " + _table + @"PK from " + _table + @" where ValueDate between @DateFrom and @DateTo and status = 1 and Selected = 1
                        and (Description is null or Description = '')

                        if exists(select " + _table + @"PK from " + _table + @" where ValueDate between @DateFrom and @DateTo and status = 1 and Selected = 1
                        and (Description is null or Description = ''))
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + PK
                        FROM #DescriptionTemp
                        SELECT 'Reject Cancel, Please Check Description in SysNo : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END    ";

                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    return Convert.ToString(dr["Result"]);

                                }
                                return "";
                            }
                        }
                    }
                }
                else
                {
                    return "";
                }
                
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public string Validate_CheckFundClientPending(DateTime _dateFrom, DateTime _dateTo, string _table)
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
                        Create Table #FundClientTemp
                        (PK nvarchar(50))
                        
                        Insert Into #FundClientTemp(PK)
                        select " + _table + @"PK from " + _table + @" A
                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2) 
                        where ValueDate between @DateFrom and @DateTo and A.status = 1 and A.Selected = 1
                        and B.status = 1

                        if exists(select " + _table + @"PK from " + _table + @" A
                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2) 
                        where ValueDate between @DateFrom and @DateTo and A.status = 1 and A.Selected = 1
                        and B.status = 1)
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + PK
                        FROM #FundClientTemp
                        SELECT 'Approve Cancel, Please Check Fund Client in SysNo : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END    ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public class Notification
        {
            public string Name { get; set; }
            public string Message { get; set; }
            public string PermissionID { get; set; }
            public string LastUpdate { get; set; }
        }

        public Notification setNotification(SqlDataReader dr)
        {
            Notification Mdl = new Notification();
            Mdl.Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]);
            Mdl.Message = dr["Message"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Message"]);
            Mdl.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdate"]);
            return Mdl;
        }

        public void Notification_Add(string _name,string _message, string _permissionID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Insert into Notification(Name,Message,PermissionID)
                            Select @Name,@Message,@PermissionID 
                        ";
                        cmd.Parameters.AddWithValue("@Name", _name);
                        cmd.Parameters.AddWithValue("@Message", _message);
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Notification> Get_Notification(string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Notification> L_data = new List<Notification>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select A.* from notification A
                        left join Permission B on A.PermissionID = B.ID and B.status = 2
                        left Join Users C on C.ID = @UsersID and C.Status = 2
                        left join RolesUsers D on C.UsersPK = D.usersPK and D.status = 2
                        left join RolesPermissionNotification E on D.RolesPK = E.RolesPK and E.status = 2 and E.PermissionPK = B.PermissionPK
                        where E.RolesPermissionNotificationPK is not null
                         and CONVERT(VARCHAR(8), A.Lastupdate, 1) = CONVERT(VARCHAR(8), GETDATE(), 1) order by A.Lastupdate desc";
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_data.Add(setNotification(dr));
                                }
                            }
                            return L_data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool Get_CheckAlreadyHasExist(string _table, string fundpk, string type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (type == "Insert")
                        {
                            cmd.CommandText = "select * from " + _table + " where FundPK = " + fundpk + "  and status = 2";
                        }
                        else if (type == "Update")
                        {
                            cmd.CommandText = "select * from " + _table + " where FundPK =" + fundpk + "  and status = 2";
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

        public string Get_MKBDCode()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select MKBDCode From Company where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["MKBDCode"]);
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

        public decimal Get_FundAUM(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select sum(Amount) AUM from(
                        select sum(dbo.FGetGroupFundJournalAccountBalanceByFundPK(@Date,FundJournalAccountPK,@FundPK)) Amount from FundJournalAccount 
                        where ID = '100.00.00.000' and status = 2
                        union all
                        select sum(dbo.FGetGroupFundJournalAccountBalanceByFundPK(@Date,FundJournalAccountPK,@FundPK)) Amount from FundJournalAccount 
                        where ID = '201.00.00.000' and status = 2
                        ) A ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["AUM"]);
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
        
     

        public DailyTransactions setDailyTransactions(SqlDataReader dr)
        {
            DailyTransactions M_Data = new DailyTransactions();
            M_Data.EndDayTrails = Convert.ToString(dr["EndDayTrails"]);
            M_Data.FundPosition = Convert.ToString(dr["FundPosition"]);
            M_Data.NAV = Convert.ToString(dr["NAV"]);
            M_Data.ClosePrice = Convert.ToString(dr["ClosePrice"]);
            M_Data.ClientSubscription = Convert.ToString(dr["ClientSubscription"]);
            M_Data.ClientRedemption = Convert.ToString(dr["ClientRedemption"]);
            M_Data.ClientSwitching = Convert.ToString(dr["ClientSwitching"]);
            M_Data.OMSTD = Convert.ToString(dr["OMSTD"]);
            M_Data.OMSEquity = Convert.ToString(dr["OMSEquity"]);
            M_Data.OMSBond = Convert.ToString(dr["OMSBond"]);
            M_Data.Dealing = Convert.ToString(dr["Dealing"]);
            M_Data.Settlement = Convert.ToString(dr["Settlement"]);
            M_Data.LastEndDayTrails = Convert.ToString(dr["LastEndDayTrails"]);
            M_Data.LastEndDayFundPosition = Convert.ToString(dr["LastEndDayFundPosition"]);
            M_Data.LastClosePrice = Convert.ToString(dr["LastClosePrice"]);
            M_Data.LastCloseNAV = Convert.ToString(dr["LastCloseNAV"]);
            return M_Data;
        }
        
        public List<DailyTransactions> GetDailyTransactionsByDate(DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DailyTransactions> L_Data = new List<DailyTransactions>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
            	declare @EndDayTrails		nvarchar(50),
			@FundPosition		nvarchar(50),
			@NAV				nvarchar(50),
			@ClosePrice			nvarchar(50),
			@ClientSubscription nvarchar(50),
			@ClientRedemption	nvarchar(50),
			@ClientSwitching	nvarchar(50),
			@OMSTD				nvarchar(50),
			@OMSEquity			nvarchar(50),
			@OMSBond			nvarchar(50),
			@Dealing			nvarchar(50),
			@Settlement			nvarchar(50),
			@LastEndDayTrails	nvarchar(50),
			@LastEndDayFundPosition	nvarchar(50),
			@LastCloseNAV	nvarchar(50),
			@LastClosePrice	nvarchar(50)
			

	Select @LastEndDayTrails = Max(ValueDate) from EndDayTrails where ValueDate <= @Date and status = 2
	Select @LastEndDayFundPosition = Max(ValueDate) from EndDayTrailsFundPortfolio where ValueDate <= @Date and status = 2
	Select @LastCloseNAV = Max(Date) from CloseNAV where Date <= @Date and status = 2
	Select @LastClosePrice = Max(Date) from ClosePrice where Date <= @Date and status = 2

	-- End Day Trails
	select @EndDayTrails = isnull(case when [Status] = 2 then 'Completed' when [Status] = 1 then 'Pending' else 'None' end, 'None') 
	from EndDayTrails 
	where ValueDate = @Date and [Status] in (1,2)
	
	-- Fund Position
	select @FundPosition = isnull(case when [Status] = 2 then 'Completed' when [Status] = 1 then 'Pending' else 'None' end, 'None') 
	from FundPosition 
	where [Date] = @Date and [Status] in (1,2)
	
	-- NAV
	select @NAV = isnull(case when [Status] = 2 then 'Completed' when [Status] = 1 then 'Pending' else 'None' end, 'None') 
	from CloseNAV 
	where [Date] = @Date and [Status] in (1,2)
	
	-- Close Price
	select @ClosePrice = isnull(case when [Status] = 2 then 'Completed' when [Status] = 1 then 'Pending' else 'None' end, 'None') 
	from ClosePrice
	where [Date] = @Date and [Status] in (1,2)
	
	-- Client Subscription
	select @ClientSubscription = isnull(cast(count(*) as nvarchar(20)), '0') 
	from ClientSubscription
	where ValueDate = @Date and [Status] in (1)
	
	-- Client Redemption
	select @ClientRedemption = isnull(cast(count(*) as nvarchar(20)), '0') 
	from ClientRedemption
	where ValueDate = @Date and [Status] in (1)
	
	-- Client Switching
	select @ClientSwitching = isnull(cast(count(*) as nvarchar(20)), '0') 
	from ClientSwitching
	where ValueDate = @Date and [Status] in (1)
	
	-- OMS TD
	select @OMSTD = isnull(cast(count(*) as nvarchar(20)), '0') 
	from Investment
	where ValueDate = @Date and StatusInvestment in (1) 
		and InstrumentTypePK in (5) -- DEP : Deposito Money Market
	
	-- OMS Equity
	select @OMSEquity = isnull(cast(count(*) as nvarchar(20)), '0') 
	from Investment
	where ValueDate = @Date and StatusInvestment in (1) 
		and InstrumentTypePK in (1) -- RG : Equity Reguler
		
	-- OMS Bond
	select @OMSBond = isnull(cast(count(*) as nvarchar(20)), '0') 
	from Investment
	where ValueDate = @Date and StatusInvestment in (1) 
		and InstrumentTypePK in (2,3,9,13,15) -- G-BOND	: Government Bond, C-BOND : Corporate Bond, MTN : Medium Term Notes, SBSN : SBSN, SUKUK	: Sukuk

	-- Dealing
	select @Dealing = isnull(cast(count(*) as nvarchar(20)), '0') 
	from Investment
	where ValueDate = @Date and StatusDealing in (1)
	
	-- Settlement
	select @Settlement = isnull(cast(count(*) as nvarchar(20)), '0') 
	from Investment
	where ValueDate = @Date and StatusSettlement in (1)
	
	-- Result
	select 
		isnull(@EndDayTrails, 'None') as EndDayTrails,
		isnull(@FundPosition, 'None') as FundPosition,
		isnull(@NAV, 'None') as NAV,
		isnull(@ClosePrice, 'None') as ClosePrice,
		isnull(@ClientSubscription, '0') as ClientSubscription,
		isnull(@ClientRedemption, '0') as ClientRedemption,
		isnull(@ClientSwitching, '0') as ClientSwitching,
		isnull(@OMSTD, '0') as OMSTD,
		isnull(@OMSEquity, '0') as OMSEquity,
		isnull(@OMSBond, '0') as OMSBond,
		isnull(@Dealing, '0') as Dealing,
		isnull(@Settlement, '0') as Settlement,
		isnull(@LastEndDayTrails,'None') as LastEndDayTrails,
		isnull(@LastEndDayFundPosition,'None') as LastEndDayFundPosition,
		isnull(@LastCloseNAV,'None') as LastCloseNAV,
		isnull(@LastClosePrice,'None') as LastClosePrice";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Data.Add(setDailyTransactions(dr));
                                }
                            }
                            return L_Data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int Get_PositionProfilRisiko(DateTime _date, int _periodPK, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select 5 as Position
                            ";
                        //cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return 1;
                            }
                            else
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Position"]);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                return 1;
                throw err;
            }
        }

        public class KebijakanInvestasi
        {
            public string Category { get; set; }
            public string Value { get; set; }
        }

        public KebijakanInvestasi Set_KebijakanInvestasi(SqlDataReader dr)
        {
            KebijakanInvestasi Mdl = new KebijakanInvestasi();
            Mdl.Category = dr["Category"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Category"]);
            Mdl.Value = dr["Value"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Value"]);
            return Mdl;
        }

        public List<KebijakanInvestasi> Get_KebijakanInvestasi(DateTime _date, int _periodPK, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<KebijakanInvestasi> L_data = new List<KebijakanInvestasi>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select *
                                from (
                                    select 'Saham' as Category, '80% - 100%' as Value
                                    union all
                                    select 'Surat Utang' as Category, '0% - 20%' as Value
                                    union all
                                    select 'Pasar Uang' as Category, '0% - 20%' as Value
                                ) dt
                            ";
                        //cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_data.Add(Set_KebijakanInvestasi(dr));
                                }
                            }
                            return L_data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public class KomposisiSektor
        {
            public string Category { get; set; }
            public decimal Value { get; set; }
        }

        public KomposisiSektor Set_KomposisiSektor(SqlDataReader dr)
        {
            KomposisiSektor Mdl = new KomposisiSektor();
            Mdl.Category = dr["Category"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Category"]);
            Mdl.Value = dr["Value"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Value"]);
            return Mdl;
        }

        public List<KomposisiSektor> Get_KomposisiSektor(DateTime _date, int _periodPK, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<KomposisiSektor> L_data = new List<KomposisiSektor>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select *
                                from (
                                    select 'JAKFIN' as Category, cast(46.60 as decimal(18,2)) as Value
                                    union all
                                    select 'JAKPROP' as Category, cast(28.94 as decimal(18,2)) as Value
                                    union all
                                    select 'JAKMINE' as Category, cast(12.64 as decimal(18,2)) as Value
                                    union all
                                    select 'JAKCONS' as Category, cast(11.82 as decimal(18,2)) as Value
                                ) dt
                            ";
                        //cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_data.Add(Set_KomposisiSektor(dr));
                                }
                            }
                            return L_data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public class InformasiProduk
        {
            public string TanggalPerdana { get; set; }
            public decimal NilaiAktivaBersih { get; set; }
            public decimal TotalUnitPenyertaan { get; set; }
            public decimal NilaiAktivaBersihAtauUnit { get; set; }
            public string FaktorRisikoUtama { get; set; }
            public string ManfaatInvestasi { get; set; }
            public string ImbalJasaManagerInvestasi { get; set; }
            public string ImbalJasaBankKustodian { get; set; }
            public string BiayaPembelian { get; set; }
            public string BiayaPenjualan { get; set; }
            public string BankKustodian { get; set; }
            public string BankAccount { get; set; }
            public string Fund { get; set; }
            public string NPWP { get; set; }
        }

        public InformasiProduk Set_InformasiProduk(SqlDataReader dr)
        {
            InformasiProduk Mdl = new InformasiProduk(); //String.Format("{0:n}", Convert.ToDecimal(dr["NilaiAktivaBersih"]))
            Mdl.TanggalPerdana = dr["TanggalPerdana"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TanggalPerdana"]);
            Mdl.NilaiAktivaBersih = dr["NilaiAktivaBersih"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["NilaiAktivaBersih"]);
            Mdl.TotalUnitPenyertaan = dr["TotalUnitPenyertaan"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["TotalUnitPenyertaan"]);
            Mdl.NilaiAktivaBersihAtauUnit = dr["NilaiAktivaBersihAtauUnit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["NilaiAktivaBersihAtauUnit"]);
            Mdl.FaktorRisikoUtama = dr["FaktorRisikoUtama"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FaktorRisikoUtama"]);
            Mdl.ManfaatInvestasi = dr["ManfaatInvestasi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ManfaatInvestasi"]);
            Mdl.ImbalJasaManagerInvestasi = dr["ImbalJasaManagerInvestasi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ImbalJasaManagerInvestasi"]);
            Mdl.ImbalJasaBankKustodian = dr["ImbalJasaBankKustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ImbalJasaBankKustodian"]);
            Mdl.BiayaPembelian = dr["BiayaPembelian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["BiayaPembelian"]);
            Mdl.BiayaPenjualan = dr["BiayaPenjualan"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["BiayaPenjualan"]);
            Mdl.BankKustodian = dr["BankKustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["BankKustodian"]);
            Mdl.BankAccount = dr["BankAccount"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["BankAccount"]);
            Mdl.Fund = dr["Fund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Fund"]);
            Mdl.NPWP = dr["NPWP"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NPWP"]);
            return Mdl;
        }

        public List<InformasiProduk> Get_InformasiProduk(DateTime _date, int _periodPK, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InformasiProduk> L_data = new List<InformasiProduk>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select '08/08/2017' as TanggalPerdana, cast(92617794667.81 as decimal(22,2)) as NilaiAktivaBersih, 
	                                cast(92710415.34 as decimal(22,2)) as TotalUnitPenyertaan, cast(999.00 as decimal(22,2)) as NilaiAktivaBersihAtauUnit,
	                                'Risiko ekonomi, politik, dan wanprestasi' as FaktorRisikoUtama, 
	                                'Pengelolaan profesional, pertumbuhan nilai investasi dan diverifikasi investasi' as ManfaatInvestasi,
	                                'Maks 3%' as ImbalJasaManagerInvestasi, 'Maks 0.25%' as ImbalJasaBankKustodian,
	                                'Maks 1%' as BiayaPembelian, 'Maks 2%' as BiayaPenjualan,
	                                'PT Bank Mega Tbk' as BankKustodian, 'PT Bank Mega Tbk. KC Jakarta Tendean' as BankAccount,
	                                'Reksa Dana Aurora Dana Ekuitas' as Fund, '01.074.0011.241060' as NPWP
                            ";
                        //cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_data.Add(Set_InformasiProduk(dr));
                                }
                            }
                            return L_data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public class GrafikKinerja1
        {
            public string Name { get; set; }
            public decimal Value { get; set; }
            public string Date { get; set; }
        }

        public GrafikKinerja1 Set_GrafikKinerja1(SqlDataReader dr)
        {
            GrafikKinerja1 Mdl = new GrafikKinerja1();
            Mdl.Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]);
            Mdl.Value = dr["Value"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Value"]);
            Mdl.Date = dr["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Date"]);
            return Mdl;
        }

        public List<GrafikKinerja1> Get_GrafikKinerja1(DateTime _date, int _periodPK, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GrafikKinerja1> L_data = new List<GrafikKinerja1>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select *
                                from (
                                    select 'ADE' as Name, cast(1006 as decimal(18,2)) as Value, '2017/12/01' as [Date]
                                    union all
                                    select 'ADE' as Name, cast(1005 as decimal(18,2)) as Value, '2017/11/01' as [Date]
                                    union all
                                    select 'ADE' as Name, cast(1004 as decimal(18,2)) as Value, '2017/10/01' as [Date]
                                    union all
                                    select 'ADE' as Name, cast(1003 as decimal(18,2)) as Value, '2017/09/01' as [Date]
                                    union all
                                    select 'ADE' as Name, cast(1002 as decimal(18,2)) as Value, '2017/08/01' as [Date]

                                    union all
                                    select 'IHSG' as Name, cast(1106 as decimal(18,2)) as Value, '2017/12/01' as [Date]
                                    union all
                                    select 'IHSG' as Name, cast(1065 as decimal(18,2)) as Value, '2017/11/01' as [Date]
                                    union all
                                    select 'IHSG' as Name, cast(1044 as decimal(18,2)) as Value, '2017/10/01' as [Date]
                                    union all
                                    select 'IHSG' as Name, cast(1023 as decimal(18,2)) as Value, '2017/09/01' as [Date]
                                    union all
                                    select 'IHSG' as Name, cast(1003 as decimal(18,2)) as Value, '2017/08/01' as [Date]

                                    union all
                                    select 'IRDSH' as Name, cast(1076 as decimal(18,2)) as Value, '2017/12/01' as [Date]
                                    union all
                                    select 'IRDSH' as Name, cast(1035 as decimal(18,2)) as Value, '2017/11/01' as [Date]
                                    union all
                                    select 'IRDSH' as Name, cast(1024 as decimal(18,2)) as Value, '2017/10/01' as [Date]
                                    union all
                                    select 'IRDSH' as Name, cast(1013 as decimal(18,2)) as Value, '2017/09/01' as [Date]
                                    union all
                                    select 'IRDSH' as Name, cast(1003 as decimal(18,2)) as Value, '2017/08/01' as [Date]
                                ) dt
                                order by Name asc, [Date] desc
                            ";
                        //cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_data.Add(Set_GrafikKinerja1(dr));
                                }
                            }
                            return L_data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public class GrafikKinerja2
        {
            public string Name { get; set; }
            public decimal Value { get; set; }
            public string Date { get; set; }
        }

        public GrafikKinerja2 Set_GrafikKinerja2(SqlDataReader dr)
        {
            GrafikKinerja2 Mdl = new GrafikKinerja2();
            Mdl.Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]);
            Mdl.Value = dr["Value"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Value"]);
            Mdl.Date = dr["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Date"]);
            return Mdl;
        }

        public List<GrafikKinerja2> Get_GrafikKinerja2(DateTime _date, int _periodPK, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GrafikKinerja2> L_data = new List<GrafikKinerja2>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select *
                                from (
                                    select 'ADE' as Name, cast(0.75 as decimal(18,2)) as Value, '2017/12/01' as [Date]
                                    union all
                                    select 'ADE' as Name, cast(-0.35 as decimal(18,2)) as Value, '2017/11/01' as [Date]
                                    union all
                                    select 'ADE' as Name, cast(-0.25 as decimal(18,2)) as Value, '2017/10/01' as [Date]
                                    union all
                                    select 'ADE' as Name, cast(-0.10 as decimal(18,2)) as Value, '2017/09/01' as [Date]

                                    union all
                                    select 'IHSG' as Name, cast(6.70 as decimal(18,2)) as Value, '2017/12/01' as [Date]
                                    union all
                                    select 'IHSG' as Name, cast(-0.70 as decimal(18,2)) as Value, '2017/11/01' as [Date]
                                    union all
                                    select 'IHSG' as Name, cast(1.65 as decimal(18,2)) as Value, '2017/10/01' as [Date]
                                    union all
                                    select 'IHSG' as Name, cast(0.85 as decimal(18,2)) as Value, '2017/09/01' as [Date]

                                    union all
                                    select 'IRDSH' as Name, cast(5.35 as decimal(18,2)) as Value, '2017/12/01' as [Date]
                                    union all
                                    select 'IRDSH' as Name, cast(-0.15 as decimal(18,2)) as Value, '2017/11/01' as [Date]
                                    union all
                                    select 'IRDSH' as Name, cast(1.15 as decimal(18,2)) as Value, '2017/10/01' as [Date]
                                    union all
                                    select 'IRDSH' as Name, cast(-1.15 as decimal(18,2)) as Value, '2017/09/01' as [Date]
                                ) dt
                                order by Name asc, [Date] desc
                            ";
                        //cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_data.Add(Set_GrafikKinerja2(dr));
                                }
                            }
                            return L_data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public class TableKinerja1
        {
            public string val_FundID { get; set; }
            public decimal val_1Mo { get; set; }
            public decimal val_3Mo { get; set; }
            public decimal val_6Mo { get; set; }
            public decimal val_YTD { get; set; }
            public decimal val_1Y { get; set; }
            public decimal val_3Y { get; set; }
            public decimal val_SejakPerdana { get; set; }
        }

        public TableKinerja1 Set_TableKinerja1(SqlDataReader dr)
        {
            TableKinerja1 Mdl = new TableKinerja1();
            Mdl.val_FundID = dr["val_FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["val_FundID"]);
            Mdl.val_1Mo = dr["val_1Mo"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["val_1Mo"]);
            Mdl.val_3Mo = dr["val_3Mo"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["val_3Mo"]);
            Mdl.val_6Mo = dr["val_6Mo"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["val_6Mo"]);
            Mdl.val_YTD = dr["val_YTD"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["val_YTD"]);
            Mdl.val_1Y = dr["val_1Y"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["val_1Y"]);
            Mdl.val_3Y = dr["val_3Y"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["val_3Y"]);
            Mdl.val_SejakPerdana = dr["val_SejakPerdana"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["val_SejakPerdana"]);
            return Mdl;
        }

        public List<TableKinerja1> Get_TableKinerja1(DateTime _date, int _periodPK, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TableKinerja1> L_data = new List<TableKinerja1>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select *
                                from (
                                    select 
                                    'ADE' as val_FundID, cast(0.87 as decimal(18,2)) as val_1Mo, 
                                    cast(0.19 as decimal(18,2)) as val_3Mo, cast(0 as decimal(18,2)) as val_6Mo,
                                    cast(0 as decimal(18,2)) as val_YTD, cast(0 as decimal(18,2)) as val_1Y, 
                                    cast(0 as decimal(18,2)) as val_3Y, cast(-0.10 as decimal(18,2)) as val_SejakPerdana

                                    union all

                                    select 
                                    'IRDSH' as val_FundID, cast(5.46 as decimal(18,2)) as val_1Mo, 
                                    cast(6.45 as decimal(18,2)) as val_3Mo, cast(0 as decimal(18,2)) as val_6Mo,
                                    cast(0 as decimal(18,2)) as val_YTD, cast(0 as decimal(18,2)) as val_1Y, 
                                    cast(0 as decimal(18,2)) as val_3Y, cast(6.49 as decimal(18,2)) as val_SejakPerdana

                                    union all

                                    select 
                                    'IHSG' as val_FundID, cast(6.78 as decimal(18,2)) as val_1Mo, 
                                    cast(7.71 as decimal(18,2)) as val_3Mo, cast(0 as decimal(18,2)) as val_6Mo,
                                    cast(0 as decimal(18,2)) as val_YTD, cast(0 as decimal(18,2)) as val_1Y, 
                                    cast(0 as decimal(18,2)) as val_3Y, cast(9.38 as decimal(18,2)) as val_SejakPerdana
                                ) dt
                            ";
                        //cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_data.Add(Set_TableKinerja1(dr));
                                }
                            }
                            return L_data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public class TableKinerja2
        {
            public decimal Beta { get; set; }
            public decimal SharpeRatio { get; set; }
            public decimal AnnStdDeviation { get; set; }
            public decimal InformationRatio { get; set; }
        }

        public TableKinerja2 Set_TableKinerja2(SqlDataReader dr)
        {
            TableKinerja2 Mdl = new TableKinerja2();
            Mdl.Beta = dr["Beta"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Beta"]);
            Mdl.SharpeRatio = dr["SharpeRatio"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SharpeRatio"]);
            Mdl.AnnStdDeviation = dr["AnnStdDeviation"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AnnStdDeviation"]);
            Mdl.InformationRatio = dr["InformationRatio"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["InformationRatio"]);
            return Mdl;
        }

        public List<TableKinerja2> Get_TableKinerja2(DateTime _date, int _periodPK, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TableKinerja2> L_data = new List<TableKinerja2>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select 
                                    cast(0.08 as decimal(18,2)) as Beta, cast(-3.40 as decimal(18,2)) as SharpeRatio, 
                                    cast(1.33 as decimal(18,2)) as AnnStdDeviation, cast(-3.68 as decimal(18,2)) as InformationRatio
                            ";
                        //cmd.Parameters.AddWithValue("@Date", _date);
                        //cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);
                        //cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_data.Add(Set_TableKinerja2(dr));
                                }
                            }
                            return L_data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public decimal Get_CurrencyRate(int _currencyPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select Rate From CurrencyRate 
                        where CurrencyPK = @CurrencyPK and Date = @Date ";

                        cmd.Parameters.AddWithValue("@CurrencyPK", _currencyPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Rate"]);
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

        public string Get_AccountName(string _AccountPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select top 1 Name From Account
                        where status = 2 and AccountPK = @AccountPK";
                        cmd.Parameters.AddWithValue("@AccountPK", _AccountPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_BankBranchName(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" select top 1 D.name Name from fund A
                        left join FundCashRef B on A.fundPK = B.fundpk and B.status = 2
                        left join BankBranch C on B.FundCashRefPK = C.BankBranchPK and C.status = 2
                        left join Bank D on C.BankPK = D.BankPK and D.status = 2
                        where A.status = 2 and A.FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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
        public string Get_FundClientName(string _FundclientPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select Name from fundclient where FundclientPK = @FundclientPK and Status in (1,2) ";
                        cmd.Parameters.AddWithValue("@FundclientPK", _FundclientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_Fund(string _FundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select Name from Fund where FundPK = @FundPK and Status in (1,2) ";
                        cmd.Parameters.AddWithValue("@FundPK", _FundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Name"]);
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

        public string Get_FundSinvestCode(string _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select top 1 SInvestCode from fund where FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["SInvestCode"]);
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


        //mandiri
        public decimal Get_TotalCashAmountSubsRedempByDateFromToGroupBySellingAgentByCurrencyID(string _dateFrom, string _dateTo, string _agentName, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,TotalCashAmount SubsAmount,0 RedAmount,E.ID CurrencyID from ClientSubscription A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and C.Name = @AgentName  and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,0 SubsAmount,CashAmount RedAmount,E.ID CurrencyID from ClientRedemption A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and C.Name = @AgentName  and E.ID = @CurrencyID
                        
                        ) A
                        group by AgentName,CurrencyID
                        order By AgentName";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@AgentName", _agentName);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSubsRedempByDateFromToGroupByFundTypeByCurrencyID(string _dateFrom, string _dateTo, string _agentName, string _fundType, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,TotalCashAmount SubsAmount,0 RedAmount,E.ID CurrencyID from ClientSubscription A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and C.Name = @AgentName and D.DescOne = @FundType and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,0 SubsAmount,CashAmount RedAmount,E.ID CurrencyID from ClientRedemption A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and C.Name = @AgentName and D.DescOne = @FundType and E.ID = @CurrencyID
                        
                        ) A
                        group by AgentName,FundType,CurrencyID
                        order By AgentName";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@AgentName", _agentName);
                        cmd.Parameters.AddWithValue("@FundType", _fundType);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSubsRedempByDateFromToGroupByCurrencyID(string _dateFrom, string _dateTo, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,TotalCashAmount SubsAmount,0 RedAmount,E.ID CurrencyID from ClientSubscription A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0   and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,0 SubsAmount,CashAmount RedAmount,E.ID CurrencyID from ClientRedemption A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0   and E.ID = @CurrencyID
                        
                        ) A
                        group by CurrencyID
                        order By CurrencyID";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSwitchingByDateFromToGroupBySellingAgentByCurrencyID(string _dateFrom, string _dateTo, string _agentName, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                         select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,TotalCashAmountFundFrom SubsAmount,0 RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKTo = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and C.Name = @AgentName  and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,0 SubsAmount,TotalCashAmountFundFrom RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKFrom = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and C.Name = @AgentName  and E.ID = @CurrencyID
                        
                        ) A
                        group by AgentName,CurrencyID
                        order By AgentName";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@AgentName", _agentName);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSwitchingByDateFromToGroupByFundTypeByCurrencyID(string _dateFrom, string _dateTo, string _agentName, string _fundType, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,TotalCashAmountFundFrom SubsAmount,0 RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKTo = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and C.Name = @AgentName and D.DescOne = @FundType and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,0 SubsAmount,TotalCashAmountFundFrom RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKFrom = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and C.Name = @AgentName and D.DescOne = @FundType and E.ID = @CurrencyID
                        
                        ) A
                        group by AgentName,FundType,CurrencyID
                        order By AgentName";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@AgentName", _agentName);
                        cmd.Parameters.AddWithValue("@FundType", _fundType);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSwitchingByDateFromToGroupByCurrencyID(string _dateFrom, string _dateTo, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                         select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,TotalCashAmountFundFrom SubsAmount,0 RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKTo = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0   and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,0 SubsAmount,TotalCashAmountFundFrom RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKFrom = B.FundPK and B.Status = 2
                        left join Agent C on A.AgentPK = C.AgentPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0   and E.ID = @CurrencyID
                        
                        ) A
                        group by CurrencyID
                        order By CurrencyID";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSubsRedempByDateFromToGroupBySellingAgentByCurrencyID2(string _dateFrom, string _dateTo, string _agentName, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name ClientName,D.DescOne FundType,TotalCashAmount SubsAmount,0 RedAmount,E.ID CurrencyID from ClientSubscription A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and B.ID = @FundID  and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name ClientName,D.DescOne FundType,0 SubsAmount,CashAmount RedAmount,E.ID CurrencyID from ClientRedemption A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and B.ID = @FundID  and E.ID = @CurrencyID
                        
                        ) A
                        group by FundID,CurrencyID
                        order By FundID ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@FundID", _agentName);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSubsRedempByDateFromToGroupByFundTypeByCurrencyID2(string _dateFrom, string _dateTo, string _agentName, string _fundType, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,TotalCashAmount SubsAmount,0 RedAmount,E.ID CurrencyID from ClientSubscription A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and B.ID = @FundID and D.DescOne = @FundType and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name AgentName,D.DescOne FundType,0 SubsAmount,CashAmount RedAmount,E.ID CurrencyID from ClientRedemption A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and B.ID = @FundID and D.DescOne = @FundType and E.ID = @CurrencyID
                        
                        ) A
                        group by FundID,FundType,CurrencyID
                        order By FundID ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@FundID", _agentName);
                        cmd.Parameters.AddWithValue("@FundType", _fundType);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSubsRedempByDateFromToGroupByCurrencyID2(string _dateFrom, string _dateTo, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name ClientName,D.DescOne FundType,TotalCashAmount SubsAmount,0 RedAmount,E.ID CurrencyID from ClientSubscription A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join Fundclient C on A.FundclientPK = C.FundclientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType' and D.status = 2
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0   and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name ClientName,D.DescOne FundType,0 SubsAmount,CashAmount RedAmount,E.ID CurrencyID from ClientRedemption A
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                        left join Fundclient C on A.FundclientPK = C.FundclientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType' and D.status = 2
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0   and E.ID = @CurrencyID
                        
                        ) A
                        group by CurrencyID
                        order By CurrencyID ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSwitchingByDateFromToGroupBySellingAgentByCurrencyID2(string _dateFrom, string _dateTo, string _agentName, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name ClientName,D.DescOne FundType,TotalCashAmountFundFrom SubsAmount,0 RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKTo = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and B.ID = @FundID  and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name ClientName,D.DescOne FundType,0 SubsAmount,TotalCashAmountFundFrom RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKFrom = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and B.ID = @FundID  and E.ID = @CurrencyID
                        
                        ) A
                        group by FundID,CurrencyID
                        order By FundID ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@FundID", _agentName);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSwitchingByDateFromToGroupByFundTypeByCurrencyID2(string _dateFrom, string _dateTo, string _agentName, string _fundType, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name ClientName,D.DescOne FundType,TotalCashAmountFundFrom SubsAmount,0 RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKTo = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and B.ID = @FundID and D.DescOne = @FundType and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name ClientName,D.DescOne FundType,0 SubsAmount,TotalCashAmountFundFrom RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKFrom = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType'
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0 and B.ID = @FundID and D.DescOne = @FundType and E.ID = @CurrencyID
                        
                        ) A
                        group by FundID,FundType,CurrencyID
                        order By FundID ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@FundID", _agentName);
                        cmd.Parameters.AddWithValue("@FundType", _fundType);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalCashAmountSwitchingByDateFromToGroupByCurrencyID2(string _dateFrom, string _dateTo, string _currencyID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(SubsAmount - RedAmount) Total from (
                        select B.ID FundID,B.Name FundName,C.Name AClientName,D.DescOne FundType,TotalCashAmountFundFrom SubsAmount,0 RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKTo = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType' and D.status = 2
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0   and E.ID = @CurrencyID
                        union all
                        select B.ID FundID,B.Name FundName,C.Name AClientName,D.DescOne FundType,0 SubsAmount,TotalCashAmountFundFrom RedAmount,E.ID CurrencyID,'Switching' Switching from ClientSwitching A
                        left join Fund B on A.FundPKFrom = B.FundPK and B.Status = 2
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                        left join MasterValue D on B.Type = D.Code and D.status = 2 and D.ID = 'FundType' and D.status = 2
                        left join Currency E on A.CurrencyPK = E.CurrencyPK and E.Status = 2
                        where Valuedate between @DateFrom and @DateTo and A.status = 2 and A.Posted = 1 and Revised = 0   and E.ID = @CurrencyID
                        
                        ) A
                        group by CurrencyID
                        order By CurrencyID ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@CurrencyID", _currencyID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Total"]);
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

        public decimal Get_TotalDanaKelolaan(string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        --Declare @Rate numeric(22,4)

--                        Select @rate = isnull(rate,0)  from CurrencyRate a 
--						left join currency b on a.CurrencyPK = b.CurrencyPK and b.Status in(1,2) where 
						
--                         Date = (
--	                        select max(date) from CurrencyRate where date <= @Date and CurrencyPK = 1
--                        )


--                        set @Rate = isnull(@rate,0)

                        -- TotalDanaKelolaan
                        select sum(A.AUM * isnull(c.Rate,1)) TotalDanaKelolaan From CloseNAV A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
						left join currencyrate c on b.currencypk = c.currencypk and a.Date = c.Date and c.status in (1,2) 
                        where A.Date = @Date and A.status = 2
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["TotalDanaKelolaan"]);
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

        public decimal Get_TotalProduk_NAVHarian(string _date, string _paramFund)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 

                             select count(FundPK) TotalProduk from (    
 
                            Select A.FundPK
                            from CloseNAV A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            Left join MasterValue C on C.Code = B.Type and C.id = 'fundType' and C.status in (1,2)
                            left join MasterValue D on D.Code = B.FundTypeInternal and D.id = 'FundTypeInternal' and D.status in (1,2)
                               
                            where A.status in (1,2) and A.Date = @Date 
                            " + _paramFund + @"
                            UNION ALL



                            select A.FundPK
                             from Fund A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            Left join MasterValue C on C.Code = B.Type and C.id = 'fundType' and C.status in (1,2)
                            left join MasterValue D on D.Code = B.FundTypeInternal and D.id = 'FundTypeInternal' and D.status in (1,2)
                            where A.FundPK not in
                            (
	                            Select FundPK from CLoseNAV where status in (1,2) and Date = @Date
                            ) and (@date < A.MaturityDate  or A.MaturityDate = '01/01/1900') and A.status in (1,2)
                            " + _paramFund + @"
                            )A
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["TotalProduk"]);
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
        public decimal Get_Kurs(string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        Declare @Rate numeric(22,4)

                        Select @rate = isnull(rate,0)  from CurrencyRate where CurrencyPK = 2
                        and Date = (
	                        select max(date) from CurrencyRate where date <= @Date and CurrencyPK = 2 and status = 2
                        ) and status = 2


                        set @Rate = isnull(@rate,0)

                        -- Kurs
                        select distinct @Rate Kurs From CloseNAV A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where A.Date = @Date and A.status = 2
                        --and B.BitNeedRecon = 1 ";

                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Kurs"]);
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

        public decimal Get_ChangeNavPerDay(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                        select sum(NavToday-NavYesterDay)/sum(NavYesterDay) * 100 ChangeNav  from (
                        select Nav NavToday,0 NavYesterDay from CloseNAV 
                        where  FundPK = @FundPK and Date = @Date and status in (1,2)
                        union all
                        select 0,Nav from CloseNAV 
                        where  FundPK = @FundPK and Date = dbo.fworkingday(@Date,-1) and status in (1,2)
                        )A ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return dr["ChangeNav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["ChangeNav"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_LastNavYesterday(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                        select Nav from CloseNAV 
                        where  FundPK = @FundPK and Date = dbo.fworkingday(@Date,-1) and status in (1,2) ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return dr["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Nav"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public decimal Get_NavLastYear(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                        select Nav from CloseNAV 
                        where  FundPK = @FundPK and Date = dbo.FWorkingDay(dateadd(DD, -1, dateadd(YY,datediff(yy,0,@date),0)),-1) and status in (1,2) ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return dr["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Nav"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_TotalPencairanDeposito(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                        select isnull(SUM(Amount),0) Amount from Investment 
                        where fundPK = 1 and valuedate between @DateFrom+1 and @DateTo
                        and StatusSettlement = 2 
                        and TrxType = 2 
                        and InstrumentTypePK = 5";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Amount"]);
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


        public decimal Get_AUM(int _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select AUM from CloseNAV where FundPK = @FundPK and Date = @Date ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["AUM"]);
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


        public decimal Get_UnitYesterday(int _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select isnull(UnitAmount,0) Unit from FundClientPosition where FundPK = @FundPK and Date = CONVERT(varchar(10),(dateadd(dd, -1, @Date)),120)";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Unit"]);
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

        public decimal Get_LastNav(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                        select isnull(Nav,0) Nav from CloseNAV 
                        where  FundPK = @FundPK and Date = @Date and status in (1,2) ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return dr["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Nav"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_TotalAccountBalanceByFundPK(string _fundPK, int _fundJournalAccountPK, DateTime _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                      
Declare @periodPK int
Declare @BeginDate datetime

select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@Date,@FundPK) 

select @PeriodPK = PeriodPK from Period where status = 2 and @Date between dateFrom and DateTo

SELECT   isnull(CASE 
                        WHEN C.type IN ( 1, 4 ) THEN Sum( 
                        B.basedebit - B.basecredit) 
                        ELSE Sum(B.basecredit - B.basedebit) 
                    END,0)  Result
FROM   fundjournal A 
        LEFT JOIN fundjournaldetail B 
            ON A.fundjournalpk = B.fundjournalpk 
                AND B.status = 2 
        LEFT JOIN fundjournalaccount C 
            ON B.fundjournalaccountpk = C.fundjournalaccountpk 
                AND C.status = 2 
WHERE  A.ValueDate  between @BeginDate and @Date
        AND A.posted = 1 
        AND A.reversed = 0 
        AND A.status = 2 
        --AND A.PeriodPK = @PeriodPK
        AND B.fundpk = @FundPK 
        AND @FundJournalAccountPK IN ( 
            C.fundjournalaccountpk, C.parentpk1, 
            C.parentpk2, 
            C.parentpk3, 
            C.parentpk4, C.parentpk5, C.parentpk6, 
            C.parentpk7, 
            C.parentpk8, C.parentpk9 ) 
GROUP  BY C.type 

                    ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Result"]);

                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_OutstandingPaymentByFundPKByDate(string _fundPK, DateTime _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

Declare @FundJournalAccountPK int        
Declare @periodPK int
Declare @BeginDate datetime

select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@Date,@FundPK) 

select @PeriodPK = PeriodPK from Period where status = 2 and @Date between dateFrom and DateTo

DECLARE @A TABLE
(FundJournalAccountPK int )

DECLARE @B TABLE
(Result numeric(18,4) )

insert into @A
select PayablePurchaseEquity from FundAccountingSetup where FundPK = @FundPK and status in (1,2)
insert into @A
select PayablePurRecBond from FundAccountingSetup where FundPK = @FundPK and status in (1,2)

 
DECLARE A CURSOR FOR 
Select * From @A
        	
Open A
Fetch Next From A
Into @FundJournalAccountPK
        
While @@FETCH_STATUS = 0
BEGIN

insert into @B

SELECT   isnull(CASE 
                        WHEN C.type IN ( 1, 4 ) THEN Sum( 
                        B.basedebit - B.basecredit) 
                        ELSE Sum(B.basecredit - B.basedebit) 
                    END,0)  Result
FROM   fundjournal A 
        LEFT JOIN fundjournaldetail B 
            ON A.fundjournalpk = B.fundjournalpk 
                AND B.status = 2 
        LEFT JOIN fundjournalaccount C 
            ON B.fundjournalaccountpk = C.fundjournalaccountpk 
                AND C.status = 2 
WHERE  A.ValueDate  between @BeginDate and @Date
        AND A.posted = 1 
        AND A.reversed = 0 
        AND A.status = 2 
        --AND A.PeriodPK = @PeriodPK
        AND B.fundpk = @FundPK 
        AND @FundJournalAccountPK IN ( 
            C.fundjournalaccountpk, C.parentpk1, 
            C.parentpk2, 
            C.parentpk3, 
            C.parentpk4, C.parentpk5, C.parentpk6, 
            C.parentpk7, 
            C.parentpk8, C.parentpk9 ) 
GROUP  BY C.type 

        
Fetch next From A Into @FundJournalAccountPK
END
Close A
Deallocate A 

select sum(Result) * -1 Result from @B

                    ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();

                                return dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Result"]);

                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_OutstandingReceivableByFundPKByDate(string _fundPK, DateTime _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

Declare @FundJournalAccountPK int        
Declare @periodPK int
Declare @BeginDate datetime

select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@Date,@FundPK) 

select @PeriodPK = PeriodPK from Period where status = 2 and @Date between dateFrom and DateTo

DECLARE @A TABLE
(FundJournalAccountPK int )

DECLARE @B TABLE
(Result numeric(18,4) )

insert into @A
select AccountReceivableSaleEquity from FundAccountingSetup where FundPK = @FundPK and status in (1,2)
insert into @A
select AccountReceivableSaleBond from FundAccountingSetup where FundPK = @FundPK and status in (1,2)

 
DECLARE A CURSOR FOR 
Select * From @A
        	
Open A
Fetch Next From A
Into @FundJournalAccountPK
        
While @@FETCH_STATUS = 0
BEGIN

insert into @B

SELECT   isnull(CASE 
                        WHEN C.type IN ( 1, 4 ) THEN Sum( 
                        B.basedebit - B.basecredit) 
                        ELSE Sum(B.basecredit - B.basedebit) 
                    END,0)  Result
FROM   fundjournal A 
        LEFT JOIN fundjournaldetail B 
            ON A.fundjournalpk = B.fundjournalpk 
                AND B.status = 2 
        LEFT JOIN fundjournalaccount C 
            ON B.fundjournalaccountpk = C.fundjournalaccountpk 
                AND C.status = 2 
WHERE  A.ValueDate  between @BeginDate and @Date
        AND A.posted = 1 
        AND A.reversed = 0 
        AND A.status = 2 
        --AND A.PeriodPK = @PeriodPK
        AND B.fundpk = @FundPK 
        AND @FundJournalAccountPK IN ( 
            C.fundjournalaccountpk, C.parentpk1, 
            C.parentpk2, 
            C.parentpk3, 
            C.parentpk4, C.parentpk5, C.parentpk6, 
            C.parentpk7, 
            C.parentpk8, C.parentpk9 ) 
GROUP  BY C.type 

        
Fetch next From A Into @FundJournalAccountPK
END
Close A
Deallocate A 

select sum(Result) Result from @B

                    ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();

                                return dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Result"]);

                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public decimal Get_LastAUMFromCloseNav(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                        select isnull(AUM,0) AUM from CloseNAV 
                        where  FundPK = @FundPK and Date = @Date and status in (1,2) ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return dr["AUM"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["AUM"]);
                                }
                            }
                            return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public decimal Get_TotalMarketValuePerFundPKByDate(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(MarketValue) MarketValue From FundPosition
                        where Date = @Date and status = 2 and FundPk = @FundPK
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["MarketValue"]);
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


        public decimal Get_TotalCashAmountUntilParamDatePerFundPK(string _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select sum(CashAmount) CashAmount from ClientSubscription 
                        where status = 2 and Posted = 1 and Revised = 0 and ValueDate <= @Date and FundPK = @FundPK
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["CashAmount"]);
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

        public string Get_PrepareByInvestment(DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select isnull(IV.EntryUsersID,'') EntryUsersID   
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
                        left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
                        left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
                        Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3 and IV.SelectedInvestment = 1  
                        order by IV.InvestmentPK";
                        cmd.Parameters.AddWithValue("@ParamListDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["EntryUsersID"]);
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

        public string Get_ApprovalByInvestment(DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select isnull(IV.ApprovedUsersID,'') ApprovedUsersID   
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
                        left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
                        left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
                        Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3 and IV.SelectedInvestment = 1  
                        order by IV.InvestmentPK";
                        cmd.Parameters.AddWithValue("@ParamListDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["ApprovedUsersID"]);
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

        public string Get_ApprovalBySettlement(DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select isnull(IV.ApprovedSettlementID,'') ApprovedSettlementID   
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
                        left join BankBranch J on IV.BankBranchPK = J.BankBranchPK and J.status = 2 
                        left join Counterpart K on IV.CounterpartPK = K.CounterpartPK and K.status = 2       
                        Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment <> 3 and IV.SelectedSettlement = 1  
                        order by IV.InvestmentPK";
                        cmd.Parameters.AddWithValue("@ParamListDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["ApprovedSettlementID"]);
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

        public void Reset_Selected(string _tableName)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Update [" + _tableName + "] set Selected = 0 ";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public bool Validation_CheckCutOffTime(int _fundPK)
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
                        Declare @StringTimeNow nvarchar(8)
                        Declare @CutoffTime decimal(22,0)
                        Declare @DecTimeNow decimal(22,0)

                        SELECT @StringTimeNow = REPLACE(substring(CONVERT(nvarchar(8),@TimeNow,108),0,9),':','')

                        select @CutoffTime = case when CutOffTime = '' then 0  else cast(CutOffTime as decimal(22,0)) end from Fund where FundPK = @FundPK and status in (1,2)

                        select @DecTimeNow = cast(@StringTimeNow as decimal(22,0))

                        IF (@DecTimeNow > @CutoffTime)
                        BEGIN
	                        select 1 Result
                        END
                        ELSE 
                        BEGIN
	                        select 0 Result
                        END    ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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


        public decimal Get_NAVFundFrom(string _fundFrom, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select A.NAV NAV from CloseNAV A
                                            left join fund B on A.fundpk = B.FundPK and B.status = 2
                                             where A.FundPK = @FundFrom and Date = @Date ";

                        cmd.Parameters.AddWithValue("@FundFrom", _fundFrom);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["NAV"]);
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

        public decimal Get_AUMFundFrom(string _fundFrom, DateTime _valuedateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select AUM from CloseNAV A
                                            left join fund B on A.fundpk = B.FundPK and B.status = 2
                                             where A.FundPK = @FundFrom and Date = @Date ";

                        cmd.Parameters.AddWithValue("@FundFrom", _fundFrom);
                        cmd.Parameters.AddWithValue("@Date", _valuedateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["AUM"]);
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

        public class InvestmentValidation
        {
            public string ResultText { get; set; }
            public string Result { get; set; }
        }

        public InvestmentValidation Validation_EntrierApproverByInvesment(string _userID, string _table, string _Type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                                
                        DECLARE @Query NVARCHAR(2000)

                        SET @Query = ''

                        if @Type = 'OMSEquity'
                        SET @Query = 'if ((select count(*) from ' + @table + ' where EntryUsersID = ''' + @UsersID + ''' and (UpdateUsersID  = '''' or UpdateUsersID is null) and StatusInvestment = 1 and SelectedInvestment = 1 and InstrumentTypePK = 1) > 0)
				                        select 0 Result, ''Failed : Approved User ID is same with Entry User ID'' ResultText
			                        else if (( select count(*) from ' + @table + ' where UpdateUsersID = ''' + @UsersID + ''' and (UpdateUsersID  != '''' or UpdateUsersID is not null) and StatusInvestment = 1 and SelectedInvestment = 1 and InstrumentTypePK = 1) > 0)
				                        select 0 Result,''Failed : Approved User ID is same with Update User ID'' ResultText 
			                        else
				                        select 1 Result,'''' ResultText'
						if @Type = 'OMSBond'
						SET @Query = 'if ((select count(*) from ' + @table + ' where EntryUsersID = ''' + @UsersID + ''' and (UpdateUsersID  = '''' or UpdateUsersID is null) and StatusInvestment = 1 and SelectedInvestment = 1 and InstrumentTypePK in (2,3,9,15)) > 0)
				                        select 0 Result, ''Failed : Approved User ID is same with Entry User ID'' ResultText
			                        else if (( select count(*) from ' + @table + ' where UpdateUsersID = ''' + @UsersID + ''' and (UpdateUsersID  != '''' or UpdateUsersID is not null) and StatusInvestment = 1 and SelectedInvestment = 1 and InstrumentTypePK in (2,3,9,15)) > 0)
				                        select 0 Result,''Failed : Approved User ID is same with Update User ID'' ResultText 
			                        else
				                        select 1 Result,'''' ResultText'
						if @Type = 'OMSTimeDeposit'
						SET @Query = 'if ((select count(*) from ' + @table + ' where EntryUsersID = ''' + @UsersID + ''' and (UpdateUsersID  = '''' or UpdateUsersID is null) and StatusInvestment = 1 and SelectedInvestment = 1 and InstrumentTypePK = 5) > 0)
				                        select 0 Result, ''Failed : Approved User ID is same with Entry User ID'' ResultText
			                        else if (( select count(*) from ' + @table + ' where UpdateUsersID = ''' + @UsersID + ''' and (UpdateUsersID  != '''' or UpdateUsersID is not null) and StatusInvestment = 1 and SelectedInvestment = 1 and InstrumentTypePK = 5) > 0)
				                        select 0 Result,''Failed : Approved User ID is same with Update User ID'' ResultText 
			                        else
				                        select 1 Result,'''' ResultText'

                        EXEC(@query)
                                
                         ";

                        cmd.Parameters.AddWithValue("@table", _table);
                        cmd.Parameters.AddWithValue("@Type", _Type);
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            InvestmentValidation InvestValidation = new InvestmentValidation();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InvestValidation.Result = dr["Result"].ToString();
                                    InvestValidation.ResultText = dr["ResultText"].ToString();
                                }
                            }
                            return InvestValidation;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }



        public decimal Get_TotalMarketValuePerFundPKByDateByInstrumentType(DateTime _date, int _fundPK, int _groupType)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        select isnull(sum(MarketValue),0) MarketValue From FundPosition A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status in (1,2)
                        where Date = @Date and A.status = 2 and FundPk = @FundPK and C.GroupType = @GroupType
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@GroupType", _groupType);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return Convert.ToDecimal(dr["MarketValue"]);
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["MarketValue"]);
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

        public decimal Get_TotalPercentAUMPerFundPKByDateByInstrumentType(DateTime _date, int _fundPK, int _groupType)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        SELECT sum(A.MarketValue/ CASE WHEN ISNULL(E.AUM,0) = 0 THEN 1 ELSE E.AUM END) Percentage
                        FROM dbo.FundPosition A
                        LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status in (1,2)
                        LEFT JOIN dbo.CloseNAV E ON A.FundPK = E.FundPK AND E.status IN (1,2) AND E.Date = (
                        SELECT MAX(date) FROM dbo.CloseNAV WHERE status = 2 AND date <= @Date and FundPK = @FundPK
                        )        
                        where A.Date = @Date and A.status = 2 and A.FundPk = @FundPK and C.GroupType = @GroupType
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@GroupType", _groupType);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return Convert.ToDecimal(dr["Percentage"]);
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Percentage"]);
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

        public decimal Get_TotalPercentOfNAV(DateTime _date, int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                       
                        SELECT sum(A.MarketValue/ CASE WHEN ISNULL(E.AUM,0) = 0 THEN 1 ELSE E.AUM END) * 100 Percentage
                        FROM dbo.FundPosition A
                        LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.status in (1,2)
                        LEFT JOIN dbo.CloseNAV E ON A.FundPK = E.FundPK AND E.status IN (1,2) AND E.Date = (
                        SELECT MAX(date) FROM dbo.CloseNAV WHERE status = 2 AND date < @Date and FundPK = @FundPK
                        )        
                        where A.Date = @Date and A.status = 2 and A.FundPk = @FundPK 
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        //cmd.Parameters.AddWithValue("@GroupType", _groupType);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                return Convert.ToDecimal(dr["Percentage"]);
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["Percentage"]);
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

        public int Get_DetailNewEquityDirectInvestmentPK(int _PK, string _table, string _param)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        /*query dibawa sama ama bgini 
                         *  declare @sql nvarchar(300)
                            declare @table nvarchar(50)
                            set @table = 'users'
                            set @sql = 'select max(historypk)+1 newHistoryPK from '+ @table +' where '+@table +'PK = 1'
                            exec(@sql) 
                         */

                        cmd.CommandText = "select isnull(max(EquityDirectInvestmentPK),0) + 1 EquityDirectInvestmentPK from [" + _table + "] where " + _param + "= @PK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["EquityDirectInvestmentPK"]);

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

        public int Get_DetailNewDividenDirectInvestmentPK(int _PK, string _table, string _param)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        /*query dibawa sama ama bgini 
                         *  declare @sql nvarchar(300)
                            declare @table nvarchar(50)
                            set @table = 'users'
                            set @sql = 'select max(historypk)+1 newHistoryPK from '+ @table +' where '+@table +'PK = 1'
                            exec(@sql) 
                         */

                        cmd.CommandText = "select isnull(max(DividenDirectInvestmentPK),0) + 1 DividenDirectInvestmentPK from [" + _table + "] where " + _param + "= @PK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["DividenDirectInvestmentPK"]);

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

        public int Get_DetailNewAdvisoryFeeClientPK(int _PK, string _table, string _param)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        /*query dibawa sama ama bgini 
                         *  declare @sql nvarchar(300)
                            declare @table nvarchar(50)
                            set @table = 'users'
                            set @sql = 'select max(historypk)+1 newHistoryPK from '+ @table +' where '+@table +'PK = 1'
                            exec(@sql) 
                         */

                        cmd.CommandText = "select isnull(max(AdvisoryFeeClientPK),0) + 1 AdvisoryFeeClientPK from [" + _table + "] where " + _param + "= @PK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["AdvisoryFeeClientPK"]);

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

        public int Get_DetailNewAdvisoryFeeTOPPK(int _PK, string _table, string _param)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        /*query dibawa sama ama bgini 
                         *  declare @sql nvarchar(300)
                            declare @table nvarchar(50)
                            set @table = 'users'
                            set @sql = 'select max(historypk)+1 newHistoryPK from '+ @table +' where '+@table +'PK = 1'
                            exec(@sql) 
                         */

                        cmd.CommandText = "select isnull(max(AdvisoryFeeTOPPK),0) + 1 AdvisoryFeeTOPPK from [" + _table + "] where " + _param + "= @PK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["AdvisoryFeeTOPPK"]);

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

        public int Get_DetailNewAdvisoryFeeExpensePK(int _PK, string _table, string _param)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        /*query dibawa sama ama bgini 
                         *  declare @sql nvarchar(300)
                            declare @table nvarchar(50)
                            set @table = 'users'
                            set @sql = 'select max(historypk)+1 newHistoryPK from '+ @table +' where '+@table +'PK = 1'
                            exec(@sql) 
                         */

                        cmd.CommandText = "select isnull(max(AdvisoryFeeExpensePK),0) + 1 AdvisoryFeeExpensePK from [" + _table + "] where " + _param + "= @PK";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["AdvisoryFeeExpensePK"]);

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

        public string Get_PeriodByRevenue(int _PeriodPk, int _different)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select ID - @Different Period from period where periodpk = (select max(ReportPeriodPK) from Revenue where ReportPeriodPK = @PeriodPk and status = 2) and status = 2";
                        cmd.Parameters.AddWithValue("@PeriodPK", _PeriodPk);
                        cmd.Parameters.AddWithValue("@Different", _different);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Period"]);
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

        public string Get_BeginningForBudgetReport(int _PeriodPk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select distinct 'YTD ' + SUBSTRING('JAN FEB MAR APR MAY JUN JUL AUG SEP OCT NOV DEC ', (month(date) * 4) - 3, 3) + ' ' + B.ID Result from AumForBudgetBegBalance A
                                            left join Period B on A.PeriodPK = B.PeriodPK and B.Status = 2
                                            where A.ReportPeriodPK = @PeriodPK";
                        cmd.Parameters.AddWithValue("@PeriodPK", _PeriodPk);
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

        public string GetEndOfMonth(DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select convert(varchar, EOMONTH(@Date), 110) Date";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Date"]);
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

        public int Get_PKByID(string _id, string _object)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select " + _object + "PK  PK From " + _object + " Where ID = @ID and status in (1,2)";
                        cmd.Parameters.AddWithValue("@ID", _id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["PK"]);
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

        public string Get_PeriodID(int _PeriodPk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select ID Period from period where periodpk = @PeriodPk and status = 2";
                        cmd.Parameters.AddWithValue("@PeriodPK", _PeriodPk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["Period"]);
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