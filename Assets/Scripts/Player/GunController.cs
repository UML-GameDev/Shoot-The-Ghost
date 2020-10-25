using UnityEngine;
/*
 * GunController
 *      GunController class handles the 
 * 
 * 
 */

public class GunController : InputUser
{
    IShooter shooter;

    void Awake()
    {
        shooter = GetComponent<IShooter>();
    }
    
    void OnEnable()
    {
        input.attackEvent += shooter.setHolding;

        shooter.OnFinished.AddListener(() =>
        {
            input.attackEvent -= shooter.setHolding;
        });
    }

    void OnDisable()
    {
        input.attackEvent -= shooter.setHolding;
    }
}
