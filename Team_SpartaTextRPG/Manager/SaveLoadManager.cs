
using System.IO;
using System.Text.Json;

namespace Team_SpartaTextRPG
{
    class SaveLoadManager : Helper.Singleton<SaveLoadManager>
    {
        public string filePath = Directory.GetCurrentDirectory();

        public void SaveToJson<T>(T? data) where T : class
        {
            string path = Path.Combine(filePath,typeof(T).Name);

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(path, json);
        }

        public T? LoadFromJson<T>() where T : class
        {
            string path = Path.Combine(filePath,typeof(T).Name);

            if (!File.Exists(path))
            {
                Console.WriteLine("파일이 존재하지 않습니다.");
                return null;
            }

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { IncludeFields = true });
        }
    }
}
