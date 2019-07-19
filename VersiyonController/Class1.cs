using log4net;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace VersiyonController
{
    public class Class1 : Mail
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
        private int catchstatus = 0;

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
        public void ScriptReader(string docPath, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            int suanVer = GetCurrentVersion(connection);
            int LastVer = GetLastVersion(connection);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            string[] dosyalar = Directory.GetFiles(docPath);
            try
            {
                for (int j = suanVer; j < LastVer; j++)
                {
                    suanVer += 1;
                    FileInfo file = new FileInfo(dosyalar[j]);
                    string sqlCommand = file.OpenText().ReadToEnd();
                    SqlCommand cmdScript = new SqlCommand(sqlCommand, connection, transaction);
                    cmdScript.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                //SendMail();
                catchstatus += 1;
                transaction.Rollback();
                transaction.Dispose();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    
                    log.Error("||" + suanVer + " .scriptin|" + ex.Errors[i].LineNumber + "| . satırında hata var! Hata Türü : " + ex.Errors[i] + " ", ex);
                }
                
                string message = ex.Message;
            }
            finally
            {
                connection.Close();

            }

            if (catchstatus == 0)
            {
                suanVer -= 1;
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                string guncelle = "update currentVer set currentVer=" + dosyalar.Length + "where currentVer=" +suanVer + "";
                SqlCommand komutUpdate = new SqlCommand(guncelle, connection1);
                komutUpdate.ExecuteNonQuery();
                connection1.Close();
            }
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
            //M.From = new MailAddress("FromAdress", "Name");
            //M.To.Add(new MailAddress("ToAdress"));
            M.IsBodyHtml = true;
        }

        public bool IsHTML
        {
            set { M.IsBodyHtml = value; }
        }

        public string ToMail
        {
            set { M.To.Add(value);}
        
        }

        public string FromMail
        {
            set { M.From = new MailAddress(value); }

        }

    public string Subject
    {
        set { M.Subject = value; }


    }

        public string Body
        { 
            set { M.Body = value; }
            get { return M.Body; }
        }

        //public  void SendMail()
        //{
        //    MailMessage mail = new MailMessage("FromAdress","ToAdress");
        //    SmtpClient smtp = new SmtpClient("fromAdress", 587)
        //    {
        //        Credentials = new NetworkCredential("FromAdress", "fromAdressPassword"),
        //        Port = 587,
        //        Host = "SmtpClient",
        //        EnableSsl = true,
        //    };
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //       smtp.Send(M);
        //       mail.Dispose();
        //}
    }




}