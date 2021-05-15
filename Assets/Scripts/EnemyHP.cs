using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;
    private GameObject self;
    private Vector3 startLocation;

    private void Start()
    {
        self = GetComponent<Transform>().gameObject;
        startLocation = GetComponent<Transform>().position;
    }

    void Update()
    {
       if (self.GetComponent<EnemyHP>().HP <= 0) Destroy(self);
    }

    List<GameObject> currentCollisions = new List<GameObject>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Add collision to the list
        currentCollisions.Add(collision.gameObject);

        //Check all current collisions for the player
        foreach (GameObject gObject in currentCollisions)
        {
            if(gObject.CompareTag("Pebble")) self.GetComponent<EnemyHP>().HP--;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        currentCollisions.Remove(other.gameObject);
    }
}
