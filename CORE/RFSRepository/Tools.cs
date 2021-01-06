using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using GemBox.Spreadsheet;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading;
using Excel.FinancialFunctions;
using RFSUtility;
using RFSModel;
using System.Data.OleDb;

namespace RFSRepository
{
    public static class Tools
    {
        public enum status
        {
            pending = 1,
            approved = 2,
            history = 3,
            Waiting = 4,
            Void = 5,
            all = 9
        }

        // 01 = ASCEND
        // 02 = AURORA
        // 03 = INSIGHT
        // 04 = INDO ARTHA BUANA
        // 05 = MNC
        // 06 = INDOASIA
        // 07 = PAYTREN
        // 08 = RHB
        // 09 = EMCO
        // 10 = MANDIRI
        // 11 = TASPEN
        // 12 = JASA CAPITAL / KOSPIN
        // 13 = RAHA
        // 14 = AYERS
        // 15 = RHB SECURITIES
        // 16 = SCG
        // 17 = JARVIS
        // 18 = NUSANTARA
        // 19 = INDOSTERLING
        // 20 = NIKKO
        // 21 = CIPTADANA
        // 22 = STAR
        // 23 = DANA HAJI
        // 24 = BNI
        // 25 = VALBURY
        // 26 = PII
        // 27 = CHUBB
        // 28 = PURWANTO
        // 29 = PRINCIPAL
        // 30 = ANARGYA
        // 31 = SEQUISE
        // 32 = NOBU
        // 33 = BERDIKARI
        // PASANG SESUAI NO CLIENT



        #region NeedToSetup

        // SINVEST HOST TO HOST SETUP
        public const string _sinvestH2HUserName = "";
        public const string _sinvestH2HPassword = "";
        public const string _sinvestH2HPrefix = "H2H";
        public const string _sinvestH2HUrl = "https://10.112.6.105:443";
        // 
        public const string _urlResetPassword = "http://localhost:17050";
        public const string ClientCode = "03";
        public const string _fxdPathAutoNAV = "\\\\10.74.2.38\\temp\\FXD11.txt"; 


        public const bool ParamFundScheme = true;                           
        public const bool RDOSync = false; // PAKE RDO INI DIBIKIN TRUE
                                           //public const string Path = "A:\\RADSOFT\\RFS\\CORE";

        //public const string Path = "A:\\RADSOFT\\RFS\\CORE";
        public const string Path = "D:\\RADSOFT\\CORE"; 
        //public const string Path = "D:\\Radsoft\\RFS\\LAST SOURCE\\CORE";
        //public const string conString = "Data Source=DESKTOP-EUFV9MN\\MSSQLSERVER01;Initial Catalog=RFS_INSIGHT;Persist Security Info=True;Integrated Security=SSPI"; //AZIZ SETTING
        public const string conString = "Data Source=.;Initial Catalog=RFS_INSIGHT;Persist Security Info=True;User ID=sa;Password=as";


        //ini diamin aja gpp, untuk setting bukalapak mandiri
        public const string conString1 = "Data Source=.;Initial Catalog=RFS_CIPTADANA;Persist Security Info=True;User ID=sa;Password=as";
        public const string ReportImageOverBooking = Path + "\\W1\\Images\\10\\MMI_OVERBOOKING.jpg";


        // SAP SETUP
        public const string _sapServer = "11_PROD";

        // EMAIL SETUP
       
