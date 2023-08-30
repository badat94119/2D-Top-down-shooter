using System.Collections;
using UnityEngine;

public class SpashSceneController : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        SceneController.Instance.LoadScene(SceneName.MainMenu, Color.black, 1f);
    }
}
