using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private float horizontalInput;
    private bool grounded;
    private Vector2 lookDirection;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        


        //flip player sprite when moving left-right
        if (horizontalInput > 0.01f) transform.localScale = new Vector3(6,6,6);
        else if (horizontalInput < 0.01f) transform.localScale = new Vector3(-6,6,6);

        if (Input.GetKey(KeyCode.W) && grounded) Jump();

        // Animation things
        if (!Mathf.Approximately(horizontalInput, 0.0f))
        {
            lookDirection.Set(horizontalInput, 0);
            lookDirection.Normalize();
        }
        anim.SetBool("run", !Mathf.Approximately(horizontalInput, 0.0f));
        anim.SetBool("grounded", grounded);
        anim.SetFloat("lookX", lookDirection.x);

    }

    void Jump()
    {  
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        grounded = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") grounded = true;
    }

}
