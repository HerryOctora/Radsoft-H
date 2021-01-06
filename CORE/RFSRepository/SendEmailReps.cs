//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data;
//using RFSModel;
//using RFSUtility;
//using System.Data.SqlClient;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using System.IO;
//using System.Drawing;
//using OfficeOpenXml.Drawing;
//using System.Data.OleDb;
//using System.Net.Mail;
//using System.Net;
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

namespace RFSRepository
{
    public class SendEmailReps
    {

        static Host _host = new Host();

        public class DataSendEmail
        {
            public string Status { get; set; }
            public string Msg { get; set; }
        }

        public static DataSendEmail SendEmailTestingByInput(string usersID, string recipientTo, string subjectMsg, string bodyMsg, string strAttachment, string recipientBcc)
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
                            string hostname, username, password, emailFrom = string.Empty;



                            hostname = Tools._hostNameEmail;
                            username = Tools._userNameEmail;
                            password = Tools._passwordEmail;
                            if (Tools.ClientCode == "02")
                            {
                                emailFrom = Tools._emailFrom02;
                            }
                            else if (Tools.ClientCode == "03")
                            {
                                emailFrom = Tools._emailFrom03;
                            }
                            else if (Tools.ClientCode == "12")
                            {
                                emailFrom = Tools._emailFrom12;
                            }
                            else
                            {
                                emailFrom = username;
                            }

                            client.Port = Tools._portEmail;
                            client.UseDefaultCredentials = Tools._defaultCredentialsEmail;
                            client.Credentials = new System.Net.NetworkCredential(username, password);


                            client.Host = hostname;
                            client.Timeout = (60 * 5 * 10000); // set timeout 5 menit
                            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;



                            mail.From = new System.Net.Mail.MailAddress(emailFrom);
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
                            //if (recipientCc != "" || !string.IsNullOrEmpty(recipientCc))
                            //{
                            //    foreach (string rCc in recipientCc.Split(";".ToCharArray()))
                            //    {
                            //        if (Tools.RegexCheckEmail(rCc))
                            //        {
                            //            mail.CC.Add(rCc);
                            //        }
                            //    }
                            //}

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
                            mail.Body = bodyMsg;


                            //Add an attachment.
                            //TODO: change file path where appropriate
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
                return new DataSendEmail()
                {
                    Status = "PENDING",
                    Msg = "Send Email Canceled. Error : " + err.Message.ToString()
                };

            }
        }


        public static DataSendEmail SendMailBySKExpiredDate(string recipientTo, string recipientCc, string recipientBcc, string subjectMsg, string strAttachment, string usersID)
        {
            try
            {
                string _bodyMsg;
                int usersPK = 0;
                try
                {
                    usersPK = _host.Get_UsersPK(usersID);
                }
                catch { throw; }

                EmployeeReps dt = new EmployeeReps();
                _bodyMsg = dt.Employee_CheckSKExpiredDate();

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
                                hostname = Tools._hostNameEmail;
                                username = Tools._userNameEmail;
                                password = Tools._passwordEmail;
                                client.Port = Tools._portEmail;
                                client.UseDefaultCredentials = Tools._defaultCredentialsEmail;
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
                                        " <div> Radsoft mendeteksi SK pegawai sebagai berikut mendekati masa <br /> tenggang: <br />  " +
                                            _bodyMsg +
                                        " </div> " +
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


        public static DataSendEmail SchedulerEmail(string usersID, string recipientTo, string subjectMsg, string bodyMsg, string strAttachment, string recipientBcc)
        {
            try
            {

                if (recipientTo != "" || !string.IsNullOrEmpty(recipientTo))
                {
                    if (Tools.RegexCheckEmail(recipientTo))
                    {
                        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                        string hostname, username, password, emailFrom = string.Empty;



                        hostname = Tools._hostNameEmail;
                        username = Tools._userNameEmail;
                        password = Tools._passwordEmail;
                        emailFrom = username;

                        client.Port = Tools._portEmail;
                        client.UseDefaultCredentials = Tools._defaultCredentialsEmail;
                        client.Credentials = new System.Net.NetworkCredential(username, password);


                        client.Host = hostname;
                        client.Timeout = (60 * 5 * 10000); // set timeout 5 menit
                        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;



                        mail.From = new System.Net.Mail.MailAddress(emailFrom);
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
                        //if (recipientCc != "" || !string.IsNullOrEmpty(recipientCc))
                        //{
                        //    foreach (string rCc in recipientCc.Split(";".ToCharArray()))
                        //    {
                        //        if (Tools.RegexCheckEmail(rCc))
                        //        {
                        //            mail.CC.Add(rCc);
                        //        }
                        //    }
                        //}

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
                        mail.Body = bodyMsg;


                        //Add an attachment.
                        //TODO: change file path where appropriate
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
            catch (Exception err)
            {
                throw err;
                return new DataSendEmail()
                {
                    Status = "PENDING",
                    Msg = "Send Email Canceled. Error : " + err.Message.ToString()
                };

            }
        }


    }
}