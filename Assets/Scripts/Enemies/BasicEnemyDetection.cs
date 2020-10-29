using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDetection : MonoBehaviour
{
    float gunAngle;

    public PoolManager pm;
    public GunShooter enemyShooter;

    //pivot point of arm
    public Transform pivot;
    public Transform gunBarrel;

    float phaseAngle;
    public Transform player;

    public int playerDetected;
    public float fireRate = 0.9f;
    public float currFireRate;

    private void Start()
    {
        currFireRate = fireRate;
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25f);
        int i;
        for(i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].CompareTag("Player"))
            {
                player = colliders[i].GetComponentInParent<Transform>();

                gameObject.GetComponent<BasicEnemyMovement>().enemyState = BasicEnemyMovement.EnemyState.AGGRESSIVE;
                playerDetected = 1;
                break;
            }
            if(i == colliders.Length - 1)
            {
                playerDetected = 0;
            }
        }
        if(playerDetected == 1)
        {
            AimAtPlayer();
            enemyShooter.setHolding(true);
        }
        if (playerDetected == 0)
        {
            gameObject.GetComponent<BasicEnemyMovement>().enemyState = BasicEnemyMovement.EnemyState.PASSIVE;
            enemyShooter.setHolding(false);
        }
    }

    void AimAtPlayer()
    {
        //Convert the screen mouse position relative to world position
        Vector3 playerPosition = player.position;
        // Finds the distance between the mouse cursor and the player's arm
        Vector3 direction = playerPosition - pivot.position;

        // Finds the angle at which the gun should be pointing by taking the inverse tangent of mousePosition.y over mousePosition.x, converts to degrees
        gunAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        //Find the dot product between right(1,0,0) and norm of direction vector to see which side the player is facing
        float dp = Vector3.Dot(Vector3.right, direction.normalized);


        //If the player is facing right and mouse is facing left
        if (transform.eulerAngles.y == 0 && dp < 0)
        {
            //Flip player to left side
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            phaseAngle = 180;
        }
        //If the player is facing left and mouse is facing right
        else if (transform.eulerAngles.y != 0 && dp > 0)
        {

            //Flip player to right side
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
            phaseAngle = 0;
        }

        //rotate the pivot point of arm according to the angle
        pivot.eulerAngles = new Vector3(pivot.eulerAngles.x, pivot.eulerAngles.y, Mathf.Sign(dp) * (gunAngle + phaseAngle));
    }
}
