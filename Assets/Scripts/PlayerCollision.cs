using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private static int iFrames;
    public static bool dead;
    public static Transform player;
    public static Collider2D playerCollider;
    public HealthManagement hpsys;
    public BasicMovement move;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        player = GetComponent<Transform>();
        iFrames = 0;
        dead = false;
    }
    private void Start()
    {
        GameObject x = GameObject.FindGameObjectWithTag("HealthManager");
        hpsys = x.GetComponent<HealthManagement>();
    }

    List<GameObject> currentCollisions = new List<GameObject>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Add collision to the list
        currentCollisions.Add(collision.gameObject);

        //Check all current collisions for the player
        foreach (GameObject gObject in currentCollisions)
        {
            if (gObject.CompareTag("Enemy") || gObject.CompareTag("JSONEnemy") && iFrames == 0)
            {
                playerEnemyCollide(gObject, player);
            }
            if (gObject.tag.Equals("instantKill"))
            {
                hpsys.dead = true;
                Debug.Log("Dead");
            }
            if (collision.gameObject.CompareTag("ResetJump")) move.groundTest = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentCollisions.Remove(collision.gameObject);
        if (collision.gameObject.CompareTag("ResetJump")) move.groundTest = false;
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

    public void playerDamage()
    {
        hpsys.hp--;
        if (hpsys.hp == 0) dead = true;
        iFrames = 30;
    }

    public static void enemyBoop(GameObject e)
    {
        e.GetComponent<EnemyHP>().HP--;
        if (e.GetComponent<EnemyHP>().HP <= 0)
        {
            Object.Destroy(e);
        }
    }
}
