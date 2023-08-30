using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        MusicPlayer.Instance.FadeVolumeDown(1f);
        SceneController.Instance.LoadScene(SceneName.Game, Color.black, 1f);
    }

    public void Leaderboard()
    {
        SceneController.Instance.LoadScene(SceneName.Leaderboard, Color.black, 1f);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
