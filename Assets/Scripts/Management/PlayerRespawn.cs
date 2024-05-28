using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string respawnPointName = PlayerPrefs.GetString("RespawnPoint", "");

        if (!string.IsNullOrEmpty(respawnPointName))
        {
            GameObject respawnPoint = GameObject.Find(respawnPointName);

            if (respawnPoint != null)
            {
                PlayerController.Instance.transform.position = respawnPoint.transform.position;
                CameraController.Instance.SetPlayerCameraFollow();
                UIFade.Instance.FadeToClear();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
