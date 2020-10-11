using UnityEngine;

public class Aim : MonoBehaviour
{
    public InputManager input;
    Vector3 mousePosition;
    Vector3 direction;

    float gunAngle;


    public Transform barrelTransform;
    public Transform pivot;

    public SpriteRenderer primaryArm;
    public SpriteRenderer secondaryArm;

    float phaseAngle;

    void OnEnable(){
        input.lookEvent += AimAtCursor;
    }

    void OnDiable(){
        input.lookEvent -= AimAtCursor;
    }

    void OnDestroy()
    {
        input.lookEvent -= AimAtCursor;
    }
    void AimAtCursor(Vector2 mp)
    {
        // Sets mousePosition to the mouse cursors position, direction to the position of the player's arm
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mp);
        // Finds the distance between the mouse cursor and the player's arm
        Vector3 direction = mousePosition - pivot.position;
        
        // Finds the angle at which the gun should be pointing by taking the inverse tangent of mousePositiony over mousePositionx, converts to degrees
        gunAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        float dp = Vector3.Dot(Vector3.right, direction.normalized);

        if(transform.eulerAngles.y == 0 && dp < 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            phaseAngle = 180;

            secondaryArm.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else if(transform.eulerAngles.y != 0 && dp > 0){
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
            phaseAngle = 0;

            secondaryArm.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        pivot.eulerAngles = new Vector3(pivot.eulerAngles.x, pivot.eulerAngles.y, Mathf.Sign(dp) * (gunAngle + phaseAngle));
    }
}
