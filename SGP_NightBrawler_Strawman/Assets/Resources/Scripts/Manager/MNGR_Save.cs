using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class MNGR_Save
{
    public static List<MNGR_GameData> saveFiles = new List<MNGR_GameData>();

    public static int currSave;

    // Sets up the three save files
    public static void InitializeProfiles()
    {
        for (int i = 0; i < 3; i++)
            saveFiles.Add(new MNGR_GameData());

        Save();
    }

    // Saves out the save files to an exterior file
    public static void Save()
    {
        BinaryFormatter bff = new BinaryFormatter();
        FileStream file = File.Create("Assets/Resources/savedGames.SAMMICH");
        bff.Serialize(file, saveFiles);
        file.Close();
    }

    // Loads in the save files from an exterior file
    public static void Load()
    {
        if (File.Exists("Assets/Resources/savedGames.SAMMICH"))
        {
            BinaryFormatter bff = new BinaryFormatter();
            FileStream file = File.Open("Assets/Resources/savedGames.SAMMICH", FileMode.Open);
            saveFiles = (List<MNGR_GameData>)bff.Deserialize(file);
            file.Close();
        }
        else
            InitializeProfiles();
    }

    // Only overwrites the data of one save profile
    public static void OverwriteCurrentSave()
    {
        saveFiles[currSave].CopyGameManager();
    }

    // Only assigns the GameManager from currently loaded save
    public static void LoadCurrentSave()
    {
        saveFiles[currSave].AssignGameManager();
    }

    // Resets a current save profile
    public static void DeleteCurrentSave(int saveIndex)
    {
        saveFiles[saveIndex] = new MNGR_GameData();
        Save();
    }
}

