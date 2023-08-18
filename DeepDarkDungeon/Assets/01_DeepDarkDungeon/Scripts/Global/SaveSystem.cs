using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveSystem 
{
    public static void SaveData(Player player)
    {
        Debug.Log("데이터 저장한다.");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/gamedata.sav";
        if (File.Exists(path))
        {
            Debug.Log("데이터 불러온다.");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

}
