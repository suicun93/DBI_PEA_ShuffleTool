using System.Collections.Generic;
using System.IO;
using DBI_ShuffleTool.Entity;
using DBI_ShuffleTool.Entity.Question;
using Newtonsoft.Json;

namespace DBI_ShuffleTool.Utils
{
    class SerializableUtils
    {
        /// <summary>
        /// Write to File
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool WriteJson(object obj, string path)
        {
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(obj));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// DeserializeJson
        /// </summary>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public static QuestionSet DeserializeJson(string localPath)
        {
            // read file into a string and deserialize JSON to a type
            return JsonConvert.DeserializeObject<QuestionSet>(File.ReadAllText(localPath));
        }
    }
}
