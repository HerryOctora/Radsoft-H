using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;

namespace RFSUtility
{
    public class SAPConnectionConfig : IDestinationConfiguration
    {
        public bool ChangeEventsSupported()
        {

            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;
        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters param = new RfcConfigParameters();
            if (destinationName.Equals("11_QA"))
            {
                //QA
                param.Add(RfcConfigParameters.AppServerHost, "/H/taspenlife.net/H/172.16.14.12");
                param.Add(RfcConfigParameters.SystemNumber, "10");
                param.Add(RfcConfigParameters.SystemID, "QAS");
                param.Add(RfcConfigParameters.User, "konsultan");
                param.Add(RfcConfigParameters.Password, "TL2017");
                param.Add(RfcConfigParameters.Client, "320");
                param.Add(RfcConfigParameters.Language, "EN");
                param.Add(RfcConfigParameters.PoolSize, "100");

            }
            else if (destinationName.Equals("11_PROD"))
            {
                //PROD
                param.Add(RfcConfigParameters.AppServerHost, "/H/taspenlife.net/H/172.16.14.11");
                param.Add(RfcConfigParameters.SystemNumber, "20");
                param.Add(RfcConfigParameters.SystemID, "PRD");
                param.Add(RfcConfigParameters.User, "ajt_super");
                param.Add(RfcConfigParameters.Password, "proajt");
                param.Add(RfcConfigParameters.Client, "320");
                param.Add(RfcConfigParameters.Language, "EN");
                param.Add(RfcConfigParameters.PoolSize, "100");
            }
            else if (destinationName.Equals("11_DEV"))
            {
                //DEV
                param.Add(RfcConfigParameters.AppServerHost, "/H/taspenlife.net/H/172.16.14.12");
                param.Add(RfcConfigParameters.SystemNumber, "00");
                param.Add(RfcConfigParameters.SystemID, "DEV");
                param.Add(RfcConfigParameters.User, "ajt_super");
                param.Add(RfcConfigParameters.Password, "devajt");
                param.Add(RfcConfigParameters.Client, "320");
                param.Add(RfcConfigParameters.Language, "EN");
                param.Add(RfcConfigParameters.PoolSize, "100");
            }
            return param;
        }

    }
}