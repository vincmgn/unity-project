using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private GameObject openedSprite;
    [SerializeField] private GameObject teleportActivate;
    [SerializeField] private AudioClip openSound = null;

    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpened && other.gameObject.GetComponent<DamageSource>())
        {
            GetComponent<PickUpSpawner>().DropTeleportStone();
            transform.GetChild(0).gameObject.SetActive(false);
            teleportActivate.SetActive(true);
            openedSprite.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(openSound);
            isOpened = true;
        }
    }
}