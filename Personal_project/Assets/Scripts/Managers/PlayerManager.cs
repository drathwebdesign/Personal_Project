using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public GameObject player;  // Reference to the Player GameObject

    float autoSaveInterval = 60f;

    void Start() {
        // Load the player data when the game scene starts
        string username = GameManager.instance.username;

        PlayerData loadedData = SaveSystem.LoadPlayer(username);
        if (loadedData != null) {
            Debug.Log("Player data loaded for: " + username);

            // Set player's position to the saved data
            Vector3 loadedPosition = loadedData.playerPosition.GetPosition();
            player.transform.position = loadedPosition;

            Debug.Log("Player position set to: " + loadedPosition);
        } else {
            Debug.LogWarning("No player data found for username: " + username);
        }

        InvokeRepeating("SavePlayerPosition", autoSaveInterval, autoSaveInterval);
    }

    void Update() {
    }

    // You can use this method to test saving the current player position
    public void SavePlayerPosition() {
        string username = GameManager.instance.username;
        Vector3 currentPosition = player.transform.position;

        PlayerData updatedPlayerData = new PlayerData(username, currentPosition);
        SaveSystem.SavePlayer(updatedPlayerData);
        Debug.Log("Auto-saved player data at position: " + currentPosition);
    }

    private void OnApplicationQuit() {
        SavePlayerPosition();
#if (UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}