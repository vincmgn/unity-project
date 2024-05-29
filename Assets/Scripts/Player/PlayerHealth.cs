using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private AudioClip openSound = null;
    [SerializeField] private GameObject MenuPausePanel = null;
    [SerializeField] private GameObject PauseButton = null;

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    const string HEART_SLIDER_TEXT = "Heart Slider";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHealthSlider();
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) { return; }

        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            currentHealth = 0;
            PlayerController.Instance.SetTeleportStone(false);
            if (openSound != null)
            {
                GetComponent<AudioSource>().PlayOneShot(openSound);
            }
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathRoutine());
        }
    }

    private IEnumerator DeathRoutine()
    {

        yield return new WaitForSeconds(3.5f);

        MenuPausePanel.SetActive(true);
        MenuPausePanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "GAME OVER !!";
        MenuPausePanel.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
        PauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Win()
    {
        MenuPausePanel.SetActive(true);
        MenuPausePanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "YOU WIN !!";
        MenuPausePanel.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
        PauseButton.SetActive(false);
        Time.timeScale = 0;
        PlayerController.Instance.SetTeleportStone(false);
    }



    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEART_SLIDER_TEXT).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    internal void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        UpdateHealthSlider();
    }
}
