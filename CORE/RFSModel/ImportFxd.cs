using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class ImportFxd
    {
            public string ID { get; set; }
            public decimal SystemBalance { get; set; }
            public decimal Diff { get; set; }
            public decimal FxdBalance { get; set; }
            public Int32 Fxd11AccountPK { get; set; }
            public string Name { get; set; }
            public decimal CurrentBaseBalance { get; set; }



    }
}