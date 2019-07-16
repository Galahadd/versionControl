using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using log4net;
using System.Net;
using VersiyonController;


namespace versionUpdate1
{

    public partial class versionControl : System.Web.UI.Page
    {

        
      // private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
       private readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        protected void Page_Load(object sender, EventArgs e)
        {


        }


        public void Button1_Click(object sender, EventArgs e)
        {
            ////CurrentVer(programVersion) değerini DB'den alıyor(aşağıda bu değer işlem başarılı olduğu takdirde güncelleniyor)
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ProgramConnectionString"].ConnectionString;
            string connectionString1 = System.Configuration.ConfigurationManager.ConnectionStrings["ProgramConnectionString1"].ConnectionString;

            //SqlConnection baglantiZımbırtı = new SqlConnection(connectionString);
            //baglantiZımbırtı.Open();
            //SqlCommand komut2 = new SqlCommand();
            //komut2.CommandText = "select  currentVer from currentVer";
            //komut2.Connection = baglantiZımbırtı;
            //int programVersion = Convert.ToInt32(komut2.ExecuteScalar());
            Class1 currentget = new Class1();

            var currentVer = currentget.GetCurrentVersion(new SqlConnection(connectionString1));
            int programVersion = currentVer;
           
            //baglantiZımbırtı.Close();
            int tmpVer = 0;
            tmpVer = programVersion;

           
            
            var adet = currentget.DocCount("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\scripts");

            var sonucmd = currentget.GetLastVersion(new SqlConnection(connectionString));
            int lastVersion = sonucmd;
                
            ////"lastVersion" bilgisini DB'den "Versions" tablosunun tarih olarak en son girilen kaydını alıyor.
            //SqlConnection baglanti = new SqlConnection("Data Source=PC_RKAYAW10E\\MYSERVERR;Initial Catalog=Program;Persist Security Info=True;User ID=sa;Password=Bimsa.1998");
            //baglanti.Open();
            //SqlCommand komut = new SqlCommand();
            //komut.CommandText = "select top 1 Versions from version order by Tarih desc";
            //komut.Connection = baglanti;
            //int lastVersion = Convert.ToInt32(komut.ExecuteScalar());
            //baglanti.Close();

            //Dosyadan scriptleri okuyor
            //DirectoryInfo di = new DirectoryInfo("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\scripts");
            //FileInfo[] files = di.GetFiles();
            //SqlConnection baglanti1 = new SqlConnection("Data Source=PC_RKAYAW10E\\MYSERVERR;Initial Catalog=Program;Persist Security Info=True;User ID=sa;Password=Bimsa.1998");
            //baglanti1.Open();
            //SqlTransaction tran = baglanti1.BeginTransaction();
            programVersion += 1;
           
            if (programVersion != lastVersion)
            {
                //do
                //{
                    for (int i = programVersion; i <= adet ; i++)
                    {
                        programVersion = i;
                        var path = "C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\scripts\\" + programVersion + ".sql";
                        var transonuc = currentget.ScriptReader(path, connectionString);
                    }
                  
                    
                //} while (lastVersion != programVersion);
                             
                //try
                //{
                //    do
                //    {
                //        for (int i = programVersion  ; i < files.Length; i++)
                //        {
                //            //Dosyadaki scriptleri işliyor
                //            programVersion = i ;
                //            int dosyaNo = i;
                //            FileInfo file = new FileInfo("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\scripts\\" + dosyaNo + ".sql");
                //            string script = file.OpenText().ReadToEnd();
                //            SqlCommand komut1 = new SqlCommand(script, baglanti1, tran);
                //            komut1.ExecuteNonQuery();


                //        }

                //    } while (lastVersion != programVersion);

                //    tran.Commit();

                //    //"BAŞARILI" işlem bilgi maili class smtpparams oluştur 
                //    MailMessage mail = new MailMessage("sabancidxtest@gmail.com", "recepkaya57@gmail.com");
                //    mail.To.Add(new MailAddress("recepkaya57@gmail.com"));
                //    mail.From = new MailAddress("sabancidxtest@gmail.com", "Recep Kaya");
                //    mail.Subject = "Sonuç";
                //    mail.BodyEncoding = UTF8Encoding.UTF8;
                //    mail.Body = "Transaction Başarılı.Hata yok!!";
                //    SmtpClient smtp = new SmtpClient("sabancidxtest@gmail.com", 587)
                //    {
                //        Credentials = new NetworkCredential("sabancidxtest@gmail.com", "134679852recep"),
                //        Port = 587,
                //        Host = "smtp.gmail.com",
                //        EnableSsl = true,

                //    };
                //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    smtp.Send(mail);
                //    mail.Dispose();

                //    //transaction başarılı olursa currentVer alanını en son versiyona eşitliyor ve update tamamlanmış oluyor.
                //    SqlConnection baglanti3 = new SqlConnection("Data Source=PC_RKAYAW10E\\MYSERVERR;Initial Catalog=Program;Persist Security Info=True;User ID=sa;Password=Bimsa.1998");
                //    baglanti3.Open();
                //    string guncelle = "update currentVer set currentVer=" + lastVersion + "where currentVer=" + tmpVer + "";
                //    SqlCommand komut3 = new SqlCommand(guncelle, baglanti3);
                //    SqlDataAdapter da = new SqlDataAdapter(komut3);
                //    komut3.ExecuteNonQuery();
                //    baglanti3.Close();
                //}

                //catch (SqlException ex)
                //{

                //    tran.Rollback();
                //    tran.Dispose();

                //    //log dosyası oluşturuluyor.

                //    log.Error("TOPLAM " + ex.Errors.Count + " hata var.");
                //    for (int i = 0 ; i < ex.Errors.Count ; i++)
                //    {

                //        log.Error("|" + programVersion + "| .scriptin|" + ex.Errors[i].LineNumber + "| . satırında hata var! Hata Türü : " + ex.Errors[i].Message + " ", ex);
                //    }

                //    //exception durumunda hata maili
                //    MailMessage mail = new MailMessage("sabancidxtest@gmail.com", "recepkaya57@gmail.com");
                //    mail.To.Add(new MailAddress("recepkaya57@gmail.com"));
                //    mail.From = new MailAddress("sabancidxtest@gmail.com", "versionControl");
                //    mail.Subject = "Hata";
                //    mail.BodyEncoding = UTF8Encoding.UTF8;
                //    mail.Body = "İŞLEM BAŞARISIZ.!(Bu script içinde TOPLAM : " + ex.Errors.Count + " hata var.)";
                //    SmtpClient smtp = new SmtpClient("sabancidxtest@gmail.com", 587)
                //    {
                //        Credentials = new NetworkCredential("sabancidxtest@gmail.com", "134679852recep"),
                //        Port = 587,
                //        Host = "smtp.gmail.com",
                //        EnableSsl = true,

                //    };
                //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    smtp.Send(mail);
                //    mail.Dispose();

                //}
                //finally
                //{
                //    baglanti1.Close();


                //}


            }
        }

        
    }



}