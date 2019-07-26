using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Google.Cloud.Translation.V2;
namespace VersiyonController
{
 
    
    public class Class1 : Mail
    {
        
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private bool catchstatus = false;

        public int GetProgramVersion(SqlConnection connectionString)
        {

            
            string cmdLastVer = "select top 1 Versions from version order by Tarih desc";
            SqlCommand cmdLastVersion = new SqlCommand();
            connectionString.Open();
            cmdLastVersion.Connection = connectionString;
            cmdLastVersion.CommandText = cmdLastVer;
            var lastVersion = Convert.ToInt32(cmdLastVersion.ExecuteScalar());
            connectionString.Close();
            return lastVersion;
        }

        public int GetScriptCount(string docPath)
        {
            string[] scripts = Directory.GetFiles(docPath);
            return scripts.Length;
        }
        public void ScriptReader(string docPath, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            int lastVer = GetScriptCount(docPath);
            int programVersion = GetProgramVersion(connection);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            string[] dosyalar = Directory.GetFiles(docPath);
            try
            {
                for (int j = programVersion; j < lastVer; j++)
                {
                    FileInfo file = new FileInfo(dosyalar[j]);
                    string sqlCommand = file.OpenText().ReadToEnd();
                    SqlCommand cmdScript = new SqlCommand(sqlCommand, connection, transaction);
                    cmdScript.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (SqlException ex)
            {

                catchstatus = true;
                transaction.Rollback();
                transaction.Dispose();
                programVersion += 1;
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    
                    log.Error("||" + programVersion + " .scriptin|" + ex.Errors[i].LineNumber + "| . satırında hata var! Hata Türü : " + ex.Errors[i] + " ", ex);
                }
            }
            finally
            {
                connection.Close();
            }
            
            if (catchstatus == false)
            {
               
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                string guncelle = "insert into Version Values("+lastVer+",' "  + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") +" ')";
                SqlCommand komutUpdate = new SqlCommand(guncelle, connection1);
                komutUpdate.ExecuteNonQuery();
                connection1.Close();
            }
        }

    }
    public class Mail 
    {
        private readonly MailMessage M;

         public Mail()
         {
            M = new MailMessage();
            M.From = new MailAddress("sabancidxtest@mgmail.com", "VersionControl");
            M.To.Add(new MailAddress("recepkaya57@gmail.com"));
            M.IsBodyHtml = true;
         } 

         public bool IsHTML
         {
            set => M.IsBodyHtml = value;
         }

         public string ToMail
         {
            set => M.To.Add(value);
         }

         public string FromMail
         {
            set => M.From = new MailAddress(value);
         }

         public string Subject
         {
        set => M.Subject = value;
         }

         public string Body
         { 
            set => M.Body = value;
            get => M.Body;
         }

        public void SendMail()
        {
            MailMessage mail = new MailMessage("sabancidxtest@gmail.com", "recepkaya57@gmail.com");
            SmtpClient smtp = new SmtpClient("sabancidxtest@gmail.com", 587)
            {
                Credentials = new NetworkCredential("sabancidxtest@gmail.com", "134679852recep"),
                Host = "smtp.gmail.com",
                EnableSsl = true,
            };
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(M);
            mail.Dispose();
        }
    }




}