using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 200;
    [SerializeField] TMP_Text healthText;
    EnemyAI enemyAI;

    private void Start()
    {
        //healthText.text = "";
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        //SetHealthText();
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
      
        if (hitPoints <= 0)
        {
            //SetHealthText();
            Die();
        }

        if (enemyAI != null)
        {
            enemyAI.SetProvokedOnFire();
        }
    }

    private void SetHealthText()
    {
        if (hitPoints >= 0)
        {
            healthText.text = $"E: {hitPoints.ToString()}";
        }
        else
        {
            healthText.text = "0";
        }
    }

    private void Die()
    {
        enemyAI.SetDeathActions();
        //Destroy(gameObject);
    }
}
