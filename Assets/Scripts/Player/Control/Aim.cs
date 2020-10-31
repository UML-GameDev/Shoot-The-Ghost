using UnityEngine;

/*
 * Aim
 *      This class handles transformation of player arms and weapon based on player and mouse position
 *      This class inherits Monobehaviour so it can attach to GameObject
 * 
 *      This class should attach to Player Object
 *      On Editor, assign InputManager class, pivot point, and sprite for player arms
 *      
 */
public class Aim : MonoBehaviour
{
    public InputManager input;
    public Transform playerTransform;

    float gunAngle;

    public SpriteRenderer primaryArm;
    public SpriteRenderer secondaryArm;

    float phaseAngle;

    void OnEnable()
    {
        input.lookEvent += AimAtCursor;
    }

    void OnDisable()
    {
        input.lookEvent -= AimAtCursor;
    }
    void OnDestroy()
    {
        input.lookEvent -= AimAtCursor;
    }

    void AimAtCursor(Vector2 mp)
    {
        //Convert the screen mouse position relative to world position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mp);
        // Finds the distance between the mouse cursor and the player's arm
        Vector3 direction = mousePosition - transform.position;

        // Finds the angle at which the gun should be pointing by taking the inverse tangent of mousePosition.y over mousePosition.x, converts to degrees
        gunAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Find the dot product between right(1,0,0) and norm of direction vector to see which side the player is facing
        float dp = Vector3.Dot(Vector3.right, direction.normalized);


        //If the player is facing right and mouse is facing left
        if (playerTransform.eulerAngles.y == 0 && dp < 0)
        {
            //Flip player to left side
            playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, 180f, playerTransform.eulerAngles.z);
            phaseAngle = 180;

            //raise the sort order so left arm is over right arm
            secondaryArm.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        //If the player is facing left and mouse is facing right
        else if (playerTransform.eulerAngles.y != 0 && dp > 0)
        {

            //Flip player to right side
            playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, 0f, playerTransform.eulerAngles.z);
            phaseAngle = 0;

            //lower the sort order so left arm is under right arm
            secondaryArm.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        //rotate the pivot point of arm accoridng to the angle
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Sign(dp) * (gunAngle + phaseAngle));
    }
}
