using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum ThrowState { IDLE, THROW, OUT_OF_AMMO }
public class Throw : MonoBehaviour, IEquippableItem
{
    public PoolManager pm;
    public ThrowData throwData;
    bool isHolding;

    public UnityEvent OnFinished { get; set; } = new UnityEvent();
    ThrowState throwState;
    
    Vector2 initVel;

    //Projectile Path
    public bool showTrajectoryLine = true;
    BezierPath bPath;
    LineRenderer path;
    //total projectile's travelling time from player to mouse position
    const float t = 1.5f;

    //For initial velocity and projectile path
    Vector3 g = Physics.gravity;
    Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        throwState = ThrowState.IDLE;
        
        if (showTrajectoryLine)
        {
            path = GetComponent<LineRenderer>();
            if(path == null)
            {
                path = gameObject.AddComponent<LineRenderer>();
            }
            bPath = new BezierPath(path);
        }
        else
        {
            path = null;
            bPath = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (throwState == ThrowState.THROW)
        {
            InvokeThrow();
        }else if (throwState == ThrowState.IDLE && showTrajectoryLine)
        {
            UpdateTrajectory();
        }
    }

    void InvokeThrow()
    {
        GameObject newObject = pm.GetObject();
        newObject.transform.position = transform.position;

        Rigidbody2D rb2d = newObject.GetComponent<Rigidbody2D>();
        rb2d.velocity = initVel;

        if(pm.outofAmmo)
        {
            throwState = ThrowState.OUT_OF_AMMO;
            if (showTrajectoryLine)
            {
                bPath.path.enabled = false;
            }
        }
        else throwState = ThrowState.IDLE;
    }

    public void AimThrow(Vector2 mp)
    {
        //Convert the screen mouse position relative to world position
        mousePosition = Camera.main.ScreenToWorldPoint(mp);
        //Calculate the v0 neede to reach mousePosition
        initVel = (mousePosition - transform.position - 0.5f * Physics.gravity * t* t) / t;
    }

    void UpdateTrajectory()
    {
        //Create Control Points
        List<Vector3> controlpoints = new List<Vector3>();
        Vector3 p0 = transform.position;
        Vector3 p3 = mousePosition;
        Vector3 v0 = initVel;

        float c = 0.125f;
        Vector3 p2 = p3 - (g * t * t + v0 * t) / 3;
        Vector3 p1 = (c * g * t * t + 0.5f * v0 * t + p0 - c * (p0 + p3)) / (3 * c) - p2;

        controlpoints.Add(p0);
        controlpoints.Add(p1);
        controlpoints.Add(p2);
        controlpoints.Add(p3);

        //Reset the Trajectory
        bPath.ResetPath();
        //Find and update the Path
        bPath.FindBezierPath(controlpoints);
    }

    public void SetHolding(bool isHolding)
    {
        this.isHolding = isHolding;
        if(this.isHolding && throwState == ThrowState.IDLE)
        {
            throwState = ThrowState.THROW;
        }
    }
}
