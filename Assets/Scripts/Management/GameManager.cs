using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        // Initialisation du jeu
    }

    public void ResetGame()
    {
        // Réinitialiser les différents composants du jeu
        PlayerController.Instance.ResetPlayer();
        PlayerHealth.Instance.ResetHealth();
        EconomyManager.Instance.ResetCurrency();
        ActiveInventory.Instance.ResetSlot();
        AudioManager.Instance.ResetAudio();
    }
}
