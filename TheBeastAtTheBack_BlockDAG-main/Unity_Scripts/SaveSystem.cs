using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void Save(string fileName, string data)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        File.WriteAllText(path, data);
    }

    public static string Load(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        return null;
    }
}
