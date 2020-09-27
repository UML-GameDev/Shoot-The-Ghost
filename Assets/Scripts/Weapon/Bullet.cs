using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public BulletData bulletData;

    float lifeTimer = 0;
    Vector3 mousePosition;
    Vector2 direction;

    public void AimAtCursor()
    {
        // I don't think i need this method
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = new Vector2(
            mousePosition.x, mousePosition.y);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        AimAtCursor();
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
}
