using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;



namespace RFSRepository
{
    public class CorporateGovAMReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CorporateGovAM] " +
                            "([CorporateGovAMPK],[HistoryPK],[Status],[Date]," +
                            "[Row1],[Row2],[Row3],[Row4],[Row5],[Row6],[Row7],[Row8],[Row9],[Row10]," +
                            "[Row11],[Row12],[Row13],[Row14],[Row15],[Row16],[Row17],[Row18],[Row19],[Row20]," +
                            "[Row21],[Row22],[Row23],[Row24],[Row25],[Row26],[Row27],[Row28],[Row29],[Row30]," +
                            "[Row31],[Row32],[Row33],[Row34],[Row35],[Row36],[Row37],[Row38],[Row39],[Row40]," +
                            "[Row41],[Row42],[Row43],[Row44],[Row45],[Row46],[Row47],[Row48],[Row49],[Row50]," +
                            "[Row51],[Row52],[Row53],[Row54],[Row55],[Row56],[Row57],[Row58],[Row59],[Row60]," +
                            "[Row61],[Row62],[Row63],[Row64],[Row65],[Row66],[Row67],[Row68],[Row69],[Row70]," +
                            "[Row71],[Row72],[Row73],[Row74],[Row75],[Row76],[Row77],[Row78],[Row79],[Row80]," +
                            "[Row81],[Row82],[Row83],[Row84],[Row85],[Row86],[Row87],[Row88],[Row89],[Row90]," +
                            "[Row91],[Row92],[Row93],[Row94],[Row95],[Row96],[Row97],[Row98],[Row99],[Row100]," +

                            "[Row101],[Row102],[Row103],[Row104],[Row105],[Row106],[Row107],[Row108],[Row109],[Row110]," +
                            "[Row111],[Row112],[Row113],[Row114],[Row115],[Row116],[Row117],[Row118],[Row119],[Row120]," +
                            "[Row121],[Row122],[Row123],[Row124],[Row125],[Row126],[Row127],[Row128],[Row129],[Row130]," +
                            "[Row131],[Row132],[Row133],[Row134],[Row135],[Row136],[Row137],[Row138],[Row139],[Row140]," +
                            "[Row141],[Row142],[Row143],[Row144],[Row145],[Row146],[Row147],[Row148],[Row149],[Row150]," +
                            "[Row151],[Row152],[Row153],[Row154],[Row155],[Row156],[Row157],[Row158],[Row159],[Row160]," +
                            "[Row161],[Row162],[Row163],[Row164],[Row165],[Row166],[Row167],[Row168],[Row169],[Row170]," +
                            "[Row171],[Row172],[Row173],[Row174],[Row175],[Row176],[Row177],[Row178],[Row179],[Row180]," +
                            "[Row181],[Row182],[Row183],[Row184],[Row185],[Row186],[Row187],[Row188],[Row189],[Row190]," +
                            "[Row191],[Row192],[Row193],[Row194],[Row195],[Row196],[Row197],[Row198],[Row199],[Row200]," +

                            "[Row201],[Row202],[Row203],[Row204],[Row205],[Row206],[Row207],[Row208],[Row209],[Row210]," +
                            "[Row211],[Row212],[Row213],[Row214],[Row215],[Row216],[Row217],[Row218],[Row219],[Row220]," +
                            "[Row221],[Row222],[Row223],[Row224],[Row225],[Row226],[Row227],[Row228],[Row229],[Row230]," +
                            "[Row231],[Row232],[Row233],[Row234],[Row235],[Row236],[Row237],[Row238],[Row239],[Row240]," +
                            "[Row241],[Row242],[Row243],[Row244],[Row245],[Row246],[Row247],[Row248],[Row249],[Row250],[Row251],";



        string _paramaterCommand = "@Date," +
                                    "@Row1,@Row2,@Row3,@Row4,@Row5,@Row6,@Row7,@Row8,@Row9,@Row10," +
                                    "@Row11,@Row12,@Row13,@Row14,@Row15,@Row16,@Row17,@Row18,@Row19,@Row20," +
                                    "@Row21,@Row22,@Row23,@Row24,@Row25,@Row26,@Row27,@Row28,@Row29,@Row30," +
                                    "@Row31,@Row32,@Row33,@Row34,@Row35,@Row36,@Row37,@Row38,@Row39,@Row40," +
                                    "@Row41,@Row42,@Row43,@Row44,@Row45,@Row46,@Row47,@Row48,@Row49,@Row50," +
                                    "@Row51,@Row52,@Row53,@Row54,@Row55,@Row56,@Row57,@Row58,@Row59,@Row60," +
                                    "@Row61,@Row62,@Row63,@Row64,@Row65,@Row66,@Row67,@Row68,@Row69,@Row70," +
                                    "@Row71,@Row72,@Row73,@Row74,@Row75,@Row76,@Row77,@Row78,@Row79,@Row80," +
                                    "@Row81,@Row82,@Row83,@Row84,@Row85,@Row86,@Row87,@Row88,@Row89,@Row90," +
                                    "@Row91,@Row92,@Row93,@Row94,@Row95,@Row96,@Row97,@Row98,@Row99,@Row100," +

                                    "@Row101,@Row102,@Row103,@Row104,@Row105,@Row106,@Row107,@Row108,@Row109,@Row110," +
                                    "@Row111,@Row112,@Row113,@Row114,@Row115,@Row116,@Row117,@Row118,@Row119,@Row120," +
                                    "@Row121,@Row122,@Row123,@Row124,@Row125,@Row126,@Row127,@Row128,@Row129,@Row130," +
                                    "@Row131,@Row132,@Row133,@Row134,@Row135,@Row136,@Row137,@Row138,@Row139,@Row140," +
                                    "@Row141,@Row142,@Row143,@Row144,@Row145,@Row146,@Row147,@Row148,@Row149,@Row150," +
                                    "@Row151,@Row152,@Row153,@Row154,@Row155,@Row156,@Row157,@Row158,@Row159,@Row160," +
                                    "@Row161,@Row162,@Row163,@Row164,@Row165,@Row166,@Row167,@Row168,@Row169,@Row170," +
                                    "@Row171,@Row172,@Row173,@Row174,@Row175,@Row176,@Row177,@Row178,@Row179,@Row180," +
                                    "@Row181,@Row182,@Row183,@Row184,@Row185,@Row186,@Row187,@Row188,@Row189,@Row190," +
                                    "@Row191,@Row192,@Row193,@Row194,@Row195,@Row196,@Row197,@Row198,@Row199,@Row200," +

                                     "@Row201,@Row202,@Row203,@Row204,@Row205,@Row206,@Row207,@Row208,@Row209,@Row210," +
                                    "@Row211,@Row212,@Row213,@Row214,@Row215,@Row216,@Row217,@Row218,@Row219,@Row220," +
                                    "@Row221,@Row222,@Row223,@Row224,@Row225,@Row226,@Row227,@Row228,@Row229,@Row230," +
                                    "@Row231,@Row232,@Row233,@Row234,@Row235,@Row236,@Row237,@Row238,@Row239,@Row240," +
                                    "@Row241,@Row242,@Row243,@Row244,@Row245,@Row246,@Row247,@Row248,@Row249,@Row250,@Row251,";

        //2
        private CorporateGovAM setCorporateGovAM(SqlDataReader dr)
        {
            CorporateGovAM M_CorporateGovAM = new CorporateGovAM();
            M_CorporateGovAM.CorporateGovAMPK = Convert.ToInt32(dr["CorporateGovAMPK"]);
            M_CorporateGovAM.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CorporateGovAM.Status = Convert.ToInt32(dr["Status"]);
            M_CorporateGovAM.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CorporateGovAM.Notes = Convert.ToString(dr["Notes"]);
            M_CorporateGovAM.Date = Convert.ToString(dr["Date"]);
            M_CorporateGovAM.Row1 = Convert.ToString(dr["Row1"]);
            M_CorporateGovAM.Row2 = Convert.ToString(dr["Row2"]);
            M_CorporateGovAM.Row3 = Convert.ToString(dr["Row3"]);
            M_CorporateGovAM.Row4 = Convert.ToString(dr["Row4"]);
            M_CorporateGovAM.Row5 = Convert.ToString(dr["Row5"]);
            M_CorporateGovAM.Row6 = Convert.ToString(dr["Row6"]);
            M_CorporateGovAM.Row7 = Convert.ToString(dr["Row7"]);
            M_CorporateGovAM.Row8 = Convert.ToString(dr["Row8"]);
            M_CorporateGovAM.Row9 = Convert.ToString(dr["Row9"]);
            M_CorporateGovAM.Row10 = Convert.ToString(dr["Row10"]);
            M_CorporateGovAM.Row11 = Convert.ToString(dr["Row11"]);
            M_CorporateGovAM.Row12 = Convert.ToString(dr["Row12"]);
            M_CorporateGovAM.Row13 = Convert.ToString(dr["Row13"]);
            M_CorporateGovAM.Row14 = Convert.ToString(dr["Row14"]);
            M_CorporateGovAM.Row15 = Convert.ToString(dr["Row15"]);
            M_CorporateGovAM.Row16 = Convert.ToString(dr["Row16"]);
            M_CorporateGovAM.Row17 = Convert.ToString(dr["Row17"]);
            M_CorporateGovAM.Row18 = Convert.ToString(dr["Row18"]);
            M_CorporateGovAM.Row19 = Convert.ToString(dr["Row19"]);
            M_CorporateGovAM.Row20 = Convert.ToString(dr["Row20"]);
            M_CorporateGovAM.Row21 = Convert.ToString(dr["Row21"]);
            M_CorporateGovAM.Row22 = Convert.ToString(dr["Row22"]);
            M_CorporateGovAM.Row23 = Convert.ToString(dr["Row23"]);
            M_CorporateGovAM.Row24 = Convert.ToString(dr["Row24"]);
            M_CorporateGovAM.Row25 = Convert.ToString(dr["Row25"]);
            M_CorporateGovAM.Row26 = Convert.ToString(dr["Row26"]);
            M_CorporateGovAM.Row27 = Convert.ToString(dr["Row27"]);
            M_CorporateGovAM.Row28 = Convert.ToString(dr["Row28"]);
            M_CorporateGovAM.Row29 = Convert.ToString(dr["Row29"]);
            M_CorporateGovAM.Row30 = Convert.ToString(dr["Row30"]);
            M_CorporateGovAM.Row31 = Convert.ToString(dr["Row31"]);
            M_CorporateGovAM.Row32 = Convert.ToString(dr["Row32"]);
            M_CorporateGovAM.Row33 = Convert.ToString(dr["Row33"]);
            M_CorporateGovAM.Row34 = Convert.ToString(dr["Row34"]);
            M_CorporateGovAM.Row35 = Convert.ToString(dr["Row35"]);
            M_CorporateGovAM.Row36 = Convert.ToString(dr["Row36"]);
            M_CorporateGovAM.Row37 = Convert.ToString(dr["Row37"]);
            M_CorporateGovAM.Row38 = Convert.ToString(dr["Row38"]);
            M_CorporateGovAM.Row39 = Convert.ToString(dr["Row39"]);
            M_CorporateGovAM.Row40 = Convert.ToString(dr["Row40"]);
            M_CorporateGovAM.Row41 = Convert.ToString(dr["Row41"]);
            M_CorporateGovAM.Row42 = Convert.ToString(dr["Row42"]);
            M_CorporateGovAM.Row43 = Convert.ToString(dr["Row43"]);
            M_CorporateGovAM.Row44 = Convert.ToString(dr["Row44"]);
            M_CorporateGovAM.Row45 = Convert.ToString(dr["Row45"]);
            M_CorporateGovAM.Row46 = Convert.ToString(dr["Row46"]);
            M_CorporateGovAM.Row47 = Convert.ToString(dr["Row47"]);
            M_CorporateGovAM.Row48 = Convert.ToString(dr["Row48"]);
            M_CorporateGovAM.Row49 = Convert.ToString(dr["Row49"]);
            M_CorporateGovAM.Row50 = Convert.ToString(dr["Row50"]);
            M_CorporateGovAM.Row51 = Convert.ToString(dr["Row51"]);
            M_CorporateGovAM.Row52 = Convert.ToString(dr["Row52"]);
            M_CorporateGovAM.Row53 = Convert.ToString(dr["Row53"]);
            M_CorporateGovAM.Row54 = Convert.ToString(dr["Row54"]);
            M_CorporateGovAM.Row55 = Convert.ToString(dr["Row55"]);
            M_CorporateGovAM.Row56 = Convert.ToString(dr["Row56"]);
            M_CorporateGovAM.Row57 = Convert.ToString(dr["Row57"]);
            M_CorporateGovAM.Row58 = Convert.ToString(dr["Row58"]);
            M_CorporateGovAM.Row59 = Convert.ToString(dr["Row59"]);
            M_CorporateGovAM.Row60 = Convert.ToString(dr["Row60"]);
            M_CorporateGovAM.Row61 = Convert.ToString(dr["Row61"]);
            M_CorporateGovAM.Row62 = Convert.ToString(dr["Row62"]);
            M_CorporateGovAM.Row63 = Convert.ToString(dr["Row63"]);
            M_CorporateGovAM.Row64 = Convert.ToString(dr["Row64"]);
            M_CorporateGovAM.Row65 = Convert.ToString(dr["Row65"]);
            M_CorporateGovAM.Row66 = Convert.ToString(dr["Row66"]);
            M_CorporateGovAM.Row67 = Convert.ToString(dr["Row67"]);
            M_CorporateGovAM.Row68 = Convert.ToString(dr["Row68"]);
            M_CorporateGovAM.Row69 = Convert.ToString(dr["Row69"]);
            M_CorporateGovAM.Row70 = Convert.ToString(dr["Row70"]);
            M_CorporateGovAM.Row71 = Convert.ToString(dr["Row71"]);
            M_CorporateGovAM.Row72 = Convert.ToString(dr["Row72"]);
            M_CorporateGovAM.Row73 = Convert.ToString(dr["Row73"]);
            M_CorporateGovAM.Row74 = Convert.ToString(dr["Row74"]);
            M_CorporateGovAM.Row75 = Convert.ToString(dr["Row75"]);
            M_CorporateGovAM.Row76 = Convert.ToString(dr["Row76"]);
            M_CorporateGovAM.Row77 = Convert.ToString(dr["Row77"]);
            M_CorporateGovAM.Row78 = Convert.ToString(dr["Row78"]);
            M_CorporateGovAM.Row79 = Convert.ToString(dr["Row79"]);
            M_CorporateGovAM.Row80 = Convert.ToString(dr["Row80"]);
            M_CorporateGovAM.Row81 = Convert.ToString(dr["Row81"]);
            M_CorporateGovAM.Row82 = Convert.ToString(dr["Row82"]);
            M_CorporateGovAM.Row83 = Convert.ToString(dr["Row83"]);
            M_CorporateGovAM.Row84 = Convert.ToString(dr["Row84"]);
            M_CorporateGovAM.Row85 = Convert.ToString(dr["Row85"]);
            M_CorporateGovAM.Row86 = Convert.ToString(dr["Row86"]);
            M_CorporateGovAM.Row87 = Convert.ToString(dr["Row87"]);
            M_CorporateGovAM.Row88 = Convert.ToString(dr["Row88"]);
            M_CorporateGovAM.Row89 = Convert.ToString(dr["Row89"]);
            M_CorporateGovAM.Row90 = Convert.ToString(dr["Row90"]);
            M_CorporateGovAM.Row91 = Convert.ToString(dr["Row91"]);
            M_CorporateGovAM.Row92 = Convert.ToString(dr["Row92"]);
            M_CorporateGovAM.Row93 = Convert.ToString(dr["Row93"]);
            M_CorporateGovAM.Row94 = Convert.ToString(dr["Row94"]);
            M_CorporateGovAM.Row95 = Convert.ToString(dr["Row95"]);
            M_CorporateGovAM.Row96 = Convert.ToString(dr["Row96"]);
            M_CorporateGovAM.Row97 = Convert.ToString(dr["Row97"]);
            M_CorporateGovAM.Row98 = Convert.ToString(dr["Row98"]);
            M_CorporateGovAM.Row99 = Convert.ToString(dr["Row99"]);
            M_CorporateGovAM.Row100 = Convert.ToString(dr["Row100"]);
            M_CorporateGovAM.Row101 = Convert.ToString(dr["Row101"]);
            M_CorporateGovAM.Row102 = Convert.ToString(dr["Row102"]);
            M_CorporateGovAM.Row103 = Convert.ToString(dr["Row103"]);
            M_CorporateGovAM.Row104 = Convert.ToString(dr["Row104"]);
            M_CorporateGovAM.Row105 = Convert.ToString(dr["Row105"]);
            M_CorporateGovAM.Row106 = Convert.ToString(dr["Row106"]);
            M_CorporateGovAM.Row107 = Convert.ToString(dr["Row107"]);
            M_CorporateGovAM.Row108 = Convert.ToString(dr["Row108"]);
            M_CorporateGovAM.Row109 = Convert.ToString(dr["Row109"]);
            M_CorporateGovAM.Row110 = Convert.ToString(dr["Row110"]);
            M_CorporateGovAM.Row111 = Convert.ToString(dr["Row111"]);
            M_CorporateGovAM.Row112 = Convert.ToString(dr["Row112"]);
            M_CorporateGovAM.Row113 = Convert.ToString(dr["Row113"]);
            M_CorporateGovAM.Row114 = Convert.ToString(dr["Row114"]);
            M_CorporateGovAM.Row115 = Convert.ToString(dr["Row115"]);
            M_CorporateGovAM.Row116 = Convert.ToString(dr["Row116"]);
            M_CorporateGovAM.Row117 = Convert.ToString(dr["Row117"]);
            M_CorporateGovAM.Row118 = Convert.ToString(dr["Row118"]);
            M_CorporateGovAM.Row119 = Convert.ToString(dr["Row119"]);
            M_CorporateGovAM.Row120 = Convert.ToString(dr["Row120"]);
            M_CorporateGovAM.Row121 = Convert.ToString(dr["Row121"]);
            M_CorporateGovAM.Row122 = Convert.ToString(dr["Row122"]);
            M_CorporateGovAM.Row123 = Convert.ToString(dr["Row123"]);
            M_CorporateGovAM.Row124 = Convert.ToString(dr["Row124"]);
            M_CorporateGovAM.Row125 = Convert.ToString(dr["Row125"]);
            M_CorporateGovAM.Row126 = Convert.ToString(dr["Row126"]);
            M_CorporateGovAM.Row127 = Convert.ToString(dr["Row127"]);
            M_CorporateGovAM.Row128 = Convert.ToString(dr["Row128"]);
            M_CorporateGovAM.Row129 = Convert.ToString(dr["Row129"]);
            M_CorporateGovAM.Row130 = Convert.ToString(dr["Row130"]);
            M_CorporateGovAM.Row131 = Convert.ToString(dr["Row131"]);
            M_CorporateGovAM.Row132 = Convert.ToString(dr["Row132"]);
            M_CorporateGovAM.Row133 = Convert.ToString(dr["Row133"]);
            M_CorporateGovAM.Row134 = Convert.ToString(dr["Row134"]);
            M_CorporateGovAM.Row135 = Convert.ToString(dr["Row135"]);
            M_CorporateGovAM.Row136 = Convert.ToString(dr["Row136"]);
            M_CorporateGovAM.Row137 = Convert.ToString(dr["Row137"]);
            M_CorporateGovAM.Row138 = Convert.ToString(dr["Row138"]);
            M_CorporateGovAM.Row139 = Convert.ToString(dr["Row139"]);
            M_CorporateGovAM.Row140 = Convert.ToString(dr["Row140"]);
            M_CorporateGovAM.Row141 = Convert.ToString(dr["Row141"]);
            M_CorporateGovAM.Row142 = Convert.ToString(dr["Row142"]);
            M_CorporateGovAM.Row143 = Convert.ToString(dr["Row143"]);
            M_CorporateGovAM.Row144 = Convert.ToString(dr["Row144"]);
            M_CorporateGovAM.Row145 = Convert.ToString(dr["Row145"]);
            M_CorporateGovAM.Row146 = Convert.ToString(dr["Row146"]);
            M_CorporateGovAM.Row147 = Convert.ToString(dr["Row147"]);
            M_CorporateGovAM.Row148 = Convert.ToString(dr["Row148"]);
            M_CorporateGovAM.Row149 = Convert.ToString(dr["Row149"]);
            M_CorporateGovAM.Row150 = Convert.ToString(dr["Row150"]);
            M_CorporateGovAM.Row151 = Convert.ToString(dr["Row151"]);
            M_CorporateGovAM.Row152 = Convert.ToString(dr["Row152"]);
            M_CorporateGovAM.Row153 = Convert.ToString(dr["Row153"]);
            M_CorporateGovAM.Row154 = Convert.ToString(dr["Row154"]);
            M_CorporateGovAM.Row155 = Convert.ToString(dr["Row155"]);
            M_CorporateGovAM.Row156 = Convert.ToString(dr["Row156"]);
            M_CorporateGovAM.Row157 = Convert.ToString(dr["Row157"]);
            M_CorporateGovAM.Row158 = Convert.ToString(dr["Row158"]);
            M_CorporateGovAM.Row159 = Convert.ToString(dr["Row159"]);
            M_CorporateGovAM.Row160 = Convert.ToString(dr["Row160"]);
            M_CorporateGovAM.Row161 = Convert.ToString(dr["Row161"]);
            M_CorporateGovAM.Row162 = Convert.ToString(dr["Row162"]);
            M_CorporateGovAM.Row163 = Convert.ToString(dr["Row163"]);
            M_CorporateGovAM.Row164 = Convert.ToString(dr["Row164"]);
            M_CorporateGovAM.Row165 = Convert.ToString(dr["Row165"]);
            M_CorporateGovAM.Row166 = Convert.ToString(dr["Row166"]);
            M_CorporateGovAM.Row167 = Convert.ToString(dr["Row167"]);
            M_CorporateGovAM.Row168 = Convert.ToString(dr["Row168"]);
            M_CorporateGovAM.Row169 = Convert.ToString(dr["Row169"]);
            M_CorporateGovAM.Row170 = Convert.ToString(dr["Row170"]);
            M_CorporateGovAM.Row171 = Convert.ToString(dr["Row171"]);
            M_CorporateGovAM.Row172 = Convert.ToString(dr["Row172"]);
            M_CorporateGovAM.Row173 = Convert.ToString(dr["Row173"]);
            M_CorporateGovAM.Row174 = Convert.ToString(dr["Row174"]);
            M_CorporateGovAM.Row175 = Convert.ToString(dr["Row175"]);
            M_CorporateGovAM.Row176 = Convert.ToString(dr["Row176"]);
            M_CorporateGovAM.Row177 = Convert.ToString(dr["Row177"]);
            M_CorporateGovAM.Row178 = Convert.ToString(dr["Row178"]);
            M_CorporateGovAM.Row179 = Convert.ToString(dr["Row179"]);
            M_CorporateGovAM.Row180 = Convert.ToString(dr["Row180"]);
            M_CorporateGovAM.Row181 = Convert.ToString(dr["Row181"]);
            M_CorporateGovAM.Row182 = Convert.ToString(dr["Row182"]);
            M_CorporateGovAM.Row183 = Convert.ToString(dr["Row183"]);
            M_CorporateGovAM.Row184 = Convert.ToString(dr["Row184"]);
            M_CorporateGovAM.Row185 = Convert.ToString(dr["Row185"]);
            M_CorporateGovAM.Row186 = Convert.ToString(dr["Row186"]);
            M_CorporateGovAM.Row187 = Convert.ToString(dr["Row187"]);
            M_CorporateGovAM.Row188 = Convert.ToString(dr["Row188"]);
            M_CorporateGovAM.Row189 = Convert.ToString(dr["Row189"]);
            M_CorporateGovAM.Row190 = Convert.ToString(dr["Row190"]);
            M_CorporateGovAM.Row191 = Convert.ToString(dr["Row191"]);
            M_CorporateGovAM.Row192 = Convert.ToString(dr["Row192"]);
            M_CorporateGovAM.Row193 = Convert.ToString(dr["Row193"]);
            M_CorporateGovAM.Row194 = Convert.ToString(dr["Row194"]);
            M_CorporateGovAM.Row195 = Convert.ToString(dr["Row195"]);
            M_CorporateGovAM.Row196 = Convert.ToString(dr["Row196"]);
            M_CorporateGovAM.Row197 = Convert.ToString(dr["Row197"]);
            M_CorporateGovAM.Row198 = Convert.ToString(dr["Row198"]);
            M_CorporateGovAM.Row199 = Convert.ToString(dr["Row199"]);
            M_CorporateGovAM.Row200 = Convert.ToString(dr["Row200"]);
            M_CorporateGovAM.Row201 = Convert.ToString(dr["Row201"]);
            M_CorporateGovAM.Row202 = Convert.ToString(dr["Row202"]);
            M_CorporateGovAM.Row203 = Convert.ToString(dr["Row203"]);
            M_CorporateGovAM.Row204 = Convert.ToString(dr["Row204"]);
            M_CorporateGovAM.Row205 = Convert.ToString(dr["Row205"]);
            M_CorporateGovAM.Row206 = Convert.ToString(dr["Row206"]);
            M_CorporateGovAM.Row207 = Convert.ToString(dr["Row207"]);
            M_CorporateGovAM.Row208 = Convert.ToString(dr["Row208"]);
            M_CorporateGovAM.Row209 = Convert.ToString(dr["Row209"]);
            M_CorporateGovAM.Row210 = Convert.ToString(dr["Row210"]);
            M_CorporateGovAM.Row211 = Convert.ToString(dr["Row211"]);
            M_CorporateGovAM.Row212 = Convert.ToString(dr["Row212"]);
            M_CorporateGovAM.Row213 = Convert.ToString(dr["Row213"]);
            M_CorporateGovAM.Row214 = Convert.ToString(dr["Row214"]);
            M_CorporateGovAM.Row215 = Convert.ToString(dr["Row215"]);
            M_CorporateGovAM.Row216 = Convert.ToString(dr["Row216"]);
            M_CorporateGovAM.Row217 = Convert.ToString(dr["Row217"]);
            M_CorporateGovAM.Row218 = Convert.ToString(dr["Row218"]);
            M_CorporateGovAM.Row219 = Convert.ToString(dr["Row219"]);
            M_CorporateGovAM.Row220 = Convert.ToString(dr["Row220"]);
            M_CorporateGovAM.Row221 = Convert.ToString(dr["Row221"]);
            M_CorporateGovAM.Row222 = Convert.ToString(dr["Row222"]);
            M_CorporateGovAM.Row223 = Convert.ToString(dr["Row223"]);
            M_CorporateGovAM.Row224 = Convert.ToString(dr["Row224"]);
            M_CorporateGovAM.Row225 = Convert.ToString(dr["Row225"]);
            M_CorporateGovAM.Row226 = Convert.ToString(dr["Row226"]);
            M_CorporateGovAM.Row227 = Convert.ToString(dr["Row227"]);
            M_CorporateGovAM.Row228 = Convert.ToString(dr["Row228"]);
            M_CorporateGovAM.Row229 = Convert.ToString(dr["Row229"]);
            M_CorporateGovAM.Row230 = Convert.ToString(dr["Row230"]);
            M_CorporateGovAM.Row231 = Convert.ToString(dr["Row231"]);
            M_CorporateGovAM.Row232 = Convert.ToString(dr["Row232"]);
            M_CorporateGovAM.Row233 = Convert.ToString(dr["Row233"]);
            M_CorporateGovAM.Row234 = Convert.ToString(dr["Row234"]);
            M_CorporateGovAM.Row235 = Convert.ToString(dr["Row235"]);
            M_CorporateGovAM.Row236 = Convert.ToString(dr["Row236"]);
            M_CorporateGovAM.Row237 = Convert.ToString(dr["Row237"]);
            M_CorporateGovAM.Row238 = Convert.ToString(dr["Row238"]);
            M_CorporateGovAM.Row239 = Convert.ToString(dr["Row239"]);
            M_CorporateGovAM.Row240 = Convert.ToString(dr["Row240"]);
            M_CorporateGovAM.Row241 = Convert.ToString(dr["Row241"]);
            M_CorporateGovAM.Row242 = Convert.ToString(dr["Row242"]);
            M_CorporateGovAM.Row243 = Convert.ToString(dr["Row243"]);
            M_CorporateGovAM.Row244 = Convert.ToString(dr["Row244"]);
            M_CorporateGovAM.Row245 = Convert.ToString(dr["Row245"]);
            M_CorporateGovAM.Row246 = Convert.ToString(dr["Row246"]);
            M_CorporateGovAM.Row247 = Convert.ToString(dr["Row247"]);
            M_CorporateGovAM.Row248 = Convert.ToString(dr["Row248"]);
            M_CorporateGovAM.Row249 = Convert.ToString(dr["Row249"]);
            M_CorporateGovAM.Row250 = Convert.ToString(dr["Row250"]);
            M_CorporateGovAM.Row251 = Convert.ToString(dr["Row251"]);
            M_CorporateGovAM.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CorporateGovAM.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CorporateGovAM.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CorporateGovAM.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CorporateGovAM.EntryTime = dr["EntryTime"].ToString();
            M_CorporateGovAM.UpdateTime = dr["UpdateTime"].ToString();
            M_CorporateGovAM.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CorporateGovAM.VoidTime = dr["VoidTime"].ToString();
            M_CorporateGovAM.DBUserID = dr["DBUserID"].ToString();
            M_CorporateGovAM.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CorporateGovAM.LastUpdate = dr["LastUpdate"].ToString();
            M_CorporateGovAM.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CorporateGovAM;
        }

        public List<CorporateGovAM> CorporateGovAM_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CorporateGovAM> L_CorporateGovAM = new List<CorporateGovAM>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from CorporateGovAM where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from CorporateGovAM order by Date";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CorporateGovAM.Add(setCorporateGovAM(dr));
                                }
                            }
                            return L_CorporateGovAM;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CorporateGovAM_Add(CorporateGovAM _CorporateGovAM, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(CorporateGovAMPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from CorporateGovAM";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CorporateGovAM.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(CorporateGovAMPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from CorporateGovAM";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _CorporateGovAM.Date);
                        cmd.Parameters.AddWithValue("@Row1", _CorporateGovAM.Row1);
                        cmd.Parameters.AddWithValue("@Row2", _CorporateGovAM.Row2);
                        cmd.Parameters.AddWithValue("@Row3", _CorporateGovAM.Row3);
                        cmd.Parameters.AddWithValue("@Row4", _CorporateGovAM.Row4);
                        cmd.Parameters.AddWithValue("@Row5", _CorporateGovAM.Row5);
                        cmd.Parameters.AddWithValue("@Row6", _CorporateGovAM.Row6);
                        cmd.Parameters.AddWithValue("@Row7", _CorporateGovAM.Row7);
                        cmd.Parameters.AddWithValue("@Row8", _CorporateGovAM.Row8);
                        cmd.Parameters.AddWithValue("@Row9", _CorporateGovAM.Row9);
                        cmd.Parameters.AddWithValue("@Row10", _CorporateGovAM.Row10);
                        cmd.Parameters.AddWithValue("@Row11", _CorporateGovAM.Row11);
                        cmd.Parameters.AddWithValue("@Row12", _CorporateGovAM.Row12);
                        cmd.Parameters.AddWithValue("@Row13", _CorporateGovAM.Row13);
                        cmd.Parameters.AddWithValue("@Row14", _CorporateGovAM.Row14);
                        cmd.Parameters.AddWithValue("@Row15", _CorporateGovAM.Row15);
                        cmd.Parameters.AddWithValue("@Row16", _CorporateGovAM.Row16);
                        cmd.Parameters.AddWithValue("@Row17", _CorporateGovAM.Row17);
                        cmd.Parameters.AddWithValue("@Row18", _CorporateGovAM.Row18);
                        cmd.Parameters.AddWithValue("@Row19", _CorporateGovAM.Row19);
                        cmd.Parameters.AddWithValue("@Row20", _CorporateGovAM.Row20);
                        cmd.Parameters.AddWithValue("@Row21", _CorporateGovAM.Row21);
                        cmd.Parameters.AddWithValue("@Row22", _CorporateGovAM.Row22);
                        cmd.Parameters.AddWithValue("@Row23", _CorporateGovAM.Row23);
                        cmd.Parameters.AddWithValue("@Row24", _CorporateGovAM.Row24);
                        cmd.Parameters.AddWithValue("@Row25", _CorporateGovAM.Row25);
                        cmd.Parameters.AddWithValue("@Row26", _CorporateGovAM.Row26);
                        cmd.Parameters.AddWithValue("@Row27", _CorporateGovAM.Row27);
                        cmd.Parameters.AddWithValue("@Row28", _CorporateGovAM.Row28);
                        cmd.Parameters.AddWithValue("@Row29", _CorporateGovAM.Row29);
                        cmd.Parameters.AddWithValue("@Row30", _CorporateGovAM.Row30);
                        cmd.Parameters.AddWithValue("@Row31", _CorporateGovAM.Row31);
                        cmd.Parameters.AddWithValue("@Row32", _CorporateGovAM.Row32);
                        cmd.Parameters.AddWithValue("@Row33", _CorporateGovAM.Row33);
                        cmd.Parameters.AddWithValue("@Row34", _CorporateGovAM.Row34);
                        cmd.Parameters.AddWithValue("@Row35", _CorporateGovAM.Row35);
                        cmd.Parameters.AddWithValue("@Row36", _CorporateGovAM.Row36);
                        cmd.Parameters.AddWithValue("@Row37", _CorporateGovAM.Row37);
                        cmd.Parameters.AddWithValue("@Row38", _CorporateGovAM.Row38);
                        cmd.Parameters.AddWithValue("@Row39", _CorporateGovAM.Row39);
                        cmd.Parameters.AddWithValue("@Row40", _CorporateGovAM.Row40);
                        cmd.Parameters.AddWithValue("@Row41", _CorporateGovAM.Row41);
                        cmd.Parameters.AddWithValue("@Row42", _CorporateGovAM.Row42);
                        cmd.Parameters.AddWithValue("@Row43", _CorporateGovAM.Row43);
                        cmd.Parameters.AddWithValue("@Row44", _CorporateGovAM.Row44);
                        cmd.Parameters.AddWithValue("@Row45", _CorporateGovAM.Row45);
                        cmd.Parameters.AddWithValue("@Row46", _CorporateGovAM.Row46);
                        cmd.Parameters.AddWithValue("@Row47", _CorporateGovAM.Row47);
                        cmd.Parameters.AddWithValue("@Row48", _CorporateGovAM.Row48);
                        cmd.Parameters.AddWithValue("@Row49", _CorporateGovAM.Row49);
                        cmd.Parameters.AddWithValue("@Row50", _CorporateGovAM.Row50);
                        cmd.Parameters.AddWithValue("@Row51", _CorporateGovAM.Row51);
                        cmd.Parameters.AddWithValue("@Row52", _CorporateGovAM.Row52);
                        cmd.Parameters.AddWithValue("@Row53", _CorporateGovAM.Row53);
                        cmd.Parameters.AddWithValue("@Row54", _CorporateGovAM.Row54);
                        cmd.Parameters.AddWithValue("@Row55", _CorporateGovAM.Row55);
                        cmd.Parameters.AddWithValue("@Row56", _CorporateGovAM.Row56);
                        cmd.Parameters.AddWithValue("@Row57", _CorporateGovAM.Row57);
                        cmd.Parameters.AddWithValue("@Row58", _CorporateGovAM.Row58);
                        cmd.Parameters.AddWithValue("@Row59", _CorporateGovAM.Row59);
                        cmd.Parameters.AddWithValue("@Row60", _CorporateGovAM.Row60);
                        cmd.Parameters.AddWithValue("@Row61", _CorporateGovAM.Row61);
                        cmd.Parameters.AddWithValue("@Row62", _CorporateGovAM.Row62);
                        cmd.Parameters.AddWithValue("@Row63", _CorporateGovAM.Row63);
                        cmd.Parameters.AddWithValue("@Row64", _CorporateGovAM.Row64);
                        cmd.Parameters.AddWithValue("@Row65", _CorporateGovAM.Row65);
                        cmd.Parameters.AddWithValue("@Row66", _CorporateGovAM.Row66);
                        cmd.Parameters.AddWithValue("@Row67", _CorporateGovAM.Row67);
                        cmd.Parameters.AddWithValue("@Row68", _CorporateGovAM.Row68);
                        cmd.Parameters.AddWithValue("@Row69", _CorporateGovAM.Row69);
                        cmd.Parameters.AddWithValue("@Row70", _CorporateGovAM.Row70);
                        cmd.Parameters.AddWithValue("@Row71", _CorporateGovAM.Row71);
                        cmd.Parameters.AddWithValue("@Row72", _CorporateGovAM.Row72);
                        cmd.Parameters.AddWithValue("@Row73", _CorporateGovAM.Row73);
                        cmd.Parameters.AddWithValue("@Row74", _CorporateGovAM.Row74);
                        cmd.Parameters.AddWithValue("@Row75", _CorporateGovAM.Row75);
                        cmd.Parameters.AddWithValue("@Row76", _CorporateGovAM.Row76);
                        cmd.Parameters.AddWithValue("@Row77", _CorporateGovAM.Row77);
                        cmd.Parameters.AddWithValue("@Row78", _CorporateGovAM.Row78);
                        cmd.Parameters.AddWithValue("@Row79", _CorporateGovAM.Row79);
                        cmd.Parameters.AddWithValue("@Row80", _CorporateGovAM.Row80);
                        cmd.Parameters.AddWithValue("@Row81", _CorporateGovAM.Row81);
                        cmd.Parameters.AddWithValue("@Row82", _CorporateGovAM.Row82);
                        cmd.Parameters.AddWithValue("@Row83", _CorporateGovAM.Row83);
                        cmd.Parameters.AddWithValue("@Row84", _CorporateGovAM.Row84);
                        cmd.Parameters.AddWithValue("@Row85", _CorporateGovAM.Row85);
                        cmd.Parameters.AddWithValue("@Row86", _CorporateGovAM.Row86);
                        cmd.Parameters.AddWithValue("@Row87", _CorporateGovAM.Row87);
                        cmd.Parameters.AddWithValue("@Row88", _CorporateGovAM.Row88);
                        cmd.Parameters.AddWithValue("@Row89", _CorporateGovAM.Row89);
                        cmd.Parameters.AddWithValue("@Row90", _CorporateGovAM.Row90);
                        cmd.Parameters.AddWithValue("@Row91", _CorporateGovAM.Row91);
                        cmd.Parameters.AddWithValue("@Row92", _CorporateGovAM.Row92);
                        cmd.Parameters.AddWithValue("@Row93", _CorporateGovAM.Row93);
                        cmd.Parameters.AddWithValue("@Row94", _CorporateGovAM.Row94);
                        cmd.Parameters.AddWithValue("@Row95", _CorporateGovAM.Row95);
                        cmd.Parameters.AddWithValue("@Row96", _CorporateGovAM.Row96);
                        cmd.Parameters.AddWithValue("@Row97", _CorporateGovAM.Row97);
                        cmd.Parameters.AddWithValue("@Row98", _CorporateGovAM.Row98);
                        cmd.Parameters.AddWithValue("@Row99", _CorporateGovAM.Row99);
                        cmd.Parameters.AddWithValue("@Row100", _CorporateGovAM.Row100);

                        cmd.Parameters.AddWithValue("@Row101", _CorporateGovAM.Row101);
                        cmd.Parameters.AddWithValue("@Row102", _CorporateGovAM.Row102);
                        cmd.Parameters.AddWithValue("@Row103", _CorporateGovAM.Row103);
                        cmd.Parameters.AddWithValue("@Row104", _CorporateGovAM.Row104);
                        cmd.Parameters.AddWithValue("@Row105", _CorporateGovAM.Row105);
                        cmd.Parameters.AddWithValue("@Row106", _CorporateGovAM.Row106);
                        cmd.Parameters.AddWithValue("@Row107", _CorporateGovAM.Row107);
                        cmd.Parameters.AddWithValue("@Row108", _CorporateGovAM.Row108);
                        cmd.Parameters.AddWithValue("@Row109", _CorporateGovAM.Row109);
                        cmd.Parameters.AddWithValue("@Row110", _CorporateGovAM.Row110);
                        cmd.Parameters.AddWithValue("@Row111", _CorporateGovAM.Row111);
                        cmd.Parameters.AddWithValue("@Row112", _CorporateGovAM.Row112);
                        cmd.Parameters.AddWithValue("@Row113", _CorporateGovAM.Row113);
                        cmd.Parameters.AddWithValue("@Row114", _CorporateGovAM.Row114);
                        cmd.Parameters.AddWithValue("@Row115", _CorporateGovAM.Row115);
                        cmd.Parameters.AddWithValue("@Row116", _CorporateGovAM.Row116);
                        cmd.Parameters.AddWithValue("@Row117", _CorporateGovAM.Row117);
                        cmd.Parameters.AddWithValue("@Row118", _CorporateGovAM.Row118);
                        cmd.Parameters.AddWithValue("@Row119", _CorporateGovAM.Row119);
                        cmd.Parameters.AddWithValue("@Row120", _CorporateGovAM.Row120);
                        cmd.Parameters.AddWithValue("@Row121", _CorporateGovAM.Row121);
                        cmd.Parameters.AddWithValue("@Row122", _CorporateGovAM.Row122);
                        cmd.Parameters.AddWithValue("@Row123", _CorporateGovAM.Row123);
                        cmd.Parameters.AddWithValue("@Row124", _CorporateGovAM.Row124);
                        cmd.Parameters.AddWithValue("@Row125", _CorporateGovAM.Row125);
                        cmd.Parameters.AddWithValue("@Row126", _CorporateGovAM.Row126);
                        cmd.Parameters.AddWithValue("@Row127", _CorporateGovAM.Row127);
                        cmd.Parameters.AddWithValue("@Row128", _CorporateGovAM.Row128);
                        cmd.Parameters.AddWithValue("@Row129", _CorporateGovAM.Row129);
                        cmd.Parameters.AddWithValue("@Row130", _CorporateGovAM.Row130);
                        cmd.Parameters.AddWithValue("@Row131", _CorporateGovAM.Row131);
                        cmd.Parameters.AddWithValue("@Row132", _CorporateGovAM.Row132);
                        cmd.Parameters.AddWithValue("@Row133", _CorporateGovAM.Row133);
                        cmd.Parameters.AddWithValue("@Row134", _CorporateGovAM.Row134);
                        cmd.Parameters.AddWithValue("@Row135", _CorporateGovAM.Row135);
                        cmd.Parameters.AddWithValue("@Row136", _CorporateGovAM.Row136);
                        cmd.Parameters.AddWithValue("@Row137", _CorporateGovAM.Row137);
                        cmd.Parameters.AddWithValue("@Row138", _CorporateGovAM.Row138);
                        cmd.Parameters.AddWithValue("@Row139", _CorporateGovAM.Row139);
                        cmd.Parameters.AddWithValue("@Row140", _CorporateGovAM.Row140);
                        cmd.Parameters.AddWithValue("@Row141", _CorporateGovAM.Row141);
                        cmd.Parameters.AddWithValue("@Row142", _CorporateGovAM.Row142);
                        cmd.Parameters.AddWithValue("@Row143", _CorporateGovAM.Row143);
                        cmd.Parameters.AddWithValue("@Row144", _CorporateGovAM.Row144);
                        cmd.Parameters.AddWithValue("@Row145", _CorporateGovAM.Row145);
                        cmd.Parameters.AddWithValue("@Row146", _CorporateGovAM.Row146);
                        cmd.Parameters.AddWithValue("@Row147", _CorporateGovAM.Row147);
                        cmd.Parameters.AddWithValue("@Row148", _CorporateGovAM.Row148);
                        cmd.Parameters.AddWithValue("@Row149", _CorporateGovAM.Row149);
                        cmd.Parameters.AddWithValue("@Row150", _CorporateGovAM.Row150);
                        cmd.Parameters.AddWithValue("@Row151", _CorporateGovAM.Row151);
                        cmd.Parameters.AddWithValue("@Row152", _CorporateGovAM.Row152);
                        cmd.Parameters.AddWithValue("@Row153", _CorporateGovAM.Row153);
                        cmd.Parameters.AddWithValue("@Row154", _CorporateGovAM.Row154);
                        cmd.Parameters.AddWithValue("@Row155", _CorporateGovAM.Row155);
                        cmd.Parameters.AddWithValue("@Row156", _CorporateGovAM.Row156);
                        cmd.Parameters.AddWithValue("@Row157", _CorporateGovAM.Row157);
                        cmd.Parameters.AddWithValue("@Row158", _CorporateGovAM.Row158);
                        cmd.Parameters.AddWithValue("@Row159", _CorporateGovAM.Row159);
                        cmd.Parameters.AddWithValue("@Row160", _CorporateGovAM.Row160);
                        cmd.Parameters.AddWithValue("@Row161", _CorporateGovAM.Row161);
                        cmd.Parameters.AddWithValue("@Row162", _CorporateGovAM.Row162);
                        cmd.Parameters.AddWithValue("@Row163", _CorporateGovAM.Row163);
                        cmd.Parameters.AddWithValue("@Row164", _CorporateGovAM.Row164);
                        cmd.Parameters.AddWithValue("@Row165", _CorporateGovAM.Row165);
                        cmd.Parameters.AddWithValue("@Row166", _CorporateGovAM.Row166);
                        cmd.Parameters.AddWithValue("@Row167", _CorporateGovAM.Row167);
                        cmd.Parameters.AddWithValue("@Row168", _CorporateGovAM.Row168);
                        cmd.Parameters.AddWithValue("@Row169", _CorporateGovAM.Row169);
                        cmd.Parameters.AddWithValue("@Row170", _CorporateGovAM.Row170);
                        cmd.Parameters.AddWithValue("@Row171", _CorporateGovAM.Row171);
                        cmd.Parameters.AddWithValue("@Row172", _CorporateGovAM.Row172);
                        cmd.Parameters.AddWithValue("@Row173", _CorporateGovAM.Row173);
                        cmd.Parameters.AddWithValue("@Row174", _CorporateGovAM.Row174);
                        cmd.Parameters.AddWithValue("@Row175", _CorporateGovAM.Row175);
                        cmd.Parameters.AddWithValue("@Row176", _CorporateGovAM.Row176);
                        cmd.Parameters.AddWithValue("@Row177", _CorporateGovAM.Row177);
                        cmd.Parameters.AddWithValue("@Row178", _CorporateGovAM.Row178);
                        cmd.Parameters.AddWithValue("@Row179", _CorporateGovAM.Row179);
                        cmd.Parameters.AddWithValue("@Row180", _CorporateGovAM.Row180);
                        cmd.Parameters.AddWithValue("@Row181", _CorporateGovAM.Row181);
                        cmd.Parameters.AddWithValue("@Row182", _CorporateGovAM.Row182);
                        cmd.Parameters.AddWithValue("@Row183", _CorporateGovAM.Row183);
                        cmd.Parameters.AddWithValue("@Row184", _CorporateGovAM.Row184);
                        cmd.Parameters.AddWithValue("@Row185", _CorporateGovAM.Row185);
                        cmd.Parameters.AddWithValue("@Row186", _CorporateGovAM.Row186);
                        cmd.Parameters.AddWithValue("@Row187", _CorporateGovAM.Row187);
                        cmd.Parameters.AddWithValue("@Row188", _CorporateGovAM.Row188);
                        cmd.Parameters.AddWithValue("@Row189", _CorporateGovAM.Row189);
                        cmd.Parameters.AddWithValue("@Row190", _CorporateGovAM.Row190);
                        cmd.Parameters.AddWithValue("@Row191", _CorporateGovAM.Row191);
                        cmd.Parameters.AddWithValue("@Row192", _CorporateGovAM.Row192);
                        cmd.Parameters.AddWithValue("@Row193", _CorporateGovAM.Row193);
                        cmd.Parameters.AddWithValue("@Row194", _CorporateGovAM.Row194);
                        cmd.Parameters.AddWithValue("@Row195", _CorporateGovAM.Row195);
                        cmd.Parameters.AddWithValue("@Row196", _CorporateGovAM.Row196);
                        cmd.Parameters.AddWithValue("@Row197", _CorporateGovAM.Row197);
                        cmd.Parameters.AddWithValue("@Row198", _CorporateGovAM.Row198);
                        cmd.Parameters.AddWithValue("@Row199", _CorporateGovAM.Row199);
                        cmd.Parameters.AddWithValue("@Row200", _CorporateGovAM.Row200);

                        cmd.Parameters.AddWithValue("@Row201", _CorporateGovAM.Row201);
                        cmd.Parameters.AddWithValue("@Row202", _CorporateGovAM.Row202);
                        cmd.Parameters.AddWithValue("@Row203", _CorporateGovAM.Row203);
                        cmd.Parameters.AddWithValue("@Row204", _CorporateGovAM.Row204);
                        cmd.Parameters.AddWithValue("@Row205", _CorporateGovAM.Row205);
                        cmd.Parameters.AddWithValue("@Row206", _CorporateGovAM.Row206);
                        cmd.Parameters.AddWithValue("@Row207", _CorporateGovAM.Row207);
                        cmd.Parameters.AddWithValue("@Row208", _CorporateGovAM.Row208);
                        cmd.Parameters.AddWithValue("@Row209", _CorporateGovAM.Row209);
                        cmd.Parameters.AddWithValue("@Row210", _CorporateGovAM.Row210);
                        cmd.Parameters.AddWithValue("@Row211", _CorporateGovAM.Row11);
                        cmd.Parameters.AddWithValue("@Row212", _CorporateGovAM.Row212);
                        cmd.Parameters.AddWithValue("@Row213", _CorporateGovAM.Row213);
                        cmd.Parameters.AddWithValue("@Row214", _CorporateGovAM.Row214);
                        cmd.Parameters.AddWithValue("@Row215", _CorporateGovAM.Row215);
                        cmd.Parameters.AddWithValue("@Row216", _CorporateGovAM.Row216);
                        cmd.Parameters.AddWithValue("@Row217", _CorporateGovAM.Row217);
                        cmd.Parameters.AddWithValue("@Row218", _CorporateGovAM.Row218);
                        cmd.Parameters.AddWithValue("@Row219", _CorporateGovAM.Row219);
                        cmd.Parameters.AddWithValue("@Row220", _CorporateGovAM.Row220);
                        cmd.Parameters.AddWithValue("@Row221", _CorporateGovAM.Row221);
                        cmd.Parameters.AddWithValue("@Row222", _CorporateGovAM.Row222);
                        cmd.Parameters.AddWithValue("@Row223", _CorporateGovAM.Row223);
                        cmd.Parameters.AddWithValue("@Row224", _CorporateGovAM.Row224);
                        cmd.Parameters.AddWithValue("@Row225", _CorporateGovAM.Row225);
                        cmd.Parameters.AddWithValue("@Row226", _CorporateGovAM.Row226);
                        cmd.Parameters.AddWithValue("@Row227", _CorporateGovAM.Row227);
                        cmd.Parameters.AddWithValue("@Row228", _CorporateGovAM.Row228);
                        cmd.Parameters.AddWithValue("@Row229", _CorporateGovAM.Row229);
                        cmd.Parameters.AddWithValue("@Row230", _CorporateGovAM.Row230);
                        cmd.Parameters.AddWithValue("@Row231", _CorporateGovAM.Row231);
                        cmd.Parameters.AddWithValue("@Row232", _CorporateGovAM.Row232);
                        cmd.Parameters.AddWithValue("@Row233", _CorporateGovAM.Row233);
                        cmd.Parameters.AddWithValue("@Row234", _CorporateGovAM.Row234);
                        cmd.Parameters.AddWithValue("@Row235", _CorporateGovAM.Row235);
                        cmd.Parameters.AddWithValue("@Row236", _CorporateGovAM.Row236);
                        cmd.Parameters.AddWithValue("@Row237", _CorporateGovAM.Row237);
                        cmd.Parameters.AddWithValue("@Row238", _CorporateGovAM.Row238);
                        cmd.Parameters.AddWithValue("@Row239", _CorporateGovAM.Row239);
                        cmd.Parameters.AddWithValue("@Row240", _CorporateGovAM.Row240);
                        cmd.Parameters.AddWithValue("@Row241", _CorporateGovAM.Row241);
                        cmd.Parameters.AddWithValue("@Row242", _CorporateGovAM.Row242);
                        cmd.Parameters.AddWithValue("@Row243", _CorporateGovAM.Row243);
                        cmd.Parameters.AddWithValue("@Row244", _CorporateGovAM.Row244);
                        cmd.Parameters.AddWithValue("@Row245", _CorporateGovAM.Row245);
                        cmd.Parameters.AddWithValue("@Row246", _CorporateGovAM.Row246);
                        cmd.Parameters.AddWithValue("@Row247", _CorporateGovAM.Row247);
                        cmd.Parameters.AddWithValue("@Row248", _CorporateGovAM.Row248);
                        cmd.Parameters.AddWithValue("@Row249", _CorporateGovAM.Row249);
                        cmd.Parameters.AddWithValue("@Row250", _CorporateGovAM.Row250);
                        cmd.Parameters.AddWithValue("@Row251", _CorporateGovAM.Row251);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _CorporateGovAM.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CorporateGovAM");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CorporateGovAM_Update(CorporateGovAM _CorporateGovAM, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_CorporateGovAM.CorporateGovAMPK, _CorporateGovAM.HistoryPK, "CorporateGovAM");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CorporateGovAM set status=2, Notes=@Notes,Date=@Date," +
                                " Row1=@Row1,Row2=@Row2,Row3=@Row3,Row4=@Row4,Row5=@Row5,Row6=@Row6,Row7=@Row7,Row8=@Row8,Row9=@Row9,Row10=@Row10," +
                                " Row11=@Row11,Row12=@Row12,Row13=@Row13,Row14=@Row14,Row15=@Row15,Row16=@Row16,Row17=@Row17,Row18=@Row18,Row19=@Row19,Row20=@Row20," + 
                                " Row21=@Row21,Row22=@Row22,Row23=@Row23,Row24=@Row24,Row25=@Row25,Row26=@Row26,Row27=@Row27,Row28=@Row28,Row29=@Row29,Row30=@Row30," +
                                " Row31=@Row31,Row32=@Row32,Row33=@Row33,Row34=@Row34,Row35=@Row35,Row36=@Row36,Row37=@Row37,Row38=@Row38,Row39=@Row39,Row40=@Row40," +
                                " Row41=@Row41,Row42=@Row42,Row43=@Row43,Row44=@Row44,Row45=@Row45,Row46=@Row46,Row47=@Row47,Row48=@Row48,Row49=@Row49,Row50=@Row50," +
                                " Row51=@Row51,Row52=@Row52,Row53=@Row53,Row54=@Row54,Row55=@Row55,Row56=@Row56,Row57=@Row57,Row58=@Row58,Row59=@Row59,Row60=@Row60," +
                                " Row61=@Row61,Row62=@Row62,Row63=@Row63,Row64=@Row64,Row65=@Row65,Row66=@Row66,Row67=@Row67,Row68=@Row68,Row69=@Row69,Row70=@Row70," +
                                " Row71=@Row71,Row72=@Row72,Row73=@Row73,Row74=@Row74,Row75=@Row75,Row76=@Row76,Row77=@Row77,Row78=@Row78,Row79=@Row79,Row80=@Row80," +
                                " Row81=@Row81,Row82=@Row82,Row83=@Row83,Row84=@Row84,Row85=@Row85,Row86=@Row86,Row87=@Row87,Row88=@Row88,Row89=@Row89,Row90=@Row90," +
                                " Row91=@Row91,Row92=@Row92,Row93=@Row93,Row94=@Row94,Row95=@Row95,Row96=@Row96,Row97=@Row97,Row98=@Row98,Row99=@Row99,Row100=@Row100," +

                                " Row101=@Row101,Row102=@Row102,Row103=@Row103,Row104=@Row104,Row105=@Row105,Row106=@Row106,Row107=@Row107,Row108=@Row108,Row109=@Row109,Row110=@Row110," +
                                " Row111=@Row111,Row112=@Row112,Row113=@Row113,Row114=@Row114,Row115=@Row115,Row116=@Row116,Row117=@Row117,Row118=@Row118,Row119=@Row119,Row120=@Row120," +
                                " Row121=@Row121,Row122=@Row122,Row123=@Row123,Row124=@Row124,Row125=@Row125,Row126=@Row126,Row127=@Row127,Row128=@Row128,Row129=@Row129,Row130=@Row130," +
                                " Row131=@Row131,Row132=@Row132,Row133=@Row133,Row134=@Row134,Row135=@Row135,Row136=@Row136,Row137=@Row137,Row138=@Row138,Row139=@Row139,Row140=@Row140," +
                                " Row141=@Row141,Row142=@Row142,Row143=@Row143,Row144=@Row144,Row145=@Row145,Row146=@Row146,Row147=@Row147,Row148=@Row148,Row149=@Row149,Row150=@Row150," +
                                " Row151=@Row151,Row152=@Row152,Row153=@Row153,Row154=@Row154,Row155=@Row155,Row156=@Row156,Row157=@Row157,Row158=@Row158,Row159=@Row159,Row160=@Row160," +
                                " Row161=@Row161,Row162=@Row162,Row163=@Row163,Row164=@Row164,Row165=@Row165,Row166=@Row166,Row167=@Row167,Row168=@Row168,Row169=@Row169,Row170=@Row170," +
                                " Row171=@Row171,Row172=@Row172,Row173=@Row173,Row174=@Row174,Row175=@Row175,Row176=@Row176,Row177=@Row177,Row178=@Row178,Row179=@Row179,Row180=@Row180," +
                                " Row181=@Row181,Row182=@Row182,Row183=@Row183,Row184=@Row184,Row185=@Row185,Row186=@Row186,Row187=@Row187,Row188=@Row188,Row189=@Row189,Row190=@Row190," +
                                " Row191=@Row191,Row192=@Row192,Row193=@Row193,Row194=@Row194,Row195=@Row195,Row196=@Row196,Row197=@Row197,Row198=@Row198,Row199=@Row199,Row200=@Row200," +


                                " Row201=@Row201,Row202=@Row202,Row203=@Row203,Row204=@Row204,Row205=@Row205,Row206=@Row206,Row207=@Row207,Row208=@Row208,Row209=@Row209,Row210=@Row210," +
                                " Row211=@Row211,Row212=@Row212,Row213=@Row213,Row214=@Row214,Row215=@Row215,Row216=@Row216,Row217=@Row217,Row218=@Row218,Row219=@Row219,Row220=@Row220," +
                                " Row221=@Row221,Row222=@Row222,Row223=@Row223,Row224=@Row224,Row225=@Row225,Row226=@Row226,Row227=@Row227,Row228=@Row228,Row229=@Row229,Row230=@Row230," +
                                " Row231=@Row231,Row232=@Row232,Row233=@Row233,Row234=@Row234,Row235=@Row235,Row236=@Row236,Row237=@Row237,Row238=@Row238,Row239=@Row239,Row240=@Row240," +
                                " Row241=@Row241,Row242=@Row242,Row243=@Row243,Row244=@Row244,Row245=@Row245,Row246=@Row246,Row247=@Row247,Row248=@Row248,Row249=@Row249,Row250=@Row250,@Row251," +


                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CorporateGovAMPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _CorporateGovAM.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                            cmd.Parameters.AddWithValue("@Notes", _CorporateGovAM.Notes);

                            cmd.Parameters.AddWithValue("@Date", _CorporateGovAM.Date);
                            cmd.Parameters.AddWithValue("@Row1", _CorporateGovAM.Row1);
                            cmd.Parameters.AddWithValue("@Row2", _CorporateGovAM.Row2);
                            cmd.Parameters.AddWithValue("@Row3", _CorporateGovAM.Row3);
                            cmd.Parameters.AddWithValue("@Row4", _CorporateGovAM.Row4);
                            cmd.Parameters.AddWithValue("@Row5", _CorporateGovAM.Row5);
                            cmd.Parameters.AddWithValue("@Row6", _CorporateGovAM.Row6);
                            cmd.Parameters.AddWithValue("@Row7", _CorporateGovAM.Row7);
                            cmd.Parameters.AddWithValue("@Row8", _CorporateGovAM.Row8);
                            cmd.Parameters.AddWithValue("@Row9", _CorporateGovAM.Row9);
                            cmd.Parameters.AddWithValue("@Row10", _CorporateGovAM.Row10);
                            cmd.Parameters.AddWithValue("@Row11", _CorporateGovAM.Row11);
                            cmd.Parameters.AddWithValue("@Row12", _CorporateGovAM.Row12);
                            cmd.Parameters.AddWithValue("@Row13", _CorporateGovAM.Row13);
                            cmd.Parameters.AddWithValue("@Row14", _CorporateGovAM.Row14);
                            cmd.Parameters.AddWithValue("@Row15", _CorporateGovAM.Row15);
                            cmd.Parameters.AddWithValue("@Row16", _CorporateGovAM.Row16);
                            cmd.Parameters.AddWithValue("@Row17", _CorporateGovAM.Row17);
                            cmd.Parameters.AddWithValue("@Row18", _CorporateGovAM.Row18);
                            cmd.Parameters.AddWithValue("@Row19", _CorporateGovAM.Row19);
                            cmd.Parameters.AddWithValue("@Row20", _CorporateGovAM.Row20);
                            cmd.Parameters.AddWithValue("@Row21", _CorporateGovAM.Row21);
                            cmd.Parameters.AddWithValue("@Row22", _CorporateGovAM.Row22);
                            cmd.Parameters.AddWithValue("@Row23", _CorporateGovAM.Row23);
                            cmd.Parameters.AddWithValue("@Row24", _CorporateGovAM.Row24);
                            cmd.Parameters.AddWithValue("@Row25", _CorporateGovAM.Row25);
                            cmd.Parameters.AddWithValue("@Row26", _CorporateGovAM.Row26);
                            cmd.Parameters.AddWithValue("@Row27", _CorporateGovAM.Row27);
                            cmd.Parameters.AddWithValue("@Row28", _CorporateGovAM.Row28);
                            cmd.Parameters.AddWithValue("@Row29", _CorporateGovAM.Row29);
                            cmd.Parameters.AddWithValue("@Row30", _CorporateGovAM.Row30);
                            cmd.Parameters.AddWithValue("@Row31", _CorporateGovAM.Row31);
                            cmd.Parameters.AddWithValue("@Row32", _CorporateGovAM.Row32);
                            cmd.Parameters.AddWithValue("@Row33", _CorporateGovAM.Row33);
                            cmd.Parameters.AddWithValue("@Row34", _CorporateGovAM.Row34);
                            cmd.Parameters.AddWithValue("@Row35", _CorporateGovAM.Row35);
                            cmd.Parameters.AddWithValue("@Row36", _CorporateGovAM.Row36);
                            cmd.Parameters.AddWithValue("@Row37", _CorporateGovAM.Row37);
                            cmd.Parameters.AddWithValue("@Row38", _CorporateGovAM.Row38);
                            cmd.Parameters.AddWithValue("@Row39", _CorporateGovAM.Row39);
                            cmd.Parameters.AddWithValue("@Row40", _CorporateGovAM.Row40);
                            cmd.Parameters.AddWithValue("@Row41", _CorporateGovAM.Row41);
                            cmd.Parameters.AddWithValue("@Row42", _CorporateGovAM.Row42);
                            cmd.Parameters.AddWithValue("@Row43", _CorporateGovAM.Row43);
                            cmd.Parameters.AddWithValue("@Row44", _CorporateGovAM.Row44);
                            cmd.Parameters.AddWithValue("@Row45", _CorporateGovAM.Row45);
                            cmd.Parameters.AddWithValue("@Row46", _CorporateGovAM.Row46);
                            cmd.Parameters.AddWithValue("@Row47", _CorporateGovAM.Row47);
                            cmd.Parameters.AddWithValue("@Row48", _CorporateGovAM.Row48);
                            cmd.Parameters.AddWithValue("@Row49", _CorporateGovAM.Row49);
                            cmd.Parameters.AddWithValue("@Row50", _CorporateGovAM.Row50);
                            cmd.Parameters.AddWithValue("@Row51", _CorporateGovAM.Row51);
                            cmd.Parameters.AddWithValue("@Row52", _CorporateGovAM.Row52);
                            cmd.Parameters.AddWithValue("@Row53", _CorporateGovAM.Row53);
                            cmd.Parameters.AddWithValue("@Row54", _CorporateGovAM.Row54);
                            cmd.Parameters.AddWithValue("@Row55", _CorporateGovAM.Row55);
                            cmd.Parameters.AddWithValue("@Row56", _CorporateGovAM.Row56);
                            cmd.Parameters.AddWithValue("@Row57", _CorporateGovAM.Row57);
                            cmd.Parameters.AddWithValue("@Row58", _CorporateGovAM.Row58);
                            cmd.Parameters.AddWithValue("@Row59", _CorporateGovAM.Row59);
                            cmd.Parameters.AddWithValue("@Row60", _CorporateGovAM.Row60);
                            cmd.Parameters.AddWithValue("@Row61", _CorporateGovAM.Row61);
                            cmd.Parameters.AddWithValue("@Row62", _CorporateGovAM.Row62);
                            cmd.Parameters.AddWithValue("@Row63", _CorporateGovAM.Row63);
                            cmd.Parameters.AddWithValue("@Row64", _CorporateGovAM.Row64);
                            cmd.Parameters.AddWithValue("@Row65", _CorporateGovAM.Row65);
                            cmd.Parameters.AddWithValue("@Row66", _CorporateGovAM.Row66);
                            cmd.Parameters.AddWithValue("@Row67", _CorporateGovAM.Row67);
                            cmd.Parameters.AddWithValue("@Row68", _CorporateGovAM.Row68);
                            cmd.Parameters.AddWithValue("@Row69", _CorporateGovAM.Row69);
                            cmd.Parameters.AddWithValue("@Row70", _CorporateGovAM.Row70);
                            cmd.Parameters.AddWithValue("@Row71", _CorporateGovAM.Row71);
                            cmd.Parameters.AddWithValue("@Row72", _CorporateGovAM.Row72);
                            cmd.Parameters.AddWithValue("@Row73", _CorporateGovAM.Row73);
                            cmd.Parameters.AddWithValue("@Row74", _CorporateGovAM.Row74);
                            cmd.Parameters.AddWithValue("@Row75", _CorporateGovAM.Row75);
                            cmd.Parameters.AddWithValue("@Row76", _CorporateGovAM.Row76);
                            cmd.Parameters.AddWithValue("@Row77", _CorporateGovAM.Row77);
                            cmd.Parameters.AddWithValue("@Row78", _CorporateGovAM.Row78);
                            cmd.Parameters.AddWithValue("@Row79", _CorporateGovAM.Row79);
                            cmd.Parameters.AddWithValue("@Row80", _CorporateGovAM.Row80);
                            cmd.Parameters.AddWithValue("@Row81", _CorporateGovAM.Row81);
                            cmd.Parameters.AddWithValue("@Row82", _CorporateGovAM.Row82);
                            cmd.Parameters.AddWithValue("@Row83", _CorporateGovAM.Row83);
                            cmd.Parameters.AddWithValue("@Row84", _CorporateGovAM.Row84);
                            cmd.Parameters.AddWithValue("@Row85", _CorporateGovAM.Row85);
                            cmd.Parameters.AddWithValue("@Row86", _CorporateGovAM.Row86);
                            cmd.Parameters.AddWithValue("@Row87", _CorporateGovAM.Row87);
                            cmd.Parameters.AddWithValue("@Row88", _CorporateGovAM.Row88);
                            cmd.Parameters.AddWithValue("@Row89", _CorporateGovAM.Row89);
                            cmd.Parameters.AddWithValue("@Row90", _CorporateGovAM.Row90);
                            cmd.Parameters.AddWithValue("@Row91", _CorporateGovAM.Row91);
                            cmd.Parameters.AddWithValue("@Row92", _CorporateGovAM.Row92);
                            cmd.Parameters.AddWithValue("@Row93", _CorporateGovAM.Row93);
                            cmd.Parameters.AddWithValue("@Row94", _CorporateGovAM.Row94);
                            cmd.Parameters.AddWithValue("@Row95", _CorporateGovAM.Row95);
                            cmd.Parameters.AddWithValue("@Row96", _CorporateGovAM.Row96);
                            cmd.Parameters.AddWithValue("@Row97", _CorporateGovAM.Row97);
                            cmd.Parameters.AddWithValue("@Row98", _CorporateGovAM.Row98);
                            cmd.Parameters.AddWithValue("@Row99", _CorporateGovAM.Row99);
                            cmd.Parameters.AddWithValue("@Row100", _CorporateGovAM.Row100);

                            cmd.Parameters.AddWithValue("@Row101", _CorporateGovAM.Row101);
                            cmd.Parameters.AddWithValue("@Row102", _CorporateGovAM.Row102);
                            cmd.Parameters.AddWithValue("@Row103", _CorporateGovAM.Row103);
                            cmd.Parameters.AddWithValue("@Row104", _CorporateGovAM.Row104);
                            cmd.Parameters.AddWithValue("@Row105", _CorporateGovAM.Row105);
                            cmd.Parameters.AddWithValue("@Row106", _CorporateGovAM.Row106);
                            cmd.Parameters.AddWithValue("@Row107", _CorporateGovAM.Row107);
                            cmd.Parameters.AddWithValue("@Row108", _CorporateGovAM.Row108);
                            cmd.Parameters.AddWithValue("@Row109", _CorporateGovAM.Row109);
                            cmd.Parameters.AddWithValue("@Row110", _CorporateGovAM.Row110);
                            cmd.Parameters.AddWithValue("@Row111", _CorporateGovAM.Row111);
                            cmd.Parameters.AddWithValue("@Row112", _CorporateGovAM.Row112);
                            cmd.Parameters.AddWithValue("@Row113", _CorporateGovAM.Row113);
                            cmd.Parameters.AddWithValue("@Row114", _CorporateGovAM.Row114);
                            cmd.Parameters.AddWithValue("@Row115", _CorporateGovAM.Row115);
                            cmd.Parameters.AddWithValue("@Row116", _CorporateGovAM.Row116);
                            cmd.Parameters.AddWithValue("@Row117", _CorporateGovAM.Row117);
                            cmd.Parameters.AddWithValue("@Row118", _CorporateGovAM.Row118);
                            cmd.Parameters.AddWithValue("@Row119", _CorporateGovAM.Row119);
                            cmd.Parameters.AddWithValue("@Row120", _CorporateGovAM.Row120);
                            cmd.Parameters.AddWithValue("@Row121", _CorporateGovAM.Row121);
                            cmd.Parameters.AddWithValue("@Row122", _CorporateGovAM.Row122);
                            cmd.Parameters.AddWithValue("@Row123", _CorporateGovAM.Row123);
                            cmd.Parameters.AddWithValue("@Row124", _CorporateGovAM.Row124);
                            cmd.Parameters.AddWithValue("@Row125", _CorporateGovAM.Row125);
                            cmd.Parameters.AddWithValue("@Row126", _CorporateGovAM.Row126);
                            cmd.Parameters.AddWithValue("@Row127", _CorporateGovAM.Row127);
                            cmd.Parameters.AddWithValue("@Row128", _CorporateGovAM.Row128);
                            cmd.Parameters.AddWithValue("@Row129", _CorporateGovAM.Row129);
                            cmd.Parameters.AddWithValue("@Row130", _CorporateGovAM.Row130);
                            cmd.Parameters.AddWithValue("@Row131", _CorporateGovAM.Row131);
                            cmd.Parameters.AddWithValue("@Row132", _CorporateGovAM.Row132);
                            cmd.Parameters.AddWithValue("@Row133", _CorporateGovAM.Row133);
                            cmd.Parameters.AddWithValue("@Row134", _CorporateGovAM.Row134);
                            cmd.Parameters.AddWithValue("@Row135", _CorporateGovAM.Row135);
                            cmd.Parameters.AddWithValue("@Row136", _CorporateGovAM.Row136);
                            cmd.Parameters.AddWithValue("@Row137", _CorporateGovAM.Row137);
                            cmd.Parameters.AddWithValue("@Row138", _CorporateGovAM.Row138);
                            cmd.Parameters.AddWithValue("@Row139", _CorporateGovAM.Row139);
                            cmd.Parameters.AddWithValue("@Row140", _CorporateGovAM.Row140);
                            cmd.Parameters.AddWithValue("@Row141", _CorporateGovAM.Row141);
                            cmd.Parameters.AddWithValue("@Row142", _CorporateGovAM.Row142);
                            cmd.Parameters.AddWithValue("@Row143", _CorporateGovAM.Row143);
                            cmd.Parameters.AddWithValue("@Row144", _CorporateGovAM.Row144);
                            cmd.Parameters.AddWithValue("@Row145", _CorporateGovAM.Row145);
                            cmd.Parameters.AddWithValue("@Row146", _CorporateGovAM.Row146);
                            cmd.Parameters.AddWithValue("@Row147", _CorporateGovAM.Row147);
                            cmd.Parameters.AddWithValue("@Row148", _CorporateGovAM.Row148);
                            cmd.Parameters.AddWithValue("@Row149", _CorporateGovAM.Row149);
                            cmd.Parameters.AddWithValue("@Row150", _CorporateGovAM.Row150);
                            cmd.Parameters.AddWithValue("@Row151", _CorporateGovAM.Row151);
                            cmd.Parameters.AddWithValue("@Row152", _CorporateGovAM.Row152);
                            cmd.Parameters.AddWithValue("@Row153", _CorporateGovAM.Row153);
                            cmd.Parameters.AddWithValue("@Row154", _CorporateGovAM.Row154);
                            cmd.Parameters.AddWithValue("@Row155", _CorporateGovAM.Row155);
                            cmd.Parameters.AddWithValue("@Row156", _CorporateGovAM.Row156);
                            cmd.Parameters.AddWithValue("@Row157", _CorporateGovAM.Row157);
                            cmd.Parameters.AddWithValue("@Row158", _CorporateGovAM.Row158);
                            cmd.Parameters.AddWithValue("@Row159", _CorporateGovAM.Row159);
                            cmd.Parameters.AddWithValue("@Row160", _CorporateGovAM.Row160);
                            cmd.Parameters.AddWithValue("@Row161", _CorporateGovAM.Row161);
                            cmd.Parameters.AddWithValue("@Row162", _CorporateGovAM.Row162);
                            cmd.Parameters.AddWithValue("@Row163", _CorporateGovAM.Row163);
                            cmd.Parameters.AddWithValue("@Row164", _CorporateGovAM.Row164);
                            cmd.Parameters.AddWithValue("@Row165", _CorporateGovAM.Row165);
                            cmd.Parameters.AddWithValue("@Row166", _CorporateGovAM.Row166);
                            cmd.Parameters.AddWithValue("@Row167", _CorporateGovAM.Row167);
                            cmd.Parameters.AddWithValue("@Row168", _CorporateGovAM.Row168);
                            cmd.Parameters.AddWithValue("@Row169", _CorporateGovAM.Row169);
                            cmd.Parameters.AddWithValue("@Row170", _CorporateGovAM.Row170);
                            cmd.Parameters.AddWithValue("@Row171", _CorporateGovAM.Row171);
                            cmd.Parameters.AddWithValue("@Row172", _CorporateGovAM.Row172);
                            cmd.Parameters.AddWithValue("@Row173", _CorporateGovAM.Row173);
                            cmd.Parameters.AddWithValue("@Row174", _CorporateGovAM.Row174);
                            cmd.Parameters.AddWithValue("@Row175", _CorporateGovAM.Row175);
                            cmd.Parameters.AddWithValue("@Row176", _CorporateGovAM.Row176);
                            cmd.Parameters.AddWithValue("@Row177", _CorporateGovAM.Row177);
                            cmd.Parameters.AddWithValue("@Row178", _CorporateGovAM.Row178);
                            cmd.Parameters.AddWithValue("@Row179", _CorporateGovAM.Row179);
                            cmd.Parameters.AddWithValue("@Row180", _CorporateGovAM.Row180);
                            cmd.Parameters.AddWithValue("@Row181", _CorporateGovAM.Row181);
                            cmd.Parameters.AddWithValue("@Row182", _CorporateGovAM.Row182);
                            cmd.Parameters.AddWithValue("@Row183", _CorporateGovAM.Row183);
                            cmd.Parameters.AddWithValue("@Row184", _CorporateGovAM.Row184);
                            cmd.Parameters.AddWithValue("@Row185", _CorporateGovAM.Row185);
                            cmd.Parameters.AddWithValue("@Row186", _CorporateGovAM.Row186);
                            cmd.Parameters.AddWithValue("@Row187", _CorporateGovAM.Row187);
                            cmd.Parameters.AddWithValue("@Row188", _CorporateGovAM.Row188);
                            cmd.Parameters.AddWithValue("@Row189", _CorporateGovAM.Row189);
                            cmd.Parameters.AddWithValue("@Row190", _CorporateGovAM.Row190);
                            cmd.Parameters.AddWithValue("@Row191", _CorporateGovAM.Row191);
                            cmd.Parameters.AddWithValue("@Row192", _CorporateGovAM.Row192);
                            cmd.Parameters.AddWithValue("@Row193", _CorporateGovAM.Row193);
                            cmd.Parameters.AddWithValue("@Row194", _CorporateGovAM.Row194);
                            cmd.Parameters.AddWithValue("@Row195", _CorporateGovAM.Row195);
                            cmd.Parameters.AddWithValue("@Row196", _CorporateGovAM.Row196);
                            cmd.Parameters.AddWithValue("@Row197", _CorporateGovAM.Row197);
                            cmd.Parameters.AddWithValue("@Row198", _CorporateGovAM.Row198);
                            cmd.Parameters.AddWithValue("@Row199", _CorporateGovAM.Row199);
                            cmd.Parameters.AddWithValue("@Row200", _CorporateGovAM.Row200);

                            cmd.Parameters.AddWithValue("@Row201", _CorporateGovAM.Row201);
                            cmd.Parameters.AddWithValue("@Row202", _CorporateGovAM.Row202);
                            cmd.Parameters.AddWithValue("@Row203", _CorporateGovAM.Row203);
                            cmd.Parameters.AddWithValue("@Row204", _CorporateGovAM.Row204);
                            cmd.Parameters.AddWithValue("@Row205", _CorporateGovAM.Row205);
                            cmd.Parameters.AddWithValue("@Row206", _CorporateGovAM.Row206);
                            cmd.Parameters.AddWithValue("@Row207", _CorporateGovAM.Row207);
                            cmd.Parameters.AddWithValue("@Row208", _CorporateGovAM.Row208);
                            cmd.Parameters.AddWithValue("@Row209", _CorporateGovAM.Row209);
                            cmd.Parameters.AddWithValue("@Row210", _CorporateGovAM.Row210);
                            cmd.Parameters.AddWithValue("@Row211", _CorporateGovAM.Row211);
                            cmd.Parameters.AddWithValue("@Row212", _CorporateGovAM.Row212);
                            cmd.Parameters.AddWithValue("@Row213", _CorporateGovAM.Row213);
                            cmd.Parameters.AddWithValue("@Row214", _CorporateGovAM.Row214);
                            cmd.Parameters.AddWithValue("@Row215", _CorporateGovAM.Row215);
                            cmd.Parameters.AddWithValue("@Row216", _CorporateGovAM.Row216);
                            cmd.Parameters.AddWithValue("@Row217", _CorporateGovAM.Row217);
                            cmd.Parameters.AddWithValue("@Row218", _CorporateGovAM.Row218);
                            cmd.Parameters.AddWithValue("@Row219", _CorporateGovAM.Row219);
                            cmd.Parameters.AddWithValue("@Row220", _CorporateGovAM.Row220);
                            cmd.Parameters.AddWithValue("@Row221", _CorporateGovAM.Row221);
                            cmd.Parameters.AddWithValue("@Row222", _CorporateGovAM.Row222);
                            cmd.Parameters.AddWithValue("@Row223", _CorporateGovAM.Row223);
                            cmd.Parameters.AddWithValue("@Row224", _CorporateGovAM.Row224);
                            cmd.Parameters.AddWithValue("@Row225", _CorporateGovAM.Row225);
                            cmd.Parameters.AddWithValue("@Row226", _CorporateGovAM.Row226);
                            cmd.Parameters.AddWithValue("@Row227", _CorporateGovAM.Row227);
                            cmd.Parameters.AddWithValue("@Row228", _CorporateGovAM.Row228);
                            cmd.Parameters.AddWithValue("@Row229", _CorporateGovAM.Row229);
                            cmd.Parameters.AddWithValue("@Row230", _CorporateGovAM.Row230);
                            cmd.Parameters.AddWithValue("@Row231", _CorporateGovAM.Row231);
                            cmd.Parameters.AddWithValue("@Row232", _CorporateGovAM.Row232);
                            cmd.Parameters.AddWithValue("@Row233", _CorporateGovAM.Row233);
                            cmd.Parameters.AddWithValue("@Row234", _CorporateGovAM.Row234);
                            cmd.Parameters.AddWithValue("@Row235", _CorporateGovAM.Row235);
                            cmd.Parameters.AddWithValue("@Row236", _CorporateGovAM.Row236);
                            cmd.Parameters.AddWithValue("@Row237", _CorporateGovAM.Row237);
                            cmd.Parameters.AddWithValue("@Row238", _CorporateGovAM.Row238);
                            cmd.Parameters.AddWithValue("@Row239", _CorporateGovAM.Row239);
                            cmd.Parameters.AddWithValue("@Row240", _CorporateGovAM.Row240);
                            cmd.Parameters.AddWithValue("@Row241", _CorporateGovAM.Row241);
                            cmd.Parameters.AddWithValue("@Row242", _CorporateGovAM.Row242);
                            cmd.Parameters.AddWithValue("@Row243", _CorporateGovAM.Row243);
                            cmd.Parameters.AddWithValue("@Row244", _CorporateGovAM.Row244);
                            cmd.Parameters.AddWithValue("@Row245", _CorporateGovAM.Row245);
                            cmd.Parameters.AddWithValue("@Row246", _CorporateGovAM.Row246);
                            cmd.Parameters.AddWithValue("@Row247", _CorporateGovAM.Row247);
                            cmd.Parameters.AddWithValue("@Row248", _CorporateGovAM.Row248);
                            cmd.Parameters.AddWithValue("@Row249", _CorporateGovAM.Row249);
                            cmd.Parameters.AddWithValue("@Row250", _CorporateGovAM.Row250);
                            cmd.Parameters.AddWithValue("@Row251", _CorporateGovAM.Row251);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _CorporateGovAM.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CorporateGovAM.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CorporateGovAM set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CorporateGovAMPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _CorporateGovAM.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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
                                cmd.CommandText = "Update CorporateGovAM set Notes=@Notes,Date=@Date," +
                                    "Row1=@Row1,Row2=@Row2,Row3=@Row3,Row4=@Row4,Row5=@Row5,Row6=@Row6,Row7=@Row7,Row8=@Row8,Row9=@Row9,Row10=@Row10," +
                                  " Row11=@Row11,Row12=@Row12,Row13=@Row13,Row14=@Row14,Row15=@Row15,Row16=@Row16,Row17=@Row17,Row18=@Row18,Row19=@Row19,Row20=@Row20," +
                                " Row21=@Row21,Row22=@Row22,Row23=@Row23,Row24=@Row24,Row25=@Row25,Row26=@Row26,Row27=@Row27,Row28=@Row28,Row29=@Row29,Row30=@Row30," +
                                " Row31=@Row31,Row32=@Row32,Row33=@Row33,Row34=@Row34,Row35=@Row35,Row36=@Row36,Row37=@Row37,Row38=@Row38,Row39=@Row39,Row40=@Row40," +
                                " Row41=@Row41,Row42=@Row42,Row43=@Row43,Row44=@Row44,Row45=@Row45,Row46=@Row46,Row47=@Row47,Row48=@Row48,Row49=@Row49,Row50=@Row50," +
                                " Row51=@Row51,Row52=@Row52,Row53=@Row53,Row54=@Row54,Row55=@Row55,Row56=@Row56,Row57=@Row57,Row58=@Row58,Row59=@Row59,Row60=@Row60," +
                                " Row61=@Row61,Row62=@Row62,Row63=@Row63,Row64=@Row64,Row65=@Row65,Row66=@Row66,Row67=@Row67,Row68=@Row68,Row69=@Row69,Row70=@Row70," +
                                " Row71=@Row71,Row72=@Row72,Row73=@Row73,Row74=@Row74,Row75=@Row75,Row76=@Row76,Row77=@Row77,Row78=@Row78,Row79=@Row79,Row80=@Row80," +
                                " Row81=@Row81,Row82=@Row82,Row83=@Row83,Row84=@Row84,Row85=@Row85,Row86=@Row86,Row87=@Row87,Row88=@Row88,Row89=@Row89,Row90=@Row90," +
                                " Row91=@Row91,Row92=@Row92,Row93=@Row93,Row94=@Row94,Row95=@Row95,Row96=@Row96,Row97=@Row97,Row98=@Row98,Row99=@Row99,Row100=@Row100," +

                                " Row101=@Row101,Row102=@Row102,Row103=@Row103,Row104=@Row104,Row105=@Row105,Row106=@Row106,Row107=@Row107,Row108=@Row108,Row109=@Row109,Row110=@Row110," +
                                " Row111=@Row111,Row112=@Row112,Row113=@Row113,Row114=@Row114,Row115=@Row115,Row116=@Row116,Row117=@Row117,Row118=@Row118,Row119=@Row119,Row120=@Row120," +
                                " Row121=@Row121,Row122=@Row122,Row123=@Row123,Row124=@Row124,Row125=@Row125,Row126=@Row126,Row127=@Row127,Row128=@Row128,Row129=@Row129,Row130=@Row130," +
                                " Row131=@Row131,Row132=@Row132,Row133=@Row133,Row134=@Row134,Row135=@Row135,Row136=@Row136,Row137=@Row137,Row138=@Row138,Row139=@Row139,Row140=@Row140," +
                                " Row141=@Row141,Row142=@Row142,Row143=@Row143,Row144=@Row144,Row145=@Row145,Row146=@Row146,Row147=@Row147,Row148=@Row148,Row149=@Row149,Row150=@Row150," +
                                " Row151=@Row151,Row152=@Row152,Row153=@Row153,Row154=@Row154,Row155=@Row155,Row156=@Row156,Row157=@Row157,Row158=@Row158,Row159=@Row159,Row160=@Row160," +
                                " Row161=@Row161,Row162=@Row162,Row163=@Row163,Row164=@Row164,Row165=@Row165,Row166=@Row166,Row167=@Row167,Row168=@Row168,Row169=@Row169,Row170=@Row170," +
                                " Row171=@Row171,Row172=@Row172,Row173=@Row173,Row174=@Row174,Row175=@Row175,Row176=@Row176,Row177=@Row177,Row178=@Row178,Row179=@Row179,Row180=@Row180," +
                                " Row181=@Row181,Row182=@Row182,Row183=@Row183,Row184=@Row184,Row185=@Row185,Row186=@Row186,Row187=@Row187,Row188=@Row188,Row189=@Row189,Row190=@Row190," +
                                " Row191=@Row191,Row192=@Row192,Row193=@Row193,Row194=@Row194,Row195=@Row195,Row196=@Row196,Row197=@Row197,Row198=@Row198,Row199=@Row199,Row200=@Row200," +


                                " Row201=@Row201,Row202=@Row202,Row203=@Row203,Row204=@Row204,Row205=@Row205,Row206=@Row206,Row207=@Row207,Row208=@Row208,Row209=@Row209,Row210=@Row210," +
                                " Row211=@Row211,Row212=@Row212,Row213=@Row213,Row214=@Row214,Row215=@Row215,Row216=@Row216,Row217=@Row217,Row218=@Row218,Row219=@Row219,Row220=@Row220," +
                                " Row221=@Row221,Row222=@Row222,Row223=@Row223,Row224=@Row224,Row225=@Row225,Row226=@Row226,Row227=@Row227,Row228=@Row228,Row229=@Row229,Row230=@Row230," +
                                " Row231=@Row231,Row232=@Row232,Row233=@Row233,Row234=@Row234,Row235=@Row235,Row236=@Row236,Row237=@Row237,Row238=@Row238,Row239=@Row239,Row240=@Row240," +
                                " Row241=@Row241,Row242=@Row242,Row243=@Row243,Row244=@Row244,Row245=@Row245,Row246=@Row246,Row247=@Row247,Row248=@Row248,Row249=@Row249,Row250=@Row250,@Row251," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CorporateGovAMPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _CorporateGovAM.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                                cmd.Parameters.AddWithValue("@Notes", _CorporateGovAM.Notes);

                                cmd.Parameters.AddWithValue("@Date", _CorporateGovAM.Date);
                                cmd.Parameters.AddWithValue("@Row1", _CorporateGovAM.Row1);
                                cmd.Parameters.AddWithValue("@Row2", _CorporateGovAM.Row2);
                                cmd.Parameters.AddWithValue("@Row3", _CorporateGovAM.Row3);
                                cmd.Parameters.AddWithValue("@Row4", _CorporateGovAM.Row4);
                                cmd.Parameters.AddWithValue("@Row5", _CorporateGovAM.Row5);
                                cmd.Parameters.AddWithValue("@Row6", _CorporateGovAM.Row6);
                                cmd.Parameters.AddWithValue("@Row7", _CorporateGovAM.Row7);
                                cmd.Parameters.AddWithValue("@Row8", _CorporateGovAM.Row8);
                                cmd.Parameters.AddWithValue("@Row9", _CorporateGovAM.Row9);
                                cmd.Parameters.AddWithValue("@Row10", _CorporateGovAM.Row10);
                                cmd.Parameters.AddWithValue("@Row11", _CorporateGovAM.Row11);
                                cmd.Parameters.AddWithValue("@Row12", _CorporateGovAM.Row12);
                                cmd.Parameters.AddWithValue("@Row13", _CorporateGovAM.Row13);
                                cmd.Parameters.AddWithValue("@Row14", _CorporateGovAM.Row14);
                                cmd.Parameters.AddWithValue("@Row15", _CorporateGovAM.Row15);
                                cmd.Parameters.AddWithValue("@Row16", _CorporateGovAM.Row16);
                                cmd.Parameters.AddWithValue("@Row17", _CorporateGovAM.Row17);
                                cmd.Parameters.AddWithValue("@Row18", _CorporateGovAM.Row18);
                                cmd.Parameters.AddWithValue("@Row19", _CorporateGovAM.Row19);
                                cmd.Parameters.AddWithValue("@Row20", _CorporateGovAM.Row20);
                                cmd.Parameters.AddWithValue("@Row21", _CorporateGovAM.Row21);
                                cmd.Parameters.AddWithValue("@Row22", _CorporateGovAM.Row22);
                                cmd.Parameters.AddWithValue("@Row23", _CorporateGovAM.Row23);
                                cmd.Parameters.AddWithValue("@Row24", _CorporateGovAM.Row24);
                                cmd.Parameters.AddWithValue("@Row25", _CorporateGovAM.Row25);
                                cmd.Parameters.AddWithValue("@Row26", _CorporateGovAM.Row26);
                                cmd.Parameters.AddWithValue("@Row27", _CorporateGovAM.Row27);
                                cmd.Parameters.AddWithValue("@Row28", _CorporateGovAM.Row28);
                                cmd.Parameters.AddWithValue("@Row29", _CorporateGovAM.Row29);
                                cmd.Parameters.AddWithValue("@Row30", _CorporateGovAM.Row30);
                                cmd.Parameters.AddWithValue("@Row31", _CorporateGovAM.Row31);
                                cmd.Parameters.AddWithValue("@Row32", _CorporateGovAM.Row32);
                                cmd.Parameters.AddWithValue("@Row33", _CorporateGovAM.Row33);
                                cmd.Parameters.AddWithValue("@Row34", _CorporateGovAM.Row34);
                                cmd.Parameters.AddWithValue("@Row35", _CorporateGovAM.Row35);
                                cmd.Parameters.AddWithValue("@Row36", _CorporateGovAM.Row36);
                                cmd.Parameters.AddWithValue("@Row37", _CorporateGovAM.Row37);
                                cmd.Parameters.AddWithValue("@Row38", _CorporateGovAM.Row38);
                                cmd.Parameters.AddWithValue("@Row39", _CorporateGovAM.Row39);
                                cmd.Parameters.AddWithValue("@Row40", _CorporateGovAM.Row40);
                                cmd.Parameters.AddWithValue("@Row41", _CorporateGovAM.Row41);
                                cmd.Parameters.AddWithValue("@Row42", _CorporateGovAM.Row42);
                                cmd.Parameters.AddWithValue("@Row43", _CorporateGovAM.Row43);
                                cmd.Parameters.AddWithValue("@Row44", _CorporateGovAM.Row44);
                                cmd.Parameters.AddWithValue("@Row45", _CorporateGovAM.Row45);
                                cmd.Parameters.AddWithValue("@Row46", _CorporateGovAM.Row46);
                                cmd.Parameters.AddWithValue("@Row47", _CorporateGovAM.Row47);
                                cmd.Parameters.AddWithValue("@Row48", _CorporateGovAM.Row48);
                                cmd.Parameters.AddWithValue("@Row49", _CorporateGovAM.Row49);
                                cmd.Parameters.AddWithValue("@Row50", _CorporateGovAM.Row50);
                                cmd.Parameters.AddWithValue("@Row51", _CorporateGovAM.Row51);
                                cmd.Parameters.AddWithValue("@Row52", _CorporateGovAM.Row52);
                                cmd.Parameters.AddWithValue("@Row53", _CorporateGovAM.Row53);
                                cmd.Parameters.AddWithValue("@Row54", _CorporateGovAM.Row54);
                                cmd.Parameters.AddWithValue("@Row55", _CorporateGovAM.Row55);
                                cmd.Parameters.AddWithValue("@Row56", _CorporateGovAM.Row56);
                                cmd.Parameters.AddWithValue("@Row57", _CorporateGovAM.Row57);
                                cmd.Parameters.AddWithValue("@Row58", _CorporateGovAM.Row58);
                                cmd.Parameters.AddWithValue("@Row59", _CorporateGovAM.Row59);
                                cmd.Parameters.AddWithValue("@Row60", _CorporateGovAM.Row60);
                                cmd.Parameters.AddWithValue("@Row61", _CorporateGovAM.Row61);
                                cmd.Parameters.AddWithValue("@Row62", _CorporateGovAM.Row62);
                                cmd.Parameters.AddWithValue("@Row63", _CorporateGovAM.Row63);
                                cmd.Parameters.AddWithValue("@Row64", _CorporateGovAM.Row64);
                                cmd.Parameters.AddWithValue("@Row65", _CorporateGovAM.Row65);
                                cmd.Parameters.AddWithValue("@Row66", _CorporateGovAM.Row66);
                                cmd.Parameters.AddWithValue("@Row67", _CorporateGovAM.Row67);
                                cmd.Parameters.AddWithValue("@Row68", _CorporateGovAM.Row68);
                                cmd.Parameters.AddWithValue("@Row69", _CorporateGovAM.Row69);
                                cmd.Parameters.AddWithValue("@Row70", _CorporateGovAM.Row70);
                                cmd.Parameters.AddWithValue("@Row71", _CorporateGovAM.Row71);
                                cmd.Parameters.AddWithValue("@Row72", _CorporateGovAM.Row72);
                                cmd.Parameters.AddWithValue("@Row73", _CorporateGovAM.Row73);
                                cmd.Parameters.AddWithValue("@Row74", _CorporateGovAM.Row74);
                                cmd.Parameters.AddWithValue("@Row75", _CorporateGovAM.Row75);
                                cmd.Parameters.AddWithValue("@Row76", _CorporateGovAM.Row76);
                                cmd.Parameters.AddWithValue("@Row77", _CorporateGovAM.Row77);
                                cmd.Parameters.AddWithValue("@Row78", _CorporateGovAM.Row78);
                                cmd.Parameters.AddWithValue("@Row79", _CorporateGovAM.Row79);
                                cmd.Parameters.AddWithValue("@Row80", _CorporateGovAM.Row80);
                                cmd.Parameters.AddWithValue("@Row81", _CorporateGovAM.Row81);
                                cmd.Parameters.AddWithValue("@Row82", _CorporateGovAM.Row82);
                                cmd.Parameters.AddWithValue("@Row83", _CorporateGovAM.Row83);
                                cmd.Parameters.AddWithValue("@Row84", _CorporateGovAM.Row84);
                                cmd.Parameters.AddWithValue("@Row85", _CorporateGovAM.Row85);
                                cmd.Parameters.AddWithValue("@Row86", _CorporateGovAM.Row86);
                                cmd.Parameters.AddWithValue("@Row87", _CorporateGovAM.Row87);
                                cmd.Parameters.AddWithValue("@Row88", _CorporateGovAM.Row88);
                                cmd.Parameters.AddWithValue("@Row89", _CorporateGovAM.Row89);
                                cmd.Parameters.AddWithValue("@Row90", _CorporateGovAM.Row90);
                                cmd.Parameters.AddWithValue("@Row91", _CorporateGovAM.Row91);
                                cmd.Parameters.AddWithValue("@Row92", _CorporateGovAM.Row92);
                                cmd.Parameters.AddWithValue("@Row93", _CorporateGovAM.Row93);
                                cmd.Parameters.AddWithValue("@Row94", _CorporateGovAM.Row94);
                                cmd.Parameters.AddWithValue("@Row95", _CorporateGovAM.Row95);
                                cmd.Parameters.AddWithValue("@Row96", _CorporateGovAM.Row96);
                                cmd.Parameters.AddWithValue("@Row97", _CorporateGovAM.Row97);
                                cmd.Parameters.AddWithValue("@Row98", _CorporateGovAM.Row98);
                                cmd.Parameters.AddWithValue("@Row99", _CorporateGovAM.Row99);
                                cmd.Parameters.AddWithValue("@Row100", _CorporateGovAM.Row100);

                                cmd.Parameters.AddWithValue("@Row101", _CorporateGovAM.Row101);
                                cmd.Parameters.AddWithValue("@Row102", _CorporateGovAM.Row102);
                                cmd.Parameters.AddWithValue("@Row103", _CorporateGovAM.Row103);
                                cmd.Parameters.AddWithValue("@Row104", _CorporateGovAM.Row104);
                                cmd.Parameters.AddWithValue("@Row105", _CorporateGovAM.Row105);
                                cmd.Parameters.AddWithValue("@Row106", _CorporateGovAM.Row106);
                                cmd.Parameters.AddWithValue("@Row107", _CorporateGovAM.Row107);
                                cmd.Parameters.AddWithValue("@Row108", _CorporateGovAM.Row108);
                                cmd.Parameters.AddWithValue("@Row109", _CorporateGovAM.Row109);
                                cmd.Parameters.AddWithValue("@Row110", _CorporateGovAM.Row110);
                                cmd.Parameters.AddWithValue("@Row111", _CorporateGovAM.Row111);
                                cmd.Parameters.AddWithValue("@Row112", _CorporateGovAM.Row112);
                                cmd.Parameters.AddWithValue("@Row113", _CorporateGovAM.Row113);
                                cmd.Parameters.AddWithValue("@Row114", _CorporateGovAM.Row114);
                                cmd.Parameters.AddWithValue("@Row115", _CorporateGovAM.Row115);
                                cmd.Parameters.AddWithValue("@Row116", _CorporateGovAM.Row116);
                                cmd.Parameters.AddWithValue("@Row117", _CorporateGovAM.Row117);
                                cmd.Parameters.AddWithValue("@Row118", _CorporateGovAM.Row118);
                                cmd.Parameters.AddWithValue("@Row119", _CorporateGovAM.Row119);
                                cmd.Parameters.AddWithValue("@Row120", _CorporateGovAM.Row120);
                                cmd.Parameters.AddWithValue("@Row121", _CorporateGovAM.Row121);
                                cmd.Parameters.AddWithValue("@Row122", _CorporateGovAM.Row122);
                                cmd.Parameters.AddWithValue("@Row123", _CorporateGovAM.Row123);
                                cmd.Parameters.AddWithValue("@Row124", _CorporateGovAM.Row124);
                                cmd.Parameters.AddWithValue("@Row125", _CorporateGovAM.Row125);
                                cmd.Parameters.AddWithValue("@Row126", _CorporateGovAM.Row126);
                                cmd.Parameters.AddWithValue("@Row127", _CorporateGovAM.Row127);
                                cmd.Parameters.AddWithValue("@Row128", _CorporateGovAM.Row128);
                                cmd.Parameters.AddWithValue("@Row129", _CorporateGovAM.Row129);
                                cmd.Parameters.AddWithValue("@Row130", _CorporateGovAM.Row130);
                                cmd.Parameters.AddWithValue("@Row131", _CorporateGovAM.Row131);
                                cmd.Parameters.AddWithValue("@Row132", _CorporateGovAM.Row132);
                                cmd.Parameters.AddWithValue("@Row133", _CorporateGovAM.Row133);
                                cmd.Parameters.AddWithValue("@Row134", _CorporateGovAM.Row134);
                                cmd.Parameters.AddWithValue("@Row135", _CorporateGovAM.Row135);
                                cmd.Parameters.AddWithValue("@Row136", _CorporateGovAM.Row136);
                                cmd.Parameters.AddWithValue("@Row137", _CorporateGovAM.Row137);
                                cmd.Parameters.AddWithValue("@Row138", _CorporateGovAM.Row138);
                                cmd.Parameters.AddWithValue("@Row139", _CorporateGovAM.Row139);
                                cmd.Parameters.AddWithValue("@Row140", _CorporateGovAM.Row140);
                                cmd.Parameters.AddWithValue("@Row141", _CorporateGovAM.Row141);
                                cmd.Parameters.AddWithValue("@Row142", _CorporateGovAM.Row142);
                                cmd.Parameters.AddWithValue("@Row143", _CorporateGovAM.Row143);
                                cmd.Parameters.AddWithValue("@Row144", _CorporateGovAM.Row144);
                                cmd.Parameters.AddWithValue("@Row145", _CorporateGovAM.Row145);
                                cmd.Parameters.AddWithValue("@Row146", _CorporateGovAM.Row146);
                                cmd.Parameters.AddWithValue("@Row147", _CorporateGovAM.Row147);
                                cmd.Parameters.AddWithValue("@Row148", _CorporateGovAM.Row148);
                                cmd.Parameters.AddWithValue("@Row149", _CorporateGovAM.Row149);
                                cmd.Parameters.AddWithValue("@Row150", _CorporateGovAM.Row150);
                                cmd.Parameters.AddWithValue("@Row151", _CorporateGovAM.Row151);
                                cmd.Parameters.AddWithValue("@Row152", _CorporateGovAM.Row152);
                                cmd.Parameters.AddWithValue("@Row153", _CorporateGovAM.Row153);
                                cmd.Parameters.AddWithValue("@Row154", _CorporateGovAM.Row154);
                                cmd.Parameters.AddWithValue("@Row155", _CorporateGovAM.Row155);
                                cmd.Parameters.AddWithValue("@Row156", _CorporateGovAM.Row156);
                                cmd.Parameters.AddWithValue("@Row157", _CorporateGovAM.Row157);
                                cmd.Parameters.AddWithValue("@Row158", _CorporateGovAM.Row158);
                                cmd.Parameters.AddWithValue("@Row159", _CorporateGovAM.Row159);
                                cmd.Parameters.AddWithValue("@Row160", _CorporateGovAM.Row160);
                                cmd.Parameters.AddWithValue("@Row161", _CorporateGovAM.Row161);
                                cmd.Parameters.AddWithValue("@Row162", _CorporateGovAM.Row162);
                                cmd.Parameters.AddWithValue("@Row163", _CorporateGovAM.Row163);
                                cmd.Parameters.AddWithValue("@Row164", _CorporateGovAM.Row164);
                                cmd.Parameters.AddWithValue("@Row165", _CorporateGovAM.Row165);
                                cmd.Parameters.AddWithValue("@Row166", _CorporateGovAM.Row166);
                                cmd.Parameters.AddWithValue("@Row167", _CorporateGovAM.Row167);
                                cmd.Parameters.AddWithValue("@Row168", _CorporateGovAM.Row168);
                                cmd.Parameters.AddWithValue("@Row169", _CorporateGovAM.Row169);
                                cmd.Parameters.AddWithValue("@Row170", _CorporateGovAM.Row170);
                                cmd.Parameters.AddWithValue("@Row171", _CorporateGovAM.Row171);
                                cmd.Parameters.AddWithValue("@Row172", _CorporateGovAM.Row172);
                                cmd.Parameters.AddWithValue("@Row173", _CorporateGovAM.Row173);
                                cmd.Parameters.AddWithValue("@Row174", _CorporateGovAM.Row174);
                                cmd.Parameters.AddWithValue("@Row175", _CorporateGovAM.Row175);
                                cmd.Parameters.AddWithValue("@Row176", _CorporateGovAM.Row176);
                                cmd.Parameters.AddWithValue("@Row177", _CorporateGovAM.Row177);
                                cmd.Parameters.AddWithValue("@Row178", _CorporateGovAM.Row178);
                                cmd.Parameters.AddWithValue("@Row179", _CorporateGovAM.Row179);
                                cmd.Parameters.AddWithValue("@Row180", _CorporateGovAM.Row180);
                                cmd.Parameters.AddWithValue("@Row181", _CorporateGovAM.Row181);
                                cmd.Parameters.AddWithValue("@Row182", _CorporateGovAM.Row182);
                                cmd.Parameters.AddWithValue("@Row183", _CorporateGovAM.Row183);
                                cmd.Parameters.AddWithValue("@Row184", _CorporateGovAM.Row184);
                                cmd.Parameters.AddWithValue("@Row185", _CorporateGovAM.Row185);
                                cmd.Parameters.AddWithValue("@Row186", _CorporateGovAM.Row186);
                                cmd.Parameters.AddWithValue("@Row187", _CorporateGovAM.Row187);
                                cmd.Parameters.AddWithValue("@Row188", _CorporateGovAM.Row188);
                                cmd.Parameters.AddWithValue("@Row189", _CorporateGovAM.Row189);
                                cmd.Parameters.AddWithValue("@Row190", _CorporateGovAM.Row190);
                                cmd.Parameters.AddWithValue("@Row191", _CorporateGovAM.Row191);
                                cmd.Parameters.AddWithValue("@Row192", _CorporateGovAM.Row192);
                                cmd.Parameters.AddWithValue("@Row193", _CorporateGovAM.Row193);
                                cmd.Parameters.AddWithValue("@Row194", _CorporateGovAM.Row194);
                                cmd.Parameters.AddWithValue("@Row195", _CorporateGovAM.Row195);
                                cmd.Parameters.AddWithValue("@Row196", _CorporateGovAM.Row196);
                                cmd.Parameters.AddWithValue("@Row197", _CorporateGovAM.Row197);
                                cmd.Parameters.AddWithValue("@Row198", _CorporateGovAM.Row198);
                                cmd.Parameters.AddWithValue("@Row199", _CorporateGovAM.Row199);
                                cmd.Parameters.AddWithValue("@Row200", _CorporateGovAM.Row200);

                                cmd.Parameters.AddWithValue("@Row201", _CorporateGovAM.Row201);
                                cmd.Parameters.AddWithValue("@Row202", _CorporateGovAM.Row202);
                                cmd.Parameters.AddWithValue("@Row203", _CorporateGovAM.Row203);
                                cmd.Parameters.AddWithValue("@Row204", _CorporateGovAM.Row204);
                                cmd.Parameters.AddWithValue("@Row205", _CorporateGovAM.Row205);
                                cmd.Parameters.AddWithValue("@Row206", _CorporateGovAM.Row206);
                                cmd.Parameters.AddWithValue("@Row207", _CorporateGovAM.Row207);
                                cmd.Parameters.AddWithValue("@Row208", _CorporateGovAM.Row208);
                                cmd.Parameters.AddWithValue("@Row209", _CorporateGovAM.Row209);
                                cmd.Parameters.AddWithValue("@Row210", _CorporateGovAM.Row210);
                                cmd.Parameters.AddWithValue("@Row211", _CorporateGovAM.Row211);
                                cmd.Parameters.AddWithValue("@Row212", _CorporateGovAM.Row212);
                                cmd.Parameters.AddWithValue("@Row213", _CorporateGovAM.Row213);
                                cmd.Parameters.AddWithValue("@Row214", _CorporateGovAM.Row214);
                                cmd.Parameters.AddWithValue("@Row215", _CorporateGovAM.Row215);
                                cmd.Parameters.AddWithValue("@Row216", _CorporateGovAM.Row216);
                                cmd.Parameters.AddWithValue("@Row217", _CorporateGovAM.Row217);
                                cmd.Parameters.AddWithValue("@Row218", _CorporateGovAM.Row218);
                                cmd.Parameters.AddWithValue("@Row219", _CorporateGovAM.Row219);
                                cmd.Parameters.AddWithValue("@Row220", _CorporateGovAM.Row220);
                                cmd.Parameters.AddWithValue("@Row221", _CorporateGovAM.Row221);
                                cmd.Parameters.AddWithValue("@Row222", _CorporateGovAM.Row222);
                                cmd.Parameters.AddWithValue("@Row223", _CorporateGovAM.Row223);
                                cmd.Parameters.AddWithValue("@Row224", _CorporateGovAM.Row224);
                                cmd.Parameters.AddWithValue("@Row225", _CorporateGovAM.Row225);
                                cmd.Parameters.AddWithValue("@Row226", _CorporateGovAM.Row226);
                                cmd.Parameters.AddWithValue("@Row227", _CorporateGovAM.Row227);
                                cmd.Parameters.AddWithValue("@Row228", _CorporateGovAM.Row228);
                                cmd.Parameters.AddWithValue("@Row229", _CorporateGovAM.Row229);
                                cmd.Parameters.AddWithValue("@Row230", _CorporateGovAM.Row230);
                                cmd.Parameters.AddWithValue("@Row231", _CorporateGovAM.Row231);
                                cmd.Parameters.AddWithValue("@Row232", _CorporateGovAM.Row232);
                                cmd.Parameters.AddWithValue("@Row233", _CorporateGovAM.Row233);
                                cmd.Parameters.AddWithValue("@Row234", _CorporateGovAM.Row234);
                                cmd.Parameters.AddWithValue("@Row235", _CorporateGovAM.Row235);
                                cmd.Parameters.AddWithValue("@Row236", _CorporateGovAM.Row236);
                                cmd.Parameters.AddWithValue("@Row237", _CorporateGovAM.Row237);
                                cmd.Parameters.AddWithValue("@Row238", _CorporateGovAM.Row238);
                                cmd.Parameters.AddWithValue("@Row239", _CorporateGovAM.Row239);
                                cmd.Parameters.AddWithValue("@Row240", _CorporateGovAM.Row240);
                                cmd.Parameters.AddWithValue("@Row241", _CorporateGovAM.Row241);
                                cmd.Parameters.AddWithValue("@Row242", _CorporateGovAM.Row242);
                                cmd.Parameters.AddWithValue("@Row243", _CorporateGovAM.Row243);
                                cmd.Parameters.AddWithValue("@Row244", _CorporateGovAM.Row244);
                                cmd.Parameters.AddWithValue("@Row245", _CorporateGovAM.Row245);
                                cmd.Parameters.AddWithValue("@Row246", _CorporateGovAM.Row246);
                                cmd.Parameters.AddWithValue("@Row247", _CorporateGovAM.Row247);
                                cmd.Parameters.AddWithValue("@Row248", _CorporateGovAM.Row248);
                                cmd.Parameters.AddWithValue("@Row249", _CorporateGovAM.Row249);
                                cmd.Parameters.AddWithValue("@Row250", _CorporateGovAM.Row250);
                                cmd.Parameters.AddWithValue("@Row251", _CorporateGovAM.Row251);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CorporateGovAM.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_CorporateGovAM.CorporateGovAMPK, "CorporateGovAM");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CorporateGovAM where CorporateGovAMPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CorporateGovAM.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _CorporateGovAM.Date);
                                cmd.Parameters.AddWithValue("@Row1", _CorporateGovAM.Row1);
                                cmd.Parameters.AddWithValue("@Row2", _CorporateGovAM.Row2);
                                cmd.Parameters.AddWithValue("@Row3", _CorporateGovAM.Row3);
                                cmd.Parameters.AddWithValue("@Row4", _CorporateGovAM.Row4);
                                cmd.Parameters.AddWithValue("@Row5", _CorporateGovAM.Row5);
                                cmd.Parameters.AddWithValue("@Row6", _CorporateGovAM.Row6);
                                cmd.Parameters.AddWithValue("@Row7", _CorporateGovAM.Row7);
                                cmd.Parameters.AddWithValue("@Row8", _CorporateGovAM.Row8);
                                cmd.Parameters.AddWithValue("@Row9", _CorporateGovAM.Row9);
                                cmd.Parameters.AddWithValue("@Row10", _CorporateGovAM.Row10);
                                cmd.Parameters.AddWithValue("@Row11", _CorporateGovAM.Row11);
                                cmd.Parameters.AddWithValue("@Row12", _CorporateGovAM.Row12);
                                cmd.Parameters.AddWithValue("@Row13", _CorporateGovAM.Row13);
                                cmd.Parameters.AddWithValue("@Row14", _CorporateGovAM.Row14);
                                cmd.Parameters.AddWithValue("@Row15", _CorporateGovAM.Row15);
                                cmd.Parameters.AddWithValue("@Row16", _CorporateGovAM.Row16);
                                cmd.Parameters.AddWithValue("@Row17", _CorporateGovAM.Row17);
                                cmd.Parameters.AddWithValue("@Row18", _CorporateGovAM.Row18);
                                cmd.Parameters.AddWithValue("@Row19", _CorporateGovAM.Row19);
                                cmd.Parameters.AddWithValue("@Row20", _CorporateGovAM.Row20);
                                cmd.Parameters.AddWithValue("@Row21", _CorporateGovAM.Row21);
                                cmd.Parameters.AddWithValue("@Row22", _CorporateGovAM.Row22);
                                cmd.Parameters.AddWithValue("@Row23", _CorporateGovAM.Row23);
                                cmd.Parameters.AddWithValue("@Row24", _CorporateGovAM.Row24);
                                cmd.Parameters.AddWithValue("@Row25", _CorporateGovAM.Row25);
                                cmd.Parameters.AddWithValue("@Row26", _CorporateGovAM.Row26);
                                cmd.Parameters.AddWithValue("@Row27", _CorporateGovAM.Row27);
                                cmd.Parameters.AddWithValue("@Row28", _CorporateGovAM.Row28);
                                cmd.Parameters.AddWithValue("@Row29", _CorporateGovAM.Row29);
                                cmd.Parameters.AddWithValue("@Row30", _CorporateGovAM.Row30);
                                cmd.Parameters.AddWithValue("@Row31", _CorporateGovAM.Row31);
                                cmd.Parameters.AddWithValue("@Row32", _CorporateGovAM.Row32);
                                cmd.Parameters.AddWithValue("@Row33", _CorporateGovAM.Row33);
                                cmd.Parameters.AddWithValue("@Row34", _CorporateGovAM.Row34);
                                cmd.Parameters.AddWithValue("@Row35", _CorporateGovAM.Row35);
                                cmd.Parameters.AddWithValue("@Row36", _CorporateGovAM.Row36);
                                cmd.Parameters.AddWithValue("@Row37", _CorporateGovAM.Row37);
                                cmd.Parameters.AddWithValue("@Row38", _CorporateGovAM.Row38);
                                cmd.Parameters.AddWithValue("@Row39", _CorporateGovAM.Row39);
                                cmd.Parameters.AddWithValue("@Row40", _CorporateGovAM.Row40);
                                cmd.Parameters.AddWithValue("@Row41", _CorporateGovAM.Row41);
                                cmd.Parameters.AddWithValue("@Row42", _CorporateGovAM.Row42);
                                cmd.Parameters.AddWithValue("@Row43", _CorporateGovAM.Row43);
                                cmd.Parameters.AddWithValue("@Row44", _CorporateGovAM.Row44);
                                cmd.Parameters.AddWithValue("@Row45", _CorporateGovAM.Row45);
                                cmd.Parameters.AddWithValue("@Row46", _CorporateGovAM.Row46);
                                cmd.Parameters.AddWithValue("@Row47", _CorporateGovAM.Row47);
                                cmd.Parameters.AddWithValue("@Row48", _CorporateGovAM.Row48);
                                cmd.Parameters.AddWithValue("@Row49", _CorporateGovAM.Row49);
                                cmd.Parameters.AddWithValue("@Row50", _CorporateGovAM.Row50);
                                cmd.Parameters.AddWithValue("@Row51", _CorporateGovAM.Row51);
                                cmd.Parameters.AddWithValue("@Row52", _CorporateGovAM.Row52);
                                cmd.Parameters.AddWithValue("@Row53", _CorporateGovAM.Row53);
                                cmd.Parameters.AddWithValue("@Row54", _CorporateGovAM.Row54);
                                cmd.Parameters.AddWithValue("@Row55", _CorporateGovAM.Row55);
                                cmd.Parameters.AddWithValue("@Row56", _CorporateGovAM.Row56);
                                cmd.Parameters.AddWithValue("@Row57", _CorporateGovAM.Row57);
                                cmd.Parameters.AddWithValue("@Row58", _CorporateGovAM.Row58);
                                cmd.Parameters.AddWithValue("@Row59", _CorporateGovAM.Row59);
                                cmd.Parameters.AddWithValue("@Row60", _CorporateGovAM.Row60);
                                cmd.Parameters.AddWithValue("@Row61", _CorporateGovAM.Row61);
                                cmd.Parameters.AddWithValue("@Row62", _CorporateGovAM.Row62);
                                cmd.Parameters.AddWithValue("@Row63", _CorporateGovAM.Row63);
                                cmd.Parameters.AddWithValue("@Row64", _CorporateGovAM.Row64);
                                cmd.Parameters.AddWithValue("@Row65", _CorporateGovAM.Row65);
                                cmd.Parameters.AddWithValue("@Row66", _CorporateGovAM.Row66);
                                cmd.Parameters.AddWithValue("@Row67", _CorporateGovAM.Row67);
                                cmd.Parameters.AddWithValue("@Row68", _CorporateGovAM.Row68);
                                cmd.Parameters.AddWithValue("@Row69", _CorporateGovAM.Row69);
                                cmd.Parameters.AddWithValue("@Row70", _CorporateGovAM.Row70);
                                cmd.Parameters.AddWithValue("@Row71", _CorporateGovAM.Row71);
                                cmd.Parameters.AddWithValue("@Row72", _CorporateGovAM.Row72);
                                cmd.Parameters.AddWithValue("@Row73", _CorporateGovAM.Row73);
                                cmd.Parameters.AddWithValue("@Row74", _CorporateGovAM.Row74);
                                cmd.Parameters.AddWithValue("@Row75", _CorporateGovAM.Row75);
                                cmd.Parameters.AddWithValue("@Row76", _CorporateGovAM.Row76);
                                cmd.Parameters.AddWithValue("@Row77", _CorporateGovAM.Row77);
                                cmd.Parameters.AddWithValue("@Row78", _CorporateGovAM.Row78);
                                cmd.Parameters.AddWithValue("@Row79", _CorporateGovAM.Row79);
                                cmd.Parameters.AddWithValue("@Row80", _CorporateGovAM.Row80);
                                cmd.Parameters.AddWithValue("@Row81", _CorporateGovAM.Row81);
                                cmd.Parameters.AddWithValue("@Row82", _CorporateGovAM.Row82);
                                cmd.Parameters.AddWithValue("@Row83", _CorporateGovAM.Row83);
                                cmd.Parameters.AddWithValue("@Row84", _CorporateGovAM.Row84);
                                cmd.Parameters.AddWithValue("@Row85", _CorporateGovAM.Row85);
                                cmd.Parameters.AddWithValue("@Row86", _CorporateGovAM.Row86);
                                cmd.Parameters.AddWithValue("@Row87", _CorporateGovAM.Row87);
                                cmd.Parameters.AddWithValue("@Row88", _CorporateGovAM.Row88);
                                cmd.Parameters.AddWithValue("@Row89", _CorporateGovAM.Row89);
                                cmd.Parameters.AddWithValue("@Row90", _CorporateGovAM.Row90);
                                cmd.Parameters.AddWithValue("@Row91", _CorporateGovAM.Row91);
                                cmd.Parameters.AddWithValue("@Row92", _CorporateGovAM.Row92);
                                cmd.Parameters.AddWithValue("@Row93", _CorporateGovAM.Row93);
                                cmd.Parameters.AddWithValue("@Row94", _CorporateGovAM.Row94);
                                cmd.Parameters.AddWithValue("@Row95", _CorporateGovAM.Row95);
                                cmd.Parameters.AddWithValue("@Row96", _CorporateGovAM.Row96);
                                cmd.Parameters.AddWithValue("@Row97", _CorporateGovAM.Row97);
                                cmd.Parameters.AddWithValue("@Row98", _CorporateGovAM.Row98);
                                cmd.Parameters.AddWithValue("@Row99", _CorporateGovAM.Row99);
                                cmd.Parameters.AddWithValue("@Row100", _CorporateGovAM.Row100);

                                cmd.Parameters.AddWithValue("@Row101", _CorporateGovAM.Row101);
                                cmd.Parameters.AddWithValue("@Row102", _CorporateGovAM.Row102);
                                cmd.Parameters.AddWithValue("@Row103", _CorporateGovAM.Row103);
                                cmd.Parameters.AddWithValue("@Row104", _CorporateGovAM.Row104);
                                cmd.Parameters.AddWithValue("@Row105", _CorporateGovAM.Row105);
                                cmd.Parameters.AddWithValue("@Row106", _CorporateGovAM.Row106);
                                cmd.Parameters.AddWithValue("@Row107", _CorporateGovAM.Row107);
                                cmd.Parameters.AddWithValue("@Row108", _CorporateGovAM.Row108);
                                cmd.Parameters.AddWithValue("@Row109", _CorporateGovAM.Row109);
                                cmd.Parameters.AddWithValue("@Row110", _CorporateGovAM.Row110);
                                cmd.Parameters.AddWithValue("@Row111", _CorporateGovAM.Row111);
                                cmd.Parameters.AddWithValue("@Row112", _CorporateGovAM.Row112);
                                cmd.Parameters.AddWithValue("@Row113", _CorporateGovAM.Row113);
                                cmd.Parameters.AddWithValue("@Row114", _CorporateGovAM.Row114);
                                cmd.Parameters.AddWithValue("@Row115", _CorporateGovAM.Row115);
                                cmd.Parameters.AddWithValue("@Row116", _CorporateGovAM.Row116);
                                cmd.Parameters.AddWithValue("@Row117", _CorporateGovAM.Row117);
                                cmd.Parameters.AddWithValue("@Row118", _CorporateGovAM.Row118);
                                cmd.Parameters.AddWithValue("@Row119", _CorporateGovAM.Row119);
                                cmd.Parameters.AddWithValue("@Row120", _CorporateGovAM.Row120);
                                cmd.Parameters.AddWithValue("@Row121", _CorporateGovAM.Row121);
                                cmd.Parameters.AddWithValue("@Row122", _CorporateGovAM.Row122);
                                cmd.Parameters.AddWithValue("@Row123", _CorporateGovAM.Row123);
                                cmd.Parameters.AddWithValue("@Row124", _CorporateGovAM.Row124);
                                cmd.Parameters.AddWithValue("@Row125", _CorporateGovAM.Row125);
                                cmd.Parameters.AddWithValue("@Row126", _CorporateGovAM.Row126);
                                cmd.Parameters.AddWithValue("@Row127", _CorporateGovAM.Row127);
                                cmd.Parameters.AddWithValue("@Row128", _CorporateGovAM.Row128);
                                cmd.Parameters.AddWithValue("@Row129", _CorporateGovAM.Row129);
                                cmd.Parameters.AddWithValue("@Row130", _CorporateGovAM.Row130);
                                cmd.Parameters.AddWithValue("@Row131", _CorporateGovAM.Row131);
                                cmd.Parameters.AddWithValue("@Row132", _CorporateGovAM.Row132);
                                cmd.Parameters.AddWithValue("@Row133", _CorporateGovAM.Row133);
                                cmd.Parameters.AddWithValue("@Row134", _CorporateGovAM.Row134);
                                cmd.Parameters.AddWithValue("@Row135", _CorporateGovAM.Row135);
                                cmd.Parameters.AddWithValue("@Row136", _CorporateGovAM.Row136);
                                cmd.Parameters.AddWithValue("@Row137", _CorporateGovAM.Row137);
                                cmd.Parameters.AddWithValue("@Row138", _CorporateGovAM.Row138);
                                cmd.Parameters.AddWithValue("@Row139", _CorporateGovAM.Row139);
                                cmd.Parameters.AddWithValue("@Row140", _CorporateGovAM.Row140);
                                cmd.Parameters.AddWithValue("@Row141", _CorporateGovAM.Row141);
                                cmd.Parameters.AddWithValue("@Row142", _CorporateGovAM.Row142);
                                cmd.Parameters.AddWithValue("@Row143", _CorporateGovAM.Row143);
                                cmd.Parameters.AddWithValue("@Row144", _CorporateGovAM.Row144);
                                cmd.Parameters.AddWithValue("@Row145", _CorporateGovAM.Row145);
                                cmd.Parameters.AddWithValue("@Row146", _CorporateGovAM.Row146);
                                cmd.Parameters.AddWithValue("@Row147", _CorporateGovAM.Row147);
                                cmd.Parameters.AddWithValue("@Row148", _CorporateGovAM.Row148);
                                cmd.Parameters.AddWithValue("@Row149", _CorporateGovAM.Row149);
                                cmd.Parameters.AddWithValue("@Row150", _CorporateGovAM.Row150);
                                cmd.Parameters.AddWithValue("@Row151", _CorporateGovAM.Row151);
                                cmd.Parameters.AddWithValue("@Row152", _CorporateGovAM.Row152);
                                cmd.Parameters.AddWithValue("@Row153", _CorporateGovAM.Row153);
                                cmd.Parameters.AddWithValue("@Row154", _CorporateGovAM.Row154);
                                cmd.Parameters.AddWithValue("@Row155", _CorporateGovAM.Row155);
                                cmd.Parameters.AddWithValue("@Row156", _CorporateGovAM.Row156);
                                cmd.Parameters.AddWithValue("@Row157", _CorporateGovAM.Row157);
                                cmd.Parameters.AddWithValue("@Row158", _CorporateGovAM.Row158);
                                cmd.Parameters.AddWithValue("@Row159", _CorporateGovAM.Row159);
                                cmd.Parameters.AddWithValue("@Row160", _CorporateGovAM.Row160);
                                cmd.Parameters.AddWithValue("@Row161", _CorporateGovAM.Row161);
                                cmd.Parameters.AddWithValue("@Row162", _CorporateGovAM.Row162);
                                cmd.Parameters.AddWithValue("@Row163", _CorporateGovAM.Row163);
                                cmd.Parameters.AddWithValue("@Row164", _CorporateGovAM.Row164);
                                cmd.Parameters.AddWithValue("@Row165", _CorporateGovAM.Row165);
                                cmd.Parameters.AddWithValue("@Row166", _CorporateGovAM.Row166);
                                cmd.Parameters.AddWithValue("@Row167", _CorporateGovAM.Row167);
                                cmd.Parameters.AddWithValue("@Row168", _CorporateGovAM.Row168);
                                cmd.Parameters.AddWithValue("@Row169", _CorporateGovAM.Row169);
                                cmd.Parameters.AddWithValue("@Row170", _CorporateGovAM.Row170);
                                cmd.Parameters.AddWithValue("@Row171", _CorporateGovAM.Row171);
                                cmd.Parameters.AddWithValue("@Row172", _CorporateGovAM.Row172);
                                cmd.Parameters.AddWithValue("@Row173", _CorporateGovAM.Row173);
                                cmd.Parameters.AddWithValue("@Row174", _CorporateGovAM.Row174);
                                cmd.Parameters.AddWithValue("@Row175", _CorporateGovAM.Row175);
                                cmd.Parameters.AddWithValue("@Row176", _CorporateGovAM.Row176);
                                cmd.Parameters.AddWithValue("@Row177", _CorporateGovAM.Row177);
                                cmd.Parameters.AddWithValue("@Row178", _CorporateGovAM.Row178);
                                cmd.Parameters.AddWithValue("@Row179", _CorporateGovAM.Row179);
                                cmd.Parameters.AddWithValue("@Row180", _CorporateGovAM.Row180);
                                cmd.Parameters.AddWithValue("@Row181", _CorporateGovAM.Row181);
                                cmd.Parameters.AddWithValue("@Row182", _CorporateGovAM.Row182);
                                cmd.Parameters.AddWithValue("@Row183", _CorporateGovAM.Row183);
                                cmd.Parameters.AddWithValue("@Row184", _CorporateGovAM.Row184);
                                cmd.Parameters.AddWithValue("@Row185", _CorporateGovAM.Row185);
                                cmd.Parameters.AddWithValue("@Row186", _CorporateGovAM.Row186);
                                cmd.Parameters.AddWithValue("@Row187", _CorporateGovAM.Row187);
                                cmd.Parameters.AddWithValue("@Row188", _CorporateGovAM.Row188);
                                cmd.Parameters.AddWithValue("@Row189", _CorporateGovAM.Row189);
                                cmd.Parameters.AddWithValue("@Row190", _CorporateGovAM.Row190);
                                cmd.Parameters.AddWithValue("@Row191", _CorporateGovAM.Row191);
                                cmd.Parameters.AddWithValue("@Row192", _CorporateGovAM.Row192);
                                cmd.Parameters.AddWithValue("@Row193", _CorporateGovAM.Row193);
                                cmd.Parameters.AddWithValue("@Row194", _CorporateGovAM.Row194);
                                cmd.Parameters.AddWithValue("@Row195", _CorporateGovAM.Row195);
                                cmd.Parameters.AddWithValue("@Row196", _CorporateGovAM.Row196);
                                cmd.Parameters.AddWithValue("@Row197", _CorporateGovAM.Row197);
                                cmd.Parameters.AddWithValue("@Row198", _CorporateGovAM.Row198);
                                cmd.Parameters.AddWithValue("@Row199", _CorporateGovAM.Row199);
                                cmd.Parameters.AddWithValue("@Row200", _CorporateGovAM.Row200);

                                cmd.Parameters.AddWithValue("@Row201", _CorporateGovAM.Row201);
                                cmd.Parameters.AddWithValue("@Row202", _CorporateGovAM.Row202);
                                cmd.Parameters.AddWithValue("@Row203", _CorporateGovAM.Row203);
                                cmd.Parameters.AddWithValue("@Row204", _CorporateGovAM.Row204);
                                cmd.Parameters.AddWithValue("@Row205", _CorporateGovAM.Row205);
                                cmd.Parameters.AddWithValue("@Row206", _CorporateGovAM.Row206);
                                cmd.Parameters.AddWithValue("@Row207", _CorporateGovAM.Row207);
                                cmd.Parameters.AddWithValue("@Row208", _CorporateGovAM.Row208);
                                cmd.Parameters.AddWithValue("@Row209", _CorporateGovAM.Row209);
                                cmd.Parameters.AddWithValue("@Row210", _CorporateGovAM.Row210);
                                cmd.Parameters.AddWithValue("@Row211", _CorporateGovAM.Row211);
                                cmd.Parameters.AddWithValue("@Row212", _CorporateGovAM.Row212);
                                cmd.Parameters.AddWithValue("@Row213", _CorporateGovAM.Row213);
                                cmd.Parameters.AddWithValue("@Row214", _CorporateGovAM.Row214);
                                cmd.Parameters.AddWithValue("@Row215", _CorporateGovAM.Row215);
                                cmd.Parameters.AddWithValue("@Row216", _CorporateGovAM.Row216);
                                cmd.Parameters.AddWithValue("@Row217", _CorporateGovAM.Row217);
                                cmd.Parameters.AddWithValue("@Row218", _CorporateGovAM.Row218);
                                cmd.Parameters.AddWithValue("@Row219", _CorporateGovAM.Row219);
                                cmd.Parameters.AddWithValue("@Row220", _CorporateGovAM.Row220);
                                cmd.Parameters.AddWithValue("@Row221", _CorporateGovAM.Row221);
                                cmd.Parameters.AddWithValue("@Row222", _CorporateGovAM.Row222);
                                cmd.Parameters.AddWithValue("@Row223", _CorporateGovAM.Row223);
                                cmd.Parameters.AddWithValue("@Row224", _CorporateGovAM.Row224);
                                cmd.Parameters.AddWithValue("@Row225", _CorporateGovAM.Row225);
                                cmd.Parameters.AddWithValue("@Row226", _CorporateGovAM.Row226);
                                cmd.Parameters.AddWithValue("@Row227", _CorporateGovAM.Row227);
                                cmd.Parameters.AddWithValue("@Row228", _CorporateGovAM.Row228);
                                cmd.Parameters.AddWithValue("@Row229", _CorporateGovAM.Row229);
                                cmd.Parameters.AddWithValue("@Row230", _CorporateGovAM.Row230);
                                cmd.Parameters.AddWithValue("@Row231", _CorporateGovAM.Row231);
                                cmd.Parameters.AddWithValue("@Row232", _CorporateGovAM.Row232);
                                cmd.Parameters.AddWithValue("@Row233", _CorporateGovAM.Row233);
                                cmd.Parameters.AddWithValue("@Row234", _CorporateGovAM.Row234);
                                cmd.Parameters.AddWithValue("@Row235", _CorporateGovAM.Row235);
                                cmd.Parameters.AddWithValue("@Row236", _CorporateGovAM.Row236);
                                cmd.Parameters.AddWithValue("@Row237", _CorporateGovAM.Row237);
                                cmd.Parameters.AddWithValue("@Row238", _CorporateGovAM.Row238);
                                cmd.Parameters.AddWithValue("@Row239", _CorporateGovAM.Row239);
                                cmd.Parameters.AddWithValue("@Row240", _CorporateGovAM.Row240);
                                cmd.Parameters.AddWithValue("@Row241", _CorporateGovAM.Row241);
                                cmd.Parameters.AddWithValue("@Row242", _CorporateGovAM.Row242);
                                cmd.Parameters.AddWithValue("@Row243", _CorporateGovAM.Row243);
                                cmd.Parameters.AddWithValue("@Row244", _CorporateGovAM.Row244);
                                cmd.Parameters.AddWithValue("@Row245", _CorporateGovAM.Row245);
                                cmd.Parameters.AddWithValue("@Row246", _CorporateGovAM.Row246);
                                cmd.Parameters.AddWithValue("@Row247", _CorporateGovAM.Row247);
                                cmd.Parameters.AddWithValue("@Row248", _CorporateGovAM.Row248);
                                cmd.Parameters.AddWithValue("@Row249", _CorporateGovAM.Row249);
                                cmd.Parameters.AddWithValue("@Row250", _CorporateGovAM.Row250);
                                cmd.Parameters.AddWithValue("@Row251", _CorporateGovAM.Row251);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CorporateGovAM.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CorporateGovAM set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where CorporateGovAMPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _CorporateGovAM.CorporateGovAMPK);
                                cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CorporateGovAM.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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

        public void CorporateGovAM_Approved(CorporateGovAM _CorporateGovAM)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateGovAM set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where CorporateGovAMPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CorporateGovAM.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _CorporateGovAM.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CorporateGovAM set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CorporateGovAMPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CorporateGovAM.ApprovedUsersID);
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

        public void CorporateGovAM_Reject(CorporateGovAM _CorporateGovAM)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateGovAM set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CorporateGovAMPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CorporateGovAM.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CorporateGovAM.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CorporateGovAM set status= 2,LastUpdate=@LastUpdate where CorporateGovAMPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
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

        public void CorporateGovAM_Void(CorporateGovAM _CorporateGovAM)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateGovAM set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CorporateGovAMPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CorporateGovAM.CorporateGovAMPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CorporateGovAM.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CorporateGovAM.VoidUsersID);
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


        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        //public List<GroupsCombo> Groups_Combo()
        //{

        //    try
        //    {
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            List<GroupsCombo> L_Groups = new List<GroupsCombo>();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText = "SELECT  GroupsPK,ID +' - '+ Name ID, Name FROM [Groups]  where status = 2 order by GroupsPK";
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    if (dr.HasRows)
        //                    {
        //                        while (dr.Read())
        //                        {
        //                            GroupsCombo M_Groups = new GroupsCombo();
        //                            M_Groups.GroupsPK = Convert.ToInt32(dr["GroupsPK"]);
        //                            M_Groups.ID = Convert.ToString(dr["ID"]);
        //                            M_Groups.Name = Convert.ToString(dr["Name"]);
        //                            L_Groups.Add(M_Groups);
        //                        }

        //                    }
        //                    return L_Groups;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }


        //}



    }
}
