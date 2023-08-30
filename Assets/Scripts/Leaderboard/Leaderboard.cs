using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private Transform _leaderboardPanel;

    [SerializeField]
    private TextMeshProUGUI _leaderBoardNameTextPrefab;

    [SerializeField]
    private TextMeshProUGUI _leaderBoardScoreTextPrefab;

    [SerializeField]
    private TextMeshProUGUI _leaderBoardNameHighlightTextPrefab;

    [SerializeField]
    private TextMeshProUGUI _leaderBoardScoreHighlightTextPrefab;

    private SceneController _sceneController;
    private LeaderboardDataController _leaderboardDataController;


    private void Start()
    {
        _sceneController = SceneController.Instance;
        _leaderboardDataController = LeaderboardDataController.Instance;

        InitialiseLeaderboard();
    }

    private void InitialiseLeaderboard()
    {
        var playerEntry = _sceneController.SceneData.GetAndRemove<LeaderboardEntry>("LeaderboardEntry");

        foreach (var leaderboardEntry in _leaderboardDataController.LeaderboardEntries)
        {
            TextMeshProUGUI nameText;
            TextMeshProUGUI scoreText;

            if (playerEntry == leaderboardEntry)
            {
                nameText = Instantiate(_leaderBoardNameHighlightTextPrefab, _leaderboardPanel);
                scoreText = Instantiate(_leaderBoardScoreHighlightTextPrefab, _leaderboardPanel);
            }
            else
            {
                nameText = Instantiate(_leaderBoardNameTextPrefab, _leaderboardPanel);
                scoreText = Instantiate(_leaderBoardScoreTextPrefab, _leaderboardPanel);
            }

            nameText.text = leaderboardEntry.Name;
            scoreText.text = leaderboardEntry.Score.ToString();
        }
    }

    public void MainMenu()
    {
        _sceneController.LoadScene(SceneName.MainMenu, Color.black, 1);
    }
}
