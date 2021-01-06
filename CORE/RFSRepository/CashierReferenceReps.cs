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
    public class CashierReferenceReps
    {
        Host _host = new Host();

        public string CashierReference_GenerateNewReference(string _type, int _periodPK, DateTime _valueDate)
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

                     Declare @LastNo int   
                     Declare @Reference nvarchar(20) 

                     if exists(Select Top 1 * from cashierReference where type =  @type And PeriodPK = @PeriodPK    
        
                     and substring(right(reference,4),1,2) = month(@ValueDate))   

					BEGIN       
    
						 Select @LastNo = max(No) +  1 From CashierReference where type = @type And PeriodPK = @periodPK and   
        
						 substring(right(reference,4),1,2) = month(@ValueDate)       
        
						 Set @Reference =  Cast(@LastNo as nvarchar(10)) + '/'  + Case when @type = 'CP' then 'OUT' When @type = 'AR' then 'AR' when @type = 'AP' then 'AP'  
        
						 when @type = 'ADJ' then 'ADJ' when @Type = 'INV' then 'INV' when @type = 'GJ' then 'GJ'  else 'IN' END 
        
						 + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')       
    
						 Update CashierReference Set Reference = @Reference, No = @LastNo where type = @type And PeriodPK = @periodPK    
        
						 and substring(right(reference,4),1,2) = month(@ValueDate)    
    
                    END    
    
                    ELSE BEGIN       
    
						 Set @Reference = '1/' +  Case when @type = 'CP' then 'OUT' When @type = 'AR' then 'AR' when @type = 'AP' then 'AP'  
        
						 when @type = 'ADJ' then 'ADJ' when @Type = 'INV' then 'INV' when @type = 'GJ' then 'GJ'  else 'IN' END + '/' +  REPLACE(RIGHT(CONVERT(VARCHAR(8), @ValueDate, 3), 5) ,'/','')    
    
						  Insert Into CashierReference(CashierReferencePK,PeriodPK,Type,Reference,No)    
        
						  Select isnull(Max(CashierReferencePK),0) +  1,@periodPK, @type,@Reference,1 from CashierReference   
    
                     END       
    
                     Select isnull(@Reference,'')   LastReference

";


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