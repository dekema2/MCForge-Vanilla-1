

using System.IO;
using System.Net;

namespace MCForge.Utilities {
   public class FileUtils {

       public const string PropertiesPath = "properties/";

       public static void CreateFileFromWeb(string url, string saveLocation){
           using (var client = new WebClient())
               client.DownloadFile(url, saveLocation);
       }
       public static void CreateFileFromBytes(byte[] bytes, string saveLocation){
           using(var stuff = File.Create(saveLocation))
                stuff.Write(bytes, 0, bytes.Length);
       }
    }
}
