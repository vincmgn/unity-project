using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private GameObject audioObject;
    [SerializeField] public GameObject music;
    [SerializeField] public GameObject cross;

    public bool isMuted = false;

    public void Start()
    {
        music.SetActive(true);
        cross.SetActive(false);
    }

    public void MuteHandler()
    {
        if (isMuted)
        {
            UnMute();
        }
        else
        {
            Mute();
        }
    }

    public void Mute()
    {
        AudioListener.volume = 0;
        music.SetActive(false);
        cross.SetActive(true);
        isMuted = true;
    }

    public void UnMute()
    {
        AudioListener.volume = 1;
        music.SetActive(true);
        cross.SetActive(false);
        isMuted = false;
    }

    public void ResetAudio()
    {
        UnMute();
        AudioSource audioSource = audioObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource not found on music GameObject.");
        }
    }

}
