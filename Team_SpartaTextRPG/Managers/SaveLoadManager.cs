
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;

namespace Team_SpartaTextRPG
{
    class SaveLoadManager : Helper.Singleton<SaveLoadManager>
    {
        public string filePath = Directory.GetCurrentDirectory();

        public void SaveToJson<T>(T? data) where T : class
        {
            string path = Path.Combine(filePath, typeof(T).Name + ".json");

            //string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });

            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText(path, json);
        }

        public T? LoadFromJson<T>() where T : class
        {
            string path = Path.Combine(filePath, typeof(T).Name + ".json");

            if (!File.Exists(path))
            {
                Console.WriteLine("파일이 존재하지 않습니다.");
                return null;
            }

            string json = File.ReadAllText(path);
            //return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { IncludeFields = true });
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