        public const string _hostNameEmail = "mail.radsoftsystem.com";
        public const string _userNameEmail = "sent@radsoftsystem.com";
        public const string _passwordEmail = "R@d5oFt#123!";
        public const string _userAPI = "admin";
        public const string _emailFrom02 = "operation@aurora-am.co.id";
        public const string _emailFrom03 = "operation@insights.id";
        public const string _emailFrom12 = "info@jasacapital.co.id";
        public const string _emailFrom = "info@system.com";
        public const int _portEmail = 587;
        public const bool _defaultCredentialsEmail = false;
        public const string _EmailExposure = "";
        public const string _EmailBcc = "";
        public const string _EmailSKExpiredDate = "";
        public const string _EmailSKExpiredDateCC = "";
        public const string _EmailSKExpiredDateBcc = "";
        //public const string _EmailExposure = "herry.octora@gmail.com"; //<- Contoh
        //public const string _EmailBcc = "herry@radsoftsystem.com;hendrawancahyady@gmail.com"; //<- Contoh lebih dari 1 email pake ;
        //setup untuk email highriskmonitoring
        public const string _urlAPIHighRiskMonitoring = "http://localhost:17050";
        public const string _EmailHighRiskMonitoring = "";
        public const int _EmailSessionTime = 60; //dalam menit
        public const bool ComplianceEmail = true; //setting email kalau breach dari oms equity
        public const string _SchedulerEmail = "";
        public const string _SchedulerEmailBcc = "";



        // INTERNET PROXY
        public const string _internetProxy = "172.31.214.1";
        public const int _proxyPort = 3128;

        // THUMBNAIL IMAGE FOR REPORT
        public const int imgWidth = 175;
        public const int imgHeight = 100;
        #endregion

        public const string SQLInjectionMessage = "SQL INJECTION ATTEMPT DETECTED";
        public const string ReportImage = Path + "\\W1\\Images\\Logo\\" + ClientCode + ".png";
        public const string ImgFilePath = Path + "\\W1\\DocumentFundClient\\";
        public const string ImgFFSPath02 = Path + "\\W1\\Images\\02\\";
        public const string HtmlReportsPath = "http://localhost:17050/Reports/";
        public const string HtmlBrokerPath = "http://localhost:17050/Reports/Broker/";
        public const string HtmlTemplatePath = "http://localhost:17050/Template/";
        public const string HtmlSIDPath = "http://localhost:17050/DocumentFundClient/SID/";
        public const string HtmlUnitTrustPath = "http://localhost:17050/Reports/UnitTrustReport/";
        public const string HtmlDailyRptTextPath = "http://localhost:17050/Reports/DailyRpt/TXT/";
        public const string HtmlDailyRptExcelPath = "http://localhost:17050/Reports/DailyRpt/EXCEL/";
        public const string HtmlDailyFormulir31RptExcelPath = "http://localhost:17050/Reports/DailyRpt/FORMULIR31/";
        public const string HtmlMKBDTextPath = "http://localhost:17050/MKBD/TXT/";
        public const string HtmlMKBDExcelPath = "http://localhost:17050/MKBD/EXCEL/";
        public const string HtmlARIATextPath = "http://localhost:17050/ARIA/TXT/";
        public const string HtmlSinvestTextPath = "http://localhost:17050/SInvest/TXT/";
        public const string HtmlTaxAmnestyTextPath = "http://localhost:17050/TaxAmnesty/TXT/";
        public const string HtmlSIPINAPath = "http://localhost:17050/SIPINA/";
        public const string DBFFilePath = Path + "\\W1\\Upload\\";
        public const string ExcelFilePath = Path + "\\W1\\Upload\\";
        public const string TxtFilePath = Path + "\\W1\\Upload\\";
        public const string CSVFilePath = Path + "\\W1\\Upload\\";
        public const string DailyRptTextPath = Path + "\\W1\\Reports\\DailyRpt\\TXT\\";
        public const string DailyRptExcelPath = Path + "\\W1\\Reports\\DailyRpt\\EXCEL\\";
        public const string DailyFormulir31RptExcelPath = Path + "\\W1\\Reports\\DailyRpt\\FORMULIR31\\";
        public const string MKBDTextPath = Path + "\\W1\\MKBD\\TXT\\";
        public const string MKBDTextFolder = Path + "\\W1\\MKBD\\TXT";
        public const string ARIATextPath = Path + "\\W1\\ARIA\\TXT\\";
        public const string SInvestTextPath = Path + "\\W1\\SInvest\\TXT\\";
        public const string TaxAmnestyTextPath = Path + "\\W1\\TaxAmnesty\\TXT\\";
        public const string MKBDExcelPath = Path + "\\W1\\MKBD\\EXCEL\\";
        public const string MKBDExcelFolder = Path + "\\W1\\MKBD\\EXCEL";
        public const string root = Path + "\\W1\\Temp\\";
        public const string storage = Path + @"\\W1\\Upload\\";

