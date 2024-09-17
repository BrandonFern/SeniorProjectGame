using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 3;
    private NavMeshAgent agent;

    void Awake()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        { Death(); }
        else
        {
            StartCoroutine(ApplyKnockbackEffect());
        }
    }
    IEnumerator ApplyKnockbackEffect()
    {
        float originalSpeed = agent.speed;
        agent.speed = 0;

        yield return new WaitForSeconds(1f);

        agent.speed = originalSpeed;
    }

    void Death()
    {
        Destroy(gameObject);
    }
}