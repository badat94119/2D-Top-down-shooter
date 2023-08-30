using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    public UnityEvent GamePaused;

    public UnityEvent GameResumed;

    private bool _isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause2();
        }
    }

    private void OnPause2()
    {
        if (_isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isPaused = false;

        GameResumed.Invoke();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _isPaused = true;
    
        GamePaused.Invoke();
    }
}