using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; }

    [SerializeField] private float knockbackTime = 5; // 5 = 0.1s, 4 = 0.08s ...

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockbackTime /= 50;
    }

    public void GetKnockedBack(Transform damageSource, float knocBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knocBackThrust * rb.mass; // force = masse * acceleration (thrust) * direction (normalized = vecteur de longueur 1)
        rb.AddForce(difference, ForceMode2D.Impulse); // on attache la force au rigidbody
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero; // on remet la vélocité à 0
        GettingKnockedBack = false;
    }
}
