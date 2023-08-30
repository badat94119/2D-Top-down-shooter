using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistSetter : MonoBehaviour
{
    [SerializeField]
    private Playlist _playlist;

    private void Start()
    {
        MusicPlayer.Instance.SetPlaylist(_playlist);
    }
}
