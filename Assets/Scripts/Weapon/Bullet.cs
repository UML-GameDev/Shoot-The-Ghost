using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public BulletData bulletData;

    float lifeTimer = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = bulletData.lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        // The bullet moves foward in whichever direction it's right side is facing, with a speed of bulletSpeed
        transform.position += transform.right * bulletData.bulletSpeed * Time.deltaTime;
        // The bullet will be deactivated after lifeTimer seconds have passed
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)

    {
        if(collision.tag == "Player") return;
        lifeTimer = 0;

    }
}
