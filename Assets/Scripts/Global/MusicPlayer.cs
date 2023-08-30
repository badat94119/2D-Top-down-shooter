using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private Playlist _playlist;

    private AudioSource _audioSource;
    private int? _currentClipIndex;

    public static MusicPlayer Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_audioSource.isPlaying)
        {
            return;
        }

        PlayNextPlaylistClip();
    }

    private void PlayNextPlaylistClip()
    {
        if (_playlist != null && _playlist.Clips.Any())
        {
            _currentClipIndex = _currentClipIndex.HasValue ? _currentClipIndex + 1 : 0;

            if (_currentClipIndex >= _playlist.Clips.Count())
            {
                _currentClipIndex = 0;
            }

            _audioSource.clip = _playlist.Clips.ElementAt(_currentClipIndex.Value);
            _audioSource.Play();
        }
    }

    public void SetPlaylist(Playlist playlist)
    {
        if (_playlist == playlist)
        {
            return;
        }

        _playlist = playlist;
        _audioSource.volume = 1;
        _currentClipIndex = null;

        PlayNextPlaylistClip();
    }

    public void FadeVolumeDown(float duration)
    {
        StartCoroutine(FadeVolumeDownCoroutine(duration));
    }

    private IEnumerator FadeVolumeDownCoroutine(float duration)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedPercentage < 1)
        {
            elapsedTime += Time.deltaTime;
            elapsedPercentage = elapsedTime / duration;

            _audioSource.volume = Mathf.Lerp(1, 0, elapsedPercentage);

            yield return null;
        }
    }
}
