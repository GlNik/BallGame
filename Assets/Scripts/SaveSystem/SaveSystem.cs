using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(Progress progress)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pleaseDontTouchIt.dude";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        ProgressDate progressDate = new ProgressDate(progress);
        binaryFormatter.Serialize(fileStream, progressDate);
        fileStream.Close();
    }

    public static ProgressDate Load()
    {
        string path = Application.persistentDataPath + "/pleaseDontTouchIt.dude";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            ProgressDate progressData = binaryFormatter.Deserialize(fileStream) as ProgressDate;
            fileStream.Close();
            return progressData;
        }
        else
        {
            Debug.Log("a kavo? chego?");
            return null;
        }
    }

    public static void DeleteFile()
    {

        string path = Application.persistentDataPath + "/pleaseDontTouchIt.dude";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

    }
}
