using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisibilityController : MonoBehaviour
{
    [SerializeField]
    private bool _cursorVisible;

    private void Start()
    {
        if (_cursorVisible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
