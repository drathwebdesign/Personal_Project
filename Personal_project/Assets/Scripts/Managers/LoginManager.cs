using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public GameObject loginConfirmationPanel;  // Overlay panel for confirmation
    public TMP_Text confirmationText;     // Text that displays "Create user" or "Log in as"

    private string username;

    private void Start() {
    }

    public void OnLoginButton() {
        username = usernameInput.text;

        // Check if the username is at least 4 characters long
        if (string.IsNullOrEmpty(username) || username.Length < 4) {
            Debug.LogWarning("Username must be at least 4 characters long!");
            return;
        }

        // Check if username exists in saved data
        PlayerData existingPlayerData = SaveSystem.LoadPlayer(username);
        if (existingPlayerData != null) {
            // User exists, show the confirmation panel with "Log in" message
            confirmationText.text = "Log in as: " + username;
        } else {
            // No user exists, show the confirmation panel with "Create new user" message
            confirmationText.text = "Create new user: " + username;
        }

        // Show the confirmation panel
        loginConfirmationPanel.SetActive(true);
    }

    // Called when the confirm button is pressed
    public void OnConfirmButton() {
        // Hide the confirmation panel
        loginConfirmationPanel.SetActive(false);

        // Check if user exists or not
        PlayerData existingPlayerData = SaveSystem.LoadPlayer(username);
        if (existingPlayerData != null) {
            // Existing player: Log in
            GameManager.instance.username = username;
            Debug.Log("Logging in as: " + username);
        } else {
            // New player: Create new player data
            GameManager.instance.username = username;
            PlayerData newPlayerData = new PlayerData(username, new Vector3(4f, -0.51f, -30f));  // Default starting position
            SaveSystem.SavePlayer(newPlayerData);
            Debug.Log("New player created: " + username);
        }

        // Load the next scene (Game Scene)
        SceneManager.LoadScene(1);
    }
}