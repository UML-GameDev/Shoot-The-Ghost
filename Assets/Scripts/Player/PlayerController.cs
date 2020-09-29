using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    //Related to Speed of Player Movement and Jumping
    public float maxSpeed = 12;
    public float smoothTime = 0.3f;
    public bool debug = true;
    public float initialJumpVelo = 10f;


    public LayerMask groundMask;
    public Transform groundCheck;
    private Rigidbody2D rb2d;

    private Vector3 currentVel = Vector3.zero;

    private bool onGround = false;

    private GameObject currentGround;

    private Collider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
       move();
       if (!onGround) checkGround();
    }

    //basic movement
    void move()
    {
        bool isJump = Input.GetKey(KeyCode.Space);
        float h = Input.GetAxisRaw("Horizontal");

        if (h != 0f)
        {
            Vector3 newVel = new Vector3(h * maxSpeed, rb2d.velocity.y);
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity,newVel,ref currentVel,smoothTime);
        }
        if (onGround && isJump)
        {
            onGround = false;
            rb2d.AddForce(new Vector2(0f, initialJumpVelo), ForceMode2D.Impulse);
        }
    }


    
    //when player jumped, check the bottom of player to check if the the radius we set hit with ground layer mask
   private void checkGround()
    {
        var collider = Physics2D.OverlapBox(groundCheck.position, new Vector2(transform.localScale.x, 0.001f), 0);
        if(rb2d.velocity.y <= 0 &&  collider != null && collider != myCollider)
        {
            onGround = true;
        }    
    }
}