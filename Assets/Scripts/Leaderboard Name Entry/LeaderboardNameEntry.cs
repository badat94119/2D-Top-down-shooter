using TMPro;
using UnityEngine;

public class LeaderboardNameEntry : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _textEntry;

    private LeaderboardDataController _leaderboardDataController;
    private SceneController _sceneController;

    public void AddNameToLeaderboard()
    {
        if (_textEntry.text.Length == 0)
        {
            _textEntry.ActivateInputField();
        }
        else
        {
            int score = _sceneController.SceneData.GetAndRemove<int>("PlayerScore");

            var leaderboardEntry = new LeaderboardEntry { Name = _textEntry.text, Score = score };

            _leaderboardDataController.AddLeaderboardEntry(leaderboardEntry);

            _sceneController.SceneData.Add("LeaderboardEntry", leaderboardEntry);
            _sceneController.LoadScene(SceneName.Leaderboard, Color.black, 1);
        }
    }

    private void Start()
    {
        _leaderboardDataController = LeaderboardDataController.Instance;
        _sceneController = SceneController.Instance;
    }
}
