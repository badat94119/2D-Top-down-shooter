using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private PauseController _pauseController;

    private void Awake()
    {
        _pauseController = FindObjectOfType<PauseController>();    
    }

    public void Resume()
    {
        _pauseController.ResumeGame();
    }

    public void MainMenu()
    {
        _pauseController.ResumeGame();
        SceneController.Instance.LoadScene(SceneName.MainMenu, Color.black, 1);
    }
}