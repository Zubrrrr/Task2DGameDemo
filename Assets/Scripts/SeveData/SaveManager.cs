using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    private static string _saveFilePath;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public static void SaveData<T>(T data)
    {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(_saveFilePath, jsonData);
    }

    public static T LoadData<T>()
    {
        if (File.Exists(_saveFilePath))
        {
            string jsonData = File.ReadAllText(_saveFilePath);
            if (!string.IsNullOrEmpty(jsonData))
            {
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
        }
        return default(T);
    }
}
