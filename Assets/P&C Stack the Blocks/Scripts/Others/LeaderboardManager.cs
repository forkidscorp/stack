using UnityEngine;
using YG;

public class LeaderboardManager : MonoBehaviour
{
    public void SaveScore(int score)
    {
        YG2.SetLeaderboard("BestScore", score);
    }
    public void ShowLeaderboard()
    {
        YG2.GetLeaderboard("BestScore");
    }
}
