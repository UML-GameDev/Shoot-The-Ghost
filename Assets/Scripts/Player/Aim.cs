using UnityEngine;

public class Aim : MonoBehaviour
{
    public InputManager input;
    Vector3 mousePosition;
    Vector3 direction;

    float gunAngle;

    bool holding = false;
    float time;

    public Transform barrelTransform;
    public Transform pivot;

    public SpriteRenderer primaryArm;
    public SpriteRenderer secondaryArm;

    float phaseAngle;

    void OnEnable(){
        input.lookEvent += AimAtCursor;
        input.attackEvent += ShootState;
    }

    void OnDiable(){
        input.lookEvent -= AimAtCursor;
        input.attackEvent = null;
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

     void ShootBullet() {
        // Calls Poolmanager to instantiate a bullet
        GameObject bulletObject = PoolManager.Instance.GetBullet();
        // Sets bullet rotation to be nearly equal to the barrel rotation, with a random offset between -10 degrees and 10 degrees
        bulletObject.transform.rotation = barrelTransform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10));
        bulletObject.transform.position = barrelTransform.position + barrelTransform.right;  
    }

    void Update(){
        if(holding){
            if (time >= .25){     
            ShootBullet();
            time = 0;
            }
            time += Time.deltaTime;
        }
    }

    void ShootState(bool hold){
        holding = hold;
        time = .30f;
    }
}
