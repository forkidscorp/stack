using UnityEngine;
using PnCCasualGameKit;
using YG;

public class GameManager : LazySingleton<GameManager>
{
    public System.Action GameInitialized, GameStarted, GameOver;

    [SerializeField]
    private string playStoreURL, appStoreURL;

    private void Awake()
    {
        PlayerData.Create();
    }

    void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        if (GameInitialized != null) {
            GameInitialized();
            //AdsInterstitial.Show();
            Debug.Log("SHOW_INT");
            YG2.InterstitialAdvShow();
        }   
    }

    public void StartGame()
    {
        if (GameStarted != null)
            GameStarted();
    }

    public void RateGame()
    {
#if UNITY_ANDROID
        Application.OpenURL(playStoreURL);
#elif UNITY_IOS
        Application.OpenURL(appStoreURL);
#endif
    }

#region TESTING

    public void IncreasePlayerCash()
    {
        float increaseBy = 100;
        PlayerData.Instance.cash += increaseBy;
        PlayerData.Instance.SaveData();
        Debug.Log("player cash increased by " + increaseBy);
    }

    public void DecreasePlayerCash()
    {
        float decreaseBy = 100;
        PlayerData.Instance.cash -= decreaseBy;
        PlayerData.Instance.SaveData();
        Debug.Log("player cash decreased by " + decreaseBy);

    }

    public void ClearPlayerData()
    {
        PlayerData.Clear();
        Debug.Log("player data cleared");

    }
#endregion

}
