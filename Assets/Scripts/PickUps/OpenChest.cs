using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private GameObject openedSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageSource>())
        {
            GetComponent<PickUpSpawner>().DropTeleportStone();
            transform.GetChild(0).gameObject.SetActive(false);
            openedSprite.SetActive(true);
        }
    }
}