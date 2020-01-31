using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void gotoGameScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void gotoMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
