using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{
    public class DukcapilData
    {
        public int DukcapilDataPK { get; set; }
        public string NIK { get; set; }
        public string NAMA_LGKP { get; set; }
        public string AGAMA { get; set; }
        public string KAB_NAME { get; set; }
        public string NO_RW { get; set; }
        public string KEC_NAME { get; set; }
        public string JENIS_PKRJN { get; set; }
        public string NO_RT { get; set; }
        public string NO_KEL { get; set; }
        public string ALAMAT { get; set; }
        public string NO_KEC { get; set; }
        public string TMPT_LHR { get; set; }
        public string STATUS_KAWIN { get; set; }
        public string NO_PROP { get; set; }
        public string PROP_NAME { get; set; }
        public string NO_KAB { get; set; }
        public string KEL_NAME { get; set; }
        public string JENIS_KLMIN { get; set; }
        public string TGL_LHR { get; set; }

        public string FCNIK { get; set; }
        public string FCNAMA_LGKP { get; set; }
        public string FCAGAMA { get; set; }
        public string FCJENIS_PKRJN { get; set; }
        public string FCJENIS_KLMIN { get; set; }
        public string FCTGL_LHR { get; set; }
        public string FCTMPT_LHR { get; set; }
        public string FCSTATUS_KAWIN { get; set; }
        public string FCALAMAT { get; set; }
    }

    public class DukcapilUpdateFundClient
    {
        public int FundClientPK { get; set; }
        public string NIK { get; set; }
        public string NAMA_LGKP { get; set; }
        public string TGL_LHR { get; set; }
        public string JENIS_KLMIN { get; set; }

    }
}