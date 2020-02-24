using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] ParticleSystem bloodSplash;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        PlayBloodSplash();
        if(hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("Die");
    }

    private void PlayBloodSplash()
    {
        bloodSplash.Play();
    }
}
