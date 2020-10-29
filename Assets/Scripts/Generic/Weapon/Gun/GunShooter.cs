//Make sure to delete the log function after the reload animation is implemented
using UnityEngine;

enum GunState { SHOOTING, RELOADING, OUT_OF_AMMO }
public class GunShooter : Shooter<GunData>, IEquippableItem
{
    public PoolManager pm;
    private float currReloadTime;
    private float reloadTime;
    private GunState gunState;

    void Awake()
    {
        reloadTime = pm.data.reloadTime;
        currReloadTime = reloadTime;
    }
    void Update()
    {
        if (gunState == GunState.SHOOTING)
        {
            CheckShouldShoot();
        }
        else if (gunState == GunState.RELOADING)
        {
            //TODO-can use animation later and finish the reload when the animation is done
            if (currReloadTime > 0)
                currReloadTime -= Time.deltaTime;
            else
            {
                Debug.Log("Finish Reload");
                gunState = GunState.SHOOTING;
                currReloadTime = reloadTime;
                pm.Refill();
            }
            //if there's bullet in the magazine, cancel the reload
            if (isHolding && !pm.outofAmmo)
            {
                Debug.Log("Cancel reload");
                gunState = GunState.SHOOTING;
            }
        }
    }

    public override void Shoot()
    {
        if (pm.outofAmmo)
        {
            gunState = GunState.OUT_OF_AMMO;
        }
        else
        {
            GameObject bulletObject = pm.GetObject();
            bulletObject.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, Random.Range(-1, 1));
            bulletObject.transform.position = transform.position + transform.right;
        }        
    }
    public override void Reload()
    {
        if (gunState != GunState.RELOADING) Debug.Log("Reloading");
        gunState = GunState.RELOADING;
        
    }
}
