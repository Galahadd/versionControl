using System.Collections.Generic;
using System.IO;


namespace versionController
{
    public class Class1 
    {
        public List<string> dosyaArama(string path)
        {
            List<string> klasor = new List<string>();
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] getdir = di.GetDirectories();
            foreach (DirectoryInfo veri in getdir)
            {
                klasor.Add(veri.ToString());
            }
            return klasor;
        }

       public string Program(string connectionStringName,string currentVerCmdText)
        {
            using (System.Data.SqlClient.SqlCommand theCommand = new System.Data.SqlClient.SqlCommand(sql, theConnection))
            {
                theConnection.Open();
                theCommand.CommandType = CommandType.Text;
                theCommand.ExecuteNonQuery();
            }

            var baglantiZımbırtı = new SqlConnection();

            baglantiZımbırtı.Open();
            SqlCommand komut2 = new SqlCommand
            {
                CommandText = "select  currentVer from currentVer",
                Connection = baglanti
            };
            return "dsad";
        }
    }
}
