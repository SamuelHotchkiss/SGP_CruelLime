using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class MNGR_Save
{
    public static List<MNGR_GameData> saveFiles = new List<MNGR_GameData>();
    public static MNGR_OptionsData optionsFile = new MNGR_OptionsData();

    public static int currSave;

    // Sets up the three save files
    public static void InitializeProfiles()
    {
        for (int i = 0; i < 3; i++)
            saveFiles.Add(new MNGR_GameData());

        SaveProfiles();
    }

    // Saves out the save files to an exterior file
    public static void SaveProfiles()
    {
        BinaryFormatter bff = new BinaryFormatter();

        if (!Directory.Exists(Application.dataPath + "GameSaves"))
            Directory.CreateDirectory(Application.dataPath +"GameSaves");

        FileStream file = File.Create(Application.dataPath + "GameSaves/savedGames.SAVES");
        bff.Serialize(file, saveFiles);
        file.Close();
    }

    // Saves out Options
    public static void SaveOptions()
    {
        optionsFile.CopyOptions();

        BinaryFormatter bff = new BinaryFormatter();

        if (!Directory.Exists(Application.dataPath + "Options"))
            Directory.CreateDirectory(Application.dataPath + "Options");

        FileStream file = File.Create(Application.dataPath + "Options/options.OPTIONS");

        bff.Serialize(file, optionsFile);
        file.Close();
    }

    // Loads in the save files from an exterior file
    public static void LoadProfiles()
    {
        if (File.Exists(Application.dataPath + "GameSaves/savedGames.SAVES"))
        {
            BinaryFormatter bff = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "GameSaves/savedGames.SAVES", FileMode.Open);
            saveFiles = (List<MNGR_GameData>)bff.Deserialize(file);
            file.Close();
        }
        else
            InitializeProfiles();
    }

    // Loads in saved options
    public static void LoadOptions()
    {
        if (File.Exists(Application.dataPath + "Options/options.OPTIONS"))
        {
            BinaryFormatter bff = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "Options/options.OPTIONS", FileMode.Open);
            optionsFile = (MNGR_OptionsData)bff.Deserialize(file);
            file.Close();
        }

        optionsFile.AssignOptions();
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
        saveFiles[currSave].isNew = false;
    }

    // Resets a current save profile
    public static void DeleteCurrentSave(int saveIndex)
    {
        saveFiles[saveIndex] = new MNGR_GameData();
        SaveProfiles();
    }
}

