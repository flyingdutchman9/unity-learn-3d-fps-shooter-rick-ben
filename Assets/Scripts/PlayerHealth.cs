using System;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 400;
    [SerializeField] TMP_Text healthText;

    private void Update()
    {
        SetHealthText();
    }

    public void TakeDamage(int damage, EnemyDemonAI enemyAI)
    {
        //EnemyDemonAI enemyAI = FindObjectOfType<EnemyDemonAI>();

        if (enemyAI.IsPlayerInAttackRange())
        {
            healthPoints -= damage;
            GetComponent<DisplayDamage>().ShowDamageCanvas();
        }

        if (healthPoints <= 0)
        {
            SetHealthText();
            Die();
        }
    }

    // Dupliciram jer je ideja bila imati samo Deamon girl...
    public void TakeDamageZombie(int damage, EnemyAI enemyAI)
    {
        //EnemyAI enemyAI = FindObjectOfType<EnemyAI>();
        
        if (enemyAI.IsPlayerInAttackRange())
        {
            healthPoints -= damage;
            GetComponent<DisplayDamage>().ShowDamageCanvas();
        }

        if (healthPoints <= 0)
        {
            GetComponent<DisplayDamage>().DisableDamageCanvas();
            SetHealthText();
            Die();
        }
    }

    public void InstaKill(string whoKilledMe)
    {
        print(whoKilledMe);
        healthPoints = 0;
        Die();
    }

    private void SetHealthText()
    {
        if (healthPoints >= 0)
        {
            healthText.text = $"P: {healthPoints.ToString()}";
        }
        else
        {
            healthText.text = $"";
        }
    }

    private void Die()
    {
        GetComponent<DeathHandler>().HandleDeath();
    }
}
