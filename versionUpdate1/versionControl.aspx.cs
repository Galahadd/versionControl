using System;
using System.Data.SqlClient;
using System.Reflection;
using log4net;
using VersiyonController;
using System.Net.Mail;
using System.Resources;
using System.Xml;
using System.Collections;
using System.IO;
using Google.Cloud.Translation.V2;

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
            Mail sendmail = new Mail();
         
            //baglantiZımbcırtı.Close();
            int tmpVer = 0;

            string path2 = System.AppDomain.CurrentDomain.BaseDirectory;


            XmlTextReader xmlReader = new XmlTextReader("C:\\Users\\recep.kaya.BIMSADOM\\source\\repos\\versionUpdate1\\versionUpdate1\\DefaultResources.resx");
            xmlReader.Read();
            while (xmlReader.Read())
            {

                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "data")
                {
                    xmlReader.MoveToAttribute("name");
                    string a = xmlReader.Value;
                }
            }
            xmlReader.Close();

            string[] resources =
                Directory.GetFiles(
                    "C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\PratisGit\\oyak\\oyakcimento\\EPWAP\\AI\\App_LocalResources\\tr-TR");
            DirectoryInfo di = new DirectoryInfo("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\PratisGit\\oyak\\oyakcimento\\EPWAP\\AI\\App_LocalResources\\tr-TR");
            FileInfo[] res = di.GetFiles();
            string[] resourcesOtherLang = Directory.GetFileSystemEntries("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\PratisGit\\oyak\\oyakcimento\\EPWAP\\AI\\App_LocalResources\\en-US");
            int resourcesAdet = resources.Length;
            foreach (FileInfo fi in res)
            {


                //for (int i = 0; i < resourcesAdet; i++)
                //{

                ResXResourceReader rsxr = new ResXResourceReader("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\PratisGit\\oyak\\oyakcimento\\EPWAP\\AI\\App_LocalResources\\tr-TR\\" + fi.Name + "");


                ResXResourceWriter resx = new ResXResourceWriter("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\PratisGit\\oyak\\oyakcimento\\EPWAP\\AI\\App_LocalResources\\en-US\\" + fi.Name.Substring(0, fi.Name.Length - 4) + "en-US.resx");

                foreach (DictionaryEntry d in rsxr)
                {
                    string a = d.Value.ToString();
                    if (a != "")
                    {
                        string jsonPath =
                            "C:\\Users\\recep.kaya.BIMSADOM\\source\\repos\\versionUpdate1\\versionUpdate1\\App_LocalResources\\sa.json";
                        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);
                        TranslationClient tran = TranslationClient.Create();
                        string sonuc = tran.TranslateText(a, "en", "tr").TranslatedText;
                        resx.AddResource(d.Key.ToString(), sonuc);

                    }

                    else
                    {
                        resx.AddResource(d.Key.ToString(), a);
                    }
                }
                rsxr.Close();
                resx.Close();
            }


            //ResXResourceReader rsxr = new ResXResourceReader("C:\\Users\\recep.kaya.BIMSADOM\\source\\repos\\versionUpdate1\\versionUpdate1\\App_LocalResources\\versionControl.aspx.en-US.resx");


            //ResXResourceWriter resx = new ResXResourceWriter(
            //    "C:\\Users\\recep.kaya.BIMSADOM\\source\\repos\\versionUpdate1\\versionUpdate1\\App_LocalResources\\versionControl.aspx.es-ES.resx");

            //foreach (DictionaryEntry d in rsxr)

            //{

            //    string a = d.Value.ToString();
            //    if (a != "")
            //    {
            //        string jsonPath =
            //            "C:\\Users\\recep.kaya.BIMSADOM\\source\\repos\\versionUpdate1\\versionUpdate1\\App_LocalResources\\sa.json";
            //        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);
            //        TranslationClient tran = TranslationClient.Create();
            //        string sonuc = tran.TranslateText(a, "es", "en").TranslatedText;

            //        resx.AddResource(d.Key.ToString(), sonuc);

            //    }

            //    else { resx.AddResource(d.Key.ToString(), a); }

            //}
            //rsxr.Close();
            //resx.Close();
            //var sonucmd = currentget.GetProgramVersion(new SqlConnection(connectionString));
            //int lastVersion = sonucmd;

            //////"lastVersion" bilgisini DB'den "Versions" tablosunun tarih olarak en son girilen kaydını alıyor.
            ////SqlConnection baglanti = new SqlConnection("Data Source=PC_RKAYAW10E\\MYSERVERR;Initial Catalog=Program;Persist Security Info=True;User ID=sa;Password=Bimsa.1998");
            ////baglanti.Open();
            ////SqlCommand komut = new SqlCommand();
            ////komut.CommandText = "select top 1 Versions from version order by Tarih desc";
            ////komut.Connection = baglanti;
            ////int lastVersion = Convert.ToInt32(komut.ExecuteScalar());
            ////baglanti.Close();

            ////Dosyadan scriptleri okuyor
            ////DirectoryInfo di = new DirectoryInfo("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\scripts");
            ////FileInfo[] files = di.GetFiles();
            ////SqlConnection baglanti1 = new SqlConnection("Data Source=PC_RKAYAW10E\\MYSERVERR;Initial Catalog=Program;Persist Security Info=True;User ID=sa;Password=Bimsa.1998");
            ////baglanti1.Open();
            ////SqlTransaction tran = baglanti1.BeginTransaction();


            ////do
            ////{
            ////for (int i = programVersion; i <= adet; i++)
            ////{
            ////programVersion = i;

            //var path = "C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\scripts";
            //        //currentget.FileFind(path,programVersion);
            //        currentget.ScriptReader(path, connectionString);
            //        currentget.GetScriptCount(path);
            //    Mail mail1 = new Mail();


            //try
            //{
            //    do                //}


            //} while (lastVersion != programVersion);
            //    {
            //for (int i = programVersion; i < files.Length; i++)
            //{
            //    //Dosyadaki scriptleri işliyor
            //    programVersion = i;
            //    int dosyaNo = i;
            //    FileInfo file = new FileInfo("C:\\Users\\recep.kaya.BIMSADOM\\Desktop\\scripts\\" + dosyaNo + ".sql");
            //    string script = file.OpenText().ReadToEnd();
            //    SqlCommand komut1 = new SqlCommand(script, baglanti1, tran);
            //    komut1.ExecuteNonQuery();


            //}

            //    } while (lastVersion != programVersion);

            //    tran.Commit();

            //    //"BAŞARILI" işlem bilgi maili class smtpparams oluştur 
            //MailMessage mail = new MailMessage("sabancidxtest@gmail.com", "recepkaya57@gmail.com");
            //mail.To.Add(new MailAddress("recepkaya57@gmail.com"));
            //mail.From = new MailAddress("sabancidxtest@gmail.com", "Recep Kaya");
            //mail.Subject = "Sonuç";
            //mail.BodyEncoding = UTF8Encoding.UTF8;
            //mail.Body = "Transaction Başarılı.Hata yok!!";
            //SmtpClient smtp = new SmtpClient("sabancidxtest@gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("sabancidxtest@gmail.com", "134679852recep"),
            //    Port = 587,
            //    Host = "smtp.gmail.com",
            //    EnableSsl = true,

            //};
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.Send(mail);
            //mail.Dispose();

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
            //    for (int i = 0; i < ex.Errors.Count; i++)
            //    {

            //        log.Error("|" + programVersion + "| .scriptin|" + ex.Errors[i].LineNumber + "| . satırında hata var! Hata Türü : " + ex.Errors[i].Message + " ", ex);
            //    }

            //    //exception durumunda hata maili
            //MailMessage mail = new MailMessage("sabancidxtest@gmail.com", "recepkaya57@gmail.com");
            //mail.To.Add(new MailAddress("recepkaya57@gmail.com"));
            //mail.From = new MailAddress("sabancidxtest@gmail.com", "versionControl");
            //mail.Subject = "Hata";
            //mail.BodyEncoding = UTF8Encoding.UTF8;
            //mail.Body = "İŞLEM BAŞARISIZ.!(Bu script içinde TOPLAM : " + ex.Errors.Count + " hata var.)";
            //SmtpClient smtp = new SmtpClient("sabancidxtest@gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("sabancidxtest@gmail.com", "134679852recep"),
            //    Port = 587,
            //    Host = "smtp.gmail.com",
            //    EnableSsl = true,

            //};
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.Send(mail);
            //mail.Dispose();

            //}
            //finally
            //{
            //    baglanti1.Close();


            //}



        }


    }

        }

    //    }
    //}