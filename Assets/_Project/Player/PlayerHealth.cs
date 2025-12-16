using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private int maxHealth;
    private float invincibilityDuration;
    [SerializeField] private bool isInvincible;
    [SerializeField] private int health;

    private void Start()
    {
        InitializedHealthData();
    }

    private void InitializedHealthData()
    {
        maxHealth = GameManager.Instance.playerHealthData.maxHealth;
        health = GameManager.Instance.playerHealthData.currentHealth;
        invincibilityDuration = GameManager.Instance.playerHealthData.timeToInvincibility;
    }
    private void CheckPlayerDeath()
    {
        if (health <= 0) PlayerEvents.PlayerDeath();
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    //Este metodo es llamado por cualquier objeto que deba hacer daño al jugador.
    public void TakeDamage(int damageAmount)
    {
        if(isInvincible) return;

        StartCoroutine(InvincibilityCoroutine());
        health = Mathf.Clamp(health - damageAmount, 0, maxHealth);
        PlayerEvents.PlayerHealthChanged(health);
        GameManager.Instance.playerHealthData.currentHealth = health;

        CheckPlayerDeath();
    }



}
