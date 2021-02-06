using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFall : MonoBehaviour
{
    public Transform stalagtite;
    public stalagtiteFall fall;
    bool falling = false;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(stalagtite.position.x, stalagtite.position.y-1f, stalagtite.position.z) , -Vector2.up);
        if (hit.collider.gameObject.CompareTag("Player")) fall.falling();
    }

    List<GameObject> currentCollisions = new List<GameObject>();
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        currentCollisions.Add(collision.gameObject);
        foreach(GameObject gObject in currentCollisions)
        {
            if (gObject.tag.Equals("Projectile")) 
            {
                fall.fall();
            }
        }

    }*/
}
