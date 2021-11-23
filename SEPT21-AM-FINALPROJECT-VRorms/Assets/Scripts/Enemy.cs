using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float starthealth = 100;
    private float health;
    private bool isDead = false;
    private Animator animator;

    
    void Start()
    {
        health = starthealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0 && !isDead)
        {
            Die();
        }
    }
    
    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");

        WaveSpawner.enemiesAlive--;
        Destroy(gameObject, 2f);

    }
    
}
