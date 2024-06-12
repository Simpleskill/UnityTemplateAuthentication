using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class LoginController : MonoBehaviour
{
    public event Action<PlayerInfo, string> OnSignedIn;
    public UILogin uiLogin;

    private PlayerInfo playerInfo;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();

        PlayerAccountService.Instance.SignedIn += SignIn;
    }

    private async void SignIn()
    {
        try
        {
            var accessToken = PlayerAccountService.Instance.AccessToken;
            await SignInWithUnityAsync(accessToken);
            await CloudSaveManager.Instance.LoadPlayerData();
            GameManager.Instance.StartGame();
        }
        catch (Exception)
        {

        }
    }

    public async Task InitSignIn()
    {
        Debug.Log("InitSignIn");
        await PlayerAccountService.Instance.StartSignInAsync();
    }

    async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful.");

            playerInfo = AuthenticationService.Instance.PlayerInfo;

            var name = await AuthenticationService.Instance.GetPlayerNameAsync();

            OnSignedIn?.Invoke(playerInfo, name);
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    public async void Logout()
    {
        await CloudSaveManager.Instance.SavePlayerData();
        //AuthenticationService.Instance.DeleteAccountAsync();
        AuthenticationService.Instance.SignOut(true);
        PlayerAccountService.Instance.SignOut();
        uiLogin.OpenLoginPanel();
        GameManager.Instance.CloseGame();
    }

    private void OnDestroy()
    {
        PlayerAccountService.Instance.SignedIn -= SignIn;
    }
}
