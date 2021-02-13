using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : MonoBehaviour
{
    public Transform self;
    public Transform player;
    public GameObject[] playerGobject;
    public Pathfinding.AIDestinationSetter target;
    public float Range = 10;
    void Start()
    {
        playerGobject = GameObject.FindGameObjectsWithTag("Player");
        player = playerGobject[0].transform;
        target = GetComponent<Pathfinding.AIDestinationSetter>();
        self = GetComponent<Transform>();
    }

    void Update()
    {
        if (CameraMovement.zoom)
        {
            self.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

            self.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            //Reset after zoom back in
            self.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            self.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(placeRay(player, self), player.position);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (hit.distance < Range) target.target = player;
                }
                else
                {
                    target.target = null;
                }
            }
        }
    }

    public Vector2 placeRay(Transform a, Transform b)
    {
        float x = 0;
        float y = 0;

        //Set X
        if (a.position.x > b.position.x) x = b.position.x + 1.5f;
        else if (a.position.x < b.position.x) x = b.position.x - 1.5f;
        else x = b.position.x;

        //Set Y
        if (a.position.y > b.position.y) y = b.position.y + 1.5f;
        else if (a.position.y < b.position.y) x = b.position.y - 1.5f;
        else x = b.position.y;

        return new Vector2(x, y);
    }

}
