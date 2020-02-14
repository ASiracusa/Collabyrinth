using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{

    public static void SaveData(SaveData gameData)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/mapstats.ms";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();

    }

    public static SaveData LoadData()
    {

        string path = Application.persistentDataPath + "/mapstats.ms";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return saveData;
        }
        else
        {
            Debug.LogError("Save data not found in " + path);
            return null;
        }

    }

}
