using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
        AdjustEnnemyFacingDirection();
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    private void AdjustEnnemyFacingDirection()
    {
        if (moveDir.x < 0) // vers la gauche
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0) // vers la droite
        {
            spriteRenderer.flipX = false;
        }
    }
}
