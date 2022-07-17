using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    Destructible destructible;

    public bool canShoot;
    public float waitTime;
    public GameObject bullet;
    public Transform bulletPos;
    public GameObject deathVFX;

    private void Start()
    {
        canShoot = true;
        anim = GetComponent<Animator>();
        destructible = GetComponent<Destructible>();    
        Shoot();
        InvokeRepeating(nameof(Shoot), 3f, waitTime);
    }

    private void Shoot()
    {
        if (canShoot)
        {
            anim.SetTrigger("Shoot");
            GameObject newBullet = Instantiate(bullet,bulletPos.position,Quaternion.identity) as GameObject;
            newBullet.GetComponent<Bullet>().moveDirection = transform.forward;
            Destroy(newBullet, 10f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knife") || other.CompareTag("Player"))
        {
            Die();
        }
    }

    public void Die()
    {
        canShoot = false;
        destructible.BreakDeath();
        GameObject newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(newDeathVFX, 15f);
    }

}
