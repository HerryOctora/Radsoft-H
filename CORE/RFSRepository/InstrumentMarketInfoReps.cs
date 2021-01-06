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

namespace RFSRepository
{
    public class InstrumentMarketInfoReps
    {
        Host _host = new Host();

        public void InstrumentMarketInfo_ReNewData(List<InstrumentMarketInfo> _listInstrumentMarketInfo)
        {
             DateTime _dateTimeNow = DateTime.Now;
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    foreach (var _obj in _listInstrumentMarketInfo)
                    {
                        InstrumentMarketInfo _instrumentMarketInfo = new InstrumentMarketInfo();
                        _instrumentMarketInfo.Date = _obj.Date;
                        _instrumentMarketInfo.InstrumentID = _obj.InstrumentID;
                        _instrumentMarketInfo.Price = _obj.Price;

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"
                            Declare @InstrumentPK int
                            Select @InstrumentPK = InstrumentPK from Instrument where ID = left(@InstrumentID,len(@InstrumentID)-3) and status = 2
                            if Exists(Select * from InstrumentMarketInfo where InstrumentPK = @InstrumentPK and Date = @Date)
                            BEGIN
	                            Update InstrumentMarketInfo Set Price = @Price Where Date = @Date and InstrumentPK = @InstrumentPK
                            END
                            ELSE
                            BEGIN
	                            Insert into InstrumentMarketInfo(Date,InstrumentPK,Price,LastUpdate)
	                            Select @Date,@InstrumentPK,@Price,@LastUpdate 	
                            END
                            ";
                            cmd.Parameters.AddWithValue("@Date", _instrumentMarketInfo.Date);
                            cmd.Parameters.AddWithValue("@InstrumentID", _instrumentMarketInfo.InstrumentID);
                            cmd.Parameters.AddWithValue("@Price", _instrumentMarketInfo.Price);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
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