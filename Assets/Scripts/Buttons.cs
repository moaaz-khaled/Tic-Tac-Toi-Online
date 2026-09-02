using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Go_to_TwoPlayer()
    {
        SceneManager.LoadScene("LocalGame");
    }

    public void GoTo_Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
