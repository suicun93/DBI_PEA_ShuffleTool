using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using DBI_ShuffleTool.Entity;

namespace DBI_ShuffleTool.Utils
{
    class JsonUtils
    {
        //Object to JsonString
        public static String SerializeJson(Object obj)
        {
            String jsonString = JsonConvert.SerializeObject(obj);
            return jsonString;
        }

        public static bool WriteJson(Object obj, String path)
        {
            try
            {
                File.WriteAllText(path + "\\ExamItems.dat", SerializeJson(obj));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<Question> DeserializeJson(String localPath)
        {
            // read file into a string and deserialize JSON to a type
            return JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(localPath));

            //// deserialize JSON directly from a file
            //using (StreamReader file = File.OpenText(@"c:\movie.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    Movie movie2 = (Movie)serializer.Deserialize(file, typeof(Movie));
            //}
        }
    }
}
