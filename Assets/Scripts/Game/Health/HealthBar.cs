using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;

    public void UpdateHealthBar(HealthController health)
    {
        _healthBarForegroundImage.fillAmount = health.RemainingHealthPercentage;
    }
}
