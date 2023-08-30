using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }
    public void OnScoreChanged(ScoreController scoreController)
    {
        _scoreText.text = $"Score: {scoreController.Score}";
    }
}
