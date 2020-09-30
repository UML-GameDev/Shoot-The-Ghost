using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    // Start is called before the first frame update
    //public float moverExtent;
    public Transform startPos;
    public Transform endPos;
    public Transform platform;
    private Transform goal;
    

    public float speed;

    void Start()
    {
        goal = endPos;
    }

    // Update is called once per frame 
    void Update()
    {
        float step =  speed * Time.deltaTime; // calculate distance to move

        platform.position = Vector3.MoveTowards(platform.position, goal.position, step);    

        if((goal.position - platform.position).magnitude < step)
            goal = (goal == endPos) ? startPos : endPos;
    }
}
