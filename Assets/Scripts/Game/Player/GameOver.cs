using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private float _timeToWaitBeforeExit;

    private ScoreController _scoreController;

    private void Awake()
    {
        _scoreController = GetComponent<ScoreController>();
    }

    public void OnDied()
    {
        Invoke(nameof(EndGame), _timeToWaitBeforeExit);
    }

    private void EndGame()
    {
        MusicPlayer.Instance.FadeVolumeDown(1f);

        if (LeaderboardDataController.Instance.ScoreQualifiesForLeaderboard(_scoreController.Score))
        {
            SceneController.Instance.SceneData.Add("PlayerScore", _scoreController.Score);
            SceneController.Instance.LoadScene(SceneName.LeaderboardNameEntry, Color.black, 1);
        }
        else
        {
            SceneController.Instance.LoadScene(SceneName.MainMenu, Color.black, 1);
        }
    }
}
