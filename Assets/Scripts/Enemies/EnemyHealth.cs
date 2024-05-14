using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    //private Animator animator;
    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        //animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            //animator.SetTrigger("Death");
            StartCoroutine(DestroyAfterAnimation()); // delai avant de détruire l'objet
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // reprend la durée de l'animation dans les infos de l'animation

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
