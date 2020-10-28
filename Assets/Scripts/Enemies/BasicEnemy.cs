using UnityEngine;
using UnityEngine.Events;

/*
 * BasicEnemy
 *      This class handles basic enemy behaviour such as managing damage
 *      
 *      This class inhertis Monobehaviour and HealthUpdate
 *      This class should attach to any basic enemy objects
 */ 
[System.Serializable]
public class BasicEnemy : MonoBehaviour, HealthUpdatable
{
    public float maxHealth = 100f;
    public float damage = 15f;
    
    public UnityEvent<float> OnHealthUpdated {get; } = new UnityEvent<float>();
    
    public float currHealth { get; set; }     
    public float attackRate = 0.5f;	    
    public float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        attackTimer = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth <= 0) BoxCollider2D.Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currHealth = (currHealth >= damage) ? currHealth - damage : 0;
        OnHealthUpdated.Invoke(currHealth);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collObj = collision.gameObject;

        bool tookDamage = false;
        float damage = 0f;
        if (collision.CompareTag("Bullet"))
        {
            damage = collObj.GetComponent<Bullet>().bulletData.bulletDamage;
            tookDamage = true;
        }
        else if(collision.CompareTag("Throwable"))
        {
            damage = collObj.GetComponent<Throwable>().throwableData.damage;
            tookDamage = true;
        }

        if(!tookDamage) return;

        TakeDamage(damage);
    }
}
