using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeepButtonSelected : MonoBehaviour
{
    private EventSystem _eventSystem;
    private GameObject _lastSelected;

    void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (_eventSystem.currentSelectedGameObject != null)
        {
            _lastSelected = _eventSystem.currentSelectedGameObject;
        }
        else
        {
            _eventSystem.SetSelectedGameObject(_lastSelected);
        }
    }
}
