using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBossProjectile : MonoBehaviour
{
    public int bounces = 1;
    public float speed = 20f;
    public Rigidbody2D rb;
    public bool test;
    public HealthManagement hpsys;


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
            if (gObject.CompareTag("Player"))
            {
                hpsys.hp--;
            }
        }
        Object.Destroy(rb.gameObject);
    }
}
