using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    private Animator animator;
    private Transform weaponCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
    }
    public void Attack()
    {
        Debug.Log("Sword Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
