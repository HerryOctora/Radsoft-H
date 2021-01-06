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
    public class FundJournalReferenceReps
    {
        Host _host = new Host();

        public string FundJournalReference_GenerateNewReference(string _type, int _periodPK, DateTime _valueDate)
        {

           try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText =
                        " Declare @LastNo int  " +
                        " Declare @Reference nvarchar(20)" +
                        " \n " +   
                        " if exists(Select Top 1 * from FundJournalReference where Type = @type And PeriodPK = @PeriodPK   " +
                         " \n " + 
                        " and substring(right(reference,4),1,2) = month(@ValueDate)  )      " +
                        " \n " + 
                        " BEGIN      " +
                        " \n " + 
                        "  Select @LastNo = max(No) + 1 From FundJournalReference where Type = @type And PeriodPK = @periodPK and  " +
                         " \n " + 
                        "  substring(right(reference,4),1,2) = month(@ValueDate)      " +
                         " \n " + 
                        " Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/' +  Case when @type = 'CP' then 'OUT' else   " +
                         " \n " + 
                        " Case When @type = 'AR' then 'AR' else Case when @type = 'AP' then 'AP' else   " +
                         " \n " + 
                        " case when @type = 'ADJ' then 'ADJ' Else Case when @Type = 'INV' then 'INV' else 'IN' END END END END END  " +
                         " \n " + 
                        " + '/' + REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')      " +
                        " \n " + 
                        " Update FundJournalReference Set Reference = @Reference, No = @LastNo where Type = @type And PeriodPK = @periodPK   " +
                         " \n " + 
                        " and substring(right(reference,4),1,2) = month(@ValueDate)   " +
                        " \n " + 
                        "END   " +
                        " \n " + 
                        "ELSE BEGIN      " +
                        " \n " + 
                        " Set @Reference = '1/'  +  Case when @type = 'CP' then 'OUT' else   " +
                         " \n " + 
                        "  Case When @type = 'AR' then 'AR' else Case when @type = 'AP' then 'AP' else   " +
                         " \n " +
                        "  case when @type = 'ADJ' then 'ADJ' Else Case when @Type = 'INV' then 'INV' else 'IN' END END END END END + '/' + REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')   " +
                        " \n " + 
                        "  Insert Into FundJournalReference(FundJournalReferencePK,PeriodPK,Type,Reference,No)   " +
                         " \n " + 
                        "  Select isnull(Max(FundJournalReferencePK),0) +  1,@periodPK,@type,@Reference,1 from FundJournalReference  " +
                        " \n " + 
                        " END      " +
                        " \n " + 
                        " Select isnull(@Reference,'')   LastReference " ;


                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.Parameters.AddWithValue("@periodPK", _periodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["LastReference"]);
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