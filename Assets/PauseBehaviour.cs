using UnityEngine;

public sealed class PauseBehaviour : MonoBehaviour
{
    public GameObject pauseScreen;
    public static bool isGamePaused;


    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        isGamePaused = !isGamePaused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        isGamePaused = !isGamePaused;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
