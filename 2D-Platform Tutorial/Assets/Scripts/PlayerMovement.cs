using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public BoxCollider2D checkPoint;

    private bool isOnGround;
    [SerializeField] private int jumpTimes;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        GroundCheck();
        Jump();
        SwitchAnim();
        Flip();        
    }

    void Run()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void GroundCheck()
    {
        isOnGround = Physics2D.IsTouchingLayers(checkPoint, LayerMask.GetMask("Ground"));
    }

    void Jump()
    {
        if (isOnGround & rb.velocity.y == 0)
        {
            jumpTimes = 2;
        }

        if (Input.GetButtonDown("Jump") && jumpTimes > 0)  
        {
            rb.velocity = Vector2.up * jumpSpeed;
            jumpTimes--;
        }
    }

    void Flip()
    {
        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void SwitchAnim()
    {
        if (isOnGround)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
        }            
        else
        {
            anim.SetBool("Idle", false);
            if (rb.velocity.y > 0)
            {
                anim.SetBool("Jump", true);
                anim.SetBool("Fall", false);
            }
            if (rb.velocity.y < 0)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", true);
            }
        }

    }
}
