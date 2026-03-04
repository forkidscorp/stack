using UnityEngine;
using PnCCasualGameKit;
/// <summary>
/// Responsible for calculating and storing the score and cash 
/// </summary>
public class ScoreAndCashManager : LazySingleton<ScoreAndCashManager> {
   
    [HideInInspector]
    public int currentScore;
    float currentCash;

    [Tooltip("Score increases by this value")]
    [SerializeField]
    private int scoreUnit;

    [Tooltip("Cash updates after this score")]
    [SerializeField]
    private int cashUpdateFrequency;
  
    /// <summary>
    /// Registering for events in start
    /// </summary>
    private void Start()
    {
        GameManager.Instance.GameOver += GameOver;
        GameManager.Instance.GameStarted += ResetScore;
    }

    /// <summary>
    /// Updates the score and cash
    /// </summary>
    public void UpdateScore()
    {
        currentScore += scoreUnit;
        if (cashUpdateFrequency!=0 && currentScore % cashUpdateFrequency == 0)
        {
            currentCash++;
        }

        UIManager.Instance.UpdateHudData(currentScore, currentCash);      
    }
   
    /// <summary>
    /// Resets current score to 0 at game start
    /// </summary>
    void ResetScore()
    {
        currentScore = 0;
        PlayerData.Instance.cash += currentCash;
    }

    /// <summary>
    /// Save highscore and cash to persistant storage at gamover and send UIManager the gameover data.
    /// </summary>
    void GameOver()
    {
        //Leaderboards.Instance.SetLeaderBoardResult();
        if (currentScore > PlayerData.Instance.highScore/* || currentCash > 0*/)
        {
            PlayerData.Instance.highScore = currentScore;
            //Leaderboards.Instance.SetLeaderBoardResult();
            //PlayerData.Instance.cash += currentCash;
            //Serialization is a heavy operation. Doing it after sometime to avoid lag during gameover.
            Invoke("SaveData", 2);
        }
        PlayerData.Instance.cash += currentCash;
        UIManager.Instance.UpdateGameOverData(currentScore, PlayerData.Instance.highScore, PlayerData.Instance.cash);
    }


    public  void SaveData(){
        PlayerData.Instance.SaveData();
    }

}
