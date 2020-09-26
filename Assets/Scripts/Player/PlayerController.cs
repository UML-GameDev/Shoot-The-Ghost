using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    //Related to Speed of Player Movement and Jumping
    [SerializeField] private float Speed= 40f;
    [SerializeField] private float JumpForce= 10f;
    [Range(0,0.3f)] [SerializeField] private float smoothTime = 0.3f;
    

    //Related to Checking Ground when Player Jump
    public Transform groundCheck;
    public LayerMask groundMask;

    private Rigidbody2D rb2d;

    private Vector3 currentVel = Vector3.zero;
    //private bool facingRight = true;

    private bool onGround = true;
    private const float groundCheckRadius = .2f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal") * Speed;
        bool isJump = Input.GetKey(KeyCode.Space);

        Movement(horizontal_input*Time.fixedDeltaTime, isJump);
    }
    private void FixedUpdate()
    {
        if (!onGround) CheckGround();
    }

    //basic movement
    void Movement(float h_input,bool isJump)
    {

        if (h_input != 0f)
        {
            Vector3 newVel = new Vector3(h_input*10f, rb2d.velocity.y);
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity,newVel,ref currentVel,smoothTime);
        }

        //if ((h_input > 0 && !facingRight) || (h_input < 0 && facingRight))
        //{
        //    Flip();
       //}

        if (onGround && isJump)
        {
            onGround = false;
            rb2d.AddForce(new Vector2(0f, JumpForce*10f));
        }
    }

    //when player jumped, check the bottom of player to check if the the radius we set hit with ground layer mask
   private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                onGround = true;
                break;
            }
        }
    }

    //flip the player based on the direction of player moving (can't really see that rn because it's just a rectangle with flat color)
    //private void Flip()
    //{
        // Switch the way the player is labelled as facing.
     //   facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
     //   Vector3 theScale = transform.localScale;
     //   theScale.x *= -1;
     //   transform.localScale = theScale;
   //}
}
