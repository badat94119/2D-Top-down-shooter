using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private SceneFade _sceneFade;

    public static SceneController Instance { get; private set; }

    public SceneData SceneData { get; } = new SceneData();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private IEnumerator Start()
    {
        yield return _sceneFade.FadeInCoroutine(1f, Color.black);
    }

    public void LoadScene(string sceneName, Color fadeColor, float fadeTime)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, fadeColor, fadeTime));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, Color fadeColor, float fadeTime)
    {
        yield return _sceneFade.FadeOutCoroutine(fadeTime, fadeColor);
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return _sceneFade.FadeInCoroutine(fadeTime, fadeColor);
    }
}