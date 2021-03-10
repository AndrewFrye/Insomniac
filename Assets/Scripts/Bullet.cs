﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bounces = 2;
    public float speed = 5f;
    public Rigidbody2D rb;


    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    List<GameObject> currentCollisions = new List<GameObject>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Add collision to the list
        currentCollisions.Add(collision.gameObject);

        //Check all current collisions for the player
        foreach (GameObject gObject in currentCollisions)
        {
            if (gObject.CompareTag("Enemy"))
            {
                PlayerCollision.enemyBoop(gObject);
                
            }
        }
        Object.Destroy(rb.gameObject);
    }
}