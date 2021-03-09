using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //save data
    public static void SaveData(GameManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = "D:/SaveFile/data.uwansummoney";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveQuestState(QuestManager qManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = "D:/SaveFile/questData.uwansummoney";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(qManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SavePetManager(PetManager pManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = "D:/SaveFile/petData.uwansummoney";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(pManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //load data
    public static GameData LoadData ()
    {
        string path = "D:/SaveFile/data.uwansummoney";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }
    public static GameData LoadQuestState()
    {
        string path = "D:/SaveFile/questData.uwansummoney";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }
    public static GameData LoadPetManager()
    {
        string path = "D:/SaveFile/petData.uwansummoney";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }
}
