using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    Vector3 mousePosition;
    public GameObject primaryArm;
    public GameObject secondaryArm;
    void LookTowardCursor()
    {
        // Finds the position of the mouse cursor
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // flips the player to look in the horizontal direction of the mouse cursor
        if (mousePosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            primaryArm.transform.Find("Arm").GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
            primaryArm.transform.Find("Barrel").GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
            primaryArm.transform.Find("Handle").GetComponentInChildren<SpriteRenderer>().sortingOrder = 0;
            secondaryArm.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else 
        { 
            transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            primaryArm.transform.Find("Arm").GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            primaryArm.transform.Find("Barrel").GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            primaryArm.transform.Find("Handle").GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            secondaryArm.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardCursor();
    }
}
