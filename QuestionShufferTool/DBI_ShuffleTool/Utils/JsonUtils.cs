using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using DBI_ShuffleTool.Entity;

namespace DBI_ShuffleTool.Utils
{
    class JsonUtils
    {
        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String SerializeJson(Object obj)
        {
            String jsonString = JsonConvert.SerializeObject(obj);
            return jsonString;
        }

        /// <summary>
        /// Write to File
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool WriteJson(Object obj, string path)
        {
            try
            {
                File.WriteAllText(path + "\\TestPackage.dat", SerializeJson(obj));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public static List<Question> DeserializeJson(String localPath)
        {
            // read file into a string and deserialize JSON to a type
            return JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(localPath));
        }
    }
}
