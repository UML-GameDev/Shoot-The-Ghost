using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Aim : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 direction;

    float gunAngle;

    float time;

    public GameObject barrel;
    public Transform pivot;
    public void AimAtCursor()
    {
        // Sets mousePosition to the mouse cursors position, direction to the position of the player's arm
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Finds the distance between the mouse cursor and the player's arm
        Vector3 direction = mousePosition - transform.position;
        
        // Finds the angle at which the gun should be pointing by taking the inverse tangent of mousePositiony over mousePositionx, converts to degrees
        gunAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       
        // Rotates the player's arm
        pivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
    }

    public void ShootBullet() {
            // Calls Poolmanager to instantiate a bullet
            GameObject bulletObject = PoolManager.Instance.GetBullet();
            // Sets bullet rotation to be nearly equal to the barrel rotation, with a random offset between -10 degrees and 10 degrees
            bulletObject.transform.rotation = barrel.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10));
            bulletObject.transform.position = barrel.transform.position + barrel.transform.right;
            time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AimAtCursor();

        if(Input.GetMouseButtonDown(0)) {
            // Player can still shoot by repeatedly clicking lmb
            ShootBullet();
        }

        if (Input.GetMouseButton(0)) {
            // Gun has a set firerate when the player holds down the left mouse button
            time += Time.deltaTime;

            if (time >= .25)
            {
                ShootBullet();               
            }
        }
        
    }
}
