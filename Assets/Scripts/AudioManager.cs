using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour

{
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
}
