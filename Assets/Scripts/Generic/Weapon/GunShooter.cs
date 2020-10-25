using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum GunState { SHOOTING, RELOADING, OUT_OF_AMMO }
public class GunShooter : Shooter<GunData>, IEquippableItem
{
    public PoolManager pm;
    float currReloadTime;
    int currAmmo;
    GunState gunState;
 
    void Start()
    {
        currAmmo = shooterData.maxAmmo;
    }

    void Update()
    {
        if(gunState == GunState.SHOOTING)
        {
            CheckShouldShoot();
        }
        else if(gunState == GunState.RELOADING)
        {
            if(currReloadTime > 0)
                currReloadTime -= Time.deltaTime;
            else
                gunState = GunState.SHOOTING;
        }
    }

    public override void Shoot()
    {

        if(currAmmo > 0)
        {

            currAmmo -= 1;

            GameObject bulletObject = pm.GetObject();
            bulletObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10));
            bulletObject.transform.position = transform.position + transform.right;  

        }
        else
            gunState = GunState.OUT_OF_AMMO;
    }
}
