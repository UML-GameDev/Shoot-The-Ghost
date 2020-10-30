using System.ComponentModel.Design;
using UnityEngine;

/*
 * BasicEnemyMovement
 *      This class handles the movement of the enemy
 *      This class inherit Monobehaviour
 *      
 *      This class should attach to any basic Enemy Object
 */ 
public class BasicEnemyMovement : MonoBehaviour
{
    public enum EnemyState { PASSIVE, AGGRESSIVE };
    public EnemyState enemyState;
    
    public float speed = 1.5f;
    public float delta = 2.0f;
    private float currTime;

    private Vector2 startPos;
    
    void Start()
    {
        startPos = transform.position;
        enemyState = 0; // Set enemystate to PASSIVE by default
        currTime = 0f;
        delta = Random.Range(-5, 5);
    }

    void Update()
    {
        if (enemyState == EnemyState.PASSIVE)
        {
            transform.position = new Vector3(startPos.x + delta * Mathf.Sin(currTime * speed), transform.position.y, 0);
            currTime += Time.deltaTime;
        }
    }
}
