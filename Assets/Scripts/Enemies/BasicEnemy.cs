using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currHealth;
    public float damage = 10f;
    public float attackRate = 0.5f;
    public float attackTimer;

    public GameObject healthBar;
    
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            currHealth -= collision.GetComponent<Bullet>().bulletData.bulletDamage;
            healthBar.transform.localScale -= new Vector3(collision.GetComponent<Bullet>().bulletData.bulletDamage / 100f, 0f, 0f);
        }
    }

}
