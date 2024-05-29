using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private AudioClip openSound = null;

    public string targetScene;
    public string respawnPointName;
    private float waitToLoadTime = 1f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerController.Instance.GetTeleportStone() && ActiveInventory.Instance.GetSlot() == 1)
        {
            PlayerPrefs.SetString("RespawnPoint", respawnPointName);
            PlayerController.Instance.SetMoveSpeed(0.3f);
            if (openSound != null)
            {
                GetComponent<AudioSource>().PlayOneShot(openSound);
            } else
            {
                Debug.LogWarning("No open sound attached to " + gameObject.name);
            }
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
        PlayerController.Instance.SetTeleportStone(false);
        SceneManager.LoadScene(targetScene);
    }
}
