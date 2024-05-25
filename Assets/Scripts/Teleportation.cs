using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    public string targetScene;
    public string respawnPointName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("RespawnPoint", respawnPointName);
            SceneManager.LoadScene(targetScene);
        }
    }
}
