using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace VersiyonController
{
    public class Class1
    {

        private static ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public int GetLastVersion(SqlConnection connectionString)
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
        public int GetCurrentVersion(SqlConnection connectionString1)
        {
            
            string cmdCurrentVer = "select currentVer from currentVer";
            SqlCommand cmdCurrentVersion = new SqlCommand();
            connectionString1.Open();
            cmdCurrentVersion.Connection = connectionString1;
            cmdCurrentVersion.CommandText = cmdCurrentVer;
            var CurrentVersion = Convert.ToInt32(cmdCurrentVersion.ExecuteScalar());
            connectionString1.Close();
            var de = connectionString1;

            return CurrentVersion;
        }
        public string ScriptReader(string docPath, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                var message = "";
                try
                {
                    FileInfo file = new FileInfo(docPath);
                    string sqlCommand = file.OpenText().ReadToEnd();
                    SqlCommand cmdScript = new SqlCommand(sqlCommand, connection, transaction);
                    cmdScript.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (SqlException ex)
                {

                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        log.Error("|| .scriptin|" + ex.Errors[i].LineNumber + "| . satırında hata var! Hata Türü : " + ex.Errors[i] + " ", ex);
                    }
                    transaction.Rollback();
                    transaction.Dispose();
                    message = ex.Message;
                }
                finally
                {
                    connection.Close();

                }

            
             string message1 = "transaction başarılı";
            log.Info("Transaction Başarılı");
            
            return message1;

        }
        public int DocCount(string docPath)
        {
            int dosyaSayisi = Directory.GetFiles(docPath).Length;
            string[] dosya = Directory.GetDirectories(docPath);
            for (int i = 0; i < dosya.Length; i++)
            {
                dosyaSayisi += DocCount((dosya[i]));
            }
            return dosyaSayisi;
        }
        
    }

        public class Mail
        {
            private readonly MailMessage M;

            public Mail()
            {
                M = new MailMessage(); 
                M.Sender = M.From = new MailAddress(ConfigurationManager.AppSettings["sabancidxtest@gmail.com"]);
                M.IsBodyHtml = true;
            }

            public bool IsHTML
            {
                set { M.IsBodyHtml = value; }
            }

            public string ToMail
            {
                set
                {
                    M.To.Add(value);
                }
            }

            public string FromMail
        {
               
                set { M.Sender = M.From = new MailAddress(value); }
        }

        public string Subject
            {
            set { M.Subject = value; }
            }

            public string Body
            {
                get { return M.Body; }
                set { M.Body = value; }
            }

            public void SendMail()
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = ConfigurationManager.AppSettings["smtpHost"].ToString();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["mail"].ToString(),
                ConfigurationManager.AppSettings["password"].ToString());
                smtpClient.Send(M);
            }
        }
        


        
    }
    

