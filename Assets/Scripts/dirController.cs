using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirController : MonoBehaviour
{
    public Pathfinding.AIPath path;
    public SpriteRenderer renderer;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Pathfinding.AIDestinationSetter>().target = player.GetComponent<Transform>();
    }

    void Update()
    {
        if(path.desiredVelocity.x < 0) renderer.flipX = true;
        else renderer.flipX = false;
    }
}
