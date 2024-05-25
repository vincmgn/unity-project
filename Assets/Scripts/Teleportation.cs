using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    public string targetScene;
    public string respawnPointName;
    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("RespawnPoint", respawnPointName);
            PlayerController.Instance.SetMoveSpeed(0.3f);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        while(waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        PlayerController.Instance.ResetMoveSpeed();
        SceneManager.LoadScene(targetScene);
    }
}
