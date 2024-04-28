using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayerData(Transform playerTransform, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + name + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerTransform);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Saved player data to " + path);
    }

    public static PlayerData LoadPlayerData(string name)
    {
        string path = Application.persistentDataPath + "/" + name + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("Loaded player data from " + path);

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveCameraData(Camera camera)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/camera.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        CameraData data = new CameraData(camera);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved camera settings to " + path);
    }

    public static CameraData LoadCameraData()
    {
        string path = Application.persistentDataPath + "/camera.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CameraData data = formatter.Deserialize(stream) as CameraData;
            stream.Close();

            Debug.Log("Loaded camera settings from " + path);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    
}