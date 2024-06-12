using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int clickCount;
    public int powerPerClick;
    public void InitializeDefaultValues()
    {
        playerName = "Bob";
        clickCount = 0;
        powerPerClick = 1;
    }
}