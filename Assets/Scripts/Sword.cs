using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Sword Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
