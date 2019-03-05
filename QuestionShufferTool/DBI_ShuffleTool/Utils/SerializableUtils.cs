using System;
using System.Collections.Generic;
using System.IO;
using DBI_ShuffleTool.Entity;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
        public static void Serialize(Object obj, string path)
        {
            FileStream fs = new FileStream(path + @"\TestPackage.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, obj);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public static List<Question> Deserialize(string path)
        {
            // Declare the hashtable reference.
            List<Question> questions = null;

            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                questions = (List<Question>)formatter.Deserialize(fs);
                return questions;
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }


        ///// <summary>
        ///// Serialize
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static String SerializeJson(Object obj)
        //{
        //    string jsonString = JsonConvert.SerializeObject(obj);
        //    return jsonString;
        //}

        ///// <summary>
        ///// Write to File
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public static bool WriteJson(Object obj, string path)
        //{
        //    try
        //    {
        //        File.WriteAllText(path + "\\TestPackage.dat", SerializeJson(obj));
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Deserialize
        ///// </summary>
        ///// <param name="localPath"></param>
        ///// <returns></returns>
        //public static List<Question> DeserializeJson(String localPath)
        //{
        //    // read file into a string and deserialize JSON to a type
        //    return JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(localPath));
        //}
    }
}
