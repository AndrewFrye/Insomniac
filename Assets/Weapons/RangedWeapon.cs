using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    Transform tf;
    public Transform player;
    private void Awake()
    {
        tf = GetComponent<Transform>();

    }
    void Update()
    {
        Vector3 point = player.position;
        Vector3 axis = new Vector3(0, 0, 1);
        tf.RotateAround(point, axis, Time.deltaTime * 10);
    }
}
