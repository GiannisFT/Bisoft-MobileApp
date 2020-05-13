using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Collections.ObjectModel;
using System.Text;

namespace BisoftMobileWCFService.HelpClasses
{
    public class EmailSender
    {
        public bool Sent { get; set; }
        public string SendEmail(EmailSent emailSent)
        {
            try
            {
                MailMessage mail = new MailMessage();

                if (!string.IsNullOrWhiteSpace(emailSent.ToAdress))
                    mail.To.Add(new MailAddress(emailSent.ToAdress));
                else
                    return "Fel: epost saknas";

                SmtpClient smtp = new SmtpClient();
                mail.Subject = emailSent.Subject;
                mail.Body = emailSent.Body;

                mail.From = new MailAddress("info@bisoft.se");
                smtp.Host = "email-smtp.eu-west-1.amazonaws.com";
                smtp.Port = 587;

                smtp.Credentials = new NetworkCredential("AKIAJHWHSHKJSAU7HHKA", "ArgG45hZzmrrMV4OKUaFtkIVELD36l5MzchqShMWOI6l");

                smtp.EnableSsl = emailSent.UseSSL;

                smtp.EnableSsl = true;

                mail.IsBodyHtml = true;

              


                smtp.Send(mail);

                Sent = true;

                return "Email skickat till " + emailSent.ToAdress;

                
            }catch(Exception e)
            {
                Sent = false;
                return "Kunde inte skicka email till" + emailSent.ToAdress + "Meddelande: " + e.Message;
            }
        }

        private string CreatedQREmailNewReport(QualityReport _report)
        {

            string EmailBody = "<html><body>";
            EmailBody += "<h2>Ny kvalitetsrapport</h2>";
            EmailBody += "<table style=\"width:700px\">";
            EmailBody += "<tr><td style=\"font-size:8pt\">Rapportnr</td><td style=\"font-size:8pt\">Skapad</td></tr>";
            EmailBody += "<tr><td></td><td>" + _report.Created.ToString("yyyy-MM-dd hh:mm") + "</td></tr>";
            EmailBody += "</table><br/>";
            EmailBody += "<table><tr><td style=\"font-size:8pt\">" + _report.CQReportTaskRows.First().OfficeDepartmentTask.OfficeDepartment.Name + "</td></tr>"; //// Förtsätt här
            EmailBody += "<tr><td style=\"color:Red;font-size:13pt\"><b>" + _report.CQReportTaskRows.First().OfficeDepartmentTask.Name + "</b></td></tr></table><br/>";
            EmailBody += "<tr><td>" + _report.Description + "</td></tr></table><br/>";
            EmailBody += "<table style=\"width:700px\">";
            EmailBody += "<tr><td style=\"font-size:8pt\">Handläggare</td><td style=\"font-size:8pt\">Skapad av</td></tr>";
            EmailBody += "<tr><td>" + _report.QRResponsible.Employee.FName + " " + _report.QRResponsible.Employee.LName + "</td><td>" + _report.Employee.FName + " " + _report.Employee.LName + "</td></tr>";
            EmailBody += "<tr><td></td><td></td></tr>";
            EmailBody += "</table>";
            EmailBody += "<hr/>";
            EmailBody += "<table style=\"width:700px\"><tr>";
            EmailBody += "<td><b>" + _report.Office.Name + "</b></td>";
            EmailBody += "<td>" + _report.Office.Address + "</td>";
            EmailBody += "<td>" + _report.Office.ZipCode + "</td>";
            EmailBody += "<td>" + _report.Office.City + "</td>";
            EmailBody += "<td>" + _report.Office.Phone + "</td>";
            EmailBody += "<td>" + _report.Office.Email + "</td>";
            EmailBody += "</tr></table>";
            EmailBody += "</body></html>";

            return EmailBody;
        }


