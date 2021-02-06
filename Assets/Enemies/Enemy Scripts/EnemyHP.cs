using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;
    private GameObject self;

    void awake()
    {
        self = GetComponent<Transform>().gameObject;
    }
    void Update()
    {
       // if (self.GetComponent<EnemyHP>().HP <= 0) Destroy(self);
    }
}
