using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem {
    private static string SavePath(string username) {
        return Application.persistentDataPath + "/" + username + "_data.json";
    }

    public static void SavePlayer(PlayerData playerData) {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(SavePath(playerData.username), json);
        Debug.Log("Player data saved at: " + SavePath(playerData.username));
    }

    public static PlayerData LoadPlayer(string username) {
        string path = SavePath(username);
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            return playerData;
        } else {
            Debug.LogWarning("Save file not found for username: " + username);
            return null;
        }
    }
}