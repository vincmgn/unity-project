using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;

    public string gameSceneName;
    public string menuSceneName;
    public GameObject pauseButton;

    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
        PlayerController.Instance.ResetPlayer();
        PauseMenuPanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void BackToMenu()
    {
        PlayerController.Instance.ResetPlayer();
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
        PauseMenuPanel.SetActive(false);
        pauseButton.SetActive(false);
    }
}