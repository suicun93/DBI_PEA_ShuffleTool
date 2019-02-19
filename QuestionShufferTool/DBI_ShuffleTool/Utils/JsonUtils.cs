using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DBI_ShuffleTool.Utils
{
    class JsonUtils
    {
        //Object to JsonString
        public String serializeJson(Object obj)
        {
            String jsonString = JsonConvert.SerializeObject(obj);
            return jsonString;
        }

        //public DeserializeJson(String localPath)
        //{
        //    // read file into a string and deserialize JSON to a type
        //    Movie movie1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\movie.json"));

        //    // deserialize JSON directly from a file
        //    using (StreamReader file = File.OpenText(@"c:\movie.json"))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        Movie movie2 = (Movie)serializer.Deserialize(file, typeof(Movie));
        //    }
        //}
    }
}
