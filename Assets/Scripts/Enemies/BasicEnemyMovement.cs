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
    public float speed = 1.5f;
    public float delta = 2.0f;

    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector2 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
