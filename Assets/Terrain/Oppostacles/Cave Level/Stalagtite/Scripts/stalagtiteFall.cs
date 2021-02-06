using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalagtiteFall : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer render;
    public Sprite broken;
    public Sprite normal;
    public int countdown = -1;

    private void Start()
    {
        render.sprite = normal;
    }

    public void fall()
    {
        animator.StartPlayback();
        countdown = 30;
    }

    public void falling()
    {
        render.sprite = broken;
        rb.gravityScale = 1f;
        rb.AddForce(new Vector3(0, -10, 0));
    }

    private void Update()
    {
        if (countdown > 0) countdown--;
        if (countdown == 0) falling();
    }
}