        public const string ReportsPath = Path + "\\W1\\Reports\\";
        public const string BrokerPath = Path + "\\W1\\Reports\\Broker\\";
        public const string ReportsTemplatePath = Path + "\\W1\\Template\\";
        public const string SIDPath = Path + "\\W1\\DocumentFundClient\\SID\\";
        public const string UnitTrustPath = Path + "\\W1\\Reports\\UnitTrustReport\\";

        public const string SIPINAPathExcel = Path + "\\W1\\SIPINA\\EXCELSIPINA\\";
        public const string SIPINAPath = Path + "\\W1\\SIPINA\\";
        //21
        public const string ReportImageFFSRisk1 = Path + "\\W1\\Template\\21\\FFS\\Risk\\Lowest.png";
        public const string ReportImageFFSRisk2 = Path + "\\W1\\Template\\21\\FFS\\Risk\\Lower.png";
        public const string ReportImageFFSRisk3 = Path + "\\W1\\Template\\21\\FFS\\Risk\\Moderate.png";
        public const string ReportImageFFSRisk4 = Path + "\\W1\\Template\\21\\FFS\\Risk\\Higher.png";
        public const string ReportImageFFSRisk5 = Path + "\\W1\\Template\\21\\FFS\\Risk\\Highest.png";

        //03 FFS
        public const string ReportImageFFSTINGGI = Path + "\\W1\\Template\\" + ClientCode + "\\ProfilResikoInvestasi\\Tinggi.png";
        public const string ReportImageFFSrENDAH = Path + "\\W1\\Template\\" + ClientCode + "\\ProfilResikoInvestasi\\Rendah.png";
        public const string ReportImageFFS1 = Path + "\\W1\\Template\\" + ClientCode + "\\ProfilResikoInvestasi\\1.png";
        public const string ReportImageFFS2 = Path + "\\W1\\Template\\" + ClientCode + "\\ProfilResikoInvestasi\\2.png";
        public const string ReportImageFFS3 = Path + "\\W1\\Template\\" + ClientCode + "\\ProfilResikoInvestasi\\3.png";
        public const string ReportImageFFS4 = Path + "\\W1\\Template\\" + ClientCode + "\\ProfilResikoInvestasi\\4.png";
        public const string ReportImageFFS5 = Path + "\\W1\\Template\\" + ClientCode + "\\ProfilResikoInvestasi\\5.png";
        //03 FFS New
        public const string ReportImageFFS_OJK1 = Path + "\\W1\\Template\\FFS\\ProfilResikoInvestasi\\1.png";
        public const string ReportImageFFS_OJK2 = Path + "\\W1\\Template\\FFS\\ProfilResikoInvestasi\\2.png";
        public const string ReportImageFFS_OJK3 = Path + "\\W1\\Template\\FFS\\ProfilResikoInvestasi\\3.png";
        public const string ReportImageFFS_OJK4 = Path + "\\W1\\Template\\FFS\\ProfilResikoInvestasi\\4.png";
        public const string ReportImageFFS_OJK5 = Path + "\\W1\\Template\\FFS\\ProfilResikoInvestasi\\5.png";

        public const string ImageOJKReksadana = Path + "\\W1\\Images\\ojkReksadana.png";
        public const string SIDImage1 = Path + "\\W1\\Images\\03.png";
        public const string SIDImage2 = Path + "\\W1\\Images\\03i.png";
        public static string DefaultDisclaimerReportFooterLeftText()
        {
            return "Laporan ini hanya sebagai ilustrasi , Laporan sebenarnya yang di terbitkan dari Bank Kustodian dan setiap keputusan untuk transaksi baik untuk tujuan investasi yang dilakukan sepenuhnya merupakan keputusan dan tanggung jawab dari masing masing pribadi. ";
        }

