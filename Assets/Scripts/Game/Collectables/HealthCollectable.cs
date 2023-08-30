using UnityEngine;

public class HealthCollectable : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField]
    private float _healthAmount;

    private HealthController _heathController;

    private void Awake()
    {
        _heathController = FindObjectOfType<PlayerMovement>().GetComponent<HealthController>();
    }

    public void OnCollected()
    {
        _heathController.AddHealth(_healthAmount);
    }
}
