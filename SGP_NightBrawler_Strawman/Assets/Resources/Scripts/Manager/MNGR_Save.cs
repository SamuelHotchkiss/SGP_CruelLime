﻿using UnityEngine;
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

        SaveProfiles();
    }

    // Saves out the save files to an exterior file
    public static void SaveProfiles()
    {
        BinaryFormatter bff = new BinaryFormatter();

        if (!Directory.Exists("Assets/Resources/GameSaves"))
            Directory.CreateDirectory("Assets/Resources/GameSaves");

        FileStream file = File.Create("Assets/Resources/GameSaves/savedGames.SAMMICH");
        bff.Serialize(file, saveFiles);
        file.Close();
    }

    // Saves out Options
    public static void SaveOptions()
    {
        BinaryFormatter bff = new BinaryFormatter();
        FileStream file = File.Create("Assets/Resources/options.OPTIONS");
        //bff.Serialize(file, saveFiles);
        file.Close();
    }

    // Loads in the save files from an exterior file
    public static void Load()
    {
        if (File.Exists("Assets/Resources/GameSaves/savedGames.SAMMICH"))
        {
            BinaryFormatter bff = new BinaryFormatter();
            FileStream file = File.Open("Assets/Resources/GameSaves/savedGames.SAMMICH", FileMode.Open);
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
        saveFiles[currSave].isNew = false;
    }

    // Resets a current save profile
    public static void DeleteCurrentSave(int saveIndex)
    {
        saveFiles[saveIndex] = new MNGR_GameData();
        SaveProfiles();
    }
}

