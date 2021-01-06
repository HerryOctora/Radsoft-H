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
    public class DukcapilDataReps
    {
        Host _host = new Host();

        //2
        private DukcapilData setDukcapilData(SqlDataReader dr)
        {
            DukcapilData M_DukcapilData = new DukcapilData();
            M_DukcapilData.DukcapilDataPK = Convert.ToInt32(dr["DukcapilDataPK"]);
            M_DukcapilData.NIK = Convert.ToString(dr["NIK"]);
            M_DukcapilData.NAMA_LGKP = Convert.ToString(dr["NAMA_LGKP"]);
            M_DukcapilData.AGAMA = Convert.ToString(dr["AGAMA"]);
            M_DukcapilData.KAB_NAME = Convert.ToString(dr["KAB_NAME"]);
            M_DukcapilData.NO_RW = Convert.ToString(dr["NO_RW"]);
            M_DukcapilData.KEC_NAME = Convert.ToString(dr["KEC_NAME"]);
            M_DukcapilData.JENIS_PKRJN = Convert.ToString(dr["JENIS_PKRJN"]);
            M_DukcapilData.NO_RT = Convert.ToString(dr["NO_RT"]);
            M_DukcapilData.NO_KEL = Convert.ToString(dr["NO_KEL"]);
            M_DukcapilData.ALAMAT = Convert.ToString(dr["ALAMAT"]);
            M_DukcapilData.NO_KEC = Convert.ToString(dr["NO_KEC"]);
            M_DukcapilData.TMPT_LHR = Convert.ToString(dr["TMPT_LHR"]);
            M_DukcapilData.STATUS_KAWIN = Convert.ToString(dr["STATUS_KAWIN"]);
            M_DukcapilData.NO_PROP = Convert.ToString(dr["NO_PROP"]);
            M_DukcapilData.PROP_NAME = Convert.ToString(dr["PROP_NAME"]);
            M_DukcapilData.NO_KAB = Convert.ToString(dr["NO_KAB"]);
            M_DukcapilData.KEL_NAME = Convert.ToString(dr["KEL_NAME"]);
            M_DukcapilData.JENIS_KLMIN = Convert.ToString(dr["JENIS_KLMIN"]);
            M_DukcapilData.TGL_LHR = Convert.ToString(dr["TGL_LHR"]);
            M_DukcapilData.FCNIK = Convert.ToString(dr["FCNIK"]);
            M_DukcapilData.FCNAMA_LGKP = Convert.ToString(dr["Name"]);
            M_DukcapilData.FCAGAMA = Convert.ToString(dr["FCAgama"]);
            M_DukcapilData.FCJENIS_PKRJN = Convert.ToString(dr["FCPekerjaan"]);
            M_DukcapilData.FCALAMAT = Convert.ToString(dr["FCAlamat"]);
            M_DukcapilData.FCTMPT_LHR = Convert.ToString(dr["FCTempatLahir"]);
            M_DukcapilData.FCSTATUS_KAWIN = Convert.ToString(dr["FCKawin"]);
            M_DukcapilData.FCJENIS_KLMIN = Convert.ToString(dr["FCJenisKelamin"]);
            M_DukcapilData.FCTGL_LHR = Convert.ToString(dr["FcTanggalLahir"]);
            return M_DukcapilData;
        }

        public List<DukcapilData> DukcapilData_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<DukcapilData> L_DukcapilData = new List<DukcapilData>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Select  NoIdentitasInd1 FCNIK,B.Name,c.DescOne FCAgama, e.DescOne FCPekerjaan, d.DescOne FCJenisKelamin,
                        B.AlamatInd1 FcAlamat,B.TanggalLahir FcTanggalLahir,TempatLahir FCTempatLahir,F.DescOne FCKawin,* from DukCapilData a 
                        left join fundclient B on a.NIK = B.NoIdentitasInd1 and B.Status in(1,2)
                        left join MasterValue C on B.Agama = C.Code and C.ID = 'Religion'
                        left join MasterValue D on B.JenisKelamin = D.Code and D.ID = 'Sex'
                        left join MasterValue E on B.Pekerjaan = E.Code and E.ID = 'Occupation'
                        left join MasterValue F on B.StatusPerkawinan = F.Code and F.ID = 'MaritalStatus'
                          order by  DukcapilDataPK";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_DukcapilData.Add(setDukcapilData(dr));
                                }
                            }
                            return L_DukcapilData;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool CheckData(string _nik)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        if Exists(Select * From DukcapilData where NIK = @NIK)
                        BEGIN
                            select 1 Result
                        END
                        ELSE
                        BEGIN
                             select 0 Result
                        END
                        ";
                        cmd.Parameters.AddWithValue("@NIK", _nik);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);
                            }
                        }
                        return false;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


   

        public void DukcapilUpdateFundClient(DukcapilUpdateFundClient _dck)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
Declare @tmpJenisKelamin nvarchar(200)
Declare @tmpTanggalLahir nvarchar(200)
Declare @tmpName nvarchar(200)

select @tmpJenisKelamin = case when JenisKelamin = 1 then 'Laki-Laki' else 'Perempuan' end  
,@tmpTanggalLahir = TanggalLahir, @tmpName = isnull(NamaDepanInd,'') + ' ' + isnull(NamaTengahInd,'') + ' ' + isnull(NamaBelakangInd,'')
from fundclient where fundclientPK = @FundClientPK and status = 1

Insert into [DukcapilDataHistoryFromFundClient] (FundClientPK,NIK,NAMA_LGKP,TGL_LHR,JENIS_KLMIN)
Select @FundClientPK,@NIK, @tmpName, @tmpTanggalLahir, @tmpJenisKelamin

Insert into DukcapilData(NIK,NAMA_LGKP,TGL_LHR,JENIS_KLMIN)
select @NIK,@Name,@TanggalLahir,@JenisKelamin

                            Update FundClient set TanggalLahir = @TanggalLahir, JenisKelamin = 
                            case when @JenisKelamin = 'Laki-Laki' then 1 else 2 end , NoIdentitasInd1 = @NIK
                            ,Name = @Name,NamaDepanInd = @Name1,NamaTengahInd = @Name2, NamaBelakangInd = @Name3
                            where fundclientPK = @FundClientPK and status = 1


                        ";
                        cmd.Parameters.AddWithValue("@TanggalLahir", _dck.TGL_LHR);
                        cmd.Parameters.AddWithValue("@JenisKelamin", _dck.JENIS_KLMIN);
                        cmd.Parameters.AddWithValue("@FundClientPK", _dck.FundClientPK);
                        cmd.Parameters.AddWithValue("@NIK", _dck.NIK);
                        cmd.Parameters.AddWithValue("@Name", _dck.NAMA_LGKP);
                        cmd.Parameters.AddWithValue("@Name1", Tools.SplitName(_dck.NAMA_LGKP).Item1);
                        cmd.Parameters.AddWithValue("@Name2", Tools.SplitName(_dck.NAMA_LGKP).Item2);
                        cmd.Parameters.AddWithValue("@Name3", Tools.SplitName(_dck.NAMA_LGKP).Item3);
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