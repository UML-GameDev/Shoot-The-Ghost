using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

enum ThrowState { IDLE, THROW, OUT_OF_AMMO }
public class Throw : WeaponBase, IEquippableItem
{
    public ThrowData throwData;
    int currCount;
    ThrowState throwState;

    public PoolManager pm;

    public bool showTrajectoryLine = true;

    Vector2 initVel;

    //Projectile Path
    BezierPath bPath;
    LineRenderer path;
    const float t = 1.5f;

    //for projectile path
    Vector3 g = Physics.gravity;
    Vector3 mousePosition;

    public Throw() : base(WeaponType.THROWABLE) { }

    // Start is called before the first frame update
    void Start()
    {
        throwState = ThrowState.IDLE;
        currCount = throwData.maxAmmo;
        
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

        //TODO use poolmanager to check the ammo count
        currCount -= 1;
        if (currCount <= 0)
        {
            throwState = ThrowState.OUT_OF_AMMO;
            if (showTrajectoryLine)
            {
                bPath.path.enabled = false;
            }
        }
        else throwState = ThrowState.IDLE;
    }

    public void aimThrow(Vector2 mp)
    {
        //Convert the screen mouse position relative to world position
        mousePosition = Camera.main.ScreenToWorldPoint(mp);

        //Calculate the v0 neede to reach mousePosition
        initVel = (mousePosition - transform.position - 0.5f * (Vector3)Physics2D.gravity * t* t) / t;
    }

     void UpdateTrajectory()
    {
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

        bPath.ResetPath();
        bPath.FindBezierPath(controlpoints);
    }

    public override void AttackState(bool isHolding)
    {
        this.isHolding = isHolding;
        if(this.isHolding && throwState == ThrowState.IDLE)
        {
            throwState = ThrowState.THROW;
        }
    }
}
