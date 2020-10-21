using UnityEngine;

enum ThrowState { IDLE,CHARGING,CHARGED, OUT_OF_AMMO }
public class Throw :  WeaponBase
{
    public ThrowData throwData;
    int currCount;
    ThrowState throwState;

    //Should Use Pool Manager
    public GameObject throwObject; //must contain Throwable.cs

    //LineRenderer trajectoryLine;
    //public bool showTrajectoryLine = false;

    Vector2 maxForce;
    Vector2 rateOfCharge;
    Vector2 resultForce;
    float rate;

    public Throw() : base(WeaponType.THROWABLE) { }

    // Start is called before the first frame update
    void Start()
    {
        throwState = ThrowState.IDLE;
        currCount = throwData.maxAmmo;
        resultForce = throwData.minForce;

        rate = throwData.chargeInterval;
        rateOfCharge = new Vector2((throwData.maxForce.x - throwData.minForce.x)/throwData.chargeDuration,
                                    (throwData.maxForce.y - throwData.minForce.y) / throwData.chargeDuration);

        maxForce = throwData.maxForce;

        //if (showTrajectoryLine)
        //{
        //    trajectoryLine =  gameObject.AddComponent<LineRenderer>();
        //    trajectoryLine.positionCount = 32;
        //}
        //else trajectoryLine = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(throwState == ThrowState.CHARGING)
        {
            //charge the throw
            if (Time.time > rate)
            {
                if (resultForce.x <= maxForce.x && resultForce.y <= maxForce.y)
                {
                    resultForce += rateOfCharge;
                }
                else
                {
                    throwState = ThrowState.CHARGED;
                    //TEMP: Should remove later
                    Debug.Log("reached max charge");
                }

                //if (showTrajectoryLine)
                //{
                //    //draw trajectory line
                //    UpdateTrajectory();
                //}
                rate = Time.time + throwData.chargeInterval;
            }
        }
    }

    //void UpdateTrajectory()
    //{
    //    Vector2 start = transform.position;
    //    Vector2 end = start + resultForce.normalized * resultForce.magnitude;

    //    trajectoryLine.SetPosition(0, start);
    //    trajectoryLine.SetPosition(31,end);
    //}

    void InvokeThrow()
    {
        //Instantiate new throwObject-> TODO: UserPool Manager, will fix when working on inventory
        GameObject newObject = Instantiate(throwObject, transform.position + new Vector3(10,0,0), new Quaternion());
        Rigidbody2D rb2d = newObject.GetComponent<Rigidbody2D>();

        rb2d.AddForce(resultForce);
        resultForce = throwData.minForce;
        currCount -= 1;
        if (currCount <= 0) throwState = ThrowState.OUT_OF_AMMO;
        else throwState = ThrowState.IDLE;
    }

    public override void AttackState(bool isHolding)
    {
        this.isHolding = isHolding;
        if (this.isHolding && throwState != ThrowState.OUT_OF_AMMO)
        {
            throwState = ThrowState.CHARGING;
        }
        else if (!this.isHolding && (throwState == ThrowState.CHARGING || throwState == ThrowState.CHARGED))
        {
            InvokeThrow();
        }
    }
}
