using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static void SaveLenguage(LanguagueManager lenguegeStats)
    {
        BinaryFormatter bf = new BinaryFormatter();

        string filePath = Application.persistentDataPath + "/Lenguege_stats.dll";
        FileStream fs = new FileStream(filePath, FileMode.Create);

        LenguageData lenguegeData = new LenguageData(lenguegeStats);

        bf.Serialize(fs, lenguegeData);
        fs.Close();


    }

    public static LenguageData LoadLenguage()
    {
        string filePath = Application.persistentDataPath + "/Lenguege_stats.dll";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filePath, FileMode.Open);
            LenguageData lenguageData = bf.Deserialize(fs) as LenguageData;
            fs.Close();
            return lenguageData;
        }
        else
        {
            return null;
        }
    }
}
