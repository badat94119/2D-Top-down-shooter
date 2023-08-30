using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private float _expiryTime;

    [SerializeField]
    private AudioClip _collectedAudioClip;

    private ICollectableBehaviour _collectableBehaviour;
    private SpriteFlash _spriteFlash;
    private ReturnToObjectPoolController _returnToObjectPoolController;

    private void Awake()
    {
        _collectableBehaviour = GetComponent<ICollectableBehaviour>();
        _spriteFlash = GetComponent<SpriteFlash>();
        _returnToObjectPoolController = GetComponent<ReturnToObjectPoolController>();
    }

    private void OnEnable()
    {
        Invoke(nameof(StartExpiry), _expiryTime);
    }

    private void StartExpiry()
    {
        if (isActiveAndEnabled)
        {
            StartCoroutine(ExpiryCoroutine());
        }
    }

    private IEnumerator ExpiryCoroutine()
    {
        yield return _spriteFlash.FlashCoroutine(3, new Color(1, 1, 1, 0.5f), 7);
        _returnToObjectPoolController.ReturnToObjectPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            collision.GetComponent<AudioSource>().PlayOneShot(_collectedAudioClip);

            _collectableBehaviour.OnCollected();
            _returnToObjectPoolController.ReturnToObjectPool();
        }
    }
}