        public static Tuple<string, string, string> SplitName(this string str)
        {
            var theName = str.Split(' ');
            var middleName = string.Empty;
            var lastName = string.Empty;

            if (theName.Length == 2)
            {
                lastName = theName[1];

            }
            else if (theName.Length > 2)
            {
                var nameUser = theName.Skip(1).ToList();
                lastName = nameUser.Last();
                nameUser.RemoveAt(nameUser.Count - 1);

                middleName = string.Join(" ", nameUser);
            }

            return new Tuple<string, string, string>(theName[0], middleName, lastName);
        }

        public static DataSendEmail SendMail(string recipientTo, string recipientCc, string recipientBcc, string subjectMsg, string bodyMsg, string strAttachment, string usersID)
        {
            try
            {
                int usersPK = 0;
                try
                {
                    usersPK = _host.Get_UsersPK(usersID);
                }
                catch { throw; }

                if (usersPK != 0 && usersPK > 0)
                {
                    if (recipientTo != "" || !string.IsNullOrEmpty(recipientTo))
                    {
                        if (Tools.RegexCheckEmail(recipientTo))
                        {
                            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                            string hostname, username, password = string.Empty;



                            if (Tools.ClientCode == "05")
                            {
                                hostname = "172.17.20.124";
                                username = "cso.mam@mncgroup.com";
                                password = "Mncam777";
                                client.Port = 25;
                                client.EnableSsl = false;
                                client.UseDefaultCredentials = false;
                                client.Credentials = new System.Net.NetworkCredential(username, password, "mncgroup.com");
                                // X509 
                            }
                            else
                            {

                                hostname = _hostNameEmail;
                                username = _userNameEmail;
                                password = _passwordEmail;
                                client.Port = _portEmail;
                                client.UseDefaultCredentials = _defaultCredentialsEmail;
                                client.Credentials = new System.Net.NetworkCredential(username, password);
                            }


                            client.Host = hostname;
                            client.Timeout = (60 * 5 * 10000); // set timeout 5 menit
                            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;



                            mail.From = new System.Net.Mail.MailAddress(username);
                            //mail.Priority = System.Net.Mail.MailPriority.High;


                            //   ServicePointManager.ServerCertificateValidationCallback =
                            //delegate(object s, X509Certificate certificate,
                            //         X509Chain chain, SslPolicyErrors sslPolicyErrors)
                            //{ return true; };

                            // Add a recipient To, Cc, Bcc
                            // TODO: Change the following recipient where appropriate.

                            // Adding multiple To Addresses
                            foreach (string rTo in recipientTo.Split(";".ToCharArray()))
                            {
                                mail.To.Add(rTo);
                            }

                            // Adding multiple Cc Addresses
                            if (recipientCc != "" || !string.IsNullOrEmpty(recipientCc))
                            {
                                foreach (string rCc in recipientCc.Split(";".ToCharArray()))
                                {
                                    if (Tools.RegexCheckEmail(rCc))
                                    {
                                        mail.CC.Add(rCc);
                                    }
                                }
                            }

                            // Adding multiple Bcc Addresses
                            if (recipientBcc != "" || !string.IsNullOrEmpty(recipientBcc))
                            {
                                foreach (string rBcc in recipientBcc.Split(";".ToCharArray()))
                                {
                                    if (Tools.RegexCheckEmail(rBcc))
                                    {
                                        mail.Bcc.Add(rBcc);
                                    }
                                }
                            }

                            // Set the basic properties.
                            mail.Subject = subjectMsg;
                            mail.IsBodyHtml = true;
                            mail.Body =
                                " <html> " +
                                " <head> " +
                                    " <title></title> " +
                                " </head> " +
                                " <body> " +
                                    " <div> " +
                                        " <div> this is your UserID and Password for login Radsoft Fund System, please change it after login <br />  " +
                                            bodyMsg +
                                        " </div> " +
                                        " <br /><br /><br /> " +
                                        " <br /><br /><br /> " +
                                        " Best Regards, " +
                                        " <br /> " +
                                        _host.Get_UsersName(usersID) +
                                        " <br /><br /><br /> " +
                                    " </div> " +
                                " </body> " +
                                " </html> ";

                            // Add an attachment.
                            // TODO: change file path where appropriate
                            if (strAttachment != "" || !string.IsNullOrEmpty(strAttachment))
                            {
                                foreach (string rAttachment in strAttachment.Split(";".ToCharArray()))
                                {
                                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(rAttachment);
                                    mail.Attachments.Add(attachment);
                                }
                            }


                            // Send the message.
                            client.Send(mail);

                            // Set Thread For Sending...
                            System.Threading.Thread.Sleep(500);

                            // Explicitly release objects.
                            mail.Dispose();
                            client.Dispose();

                            // Clear release objects
                            mail = null;
                            client = null;

                            return new DataSendEmail()
                            {
                                Status = "SEND",
                                Msg = "Send Email Success"
                            };
                        }
                        else
                        {
                            return new DataSendEmail()
                            {
                                Status = "PENDING",
                                Msg = "Send Email Canceled, Data Recipient Not Valid!"
                            };
                        }
                    }
                    else
                    {
                        return new DataSendEmail()
                        {
                            Status = "PENDING",
                            Msg = "Send Email Canceled, Data Recipient Not Found!"
                        };
                    }
                }
                else
                {
                    return new DataSendEmail()
                    {
                        Status = "PENDING",
                        Msg = "Send Email Canceled, Data Users Not Found!"
                    };
                }
            }
            catch (Exception err)
            {
                throw err;
                //return new DataSendEmail()
                //{
                //    Status = "PENDING",
                //    Msg = "Send Email Canceled. Error : " + err.Message.ToString()
                //};

            }
        }