        public void SendNewQualityReportMail(QualityReport mailreport)
        {

            try
            {
                KvalPortDbEntities db = new KvalPortDbEntities();

                QualityReport _report = db.QualityReports.Where(r => r.Id == mailreport.Id).FirstOrDefault();

                if(_report != null) 
                {
                    ObservableCollection<QRStatusEmailNotification> SelectedOfficeQREmailNotifications = new ObservableCollection<QRStatusEmailNotification>(from qrEmail in db.QRStatusEmailNotifications
                                                                                                                                                             where qrEmail.OfficeId == _report.OfficeId && qrEmail.StatusId == 1
                                                                                                                                                             select qrEmail);
                    if (SelectedOfficeQREmailNotifications.Count > 0)
                    {
                        ObservableCollection<string> sentEmails = new ObservableCollection<string>();

                        EmailSent es;

                        QRLog qrLog = new QRLog();
                        qrLog.Cost = 0;
                        qrLog.Created = DateTime.Now;
                        qrLog.CreatedBy = _report.CreatedBy; ;
                        qrLog.LogTypeId = 3; // (int)QRLogTypesEnum.Programåtgärd;
                        qrLog.QualityReportId = _report.Id;
                        qrLog.Deleted = false;
                        qrLog.Subject = "Epost har skickats (rapport skapad)";
                        qrLog.Description = "";

                        StringBuilder descrp = new StringBuilder();

                        string EmailBody = CreatedQREmailNewReport(_report);

                        foreach (QRStatusEmailNotification not in SelectedOfficeQREmailNotifications)
                        {

                            es = new EmailSent();
                            es.Automatic = true;

                            es.Created = DateTime.Now;
                            es.FromAdress = "info@bisoft.se";
                            es.ModuleId = 1;
                            es.OfficeId = _report.OfficeId;
                            es.Password = "none";
                            es.Username = "none";
                            es.UseSSL = true;
                            es.Port = 25;
                            es.Server = "localhost";
                            es.Subject = "Kvalitetsrapport";
                            es.ToAdress = "";
                            es.SendMessage = "test";
                            es.Sent = false;

                            if (not.IsEmployee)
                            {
                                if (not.Employee.Email == null || not.Employee.Email.Count() < 1)
                                {
                                    descrp.AppendLine("Epost kunde inte skickas till användaren: " + not.Employee.FName + " " + not.Employee.LName + ". Orsak: Epost-adress saknades");
                                }
                                else if (not.Employee.Email.Count() < 5)
                                {
                                    descrp.AppendLine("Epost kunde inte skickas till användaren: " + not.Employee.FName + " " + not.Employee.LName + ". Orsak: Epost-adress felaktig");
                                }
                                else
                                {
                                    es.Body = EmailBody;
                                    descrp.AppendLine("Epost har skickats till : " + not.Employee.FName + " " + not.Employee.LName + " (" + not.Employee.Email + ")");
                                    es.ToAdress = not.Employee.Email;

                                    if (sentEmails.Contains(es.ToAdress))
                                        es.Sent = true;
                                    else
                                    {
                                        sentEmails.Add(es.ToAdress);
                                        es.SendMessage = SendEmail(es);
                                    }

                                    db.EmailSents.Add(es);
                                   
                                }
                            }
                            else if (not.IsMailContact)
                            {
                                if (not.EmailContact.Email == null || not.EmailContact.Email.Count() < 1)
                                {
                                    descrp.AppendLine("Epost kunde inte skickas till epost-kontakten: " + not.EmailContact.Name + ". Orsak: Epost-adress saknades");
                                }
                                else if (not.EmailContact.Email.Count() < 5)
                                {
                                    descrp.AppendLine("Epost kunde inte skickas till epost-kontakten: " + not.EmailContact.Name + ". Orsak: Epost-adress felaktig");
                                }
                                else
                                {
                                    es.Body = EmailBody;
                                    descrp.AppendLine("Epost har skickats till epost-kontkten: " + not.EmailContact.Email + " (" + not.EmailContact.Email + ")");
                                    es.ToAdress = not.EmailContact.Email;

                                    if (sentEmails.Contains(es.ToAdress))
                                        es.Sent = true;
                                    else
                                    {
                                        es.SendMessage = SendEmail(es);
                                        sentEmails.Add(es.ToAdress);
                                    }
                                        

                                    db.EmailSents.Add(es);
                                }
                            }
                            else if (not.IsReportRole)
                            {
                                if (not.ReportRoll == "Responsible")
                                {
                                    if (_report.QRResponsible.Employee.Email == null || _report.QRResponsible.Employee.Email.Count() < 1)
                                    {
                                        descrp.AppendLine("Epost kunde inte skickas till handläggaren: " + _report.QRResponsible.Employee.FName + " " + _report.QRResponsible.Employee.LName + ". Orsak: Epost-adress saknades");
                                    }
                                    else if (_report.QRResponsible.Employee.Email.Count() < 5)
                                    {
                                        descrp.AppendLine("Epost kunde inte skickas till handläggaren: " + _report.QRResponsible.Employee.FName + " " + _report.QRResponsible.Employee.LName + ". Orsak: Epost-adress felaktig");
                                    }
                                    else
                                    {
                                        es.Body = EmailBody;
                                        descrp.AppendLine("Epost har skickats till handläggaren: " + _report.QRResponsible.Employee.FName + " " + _report.QRResponsible.Employee.LName + " (" + _report.QRResponsible.Employee.Email + ")");
                                        es.ToAdress = _report.QRResponsible.Employee.Email;

                                        if (sentEmails.Contains(es.ToAdress))
                                            es.Sent = true;
                                        else
                                        {
                                            es.SendMessage = SendEmail(es);
                                            sentEmails.Add(es.ToAdress);
                                        }
                                           
                                        db.EmailSents.Add(es);
                                       
                                    }
                                }
                                else if (not.ReportRoll == "CausedBy")
                                {
                                    if (_report.OfficeDepartment.Employee.Email == null || _report.OfficeDepartment.Employee.Email.Count() < 1)
                                    {
                                        descrp.AppendLine("Epost kunde inte skickas till avdelningsansvarig för orsakad av: " + _report.OfficeDepartment.Employee.FName + " " + _report.OfficeDepartment.Employee.LName + ". Orsak: Epost-adress saknades");
                                    }
                                    else if (_report.OfficeDepartment.Employee.Email.Count() < 5)
                                    {
                                        descrp.AppendLine("Epost kunde inte skickas till avdelningsansvarig för orsakad av: " + _report.OfficeDepartment.Employee.FName + " " + _report.OfficeDepartment.Employee.LName + ". Orsak: Epost-adress felaktig");
                                    }
                                    else
                                    {
                                        es.Body = EmailBody;
                                        descrp.AppendLine("Epost har skickats till avdelningsansvarig för orsakad av: " + _report.OfficeDepartment.Employee.FName + " " + _report.OfficeDepartment.Employee.LName);
                                        es.ToAdress = _report.OfficeDepartment.Employee.Email;

                                        if (sentEmails.Contains(es.ToAdress))
                                            es.Sent = true;
                                        else
                                        {
                                            es.SendMessage = SendEmail(es);
                                            sentEmails.Add(es.ToAdress);
                                        }
                                            

                                        db.EmailSents.Add(es);
                                    }
                                }
                                else if (not.ReportRoll == "CreatedBy")
                                {
                                    if (_report.Employee.Email == null || _report.Employee.Email.Count() < 1)
                                    {
                                        descrp.AppendLine("Epost kunde inte skickas till användaren som skapade ärendet: " + _report.Employee.FName + " " + _report.Employee.LName + ". Orsak: Epost-adress saknades");
                                    }
                                    else if (_report.Employee.Email.Count() < 5)
                                    {
                                        descrp.AppendLine("Epost kunde inte skickas till användaren som skapade ärendet: " + _report.Employee.FName + " " + _report.Employee.LName + ". Orsak: Epost-adress felaktig");
                                    }
                                    else
                                    {
                                        es.Body = EmailBody;
                                        descrp.AppendLine("Epost har skickats till användaren som skapade ärendet: " + _report.Employee.FName + " " + _report.Employee.LName + " (" + _report.Employee.Email + ")");
                                        es.ToAdress = _report.Employee.Email;

                                        if (sentEmails.Contains(es.ToAdress))
                                            es.Sent = true;
                                        else
                                        {
                                            es.SendMessage = SendEmail(es);
                                            sentEmails.Add(es.ToAdress);
                                        }
                                            

                                        db.EmailSents.Add(es);
                                    }
                                }

                            }


                        }


                        if (descrp.Length > 5)
                        {
                            qrLog.Description = descrp.ToString();
                            db.QRLogs.Add(qrLog);
                        }

                        db.SaveChanges();

                    }
                }

            }
            catch (Exception ex)
            {
                //IsLoading = false;
                // ErrorWindow.CreateNew("Kunde inte skapa och skicka email. Felmeddelande: " + ex.Message);
            }

        }

        
    }
}