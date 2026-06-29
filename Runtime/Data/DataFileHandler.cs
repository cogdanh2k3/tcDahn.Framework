using System;
using System.IO;
using UnityEngine;

namespace tcDahn
{
    public static class DataFileHandler
    {
        private const string RootFolderName = "tcDahn.Data";

        private static string GetDevicePath(string filePath)
        {
            return Path.Combine(Application.persistentDataPath, RootFolderName, filePath);
        }

        private static string GetProjectPath(string filePath)
        {
            return Path.Combine(Application.dataPath, filePath);
        }

        public static void Save<T>(T data, string key)
        {
            string json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(Path.Combine(Application.persistentDataPath, key + ".json"), json);
        }

        public static T Load<T>(string key) where T : class
        {
            string path = Path.Combine(Application.persistentDataPath, key + ".json");
            if (!File.Exists(path))
                return null;
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        }

        public static void Delete(string key)
        {
            string path = Path.Combine(Application.persistentDataPath, key + ".json");
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
