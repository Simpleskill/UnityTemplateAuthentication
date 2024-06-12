using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] CookieClicker cookie;
    public GameObject gamePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        LoadUI();
        gamePanel.SetActive(true);
    }

    public void LoadUI()
    {
        cookie.clickCount = CloudSaveManager.Instance.playerData.clickCount;
        cookie.powerPerClick = CloudSaveManager.Instance.playerData.powerPerClick;
        cookie.clickText.text = CloudSaveManager.Instance.playerData.clickCount.ToString();
    }

    public void CloseGame()
    {
        gamePanel.SetActive(false);

    }
}
