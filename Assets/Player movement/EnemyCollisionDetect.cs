using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetect : MonoBehaviour
{
    public static bool hit;
    private int hitTimer;
    public static bool enemyHit;
    List<GameObject> currentCollisions = new List<GameObject>();
    GameObject enemy;

    private void Awake()
    {

    }
    private void Update()
    {
        if (hitTimer == 0 && hit) hit = false;
        else hitTimer--;
    }
    void OnCollsiionEnter2D(Collision2D col)
    {
        //Add collision to the list
        currentCollisions.Add(col.gameObject);

        //Check all current collisions for the player
        foreach (GameObject gObject in currentCollisions)
        {
            if (gObject.CompareTag("PlayerProjectile"))
            {
                enemy.SetActive(false);
            } 
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        //Remove object from the list when no longer colliding
        currentCollisions.Remove(col.gameObject);
    }
}
