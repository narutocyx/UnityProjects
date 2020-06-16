using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool canMove = true;

    private float moveH, moveV;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        moveV = 0;
        if (canMove)
            Flip();
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.velocity = new Vector2(moveH, moveV);
        else
            rb.velocity = Vector2.zero;
    }

    void Flip()
    {
        if (moveH > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (moveH < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
