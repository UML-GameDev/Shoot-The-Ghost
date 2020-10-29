/*
 * ThrowController
 *      ThrowController class handles the link between input and control of the throwable weapon 
 *      This class is different from weaponcontroller because it needs to take in mouseposition as part of input 
 */

public class ThrowController : InputUser
{
    Throw throwable;

    void Awake()
    {
        throwable = GetComponent<Throw>();
    }
    void OnEnable()
    {
        input.lookEvent += throwable.AimThrow;
        input.attackEvent += throwable.SetHolding;
        throwable.OnFinished.AddListener(() =>
        {
            input.attackEvent -= throwable.SetHolding;
            input.lookEvent -= throwable.AimThrow;
        });
    }

    void OnDisable()
    {
        input.attackEvent -= throwable.SetHolding;//similiar to setHolding in IShooter class
        input.lookEvent -= throwable.AimThrow;
    }
}
