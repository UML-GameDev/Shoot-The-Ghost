using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum GhostState { CHASING_PLAYER, SUBDUED }
public class Ghost : MonoBehaviour
{

    GhostState ghostState;
    public Transform player;

    public float speed;
    public float damagePerSec = 10;

    float targetTime, t, randNum, targetRandNum;

    Vector3 moveDir;

    void Start()
    {
        ghostState = GhostState.CHASING_PLAYER;
    }

    // Update is called once per frame

    void Update()
    {
        if(ghostState == GhostState.CHASING_PLAYER)
        {
            Vector3 distVec = player.transform.position - transform.position;

            t += Time.deltaTime;

            if(t >= targetTime)
            {
                targetTime = t + 1.5f;
                randNum = targetRandNum;
                targetRandNum = Random.Range(-50, 50);
            }

            randNum = Mathf.Lerp(randNum, targetRandNum, Time.deltaTime * 2f);

            moveDir = Vector3.Slerp(moveDir, Quaternion.Euler(0, 0, randNum) * distVec.normalized, Time.deltaTime * 10f);
            transform.position = transform.position + moveDir * speed * Time.deltaTime;
        }

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(ghostState == GhostState.CHASING_PLAYER && collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().TakeDamage(damagePerSec * Time.deltaTime);
        }
    }
}
