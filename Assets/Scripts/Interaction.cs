using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform self;
    public GameObject[] player;
    public List<GameObject> currentTriggers = new List<GameObject>();
    public bool canInteract;

    private void Start()
    {
        self = GetComponent<Transform>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    private void FixedUpdate()
    {
        GameObject[] temp = currentTriggers.ToArray();

        foreach(GameObject x in temp)
        {
            foreach(GameObject y in player)
            {
                if(x == y)
                {
                    BasicMovement.currentInteractable = self.gameObject;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentTriggers.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentTriggers.Remove(collision.gameObject);
    }
}
