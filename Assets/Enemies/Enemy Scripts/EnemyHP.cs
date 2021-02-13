using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;
    private GameObject self;
    private Vector3 startLocation;

    void awake()
    {
        self = GetComponent<Transform>().gameObject;
    }

    private void Start()
    {
        startLocation = GetComponent<Transform>().position;
    }

    void Update()
    {
       // if (self.GetComponent<EnemyHP>().HP <= 0) Destroy(self);
    }
}
