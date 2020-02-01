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

    void Update()
    {
        // TODO: allow two step exit
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main" 
            && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
