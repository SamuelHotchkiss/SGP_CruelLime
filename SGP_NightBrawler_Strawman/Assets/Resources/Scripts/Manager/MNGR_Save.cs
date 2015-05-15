using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class MNGR_Save
{
    public static List<MNGR_GameData> saveFiles = new List<MNGR_GameData>();

    public static void Save()
    {
        saveFiles.Add(new MNGR_GameData());

        BinaryFormatter bff = new BinaryFormatter();
        FileStream file = File.Create("Assets/Resources/savedGames.SAMMICH");
        bff.Serialize(file, saveFiles);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists("Assets/Resources/savedGames.SAMMICH"))
        {
            BinaryFormatter bff = new BinaryFormatter();
            FileStream file = File.Open("Assets/Resources/savedGames.SAMMICH", FileMode.Open);
            saveFiles = (List<MNGR_GameData>)bff.Deserialize(file);
            file.Close();
        }
    }
}

