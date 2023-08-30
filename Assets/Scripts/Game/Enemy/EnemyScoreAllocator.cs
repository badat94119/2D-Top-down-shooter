using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    private EnemyAttributes _enemyAttributes;
    private ScoreController _scoreController;

    private void Awake()
    {
        _enemyAttributes = GetComponent<EnemyAttributes>();
        _scoreController = FindObjectOfType<ScoreController>();
    }

    public void AllocateScore()
    {
        _scoreController.AddScore(_enemyAttributes.KillScore);
    }
}
