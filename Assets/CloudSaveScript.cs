using Unity.Services.CloudSave;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager Instance { get; private set; }

    public PlayerData playerData;

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

    public async Task SavePlayerData()
    {
        var data = new Dictionary<string, object>
        {
            { "playerName", playerData.playerName },
            { "clickCount", playerData.clickCount },
            { "powerPerClick", playerData.powerPerClick }
        };

        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        Debug.Log("Player data saved to cloud");
    }

    public async Task LoadPlayerData()
    {
        try
        {
            var savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "playerName", "clickCount", "powerPerClick" });

            playerData.playerName = savedData["playerName"].ToString();
            playerData.clickCount = int.Parse(savedData["clickCount"].ToString());
            playerData.powerPerClick = int.Parse(savedData["powerPerClick"].ToString());

            Debug.Log("Player data loaded from cloud");
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("No saved data found, initializing default player data");

            // Initialize default values if no data found
            playerData.InitializeDefaultValues();

            // Save the default values to the cloud
            SavePlayerData();
        }
    }

    //private void OnApplicationQuit()
    //{
    //    SavePlayerData();
    //}

    //private void Start()
    //{
    //    LoadPlayerData();
    //}
}
