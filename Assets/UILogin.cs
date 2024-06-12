using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{

    [SerializeField] private Button loginButton;

    [SerializeField] private Button logoutButton;

    [SerializeField] private TMP_Text userIdText;

    [SerializeField] private Transform loginPanel, userPanel;

    [SerializeField] private LoginController loginController;

    private void OnEnable()
    {
        //loginButton.onClick.RemoveAllListeners();
        loginButton.onClick.AddListener(LoginButtonPressed);
        logoutButton.onClick.AddListener(LogoutButtonPressed);

        loginController.OnSignedIn += LoginController_OnSignedIn;
    }

    private void LoginController_OnSignedIn(PlayerInfo playerInfo, string playerName)
    {
        OpenUserPanel();
        userIdText.text = $"id_{playerInfo.Id}";
        Debug.Log(playerName);
    }

    private async void LoginButtonPressed()
    {
        Debug.Log("Login Button Pressed");
        await loginController.InitSignIn();
    }

    private void LogoutButtonPressed()
    {
        //Logout
        loginController.Logout();
    }

    private void OnDisable()
    {

        loginButton.onClick.RemoveAllListeners();
        loginController.OnSignedIn -= LoginController_OnSignedIn;
    }
    public void HideAllPanels()
    {
        loginPanel.gameObject.SetActive(false);
        userPanel.gameObject.SetActive(false);
    }

    public void OpenLoginPanel()
    {
        HideAllPanels();
        loginPanel.gameObject.SetActive(true);
    }
    public void OpenUserPanel()
    {
        HideAllPanels();
        userPanel.gameObject.SetActive(true);
    }
}
