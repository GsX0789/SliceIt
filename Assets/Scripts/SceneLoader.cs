using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    int MAIN_SCENE = 0;
    int CREDITS_SCENE = 1;


    public void LoadMainScene()
    {
        SceneManager.LoadScene(MAIN_SCENE);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(CREDITS_SCENE);
    }
}
