/*
 * WeaponController
 *      WeaponController class handles the link between input and control of the weapon 
 * 
 */
public class GunController : InputUser
{
    IShooter weapon;

    void Awake()
    {
        weapon = GetComponent<IShooter>();
    }
    
    void OnEnable()
    {
        input.attackEvent += weapon.SetHolding;
        input.reloadEvent += weapon.Reload;

        weapon.OnFinished.AddListener(() =>
        {
            input.attackEvent -= weapon.SetHolding;
            input.reloadEvent -= weapon.Reload;
        });
    }
}