        public const string conStringOdBc = "Driver={Microsoft dBase Driver (*.dbf)};SourceType=DBF;SourceDB=" + Tools.DBFFilePath;
        public const string conStringOdBcTxt = "Driver={Microsoft Text Driver (*.txt; *.csv)};SourceDB=" + Tools.TxtFilePath;
        public const string conStringOdBcExcel = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" + Tools.ExcelFilePath;

        public const string NoSessionMessage = "You have no session Please Re-login";
        public const string ErrorPrefix = "Error : ";
        public const string NoPermissionMessage = "You don't have permission";
        public const string NoPermissionLogMessage = "No permission Attempt";
        public const string NoPrivillegeMessage = "You don't have privillege";
        public const string NoPrivillegeLogMessage = "No privillege Attempt";
        public const string NoSessionLogMessage = "No Session Attempt";
        public const string InternalErrorMessage = "Internal Error";
        public const string NoActionMessage = "Can't Find Correct Action";
        public const string CreateReportFailedOrNoData = "Create Report Failed Or No Data";
        public const string NoDataAttempt = "No Data Attempt";

        public static string ConvertDate(DateTime _date)
        {
            // DD MM YYYY
            int _culture = 1;


            // MM DD YYYY
            //public int _culture = 2;
            if (_culture == 1)
            {
                return _date.ToString("dd/MM/yyyy").Replace('-', '/');
            }
            else
            {
                return _date.ToString("MM/dd/yyyy").Replace('-', '/');
            }
        }

        public static void ExportFromExcelToPDF(string excelPath, string pdfPath)
        {
            SpreadsheetInfo.SetLicense("EDWG-UKPC-D7GM-99AP");
            ExcelFile ef = ExcelFile.Load(excelPath);
            ef.Save(pdfPath);
        }

        public static string ConStringExcel2003(string _fileSource)
        {
            return @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + _fileSource + "; Extended Properties=Excel 8.0";
        }
        public static string ConStringCSV(string _fileSource)
        {
            return @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + _fileSource + "; Extended Properties=\"Text;HDR=Yes\"";
        }

