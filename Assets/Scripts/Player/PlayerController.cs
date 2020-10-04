
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public InputManager input;
    //Related to Speed of Player Movement and Jumping
    public float maxSpeed = 12;
    public float smoothTime = 0.3f;
    public bool debug = true;
    public float initialJumpVelo = 10f;

    float currHealth;
    public float maxHealth = 100f;
    public GameObject healthBar;

    public LayerMask groundMask;
    public Transform groundCheck;
    private Rigidbody2D rb2d;

    private Vector3 currentVel = Vector3.zero;
    private float velVec = 0f;

    private bool onGround = false;

    private GameObject currentGround;

    private Collider2D myCollider;

    void OnEnable(){
        input.moveEvent += Move;
        input.jumpEvent += Jump;
    }

    void OnDiable(){
        input.moveEvent -= Move;
        input.jumpEvent -= Jump;
    }

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }
    private void FixedUpdate()
    {
        if(velVec != 0f){
            Vector3 newVel = new Vector3(velVec*maxSpeed,rb2d.velocity.y);
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity,newVel,ref currentVel,smoothTime);
        }
        if (!onGround) CheckGround();
    }

    //basic movement
    void Move(Vector2 input)
    {
        velVec = input.x;
    }

    void Jump(){
        if (onGround)
        {
            onGround = false;
            rb2d.AddForce(new Vector2(0f, initialJumpVelo), ForceMode2D.Impulse);
        }
    }

    //when player jumped, check the bottom of player to check if the the radius we set hit with ground layer mask
   private void CheckGround()
    {
        var collider = Physics2D.OverlapBox(groundCheck.position, new Vector2(transform.localScale.x, 0.001f), 0);
        if(rb2d.velocity.y <= 0 &&  collider != null && collider != myCollider)
        {
            onGround = true;
        }    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //player takes damage
        if(collision.CompareTag("Enemy")&& currHealth >= 0)
        {
            float damage = collision.GetComponent<BasicEnemy>().damage;
            currHealth -= damage;
            healthBar.transform.localScale -= new Vector3(damage / 100f, 0f, 0f);
        }
    }
}