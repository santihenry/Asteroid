using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{

    /// LIFES

    public static void SaveLifeStats(LifeSystem lifeStats)
    {
        BinaryFormatter bf = new BinaryFormatter();


        string filePath = Application.persistentDataPath + "/Life_stats.dll";
        FileStream fs = new FileStream(filePath, FileMode.Create);

        LifeData lifeData = new LifeData(lifeStats);

        bf.Serialize(fs, lifeData);
        fs.Close();


    }

    public static LifeData LoadLifeStats()
    {
        string filePath = Application.persistentDataPath + "/Life_stats.dll";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filePath, FileMode.Open);
            LifeData lifeData = bf.Deserialize(fs) as LifeData;
            fs.Close();
            return lifeData;
        }
        else
        {
            return null;
        }
    }



    /// SHIP
    public static void SaveShipStats(ShipModel shipStats)
    {
        BinaryFormatter bf = new BinaryFormatter();

        string filePath = Application.persistentDataPath + "/Ship_stats.dll";
        FileStream fs = new FileStream(filePath, FileMode.Create);

        ShipData shipData = new ShipData(shipStats);

        bf.Serialize(fs, shipData);
        fs.Close();


    }
    

    public static ShipData LoadShipStats()
    {
        string filePath = Application.persistentDataPath + "/Ship_stats.dll";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filePath, FileMode.Open);
            ShipData shipData = bf.Deserialize(fs) as ShipData;
            fs.Close();
            return shipData;
        }
        else
        {
            return null;
        }
    }



    /// LANGUAGE
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
