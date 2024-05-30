using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private GameObject teleportStoneSprite;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float startingMoveSpeed;
    private Knockback knockback;

    private bool isDashing;
    private bool haveTeleportStone;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        playerControls.Player.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
        SetTeleportStone(false);

        if (teleportStoneSprite != null)
        {
            teleportStoneSprite.SetActive(false);
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
        AttackInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void PlayerInput()
    {
        movement = playerControls.Player.Move.ReadValue<Vector2>();

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
    }

    private void AttackInput()
    {
        if (playerControls.Player.Attack.triggered)
        {
            animator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
        }
    }

    public void OnAttackAnimationEnd()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void Move()
    {
        if (PlayerHealth.Instance.isDead) return; 

        if (knockback.GettingKnockedBack) return;

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }


    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void ResetMoveSpeed()
    {
        moveSpeed = startingMoveSpeed;
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            weaponCollider.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            spriteRenderer.flipX = false;
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(DashCooldown());
        }
    }

    public void SetTeleportStone(bool value)
    {
        haveTeleportStone = value;

        if (teleportStoneSprite != null)
        {
            teleportStoneSprite.SetActive(value);
        }
    }


    public bool GetTeleportStone()
    {
        return haveTeleportStone;
    }

    private IEnumerator DashCooldown()
    {
        float dashTime = 0.2f;
        float dashCooldown = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        trailRenderer.emitting = false;
    }

    internal void ResetPlayer()
    {
        PlayerHealth.Instance.ResetHealth();
        EconomyManager.Instance.ResetCurrency();
    }
}
