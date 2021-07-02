using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //save data
    public static void SaveData(GameManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string path = "D:/SaveFile/data.uwansummoney";
        string path = Application.persistentDataPath + "/data.uwansummoney";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveData(IdleManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string path = "D:/SaveFile/data.uwansummoney";
        string path = Application.persistentDataPath + "/dataBank.uwansummoney"; //this change every update
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveQuestState(QuestManager qManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string path = "D:/SaveFile/questData.uwansummoney";
        string path = Application.persistentDataPath + "/questData.uwansummoney";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(qManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SavePetManager(PetManager pManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string path = "D:/SaveFile/petData.uwansummoney";
        string path = Application.persistentDataPath + "/petData.uwansummoney";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(pManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveCostumeManager(CostumeManager cManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string path = "D:/SaveFile/costumeData.uwansummoney";
        string path = Application.persistentDataPath + "/costumeData.uwansummoney";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(cManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveNPCManager(HireNPCManager npcManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string path = "D:/SaveFile/questData.uwansummoney";
        string path = Application.persistentDataPath + "/npcData.uwansummoney";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(npcManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //load data
    public static GameData LoadData()
    {
        //string path = "D:/SaveFile/data.uwansummoney";
        string path = Application.persistentDataPath + "/dataBank.uwansummoney"; //change this every update
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
    public static GameData LoadPreviousData()
    {
        //string path = "D:/SaveFile/data.uwansummoney";
        string path = Application.persistentDataPath + "/data.uwansummoney"; //change this every update
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
        //string path = "D:/SaveFile/questData.uwansummoney";
        string path = Application.persistentDataPath + "/questData.uwansummoney";
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
        //string path = "D:/SaveFile/petData.uwansummoney";
        string path = Application.persistentDataPath + "/petData.uwansummoney";
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
    public static GameData LoadCostumeManager()
    {
        //string path = "D:/SaveFile/costumeData.uwansummoney";
        string path = Application.persistentDataPath + "/costumeData.uwansummoney";
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
    public static GameData LoadNPCManager()
    {
        //string path = "D:/SaveFile/costumeData.uwansummoney";
        string path = Application.persistentDataPath + "/npcData.uwansummoney";
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
