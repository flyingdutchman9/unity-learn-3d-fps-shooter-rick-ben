using UnityEngine;
using TMPro;
using System;

public class EnemyHealthDemon : MonoBehaviour
{
    [SerializeField] int hitPoints = 200;
    [SerializeField] TMP_Text healthText;
    [SerializeField] GameObject healthBar;

    EnemyDemonAI enemyAI;
    float defaultHealthBarWidth;
    float defaultHitPoints;

    private void Start()
    {
        enemyAI = GetComponent<EnemyDemonAI>();
        defaultHealthBarWidth = healthBar.transform.localScale.x;
        defaultHitPoints = hitPoints;
    }

    private void Update()
    {
        //SetHealthText();
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        SetHealthBarSize(damage);

        if (hitPoints <= 0)
        {
            //SetHealthText();
            Die();
        }

        if (enemyAI != null)
        {
            enemyAI.SetProvokeActions();
        }
    }

    private void SetHealthBarSize(int damage)
    {
        // How wide is the bar comparing to enemy health...at the point of starting the game
        float widthReduceFactor = defaultHealthBarWidth / defaultHitPoints; //hitPoints / healthBar.transform.localScale.x;
        // reduce health bar width, with every shot using the factor and taken damage
        float newWidth = healthBar.transform.localScale.x - widthReduceFactor * damage;
        newWidth = newWidth < 0 ? 0 : newWidth;
        healthBar.transform.localScale = new Vector3(newWidth, healthBar.transform.localScale.y);
    }

    private void SetHealthText()
    {
        healthText.text = ""; // not in use so far...
        //if (hitPoints >= 0)
        //{
        //    healthText.text = $"E: {hitPoints.ToString()}";
        //}
        //else
        //{
        //    healthText.text = "0";
        //}
    }

    private void Die()
    {
        enemyAI.SetDeathActions();
        Destroy(gameObject, 3f);
    }
}
