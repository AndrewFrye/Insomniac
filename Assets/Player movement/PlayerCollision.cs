﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private static int iFrames;
    public static bool dead;
    public static Transform player;
    public static Collider2D playerCollider;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        player = GetComponent<Transform>();
        iFrames = 0;
        dead = false;
    }

    List<GameObject> currentCollisions = new List<GameObject>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Add collision to the list
        currentCollisions.Add(collision.gameObject);

        //Check all current collisions for the player
        foreach (GameObject gObject in currentCollisions)
        {
            if (gObject.CompareTag("Enemy") && iFrames == 0)
            {
                playerEnemyCollide(gObject, player);
            }
            if (gObject.tag.Equals("instantKill"))
            {
                HealthManagement.dead = true;
                Debug.Log("Dead");
            }
            if (collision.gameObject.CompareTag("ResetJump")) BasicMovement.groundTest = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentCollisions.Remove(collision.gameObject);
        if (collision.gameObject.CompareTag("ResetJump")) BasicMovement.groundTest = false;
    }

    private void playerEnemyCollide(GameObject e, Transform p)
    {
        Transform enemy = e.transform;
        if (iFrames == 0)
        {
            if (enemy.position.y + 0.4 <= p.position.y) enemyBoop(e);
            else playerDamage();
        }
    }

    private void Update()
    {
        if (iFrames > 0) iFrames--;
    }

    public static void playerDamage()
    {
        HealthManagement.hp--;
        if (HealthManagement.hp == 0) dead = true;
        iFrames = 30;
    }

    public static void enemyBoop(GameObject e)
    {
        e.GetComponent<EnemyHP>().HP--;
        if (e.GetComponent<EnemyHP>().HP <= 0) Object.Destroy(e);
    }
}
