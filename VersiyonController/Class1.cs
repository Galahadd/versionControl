using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace VersiyonController
{
    public class Class1
    {

        //public List<string> FileFind(string docPath,int programVersion)
        //{
        //    List<string> dosyalar = new List<string>();
        //    DirectoryInfo di = new DirectoryInfo(docPath);
        //    FileInfo[] files = di.GetFiles();

        //    for (int i = programVersion; i < dosyalar; i++)
        //    {

        //    }            {
        //        dosyalar.Add(script.ToString());

        //    }
             
        //    return dosyalar;
        //}


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
        public string ScriptReader(string docPath, string connectionString,int programVersion)//currentVer
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            string[] dosyalar = Directory.GetFiles(docPath);
            try
            {
                for (int j = programVersion - 1; j < dosyalar.Length; j++)
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
                transaction.Rollback();
                transaction.Dispose();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    
                    log.Error("||" + programVersion + " .scriptin|" + ex.Errors[i].LineNumber + "| . satırında hata var! Hata Türü : " + ex.Errors[i] + " ", ex);
                }
                string message = ex.Message;
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
            M.IsBodyHtml = true;
        }

        public bool IsHTML
        {
            set { M.IsBodyHtml = value; }
        }

        public string ToMail
        {
            set;
            get;
           
           
        }

        public string FromMail
        {

            set;
            get;
            
        }

        public string Subject
        {
            set;
            get;

        }

        public string Body
        { 
            set { M.Body = value; }
            get { return M.Body; }
        }

        public void SendMail()
        {

            MailMessage mail = new MailMessage(FromMail,ToMail, Subject, Body);
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("sabancidxtest@gmail.com", "134679852recep");
          smtpClient.Send(M);
        }
    }




}