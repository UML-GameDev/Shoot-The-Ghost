using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter<T> : WeaponBase
where T : ShooterData
{
    public T shooterData;
    float timeToFire;

    public Shooter() : base(WeaponType.SHOOT) { }

    protected void CheckShouldShoot()
    {
        if(isHolding && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1/shooterData.fireRate;

            Shoot();
        }
    }

    public override void AttackState(bool isHolding)
    {
        this.isHolding = isHolding;
    }
    public abstract void Shoot();
}
