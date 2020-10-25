using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*
 * PlayerController
 *      This class handles player movement as well as damages taken from enemy, also handle scene reset(when player died)
 *      
 *      This class inhertis Monobehaviour to attach to GameObject
 *      and HealthUpdatable to handle the damage and Health UI
 *      
 *      This class should attach to Player Object
 */

[System.Serializable]
public class PlayerController : MonoBehaviour, HealthUpdatable
{
    public InputManager input;
    //Related to Speed of Player Movement and Jumping
    public float maxSpeed = 12;
    public float smoothTime = 0.3f;
    public bool debug = true;
    public float initialJumpVelo = 10f;

    public float currHealth { get; set; }
    public float maxHealth = 100f;

    public float regenDelay = 1f;
    public float regenRate = 5f;

    float regenTimer;
    
    public UnityEvent<float> OnHealthUpdated {get; } = new UnityEvent<float>();

    public LayerMask groundMask;
    public Transform groundCheck;
    private Rigidbody2D rb2d;

    private Vector3 currentVel = Vector3.zero;
    private float velVec = 0f;

    private bool onGround = false;

    private Collider2D myCollider;

    Scene currScene;

    void OnEnable(){
        input.moveEvent += MoveVector;
        input.jumpEvent += Jump;
    }

    void OnDisable(){
        input.moveEvent -= MoveVector;
        input.jumpEvent -= Jump;
    }

    // Start is called before the first frame update
    void Start()
    {
        currScene = gameObject.scene;
        myCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
            
        regenTimer = regenDelay;
    }
    void FixedUpdate()
    {
        if(velVec != 0) Move();
        if (!onGround) CheckGround();

        if (currHealth <= 0) SceneManager.LoadScene(currScene.name); // Resets the scene if currHealth reaches 
        if (regenTimer >= 0) regenTimer -= Time.fixedDeltaTime; // Regen timer
        if (regenTimer <= 0 && currHealth < maxHealth) RegenHealth();
    }
    void Move(){
        Vector3 newVel = new Vector3(velVec*maxSpeed,rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity,newVel,ref currentVel,smoothTime);
    }
    // Player regenerates health when they haven't taken damage within a set period of time
    void RegenHealth()
    {
        float regenAmt = regenRate * Time.deltaTime;
        currHealth = (currHealth + regenAmt <= maxHealth) ? currHealth + regenAmt : maxHealth;

        OnHealthUpdated.Invoke(currHealth);
    }
    
    //when player jumped, check the bottom of player to check if the the radius we set hit with ground layer mask
    void CheckGround()
    {
        var collider = Physics2D.OverlapBox(groundCheck.position, new Vector2(transform.localScale.x, 0.001f), 0);
        if(rb2d.velocity.y <= 0 &&  collider != null && collider != myCollider)
        {
            onGround = true;
        }
    }

    public void TakeDamage(float damage)
    {
        //same as if(currHealth >= damage) currHeath -= damage; else currHealth = 0;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet"))
        {
            float damage = collision.gameObject.GetComponent<EnemyBullet>().bulletData.bulletDamage; // Enemy does less damage than the player does

            TakeDamage(damage);
            OnHealthUpdated.Invoke(currHealth);
        }
    }

    //Event function that attach to moveEvent in InputManager for callback
    void MoveVector(Vector2 input)
    {
        velVec = input.x;
    }
    //Event function that attach to jumpEvent in InputManager for callback
    void Jump(){
        if (onGround)
        {
            onGround = false;
            rb2d.AddForce(new Vector2(0f, initialJumpVelo), ForceMode2D.Impulse);
        }
    }

}