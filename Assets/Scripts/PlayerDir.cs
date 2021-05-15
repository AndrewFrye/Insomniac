using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour
{
    public Rigidbody2D path;
    public SpriteRenderer renderer;
    public Sprite still;
    public Animator animator;
    public Transform pos;
    public Rigidbody2D rb2d;
    bool flipped;

    void Start()
    {
        pos = renderer.gameObject.GetComponent<Transform>();
        animator = renderer.gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Face movement Direction
        if(path.velocity.x < 0) {
            renderer.flipX = true;
            
            if(!flipped){
                pos.position = new Vector3(pos.position.x - 0.5f, pos.position.y, pos.position.z); 
                flipped=true;
            }
        }
        else if(path.velocity.x > 0) {
            renderer.flipX = false;
            if(flipped){
                pos.position = new Vector3(pos.position.x + 0.5f, pos.position.y, pos.position.z);
                flipped=false;
            } 
        }

        //Start & Stop animation
        if(rb2d.velocity.x != 0f){
            animator.enabled = true;
        } else {
            animator.enabled = false;
            renderer.sprite = still;
        }
    }
}
