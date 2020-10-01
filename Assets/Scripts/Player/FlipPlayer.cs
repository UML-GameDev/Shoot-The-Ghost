using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    Vector3 mousePosition;
    public SpriteRenderer primaryArm;
    public SpriteRenderer secondaryArm;
    public SpriteRenderer gun;
    void LookTowardCursor()
    {
        // Finds the position of the mouse cursor
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // flips the player to look in the horizontal direction of the mouse cursor
        if (mousePosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            secondaryArm.GetComponent<SpriteRenderer>().sortingOrder = 2;
            gun.flipY = true;
            primaryArm.flipY = true;
            secondaryArm.flipY = true;
        }
        else 
        { 
            transform.eulerAngles = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            secondaryArm.GetComponent<SpriteRenderer>().sortingOrder = 0;
            gun.flipY = false;
            primaryArm.flipY = false;
            secondaryArm.flipY = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardCursor();
    }
}