        public static string ConStringExcel2007(string _fileSource)
        {
            return string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", _fileSource);
        }

        public static string RemoveApostrophe(string _message)
        {
            return _message.Replace("'", "");
        }

        public static string GetRightString(this string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }

        public static string GetSubstring(this string str, int int1, int length)
        {
            return str.Substring(0 + int1, length);
        }

        public static bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public static bool IsNumeric(this string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, "^\\d+$");
        }

        public static bool IsNumber(string s)
        {
            foreach (char c in s)
            {
                if (!c.Equals('.'))
                {
                    if (!Char.IsDigit(c))
                        return false;
                }
            }
            return true;
        }

        public static bool IsAllLettersOrDigits(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetterOrDigit(c))
                    return false;
            }
            return true;
        }

        public static bool IsAllLetters(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }

        public static bool IsContainLetter(string s)
        {
            foreach (char c in s)
            {
                if (Char.IsLetter(c))
                    return true;
            }
            return false;
        }

        public static bool IsContainDigit(string s)
        {
            foreach (char c in s)
            {
                if (Char.IsDigit(c))
                    return true;
            }
            return false;
        }

        public static bool IsContainNonAlphaNumeric(string s)
        {
            foreach (char c in s)
            {
                if (Char.IsPunctuation(c))
                    return true;
            }
            return false;
        }

        public static bool SessionCheck(string _userID, string _sessionID)
        {
            UsersReps _usersReps = new UsersReps();
            Users mUsers = new Users();
            mUsers = _usersReps.Users_SelectByUserID(_userID);
            if (mUsers == null)
            {
                return false;
            }
            else
            {
                DateTime _sessionTime = DateTime.Parse(mUsers.ExpireSessionTime);
                if (mUsers.SessionID == _sessionID && DateTime.Now <= _sessionTime)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public static string DefaultReportHeaderRightText()
        {
            Host _host = new Host();
            return "&12 " + _host.Get_CompanyName() + " \n";
            //"&12 " + _host.Get_CompanyAddress(); 
        }
        public static string DefaultReportHeaderAddressLeftText()
        {
            Host _host = new Host();
            return "&12 " + _host.Get_CompanyAddress() + " \n";
            //"&12 " + _host.Get_CompanyAddress(); 
        }

        public static string DefaultReportHeaderPhoneLeftText()
        {
            Host _host = new Host();
            return _host.Get_CompanyPhone();

        }

        public static string DefaultReportHeaderFaxLeftText()
        {
            Host _host = new Host();
            return _host.Get_CompanyFax();

        }

        public static Color DefaultReportColumnHeaderFontColor()
        {
            return Color.Black;
        }

        public static float DefaultReportFontSize()
        {
            return 8;
        }

        public static string DefaultReportHeaderRightTextKospin(string _fundName)
        {
            Host _host = new Host();
            return "&12 " + _host.Get_CompanyName() + " \n &12 " + _fundName;
            ; 
        }

        public static string DefaultReportFooterCenterTextKospin()
        {
            return "&14 Disclaimer : Seluruh informasi, keterangan, yang disampaikan melalui media elektronik (“e-mail”) ataupun dalam bentuk hardcopy dari PT. Jasa Capital Asset \n  Management (“Dokumen”) hanya merupakan informasi dan/atau keterangan yang tidak dapat diartikan sebagai suatu saran/advise bisnis tertentu, karenanya \n  Dokumen tersebut tidak bersifat mengikat. Segala hal yang berkaitan dengan diterimanya dan/atau dipergunakannya Dokumen tersebut sebagai pengambilan \n  keputusan bisnis dan/atau investasi merupa kan tanggung jawab pribadi atas segala risiko yang mungkin timbul. Sehubungan dengan risiko dan tanggung jawab pribadi \n  atas Dokumen, pengguna dengan ini menyetujui untuk melepaskan segala tanggung jawab dan risiko hukum kepada PT. Jasa Capital Asset Management atas \n  diterimanya dan/atau dipergunakannya Dokumen.";
        }

        public static ExcelHorizontalAlignment DefaultReportColumnHeaderHorizontalAlignment()
        {
            return ExcelHorizontalAlignment.Center;
        }

        public static ExcelFillStyle DefaultReportColumnHeaderFillStyle()
        {
            return ExcelFillStyle.Solid;
        }

        public static Color DefaultReportColumnHeaderBackgroundColor()
        {
            // return Color.FromArgb(184, 204, 228); ( warna biru muda )
            return Color.White;
        }

        public static float DefaultReportColumnHeaderFontSize()
        {
            return 10;
        }

        public static bool DefaultReportColumnHeaderFontBold()
        {
            return true;
        }

        public static ExcelBorderStyle DefaultReportColumnHeaderBorderTop()
        {
            return ExcelBorderStyle.None;
        }

        public static ExcelBorderStyle DefaultReportColumnHeaderBorderBottom()
        {
            return ExcelBorderStyle.None;
        }

        public static ePaperSize DefaultReportPaperSize()
        {
            if (Tools.ClientCode == "11")
            {
                return ePaperSize.Letter;
            }
            else
            {
                return ePaperSize.A4;
            }
            
        }

        public static bool DefaultReportPageLayoutView()
        {
            return false;
        }

        public static bool DefaultReportShowGridLines()
        {
            return true;
        }

        public static decimal DefaultReportTopMargin()
        {
            return 1;
        }

        public static decimal DefaultReportTopMarginBatchReport()
        {
            return 2;
        }

        public static string DefaultReportHeaderCenterText()
        {
            return "&14 R-REPORTS";
        }

        public static string DefaultReportHeaderLeftText()
        {
            Host _host = new Host();
            return "&12 " + _host.Get_CompanyName() + " \n";
            //"&12 " + _host.Get_CompanyAddress(); 
        }

        public static string DefaultReportHeaderLeftBatchReport()
        {
            Host _host = new Host();
            return _host.Get_CompanyName();
            //"&11 " + _host.Get_CompanyAddress() + " \n " +
            //"&11 " + _host.Get_CompanyPhone() + " \n " +
            //"&11 " + _host.Get_CompanyFax();
        }

        public static string DefaultReportHeaderRightBatchReport()
        {
            Host _host = new Host();
            return "&14 " + "Ref No : " + ".................." + " \n " +
                "&11 " + "Date : " + _host.Get_DateBatchReport() + " \n " +
                "&11 " + "To : " + _host.Get_BankCustodianID();

        }

        public static string DefaultReportFooterCenterText()
        {
            return "&9 RADSOFT-SYSTEM";
        }

        public static string DefaultReportFooterLeftText()
        {
            return "&8 Time : " + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss");
        }

        public static string DefaultReportAuthor()
        {
            return "Radsoft-System";
        }

        public static string DefaultReportComments()
        {
            return "This Report is generated by Radsoft System";
        }

        public static string DefaultReportCompany()
        {
            return "Radsoft-System";
        }

        public static string DefaultReportAssemblyName()
        {
            return "RadSoft";
        }

        // Others Function
        static Host _host = new Host();

        public static ExcelBorderStyle DefaultReportColumnHeaderBorderLineTop()
        {
            return ExcelBorderStyle.Thin;
        }

        public static ExcelBorderStyle DefaultReportColumnHeaderBorderLineBottom()
        {
            return ExcelBorderStyle.Thin;
        }

        public static ExcelBorderStyle DefaultReportColumnHeaderBorderLineLeft()
        {
            return ExcelBorderStyle.Thin;
        }

        public static ExcelBorderStyle DefaultReportColumnHeaderBorderLineRight()
        {
            return ExcelBorderStyle.Thin;
        }

        public static ePaperSize ReportPaperSizeA4Extra()
        {
            return ePaperSize.A4Extra;
        }

        public static string DefaultReportFontName()
        {
            return "Tahoma";
        }

        // Send Email //

        public static bool RegexCheckEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);
            if (re.IsMatch(inputEmail)) { return true; } else { return false; }
        }

        public class DataSendEmail
        {
            public string Status { get; set; }
            public string Msg { get; set; }
        }
        public static string RandomChar()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }


        public static void CreateNewFolder(string directory, string subdirectory)
        {
            if (directory != "" && subdirectory == "")
            {
                // If directory does not exist, create it. 
                if (!Directory.Exists(directory))
                {
                    // buat directory yang baru
                    Directory.CreateDirectory(directory);
                }
                else
                {
                    ////Directory.Delete(directory, true); // hapus directory yang lama
                    //CleanDirectory(subdirectory); // hapus sub directory yang lama
                    //Directory.CreateDirectory(directory); // buat directory yang baru
                }
            }
            else { CreateFolder(directory, subdirectory); }
        }

        public static void CreateFolder(string directory, string subdirectory)
        {
            // If directory does not exist, create it. 
            if (!Directory.Exists(directory))
            {
                // Create directory
                Directory.CreateDirectory(directory);

                // Create sub directory
                if (!Directory.Exists(subdirectory))
                {
                    Directory.CreateDirectory(subdirectory); // buat sub directory baru
                }
            }
            else
            {
                // If sub directory does not exist, create it. 
                if (!Directory.Exists(subdirectory))
                {
                    // buat sub directory baru
                    Directory.CreateDirectory(subdirectory);
                }
                else // jika ada, hapus sub folder lama beserta file didalamnya
                {
                    //Directory.Delete(subdirectory, true); // hapus sub directory yang lama
                    DeleteFolder(subdirectory); // hapus sub directory yang lama
                    Directory.CreateDirectory(subdirectory); // buat sub directory yang baru
                }
            }
        }

        public static void CreateFolderToZIP(string directory)
        {
            try
            {
                string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories); // cek ada file apa ngga di dalem foldernya
                // buat folder zip kalo di foldernya ada file atau datanya
                if (files != null)
                {
                    FileInfo zipFile = new FileInfo(directory + ".zip");
                    if (zipFile.Exists)
                    {
                        zipFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                        System.IO.Compression.ZipFile.CreateFromDirectory(directory, directory + ".zip");
                    }
                    else
                    {
                        System.IO.Compression.ZipFile.CreateFromDirectory(directory, directory + ".zip");
                    }
                }
            }
            catch { throw; }
        }

        public static void DeleteFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.IsReadOnly = false;
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                DeleteFolder(di.FullName);
                di.Delete();
            }

            WaitForDirectoryToBecomeEmpty(dir);
        }

        public static void CleanDirectory(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            WaitForDirectoryToBecomeEmpty(di);
        }

        public static void WaitForDirectoryToBecomeEmpty(DirectoryInfo di)
        {
            for (int i = 0; i < 5; i++)
            {
                if (di.GetFileSystemInfos().Length == 0)
                    return;
                Thread.Sleep(50 * i);
            }
        }

        public static DayCountBasis BondInterestBasisExcelConvertion(int _interestBasis)
        {
            switch (_interestBasis)
            {

                case 2:
                    return DayCountBasis.ActualActual;
                case 3:
                    return DayCountBasis.Actual360;
                case 4:
                    return DayCountBasis.Actual365;
                case 5:
                    return DayCountBasis.Europ30_360;
                default:
                    return DayCountBasis.UsPsa30_360;
            }

        }

        public static Frequency BondPaymentPeriodExcelConversion(int _period)
        {
            switch (_period)
            {

                case 19:
                    return Frequency.Annual;
                case 16:
                    return Frequency.SemiAnnual;
                case 13:
                    return Frequency.Quarterly;
                case 10:
                    return Frequency.Quarterly;
                default:
                    return Frequency.Annual;
            }

        }

        public static string GetTableName(string connectionString, int row = 0)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open();
                return conn.GetSchema("Tables").Rows[row]["TABLE_NAME"] + "";
            }
            catch { }
            finally { conn.Close(); }
            return "sheet1";
        }

    }
}