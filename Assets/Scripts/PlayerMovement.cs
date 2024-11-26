using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    private float Move;
    private Animator anim;
    private bool isFacingRight;

    public float speed;
    public float castDistance;
    public float jump;

    private bool canDoubleJump;
    public bool isGrounded;
    public Vector2 boxSize;
    public LayerMask groundLayer;

    void Start()
    {
        isFacingRight = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isItGrounded();
        Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);
    
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Move != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        anim.SetBool("isJumping", !isGrounded);

        if(!isFacingRight && Move > 0)
        {
            Flip();
        }
        else if(isFacingRight && Move < 0)
        {
            Flip();
        }
        
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(rb.velocity.x, jump * 10));
            canDoubleJump = true;
        }
        else if(canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(rb.velocity.x, jump * 10));
            canDoubleJump = false;
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    public void isItGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    // public bool isWalled()
    // {
    //     if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, wallLayer))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // private void WallSlide()
    // {
    //     if(isWalled() && !isGrounded())
    //     {
    //         isWallSliding = true;
    //         rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
    //     }
    //     else
    //     {
    //         isWallSliding = false;
    //     }
    // }

    // private void WallJump()
    // {
    //     if(isWallSliding)
    //     {
    //         isWallJumping = false;

    //         wallJumpingDirection = isFacingRight ? -1f : 1f;
    //         wallJumpingCounter = wallJumpingTime;

    //         CancelInvoke(nameof(StopWallJumping));
    //     }
    //     else
    //     {
    //         wallJumpingCounter -= Time.deltaTime;
    //     }

    //     if(Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
    //     {
    //         isWallJumping = true;
    //         rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x , wallJumpingPower.y);
    //         wallJumpingCounter = 0f;

    //         if(transform.localScale.x != wallJumpingDirection)
    //         {
    //             isFacingRight = ! isFacingRight;
    //             Vector3 localScale = transform.localScale;
    //             localScale.x *= -1f;
    //             transform.localScale = localScale;

    //         }

    //         Invoke(nameof(StopWallJumping), wallJumpingDuration);
    //     }
    // }

    // private void StopWallJumping()
    // {
    //     isWallJumping = false;
    // }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if(other.gameObject.CompareTag("Ground"))
    //     {
    //         Vector3 normal = other.GetContact(0).normal;
    //         if(normal == Vector3.up)
    //         {
    //             grounded = true;
    //         }
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if(other.gameObject.CompareTag("Ground"))
    //     {
    //         grounded = false;
    //     }
    // }
}
 
