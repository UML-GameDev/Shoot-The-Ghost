using UnityEngine;

/*
 * Ghost
 *      This is a class where it handles the Ghost Object which spawns after the Enemies are dead, also follow and attack the player
 *      This class inherits MonoBehaviour
 *      
 *      This class should attach to any ghost class
 */
enum GhostState { CHASING_PLAYER, SUBDUED }
public class Ghost : MonoBehaviour
{
    GhostState ghostState;
    Transform target;

    public float speed;
    public float damagePerSec = 10;

    float targetTime, t, randNum, targetRandNum;

    Vector3 moveDir;

    //particle effect
    ParticleSystem.EmissionModule noteEffectEmi;

    void Start()
    {
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;

        ghostState = GhostState.CHASING_PLAYER;
        noteEffectEmi = GetComponent<ParticleSystem>().emission;
        noteEffectEmi.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distVec = new Vector3();
        if (target)
        {
            distVec = target.transform.position - transform.position;
        }
        else
        {
            Debug.Log("No Target Found");
            Destroy(gameObject);
        }

        t += Time.deltaTime;

        if (t >= targetTime)
        {
            targetTime = t + 1.5f;
            randNum = targetRandNum;
            targetRandNum = Random.Range(-30, 30);
        }

        randNum = Mathf.Lerp(randNum, targetRandNum, Time.deltaTime * 2f);

        moveDir = Vector3.Slerp(moveDir, Quaternion.Euler(0, 0, randNum) * distVec.normalized, Time.deltaTime * 10f);
        transform.position = transform.position + moveDir * speed * Time.deltaTime;
    }

    void FindTarget()
    {
        //TODO-See on forum said use spherecast instead so that it could take in less enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        float closestDistance = Mathf.Infinity;
        foreach(GameObject enemy in enemies)
        {
            float distance = (enemy.transform.position - transform.position).magnitude;
            if (closestDistance > distance && enemy.GetComponent<Renderer>().isVisible)
            {
                target = enemy.transform;
                closestDistance = distance;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(ghostState == GhostState.CHASING_PLAYER && collider.CompareTag("Violin"))
        {
            ghostState = GhostState.SUBDUED;
            target = null;
            FindTarget();
            noteEffectEmi.enabled = true;  
            Debug.Log("Ghost Subdued");
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(ghostState == GhostState.CHASING_PLAYER && collider.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().TakeDamage(damagePerSec * Time.deltaTime);
        }else if(ghostState == GhostState.SUBDUED && collider.CompareTag("Enemy"))
        {
            //if should one hit kill the enemy
            collider.gameObject.GetComponent<BasicEnemy>().TakeDamage(200f);
            Destroy(gameObject);
        }
    }
}
