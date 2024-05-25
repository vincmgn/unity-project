using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public AudioManager audioManager;


    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
        audioManager.MuteHandler();

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
