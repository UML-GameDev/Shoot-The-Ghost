using System;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.Serialization;
using UnityEngine.SceneManagement;


[System.Serializable]

public class PlayerController : MonoBehaviour, HealthUpdatable
{
    //Related to Speed of Player Movement and Jumping
    public float maxSpeed = 12;
    public float smoothTime = 0.3f;
    public bool debug = true;
    public float initialJumpVelo = 10f;

    public float currHealth { get; set; }
    public float maxHealth = 100f;
    public GameObject healthBar;
    Vector3 healthBarSize;
    Vector3 healthBarPosition;
    public float regenDelay = 1f;
    float regenTimer;
    public float regenRate = 5f;

    public UnityEvent<float> OnHealthUpdated {get; } = new UnityEvent<float>();


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
        currScene = gameObject.scene;
        myCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
        
        // The original dimensions of the healthbar, used in RegenHealth
        healthBarSize = new Vector3(healthBar.transform.localScale.x, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        healthBarPosition = new Vector3(healthBar.transform.localPosition.x, healthBar.transform.localPosition.y, healthBar.transform.localPosition.z);
        
        regenTimer = regenDelay;
    }
    private void FixedUpdate()
    {
        move();
        if (!onGround) checkGround();
        
        if (currHealth <= 0) SceneManager.LoadScene(currScene.name); // Resets the scene if currHealth reaches 0

        if (regenTimer >= 0) regenTimer -= Time.fixedDeltaTime; // Regen timer
        if (regenTimer <= 0 && currHealth < maxHealth) RegenHealth();

    }
    // Player regenerates health when they haven't taken damage within a set period of time
    void RegenHealth ()
    {
        if (currHealth + regenRate <= maxHealth) 
        { 
            currHealth += regenRate * Time.deltaTime;
            healthBar.transform.localScale += new Vector3(regenRate * Time.deltaTime / 100f * healthBarSize.x, 0f, 0f);
            healthBar.transform.localPosition += new Vector3(regenRate * Time.deltaTime / 100f * healthBarSize.x / 2, 0f, 0f);
        } 
        else 
        {
            // Fills the health bar and health when the player has reached a high enough currHealth
            healthBar.transform.localScale = new Vector3(healthBarSize.x, healthBarSize.y, healthBarSize.z);
            healthBar.transform.localPosition = new Vector3(healthBarPosition.x, healthBarPosition.y, healthBarPosition.z);
            currHealth = maxHealth;
            
        }
        
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

    public void TakeDamage(float damage)
    {
        currHealth = (currHealth >= damage) ? currHealth - damage : 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //player takes damage
        var collObj = collision.gameObject;

        if(collObj.layer == LayerMask.NameToLayer("Enemy"))
        {
            float damage = collObj.GetComponent<BasicEnemy>().damage;
            
            TakeDamage(damage);
            OnHealthUpdated.Invoke(currHealth);
        }
        
    }
}