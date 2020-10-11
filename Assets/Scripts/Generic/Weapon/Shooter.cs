using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter<T> : MonoBehaviour
where T : ShooterData
{   
    public T shooterData;
    float timeToFire;

    public InputManager input;

    bool isHolding;

    void OnEnable(){
        input.attackEvent += setHolding;
    }
    
    void OnDisable()
    {
        input.attackEvent = null;
    }


    protected void CheckShouldShoot()
    {
        if(isHolding && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1/shooterData.fireRate;

            Shoot();
        }
    }

    void setHolding(bool isHolding)
    {
        this.isHolding = isHolding;
    }
    public abstract void Shoot();
}
