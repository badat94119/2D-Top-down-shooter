using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playlist", menuName = "ScriptableObjects/Playlist", order = 1)]
public class Playlist : ScriptableObject
{
    [SerializeField]
    private List<AudioClip> _clips;

    public IEnumerable<AudioClip> Clips
    {
        get
        {
            return _clips;
        }
    }
}