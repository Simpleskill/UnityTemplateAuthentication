using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CookieClicker : MonoBehaviour
{
    public int clickCount = 0;
    public int powerPerClick = 1;
    public TMP_Text clickText;

    public void Click()
    {
        clickCount += powerPerClick;
        CloudSaveManager.Instance.playerData.clickCount = clickCount;
        clickText.text = clickCount.ToString();
        CloudSaveManager.Instance.SavePlayerData();
    }

}
