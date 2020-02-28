using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private AsyncOperation nextScene;

    // Start is called before the first frame update
    void Start () {
        nextScene = SceneManager.LoadSceneAsync ("MainGame", LoadSceneMode.Single);
        nextScene.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update () {
#if UNITY_EDITOR
        if (Input.GetButtonUp ("Fire1"))
            nextScene.allowSceneActivation = true;
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)
            nextScene.allowSceneActivation = true;
#endif
    }
}