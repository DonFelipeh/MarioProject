﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    // Movement
    public float speed = 10;
    public float jumpForce = 10;
    protected float horizontal = 0;

    // Shoot
    public GameObject projectile;
    public float projectileForce = 50;
    public Transform spawnPoint;

    // GroundCheck
    protected bool isGrounded;
    protected Rigidbody2D rb;

    // Animation
    public Animator anim;

    // Death
    public bool dead = false;

    // Enemy
    public int enemyLayer;

    // audio
    public AudioSource src;
    public AudioClip jumpAudio;
    public AudioClip shootAudio;


    public float GetVerticalSpeed()
    {
        return rb.velocity.y;
    }
    // Use this for initialization
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        src = GetComponent<AudioSource>();
    }

    public float GetHorizontalSpeed()
    {
        return speed * horizontal;
    }
    public virtual void Move()
    {
        transform.position += speed * horizontal * Vector3.right * Time.deltaTime;
        if (horizontal > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < -0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public virtual void Jump()
    {
        isGrounded = false;
        rb.AddForce(jumpForce * Vector3.up, ForceMode2D.Impulse);
        if(jumpAudio != null && src != null)
        {
            src.volume = 0.3f;
            src.PlayOneShot(jumpAudio);
        }

    }

    public virtual void Shoot()
    {
        GameObject spawnedProjectile = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
        spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(projectileForce * transform.right, ForceMode2D.Impulse);

        if (shootAudio != null && src != null)
        {
            src.volume = 1f;
            src.PlayOneShot(shootAudio);
        }
    }

    public virtual void OnGroundHit()
    {
        isGrounded = true;
    }

    public virtual void OnEnemyHit(GameObject enemy)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            OnGroundHit();
        }
        if (collision.gameObject.layer == enemyLayer)
        {
            OnEnemyHit(collision.gameObject);
        }
    }

    public virtual void Death()
    {

    }
	
}
