using Newtonsoft.Json;
using System;
using System.IO;

namespace reintegrate.Core.Readers
{
    public static class JsonReader
    {
        public static T Load<T>(string path)
        {
            return (T)Load(typeof(T), path);
        }

        public static object Load(Type type, string path)
        {
            if (path.Contains("/"))
                path = path.Replace("/", "\\");

            path = Path.Combine(Directory.GetCurrentDirectory(), path + ".json");

            using (TextReader reader = new StreamReader(path))
            {
                var value = reader.ReadToEnd();
                return JsonConvert.DeserializeObject(value, type);
            }
        }

        public static void Save<T>(string path, T @object)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                var json = JsonConvert.SerializeObject(@object);
                writer.Write(json);
            }
        }
    }
}
